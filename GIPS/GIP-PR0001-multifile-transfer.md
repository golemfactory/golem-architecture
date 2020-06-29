
| | |
|---|---|
|title | Multi file transfer extension | 
|author|  Przemysław Rekucki <prekucki@golem.network>
|category| exe-unit/transports |
|status| Draft |
|created| 2020-06-29 |

# Abstract

This document describes a extension for `transfer` command for transfering sets of files.

# Motivation

There are many situations in which sending a large number of single `transfer` operations for individual files is inconvenient.

In particular when:
- we do not know the exact names of the output files (e.g. we want to download everything that went to the results directory).
  Now it requires downloading the file list and ordering transfers or executing `run` to create a zip file inside the container with the entire contents of the catalog and ordering the transfer.
- ...


# Specification

### Transfer Command

New transfer command arguments:

- `format` (`str`) - describes format for encoding/decoding directory structure.
- `depth` (`int?`) - defines recursion level:
  - `0` - no recursion subdirectories shold be skiped.
  - `null` (or not set) infinite recursion.
- `fileset`: (`object|[object]|?`) - defines filtering rule for encoded/decoded files.

Format for `fileset`:

- `desc` (str?) optional human readable annotation. should not be interpreted by __exe-unit__
- `includes` (`str|[str]|?`) - optional pattern or list of patterns. if not set if matches any file
- `excludes` (`str|[str]|?`) - optiobal pattern or list of patterns for file exclusion.

Pattern format:

ANT compatible pattern (see [ANT Patterns](http://ant.apache.org/manual/dirtasks.html#patterns))

1. char `*` matches zero or more characters  (excluding path separator).
2. char `?` matches one character (excluding path separator).
3. chars `**` matches zero or more characters (including path separator)

### Proposed formats

In standart implementation we expect to have following codecs:

- `zip`
- `tar.gz` - tar with gzip compression
- `tar.xz` - tar with lzma compression.
- `zip.0` - zip store only compression.


### Examples

Current flow

```json
[
    ...
    {"transfer" {
            "from": "http://some-site/data.zip",
            "to": "container:/app//in/data.zip"
        }
    },
    {"run" {
            "entry_point": "/bin/7z",
            "args" ["x", "/app/in/data.zip"]
        }
    }
    ....
]
```

Could be replaced with:

```json
[
    ...
    {"transfer" {
            "from": "http://some-site/data.zip",
            "to": "container:/app//in/",
            "format": "zip"
        }
    }
    ....
]
```
