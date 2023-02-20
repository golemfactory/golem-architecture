---
gap: 25
title: Golem Certificates structure
description: Describes certificates usage to enable more sensitive golem features.
author: <Witold Dzięcioł (@nieznanysprawiciel)>, evik (@evik42)
status: Draft
type: Feature
requires: GAP-5, GAP-4
---

## Abstract

Some Golem features are more sensitive because of potential malicious usage. A good example is outbound network traffic, where a malicious user could cause harm if they were able to access any internet resource. In GAP-5 and GAP-4 we decided to limit Requestor abilities by introducing Computational Manifest which defines allowed commands and accessible domains. Payload Manifest may be signed to confirm that the image has been verified to be safe to use.

Outbound network is not the only use case which requires certificates. Soon we will have other cases and we need to unify certificates usage to allow different sets of permissions.

## Motivation

Golem use cases allowing for interaction with the external world (like outbound, inbound network traffic) put `Providers` in danger of malicious behavior from the `Requestor` side. To alleviate this danger, in GAP-5 and GAP-4 we decided to limit Requestor abilities by introducing Computational Manifest which may be signed by a trusted party.

Currently `Providers` are able to choose certificates they trust, by adding them to Provider configuration. This means that any party is able to gain `Providers'` trust, distribute their own certificates and validate safety and security of different Payloads. Despite this, we are aware that gaining trust can be difficult, so Golem Factory has to take initiative to be at least the initial source of trust in the Network.

Safety and security of Payloads ran on `Providers'` machines can be ensured in few ways:

- Audit of Payload Manifest and VM image, to validate if permissions are low enough for image to be safe
- Signing legal Agreements with partner companies developing software on Golem, in which they commit not to do anything malicious
- Distribute permissions for signing Payloads to external companies taking responsibility for Payloads safety

When designing certificates structure that will allow to meet requirements mentioned above, we must keep in mind future use cases that might require signing with certificates:

- KYC
- Inbound network traffic

This means that certificates will be used to sign different kinds of permissions. In case of transferring certification to other companies or signing legal Agreements, we need to ensure that we control which permissions they will be able to use.

In the future of Golem Network we expect that new, independent features will be implemented by the community and they may need to use certification. This document aims to standardize this approach.

## Specification

We decided to create a JSON based certificate format to be used within the Golem network. We think that using a human readable text format enables users of Golem certificate to quickly assess what a certificate is about, what kind of permissions does it grant to its subject and what/who is the subject. We also think that we cannot think about all possible use cases upfront and by using a format that can be easily extended the content of the certificate can evolve with a use case and can contain elements that makes sense for some agents of the network while they are meaningless for others. We try to define a small set of properties that would be required and supported by the reference implementation but that does not prohibit anyone to include more details about the subject for example as they deem fit.

We are utilizing JSON Schema as a definition language to specify the different JSON objects. The schemas are open, meaning that they define required properties of each object in question but does allow extra properties to be defined.

### Signed Envelope

The purpose of the signed envelope is to defined a format that allows to cryptographically sign any kind of JSON object. The envelope contains a single signed object with any number of attached signatures. The only requirement for the signed data object is to contain a schema id so the structure of the object can be determined by looking at the `$schema` property.

#### Hash generation

