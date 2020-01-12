using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace customLms.Entities
{   
    [Table("Course")]
    public class Course
    { 
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

       
        [MaxLength(255)]
        [Required]
        public string name { get; set; }

        [MaxLength(1024)]
        
        public string searchTags { get; set; }

        [MaxLength(2048)]
         
        public string description { get; set; }

        [MaxLength(1024)]

        public string imgUrl { get; set; }

        public int courseLinkTextId { get; set; }
        public int catagoryId { get; set; }






    }
}