using System;
using System.Collections.Generic;
using System.Text;
using DAL;
using Dapper;
using System.Linq;
using DAL.Interfaces;
using System.Threading;
using System.Threading.Tasks;
using DAL.Models;
using System.Data.SqlClient;
using Npgsql;
using Microsoft.EntityFrameworkCore;

namespace DAL.Service
{
     public class user_service : Iuser
    {
        private DBContext _context;
      
        
        public user_service(DBContext context)
        {
            _context = context;
            //_authService = authService;
            var a = _context.users.Count();
        }

        public int CountUsers()
        {
            return _context.users.Count();
        }

        public async Task<user_response> GetUsers(request req)
        {
          user_response response = new user_response();
          using (NpgsqlConnection conn = _context.ReturnSQLConn())
            {
                var q = await conn.QueryMultipleAsync("SELECT * FROM get_users(" + req.page_no.ToString() + "," + req.page_size.ToString() + ",'" + req.search_text + "','" + req.order + "')"  );
                response.users = q.Read<user>().AsList();
                var count = await conn.QueryMultipleAsync("SELECT get_users_count('" + req.search_text + "')");
                response.user_count = count.Read<int>().FirstOrDefault();
            }
            return response;
        }

        public bool Validation(user u, ref user_response_add response)
        {
            var email = _context.users.Where(x => x.email == u.email).Where(x => x.id != u.id).Count();
            var username = _context.users.Where(x => x.username == u.username).Where(x => x.id != u.id).Count();

            if (email > 0 || username > 0)
            {
                response.success = false;
                if (email > 0)
                    response.response.Add("Email already exists");
                if (username > 0)
                    response.response.Add("Username already exists");

                return false;
            }
            else
            {
                return true;
            }

        }

        public async Task<user_response_add> AddUser(user u)
        {
            auth_service _authService = new auth_service(_context);
            auth_hash pass = new auth_hash();
            pass = _authService.Encrypt(u.password);
            u.password = pass.hash;
            u.salt = pass.salt;
            user_response_add response = new user_response_add();
            response.response = new List<string>();
            if(Validation(u, ref response))
            {
                if(u.id == 0)
                _context.users.Add(u);
                else
                _context.users.Update(u);

                await _context.SaveChangesAsync();
                response.success = true;
                response.id = u.id;
            }

            return response;
           
        }

        public async Task<role_list> GetRoles(request req)
        {
            if (req.search_text == null)
                req.search_text = "";

            var query = from r in _context.roles
                        .Skip(req.page_size * (req.page_no - 1))
                        .Take(req.page_size)
                        .Where(x=> x.name.Contains(req.search_text))
                        select new role_item
                        {
                            role = new role { id = r.id, name = r.name },
                            rights = 
                            (from rr in _context.role_rights
                                      join ri in _context.rights on rr.right_id equals ri.id
                                      where rr.role_id == r.id
                                      select new right { id = ri.id, name = ri.name, description = ri.description }).ToList()

                        };

            role_list response = new role_list();
            response.roles = await query.ToListAsync();
            response.roles_count = await _context.roles.Where(x => x.name.Contains(req.search_text)).CountAsync();

            return response;

        }


       


    }
}
