# Computation Runtime Specifiation 
Specification of ExeUnit/Runtime to host the resources provided.

## Common Properties

(Not applicable)
  
## Specific Properties

## `golem.runtime.name : String [Fact]` 

### Describes: Offer

Indicates the ExeUnit/Runtime required/provided. 
### Value enum
| Value      | Description                                        |
| ---------- | -------------------------------------------------- |
| "wasmtime" | Golem Factory's WASI runtime (based on `wasmtime`) |
|            |                                                    |

### **Examples**

* `golem.runtime.name="wasmtime"` - declares that `wasmtime` is available as runtime on the provider node.
  
## `golem.runtime.version : Version [Fact]` 

### Describes: Offer

Version of the ExeUnit/Runtime required/provided.

### **Examples**

* `golem.runtime.version="0.0.0"` - declares runtime version 0.0.0.


