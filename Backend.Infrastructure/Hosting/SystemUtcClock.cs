using System;
using Backend.Infrastructure.Abstraction.Hosting;

namespace Backend.Infrastructure.Hosting
{
    public class SystemUtcClock : IClock
    {
        public DateTimeOffset Now() => DateTimeOffset.Now;
    }
}