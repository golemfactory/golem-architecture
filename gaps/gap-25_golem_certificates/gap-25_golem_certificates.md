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

We are utilizing JSON Schema as a definition language to specify the different JSON objects. The schemas are open, meaning that they define required properties of each object in question but does allow extra properties to be defined. These extra properties are not processed by the reference implementation.

Our JSON Schema defines hexadecimal string encoding for properties holding binary data instead of BASE64 encoding. We only define a handful of properties containing binary data so it would not save much space, but when storing node ids for example (public keys of a node in the Golem Network) it is preferred to store it in a format that is used by other parts of the network (logging, user interfaces) for easier matching. This does not prevent anyone to add larger chunks of binary data with a more appropriate encoding in additional properties that are not controlled by our schemas.

### Schemas

#### Certificate

The purpose of the certificate is to allow creation of digital signatures by a verified entity. The subject of the certificate is the entity connected to the private key of a key pair where the public key is part of the certificate structure. This enables using the certificate in a PKI system. We chose a set of required properties that are essential to use the certificate for signing and facilitates fine grained control of permissions in the Golem Network.

When a certificate is signed, all properties in the `certificate` object are included in the signature not only the required properties. This allows inclusion of extra information that inherits the same cryptographic tamper protection as the essential data.

The schema file can be found in the `schemas/v1` folder and accessible at `https://golem.network/schemas/v1/certificate.schema.json"`.

##### Subject

Subject property defines the entity to which the certificate is issued. The content of this node must be verified by the issuer to be representative of the entity. We define a small set of required properties but this does not prohibit any issuer to require more data and include it in the certificate.

The required properties are:
- `subject.displayName` the name to be displayed when presenting the certificate to a user
- `subject.contact.email` an email address where the subject can be reached for any kind inquiry

The property `subject.legalEntity` is an example for extra information that can be included in the subject property of the certificate if it makes sense for some use case, but it is not required for using the certificate with the Golem Network reference implementation.

##### Public Key

The `publicKey` property stores the public key of the subject's key pair to allow verification of their signatures. The node defines the following properties:
- `publicKey.algorithm` the encryption algorithm for which the key pair is generated for
- `publicKey.parameters` any parameters that are relevant to key or the algorithm for proper usage
- `publicKey.key` the hexadecimal encoded string representation of the public key optionally with a "0x" prefix

##### Validity Period

The `validityPeriod` property defines the time interval for which the certificate is valid. It is defined by the start of the term in `validityPeriod.notBefore` property and the end of the term in the `validityPeriod.notAfter` property.

##### Key usage

The `keyUsage` property defines how the key attached to the certificate can be used. We define the following usages:
- `signCertificate` the key can be used to sign other certificates to extend the chain n trust
- `signManifest` the key can be used to sign payload manifests (defined in GAP-5)
- `signNode` the key can be used to sign a node descriptor (defined in GAP-XX)
- `all` the key can be used without restrictions, including use cases that are defined after the certificate was created

##### Permissions

Details of how the holder of the certificate is permitted to use the Golem network. The content of this property is governed by the Permissions schema.
##### Signature

This object contains the details of the signature validating the subject and providing tamper protection for properties in the `certificate` object. It defines the following properties:
- `algorithm` the cryptographic algorithm that was used to create the signature, it requires two details:
  - `hash` the hash function used to create the fingerprint of the signed data
  - `encryption` the cryptographic algorithm used to encrypt the fingerprint
- `signer` for certificates the signer can be an object containing a signed certificate or the string `self` meaning that the certificate was self signed (aka Root certificate)
- `value` the encrypted fingerprint that can be decrypted via the signer's public key for verification

#### Permissions

This schema defines the available permission control in the Golem network. It can be found in the `schemas/v1` folder or can be accessed at `https://golem.network/schemas/v1/permissions.schema.json`.

Permissions have two forms:
- the string `all` which means that the subject is permitted to use all capabilities (present and future) of the Golem network
- an object containing the detailed permissions of the subject, this object contains the following properties:
  - `outbound` property contains the detailed permissions related to the `outbound` feature
    - a string value `unrestricted` means that the subject can use the `outbound` feature without restriction
    - an object containing information about the feature restrictions, it contains:
        - `urls` property, which contains an array of URIs that the subject can access via the `outbound` feature. This list has the same semantics as `golem.srv.comp.manifest.net.inet.out.urls` property defined in [GAP-4 Computation Manifest](https://github.com/golemfactory/golem-architecture/blob/master/gaps/gap-4_comp_manifest/gap-4_comp_manifest.md#gcrs-golemsrvcompmanifest-namespace)


### Signature creation and verification

#### Serialization method

To create and verify signatures, the signed data object needs to be processed via a hashing algorithm. The message digest depends on the binary data fed into the algorithm. It is a must to create the same binary stream from the JSON data for the algorithm to produce the same hash. Otherwise verification of the signature would not be possible. There are some common encoding methods used to include binary data into JSON but unfortunately these make the data unreadable for humans without preprocessing it (for example decoding a BASE64 encoded string). To avoid losing readability and allow embedding the certificate into other JSON structures, we decided to utilize the `JSON Canonicalization Scheme` described in [RFC 8785](https://www.rfc-editor.org/rfc/rfc8785). This scheme enables reproducible cryptographic operations on JSON data by defining a strict serialization scheme that results in the same binary stream on all platforms.

#### Creating a signature

In order to cryptographically sign a certificate the following steps need to be taken:

1. Create a binary representation of the `certificate` property by serializing the content via the above mentioned `JSON Canonicalization Scheme`
2. Feed the binary data created in step 1. into a hashing algorithm that is compatible with the chose encryption to obtain the fingerprint of the certificate
3. Encrypt the fingerprint obtained in step 2. via the encryption algorithm supported by the private key of the signer. The private key must be the one connected to the public key specified in the signer's certificate. This encrypted fingerprint is the signature.
4. Create the `signature` property in the JSON certificate and add the following details
  - name of the hash and encryption algorithm used to create the signature into the `algorithm` property
  - binary data of the signature into `value` property
  - the `signer` property of the `signature` should contain the signing certificate or the string `self` in case of self signature

#### Verifying a signature

1. Create a binary representation of the `certificate` property by serializing the content via the above mentioned `JSON Canonicalization Scheme`
2. Feed the binary data created in step 1. into the hashing algorithm defined in the `signature.algorithm.hash` property of the certificate to obtain the fingerprint of the certificate
3. Decrypt the signature stored in `signature.value` using the public key of the signer (the `certificate.publicKey` property of the signing certificate) and the encryption algorithm defined in `signature.algorithm.encryption`. The result of this step will be the signed fingerprint.
4. Verify that the fingerprint obtained in step 2. and step 3. are the same.


#### Verifying a certificate chain







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