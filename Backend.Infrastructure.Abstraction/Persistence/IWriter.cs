using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Backend.Infrastructure.Abstraction.Persistence
{
    public interface IWriter : IReader
    {
        Task DeleteAsync<TEntity>(Guid id)
            where TEntity : IEntity, new();

        Task<TEntity> InsertAsync<TEntity>(TEntity record)
            where TEntity : IEntity, new();

        Task InsertManyAsync<TEntity>(IEnumerable<TEntity> records)
            where TEntity : IEntity, new();

        Task UpdateAsync<TEntity>(Guid id, object updateDefinition)
            where TEntity : IEntity, new();

        Task UpdateAsync<TEntity>(TEntity record)
            where TEntity : IEntity, new();

        Task UpdatePullAsync<TEntity, TItem>(Guid id, Expression<Func<TEntity, IEnumerable<TItem>>> field, TItem valueToPull)
            where TEntity : IEntity, new();
    }
}