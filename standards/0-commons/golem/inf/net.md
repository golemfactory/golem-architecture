# Golem Infrastructural Properties - Network
Properties which describe network properties of Golem service (network protocols supported, network visibility, VPN capabilities, etc.)

## `golem.inf.net.ipv4.has_pub_addr : Boolean`
Declares that a Provider does have a public IP.

### Sample values
* `golem.inf.net.ipv4.has_pub_addr=true` - The host node explicitly states that it has a public IP.
* `golem.inf.net.ipv4.has_pub_addr` - Declares host node supports the has_pub_addr, but the value isn't declared at the moment of Offer formulation.

## `golem.inf.net.ipv4.tcp_visible{*} : Boolean`
Pseudo-function property allowing to verify the TCP protocol connectivity of specific address, specified via:
- `IP(:port)`
- `dnsname(:port)`
  
*Note:* This property is not going to be declared specifically, but rather it will be declared as 'supported' in Offer, and the value shall be declared by Offer's Provider at the time of Demand matching resolution (ie. it is a purely dynamic property).

### Sample values
* `golem.inf.net.ipv4.tcp_visible{*}` - Declares host node supports the `tcp_visible` pseudo-function.
