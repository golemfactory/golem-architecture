# Golem — current architecture
Authors: Witold Dzięcioł, Przemysław Rekucki, Marek Dopiera,
\<YOUR NAME GOES HERE\>\
Reviewers: Maciej Maciejowski, Paweł Burgchardt\
Status: WIP
# About this document
The goal of this document is to describe present Golem architecture in enough
detail for an outside person to understand how it works under the hood. The
intended audience is assumed to be technical but not necessarily have deep
expertise in the crypto world. The level of detail stops short of describing
code and its organization but describes key technologies used in order to
implement the needed functionalities.

The aim is not to dive deep into every integration, but to capture architectural
decisions and their consequences.

# Framework Concept

This section describes what comprises Golem network, namely the actors,
technical artifacts and activities they actors may perform on those activities.
The objective of this paragraph is to tie together all the terms and provide a
very high level description of what they are.

## What Golem is
## What Golem is not
## Actors
This section describes the actors using Golem Network and their role in the
system.
### Providers
### Requesters

note: The requestor is what talks directly to the golem node. it could be, for example, a rendering portal. and user is the person who uses this portal. in payment scenarios there are advantages when these roles are separated.

### Software engineer

### Users


## Activities
This section describes what actors can do to the system. The descriptions are
only as detailed as to explain how the actors control the artifacts. The goal is
to give the reader an overview of the terms introduced by Golem without any
details and establish a glossary to ensure consistency within the document.

### Selling on golem platform
### Searching on market
### Buying on golem platform.
### Running something

## Layers

decomposition into layers. responsibility of the layers.

### Golem Node

### Business logic

### Execution

## Functional modules

decomposition into functional areas and scopes of responsibility of these layers.

### Market
### Payment
### Activity
### Identity
### Net

## Applications/Exe-Units

a brief overview of sample applications.

### WASM Runtime

### VM Runtime

### GH/AI Runtime

### HTTP Auth Runtime 

## Artifacts

This section describes the artifacts, i.e. the terms introduced in Golem Network
on which actors can act. They are organized by respective aspects of Golem
Network. The descriptions describe their function rather than their
implementation.

### Participating entities

#### Yagna daemon
#### Provider agent
#### Requester agent
### Marketplace
#### Offer
#### Agreement
#### Invoice
#### Debit note
#### Payment
#### Wallet
#### Allocation
#### Demand
#### Proposal
#### Activity
### Execution environment
#### VM
#### Image
#### WASM image
#### VPN
## Key architectural decisions
### GLM is built on XYZ
### GLM is used for clearing
### No centralized offer matching rules
### Only providers' offers are propagated
### Agreements are not stored on the blockchain
### Offline requestors are not supported 
### Local storage (TODO: what role does the local DB play?)

## Technical view - architecture layers

Bottom-up description of different layers of Golem that impose structure on how
we split components and facilitate communication between them. Golem is not
monolithic and division of responsibility is not arbitrary.

The goal of this chapter is to sketch general idea, rather than describe
specifics.
### Network layer
### GSB layer
RPC-like communication layer providing a way to call other modules or other
nodes in unified way.
### Specialized modules layer (market, payment, activity, vpn etc.)
### Market negotiation protocols
Offers/Demands property and constraint language is used to build subprotocols
for negotiating certain aspects of Agreements.

### Execution environments

There are a few ways to implement an execution environment. We have a few APIs
here:
* Bind and handle GSB messages directly
* Implement RuntimeSDK and use ExeUnit binary for common functionalities
* Command specification which can be executed by ExeUnit is flexible enough that
  higher level protocols could be created here.

### API layer
Each specialized module exposes some APIs to users. It is either REST API, yagna
cli or set of GSB endpoints for communication between modules and nodes.
### REST API
### CLI
### GSB API (communication between modules)
### SDKs layer (python and js SDK)
### Agent application (Requestor script, Provider Agent)
### Higher level SDKs
### Golem compose
### Ray on Golem

## Technical view - functional aspects
This section dives into aspects of Golem network architecture to explain how
they work under the hood. The level of detail does not include code structure,
interfaces nor data structures but names key components and interaction between
them.

note: I would group it by modules.

### Market

#### Offer propagation
#### Negotiation/Agreement
Process of making offers, negotiations and making an agreement

### Payment

TODO: list of contract variants and settlement types. name those 
currently implemented. I don't know if it's worth mentioning the variants for which there is room in the protocol.

#### Contract supervision and billing records.
#### Payments & Clearing


### Net

#### Discovery
#### Identification
#### Communication between nodes
#### Cryptography

### Generic ExeUnit

#### Runtime provisioning

Image downloading, file transfers. 
modes: simple, server. 
requirements to runtimes.

note PR: I would say that this component was a bad decision because it included functionalities 
that are UC specific. it should be a set of libraries.

image cache, etc.

#### Process management

- generating usage counters.
- manage a group of processes so that everything is cleaned properly.

#### Outbound network firewall
 
#### VPN

#### IPC

PR: exe-unit runtime communication

### VM Runtime

#### VM images
#### VM agent

PR: runtime vm agent communication. (protocol over virtio-serial) 

#### Network interfaces

#### Filesystem configuration

#### Tools

PR: I don't know where to mention image building tools. 
secondary server for hosting images for tech who do not have their own http server.

### Processes and the responsibility split between them

### WASM runtime
### VPN

### Reputation management

PR: this is part of the business logic layer. you would need to think about how to add objects from this layer and SDK implementations in different versions to this document. and the concept of building various reputation methods.

PR: ya-provider is also from this layer and you could write down what configurations it supports. e.g. node attestation, authorization certificates, etc.

## Key architectural shortcomings
This section contains known shortcomings of the implemented architecture —
irrespective of whether they were intentional or unintentional.

### Preexisting two categories of actors
The preexisting categories of actors (providers and requesters) and their
asymmetric roles are limiting in certain scenarios. FIXME FIXME FIME
### TODO

