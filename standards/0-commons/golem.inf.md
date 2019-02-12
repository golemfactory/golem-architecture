# Golem Infrastructural Properties
Properties which describe infrastructural aspects of Golem service (hardware parameters, software platform parameters)

## `golem.inf.cores : Number (int32)`

Number of cores assigned to service.

### Sample values

* `golem.inf.cores=4` - Declares 4 cores available to a requestor's software.

## `golem.inf.ram.gb : Number (float)`
_Description_

### Sample values

* `golem.inf.ram.gb=16` - Declares 16 GB RAM available to a requestor's software.

## `golem.inf.cpu.architecture : String`
CPU architecture.

### Value enum
|Value| Description |
|---|---|
|"x86"|Intel x86/x64 architecture|

## `golem.inf.cpu.bit : List of String`

CPU bitness.

### Value enum
|Value| Description |
|---|---|
|"32"|32-bit CPU|
|"64"|64-bit CPU|

### Sample values

* `golem.inf.cpu.bit:List=["32"]` - Declares 32-bit platform.
* `golem.inf.cpu.bit=["32","64"]` - Declares availability of both 32-bit and 64-bit platform.


## `golem.inf.disk.gb : Number (double)`
_Description_

### Sample values

* `golem.inf.disk.gb=30` - Declares 30 GB of storage available to guest process.
* `golem.inf.disk.gb=15.5` - Declares 15.5 GB of storage available to guest process.


# Sample property block
```
golem.inf.cores=2
golem.inf.cpu.platform="x86"
golem.inf.ram.gb=4
golem.inf.cpu.bit=["32","64"]
golem.inf.disk.gb=30
```
