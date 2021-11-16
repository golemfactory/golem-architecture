---
gap: 4
title: Computation Manifest
description:
author: mf (@mfranciszkiewicz)
status: Draft
type: Feature
---

## Abstract

This GAP introduces a manifest file specification and a set of GCRS (Golem
Computation Resource Standards) properties (herein after referred to as
"Computation Manifest"), which allow Requestors to declare the constraints of
computations they may execute on a Provider's machine.

## Motivation

The need for a Computation Manifest originates from the upcoming Internet
access feature, where each outbound request is routed via Provider's host
operating system. In order to prevent unwanted / illegal behavior on Provider's
behalf, they need to remain in control of actions that Requestors can execute
on their machine.

With Computation Manifests, Requestors constrain themselves to
a certain set of allowed actions, to be negotiated with and approved by a
Provider. Requestors' actions will be verified against the Manifest during
computation.

## Specification

Computation Manifest can take one of the following forms:

- an embedded JSON / YAML object within an external configuration file,
- `golem.srv.comp.manifest` namespace properties, an extension to
  Golem Computation Resource Standards.

The Computation Manifest is expected to be included in a Payload
Manifest ([GAP-5](https://github.com/golemfactory/golem-architecture/pull/28))
embedded or linked in an Agreement, where its hash is co-signed with Payload 
hashes. If a Computation Manifest is defined in a Demand (and thus, 
Agreement) via GCRS properties, its role is to override those properties in 
the original Manifest.

The process of signing and verifying a Computation Manifest remains out of 
scope of this GAP. Computation Manifests are meant to be signed externally, 
i.e. either as a part of an Agreement or a Computation Payload Manifest.

### Development cycle

Computation Manifest is an optional component of a Payload Manifest, 
created by an application developer. Payload Manifests may be signed by any 
willing entity, who is free to establish and govern their own signing 
process; i.e., that entity may choose to sign any submitted manifest or 
involve extra preliminary steps, e.g.:

- manifest correctness / semantic validation
- opinionated security checks
- developer identity verification
- charging fees

Initially, Golem Factory may enact as a trusted authority by
verifying and signing application manifests created by chosen developers.
This process is out of scope of this GAP.

Computation Manifests can optionally be specified or extended by Requestors
in their Demands. CMs defined solely via properties will lack the signature of
a trusted authority, unless the Requestor is considered as one and the 
Agreement is signed with his trusted key counterpart.

Positive verification of a Payload Manifest signature requires Providers to 
add the public key of an entity to their trusted key storage. The storage is 
initialized with a default set of trusted keys, including the one of 
Golem Factory.

### GCRS `golem.srv.comp.manifest` namespace

Intended for overriding an external Manifest. Allows defining new Manifests
from the ground up using properties only.

1. `golem.srv.comp.manifest.version : String`

    Specifies a version (Semantic Versioning 2.0 specification) of the 
    manifest, **defaults** to "0.1.0"

2. `golem.srv.comp.manifest.script` (sub-namespace)

    Defines a set of allowed ExeScript commands and applies constraints to
    their arguments.


3. `golem.srv.comp.manifest.script.commands : List[String]`

    Specifies a curated list of commands in form of:

    - UTF-8 encoded strings

      No command context or matching mode need to be specified.

      E.g. `["run /bin/cat /etc/motd", "run /bin/date -R"]`

    - UTF-8 encoded JSON strings

      Command context (e.g. `env`) or argument matching mode need to be
      specified for a command.

      E.g. `["{\"run\": { \"args\": \"/bin/date -R\", \"env\": { \"MYVAR\": \"42\" }, \"match\": \"strict\" }}"]`

    - mix of both

    `"deploy"`, `"start"` and `"terminate'` commands are always allowed.
    These values become the **default** if no `manifest.script.command` 
    property has been set in the Demand, but the `manifest` namespace is 
    present.

4. `golem.srv.comp.manifest.script.match : String`

    Selects a default way of comparing command arguments stated in the 
    manifest and the ones received in the ExeScript, unless stated 
    otherwise in a command JSON object.

    The `match` property could be one of:

      - `"strict"`: byte-to-byte argument equality (**default**)
      - `"regex"`: treat arguments as regular expressions

        Syntax: Perl-compatible regular expressions (UTF-8 Unicode mode),
        w/o the support for look around and backreferences (among others);
        for more information read the documentation of the Rust
        [regex](https://docs.rs/regex/latest/regex/) crate.

5. `golem.srv.comp.manifest.net` (sub-namespace)

    Applies constraints to networking. Currently, outgoing requests to the
    public Internet network are covered.

6. `golem.srv.comp.manifest.net.inet.out.protocols : List[String]`

    List of allowed outbound protocols. Currently **fixed at** `["http", "https"]`.

7. `golem.srv.comp.manifest.net.inet.out.urls : List[String]`

    List of allowed external URLs that outbound requests can be sent to.
    E.g. `["http://golemfactory.s3.amazonaws.com/file1", "http://golemfactory.s3.amazonaws.com/file2"]`

#### Example

```json
{
  "golem.srv.comp.manifest.script.match": "regex",
  "golem.srv.comp.manifest.script.commands": [
    "run /bin/cat /etc/motd",
    "{\"run\": { \"args\": \"/bin/date -R\", \"env\": { \"MYVAR\": \"42\", \"match\": \"strict\" }}}"
  ],
  "golem.srv.comp.manifest.net.inet.out.protocols": [
    "http",
    "https"
  ],
  "golem.srv.comp.manifest.net.inet.out.urls": [
    "http://golemfactory.s3.amazonaws.com/file1",
    "http://golemfactory.s3.amazonaws.com/file2"
  ]
}
```

### Object representation

#### Example

1. JSON

    ```json
    {
      "script": {
        "match": "regex",
        "commands": [
          "run /bin/cat /etc/motd",
          {
            "run": {
              "args": "/bin/date -R",
              "env": {
                "MYVAR": "42"
              },
              "match": "strict"
            }
          }
        ]
      },
      "net": {
        "inet": {
          "out": {
            "protocols": [
              "http",
              "https"
            ],
            "urls": [
              "http://golemfactory.s3.amazonaws.com/file1",
              "http://golemfactory.s3.amazonaws.com/file2"
            ]
          }
        }
      }
    }
    ```

2. Imploded JSON (optional)

    ```json
    {
      "script.match": "regex",
      "script.commands": [
        "run /bin/cat /etc/motd",
        {
          "run": {
            "args": "/bin/date -R",
            "env": {
              "MYVAR": "42"
            },
            "match": "strict"
          }
        }
      ],
      "net.inet.out.protocols": [
        "http",
        "https"
      ],
      "net.inet.out.urls": [
        "http://golemfactory.s3.amazonaws.com/file1",
        "http://golemfactory.s3.amazonaws.com/file2"
      ]
    }
    ```

3. YAML

    ```yaml
    ---
    script:
      match: regex
      commands:
      - run /bin/cat /etc/motd
      - run:
          args: "/bin/date -R"
          env:
            MYVAR: '42'
          match: strict
    net:
      inet:
        out:
          protocols:
          - http
          - https
          urls:
          - http://golemfactory.s3.amazonaws.com/file1
          - http://golemfactory.s3.amazonaws.com/file2
    ```


## Rationale

### External Manifest sourcing

The main purpose of external sourcing is to provide a trusted set of actions
approved by the signing authority (e.g. Golem Factory). The prevalent case of
sourcing would be to download the Manifest included within a Computation
Payload. This way, in most common cases, Requestors won't need to provide a
custom Computation Manifest.

### Usefulness of the new property set

Because some Providers may be equipped with a custom set of filters, they may
e.g. whitelist a broader range of domain names. This way, the Manifests crafted
by Requestors or the ones overriding a trusted Manifest via properties may
still be accepted by Providers.

### Lack of signatures

The manifest is meant to be co-signed with the payload by a trusted authority
(i.e. bundled) or signed by a Requestor as a part of the Agreement.

## Backwards Compatibility

Certain Runtime capabilities (e.g. Internet access) will require presence of
the Computation Manifest. Requestors expecting the new capability will need to
include a Computation Manifest in the Demand, otherwise it may be discarded by
Providers (unless it's included in the Computation Payload).

Computation Manifests are not required by the legacy Runtime capabilities and
negotiations will commence as prior to introducing this feature.

## Test Cases

N/A

## [Optional] Reference Implementation

N/A

## Security Considerations

- it must be possible to disable the restrictions via configuration option(s)
  within the Provider Agent / ExeUnit; the user should be frequently reminded
  of the current restriction state,
- regex argument matching mode may pose difficulties to some users and mislead
  them into trusting a malicious Manifest author,
- the Manifest is a subject to evolve and does not cover all dimensions of
  computation constraints.

## Copyright
Copyright and related rights waived via
[CC0](https://creativecommons.org/publicdomain/zero/1.0/).
