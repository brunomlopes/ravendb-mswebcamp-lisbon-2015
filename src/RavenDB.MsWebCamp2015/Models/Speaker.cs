using Bogus;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace RavenDB.MsWebCamp2015.Models
{
    public class Speaker
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string[] Tags { get; set; }
        public DateTimeOffset CreationDate { get; set; }

        public static Speaker CreateRandom()
        {
            return CreateRandom(1).First();
        }
        public static IEnumerable<Speaker> CreateRandom(int count)
        {
            var speaker = new Faker<Speaker>()
                .RuleFor(s => s.Name, f => f.Name.FindName())
                .RuleFor(s => s.CreationDate, f => f.Date.Between(DateTime.Today.AddDays(-5), DateTime.Today))
                .RuleFor(s => s.Tags,
                    f =>
                        Enumerable.Range(0, f.Random.Number(1, 3))
                            .Select(
                                i =>
                                    f.PickRandom(new[]
                                    {"c#", ".net", "cloud", "azure", "vb", "docker", "osx", "windows", "linux", "javascript", "web", "razor", "mvc","knockout","angular","typescript","es6","mono","json"}))
                            .ToArray());

            return speaker.Generate(count);
        }
    }

    public class Event
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public DateTimeOffset Date { get; set; }
        public IList<Presentation> Presentations { get; set; }
    }

    public class Presentation
    {
        public string Title { get; set; }
        public string SpeakerId { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
    }
}