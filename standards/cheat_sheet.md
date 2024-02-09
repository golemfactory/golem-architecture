
# Golem Standards Cheat Sheet

This page contains an aggregated summary of all namespaces and properties specified in Golem Standards.

# Standards Hierarchy
###	0-commons
 * golem
   * activity
     * [caps](0-commons/golem/activity/caps.md)
   * [inf](0-commons/golem/inf.md)
     * [cpu](0-commons/golem/inf/cpu.md)
     * [mem](0-commons/golem/inf/mem.md)
     * [storage](0-commons/golem/inf/storage.md)
   * [runtime](0-commons/golem/runtime.md)
###	1-node
 * node
   * [geo](1-node/node/geo.md)
   * [id](1-node/node/id.md)
###	2-service
 * srv
   * app
     * [eth](2-service/srv/app/eth.md)
   * [caps](2-service/srv/caps.md)
   * [comp](2-service/srv/comp.md)
     * [wasm](2-service/srv/comp/wasm.md)
###	3-commercial
 * com
   * [payment](3-commercial/com/payment.md)
   * pricing
     * [model](3-commercial/com/pricing/model.md)
   * [scheme](3-commercial/com/scheme.md)
   * [usage](3-commercial/com/usage.md)

# Category: 0-commons

## [`golem.activity.caps`](0-commons/golem/activity/caps.md)

Namespace that describes Activity API capabilities, which can be perceived as "segments" of Activity API functionality. 

### Properties


| Property                                           | Type           | Applies to   | Category | Description                                                                                          |
|----------------------------------------------------|----------------|--------------|----------|------------------------------------------------------------------------------------------------------|
| **`golem.activity.caps.transfer.protocol`**        | `List[String]` | Offer        |          | Indicates the data transmission protocols available for TRANSFER operation on this Provider/ExeUnit. |
| **`golem.activity.caps.transfer.report-progress`** | `Boolean`      | Demand/Offer |          | ExeUnit can report progress of executed commands. [Specification](./command-progress.md)             |
| **`golem.activity.caps.deploy.report-progress`**   | `Boolean`      | Demand/Offer |          | ExeUnit can report progress of executed commands. [Specification](./command-progress.md)             |

## [`golem.inf.cpu`](0-commons/golem/inf/cpu.md)

Specifications of CPU computing power assigned to a service. 

### Properties


| Property                         | Type             | Applies to | Category | Description                                                                                                                                                                                                                           |
|----------------------------------|------------------|------------|----------|---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| **`golem.inf.cpu.architecture`** | `String`         | Offer      |          | CPU architecture.                                                                                                                                                                                                                     |
| **`golem.inf.cpu.cores`**        | `Number (int32)` | Offer      |          | Total number of CPU cores assigned to service. It is a sum of CPU cores possibly from multiple CPUs.                                                                                                                                  |
| **`golem.inf.cpu.threads`**      | `Number (int32)` | Offer      |          | Total number of CPU threads assigned to service. It is a sum of CPU threads possibly from multiple CPUs and cores.                                                                                                                    |
| **`golem.inf.cpu.capabilities`** | `List[String]`   | Offer      |          | CPU capability flags.  For x86 architectures this property is populated with CPU features as returned by CPUID instruction. For full list, see here: https://github.com/golemfactory/ya-runtime-vm/blob/master/runtime/src/cpu.rs#L59 |
| **`golem.inf.cpu.brand`**        | `String`         | Offer      |          |                                                                                                                                                                                                                                       |
| **`golem.inf.cpu.model`**        | `String`         | Offer      |          |                                                                                                                                                                                                                                       |
| **`golem.inf.cpu.vendor`**       | `String`         | Offer      |          |                                                                                                                                                                                                                                       |



## [`golem.inf.mem`](0-commons/golem/inf/mem.md)

Specifications of operating memory assigned to a service. 

### Properties


| Property                | Type             | Applies to | Category | Description                       |
|-------------------------|------------------|------------|----------|-----------------------------------|
| **`golem.inf.mem.gib`** | `Number (float)` | Offer      |          | Amount of RAM available (in GiB). |


## [`golem.inf.storage`](0-commons/golem/inf/storage.md)

