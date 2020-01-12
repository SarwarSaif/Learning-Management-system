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

namespace customLms.Areas.Instructor.Controllers
{
    [Authorize(Roles = "Instructor")]
    public class CourseLinkTextsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Instructor/CourseLinkTexts
        public async Task<ActionResult> Index()
        {
            return View(await db.coursesLinkTexts.ToListAsync());
        }

        // GET: Instructor/CourseLinkTexts/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CourseLinkText courseLinkText = await db.coursesLinkTexts.FindAsync(id);
            if (courseLinkText == null)
            {
                return HttpNotFound();
            }
            return View(courseLinkText);
        }

        // GET: Instructor/CourseLinkTexts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Instructor/CourseLinkTexts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id,name")] CourseLinkText courseLinkText)
        {
            if (ModelState.IsValid)
            {
                db.coursesLinkTexts.Add(courseLinkText);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(courseLinkText);
        }

        // GET: Instructor/CourseLinkTexts/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CourseLinkText courseLinkText = await db.coursesLinkTexts.FindAsync(id);
            if (courseLinkText == null)
            {
                return HttpNotFound();
            }
            return View(courseLinkText);
        }

        // POST: Instructor/CourseLinkTexts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id,name")] CourseLinkText courseLinkText)
        {
            if (ModelState.IsValid)
            {
                db.Entry(courseLinkText).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(courseLinkText);
        }

        // GET: Instructor/CourseLinkTexts/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CourseLinkText courseLinkText = await db.coursesLinkTexts.FindAsync(id);
            if (courseLinkText == null)
            {
                return HttpNotFound();
            }
            return View(courseLinkText);
        }

        // POST: Instructor/CourseLinkTexts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            CourseLinkText courseLinkText = await db.coursesLinkTexts.FindAsync(id);
            db.coursesLinkTexts.Remove(courseLinkText);
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
