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
    public static class SectionExtensions
    {
        public static async Task<CourseSectionModel> GetCourseSectionAsync(int courseId, string studentId)
        {
            var db = ApplicationDbContext.Create();

            var sections = await (
                from p in db.courses
                join pi in db.courseContents on p.id equals pi.courseId
                join i in db.contents on pi.contentId equals i.id
                join s in db.sections on i.sectionId equals s.id
                where p.id.Equals(courseId)
                orderby s.name
                select new CourseSection
                {
                    id = s.id,
                    ItemTypeId = i.contentTypeId,
                    Title = s.name

                }).ToListAsync();

            foreach (var section in sections)
                section.Items = await GetCourseSectionRowAsync(courseId, section.id, section.ItemTypeId, studentId);

            var result = sections.Distinct(new CourseSectionEqualityComparer()).ToList();

            var union = result.Where(r => !r.Title.ToLower().Contains("download"))
                .Union(result.Where(r => r.Title.ToLower().Contains("download")));

            var model = new CourseSectionModel
            {
                Sections = union.ToList(),
                Title = await (from p in db.courses
                               where p.id.Equals(courseId)
                               select p.name).FirstOrDefaultAsync()
            };
            return model;
        }

        public static async Task<IEnumerable<CourseSectionRow>> GetCourseSectionRowAsync(
            int courseId, int sectionId, int contentTypeId,
            string studentId, ApplicationDbContext db = null)
        {
            if (db == null) db = ApplicationDbContext.Create();

            var today = DateTime.Now.Date;

            var contents = await (from i in db.contents
                                  join it in db.contentTypes on i.contentTypeId equals it.id
                                  join pi in db.courseContents on i.id equals pi.contentId
                                  join sp in db.subscriptionOfCourses on pi.courseId equals sp.courseId
                                  join us in db.studentSubscriptions on sp.subscriptionId equals us.subscriptionId
                                  where i.sectionId.Equals(sectionId) &&
                                  i.contentTypeId.Equals(contentTypeId) &&
                                  pi.courseId.Equals(courseId) &&
                                  us.studentId.Equals(studentId)
                                  orderby i.partId
                                  select new CourseSectionRow
                                  {
                                      ItemId = i.id,
                                      Description = i.description,
                                      Title = i.name,
                                      Link = "/CourseContent/Content/" + pi.courseId + "/" + i.id,
                                      ImageUrl = i.imgUrl,
                                      ReleaseDate = DbFunctions.CreateDateTime(us.startDate.Value.Year,
                                     us.startDate.Value.Month, us.startDate.Value.Day +20, 0, 0, 0),
                                      IsAvailable = DbFunctions.CreateDateTime(today.Year,
                                     today.Month, today.Day, 0, 0, 0) >= DbFunctions.CreateDateTime(us.startDate.Value.Year,
                                     us.startDate.Value.Month, us.startDate.Value.Day + 20, 0, 0, 0)
                                  }).ToListAsync();
            return contents;
        }
    }
}