Properties which describe storage properties of Golem service (hardware parameters, disk categories, sizes, etc.) 

### Properties


| Property                    | Type                          | Applies to | Category | Description              |
|-----------------------------|-------------------------------|------------|----------|--------------------------|
| **`golem.inf.storage.gib`** | `Number (float) [Negotiable]` | Offer      |          | Storage available in GiB |


## [`golem.runtime`](0-commons/golem/runtime.md)

Specification of ExeUnit/Runtime to host the resources provided. 

### Properties


| Property                         | Type           | Applies to | Category | Description                                                                        |
|----------------------------------|----------------|------------|----------|------------------------------------------------------------------------------------|
| **`golem.runtime.capabilities`** | `List[String]` | Offer      |          | Indicates the supported capabilities of the ExeUnit/Runtime offered on the market. |
| **`golem.runtime.name`**         | `String`       | Offer      |          | Indicates the ExeUnit/Runtime required/provided.                                   |
| **`golem.runtime.version`**      | `Version`      | Offer      |          | Version of the ExeUnit/Runtime required/provided.                                  |


# Category: 1-node

## [`node`](1-node/node.md)

Namespace defining properties of a Golem node. 

### Properties


| Property                       | Type             | Applies to   | Category | Description                                                      |
|--------------------------------|------------------|--------------|----------|------------------------------------------------------------------|
| **`golem.node.id.name`**       | `String [Fact]`  | Demand/Offer |          | Name of the Golem Node's owning party.                           |
| **`golem.node.debug.subnet`**  | `String [Fact]`  | Demand/Offer |          | Debug property for limiting Nodes we can match on market.        |
| **`golem.node.net.is-public`** | `Boolean [Fact]` | Demand/Offer |          | Indicates that Node has publicly exposed ports and can talk p2p. |


# Category: 2-service


## [`srv.caps`](2-service/srv/caps.md)

Namespace that describes capabilities of a Golem service. 

### Properties


| Property                                           | Type      | Applies to   | Category | Description                                                                                                         |
|----------------------------------------------------|-----------|--------------|----------|---------------------------------------------------------------------------------------------------------------------|
| **`golem.srv.caps.multi-activity`**                | `Boolean` | Demand/Offer |          | Indicates the that the Provider supports the multi-activity Agreements. [Specification](./../specs/capabilities.md) |



## [`srv.comp`](2-service/srv/comp.md)

Generic properties describing the Computation Platform aspects. 

### Properties


| Property                               | Type             | Applies to | Category | Description                                                                                                                                                                                                                                                                                                                                           |
|----------------------------------------|------------------|------------|----------|-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| **`golem.srv.comp.expiration`**        | `Number (int32)` | Demand     |          | Indicates the expiration time of the Agreement which is being negotiated. This is expressed as  Javascript timestamp (number of milliseconds since 1970-01-01 00:00:00 UTC, as returned by `Date.now()`)                                                                                                                                              |
| **`golem.srv.comp.task_package`**      | `String`         | Demand     |          | Indicates the URI of a package/binary which is to be executed by the Provider. This is a generic property, which, however, may be interpreted differently per each Computation Platform. Therefore, in a Computation Platform-specific namespace it is expected to specify the semantics of `golem.srv.comp.task_package` property for that Platform. |
| **`golem.srv.comp.vm.package_format`** | `String`         | Demand     |          | Format of vm image.                                                                                                                                                                                                                                                                                                                                   |


# Category: 3-commercial

## [`com.payment`](3-commercial/com/payment.md)

Namespace with properties defining payment parameters.  

### Properties


| Property                                                 | Type             | Applies to   | Category   | Description                                                                                 |
|----------------------------------------------------------|------------------|--------------|------------|---------------------------------------------------------------------------------------------|
| **`golem.com.payment.chosen-platform`**                  | `String`         | Demand/Offer |            | Payment Platform to be used for settlement of the Agreement.                                |
| **`golem.com.payment.debit-notes.accept-timeout?`**      | `Number (int32)` | Demand/Offer | Negotiable | Indicates the timeout period (in seconds) for the Requestor to accept incoming Debit Notes. |
| **`golem.com.payment.platform.<platform name>.address`** | `String`         | Demand/Offer |            | The address of GLM payment receiver (Provider) for indicated payment platform.              |


