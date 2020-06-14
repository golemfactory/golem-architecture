# WebAssembly Host 
This namespace defines properties used to indicate ability to host and execute a WebAssembly program.

## Common Properties

* [golem.inf](../../../0-commons/golem/inf.md)
* [golem.activity](../../../0-commons/golem/activity.md)

## Specific Properties

## `golem.srv.comp.wasm.api : List of String` 

Indicates the APIs supported by the WebAssembly runtime and available to WebAssembly packages. 

### Value enum
| Value        | Description                                                                                                                                |
| ------------ | ------------------------------------------------------------------------------------------------------------------------------------------ |
| "wasi"       | Indicates that WASI (WebAssembly System Interface) API is supported. For more detailed information on WASI look [here](https://wasi.dev/). |
| "emscripten" | Indicates support for [Emscripten](https://emscripten.org/) API in WebAssembly modules.                                                    |

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
| Value | Description                                 |
| ----- | ------------------------------------------- |
| "fs"  | (draft) Indicates "File System" capability. |

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
| Value | Description                                 |
| ----- | ------------------------------------------- |
| "fs"  | (draft) Indicates "File System" capability. |

### **Examples**
* `golem.srv.comp.wasm.wasi.caps=["fs"]` - Declares that WASI-compliat runtime offers File System operation capabilities.


## `golem.srv.comp.wasm.task_package : String` 

Indicates WebAssembly package which is to be hosted by the Provider. The Demand indicates the requested package by specifying the package URL and hash value. The hash value shall be validated by the Provider.

This property is a String value, which is a package address pointing to a WebAssembly package in the following format: 

`hash:<hash algorithm indicator>:<package hash value in hex>:<package URL>`

The WebAssembly package pointed at by `package URL` must follow the format specified [here](TODO: link do WebAssembly package format spec). It is expected that the binary indicated by the must return the hash value as indicated in `package hash value value in hex`.

### **Examples**

* `golem.srv.comp.wasm.task_package=["hash:sha3:44aba2d41021fac2a3b7af8a3ccfc0a3d4a435f9187ea7d5c162035b:http://64.123.43.186:8000/app-44aba2d4.yimg"]` - mentioned in Demand indicates that the Requestor expects the Provider to host the binary available from `http://64.123.43.186:8000/app-44aba2d4.yimg`, with SHA3 hash value as indicated.


