using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace customLms.Areas.Instructor.Models
{
    public class SmallButtonModel
    {
        public string Action { get; set; }
        public string Text { get; set; }
        public string Glyph { get; set; }
        public string ButtonType { get; set; }
      
        public int? Id { get; set; }
        public int? ContentId { get; set; }
        public int? CourseId { get; set; }
        public int? SubscriptionId { get; set; }
        public string studentId { get; set; }
        public string ActionParameters {

            get
            {
                var param = new StringBuilder("?");
                if(Id!=null && Id>0)
                {

                    param.Append(string.Format("{0}={1}&", "id", Id));


                }

                if (ContentId != null && ContentId > 0)
                {

                    param.Append(string.Format("{0}={1}&", "contentId", ContentId));


                }

                if (CourseId != null && CourseId > 0)
                {

                    param.Append(string.Format("{0}={1}&", "Courseid", CourseId));


                }
                if (studentId != null && !studentId.Equals(string.Empty))
                {
                    param.Append(string.Format("{0}={1}&", "studentId", studentId));
                        }
                return param.ToString().Substring(0, param.Length - 1); //excluding & sign thats why -1










            }


        }




    }
}