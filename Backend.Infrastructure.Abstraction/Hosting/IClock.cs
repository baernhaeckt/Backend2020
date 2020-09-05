using System;

namespace Backend.Infrastructure.Abstraction.Hosting
{
    public interface IClock
    {
        DateTimeOffset Now();
    }
}