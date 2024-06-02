---
gap: GAP-34
title: Demand/Offer Language Grammar Specifications
description: Grammar Specifications for elements of Demand&Offer language
author: stranger80
status: Draft
type: Standards
---

## Abstract
The Demand&Offer Specification Language [article](https://golem-network.gitbook.io/golem-infrastructure-documentation-develop/architecture/golem-demand-and-offer-specification-language) created as a description for a Golem building block, does only include an informal specification of the language (eg. constraints filter expressions) grammar. An implementation of expression resolver has been created as part of `yagna` reference implementation, however this implementation is not fully consistent, and fails on processing of a number of edge cases. Therefore a formal grammar specification is proposed, with clarifying enhancements, so that a non-ambiguous point of reference exists.

## Motivation
The proposed grammar fixes a number of errors in the `yagna` reference implementation of the Golem constraint expression resolver, and removes a number of known ambiguities of the original Demand&Offer Specification Language article.

## Specification
The Demand&Offer constraints expression grammar is specified [here](../../standards/constraints_grammar.md). 

## Rationale
The proposed grammar has been largely based on LDAP Filters [RFC 4515](https://www.rfc-editor.org/rfc/rfc4515). This reference has been consciously selected to avoid "reinventing the wheel" and only modify the existing specification to suit the intended purpose.

## Backwards Compatibility
The proposed enhancements to the constraint expression language are not 100% backwards compatible, however it is assumed that currently the only working Demand/Offer implementations are the ones maintained by Golem Factory. None of the "breaking" edge cases are used by current Golem Factory-maintained software.

## Security Considerations
N/A

## Copyright
Copyright and related rights waived via [CC0](https://creativecommons.org/publicdomain/zero/1.0/).