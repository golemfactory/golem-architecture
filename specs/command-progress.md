# Command progress reporting

Downloading large images and files poses problem for Requestor controlling ExeUnit.
Since he doesn't have any information about Provider's internet bandwidth, there is no
way to estimate download time. Slow internet connection is indistinguishable from Provider
that just doesn't work and is unable to finish task. This results in wasted resources and
non-optimal Requestor behavior.


This specification describes generic mechanisms allowing to report progress of different
commands from ExeUnit to Reqeustors. Moreover, it describes current implementation of ExeUnit.

## Specification

### Capabilities

ExeUnit supporting progress reporting should put following properties in the Offer:
- `golem.activity.caps.transfer.report-progress` - if he can report `transfer` command progress
- `golem.activity.caps.deploy.report-progress` - if he can report `deploy` command progress

To support progress for any other command, specification should be extended with new properties.
Current ExeUnit implementation supports only these 2 commands.

### Progress parameters

To enable progress events Requestor agent has to attach progress parameters to `ExeScriptCommand`.
Example `deploy` and `transfer` command:
```json
[
  {
    "deploy": {
      "progress" : { "updateInterval" : "300ms", "updateStep" : null }
    }
  },
  {
    "transfer": {
      "from": "http://34.244.4.185:8000/LICENSE",
      "to": "container:/input/file_in",
      "progress" : { "updateInterval" : null, "updateStep" : 1048576 }
    }
  }
]
```

`ProgressArgs` structure is described [here](https://golemfactory.github.io/ya-client/index.html?urls.primaryName=Activity%20API#model-ProgressArgs).

### Progress event




### Listening to events 

