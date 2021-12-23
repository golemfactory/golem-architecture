---
gap: 14
title: Node indices APIs
description: Discuss place of the future APIs providing per-node indices in the Golem ecosystem.
author: Jan Betley (@johny-b)
status: Draft
type: Meta
---

## Abstract
External information about market participants is an important factor in the decision making process on every market.
We - the Golem Factory - should decide about our role in shaping the ecosystem of [TODO]

## Definitions

* `node_id` - either `provider_id` or `requestor_id`
* `node` - participant of the Golem marketplace identified by `node_id`
* `indice` - any scalar information about a particular `node` or a group of `nodes`
* `node indices API` - API that could be queried for `indices`. Examples:
    *   an API that could be queried for "last time online" indice for a given `provider_id`
    *   an API that could be queried for "total amount of all debit notes issued in the last hour"

## Node indices API vs the "reputation" 
There are three types of information about the Golem market that influence the decisions of the agents trading on the Golem marketplace:

1. Information available directly on the Golem market (e.g. offers and demands)
2. Information gathered locally by the agent
3. External information, gathered & provided by other agents

The topic of this GAP is in no way related to 1./2. and should fully cover the most general approach to 3.
While "local" reputation is part of 2, 3 fully includes everything related to the "non-local" reputation, extended with:

* the node-based indices that really can't be labelled as "reputation" (e.g. how often it is rented, average price etc)
* the aggregated indices (e.g. average prices for all nodes with a given set of parameters)

## Motivation
### Current and future node indices APIs

There are already few examples of node indices APIs that are in progress or planned:

* [Benchmark-based provider reputation (GAP-10)](https://github.com/golemfactory/golem-architecture/pull/33)
* [Golem stats](https://stats.golem.network)
* All the future efforts related to the reputation

And from the other side:

* Every known marketplace uses sort of similiar "API"s (be it stars on Google, reviews on Steam, or guild-issued titles on the medieval real world marketplace).
* This sort of information has a real value, so we should expect there to be an incentive to provide such APIs commercially (someday, hopefully).

All in all, we should expect such APIs to be a permanent part of the Golem marketplace forever.

### The role of the Golem Factory

Now we get (at last) to the main topic of this GAP: does Golem Factory want to actively shape the ecosystem of node indices APIs? And if yes, then how?
Consider few different approaches:

* Full indifference. We decide this is not a concern of Golem Factory, close this GAP & decide that e.g. GAP-10 should be implemented without any general reflection on other
  possible node indices APIs.

* Implement model client libraries in `ya*apis`, things like:
  
  ```
  >>> my_api = NodeIndicesApi("http://golem.network/api/provider_benchmark")
  >>> my_api.indices
  {"bogomips": "[description what we really measure]"}
  >>> my_api.bogompis(provider_id="123abcd")
  [("2021-12-23T15:32:24", 100),
   ("2021-12-24T15:33:25", 107)]
  >>> my_api.bogomips().median
  137
  >>> my_api.bogomips(start="2021-12-23").median
  140
  ```

  Such libraries should come with a specification of the expected API interface - with an important caveat
  that it should be as non-demanding as possible. E.g. if one has only a single interesting number to share,
  they should be able to do this pretty effortlessly.

* Implement similiar logic in `yagna`

  ```
  yagna ext-api add provider-benchmark "http://golem.network/api/provider\_benchmark"
  # (or maybe even without the above line for APIs recommended by the Golem Factory?)

  curl http://localhost:7777/provider-benchmark/bogomips/median  # 137
  ```

  (Note that "logic" here is understood not as a simple proxy, but as a maximum effort to unify the
  interface for different APIs. E.g. the external API might provide the "median" statistic, but it also
  might just share a full collection and the median would then be computed by `yagna`).


### Deciding on the best approach
There is no obvious solution here, we have many different tradeoffs to consider.
So, what do we really want to achieve?

1. We want to maximize the adoption of the few early node indices APIs.

    Every early adopter of e.g. the provider benchmarks [GAP-10](https://github.com/golemfactory/golem-architecture/pull/33) will be priceless.
    If we go in the "here is some poorly documented API, have fun" direction, we'll have only little adopters. If we provide a nice, convenient
    library, there will be more.

2. We want to encourage others to create & maintain their own APIs.

    There was already some effort done in similiar directions in the community (e.g. [gc_listoffers](https://github.com/krunch3r76/gc__listoffers)).
    The shorter is the path between "I have some data to share" and "This data is easily available in the requestor (or provider) agent", the better
    chance developers will take part in it.
    It's worth noting that there are possibly useful indices that can be computed costlessly (e.g. "how long this provider is on the market", "how many
    offers are there for a given payload at a given time") - we can implement them as a part of the Golem Stats, but this might as well end up as a nice small community project.

3. We want to avoid logic duplication.
    
    This is pretty straightforward. The more logic we put in `yagna`, the less logic we have to duplicate in all `ya*apis`, or expect to be implemented
    by developers using `yagna` directly. Also we should keep in mind possible provider agents, things like:

    ```
    if ($(yagna ext-api golem-stats avg-cpu-price-yesterday) > 0.01)
    then golemsp --foo
    else golemsp --bar
    ```

4. We want to allocate our resources well

  In short, `yagna`-side implementation is probably not worth it, at least for now.


## Specification

[TODO]

1. Decide we want to have a python library that:
    *   is enough for GAP-10
    *   is enough for some `golem stats` things
    *   is open for every other API that is sufficiently similiar to either of these
    *   can be extended for different apis
2. Decide that we'll consider moving parts of the logic to `yagna` only when the python prototype will be good enough.
3. Implement this library along works spawned by GAP-10
    *   in yapapi or not?
    *   good enough docs to implement the `golem stats` part
4. Implement a `golem stats` api (another GAP?)

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
* open/closed source
* quality of APIs hosted by the Golem Factory
* quality of all other APIs
* defamation? injustice? challenge?

All GAPs must contain a section that discusses the security implications/considerations relevant to the proposed change. Include information that might be important for security discussions, surfaces risks and can be used throughout the life cycle of the proposal. E.g. include security-relevant design decisions, concerns, important discussions, implementation-specific guidance and pitfalls, an outline of threats and risks and how they are being addressed. 

## Copyright
Copyright and related rights waived via [CC0](https://creativecommons.org/publicdomain/zero/1.0/).
