using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using Backend.Core.Entities;
using Backend.Infrastructure.Abstraction.Hosting;
using Backend.Infrastructure.Abstraction.Persistence;
using MongoDB.Driver;

namespace Backend.Infrastructure.Persistence
{
    internal class MongoDbWriter : MongoDbReader, IWriter
    {
        private readonly IClock _clock;

        public MongoDbWriter(DbContextFactory dbContextFactory, IClock clock)
            : base(dbContextFactory) => _clock = clock;

        public virtual async Task<TEntity> InsertAsync<TEntity>(TEntity record)
            where TEntity : IEntity, new()
        {
            try
            {
                DbContext dbContext = DbContextFactory.Create();
                record.CreatedAt = _clock.Now().DateTime;
                await dbContext.GetCollection<TEntity>().InsertOneAsync(record);
                return record;
            }
            catch (MongoWriteException e)
                when (e.WriteError.Category == ServerErrorCategory.DuplicateKey)
            {
                throw new DuplicateKeyException();
            }
        }

        public virtual async Task InsertManyAsync<TEntity>(IEnumerable<TEntity> records)
            where TEntity : IEntity, new()
        {
            try
            {
                DbContext dbContext = DbContextFactory.Create();
                foreach (TEntity record in records)
                {
                    record.CreatedAt = _clock.Now().DateTime;
                }

                await dbContext.GetCollection<TEntity>().InsertManyAsync(records);
            }
            catch (MongoBulkWriteException e)
                when (e.WriteErrors.Any(w => w.Category == ServerErrorCategory.DuplicateKey))
            {
                throw new DuplicateKeyException();
            }
        }

        public virtual async Task DeleteAsync<TEntity>(Guid id)
            where TEntity : IEntity, new()
        {
            var filter = new ExpressionFilterDefinition<TEntity>(collection => collection.Id == id);
            DeleteResult result = await DbContextFactory.Create().GetCollection<TEntity>().DeleteOneAsync(filter);
            if (result.DeletedCount < 0)
            {
                throw new ValidationException($"No record with id {id} found.");
            }
        }

        public virtual async Task UpdateAsync<TEntity>(TEntity record)
            where TEntity : IEntity, new()
        {
            DbContext dbContext = DbContextFactory.Create();
            record.UpdatedAt = _clock.Now().DateTime;
            FilterDefinition<TEntity> filter = new ExpressionFilterDefinition<TEntity>(u => u.Id == record.Id);
            await dbContext.GetCollection<TEntity>().ReplaceOneAsync(filter, record);
        }

        public virtual async Task UpdateAsync<TEntity>(Guid id, object definition)
            where TEntity : IEntity, new()
        {
            IList<UpdateDefinition<TEntity>> updateDefinitions = new List<UpdateDefinition<TEntity>>();
            updateDefinitions.Add(Builders<TEntity>.Update.Set(e => e.UpdatedAt, _clock.Now().DateTime));
            foreach (PropertyInfo property in definition.GetType().GetProperties())
            {
                UpdateDefinitionBuilder<TEntity> builder = Builders<TEntity>.Update;
                if (property.PropertyType.GetInterfaces()
                    .Any(t => t.IsGenericType && t.GetGenericTypeDefinition() == typeof(ICollection<>)))
                {
                    // This will not replace child collections but add the elements to it.
                    object propertyValue = property.GetValue(definition);

                    // This needs to be treated specially...
                    if (propertyValue is IEnumerable<Guid> value)
                    {
                        updateDefinitions.Add(builder.PushEach(property.Name, value));
                    }
                    else
                    {
                        updateDefinitions.Add(builder.PushEach(property.Name, (IEnumerable<object>)propertyValue));
                    }
                }
                else
                {
                    updateDefinitions.Add(builder.Set(property.Name, property.GetValue(definition)));
                }
            }

            UpdateDefinition<TEntity> updateDefinition = Builders<TEntity>.Update.Combine(updateDefinitions);
            FilterDefinition<TEntity> filter = Builders<TEntity>.Filter.Eq(nameof(IEntity.Id), id);

            DbContext dbContext = DbContextFactory.Create();
            UpdateResult result = await dbContext.GetCollection<TEntity>().UpdateOneAsync(filter, updateDefinition);
        }

        public virtual async Task UpdatePullAsync<TEntity, TItem>(Guid id, Expression<Func<TEntity, IEnumerable<TItem>>> field, TItem valueToPull)
            where TEntity : IEntity, new()
        {
            IList<UpdateDefinition<TEntity>> updateDefinitions = new List<UpdateDefinition<TEntity>>();
            updateDefinitions.Add(Builders<TEntity>.Update.Set(e => e.UpdatedAt, _clock.Now().DateTime));

            updateDefinitions.Add(Builders<TEntity>.Update.Pull(field, valueToPull));
            FilterDefinition<TEntity> filter = Builders<TEntity>.Filter.Eq(nameof(IEntity.Id), id);

            UpdateDefinition<TEntity> updateDefinition = Builders<TEntity>.Update.Combine(updateDefinitions);
            DbContext dbContext = DbContextFactory.Create();
            await dbContext.GetCollection<TEntity>().FindOneAndUpdateAsync(filter, updateDefinition);
        }
    }
}