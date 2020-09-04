using System;
using MongoDbGenericRepository.Models;

namespace Backend.Core.Entities
{
    public interface IEntity : IDocument
    {
        DateTime CreatedAt { get; set; }

        DateTime UpdatedAt { get; set; }
    }
}