# Payment
Namespace with properties defining payment parameters. 

## `golem.com.payment.chosen-platform : String [Negotiable]`

### Describes: Demand/Offer

Payment Platform to be used for settlement of the Agreement.

### Value enum
| Value     | Description                                          |
| --------- | ---------------------------------------------------- |
| "ngnt"    | Golem new GNT ERC-20 token on plain Ethereum         |
| "zk-ngnt" | Golem new GNT zk-sync ERC-20 token on plain Ethereum |
|           |                                                      |

## `golem.com.payment.platform.ngnt.address : String [Fact]`

### Describes: Demand/Offer

The address of GNT payment receiver (Provider) for **plain GNT platform**.

## `golem.com.payment.platform.zk-ngnt.address : String [Fact]`

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
golem.com.payment.platform.ngnt.address = "0xdeadbeef"
golem.com.payment.platform.zk-ngnt.address = "0xdeadbeef"
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
(golem.com.payment.platform.ngnt.address=*)
```

3. Requestor formulates a counter-Proposal for `Offer 1`, where it indicates selected payment platform
```
Demand 2 (Proposal)

Properties:
golem.com.payment.chosen-platform = "ngnt"
```

4. Provider responds with a counter-Offer, where it confirms the selected payment platform
```
Offer 2 (Proposal)

Properties:
golem.com.payment.chosen-platform = "ngnt"
golem.com.payment.platform.ngnt.address = "0xdeadbeef"
```

5. Requestor creates Agreement proposal from `Demand 2` and `Offer 2`, where `golem.com.payment.platform = "ngnt"` is repeated by both sides. 
```
Agreement (Proposal)

Demand

Properties:
golem.com.payment.chosen-platform = "ngnt"

Offer

Properties:
golem.com.payment.chosen-platform = "ngnt"
golem.com.payment.platform.ngnt.address = "0xdeadbeef"

```


### **Examples**
* `golem.com.payment.chosen-platform="ngnt"` - specifies ERC-20 plain Ethereum as payment platform.
