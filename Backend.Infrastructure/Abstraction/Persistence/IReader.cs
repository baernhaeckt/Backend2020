using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Backend.Core.Entities;

namespace Backend.Infrastructure.Abstraction.Persistence
{
    public interface IReader
    {
        IQueryable<TEntity> GetQueryable<TEntity>()
            where TEntity : IEntity, new();

        Task<long> CountAsync<TEntity>()
            where TEntity : IEntity, new();

        Task<long> CountAsync<TEntity>(Expression<Func<TEntity, bool>> filterPredicate)
            where TEntity : IEntity, new();

        Task<IEnumerable<TEntity>> GetAllAsync<TEntity>()
            where TEntity : IEntity, new();

        Task<TEntity> GetByIdOrDefaultAsync<TEntity>(Guid id)
            where TEntity : IEntity, new();

        Task<TEntity> GetByIdOrThrowAsync<TEntity>(Guid id)
            where TEntity : IEntity, new();

        Task<TProjection> GetByIdOrDefaultAsync<TEntity, TProjection>(Guid id, Expression<Func<TEntity, TProjection>> selectPredicate)
            where TEntity : IEntity, new()
            where TProjection : class;

        Task<TProjection> GetByIdOrThrowAsync<TEntity, TProjection>(Guid id, Expression<Func<TEntity, TProjection>> selectPredicate)
            where TEntity : IEntity, new()
            where TProjection : class;

        Task<IEnumerable<TEntity>> WhereAsync<TEntity>(Expression<Func<TEntity, bool>> filterPredicate, int? take = null)
            where TEntity : IEntity, new();

        Task<IEnumerable<TProjection>> WhereAsync<TEntity, TProjection>(Expression<Func<TEntity, bool>> filterPredicate, Expression<Func<TEntity, TProjection>> selectPredicate, int? take = null)
            where TEntity : IEntity, new()
            where TProjection : class;

        Task<TEntity> FirstOrDefaultAsync<TEntity>(Expression<Func<TEntity, bool>> filterPredicate)
            where TEntity : IEntity, new();

        Task<TEntity> SingleOrDefaultAsync<TEntity>(Expression<Func<TEntity, bool>> filterPredicate)
            where TEntity : IEntity, new();

        Task<TProjection> SingleOrDefaultAsync<TEntity, TProjection>(Expression<Func<TEntity, bool>> filterPredicate, Expression<Func<TEntity, TProjection>> selectPredicate)
            where TEntity : IEntity, new()
            where TProjection : class;

        Task<TEntity> SingleAsync<TEntity>(Expression<Func<TEntity, bool>> filterPredicate)
            where TEntity : IEntity, new();

        Task<TProjection> SingleAsync<TEntity, TProjection>(Expression<Func<TEntity, bool>> filterPredicate, Expression<Func<TEntity, TProjection>> selectPredicate)
            where TEntity : IEntity, new()
            where TProjection : class;
    }
}