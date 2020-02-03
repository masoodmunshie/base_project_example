using DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface Iuser
    {
        int CountUsers();

        Task<user_response> GetUsers(request req);

        Task<user_response_add> AddUser(user u);

       
    }
}
