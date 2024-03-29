using System.Linq.Expressions;

namespace Domain.Ports;

public interface IGenericRepository<E> : IDisposable
    where E : DomainEntity
{
    Task<PagedResult<E>> GetPagedAsync(int page, int pageSize, Expression<Func<E, bool>>? filter = null,
        Func<IQueryable<E>, IOrderedQueryable<E>>? orderBy = null, string includeStringProperties = "",
        bool isTracking = false);

    Task<PagedResult<E>> GetPagedAsync(int page, int pageSize, Expression<Func<E, bool>>? filter = null,
        Func<IQueryable<E>, IOrderedQueryable<E>>? orderBy = null, bool isTracking = false,
        string includeStringProperties = "",
        params Expression<Func<E, object>>[] includeObjectProperties);

    Task<IEnumerable<E>> GetAsync(Expression<Func<E, bool>>? filter = null,
        Func<IQueryable<E>, IOrderedQueryable<E>>? orderBy = null, string includeStringProperties = "",
        bool isTracking = false);

    Task<IEnumerable<E>> GetAsync(Expression<Func<E, bool>>? filter = null,
        Func<IQueryable<E>, IOrderedQueryable<E>>? orderBy = null, bool isTracking = false,
        params Expression<Func<E, object>>[] includeObjectProperties);


    Task<PagedResult<E>> GetPagedFilterAsync(int page, int pageSize,
        Expression<Func<E, bool>>? filter =
            null,
        Func<IQueryable<E>, IOrderedQueryable<E>>? orderBy = null, string includeStringProperties = "",
        bool isTracking = false);

    Task<PagedResult<E>> GetPagedFilterAsync(int page, int pageSize, Expression<Func<E, bool>>? filter = null,
        Func<IQueryable<E>, IOrderedQueryable<E>>? orderBy = null, bool isTracking = false,
        string includeStringProperties = "",
        params Expression<Func<E, object>>[] includeObjectProperties);

    Task<E> GetByIdAsync(object id);

    Task<E> GetByIdAsync(object id, Expression<Func<E, bool>>? filter = null,
        Func<IQueryable<E>, IOrderedQueryable<E>>? orderBy = null, bool isTracking = false,
        string includeStringProperties = "",
        params Expression<Func<E, object>>[] includeObjectProperties);

    Task<E> AddAsync(E entity);
    Task UpdateAsync(E entity);
    Task DeleteAsync(ISoftDelete entity, bool deleteCascade = true);
    Task<bool> Exist(Expression<Func<E, bool>> filter);
}

public class PagedResult<T>
{
    public int TotalRecords { get; set; }
    public int TotalPages { get; set; }
    public IEnumerable<T> Records { get; set; } = Enumerable.Empty<T>();
}