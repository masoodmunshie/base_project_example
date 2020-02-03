using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL;
using DAL.Interfaces;
using DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading;

namespace ApiLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class authController : ControllerBase
    {
        private Iauth _auth;
        public authController (Iauth auth)
        {
            _auth = auth;
        }

        [HttpPost, Route("login")]
        public async Task<auth_response> Login([FromBody] auth user)
        {
            return await _auth.Login(user);
        }

    }
}