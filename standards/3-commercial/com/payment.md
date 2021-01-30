# Payment
Namespace with properties defining payment parameters. 

## `golem.com.payment.chosen-platform : String`

### Describes: Demand/Offer

Payment Platform to be used for settlement of the Agreement.

### Value enum
| Value     | Description                                          |
| --------- | ---------------------------------------------------- |
| "zksync-rinkeby-tglm" | L2 off-chain **tGLM** on zkSync rinkeby  |
| "zksync-mainnet-glm" | L2 off-chain **GLM** on zkSync mainnet  |
| "erc20-rinkeby-tglm"  | L1 on-chain **tGLM** ERC-20 on Ethereum rinkeby |
| "erc20-mainnet-glm"  | L1 on-chain **GLM** ERC-20 on Ethereum mainnet |
|           |                                                      |

### **Examples**
* `golem.com.payment.chosen-platform="erc20-mainnet-glm"` - specifies ERC-20 plain Ethereum as payment platform.

## `golem.com.payment.debit-notes.accept-timeout? : Number (int32)` [[Negotiable]](/standards/README.md#negotiable-properties)

### Describes: Demand/Offer

Indicates the timeout period (in seconds) for the Requestor to accept incoming Debit Notes.

### Negotiation protocol

This property is *negotiable*, ie. both Requestor and Provider place it in the Demand/Offer in order to agree the value of the property they both intend to adhere to. The "negotiation" can be initiated by either side, and ends when two subsequent Demand/Offer Proposals include the same value of this property.

The semantics of the property is as follows:

**Provider** - expects, that the Requestor will accept every received Debit Note within the specified timeout period after the Debit Note creation `timestamp`. If this property is not set in the Offer, the Provider will not check Debit Note acceptance timeouts.

**Requestor** - declares that he will accept Debit Notes within specified time after they are created (as indicated by `timestamp` field in Debit Note struct). If this property is not set in the Demand, the Requestor will not attempt to accept the incoming Debit Notes within specified timeline (though they may still accept the Debit Note later).

#### Notes

The `golem.com.payment.debit-notes.accept-timeout?` property is a way for the Provider to verify the Requestor is still 'active' and therefore can be expected to pay for the Agreement. Therefore the Provider may expect a different value of Agreement `golem.srv.comp.expiration` (in Demand), depending on whether the Requestor supports Debit Note accept timeout or not. If the Requestor doesn't set property, the Provider will remove his property and compare Requestor's expiration to a lower limit. If Requestor supports Debit Note accept timeout, the Provider uses a higher Agreement expiration limit.

During negotiation the Provider will adjust `golem.com.payment.debit-notes.accept-timeout?`, if Requestor's deadline is lower than Provider's. If deadline is higher, Provider rejects such a Proposal.

## `golem.com.payment.platform.<platform name>.address : String`

### Describes: Demand/Offer

The address of GNT payment receiver (Provider) for indicated payment platform.


## Payment platform negotiation convention

The semantics of `golem.com.payment.platform` namespace include the negotiation convention & patterns. 

To ensure consistency and non-repudiability of Agreements, the Agreement negotiation must lead to confirmation of the "Negotiable" properties by both sides. As the actual payment platform to be used for Agreement is a negotiable property, the Demand/Offer exchange must converge to situation where both parties indicate the agreed payment platform in their Demand/Offer.

An example negotiation scenario:

1. Provider publishes an open Offer with multiple payment platform options 
```
Offer 1

Properties:
golem.com.payment.platform.erc20-mainnet-glm.address = "0xdeadbeef"
golem.com.payment.platform.zksync-mainnet-glm.address = "0xdeadbeef"
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
(golem.com.payment.platform.erc20-mainnet-glm.address=*)
```

3. Requestor formulates a counter-Proposal for `Offer 1`, where it indicates selected payment platform
```
Demand 2 (Proposal)

Properties:
golem.com.payment.chosen-platform = "erc20-mainnet-glm"
```

4. Provider responds with a counter-Offer, where it confirms the selected payment platform
```
Offer 2 (Proposal)

Properties:
golem.com.payment.chosen-platform = "erc20-mainnet-glm"
golem.com.payment.platform.erc20-mainnet-glm.address = "0xdeadbeef"
```


