---
gap: 24
title: Agreement Permissions Management
description: Add ability to manage and delegate rights to Agreement and all related entities.
author: stranger80 (@stranger80)
status: Draft
type: Feature
---

## Abstract
The fundamental model in Golem assumes that visibility and rights to manage an Agreement, and all related domain entities (Activity, Invoice, DebitNote, Payment) are limited to the Requestor and Provider nodes which signed this Agreement. For more complex Golem application scenarios, however, a mechanism is required whereby the permissions can be delegated to a different node/identity. This GAP includes a summary of proposed permission model, enhancements to relevant APIs and some considerations on possible implementations.

## Motivation
The main objective of this GAP is to enable "offline requestor" scenarios, where the original Requestor delegates the Agreement permissions to another node (single or a consortium of nodes) so that it can disconnect from the network. This feature is a "building block" required by "Golem Supervisor" concept, where "supervisor swarms" will need to delegate Agreement Permissions between themselves to exercise control over a "self-sustaining" Golem application. Note that the "supervisor swarms" shall require other, higher-level permissions management and consensus protocols, which are out of scope of this GAP, and will be described in a dedicated GAP.

A coherent permissions management model may also enable previously unfeasible scenarios where eg. a "broker" Requestor node negotiates Agreements with Providers and then sells them to intrested customers, who then execute the Agreement by launching Activities and accepting Invoices/DebitNotes.

## Specification
The technical specification should describe the syntax and semantics of any new feature. 

### Scope

Following entities are in scope of Agreement's permissions management: 
- Agreement
- Activity
- Invoice
- DebitNote
- Payment
- ExecBatch

All other Golem entities (as exposed by Golem low-level APIs) are not covered by Agreement Permissions mechanism.

### Roles
From the point of view of Agreement permissions, following actor roles are relevant:

- *Owner* - Requestor and Provider which signed the Agreement as a result of negotiations are always the Owners of the Agreement (and all related entities). 
- *Grantee* - any other (non-Owner) node/ID, which has been granted permissions and is included in Agreement's Access Control List.

### Permissions catalogue

A **permission** indicates an ability to perform a specific action which refers to an Agreement or any related entity. For the purposes of rights management, the permissions are indicated via literals, which include permission's category and specific action - where actions map to respective low-level Golem REST API operations, as implemented. Following table summarizes permissions which are covered by this GAP:

