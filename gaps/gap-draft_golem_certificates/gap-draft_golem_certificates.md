---
gap: <to be assigned>
title: Golem Certificates structure
description: Describes certificates usage to enable more sensitive golem features.
author: <Witold Dzięcioł (@nieznanysprawiciel)>
status: Draft
type: Feature
requires: GAP-5
---
## Abstract

Some Golem features are more sensitive, because of potential malicious usage. A good example is outbound network traffic, where malicious user could cause harm, if he was able to access any internet resource. In GAP-5 and GAP-4 we decided to limit Requestor abilities by introducing Computational Manifest, which defines allowed commands and domains access. Payload Manifest is signed to confirm, that image is safe to use.

Outbound network is not the only use case, which requires certificates. Soon we will have other cases and we need unify certificates usage to allow different sets of permissions. 

## Motivation

Golem use cases allowing for interaction with external world (like outbound, inbound network traffic), put `Providers` in danger of malicious behavior from Requestor side. To alleviate this danger, in GAP-5 and GAP-4 we decided to limit Requestor abilities by introducing Computational Manifest, which should be signed by trusted party.

Currently `Providers` are able to choose certificates, they trust, by adding them to Provider configuration. This means that any party is able to gain `Providers'` trust, distribute their own certificates and validate safety and security of different Payloads. Despite this, we are aware that gaining trust can be difficult, so Golem Factory has to take initiative to be at least initial source of trust in the Network.

Ensuring safety and security of Payloads ran on `Providers'` machines can be ensured in few ways:
- Making audit of Payload Manifest and VM image, to validate if permissions are low enough for image to be safe 
- Signing legal Agreements with partner companies developing software on Golem, in which they commit not to do anything malicious
- Distribute permissions for signing Payloads to external companies taking responsibility for Payloads safety

When designing certificates structure, that will allow to meet requirements mentioned above, we must keep in mind future use cases, that might require signing with certificates:
- KYC
- Inbound network traffic

That means, that certificates will be used to sign different kinds of permissions. In case of transferring certification to other companies or signing legal Agreements, we need to ensure, that we keep control, which permissions they will be able to use.

In the future of Golem Network we expect, that new independent features will be implemented by community and they may need to use certification. This document aims to standardize this approach.

## Specification

The technical specification should describe the syntax and semantics of any new feature.

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
