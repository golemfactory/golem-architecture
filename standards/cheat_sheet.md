
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
   * [usage](0-commons/golem/usage.md)
###	1-node
 * node
   * [id](1-node/node/id.md)
###	2-service
 * srv
   * comp
     * [wasm](2-service/srv/comp/wasm.md)
   * [runtime](2-service/srv/runtime.md)
###	3-commercial
 * com
   * [payment](3-commercial/com/payment.md)
     * [scheme](3-commercial/com/payment/scheme.md)
   * pricing
     * [model](3-commercial/com/pricing/model.md)
   * [term](3-commercial/com/term.md)

# Category: 0-commons

## [`golem.activity.caps`](0-commons/golem/activity/caps.md)

Namespace that describes Activity API capabilities, which can be perceived as "segments" of Activity API functionality. 

### Properties

| Property | Type | Applies to | Category | Description |
|---|---|---|---|---|
|**`golem.activity.caps.transfer.format`**|`List[String] `|Offer|Fact|Indicates the transferred file format which can be decoded (eg. decompressed) by the Provider/ExeUnit. This indicates possible archive file formats which can be transferred and seamlessly unpacked on Provider side (eg. without the need to explicitly perform additional decompression activities). |
|**`golem.activity.caps.transfer.protocol`**|`List[String] `|Offer|Fact|Indicates the data transmission protocols available for TRANSFER operation on this Provider/ExeUnit. |
---

## [`golem.inf.cpu`](0-commons/golem/inf/cpu.md)

Specifications of CPU computing power assigned to a service. 

### Properties

| Property | Type | Applies to | Category | Description |
|---|---|---|---|---|
|**`golem.inf.cpu.cores`**|`Number (int32) `|Demand/Offer|Negotiable|Total number of CPU cores assigned to service. It is a sum of CPU cores possibly from multiple CPUs. |
---

## [`golem.inf.mem`](0-commons/golem/inf/mem.md)

Specifications of operating memory assigned to a service. 

### Properties

| Property | Type | Applies to | Category | Description |
|---|---|---|---|---|
|**`golem.inf.mem.gib`**|`Number (float) `|Demand/Offer|Negotiable|Amount of RAM available (in GiB). |
---

## [`golem.inf.storage`](0-commons/golem/inf/storage.md)

Properties which describe storage properties of Golem service (hardware parameters, disk categories, sizes, etc.) 

### Properties

| Property | Type | Applies to | Category | Description |
|---|---|---|---|---|
|**`golem.inf.storage.gib`**|`Number (float) `|Demand/Offer|Negotiable|Storage available in GiB |
---

## [`golem.usage`](0-commons/golem/usage.md)

Namespace defining service usage aspects (usage vector and counters). 

### Properties

| Property | Type | Applies to | Category | Description |
|---|---|---|---|---|
|**`golem.usage.vector`**|`List[String] `|Offer|Fact|This property specifies the usage counters from which the service cost is calculated. |
---

# Category: 1-node

## [`node.id`](1-node/node/id.md)

Namespace defining identity aspects of a Golem node. 

### Properties

| Property | Type | Applies to | Category | Description |
|---|---|---|---|---|
|**`golem.node.id.name`**|`String `|Demand/Offer|Fact|Name of the Golem Node's owning party. |
---

# Category: 2-service

## [`srv.comp.wasm`](2-service/srv/comp/wasm.md)

This namespace defines properties used to indicate ability to host and execute a WebAssembly program. 

### Included Namespaces

* [golem.inf](../../../0-commons/golem/inf.md)
* [golem.activity](../../../0-commons/golem/activity.md)

### Properties

| Property | Type | Applies to | Category | Description |
|---|---|---|---|---|
|**`golem.srv.comp.wasm.wasi.version`**|`Version `|Offer|Fact|Indicates the version of WASI API supported by the runtime. |
|**`golem.srv.comp.wasm.task_package`**|`String `|Demand|Fact|Indicates WebAssembly package which is to be hosted by the Provider. The Demand indicates the requested package by specifying the package URL and hash value. The hash value shall be validated by the Provider. |
---

## [`srv.runtime`](2-service/srv/runtime.md)

Specification of ExeUnit/Runtime to host the resources provided. 

### Properties

| Property | Type | Applies to | Category | Description |
|---|---|---|---|---|
|**`golem.srv.runtime.name`**|`String `|Offer|Fact|Indicates the ExeUnit/Runtime required/provided.  |
|**`golem.srv.runtime.version`**|`Version `|Offer|Fact|Version of the ExeUnit/Runtime required/provided. |
---

# Category: 3-commercial

## [`com.payment.scheme`](3-commercial/com/payment/scheme.md)

Payment schemes, which describe the "protocols" of payment for services/resources published on Golem Network. The purpose of the standardized schemes is to put structure into typical scenarios of payment for consumed resources - these scenarios define de facto "protocols" of Provider-Requestor interaction in the aspect of paying for a Golem service.  

### Properties

| Property | Type | Applies to | Category | Description |
|---|---|---|---|---|
|**`golem.com.payment.scheme`**|`String `|Offer|Fact|Scheme of payments made for computing resources consumed. |
|**`golem.com.payment.scheme.payu.interval_sec`**|`Number `|Offer|Fact|For "pay-as-you-use" payment scheme, indicates interval of invoices issued during the service usage. |
---

## [`com.payment`](3-commercial/com/payment.md)

Namespace with properties defining payment parameters.  

### Properties

| Property | Type | Applies to | Category | Description |
|---|---|---|---|---|
|**`golem.com.payment.chosen-platform`**|`String `|Demand/Offer|Negotiable|Payment Platform to be used for settlement of the Agreement. |
|**`golem.com.payment.platform.ngnt.address`**|`String `|Demand/Offer|Fact|The address of GNT payment receiver (Provider) for **plain GNT platform**. |
|**`golem.com.payment.platform.zk-ngnt.address`**|`String `|Demand/Offer|Fact|The address of GNT payment receiver (Provider) for **zk-GNT platform**.. |
---

## [`com.pricing.model`](3-commercial/com/pricing/model.md)

This namespace defines **pricing models** for Golem computation resources.  

### Properties

| Property | Type | Applies to | Category | Description |
|---|---|---|---|---|
|**`golem.com.pricing.model`**|`String `|Offer|Fact|Type of pricing function describing the pricing model. |
|**`golem.com.pricing.model.linear.coeffs`**|`List[Number] `|Offer|Negotiable|Property to express coefficients for the linear pricing function. |
---

## [`com.term`](3-commercial/com/term.md)

Namespace defining time-related contract aspects of a Golem service.  

### Properties

| Property | Type | Applies to | Category | Description |
|---|---|---|---|---|
|**`golem.com.term.expiration_dt`**|`DateTime `|Demand/Offer|Fact|Indicates the expiration time of the entity to which it refers (Demand or Offer). |
---

