using System;
using Backend.Infrastructure.Abstraction.Persistence;

namespace Backend.Core.Entities
{
    public abstract class Entity : IEntity
    {
        public Guid Id { get; set; }

        public int Version { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}