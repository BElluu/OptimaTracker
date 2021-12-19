using System.Collections.Generic;

namespace OptimaTracker
{
    public class Company
    {
        public string serialKey { get; set; }
        public string TIN { get; set; }
        public List<Event> events { get; set; }
    }
}
