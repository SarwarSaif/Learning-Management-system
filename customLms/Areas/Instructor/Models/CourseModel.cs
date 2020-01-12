using customLms.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace customLms.Areas.Instructor.Models
{
    public class CourseModel
    {

        public int id { get; set; }


        [MaxLength(255)]
        [Required]
        public string name { get; set; }

        [MaxLength(1024)]

        public string searchTags { get; set; }

        [MaxLength(2048)]
        [DisplayName("Description")]
        public string description { get; set; }

        [MaxLength(1024)]
        [DisplayName("Image Url")]
        public string imgUrl { get; set; }
       
        public int courseLinkTextId { get; set; }
        public int catagoryId { get; set; }
        [DisplayName("Course Link Tag")]
        public ICollection<CourseLinkText> courseLinkTexts { get; set; }
        [DisplayName("Course Catagory")]
        public ICollection<Catagory> catagories { get; set; }
        public string Catagory
        {

            get
            {
                return catagories == null || catagories.Count.Equals(0) ?
                    string.Empty : catagories.First(c => c.id.Equals(catagoryId)).name;

            }
        }

        public string CourseLinkText
        {

            get
            {
                return courseLinkTexts == null || courseLinkTexts.Count.Equals(0) ?
                    string.Empty : courseLinkTexts.First(c => c.id.Equals(courseLinkTextId)).name;

            }
        }


    }
}