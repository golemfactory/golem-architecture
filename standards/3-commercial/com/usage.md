# Golem Usage Specification & Counters
Namespace defining service usage aspects (usage vector and counters).

# Usage Vector Specification

## `golem.com.usage.vector : List[String]`

### Describes: Offer

This property specifies the usage counters from which the service cost is calculated.

**Note:** This standard specifies a set of commonly applicable usage counters. It is expected from an execution environment on Provider node to support a certain set of usage counters (ie. be able to tally up the usage as the Activity is progressing). 

### **Examples**
* `golem.com.usage.vector=["golem.usage.duration-sec"]` - Specifies 1-dimensional usage vector, consisting of Activity duration counter.

# Usage Counters
The usage counters used to specify the service usage vector are defined in this section.

**Note:** Usage counters are always expressed in Number type.

## `golem.usage.duration-sec : Number (int32)`
Duration of Activity (in seconds). Replacement for deprecated `golem.usage.duration_sec`.

## `golem.usage.cpu-sec : Number (int32)`
Duration of CPU time during Activity execution (in seconds). Replacement for deprecated `golem.usage.cpu_sec`.

## `golem.usage.gib : Number (float)`
Maximum level ("high water mark") of RAM memory usage during activity execution (in GBytes).

## `golem.usage.storage-gib : Number (float)`
Maximum level ("high water mark") of storage usage during activity execution (in GBytes). Replacement for deprecated `golem.usage.storage_gib`.

## `golem.usage.duration_sec : Number (int32)` [[Deprecated]](/standards/README.md#deprecated-properties)
Duration of Activity (in seconds).
Use `golem.usage.duration-sec` instead.

## `golem.usage.storage_gib : Number (float)` [[Deprecated]](/standards/README.md#deprecated-properties)
Maximum level ("high water mark") of storage usage during activity execution (in GBytes).
Use `golem.usage.storage-gib` instead.

## `golem.usage.cpu_sec : Number (int32)` [[Deprecated]](/standards/README.md#deprecated-properties)
Duration of CPU time during Activity execution (in seconds).
Use `golem.usage.cpu-sec` instead.

# Use case specific usage counters

## Http-authentication runtime (ya-runtime-http-auth)

- Repository: https://github.com/golemfactory/ya-runtime-http-auth

### `http-auth.requests : Number (int32)`
Number of requests made to http endpoints exposed by runtime.
Reference: https://github.com/golemfactory/ya-runtime-http-auth#runtime-definition

## Outbound gateway (ya-runtime-outbound-gateway)

- Repository: https://github.com/golemfactory/ya-runtime-outbound-gateway
- Reference: https://github.com/golemfactory/ya-runtime-outbound-gateway/blob/master/conf/ya-runtime-outbound-gateway.json

### `golem.usage.network.in-mib: Number (float)`

Incoming network traffic usage in MiB/s

### `golem.usage.network.out-mib: Number (float)`

Outgoing network traffic usage in MiB/s
