# Golem Infrastructural Properties
Properties which describe infrastructural aspects of Golem service (hardware parameters, software platform parameters)

## `golem.inf.cores:Number`

Number of cores assigned to service.

### Sample values

* `golem.inf.cores:Number=4` - Declares 4 cores available to a requestor's software.

## `golem.inf.ram.gb:Number`
_Description_

### Sample values

* `golem.inf.ram.gb:Number=16` - Declares 16 GB RAM available to a requestor's software.

## `golem.inf.cpu.platform:String`
_Description_

### Sample values

* `golem.inf.cpu.platform=x86` - Declares x86 CPU available.
* `golem.inf.cpu.platform=x64` - Declares x64 CPU available.
* `golem.inf.cpu.platform=arm` - Declares ARM CPU available.

## `golem.inf.cpu.bit:String`
_Description_

### Sample values

* `golem.inf.cpu.bit=32` - Declares 32-bit platform.
* `golem.inf.cpu.bit=64` - Declares 64-bit platform.


## `golem.inf.disk.gb:Number`
_Description_

### Sample values

* `golem.inf.disk.gb:Number=30` - Declares 30 GB of storage available to guest process.
* `golem.inf.disk.gb:Number=15.5` - Declares 15.5 GB of storage available to guest process.


# Sample property block
```
golem.inf.cores:Number=
golem.inf.cpu.platform=
golem.inf.ram.gb:Number=
golem.inf.cpu.bit:Number=
golem.inf.disk.gb:Number=
```
