# Golem Usage Specification & Counters
Namespace defining service usage aspects (usage vector and counters).

# Usage Vector Specification

## `golem.usage.vector : List of Strings`
This property specifies the usage counters from which the service cost is calculated.

**Note:** This standard specifies a set of commonly applicable usage counters. It is expected from an execution environment on Provider node to support a certain set of usage counters (ie. be able to tally up the usage as the Activity is progressing). This implies that 
### **Examples**
* `golem.usage.vector=["golem.usage.duration_sec"]` - Specifies 1-dimensional usage vector, consisting of Activity duration counter.

# Usage Counters
The usage counters used to specify the service usage vector are defined in this section.

**Note:** Usage counters are always expressed in Number type.

## `golem.usage.duration_sec : Number (int32)`
Duration of Activity (in seconds).

## `golem.usage.transactions : Number (int32)`
Number of "transactions" executed via the service hosted by Provider. The actual definition of "transaction" varies between services, eg. for a serverless function a "transaction" can be equivalent to a single "function call", etc.

## `golem.usage.gib_sec : Number (float)`
Aggregated RAM consumption of a service (in GB * sec). This counter is typical for eg. a serverless function where the RAM usage of each function call is tallied-up to a cummulated amount which corresponds to the amount charged for the function usage.

(TODO) other counter definitions.

