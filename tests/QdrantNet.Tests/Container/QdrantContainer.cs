using DotNet.Testcontainers.Containers;
using Microsoft.Extensions.Logging;

namespace QdrantNet.Tests.Container;

public class QdrantContainer : DockerContainer
{
    public QdrantContainer(QdrantConfiguration configuration, ILogger logger) : base(configuration, logger)
    {
    }
}