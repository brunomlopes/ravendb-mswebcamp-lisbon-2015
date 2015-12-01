using Microsoft.AspNet.Mvc;
using Raven.Client;
using RavenDB.MsWebCamp2015.Models;

namespace RavenDB.MsWebCamp2015.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDocumentSession _session;

        public HomeController(IDocumentSession session)
        {
            _session = session;
        }

        public IActionResult Index()
        {
            var config = _session.Load<SiteConfig>(SiteConfig.SiteConfigId);
            return View(config);
        }
    }
}
