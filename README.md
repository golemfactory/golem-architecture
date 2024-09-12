# Golem Architecture

This repository contains documentation and artifacts defining the protocol and architecture for the Golem Project.


This repository contains specification on protocol level and should be baseline for
future implementation. That means that implementation details of [yagna](https://github.com/golemfactory/yagna)
or SDKs should be documented in corresponding repositories, not here.

## Content

### [Daemon REST API](https://golemfactory.github.io/ya-client/)

REST API specifications (OpenAPI, yaml) are maintained in [repository](https://github.com/golemfactory/ya-client/tree/master/specs).
Documentation is generated [here](https://golemfactory.github.io/ya-client/).
Read this documentation if you need to interact with daemon directly without using SDKs. 

### [Golem market protocol properties standards](./standards/README.md)

Describes market protocol properties hierarchy and protocol design conventions.
Read if you are looking for meaning of specific properties.

### [Features specification](./specs/README.md)

Specification of Golem features. This directory contains unified description of protocol.
Read here if you want to know how certain features work, or you need to implement interactions
between Nodes on SDK on daemon level.

### [Golem Amendment Proposals](./gaps/Readme.md) (GAPs)

Process of proposing and discussing changes to Golem protocol.
Read this chapter if you want to observe progressive development of the protocol and motivations
behind design decisions.

