# Computation Manifest 
This namespace defines properties used to specify the Golem Computation Manifest (as originally designed in [GAP-4](https://github.com/golemfactory/golem-architecture/blob/master/gaps/gap-4_comp_manifest/gap-4_comp_manifest.md)).

## Computation Manifest Example

```json
{
  "golem.srv.comp.manifest.script.match": "regex",
  "golem.srv.comp.manifest.script.commands": [
    "run /bin/cat /etc/motd",
    "{\"run\": { \"args\": \"/bin/date -R\", \"env\": { \"MYVAR\": \"42\", \"match\": \"strict\" }}}"
  ],
  "golem.srv.comp.manifest.net.inet.out.protocols": [
    "http",
    "https"
  ],
  "golem.srv.comp.manifest.net.inet.out.urls": [
    "http://golemfactory.s3.amazonaws.com/file1",
    "http://golemfactory.s3.amazonaws.com/file2"
  ]
}
```

### Object representation

1. JSON

```json
{
    "script": {
    "match": "regex",
    "commands": [
        "run /bin/cat /etc/motd",
        {
        "run": {
            "args": "/bin/date -R",
            "env": {
            "MYVAR": "42"
            },
            "match": "strict"
        }
        }
    ]
    },
    "net": {
    "inet": {
        "out": {
        "protocols": [
            "http",
            "https"
        ],
        "urls": [
            "http://golemfactory.s3.amazonaws.com/file1",
            "http://golemfactory.s3.amazonaws.com/file2"
        ]
        }
    }
    }
}
```

2. Imploded JSON (optional)

```json
{
    "script.match": "regex",
    "script.commands": [
    "run /bin/cat /etc/motd",
    {
        "run": {
        "args": "/bin/date -R",
        "env": {
            "MYVAR": "42"
        },
        "match": "strict"
        }
    }
    ],
    "net.inet.out.protocols": [
    "http",
    "https"
    ],
    "net.inet.out.urls": [
    "http://golemfactory.s3.amazonaws.com/file1",
    "http://golemfactory.s3.amazonaws.com/file2"
    ]
}
```

3. YAML

```yaml
---
script:
    match: regex
    commands:
    - run /bin/cat /etc/motd
    - run:
        args: "/bin/date -R"
        env:
        MYVAR: '42'
        match: strict
net:
    inet:
    out:
        protocols:
        - http
        - https
        urls:
        - http://golemfactory.s3.amazonaws.com/file1
        - http://golemfactory.s3.amazonaws.com/file2
```

## Common Properties

N/A

## Specific Properties

## `golem.srv.comp.manifest.version : String` 

### Describes: Demand

Specifies a version (Semantic Versioning 2.0 specification) of the manifest, **defaults** to "0.1.0"

### **Examples**
* `golem.srv.comp.manifest.version="0.1.0"` - The manifest version is 0.1.0.

