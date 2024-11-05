# Computation Runtime Specification 
Specification of ExeUnit/Runtime to host the resources provided.

## Common Properties

(Not applicable)
  
## Specific Properties

## `golem.runtime.capabilities : List[String]` 

### Describes: Offer

Indicates the supported capabilities of the ExeUnit/Runtime offered on the market.

### Value enum
| Value              | Description                                                                                                              |
|--------------------|--------------------------------------------------------------------------------------------------------------------------|
| "vpn"              | VPN network capabilities are supported                                                                                   |
| "inet"             | Outbound internet access from VM guest is supported                                                                      |
| "manifest-support" | Supports payload specification based on [computation manifest](../../../gaps/gap-4_comp_manifest/gap-4_comp_manifest.md) |
| "start-entrypoint" | Docker ENTRYPOINT command is ran on the container startup                                                                |
| "!exp:gpu"         | Supports GPU access                                                                                                      |

### **Examples**

* `golem.runtime.capabilities=["vpn"]` - declares runtime supporting VPN capabilities.

## `golem.runtime.name : String` 

### Describes: Offer

Indicates the ExeUnit/Runtime required/provided. 
### Value enum
| Value       | Description                                                                                                                                      |
|-------------|--------------------------------------------------------------------------------------------------------------------------------------------------|
| "wasmtime"  | Golem Factory's WASI runtime (based on `wasmtime`) https://github.com/golemfactory/ya-runtime-wasi                                               |
| "vm"        | Golem Factory's VM runtime (based on `QEMU`) https://github.com/golemfactory/ya-runtime-vm                                                       |
| "vm-nvidia" | Golem Factory's VM runtime with nvidia GPU support https://github.com/golemfactory/ya-runtime-vm-nvidia                                          |
| "automatic" | Specialized runtime for AI inference based on [Automatic1111](https://github.com/automatic1111) https://github.com/golemfactory/gamerhash-facade |


### **Examples**

* `golem.runtime.name="wasmtime"` - declares that `wasmtime` is available as runtime on the provider node.
  
## `golem.runtime.version : Version` 

### Describes: Offer

Version of the ExeUnit/Runtime required/provided.

### **Examples**

* `golem.runtime.version="0.0.0"` - declares runtime version 0.0.0.

