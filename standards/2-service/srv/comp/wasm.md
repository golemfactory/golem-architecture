# WebAssembly Host 
This namespace defines properties used to indicate ability to host and execute a WebAssembly program.

## Common Properties

* [golem.inf](../../../0-commons/golem/inf.md)
* [golem.activity](../../../0-commons/golem/activity.md)

### 'golem.srv.comp.image_uri' semantics

For WASM platform the `golem.srv.comp.image_uri` property indicates WebAssembly package which is to be hosted by the Provider. The Demand indicates the requested package by specifying the package URL and hash value. **The hash value shall be validated by the Provider.**

This property is a String value, which is a package address pointing to a WebAssembly package in the following format: 

`hash:<hash algorithm indicator>:<package hash value in hex>:<package URL>`

The WebAssembly package pointed at by `package URL` must follow the format specified [here](TODO: link do WebAssembly package format spec). It is expected that the binary indicated by the must return the hash value as indicated in `package hash value value in hex`.

## Specific Properties

## `golem.srv.comp.wasm.wasi.version : Version [Fact]` 

### Describes: Offer

Indicates the version of WASI API supported by the runtime.

### **Examples**
* `golem.srv.comp.wasm.wasi.version:Version="0.1.0"` - Declares WASI v0.1.0 as supported. Note the implied `Version` property type.

