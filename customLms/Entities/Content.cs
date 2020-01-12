using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace customLms.Entities
{   
    [Table("Content")]
    public class Content
    { 
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [MaxLength(255)]
        [Required]
        [DisplayName("Name of Content")]
        public string name { get; set; }

        [MaxLength(2048)]
        [DisplayName("Description")]
        public string description { get; set; }
        [MaxLength(1024)]
        [DisplayName("Url")]
        public string url { get; set; }

        [MaxLength(1024)]
        [DisplayName("Image/doc/pdf url")]
        public string imgUrl { get; set; }

        

        public int courseId { get; set; }
        public int partId { get; set; }
        public int contentTypeId { get; set; }
        public int sectionId { get; set; }
         

            [DisplayName("Content Type ")] //displaying the type name
        public ICollection<ContentType> contentTypes { get; set; }


        [DisplayName("Part ")] //displaying the type name
        public ICollection<Part> parts { get; set; }



        [DisplayName("section ")] //displaying the type name
        public ICollection<Section>sections{ get; set; }












    }
}