# .NET client for Qdrant vector database

A .NET client for [Qdrant vector database](https://qdrant.tech/).

The client currently supports only gRPC services.

## Getting started

### Installing

```sh
dotnet add package QdrantNet --version 1.0.0-alpha1
```

### Usage

```csharp
using QdrantNet;

public class Main
{
    public void Main()
    {
        var address = GrpcChannel.ForAddress("http://localhost:6334");
        var client = new QdrantGrpcClient(address);
        var healthResponse = client.Qdrant.HealthCheck(new HealthCheckRequest());
    }
}
```