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
        public Task<user_response> GetUsers(request request)
        {
            return _iuser.GetUsers(request);
        }

        [Route("user")]
        [HttpPost]
        public Task<user_response_add> AddUpdateUser(user user)
        {
            return _iuser.AddUser(user);
        }

      


    }


}