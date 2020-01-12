using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using customLms.Entities;
using customLms.Models;
using customLms.Areas.Instructor.Models;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Transactions;

namespace customLms.Areas.Instructor.Extensions
{
    public static class ConversionExtensions
    {
        #region course

        public static async Task<IEnumerable<CourseModel>> Convert(this IEnumerable<Course> courses, ApplicationDbContext db)
        {
            if (courses.Count().Equals(0))
                return new List<CourseModel>();
            var texts = await db.coursesLinkTexts.ToListAsync();
            var types = await db.catagories.ToListAsync();

            return from c in courses
                   select new CourseModel
                   {
                       id = c.id,
                       name = c.name,
                       description = c.description,
                       imgUrl = c.imgUrl,
                       courseLinkTextId = c.courseLinkTextId,
                       catagoryId = c.catagoryId,
                       courseLinkTexts = texts,
                       catagories = types


                   };



        }

        public static async Task<CourseModel> Convert(this Course courses, ApplicationDbContext db)
        {

            var text = await db.coursesLinkTexts.FirstOrDefaultAsync(
                        p => p.id.Equals(courses.courseLinkTextId)

                );
            var type = await db.catagories.FirstOrDefaultAsync(
                p => p.id.Equals(courses.catagoryId));

            var model = new CourseModel

            {
                id = courses.id,
                name = courses.name,
                description = courses.description,
                imgUrl = courses.imgUrl,
                courseLinkTextId = courses.courseLinkTextId,
                catagoryId = courses.catagoryId,
                courseLinkTexts = new List<CourseLinkText>(),
                catagories = new List<Catagory>()


            };

            model.courseLinkTexts.Add(text);
            model.catagories.Add(type);
            return model;

        }

        #endregion




        #region coursecontent

        public static async Task<IEnumerable<CourseContentModel>> Convert(this IQueryable<CourseContent> CourseContents, ApplicationDbContext db)
        {
            if (CourseContents.Count().Equals(0))
                return new List<CourseContentModel>();


            return await (from cc in CourseContents
                          select new CourseContentModel
                          {
                              contentId = cc.contentId,
                              courseId = cc.courseId,
                              contentName = db.contents.FirstOrDefault(
                                 i => i.id.Equals(cc.contentId)).name,

                              courseName = db.courses.FirstOrDefault(
                                 p => p.id.Equals(cc.courseId)).name


                          }).ToListAsync();








        }

        public static async Task<CourseContentModel> Convert(this CourseContent courseContents, ApplicationDbContext db)
        {


            var model = new CourseContentModel

            {
                contentId = courseContents.contentId,
                courseId = courseContents.courseId,
                contents = await db.contents.ToListAsync(),
                courses = await db.courses.ToListAsync()



            };


            return model;

        }


        public static async Task<bool> CanChange(this CourseContent courseContent, ApplicationDbContext db)
        {
            var oldCC = await db.courseContents.CountAsync(

                cc => cc.courseId.Equals(courseContent.OldCourseId) &&
                cc.contentId.Equals(courseContent.OldContentId));

            var newCC = await db.courseContents.CountAsync(

                cc => cc.courseId.Equals(courseContent.courseId) &&
                cc.contentId.Equals(courseContent.contentId));


            return oldCC.Equals(1) && newCC.Equals(0);
        }

        public static async Task Change(this CourseContent courseContent, ApplicationDbContext db)
        {
            var OldCourseContent = await db.courseContents.FirstOrDefaultAsync(
                cc => cc.courseId.Equals(courseContent.OldCourseId) && cc.contentId.Equals(courseContent.OldContentId)

                );


            var NewCourseContent = await db.courseContents.FirstOrDefaultAsync(
                cc => cc.courseId.Equals(courseContent.courseId) && cc.contentId.Equals(courseContent.contentId)

                );


            if (OldCourseContent != null && NewCourseContent == null)
            {
                NewCourseContent = new CourseContent
                {
                    contentId = courseContent.contentId,
                    courseId = courseContent.courseId

                };
                using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {


                    try
                    {
                        db.courseContents.Remove(OldCourseContent);
                        db.courseContents.Add(NewCourseContent);
                        await db.SaveChangesAsync();
                        transaction.Complete();
                    }
                    catch { transaction.Dispose(); }
                }



            }


        }


