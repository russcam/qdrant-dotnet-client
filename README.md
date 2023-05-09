# .NET gRPC client for Qdrant vector database

A .NET gRPC client for [Qdrant vector database](https://qdrant.tech/).

## Getting started

### Installing

```sh
dotnet add package Qdrant.Grpc --version 1.0.0-alpha1
```

### Usage

The `QdrantGrpcClient` provides an entry point to interact with all of 
qdrant's gRPC services

```csharp
using Qdrant.Grpc;

public class Program
{
    public static void Main()
    {
        var address = GrpcChannel.ForAddress("http://localhost:6334");
        var client = new QdrantGrpcClient(address);
        
        // check qdrant is healthy
        var healthResponse = client.Qdrant.HealthCheck(new HealthCheckRequest());
        
        // create a collection
        var collectionOperationResponse = client.Collections.Create(new CreateCollection
        {
            CollectionName = "my_collection",
            VectorsConfig = new VectorsConfig
            {
                Params = new VectorParams
                { 
                    Size = 4,
                    Distance = Distance.Cosine
                }
            }
        });
    }
}
```