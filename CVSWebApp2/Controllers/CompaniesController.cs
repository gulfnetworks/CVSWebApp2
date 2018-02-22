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
    public class CompaniesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CompaniesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Companies
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Companies.Include(c => c.Country).Include(c => c.LocationTimeZone);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Companies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var company = await _context.Companies
                .Include(c => c.Country)
                .Include(c => c.LocationTimeZone)
                .SingleOrDefaultAsync(m => m.CompanyId == id);
            if (company == null)
            {
                return NotFound();
            }

            return View(company);
        }

        // GET: Companies/Create
        public IActionResult Create()
        {
            ViewData["CountryId"] = new SelectList(_context.Countries, "CountryId", "Description");
            ViewData["LocationTimeZoneId"] = new SelectList(_context.LocationTimeZones, "LocationTimeZoneId", "Name");
            return View();
        }

        // POST: Companies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CompanyId,CompanyName,CompanyAddress,CompanyContact,CompanyLogoUrl,CompanyCode,CreatedDate,ExpiryDate,LocationTimeZoneId,CountryId")] Company company)
        {
            if (ModelState.IsValid)
            {
                _context.Add(company);
                await _context.SaveChangesAsync();


                // INSERT SOCIAL MEDIA FOR NEWLY ADDED COMPANY
                var lastInsertedId = _context.Companies.Max(c => c.CompanyId);
                var lastLInsertedCompany = _context.Companies.Find(lastInsertedId);


                _context.Add(new SocialMedia() { Company = lastLInsertedCompany, CompanyId = lastInsertedId, Active = true, JSONSettings = "", Name = "twitter", Url = "https://twitter.com/?lang=en" });
                _context.Add(new SocialMedia() { Company = lastLInsertedCompany, CompanyId = lastInsertedId, Active = true, JSONSettings = "", Name = "facebook", Url = "https://www.facebook.com/" });
                _context.Add(new SocialMedia() { Company = lastLInsertedCompany, CompanyId = lastInsertedId, Active = true, JSONSettings = "", Name = "googleplus", Url = "https://plus.google.com/discover" });
                _context.Add(new SocialMedia() { Company = lastLInsertedCompany, CompanyId = lastInsertedId, Active = true, JSONSettings = "", Name = "pinterest", Url = "https://www.pinterest.com/" });
                _context.Add(new SocialMedia() { Company = lastLInsertedCompany, CompanyId = lastInsertedId, Active = true, JSONSettings = "", Name = "whatsapp", Url = "https://www.whatsapp.com/" });

                await _context.SaveChangesAsync();

                // INSERT END HERE

                return RedirectToAction(nameof(Index));
            }
            ViewData["CountryId"] = new SelectList(_context.Countries, "CountryId", "Description", company.CountryId);
            ViewData["LocationTimeZoneId"] = new SelectList(_context.LocationTimeZones, "LocationTimeZoneId", "Name", company.LocationTimeZoneId);
            return View(company);
        }

        // GET: Companies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var company = await _context.Companies.SingleOrDefaultAsync(m => m.CompanyId == id);
            if (company == null)
            {
                return NotFound();
            }
            ViewData["CountryId"] = new SelectList(_context.Countries, "CountryId", "Description", company.CountryId);
            ViewData["LocationTimeZoneId"] = new SelectList(_context.LocationTimeZones, "LocationTimeZoneId", "Name", company.LocationTimeZoneId);
            return View(company);
        }

        // POST: Companies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CompanyId,CompanyName,CompanyAddress,CompanyContact,CompanyLogoUrl,CompanyCode,CreatedDate,ExpiryDate,LocationTimeZoneId,CountryId")] Company company)
        {
            if (id != company.CompanyId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(company);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CompanyExists(company.CompanyId))
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
            ViewData["CountryId"] = new SelectList(_context.Countries, "CountryId", "Description", company.CountryId);
            ViewData["LocationTimeZoneId"] = new SelectList(_context.LocationTimeZones, "LocationTimeZoneId", "Name", company.LocationTimeZoneId);
            return View(company);
        }

        // GET: Companies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var company = await _context.Companies
                .Include(c => c.Country)
                .Include(c => c.LocationTimeZone)
                .SingleOrDefaultAsync(m => m.CompanyId == id);
            if (company == null)
            {
                return NotFound();
            }

            return View(company);
        }

        // POST: Companies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var company = await _context.Companies.SingleOrDefaultAsync(m => m.CompanyId == id);
            _context.Companies.Remove(company);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CompanyExists(int id)
        {
            return _context.Companies.Any(e => e.CompanyId == id);
        }
    }
}
