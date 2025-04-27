using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MVTQ.LogifySearch.Application.Interfaces.Repositories;
using MVTQ.LogifySearch.Application.Interfaces.Services;
using MVTQ.LogifySearch.Domain.Common;
using MVTQ.LogifySearch.Domain.Entities;
using MVTQ.LogifySearch.Domain.Model.Account;
using MVTQ.LogifySearch.Infrastructure.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace MVTQ.LogifySearch.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<User> _signInManager;
        private readonly LogifySearchDbContext _context;
        public UserService(IMapper mapper, IGenericRepository<User> repository, 
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager,
            SignInManager<User> signInManager,
            LogifySearchDbContext logifySearchDbContext
            )
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _context = logifySearchDbContext;
        }


        //public async Task<IList<loginviewmodel>> GetAllAsync(Expression<Func<User, bool>> expression = null, Func<IQueryable<User>, IOrderedQueryable<T>> orderBy = null, PaginationFilter paginationFilter = null, params Expression<Func<User, object>>[] includes)
        //{
        //    var users = _dbContext.Users.Include(q=>q.user)
        //}

        public async Task<BootstrapTableJsonModel<UserViewModel>> GetUsers(Expression<Func<User, bool>> expression = null, Func<IQueryable<User>, IOrderedQueryable<User>> orderBy = null, List<string> roles = null, PaginationFilter paginationFilter = null)
        {
           
            var query = _userManager.Users;
            if (expression != null)
            {
                query = query.Where(expression);
            }
            if (orderBy != null)
            {
                query = orderBy(query);
            }
            if (paginationFilter == null)
            {
                paginationFilter = new PaginationFilter();
            }
            if (roles == null)
            {
                roles = new List<string>();
            }

            var users = await query.ToListAsync();
            var result = new BootstrapTableJsonModel<UserViewModel>();
          

            foreach (var user in users)
            {
                var currentUserRoles = await _userManager.GetRolesAsync(user);
                if ((currentUserRoles.Any() && currentUserRoles.Any(q => roles.Contains(q))) || !roles.Any())
                {
                    result.Items.Add(new UserViewModel()
                    {
                        UserName = user.UserName,
                        Email = user.Email,
                        Roles = currentUserRoles.ToList()
                    });
                }
            }

            result.Count = result.Items.Count;
            result.Items = result.Items.Skip(paginationFilter.Offset).Take(paginationFilter.Limit).ToList();

            return result;
        }
    }
}
