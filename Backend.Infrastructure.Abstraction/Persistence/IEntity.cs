using System;
using MongoDbGenericRepository.Models;

namespace Backend.Infrastructure.Abstraction.Persistence
{
    public interface IEntity : IDocument
    {
        DateTime CreatedAt { get; set; }

        DateTime UpdatedAt { get; set; }
    }
}