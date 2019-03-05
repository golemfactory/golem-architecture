# Property set name
CPU specifications

## `golem.inf.cpu.architecture : String`
CPU architecture
### Value enum
|Value| Description |
|---|---|
|"x86"|Intel x86/x64 architecture|
|"arm"|ARM architecture|
### Sample values
* `golem.inf.cpu.architecture="x86"` - Declares x86/x64 CPU available.
* `golem.inf.cpu.architecture="arm"` - Declares ARM CPU available.


## `golem.inf.cpu.bit : List[String]`
CPU bitness
### Value enum
|Value| Description |
|---|---|
|"32"|32-bit CPU|
|"64"|64-bit CPU|
### Sample values
* `golem.inf.cpu.bit:List=["32"]` - Declares 32-bit bitness
* `golem.inf.cpu.bit=["32","64"]` - Declares availability of both 32-bit and 64-bit bitness


## `golem.inf.cpu.cores : Number (int32)`
Total number of CPU cores assigned to service. It is a sum of CPU cores possibly from different CPUs
### Sample values
* `golem.inf.cpu.cores=4` - Declares 4 cores available


## `golem.inf.cpu.threads : Number (int32)`
Total number of CPU threads assigned to service. It is a sum of CPU threads possibly from different CPUs and cores
### Sample values
* `golem.inf.cpu.threads=8` - Declares 8 cores available


## `golem.inf.cpu.max_frequency_mhz : Number (float)`
Maximum allowed CPU clock frequency in Mhz
### Sample values
* `golem.inf.cpu.max_frequency_mhz=3400` - Declares maximum CPU frequency of 3400 Mhz 


## `golem.inf.cpu.vendor : String`
CPU vendor
### Sample values
* `golem.inf.cpu.vendor="Intel"` - Declares Intel's CPU 


## `golem.inf.cpu.model : String`
CPU model
### Sample values
* `golem.inf.cpu.model="i7-3770"` - Declares i7-3770 CPU


## `golem.inf.cpu.capabilities : List[String]`
CPU capability flags 
### Sample values
* `golem.inf.cpu.capabilities=["fpu","sse","sse2","ssse3","sse4_1","sse4_2","ht","x86-64","avx","avx2"]`


## `golem.inf.cpu.l1cache.kib : Number (float)`
Amount of L1 cache in kiB


## `golem.inf.cpu.l2cache.kib : Number (float)`
Amount of L2 cache in kiB


## `golem.inf.cpu.l3cache.kib : Number (float)`
Amount of L3 cache in kiB


# Sample property block
```
golem.inf.cpu.architecture="x86"
golem.inf.cpu.bit=["32","64"]
golem.inf.cpu.cores=4
golem.inf.cpu.threads=8
golem.inf.cpu.max_frequency_mhz=3400
golem.inf.cpu.vendor="Intel"
golem.inf.cpu.model="i7-3770"
golem.inf.cpu.capabilities=["fpu","sse","sse2","ssse3","sse4_1","sse4_2","ht","x86-64","avx","avx2"]
golem.inf.cpu.l1cache.kib=32
golem.inf.cpu.l2cache.kib=256
golem.inf.cpu.l3cache.kib=8192
```