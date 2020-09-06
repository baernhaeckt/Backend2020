using System;
using System.Collections.ObjectModel;

namespace Backend.Core.Features.Guiding.Models
{
    public class GuideResponse
    {
        public GuideResponse(Guid id, string displayName, string description, Collection<string> languages)
        {
            Id = id;
            DisplayName = displayName;
            Description = description;
            Languages = languages;
        }

        public Guid Id { get; set; }

        public string DisplayName { get; set; }

        public string Description { get; set; }

        public Collection<string> Languages { get; set; }
    }
}
