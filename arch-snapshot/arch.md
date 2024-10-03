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
### Provider
### Requestor
Note: We use term Requestor not Requester.
### Developer
### End User
Consumer of resources can be different person than Requestor.
For example we can have web service which forwards computationally expensive jobs
to Golem Network. Example: [Deposits](https://github.com/golemfactory/golem-architecture/blob/master/specs/deposits.md)

#### Service Owner
#### Funder
#### Spender

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

The main problem with Golem Clay was the difficulty in adding new applications. Therefore, in this iteration, it was
decided to divide the system into layers.

The goal is to enable building various solutions and testing hypotheses based on mechanisms provided by the lower
layers.

### Core Network

A basic set of functionalities that allows building a decentralized application.

In this layer, the logic includes:
 - Communication between nodes
 - Discovering offers in the network
 - Ability to negotiate a structure representing the contract description
 - Ability to issue invoices
 - Ability to pay invoices
 - Ability to transmit events leading to the start of contract execution

What is not part of the Core Network:

- File transfer (this needs to be addressed in other layers)
- VPN and other substitutes for direct communication like HTTP proxies, etc.

This layer is divided into fundamental areas:
 - **Identity**: Managing identity in the network. Provides other modules with access to keys identifying the node and an API permissions model.
 - **Market**: Includes functions for searching for offers, broadcasting offers, negotiating contracts, contract registry, and notifications about the start and end of a contract.
 - **Payment**: Includes functions for reserving funds, notifying about costs, agreeing on settlements, and making payments.
 - **Activity**: Transmitting information between parties that an activity needs to start or stop; notifications about resources consumed by the activity.

### Business logic

This layer utilizes components from the Core Network and Execution layers to implement applications.

In this layer, the logic includes:

- How the contract will proceed (how long it will last, under what circumstances it can be terminated, what significance the attributes of the negotiated contract have).
- How the contract will be settled: pre-paid, post-paid, pay-as-you-go.
- How prices for resources will be calculated.
- How offers will be selected for service realization.
- Reputation management.
- Verification of task is being performed correctly.
- Verification of whether the costs make sense.

The concept of building this layer is to create lightweight libraries in scripting languages so that various concepts can be prototyped, particularly the unsolvable problem of verifying the correctness of computations and costs.

Types of applications in this area include:

 - Provider Agent: An application that sells resources on the Core Network.
 - Requestor Agent: A purchasing application.

### Execution

When building applications in the "Business Logic" layer, it becomes apparent that the most variable elements are
negotiation strategies and settlement mechanisms. Regardless of these elements, the way tasks are executed is often
identical. Therefore, it might be beneficial to extract a layer responsible solely for task execution, on which it will
be easy to build custom versions of the provider agent and requestor agent.

This layer is responsible for:

- Downloading images needed to execute the task (e.g., AI models or VM images)
- Managing the cache of these images
- Transferring files using various protocols
- Launching processes and monitoring their state
- Counting resource usage without relying on operating system processes
- Assigning and executing scripts with commands sent to providers
- Tools for local firewall management for outgoing traffic
- VPN mechanisms and other communication methods between running components (e.g., message queue server, HTTP proxy)

In this layer, there will be various execution engines such as:

- VM
- WASI
- HTTP-AUTH
- GamerHash AI runtime
- Outbound gateway
- SGX runtime


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

Section should serve as dictionary to be linked by other chapters.

### Participating entities

#### Yagna daemon
#### Provider agent
#### Requester agent

### Marketplace
#### Offer
#### Demand
#### Subscription
This word is used to describe Offer/Demand put on market, so we should mention it.
#### Proposal
#### Negotiation
#### Agreement

### Execution system
#### Activity
#### Execution environment (ExeUnit)
##### ExeUnit Batch
##### ExeUnit Command
##### VM
##### VM Image
##### WASM
##### WASM image

### VPN
#### Network

### Payment System
#### Payment Driver
#### Payment Platform
#### Token
#### Wallet
#### Allocation
#### Debit Note
#### Invoice
#### Payment
#### Transaction (on blockchain)


## Key architectural decisions
### GLM is built on XYZ
### GLM is used for clearing
### No centralized offer matching rules
### Only providers' offers are propagated
### Agreements are not stored on the blockchain
### Offline requestors are not supported 
### Local storage (TODO: what role does the local DB play?)

## Technical view - components
This section describes key components of Golem Network, i.e. their
responsibilities, interfaces and which other components they utilize.

### Networking
* how it works that two separate Yagnas can talk to each other
#### Central net
#### Hybrid net
- Identification
- Relay
- Discovering Nodes
- P2P communication
- Relayed communication
- Cryptography
  - Node identity verification (challenges)
  - Communication encryption

### GSB
* what it is, how it works and how it imposes a code structure and how
  addressing works

### Offer / negotiation
A description of the component responsible for making offers, counter-offers,
negotiations, etc.

* Offer/Demand properties and constraint langauge
* Yagna is property agnostic - doesn't understand semantic of properties, only
  agent do
* Some examples of properties and costraints and how it works
* Links to more detailed docs for properties langauge and properties
  specification (?)
#### Process of negotiations and making an agreement
- Initial Proposal
- Countering Proposal
- What can change in counter proposal (protocols based on property langauge)?
- Provider Agent possible Proposal responses (counter, reject)
- Requestor Agent possible Proposal responses (counter, reject, propose Agreement)
- Provider Agent possible Agreement responses (accept, reject)
- Requestor possbility of Agreement Proposal cancallation
- Restarting negotiations (who can, who can't and how?)
#### Agreement termination
- Who is allowed to terminate? In what situation?
- What is specified by protocol and what is left to future specifications?
- Termination reason concept

### Offer propagation
* a description of how it happens that offers are visible to reqestors
* Link to design [decision](#only-providers-offers-are-propagated)
* Algorithm overview

### Payments
* a description of current payment driver, its modes of operations and how it
  can be extended
#### Payments models
- Describe generic model which is open for new implementations
- Payment model specification in Offer/Demand language
- Linear Payment model as an example
#### Payments flow during Agreement
- Negotiating payment platform and other payment details
- Testnet(s) vs. mainnet(s)
- Tokens
- Partial payments vs. payments after Agreement finish
- DebitNotes/Invoices interactions (acceptance, rejection, cancellation)
- How DebitNote/Invoice acceptance relates to payment on blockchain?
- Payment settlement and payment confirmation for Provider
#### Payment drivers
- Abstract concept (independance from underlying payment mechanisms)
- How payment platform relates to payment driver?
- Examples: erc20 driver, zksync (?)
#### Payments batching
#### Deposits payments
- Overview of the concept
- Link to external documentation describing details

### Activity
* How the actions on behalf of the requestor are performed
* We should dive into each important and general implementation, i.e. WASM and
  VM
#### Abstract concept
- ExeUnit concept is generic enough to sell any kind of computation resources
- Generic ExeUnits (for example VM, WASM etc.) vs. specialized ExeUnits for specific tasks like:
  - [GamerHash](https://github.com/golemfactory/ya-runtime-ai)
  - [outbound gateway](https://github.com/golemfactory/ya-runtime-outbound)
  - [http authentication](https://github.com/golemfactory/ya-runtime-http-auth)
  - SGX variant of ExeUnit
  - These points are not meant to document those ExeUnits, rather show possible variaty based
    on these examples
- Interaction with yagna through GSB
- Control flow between Requestor and ExeUnit
- Extensible commands list (ExeUnit implementation dependent)
##### Controlling ExeUnit (basic concepts)
- Spawning ExeUnit (contract between Provider Agent and ExeUnit)
  - Self-test
  - Offer template
- Binding to GSB (addressing based on activity id)
- Requestor state control
- Commands and batches:
  - Deploy, Start, Transfer, Run, Terminate
  - Querying command/batch state, receiving results
  - Transfer methods ([GFTP](#gftp), http)
#### ExeUnit Supervisor
- Why splitting Supervisor and Runtime?
- Common functionalities provided by Supervisor
#### ExeUnit Runtime
#### GFTP
#### VM runtime
- Virtual machine desciption (so the reader knows what is there, but not details)
- Functionalities (outbound, VPN, process output capturing)
- VM images, gvmkit-build etc
#### WASM runtime
- WASM supported execution engines
- WASM images

### VPN
* The component responsible for creating a VPN between VMs

### Reputation
* a description of how it is evaluated, distributed and used

### SDK
* which of the logic useful to the user ends up in the SDK

## Technical view - deployment
How the components are reflected in processes, where the processes are run, what
is their relation ship, etc.

## Technical view - flows & algorithms
This section documents how control and responsibility flows through the listed
components to achieve Golem's functionalities. Any non-trivial algorithms
spanning more than one component are also described here.

### Starting a provider and publishing an offer
### Receiving and executing work
### Finding a provider and requesting work
### Starting a cluster of VMs
### Creating a custom image

PR: this is part of the business logic layer. you would need to think about how to add objects from this layer and SDK implementations in different versions to this document. and the concept of building various reputation methods.

PR: ya-provider is also from this layer and you could write down what configurations it supports. e.g. node attestation, authorization certificates, etc.

## Key architectural shortcomings
This section contains known shortcomings of the implemented architecture —
irrespective of whether they were intentional or unintentional.

### Preexisting two categories of actors
The preexisting categories of actors (providers and requesters) and their
asymmetric roles are limiting in certain scenarios. FIXME FIXME FIME
### TODO

