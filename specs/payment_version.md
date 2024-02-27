### Added possibility of setting version of payment protocol

A new field was added to offer on provider side and demand on requestor side
golem.com.payment.protocol.version
There is only one possible value for now: 2

```golem.com.payment.protocol.version = 2```

This field is validate against constraint in the requestor demand (it is added by default by yagna):

```"golem.com.payment.protocol.version" > 1```

That means that requestor with version 2 will only talk with new providers.

It was needed because old providers cannot confirm payments done by new payment driver (multi-payments).


