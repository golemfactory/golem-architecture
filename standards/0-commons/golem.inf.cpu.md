# Property set name
CPU specifications

## `golem.inf.cpu.platform : String`
CPU platform

### Sample values
* `golem.inf.cpu.platform="x86"` - Declares x86 CPU available.
* `golem.inf.cpu.platform="x64"` - Declares x64 CPU available.
* `golem.inf.cpu.platform="arm"` - Declares ARM CPU available.


## `golem.inf.cpu.bit : List of String`
CPU bitness

### Sample values
* `golem.inf.cpu.bit:List=["32"]` - Declares 32-bit bitness.
* `golem.inf.cpu.bit=["32","64"]` - Declares availability of both 32-bit and 64-bit bitness.


## `golem.inf.cpu.cores : Number`
Total number of CPU cores assigned to service. It is a sum of CPU cores possibly from different CPUs.

### Sample values
* `golem.inf.cpu.cores=4` - Declares 4 cores available.


## `golem.inf.cpu.threads : Number`
Total number of CPU threads assigned to service. It is a sum of CPU threads possibly from different CPUs and cores.

### Sample values
* `golem.inf.cpu.threads=8` - Declares 8 cores available.


## `golem.inf.cpu.max_frequency : Number`
Maximum allowed CPU clock frequency in Mhz.

### Sample values
* `golem.inf.cpu.max_frequency=3400` - Declares maximum CPU frequency of 3400 Mhz 