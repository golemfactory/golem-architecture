Golem Mission Statement
========================

# What is Golem

Golem is a decentralized platform for facilitating renting of computational
resources to clients, called **requestors**. Likewise, it can also be described
as a platform for **providers** to offer and rent their computational resources
to requestors. The resources are primarily a CPU or GPU time, but in the future
might also include e.g. storage.

    TODO: I'm not entirely convinced if we should not focus on compute nodes
    renting only? -- JR

To incentives providers, Golem offers an infrastructure for implementing market
economies.

Golem strives to remain as generic as reasonably possible. This means that it
does not try to impose any particular policies, payment or reputation schemes.

Golem does, however, provide a number of building blocks, which allows **service
providers** to build tailored solutions, by plugging in modules realizing
particular economies, policies, or reputation systems.

Golem also provides a number of mechanisms (primarily in a form of pluggable
modules) to secure the client data and payloads, as well as the infrastructure.
These include, but are not limited to, sandboxed execution environments to
protect both the hosts from the payloads, as well as the payloads from the
hosts.

# How Golem is used

Each requestor defines a **job** (e.g. an executable for a service to be
deployed) and a set of minimal **properties**, which should be satisfied by
the resources managed by the selected provider **nodes** allocated for the job.
Golem network takes care of selecting providers matching the requirements and
deploying the job on these nodes.

The job might be a batch-processing (i.e. non-interactive) program, or an
interactive network-aware service. Golem does not also make any assumptions with
regards whether the job will be stateless or stateful.

# Level of service and other requirements

The requirements for nodes are generally job-specific. They may take the form of:

 - some objectively quantifiable minimal level of service (e.g.
   **availibility** or **performance**),

 - certain objective properties (e.g. the underlying architecture of the node
   being "Intel x86"),

 - some form of non-objective, non-quantifiable properties, such as guarantees
   of **integrity** (i.e. that the job's instructions are executed verbatim) and
   **confidentiality** (i.e. that the owner of the platform has not learned any
   internal secrets of the job). These, in particular, depend on the
   requestor-specific threat model.

Golem strives to stay as generic and application/service-agnostic as possible
and thus does not attempt to enforce any particular scheme for defining
requirements or level of service.

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

The actual implementation of how should the policy be expressed formally, as
well as who should evaluate it and build the list of matching nodes is not part
of this high-level document.

# What functionality does Golem provide

Once the requestor has defined the policy and the job(s), the Golem network
takes care of selecting the suitable node(s) and then sending and starting the
job(s) on the node(s).

The Golem network does _not_ attempt to monitor or verify the correctness of the
job execution. This is expected to be implemented by the actual
application/service logic as different applications/services require different
forms of verification. In this respect Golem is analogical to the UDP protocol,
rather than to the TCP.

# Flexible reputation/scoring system

Golem does provide, however, an easy way for service providers to build
reputation/scoring systems of the nodes, based on variety criteria. These
reputation systems are bound to the specific service provider (specific
requestor nodes).

This is to ensure that a scheme used by one service provider does not _directly_
interfere with reputation systems used by other providers. This does not
preclude other providers from utilizing reputation systems created by others,
but they would need to explicitly include such other systems (or some measures
created by these other systems) into their own scheme. Some providers might even
specialize in exposing reputation systems to others, perhaps for a fee.

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
