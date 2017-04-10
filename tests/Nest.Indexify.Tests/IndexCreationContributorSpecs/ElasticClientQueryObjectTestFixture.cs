using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Elasticsearch.Net;
using FluentAssertions;

namespace Nest.Indexify.Tests.IndexCreationContributorSpecs
{
    public class ElasticClientQueryObjectTestFixture : IDisposable
    {
        public class CallDetails
        {
            public string HttpMethod { get; set; }
            public Uri Uri { get; set; }
        }

        public IConnection Connection { get; private set; }
        public IConnectionSettingsValues ConnectionSettings { get; private set; }

        public Uri HostUri { get; }
        public string DefaultIndex { get; }

        public IList<CallDetails> ApiCallsList = new List<CallDetails>();

        public IElasticClient Client { get; private set; }

        public ElasticClientQueryObjectTestFixture() : this("my-application") { }

        private void SetupElasticClient()
        {
            Connection = new InMemoryConnection();
            ConnectionSettings = new ConnectionSettings(new SingleNodeConnectionPool(HostUri), Connection)
                .DefaultIndex(DefaultIndex)
                .DisableDirectStreaming()
                .OnRequestCompleted(details =>
                {
                    ApiCallsList.Add(new CallDetails
                    {
                        Uri = details.Uri,
                        HttpMethod = details.HttpMethod.GetStringValue()
                    });
                });

            Client = new ElasticClient(ConnectionSettings);
        }

        protected ElasticClientQueryObjectTestFixture(string defaultIndex, string uri = null)
        {
            HostUri = new Uri(uri ?? "http://localhost:9200");
            DefaultIndex = defaultIndex;

            SetupElasticClient();
        }

        public T DeserializeRequestBody<T>(IApiCallDetails apiCall) where T : class
        {
            return Deserialize<T>(apiCall.RequestBodyInBytes);
        }

        public T Deserialize<T>(byte[] bytes) where T : class
        {
            var s = ConnectionSettings.SerializerFactory.Create(ConnectionSettings);
            var stream = new MemoryStream(bytes);
            if (stream.CanRead && stream.CanSeek)
            {
                stream.Seek(0, SeekOrigin.Begin);
                return s.Deserialize<T>(stream);
            }
            return null;
        }

        public void ShouldUseHttpMethod(string method, int requestIndex = 0)
        {
            ApiCallsList.Count.Should().BeGreaterOrEqualTo(requestIndex + 1);
            ApiCallsList.ElementAt(requestIndex).HttpMethod.Should().Be(method);
        }

        public void ShouldUseUri(Uri uri, int requestIndex = 0)
        {
            ApiCallsList.Count.Should().BeGreaterOrEqualTo(requestIndex + 1);
            ApiCallsList.ElementAt(requestIndex).Uri.Should().Be(uri);
        }

        public virtual void Dispose()
        {
            ApiCallsList.Clear();
        }
    }
}