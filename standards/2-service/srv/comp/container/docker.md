# Docker Service 
Ability to run a containerized software component, within a Docker host.

## 

## Common Properties

* [golem.inf](../0-commons/golem.inf.md)

## Specific Properties

## `golem.svc.docker.image : List of String` 

Indicates Docker images which are to be hosted by the Provider. The Offer may either declare specific images as available, or indicate the whole property as dynamic, so that the actual image required by the Requestor is specified by the Demand. In the latter scenario, during the negotiation phase the Provider shall decide whether the image indicated in Demand is trustworthy (eg. by checking an internal whitelist).

This property is a list of String values, where each item in the list is an image descriptor (identifying a docker image precisely, the format of image name is as in the ‘[`docker pull`](https://docs.docker.com/engine/reference/commandline/pull/)’ command).

### Sample values

* `golem.svc.docker.image=["golemfactory/blender"]` - declares Golem Factory's Blender image.
* `golem.svc.docker.image=["myregistry.local:5000/testing/test-image"]` - example of specifying a docker image from a non-GitHub repository.
  
## `golem.svc.docker.benchmark{<image>} : Number` 
A benchmark performance metric calculated for specific docker image for the Provider node.

_(Note: it should probably be specified how a Provider host should run the image to measure the benchmark)_
  
## `golem.svc.docker.timeout.secs : Number` 
A timeout value for docker-hosted computation (used for docker-based batch processes)


