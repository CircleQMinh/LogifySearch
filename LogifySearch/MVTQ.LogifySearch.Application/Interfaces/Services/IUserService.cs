using MVTQ.LogifySearch.Domain.Common;
using MVTQ.LogifySearch.Domain.Entities;
using MVTQ.LogifySearch.Domain.Model.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MVTQ.LogifySearch.Application.Interfaces.Services
{
    public interface IUserService
    {
        Task<BootstrapTableJsonModel<UserViewModel>> GetUsers(Expression<Func<User, bool>> expression = null, Func<IQueryable<User>, IOrderedQueryable<User>> orderBy = null, List<string> roles = null, PaginationFilter paginationFilter = null);
    }
}
