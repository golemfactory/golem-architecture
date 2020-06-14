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
golem.com.payment.scheme
golem.com.payment.scheme.payu.interval_sec
golem.com.term.expiration_dt

```

### Sample Offer

```properties
# properties

# Supported transfer protocol
golem.activity.caps.transfer.protocol=["http"]

# Supported Payment model "payu" = *Pay* as you *U*se
golem.com.payment.scheme="payu"
golem.com.payment.scheme.payu.interval_sec=6

# Price = 0 + 0.01 * <exe unit duration in secs> + <exe unit cpu usage in cpu secs> * 0.01
golem.com.pricing.model="linear"
golem.com.pricing.model.linear.coeffs=[0, 0.01, 0.01]
golem.com.usage.vector=["golem.usage.duration_sec", "golem.usage.cpu_sec"]

# Offered memory is 1GB
golem.inf.mem.gib=1
# Storage for image + input files + output files is 10GB.
golem.inf.storage.gib=10

# Offered runtime is wasmtime version 0.0.0
golem.srv.runtime.name="wasmtime"
golem.srv.runtime.version@v="0.0.0"

# Optional provider node name
golem.node.id.name="smok1"


# constraints
()
```

### Sample Demand

```properties
# properties

golem.node.id.name="test1"
golem.com.term.expiration_dt="2020-06-15T23:20:50.52Z"
golem.srv.comp.wasm.task_package="hash:sha3:44aba2d41021fac2a3b7af8a3ccfc0a3d4a435f9187ea7d5c162035b:http://54.231.6.186:4500/app-44aba2d4.yimg"

# constraints
(&
    (golem.inf.mem.gib>0.5)
    (golem.inf.storage.gib>1)
    (golem.com.pricing.model=linear)
    (golem.srv.runtime.name=wasmtime)
)

```

