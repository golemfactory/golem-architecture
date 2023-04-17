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

We would like to split features into 2 categories:
- Experimental Features
- Stable Features

**Experimental Features** can be thought of as prototypes, which purpose is to evaluate ideas
and confront them with users feedback.
They will be developed in separation on protocol level, what will allow to break compatibility
often and make iterative changes. Each new version will cooperate only with other Nodes
implementing exactly the same version. **Experimental Features** can be dropped at any time, 
but it is not allowed to break any of existing **Stable Features**.

Every feature should begin its lifetime as experimental. After feature proved its business
and UX value, it can be upgraded to **Stable Feature**. **Stable Feature** should keep backward
compatibility according to [Golem Compatibility Policy](https://handbook.golem.network/see-also/compatibility-policy).

Before upgrading feature, following things should be considered or taken care of:
- Backward compatibility with previous versions of protocol (and potential interactions/collisions)
- Forward compatibility and reasonable level of extensibility
- Security
- Stability and quality
- Generality level: **Stable Features** shouldn't contain implementation solutions that close
  protocol usage for entities outside of Golem Factory

Notice that _upgrade process_ from **Experimental Feature** to **Stable Feature** is not backward
compatible itself, although developers can choose to keep compatibility if they see reasons for it.

### Experimental features separation

**Experimental features** should be separated on protocol level by adding specific prefix
and creating special _experimental_ namespace this way. Everything within this namespace
can be changed and any time and doesn't have stable semantic. Moreover, everyone is allowed
to use names from experimental namespace.

**Stable Features** protocols aren't allowed to use reserved prefixes, otherwise they will be
treated as experimental.

In following paragraphs experimental namespacing for different kind of APIs will be introduced.
Moreover, a few guidelines will be proposed to avoid naming collisions.

Keep in mind, that since everything within _experimental_ namespace is undefined, no one
is forced to keep the guidelines, if they would hinder ability to fast development.

#### Negotiation protocols

Assuming we have `golem.com.payment`, let's introduce experimental `payment-platform` namespace:

`"golem.com.payment.!exp.payment-platform" : "erc20-rinkeby-tglm"`

(TODO: Check if we can use `!` sign. May cause conflict in constraint language,
json or yaml Offer/Demand representation)

We have multiple options of correct experimental namespaces:
- `"golem.com.payment.!exp.payment-platform"`
- `"golem.com.!exp.payment.payment-platform"`
- `"golem.!exp.com.payment.payment-platform"`
- `"!exp.golem.com.payment.payment-platform"`

Example of incorrect namespacing:

`"golem.com.payment.payment-platform.!exp" : "erc20-rinkeby-tglm"`

`payment-platform` property didn't exist before, so we are not allowed to introduce it.

#### GSB protocols

GSB allows to make RPC calls to other yagna daemons. Messages are sent to specific address
like for example:
`/public/market/protocol/mk1/discovery/offers/GetOffers`

Examples of introducing new GSB message `ListOffers`:
- `/public/!exp/market/protocol/mk1/discovery/offers/ListOffers`
- `/public/market/!exp/protocol/mk1/discovery/offers/ListOffers`
- `/public/market/protocol/!exp/mk1/discovery/offers/ListOffers`
- `/public/market/protocol/mk1/!exp/discovery/offers/ListOffers`
- `/public/market/protocol/mk1/discovery/!exp/offers/ListOffers`
- `/public/market/protocol/mk1/discovery/offers/!exp/ListOffers`

Note that `/public` prefix has special meaning. Net module redirects all calls to `/public`
addresses. For this reason `/!exp/public` is not correct namespacing.

##### Backoff to stable version

In case of GSB messages it is hard to imagine, that changes will be completely independent
of previous versions of protocols. In this case it might be useful to check if other yagna
daemon supports experimental version and backoff to stable code if it doesn't.

We would recommend always binding `IsSupported` endpoint, when developing new **Experimental Feature**.
In combination with [versioning](#Versioning-scheme) it could allow us to send GSB message
to check if other yagna supports this specific experimental feature.
Sending message will result with `Endpoint not found` in case it is not supported.

#### REST APIs

Example address for responding to Proposal:
`POST /market-api/v1/demands/{demand_id}}/proposals/{proposal_id}`

Examples of experimental namespacing:
- `POST /market-api/v1/!exp/demands/{demand_id}}/proposals/{proposal_id}`
- `POST /market-api/v1/demands/!exp/{demand_id}}/proposals/{proposal_id}`
- `POST /market-api/v1/demands/{demand_id}}/!exp/proposals/{proposal_id}`
- `POST /market-api/v1/demands/{demand_id}}/proposals/!exp/{proposal_id}`

#### CLIs and other user facing interfaces

- Commands should be explicitly classified as **Stable** or **Experimental**
- We should introduce new commands instead of changing previous, when introducing
**Experimental Feature**

In general, I would treat all CLI commands that we have now, as experimental and slowly
move them to stable. I don't think CLI stability is important for us at this point.

#### SDKs

SDK features are outside the scope of this GAP.

### Versioning scheme

Since content of _experimental_ namespace is undefined, we can expect properties collisions,
when many independent entities will work on features at the same time. That's why additional
namespacing and versioning is recommended.
- Use feature identifier to separate properties from unrelated features
- Use version number to separate subsequent iterations of the feature 

Suggested template for namespacing:

`"golem.com.payment.!exp.{feature-identitfier}.{version}.payment-platform"`

Example of using identifier and version number.

`"golem.com.payment.!exp.gap-12345.v3.22.payment-platform" : "erc20-rinkeby-tglm"`

Version consists of 2 numbers:

| Number             | Description                                                            |
|--------------------|------------------------------------------------------------------------|
| Release number     | Incremented with every public release of the feature                   |
| Development number | Incremented during development to separate each new change in protocol |


Characteristic of versioning:
- Only exact versions are compatible
- Developer shouldn't consider compatibility with previous version at all
  - If the code related to protocol was changed than he should always increment `Development` number
  - When releasing, if the code has changed in relation to previous release than `Release`
    number should always be incremented
- Developer can prefix or postfix Development number with his nick/identifier/whatever to
  avoid version clashes with other team members

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