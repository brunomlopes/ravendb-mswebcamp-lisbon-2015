using Microsoft.Extensions.Configuration;
using Raven.Client;
using Raven.Client.Document;

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

                return new DocumentStore
                {
                    Url = url,
                    ApiKey = apiKey
                }.Initialize();
            });

            collection.AddScoped(p => p.GetService<IDocumentStore>().OpenSession());
            collection.AddScoped(p => p.GetService<IDocumentStore>().OpenAsyncSession());

            return collection;
        }
    }
}