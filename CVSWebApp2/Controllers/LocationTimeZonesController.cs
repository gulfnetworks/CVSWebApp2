using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CVSWebApp2.Data;
using CVSWebApp2.Models;
using Microsoft.AspNetCore.Authorization;

namespace CVSWebApp2.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class LocationTimeZonesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LocationTimeZonesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: LocationTimeZones
        public async Task<IActionResult> Index()
        {
            return View(await _context.LocationTimeZones.ToListAsync());
        }

        // GET: LocationTimeZones/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var locationTimeZone = await _context.LocationTimeZones
                .SingleOrDefaultAsync(m => m.LocationTimeZoneId == id);
            if (locationTimeZone == null)
            {
                return NotFound();
            }

            return View(locationTimeZone);
        }

        // GET: LocationTimeZones/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: LocationTimeZones/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LocationTimeZoneId,Name,TimeZoneValue")] LocationTimeZone locationTimeZone)
        {
            if (ModelState.IsValid)
            {
                _context.Add(locationTimeZone);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(locationTimeZone);
        }

        // GET: LocationTimeZones/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var locationTimeZone = await _context.LocationTimeZones.SingleOrDefaultAsync(m => m.LocationTimeZoneId == id);
            if (locationTimeZone == null)
            {
                return NotFound();
            }
            return View(locationTimeZone);
        }

        // POST: LocationTimeZones/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LocationTimeZoneId,Name,TimeZoneValue")] LocationTimeZone locationTimeZone)
        {
            if (id != locationTimeZone.LocationTimeZoneId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(locationTimeZone);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LocationTimeZoneExists(locationTimeZone.LocationTimeZoneId))
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
            return View(locationTimeZone);
        }

        // GET: LocationTimeZones/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var locationTimeZone = await _context.LocationTimeZones
                .SingleOrDefaultAsync(m => m.LocationTimeZoneId == id);
            if (locationTimeZone == null)
            {
                return NotFound();
            }

            return View(locationTimeZone);
        }

        // POST: LocationTimeZones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var locationTimeZone = await _context.LocationTimeZones.SingleOrDefaultAsync(m => m.LocationTimeZoneId == id);
            _context.LocationTimeZones.Remove(locationTimeZone);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LocationTimeZoneExists(int id)
        {
            return _context.LocationTimeZones.Any(e => e.LocationTimeZoneId == id);
        }
    }
}