|Permission|Comments|
|-|-|
| req:getAgreement | |
| req:getAgreementPermissions | |
| req:collectAgreementEvents | |
| req:getActivityState | |
| req:getActivityUsage | |
| req:getRunningCommand | |
| req:getExecBatchResults | |
| req:getDebitNotes | |
| req:getDebitNote | |
| req:getPaymentsForDebitNote | |
| req:getDebitNoteEvents | |
| req:getInvoices | |
| req:getInvoice | |
| req:getPaymentsForInvoice | |
| req:getInvoiceEvents | |
| req:getPayments | |
| req:terminateAgreement | |
| req:setAgreementPermissions | See: [Transitive permissions](#transitive-permissions) below |
| req:createActivity | |
| req:exec | |
| req:callEncrypted | |
| req:destroyActivity | |
| req:acceptDebitNote | See: [Accept Invoice and Debit Note logic](#accept-invoice-and-debit-note-logic) below |
| req:rejectDebitNote | |
| req:acceptInvoice | See: [Accept Invoice and Debit Note logic](#accept-invoice-and-debit-note-logic) below |
| req:rejectInvoice | |

If a permission to a specific REST API action on a given Agreement is granted to a Grantee, this Grantee is allowed to make respective REST API call referring to the Agreement (or related entity). 

**Note:** this GAP deliberately leaves Provider-side action permissions out of scope.

### Access Control List

An Agreement shall have an *Access Control List* (ACL) associated with it, to indicate all rights granted to any non-Owner node/ID.

An indicative example of an Agreement's ACL is shown below:

```
agreementACL = {
    version: 123,
    aclEntries: [
        {
            "grantee": "0x7735df0c0bbb3ca65c55d61eadded653573e0152",
            "permissions" : [
                "req:createActivity",
                "req:exec",
                "req:destroyActivity"
            ]
        },
        ...
        {
            "grantee": "0x0053b2c00006d1d8b75bb7dfa068990a19e4c32e",
            "permissions" : [
                "req:terminateAgreement",
                "req:acceptInvoice",
                "req:rejectInvoice"
            ]

        }
    ]
}
```

**Note:** the ACL includes a `version` attribute - this attribute is required to prevent accidental ACL overwrite "races" - see **SetAgreementPermissions** method below.

### MarketAPI: SetAgreementPermissions

A new method shall be added in Market API to enable setting of an Agreement's ACL.

**Who:**
- Requestor

**Input:**
- agreementId
- new ACL content

**Output:**
- OK
- Error 

#### "Race condition" prevention

Note that the ACL artifact includes a `version` attribute, which shall meet following conditions:
- If `setAgreementPermissions` is called with a `version` attribute value matching the latest recorded ACL version - the ACL update is accepted, and the recorded `versin` is incremented.
- If `setAgreementPermissions` is called with a `version` attribute value lower than the latest recorded ACL version - the method shall return Error with detailed message similar to 'newer version exists'.


### MarketAPI: GetAgreementPermissions

A new method shall be added in Market API to enable retrieving an Agreement's ACL.

**Who:**
- Requestor

**Input:**
- agreementId

**Output:**
- OK + Agreement's current ACL 
- Error 

### Accept Invoice and Debit Note logic

**Note** that one of the parameters required for `AcceptDebitNote`/`AcceptInvoice` Payment API calls is an `allocationId`. The ACL allows for delegation of rights to an Agreement and associated entities, but there is no equivalent rights management for funds managed/owned by Agreement Owner. 

Therefore a rights Grantee, who is willing to Accept an incoming Debit Note or Invoice must pass `allocationId` of their own Allocation.

### Transitive permissions

**Note** granting access to `req:setAgreementPermissions` effectively allows grantee to delegates access rights further. This implies transitivity of permissions.

### Implementation considerations

To implement the Agreement Permissions Management it is necessary to introduce permission control in yagna REST API. However there is a number of possible ways to manage and 'guard' the Agreement's ACL.

1. ACL managed and exerted on Provider node

In this model, the ACL is persisted and managed on the Provider node indicated by the Agreement. Therefore all actions resulting from REST API calls must be routed to Provider node, and the permissionsneed to be validated there. Any attempt to violate the permissions as indicated by the ACL must result with "permissions violation" error returned to the calling node (and respective REST API caller).

The benefit of this model is the fact that ACL is managed and verified in one place, which limits the possibility of race conditions, therefore increasing the robustness of implementation.

The drawback is - all actions must be routed to the Provider for validation, and that increases delay before a REST API call result is returned to the caller. 

2. ACL distributed and exerted on Requestor and Grantee nodes

In this model, the ACL is managed by the Provider node indicated by the Agreement, however each change in the ACL is distributed to all Grantee nodes. This allows for the permission verification to happen on Grantee nodes.

The benefit is - immediate verification of permissions, as each Grantee node can verify the permissions against their copy of the ACL.

The drawback is the risk of local ACL copies going out of sync versus theProvider's master-copy. This introduces a risk of race conditions, and generally reduces the security level of the mechanism.

**Recommendation:** Implementation 1. 

## Rationale

Permission management enhancements are to be applied on Market API only, as only Agreement ACLs are to be managed. An alternative is a broad, generic Permissions API, but it seems overkill.


## Backwards Compatibility
The ACL mechanism does not impact the actions allowed for Agreement Owners. This implies that all Requestor Agent modules designed for a model where owning Requestor is the only identity calling Requestor-side APIs will continue to work successfully.

Question: do we need some mechanism for compatibility of Requestor Agent apps vs Requestor yagna daemons? Daemon capability list (there was a GAP for that)?


## Test Cases
Test cases for this GAP must include the following segments:
1. Grant individual permissions to a specific Grantee node, and run Requestor Agent process to confirm the granted actions are possible on the REST API. Ensure that actions to which permissions hadn't been granted - are not available.
2. Grant individual permissions to a specific Grantee node A, and run Requestor Agent process against node B to ensure the actions are not available to node B.

## Security Considerations
This GAP is all about security and permissions. It is recommended to obtain an external security review/pen test for the implementation.

## Copyright
Copyright and related rights waived via [CC0](https://creativecommons.org/publicdomain/zero/1.0/).