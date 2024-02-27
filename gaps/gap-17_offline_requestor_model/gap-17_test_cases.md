# Offline Requestor Model - Test Scenarios

## 1. Requestor partially connected

### 1.1. Requestor Agent outage
1. Launch & initialize Requestor `yagna` daemon
2. Launch Requestor Agent to start an interactive service payload on a VM 
3. Execute a simple ExeScript batch to ensure successful interaction with payload
4. Shutdown the Requestor Agent
5. Restart the Requestor Agent
6. Execute a simple ExeScript batch to ensure the Requestor Agent application has successfully reconnected to the Daemon

### 1.2. Requestor Daemon outage
1. Launch & initialize Requestor `yagna` daemon
2. Launch Requestor Agent to start an interactive service payload on a VM 
3. Execute a simple ExeScript batch to ensure successful interaction with payload
4. Shutdown the Requestor Daemon
5. Restart the Requestor Daemon
6. Execute a simple ExeScript batch to ensure the Daemon has successfully reconnected

### 1.3. Network disruption
1. Launch & initialize Requestor `yagna` daemon
2. Launch Requestor Agent to start an interactive service payload on a VM 
3. Execute a simple ExeScript batch to ensure successful interaction with payload
4. Disconnect the Requestor machine from network
5. Reconnect the Requestor machine to network
6. Execute a simple ExeScript batch to ensure the Requestor Daemon has successfully reconnected

### 1.4. Requestor Daemon network address change
1. Launch & initialize Requestor `yagna` daemon on machine with IP address A
2. Launch Requestor Agent to start an interactive service payload on a VM 
3. Execute a simple ExeScript batch to ensure successful interaction with payload
4. Shutdown the Requestor Daemon
5. Start the Requestor Daemon with the same node id/identity on a different IP address
6. Execute a simple ExeScript batch to ensure the Daemon has successfully reconnected from a different network address


## 2. Requestor offline ("fire&forget") 

### 2.1. Upfront payment
1. Launch & initialize Requestor `yagna` daemon
2. Launch Requestor Agent to start an interactive service, with upfront payment allowing for `t` seconds of operation
3. Ensure the service runs successfully (eg. by observing logs on Provider)
4. Shutdown the Requestor Agent & Daemon
5. Ensure the service runs successfully on provider
6. Wait `t` seconds
7. Ensure the Provider has terminated the Activity after the budget runs out

### 2.2. Self-sustained payment
1. Launch & initialize Requestor `yagna` daemon
2. Launch Requestor Agent to start an interactive service, with self-sustained payment platform
3. Ensure the service runs successfully (eg. by observing logs on Provider)
4. Shutdown the Requestor Agent & Daemon
5. Ensure the service runs successfully on Provider
6. Restart the Requestor Agent & Daemon
7. Terminate the Activity
 
## 3. Requestor delegates control

### 3.1. Full control delegation
1. Launch & initialize Requestor `yagna` daemons A & B
2. Launch Requestor Agent on Requestor A to start an interactive service, with pay-as-you-go payment scheme
3. Execute a simple ExeScript batch from Requestor A to ensure successful interaction with payload
4. Grant control over the Agreement to Requestor B
5. Launch Requestor Agent on Requestor B 
6. Execute a simple ExeScript batch from Requestor B to ensure successful interaction with payload
7. Terminate the Activity from Requestor B

### 3.2. Partial control delegation
...basically run scenarios as in 3.1., but granting various atomic permissions and validating that respective actions are permitted/forbidden accordingly.

