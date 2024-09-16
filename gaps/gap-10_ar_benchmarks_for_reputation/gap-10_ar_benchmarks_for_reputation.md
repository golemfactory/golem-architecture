---
gap: 10
title: AR benchmarks for reputation
description: Share benchmarks gathered by the Golem Factory as indicators of the provider quality, to be used in requestor agents.
author: Jan Betley (@johny-b)
status: Draft
type: Feature
requires: GAP-14
---


## Abstract

[TODO]

## Motivation
Golem Factory is currently maintainig Artificial Requestors on the mainnet and will continue doing so in the foreseeable future.
The computations commisioned by the ARs could be used for gathering performance data about the providers.

[TODO]
## Specification

General note: the purpose of this GAP is to design a product of a MVP quality that will be easy to improve/extend. 
That's why we don't put much effort into e.g. determining the best benchmarking commands, best measurements aggregates, or full security.

The information flow looks as follows:
1.  AR gathers some data about a provider
2.  AR saves the data somewhere where it can be accessed by a node indices API
3.  Node indices API can be queried for per-provider and population indices
4.  Responses from the node indices API are consumed in the requestor code

Details:

### Benchmarking

NOTE: Things related to AR operations are not part of this GAP. Only AR logic regarding the provider benchmarks is discussed here.

1.  AR benchmarking is done with a `yapapi`-based requestor script
2.  This script resides in a **private** repository, and no low-level details should be shared publicly.
    Reasoning behind this is stated in the *Security Considerations* in the GAP-14.
3.  Measurement details:
    *   The output of an AR task running on the provider is a single number - [the BogoMips index](https://en.wikipedia.org/wiki/BogoMips).
    *   Failed runs are ignored.
    *   AR should put maximum effort into benchmarking all providers with the same address at the same time - this way we'll somehow guard against overprovisioning.
4.  BogoMips score together with `provider_id` is saved in the database.

### Storage

A `(id, timestamp, provider_id, bogomips)` table in the database behind the GolemStats.

### API

* `/indices` - definition of all other endpoints
* `/bogomips_rating/[PROVIDER_ID]` - some aggregated scalar value. E.g. average of all measurements in the last 2 weeks standarized to (0, 1) range.
* `/bogomips/[PROVIDER_ID]` - a list of all pairs (bogomips, timestamp) for a given provider.
* `/bogomips/median` - a global median measurement

### API client

A `yapapi` library described in GAP-14.
    

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
