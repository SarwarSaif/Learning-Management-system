using customLms.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace customLms.Models
{
    public class StudentSubscriptionViewModel
    {
        public ICollection<Subscription> subscriptions { get; set; }
        public ICollection<StudentSubscriptionModel>studentSubscriptions { get; set; }
        public bool disableDropDown { get; set; }
        public string studentId { get; set; }
        public int subscriptionId { get; set; }


    }
}