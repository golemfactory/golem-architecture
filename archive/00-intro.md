# Introduction to Golem

## What problem(s) does Golem want to solve

TODO: rewrite with proper narration

- Cloud market centralization (why important?)

- Wasted power of idling computers (why important?)

- Untrustworthy cloud (app providers and infrastructure owners controlling our
data)

- Censorship (denial of service for select users, regions or apps/services)

## What is Golem

Golem is a decentralized platform facilitating renting of computational
resources.

The resources are offered by __provider__ nodes and consumed by __requestor__
nodes. A __node__ is an elementary building block of the Golem network. Each
node has a unique Golem ID (address). The actual consumers of the applications
and services deployed on the Golem network do _not_ need to be the same as the
__requestor__ nodes, i.e. they don't need to be part of the Golem network.

To incentivize providers to share their computing resources, Golem offers an
infrastructure for implementing __market economies__.

Golem strives to remain as generic as reasonably possible. This means that it
does not try to impose any particular policies, payment, reputation or
monitoring and verification schemes or protocols. Instead, Golem provides
building blocks, such as various backends, SDKs, and sample code, to facilitate
creation of such entities by independent 3rd parties.

Golem also provides a number of mechanisms (primarily in a form of pluggable
modules or backends) to secure client data and code during execution. These
include, but are not limited to, trustworthy execution environments to protect
both the hosts from the payloads, as well as the payloads from the hosts.

## How Golem is used

Each requestor sends out a __demand order__, which defines the __payload__ (e.g.
the executable for a service to be deployed), a set of minimal requirements for
the activity (e.g. the type of hardware architecture), as well as other optional
information or constrains, such as e.g. a bundled payment.

Similarly, each provider publishes __offers__ for the computing resources it
manages, which include description of properties/capabilities of the resources,
possible restrictions of use (e.g. no ability to make external network
connections) and other information, which might e.g. define the accepted payment
schemes and minimal price for the service or perhaps white- or black-lists of
the requestors it is willing to serve.

The payload might be a batch-processing (i.e. non-interactive) program, or an
interactive network-aware service. Golem does not also make any assumptions
about the deployed activities, which might be completely opaque to the Golem
infrastructure. Golem does provide, however, reasonable amount of building
blocks, primarily in a form of various SDKs, to allow easier integration of
applications within the decentralized networking environment.

Golem network __takes care of finding the matching requestors to providers__,
facilitating deployment of the activity on a suitable provider node.

The node which accepted the __demand__ order generates a digitally signed
statement, called __contract note__ which testifies that the provider has
accepted the  and makes a commitment for its execution.

The Golem network does _not_ attempt to monitor or verify the correctness of the
execution. This is expected to be implemented by the actual application/service
provider as different applications/services require different forms of
verification. In this respect Golem is analogical to the UDP protocol, rather
than to the TCP.

Golem does, however, provide an infrastructure for 3rd-party service providers
to easily integrate such verification/monitoring services with the Golem
ecosystem, e.g. with other providers handling reputation systems and with
particular economies.

## Exemplary scenarios

TODO: rewrite with proper narration

 - Standalone service providers (Brass Blender)

 - Service Providers as requestors offering services to 3rd part clients

 - Datacenters as providers offering computation power ("Unlimited" case)

 - Service Providers as reputation/quality of service providers

## The trap of over generality vs. app-specificness

TODO: rewrite with proper narration

 - Over generality is already solved -- we have computers and networks, we can
    build everything!

 - Too much app-specificness hampers architecture decisions, which then
    unnecessarily limits future applications :/

 - Finding the right spot is an art, and largely falls under the "Beauty"
    optimization, rather than "Truth" seeking ;)
