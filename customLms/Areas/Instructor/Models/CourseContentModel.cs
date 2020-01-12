using customLms.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace customLms.Areas.Instructor.Models
{
    public class CourseContentModel
    {
          [DisplayName("Course Id")]
        
        public int courseId { get; set; }
        [DisplayName("Content Id")]
        public int contentId { get; set; }
        [DisplayName("Course Name")]
        public string courseName { get; set; }
        [DisplayName("Content Name")]
        public string contentName { get; set; }

        public ICollection<Course> courses { get; set; }
        public ICollection<Content> contents { get; set; }
    }
}