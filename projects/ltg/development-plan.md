# Software Development Plan

## Introduction

This document describes...

To change this document...

## Goals and Scope

### Software Description

The software will be used to...

| Phase | Features |
|--|--|
| Minimum Viable Product | ... |
| 2nd Phase | ... |

### Artifacts

| Platform | Artifacts |
|--|--|
| Linux | Debian/Ubuntu Package (.deb) |
| macOS | Apple Disk Image (.dmg) |
| Windows | Windows Installer (.msi) |

## Process

### Planning

### Specification

### Working on a Git Branch

### Pull Request

### Automatic Testing

### Code Review

### Code Merging

### Automatic Builds

## Source Code Requirements

### Technology Stack

The programming language used in this project will be Rust (https://www.rust-lang.org/).

### Supported Platforms

All code should compile and run on Linux, macOS and Windows. The main development platform is Ubuntu Linux, but all code should be portable. E.g. instead of using "/tmp", use `std::env::temp_dir()` function; instead of using platform-native functions, use `std::env::current_exe()` to find the path of the current executable.

### Coding Standard

Rust coding style guidelines:

https://doc.rust-lang.org/1.0.0/style/README.html

To enforce formatting, code should be formatted using rustfmt tool (https://github.com/rust-lang/rustfmt).
To install it, run `rustup component add rustfmt` command. To format files in the working dictory, please run `cargo fmt` command.

### Code Repositories

Most Rust crates used in the project should be located in one repository.
This repository should contain a subdirectory with a `Cargo.toml` file and a `src` directory for every crate.

### Usage Examples

If some create need usage examples, they should be placed in the `examples` subdirectory of the crate. To run such example, 
please run `cargo run --example <example name>` command.

### Tests

Tests can be run using `cargo test` command:

https://doc.rust-lang.org/book/ch11-01-writing-tests.html

To create a test, open a `tests` module prefixed with `#[cfg(test)]`, add test functions and prefix them with `#[cfg(test)]`.

### Documentation

Documentation will be automatically generated using rustdoc:

https://doc.rust-lang.org/rustdoc/index.html

To generate documentation, please enter `cargo doc` command in your shell.

The comments that are copied to the documentation are prefixed with `///`, Markdown is supported.

### Other Teams and Technologies

Other teams in the company: ... Technologies used and provided by them: ...
