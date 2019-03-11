# Pricing Models
Pricing Models

## `golem.com.pricing.model : String`
Type of pricing function describing the pricing model.

### Value enum
|Value| Description |
|---|---|
|"linear"| Price is a linear function of vector of counters. |
|"stepped"| Price is specified for... |
|"fixed"| Fized price for one-off. |


## `golem.com.pricing.model.linear.coeffs : List of Number`
Property to express coefficients for the linear pricing function.

A linear pricing function is a function of following form:

**price = [counter_1, ..., counter_n, 1] X [coeff_1, ..., coeff_n, coeff_fixed]<sup>T</sup>**

To define a linear pricing function of n-counter usage vector, it is required to specify the numeric coefficients to be multiplied by respective counter values of a usage vector, plus a fixed coefficient. 

### Sample values
* `golem.com.pricing.model.linear.coeffs=[0.3, 0]` - For a 1-counter usage vector (eg. `golem.usage.vector=["golem.usage.duration_sec"]`) the price is calculated as: `price = 0.3*golem.usage.duration_sec`.

# Sample property block
```
golem.com.pricing....
```