# Golem Activity-related Properties
Namespace that describes various properties related to Activities and their execution.

## `golem.activity.timeout_secs : Number` 
A timeout value for batch computation (eg. used for container-based batch processes). This property allows to set the timeout to be applied by the Provider when running a batch computation: the Requestor expects the Activity to take no longer than the specified timeout value - which implies that eg. the `golem.usage.duration_sec` counter shall not exceed the specified timeout value.

**Important:** This property shall be declared in the Demand, as it is the Requestor which sets the timeout. The Provider may however express constraints on the timeout (eg. I do not accept timeout periods smaller than `x`) - by expressing the timeout in the constraints in the Offer.

### **Examples**

* `golem.activity.timeout_secs=30` - this property included in the Demand sets the Activity timeout to 30 seconds.


