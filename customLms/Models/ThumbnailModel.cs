using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace customLms.Models
{
    public class ThumbnailModel
    {
        public int courseId { get; set; }
        public int SubscriptionId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string TagText { get; set; }

        public string ImageUrl { get; set; }

        public string ContentTag { get; set; }
        public string Link { get; internal set; }
    }
}