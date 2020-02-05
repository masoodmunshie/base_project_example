using DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface Iauth
    {
      Task<auth_response> Login(auth Auth);

     Task<List<right>> GetRights(int userid);

    }
}
