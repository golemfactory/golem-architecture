# Golem Infrastructural Properties - CPU
Specifications of CPU computing power assigned to a service.

## `golem.inf.cpu.architecture : String`

### Describes: Offer

CPU architecture.
### Value enum
|Value| Description |
|---|---|
|"x86"| Intel x86 32-bit architecture |
|"x86_64"| Intel x86 64-bit architecture |
### **Examples**
* `golem.inf.cpu.architecture="x86_64"` - Declares x86 64-bit CPU available.

## `golem.inf.cpu.cores : Number (int32)`

### Describes: Offer

Total number of CPU cores assigned to service. It is a sum of CPU cores possibly from multiple CPUs.

### **Examples**
* `golem.inf.cpu.cores=4`

## `golem.inf.cpu.threads : Number (int32)`

### Describes: Offer

Total number of CPU threads assigned to service. It is a sum of CPU threads possibly from multiple CPUs and cores.
### **Examples**
* `golem.inf.cpu.threads=8`

## `golem.inf.cpu.capabilities : List[String]`

### Describes: Offer

CPU capability flags. 
For x86 architectures this property is populated with CPU features as returned by CPUID instruction.
For full list, see here: https://github.com/golemfactory/ya-runtime-vm/blob/master/runtime/src/cpu.rs#L59 
### **Examples**
* `golem.inf.cpu.capabilities=["fpu","sse","sse2","ssse3","sse4_1","sse4_2","ht","x86-64","avx","avx2"]`