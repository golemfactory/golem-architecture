
# Golem Standards Cheat Sheet

This page contains an aggregated summary of all namespaces and properties specified in Golem Standards.

# Standards Hierarchy
###	0-commons
 * golem
   * [activity](0-commons/golem/activity.md)
     * [caps](0-commons/golem/activity/caps.md)
   * [inf](0-commons/golem/inf.md)
     * [cpu](0-commons/golem/inf/cpu.md)
     * [gpu](0-commons/golem/inf/gpu.md)
     * [mem](0-commons/golem/inf/mem.md)
     * [net](0-commons/golem/inf/net.md)
     * [os](0-commons/golem/inf/os.md)
     * [storage](0-commons/golem/inf/storage.md)
   * [usage](0-commons/golem/usage.md)
###	1-node
 * node
   * [geo](1-node/node/geo.md)
   * [id](1-node/node/id.md)
###	2-service
 * srv
   * comp
     * container
       * [docker](2-service/srv/comp/container/docker.md)
     * [vm](2-service/srv/comp/vm.md)
     * [wasm](2-service/srv/comp/wasm.md)
###	3-commercial
 * com
   * payment
     * [scheme](3-commercial/com/payment/scheme.md)
   * pricing
     * [est](3-commercial/com/pricing/est.md)
     * [model](3-commercial/com/pricing/model.md)

# Category: 0-commons

## [`golem.activity`](0-commons/golem/activity.md)

Namespace that describes various properties related to Activities and their execution. 

### Properties

| Property | Type | Description |
|---|---|---|
|**`golem.activity.timeout_secs`**|`Number`|A timeout value for batch computation (eg. used for container-based batch processes). This property allows to set the timeout to be applied by the Provider when running a batch computation: the Requestor expects the Activity to take no longer than the specified timeout value - which implies that eg. the `golem.usage.duration_sec` counter shall not exceed the specified timeout value. |
---

## [`golem.inf.cpu`](0-commons/golem/inf/cpu.md)

Specifications of CPU computing power assigned to a service. 

### Properties

| Property | Type | Description |
|---|---|---|
|**`golem.inf.cpu.architecture`**|`String`|CPU architecture. |
|**`golem.inf.cpu.bit`**|`List[String]`|CPU bitness. |
|**`golem.inf.cpu.cores`**|`Number (int32)`|Total number of CPU cores assigned to service. It is a sum of CPU cores possibly from multiple CPUs. |
|**`golem.inf.cpu.threads`**|`Number (int32)`|Total number of CPU threads assigned to service. It is a sum of CPU threads possibly from multiple CPUs and cores. |
|**`golem.inf.cpu.max_freq_mhz`**|`Number (float)`|Maximum allowed CPU clock frequency (in Mhz). |
|**`golem.inf.cpu.vendor`**|`String`|CPU vendor. |
|**`golem.inf.cpu.model`**|`String`|CPU model. (note: this is a scalar property, ie. we do not attempt to model very unusual variants or nuances like multi-CPU-model host machines... Common sense! ) |
|**`golem.inf.cpu.capabilities`**|`List[String]`|CPU capability flags.  |
|**`golem.inf.cpu.l1cache.kib`**|`Number (float)`|Amount of L1 cache (in kiB). |
|**`golem.inf.cpu.l2cache.kib`**|`Number (float)`|Amount of L2 cache (in kiB). |
|**`golem.inf.cpu.l3cache.kib`**|`Number (float)`|Amount of L3 cache (in kiB). |
---

## [`golem.inf.gpu`](0-commons/golem/inf/gpu.md)

Specifications of GPU computing power assigned to a service. 

### Properties

| Property | Type | Description |
|---|---|---|
|**`golem.inf.gpu.vendor`**|`String`|GPU card vendor name. |
|**`golem.inf.gpu.model`**|`String`|GPU card model name. |
|**`golem.inf.gpu.count`**|`Number (int32)`|Number of GPU cards. |
|**`golem.inf.gpu.gib`**|`Number (float)`|GPU RAM available (in GiB) |
---

## [`golem.inf.net`](0-commons/golem/inf/net.md)

Properties which describe network properties of Golem service (network protocols supported, network visibility, VPN capabilities, etc.) 

### Properties

| Property | Type | Description |
|---|---|---|
|**`golem.inf.net.ipv4.has_pub_addr`**|`Boolean`|Declares that a Provider does have a public IP. |
|**`golem.inf.net.ipv4.tcp_visible{*}`**|`Boolean`|Pseudo-function property allowing to verify the TCP protocol connectivity of specific address, specified via: - `IP(:port)` - `dnsname(:port)` |
---

## [`golem.inf.os`](0-commons/golem/inf/os.md)

Specifications of operating systems. 

### Properties

| Property | Type | Description |
|---|---|---|
|**`golem.inf.os.platform`**|`String`|Operating system platform. |
|**`golem.inf.os.{platform}.version`**|`Version`|The version of the operating system. |
|**`golem.inf.os.name`**|`String`|Human-readable name of operating system. |
---

## [`golem.inf.storage`](0-commons/golem/inf/storage.md)

Properties which describe storage properties of Golem service (hardware parameters, disk categories, sizes, etc.) 

### Properties

| Property | Type | Description |
|---|---|---|
|**`golem.inf.storage.gib`**|`Number (float)`|Storage available in GiB |
|**`golem.inf.storage.type`**|`String`|Disk type |
|**`golem.inf.storage.write.mibs`**|`Number (float)`|Disk write speed in MiB/s |
|**`golem.inf.storage.read.mibs`**|`Number (float)`|Disk read speed in MiB/s |
---

