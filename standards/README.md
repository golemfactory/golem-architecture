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

* [0-commons](0-commons) - A collection of namespaces which specify generic aspects of Golem ecosystem. These namespaces are designed to be building blocks to which other, more area-specifc property namespaces may refer.
* [1-node](1-node) - A collection of namespaces containing properties which describe a Golem node - various aspects of Golem Requestor/Provider.
* [2-service](2-service) - A collection of namespaces containing properties which describe a Golem service or resource. A hierarchy of different **categories of services/resources** is defined.
* [3-commercial](3-commercial) - A collection of namespaces containing properties which describe commercial aspects of Golem ecosystem. All properties referring to **pricing**, **payments** or **licenses** are defined here.

## Standard properties - Cheat sheet
[TODO]