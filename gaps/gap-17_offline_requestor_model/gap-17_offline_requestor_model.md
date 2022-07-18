---
gap: GAP-17
title: Offline Requestor Model
description: Considerations and features required to implement Golem applications where Requestor is not required to be constantly connected to the network.
author: stranger80 (@stranger80)
status: Draft
type: Feature
---

## Abstract
TBC

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
- (TBD) Not sure how payments are controlled by a “delegate”...? Are all Payment actions allowed for Payments associated with a given Agreement?

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

#### **Feature: Self-sustained payments**
A `yagna` Payment Platform abstraction is proposed which implements the standard Payment API logic (Invoice/DebitNote issuance, Payment processing), however does not require the Requestor to be online to accept Invoices/DebitNotes. 

Such a self-sustained Payment Platform shall be implemented as standard payment platform, where Provider-side Payment API calls are not routed to Requestor node (and corresponding Agent application), but instead are handled by a party, which provides the "Accept/Reject" logic, and is able to lauch respective payments on behalf of the Reuqestor who signed the Agreement.

The "payment issuing" party can be for example:
- A "payment depositary/broker" service, which accepts prepayment from the Reuqestor, as well as instructions on how the funds can be released (which can be as simple as "release x GLM per block on bloackchain", or may include more sophisticated Invoice acceptance logic).
- A "payment channel" smart-contract on a blockchain network.

#### **Feature: Agreement Permissions Management**
TBC


## Rationale
TBC 

## Backwards Compatibility
TBC

## Test Cases
TBC

## Security Considerations
TBC 

## Copyright
Copyright and related rights waived via [CC0](https://creativecommons.org/publicdomain/zero/1.0/).