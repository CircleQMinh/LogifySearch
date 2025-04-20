using Microsoft.AspNetCore.Identity;
using MVTQ.LogifySearch.Domain.Common;
using MVTQ.LogifySearch.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MVTQ.LogifySearch.Application.Interfaces.Services
{
    public interface IEntityService<T> where T : BaseEntity
    {
        Task<IList<T>> GetAllAsync(Expression<Func<T, bool>> expression = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, PaginationFilter paginationFilter = null, params Expression<Func<T, object>>[] includes);

        Task<T> FindAsync(int id);
        Task<T> FindAsync(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] includes);

        Task<bool> Exist(int id);
        Task<bool> Exist(Expression<Func<T, bool>> expression);
        Task<T> Add(T entity);
        Task<ICollection<T>> AddRange(ICollection<T> entities);
        Task<T> Update(T entity);
        Task Delete(int id);

        Task<int> CountAsync();
        Task<int> CountAsync(Expression<Func<T, bool>> expression = null);
    }
}
