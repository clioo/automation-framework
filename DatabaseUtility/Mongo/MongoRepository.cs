using DatabaseUtility.Extensions;
using DatabaseUtility.Models;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace DatabaseUtility.Mongo
{
    public class MongoRepository<TEntity, TSubEntity> : IMongoRepository<TEntity, TSubEntity>
        where TEntity : EntityBase<TSubEntity>
        where TSubEntity : ContentBase
    {
        private readonly IMongoDatabase _database;
        private IMongoCollection<TEntity> _collection;

        public MongoRepository(IMongoDatabase database, string collectionName = nameof(TEntity))
        {
            _database = database;
            _collection = database.GetCollection<TEntity>(collectionName + "s_Current");
        }

        public async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default(CancellationToken))
        {
            entity.Contents.SetCreatedFields();
            await _collection.InsertOneAsync(entity, new InsertOneOptions(), cancellationToken);
            return entity;
        }

        public async Task<ICollection<TEntity>> AllAsync(Expression<Func<TEntity, bool>> predicate = null, Expression<Func<TEntity, object>> orderBy = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            var query = _collection.AsQueryable().OfType<TEntity>();
            if (predicate != null)
            {
                query = query.Where(predicate);
            }
            return await query.ToListAsync();
        }

        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await _collection.AsQueryable().AnyAsync(predicate, cancellationToken);
        }

        public async Task<TEntity> DeleteAsync(Guid id, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await _collection.FindOneAndDeleteAsync(e => e.Contents.Identifier == id, cancellationToken: cancellationToken);
        }

        public async Task<TEntity> FindAsync(Guid id, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await _collection.AsQueryable().FirstOrDefaultAsync(e => e.Contents.Identifier == id, cancellationToken);
        }

        public async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default(CancellationToken))
        {
            entity.Contents.SetUpdatedFields();
            var update = Builders<TEntity>.Update.Set(c => c.Contents, entity.Contents);
            return await _collection.FindOneAndUpdateAsync(Builders<TEntity>.Filter.Where(e => e.Contents.Identifier == entity.Contents.Identifier), update);
        }
    }
}