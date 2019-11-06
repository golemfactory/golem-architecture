# Golem Activity-related Properties
Namespace that describes various properties related to Activities and their execution.

## `golem.activity.cost_cap : Number` 
Sets a **Hard** cap on total cost of the Activity (regardless of the usage vector or pricing function). 
The Provider is entitled to 'kill' an Activity which exceeds the capped cost amount indicated by Requestor. Note, the Provider still is entitled to issue an Invoice to cover the cost, and the Requestor is obliged to pay it.

**Important:** This property shall be declared in the Demand, as it is the Requestor which sets the cap on total Activity cost.

### **Examples**

* `golem.activity.cost_cap=100` - this property included in the Demand sets the Activity hard cap on total cost to 100 GNT.

## `golem.activity.cost_warning : Number` 
Sets a **Soft** cap on total cost of the Activity (regardless of the usage vector or pricing function). 
When the cost_warning amount is reached for the Activity, the Provider is expected to send a Debit Note to the Requestor, indicating the current amount due.

**Important:** This property shall be declared in the Demand, as it is the Requestor which sets the warning level on total Activity cost.

### **Examples**

* `golem.activity.cost_warning=100` - this property included in the Demand sets the Activity warning level on total cost to 100 GNT.

## `golem.activity.timeout_secs : Number` 
A timeout value for batch computation (eg. used for container-based batch processes). This property allows to set the timeout to be applied by the Provider when running a batch computation: the Requestor expects the Activity to take no longer than the specified timeout value - which implies that eg. the `golem.usage.duration_sec` counter shall not exceed the specified timeout value.

**Important:** This property shall be declared in the Demand, as it is the Requestor which sets the timeout. The Provider may however express constraints on the timeout (eg. I do not accept timeout periods smaller than `x`) - by expressing the timeout in the constraints in the Offer.

### **Examples**

* `golem.activity.timeout_secs=30` - this property included in the Demand sets the Activity timeout to 30 seconds.


