using DatabaseUtility.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace DatabaseUtility.Mongo
{
    public interface IMongoRepository<TEntity, TSubEntity>
        where TEntity : EntityBase<TSubEntity>
        where TSubEntity : ContentBase
    {
        Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default(CancellationToken));

        Task<TEntity> DeleteAsync(Guid id, CancellationToken cancellationToken = default(CancellationToken));

        Task<ICollection<TEntity>> AllAsync(Expression<Func<TEntity, bool>> predicate = null, Expression<Func<TEntity, object>> orderBy = null, CancellationToken cancellationToken = default(CancellationToken));

        Task<TEntity> FindAsync(Guid id, CancellationToken cancellationToken = default(CancellationToken));

        Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default(CancellationToken));

        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default(CancellationToken));
    }
}