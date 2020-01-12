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
using customLms.Areas.Instructor.Extensions;
using customLms.Areas.Instructor.Models;
using System.Threading.Tasks;

namespace customLms.Areas.Instructor.Controllers
{
    [Authorize(Roles = "Instructor")]
    public class CoursesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Instructor/Courses
        public async Task<ActionResult> Index()
        {

            var courses = await db.courses.ToListAsync();
            var model = await courses.Convert(db);

            return View(model);
        }

        // GET: Instructor/Courses/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = await db.courses.FindAsync(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            var model = await course.Convert(db);
            return View(model);
        }

        // GET: Instructor/Courses/Create
        public async Task<ActionResult>Create()
        {
            var model = new CourseModel
            {
                courseLinkTexts = await db.coursesLinkTexts.ToListAsync(),
                catagories = await db.catagories.ToListAsync(),

            };

            return View(model);
        }

        // POST: Instructor/Courses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id,name,searchTags,description,imgUrl,courseLinkTextId,catagoryId")] Course course)
        {
            if (ModelState.IsValid)
            {
                db.courses.Add(course);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(course);
        }

        // GET: Instructor/Courses/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = await db.courses.FindAsync(id);
            if (course == null)
            {
                return HttpNotFound();
            }

            var crs = new List<Course>();
            crs.Add(course);
            var courseModel = await crs.Convert(db);

            return View(courseModel.First()); //fetch the frst one
        }

        // POST: Instructor/Courses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id,name,searchTags,description,imgUrl,courseLinkTextId,catagoryId")] Course course)
        {
            if (ModelState.IsValid)
            {
                db.Entry(course).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(course);
        }

        // GET: Instructor/Courses/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = await db.courses.FindAsync(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            var model = await course.Convert(db);
            return View(model);
        }

        // POST: Instructor/Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Course course = await db.courses.FindAsync(id);
            db.courses.Remove(course);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
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
