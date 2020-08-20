# Golem Computation Resource Standards

This repository defines a framework for the specification of Standards for Computing Resources available on Golem ecosystem. It is supplementary to Golem Demand & Offer Specification Language and is meant to prescriptively implement structure in a broad space of conceivable services.

## Computing Resource Metaspace Decomposition
The computation resources offered for trade on Golem marketplaces are described by a generic meta-language of Demands & Offers. The specification language is designed to be broad and open, however by itself it does not specify the possible space of resources/services. It comprises the grammar, rather than semantics.
We acknowledge the fact that computation resources can be defined via a multitude of aspects, which we call “dimensions”. A single distinguishable service requires a combination of different specifications describing different aspects of this service’s existence on Golem marketplaces. Each of these dimensions can benefit from standardisation, and the dimension standards can be perceived as largely independent from each other. Therefore we consider this multidimensional standard space as an open and powerful framework.

## Resource Aspects - Dimensions -> Namespaces
As mentioned above, a “dimension” defines a single aspect of resource’s/service’s definition. A dimension standard defines rules which must be followed by elements of resource’s Offer so that it can be considered a standardised Offer. A dimension standard may specify, among others:
* Set of mandatory and optional properties.
* Combinations of specific property values (eg. standard “grades” of VMs for rent)
* Semantics of properties which are specific to a particular standard.
* Schemes of computation resource’s consumption.
* Charging models:
  * Usage vector components (counters),
  * specific formats of pricing functions,
  * schemes of payments.
The properties applicable to a distinguished dimension of resource description are organized in __namespaces__.

## Namespace categories

The Golem Standards repository consists of following sections/categories:

* [0-commons](0-commons) - A collection of namespaces which specify generic aspects of Golem ecosystem. These namespaces are designed to be building blocks to which other, more area-specific property namespaces may refer.
* [1-node](1-node) - A collection of namespaces containing properties which describe a Golem node - various aspects of Golem Requestor/Provider.
* [2-service](2-service) - A collection of namespaces containing properties which describe a Golem service or resource. A hierarchy of different **categories of services/resources** is defined.
* [3-commercial](3-commercial) - A collection of namespaces containing properties which describe commercial aspects of Golem ecosystem. All properties referring to **pricing**, **payments** or **licenses** are defined here.

## Generic conventions

### "Fact" vs "Negotiable" properties (DRAFT)

A property indicates a specific aspect of its issuer (Requestor or Provider) or an Agreement which is being negotiated between the parties. Properties can be generally split into following classes:
- a **`Fact`** is a property which indicates an objective fact, an attribute of the issuer, a trait which cannot be adjusted or negotiated. Examples of facts can be: payment address, node id, etc.
- a **`Negotiable`** property is a property which must be agreed by **both sides** of the Agreement, and can be adjusted as a result of a negotiation. Examples of negotiable properties are: payment platform, pricing terms, etc.

**`Facts`** and **`Negotiables`** require respective representations in Demands, Offers and resulting agreements:
- **`Facts`** are indicated only by their issuers, which means they are specified in respective Demand/Offer.
- **`Negotiables`** can be first indicated by one side, but a confirmation of the property value is required from the alternate side. 
  - This implies, that the "negotiable" properties need to be eventually **repeated in both Demand and Offer** for them to bind into a valid Agreement.
  - Moreover, the Demand-Offer exchange may form a 'dialogue' between parties, which declare their *preferred* values of properties, while the other side may either *agree* by repeating the property and its value, or propose their preferred value, etc.

**`Facts`** and **`negotiable`** properties are marked as such in respective standard documents.

#### Why are `Negotiables` required?

There are a couple of reasons for the `Negotiable` property convention:
- To provide a clear way to negotiate terms of the Agreement via properties only (rather than constraints). While it may be intuitive to express the "negotiation intents" by encoding them in constraint expression - processing the intent by the receiver would be non-trivial (with the constraint expressions may be complex and in fact internally contradicting). Indicating the intent by declaring property - is simpler to process.
- To establish a 'cross-check' mechanism to indicate the parties conform with compatible property sets. The reasoning is as follows: a party which includes `Negotiable` properties in their Proposal expects the other side to understand the semantics of those properties. Yet, the market matching mechanism does not include (intentionally) the validation of whether properties of a Demand are properly 'understood' by the Provider (and vice versa). Therefore the responder is expected to explicitly 'confirm' they are familiar with a received property - bo repeating it in their counter-Proposal. 
  - This 'confirmation' mechanism is designed to 'protect' the Providers, as their Offers are likely to include numerous 'terms of business' properties which dtermine the commercial terms of the Agreement. The Provider would like to ensure that the Requestor side is fully accepting those terms, and will properly fulfill them. As the Requestor repeats the Negotiable property values, the Provider assumes the terms of business are accepted and will be followed.
  - Note that if the issuer of a `Negotiable` property does not receive the 'confirmation' in the response - it may still choose to accept that proposal, yet there may be risk involved (eg. the other side may run an older version of agent software which does not support a particular property).

## Standard properties - Cheat sheet
[Cheat sheet](cheat_sheet.md)
