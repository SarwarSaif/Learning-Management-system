using customLms.Extensions;
using customLms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace customLms.Controllers
{
    [Authorize]
    public class CourseContentController : Controller
    {
        // GET: CourseContent
        public async Task<ActionResult> Index(int id)
        {
            var studentId = Request.IsAuthenticated ? HttpContext.GetUserId() : null;
            var sections = await SectionExtensions.GetCourseSectionAsync(id, studentId);
            return View(sections);
        }
    }
}