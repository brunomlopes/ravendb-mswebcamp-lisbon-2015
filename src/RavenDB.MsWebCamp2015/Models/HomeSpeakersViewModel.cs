using System.Collections.Generic;

namespace RavenDB.MsWebCamp2015.Models
{
    public class HomeSpeakersViewModel
    {
        public int Count { get; set; } 
        public IEnumerable<Speaker> Speakers { get; set; } 
    }
}