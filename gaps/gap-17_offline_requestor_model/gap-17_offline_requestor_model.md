---
gap: GAP-17
title: Offline Requestor Model
description: Considerations and features required to implement Golem applications where Requestor is not required to be constantly connected to the network.
author: stranger80 (@stranger80)
status: Draft
type: Feature
---

## Abstract
Golem Network is a space where Requestors and Providers interact to trade computing resources in exchange for crypto-tokens. Such collaboration may not require constant connection between Requestor and Providers of the resources. The connectivity may also be disrupted by such events as machine failures & outages, network issues, etc. This article considers a number of scenarios where Requestor is not required to remain constantly online, and introduces a number of features required to support such scenarios in Golem ecosystem. 

## Motivation
The features indicated or referenced in this GAP are intended to meet following objectives:
- Improve application resilience to network & requestor node failures
- Enable "fire&forget", and self-sustaining application models

## Specification
Three "Offline Requestor" scenarios are considered in this GAP. 

#### **A. Requestor partially connected**
Characteristics:
- Requestor daemon remains online until Agreement is signed and Activity started
- Requestor daemon may then disconnect while the Activity is in progress
- Requestor daemon may reconnect at any time, and exercise control over the Activity via ExeScript

**Notes:**
- This scenario is only possible with payment schemes which:
  * either assume upfront payment, 
  * or allow long intervals between payments,
  * or are self-sustained and not depending on the presence of the daemon.
 
#### **B. Requestor offline ("fire&forget")** 
Characteristics:
- Requestor daemon remains online until Agreement is signed and Activity started
- Requestor daemon disconnects permanently, while the Activity continues (probably until agreed computation is complete, or funds run out)

**Notes:**
- This scenario is only possible with payment schemes which:
  * either assume upfront payment, 
  * or allow long intervals between payments,
  * or are self-sustained and not depending on the presence of the daemon.
 
#### **C. Requestor delegates control**
Characteristics:
- Requestor daemon remains online until Agreement is signed and Activity started
- Requestor daemon transfers grants control rights to another node/identity, which remains online and exercises control over the Activity 

**Notes:**
- This requires **Agreement Permissions Management** feature

### Proposed features

This GAP introduces following features which are aimed at enabling the "offline Reuqestor" scenarios listed above:
- Activity Attach/Detach
- Self-sustained Payments
- Agreement Permissions Management

The mapping between the scenarios and features required to implement them is indicated below:

| | Activity attach/detach | Self-sustained payments | Agreement Permissions Management |
|-|-|-|-|
| Requestor partially connected     | X | X | |
| Requestor offline ("fire&forget") | X | X | |
| Requestor delegates control       | X | | X |
|        |  | |  |

#### **Feature: Activity attach/detach**
We propose to introduce an ability for Requestor node to disconnect from the network while controlled Activity remains active, and then gracefully reconnect, and take control of the Activity. **Note** this can happen intentionally, or as a result of eg. a network failure or software error. Therefore the ability to attach a Requestor to resume control over an Activity is improving the reliability and robustness of Golem as a platform. 

The implementation of this feature requires considerations on two levels:

1. Requestor Agent application goes offline, while corresponding `yagna` daemon remains online 
    - The HL API implementations shall include the ability to obtain a "live" instance of Activity object and corresponding Agreement object based on `activityID`, and then perform actions on them (ExeScript command execution and results processing, Agreement control, including termination), as in the following pseudo-code example:
       ```
       ...
       activity = Golem.attach_activity(activityId)   # returns an "attached" Activity object, which can then be used to manipulate the Activity

       exeScriptResult = activity.exec(exeScript)     # Activity can receive ExeScript commands, etc.

       activity.agreement.terminate(reason)           # ...eventually the corresponding Agreement can be terminated
       ...
       ```
    - Persisting the information about running Activities to which an HL API caller may "attach" is out of scope for HL API implementation, ie. it is the designer of the Requestor Agent application, whois responsible for eg. persisitng the obtained `activityIDs`, in order to be able to "attach" to Activities which are "in-flight".   
    - TBC decide how to approach the Task and Service objects.

2. Requestor `yagna` daemon goes offline and should have ability to reconnect to network
    - The daemon may disconnect from the network, and the current state of the Golem node (incuding Demands/Offers, Agreements, Acitivites, etc.) should be persisted.
    - The daemon may re-connect to the network, and it should be capable of synchronizing the persisted state with the actual state of relevant entities on the network (eg. update the actual state of Agreements, Activities, respective Invoices/DebitNotes, events, etc.)
    - After the reconnecting and successful synchronization, the `yagna` APIs should continue to work as if there was no offline period. 

