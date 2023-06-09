# .NET gRPC client for Qdrant vector database

[![NuGet Release][QdrantGrpc-image]][QdrantGrpc-nuget-url]
[![Build Status](https://img.shields.io/endpoint.svg?url=https%3A%2F%2Factions-badge.atrox.dev%2Frusscam%2Fqdrant-dotnet-client%2Fbadge%3Fref%3Dmain&style=flat)](https://actions-badge.atrox.dev/russcam/qdrant-dotnet-client/goto?ref=main)
[![Documentation][ElasticApm-image]][Documentation-url]

A .NET gRPC client for [Qdrant vector database](https://qdrant.tech/).

## Getting started

### Installing

```sh
dotnet add package Qdrant.Grpc
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

[Documentation-url]:https://forloop.co.uk/qdrant-dotnet-client/
[ElasticApm-image]:
https://img.shields.io/badge/Documentation-blue

[QdrantGrpc-nuget-url]:https://www.nuget.org/packages/Qdrant.Grpc/
[QdrantGrpc-image]:
https://img.shields.io/nuget/v/Qdrant.Grpc.svg