using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models
{
    public class request
    {
        public int page_no { get; set; }
        public int page_size { get; set; }
        public string search_text { get; set; }
        public string order { get; set; }
    }
}
