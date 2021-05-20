using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Proj_AGL.Data;
using Proj_AGL.Models;

namespace Proj_AGL.Controllers
{
    [Authorize]
    public class RequestsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> userManager;

        public RequestsController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            this.userManager = userManager;
        }

        // GET: Requests
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var applicationDbContext = _context.Requests.Include(r => r.Location).Include(r => r.RequestType).Include(r => r.Status);
            return View(await applicationDbContext.Where(r => r.UserId == userId).OrderByDescending(r => r.RequestedDate).ToListAsync());
        }

        // GET: Requests/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var request = await _context.Requests
                .Include(r => r.Location)
                .Include(r => r.RequestType)
                .Include(r => r.Status)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (request == null)
            {
                return NotFound();
            }

            return View(request);
        }

        // GET: Requests/Create
        public IActionResult Create()
        {
            ViewData["LocationId"] = new SelectList(_context.Locations, "Id", "Name");
            ViewData["RequestTypeId"] = new SelectList(_context.RequestTypes, "Id", "Name");
            ViewData["StatusId"] = new SelectList(_context.StatusTypes, "Id", "Name");
            return View();
        }

        // POST: Requests/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,RequestTypeId,Count,UserId,StatusId,LocationId")] Request request)
        {
            if (ModelState.IsValid)
            {
                ProcessRequest(request);
                request.RequestedDate = DateTime.Now;
                request.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                request.StatusId = 1;
                request.LocationId = 1;
                _context.Add(request);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LocationId"] = new SelectList(_context.Locations, "Id", "Name", request.LocationId);
            ViewData["RequestTypeId"] = new SelectList(_context.RequestTypes, "Id", "Name", request.RequestTypeId);
            ViewData["StatusId"] = new SelectList(_context.StatusTypes, "Id", "Name", request.StatusId);
            return View(request);
        }

        private Request ProcessRequest(Request request)
        {
            int requestTypeId = request.RequestTypeId;
            int statusId = request.StatusId;
            switch (requestTypeId)
            {
                case 1:
                    request.StatusId = 3;
                    request.LocationId = 1;
                    break;

                case 2:
                    request.StatusId = 3;
                    request.LocationId = 1;
                    break;

                case 3:
                    request.StatusId = 3;
                    request.LocationId = 4;
                    break;
            }

            return request;
        }

        // GET: Requests/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var request = await _context.Requests
                .Include(r => r.Location)
                .Include(r => r.RequestType)
                .Include(r => r.Status)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (request == null)
            {
                return NotFound();
            }

            return View(request);
        }

        // POST: Requests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var request = await _context.Requests.FindAsync(id);
            _context.Requests.Remove(request);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RequestExists(int id)
        {
            return _context.Requests.Any(e => e.Id == id);
        }
    }
}