To create and verify signatures, the signed data object needs to be processed via a hashing algorithm. The message digest depends on the binary data fed into the algorithm. It is a must to create the same binary stream from the JSON data for the algorithm to produce the same hash. Otherwise verification of the signature would not be possible. There are some common encoding methods used to include binary data into JSON but unfortunately these make the data unreadable for humans without preprocessing it (for example decoding a BASE64 encoded string). To avoid losing readability and allow embedding the signed envelope into other JSON structures, we decided to utilize the `JSON Canonicalization Scheme` described in [RFC 8785](https://www.rfc-editor.org/rfc/rfc8785). This scheme enables reproducible cryptographic operations on JSON data by defining a strict serialization scheme that results in the same binary stream on all platforms.

#### Signing process

TODO: elaborate

- Create hash of signed data
- Encode hash via private key
- Add signature data to signatures array

#### Verifying signatures

TODO: elaborate

- Read signature data from signatures array
- Create hash of signed data
- Verify that the encrypted signature encrypts the same hash as the one that was calculated from the data

### Certificate

TODO: elaborate

Certificate properties, with some explanation and why we selected those

#### Subject

#### Public Key

#### Verifying a certificate chain

##### Validity

##### Key usage

##### Permissions






XXXXXXXXXXXXXXXXX


We decided to use [X.509 Certificate Extensions (Section 4.2)](https://www.rfc-editor.org/rfc/inline-errata/rfc5280.html) to control key usage and permissions in the Golem network.

### Key usage

A certificate can be used to extend the chain of trust via signing other certificates or sign other data that is relevant to the network (for example a Computation Manifest).
To control what a certificate can sign, standard extensions defined in [RFC 5280 Section 4.2.1](https://www.rfc-editor.org/rfc/inline-errata/rfc5280.html) will be used.

Extensions
- Key Usage (OID: joint-iso-ccitt(2) ds(5) 29 15) extension must be marked as critical
-- Bit `digitalSignature` (0) is used to denote the ability to sign data but not certificates, this is used to grant permission to the signed data.
-- Bit `keyCertSign` (5) marks the ability to extend the chain of trust and sign other certificates. If this bit is set, then in `Basic Constraints` the `cA` boolean must be set to true

- Basic Constraints (OID: joint-iso-ccitt(2) ds(5) 29 19) extension must be present for certificates that can sign other certificates (`keyCertSign` bit is set in the `Key Usage` extension)
-- `cA` boolean must be set to true if the `keyCertSign` bit is set in the `Key Usage` extension
-- `pathLenConstraint` can be used optionally to limit the length of the certificate chain

### Golem Factory certificate extension

This certificate extension is used to grant permissions to a certificate to use certain capabilities of the golem network. Based on how the certificate is intended to be used (defined in the `Key Usage` extension) these permissions or a subset of them can be granted to ‘child’ certificates or other signed data (for example a Requestor identifier).
If this extension is present, it must be marked critical.

When processing this extension, the verifying party will check if the certificate contains all permissions required to fulfil the requested operation. It may ignore all other permission values (including ones it cannot process) as those have no effect on executing the request.

In a chain of certificates a certificate is only valid if the Golem Factory certificate extension contains a subset of the permissions of the signing certificate. The self signed root certificate is always considered to have all permissions even if this extension is not present. This can be limited by settings on the Agent application when setting up trust for the root certificate.

Structure of the Golem Factory certificate extension:
```
id-gf OBJECT IDENTIFIER ::= { iso(1) identified-organization(3) dod(6) internet(1) private(4) iana(1) 59850 }
id-gf-certificate OBJECT IDENTIFIER ::= { id-gf 1 }
id-gf-certificate-extension OBJECT IDENTIFIER ::= { id-gf-certificate 1 }


GolemFactoryCertificateExtension ::= CHOICE {
       permitAll BOOLEAN,
       permissions SEQUENCE SIZE (1..MAX) OF GolemPermissionId

GolemPermissionID ::= OBJECT IDENTIFIER
```

This GAP lists all the Golem permission IDs. When a new permission is created it should be listed here with the assigned OID.

Golem permission ids:
```
id-gf-permission OBJECT IDENTIFIER ::= { id-gf 2 }
id-gf-features OBJECT IDENTIFIER ::= { id-gf-permissions 1 }

id-gf-manifest-outbound OBJECT IDENTIFIER ::= { id-gf-features 1 }
-- Permission to use outbound capabilities with a request defining manifest
```

### Permission chain examples

Given a hierarchy:


| Certificate           | permissions               |
| --------------------- | ------------------------- |
| Golem Root            | all                       |
| Golem Intermediate    | manifest-outbound,inbound |
| Partner's certificate | manifest-outbound         |

then Payload Manifest signature is valid and `Parter's certificate` is allowed to sign Manifests, but has no rights to sign inbound related features.

On the other hand, if structure looks like this:

| Certificate           | permissions       |
| --------------------- | ----------------- |
| Golem Root            | all               |
| Golem Intermediate    | inbound           |
| Partner's certificate | manifest-outbound |

then `Partner's certificate` is not allowed to sign manifests and Agent application should reject such Proposals.
Note that the certificate chain is invalid, because `Partner's certificate` shouldn't be signed by `Golem Intermediate` in the first place.

### Limiting certificates chain length

When Golem Factory signs a certificate with certificate signing capabilities, it could extend the chain of trust indefinitely. In some cases this is not desired, to limit the length of the chain the `pathLenConstraint` field should be used in the `Basic Constraints` extension.
For certificates that are only allowed to sign certificates that must not extend the chain of trust, the `pathLenConstraint` must be set to 0.

## Rationale

### Why not use an existing certificate format?

We wanted to address two goals with the choice of certificate format:
- The certificate should be human readable, understanding it's content should be viable without the use of any kind of software other than a text editor
- Extending permissions or other parts of the certificate should be viable for the Golem Community via the

x509 - binary, extension requires a legal entity to govern OIDs,
JWS - it is a signature format that uses BASE64 encoding to preserve content in binary, uses x509 certificates for actual signer entity

XXXXXX

### Why can't we wait with introducing permissions?

Any future releases introducing sensitive features will put Providers in danger, because the same certificates could be used to sign new features.

### Are there any existing x509 extensions, that we could use?

Beside the standard extensions mentioned in this document, there is no extension that could host a freely extendable list of permissions.

### Why we need `all` permission?

Since the list of permissions is not exhaustive, we don't want to be forced to change certificates, when adding new features.
We must keep in mind that `all` permission should be used sparingly only for top level certificates. Most importantly, we shouldn't sign any certificates with the `all` permission for external parties.

## Backwards Compatibility

Since outbound network isn't release yet, we don't have any problems with backward compatibility.
This GAP aims to solve backward compatibility and security problems, that could arise in the future.

## Test Cases

## [Optional] Reference Implementation

### Generating openssl certificate

TODO: update this part and cert configuration

You can check [here](file://cert.confi) example config, which can be passed to openssl to generate certificate containing expected fields. To generate certificate run:

`openssl req -x509 -sha256 -days 1825 -newkey rsa:4096 -keyout rootCA.key -out rootCA.crt -config cert.config  --extensions golem_network_certificates`

than check if certificate contains expected fields with:

`openssl x509 -text -noout -in rootCA.crt`

Output looks like this:

```
...
        X509v3 extensions:
            1.3.6.1.4.1.59850.1.1:
                0...all..manifest-outbound
...
```

### Provider Agent changes

Provider agent supports using certificates without the `permission` extension.
We need following changes:

- Certificates chain permissions validation
- Check the outbound permission during negotiations (currently signature is enough)

No changes on the Requestor side are expected.

## Security Considerations

### Old yagna versions

If we don't implement certificate `permissions` before releasing the outbound network, Providers could accept Manifest signatures signed by certificates,
that don't have the `manifest-outbound` permission. This is safe, as long as we don't introduce new sensitive features.

### Private certificates keys management

We need to consider security issues related to storing and using Root Golem Certificate and important intermediate certificates.
We need internal company policies, who has permissions to decide about signing using certain certificates and who has access to them.

### Root Golem certificate

Root certificate is always self-signed certificate. We could sign it using [Golem Multisig](https://etherscan.io/address/0x7da82c7ab4771ff031b66538d2fb9b0b047f6cf9) contract to allow anyone validate its authenticity.

## Copyright

Copyright and related rights waived via [CC0](https://creativecommons.org/publicdomain/zero/1.0/).