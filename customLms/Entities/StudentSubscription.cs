using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace customLms.Entities
{   
    [Table("StudentSubscription")]
    public class StudentSubscription
    { 
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]



        [Required]
        [Key, Column(Order = 1)]
        public int subscriptionId { get; set; }

        [Required]
        [Key,Column(Order =2)]
        [MaxLength(256)]
        public string studentId { get; set; } //string use korsi

        public DateTime?  startDate { get; set; }
        public DateTime? endDate { get; set; }
       }





    
}