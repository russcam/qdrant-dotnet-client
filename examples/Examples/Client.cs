using Grpc.Net.Client;
using Qdrant.Grpc;

namespace Examples;

public class Client
{
	public void Create()
	{
		#region CreateClient
		var address = GrpcChannel.ForAddress("http://localhost:6334");
		var client = new QdrantGrpcClient(address);
		#endregion
	}
}
