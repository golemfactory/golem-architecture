# Golem Timeline-related Properties 
Namespace defining time-related contract aspects of a Golem service. This namespace shall include properties and concepts similar to eg. futures and forwards. Imagine:
  - Demand for resource to be available starting from time `T1`. 
  - Demand for resource (eg. storage) to be available for at least `t` days. 
  - Describe a service which takes eg. `h` hours to be available after request (eg. similar to AWS Glacier where cheap archiving is available but a restore will be returned after 3 days).
