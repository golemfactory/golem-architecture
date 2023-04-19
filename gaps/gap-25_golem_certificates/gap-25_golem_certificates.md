---
gap: GAP-25
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

Golem use cases allowing for interaction with the external world (like outbound, inbound network traffic) put `Providers` in danger of malicious behaviour from the `Requestor` side. To alleviate this danger, in GAP-5 and GAP-4 we decided to limit Requestor abilities by introducing Computational Manifest which may be signed by a trusted party.

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

This description of the schemas and values of properties should be considered complete. In case a future GAP would introduce new properties or extend values of existing ones, this GAP should be updated to serve as a comprehensive documentation of Golem Certificates and related JSON structures.

#### Certificate

The purpose of the certificate is to allow creation of digital signatures by a verified entity. The subject of the certificate is the entity connected to the private key of a key pair where the public key is part of the certificate structure. This enables using the certificate in a PKI system. We chose a set of required properties that are essential to use the certificate for signing and facilitates fine grained control of permissions in the Golem Network.

When a certificate is signed, all properties in the `certificate` object are included in the signature not only the required properties. This allows inclusion of extra information that inherits the same cryptographic integrity protection as the essential data.

The schema file can be found in the `schemas/v1` folder and accessible at `https://golem.network/schemas/v1/certificate.schema.json`.

##### Subject

Subject property defines the entity to which the certificate is issued. The content of this node must be verified by the issuer to be representative of the entity. We define a small set of required properties but this does not prohibit any issuer to require more data and include it in the certificate.

The required properties are:
- `subject.displayName` the name to be displayed when presenting the certificate to a user
- `subject.contact.email` an email address where the subject can be reached for any kind inquiry

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
- `signManifest` the key can be used to sign payload manifests (defined in GAP-5) /manifest will not support Golem certificates right now, it requires some update to GAP-5 and the implementation/
- `signNode` the key can be used to sign a node descriptor (defined in GAP-XX)
- `all` the key can be used without restrictions, including use cases that are defined after the certificate was created

##### Permissions

Details of how the holder of the certificate is permitted to use the Golem network. The content of this property is governed by the Permissions schema.
##### Signature

This object contains the details of the signature validating the subject and providing integrity protection for properties in the `certificate` object. It defines the following properties:
- `algorithm` the cryptographic algorithm that was used to create the signature, it requires two details:
  - `hash` the hash function used to create the fingerprint of the signed data
  - `encryption` the cryptographic algorithm used to encrypt the fingerprint
- `signer` for certificates the signer can the string `self` meaning that the certificate was self signed (aka Root certificate) or an object containing a signed certificate. We opted to include the full certificate as it is easier to handle and understand when opening the JSON certificate in a text editor. We do not anticipate long certificate chains where this would a problem and we think that requiring the certificate to be accessible via the internet would limit the usage for individuals.
- `value` the hexadecimal encoded string representation of the encrypted fingerprint optionally with a "0x" prefix, it can be decrypted via the signer's public key for verification

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
2. Sign the binary data with the chosen signature algorithm using the private key of the signer. The private key must be the one connected to the public key specified in the signer's certificate.
3. Create the `signature` property in the JSON certificate and add the following details
    - name of the hash and encryption algorithm (details of the signature algorithm) used to create the signature into the `algorithm` property
    - binary data of the signature into `value` property
    - the `signer` property of the `signature` should contain the signing certificate or the string `self` in case of self signature

#### Verifying a signature

