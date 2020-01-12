using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace customLms.Entities
{   
    [Table("SubscriptionOfCourses")]
    public class SubscriptionOfCourse
    { 
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]



        [Required]
        [Key, Column(Order = 1)]
        public int courseId { get; set; }

        [Required]
        [Key,Column(Order =2)]
        public int subscriptionId { get; set; }


        [NotMapped]
        public int OldSubscriptionId { get; set; }
        [NotMapped]
        public int OldCourseId { get; set; }




    }
}