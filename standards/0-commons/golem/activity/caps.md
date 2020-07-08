# Golem Activity Capabilities
Namespace that describes Activity API capabilities, which can be perceived as "segments" of Activity API functionality.

Example segments can be:

- ways of transferring/specifying input for activity running on Provider node
- ways of obtaining/receiving output of activity running on Provider node

**Important:** 
- this standard also specifies both "caps pattern" of specification, ie. how an Offer indicates capabilites supported, and what capability namespaces are there.
- the "caps pattern" shall define the versioning and backward-compatibility specification guidelines.

## Specific Properties

## `golem.activity.caps.transfer.format : List[String]`

### Describes: Offer

Indicates the transferred file format which can be decoded (eg. decompressed) by the Provider/ExeUnit. This indicates possible archive file formats which can be transferred and seamlessly unpacked on Provider side (eg. without the need to explicitly perform additional decompression activities).

### Value enum
| Value    | Description                     |
| -------- | ------------------------------- |
| "zip"    | zip compression                 |
| "zip.0"  | zip store only - no compression |
| "tar.gz" | tar with gzip compression       |
| "tar.xz" | tar with lzma compression       |

### **Examples**
* `golem.activity.caps.transfer.format=["zip"]` - Declares availability of ZIP codec for file transfer in Provider ExeUnit
* `golem.activity.caps.transfer.format=["zip","tar.gz","tar.xz"]` - Declares availability of ZIP and TAR (gzip and lzma) codecs for file transfer

## `golem.activity.caps.transfer.protocol : List[String]`

### Describes: Offer

Indicates the data transmission protocols available for TRANSFER operation on this Provider/ExeUnit.
### Value enum
| Value   | Description                                                                                         |
| ------- | --------------------------------------------------------------------------------------------------- |
| "http"  | HTTP Protocol                                                                                       |
| "https" | HTTPS Protocol                                                                                      |
| "gft"   | Golem File Transfer - proprietary protocol allowing for transfer of data over the Golem P2P network |
### **Examples**
* `golem.activity.caps.transfer.protocol:List=["https"]` - Declares availability of HTTPS protocol for data transfer
* `golem.activity.caps.transfer.protocol=["http","https","gft"]` - Declares availability of HTTP, HTTPS and "GFT" protocols for file transfer
