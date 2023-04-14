---
gap: <to be assigned>
title: Experimental features
description: GAP describes process of introducing new experimental features that would speed up development.
author: <Witold Dzięcioł (@nieznanysprawiciel)>
status: Draft
type: Standards
---

## Abstract
Current process of introducing new features in Golem Factory is not suitable for fast
development and releasing of experimental features, which can be later improved in
iterative manner, after collecting feedback from community.

This GAP will explain problems that block us from fast development and will attempt
to propose solution, that can be used on protocol level, to make development easier,
without sacrificing backward (and sometimes forward) compatibility.

## Motivation

Most of the problems stem from the fact that we control only small part of the Golem
Network ecosystem. Nodes in the network can operate on different versions
of the software, inclusive of experimental changes implemented by developers or earlier
releases from the past.

That's why we need to be very careful, when altering the Golem protocol, to avoid
multiplying the number of bugs that may emerge from the interaction of different versions.
This may result in additional cost of developing features, because they require more
considerations and must be fully thought-through, before they can be released.

Let's see potential problems, that could be consequence of specific protocol changes,
using negotiations as an example.

### Negotiation protocol problems examples

Let's assume, we have the following property on Requestor side, which indicates
payment platform, that Requestor wants to use:

`"golem.com.payment.platform" : "erc20-rinkeby-tglm"`

In next version of software we realize, that platform string is not enough, and we need
additional properties describing platform. We change the property to:

```
"golem.com.payment.platform.erc20-rinkeby-tglm": {
    "address": "0xa5ad3f81e283983b8e9705b2e31d0c138bb2b1b7"
}
```

What will happen if new version of software will interact with old version?
Old version will return an error, because it expected `string` and got a `dictionary`.

But this is optimistic scenario, because we get an error (assuming correct Provider
implementation of course). We can imagine scenarios, where Requestor is happy with
properties format, but their semantic changed, so the resulting bug is much harder
to find.

For example let's imagine we want to implement payments using allowance. In this case
we have 2 accounts that are engaged in the process: one is signer of transaction,
the second is address that gave allowance.

If a provider is operating on an older version of the software that is unaware of
allowance payments, they may face issues while verifying transactions. The most
problematic aspect is that the problem may arise long after the agreement has been
negotiated.

#### Conclusions

From the provided example we can see how fast, iterative development process can be 
affected. Each iteration introducing changes to protocol, multiplies number of versions
existing in the network and produces new potential bugs. Ensuring backward compatibility
is an essential necessity, not just a mere concern for our users.

### Goals

Goal of this GAP is to provide methods, that will make development process as lightweight
as possible, but at the same time they will help us avoid compatibility issues and 
protocol collisions.

At the same time presented methods will improve stability and quality of non-experimental
features and parts of the protocol.
From the perspective of Golem Network users, frequently changing APIs may discourage
adoption in a production environment. Hence, the second goal is to make clear distinction
between Golem features that may undergo modifications and those that are already stable.

## Specification

### Categories of features
- Stable features
- Experimental features

### Experimental features separation

#### Negotiation protocols

#### GSB protocols

#### REST APIs

#### CLIs and other user facing interfaces

#### SDKs

SDK features are outside the scope of this GAP.

### Versioning scheme

### Phases of feature development

- PoC version
- Release and iterating over implementation
- Upgrade to stable version

### Problems that won't be solved

## Rationale

## Backwards Compatibility

Solutions proposed in this GAP are created specifically to address compatibility issues,
so aren't introducing any problems, but rather are solving them.  

## Test Cases
N/A

## Security Considerations

Experimental features should be considered potentially insecure since they are development
in progress. Although experimental features shouldn't be released without security considerations,
because they are public.

Upgrading feature from experimental to stable should always include security audit. 

## Copyright
Copyright and related rights waived via [CC0](https://creativecommons.org/publicdomain/zero/1.0/).