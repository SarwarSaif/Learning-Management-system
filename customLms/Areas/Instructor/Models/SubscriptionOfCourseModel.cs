using customLms.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace customLms.Areas.Instructor.Models
{
    public class SubscriptionOfCourseModel
    {
          [DisplayName("Course Id")]
        
        public int courseId { get; set; }
        [DisplayName("Subscription Id")]
        public int subscriptionId { get; set; }
        [DisplayName("Course Name")]
        public string courseName { get; set; }
        [DisplayName("Subscription Name")]
        public string subscriptionName { get; set; }

        public ICollection<Course> courses { get; set; }
        public ICollection<Subscription> subscriptions { get; set; }
    }
}