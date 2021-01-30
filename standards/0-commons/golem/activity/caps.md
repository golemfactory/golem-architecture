# Golem Activity Capabilities
Namespace that describes Activity API capabilities, which can be perceived as "segments" of Activity API functionality.

Example segments can be:

- ways of transferring/specifying input for activity running on Provider node
- ways of obtaining/receiving output of activity running on Provider node

**Important:** 
- this standard also specifies both "caps pattern" of specification, ie. how an Offer indicates capabilites supported, and what capability namespaces are there.
- the "caps pattern" shall define the versioning and backward-compatibility specification guidelines.

## Specific Properties

## `golem.activity.caps.transfer.protocol : List[String]`

### Describes: Offer

Indicates the data transmission protocols available for TRANSFER operation on this Provider/ExeUnit.
### Value enum
| Value   | Description                                                                                                  |
| ------- | ------------------------------------------------------------------------------------------------------------ |
| "http"  | HTTP Protocol                                                                                                |
| "https" | HTTPS Protocol                                                                                               |
| "gftp"  | Golem File Transfer Protocol - proprietary protocol allowing for transfer of data over the Golem P2P network |
### **Examples**
* `golem.activity.caps.transfer.protocol:List=["https"]` - Declares availability of HTTPS protocol for data transfer
* `golem.activity.caps.transfer.protocol=["http","https","gftp"]` - Declares availability of HTTP, HTTPS and "GFTP" protocols for file transfer
