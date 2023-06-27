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

* golem: {
    * !exp: {
        * gap-pci: { 
            * v1: {
                * inf: {
                    * [gpu](#inf-gpu): {
                        * model: string,
                        * [video-mem](#inf-gpu-video-mem): {
                            * dedicated.gib: number,
                            * total.gib: number
                        * },
                        * [cuda](#inf-gpu-cuda): {
                            * enabled: boolean,
                            * cores: integer,  
                            * version-compatibility: integer, 
                        * },
                        * [clock](#inf-gpu-clock): {
                            * base.mhz: integer,
                            * boost.mhz: integer
                        * },
                        * [memory](#inf-gpu-memory): {
                            * clock.mhz: integer,
                            * type: string,
                            * bus: integer,
                            * bandwidth: integer 
                        * },
                        * [tpu](#inf-gpu-tpu): {
                            * enabled: bool,
                            * gen: string,
                            * cores: int
                        * }
                    * }
                * }
            * }
        * }
    * }
}


### inf-gpu

Namespace that describes GPU capabilities

#### Properties

| Property                                                   | Type      | Applies to | Description                                                 |
|------------------------------------------------------------|-----------|------------|-------------------------------------------------------------|
| `golem.!exp.gap-pci.v1.inf.gpu.model`                      | `string`  | Offer      | Indicates the name of the GPU model                         |

### inf-gpu-video-mem

Namespace that describes GPU video memory

#### Properties

| Property                                                      | Type      | Applies to | Description                                                 |
|---------------------------------------------------------------|-----------|------------|-------------------------------------------------------------|
| `golem.!exp.gap-pci.v1.inf.gpu.video-mem.dedicated.gib` | `integer` | Offer      | indicates the amount of memory built into GPU               |
| `golem.!exp.gap-pci.v1.inf.gpu.video-mem.total.gib`     | `integer` | Offer      | indicates the total amount of memory available for graphics |

### inf-gpu-cuda

Namespace that describes GPU CUDA capabilities

#### Properties

| Property                                                         | Type      | Applies to | Description                                                 |
|------------------------------------------------------------------|-----------|------------|-------------------------------------------------------------|
| `golem.!exp.gap-pci.v1.inf.gpu.cuda.enabled`               | `boolean` | Offer      | this is related to the image running and not the VM runtime |
| `golem.!exp.gap-pci.v1.inf.gpu.cuda.cores`                 | `integer` | Offer      | This can be found from the general specification of each GPU. Otherwise it is platform specific that we would not like to work with. We should aim to focus on platform independent metrics that are easy to access in a health check |
| `golem.!exp.gap-pci.v1.inf.gpu.cuda.version-compatibility` | `number`  | Offer      | this is not known by the GPU because there is a minimum CUDA version and also it can be deprecated in a future CUDA version. It can also be different for CUDA and a given library, for example tensorflow had a different GPU compatibility requirement for building from source and using prebuild binaries. Get the info from Nvidia and/or a general guideline from StackOverflow |

### inf-gpu-clock

Namespace that describes information about GPU clock 

#### Properties

| Property                                              | Type      | Applies to | Description                                                                     |
|-------------------------------------------------------|-----------|------------|---------------------------------------------------------------------------------|
| `golem.!exp.gap-pci.v1.inf.gpu.clock.base.mhz`  | `integer` | Offer      | the standard speed your graphics card works at when not pressed by a heavy load |
| `golem.!exp.gap-pci.v1.inf.gpu.clock.boost.mhz` | `integer` | Offer      | the optimum speed at which the graphics card can run                            |

### inf-gpu-memory

Namespace that describes information about GPU memory

#### Properties

| Property                                                   | Type      | Applies to | Description                                                               |
|------------------------------------------------------------|-----------|------------|---------------------------------------------------------------------------|
| `golem.!exp.gap-pci.v1.inf.gpu.memory.clock.mhz`     | `integer` | Offer      | the speed at which the GPU's memory operates                              |
| `golem.!exp.gap-pci.v1.inf.gpu.memory.type`          | `string`  | Offer      | the type of the GPU's memory, e.g. GDDR5                                  |
| `golem.!exp.gap-pci.v1.inf.gpu.memory.bus`           | `integer` | Offer      | GPU's memory interface width in bits                                      |
| `golem.!exp.gap-pci.v1.inf.gpu.memory.bandwidth.gib` | `integer` | Offer      | the theoretical maximum amount of data that the bus can handle per second |


### inf-gpu-tpu

Namespace that describes information about TPU

#### Properties

| Property                                          | Type      | Applies to | Description                         |
|---------------------------------------------------|-----------|------------|-------------------------------------|
| `golem.!exp.gap-pci.v1.inf.gpu.tpu.enabled` | `boolean` | Offer      | indicates if the GPU is TPU-enabled |
| `golem.!exp.gap-pci.v1.inf.gpu.tpu.gen`     | `string`  | Offer      | the generation of the TPU           |
| `golem.!exp.gap-pci.v1.inf.gpu.tpu.cores`   | `integer` | Offer      | the number of TPU cores available   |




## Rationale
This specification is based on the wish-list provided in [#157](https://github.com/golemfactory/ya-runtime-vm/issues/157). \
For now the assumption is that we only detect the first pci (possibly nvidia) gpu, tpu is only supported as a tpu-enabled gpu device.

Other PCI (and similarly non-PCI) devices can be added as another sub-tree next to `golem.!exp.gap-pci.v1.inf.gpu`.
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
