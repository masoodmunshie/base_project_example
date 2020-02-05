using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DAL.Models
{
    public class role_rights
    {
        [Key]
        public int role_rights_id { get; set; }
        public int right_id { get; set; }
        public int role_id { get; set; }
    }
}
