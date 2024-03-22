---
gap: GAP-35
title: GPU/PCI capability
description: Golem Computing Resource Standard properties for elements of GPU/PCI specification
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
        * gap-35: { 
            * v1: {
                * inf: {
                    * [gpu](#inf-gpu): {
                        * model: string,
                        * [cuda](#inf-gpu-cuda): {
                            * enabled: boolean,
                            * cores: integer,  
                            * version: integer, 
                        * },
                        * [clocks](#inf-gpu-clocks): {
                            * graphics.mhz: integer,
                            * memory.mhz: integer,
                            * sm.mhz: integer,
                            * video.mhz: integer,
                        * },
                        * [memory](#inf-gpu-memory): {
                            * bandwidth.gib: integer
                            * total.gib: number
                        * },
                    * }
                * }
            * }
        * }
    * }
}


### inf-gpu

Namespace that describes GPU capabilities

#### Properties

| Property                              | Type     | Applies to | Description                         |
| ------------------------------------- | -------- | ---------- | ----------------------------------- |
| `golem.!exp.gap-35.v1.inf.gpu.model` | `string` | Offer      | Indicates the name of the GPU model |

### inf-gpu-cuda

Namespace that describes GPU CUDA capabilities

#### Properties

| Property                                                | Type      | Applies to | Description |
| ------------------------------------------------------- | --------- | ---------- | ----------- |
| `golem.!exp.gap-35.v1.inf.gpu.cuda.enabled`             | `boolean` | Offer      | this is related to the image running and not the VM runtime |
| `golem.!exp.gap-35.v1.inf.gpu.cuda.cores`               | `integer` | Offer      | This can be found from the general specification of each GPU. Otherwise it is platform specific that we would not like to work with. We should aim to focus on platform independent metrics that are easy to access in a health check |
| `golem.!exp.gap-35.v1.inf.gpu.cuda.version`             | `string`  | Offer      | A version of CUDA that is guaranteed to work. The range of compatibile versions is not known by the GPU because there is a minimum CUDA version and also it can be deprecated in a future CUDA version. It can also be different for CUDA and a given library, for example tensorflow had a different GPU compatibility requirement for building from source and using prebuild binaries. Get the info from Nvidia and/or a general guideline from StackOverflow. The property is a version string including a major version, and a minor version in #.# format. |
| `golem.!exp.gap-35.v1.inf.gpu.cuda.compute-capability`  | `string`  | Offer      | Every [Nvidia GPU](https://developer.nvidia.com/cuda-gpus) has Compute Capability level. It allows to identify [CUDA features it supports](https://docs.nvidia.com/cuda/cuda-c-programming-guide/index.html#compute-capabilities). The property is a version string including a major version, and a minor version in #.# format. |

### inf-gpu-clocks

Namespace that describes information about GPU clocks

#### Properties

| Property                                           | Type      | Applies to | Description                                                                                                        |
| -------------------------------------------------- | --------- | ---------- | ------------------------------------------------------------------------------------------------------------------ |
| `golem.!exp.gap-35.v1.inf.gpu.clocks.graphics.mhz` | `integer` | Offer      | The max rate of the graphics clock as reported by nvidia-smi                                                       |
| `golem.!exp.gap-35.v1.inf.gpu.clocks.memory.mhz`   | `integer` | Offer      | The max rate of the memory clock as reported by nvidia-smi                                                         |
| `golem.!exp.gap-35.v1.inf.gpu.clocks.sm.mhz`       | `integer` | Offer      | The max rate of the streaming multiprocessor clock as reported by nvidia-smi. CUDA cores are driven by this clock. |
| `golem.!exp.gap-35.v1.inf.gpu.clocks.video.mhz`    | `integer` | Offer      | The max rate of the video clock as reported by nvidia-smi                                                          |

### inf-gpu-memory

Namespace that describes information about GPU memory

#### Properties

| Property                                            | Type      | Applies to | Description                                                               |
| --------------------------------------------------- | --------- | ---------- | ------------------------------------------------------------------------- |
| `golem.!exp.gap-35.v1.inf.gpu.memory.bandwidth.gib` | `integer` | Offer      | **Optional** The theoretical maximum amount of data that the bus can handle per second |
| `golem.!exp.gap-35.v1.inf.gpu.memory.total.gib`     | `integer` | Offer      | Indicates the amount of memory available to the GPU                       |

## Rationale
This specification is based on the wish-list provided in [#157](https://github.com/golemfactory/ya-runtime-vm/issues/157). \
For now the assumption is that we only detect the first pci (possibly nvidia) gpu.

Other PCI (and similarly non-PCI) devices can be added as another sub-tree next to `golem.!exp.gap-35.v1.inf.gpu`.
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
