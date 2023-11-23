---
gap: GAP-33
title: Attestation types
description: Specification of universal attestation types for Golem ecosystem
author: 
status: Draft
type: Standards
requires (*optional): GAP-25
---

## Abstract
Abstract is a multi-sentence (short paragraph) technical summary. This should be a very terse and human-readable version of the specification section. Someone should be able to read only the abstract to get the gist of what this specification does.

## Motivation
The motivation section should describe the "why" of this GAP. What problem does it solve? What benefit does it provide to the Golem ecosystem? What use cases does this GAP address?

## Specification

### Attestation elements
- attestation type attribute - which attestation type is used
- attestation params attribute/data - configuration for the attestation to apply
- attestation scope - what is "attested" (eg. a request content, a descriptor content)
- attestation artifact - the artifact confirming the attestation (eg. the signature)
- context - additional info related tot he use case (eg. noteId of the node which had sent the request)

### Attestation plugin abstraction
Each "attestation mechanism" can be implemented in a form of "plugin", which is generic and offers a set of standard operations.
Operations:
`GetAttestationArtifact(attestation params, attestation scope artifact) -> attestation artifact`
`Validate(attestation artifact, attestation params, attestation scope artifact, context) -> bool`

### Attestation types

| Attestation type  | Description |
| ----------------- | ----------- |
| `golem-cert-sig`  | Attestation via a digital signature created using Golem Certificate (GAP-25) |
| `x509-cert-sig`   | Attestation via a digital signature created using X.509 Certificate          |
| `token`           | Attestation based on the digital token presented by the issuer/initiator of an action, request, process |
| `node-id`         | Attestation based on the Golem Identity of the node which initiated an action, request, process |
| `golem-multi-sig` | TODO: needs to specify the standard to be used by this attestation type - following a Golem standard |
| `x509-multi-sig`  | TODO: needs to specify the standard to be used by this attestation type | 

### Attestation type spec formats - JSON

Specification of attestation types via a JSON object:

```
// Attestation descriptor object
{
    "type": "golem-cert-sig",  // attestation type attribute - must be one of standard attestation types (enum)
    "params" : {
        // attestation type-specific parameter set for the attestation type/plugin
    },
    "content" : {
        // attestation type-specific object containing the attestation artifact
    }
}
```

#### JSON attestation schema - `golem-cert-sig`
- `params` schema
- `content` schema
```
{
    /* attestation artifact, object, schema specific for selected attestation type */
    "algorithm": {
        "hash": "sha512",
        "encryption": "EdDSA"
    },
    "value": "73814ddd3786786010849519fc30edaeec0d20d1df63d9d171da05cdc2b8419580219feca183a1834bd1e8ad34e0b471b9a59ef64087b32b545748c0e40a1c0c",
    "signer": {
        "$schema": "https://golem.network/schemas/v1/certificate.schema.json",
        "certificate": {
            ...
        },
        "signature": {
            ...
        }
    }
}
```

#### JSON attestation schema - `x509-cert-sig`
- `params` schema
- `content` schema
...reference to some JSON Signature standard?

#### JSON attestation schema - `token`  
- `params` schema
- `content` schema

#### JSON attestation schema - `golem-multi-sig`
- `params` schema
- `content` schema

#### JSON attestation schema - `x509-multi-sig`
- `params` schema
- `content` schema




## Rationale
The rationale fleshes out the specification by describing what motivated the design and why particular design decisions were made. It should describe alternate designs that were considered and related work.

## Backwards Compatibility
All GAPs that introduce backwards incompatibilities must include a section describing these incompatibilities and their severity. The GAP **must** explain how the author proposes to deal with these incompatibilities.

## Test Cases
Test cases are very useful in summarizing the scope and specifics of a GAP.  If the test suite is too large to reasonably be included inline, then consider adding it as one or more files in `./gaps/gap-draft_title/` directory.

## [Optional] Reference Implementation
An optional section that contains a reference/example implementation that people can use to assist in understanding or implementing this specification.  If the implementation is too large to reasonably be included inline, then consider adding it as one or more files in `./gaps/gap-draft_title/`.

## Security Considerations
All GAPs must contain a section that discusses the security implications/considerations relevant to the proposed change. Include information that might be important for security discussions, surfaces risks and can be used throughout the life cycle of the proposal. E.g. include security-relevant design decisions, concerns, important discussions, implementation-specific guidance and pitfalls, an outline of threats and risks and how they are being addressed. 

## Copyright
Copyright and related rights waived via [CC0](https://creativecommons.org/publicdomain/zero/1.0/).