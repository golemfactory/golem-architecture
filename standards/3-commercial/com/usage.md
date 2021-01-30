# Golem Usage Specification & Counters
Namespace defining service usage aspects (usage vector and counters).

# Usage Vector Specification

## `golem.com.usage.vector : List[String]`

### Describes: Offer

This property specifies the usage counters from which the service cost is calculated.

**Note:** This standard specifies a set of commonly applicable usage counters. It is expected from an execution environment on Provider node to support a certain set of usage counters (ie. be able to tally up the usage as the Activity is progressing). 

### **Examples**
* `golem.com.usage.vector=["golem.usage.duration_sec"]` - Specifies 1-dimensional usage vector, consisting of Activity duration counter.

# Usage Counters
The usage counters used to specify the service usage vector are defined in this section.

**Note:** Usage counters are always expressed in Number type.

## `golem.usage.duration_sec : Number (int32)`
Duration of Activity (in seconds).

## `golem.usage.cpu_sec : Number (int32)`
Duration of CPU time during Activity execution (in seconds).

## `golem.usage.gib : Number (float)`
Maximum level ("high water mark") of RAM memory usage during activity execution (in GBytes).

## `golem.usage.storage_gib : Number (float)`
Maximum level ("high water mark") of storage usage during activity execution (in GBytes).

