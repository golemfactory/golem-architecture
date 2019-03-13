# Golem Usage Specification & Counters
Namespace defining service usage aspects (usage vector and counters).

# Usage Vector Specification

## `golem.usage.vector : List of Strings`
This property specifies the usage counters from which the service cost is calculated.
### **Examples**
* `golem.usage.vector=["golem.usage.duration_sec"]` - Specifies 1-dimensional usage vector, consisting of Activity duration counter.

# Usage Counters
The usage counters used to specify the service usage vector are defined in this section.

**Note:** Usage counters are always expressed in Number type.

## `golem.usage.duration_sec`
Duration of Activity (in seconds).

(TODO) other counter definitions.

