---
gap: <to be assigned>
title: Daemon Capabilities API
description: A proposal for adding capability tracking to the yagna daemon along with an accompanying REST API endpoint.
author:  blueshade (@shadeofblue)
status: Draft
type: <Feature>
---


## Abstract
To make different versions of the agents more interoperative with different versions of the daemon and thus depend less on mutual version-wise synchronization, we acknowledge the need add a registry of daemon's capabilities that can be queried by the agents. This GAP aims to define such registry and a way to query it.


## Motivation
Currently, when we add functionality to the daemon and the APIs, we blindly assume that users will upgrade both their libraries and their yagna daemons at the same time. While this is somewhat acceptable at the beta stage of the software, looking forward, to allow the ecosystem to grow and to allow people to build upon it, it would be better to acknowledge that it will not always be feasible for users of Golem infrastructure to immediately upgrade their stacks.

We have already moved in this direction with regards to requestor-to-provider capability detection, the example being VPN capability of the VM runtime that can be filtered against in a Demand.

Now, we'd like to introduce a Daemon's capability API endpoint to allow the agent to query its own daemon to verify that it supports or does not support a certain capability.


### Example use case: Batched payments

An example use case for this functionality is the support for batch payments.

The requestor would like to accept all debit notes with a certain "payment due" date. At the same time though, they would prefer not to have to issue a payment for each debit note separately, because then a lot of what they pay (possibly way more than the payments themselves) would have to be spent on the transaction fees. 

That's why they'd like to delay the payments until the last possible moment and pay for _all_ the debit notes issued in the meantime for a particular address.

They cannot do it now because yagna does not support batched payments. But even if it did, how could the requestor agent's code be sure that the daemon the user is using does indeed support it?

That's where the capability list comes in. The requestor agent may agree to an agreement with another provider that involves accepting each debit note as payable if they know they can rely on their own daemon's ability not to overpay for the transaction fees.

Other use cases may follow in the future but this is one that we have already identified as desirable.


## Specification

### Capabilities map

Yagna will keep an internal, hierarchical map of capabilities. Modules may choose to add keys anywhere to the map but it would be desirable for the map hierarchy to be consistent with the hierarchy of the modules themselves. 

The support of a certain capability will be determined based on the existence of a certain key. The value of a capability key map may hold additional dictionary of meta parameters.

This specification is agnostic to the semantics of each capability and its possible parameters. It is up to the authors of the modules to specify and document the specific capabilities of each module and its possible meta parameters (if any).

#### Example

An example (JSON-serialized) map, depicting a possible definition of the batching payments capability in the `erc20` payment driver. Shown here is also an example additional property - `multicast` - here describing an ability to issue payments to multiple addresses in a single transaction.

```
{
    "core": {
        "payment": {
            "erc20": {
                "batching": {
                    "multicast": {}
                }
            }
        }
    }
}
```

### Capabilities REST API endpoint

Proposed new API for yagna:

https://github.com/golemfactory/ya-client/tree/blue/caps-api


## Rationale

### No enforced structure
The decision not to enforce any strict structure into the Caps API serves to simplifly the implementation and usage of the API, leaving the structuring decisions to the authors of particular yagna modules.

### All capabilities are dictionary key-based
The decision to inform about the existence or lack of a particular capability on a dictionary key level will benefit future extensibility.

e.g. we could say that yagna supports, let's say batching payments by adding a key with the name `batching` within the appropriate driver's dictionary and later add keys below that level to signify additional capabilities of the _batching_ functionality.

## Backwards Compatibility

As this is a new functionality, there are no known backwards-compatibility issues.


## Test Cases

N/A


## [Optional] Reference Implementation

N/A


## Security Considerations

This feature does not seem to have an impact on security of either the daemon or the agents.


## Copyright
Copyright and related rights waived via [CC0](https://creativecommons.org/publicdomain/zero/1.0/).
