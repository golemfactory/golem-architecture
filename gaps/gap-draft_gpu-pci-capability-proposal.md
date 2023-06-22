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
        * gpu: {
            * v1: {
                * inf: {
                    * [gpu](#inf-gpu): {
                        * model: "string",
                        * [video-mem](#inf-gpu-model-video-mem): {
                            * dedicated.gib: f64,
                            * total.gib: f64
                        * },
                        * [cuda](#inf-gpu-model-cuda): {
                            * enabled: bool,
                            * cores: int,  
                            * version-compatibility: int, 
                        * },
                        * [clock](#inf-gpu-model-clock): {
                            * base.mhz: int,
                            * boost.mhz: int
                        * },
                        * [memory](#inf-gpu-model-memory): {
                            * clock.mhz: int,
                            * type: "string",
                            * bus: int,
                            * bandwidth: "string" / int 
                        * },
                        * [tpu](#inf-gpu-model-tpu): {
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


### inf gpu

Namespace that describes GPU capabilities

#### Properties

| Property                                                  | Type      | Applies to | Description                                                 |
|-----------------------------------------------------------|-----------|------------|-------------------------------------------------------------|
| `golem.!exp.gpu.v1.inf.gpu.model`                         | `string`  | Offer      | Indicates the name of the GPU model                         |

### inf gpu model video-mem

Namespace that describes GPU video memory

#### Properties

| Property                                                  | Type      | Applies to | Description                                                 |
|-----------------------------------------------------------|-----------|------------|-------------------------------------------------------------|
| `golem.!exp.gpu.v1.inf.gpu.model.video-mem.dedicated.gib` | `integer` | Offer      | indicates the amount of memory built into GPU               |
| `golem.!exp.gpu.v1.inf.gpu.model.video-mem.total.gib`     | `integer` | Offer      | indicates the total amount of memory available for graphics |

### inf gpu model cuda

Namespace that describes GPU CUDA capabilities

#### Properties

| Property                                                     | Type      | Applies to | Description                                                 |
|--------------------------------------------------------------|-----------|------------|-------------------------------------------------------------|
| `golem.!exp.gpu.v1.inf.gpu.model.cuda.enabled`               | `boolean` | Offer      | this is related to the image running and not the VM runtime |
| `golem.!exp.gpu.v1.inf.gpu.model.cuda.cores`                 | `integer` | Offer      | This can be found from the general specification of each GPU. Otherwise it is platform specific that we would not like to work with. We should aim to focus on platform independent metrics that are easy to access in a health check |
| `golem.!exp.gpu.v1.inf.gpu.model.cuda.version-compatibility` | `number`  | Offer      | this is not known by the GPU because there is a minimum CUDA version and also it can be deprecated in a future CUDA version. It can also be different for CUDA and a given library, for example tensorflow had a different GPU compatibility requirement for building from source and using prebuild binaries. Get the info from Nvidia and/or a general guideline from StackOverflow |

### inf gpu model clock

Namespace that describes information about GPU clock 

#### Properties

| Property                                          | Type      | Applies to | Description                                                                     |
|---------------------------------------------------|-----------|------------|---------------------------------------------------------------------------------|
| `golem.!exp.gpu.v1.inf.gpu.model.clock.base.mhz`  | `integer` | Offer      | the standard speed your graphics card works at when not pressed by a heavy load |
| `golem.!exp.gpu.v1.inf.gpu.model.clock.boost.mhz` | `integer` | Offer      | the optimum speed at which the graphics card can run                            |

### inf gpu model memory

Namespace that describes information about GPU memory

#### Properties

| Property                                               | Type      | Applies to | Description                                                               |
|--------------------------------------------------------|-----------|------------|---------------------------------------------------------------------------|
| `golem.!exp.gpu.v1.inf.gpu.model.memory.clock.mhz`     | `integer` | Offer      | the speed at which the GPU's memory operates                              |
| `golem.!exp.gpu.v1.inf.gpu.model.memory.type`          | `string`  | Offer      | the type of the GPU's memory, e.g. GDDR5                                  |
| `golem.!exp.gpu.v1.inf.gpu.model.memory.bus`           | `integer` | Offer      | GPU's memory interface width in bits                                      |
| `golem.!exp.gpu.v1.inf.gpu.model.memory.bandwidth.gib` | `integer` | Offer      | the theoretical maximum amount of data that the bus can handle per second |


### inf gpu model tpu

Namespace that describes information about TPU

#### Properties

| Property                                      | Type      | Applies to | Description                         |
|-----------------------------------------------|-----------|------------|-------------------------------------|
| `golem.!exp.gpu.v1.inf.gpu.model.tpu.enabled` | `boolean` | Offer      | indicates if the GPU is TPU-enabled |
| `golem.!exp.gpu.v1.inf.gpu.model.tpu.gen`     | `string`  | Offer      | the generation of the TPU           |
| `golem.!exp.gpu.v1.inf.gpu.model.tpu.cores`   | `integer` | Offer      | the number of TPU cores available   |


---


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