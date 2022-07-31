---
gap: 5
title: Computation Payload Manifest
description: Replacement for the `golem.srv.comp.task_package` property
author: mf (@mfranciszkiewicz)
status: Draft
type: Feature
requires (*optional): <GAP number TBD>
---

## Abstract

The GAP proposes a new `golem.srv.comp.payload` property aiming to replace the `golem.srv.comp.task_package`. 
The `payload` namespace covers URLs of computation payloads for multiple hardware architectures, an optional Computation
Manifest ([GAP](https://github.com/golemfactory/golem-architecture/pull/27)) and also an optional Payload Manifest 
signature.

## Motivation

The main motivation behind the proposed changes is to provide a source of trust for the executed Payload. The Manifest 
may include a default, safe set of actions Requestors can execute on a Provider's machine, approved by a trusted 
Authority. This source of trust allows for implementing high-risk features, such as Internet access for Runtimes, where 
all traffic is routed via Provider's OS. In order to prevent unwanted actions in a public network on Provider's behalf, 
she must remain in control of all the actions executed by the Requestor.

The secondary objective is to provide multiple locations of Payloads, targeting different hardware architectures and 
meeting specific software constraints.

None of the above are possible with the `golem.srv.comp.task_package` property.

## Specification

Computation Payload Manifest signatures are verified by either the Provider Agent, the ExeUnit Supervisor or both.
Payload and Computation manifests are not expected to have constraints put on them. 

### `golem.srv.comp.payload: String`

Base64-encoded JSON manifest.

### `golem.srv.comp.payload.sig: String`

Base64-encoded signature of the base64-encoded manifest.

### `golem.srv.comp.payload.sig.algorithm: String`

Digest algorithm used to generate manifest signature.

### `golem.srv.comp.payload.cert: String`

Base64-encoded certificate in DER format.

### Manifest format

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

## Rationale

### Base64 encoding

Base64-encoded JSON manifest enables straightforward checksum calculation, 
without concern for JSON property ordering.

## Backwards Compatibility

Providers need to declare that they support payload manifests by setting 
the `golem.srv.caps.payload-manifest` property to `true`.

The `golem.srv.comp.task_package` property will still be supported for 
compatibility reasons, until it's removed (2 minor versions).

## Test Cases

N/A

## [Optional] Reference Implementation

N/A

## Security Considerations

Users may add their own trusted Authority certificates without realizing the potential danger of their actions.

## Copyright
Copyright and related rights waived via [CC0](https://creativecommons.org/publicdomain/zero/1.0/).
