using Grpc.Core;
using Grpc.Net.Client;
using Qdrant.Grpc;

namespace Examples;

public class Client
{
	public void Create()
	{
		#region CreateClient
		var channel = GrpcChannel.ForAddress("http://localhost:6334");
		var client = new QdrantGrpcClient(channel);
		#endregion
	}

	public void CreateWithApiKey()
	{
		#region CreateClientWithApiKey
		var channel = QdrantChannel.ForAddress("http://localhost:6334", new ClientConfiguration
		{
			ApiKey = "<api key>"
		});
		var client = new QdrantGrpcClient(channel);
		#endregion
	}

	public void CreateWithApiKeyAndSelfSignedCert()
	{
		#region CreateClientWithApiKeyAndSelfSignedCert
		var channel = QdrantChannel.ForAddress("https://localhost:6334", new ClientConfiguration
		{
			ApiKey = "<api key>",
			CertificateThumbprint = "<certificate thumbprint>"
		});
		var client = new QdrantGrpcClient(channel);
		#endregion
	}
}
