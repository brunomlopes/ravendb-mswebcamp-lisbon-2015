using System;
using System.Linq;
using Bogus;
using Microsoft.AspNet.Mvc;
using Raven.Client;
using RavenDB.MsWebCamp2015.Models;
using Raven.Client.Linq;
using RavenDB.MsWebCamp2015.Indexes;

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

        public IActionResult Speakers(string tag = null)
        {
            var query = _session.Query<Speaker>();
            if (!string.IsNullOrWhiteSpace(tag))
                query = query.Where(s => s.Tags.Contains(tag));

            var allSpeakers = query
                .OrderBy(s => s.Name)
                .ToList();
            return View(new HomeSpeakersViewModel()
            {
                Count = allSpeakers.Count,
                Speakers = allSpeakers
            });
        }

        public IActionResult Tags()
        {
            var counts = _session.Query<Speakers_PerTags.SpeakerCountByTag, Speakers_PerTags>()
                .ToList();

            return View(new HomeTagsViewModel()
            {
                CountPerTag = counts.ToDictionary(t => t.Tag, t => t.Count)
            });
        }
    }
}