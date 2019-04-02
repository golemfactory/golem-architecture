## Golem transcoding use case (in spe)

This article contains examples of Demands & Offers which cover the Golem Transcoding batch computation use case.


### Sample Offer

```properties
# properties
golem.svc.docker.image=["golemfactory/ffmpeg"]
golem.svc.docker.benchmark{golemfactory/ffmpeg}=7
golem.svc.docker.benchmark{*}
golem.svc.docker.timeout

golem.inf.cpu.cores=4
golem.inf.mem.gib=16
golem.inf.cpu.threads=8
golem.inf.storage.gib=30
golem.inf.storage.gib=30

## TODO pricing is not yet decided

golem.usage.vector=["golem.usage.duration_sec"]
golem.com.payment.scheme="after"

## TODO: use case specific

# constraints
()
```

### Sample Demand

```properties
# properties

## use case specific

golem.app.transcoding.key_frames_number=307
golem.app.transcoding.input_resolution.x=1024
golem.app.transcoding.input_resolution.y=768
golem.app.transcoding.bitrate_kibs=1522
golem.app.transcoding.input_container=avi
golem.app.transcoding.input_video_codecs=["H264", "H265"]
golem.app.transcoding.input_container=mp4
golem.app.transcoding.output_video_codecs=["vp6", "DivX"]

## audio is currently not supported

# constraints
(&
    (golem.svc.docker.image=["golemfactory/blender"])
    (golem.svc.docker.benchmark{golemfactory/blender}>300)
    (golem.com.payment.scheme="after")
    (golem.usage.vector=["golem.usage.duration_sec"])
    (golem.com.pricing.model="linear")
    #(golem.com.pricing.est{[30]}<125)
)

```
