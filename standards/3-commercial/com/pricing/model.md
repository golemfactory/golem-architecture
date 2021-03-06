# Pricing Models
This namespace defines **pricing models** for Golem computation resources. 

## `golem.com.pricing.model : String`

### Describes: Offer

Type of pricing function describing the pricing model.

**Important:** The pricing models described by this namespace meet one fundamental condition: they need to be expressed by a 'pricing function':
```
price = p(usage_vector)
```
where `usage_vector` is a vector of usage counter values (numbers) accumulated during Activity execution, and `price` is a scalar value.

The `p(u)` can be ultimately any function, however this namespace defines a set of sub-classes of relatively **simple yet useful** pricing functions which can be described by concise sets of attributes/parameters. 

### Value enum
| Value    | Description                                       |
| -------- | ------------------------------------------------- |
| "linear" | Price is a linear function of vector of counters. |
| ...      | ...                                               |


## `golem.com.pricing.model.linear.coeffs : List[Number]`

### Describes: Offer

Property to express coefficients for the linear pricing function.

A linear pricing function is a function of following form:

**price = [counter_1, ..., counter_n, 1] X [coeff_1, ..., coeff_n, coeff_fixed]<sup>T</sup>**

To define a linear pricing function of n-counter usage vector, it is required to specify the numeric coefficients to be multiplied by respective counter values of a usage vector, plus a fixed coefficient. 

### **Examples**
* `golem.com.pricing.model="linear"` - Declares a linear pricing model (with prameters indicated via `golem.com.pricing.model.linear.*` properties).
* `golem.com.pricing.model.linear.coeffs=[0.3, 0]` - For a 1-counter usage vector (eg. `golem.usage.vector=["golem.usage.duration_sec"]`) the price is calculated as: `price = 0.3 * golem.usage.duration_sec`.

