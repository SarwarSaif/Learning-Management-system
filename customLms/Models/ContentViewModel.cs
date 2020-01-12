using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace customLms.Models
{
    public class ContentViewModel
    {
        public int courseId { get; set; }

        public string name { get; set; }

        public string description { get; set; }

        public string HTML { get; set; }

        public string VideoURL { get; set; }
    }
}