# Payment
Namespace with properties defining payment parameters. 

## `golem.com.payment.chosen-platform : String`

### Describes: Demand/Offer

Payment Platform to be used for settlement of the Agreement.

### Value enum
| Value                              | Description                                       |
|------------------------------------|---------------------------------------------------|
| "zksync-rinkeby-tglm" (deprecated) | L2 off-chain **tGLM** on zkSync rinkeby           |
| "zksync-mainnet-glm" (deprecated)  | L2 off-chain **GLM** on zkSync mainnet            |
| "erc20-rinkeby-tglm" (deprecated)  | L1 on-chain **tGLM** ERC-20 on Ethereum rinkeby   |
| "erc20-goerli-tglm" (deprecated)   | L1 on-chain **tGLM** ERC-20 on Ethereum goerli    |
| "erc20-holesky-tglm"               | L1 on-chain **tGLM** ERC-20 on Ethereum holesky   |
| "erc20-polygon-glm"                | L2 on-chain **GLM** ERC-20 on Polygon POS Network |
| "erc20-mainnet-glm"                | L1 on-chain **GLM** ERC-20 on Ethereum mainnet    |
|                                    |                                                   |

### **Examples**
* `golem.com.payment.chosen-platform="erc20-mainnet-glm"` - specifies ERC-20 plain Ethereum as payment platform.

## `golem.com.payment.debit-notes.accept-timeout? : Number (int32)` [[Negotiable]](/standards/README.md#fact-vs-negotiable-properties)

### Describes: Demand/Offer

Indicates the timeout period (in seconds) for the Requestor to accept incoming Debit Notes.

### Negotiation protocol

This property is *negotiable*, ie. both Requestor and Provider place it in the Demand/Offer in order to agree the value of the property they both intend to adhere to. The "negotiation" can be initiated by either side, and ends when two subsequent Demand/Offer Proposals include the same value of this property.

The semantics of the property is as follows:

**Provider** - expects, that the Requestor will accept every received Debit Note within the specified timeout period after the Debit Note creation `timestamp`. If this property is not set in the Offer, the Provider will not check Debit Note acceptance timeouts.

**Requestor** - declares that he will accept Debit Notes within specified time after they are created (as indicated by `timestamp` field in Debit Note struct). If this property is not set in the Demand, the Requestor will not attempt to accept the incoming Debit Notes within specified timeline (though they may still accept the Debit Note later).

#### Notes

The `golem.com.payment.debit-notes.accept-timeout?` property is a way for the Provider to verify the Requestor is still 'active' and therefore can be expected to pay for the Agreement. Therefore the Provider may choose different strategies, depending on whether the Requestor supports this mechanism. For example, the Provider's approach to Agreement's expiration, `golem.srv.comp.expiration` (in Demand), may differ as follows: if the Requestor doesn't set property, the Provider will remove his property and compare Requestor's expiration to a lower limit. If Requestor supports Debit Note accept timeout, the Provider uses a higher Agreement expiration limit.

During negotiation the Provider will adjust `golem.com.payment.debit-notes.accept-timeout?`, if Requestor's deadline is lower than Provider's. If deadline is higher, Provider rejects such a Proposal.

## `golem.com.payment.platform.<platform name>.address : String`

### Describes: Demand/Offer

The address of GLM payment receiver (Provider) for indicated payment platform.

## Payment platform selection convention

The semantics of `golem.com.payment.platform` namespace include the platform selection convention & patterns. 

An example selection scenario:

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
(golem.com.payment.protocol.version>1)
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

## `golem.com.payment.protocol.version: Number (int32)`

### Describes: Demand/Offer

## Specifying protocol version convention

```
Demand constraint example:

Constraints:
(golem.com.payment.protocol.version>1)
```

```
Demand 2 (Proposal)

Properties:
golem.com.payment.protocol.version = 2
```

4. Provider responds with a counter-Offer, where it confirms the selected protocol version
```
Offer 2 (Proposal)

Properties:
golem.com.payment.protocol.version = 2
```

Payment protocol version spec:

[Payment Version Spec](../../../spec/payment_version.md)


