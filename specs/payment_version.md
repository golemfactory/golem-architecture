# Payment protocol version

## Original protocol (version < 2)

### Payment transaction validation

Payment driver validates transaction in following way
1. Provider receives payment confirmation from requestor (with specific transaction id)
2. It downloads transaction information from blockchain.
3. It parses logs from transaction to find out if sender address matches requestor address and if value is correct.
One transfer in one transaction is supported.

## Introduction of payment protocol version (>=2)

Both provider and requestor should specify property `golem.com.payment.protocol.version`
It has to be included in offer and in demand.

Requestor willing to send multi-payment transactions should choose only providers with version >= 2.

This can be achieved by setting constraint in demand:

```"golem.com.payment.protocol.version" > 1```


### Why

1. Provider is unable to validate transactions made via contract.

### Requirements

Payment driver validates transaction in following way
1. Provider receives payment confirmation from requestor (with specific transaction id)
2. It downloads transaction information from blockchain.
3. It parses logs from transaction to find out if sender address matches requestor address and if value is correct.
Multiple transfers in one transaction are supported.

   