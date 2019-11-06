# WebAssembly Host 
This namespace defines properties used to indicate ability to host and execute a WebAssembly program.

## Common Properties

* [golem.inf](../../../0-commons/golem/inf.md)
* [golem.activity](../../../0-commons/golem/activity.md)

## Specific Properties

## `golem.srv.comp.wasm.api : List of String` 

Indicates the APIs supported by the WebAssembly runtime and available to WebAssembly packages. 

### Value enum
|Value| Description |
|---|---|
|"wasi"| Indicates that WASI (WebAssembly System Interface) API is supported. For more detailed information on WASI look [here](https://wasi.dev/). |
|"emscripten"| Indicates support for [Emscripten](https://emscripten.org/) API in WebAssembly modules. |

### **Examples**
* `golem.srv.comp.wasm.api=["wasi","emscripten"]` - Declares both WASI and Emscripten as available on the Provider.

## `golem.srv.comp.wasm.emscripten.js : Version` 

Indicates the version of JavaScript supported by the Emscripten-compliant runtime.

### **Examples**
* `golem.srv.comp.wasm.emscripten.js:Version="9.0.0"` - Declares JavaScript v9.0.0 (ECMAScript 2018) as supported. Note the implied `Version` property type.

## `golem.srv.comp.wasm.emscripten.caps : List of String` 

Indicates the runtime capabilities of the Emscripten-compliant runtime.

TODO: Need to research on relevant capability catalogue

### Value enum
|Value| Description |
|---|---|
|"fs"| (draft) Indicates "File System" capability. |

### **Examples**
* `golem.srv.comp.wasm.emscripten.caps=["fs"]` - Declares that Emscripten-compliant runtime offers File System operation capabilities.


## `golem.srv.comp.wasm.wasi.version : Version` 

Indicates the version of WASI API supported by the runtime.

### **Examples**
* `golem.srv.comp.wasm.wasi.version:Version="0.1.0"` - Declares WASI v0.1.0 as supported. Note the implied `Version` property type.

## `golem.srv.comp.wasm.wasi.caps : List of String` 

Indicates the runtime capabilities of the WASI-compliant runtime.

TODO: Need to research on relevant capability catalogue

### Value enum
|Value| Description |
|---|---|
|"fs"| (draft) Indicates "File System" capability. |

### **Examples**
* `golem.srv.comp.wasm.wasi.caps=["fs"]` - Declares that WASI-compliat runtime offers File System operation capabilities.


## `golem.srv.comp.wasm.task_package : List of String` 

Indicates WebAssembly packages which are to be hosted by the Provider. The Offer may either declare specific images as available, or indicate the whole property as dynamic, so that the actual image required by the Requestor is specified by the Demand. In the latter scenario, during the negotiation phase the Provider shall decide whether the image indicated in Demand is trustworthy (eg. by checking an internal whitelist).

This property is a list of String values, where each item in the list is a package URL pointing to a WebAssembly package in the Golem Factory WASM format specified in this [article](https://github.com/golemfactory/golem/wiki/Launching-Wasm-tasks-in-Golem).

The repo of WASM task packages maintained by Golem Factory can be found [here](https://github.com/golemfactory/wasm-store).

### **Examples**

* `golem.srv.comp.wasm.task_package=["https://github.com/golemfactory/wasm-store/tree/master/flite/flite.zip"]` - declares Golem Factory's 'text-to-speech' sample WASM package.


