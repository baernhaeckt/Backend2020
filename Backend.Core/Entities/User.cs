using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Backend.Core.Entities
{
    public class User : Entity
    {
        public string Email { get; set; } = string.Empty;

        public string DisplayName { get; set; } = string.Empty;

        public Location Location { get; set; } = new Location();

        public string PasswordHash { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public List<string> Roles { get; set; } = Enumerable.Empty<string>().ToList();

        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public Collection<string> Languages { get; set; }
    }
}