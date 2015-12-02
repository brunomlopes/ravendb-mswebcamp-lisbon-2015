using Microsoft.Extensions.Configuration;
using Raven.Client;
using Raven.Client.Document;
using Raven.Client.Indexes;
using RavenDB.MsWebCamp2015.Indexes;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class RavenDBServiceCollectionExtensions
    {
        public static IServiceCollection AddRavenDB(this IServiceCollection collection, string urlKey = "RavenDb:Url", string apiKeyKey = "RavenDb:ApiKey")
        {
            collection.AddSingleton(p =>
            {
                var configuration = p.GetService<IConfiguration>();
                var url = configuration[urlKey];
                var apiKey = configuration[apiKeyKey];

                var documentStore = new DocumentStore
                {
                    Url = url,
                    ApiKey = apiKey
                }.Initialize();

                IndexCreation.CreateIndexes(typeof(Speakers_PerTags).Assembly, documentStore);
                return documentStore;
            });

            collection.AddScoped(p => p.GetService<IDocumentStore>().OpenSession());
            collection.AddScoped(p => p.GetService<IDocumentStore>().OpenAsyncSession());

            return collection;
        }
    }
}