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

### Selling on Golem platform

Even though currently Golem is used for trading computational resources, it has been decided
that it can theoretically be used to trade anything. This means that the Marketplace does not
enforce any standards on goods traded, making it conceptually more similar to [craigslist](https://craigslist.org/) than
to a stock exchange. As a consequence, direct interaction between buyer and seller is required
to make a transaction.

It is the main goal of the [Provider Agent](#provider-agent) to implement logic for selling resources
in Golem Network. From high level perspective, Agent application should do following things: 
1. Describe Resources using property language to create [Offer](#offer)
2. Publish Offer on market
3. Listen on incoming Proposal events and negotiate [Agreement](#agreement) with the most promising [Requestor](#requestor)
4. Wait until [Requestor](#requestor) will demand [Activity](#activity) creation by listening to activity events
5. Allocate promised Resources according to [Agreement](#agreement)
6. Send [DebitNotes](#debit-note) to notify [Requestor](#requestor) with update cost
7. Terminate Agreement or wait for Agreement termination event sent by [Requestor](#requestor)
8. Send [Invoice](#invoice) to summarize the cost of the [Agreement](#agreement)
9. Listen on Payment API events for [Invoice](#invoice) settlement and payment confirmation 

#### 1. Describe Resources using property language to create [Offer](#offer)

The Golem design was created to support the sale of any type of computing resource.To achieve this,
Golem employs a generic [property and constraints language](#discovery-and-offersdemand-matching) to
describe the resources being offered. 

The [Yagna market](#market) is agnostic to the specific properties used and can match Offers and Demands as long
as they adhere to the language specification. However, the Yagna daemon does not interpret the semantics
of the properties in the Offer, nor does its behavior depend on the negotiated Agreement. It is the
responsibility of the Provider Agent application to accurately interpret the semantics and implement
the agreed-upon behavior between the parties.

In this chapter, the term "resource" is used in a generic sense. However, illustrating a generic
example can be challenging. Therefore, we will focus on selling computational power in a virtual
machine (VM) to provide the reader with a clearer understanding.

In this case, the Offer should include the following key aspects:
- The type of [Execution Environment (ExeUnit)](#execution-environments) that will be used.
- [Hardware specifications](https://github.com/golemfactory/golem-architecture/blob/master/standards/cheat_sheet.md#goleminfcpu),
  including the number of CPU cores, RAM, and disk space.
- The price and the [pricing model](#payments-models) applied.
- The payment scheme, outlining how the agent application interacts with the payment system (e.g. in `pay-as-you-use` scheme,
  what is expected transaction frequency).
- The [Wallet](#wallet) address for receiving payments, along with the supported [payment platforms](#payment-platform).

#### 2. Publish Offer on market

Golem is a decentralized network of independent [Nodes](#yagna-node), with no central repository for [Offers](#offer) or any
central server to facilitate [Agreements](#agreement) between parties. As a result, offers must be propagated between nodes,
and transactions are conducted through direct communication.

Developers don’t need to worry about [offer propagation](#offer-propagation). The responsibility for propagating offers
lies with the Yagna daemon, specifically its market module. The only task for the Provider Agent is to publish the offer
on the market using its REST API. The REST endpoint returns a [subscription](#subscription) ID, which can later be used
to listen for incoming [proposal](#proposal) events.

#### 3. Listen on incoming Proposal events and negotiate [Agreement](#agreement) with the most promising Requestor

The [Provider Agent](#provider-agent) plays a passive role in negotiations. Offers are propagated across the network and
received by [Requestors](#requestor). The offer is matched locally on the Requestor's node with a [Demand](#demand).
If the Requestor is interested, they respond by sending a [Proposal](#proposal) to the Provider. This triggers a Proposal
event on the Provider's [subscription](#subscription) endpoint.

Negotiation is the process of exchanging Proposals and adjusting their terms until the Requestor proposes an Agreement.
The structure of a Proposal is identical to that of an Offer or Demand, using the same property and constraint language
to describe the Agreement's conditions. During negotiations, certain aspects of the Agreement can be modified. While Offers
and Demands represent the initial declaration of resources, terms, and conditions, the proposal exchange is a dynamic
process of refining these terms to reach an optimal agreement for both parties.

The negotiation stage serves several purposes:
- Ensures that the Provider and Requestor communicate before signing an Agreement (since offer propagation doesn’t
  require direct interaction between parties).
- Allows both the Provider and Requestor to implement different strategies to maximize their benefits and select suitable partners.
- Provides an opportunity for the Provider and Requestor to negotiate additional terms that weren’t included in the  
  initial Proposals. This is possible through protocols built on top of the property language.



#### 4. Wait until Requestor will demand [Activity](#activity) creation by listening to activity events
#### 5. Allocate promised Resources according to [Agreement](#agreement)
#### 6. Send [DebitNotes](#debit-note) to notify Requestor with update cost
#### 7. Terminate Agreement or wait for Agreement termination event sent by Requestor
#### 8. Send [Invoice](#invoice) to summarize the cost of the Agreement
#### 9. Listen on Payment API events for Invoice settlement and payment confirmation

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

Section should serve as dictionary to be linked by other chapters.

### Participating entities

#### Yagna daemon
#### Yagna Node
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

