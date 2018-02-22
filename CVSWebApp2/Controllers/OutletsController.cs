using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CVSWebApp2.Data;
using CVSWebApp2.Models;

namespace CVSWebApp2.Controllers
{
    public class OutletsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OutletsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Outlets
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Outlets.Include(o => o.Company).Include(o => o.Country);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Outlets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var outlet = await _context.Outlets
                .Include(o => o.Company)
                .Include(o => o.Country)
                .SingleOrDefaultAsync(m => m.OutletId == id);
            if (outlet == null)
            {
                return NotFound();
            }

            return View(outlet);
        }

        // GET: Outlets/Create
        public IActionResult Create()
        {
            ViewData["CompanyId"] = new SelectList(_context.Companies, "CompanyId", "CompanyName");
            ViewData["CountryId"] = new SelectList(_context.Countries, "CountryId", "Description");
            return View();
        }

        // POST: Outlets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OutletId,OutletName,OutletAddress,CountryId,CompanyId")] Outlet outlet)
        {
            if (ModelState.IsValid)
            {
                _context.Add(outlet);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CompanyId"] = new SelectList(_context.Companies, "CompanyId", "CompanyName", outlet.CompanyId);
            ViewData["CountryId"] = new SelectList(_context.Countries, "CountryId", "Description", outlet.CountryId);
            return View(outlet);
        }

        // GET: Outlets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var outlet = await _context.Outlets.SingleOrDefaultAsync(m => m.OutletId == id);
            if (outlet == null)
            {
                return NotFound();
            }
            ViewData["CompanyId"] = new SelectList(_context.Companies, "CompanyId", "CompanyName", outlet.CompanyId);
            ViewData["CountryId"] = new SelectList(_context.Countries, "CountryId", "Description", outlet.CountryId);
            return View(outlet);
        }

        // POST: Outlets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OutletId,OutletName,OutletAddress,CountryId,CompanyId")] Outlet outlet)
        {
            if (id != outlet.OutletId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(outlet);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OutletExists(outlet.OutletId))
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
            ViewData["CompanyId"] = new SelectList(_context.Companies, "CompanyId", "CompanyName", outlet.CompanyId);
            ViewData["CountryId"] = new SelectList(_context.Countries, "CountryId", "Description", outlet.CountryId);
            return View(outlet);
        }

        // GET: Outlets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var outlet = await _context.Outlets
                .Include(o => o.Company)
                .Include(o => o.Country)
                .SingleOrDefaultAsync(m => m.OutletId == id);
            if (outlet == null)
            {
                return NotFound();
            }

            return View(outlet);
        }

        // POST: Outlets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var outlet = await _context.Outlets.SingleOrDefaultAsync(m => m.OutletId == id);
            _context.Outlets.Remove(outlet);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OutletExists(int id)
        {
            return _context.Outlets.Any(e => e.OutletId == id);
        }
    }
}
