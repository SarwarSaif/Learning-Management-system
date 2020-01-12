using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using customLms.Entities;
using customLms.Models;
using customLms.Areas.Instructor.Models;
using customLms.Areas.Instructor.Extensions;

namespace customLms.Areas.Instructor.Controllers
{
    [Authorize(Roles = "Instructor")]
    public class CourseContentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Instructor/CourseContents
        public async Task<ActionResult> Index()
        {
            return View(await db.courseContents.Convert(db));
        }

        // GET: Instructor/CourseContents/Details/5
        public async Task<ActionResult> Details(int? contentId, int? courseId)
        {
            if (contentId == null || courseId==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CourseContent courseContent = await GetCourseContent(contentId, courseId);
            if (courseContent == null)
            {
                return HttpNotFound();
            }
            return View(await courseContent.Convert(db));
        }

        // GET: Instructor/CourseContents/Create
        public async Task< ActionResult> Create()
        {
            var model = new CourseContentModel
            {
                contents = await db.contents.ToListAsync(),
                courses = await db.courses.ToListAsync()


            };

            return View(model);
        }

        // POST: Instructor/CourseContents/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "courseId,contentId")] CourseContent courseContent)
        {
            if (ModelState.IsValid)
            {
                db.courseContents.Add(courseContent);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(courseContent);
        }

        // GET: Instructor/CourseContents/Edit/5
        public async Task<ActionResult> Edit(int? contentId, int? courseId)
        {
            if (contentId == null || courseId==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CourseContent courseContent = await GetCourseContent(contentId ,courseId);
            if (courseContent == null)
            {
                return HttpNotFound();
            }
            return View(await courseContent.Convert(db));
        }

        // POST: Instructor/CourseContents/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "courseId,contentId,OldCourseId,OldContentId")] CourseContent courseContent)
        {
            if (ModelState.IsValid)
            {
                var canChange = await courseContent.CanChange(db);
                if(canChange)
                {
                    await courseContent.Change(db);
                }
                return RedirectToAction("Index");
            }
            return View(courseContent);
        }

        // GET: Instructor/CourseContents/Delete/5
        public async Task<ActionResult> Delete(int? contentId, int? courseId)
        {
            if (contentId == null || courseId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CourseContent courseContent = await GetCourseContent(contentId, courseId);
            if (courseContent == null)
            {
                return HttpNotFound();
            }
            return View(await courseContent.Convert(db));
        }

        // POST: Instructor/CourseContents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int contentId,int courseId)
        {
            CourseContent courseContent = await GetCourseContent(contentId,courseId);
            db.courseContents.Remove(courseContent);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        //httpgetmethod
        private async Task<CourseContent>GetCourseContent(int? contentId, int? courseId)
        {
            try {
                int conId = 0, crsId = 0;
                int.TryParse(contentId.ToString(), out conId);
                int.TryParse(courseId.ToString(), out crsId);

                var courseContent = await db.courseContents.FirstOrDefaultAsync(
                    cc => cc.courseId.Equals(crsId) && cc.contentId.Equals(conId));
                return courseContent;           }
            catch
            {
                return null;
            }


        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
