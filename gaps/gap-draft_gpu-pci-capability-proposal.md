---
gap: <to be assigned>
title: GPU/PCI capability proposal
description: Grammar Specifications for elements of GPU/PCI specification
author: Stanislaw Krotki
status: Draft
type: <Experimental>
---


## Abstract
GPU/PCI capability information in the Offer

## Motivation
This GAP introduces GPU (and possibly other PCI) devices capability description. Providers will specify this information so that Requestors would be able to specify certain processing requirements in Demands.

## Specification

```
properties: {
    golem: {
        inf: {
            gpu: {
                model: "string",
                video-mem: {
                    dedicated.gib: f64,
                    total.gib: f64
                },
                cuda: {
                    enabled: bool, /* this is related to the image running and not the VM runtime */
                    cores: int, /* This can be found from the general specification of each GPU. Otherwise it is platform specific that we would not like to work with. We should aim to focus on platform independent metrics that are easy to access in a health check. */ 
                    version-compatibility: int, /* this is not known by the GPU because there is a minimum CUDA version and also it can be deprecated in a future CUDA version. It can also be different for CUDA and a given library, for example tensorflow had a different GPU compatibility requirement for building from source and using prebuild binaries. Get the info from Nvidia and/or a general guideline from StackOverflow */
                },
                clock: {
                    base: int,
                    boost: int
                },
                memory: {
                    clock: int,
                    type: "string",
                    bus: "string",
                    bandwidth: "string" / int 
                },
                tpu: {
                    enabled: bool,
                    gen: bool,
                    cores: int
                }                
            }
        }
    }
}
```

## Rationale
This specification is based on the wish-list provided in [#157](https://github.com/golemfactory/ya-runtime-vm/issues/157). \
For now the assumption is that we only detect the first pci (possibly nvidia) gpu, tpu is only supported as a tpu-enabled gpu device. 

## Backwards Compatibility
If `gpu` node is not present in the offer then it means the Provider does not have it or does not allow using it. \
If `gpu` node is present but the Requestor does not demand it then it should be skipped in the agreement.

## Test Cases
TBD

## [Optional] Reference Implementation
TBD

## Security Considerations
TBD 

## Copyright
Copyright and related rights waived via [CC0](https://creativecommons.org/publicdomain/zero/1.0/).