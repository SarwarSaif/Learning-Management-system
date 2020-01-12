using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace customLms.Entities
{   
    [Table("Subscription")]
    public class Subscription
    { 
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [MaxLength(255)]
        [Required]
        [DisplayName("Name")]
        public string name { get; set; }

        [MaxLength(2048)]
        [DisplayName("Description")]
        public string description { get; set; }

        [MaxLength(255)]
        [DisplayName("Resgistration Code")]
        public string regCode { get; set; }

        



    }
}