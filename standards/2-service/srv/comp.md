# Computation Platform properties 
Generic properties describing the Computation Platform aspects.

## Common Properties

(Not applicable)
  
## Specific Properties

## `golem.srv.comp.image_uri : String [Fact]` 

### Describes: Demand

Indicates the URI of a package/binary which is to be hosted by the Provider. This is a generic property, which, however, may be interpreted differently per each Computation Platform. Therefore, in a Computation Platform-specific namespace it is expected to specify the semantics of `golem.srv.comp.image_uri` property for tat Platform.

### **Examples**

* `golem.srv.comp.image_uri=["hash:sha3:44aba2d41021fac2a3b7af8a3ccfc0a3d4a435f9187ea7d5c162035b:http://64.123.43.186:8000/app-44aba2d4.yimg"]` - mentioned in Demand indicates that the Requestor expects the Provider to host the binary available from `http://64.123.43.186:8000/app-44aba2d4.yimg`, with SHA3 hash value as indicated (the example specifies a payload package for a WASM ExeUnit).
  


