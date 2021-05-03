# Computation Platform properties 
Generic properties describing the Computation Platform aspects.

## Common Properties

(Not applicable)
  
## Specific Properties

## `golem.srv.comp.expiration : Number (int32)`

### Describes: Demand

Indicates the expiration time of the Agreement which is being negotiated. This is expressed as 
Javascript timestamp (number of milliseconds since 1970-01-01 00:00:00 UTC, as returned by `Date.now()`)

## Expiring vs Perpetual Agreements

For scenarios where perpetual (non-expiring) Agreements are required, eg. long-running services, 
the `golem.srv.comp.expiration` may remain **unspecified**. This indicates the intent of Demand issuer 
to negotiate a *Perpetual* Agreement.

### **Examples**
* `golem.srv.comp.expiration=1608556352458` - specifies the expiration timestamp of the Agreement.

## `golem.srv.comp.task_package : String` 

### Describes: Demand

Indicates the URI of a package/binary which is to be executed by the Provider. This is a generic property, which, however, may be interpreted differently per each Computation Platform. Therefore, in a Computation Platform-specific namespace it is expected to specify the semantics of `golem.srv.comp.task_package` property for that Platform.

### **Examples**

* `golem.srv.comp.task_package="hash:sha3:44aba2d41021fac2a3b7af8a3ccfc0a3d4a435f9187ea7d5c162035b:http://64.123.43.186:8000/app-44aba2d4.yimg"` - mentioned in Demand indicates that the Requestor expects the Provider to host the binary available from `http://64.123.43.186:8000/app-44aba2d4.yimg`, with SHA3 hash value as indicated (the example specifies a payload package for a WASM ExeUnit).
  


