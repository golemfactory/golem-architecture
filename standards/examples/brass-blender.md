## Brass Golem blender use case

### Props accepted
```properties
# inf
golem.inf.cpu.cores
golem.inf.storage.gib
golem.inf.mem.gib
golem.brass.blender.performance
golem.net.mask
golem.brass.min_version

# com
golem.com.payment.scheme
golem.com.pricing.model.linear.micro_gnt_per_second

# service

```

### Sample Offer

```properties
# props
golem.inf.cpu.cores=4
golem.inf.cpu.threads=8
golem.inf.mem.gib=16
golem.inf.storage.gib=30
golem.brass.blender.performance=682.107663718836
golem.brass.min_version=v"0.19.1"
golem.com.payment.scheme="after"
golem.usage.vector=["golem.usage.duration_sec"]
golem.com.pricing.model="linear"
golem.com.pricing.model.linear.coeffs=[200000]

# constraints
()
```

### Sample Demand

```properties
#properties
golem.inf.cpu.cores=4
golem.inf.cpu.threads=8
golem.inf.mem.gib=16
golem.inf.storage.gib=30
golem.brass.blender.performance=682.107663718836
golem.net.mask="???"
golem.brass.min_version="0.19.1"
golem.com.payment.scheme="GNTB@ETH"
golem.com.pricing.model.linear.micro_gnt_per_second=200000

```