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
                    Description = "Ich bin Hans, ich liebe es den Sommer in Bern zu verbringen. Am liebsten mit einem Eis auf dem See!",
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
                    Id = Guid.Parse("fae2f93f-00fa-44d8-ad73-b2162128b6ee"),
                    Firstname = "Reto",
                    Lastname = "Jost",
                    Description = "Lerne mit mir Bern kennen! Lebe seit über 20 Jahren in Bern und kenne die geheimen Ecken der Stadt.",
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
                    Id = Guid.Parse("b1ee7b15-1976-4152-ba15-e13f0e70e699"),
                    Firstname = "Veronika",
                    Lastname = "Haenni",
                    Description = "Verliebt in Bern - Hier wo man schnell in der Natur ist, komm mit mir in die Blumenfelder und entdecke den Kanton Bern von einer anderen Seite.",
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
                    Id = Guid.Parse("6a74f3f1-40dc-4590-9f75-3c6f59581d34"),
                    Firstname = "Mario",
                    Lastname = "Scholz",
                    Description = "Bern ist eine der Universitätsstädte der Schweiz und hat viel Kultur und Wissen zu bieten!",
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
                    Id = Guid.Parse("9b5beddd-2632-4956-bbe3-702bef8ef5ae"),
                    Firstname = "Mike",
                    Lastname = "Schroeder",
                    Description = "Seit Jahren in der Schweiz und sehr vetraut mit den Traditionen, lass mir dir den Charm der Schweiz zeigen.",
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
                    Id = Guid.Parse("cf7a81fd-d556-4003-977b-7de77b38fa3f"),
                    Firstname = "Olf",
                    Lastname = "Dagobert",
                    Description = "Voll fett alteeer!",
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
                    Id = Guid.Parse("8baeb1b5-8b8d-42c2-86ae-416b6fbc9e8c"),
                    Firstname = "Anna",
                    Lastname = "Lehmann",
                    Description = "Schweiz ist der Angelpunkt der Kultur! Soviele Länder treffen sich hier. Ich freue mich dir meine Kultur zu zeigen und deine kennen zu lernen.",
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
                    Id = Guid.Parse("bc851f39-92b7-4ab5-8b0c-6412e433281a"),
                    Firstname = "Ursula",
                    Lastname = "Holzmann",
                    Description = "Wandern in der Schweiz ist das tollste im Leben!",
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