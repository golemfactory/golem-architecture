# Computation Manifest Net namespace
This namespace defines properties used to specify details the Golem Computation Manifest network aspects. Applies constraints to networking. Currently, outgoing requests to the public Internet network are covered.

## Common Properties

N/A 

## Specific Properties

## `golem.srv.comp.manifest.net.inet.out.protocols : List[String]` 

### Describes: Demand

List of allowed outbound protocols. Currently **fixed at** `["http", "https"]`.

### **Examples**
* `golem.srv.comp.manifest.net.inet.out.protocols=["http","https"]` - HTTP/HTTPS allowed.


## `golem.srv.comp.manifest.net.inet.out.urls : List[String]` 

### Describes: Demand

List of allowed external URLs that outbound requests can be sent to.  

If unrestricted outbound access is requested this property must not be set.


### **Examples**
* `golem.srv.comp.manifest.net.inet.out.urls=["http://golemfactory.s3.amazonaws.com/file1", "http://golemfactory.s3.amazonaws.com/file2"]`


## `golem.srv.comp.manifest.net.inet.out.unrestricted.urls : Boolean` 

### Describes: Demand

This property means that the payload requires unrestricted outbound access. When present the value is always `true`. Either this property or the URL list in `golem.srv.comp.manifest.net.inet.out.urls` must be present.

The manifest must be considered invalid and outbound access should not be permitted in the following scenarios:
- neither `golem.srv.comp.manifest.net.inet.out.unrestricted.urls` nor `golem.srv.comp.manifest.net.inet.out.urls` is present
- both `golem.srv.comp.manifest.net.inet.out.unrestricted.urls` and `golem.srv.comp.manifest.net.inet.out.urls` are present
- property `golem.srv.comp.manifest.net.inet.out.unrestricted.urls` is present and it contains a value different from `true`

### **Examples**
* `golem.srv.comp.manifest.net.inet.out.unrestricted.urls=true`
