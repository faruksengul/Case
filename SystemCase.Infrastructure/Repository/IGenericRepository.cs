using System.Linq.Expressions;
using SystemCase.Domain.Core.Base.Abstract;
using SystemCase.Domain.Core.Base.Concrete;
using SystemCase.Domain.Core.Pagination;

namespace SystemCase.Infrastructure.Repository;

public interface IGenericRepository<TEntity> where TEntity : IEntity
{
    Task<TEntity> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<TDto> GetByIdWithProjectToAsync<TDto>(Guid id, CancellationToken cancellationToken) where TDto : BaseDto;

    (IQueryable<TEntity>, int) GetAll(PaginationParams paginationParams);

    IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> predicate);

    Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken);

    Task AddAsync(TEntity entity, CancellationToken cancellationToken);

    void Remove(TEntity entity);

    void Update(TEntity entity);
    Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);
}