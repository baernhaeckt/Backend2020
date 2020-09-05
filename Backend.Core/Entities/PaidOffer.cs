using System;
using System.Collections.Generic;

namespace Backend.Core.Entities
{
    public class PaidOffer : Entity
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public int Ranking { get; set; }

        public string Location { get; set; }

        public IEnumerable<StartOption> Starts { get; set; }

        public String Duration { get; set; }

        public ICollection<string> Categories { get; set; }
    }
}
