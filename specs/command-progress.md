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

To enable progress events Requestor agent has to attach progress parameters to [ExeScriptCommand](https://golemfactory.github.io/ya-client/?urls.primaryName=Activity%20API#model-RunCommand).
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


### Listening to events 

Events will be available on [getStreamingBatchResults](https://golemfactory.github.io/ya-client/index.html?urls.primaryName=Activity%20API#/requestor_control/getExecBatchResults) endpoint (with `text/event-stream` accept header).
Event structure is defined [here](https://golemfactory.github.io/ya-client/index.html?urls.primaryName=Activity%20API#model-RuntimeEventKindProgress).

Example:
```json
{
  "batch_id": "5c9a8f0e13dd49edb7fa570a10b1b14b",
  "index": 0,
  "timestamp": "2024-02-09T15:12:07.540318580",
  "kind": {
    "progress": {
      "step": [
        0,
        1
      ],
      "message": "Transfer retried",
      "progress": [
        60,
        1024
      ],
      "unit": "Bytes"
    }
  }
}
```

You must check specific ExeUnit documentation to know implementation details not covered
by this specification.

## List of supporting ExeUnits

- [ya-exe-unit](https://github.com/golemfactory/yagna/blob/master/docs/provider/exe-unit/command-progress.md) (`deploy` and `transfer` commands)
- [ya-runtime-ai](https://github.com/golemfactory/ya-runtime-ai) (only `deploy` command)
