using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.Interfaces;
using DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize, Authorize(Policy = "roles")]

    public class userController : ControllerBase
    {

        private Iuser _iuser;

        public userController(Iuser interface_user)
        {
            this._iuser = interface_user;
        }

        [Route("get")]
        [HttpGet]
        public async  Task<user_response> GetUsers(request request)
        {
            return await _iuser.GetUsers(request);
        }

        [Route("user")]
        [HttpPost]
        public async Task<user_response_add> AddUpdateUser(user user)
        {
            return await _iuser.AddUser(user);
        }

        [Route("roles")]
        [HttpGet]
        public async Task<role_list> GetRoles(request req)
        {
            return await _iuser.GetRoles(req);
        }

      


    }


}