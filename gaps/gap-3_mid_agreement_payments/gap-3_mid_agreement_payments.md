---
gap: 3
title: Mid Agreement payments
description: Specification of Agreement standard for negotiating and describing continues payments during task execution.  
author: nieznany.sprawiciel (@nieznanysprawiciel), stranger80 (@stranger80), shadeofblue (@shadeofblue)
status: Draft
type: Feature
---

## Abstract
Introduces Agreement properties standard to allow negotiating incremental payments during task execution.

## Motivation
The current payments scheme allows only to pay for tasks, after all computations are finished and Invoice is sent to the Requestor.

In case of long-running tasks or services, Provider would expect that they will be paid during the activity's execution, because they don't want to wait for the task to finish before they have a chance to receive any payment for it.

## Specification

Setting `payment_due_date` in a Debit Note will cause the yagna payment module on the requestor's end to automatically pay the Debit Note, after it is accepted.

Also, because of the current limitations of the yagna daemon's payment driver, the payment will be issued immediately after such acceptance.

This specification describes the semantics of the required payment parameters and how Requestor and Provider should agree on them.


### New properties

| Name | Type | Description |
| ---- | ---- | ----------- |
| `golem.com.scheme.payu.debit-note.interval-sec?` | Number (uint32) [Negotiable] | Minimum time (in seconds) between subsequent Debit Notes related to a single Activity. The Provider must not issue the next Debit Note before the interval period. The Requestor ignores Debit Notes issued before interval elapses and terminates the offending agreement. |
| `golem.com.scheme.payu.payment-timeout-sec?` | Number (uint32) [Negotiable] | Maximum time to receive payment for any Debit Note. This time is counted from Debit Note’s issue date. At the same time, this is the minimum interval (in seconds) between two subsequent payable Debit Notes (Debit Notes with `payment_due_date` property set) related to a single Activity. The Provider must not issue a next payable Debit Note before this interval elapses. The Requestor ignores any offending Debit Notes and terminates the agreement. |

- Requestor can ignore Debit Note, if it came before expected interval
  - If the Requestor receives Debit Notes more often than the interval specified by the agreed `golem.com.scheme.payu.debit-note.interval-sec?`, they are allowed to break the Agreement stating (in TerminateAgreement) that the **Reason** for termination is Provider's perceived misconduct. They should set the `golem.requestor.code` field in **Reason** structure to `TooManyDebitNotes`.
  - Normally, the sent Debit Notes serve as an information of the accumulated costs and as a keep-alive message to ensure both sides that the agreement is still in action. 
- To make use of the negotiated mid-agreement payments capability, the Provider may periodically send payable Debit Notes (Debit Notes that have the `payment_due_date` attribute set to a non-empty value) to the requestor.
  - Because of a current limitation of yagna's payment driver, any accepted Debit Notes with `payment_due_date` set will be paid immediately regardless of the actual value of that field. Therefore, the Provider is not allowed to send such payable Debit Notes sooner than the time specified in the agreed `golem.com.scheme.payu.payment-timeout-sec?` property elapses since the last payable Debit Note - or - since the start of the activity if it's the first payable Debit Note.
  - If the Requestor receives payable Debit Notes more often that the interval specified by `golem.com.scheme.payu.payment-timeout-sec?`, they are allowed to break the Agreement, stating Provider's misconduct as the termination reason **Reason**. In this case, the `golem.requestor.code` field in the **Reason** structure should be set to `TooManyPayableDebitNotes`.
- Both intervals are relative to the agreement creation timestamp
- Debit Notes are created and sent per Activity, not per Agreement, i.e. if there are two activities happening at the same time, provider should issue Debit Notes related to each Activity separately.
- Provider puts new negotiable properties in its offer; Requestor may propose different values when responding to that offer; Provider will then either accept or reject those values.
- It is up to payment driver implementation to decide, when we can treat Debit Note as paid.
  - Requestor's payment driver must ensure, that transaction will be confirmed within the agreed time. 
  - Provider can check `DebitNoteSettledEvent` timestamp to know, if Debit Note was paid in time.
    - Provider is allowed to terminate Agreement, if they don't receive the payment. They should set the `golem.provider.code` field in **Reason** structure to `DebitNoteNotPaid`.

### Deprecation

`golem.com.scheme.payu.interval_sec` - Replaced by negotiable `golem.com.scheme.payu.debit-note.interval-sec?`. Provider
using mid-agreement payments specification can't set `interval_sec` property.

**Note:** We encourage using `golem.com.scheme.payu.debit-note.interval-sec?` property in new implementations,
even when parties don't need mid-agreement payments.

### Impact analysis

Following modifications need to be applied to components in Golem suite:

- `yagna`
  - payment module - confirm that DebitNote `payment_due_date` logic is implemented to ensure accepted DebitNotes are paid in time
- `ya-provider` 
  - logic to support negotiation of `golem.com.scheme.payu.debit-note.interval-sec?` and `golem.com.scheme.payu.payment-timeout-sec?` properties
  - logic of `golem.com.scheme.payu.debit-note.interval-sec?` to ensure DebitNotes are generated based on negotiated time interval
  - logic of `golem.com.scheme.payu.payment-timeout-sec?` to validate payments are made according to negotiated terms
- `yapapi`/`yajsapi` 
  - logic to support negotiation of `golem.com.scheme.payu.debit-note.interval-sec?` and `golem.com.scheme.payu.payment-timeout-sec?` properties
  - logic to ensure, that Provider doesn't overwhelm Requestor with big amount of DebitNotes or DebitNotes that require them to pay too often (and thus incur larger-than-acceptable gas costs).
  
  
## Rationale

### Debit Note interval and payment timeout relative to Agreement approve timestamp

Debit Note creation time can't serve as a base for payment timeout, because both Provider
and Requestor can lie about timestamps of this artifact. Moreover, Provider first issues Debit Note
and then sends it to Requestor.  

We considered malicious Provider, that tries to break the Agreement, not from his fault, by waiting with
sending Debit Note, and stealing this way time required for making transaction on blockchain.
The conclusion was, that we need absolute timestamp for computing payment timeout, which both parties
can agree on.

### Deprecation of `golem.com.scheme.payu.interval_sec`

This property was used by Provider for declaring the interval between Debit Notes. Since more Debit Notes means
more transactions, it is crucial for Requestor to negotiate higher intervals. On the other side Provider wants
to be paid as often as possible. That's why the choice of this value can't be left
in Provider's hands and these 2 parties have to negotiate it.

## Backwards Compatibility

- If the Agreement does not include above properties, Provider is not allowed to issue Debit Notes with `payment_due_date` set.
- Provider using this specification can't set `golem.com.scheme.payu.interval_sec` property.

**Problem:**
If Requestors filtered Offers by `golem.com.scheme.payu.interval_sec`, they might be incompatible now,
if no value isn't handled properly.

## Test Cases
TODO:

## Reference Implementation
N/A

## Security Considerations
 
Provider can DDoS a Requestor by sending Debit Notes too often, incurring the processing overhead on them.
By making the Debit Note interval negotiable, we can avoid this problem, because the Requestor is allowed
to break the Agreement, if the Provider sends to many Debit Notes.

No other security issues are known.

## Copyright
Copyright and related rights waived via [CC0](https://creativecommons.org/publicdomain/zero/1.0/).
