using System;
using System.Diagnostics;
using Moq;

namespace Nest.Indexify.Tests
{
    public class ElasticClientContext : IDisposable
    {
        private readonly Mock<IConnectionSettingsValues> _mockSettings;
        private readonly Mock<IElasticClient> _mockClient;

        public ICreateIndexRequest IndexCreateRequest { get; private set; }
        
        public Mock<IElasticClient> Client
        {
            get { return _mockClient; }
        }
        
        public Mock<IConnectionSettingsValues> Settings
        {
            get { return _mockSettings; }
        }

        public ElasticClientContext()
        {
            Debug.WriteLine("Setup ElasticClientContext");
            _mockSettings = new Mock<IConnectionSettingsValues>();
            _mockClient = SetupClientCore(new Mock<IElasticClient>());
        }

        private Mock<IElasticClient> SetupClientCore(Mock<IElasticClient> clientMock)
        {
            clientMock.SetupGet(s => s.Infer).Returns(new ElasticInferrer(_mockSettings.Object));
            clientMock.Setup(s => s.CreateIndex(It.IsAny<Func<CreateIndexDescriptor, CreateIndexDescriptor>>())).Callback((Func<CreateIndexDescriptor, CreateIndexDescriptor> cb) =>
            {
                IndexCreateRequest = cb(new CreateIndexDescriptor(Settings.Object));
            });

            clientMock.Setup(s => s.CreateIndexAsync(It.IsAny<Func<CreateIndexDescriptor, CreateIndexDescriptor>>())).Callback((Func<CreateIndexDescriptor, CreateIndexDescriptor> cb) =>
            {
                IndexCreateRequest = cb(new CreateIndexDescriptor(Settings.Object));
            });

            return SetupClient(clientMock);
        }

        protected virtual Mock<IElasticClient> SetupClient(Mock<IElasticClient> clientMock)
        {
            return clientMock;
        }

        public void Dispose()
        {
            Debug.WriteLine("Dispose ElasticClientContext ({0})", GetHashCode());
        }
    }
}