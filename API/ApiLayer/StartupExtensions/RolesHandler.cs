using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using DAL.Models;
using Microsoft.AspNetCore.Routing;
using DAL.Interfaces;
using DAL;

namespace ApiLayer.StartupExtensions
{
    public class RolesHandler : AuthorizationHandler<auth_handler_requirement>
    {

       
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
                                                  auth_handler_requirement requirement)
        {
            //if (!context.User.HasClaim(c => c.Type == ClaimTypes.DateOfBirth &&
            //                                c.Issuer == "http://contoso.com"))
            //{
                //TODO: Use the following if targeting a version of
                //.NET Framework older than 4.6:
                //      return Task.FromResult(0);
                // return Task.CompletedTask;

              
           RouteEndpoint endpoint = (RouteEndpoint)context.Resource;
           var apistring = endpoint.RoutePattern.RawText;
           //context.Succeed(requirement);
            var a =  requirement._iAuth.GetRights(int.Parse(context.User.Claims.First(i => i.Type == "userID").Value)).Result;

            if (a.Where(x => x.api == apistring).Count() > 0)
                context.Succeed(requirement); // Basically check if the user has that specific right.

            return Task.CompletedTask;
        }
    }
}
