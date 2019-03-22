# Pricing Estimation
This namespace defines mechanism for **price estimation** for Offers published in Golem network. 

## `golem.com.pricing.est{*} : Number`
Pseudo-function property allowing to query the Provider for price calculation for a given estimated usage vector value (amount is indicated in GNT).

**Important:** The pricing models described by this `golem.com.pricing` namespace must be expressed by a 'pricing function', and the types of pricing functions are standardized, it still may be useful for the Requestor to query the Provider for their accurate calculation. This may be aplicable for cases where:
* a Demand includes a filter constraint referring to the price estimation for estimated usage which expects the price to be within specific range,
* a Requestor wants to validate their calculation of the estimated price (derived from the standardized pricing model) with the calculation made by the Provider (to ensure both have identical "understanding" of the pricing model).

This property defined an "estimation query" protocol, whereby:
* An Offer which includes the property declaration indicates that the Provider supports the protocol.
* A Demand published on the Golem market may include constraints referring to the price estimation 'pseudo-function'. As a result, the estimation is either:
  * calculated via the "dynamic property resolution" protocol, so that it gets resolved during the "Discovery" phase of Market API flow, or
  * the constraint referring to the estimation pseudo-function is processed by Provider in the "Negotiation" phase, so that the responding counter-Offer is enhanced with specific estimated value, calculated for the usage vector received in the Demand constraints.

### **Examples**
* `golem.com.pricing.est{*}` - in an Offer - declares that the Provider supports the price estimation.
* `golem.com.pricing.est{[0.3]}<120` - for a 1-counter usage vector (eg. `golem.usage.vector=["golem.usage.duration_sec"]`) this filter constraint expects the price estimation for given estimated usage to be less than 120 GNT.

