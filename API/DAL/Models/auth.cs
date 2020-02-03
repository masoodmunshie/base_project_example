using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DAL.Models
{
    public class auth
    {
        [Key]
        public string username { get; set; }
        public string password { get; set; }
    }

    public class auth_response
    {
        public bool success { get; set; }
        public string message { get; set; }
        public string token { get; set; }
    }

    public class auth_hash
    {
        public string hash { get; set; }
        public string salt { get; set; }
    }
}
