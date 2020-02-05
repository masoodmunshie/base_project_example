using DAL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models
{
    public class auth_handler_requirement : IAuthorizationRequirement
    {
        public int Value { get; }

        public Iauth _iAuth; 
      
      

        public auth_handler_requirement(int value, Iauth iauth)
        {
            Value = value;
            _iAuth = iauth;
            
        }
    }
}
