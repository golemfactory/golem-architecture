---
gap: 20
title: Golem dApp Store
description: Concept of a dApp Store - a marketplace for Golem dApps where app designers can offer their applications to requestors.
author: stranger80 (@stranger80)
status: Draft
type: Feature
requires: GAP-16
---

## Abstract
This GAP introduces a concept of a Golem dApp Store which is a marketplace allowing for application designer to publish and advertise their applications for the requestors to discover and use. The proposal refers to [GAP-16] Golem Compose and the idea of application descriptor - and suggests publishing the applciation descriptor packages in a dApp Store. Basic functionality of a Golem dApp Store is described, but no strict standard is imposed - giving room for potential dApp Store owners for creative implementations.  

## Motivation
A dApp Store proposes to meet following objectives:
- enable Golem application designers/developers to publish their applications in a repository where they can be discovered by requestors
- provide requestors with ability to search and discover Golem applications
- create opportunity for parties willing to host Golem dApp Stores, as a dApp Store may open potential business areas:
  - advertising applications (which potentially may bring income from application designers)
  - apprication assessment & ratings - a dApp Store owner may put the published applications through a certification process, thus increasing the trustworthiness of apps available through the Store.

## Specification
A Golem dApp Store is a repository of application descriptors, published by application designers and available to Requestors. Dapp-stores may be public or restricted. Dapp-stores shall provide following capabilities:

### Upload application descriptor package
Application designers shall be able to publish application descriptor packages via an API. New application descriptors and updated versions of pre-existing application descriptors can be uploaded. A Dapp-store shall maintain the version history of published application descriptors.

### Application descriptor package indexing
A Dapp-store shall maintain an index of application descriptors where application descriptor `meta` attributes shall be used for indexing.

### Search application descriptors
Golem Requestors shall have ability to search application descriptors published in a Dapp-store, using an API. Application descriptor index attributes can be referenced to search/filter the application descriptors.

### Download application descriptor package
Golem Requestors shall have ability to download the content of application descriptor package using an API.

#### Single-YAML descriptor packages
A package may consist of a single decriptor file in YAML format. This package type is called a **single-YAML** descriptor package.

#### Multi-YAML descriptor package
Complex application descriptors may benefit from splitting the YAML content into multiple files, groupped by eg. areas of concern, packaged in a ZIP archive. This package type is called a **multi-YAML** descriptor package. 

## Rationale
The Golem dApp Store concept is based directly on similar "stores" related to eg. mobile applications available for various mobile device platforms. 

## Backwards Compatibility
N/A

## Test Cases
Test cases are implied by the dApp Store features, as listed in the Specification section.

## Security Considerations
An implementation of a Golem dApp Store is independent from fundamental Golem APIs - no dedicated enhancements to `yagna` security features are required.

The Golem dApp Store must include authentication & authorization mechanisms to enable secure access for application designers/owners who manage their aplication descripts & profiles as advertised in the Store. Some dApp Store implementations may choose to authenticate client Requestors as well. In all cases, the implementation must follow adequate, industry-standard identity and credentials management patterns. 

## Copyright
Copyright and related rights waived via [CC0](https://creativecommons.org/publicdomain/zero/1.0/).