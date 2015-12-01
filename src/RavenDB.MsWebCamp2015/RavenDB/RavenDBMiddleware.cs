using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Http;
using Raven.Client;
using Microsoft.Extensions.DependencyInjection;

namespace RavenDB.MsWebCamp2015.RavenDB
{
    public class RavenDBMiddleware
    {
        private readonly RequestDelegate _next;

        public RavenDBMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var session = context.RequestServices.GetService<IDocumentSession>();
            var asyncSession = context.RequestServices.GetService<IAsyncDocumentSession>();
            try
            {
                await _next(context);
                session.SaveChanges();
                await asyncSession.SaveChangesAsync();
            }
            finally 
            {
                session.Dispose();
                asyncSession.Dispose();
            }
        }
    }
}