## [`com.pricing.model`](3-commercial/com/pricing/model.md)

This namespace defines **pricing models** for Golem computation resources.  

### Properties


| Property                                    | Type           | Applies to | Category | Description                                                       |
|---------------------------------------------|----------------|------------|----------|-------------------------------------------------------------------|
| **`golem.com.pricing.model`**               | `String`       | Offer      |          | Type of pricing function describing the pricing model.            |
| **`golem.com.pricing.model.linear.coeffs`** | `List[Number]` | Offer      |          | Property to express coefficients for the linear pricing function. |


## [`com.scheme`](3-commercial/com/scheme.md)

Payment schemes, which describe the "protocols" of payment for services/resources published on Golem Network. The purpose of the standardized schemes is to put structure into typical scenarios of payment for consumed resources - these scenarios define de facto "protocols" of Provider-Requestor interaction in the aspect of paying for a Golem service.  

### Properties


| Property                                             | Type     | Applies to   | Category                 | Description                                                                                                                                                                                                                                                                                                                                                              |
|------------------------------------------------------|----------|--------------|--------------------------|--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| **`golem.com.scheme`**                               | `String` | Offer        |                          | Scheme of payments made for computing resources consumed.                                                                                                                                                                                                                                                                                                                |
| **`golem.com.scheme.payu.debit-note.interval-sec?`** | `Number` | Demand/Offer | Negotiable, Experimental | For "pay-as-you-use" payment scheme, indicates interval of Debit Notes issued during the service usage.                                                                                                                                                                                                                                                                  |
| **`golem.com.scheme.payu.payment-timeout-sec?`**     | `Number` | Demand/Offer | Negotiable, Experimental | For "pay-as-you-use" payment scheme, indicates the maximum payment delay allowed after Debit Note or Invoice is issued. The Debit Notes' `paymentDueDate` field must be set by the Providar in alignment with the negotiated `payment-timeout-sec` value. [Mid-Agreement payments Specification](./../gaps/gap-3_mid_agreement_payments/gap-3_mid_agreement_payments.md) |
| **`golem.com.scheme.payu.interval_sec`**             | `Number` | Offer        | Deprecated               | For "pay-as-you-use" payment scheme, indicates interval of invoices issued during the service usage.                                                                                                                                                                                                                                                                     |

## [`com.usage`](3-commercial/com/usage.md)

Namespace defining service usage aspects (usage vector and counters). 

### Properties


| Property                       | Type             | Applies to | Category   | Description                                                                                                                                     |
|--------------------------------|------------------|------------|------------|-------------------------------------------------------------------------------------------------------------------------------------------------|
| **`golem.com.usage.vector`**   | `List[String]`   | Offer      |            | This property specifies the usage counters from which the service cost is calculated.                                                           |
| **`golem.usage.duration-sec`** | `Number (int32)` |            |            | Duration of Activity (in seconds). Replacement for deprecated `golem.usage.duration_sec`.                                                       |
| **`golem.usage.cpu-sec`**      | `Number (int32)` |            |            | Duration of CPU time during Activity execution (in seconds). Replacement for deprecated `golem.usage.cpu_sec`.                                  |
| **`golem.usage.gib`**          | `Number (float)` |            |            | Maximum level ("high water mark") of RAM memory usage during activity execution (in GBytes).                                                    |
| **`golem.usage.storage-gib`**  | `Number (float)` |            |            | Maximum level ("high water mark") of storage usage during activity execution (in GBytes). Replacement for deprecated `golem.usage.storage_gib`. |
| **`golem.usage.duration_sec`** | `Number (int32)` |            | Deprecated | Duration of Activity (in seconds).                                                                                                              |
| **`golem.usage.cpu_sec`**      | `Number (int32)` |            | Deprecated | Duration of CPU time during Activity execution (in seconds).                                                                                    |
| **`golem.usage.storage_gib`**  | `Number (float)` |            | Deprecated | Maximum level ("high water mark") of storage usage during activity execution (in GBytes).                                                       |

