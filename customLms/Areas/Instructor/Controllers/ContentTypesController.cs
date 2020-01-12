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
    public class ContentTypesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Instructor/ContentTypes
        public async Task<ActionResult> Index()
        {
            return View(await db.contentTypes.ToListAsync());
        }

        // GET: Instructor/ContentTypes/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContentType contentType = await db.contentTypes.FindAsync(id);
            if (contentType == null)
            {
                return HttpNotFound();
            }
            return View(contentType);
        }

        // GET: Instructor/ContentTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Instructor/ContentTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id,name")] ContentType contentType)
        {
            if (ModelState.IsValid)
            {
                db.contentTypes.Add(contentType);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(contentType);
        }

        // GET: Instructor/ContentTypes/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContentType contentType = await db.contentTypes.FindAsync(id);
            if (contentType == null)
            {
                return HttpNotFound();
            }
            return View(contentType);
        }

        // POST: Instructor/ContentTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id,name")] ContentType contentType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(contentType).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(contentType);
        }

        // GET: Instructor/ContentTypes/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContentType contentType = await db.contentTypes.FindAsync(id);
            if (contentType == null)
            {
                return HttpNotFound();
            }
            return View(contentType);
        }

        // POST: Instructor/ContentTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ContentType contentType = await db.contentTypes.FindAsync(id);
            db.contentTypes.Remove(contentType);
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
