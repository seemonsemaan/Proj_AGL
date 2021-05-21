using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Proj_AGL.Data;
using Proj_AGL.Models;

namespace Proj_AGL.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminRequestsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminRequestsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: AdminRequests
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Requests.Include(r => r.Location).Include(r => r.RequestType).Include(r => r.Status).Include(r => r.User);
            return View(await applicationDbContext.OrderByDescending(r => r.RequestedDate).ToListAsync());
        }

        // GET: AdminRequests/Details/5
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

        // GET: AdminRequests/Create
        public IActionResult Create()
        {
            ViewData["LocationId"] = new SelectList(_context.Locations, "Id", "Name");
            ViewData["RequestTypeId"] = new SelectList(_context.RequestTypes, "Id", "Name");
            ViewData["StatusId"] = new SelectList(_context.StatusTypes, "Id", "Name");
            return View();
        }

        // POST: AdminRequests/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,RequestTypeId,Count,UserId,StatusId,LocationId,RequestedDate")] Request request)
        {
            if (ModelState.IsValid)
            {
                _context.Add(request);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LocationId"] = new SelectList(_context.Locations, "Id", "Name", request.LocationId);
            ViewData["RequestTypeId"] = new SelectList(_context.RequestTypes, "Id", "Name", request.RequestTypeId);
            ViewData["StatusId"] = new SelectList(_context.StatusTypes, "Id", "Name", request.StatusId);
            return View(request);
        }

        // GET: AdminRequests/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var request = await _context.Requests.Include(r => r.RequestType).FirstOrDefaultAsync(q => q.Id == id);
            if (request == null)
            {
                return NotFound();
            }
            ViewData["LocationId"] = new SelectList(_context.Locations, "Id", "Name", request.LocationId);
            ViewData["RequestTypeId"] = new SelectList(_context.RequestTypes, "Id", "Name", request.RequestTypeId);
            ViewData["StatusId"] = new SelectList(_context.StatusTypes, "Id", "Name", request.StatusId);
            return View(request);
        }

        // POST: AdminRequests/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,RequestTypeId,Count,UserId,StatusId,LocationId,RequestedDate")] Request request)
        {
            if (id != request.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var dbRequest = _context.Requests.Where(r => r.Id == request.Id).FirstOrDefault();
                    dbRequest.StatusId = request.StatusId;
                    dbRequest.LocationId = request.LocationId;
                    _context.Update(dbRequest);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RequestExists(request.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["LocationId"] = new SelectList(_context.Locations, "Id", "Name", request.LocationId);
            ViewData["RequestTypeId"] = new SelectList(_context.RequestTypes, "Id", "Name", request.RequestTypeId);
            ViewData["StatusId"] = new SelectList(_context.StatusTypes, "Id", "Name", request.StatusId);
            return View(request);
        }

        // GET: AdminRequests/Delete/5
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

        // POST: AdminRequests/Delete/5
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
