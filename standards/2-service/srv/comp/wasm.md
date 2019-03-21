# WebAssembly Host 
Ability to host and execute a WebAssembly program.

## Common Properties

* [golem.inf](../../../../0-commons/golem/inf.md)
* [golem.activity](../../../../0-commons/golem/activity.md)

## Specific Properties

## `golem.svc.wasm.task_package : List of String` 

Indicates WebAssembly packages which are to be hosted by the Provider. The Offer may either declare specific images as available, or indicate the whole property as dynamic, so that the actual image required by the Requestor is specified by the Demand. In the latter scenario, during the negotiation phase the Provider shall decide whether the image indicated in Demand is trustworthy (eg. by checking an internal whitelist).

This property is a list of String values, where each item in the list is a package URL pointing to a WebAssembly package in the Golem Factory WASM format specified in this [article](https://github.com/golemfactory/golem/wiki/Launching-Wasm-tasks-in-Golem).

The repo of WASM task packages maintained by Golem Factory can be found [here](https://github.com/golemfactory/wasm-store).

### **Examples**

* `golem.svc.wasm.task_package=["https://github.com/golemfactory/wasm-store/tree/master/flite/flite.zip"]` - declares Golem Factory's 'text-to-speech' sample WASM package.


