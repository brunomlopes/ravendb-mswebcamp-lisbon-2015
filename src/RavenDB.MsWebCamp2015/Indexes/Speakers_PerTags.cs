using Raven.Client.Indexes;
using RavenDB.MsWebCamp2015.Models;
using System.Linq;

namespace RavenDB.MsWebCamp2015.Indexes
{
    public class Speakers_PerTags : AbstractIndexCreationTask<Speaker, Speakers_PerTags.SpeakerCountByTag>
    {
        public class SpeakerCountByTag
        {
            public string Tag { get; set; }
            public int Count { get; set; }
        }

        public Speakers_PerTags()
        {
            Map = speakers =>
                from speaker in speakers
                from tag in speaker.Tags
                select new SpeakerCountByTag()
                {
                    Tag = tag,
                    Count = 1
                };

            Reduce = counts =>
                from count in counts
                group count by count.Tag
                into g
                select new SpeakerCountByTag()
                {
                    Tag = g.Key,
                    Count = g.Sum(c => c.Count)
                };
        }
    }
}