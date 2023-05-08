using Grpc.Core;
using Qdrant;

namespace QdrantNet;

/// <summary>
/// gRPC client for qdrant vector database
/// </summary>
public class QdrantGrpcClient
{
    private readonly CallInvoker _callInvoker;

    /// <summary>Creates a new client for Qdrant</summary>
    /// <param name="channel">The channel to use to make remote calls.</param>
    public QdrantGrpcClient(ChannelBase channel) : this(channel.CreateCallInvoker())
    {
    }

    /// <summary>Creates a new client for Qdrant that uses a custom <c>CallInvoker</c>.</summary>
    /// <param name="callInvoker">The callInvoker to use to make remote calls.</param>
    public QdrantGrpcClient(CallInvoker callInvoker) => _callInvoker = callInvoker;

    /// <summary>
    /// Gets the client for Qdrant services
    /// </summary>
    public Qdrant.Qdrant.QdrantClient Qdrant => new(_callInvoker);

    /// <summary>
    /// Gets the client for Points
    /// </summary>
    public Points.PointsClient Points => new(_callInvoker);

    /// <summary>
    /// Gets the client for Collections
    /// </summary>
    public Collections.CollectionsClient Collections => new(_callInvoker);

    /// <summary>
    /// Gets the client for Snapshots
    /// </summary>
    public Snapshots.SnapshotsClient Snapshots => new(_callInvoker);
}
