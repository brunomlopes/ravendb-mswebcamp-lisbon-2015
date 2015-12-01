using Microsoft.AspNet.Builder;
using RavenDB.MsWebCamp2015.RavenDB;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class RavenDBMiddlewareExtensions
    {
        public static IApplicationBuilder UseRavenDB(this IApplicationBuilder app)
        {
            app.UseMiddleware<RavenDBMiddleware>();
            return app;
        }
    }
}