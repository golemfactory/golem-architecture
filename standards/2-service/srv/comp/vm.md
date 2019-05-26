# Generic Virtual Machine 
Ability to host a generic Virtual Machine.

## Common Properties

* [golem.inf](../0-commons/golem.inf.md)

## Specific Properties

## `golem.srv.comp.vm.host : String` 
Indicates the VM host platform available. 
### Value enum
|Value| Description |
|---|---|
|"VirtualBox"|Oracle's VirtualBox platform|
|"HyperV"|Microsoft Windows HyperV hypervisor platform|

### **Examples**

* `golem.srv.comp.vm.host="VirtualBox"` - declares that Oracle's VirtualBox is available as a virtualization platform on the Provider node.
  
## `golem.srv.comp.vm.term_protocol : List of String` 
A list of protocols that can be used to establish a terminal session to the VM.

### Value enum
|Value| Description |
|---|---|
|"telnet"|Telnet|
|"ssh"|SSH|
|"rdp"|Microsoft Remote Desktop Protocol|
  
### **Examples**

* `golem.srv.comp.vm.term_protocol="ssh"` - declares that SSH is available as terminal protocol.


