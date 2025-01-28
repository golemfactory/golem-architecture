---
gap: GAP-30
title: Node presence notification
description: Specification of network broadcast fallged when a node appears on the Golem Network. 
author: nieznanysprawiciel
status: Draft
type: Feature
---

## Abstract
This document describes proposal of new event broadcast through network, which will notify other yagna daemons, that a new Node has joined. Each module in yagna will be able to listen for this event and react to it. 

## Motivation
The node presence notification mechanism is generic and has a number of uses:
- Propagation of Agreement permissions (GAP-24).
- Re-synchronization of information after an offline period, eg. in node detach/attach scenario (GAP-17)

## Specification

### Currently state

#### Offers propagation

To propagate Offers in p2p manner, Yagna Nodes are using concept of local neighborhood. Each Node maintains a set of neighbors and relay Server is responsible for managing and returning information about neighborhoods. Neighborhood is only virtual, it has nothing to do with network topology and is computed based on Hamming distance between NodeIds.

Each Node sends a subset of Offer ids known to it, in random intervals. This subset always includes all its Offers and some part of Offers collected from other Nodes. A Node receiving the broadcast checks if it has seen a received Offer id before and if it didn't then:
- It asks other Nodes for Offer details
- Rebroadcasts Offer id to its neighborhood

Offers are propagated on other occasions as well, like when Offer is first time subscribed on the market.

As you can see, a new Offer will be propagated to entire network, but old Offers will stop being propagated in the local neighborhood.

#### Drawbacks of propagation

In this mechanism there is a tradeoff between amount of unnecessary data sent through network and a mean time after which Requestor will receive any Offer. We can regulate this by changing mean random broadcast time.
But there is another problem related to neighborhood. To avoid making too many requests on a relay, neighborhood is cached on yagna Node and cache is invalidated every few minutes. New Node joining the network is invisible to other neighbors until cache timeout elapses.

So the mean time after which a Requestor gets Offers depends on configuration of these to value on other Nodes in the network and the size of the network.

#### `NewNeighbor` notification

To improve the mean Offer reception time on Requestor side, notification mechanism was introduced. After Node joins the network it immediately sends `NewNeighbor` message to all neighbor Nodes. Market and net module of yagna daemon is listening for these broadcasts and does 2 things on receiving:
- Invalidates neighborhood cache, so next broadcast will query relay for neighbors
- Sends random subset of Offers known to this Node (in the same way as in cyclic broadcast case)

This way Requestor receives Offers very fast and cyclic broadcast interval and cache invalidation could be made bigger, making network traffic smaller.

### Mechanism proposed

We could extend the same mechanism to make generic functionality for notifying that Node appeared in the network.
It would be enough to implement propagation mechanism like Offers broadcasting described.

#### Problems and challenges

- This mechanism is rather an optimization, because of its unreliable nature. Broadcasts can be lost and yagna Node should still function properly, no code should depend on it.
- We need to keep track of Nodes that appeared lately in the network to have stop condition for propagation, otherwise we will spam the network. There is a question how long we should keep Nodes registry (What in case when Node is shutdown few times in row?)
- How should we handle lost network connection? This mechanism assumes, that we send message only on start, but maybe we should do this after we lost connection.
- We will have scaling problems when network will grow. Offers broadcasting will for sure be replaced somehow, when sharding will be implemented. I don't have idea how to replace this mechanism.

#### Additional network traffic

We need to evaluate if it is worth to add additional network traffic, that most of the Nodes won't use anyway.
My estimation is that we shouldn't add much less than Offers broadcasting causes, because Provider joining network is always subscribing Offers on the beginning.
This event will be rather more lightweight, than broadcasts. It could contain our default NodeId and all secondary ids.

### Agreement Permissions case

(See GAP-24 for details of Agreement Permissions Management)

There are 2 problems to solve:
- Sharing ACL list between Nodes for Agreement
- Synchronizing all the data for Agreement (Activities, Payments etc)

Each Node having ACL list for any Agreement should track `NodeOnline` (name??) notifications.
When receiving notification for Node on one of his list, he should send `AgreementSync` (name??) message to this Node.
It is up to receiver to decide, if he wants to synchronize his state (or part of it??).

#### Problems and challenges

- If we want to propagate anything, we need to know NodeIds. That means that ACL list should include NodeIds. This may limit access methods, which we can implement.
- This can be only optimization mechanism. Nodes having ACL list should repeatedly try to notify all other `grantees`. If `NodeOnline` is missed, Node will have long time, before it gets synchronization.
    - After we notified a Node, we don't have to spam it anymore, because it will always keep ACL information
    - If ACL changes we need a notification
    - Node having Agreement Permissions assigned should be later responsible for synchronizing state (after it was notified for the first time)


## Rationale
TODO The rationale fleshes out the specification by describing what motivated the design and why particular design decisions were made. It should describe alternate designs that were considered and related work.

## Backwards Compatibility
TODO All GAPs that introduce backwards incompatibilities must include a section describing these incompatibilities and their severity. The GAP **must** explain how the author proposes to deal with these incompatibilities.

## Test Cases
TODO Test cases are very useful in summarizing the scope and specifics of a GAP.  If the test suite is too large to reasonably be included inline, then consider adding it as one or more files in `./gaps/gap-draft_title/` directory.

## Security Considerations
TODO All GAPs must contain a section that discusses the security implications/considerations relevant to the proposed change. Include information that might be important for security discussions, surfaces risks and can be used throughout the life cycle of the proposal. E.g. include security-relevant design decisions, concerns, important discussions, implementation-specific guidance and pitfalls, an outline of threats and risks and how they are being addressed. 

## Copyright
Copyright and related rights waived via [CC0](https://creativecommons.org/publicdomain/zero/1.0/).