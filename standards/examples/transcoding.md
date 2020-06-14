## Golem transcoding use case 

This article contains examples of Demands & Offers which cover the Golem Transcoding batch computation use case.

**Note:** Transcoding computation service can be described in various ways.

## Scenario 1 - Containerized Golem FFMPEG Transcoding Docker Image

One way of providing Transcoding computation service is to host a standardized Docker Image with Transcoding logic in it. The Provider assigns a dedicated about of machine resources to Docker Host, and declares an Offer describing a generic Docker-based hosting capability with a known amount of resources.

### Sample Offer

```properties
# properties
golem.srv.comp.container.docker.image=["golemfactory/ffmpeg"]
golem.srv.comp.container.docker.benchmark{golemfactory/ffmpeg}=7
golem.srv.comp.container.docker.benchmark{*}

golem.inf.cpu.cores=4
golem.inf.cpu.threads=8
golem.inf.mem.gib=16
golem.inf.storage.gib=30

golem.usage.vector=["golem.usage.duration_sec"]
golem.com.payment.scheme="after"
golem.com.pricing.model="linear"
golem.com.pricing.est{*}

# constraints
()
```

### Sample Demand

```properties
# properties

# constraints
(&
    (golem.srv.comp.container.docker.image=golemfactory/ffmpeg)
    (golem.srv.comp.container.docker.benchmark{golemfactory/ffmpeg}>300)
    (golem.com.payment.scheme=after)
    (golem.usage.vector=[golem.usage.duration_sec])
    (golem.com.pricing.model=linear)
    (golem.com.pricing.est{[30]}<125)
)

```

## Scenario 2 - Dedicated specialized Transcoding service

It is also possible to offer a specialized Transcoding service, which abstracts from hardware & infrastructure aspects. Such a service is described by app-specific properties as defined in `golem.srv.app.*` namespace. 

### Sample Offer

```properties
# properties
golem.srv.app.transcode.input_resolution.x
golem.srv.app.transcode.input_resolution.y
golem.srv.app.transcode.input_container
golem.srv.app.transcode.input_video_codecs=["H264", "H265"]
golem.srv.app.transcode.key_frames_number
golem.srv.app.transcode.bitrate_kibs
golem.srv.app.transcode.output_video_codecs=["vp6", "DivX"]

golem.usage.vector=["golem.usage.duration_sec"]
golem.com.payment.scheme="after"
golem.com.pricing.model="linear"
golem.com.pricing.est{*}

# constraints
()
```

### Sample Demand

```properties
# properties
golem.srv.app.transcode.input_resolution.x=1024
golem.srv.app.transcode.input_resolution.y=768
golem.srv.app.transcode.key_frames_number=307
golem.srv.app.transcode.bitrate_kibs=1522

# constraints
(&
    (golem.srv.app.transcode.input_container=avi)
    (golem.srv.app.transcode.input_video_codecs=H264)
    (golem.srv.app.transcode.output_video_codecs=DivX)
    (golem.com.payment.scheme=after)
    (golem.usage.vector=[golem.usage.duration_sec])
    (golem.com.pricing.model=linear)
    (golem.com.pricing.est{[30]}<125)
)

```
