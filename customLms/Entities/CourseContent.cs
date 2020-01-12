using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace customLms.Entities
{   
    [Table("CourseContent")]
    public class CourseContent
    { 
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]



        [Required]
        [Key, Column(Order = 1)]
        public int courseId { get; set; }
        
        
        [Required]
       [Key,Column(Order =2)]
        public int contentId { get; set; }
        [NotMapped]
        public int OldCourseId { get;  set; }
        [NotMapped]
        public int OldContentId { get;  set; }
        
    }
}