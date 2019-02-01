# Docker Service 
Ability to run a containerized software component, within a Docker host.

## 

## Common Properties

* [golem.inf](../0-commons/golem.inf.md)

## Specific Properties
* `golem.svc.docker.image` - an image descriptor (identifying a docker image precisely , the format of image name is as in the ‘[`docker pull`](https://docs.docker.com/engine/reference/commandline/pull/)’ command
  
* `golem.svc.docker.benchmark{<image>}` - a benchmark performance metric calculated for specific docker image for the Provider node (Note: it should probably be specified how a Provider host should run the image to measure the benchmark)
  
* `golem.svc.docker.timeout.secs` - a timeout value for docker-hosted computation (used for docker-based batch processes)


