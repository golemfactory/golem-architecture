## Brass Golem blender use case

This article contains examples of Demands & Offers which cover the Brass Golem Blender batch computation use case.

**Note:** Blender computation service can be described in various ways.

## Scenario 1 - Containerized Golem Brass Blender Docker Image

One way of providing Blender computation service is to host a standardized Docker Image with Blender logic in it. The Provider assigns a dedicated about of machine resources to Docker Host, and declares an Offer describing a generic Docker-based hosting capability with a known amount of resources.

### Properties used
```properties
# srv 
golem.srv.comp.container.docker.image
golem.srv.comp.container.docker.benchmark{<image>}
golem.inf.cpu.cores
golem.inf.cpu.threads
golem.inf.storage.gib
golem.inf.mem.gib

# com
golem.usage.vector
golem.com.payment.scheme
golem.com.pricing.est{<usage_vector>}
golem.com.pricing.model
golem.com.pricing.model.linear.coeffs

# use case specific
golem.srv.app.media.render.input_file_size_kib
golem.srv.app.media.render.output.resolution.x
golem.srv.app.media.render.output.resolution.y
## TODO: what else here?


```

### Sample Offer

```properties
# properties
golem.srv.comp.container.docker.image=["golemfactory/blender"]
golem.srv.comp.container.docker.benchmark{golemfactory/blender}=682.1076
golem.srv.comp.container.docker.benchmark{*}
golem.activity.timeout_secs

golem.inf.cpu.cores=4
golem.inf.cpu.threads=8
golem.inf.mem.gib=16
golem.inf.storage.gib=30

golem.usage.vector=["golem.usage.duration_sec"]
golem.com.payment.scheme="after"
golem.com.pricing.model="linear"
golem.com.pricing.model.linear.coeffs=[20, 0]

# constraints
()
```

### Sample Demand

```properties
# properties

## use case specific
golem.srv.app.media.render.input_file_size_kib=7233
golem.srv.app.media.render.output.resolution.x=1920
golem.srv.app.media.render.output.resolution.y=1080

# constraints
(&
    (golem.srv.comp.container.docker.image=golemfactory/blender)
    (golem.srv.comp.container.docker.benchmark{golemfactory/blender}>300)
    (golem.com.payment.scheme=after)
    (golem.usage.vector=[golem.usage.duration_sec])
    (golem.com.pricing.model=linear)
    (golem.com.pricing.est{[30]}<125)
)

```

## Scenario 2 - Dedicated specialized Blender rendering service

It is also possible to offer a specialized Blender service, which abstracts from hardware & infrastructure aspects. Such a service is described by app-specific properties as defined in `golem.srv.app.*` namespace. 

### Properties used
```properties
# srv 
golem.srv.app.media.render.engine
golem.srv.app.media.render.blender.benchmark
golem.srv.app.media.render.blender.????

# com
golem.usage.vector
golem.com.payment.scheme
golem.com.pricing.est{<usage_vector>}
golem.com.pricing.model
golem.com.pricing.model.linear.coeffs

# use case specific
golem.srv.app.media.render.input_file_size_kib
golem.srv.app.media.render.output.resolution.x
golem.srv.app.media.render.output.resolution.y

```

### Sample Offer

```properties
# properties
golem.srv.app.media.render.engine="blender"
golem.srv.app.media.render.blender.benchmark=553

golem.usage.vector=["golem.usage.duration_sec"]
golem.com.payment.scheme="after"
golem.com.pricing.model="linear"
golem.com.pricing.model.linear.coeffs=[0, 37]

# constraints
()
```

### Sample Demand

```properties
# properties

golem.srv.app.media.render.input_file_size_kib=7233
golem.srv.app.media.render.output.resolution.x=1920
golem.srv.app.media.render.output.resolution.y=1080

# constraints
(&
    (golem.srv.app.media.render.engine=blender)
    (golem.srv.app.media.render.blender.benchmark>100)
    (golem.com.payment.scheme=after)
    (golem.usage.vector=[golem.usage.duration_sec])
    (golem.com.pricing.est{[30]}<125>)
    (golem.activity.timeout_secs=30)
)

```
