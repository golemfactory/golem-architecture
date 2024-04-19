# Linear pricing model

This document describes how to compute the price of a Golem service using
a linear pricing model.

## Properties

Linear model uses the following properties:
- [`golem.com.pricing.model`](model.md#golemcompricingmodel--string) - Setting to "linear"
  indicates that the linear pricing model is used.
- [`golem.com.pricing.model.linear.coeffs`](model.md#golemcompricingmodellinearcoeffs--listnumber) - 
  Price coefficients for the linear pricing function.
- [`golem.usage.vector`](../usage.md#golemcomusagevector--liststring) - List of usage counters to be reported by Exeunit.


## Usage Counters

Usage counters are measure of resources consumption during the execution of the task or service.
Each ExeUnit implements chosen subset of usage counters and reports their values to Yagna daemon.
It is Provider's choice to decide which counters are used and how much he wants to charge.

`golem.usage.vector` contains ordered list of names of usage counters. Usage coefficients
`golem.com.pricing.model.linear.coeffs` on the other side, contains prices of specific counters
chosen in usage vector.

```
coeffs = [coeff_1, ..., coeff_n, coeff_fixed]
counters = [counter_1, ..., counter_n]
```

Coefficient on position n (`coeff_n`) corresponds to usage counter on position n (`counter_n`).

Notice that `coeffs` vector has one element more than `counters` vector, which represents fixed cost
per starting Activity.

## Computing cost

ExeUnit is part of code responsible for reporting counters. It is initialized with Agreement
containing `counters` vector and during Activity execution reports vector of usage counters
according to order found in `counters`.

```
usage = [usage(counter_1), ..., usage(counter_n)]
```
Usage is vector of floating point values (64 bit).

### Price calculation specification

Since reported usage is floating point vector, price calculation requires detailed specification
to avoid potential inaccuracies due to floating point arithmetic and to allow these calculations
to be deterministically verified by other party (for example Requestor).

For each activity separate usage vector is given:
```
activity_usage_1 = [usage(counter_1, activity_1), ..., usage(counter_n, activity_1)]
...
activity_usage_n = [usage(counter_1, activity_n), ..., usage(counter_n, activity_n)]
```



