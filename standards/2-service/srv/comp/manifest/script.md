# Computation Manifest Script namespace
This namespace defines properties used to specify details the Golem Computation Manifest ExeScript allowance. Defines a set of allowed ExeScript commands and applies constraints to their arguments. 

## Common Properties

## Specific Properties

## `golem.srv.comp.manifest.script.commands : List[String]` 

### Describes: Demand

Specifies a curated list of commands in form of:

- UTF-8 encoded strings

    No command context or matching mode need to be specified.

    E.g. `["run /bin/cat /etc/motd", "run /bin/date -R"]`

- UTF-8 encoded JSON strings

    Command context (e.g. `env`) or argument matching mode need to be
    specified for a command.

    E.g. `["{\"run\": { \"args\": \"/bin/date -R\", \"env\": { \"MYVAR\": \"42\", \"match\": \"strict\" }}}"]`

- mix of both

`"deploy"`, `"start"` and `"terminate'` commands are always allowed.
These values become the **default** if no `manifest.script.command` property
has been set in the Demand, but the `manifest` namespace is present.

### **Examples**
* `golem.srv.comp.manifest.script.commands=[
    "run /bin/cat /etc/motd",
    "{\"run\": { \"args\": \"/bin/date -R\", \"env\": { \"MYVAR\": \"42\", \"match\": \"strict\" }}}"
  ]` 


## `golem.srv.comp.manifest.script.match : String` 

### Describes: Demand

Selects a default way of comparing command arguments stated in the manifest
and the ones received in the ExeScript, unless stated otherwise in a
command JSON object.

### Value enum
|Value| Description |
|---|---|
|`strict`| byte-to-byte argument equality (**default**) |
|`regex`| treat arguments as regular expressions |

`regex` syntax: Perl-compatible regular expressions (UTF-8 Unicode mode),
w/o the support for look around and backreferences (among others);
for more information read the documentation of the Rust
[regex](https://docs.rs/regex/latest/regex/) crate.

### **Examples**
* `golem.srv.comp.manifest.script.match="regex"` - The manifest version is 0.1.0.
