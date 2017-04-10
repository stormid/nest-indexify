using System;
using System.Diagnostics;
using System.Threading;
using Elasticsearch.Net;
using Moq;

namespace Nest.Indexify.Tests
{
    public class ElasticClientContext : IDisposable
    {
        public ICreateIndexRequest IndexCreateRequest { get; private set; }
        
        public IElasticClient Client { get; }

        public IConnection Connection { get; }

        public Mock<IConnectionSettingsValues> Settings { get; }

        public ElasticClientContext()
        {
            Debug.WriteLine("Setup ElasticClientContext");
            Settings = new Mock<IConnectionSettingsValues>();
            Connection = new InMemoryConnection();
            // Client = SetupClientCore(new Mock<IElasticClient>());

            Client = SetupClientCore(Connection);
        }

        private IElasticClient SetupClientCore(IConnection connection)
        {
            //clientMock.SetupGet(s => s.Infer).Returns(new Inferrer(Settings.Object));
            //clientMock.SetupGet(s => s.ConnectionSettings).Returns(() => new ConnectionSettings(new SingleNodeConnectionPool(new Uri("http://localhost:9200")), new InMemoryConnection()));
            ////clientMock.Setup(s => s.CreateIndex(It.IsAny<IndexName>(), It.IsAny<Func<CreateIndexDescriptor, ICreateIndexRequest>>())).Callback((IndexName indexName, Func<CreateIndexDescriptor, ICreateIndexRequest> cb) =>
            ////{
            ////    IndexCreateRequest = cb(new CreateIndexDescriptor(Settings.Object.DefaultIndex));
            ////}).Returns(new CreateIndexResponse());

            ////clientMock.Setup(s => s.CreateIndexAsync(It.IsAny<IndexName>(), It.IsAny<Func<CreateIndexDescriptor, ICreateIndexRequest>>(), It.IsAny<CancellationToken>())).Callback((IndexName indexName, Func<CreateIndexDescriptor, ICreateIndexRequest> cb) =>
            ////{
            ////    IndexCreateRequest = cb(new CreateIndexDescriptor(Settings.Object.DefaultIndex));
            ////}).ReturnsAsync(new CreateIndexResponse());

            return SetupClient(connection);
        }

        protected virtual IElasticClient SetupClient(IConnection connection)
        {
            return new ElasticClient(new ConnectionSettings(new SingleNodeConnectionPool(new Uri("http://localhost:9200")), connection));
        }

        public void Dispose()
        {
            Debug.WriteLine("Dispose ElasticClientContext ({0})", GetHashCode());
        }
    }
}