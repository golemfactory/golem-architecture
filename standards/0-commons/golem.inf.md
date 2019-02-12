# Golem Infrastructural Properties
Properties which describe infrastructural aspects of Golem service (hardware parameters, software platform parameters)

## `golem.inf.cpu.*`
Specifications of CPU assigned to a service

#### `golem.inf.cpu.vendor`
CPU vendor

#### `golem.inf.cpu.model`
CPU model name.
(note: this is a scalar property, ie. we do not attempt to model very unusual variants or nuances like multi-CPU-model host machines... Common sense! ;) )

#### `golem.inf.cpu.cores`
Number of CPU cores

#### `golem.inf.cpu.bit`
CPU bitness e.g. 32, 64

#### `golem.inf.cpu.platform`
CPU platform e.g. x86, x64, arm 

#### `golem.inf.cpu.mhz`
Maximum CPU speed in MHz

#### `golem.inf.cpu.capabilities`
CPU capability flags


##### Sample values

```
golem.inf.cpu.vendor="GenuineIntel"
golem.inf.cpu.model="Intel(R) Core(TM) i7-6700 CPU @ 3.40GHz"
golem.inf.cpu.cores=4
golem.inf.cpu.bit=[32, 64]
golem.inf.cpu.platform="x86"
golem.inf.cpu.mhz=4000
golem.inf.cpu.capabilities=["fpu","sse","sse2","ssse3","sse4_1","sse4_2","ht","x86-64","avx","avx2"]
```

## `golem.inf.mem.*`
Specifications of operating memory assigned to a service

#### `golem.inf.mem.ram.gib`
Amount of RAM in GiB

#### `golem.inf.mem.ram.mhz`
RAM clock in MHz

#### `golem.inf.mem.l1cache.kib`
Amount of L1 cache in kiB

#### `golem.inf.mem.l2cache.kib`
Amount of L1 cache in kiB

#### `golem.inf.mem.l3cache.kib`
Amount of L1 cache in kiB

##### Sample values

```
golem.inf.mem.ram.gib=16
golem.inf.mem.ram.mhz=2133
golem.inf.mem.l1cache.kib=256
golem.inf.mem.l1cache.kib=1024
golem.inf.mem.l1cache.kib=8192
```

## `golem.inf.disk.*`
Specifications of disk assigned to a service

#### `golem.inf.disk.gib`
Amount of storage in GiB

#### `golem.inf.disk.type`
Disk type eg. SSD, HDD

#### `golem.inf.disk.write.mibs`
Disk write speed in MiB/s

#### `golem.inf.disk.read.mibs`
Disk read speed in MiB/s

##### Sample values

```
golem.inf.disk.gib=15.5
golem.inf.disk.type="SSD"
golem.inf.disk.write.mibs=900.51
golem.inf.disk.read.mibs=9846.63
```

## `golem.inf.gpu.*`
Specifications of GPU card(s) assigned to a service

#### `golem.inf.gpu.vendor`
GPU card vendor name

#### `golem.inf.gpu.model`
GPU card model name

#### `golem.inf.gpu.count`
Number of GPU cards

#### `golem.inf.gpu.gib`
GPU RAM available in GiB


# Sample property block
```
golem.inf.gpu.vendor="NVidia"
golem.inf.gpu.model=”GeForce GTX 960”
golem.inf.gpu.count=8
golem.inf.gpu.gib=3.947
```
