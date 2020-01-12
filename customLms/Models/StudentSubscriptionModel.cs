using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace customLms.Models
{
    public class StudentSubscriptionModel
    {
        public int id { get; set; }
        [MaxLength(255)]
        [Required]
        public string name { get; set; }

        
        public string description { get; set; }

        public string regCode { get; set; }
        public DateTime? startDate { get; set; }
        public DateTime? endDate { get; set; }



    }
}