**'Detach' and in-flight operations**

It shall be acceptable for 'detach' to happen while there are API calls in-flight. 

For long-polling API calls (eg. `collectDemands`/`collectOffers`, `getExecBatchResults`) the 'detach' should break the running API calls. 
- For scenario when Agent application goes offline - the daemon implementation shall persist the relevant data structures, so that after subsequent 'attach' the Agent may query for data received by the deamon while the Agent remained offline.
- For scenario when daemon goes offline - the Golem network protocol shall ensure that relevant data objects are persisted on sender side, and delivered after the daemon subsequently 'attaches' itself.

**Note:** The `getExecBatchResults` API called in `text/event-stream` ('streaming') mode will not attempt to persist the undelivered stream content while calling Agent/daemon remains offline. The same API called in `application/json` ('non-streaming') mode will operate as described above.

#### **Feature: Self-sustained payments**
A `yagna` Payment Platform abstraction is proposed which implements the standard Payment API logic (Invoice/DebitNote issuance, Payment processing), however does not require the Requestor to be online to accept Invoices/DebitNotes. 

Such a self-sustained Payment Platform shall be implemented as standard payment platform, where Provider-side Payment API calls are not routed to Requestor node (and corresponding Agent application), but instead are handled by a party, which provides the "Accept/Reject" logic, and is able to lauch respective payments on behalf of the Reuqestor who signed the Agreement.

The "payment issuing" party can be for example:
- A "payment depositary/broker" service, which accepts prepayment from the Reuqestor, as well as instructions on how the funds can be released (which can be as simple as "release x GLM per block on bloackchain", or may include more sophisticated Invoice acceptance logic).
- A "payment channel" smart-contract on a blockchain network.

**In general,** the self-sustained Payment mechanism shall be expressed in Demand&Offer via dedicated, standardized properties from `golem.com.payment` property namespace. This is important, as a specific payment platform & scheme are required to ensure compatibility between Requestor and Provider nodes to support a specific "offline Requestor" scenario.

#### **Feature: Agreement Permissions Management**
In order to support scenarios where control delegation from Requestor to a different Golem node is performed, the Golem APIs must include a concept of permissions and grants. 

This feature is described in a dedicated [GAP-24](). 

## Rationale
The proposed behaviour of 'attach/detach' feature is driven by intent to increase robustness of Golem platform, and expand the space of supported usage scenarios, but also taking into account the state of existing `yagna` implementation. It is assumed that the implementaion of the proposed feature logic would not imply substantial redesign of Golem network protocols & implementations.

## Backwards Compatibility
Backwards compatibility can be considered separately for each feature proposed.
### Activity attach/detach

#### 1. Requestor Agent application goes offline - `yagna` daemon remains online
This scenario is implemented only on Agent application level (so in HL API library), so backwards compatibility is ensured between a "new" HL API library implementation and "legacy" `yagna` daemon implementation (as no `yagna` REST API changes are required).

#### 2. Requestor `yagna` daemon goes offline
**IMPORTANT** Will current Golem net implementations (eg. "hybrid net") support a scenario where `yagna` daemon is reconnecting to network after downtime?

### Self-sustained payments
As self-susteined payment mechanisms are to be provided by specific, distinguished payment platform & scheme implementations, the Golem nodes and Agent applications will ensure compatibility by indicating respective payment conditions via properties & constraints in Demands & Offers. Therefore only nodes which support self-sustained payments will enter an Agreement, so backwards compatibility will be ensured.

### Agreement Permissions Management
See relevant [GAP-24]() for details.

## Test Cases
Indicative test scenario suite is available [here](gap-17_test_cases.md). 

## Security Considerations
Considerations must be given to all vulnerabilities which may result from a fact that the original Requestor (who signed the Agreement) disconnects from the network, leaving the Provider 'unattended'. A malicious 'usurper' may be tempted to disguise as the original Requestor to gain control over the in-flight Agreements&Activites. 
It seems mandatory to ensure the communciation security (ie. message integrity and authentication) is achieved on Golem Net level - so that the Golem Net transport layer ensures the identity of the sender of data over the network.

## Copyright
Copyright and related rights waived via [CC0](https://creativecommons.org/publicdomain/zero/1.0/).