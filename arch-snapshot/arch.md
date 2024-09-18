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
# Functional view
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
### Software engineer
## Activities
This section describes what actors can do to the system. The descriptions are
only as detailed as to explain how the actors control the artifacts. The goal is
to give the reader an overview of the terms introduced by Golem without any
details and establish a glossary to ensure consistency within the document.
### Provisioning a provider
### Setting up a requester
### Finding a provider
### Fulfilling an agreement
### Creating an image
### Starting a VM
### Setting up a VPN
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
### Discovery
### Identification
### Offer propagation
### Communication between nodes
### Process of making offers, negotiations and making an agreement
### Payments & Clearing
### IPC
### Processes and the responsibility split between them
### VM images
### WASM runtime
### VPN
### Reputation management
### Cryptography
## Key architectural shortcomings
This section contains known shortcomings of the implemented architecture —
irrespective of whether they were intentional or unintentional.
### Preexisting two categories of actors
The preexisting categories of actors (providers and requesters) and their
asymmetric roles are limiting in certain scenarios. FIXME FIXME FIME
### TODO

