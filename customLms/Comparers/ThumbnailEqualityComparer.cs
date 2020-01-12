using customLms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace customLms.Comparers
{
    public class ThumbnailEqualityComparer : IEqualityComparer<ThumbnailModel>
    {
        public bool Equals(ThumbnailModel thumb1, ThumbnailModel thumb2)
        {
            return thumb1.courseId.Equals(thumb2.courseId);
        }

        public int GetHashCode(ThumbnailModel thumb)
        {
            return thumb.courseId;
        }
    }
}