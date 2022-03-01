---
gap: GAP-16
title: Multi-service application deployment framework for Golem Network
description: A solution for defining, deploying and managing apps based on multiple services operating within one or more Golem VPNs
author: Jakub Mazurek (@zakaprov) <jakub.mazurek@golem.network>
status: Draft
type: Feature
requires (*optional): <GAP number(s)>
replaces (*optional): <GAP number(s)>
---

## Abstract
This GAP introduces a proposal for a language-agnostic layer of abstraction over the Golem VPN and its high-level APIs (`yapapi`, `yajsapi`).
The main element of this proposal is the introduction of a **network configuration descriptor** file which can be used to describe a deployment of a set of services to be run within the Golem Network.
This configuration descriptor is intended to be used together with high-level APIs to enable deploying and managing the nodes described within the descriptor.

## Motivation
### Why?
The main motivation for this proposal is providing support for development of distributed applications (dApps) deployed within the Golem Network.
### Goals
- enable reusable application deployments through a version-control-friendly configuration format (descriptor files)
- simplify creating complex network topologies (adding a layer of abstraction over the existing Golem VPN functionality)
- allow for easily starting and tearing down multiple different payloads within the Golem Network

## Specification
### Configuration descriptor file
Here's an example of such a descriptor file:
```
version: "0.0.1"

auth:
  address: "http://rest-api:6000"
  app_key: "$YAGNA_APP_KEY"
  gsb_url: "tcp://gsb-url:6666"

payment:
  budget: 10  # GLM
  driver: "polygon"
  network: "mainnet"

payloads:
  nginx:
    runtime: "vm" # runtimes should have their own capabilities assigned
    params:
      image: "image-hash"
      repo-url: "http://girepo.dev.golem.network:8000"
    constraints:
      "golem.inf.cpu.cores": 2
      "golem.inf.mem.gib": 4
      "golem.inf.storage.gib": 10
    capabilities:
      - "vpn"

networks:
  default:
    ip: "192.168.0.0/24"

nodes:
  reverse-proxy:
    payload: "nginx"
    args:
      deploy: # dict, optional
      start: # list, optional
    networks:
      - "name": "default"
        "address": "192.168.0.2"  # optional
    environment:
      - "SOME_VARIABLE=1234"
```

[TODO: detailed description of each field] 

#### Payloads
Depending on the chosen `runtime` certain values should be added to the `capabilities` list.

#### Merging descriptor files
Multiple descriptor files may be used within the scope of a single deployment. In such a case, the files are merged based on their ordering. The merging is performed using a deep merge strategy.
Here's an example of how this merging strategy is applied:

Base file:
```
version: "0.0.1"

payment:
  budget: 10
  driver: "polygon"

payloads:
  nginx:
    runtime: "vm"
    params:
      image: "image-hash"
    constraints:
      "golem.inf.cpu.cores": 2
    capabilities:
      - "vpn"
```

Override file:
```
version: "0.0.1"

payment:
  budget: 1

payloads:
  nginx:
    params:
      repo: "repo-url"
    capabilities:
      - "gpu"
```

Resulting file:
```
version: "0.0.1"

payment:
  budget: 1
  driver: "polygon"

payloads:
  nginx:
    runtime: "vm"
    params:
      image: "image-hash"
      repo: "repo-url"
    constraints:
      "golem.inf.cpu.cores": 2
    capabilities:
      - "vpn"
      - "gpu"
```

A depth-first approach is used to determine the values which need to be added or updated to existing collections (lists and key-value maps).
For maps, keys from overriding files have precedence over the base ones.
In the case of lists, when merging lists from two files, the override values are simply concatenated to the base list. If required, this behaviour can be made configurable (e.g. to enable overriding the entire list instead).

[TODO: describe how these files can be composed (CLI, file system hierarchy)]

#### File schema
Once the name and the structure of the descriptor file is finalized, its schema can be published to https://www.schemastore.org/json/.
This way, the YAML language server will provide support for schema validation and completion in IDEs and editors.

## Rationale
#### File format
The file format of choice is **YAML**.
YAML is used in both Docker Compose and Kubernetes, both of which are widely-adopted deployment solutions. As such, it's become a de-facto standard
1. It's an established standard
2. It's more flexible compared to alternatives (e.g. TOML)
3. Users of the solutions mentioned above should find it easy to start using Golem Compose

#### Market strategy
In its current form, the deployment descriptor **does not** include support for specifying the market strategy which should be used by the requestor. There are two primary reasons for this:
1. A feasible approach which would allow for specifying/defining market strategies in a language-agnostic matter could not be found in this iteration of the proposal.
2. Including some form of support for market strategies at an early stage of development would unnecessarily increase complexity.
Market strategies will be addressed in a future GAP.

#### Inspirations
- [Docker Compose](https://github.com/compose-spec/compose-spec/blob/master/spec.md)
- [Terraform](https://www.terraform.io/language)
- [Kubernetes](https://kubernetes.io/docs/concepts/overview/working-with-objects/kubernetes-objects)

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
