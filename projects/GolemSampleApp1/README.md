# Golem Sample Applications

These applications are illustrating the basic flow of Market API->Activity API->Payment API in Golem Network.
The can be run against the GolemCLientMock module (the "TestBed") on one machine. They are designed to work in pairs, ie. run both Requestor application and Provider applicatin simultaneously to observe the Requestor-Provider interaction via the Golem APIs.

## Golem Provider Sample

```
dotnet run -p GolemSampleProvider1
```

## Golem Requestor Sample

```
dotnet run -p GolemSampleApp1
```