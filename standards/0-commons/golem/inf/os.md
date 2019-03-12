# Golem Infrastructural Properties - Operating System
Specifications of operating systems.

## `golem.inf.os.platform : String`
Operating system platform.
### Value enum
|Value| Description |
|---|---|
|"win"|Microsoft Windows|
|"linux"|Linux|
|"macos"|MacOS|
|"android"|Android|

## `golem.inf.os.{platform}.version : Version`
The version of the operating system.

**Note:** Each operating system platform may have different versioning schemes.

## `golem.inf.os.name : String`
Human-readable name of operating system.

**Note:** Each operating system platform may have different naming conventions.

## **Examples**

 ```
# Windows 10 
golem.inf.os.platform="win"
golem.inf.os.name="Windows 10"
golem.inf.os.win.version=v"1803.17134.176"

# Android
golem.inf.os.platform="android"
golem.inf.os.name="Android 9 (Pie)"
golem.inf.os.android.version=v"52.0.A.3.302"
```