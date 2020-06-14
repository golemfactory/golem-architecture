
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
   * app
     * media
       * [render](2-service/srv/app/media/render.md)
       * [transcode](2-service/srv/app/media/transcode.md)
   * comp
     * container
       * [docker](2-service/srv/comp/container/docker.md)
     * [vm](2-service/srv/comp/vm.md)
     * [wasm](2-service/srv/comp/wasm.md)
   * [runtime](2-service/srv/runtime.md)
###	3-commercial
 * com
   * [payment](3-commercial/com/payment.md)
     * [scheme](3-commercial/com/payment/scheme.md)
   * pricing
     * [est](3-commercial/com/pricing/est.md)
     * [model](3-commercial/com/pricing/model.md)
   * term
     * [term](3-commercial/com/term/term.md)

# Category: 0-commons

## [`golem.activity.caps`](0-commons/golem/activity/caps.md)

Namespace that describes Activity API capabilities, which can be perceived as "segments" of Activity API functionality. 

### Properties

| Property | Type | Description |
|---|---|---|
|**`golem.activity.caps.transfer.protocol`**|`List[String]`|Indicates the data transmission protocols available for TRANSFER operation on this Provider/ExeUnit. |
---

## [`golem.activity`](0-commons/golem/activity.md)

Namespace that describes various properties related to Activities and their execution. 

### Properties

| Property | Type | Description |
|---|---|---|
|**`golem.activity.cost_cap`**|`Number`|Sets a **Hard** cap on total cost of the Activity (regardless of the usage vector or pricing function).  The Provider is entitled to 'kill' an Activity which exceeds the capped cost amount indicated by Requestor. Note, the Provider still is entitled to issue an Invoice to cover the cost, and the Requestor is obliged to pay it. |
|**`golem.activity.cost_warning`**|`Number`|Sets a **Soft** cap on total cost of the Activity (regardless of the usage vector or pricing function).  When the cost_warning amount is reached for the Activity, the Provider is expected to send a Debit Note to the Requestor, indicating the current amount due. |
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

## [`golem.inf.mem`](0-commons/golem/inf/mem.md)

Specifications of operating memory assigned to a service. 

### Properties

| Property | Type | Description |
|---|---|---|
|**`golem.inf.mem.gib`**|`Number (float)`|Amount of RAM available (in GiB). |
|**`golem.inf.mem.freq_mhz`**|`Number (float)`|RAM clock frequency (in MHz). |
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
|**`golem.usage.gib_sec`**|`Number (float)`|Aggregated RAM consumption of a service (in GB * sec). This counter is typical for eg. a serverless function where the RAM usage of each function call is tallied-up to a cummulated amount which corresponds to the amount charged for the function usage. |
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

## [`srv.app.media.render`](2-service/srv/app/media/render.md)

This namespace includes properties used to describe visualization and rendering services. 

### Included Namespaces

* [golem.activity](../../../../0-commons/golem/activity.md)

### Properties

| Property | Type | Description |
|---|---|---|
|**`golem.srv.app.media.render.input_file_size_kib`**|`Number (int32)`|Size of input scene file, in KB. |
|**`golem.srv.app.media.render.output.resolution.x`**|`Number (int32)`|X resolution of output rendered image (in pixels). |
|**`golem.srv.app.media.render.output.resolution.y`**|`Number (int32)`|Y resolution of output rendered image (in pixels). |
|**`golem.srv.app.media.render.engine`**|`List of String`|Indicates rendering engines supported by the provider.  |
|**`golem.srv.app.media.render.blender.benchmark`**|`Number (int32)`|Indicates benchmark value of Provider tested by rendering a standardized benchmark scene.  _(**Note:** (TODO) the standard must define the standardized input scene to be used as benchmark.)_ |
---

## [`srv.app.media.transcode`](2-service/srv/app/media/transcode.md)

This napespace includes properties used to describe visualization and rendering services. 

### Included Namespaces

* [golem.activity](../../../../0-commons/golem/activity.md)

### Properties

