## WebAssembly use case

This article contains examples of Demands & Offers which cover the WebAssembly computation use case.

## Golem WebAssembly Package Computation

The Provider assigns a dedicated about of machine resources to WebAssembly runtime, and declares an Offer describing a generic WebAssembly hosting capability with a known amount of resources.

### Properties used
```properties
# common
golem.activity.caps.transfer.protocol
golem.inf.mem.gib
golem.inf.storage.gib
golem.usage.vector

# node
golem.node.id.name

# srv 
golem.srv.runtime.name
golem.srv.runtime.version
golem.srv.comp.wasm.wasi.version
golem.srv.comp.wasm.task_package

# com
golem.com.pricing.model
golem.com.pricing.model.linear.coeffs
golem.com.payment.platform
golem.com.scheme
golem.com.scheme.payu.interval_sec
golem.srv.comp.expiration

```

### Sample Offer

```properties
# properties

# Supported transfer protocol
golem.activity.caps.transfer.protocol=["http"]

# Supported Payment model "payu" = *Pay* as you *U*se
golem.com.scheme="payu"
golem.com.scheme.payu.interval_sec=6

# Price = 0 + 0.01 * <exe unit duration in secs> + <exe unit cpu usage in cpu secs> * 0.01
golem.com.pricing.model="linear"
golem.com.pricing.model.linear.coeffs=[0, 0.01, 0.01]
golem.com.usage.vector=["golem.usage.duration_sec", "golem.usage.cpu_sec"]

# Offered memory is 1GB
golem.inf.mem.gib=1
# Storage for image + input files + output files is 10GB.
golem.inf.storage.gib=10
golem.inf.cpu.architecture="x86_64"
golem.inf.cpu.cores=3
golem.inf.cpu.threads=5

# Offered runtime is wasmtime version 0.0.0
golem.runtime.name="wasmtime"
golem.runtime.version="0.2.1"

# Optional provider node name
golem.node.id.name="smok1"


# constraints
(golem.srv.comp.expiration>1608555752242)
```

### Sample Demand

```properties
# properties

golem.node.id.name="test1"
golem.srv.comp.expiration=1608556352458
golem.srv.comp.wasm.task_package="hash:sha3:44aba2d41021fac2a3b7af8a3ccfc0a3d4a435f9187ea7d5c162035b:http://54.231.6.186:4500/app-44aba2d4.yimg"

# constraints
(&
    (golem.inf.mem.gib>0.5)
    (golem.inf.storage.gib>1)
    (golem.com.pricing.model=linear)
    (golem.runtime.name=wasmtime)
)

```