1. Verify that the signing certificate contained in `signature.signer` is allowed to sign certificates (it's `keyUsage` property is either set to the string `all` or the list contains `signCertificate`).
2. Create a binary representation of the `certificate` property by serializing the content via the above mentioned `JSON Canonicalization Scheme`.
3. Using the signature algorithm matching the one used for creating the signature (`signature.algorithm.hash` and `signature.algorithm.encryption`) and the signer's public key (the `certificate.publicKey` property of the signing certificate), verify that the signature is valid for the binary data obtained in step 2.

### Verifying a certificate chain

A signed certificate is only valid if the cryptographic signature is valid and the following constraints are fulfilled:
- The certificate cannot have a validity period that is not fully contained in the signer's validity period. The certificates can have the same `notBefore` and `notAfter` values but the signer cannot sign any certificate that would 'outlive' it.
  - A certificate chain is valid (from validity period point of view) if the above is true, but when looking at if a certificate can be used right now, then the timestamp of usage must not be before the `notBefore` timestamp and must not be after the timestamp in the `notAfter` property. 
- The certificate cannot have key usage value that is not granted to the signer.
- The signer certificate must have the permission to sign certificates (it's `keyUsage` property is either set to the string `all` or the list contains `signCertificate`).
- The certificate cannot have any permission that is not granted to the signer.
  - If the signer has the `all` permission, it can grant any permission to the certificate.
  - If the signer has `unrestricted` `outbound` permission, it can grant either `unrestricted` or restricted (via the list of URIs) access to the outbound feature.
  - If the signer has a list of permitted URIs for `outbound` permission, it can only grant access to URIs that it has permission to access. The URI list in the signed certificate must be a subset of the list of URIs in the signer certificate.

In general the signing certificate cannot bestow any kind of permission that would allow the signed certificate to perform an action on the Golem network or related infrastructure (now or in the future) that the signing certificate itself is not capable of. 

## Rationale

### Why not use an existing certificate format?

We wanted to address two goals with the choice of certificate format:
- The certificate should be human readable, understanding it's content should be viable without the use of any kind of software other than a text editor
- Extending permissions or other parts of the certificate should be viable for the Golem Community via the GAP process, without the need to create or update elaborate software tools

We believe that Golem Certificates provide considerable value and are a good investment from the tooling perspective.

#### X.509

Unfortunately the industry standard [X.509](https://www.rfc-editor.org/rfc/rfc5280) certificate is hard to work with. Custom extension can be created but these would be hard to process with existing tools. Especially if we take into consideration the need to not just verify the signature but also validate the contents of the permission extension against the same extension in the signer certificate. This would require custom tooling. We did not want to compromise on our goals if we have to spend the resources to create custom tooling anyhow.

#### JWS 

[JSON Web Signature](https://www.rfc-editor.org/rfc/rfc7515) is a JSON based format to support signed content. Although the data is represented in JSON, the signed data is handled in BASE64 encoded format to guarantee binary consistency and verification of the signature.

### Why we need `all` permission?

Since the list of permissions can be extended in the future, we don't want to be forced to change root certificates with each additional feature.
We must keep in mind that `all` permission should be used sparingly only for top level certificates. Most importantly, we shouldn't sign any certificates with the `all` permission for external parties.

## Backwards Compatibility

Golem Certificates are not intended to replace X.509 certificates used for [Payload Manifest](https://github.com/golemfactory/golem-architecture/blob/master/gaps/gap-5_payload_manifest/gap-5_payload_manifest.md) right now, but in the future this is a possibility to introduce certificate based permission control.

## Reference Implementation

https://github.com/golemfactory/golem-certificate

## Security Considerations

### Private certificates keys management

We need to consider security issues related to storing and using Root Golem Certificate and important intermediate certificates.
We need internal company policies, who has permissions to decide about signing using certain certificates and who has access to them.

### Provider trust

Providers should only trust certificate authorities who they can verify and consider safe.

### Default signature algorithm

The reference implementation, [Golem certificate](https://github.com/golemfactory/golem-certificate) library by default is using EdDSA signatures with Ed25519 scheme. This provides security equivalent of 128 bit symmetric keys. The schema does not dictate any specific algorithms and implementors can use different ones based on their needs.

## Copyright

Copyright and related rights waived via [CC0](https://creativecommons.org/publicdomain/zero/1.0/).