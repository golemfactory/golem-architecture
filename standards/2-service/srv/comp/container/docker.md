# Docker Service 
Ability to run a containerized software component, within a Docker host.

A Provider which declares any property from this namespace is assumed to be able to support a Docker Image execution environment.

## Common Properties

* [golem.inf](../../../../0-commons/golem/inf.md)
* [golem.activity](../../../../0-commons/golem/activity.md)

## Specific Properties

## `golem.srv.comp.container.docker.image : List of String` 

Indicates Docker images which are to be hosted by the Provider. The Offer may either declare specific images as available, or indicate the whole property as dynamic, so that the actual image required by the Requestor is specified by the Demand. In the latter scenario, during the negotiation phase the Provider shall decide whether the image indicated in Demand is trustworthy (eg. by checking an internal whitelist).

This property is a list of String values, where each item in the list is an image descriptor (identifying a docker image precisely, the format of image name is as in the ‘[`docker pull`](https://docs.docker.com/engine/reference/commandline/pull/)’ command).

### **Examples**

* `golem.srv.comp.container.docker.image=["golemfactory/blender"]` - declares Golem Factory's Blender image.
* `golem.srv.comp.container.docker.image=["myregistry.local:5000/testing/test-image"]` - example of specifying a docker image from a non-GitHub repository.
* `golem.srv.comp.container.docker.image` - declares that the Provider supports the `golem.svc.docker.image` property, which can be referred to by Demand's constraint expressions. The dynamic resolution of the property value shall unveil the images currently supported by the Provider.
  
## `golem.srv.comp.container.docker.benchmark{<image>} : Number` 
A benchmark performance metric calculated for specific docker image for the Provider node.

_(**Note:** (TODO) it must be specified how a Provider host should run the image to measure the benchmark. This property defines the benchmark trigger standard which needs to be supported by the Golem Node's Docker execution environment.)_
  

