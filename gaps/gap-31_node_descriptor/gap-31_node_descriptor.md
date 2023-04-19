---
gap: GAP-31
title: Signed node descriptor
description: Define node descriptor document format to enable requestor verification.
author: evik (@evik42)
status: Draft
type: Feature
requires: GAP-25
---

## Abstract

This GAP introduces signed node descriptors that allows Requestors provide identification data in Demands and enables Providers to restrict access to certain features based on identification.

## Motivation

So far Providers had very limited options to restrict access to sensitive features (for example outbound network access). They could accept audited payloads (utilizing [Computation Payload Manifest](https://github.com/golemfactory/golem-architecture/blob/master/gaps/gap-5_payload_manifest/gap-5_payload_manifest.md)) and allow requests with those payloads to access the internet without restrictions or they could have set a whitelist to allow restricted outbound access to requests with any kind of payload.

This GAP introduces a method to allow restricting features based on the Requestor's identity. Providers will be able to trust some parties and give better access to Requestor nodes associated with those parties instead of basing their trust on the actual payload of a request. This gives larger freedom for the Requestor to use the network and develop their use cases. Node descriptors also allow identifying parties to restrict the permissions granted to each node separately, deriving from the permission setup in [Golem Certificates](https://github.com/golemfactory/golem-architecture/blob/master/gaps/gap-25_golem_certificates/gap-25_golem_certificates.md).

## Specification

Building on [Golem certificate structure](https://github.com/golemfactory/golem-architecture/blob/master/gaps/gap-25_golem_certificates/gap-25_golem_certificates.md) the Node descriptors are also utilizing JSON representation. This allows us to reuse previously defined data structures and create a uniform environment for digital signatures.

Node descriptors are the first things to utilize Golem certificates and allow a verification of identity up to a trusted party. At the same time this chain of trust also enables restricting network permissions at each step, allowing the creation of node descriptors with the minimum required permissions in a given context.

This GAP introduces the key usage `signNode` for [Golem certificates](https://github.com/golemfactory/golem-architecture/blob/master/gaps/gap-25_golem_certificates/gap-25_golem_certificates.md#key-usage).

### Schema

The schema heavily relies on references to previously defined items in [GAP-25](https://github.com/golemfactory/golem-architecture/blob/master/gaps/gap-25_golem_certificates/gap-25_golem_certificates.md). When a node descriptor is signed, all data in the `nodeDescriptor` property are digitally signed to add cryptographic integrity protection. Similarly to the previously defined certificate schema, the node descriptor schema does not restrict additional properties allowing adding contextual information and allowing compatibility with future additions.

The schema file can be found in the `schemas/v1` folder and accessible at `https://golem.network/schemas/v1/node-descriptor.schema.json`.

#### Node Id

The `nodeId` property contains the node's identifier on the network in hexadecimal format.

#### Permissions

Details of how the node is permitted to use the Golem network. The content of this property is governed by the [permissions schema](https://golem.network/schemas/v1/permissions.schema.json).

#### Validity Period

The time period for which this node descriptor is valid. Semantics defined in the [certificate schema](https://github.com/golemfactory/golem-architecture/blob/master/gaps/gap-25_golem_certificates/gap-25_golem_certificates.md#validity-period).

#### Signature

Semantics of the signature are defined in the [certificate schema](https://github.com/golemfactory/golem-architecture/blob/master/gaps/gap-25_golem_certificates/gap-25_golem_certificates.md#signature) with one difference. A node descriptor cannot be used to create signatures so it cannot be self signed.

### Signature creation and verification

The process is quite similar to what is described for [certificates](https://github.com/golemfactory/golem-architecture/blob/master/gaps/gap-25_golem_certificates/gap-25_golem_certificates.md#signature-creation-and-verification), but the signature is created for data in the `nodeDescriptor` property instead of the `certificate` one.

#### Creating a signature

In order to cryptographically sign a node descriptor the following steps need to be taken:

1. Create a binary representation of the `nodeDescriptor` property by serializing the content via the `JSON Canonicalization Scheme`
2. Sign the binary data with the chosen signature algorithm using the private key of the signer. The private key must be the one connected to the public key specified in the signer's certificate.
3. Create the `signature` property in the JSON certificate and add the following details
    - name of the hash and encryption algorithm (details of the signature algorithm) used to create the signature into the `algorithm` property
    - binary data of the signature into `value` property
    - the `signer` property of the `signature` must contain the signing certificate

#### Verifying a signature

1. Verify that the signing certificate contained in `signature.signer` is allowed to sign node descriptors (it's `keyUsage` property is either set to the string `all` or the list contains `signNode`).
2. Create a binary representation of the `nodeDescriptor` property by serializing the content via the `JSON Canonicalization Scheme`.
3. Using the signature algorithm matching the one used for creating the signature (`signature.algorithm.hash` and `signature.algorithm.encryption`) and the signer's public key (the `certificate.publicKey` property of the signing certificate), verify that the signature is valid for the binary data obtained in step 2.

### Node descriptor in the Demand

The node descriptor is added as a json object in the `golem.node.descriptor` property of the demand and becomes integral part of the demand during negotiation.

### Verifying during negotiation

This part explains the steps to be taken when verifying a node descriptor during negotiation to validate if it can be used for the demand or not.

1. Verify that the signature on the node descriptor is valid and that the signing [certificate chain is valid](https://github.com/golemfactory/golem-architecture/blob/master/gaps/gap-25_golem_certificates/gap-25_golem_certificates.md#verifying-a-certificate-chain).
2. Verify that the moment the negotiation is happening is within the node descriptor's validity period (it is not before the `notBefore` property and not after the `notAfter` property).
3. Verify that the node descriptor's permission property does not include any permissions that is not granted to the signing certificate. (Details about permission verification can be found in the [Golem certificate structure document](https://github.com/golemfactory/golem-architecture/blob/master/gaps/gap-25_golem_certificates/gap-25_golem_certificates.md#verifying-a-certificate-chain))
4. Verify that the Computation manifest does not request any features or access that the node is not granted via the node descriptor.

## Rationale

### Why do we need node descriptors?

Node descriptors are one way for providers to trust requestors that their requests are valid and does not intent to cause harm. This is the first KYC solution to allow finer control over outbound and similar sensitive upcoming features.

## Backwards Compatibility

When a requestor does not know about node descriptors or the node does not have one, the property in the demand will not be set. It is up to the provider if it will serve such requests or not.
An older provider will not recognize the data in this property and will simply ignore it, reducing its capability to verify the requestor.

## Test Cases

The reference implementation contains appropriate tests.

## Reference Implementation

[Golem certificate](https://github.com/golemfactory/golem-certificate) library is available to create, sign, verify node descriptors. The reference implementation of Golem network node [yagna](https://github.com/golemfactory/yagna) will also use node descriptors for the 'partner rule' feature released in a future version.

## Security Considerations

[Golem certificate](https://github.com/golemfactory/golem-certificate) library by default is using EdDSA signatures with Ed25519 scheme. This provides security equivalent of 128 bit symmetric keys.

## Copyright
Copyright and related rights waived via [CC0](https://creativecommons.org/publicdomain/zero/1.0/).