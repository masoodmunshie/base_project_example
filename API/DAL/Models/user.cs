using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models
{
    public class user
    {
        public int id { get; set; }
        public string username { get; set; }
        public string firstname { get; set; }
        public string surname { get; set; }
        public string password { get; set; }
        public string salt { get; set; }  
        public string email { get; set; }
        public string cell_number { get; set; }
        public string cell_code { get; set; }
        public string company { get; set; }
        public string profile_pic { get; set; }
        public int role_id { get; set; }
        public bool is_deleted { get; set; }


    }

    public class user_response
    {
        public List<user> users { get; set; }
        public int user_count { get; set; }
    }

    public class user_response_add
    {
        public int id { get; set; }
        public List<string> response { get; set; }

        public bool success { get; set; }
    }
}
