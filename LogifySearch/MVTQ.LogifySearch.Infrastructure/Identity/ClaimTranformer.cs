using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Configuration;
using MVTQ.LogifySearch.Application.Interfaces.Services;
using MVTQ.LogifySearch.Domain.Entities;
using MVTQ.LogifySearch.Domain.Enums;
using MVTQ.LogifySearch.Domain.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MVTQ.LogifySearch.Infrastructure.Identity
{
    public class ClaimTranformer : IClaimsTransformation
    {
        private readonly IUserService _userServices;
        private readonly IConfiguration _configuration;
        private const string Roles = "Roles";
        public ClaimTranformer(IUserService userServices, IConfiguration configuration)
        {
            _configuration = configuration;
            _userServices = userServices;
        }

        public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
        {
            if (principal.Identity == null)
            {
                return await Task.FromResult(principal);
            }
            if (!principal.Identity.IsAuthenticated)
            {
                return await Task.FromResult(principal);
            }

            //var identity = (ClaimsIdentity)principal.Identity; //e.g abcd@mgi.com
            //var userName = identity.Name.Split('@')[0]; // abcd

            //var userRoles = await _userServices.GetUserRolesFromUserName(userName); //get role assigned for user from database
            //userRoles.Add(AppRole.Viewer.GetDescription()); //add default role

            //foreach (var role in userRoles)
            //{
            //    AddClaim(identity, role, role);
            //}
            //AddClaim(identity, Roles, string.Join(";", userRoles));
            //AddNewUserIfNotExist(identity, userName, identity.Name);
            return await Task.FromResult(principal);
        }

        private static void AddClaim(ClaimsIdentity identity, string type, string value)
        {
            if (!identity.HasClaim(type, value) && !string.IsNullOrEmpty(value))
            {
                identity.AddClaim(new Claim(type, value));
            }
        }

        //private async void AddNewUserIfNotExist(ClaimsIdentity identity, string userName, string email)
        //{
        //    if (string.IsNullOrEmpty(userName))
        //    {
        //        return;
        //    }
        //    var exist = await _userServices.Exist(q=>q.Username == userName);
        //    if (!exist)
        //    {
        //        var newUser = new User()
        //        {
        //            Username = userName,
        //            Email = email,
        //        };
        //        await _userServices.Add(newUser);
        //    }
        //}
    }
}
