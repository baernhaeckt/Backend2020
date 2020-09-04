using System.Collections.Generic;
using System.Linq;

namespace Backend.Core.Entities
{
    public class User : Entity
    {
        public string Email { get; set; } = string.Empty;

        public string DisplayName { get; set; } = string.Empty;

        public Location Location { get; set; } = new Location();

        public string PasswordHash { get; set; } = string.Empty;

        public List<string> Roles { get; set; } = Enumerable.Empty<string>().ToList();
    }
}