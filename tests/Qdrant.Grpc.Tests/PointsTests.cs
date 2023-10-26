using Xunit;

namespace Qdrant.Grpc.Tests;

[Collection("Qdrant")]
public class PointsTests
{
	private readonly QdrantGrpcClient _client;

	public PointsTests(QdrantFixture qdrantFixture) => _client = qdrantFixture.CreateGrpcClient();


	[Fact]
	public async Task SearchBatch()
	{
		await CreateAndSeedCollection("collection_1");

		SearchBatchResponse? batchResults = default;

		try
		{
			batchResults = await _client.Points.SearchBatchAsync(new SearchBatchPoints
			{
				CollectionName = "collection_1",
				SearchPoints =
				{
					new SearchPoints { Vector = { 10.4f, 11.4f }, Limit = 1, WithPayload = new WithPayloadSelector { Enable = true }},
					new SearchPoints { Vector = { 3.4f, 4.4f }, Limit = 1, WithPayload = new WithPayloadSelector { Enable = true } }
				}
			});
		}
		catch (Exception e)
		{
			Assert.Fail(e.ToString());
		}

		Assert.Collection(
			batchResults.Result,
			br =>
			{
				var point = Assert.Single(br.Result);

				Assert.Equal(9ul, point.Id);
				var payloadKeyValue = Assert.Single(point.Payload);
				Assert.Equal("foo", payloadKeyValue.Key);
				Assert.Equal("goodbye", payloadKeyValue.Value.StringValue);
			},
			br =>
			{
				var point = Assert.Single(br.Result);

				Assert.Equal(8ul, point.Id);
				var payloadKeyValue = Assert.Single(point.Payload);
				Assert.Equal("foo", payloadKeyValue.Key);
				Assert.Equal("hello", payloadKeyValue.Value.StringValue);
			});
	}

	private async Task CreateAndSeedCollection(string collection)
	{
		await _client.Collections.CreateAsync(new CreateCollection
		{
			CollectionName = collection,
			VectorsConfig = new VectorsConfig { Params = new VectorParams { Size = 2, Distance = Distance.Cosine } }
		});

		await _client.Points.UpsertAsync(
			new UpsertPoints
			{
				CollectionName = collection,
				Points =
				{
					new PointStruct
					{
						Id = 8,
						Vectors = new() { Vector = new() { Data = { 3.5f, 4.5f } } },
						Payload = { ["foo"] = "hello" }
					},
					new PointStruct
					{
						Id = 9,
						Vectors = new() { Vector = new() { Data = { 10.5f, 11.5f } } },
						Payload = { ["foo"] = "goodbye" }
					}
				}
			});
	}
}