## [`golem.usage`](0-commons/golem/usage.md)

Namespace defining service usage aspects (usage vector and counters). 

### Properties

| Property | Type | Description |
|---|---|---|
|**`golem.usage.vector`**|`List of Strings`|This property specifies the usage counters from which the service cost is calculated. |
|**`golem.usage.duration_sec`**|`Number (int32)`|Duration of Activity (in seconds). |
|**`golem.usage.transactions`**|`Number (int32)`|Number of "transactions" executed via the service hosted by Provider. The actual definition of "transaction" varies between services, eg. for a serverless function a "transaction" can be equivalent to a single "function call", etc. |
|**`golem.usage.gib_sec`**|`Number (float)`|Aggregated RAM consumption of a service (in GB * sec). This counter is typical for eg. a serverless function where the RAM usage of each funciton call is tallied-up to a cummulated amount which corresponds to the amount charged for the function usage. |
---

# Category: 1-node

## [`node.geo`](1-node/node/geo.md)

Namespace defining location/geography aspects of a Golem node. 

### Properties

| Property | Type | Description |
|---|---|---|
|**`golem.node.geo.country_code`**|`String`|Country of location of Golem node (expressed in [ISO 3166-1 Alpha-2](https://en.wikipedia.org/wiki/ISO_3166-1_alpha-2#Officially_assigned_code_elements) country codes). |
---

## [`node.id`](1-node/node/id.md)

Namespace defining identity aspects of a Golem node. 

### Properties

| Property | Type | Description |
|---|---|---|
|**`golem.node.id.name`**|`String`|Name of the Golem Node's owning party. |
---

# Category: 2-service

## [`srv.comp.container.docker`](2-service/srv/comp/container/docker.md)

Ability to run a containerized software component, within a Docker host. 

### Included Namespaces

* [golem.inf](../../../../0-commons/golem/inf.md)
* [golem.activity](../../../../0-commons/golem/activity.md)

### Properties

| Property | Type | Description |
|---|---|---|
|**`golem.srv.comp.container.docker.image`**|`List of String`|Indicates Docker images which are to be hosted by the Provider. The Offer may either declare specific images as available, or indicate the whole property as dynamic, so that the actual image required by the Requestor is specified by the Demand. In the latter scenario, during the negotiation phase the Provider shall decide whether the image indicated in Demand is trustworthy (eg. by checking an internal whitelist). |
|**`golem.srv.comp.container.docker.benchmark{<image>}`**|`Number`|A benchmark performance metric calculated for specific docker image for the Provider node. |
---

## [`srv.comp.vm`](2-service/srv/comp/vm.md)

Ability to host a generic Virtual Machine. 

### Included Namespaces

* [golem.inf](../0-commons/golem.inf.md)

### Properties

| Property | Type | Description |
|---|---|---|
|**`golem.svc.vm.host`**|`String`|Indicates the VM host platform available.  |
|**`golem.svc.vm.term_protocol`**|`List of String`|A list of protocols that can be used to establish a terminal session to the VM. |
---

## [`srv.comp.wasm`](2-service/srv/comp/wasm.md)

Ability to host and execute a WebAssembly program. 

### Included Namespaces

* [golem.inf](../../../0-commons/golem/inf.md)
* [golem.activity](../../../0-commons/golem/activity.md)

### Properties

| Property | Type | Description |
|---|---|---|
|**`golem.svc.wasm.task_package`**|`List of String`|Indicates WebAssembly packages which are to be hosted by the Provider. The Offer may either declare specific images as available, or indicate the whole property as dynamic, so that the actual image required by the Requestor is specified by the Demand. In the latter scenario, during the negotiation phase the Provider shall decide whether the image indicated in Demand is trustworthy (eg. by checking an internal whitelist). |
---

# Category: 3-commercial

## [`com.payment.scheme`](3-commercial/com/payment/scheme.md)

Payment schemes, which describe the "protocols" of payment for services/resources published on Golem Network. The purpose of the standardized schemes is to put structure into typical scenarios of payment for consumed resources - these scenarios define de facto "protocols" of Provider-Requestor interaction in the aspect of paying for a Golem service.  

### Properties

| Property | Type | Description |
|---|---|---|
|**`golem.com.payment.scheme`**|`String`|Scheme of payments made for computing resources consumed. |
|**`golem.com.payment.payu.interval_sec`**|`Number`|For "pay-as-you-use" payment scheme, indicates interval of invoices issued during the service usage. |
---

## [`com.pricing.est`](3-commercial/com/pricing/est.md)

This namespace defines mechanism for **price estimation** for Offers published in Golem network.  

### Properties

| Property | Type | Description |
|---|---|---|
|**`golem.com.pricing.est{*}`**|`Number`|Pseudo-function property allowing to query the Provider for price calculation for a given estimated usage vector value (amount is indicated in GNT). |
---

## [`com.pricing.model`](3-commercial/com/pricing/model.md)

This namespace defines **pricing models** for Golem computation resources.  

### Properties

| Property | Type | Description |
|---|---|---|
|**`golem.com.pricing.model`**|`String`|Type of pricing function describing the pricing model. |
|**`golem.com.pricing.model.fixed.price`**|`Number`|Property to express a scalar fixed price for an Activity. |
|**`golem.com.pricing.model.linear.coeffs`**|`List of Number`|Property to express coefficients for the linear pricing function. |
---

