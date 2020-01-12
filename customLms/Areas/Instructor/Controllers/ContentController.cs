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
    public class ContentController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Instructor/Content
        public async Task<ActionResult> Index()
        {
            return View(await db.contents.ToListAsync());
        }

        // GET: Instructor/Content/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Content content = await db.contents.FindAsync(id);
            if (content == null)
            {
                return HttpNotFound();
            }
            return View(content);
        }

        // GET: Instructor/Content/Create
        public ActionResult Create()
        {

            var model = new Content
            {

                contentTypes = db.contentTypes.ToList(),
                parts=db.parts.ToList(),
                sections=db.sections.ToList()

            };


            return View(model);
        }

        // POST: Instructor/Content/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id,name,description,url,imgUrl,permitDays,courseId,partId,contentTypeId,sectionId")] Content content)
        {
            if (ModelState.IsValid)
            {
                db.contents.Add(content);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(content);
        }

        // GET: Instructor/Content/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Content content = await db.contents.FindAsync(id);
            if (content == null)
            {
                return HttpNotFound();
            }

            content.contentTypes = await db.contentTypes.ToListAsync();
            content.parts = await db.parts.ToListAsync();
            content.sections = await db.sections.ToListAsync();

            return View(content);
        }

        // POST: Instructor/Content/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id,name,description,url,imgUrl,permitDays,courseId,partId,contentTypeId,sectionId")] Content content)
        {
            if (ModelState.IsValid)
            {
                db.Entry(content).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(content);
        }

        // GET: Instructor/Content/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Content content = await db.contents.FindAsync(id);
            if (content == null)
            {
                return HttpNotFound();
            }
            return View(content);
        }

        // POST: Instructor/Content/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Content content = await db.contents.FindAsync(id);
            db.contents.Remove(content);
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
