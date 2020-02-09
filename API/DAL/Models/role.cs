using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models
{
    public class role
    {
        public int id { get; set; }
        public string name { get; set; }
    }

    public class role_item
    {
        public role role { get; set; }

        public List<right> rights { get; set; }
    }

    public class role_list
    {
        public List<role_item> roles { get; set; }
        public int roles_count { get; set; }
    }

}