        public static async Task<CourseContentModel> Convert(this CourseContent courseContents, ApplicationDbContext db, bool addListData = true)
        {


            var model = new CourseContentModel

            {
                contentId = courseContents.contentId,
                courseId = courseContents.courseId,
                contents = addListData ? await db.contents.ToListAsync() : null,
                courses = addListData ? await db.courses.ToListAsync() : null,
                contentName = (await db.contents.FirstOrDefaultAsync(
                    i => i.id.Equals(courseContents.contentId))).name,
                courseName = (await db.courses.FirstOrDefaultAsync(
                    c => c.id.Equals(courseContents.courseId))).name


            };


            return model;

        }

        #endregion


        #region subOf courses

        public static async Task<IEnumerable<SubscriptionOfCourseModel>> Convert(this IQueryable<SubscriptionOfCourse> subscriptionOfCourses, ApplicationDbContext db)
        {
            if (subscriptionOfCourses.Count().Equals(0))
                return new List<SubscriptionOfCourseModel>();


            return await (from cc in subscriptionOfCourses
                          select new SubscriptionOfCourseModel
                          {
                              subscriptionId = cc.subscriptionId,
                              courseId = cc.courseId,
                              subscriptionName = db.subscriptions.FirstOrDefault(
                                 i => i.id.Equals(cc.subscriptionId)).name,

                              courseName = db.courses.FirstOrDefault(
                                 p => p.id.Equals(cc.courseId)).name


                          }).ToListAsync();








        }


        public static async Task<SubscriptionOfCourseModel> Convert(this SubscriptionOfCourse subscriptionOfCourses, ApplicationDbContext db)
        {


            var model = new SubscriptionOfCourseModel

            {
                courseId = subscriptionOfCourses.courseId,
                subscriptionId = subscriptionOfCourses.subscriptionId,
                courses = await db.courses.ToListAsync(),
                subscriptions = await db.subscriptions.ToListAsync()



            };


            return model;

        }


        public static async Task<bool> CanChange(this SubscriptionOfCourse subscriptionOfCourses, ApplicationDbContext db)
        {
            var oldCC = await db.subscriptionOfCourses.CountAsync(

                cc => cc.courseId.Equals(subscriptionOfCourses.OldCourseId) &&
                cc.subscriptionId.Equals(subscriptionOfCourses.OldSubscriptionId));

            var newCC = await db.subscriptionOfCourses.CountAsync(

                cc => cc.courseId.Equals(subscriptionOfCourses.courseId) &&
                cc.subscriptionId.Equals(subscriptionOfCourses.subscriptionId));


            return oldCC.Equals(1) && newCC.Equals(0);
        }

        public static async Task Change(this SubscriptionOfCourse subscriptionOfCourses, ApplicationDbContext db)
        {
            var OldSubCourses = await db.subscriptionOfCourses.FirstOrDefaultAsync(
                cc => cc.courseId.Equals(subscriptionOfCourses.OldCourseId) && cc.subscriptionId.Equals(subscriptionOfCourses.OldSubscriptionId)

                );


            var NewSubCourses = await db.subscriptionOfCourses.FirstOrDefaultAsync(
                cc => cc.courseId.Equals(subscriptionOfCourses.courseId) && cc.subscriptionId.Equals(subscriptionOfCourses.subscriptionId)

                );


            if (OldSubCourses != null && NewSubCourses == null)
            {
                 NewSubCourses = new SubscriptionOfCourse
                {
                    subscriptionId = subscriptionOfCourses.subscriptionId,
                    courseId =subscriptionOfCourses .courseId

                };
                using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {


                    try
                    {
                        db.subscriptionOfCourses.Remove(OldSubCourses);
                        db.subscriptionOfCourses.Add(NewSubCourses);
                        await db.SaveChangesAsync();
                        transaction.Complete();
                    }
                    catch { transaction.Dispose(); }
                }



            }


        }


        public static async Task<SubscriptionOfCourseModel> Convert(this SubscriptionOfCourse subscriptionOfCourses, ApplicationDbContext db, bool addListData = true)
        {


            var model = new SubscriptionOfCourseModel

            {
                subscriptionId = subscriptionOfCourses.subscriptionId,
                courseId = subscriptionOfCourses.courseId,
               subscriptions = addListData ? await db.subscriptions.ToListAsync() : null,
                courses = addListData ? await db.courses.ToListAsync() : null,
                subscriptionName = (await db.subscriptions.FirstOrDefaultAsync(
                    i => i.id.Equals(subscriptionOfCourses.subscriptionId))).name,
                courseName = (await db.courses.FirstOrDefaultAsync(
                    c => c.id.Equals(subscriptionOfCourses.courseId))).name


            };


            return model;

        }




        #endregion





    } 
}