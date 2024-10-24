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

Golem Network allows buyers and sellers to connect and reach agreements. The market is designed to be asymmetrical:
the sellers ([Providers](#provider)) publish Offers, and the buyers ([Requestors](#requestor)) browse through these Offers.
When a Requestor finds a suitable Offer, they contact the Provider directly to negotiate the deal.

Typically, humans are not involved in the process of finding, matching, negotiating, or finalizing agreements. Instead,
users define their needs programmatically, allowing the [Provider Agent](#provider-agent) and [Requestor Agent](#requester-agent)
software to handle these tasks automatically.

The [Provider Agent](#provider-agent) is primarily responsible for implementing the logic needed to sell resources on
the Golem Network. From high level perspective, Provider Agent application should do following things: 
1. Describe Resources using property language to create an [Offer](#offer)
2. Publish the Offer on the market
3. Monitor incoming Proposals and negotiate an [Agreement](#agreement) with the most promising [Requestor](#requestor)
4. Allocate the promised Resources in accordance with the [Agreement](#agreement)
5. Monitor resources usage and charge Requestor Agent
6. Terminate the Agreement or await the Agreement termination event from the [Requestor](#requestor)
7. Send an [Invoice](#invoice) summarizing the total cost of the [Agreement](#agreement)
8. Wait until the payment for the [Invoice](#invoice) is settled and payment confirmed. 

#### 1. Describe Resources using property language to create an [Offer](#offer)

While Golem is currently used for trading computational resources, it was designed to support the exchange of any type of
[resource](#resource). This means the Marketplace does not enforce strict standards on the goods being traded.
To enable this flexibility, Golem uses a generic [property and constraints language](#discovery-and-offersdemand-matching)
to describe the resources being offered. The Core Network does not interpret the semantics of the properties in the Offer,
nor does its behavior depend on the negotiated Agreement. It is the responsibility of the Provider Agent application to
accurately interpret the semantics and implement the agreed-upon behavior between the parties.

In this chapter, the term ["resource"](#resource) is used in a generic sense. However, illustrating a generic
example can be challenging. Therefore, we will focus on selling computational power in a virtual
machine (VM) to provide the reader with a clearer understanding.

In this case, the Offer should include the following key aspects:
- The type of [Execution Environment (ExeUnit)](#execution-environment-exeunit) that will be used. (The [VM](#vm-runtime)
  is an example of an execution environment. [WASM runtime](#wasm-runtime) is another)
- [Hardware specifications](https://github.com/golemfactory/golem-architecture/blob/master/standards/cheat_sheet.md#goleminfcpu),
  including the number of CPU cores, RAM, and disk space.
- The price and the [pricing model](#payments-models) applied.
- The payment scheme, outlining how the agent application interacts with the payment system (e.g. in `pay-as-you-use` scheme,
  what is expected transaction frequency).
- The [Wallet](#wallet) address for receiving payments, along with the supported [payment platforms](#payment-platform).

#### 2. Publish the Offer on the market

Golem is a decentralized network of independent [Nodes](#yagna-node), with no central repository for [Offers](#offer) or any
central server to facilitate [Agreements](#agreement) between parties. As a result, offers must be propagated between nodes,
and transactions are conducted through direct communication.

Developers don’t need to worry about [offer propagation](#offer-propagation). The responsibility for propagating offers
lies with the Core Network. The only task for the Provider Agent is to publish the offer on the market and listen
for incoming [Proposals](#proposal).

#### 3. Monitor incoming Proposals and negotiate an Agreement with the most promising Requestor

The [Provider Agent](#provider-agent) plays a passive role in negotiations. Offers are propagated across the network and
received by [Requestors](#requestor). The offer is matched locally on the Requestor's node with a [Demand](#demand).
If the Requestor is interested, they respond by sending a [Proposal](#proposal) to the Provider Agent.

[Negotiation](#process-of-negotiations-and-making-an-agreement) is the process of exchanging Proposals and adjusting their
terms until the [Requestor Agent](#requester-agent) proposes an Agreement. The structure of a Proposal is identical to
that of an Offer or Demand, using the same [property and constraints language](#discovery-and-offersdemand-matching) to 
describe the Agreement's conditions. During negotiations, certain aspects of the Agreement can be modified. While Offers
and Demands represent the initial declaration of resources, terms, and conditions, the proposal exchange is a dynamic
process of refining these terms to reach an optimal [Agreement](#agreement) for both parties.

The negotiation stage serves several purposes:
- Ensures that the Provider Agent and Requestor Agent communicate before signing an Agreement (since offer propagation
  doesn’t require direct interaction between parties).
- Allows both the Provider Agent and Requestor Agent to implement different [strategies](#market-strategies) to maximize
  their benefits and select suitable partners.
- Provides an opportunity for the Provider Agent and Requestor Agent to negotiate additional terms that weren’t included
  in the initial [Proposals](#proposal). This is possible through protocols built on top of the property language.

Both the Provider Agent and Requestor Agent negotiate with multiple Agents simultaneously. The Requestor Agent initiates
the Agreement by proposing it to the Provider Agent, who can either accept or reject the Proposal. Once the Agreement is signed
by both parties, the Requestor Agent can begin using the resources. The Agreement remains valid until it is terminated by
either party. The terms of termination (e.g., duration of the Agreement and conditions under which it can be terminated) are
specified within the Agreement.

```mermaid
---
title: Simplified negotiations from Provider Agent's perspective
---
sequenceDiagram
  actor ProviderAgent as Provider Agent
  participant GolemNetwork as Golem Network
  actor RequestorAgent as Requestor Agent

  GolemNetwork->>RequestorAgent: Receive propagated Offer
  Note over GolemNetwork,RequestorAgent: Offer is not necessarily received directly<br/> from Provider Node
  RequestorAgent->>RequestorAgent: Match Offer with Demand <br/>generate Proposal as a result

  loop
    RequestorAgent->>RequestorAgent: Adjust Proposal
    
    par
      RequestorAgent->>ProviderAgent: Counter Proposal
    and Proposals from other Nodes in the network
      GolemNetwork->>ProviderAgent: Receive Proposals    
    end

    ProviderAgent->>ProviderAgent: Select best Proposals <br/>according to implemented strategy
    ProviderAgent->>ProviderAgent: Adjust Proposals
    ProviderAgent->>RequestorAgent: Counter Proposal

    break when the terms of Agreement are satisfactory
      RequestorAgent->>ProviderAgent: Propose Agreement
    end
  end
  
  par Proposals from other Nodes in the network
    GolemNetwork->>ProviderAgent: Receive other Agreement Proposals
  end
  ProviderAgent->>ProviderAgent: Select best Agreement Proposal
  ProviderAgent->>RequestorAgent: Approve Agreement Proposal
  ProviderAgent->>GolemNetwork: Reject remaining Agreement Proposals
```

##### Example of negotiation

To better understand the [Negotiation](#process-of-negotiations-and-making-an-agreement) process, let’s consider an example 
involving the negotiation of a [payment platform](#payment-platform). This will illustrate how agents can use different
strategies and what negotiation protocols can be built on top of the [property and language](#discovery-and-offersdemand-matching).

When declaring a payment platform in an [Offer](#offer), the Provider Agent lists [wallet](#wallet) addresses for each
platform it supports. It is the Requestor Agent's responsibility to choose the platform by specifying the appropriate
[property](#property) in their demand. The Requestor Agent can approach negotiations in two ways:

###### 1. Static Negotiations
Suppose the Requestor Agent prefers payments on the Polygon network. In this case, they require the Provider Agent to support
Polygon and will not select a Provider Agent that doesn’t.

Since the Requestor has a specific requirement, multiple negotiation stages aren't necessary. They can simply add
a [constraint](#constraint) to their [Demand](#demand), instructing the matching algorithm to filter out Providers that
don’t meet this requirement. In their Demand, they set the chosen platform as a fixed value.

###### 2. Dynamic Negotiations
Now imagine a Requestor Agent that can pay on multiple platforms but prioritizes them based on transaction fees. In this
scenario, the Requestor Agent has a larger pool of potential Providers since they don’t restrict the platform by adding
a constraint to their demand.

Instead, the Requestor Agent collects Proposals from the market and evaluates them based on estimated costs. In later stages
of Proposal exchange, they choose the platform by setting the relevant [property](#property) according to the Providers' scores,
which are based on potential [transaction](#transaction-on-blockchain) costs.

#### 4. Allocate the promised Resources in accordance with the Agreement

Once the [Agreement](#agreement) is signed, the Provider Agent is expected to reserve the promised [resources](#resource) for the
Requestor’s use. During this time, the Provider Agent cannot sell these resources to anyone else and must be prepared
to start the [Activity](#activity). For instance, if the Provider Agent is selling computing power through a [Virtual Machine Execution
Environment](#vm), they declared in the Agreement a specific amount of RAM and a certain number of threads to be allocated for the VM.
The Provider Agent can only sell any remaining RAM and cores to other Requestors.

Making an Agreement reserves the Provider's resources. To actually use these resources, the Requestor Agent must take
an additional step by creating an [Activity](#activity). Most parameters are already included in the Agreement, but some
additional parameters may be required and will be specified later using Activity commands. For example, if the Requestor Agent
wants to utilize the Provider's resources by running a virtual machine, details like the [image](#vm-image) to run, RAM, and the
number of cores are taken from the Agreement. However, to allocate an IP address or transfer necessary files, Activity
[commands](#controlling-exeunit-basic-concepts) are used. Further details on controlling an Activity from the Requestor's
perspective can be found in the["Running something"](#running-something) section.

From the Provider Agent's perspective, the primary focus is to listen for incoming Activity events and create an Activity
when requested by the Requestor Agent. Upon receiving an Activity creation event, the Provider Agent should spawn an
[ExeUnit](#exeunits) process (and a Virtual Machine in consequence). Conversely, receiving an Activity destruction event should trigger
the termination of the ExeUnit processes.

The Requestor Agent is allowed to spawn multiple Activities consecutively. In general, multiple Activities running
simultaneously may be permitted; however, this does not apply in the case of a [Virtual Machine](#vm), as hardware
resources can only be allocated once.

#### 5. Monitor resources usage and charge Requestor Agent

The [ExeUnit](#exeunits) is directly controlled by the Requestor Agent, with no intervention from the Provider Agent.
Communication happens solely between the Requestor Agent and the ExeUnit. However, this doesn't mean the Provider
Agent is inactive during this time. The Provider Agent's responsibility is limited to calculating the cost of resource usage
based on the pricing model defined in the Agreement and informing the Requestor Agent accordingly.

There are two types of payment documents used in the Golem Network: [Debit Notes](#debit-note) and [Invoices](#invoice).

Debit Notes are sent at regular intervals during the execution of an activity to inform the Requestor Agent of the
accumulating costs of the Agreement. These notes act as building blocks that support various payment schemes.
The handling of Debit Notes by Agents is governed by the terms negotiated in the Agreement. Generally, Debit Notes
serve the following purposes:
- Informing the Requestor Agent about resource usage and activity costs, and obtaining explicit acceptance of these costs.
- Acting as a health check, allowing the Provider Agent to monitor if the Requestor Agent is still active and hasn’t
  abandoned the Agreement, helping avoid not getting paid.
- Facilitating [mid-agreement payments](#mid-agreement-payments).

Invoices are issued after the Agreement is terminated, providing a summary of the total costs. They allow the Provider
Agent to include any additional costs not covered in the Debit Notes, as the final Debit Note doesn’t have to be sent
immediately after the activity ends.

```mermaid
flowchart LR
Activity1((Activity 1)) --o D11[Debit Note 1] --> D12[Debit Note 2] -->|...| D13[Debit Note N-th] --> Invoice[Invoice]
Activity2((Activity 2)) --o D21[Debit Note 1] --> D22[Debit Note 2] -->|...| D23[Debit Note N-th] --> Invoice[Invoice]
Activity3((Activity 3)) --o D31[Debit Note 1] --> D32[Debit Note 2] -->|...| D33[Debit Note N-th] --> Invoice[Invoice]
```

Both Debit Notes and Invoices can be either accepted or rejected by the other party. Acceptance signals that the
Requestor Agent agrees to pay the specified amount. Rejection, on the other hand, indicates refusal to pay the
non-accepted amount. However, it’s important to note that a rejection does not absolve the Requestor Agent from paying
for all previously accepted Debit Notes. The conditions under which rejection is allowed should be defined in the
Agreement. Currently, no payment scheme permits rejections.

Accepting a Debit Note or Invoice does not result in immediate payment for a few reasons.
Debit Notes can be classified as payable or non-payable, with payable Debit Notes identified by the due date included
in the document. While payable Debit Notes are scheduled for processing upon acceptance, this still does not necessitate
immediate payment. The payment mechanism allows for the [batching of payments](#payments-batching) or delaying them
to accommodate additional Debit Notes or [Invoices](#invoice), thereby reducing [transaction](#transaction-on-blockchain) costs on the blockchain.

The consequence of delaying payments is that they are not guaranteed. However, this design opens the possibility of
implementing mechanisms that can mitigate or eliminate the risk of non-payment. For instance, a payment platform
could be developed using a deposit or escrow contract, or by integrating payment channels into the Core Network.

It’s important to note that, regardless of the payment scheme or platform used, Golem Factory does not act as an
intermediary for payments. Since transactions occur on the blockchain, and due to the decentralized nature of blockchain
technology, Golem Factory has no control over these transactions.

#### 6. Terminate the Agreement or await the Agreement termination event from the Requestor Agent

The [Agreement](#agreement) can be terminated when either party chooses to end it. Core Network doesn't enforce any
specific termination rules, so the Agreement should clearly define the conditions under which termination is
permitted. Below is a non-exhaustive list of possible reasons for termination:
- The Agreement expires if it was established for a fixed duration.
- The Requestor Agent no longer needs the [resources](#resource) or has completed the computations.
- One of the parties violates the terms of the Agreement, such as:
  - The Requestor Agent fails to accept [Debit Notes](#debit-note) within the agreed timeframe.
  - The Provider Agent issues Debit Notes more frequently than agreed.
  - The Requestor Agent fails to make timely payments, particularly in cases involving [mid-agreement payments](#mid-agreement-payments).

It is the Agent—whether Requestor or Provider—who decides to terminate the Agreement. The Agent is also responsible for
detecting if the other party has terminated the Agreement and taking the appropriate action in response.

Provider Agent has the option to attach additional information outlining the reasons for termination when ending the
Agreement. While this is not mandatory, it is encouraged as it can provide valuable context for the other party,
serving as diagnostic information or for other purposes.

#### 7. Send an Invoice summarizing the total cost of the Agreement

Once the Agreement is terminated, the Provider Agent should send an [Invoice](#invoice) to the Requestor Agent summarizing
the total costs incurred throughout the Agreement. This Invoice should reflect the cumulative costs from all [Activities](#activity).
In response, the Requestor Agent must either accept or reject the Invoice. However, regardless of the acceptance status,
payment is mandatory for the total amount indicated by the accepted [Debit Notes](#debit-note), as their acceptance constitutes
a binding commitment to pay.

#### 8. Wait until the payment for the Invoice is settled and payment confirmed.

It's important for the Provider to monitor payments after the Agreement is completed. This is when the Provider Agent
should adjust its market strategy to ensure profitability. Since the Core Network doesn't guarantee payment delivery,
the Provider Agent should implement measures to prevent being exploited by Requestors. One example is rejecting
non-paying Requestors and prioritizing those with a good reputation. Lack of payment isn't the only reason for
declining a Requestor in the future. The Provider Agent may also choose to reject subsequent Agreements with
Requestors who break the Agreement conditions.

Payment confirmation is received by the Provider Agent from the Requestor once the transaction is confirmed on the
blockchain. This confirmation specifies which Activities and Agreements are covered by the transaction. There is no
1-to-1 relationship between transactions and Activities or Agreements. A single blockchain transaction can cover
multiple Activities or Agreements, while each Activity or Agreement may also be covered by multiple transactions.

### Searching on market

Providers or Requestors on Golem don’t always aim to buy or sell resources. Sometimes, they simply want to observe the market.
There are several possible reasons for this:
- Checking which resources are available on the network
- Estimating potential computation costs
- Gathering insights to better adjust negotiation strategies
- Collecting statistics about the network

The approach to observing the market is similar to the methods described in the chapters on buying and selling resources.
An agent specifies their Offer or Demand using a property and constraints specification language.
The key difference is that they are not required to negotiate with other agents, nor must they provide all necessary
information in their Offer or Demand. This is a more lightweight approach compared to going through the full negotiation
process.

### Buying on golem platform
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

#### Core Network
#### Yagna daemon
#### Yagna Node
#### Provider agent
#### Requester agent

### Marketplace
#### Offer
##### Property
##### Constraint
#### Demand
#### Subscription
This word is used to describe Offer/Demand put on market, so we should mention it.
#### Proposal
#### Negotiation
#### Agreement

### Execution system
#### Resource
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

### GSB
* what it is, how it works and how it imposes a code structure and how
  addressing works

### Networking

The core networking component in Golem is the yagna Net module. It acts as a middleman between the other modules on 
the yagna daemon and the Golem Network by facilitating message exchange with [GSB (Golem Service Bus)](#gsb) and the 
network itself. The Net module provides a uniform interface that allows for different implementations of the 
networking layer.  

The [Net Module interface](#net-module-interface) chapter will focus on general networking concepts, while specific 
implementations will be covered in the [Hybrid net](#hybrid-net) and [Central net](#central-net) chapters. 

```mermaid
flowchart TB
  GolemNetwork(((GolemNetwork)))
  
  subgraph Node1[Golem Node]
      Net1>Net]
      Market1[Market] <--->|GSB| Net1
      Activity1[Activity] <--->|GSB| Net1
      Payment1[Payment] <--->|GSB| Net1
      VPN1[VPN] <--->|GSB| Net1
      ExeUnit1[ExeUnit] <--->|GSB| Net1
  end
  Net1 <-...-> GolemNetwork
  
  subgraph Node2[Golem Node]
    Net2>Net]
    Market2[Market] <--->|GSB| Net2
    Activity2[Activity] <--->|GSB| Net2
    Payment2[Payment] <--->|GSB| Net2
    VPN2[VPN] <--->|GSB| Net2
  end
  Net2 <-...-> GolemNetwork
```

#### Net Module interface

The Network module offers the following functionalities:
- Sending messages to other Nodes
- Forwarding received messages to the appropriate modules
- Broadcasting messages to the network
- Receiving and processing broadcasted messages

##### Address translation

The Net module follows specific GSB address naming conventions to enable cooperation with other modules. Addresses 
prefixed with `/net/{NodeId}` are reserved for the Net module, where it listens for incoming messages and forwards 
them to the Golem Network. Conversely, addresses starting with `/public/...` are available for yagna modules to expose 
public methods that can be called from other Nodes.   

When the Net module receives a local incoming message, it extracts the NodeId from the address prefix and uses it to 
forward the message into the Golem Network. On the receiving end, messages coming from the Network are processed, 
and the address is checked to extract the NodeId. If the NodeId belongs to the recipient Node, the address is routed to 
the appropriate GSB handler registered under the `/public/...` address.   

```mermaid
block-beta
    columns 2
    Prefix{{"Prefix"}}
    Address{{"Address"}}
    
    Prefix1["/net/0x467ab03ac10877d0ccff89fac547a4ce8aa0cc5e"]
    Address1["/market/protocol/mk1/discovery/offers/Get"]
    
    arrow1<["Translate"]>(down)
    space
    
    Prefix2["/public"]
    Address2["/market/protocol/mk1/discovery/offers/Get"]
```

##### Broadcasting

Message broadcasting in the Net module is organized around the concept of 'topics,' which can be thought of as 
message categories. Different modules can register a message handler with the Net module that gets triggered 
whenever a message for a specific topic is received. 

To send a broadcast, a module must send a GSB message to the Net module on the designated topic. The Net module then 
forwards this message to the network. Depending on the network's implementation, the message may be routed either to 
neighboring Nodes or to all Nodes across the network.  

```mermaid
sequenceDiagram
    participant Market
    participant Net
    participant GolemNetwork
    
    Note over Market, Net: Those are only example addresses for illustration 
    Market->>Market: Bind GSB handler for Offers broadcast (for example '/market/offers')
    Market->>Net: Subscribe topic `OffersBcast`, register handler '/market/offers'
    GolemNetwork->>Net: Broadcast for topic `OffersBcast
    Net->>Net: Find all handlers for topic `OffersBcast
    Net->>Market: Call '/market/offers'
    Market->>Market: Select previously unseen Offers
    Market->>Net: Send re-broadcast of new Offers
    Net->>GolemNetwork: Broadcast new Offers to neighborhood
```

##### Handling identities

Each Golem Node can have multiple identities, with one of them (the default identity) used to identify the Node 
within the network. However, operations on a Golem Node can also be performed in the context of secondary identities.
The Net module must be able to handle messages sent to and from any of these identities. For more information on 
identification, refer to the chapter about the [identity module](#identity). This section focuses solely on the Net 
module interface.

In addition to the GSB endpoints bound to the `/net/{NodeId}` prefix, as described in the [Address Translation 
chapter](#address-translation), there is another prefix: `/from/{LocalId}/to/{RemoteId}`. This enables messages to 
be sent from a specific identity on one Node to a specific identity on a remote Node.

Another important aspect is that the Net module always checks if the target identity belongs to the local Node. If 
it does, the message is routed back to the local GSB instead of being sent over the network. This mechanism allows 
GSB calls to be handled uniformly by the calling code, regardless of whether the target is local or remote.

##### Reliable, unreliable and transfers channels

The Net module supports multiple channels for message transmission. The basic channel provides reliable message 
delivery via GSB, which is used for most control messages between Nodes.

However, certain functionalities require different handling. For example, VPN embeds IP packets into GSB messages 
and routes them through the Golem Protocol. Although VPN users can choose any protocol, TCP is typically used 
because many higher-level protocols rely on it. Sending VPN messages through a reliable protocol would hurt 
performance, as this would essentially embed TCP within TCP (or another reliable protocol implemented in Net). To 
address this, the Net module also allows for sending messages in an unreliable manner without packet delivery 
guarantee.

The third option is the transfer channel. Mixing transfers with GSB control messages can cause delays, as large file 
transfers can quickly fill the sender’s buffer queue. To avoid this, it is recommended to use a separate channel 
specifically for transfers.

#### Hybrid Net

Hybrid Net was developed as an intermediate step towards decentralization, enabling peer-to-peer (P2P) communication 
between some of Golem Nodes. However, since most of the network operates behind NATs, P2P cannot be the sole 
communication method. To address this, the Net implementation supports communication forwarding through specialized 
component known as Relay.

An additional advantage of relay server is it's ability to expedite Node discovery. In a pure P2P network, Node
discovery can be slow, as no single Node has a complete view of the network, requiring multiple hops to find new Nodes.
Relay server can also facilitate P2P communication between Nodes when direct connections are not possible.

Although HybridNet remains centralized for node discovery and relaying communication between Nodes behind NATs, 
broadcasting has already been designed in a decentralized manner as a step toward full network decentralization.

##### Nodes identification

A Golem Node is identified by its NodeId, which is derived from its public key. This NodeId allows the Node to be 
located within the network, and the public key is used to verify the Node’s identity. This ensures that only one 
Node can claim a specific ID within the network.

Additionally, each Node may have multiple secondary identities, each associated with its own public/private key pair.
The Net module must be capable of locating a Golem Node based on these secondary identities as well as the primary 
identity.

##### Relay server

The Relay server is a core component of the networking layer in the Golem Network. All newly connected Nodes 
register with the Relay server, providing the necessary information for discovery and communication. The Relay server:
- Maintains a list of Nodes present in the network.
- Stores each Node's public key, [identity](#identity) derived from the public key, and associated IP address.
- Stores information about which secondary identities are associated with specific Nodes.
- Assists in establishing peer-to-peer (p2p) communication when possible.
- Routes traffic between Nodes if a p2p communication cannot be established.
- Checks if connecting Nodes have public IP port exposed
- Offers functions for:
  - Querying Node's information.
  - Retrieving a Node's neighborhood.

Communication with the Relay server is handled through a custom protocol built on top of UDP, defined using Protocol 
Buffers (protobuf). UDP was chosen for its lightweight nature, as it does not require maintaining open connections, 
which would consume more system resources compared to TCP. This makes it possible to handle a large number of Nodes 
concurrently, ensuring decent scalability.

###### Connecting to Relay server

The Hybrid Net protocol introduces the concept of a Session, which operates on top of the UDP protocol. All requests 
to the Relay server and network traffic routing occur within the context of a Session, with only one Session allowed 
at a time. To establish a Session with the Relay server, a Node must undergo a handshake process that serves several 
purposes:
1. Verifies that the Node presenting its NodeId possesses the private key corresponding to that NodeId.
2. Collects and verifies any secondary identities associated with the Node.
3. Gathers additional Node information, such as supported symmetric encryption algorithms.
4. Determines if the Node is behind a NAT or if it has a public IP address and exposed port. The public IP acquired in 
   this step may later be used by other Nodes to establish a peer-to-peer Session.

**Challenge**

Identity verification is performed via a challenge mechanism, where the Relay server sends random bytes to the Node, 
which must compute a hash with a specified number of leading zeros. The difficulty level, set by the Relay server, 
determines the number of leading zeros required. This computationally expensive task protects the Relay server from 
DDoS attacks by forcing the Node to complete a certain amount of work before establishing a Session.

The challenge response is cryptographically signed using the private key of each identity associated with the Node.
The Relay server can then recover the Node's identities and public keys from these signatures, verifying that the Node 
possesses the corresponding private keys. The recovered public keys are later made available to other Nodes that 
request information about the Node.

**Public IP check**

When a Node initiates a Session, an additional mechanism is required to determine whether the IP address from which 
the packets are received is behind a NAT. This is achieved by sending Ping packets (network protocol ping, not to be 
confused with the ICMP packet) from a different UDP port than the one used for receiving incoming Sessions. This 
ensures that any network devices between the Node and the Relay server won't recognize these Ping packets as part of 
the same communication stream. If the ports are not publicly exposed, the Ping packets will be dropped, confirming 
that the Node is behind a NAT.

```mermaid
sequenceDiagram
    participant GolemNode as Golem Node 1
    participant RelayServer as Relay Server
    
    GolemNode->>RelayServer: Session request
    RelayServer->>GolemNode: Challenge
    GolemNode->>GolemNode: Solve challenge
    GolemNode->>GolemNode: Sign solution with Node's identities
    GolemNode->>RelayServer: Challenge response
    RelayServer->>RelayServer: Verify solution
    RelayServer->>RelayServer: Recover identities from signatures
    RelayServer->>GolemNode: Session established response
    
    GolemNode->>RelayServer: Register
    activate RelayServer
    RelayServer-->>GolemNode: Ping from different UDP port (Check if IP address is public)
    alt 
        GolemNode-->>RelayServer: Ping response
    else Timeout
        GolemNode--xRelayServer: Packet dropped due to NAT
    end
    RelayServer->>GolemNode: Register response (discovered public address)
    deactivate RelayServer

    Note right of GolemNode: Use session to discover Nodes 

    loop In regular intervals to keep session alive
        GolemNode->>RelayServer: Ping
        RelayServer->>GolemNode: Pong
    end
    
    opt Close Session
        GolemNode->>RelayServer: Disconnect
    end
```

Important note: The Relay server does not possess private keys, and its identity is not verified in the current 
implementation. This marks a significant distinction compared to the process of establishing peer-to-peer Sessions 
with regular Nodes.

##### Establishing Sessions between Nodes

Currently, a peer-to-peer (p2p) Session can be established in two scenarios. In the first, if the target Node has a 
public IP, the initiating Node can directly connect to it. In the second scenario, where the initiating Node has a 
public IP but the target Node is behind a NAT, the Session is facilitated by the Relay server. The initiating 
Node first sends a Reverse Connection message to the Relay server, which forwards it to the target Node. The target 
Node then attempts to establish a direct Session with the initiating Node. Whether the target Node has a public 
IP can be determined based on information returned by the Relay server.

Since the current Net implementation does not support NAT hole punching, if both Nodes are behind NAT, communication 
must be routed through the Relay server.

```mermaid
---
title: Scenarios of communication between Nodes   
---
sequenceDiagram
    participant GolemNode1 as Golem Node 1
    participant RelayServer as Relay Server
    participant GolemNode2 as Golem Node 2

    GolemNode1-->RelayServer: Established Session
    GolemNode2-->RelayServer: Established Session

    GolemNode1->>RelayServer: Get Node 2's information
    RelayServer->>GolemNode1: Node 2's information
    
    alt Golem Node 2 has public IP
        GolemNode1->>GolemNode2: Establish P2P Session
    else Golem Node 1 has public IP
        GolemNode1->>RelayServer: Reverse Connection message
        RelayServer->>GolemNode2: Reverse Connection message

        GolemNode2->>RelayServer: Get Node 1's information
        RelayServer->>GolemNode2: Node 1's information
        
        GolemNode2->>GolemNode1: Establish P2P Session
    else Golem Node 1 and 2 are behind NAT
        par Communication routed through relay
          GolemNode1->>RelayServer: Forward packet
          RelayServer->>GolemNode2: Forward packet
          GolemNode2->>RelayServer: Forward packet
          RelayServer->>GolemNode1: Forward packet 
        end
    end
```

**Peer-to-peer Session handshake**

Establishing a peer-to-peer (p2p) Session between Nodes is similar to the Relay server handshake but with a few 
key differences:
- Both Nodes must solve a challenge and prove their identities to each other.
- There is no registration step or public IP check, as the public IP of the Nodes is already known.
- The initiating Node sends a "Resume Forwarding" control message to the target Node, informing it that it can begin 
  sending packets. This step ensures that packets arriving too early are not dropped.

```mermaid
---
title: Protocol for establishing peer-to-peer Session   
---
sequenceDiagram
    participant GolemNode1 as Golem Node 1
    participant GolemNode2 as Golem Node 2
    
    GolemNode1->>GolemNode2: Session request (+challenge)
    GolemNode2->>GolemNode1: Challenge
    
    GolemNode1->>GolemNode1: Solve challenge
    GolemNode2->>GolemNode2: Solve challenge
    GolemNode1->>GolemNode1: Sign solution with Node's identities
    GolemNode2->>GolemNode2: Sign solution with Node's identities
    
    GolemNode1->>GolemNode2: Challenge response
    GolemNode2->>GolemNode2: Verify solution
    GolemNode2->>GolemNode2: Recover identities from signatures
    GolemNode2->>GolemNode1: Challenge response

    GolemNode1->>GolemNode1: Verify solution
    GolemNode1->>GolemNode1: Recover identities from signatures
    Note over GolemNode1: Node can start sending packets from this moment

    GolemNode1->>GolemNode2: Resume Forwarding control message
    Note over GolemNode2: Node can start sending packets from this moment

    loop In regular intervals to keep Session alive
        GolemNode1->>GolemNode2: Ping
        GolemNode2->>GolemNode1: Pong
    end
    
    opt Close Session
        GolemNode1->>GolemNode2: Disconnect
    end
```

##### Forwarding Network Traffic

The low-level abstraction provides a single message type for sending data: the `Forward` packet. This packet can be 
used to send arbitrary content between Nodes, either directly or through the Relay server. Like UDP, the Forward 
packet does not offer delivery guarantees. It is the responsibility of higher-level layers to ensure the correct and 
reliable delivery of data in case it is necessary.  

##### Virtual TCP

To enable reliable data delivery, the hybrid Net utilizes an embedded TCP stack implementation. This stack takes an 
input data stream and generates IP packets, which are then sent using Forward packets. The receiving Node employs 
the TCP stack to decode incoming packets back into a message stream. These messages are subsequently dispatched as 
GSB messages and passed to the appropriate modules, as detailed in the [Address Translation chapter](#address-translation).

##### Reliable, unreliable and transfers channels in Hybrid Net

Different channels can be utilized to send messages, as explained in the [reliable, unreliable, and transfers 
channels chapter](#reliable-unreliable-and-transfers-channels). In the Hybrid Net, reliable and transfer channels 
are distinguished by using separate TCP connections. This separation ensures that independent sender buffers are 
maintained, preventing messages in one channel from being blocked by messages in the other. Each channel type has 
its own single TCP connection. This means that independent transfer still can interfere with each other.

The sender can also opt to use the unreliable channel, where GSB messages are sent directly as `Forward` packets 
without message fragmentation. A key implication of this is that large GSB messages could exceed the Maximum 
Transmission Unit (MTU) and may be dropped by network devices along the packet's route.

##### Broadcasting and neighborhood

**What is a Neighborhood?**

Before diving into broadcasting, it's essential to understand the concept of a neighborhood. In a network, a 
neighborhood is a subset of Nodes that are considered closest to a given Node based on an abstract metric. Each Node 
has its own neighborhood. This metric doesn't necessarily reflect the real-world proximity of Nodes. For instance, 
two Nodes on opposite sides of the globe could be neighbors, while two Nodes within the same physical network may be 
too distant in terms of this metric to be in the same neighborhood.

In peer-to-peer networks, the concept of Node distance is often used for efficient Node discovery. However, since 
the current Golem network layer relies on the Relay server for Node discovery, neighborhoods don't serve that 
purpose. Although it’s conceivable that a fallback, such as a Kademlia-like implementation, could be used in the 
event of a Relay server downtime, for now, the primary purpose of the neighborhood concept is broadcasting.

Sending broadcast message is an operation used by various algorithms to disseminate information throughout the network. 
Its most significant application is in [the Offer Propagation algorithm](#offer-propagation). While the implementation of 
specific algorithms is handled by other modules, the network module provides the necessary operations as building 
blocks for these processes.

**Broadcasting**

Hybrid Net implements local broadcast operation that sends message to the nearest neighborhood of the Node. To query 
its neighbors, a Node can send a `Neighborhood` request to the Relay server. The Relay server then responds with a 
list of Nodes that are closest to the querying Node, based on a predefined metric. 

After receiving the list of neighbors, the Node attempts to establish Sessions with them, as described in the 
chapter on [communication](#establishing-connections-between-nodes). The neighborhood algorithm does not 
differentiate between Nodes capable of establishing peer-to-peer Sessions and those that require relayed 
communication. Unlike IP-level broadcasts, Hybrid Net uses reliable channels for message transmission. 

**Neighborhood - distance function**

To prevent clustering of Nodes and accidental splits in the network, where subsets of Nodes become unreachable, a proper
neighborhood function must be utilized. This function is defined by the network module, and the market relies on the
broadcast function, leaving it with no alternative in this regard.

Optimal guarantees can be achieved when two neighboring Nodes have distinctly different neighborhoods, minimizing their
number of common neighbors. Currently, in the Hybrid Net, neighborhood is determined based on the reversed Hamming
distance between Node IDs.

##### Encryption

The currently released version of Hybrid Net does not support encryption, but this feature is in progress. This 
chapter is important to help the reader understand the relationship between identities and how different keys will 
be used once encryption is implemented.

The [Nodes identification chapter](#nodes-identification) explains how identities are used within the Network. 
However, the public key pairs associated with these identities are only used to verify whether a Node accurately 
claims its identity. For encryption purposes, symmetric keys are employed, which are not related to the identity keys.

**Symmetric encryption algorithm choice**

The encryption algorithm chosen must meet the following criteria:
1. Independent Message Encryption: The algorithm must not assume an order for messages; each packet must be 
   encrypted independently. Since the entire network traffic between Nodes should be encrypted, and part of the 
   communication occurs over an unreliable channel, the encryption algorithm must be capable of handling individual 
   UDP packets. Some packets may be lost, while others could arrive out of order.
2. No Key Exchange Required: There should be no need for explicit key or information exchange between Nodes. In cases 
   where communication is relayed, there is no Session-establishing phase between parties sending relayed packets, 
   so key exchanges aren't feasible.

For these reasons, the AES-GCM-SIV variant of the AES algorithm was chosen.

**Symmetric keys derivation**

Hybrid Net employs Elliptic Curve Diffie-Hellman (ECDH) to derive symmetric encryption keys. Each Node generates a 
new private/public key pair, separate from its identity keys, which is specifically used for communication. The 
public key is exchanged during the [p2p handshake](#establishing-sessions-between-nodes), and a shared secret is 
derived from this key to enable AES encryption.

In cases of relayed communication, where no direct Session is established, the public key is retrieved from the 
Relay server. The shared secret is then derived using the Node's private key, enabling encrypted communication 
through the Relay. The recipient Node also requests the sender's public key from the Relay server and uses it to 
derive the shared secret, allowing it to decrypt the message. No special control packet exchanges are required 
between the sender and receiver.

#### Central net

### Offer / negotiation
A description of the component responsible for making offers, counter-offers,
negotiations, etc.

### Market interactions
#### Discovery and Offers/Demand matching
- Offer/Demand properties and constraint language
- Yagna is property agnostic - doesn't understand semantic of properties, only agent do
- Some examples of properties and constraints and how it works
- Links to more detailed docs for properties language and properties specification (?)
#### Offer propagation
- Link to design [decision](#only-providers-offers-are-propagated)
- Algorithm overview
#### Process of negotiations and making an agreement
- Initial Proposal
- Countering Proposal
- What can change in counter proposal (protocols based on property language)?
- Provider Agent possible Proposal responses (counter, reject)
- Requestor Agent possible Proposal responses (counter, reject, propose Agreement)
- Provider Agent possible Agreement responses (accept, reject)
- Requestor possibility of Agreement Proposal cancellation
- Restarting negotiations (who can, who can't and how?)

```mermaid
---
title: Simplified negotiations from Provider's perspective
---
sequenceDiagram
  box Provider Node
    actor ProviderAgent as Provider Agent
    participant ProviderYagna as Provider Yagna daemon
  end
  participant GolemNetwork as Golem Network
  box Requestor Node
    participant RequestorYagna as Requestor Yagna
    actor RequestorAgent as Requestor Agent
  end
  
  RequestorAgent->>RequestorAgent: Describe Resoruce Demand
  RequestorAgent->>RequestorYagna: Publish Demand
  RequestorAgent->>RequestorYagna: Subscribe for Proposal events
  activate RequestorYagna
  
  ProviderAgent->>ProviderAgent: Describe Resources
  ProviderAgent->>ProviderYagna: Publish Offer
  ProviderYagna->>GolemNetwork: Offer propagation
  activate GolemNetwork
  ProviderAgent->>ProviderYagna: Subscribe for Proposal events
  activate ProviderYagna
  

  GolemNetwork->>RequestorYagna: Receive Offer
  Note over GolemNetwork,RequestorYagna: Offer wasn't received directly<br/> from Provider Node 
  RequestorYagna->>RequestorYagna: Match Offer with Demand
  RequestorYagna->>RequestorAgent: Generate Proposal

  loop
    RequestorAgent->>RequestorAgent: Adjust Proposal
    RequestorAgent->>RequestorYagna: Counter Proposal
    
    par
      RequestorYagna->>ProviderYagna: Counter Proposal
      ProviderYagna->>ProviderAgent: Receive Proposal
    and Proposals from other Nodes in the network
      GolemNetwork->>ProviderAgent: Receive Proposals    
    end

    ProviderAgent->>ProviderAgent: Select best Proposals to respond
    ProviderAgent->>ProviderAgent: Adjust Proposals
    ProviderAgent->>ProviderYagna: Counter Proposal
    ProviderYagna->>RequestorYagna: Counter Proposal
    RequestorYagna->>RequestorAgent: Receive Proposal
  end

  RequestorAgent->>RequestorYagna: Propose Agreement
  RequestorYagna->>ProviderYagna: Propose Agreement
  ProviderYagna->>ProviderAgent: Receive Agreement Proposal
  ProviderAgent->>ProviderAgent: Select best Agreement Proposal
  ProviderAgent->>ProviderYagna: Approve Agreement
  ProviderYagna->>RequestorYagna: Approve Agreement Proposal
  RequestorYagna->>RequestorAgent: Agreement approval notification

  ProviderAgent->>ProviderYagna: Unsubscribe Proposal events
  deactivate ProviderYagna
  ProviderYagna-->GolemNetwork: Stop Offer propagation
  deactivate GolemNetwork
  deactivate RequestorYagna
```

#### Market strategies

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

##### Mid-Agreement payments
##### Post-Agreement payments

#### Payment drivers
- Abstract concept (independance from underlying payment mechanisms)
- How payment platform relates to payment driver?
- Examples: erc20 driver, zksync (?)
#### Payments batching
#### Deposits payments
- Overview of the concept
- Link to external documentation describing details

### ExeUnits

```mermaid
sequenceDiagram
  participant Requestor
  participant Provider
  Requestor-->Provider: Negotiations
  Requestor->>Provider: Propose Agreement 
  Provider->>Requestor: Approve Agreement
  
  loop Multiple Activities allowed
    Requestor->>Provider: Create Activity
    create participant ExeUnit
    Provider->>ExeUnit: Spawn ExeUnit
    Requestor-->ExeUnit: Commands controlling ExeUnit
    activate ExeUnit
    
    loop Regular intervals
      ExeUnit->>Provider: Report resources consumption
      Provider->>Provider: Calculate costs
      Provider->>Requestor: Send DebitNote
      Requestor->>Provider: Accept DebitNote
    end
    
    Requestor-->ExeUnit: Finish computations
    deactivate ExeUnit
    Requestor->>Provider: Destroy Activity
    destroy ExeUnit
    Provider->>ExeUnit: Terminate ExeUnit
  end
  Requestor->>Provider: Terminate Agreement
```

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
##### Usage counters
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

