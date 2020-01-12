using customLms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace customLms.Comparers
{
    public class CourseSectionEqualityComparer : IEqualityComparer<CourseSection>
    {
        public bool Equals(CourseSection section1, CourseSection section2)
        {
            return section1.id.Equals(section2.id);
        }

        public int GetHashCode(CourseSection section)
        {
            return (section.id).GetHashCode();
        }
    }
}