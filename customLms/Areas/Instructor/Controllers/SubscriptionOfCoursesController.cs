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

namespace customLms.Areas.Instructor.Controllers
{
    [Authorize(Roles = "Instructor")]
    public class SubscriptionOfCoursesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Instructor/SubscriptionOfCourses
        public async Task<ActionResult> Index()
        {
            return View(await db.subscriptionOfCourses.Convert(db));
        }

        // GET: Instructor/SubscriptionOfCourses/Details/5
        public async Task<ActionResult> Details(int? subscriptionId,int? courseId)
        {
            if (subscriptionId == null || courseId==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubscriptionOfCourse subscriptionOfCourse = await GetCourseSubscription(subscriptionId,courseId);
            if (subscriptionOfCourse == null)
            {
                return HttpNotFound();
            }
            return View(await subscriptionOfCourse.Convert(db));
        }

        // GET: Instructor/SubscriptionOfCourses/Create
        public async Task<ActionResult> Create()
        {
            var model = new SubscriptionOfCourseModel
            {






                subscriptions = await db.subscriptions.ToListAsync(),
                courses = await db.courses.ToListAsync()


            };



            return View(model);
        }

        // POST: Instructor/SubscriptionOfCourses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "courseId,subscriptionId")] SubscriptionOfCourse subscriptionOfCourse)
        {
            if (ModelState.IsValid)
            {
                db.subscriptionOfCourses.Add(subscriptionOfCourse);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(subscriptionOfCourse);
        }

        // GET: Instructor/SubscriptionOfCourses/Edit/5
        public async Task<ActionResult> Edit(int? subscriptionId,int? courseId)
        {
            if (subscriptionId == null || courseId==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubscriptionOfCourse subscriptionOfCourse = await GetCourseSubscription(subscriptionId, courseId);
            if (subscriptionOfCourse == null)
            {
                return HttpNotFound();
            }
            return View(await subscriptionOfCourse.Convert(db));
        }

        // POST: Instructor/SubscriptionOfCourses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "courseId,subscriptionId,OldCourseId,OldSubscriptionId")] SubscriptionOfCourse subscriptionOfCourse)
        {
            if (ModelState.IsValid)
            {
                var canChange = await subscriptionOfCourse.CanChange(db);
                if (canChange)
                {
                    await subscriptionOfCourse.Change(db);
                }
                return RedirectToAction("Index");
            }
            return View(subscriptionOfCourse);
        }

        // GET: Instructor/SubscriptionOfCourses/Delete/5
        public async Task<ActionResult> Delete(int? subscriptionId,int? courseId)
        {
            if (subscriptionId == null || courseId==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubscriptionOfCourse subscriptionOfCourse = await GetCourseSubscription(subscriptionId, courseId);
            if (subscriptionOfCourse == null)
            {
                return HttpNotFound();
            }
            return View(await subscriptionOfCourse.Convert(db));
        }

        // POST: Instructor/SubscriptionOfCourses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int subscriptionId, int courseId)
        {
            SubscriptionOfCourse subscriptionOfCourse = await GetCourseSubscription(subscriptionId,courseId);
            db.subscriptionOfCourses.Remove(subscriptionOfCourse);
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
        private async Task<SubscriptionOfCourse> GetCourseSubscription(int? subscriptionId, int? courseId)
        {
            try
            {
                int subId = 0, crsId = 0;
                int.TryParse(subscriptionId.ToString(), out subId);
                int.TryParse(courseId.ToString(), out crsId);

                var subOfcourse = await db.subscriptionOfCourses.FirstOrDefaultAsync(
                    cc => cc.courseId.Equals(crsId) && cc.subscriptionId.Equals(subId));
                return subOfcourse;
            }
            catch
            {
                return null;
            }


        }



    }





}
