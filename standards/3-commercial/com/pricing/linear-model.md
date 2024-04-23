# Linear pricing model

This document describes how to compute the price of a Golem service using
a linear pricing model.

## Properties

The `Linear` model uses the following properties:

- [`golem.com.pricing.model`](model.md#golemcompricingmodel--string) - Setting to `linear` indicates that the linear pricing model is used.
- [`golem.com.pricing.model.linear.coeffs`](model.md#golemcompricingmodellinearcoeffs--listnumber) -
   An array of price coefficients for the linear pricing function.

```
coeffs = [coeff_1, ..., coeff_N, coeff_fixed]
```

- [`golem.com.usage.vector`](../usage.md#golemcomusagevector--liststring) - Array of usage counters for resources consumed by a given activity.

```
counters = [counter_1, ..., counter_N] 
```

In general, the linear pricing model assumes that the total price for an activity is a sum of products of coefficients of a usage array and a coeffs array:

```
price = usage_1 x coeffs_1 + ... + usage_N x coeffs_N + 1 x coeff_fixed.
```

where `n` denotes an index of a particular measure agreed between the provider and the requestor to be used for this agreement and `usage` is the actual resource usage per counter.

## Usage Counters

Usage `counters` are measures of resource consumption during the execution of the task or service. Each ExeUnit implements a specific subset of usage counters and reports their values to the Yagna daemon. It is the Provider's choice to decide which counters he uses to measure the resource consumption and finally calculate the price. Provider provides this information in the offer as the `golem.usage.vector`.

A `golem.com.usage.vector` may be set as i.e. `["golem.usage.cpu_sec",
   "golem.usage.duration_sec"]`
 which indicates that the price will be a function of both the time of CPU and the environment run. The actual price value will depend on the actual coeffs values.

The order of the counters in the `golem.com.usage.vector` is important. The same order is used for the price coefficient array, so the coefficient on position n (`coeff_n`) corresponds to the usage counter on position n (`counter_n`). The provider will report the consumption also using this order.

## Price Coefficients

Price coefficients `golem.com.pricing.model.linear.coeffs` on the other side, contain prices of specific counters declared in the `usage` vector. Notice that the `coeffs` array has one element more than the `counters` vector, which always represents a fixed cost for starting an Activity. These values are decided by the provider and are subject to the agreement.

## Computing cost

Resource usage vector `usage` is reported by the ExeUnit - a part of the code responsible for running activities on the provider node. The ExeUnit is initialized with an Agreement containing the counters vector. During the Activity execution, it reports the vector of usage per counters according to the order found in the counters vector.

Usage is a vector of floating point values (64-bit).

### Price calculation specification

Since reported usage is a floating point vector, price calculation requires a detailed specification to avoid potential inaccuracies due to floating point arithmetic and to allow these calculations to be deterministically verified by the other party (for example Requestor).

For each activity separate `usage` vector is given:

```
activity_X_usage = [usage(counter_1, activity_X), ..., usage(counter_n, activity_X)]

```

Before any calculations are done, all vectors must be converted to decimal representation:

```
activity_X_usage = [decimal(activity_X_usage_1), ..., decimal(activity_X_usage_n)]
```

and:

```
coeffs = [decimal(coeff_1), ..., decimal(coeff_n), decimal(coeff_fixed)]
```

`decimal` function converts 64-bit floating point numbers to string using precision:

**precision = floor(log<sub>10</sub>&nbsp;2<sup>[`MANTISSA_DIGITS`]&nbsp;&minus;&nbsp;1</sup>)**

which is equal to the number of significant digits in a 64-bit floating point number.

The costs for activity X will be then:

**activity_price = [activity_X_usage, 1] X coeffs<sup>T</sup>**

This formula is i.e. used to calculate the costs for debit notes, which are issued independently for each activity.

To compute the price for the whole agreement under each M activities were started, use the following formula:

**agreement_price = [activity_1_usage, 1] X coeffs<sup>T</sup> + ... + [activity__M_usage, 1] X coeffs<sup>T</sup>**
