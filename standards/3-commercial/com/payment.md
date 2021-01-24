# Payment
Namespace with properties defining payment parameters. 

## `golem.com.payment.chosen-platform : String`

### Describes: Demand/Offer

Payment Platform to be used for settlement of the Agreement.

### Value enum
| Value     | Description                                          |
| --------- | ---------------------------------------------------- |
| "NGNT"    | Golem new GNT ERC-20 token on plain Ethereum         |
| "ZK-NGNT" | Golem new GNT zk-sync ERC-20 token on plain Ethereum |
|           |                                                      |

## `golem.com.payment.debit-note.accept-timeout-negotiable : Number (int32) [Negotiable]` 

### Describes: Demand/Offer

Indicates the timeout period for the Requestor to accept incoming Debit Notes.

### Negotiation protocol

This property is *negotiable*, ie. both Requestor and Provider place it in the Demand/Offer in order to agree the value of the property they both intend to adhere to. The "negotiation" can be initiated by either side, and ends when two subsequent Demand/Offer Proposals include the same value of this property.

*Provider* - expects, that the Requestor will accept every received Debit Note within the specified timeout period after the Debit Note has been received by the Requestor. When this property is not set in the Offer, this indicates the Provider will not check Debit Note acceptance timeouts.
*Requestor* - declares that he will accept Debit Notes within specified time after they are received. When this property is not set in the Demand, this indicates the Requestor will not attempt to accept the incoming Debit Notes within specified timeline (though they may still accept the Debit Note later).

The `golem.com.payment.debit-note.acceptance-timeout-negotiable` property is a way for the Provider to verify the Requestor is still 'active' and therefore can be expected to pay for the Agreement. Therefore the Provider may expect different value of Agreement expiration (in Demand), depending on whether the Requestor supports DebitNote accept timeout or not. If the Requestor doesn't set property, Provider will remove his property and compare Requestor's expiration to lower limit. If Requestor supports Debit Note accept timeout, the Provider uses higher Agreement expiration limit.

During negotiation the Provider will adjust `golem.com.payment.debit-note.acceptance-timeout-negotiable`, if Requestor's deadline is lower than Provider's. If deadline is higher, Provider rejects such a Proposal.

### **Examples**
* `golem.inf.cpu.cores=4`



## `golem.com.payment.platform.NGNT.address : String`

### Describes: Demand/Offer

The address of GNT payment receiver (Provider) for **plain GNT platform**.

## `golem.com.payment.platform.ZK-NGNT.address : String [Fact]`

### Describes: Demand/Offer

The address of GNT payment receiver (Provider) for **zk-GNT platform**..

## Payment platform negotiation convention

The semantics of `golem.com.payment.platform` namespace include the negotiation convention & patterns. 

To ensure consistency and non-repudiability of Agreements, the Agreement negotiation must lead to confirmation of the "Negotiable" properties by both sides. As the actual payment platform to be used for Agreement is a negotiable property, the Demand/Offer exchange must converge to situation where both parties indicate the agreed payment platform in their Demand/Offer.

An example negotiation scenario:

1. Provider publishes an open Offer with multiple payment platform options 
```
Offer 1

Properties:
golem.com.payment.platform.NGNT.address = "0xdeadbeef"
golem.com.payment.platform.ZK-NGNT.address = "0xdeadbeef"
```

2. Requestor publishes a Demand with no constraints on payment platform (it wants to choose from Offers it receives)

```
Demand 1

Constraints:
()
```
...and from the market matching, it receives `Offer 1`.

(alternatively, the Demand may include constraints to filter only the preferred payment platform: )
```
Demand 1a

Constraints:
(golem.com.payment.platform.NGNT.address=*)
```

3. Requestor formulates a counter-Proposal for `Offer 1`, where it indicates selected payment platform
```
Demand 2 (Proposal)

Properties:
golem.com.payment.chosen-platform = "NGNT"
```

4. Provider responds with a counter-Offer, where it confirms the selected payment platform
```
Offer 2 (Proposal)

Properties:
golem.com.payment.chosen-platform = "NGNT"
golem.com.payment.platform.ngnt.address = "0xdeadbeef"
```

5. Requestor creates Agreement proposal from `Demand 2` and `Offer 2`, where `golem.com.payment.platform = "NGNT"` is repeated by both sides. 
```
Agreement (Proposal)

Demand

Properties:
golem.com.payment.chosen-platform = "NGNT"

Offer

Properties:
golem.com.payment.chosen-platform = "NGNT"
golem.com.payment.platform.ngnt.address = "0xdeadbeef"

```

### **Examples**
* `golem.com.payment.chosen-platform="NGNT"` - specifies ERC-20 plain Ethereum as payment platform.
