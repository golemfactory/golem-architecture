# Golem Timeline-related Properties 
Namespace defining time-related contract aspects of a Golem service. This namespace shall include properties and concepts similar to eg. futures and forwards. Imagine:
  - Demand for resource to be available starting from time `T1`. 
  - Demand for resource (eg. storage) to be available for at least `t` days. 
  - Describe a service which takes eg. `h` hours to be available after request (eg. similar to AWS Glacier where cheap archiving is available but a restore will be returned after 3 days).

QUESTION A):
- Do we need to address scenarios where resource parameters may change over time. 
  - Is it feasible to try and regulate/express "temporal properties", ie props specified for given, explicit periods of time? For example: property x=10 until 2019-05-30, and x=15 after 2019-05-30???
- 
- How do we address scenarios where we have "ongoing resources" (eg. storage), but the Agreements need to change terms (eg. periodically, or after some condition is met?)
   - So "term Agreements" - ie. Agreement is valid until specific date. After this date the Provider is entitled to raise Invoice and shutdown the activity, unless Agreement is extended. Agreement extension may either follow pre-agreed terms, or force renegotiation (how do we handle that? Is this in scope of Market API? Probably yes, but that would mean negotiation phase is triggered based on given Agreement, and without Matching phase - q1) is this an amendment to Market aPI scenarios???)
   - Agreement NOTICE PERIOD - property to express the notice period before the Prvider terminates the Agreement. Should this be mutual - ie should the Requestor also be obliged by a notice period?
   - 
 - "Conditional Agreements" - ie. Agreement is ongoing and valid until a specific condition is met (condition expressed via "pseudoLDAP query"? ) Once condition is met, the Requestor may receive another "Offer" meeting their previous, original Demand, which if accepted would override the previous Agreement. 

 - ...the above introduces the cncept of Parent-Child agreements, ie. Agreement which is an extension of a pre-existing agreement (like Mobile Phone Plan extension under different conditions), usually with different pricing? or same pricing but different resource properties?


QUESTION: How do we express (via props/standardized behaviors) scenarios where:
- Requestor expects the Provider to be "accessible" (not necessarily while executing code/payload). This means that Provider is "contactable" and is able to execute ExeScript commands. It is possible to implement eg. a heartbeat via exescript...
- Imagine a case where Golem network nodes are used to establish a "self-governing" network
1) In generic container model - we have a bootstrap "Reuqestor app" which launches n container instances on n Providers, and deploys a copy of itself. The communication/control over the child nodes is exerted via simple ExeScript commands, which re issued by contrlling app. MOreover, each of the children can itself be a "master" node, and propagate itself further to subsequent Providers. 
   In Golem sense - such solution is inly expressed via basic properties (containerized generic apps).
2) In a "service model" - bootstrap Requestor app deploys agents, which launch services, eg. with RESTful APIs? The services implement a proprietary protocol to control the running swarm, query its state, topology, etc. 
   In Golem sense - this solution is perceived as an online service... (do we have any standardized properites for online, eg web services?)
3)  In a "swarm model" - where property standards explicitly define "swarm resource", ie. have dedicated props to specify the swarm topology, cardinality, protocol???

- In each of the said cases, the controlled payments are challenging. Each "master" node is a Requestor and must be able to pay for resources to its Providers. This means:
- either the Root node's portfolio private key needs to be propagated, so that each of the subordinate "masters" can pay from one portfolio
- a sub-portfolio is dynamically created with soe GNT allowance so that each subrdinate "master" uses its own portfolio
- create a smart-contract which deploys payments as requested by each of the "master" nodes in the "swarm"

