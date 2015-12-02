using System;
using System.Linq;
using Bogus;
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

        public IActionResult NewSpeaker()
        {
            var newSpeaker = Speaker.CreateRandom();
            _session.Store(newSpeaker);
            return Json(newSpeaker);
        }

        public IActionResult NewSpeakers(int count = 10)
        {
            var newSpeakers = Speaker.CreateRandom(count).ToList();
            foreach (var speaker in newSpeakers)
            {
                _session.Store(speaker);
            }
            return Json(newSpeakers);
        }
    }
}
