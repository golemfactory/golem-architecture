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

Actors: Requestor, Provider

The Requestor wants to purchase a service on the Golem platform. There are multiple entry points to this scenario, but for now, let's assume that it involves purchasing a service on one's own node.

#### Step 1: Starting the Golem Node
 
#### Step 2: Configuring the Wallet

Before the Requestor begins, they must secure appropriate funds. To do this, they should have funds on the address from which they will pay, on one of the two supported blockchains: Ethereum or Polygon. It is strongly recommended to use the Polygon network.

Upon starting the node, a key is automatically generated that identifies the Requestor's identity on the network. For production use, it is recommended to secure the key with a password using the command yagna id lock or generate a new key with a password using yagna id create.

#### Step 3: Funding the Wallet

The Requestor buys GLM tokens on the Polygon network (address: 0x0B220b82F3eA3B7F6d9A1D8ab58930C064A2b5Bf), for example, using the Quickswap application, and transfers them to the key address generated during wallet configuration.

They can also purchase funds via credit card through the onboarding portal. This can be accessed by running yagna payment fund --network polygon.

#### Step 4: Creating an Allocation

Since multiple applications using the same wallet can run on a single node, it is required to reserve funds for a task to reduce potential issues before creating an order. The reservation can be expanded or reduced as execution progresses.

Creating an allocation verifies that:

 - There are sufficient funds on the given wallet and network.
 - There is proper communication with the blockchain node.
 - The sum of allocations does not exceed the account balance.

#### Step 5: Creating a DEMAND

To negotiate an agreement, the requestor's application must create a DEMAND. This object consists of two parts: a description and conditions on the provider's description in the form of a query. Let's assume we want to purchase access to an Ethereum node.

As a requestor, in the query we can specify:

 - The type of runtime engine
 - In our case, also the chain ID

 Next, the DEMAND is extended with information resulting from the allocation and is submitted to the Golem API to receive 
 offer proposals that meet the conditions.

Before Entering Negotiations, the Requestor Should Know:

What will be the mode of conducting the agreement: a fixed-term contract or a long-term one.
What their budget assumptions are.
What their strategy is for selecting the best contractors.
Which billing model they want to choose.

#### Step 6: Negotiations

For the created DEMAND, the Requestor receives a stream of agreement proposals. The proposals can be of two types:

 Initial
 : An offer presented to the network in general without knowledge of the other party. Such an offer should be reviewed, and the Requestor should specify their expectations regarding the contract. An example of such an interaction is reading the list of networks on which the Provider can receive payments and choosing the one preferred by the Requestor.

Draft
: An offer created after the Provider has been presented with the Requestor's expectations. The Requestor can decide to accept such an offer and proceed to create a contract, or they can continue negotiations by changing the terms. Requestor can also reject the offer.

In the case of purchasing access to the Ethereum API, the Requestor needs to find 2-3 
quality nodes. Currently, the sample application implements a naive strategy by selecting the cheapest nodes that meet the conditions. Then, in case of problems, the contract is terminated to search for another Provider.

Negotiations ends with the creation of the agreement. 
After the Provider confirms it, the agreement becomes effective.

#### Step 7: Service Activation and Agreement Supervision

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
#### Market negotiation protocols
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
#### REST API
#### CLI
#### GSB API (communication between modules)
### SDKs layer (python and js SDK)
#### Agent application (Requestor script, Provider Agent)
### Higher level SDKs
#### Golem compose
#### Ray on Golem

## Technical view - functional aspects
This section dives into aspects of Golem network architecture to explain how
they work under the hood. The level of detail does not include code structure,
interfaces nor data structures but names key components and interaction between
them.

### Market interactions
#### Discovery and Offers/Demand matching
- Offer/Demand properties and constraint langauge
- Yagna is property agnostic - doesn't understand semantic of properties, only agent do
- Some examples of properties and costraints and how it works
- Links to more detailed docs for properties langauge and properties sepcification (?)
#### Offer propagation
- Link to design [decision](#only-providers-offers-are-propagated)
- Algorithm overview
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

### Communication between nodes
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
#### GSB communication through Net
- Communication is independent from net implementation
- GSB addressing

### Payments & Clearing
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

### ExeUnits
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
### Processes and the responsibility split between them
#### IPC

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

