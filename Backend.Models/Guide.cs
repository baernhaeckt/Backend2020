using System;
using System.Collections.Generic;
using System.Globalization;

namespace Backend.Models
{
    public class Guide
    {
        public Guid Id { get; set; }

        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public string Description { get; set; }

        public DateTime Birthday { get; set; }

        public ICollection<CultureInfo> Languages { get; set; }
    }
}
