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

We decided to use [x509 arbitrary extensions](https://www.openssl.org/docs/man1.1.1/man5/x509v3_config.html) to enrich certificates with custom fields describing permissions.

### Custom OIDs

To use arbitrary extensions we need to apply to https://pen.iana.org/pen/PenApplication.page to assign OID (Private Enterprise Number) for GolemFactory.
In further documentation I assume we got number `1.3.6.1.4.1.60000`, until we will acquire real OID.

Having our OID, we can define arbitrary semantic to numbers in our namespace:


| Name                       | OID                      | Semantic                                                             |
| -------------------------- | ------------------------ | -------------------------------------------------------------------- |
| GolemFactory               | 1.3.6.1.4.1.60000 (TODO) | Root OID for Golem Factory                                           |
| GolemCertificates          | ${GolemFactory}.1        | Root for Golem certificates parameters                               |
| GolemCertificatesAllowance | ${GolemCertificates}.1   | List of permissions to sign certain Golem features using certificate |

Example values of `GolemCertificatesAllowance` field (this list will be used in further examples, to illustrate how certification works):


| Field             | Semantic                                                                        |
| ----------------- | ------------------------------------------------------------------------------- |
| all               | Certificate is allowed to sign any feature (even not existing future use cases) |
| manifest-outbound | Allowed to sign manifests with outbound network enabled                         |
| inbound           | Allowed to sign inbound allowance for Requestors.                               |
| ...               | {List is non-exhaustive - new items will be created in future}                  |

### Permissions chains

To transfer some set of permissions to external entity, Golem Factory has to sign it's certificate. This will create certificates chain, that will be later validated by Agent application. For signature to be valid, both certificate and it's parent has to contain specific allowance (or `all`) included.

For example, if we have hierarchy:


| Certificate           | allowance                  |
| --------------------- |----------------------------|
| Golem Root            | all                        |
| Golem Intermediate    | manifest-outbound,inbound  |
| Partner's certificate | manifest-outbound          |

than Payload Manifest signature is valid and `Parter's certificate` is able to sign manifests, but has no rights to sign inbound related features.

But if structure looks like this:


| Certificate           | allowance         |
| --------------------- | ----------------- |
| Golem Root            | all               |
| Golem Intermediate    | inbound           |
| Partner's certificate | manifest-outbound |

Than `Parter's certificate` is not allowed to sign manifests and Agent application should reject such Proposals.
Note that certificates chain is incorrect, because `Partner's certificate` shouldn't be signed by `Golem Intermediate` in the first place.

### 

Certificate Allowance list

List of all available values for `GolemCertificatesAllowance` field. This list should be updated when new options appear:


| Field             | Semantic                                                                        |
| ----------------- | ------------------------------------------------------------------------------- |
| all               | Certificate is allowed to sign any feature (even not existing future use cases) |
| manifest-outbound | Allowed to sign manifests with outbound network enabled                         |

## Rationale

The rationale fleshes out the specification by describing what motivated the design and why particular design decisions were made. It should describe alternate designs that were considered and related work.

## Backwards Compatibility

All GAPs that introduce backwards incompatibilities must include a section describing these incompatibilities and their severity. The GAP **must** explain how the author proposes to deal with these incompatibilities.

## Test Cases

Test cases are very useful in summarizing the scope and specifics of a GAP.  If the test suite is too large to reasonably be included inline, then consider adding it as one or more files in `./gaps/gap-draft_title/` directory.

## [Optional] Reference Implementation

### Generating openssl certificate

You can check [here](file://cert.confi) example config, which can be passed to openssl to generate certificate containing expected fields. To generate certificate run:

`openssl req -x509 -sha256 -days 1825 -newkey rsa:4096 -keyout rootCA.key -out rootCA.crt -config cert.config  --extensions golem_network_certificates`

than check if certificate contains expected fields with:

`openssl x509 -text -noout -in rootCA.crt`

Output looks like this:

```
...
        X509v3 extensions:
            1.3.6.1.4.1.60000.1.1: 
                0...all..manifest-outbound
...
```

## Security Considerations

All GAPs must contain a section that discusses the security implications/considerations relevant to the proposed change. Include information that might be important for security discussions, surfaces risks and can be used throughout the life cycle of the proposal. E.g. include security-relevant design decisions, concerns, important discussions, implementation-specific guidance and pitfalls, an outline of threats and risks and how they are being addressed.

## Copyright

Copyright and related rights waived via [CC0](https://creativecommons.org/publicdomain/zero/1.0/).
