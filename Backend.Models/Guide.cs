using System;
using System.Collections.ObjectModel;
using System.Globalization;

namespace Backend.Models
{
    public class Guide
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public Collection<CultureInfo> Languages { get; set; }
    }
}
