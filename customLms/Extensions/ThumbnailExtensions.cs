using customLms.Comparers;
using customLms.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace customLms.Extensions
{
    public static class ThumbnailExtensions
    {
        private static async Task<List<int>> GetSubscriptionIdAsync(
            string studentId = null, ApplicationDbContext db = null)
        {
            try
            {
                if (studentId == null) return new List<int>();
                if (db == null) db = ApplicationDbContext.Create();

                return await (
                    from us in db.studentSubscriptions
                    where us.studentId.Equals(studentId)
                    select us.subscriptionId).ToListAsync();
            }
            catch { }
            return new List<int>();
        }


        public static async Task<IEnumerable<ThumbnailModel>> GetProductThumbnailsAsync(
            this List<ThumbnailModel> thumbnails, string studentId = null,
            ApplicationDbContext db = null)
        {
            try
            {
                if (studentId == null) return new List<ThumbnailModel>();
                if (db == null) db = ApplicationDbContext.Create();

                var subscriptionIds = await GetSubscriptionIdAsync(studentId, db);

                thumbnails = await (
                    from ps in db.subscriptionOfCourses
                    join p in db.courses on ps.courseId equals p.id
                    join plt in db.coursesLinkTexts on p.courseLinkTextId equals plt.id
                    join pt in db.catagories on p.catagoryId equals pt.id
                    where subscriptionIds.Contains(ps.subscriptionId)
                    select new ThumbnailModel
                    {
                        courseId = p.id,
                        SubscriptionId = ps.subscriptionId,
                        Title = p.name,
                        Description = p.description,
                        ImageUrl = p.imgUrl,
                        //Link = $"/CourseContents/Index/{p.id}",
                        Link = "/CourseContents/Index/" + p.id,
                        TagText = plt.name,
                        ContentTag = pt.name
                    }).ToListAsync();

            }
            catch { }
            return thumbnails.Distinct(new ThumbnailEqualityComparer()).OrderBy(o => o.Title);
        }
    }
}