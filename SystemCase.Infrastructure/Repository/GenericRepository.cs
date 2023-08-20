using System.Linq.Expressions;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using SystemCase.Domain.Core.Base.Abstract;
using SystemCase.Domain.Core.Base.Concrete;
using SystemCase.Domain.Core.Pagination;
using SystemCase.Infrastructure.Extensions;

namespace SystemCase.Infrastructure.Repository;

public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity, IEntity
{
    private readonly DbContext _context;
    private readonly DbSet<TEntity> _dbSet;
    private readonly IMapper _mapper;

    public GenericRepository(DbContext context, IMapper mapper)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _dbSet = _context.Set<TEntity>();
        _mapper = mapper;
    }


    public async Task<TEntity> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = await _dbSet.FindAsync(new object[] { id }, cancellationToken);

        if (entity is not null)
            _context.Entry(entity).State = EntityState.Detached;

        return entity;
    }

    public async Task<TDto> GetByIdWithProjectToAsync<TDto>(Guid id, CancellationToken cancellationToken) where TDto : BaseDto
    {
        var record = await _dbSet
            .ProjectTo<TDto>(_mapper.ConfigurationProvider)
            .SingleOrDefaultAsync(x => x.Id == id, cancellationToken);

        return record;
    }
    public async Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
    {
        var entity = await _dbSet.FirstOrDefaultAsync(predicate);
        return entity;
    }
    public (IQueryable<TEntity>, int) GetAll(PaginationParams paginationParams)
    {
        var query = _dbSet.AsNoTracking();

        int count = query.Count();

        query = string.IsNullOrEmpty(paginationParams.OrderKey)
            ? query.OrderByDescending(x => x.CreatedDate)
            : query.OrderBy(paginationParams.OrderKey, paginationParams.OrderType ?? "ascending");

        query = query
            .Skip((paginationParams.PageId - 1) * paginationParams.PageSize)
            .Take(paginationParams.PageSize);

        return (query, count);
    }

    public IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> predicate)
    {
        return _dbSet.Where(predicate);
    }

    public async Task AddAsync(TEntity entity, CancellationToken cancellationToken)
    {
        await _dbSet.AddAsync(entity, cancellationToken);
    }

    public void Remove(TEntity entity)
    {
        _dbSet.Remove(entity);
    }

    public void Update(TEntity entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
    }

    public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken)
    {
        return await _dbSet.AnyAsync(predicate, cancellationToken);
    }
}