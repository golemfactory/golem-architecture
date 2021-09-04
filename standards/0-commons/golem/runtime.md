# Computation Runtime Specifiation 
Specification of ExeUnit/Runtime to host the resources provided.

## Common Properties

(Not applicable)
  
## Specific Properties

## `golem.runtime.capabilities : List[String]` 

### Describes: Offer

Indicates the supported capabilities of the ExeUnit/Runtime offered on the market.

### Value enum
| Value      | Description                                        |
| ---------- | -------------------------------------------------- |
| "vpn" | VPN network capabilities are supported |
|            |                                                    |

### **Examples**

* `golem.runtime.capabilities=["vpn"]` - declares runtime supporting VPN capabilities.

## `golem.runtime.name : String` 

### Describes: Offer

Indicates the ExeUnit/Runtime required/provided. 
### Value enum
| Value      | Description                                        |
| ---------- | -------------------------------------------------- |
| "wasmtime" | Golem Factory's WASI runtime (based on `wasmtime`) https://github.com/golemfactory/ya-runtime-wasi |
| "vm" | Golem Factory's VM runtime (based on `QEMU`) https://github.com/golemfactory/ya-runtime-vm |
|            |                                                    |

### **Examples**

* `golem.runtime.name="wasmtime"` - declares that `wasmtime` is available as runtime on the provider node.
  
## `golem.runtime.version : Version` 

### Describes: Offer

Version of the ExeUnit/Runtime required/provided.

### **Examples**

* `golem.runtime.version="0.0.0"` - declares runtime version 0.0.0.

