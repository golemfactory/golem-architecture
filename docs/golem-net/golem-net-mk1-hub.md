# GolemNet Mk1 (Net API)

## Description

GolemNet is a module that is responsible for network communication and discovery between Lightweight Golem nodes.
The first implementation called GolemNet Mk1 uses a centralized server to allow communication between nodes.

## Architecture

GolemNet module receives messages from two sources:
- Golem Service Bus (GSB)
- Other nodes in the network

After receiving a message, GolemNet module may put it on the Golem Service Bus if it is addressed to the current node, 
send it to the centralized server which forwards it to the GolemNet module of the destination node (GolemNet Mk1) 
or use P2P network to send it to the destination node (GolemNet Mk2).

## Message Format

### Message Components

Every message contains following parts:

| Message Part Name | Example |
|--|--|
| [Destination](#message-destination) | net/0x123/market-api/get-offers |
| [Payload](#payload) | { "max-offers": 50 } |
| [Reply To](#reply-to) | 0x789 |
| [Request ID](#request-id) | 1574244629 |
| [Message Type](#message-type) | REQUEST |

### Message Destination

#### Prefix

Messages addressed to GolemNet module must start with `net/`.

#### Address

The messages could be sent to a given node address or to a broadcast address.

##### Node Address

A Lightweight Golem node address, e.g. `0x123...`.
A message with an address like this should be delivered to the given node.
In the GolemNet Mk1 implementation it is sent to the centralized server, which sends it directly to the destination node.
The GolemMk2 implementation should allow P2P messages without a centralized server; sometimes the messages will need to
travel between many nodes before reaching the destination node.

##### Broadcast Address

When a messages is sent to a broadcast address, it can be delivered to more than one node in the network.
The broadcast address is a string `broadcast` (which means all nodes in the network)
optionally followed by a `:N` (which means all nodes that are not further than N hops
from the originating node - this can be used to send broadcast messages only to a part of the network).

#### Destination Module and Function

The next part of the message should be the destination module name followed by the method name, 
e.g. `market-api/get-offers`.

#### Example Destinations

| Destination | Description |
|--|--|
| net/0x123/market-api/get-offers | Get offers from the Market API module on node 0x123. |
| net/broadcast:5/payment/get-payment-method | Get payment methods from nodes that are not further than N hops
from the originating node. |

### Payload

The payload depends on the destination module and method. It could contain method parameters encoded in JSON
(for example: `[ "method-name": "param" ]`) or binary data. The GolemNet module does not check payload content format.

### Reply To

Specifies node address that send this message. It is automatically added by GolemNet module so that the reply
could be sent to the originating node.

### Request ID

Request ID is necessary to pair a request with response. It should be a unguessable random number.

### Message Type

| Message Type | Description |
|--|--|
| Request | Remote method call |
| Response | Reply to a remote method call |
| Error | Error message |

## Message Handling

### Requests

When GolemNet receives a network message prefixed with `net/NODE_ID/`, where NODE_ID is the current node identifier, 
the message is put on the Golem Service Bus without the `net/NODE_ID/` prefix, so that modules subscribed to this type
of message receive it.

If the message is prefixed with `net/NODE_ID/`, where NODE_ID is different from the current node identifier,
the message (in GolemNet Mk1 version) is forwarded to the centralized server (hub) which relays it to the destination node.

### Responses and Errors

TODO
