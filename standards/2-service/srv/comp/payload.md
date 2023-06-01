# Payload Manifest 
This namespace defines properties used to specify the Golem Payload Manifest (as originally designed in [GAP-5](https://github.com/golemfactory/golem-architecture/blob/master/gaps/gap-5_payload_manifest/gap-5_payload_manifest.md)).

Computation Payload Manifest signatures are verified by either the Provider Agent, the ExeUnit Supervisor or both.
Payload and Computation manifests are not expected to have constraints put on them. 

### Payload Manifest example

```json
  {
    "version": "0.1.0",
    "createdAt": "2020-12-12T12:12:12.1200012",
    "expiresAt": "2022-12-12T12:12:12.1200012",
    
    "metadata": {
      "name": "Service1",
      "description": "Description of Service1",
      "version": "0.1.1",
      "authors": [
        "mf <mf@golem.network>",
        "ng <ng@golem.network>"
      ],
      "homepage": "https://github.com/golemfactory/s1"
    },
    
    "payload": [
      {
        "platform": {
          "arch": "amd64",
          "os": "win32",
          "osVersion": "6.1.7601"
        },
        "urls": [
          "https://golemfactory-payloads.s3.amazonaws.com/payloads/s1-amd64-win32",
          "ipfs://Qa.........."
        ],
        "hash": "sha3-224:deadbeef01"
      },
      {
        "platform": {
          "arch": "ARMv7E-M",
          "os": "linux"
        },
        "urls": [
          "https://golemfactory-payloads.s3.amazonaws.com/payloads/s1-armv7e-m",
          "ipfs://Qb.........."
        ],
        "hash": "sha3-224:deadbeef02"
      }
    ],
    
    "compManifest": {}
  }
```

`version` and `metadata.version` follow SemVer 2.0 specification.

## Common Properties

N/A

## Specific Properties

## `golem.srv.comp.payload: String` 

### Describes: Demand

Base64-encoded JSON manifest.

## `golem.srv.comp.payload.sig: String` 

### Describes: Demand

Base64-encoded signature of the base64-encoded manifest.

## `golem.srv.comp.payload.sig.algorithm: String` 

### Describes: Demand

Digest algorithm used to generate manifest signature.

## `golem.srv.comp.payload.cert: String` 

### Describes: Demand

Base64-encoded certificate in DER format.




