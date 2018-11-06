# Negotiation phase


TODO:
 - pricing function


## Different Negotiation scenarios

 1. Tender model
 2. Quasi-centralized nodes
 3. Smart contract


## Describing desired level of service

I order to achieve the above we need ability for requestors and providers to
define various properties and requirements.

The requirements for nodes are generally application- or service-specific. They
may take the form of:

 - some objectively quantifiable minimal level of service (e.g.
   **availability** or **performance**),

 - certain objective properties (e.g. the underlying architecture of the node
   being "Intel x86"),

 - some form of non-objective, non-quantifiable properties, such as guarantees
   of **integrity** (i.e. that the job's instructions are executed verbatim) and
   **confidentiality** (i.e. that the owner of the platform has not learned any
   internal secrets of the job). These, in particular, depend on the
   requestor-specific threat model.

For illustration, one can easily imagine differently defined requirements by
various **service providers** (i.e. integrators operating requestor nodes who
wish to offer apps and services running on Golem to a wider audience):

 - "Select only x86-based nodes which are capable to run jobs within SGX v2
   enclaves, and which are _not_ based in the US",

 - "Select only nodes whose Golem addresses (IP address) fall into the specified
   whitelists" (perhaps the service provider has established trust into these
   nodes using some out-of-protocol method),

 - "Make sure that at least 75% of the nodes selected have a score no less than
   3 Happy Mice (using the Kittycat Reputation System), while the other 25% are
   based in the jurisdiction of San Escobar".

## Mutual

TODO: provider requirements

## Verification schemes

Golem does not attempt to enforce any particular form of monitoring or
verification of how well does the provider nodes execute the activities. The
payloads which are sent out by the requestor nodes and then executed by the
provider nodes, are in general opaque for the Golem network.

Likewise, Golem has no technical means to enforce or verify the actual
fulfillment of the requirements expressed within demand and offer records by the
actual provider and requestor nodes, except for a few basic properties, which
could be evaluated by the Golem network.

TODO: Note on payload transparency: opaque to Golem, but inspectable to
provider. Might use 2-stage loader to protect code from inspection, like SGX.

As mentioned earlier, Golem does _not_ attempt to monitor or verify the
correctness of the job execution.

## Flexible reputation/scoring system

Golem does provide, however, an easy way for service providers to build
reputation/scoring systems of the nodes, based on variety criteria.

These reputation systems are bound to the specific service provider. This is to
ensure that a scheme used by one service provider does not _directly_ interfere
with reputation systems used by other providers. This does not preclude other
providers from utilizing reputation systems created by others, but they would
need to explicitly include such other systems (or some measures created by these
other systems) into their own scheme. Some providers might even specialize in
exposing reputation systems to others, perhaps for a fee.

## Exemplary scenarios

For example, a 3D scene rendering service implemented on top of Golem might use
a user's subjective input of how good (or bad) the returned image or movie was
and rate nodes according to this criteria.

In a different scenario a service provider might only request nodes which are
SGX capable. The application (job) could then use Golem SDK-exposed
functionality to prepare the input data in such a way that indeed only
SGX-capable nodes will be able to decrypt the input and perform any meaningful
computation. In this case verification of the returned data is trivial and a
reputation system is not required. The service provider might, however, still
want to rank providers according to how fast they completed the jobs.

In another example, a web service job might be randomly pinged by other jobs
(perhaps offered by another provider, whose business model is to build
monitoring-based reputation system) to measure availability, latency, and
perhaps even integrity of their execution.

Each of these service providers would build and use a different reputation
system.
