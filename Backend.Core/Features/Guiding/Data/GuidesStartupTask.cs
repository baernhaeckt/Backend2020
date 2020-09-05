using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using Backend.Core.Entities;
using Backend.Infrastructure.Abstraction.Hosting;
using Backend.Infrastructure.Abstraction.Persistence;
using Backend.Infrastructure.Abstraction.Security;

namespace Backend.Core.Features.Guiding.Data
{
    public class GuidesStartupTask : IStartupTask
    {
        private readonly IWriter _writer;

        private readonly IPasswordStorage _passwordStorage;

        public GuidesStartupTask(IWriter writer, IPasswordStorage passwordStorage)
        {
            _writer = writer;
            _passwordStorage = passwordStorage;
        }

        public async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            if (await _writer.CountAsync<User>(u => u.Roles.Contains(Roles.Guide)) > 1)
            {
                return;
            }

            var users = new List<User>
            {
                new User
                {
                    Id = Guid.Parse("5d017691-a2b7-4af8-b14e-357879746dc6"),
                    Firstname = "Hans",
                    Lastname = "Meier",
                    Description = "Cooler Fischer!",
                    Email = "hans.meier@test.ch",
                    Languages = new Collection<string>
                    {
                        "de"
                    },
                    Roles = new List<string>{ Roles.Guide },
                    PasswordHash = _passwordStorage.Create("test")
                },
                new User
                {
                    Firstname = "Reto",
                    Lastname = "Jost",
                    Description = "Voll der Botscha spieler",
                    Email = "reto.jost@test.ch",
                    Languages = new Collection<string>
                    {
                        "en",
                        "de"
                    },
                    Roles = new List<string>{ Roles.Guide },
                    PasswordHash = _passwordStorage.Create("test")
                },
                new User
                {
                    Firstname = "Veronika",
                    Lastname = "Haenni",
                    Description = "Voll der Botscha spieler",
                    Email = "veronika.haenni@test.ch",
                    Languages = new Collection<string>
                    {
                        "en",
                        "de"
                    },
                    Roles = new List<string>{ Roles.Guide },
                    PasswordHash = _passwordStorage.Create("test")
                },
                new User
                {
                    Firstname = "Mario",
                    Lastname = "Scholz",
                    Description = "Voll der Botscha spieler",
                    Email = "mario.scholz@test.ch",
                    Languages = new Collection<string>
                    {
                        "en",
                        "de"
                    },
                    Roles = new List<string>{ Roles.Guide },
                    PasswordHash = _passwordStorage.Create("test")
                },
                new User
                {
                    Firstname = "Mike",
                    Lastname = "Schroeder",
                    Description = "Voll der Botscha spieler",
                    Email = "mike.schroeder@test.ch",
                    Languages = new Collection<string>
                    {
                        "en",
                        "de"
                    },
                    Roles = new List<string>{ Roles.Guide },
                    PasswordHash = _passwordStorage.Create("test")
                },
                new User
                {
                    Firstname = "Olf",
                    Lastname = "Dagobert",
                    Description = "Voll der Botscha spieler",
                    Email = "olf.dagobert@test.ch",
                    Languages = new Collection<string>
                    {
                        "en",
                        "de"
                    },
                    Roles = new List<string>{ Roles.Guide },
                    PasswordHash = _passwordStorage.Create("test")
                },
                new User
                {
                    Firstname = "Anna",
                    Lastname = "Lehmann",
                    Description = "Voll der Botscha spieler",
                    Email = "anna.lehmann@test.ch",
                    Languages = new Collection<string>
                    {
                        "en",
                        "de"
                    },
                    Roles = new List<string>{ Roles.Guide },
                    PasswordHash = _passwordStorage.Create("test")
                },
                new User
                {
                    Firstname = "Ursula",
                    Lastname = "Holzmann",
                    Description = "Voll der Botscha spieler",
                    Email = "ursula.holzmann@test.ch",
                    Languages = new Collection<string>
                    {
                        "en",
                        "de"
                    },
                    Roles = new List<string>{ Roles.Guide },
                    PasswordHash = _passwordStorage.Create("test")
                }
            };

            await _writer.InsertManyAsync(users);
        }
    }
}