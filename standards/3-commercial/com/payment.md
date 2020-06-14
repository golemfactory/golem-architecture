# Payment
Namespace with properties defining payment parameters. 

## `golem.com.payment.platform : String`
Payment Platform to be used for settlement of the Agreement.

### Value enum
| Value   | Description                          |
| ------- | ------------------------------------ |
| "erc20" | Golem ERC-20 token on plain Ethereum |
|         |                                      |

### **Examples**
* `golem.com.payment.platform="erc20"` - specifies ERC-20 plain Ethereum as payment platform.

## `golem.com.payment.eth_address_credit : String`
Beneficiary (Provider's) ethereum address - GNT payments are expected on this address.

### **Examples**
* `golem.com.payment.eth_address_credit="0x793ea9692ada1900fbd0b80fffec6e431fe8b391"` - specifies Provider credit account for payments.

# Sample property block
```
golem.com.payment.eth_address_credit="0x793ea9692ada1900fbd0b80fffec6e431fe8b391"
```
