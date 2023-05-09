using FluentAssertions;
using Grpc.Net.Client;
using Qdrant;
using Xunit;

namespace Qdrant.Grpc.Tests;

[Collection("Qdrant")]
public class CollectionTests
{
	private readonly QdrantGrpcClient _client;

	public CollectionTests(QdrantFixture qdrantFixture)
	{
		var address = GrpcChannel.ForAddress($"http://{qdrantFixture.Host}:{qdrantFixture.GrpcPort}");
		_client = new QdrantGrpcClient(address);
	}

	[Fact]
	public void CanCreateCollection()
	{
		var response = _client.Collections.Create(new CreateCollection
		{
			CollectionName = nameof(CanCreateCollection),
			VectorsConfig = new VectorsConfig
			{
				Params = new VectorParams { Size = 4, Distance = Distance.Cosine }
			}
		});

		response.Result.Should().BeTrue();
		response.Time.Should().BeGreaterThan(0);
	}
}