| Property | Type | Description |
|---|---|---|
|**`golem.srv.app.media.transcode.input.resolution.x`**|`Number (int32)`|X resolution of input stream. |
|**`golem.srv.app.media.transcode.input.resolution.y`**|`Number (int32)`|Y resolution of input stream. |
|**`golem.srv.app.media.transcode.input.container`**|`List of String`|Supported container formats of input stream. |
|**`golem.srv.app.media.transcode.input.video.codecs`**|`List of String`|Supported video codecs for input processing. |
|**`golem.srv.app.media.transcode.output.video.codecs`**|`List of String`|Supported video codecs for output generation. |
|**`golem.srv.app.media.transcode.key_frames_number`**|`Number (int32)`|Number of key frames in input stream. |
|**`golem.srv.app.media.transcode.bitrate_kibs`**|`Number (int32)`|Requested output stream bitrate (in kbit/sec) |
---

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
|**`golem.srv.comp.vm.host`**|`String`|Indicates the VM host platform available.  |
|**`golem.srv.comp.vm.term_protocol`**|`List of String`|A list of protocols that can be used to establish a terminal session to the VM. |
---

## [`srv.comp.wasm`](2-service/srv/comp/wasm.md)

This namespace defines properties used to indicate ability to host and execute a WebAssembly program. 

### Included Namespaces

* [golem.inf](../../../0-commons/golem/inf.md)
* [golem.activity](../../../0-commons/golem/activity.md)

### Properties

| Property | Type | Description |
|---|---|---|
|**`golem.srv.comp.wasm.api`**|`List of String`|Indicates the APIs supported by the WebAssembly runtime and available to WebAssembly packages.  |
|**`golem.srv.comp.wasm.emscripten.js`**|`Version`|Indicates the version of JavaScript supported by the Emscripten-compliant runtime. |
|**`golem.srv.comp.wasm.emscripten.caps`**|`List of String`|Indicates the runtime capabilities of the Emscripten-compliant runtime. |
|**`golem.srv.comp.wasm.wasi.version`**|`Version`|Indicates the version of WASI API supported by the runtime. |
|**`golem.srv.comp.wasm.wasi.caps`**|`List of String`|Indicates the runtime capabilities of the WASI-compliant runtime. |
|**`golem.srv.comp.wasm.task_package`**|`String`|Indicates WebAssembly package which is to be hosted by the Provider. The Demand indicates the requested package by specifying the package URL and hash value. The hash value shall be validated by the Provider. |
---

## [`srv.runtime`](2-service/srv/runtime.md)

Specification of ExeUnit/Runtime to host the resources provided. 

### Properties

| Property | Type | Description |
|---|---|---|
|**`golem.srv.runtime.name`**|`String`|Indicates the ExeUnit/Runtime required/provided.  |
|**`golem.srv.runtime.version`**|`Version`|Version of the ExeUnit/Runtime required/provided. |
---

# Category: 3-commercial

## [`com.payment.scheme`](3-commercial/com/payment/scheme.md)

Payment schemes, which describe the "protocols" of payment for services/resources published on Golem Network. The purpose of the standardized schemes is to put structure into typical scenarios of payment for consumed resources - these scenarios define de facto "protocols" of Provider-Requestor interaction in the aspect of paying for a Golem service.  

### Properties

| Property | Type | Description |
|---|---|---|
|**`golem.com.payment.scheme`**|`String`|Scheme of payments made for computing resources consumed. |
|**`golem.com.payment.scheme.payu.interval_sec`**|`Number`|For "pay-as-you-use" payment scheme, indicates interval of invoices issued during the service usage. |
---

## [`com.payment`](3-commercial/com/payment.md)

Namespace with properties defining payment parameters.  

### Properties

| Property | Type | Description |
|---|---|---|
|**`golem.com.payment.platform`**|`String`|Payment Platform to be used for settlement of the Agreement. |
|**`golem.com.payment.eth_address_credit`**|`String`|Beneficiary (Provider's) ethereum address - GNT payments are expected on this address. |
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

## [`com.term.term`](3-commercial/com/term/term.md)

Namespace defining time-related contract aspects of a Golem service.  

### Properties

| Property | Type | Description |
|---|---|---|
|**`golem.com.term.expiration_dt`**|`DateTime`|Indicates the expiration time of the entity to which it refers (Demand or Offer). |
---

