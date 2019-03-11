# Payment Schemes
Payment Schemes

## `golem.com.payment.scheme : String`
Scheme of payments made for computing resources consumed.

### Value enum
|Value| Description |
|---|---|
|"before"| Payment is expected before any Activity is started as part of the Agreement. |
|"after"| Payment is expected after an Activity is completed. |
|"pay-as-you-use"| Payments are expected in reqular intervals while Activity is ongoing. |

### Sample values
* `golem.com.payment.scheme="before"` - The Provider declares the `"before"` payment scheme.

# Sample property block
```
golem.com.payment.scheme="pay-as-you-use"
```