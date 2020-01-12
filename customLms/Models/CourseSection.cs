using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace customLms.Models
{
    public class CourseSection
    {
        public int id { get; set; }

        public string Title { get; set; }

        public int ItemTypeId { get; set; }

        public IEnumerable<CourseSectionRow> Items { get; set; }
    }
}