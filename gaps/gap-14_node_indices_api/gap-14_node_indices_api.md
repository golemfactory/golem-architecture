---
gap: 14
title: Node indices APIs
description: Discuss the place in the Golem ecosystem of the future APIs providing per-node indices.
author: Jan Betley (@johny-b)
status: Draft
type: Meta
---

## Abstract
External information about market participants is an important factor in the decision making process on every market.
First attempts to provide & use this sort of data on the Golem market were already made, other are being actively developed (GAP-10).
We - the Golem Factory - should decide about our role in shaping the ecosystem of such data providers.

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

### Examples

`Node indices API` might be providing just any information about nodes on the Golem market, but we can safely restrict our attention only to
types of information that can really be utilized in a market strategy. Few examples of possible node indice APIs:

*   Average duration of the agreement for a given `node_id` - we might be looking for a long agreement and prefer nodes that take have a history of long agreements
*   Total amount sent to a given `provider_id` - requestor might want to work with "experienced" providers
*   Current/past average prices - provider might want to adjust their offer to the current market situation
*   Node rating provided by the Golem Factory - we might want to work with "good" nodes and trust the Golem Factory scoring methods
*   All scores of the benchmark performed by the Golem Factory for a given node, with timestamps - ditto, but we only trust Golem Factory benchmarking method, not aggregation
*   Average traffic on the Golem Market in a given hour of a day - because we expect to find more cheap providers when there's not much going on
*   How many times given `node_id` shows up in posts on www.reddit.com/r/CheatedOnGolem/ - that's quite obscure, but still - why not?

### Current and future node indices APIs

There are already few examples of node indices APIs that are in progress or planned:

* [Benchmark-based provider reputation (GAP-10)](https://github.com/golemfactory/golem-architecture/pull/33)
* [Golem Stats](https://stats.golem.network). This "API" is currently used e.g. by the [community-created provider agent](https://gist.github.com/sv3t0sl4v/28f896752edc9e20347ffc6d8cefe74c)
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
  # (or maybe this line is redundant for APIs recommended by the Golem Factory?)

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
    If we go in the "here is some poorly documented API, have fun" direction, we won't have many adopters. If we provide a nice, convenient
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

Decisions:

1. There is value in treating "node indices API" as a single general concept.
2. Golem Factory intends to manage the ecosystem of the node indices API by providing and maintaining:
    
    * A list of recommended APIs
    * Sample implementations of client libraries
    * Example usages of client libraries together with particular APIs
    * Specifications and/or guides on writing such APIs

3. In the near future Golem Factory will **not** define a general node indices API interface and will **not** make any attempt to implement a complex client library.
   All efforts will be directly connected to specific needs, expressed in other GAPs.
   On the other hand, every GAP describing changes to the node indices API ecosystem should reference this GAP, with a particular emphasis on compliance with p. 1 and 2 above.

4. First efforts towards implemeting such APIs will be specified in:
    
    * GAP-10 - reputation based on provider benchmarks
    * GAP-XX - data from the Golem Stats

5. First client library will be implemented together with GAP-10. Details:
    
    * An async python library
    * A standalone part of the `yapapi`
    * A "general" client library, i.e.
      
      * Should implement no logic directly referencing "provider benchmarks"
      * Sufficiently similiar APIs should work out of the box
      * Easy to extend (e.g. it should be obvious what changes are necessary for GAP-XX (Golem Stats))

## Rationale

Basic rationale is included in the `Motivation` section.
It might be extended here after the initial discussions & acceptance.

## Backwards Compatibility

N/A

## Test Cases

N/A

## [Optional] Reference Implementation

N/A

## Security Considerations

There are few different topics to consider:

1.  Is the code that collects the data open or closed?
   
    Open source has obvious advantages, but in this case closing it might often be a better decision. The reasoning:
   
    * Data provided by node indices APIs will be used in market strategies, so there will be a direct relationship 
      between "value of index X for node Y" and "market gains of node Y".
    * Because of this we should expect a strong incentive for node operators to have nodes with as good scores as possible
    * This is what we want, but only with an additional assumption: that the score can't be cheated
    * Creating a data gathering tool that can't be cheated in any way might be impossible, but there are many ways to make it harder
    * Closing the source seems to be a good first step

    As a crude example: let's say we're benchmarking providers (as a generalization of GAP-10). If the VM image hash used for benchmarking is always the same and known to the public,
    a dishonest provider might be allocating high resources to runtimes using this image and low resources to other.

2.  Bugs in APIs hosted and/or recommended by the Golem Factory

    *   From the API consumer POV.
    
        Making wrong market decisions equals wasting money. If it's caused by incorrect indices recommended by the Golem Factory, the money-wasting market participant might expect some
        compensation from the Golem Factory. One day we'll almost surely have to face that claims (because we decided to recommend APIs and some APIs will surely have bugs).
        That's why it's important to have an unequivocal terms & conditions.
    
    *   From the node operator POV

        What should a node operator do if they think some node indices API is reporting incorrect values for their node?
        For sure we don't want them to fill a defamation lawsuit. 
        Again, we should ensure we have a correct terms & conditions, but maybe one day we'll also have to consider some "appeal" institution.
        Also, this is closely related to the open/closed problem from p.1 - indices calculated with an open code are easier to explain/defend.

3.  Malicious APIs

    We might expect one day to have to deal with malicious APIs that pretend to provide some useful information (e.g. reputation), but their hidden de facto purpose is to
    improve market gains of some nodes (at the expense of the API consumers and other nodes).
    We should never recommend APIs using any code we can't access. Also: we might want to touch this in the T&C.

## Copyright
Copyright and related rights waived via [CC0](https://creativecommons.org/publicdomain/zero/1.0/).
