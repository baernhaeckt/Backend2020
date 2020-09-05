using System;
using System.Collections.Generic;
using System.Globalization;

namespace Backend.Models
{
    public interface IGuide
    {
        DateTime Birthday { get; }

        string Description { get; }

        string Firstname { get; }

        Guid Id { get; }

        ICollection<CultureInfo> Languages { get; }

        string Lastname { get; }
    }
}