# Ethereum 
This namespace defines properties describing Ethereum-related applications & services.

## Common Properties

* [golem.inf](../../../0-commons/golem/inf.md)
* [golem.activity](../../../0-commons/golem/activity.md)

## Specific Properties

## `golem.srv.app.eth.network : String` 

### Describes: Demand

For Ethereum node hosting services - indicates the Ethereum network that the Geth is requested to connect to.

### Value enum
|Value| Description |
|---|---|
|"mainnet"| Ethereum Mainnet |
|"rinkeby"| Ethereum Rinkeby Testnet |
|"goerli"| Ethereum Goerli Testnet |
|"kovan"| Ethereum Kovan Testnet |
|"ropsten"| Ethereum Ropsten Testnet |

### **Examples**
* `golem.srv.app.eth.network="mainnet"` - Requestor requests so that the Ethereum node service connects to Ethereum Mainnet.

