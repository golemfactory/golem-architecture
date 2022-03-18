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
Current payments scheme allows only to pay for tasks, after all computations are finished and Invoice is sent to Requestor.
In case of long-running tasks or services, Provider would expect, that he will be paid during
execution, because he doesn't want to wait for task finish.

## Specification

Setting `due_date` in Debit Note will cause yagna payment module to automatically pay for Debit Note,
after it was accepted. This specification describes, how Requestor and Provider should negotiate payments parameters.

### New properties

| Name | Type | Description |
| ---- | ---- | ----------- |
| `golem.com.scheme.payu.debit-note.interval-sec?` | Number (uint32) [Negotiable] | Minimum time (in seconds) between subsequent Debit Notes related to a single Activity. The Provider must not issue the next Debit Note before the interval period. The Requestor ignores Debit Notes issued before interval elapses. |
| `golem.com.scheme.payu.payment-timeout-sec?` | Number (uint32) [Negotiable] | Maximum time to receive payment for any Debit Note. This time is counted from Debit Note’s issue date. |

- Requestor can ignore Debit Note, if it came before expected interval
  - If Requestor is spammed with Debit Notes by Provider, he is allowed to break Agreement stating (in TerminateAgreement) that the **Reason** for termination is Provider's perceived misconduct. He should set `golem.requestor.code` field in **Reason** structure to `TooManyDebitNotes`.
- In the mechanism suggested above a Debit Note shall have PaymentDueDate set, and the values must be set so that they correspond with the agreed `payment-interval-sec` value.
  - Intervals are relative to the agreement timestamp
- Debit Notes are created and sent per Activity, not per Agreement, i.e. if there are two activities happening at the same time, provider should issue Debit Notes related to each Activity separately.
- Provider puts new negotiable properties in its offer; Requestor may propose different values when responding to that offer; Provider will then either accept or reject those values.
- It is up to payment driver implementation to decide, when we can treat Debit Note as paid.
  - Requestor's payment driver must ensure, that transaction will be confirmed in given timestamp. 
  - Provider can check `DebitNoteSettledEvent` timestamp to know, if Debit Note was paid in time.
    - Provider is allowed to terminate Agreement, if he didn't get payment. He should set `golem.provider.code` field in **Reason** structure to `DebitNoteNotPaid`.

### Deprecation

`golem.com.scheme.payu.interval_sec` - Replaced by negotiable `golem.com.scheme.payu.debit-note.interval-sec?`. Provider
using mid-agreement payments specification can't set `interval_sec` property.

**Note:** We encourage using `golem.com.scheme.payu.debit-note.interval-sec?` property in new implementations,
even when parties don't need mid-agreement payments.

### Impact analysis

Following modifications need to be applied to components in Golem suite:

- `yagna`
  - payment module - confirm that DebitNote `due_date` logic is implemented to ensure accepted DebitNotes are paid in time
- `ya-provider` 
  - logic to support negotiation of `golem.com.scheme.payu.debit-note.interval-sec?` and `golem.com.scheme.payu.payment-timeout-sec?` properties
  - logic of `golem.com.scheme.payu.debit-note.interval-sec?` to ensure DebitNotes are generated based on negotiated time interval
  - logic of `golem.com.scheme.payu.payment-timeout-sec?` to validate payments are made according to negotiated terms
- `yapapi`/`yajsapi` 
  - logic to support negotiation of `golem.com.scheme.payu.debit-note.interval-sec?` and `golem.com.scheme.payu.payment-timeout-sec?` properties
  - logic to ensure, that Provider doesn't overwhelm Requestor with big amount of DebitNotes
  
  
## Rationale

### Debit Note interval and payment timeout relative to Agreement approve timestamp

Debit Note creation time can't serve as a base for payment timeout, because both Provider
and Requestor can lie about timestamps of this artifact. Moreover, Provider first issues Debit Note
and then sends it to Requestor.  

We considered malicious Provider, that tries to break Agreement, not from his fault, by waiting with
sending Debit Note, and stealing this way time required for making transaction on blockchain.
The conclusion was, that we need absolute timestamp for computing payment timeout, which both parties
can agree on.

### Deprecation of `golem.com.scheme.payu.interval_sec`

This property was used by Provider for declaring the interval between Debit Notes. Since more Debit Notes means
more transactions, it is crucial for Requestor to negotiate higher intervals. On the other side Provider wants
to be paid as often as possible. That's why the choice of this value can't be left
in Provider's hands and these 2 parties have to negotiate it.

## Backwards Compatibility

- If the Agreement does not include above properties, Provider is not allowed to issue Debit Notes with `due_date` set.
- Provider using this specification can't set `golem.com.scheme.payu.interval_sec` property.

**Problem:**
If Requestors filtered Offers by `golem.com.scheme.payu.interval_sec`, they might be incompatible now,
if no value isn't handled properly.

## Test Cases
TODO:

## Reference Implementation
N/A

## Security Considerations
 
Provider can DDoS Requestor by sending Debit Notes too often, forcing him to process them.
Making Debit Note interval negotiable, we can avoid this problem, because Requestor is allowed
to break Agreement, if Provider sends to many Debit Notes.

No other security issues are known.

## Copyright
Copyright and related rights waived via [CC0](https://creativecommons.org/publicdomain/zero/1.0/).
