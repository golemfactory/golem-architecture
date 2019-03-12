# Payment Schemes
Payment schemes, which describe the "protocols" of payment for 

## `golem.com.payment.scheme : String`
Scheme of payments made for computing resources consumed.

### Value enum
|Value| Description |
|---|---|
|"before"| Payment is expected before any Activity is started as part of the Agreement. |
|"after"| Payment is expected after an Activity is completed. |
|"payu"| "pay-as-you-use" - payments are expected in regular intervals while Activity is ongoing. |

### **Examples**
* `golem.com.payment.scheme="before"` - The Provider declares the `"before"` payment scheme.

## `golem.com.payment.payu.interval_sec : Number`
For "pay-as-you-use" payment scheme, indicates interval of invoices issued during the service usage.

### **Examples**
* `golem.com.payment.payu.interval_sec=3600` - The Provider expects the Requestor to pay for the usage in 1 hour intervals.

# Sample property block
```
golem.com.payment.scheme="payu"
golem.com.payment.payu.interval_sec=3600
```