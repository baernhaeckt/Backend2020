using System;
using System.Collections.ObjectModel;
using System.Globalization;

namespace Backend.Models
{
    class Guide
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public Collection<CultureTypes> Languages { get; set; }
    }
}
