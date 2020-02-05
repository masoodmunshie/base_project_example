using DAL.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using DAL.Interfaces;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace DAL.Service
{
    public class auth_service : Iauth
    {
        private DBContext _context;

        public auth_service(DBContext context)
        {
            _context = context;
        }
          

        private string GenerateJWTToken(user User)
        {
            var key = Encoding.ASCII.GetBytes("topgear196719671967");
            var signinCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);
            List<Claim> Claims = new List<Claim>();
            Claims.Add(new Claim("userID", User.id.ToString()));
            


            var tokeOptions = new JwtSecurityToken(
                issuer: "http://localhost:44394",
                audience: "http://localhost:44394",
                claims: Claims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: signinCredentials
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);

            return tokenString;
        }

        //get the rights from the database

        public async Task<List<right>> GetRights(int userid)
        {
            List<right> response = new List<right>();

            var q = from u in _context.users
                    join r in _context.roles on u.role_id equals r.id
                    join rr in _context.role_rights on r.id equals rr.role_id
                    join ri in _context.rights on rr.right_id equals ri.id
                    where u.id == userid
                    select new right { id = ri.id, name = ri.name, api = ri.api };

            response = await q.ToListAsync();

            return response;
        }

        public async Task<auth_response> Login(auth Auth)
        {
            auth_response ar = new auth_response();
             var user =  await _context.users.Where(x => x.email == Auth.username).FirstOrDefaultAsync();
           // var user2 = _context.users.Where(x => x.email == Auth.username).FirstOrDefault();



            if (user != null && VerifyPassword(Auth, user))
            {
                ar.token = GenerateJWTToken(user);
                ar.success = true;
            }
            else
            {
                ar.success = false;
                ar.message = "Invalid Login";
            }

            return ar;
        }

        public bool VerifyPassword(auth userLogin, user u)
        {

            byte[] salted = new byte[128 / 8];
            salted = Convert.FromBase64String(u.salt);
            string hash = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: userLogin.password,
                salt: salted,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            var a = u.password;
            var b = hash;
            return (u.password == hash) ? true : false;

        }

        public auth_hash Encrypt(string password)
        {

            auth_hash response = new auth_hash();

            
                byte[] salted = new byte[128 / 8];
                using (var rng = RandomNumberGenerator.Create())
                {
                    rng.GetBytes(salted);
                }
                response.salt = Convert.ToBase64String(salted);
                response.hash = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salted,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            return response;
            
        }



    }


}
