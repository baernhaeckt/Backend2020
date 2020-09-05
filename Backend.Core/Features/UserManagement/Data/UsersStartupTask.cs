using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using Backend.Core.Entities;
using Backend.Infrastructure.Abstraction.Hosting;
using Backend.Infrastructure.Abstraction.Persistence;
using Backend.Infrastructure.Abstraction.Security;

namespace Backend.Core.Features.UserManagement.Data
{
    public class UsersStartupTask : IStartupTask
    {
        private readonly IWriter _writer;

        private readonly IPasswordStorage _passwordStorage;

        public UsersStartupTask(IWriter writer, IPasswordStorage passwordStorage)
        {
            _writer = writer;
            _passwordStorage = passwordStorage;
        }

        public async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            if (await _writer.CountAsync<User>(u => u.Roles.Contains(Roles.User)) > 1)
            {
                return;
            }

            var users = new List<User>
            {
                new User
                {
                    Firstname = "Turi1",
                    Lastname = "Meier",
                    Email = "t1@test.ch",
                    Languages = new Collection<string>
                    {
                        "de"
                    },
                    Roles = new List<string>{ Roles.User },
                    PasswordHash = _passwordStorage.Create("test")
                },
                new User
                {
                    Firstname = "Turi2",
                    Lastname = "Meier",
                    Email = "t2@test.ch",
                    Languages = new Collection<string>
                    {
                        "de",
                        "es"
                    },
                    Roles = new List<string>{ Roles.User },
                    PasswordHash = _passwordStorage.Create("test")
                },
                new User
                {
                    Firstname = "Turi3",
                    Lastname = "Meier",
                    Email = "t3@test.ch",
                    Languages = new Collection<string>
                    {
                        "en",
                        "fr"
                    },
                    Roles = new List<string>{ Roles.User },
                    PasswordHash = _passwordStorage.Create("test")
                }
            };

            await _writer.InsertManyAsync(users);
        }
    }
}