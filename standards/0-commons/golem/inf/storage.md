# Golem Infrastructural Properties - Storage
Properties which describe storage properties of Golem service (hardware parameters, disk categories, sizes, etc.)

## `golem.inf.storage.gib : Number (float)`
Storage available in GiB
### Sample values
* `golem.inf.storage.gib=30` - Declares 30 GB of storage available to guest process
* `golem.inf.storage.gib=15.5` - Declares 15.5 GB of storage available to guest process


## `golem.inf.storage.type : String`
Disk type
### Value enum
|Value| Description |
|---|---|
|SSD|Solid state drive|
|HDD|Hard drive|

## `golem.inf.storage.write.mibs : Number (float)`
Disk write speed in MiB/s

## `golem.inf.storage.read.mibs : Number (float)`
Disk read speed in MiB/s


# Sample property block
```
golem.inf.storage.gib=30
golem.inf.storage.type="SSD"
golem.inf.storage.write.mibs=350
golem.inf.storage.write.mibs=550

```
