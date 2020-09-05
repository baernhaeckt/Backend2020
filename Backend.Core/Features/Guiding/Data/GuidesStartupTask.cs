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
                    Firstname = "Hans",
                    Lastname = "Meier",
                    Description = "Cooler Fischer!",
                    Email = "hans.meier@test.ch",
                    Languages = new Collection<string>
                    {
                        "de"
                    },
                    PasswordHash = _passwordStorage.Create("test")
                },
                new User
                {
                    Firstname = "Hans",
                    Lastname = "Müller",
                    Description = "Voll der Botscha spieler",
                    Email = "hans.mueller@test.ch",
                    Languages = new Collection<string>
                    {
                        "en",
                        "de"
                    },
                    PasswordHash = _passwordStorage.Create("test")
                }
            };

            await _writer.InsertManyAsync(users);
        }
    }
}