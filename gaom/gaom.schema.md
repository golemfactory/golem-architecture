# Objects
* [`Application`](#reference-application) (root object)
* [`Meta`](#reference-meta)
* [`Network`](#reference-network)
* [`Payload`](#reference-payload)
* [`Service`](#reference-service)
    * [`ExeScriptCommand`](#reference-exescriptcommand)


---------------------------------------
<a name="reference-application"></a>
## Application

Golem Application Object Model

**`Application` Properties**

|   |Type|Description|Required|
|---|---|---|---|
|**meta**|`meta`|Descriptor metadata structure|No|
|**payloads**|`object`|Payload specifications|No|
|**networks**|`object`|Network specifications|No|
|**services**|`object`|Service specifications|No|

Additional properties are allowed.

### Application.meta

Descriptor metadata structure

* **Type**: `meta`
* **Required**: No

### Application.payloads

Payload specifications

* **Type**: `object`
* **Required**: No

### Application.networks

Network specifications

* **Type**: `object`
* **Required**: No

### Application.services

Service specifications

* **Type**: `object`
* **Required**: No




---------------------------------------
<a name="reference-exescriptcommand"></a>
## ExeScriptCommand

A structure to specify a single ExeScript command. Note: This schema does not specify the format of ExeScript commands. For reference of ExeScriptCommand structure, refer to (https://github.com/golemfactory/ya-client/blob/master/specs/activity-api.yaml)

Additional properties are allowed.




---------------------------------------
<a name="reference-meta"></a>
## Meta

Descriptor metadata structure

**`Meta` Properties**

|   |Type|Description|Required|
|---|---|---|---|
|**name**|`string`|Name of the application| &#10003; Yes|
|**description**|`string`|Description of the application| &#10003; Yes|
|**author**|`string`|Author| &#10003; Yes|
|**version**|`string`|Version of the application (following the sem-ver convention)| &#10003; Yes|

Additional properties are allowed.

### meta.name

Name of the application

* **Type**: `string`
* **Required**:  &#10003; Yes

### meta.description

Description of the application

* **Type**: `string`
* **Required**:  &#10003; Yes

### meta.author

Author

* **Type**: `string`
* **Required**:  &#10003; Yes

### meta.version

Version of the application (following the sem-ver convention)

* **Type**: `string`
* **Required**:  &#10003; Yes




---------------------------------------
<a name="reference-network"></a>
## Network

**`Network` Properties**

|   |Type|Description|Required|
|---|---|---|---|
|**mask**|`string`|Network address mask|No|

Additional properties are allowed.

### network.mask

Network address mask

* **Type**: `string`
* **Required**: No




---------------------------------------
<a name="reference-payload"></a>
## Payload

**`Payload` Properties**

|   |Type|Description|Required|
|---|---|---|---|
|**runtime**|`string`|Specifies the ExeUnit/runtime required by the payload| &#10003; Yes|
|**constraints**|`string` `[]`|List of the Golem market constraints required by the payload|No|
|**capabilities**|`string` `[]`|List of Provider capabilities required by the payload|No|
|**params**|`object`|Additional payload parameters|No|

Additional properties are allowed.

### payload.runtime

Specifies the ExeUnit/runtime required by the payload

* **Type**: `string`
* **Required**:  &#10003; Yes

### payload.constraints

List of the Golem market constraints required by the payload

* **Type**: `string` `[]`
* **Required**: No

### payload.capabilities

List of Provider capabilities required by the payload

* **Type**: `string` `[]`
* **Required**: No

### payload.params

Additional payload parameters

* **Type**: `object`
* **Required**: No




---------------------------------------
<a name="reference-service"></a>
## Service

A structure to specify a service resource to be provisioned on Golem network as part of the Golem application.

**`Service` Properties**

|   |Type|Description|Required|
|---|---|---|---|
|**payload**|`string`|Specifies the payload to be executed by the service| &#10003; Yes|
|**depends_on**|`string` `[]`|List of explicit dependencies - services which must be provisioned before this service can be instantiated|No|
|**init**|`exeScriptCommand` `[]`|List of ExeScript commands to be executed as the service's Activity becomes active|No|

Additional properties are allowed.

### service.payload

Specifies the payload to be executed by the service

* **Type**: `string`
* **Required**:  &#10003; Yes

### service.depends_on

List of explicit dependencies - services which must be provisioned before this service can be instantiated

* **Type**: `string` `[]`
* **Required**: No

### service.init

List of ExeScript commands to be executed as the service's Activity becomes active

* **Type**: `exeScriptCommand` `[]`
* **Required**: No


