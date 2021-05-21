using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proj_AGL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proj_AGL.Controllers
{
    public class PrintResultsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> userManager;

        public PrintResultsController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            this.userManager = userManager;
        }
        public async Task<IActionResult> Grades(int id)
        {
            var req = _context.Requests.Where(r => r.Id == id).FirstOrDefault();
            if (req == null)
                return NotFound();
            var grades = _context.Grades.Where(g => g.UserId == req.UserId).Include(g => g.User).Include(g => g.Course).ToListAsync();
            return View(await grades);
        }

        public IActionResult FollowLessons(int id)
        {
            var req = _context.Requests.Where(r => r.Id == id).Include(r=> r.User).FirstOrDefault();
            if (req == null)
                return NotFound();

            int month = DateTime.Now.Month;

            var model = new FollowLessonsViewModel { Request = req, Month = month };

            return View(model);
        }

        public IActionResult EndOfStudy(int id)
        {
            var req = _context.Requests.Where(r => r.Id == id).Include(r => r.User).FirstOrDefault();
            if (req == null)
                return NotFound();

            return View(req);
        }
    }
}
