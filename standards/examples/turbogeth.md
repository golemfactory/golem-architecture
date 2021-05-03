## Turbogeth managed service

This article contains examples of Demands & Offers which cover the Managed Service example (turbogeth).

Compared to Standards v0.6.0, the sample includes a couple of extensions:

### Quasi-Perpetual vs Perpetual Agreement

The Agreement terms must be able to support Agreements enabling long-running services. This is governed by `golem.srv.comp.expiration` property, which indicates the demanded 'lifetime' of the Agreement. There are two approaches to enable long-term agreements:

- (Tactical) Set `golem.srv.comp.expiration` far in the future (like 20 years :) ) to make sure Agreement does not expire until it is no longer required - this is already supported by Golem Standards v0.6.x.
- (Strategic) No `golem.srv.comp.expiration` property specified on the Demand side indicates no expected Agreement end date.

### "Eth" Application Namespace

Any "Ethereum-specific" properties would be maintained in this namespace. This sample includes:

`golem.srv.app.eth.rpc-port` - Indication of RPC endpoint port exposed by the provisioned Geth service.

### Managed Turbogeth runtime

`golem.runtime.name=turbogeth-managed` - Indicated a Managed Turbogeth service to be launched

### PAYU payment scheme enhancements

Two negotiable properties to determine Debit Note and Payment intervals:

`golem.com.scheme.payu.debit-notes.interval-sec?` - Debit Notes issue interval
`golem.com.scheme.payu.payment-interval-sec?` - Expects Payments to be made within specific interval after Debit Note is issued

### Sample Offer

```
"properties": {
    "golem.com.payment.platform.erc20-mainnet-glm.address": "0x128282ae0ba38689f215cb3cc278008620f50253",
    "golem.com.payment.platform.zksync-mainnet-glm.address": "0x128282ae0ba38689f215cb3cc278008620f50253",
    "golem.com.pricing.model": "linear",
    "golem.com.pricing.model.linear.coeffs": [
        0.0002777777777777778,
        0.001388888888888889,
        0.0
    ],
    "golem.com.scheme": "payu",
    "golem.com.scheme.payu.debit-notes.interval-sec?": 60, // Debit Notes issues every minute
    "golem.com.scheme.payu.payment-interval-sec?": 3600,  // Expects Payments to be made within 1 hour 
                                                          // of Debit Note issue (this is plausible, 
                                                          // as typical geth operation time would be days)
    "golem.com.usage.vector": [
        "golem.usage.duration-sec",
        "golem.usage.cpu-sec",
        "golem.usage.gib",
        "golem.usage.storage-gib"
    ],
    "golem.inf.cpu.cores": 3,
    "golem.inf.mem.gib": 16,
    "golem.inf.storage.gib": 2048,
    "golem.node.id.name": "provider_2",
    "golem.runtime.name": "turbogeth-managed",  // indicates the "managed"/"wrapped" turbogeth ExeUnit
    "golem.runtime.version": "0.2.1",
    },
},
"constraints": "()"
```

### Sample Demand

```
"properties": {
    "golem.node.id.name": "test1",
    "golem.srv.app.eth.rpc-port": 9001,  // Indicates requested RPC endpoint port number
},
constraints: "(&
    (golem.runtime.name=turbogeth-managed)
    (golem.inf.mem.gib>=16)
    (golem.inf.storage.gib>=1024)
    (golem.com.pricing.model=linear)
    )"
```
