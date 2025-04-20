using AutoMapper;
using Microsoft.AspNetCore.Identity;
using MVTQ.LogifySearch.Application.Interfaces.Repositories;
using MVTQ.LogifySearch.Application.Interfaces.Services;
using MVTQ.LogifySearch.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVTQ.LogifySearch.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<User> _signInManager;
        public UserService(IMapper mapper, IGenericRepository<User> repository, 
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager,
            SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }

    }
}
