# Centralised (Mk1) Yagna Market

First incarnation (Mk1) of Yagna Market component will be centralized.
Single Server will handle all the traffic on the market.

It will serve REST API conforming with Cabability Level 1 of the 
[Market API specification](
https://docs.google.com/document/d/1Zny_vfgWV-hcsKS7P-Kdr3Fb0dwfl-6T_cYKVQ9mkNg/edit#heading=h.8anq3nlk2en7
) which means support for the three basic phases of the market interaction: [Discovery](#discovery-phase),
[Negotiation](#negotiation-phase) and [Agreement](#agreement-phase).

Actual Centralized (Mk1) Market Server implementation is described here:

https://github.com/golemfactory/yagna/tree/master/test-utils/market-hub

## Yagna Market

The Yagna Market is a core component of the Yagna Network, which enables computational Offers
and Demands circulation. The Market open for all entities willing to buy computations (Demands)
or monetize computational resources (Offers).

## Yagna Market API

The Yagna Market API is the entry to the Yagna Market through which Requestors and Providers (parties) 
can publish their Demands and Offers respectively, find matching counterparty, conduct negotiations
and make an agreement.

Each of the two roles: Requestors and Providers have their own interface in the Market API.

## Market Interaction

Market interaction is divided into tree phases described below.

In target solution (Mk2) the Negotiation and Agreement phases will involve direct one-to-one
communication, but for Mk1 the central server will be an intermediary here.


### Discovery Phase
Parties are joining the Yagna Network by publishing their Offers or Demands.
Yagna Market is matching incoming Demands and Offers, and creates Proposals, wchich are then
fed to both parties: Requestor and Provider.

### Negotiation Phase
Upon Proposal reception party (usually the Requestor) can start interaction with selected
counterparty to negotiate the Proposal. During the negotiation parties are alternately
exchanging Proposals adjusting their properties or/and constraints to fully fit Offer with Demand.

For the Mk1 Market implementation will **not** support [dynamic property resolution nor
pseudo-function support](
https://docs.google.com/document/d/1Zny_vfgWV-hcsKS7P-Kdr3Fb0dwfl-6T_cYKVQ9mkNg/edit#heading=h.6y5qk7bcl9qy
) during the Negotiation phase.

### Agreement Phase
After successfull negotiation Requestor receives Proposal which satisfies all his constrains
and is able to promote this Proposal into Agreement. The Agreement is send to Provider
to be finally accepted.

Provider accept finishes the Market interaction for both parties and enables Requestor to start an Activity.

