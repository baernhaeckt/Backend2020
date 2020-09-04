using System;
using System.Diagnostics.CodeAnalysis;

namespace Backend.Infrastructure.Abstraction.Persistence
{
    [SuppressMessage("Design", "CA1032:Implement standard exception constructors", Justification = "Must not be invoked with those constructors.")]
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException(Type type, string methodName, string filter)
        {
            MethodName = methodName;
            Filter = filter;
            Name = type.Name;
        }

        public string MethodName { get; }

        public string Filter { get; }

        public string Name { get; }
    }
}