# Agreement Permissions Management - Test Scenarios

## 1. API permissions
For each APIs, the effectiveness of permissions grant/revoke must be verified - following "template" test scenarios should applied to each API.

### 1.1. Permissions grant - one Grantee positive scenario
Grant individual permission to an API to a specific Grantee node A, and run Requestor Agent process on node A to confirm the granted actions are **possible**. Run Requestor Agent process on node B to confirm the granted actions are **not possible**.

### 1.2. Permissions grant - one Grantee negative scenario
Revoke individual permission to an API to a specific Grantee node A, and run Requestor Agent process on node A to confirm the granted actions are **not possible**.


## 2. API "race conditions"
For all state-changing API calls, it must be ensured that the actual state change can only be performed by an entitled Grantee, and any subsequent state-changing call from a different Grantee will be handled gracefully (according to the ACL and the allowed state transitions).

### 2.1. Accept/Reject Invoice
Given: two Grantee nodes (A and B) with permission to call Accept/Reject Invoice

### 2.1.1. Double "AcceptInvoice" call
- An `Invoice` is issued and can be downloaded by both Grantees A and B
- Grantee A issues a `AcceptInvoice` call - with success (Invoice gets paid by Grantee A)
- Grantee B issues a `AcceptInvoice` call - with error stating that the Invoice has already been paid

### 2.1.2. "AcceptInvoice" vs "RejectInvoice" call
- An `Invoice` is issued and can be downloaded by both Grantees A and B
- Grantee A issues a `AcceptInvoice` call - with success (Invoice gets paid by Grantee A)
- Grantee B issues a `RejectInvoice` call - with error stating that the Invoice has already been paid

### 2.1.3. "RejectInvoice" vs "AcceptInvoice" call
- An `Invoice` is issued and can be downloaded by both Grantees A and B
- Grantee A issues a `RejectInvoice` call - with success (Invoice gets rejected, no payment is issued)
- Grantee B issues a `AcceptInvoice` call - with success (Invoice gets paid by Grantee B)

### 2.2. Accept/Reject DebitNote
Given: two Grantee nodes (A and B) with permission to call Accept/Reject DebitNote
### 2.2.1. Double "AcceptDebitNote" call
As per similar Invoice scenario.
### 2.2.2. "AcceptDebitNote" vs "RejectDebitNote" call
As per similar Invoice scenario.
### 2.2.2. "RejectDebitNote" vs "AcceptDebitNote" call
As per similar Invoice scenario.

### 2.3. CreateActivity
TBD

### 2.4. DestroyActivity
Given: two Grantee nodes (A and B) with permission to call DestroyActivity
### 2.4.1. Double "DestroyActivity" call
- An `Activity` is created and is visible to both Grantees A and B
- Grantee A issues a `DestroyActivity` call - with success (Activity gets terminated)
- Grantee B issues a `DestroyActivity` call - with warning saying that the Actvity has already been terminated

### 2.5. TerminateAgreement
Given: two Grantee nodes (A and B) with permission to call TerminateAgreement
### 2.5.1. Double "TerminateAgreement" call
- An `Agreement` is created and is visible to both Grantees A and B
- Grantee A issues a `TerminateAgreement` call - with success (Agreement gets terminated)
- Grantee B issues a `TerminateAgreement` call - with warning saying that the Agreement has already been terminated


## 3. Event propagation
It must be ensured that relevant API Events get propagated to all relevant permission Grantees.

### 3.1. CollectAgreementEvents - AgreementTerminatedEvent
- An `Agreement` is created and is visible to both Grantees A and B
- Grantee B begins long-polling on a `CollectAgreementEvents` call 
- Grantee A issues a `TerminateAgreement` call - with success (Agreement gets terminated)
- Grantee B receives a `AgreementTerminatedEvent`

### 3.2. GetInvoiceEvents - InvoiceReceivedEvent
- An `Agreement` is created and is visible to both Grantees A and B
- An `Activity` is launched
- Grantee B begins long-polling on a `GetInvoiceEvents` call 
- Grantee A issues a `DestroyActivity` call - with success (Activity gets terminated, Invoice gets issued by Provider)
- Grantee B receives a `InvoicReceivedEvent`

