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
    //[Authorize]
    public class UserOutletsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserOutletsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: UserOutlets
        public async Task<IActionResult> Index()
        {
            //var applicationDbContext = _context.UserOutlets.Include(u => u.Outlet).Include(u=>u.ApplicationUser);
            var ret = (from a in _context.UserOutlets
                       join b in _context.Outlets on a.OutletId equals b.OutletId
                       join c in _context.ApplicationUsers on a.Id equals c.Id
                       select new UserOutlet
                       {
                           ApplicationUser = c,
                           DefaultOutlet = a.DefaultOutlet,
                           Id = a.Id,
                           Outlet = b,
                           OutletId = b.OutletId,
                           UserOutletId = a.UserOutletId
                       }).OrderBy(a=>a.OutletId).ToListAsync();



            //return View(await applicationDbContext.ToListAsync());
            return View(await ret);
        }

        // GET: UserOutlets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userOutlet = await _context.UserOutlets
                .Include(u => u.Outlet)
                .SingleOrDefaultAsync(m => m.UserOutletId == id);
            if (userOutlet == null)
            {
                return NotFound();
            }

            return View(userOutlet);
        }

        // GET: UserOutlets/Create
        public IActionResult Create()
        {
            ViewData["OutletId"] = new SelectList(_context.Outlets.Distinct(), "OutletId", "OutletName");
            ViewData["Id"] = new SelectList(_context.ApplicationUsers.Distinct(), "Id", "FullName");
            return View();
        }

        // POST: UserOutlets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserOutletId,OutletId,Id,DefaultOutlet")] UserOutlet userOutlet)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userOutlet);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["OutletId"] = new SelectList(_context.Outlets.Distinct(), "OutletId", "OutletName", userOutlet.OutletId);
            return View(userOutlet);
        }

        // GET: UserOutlets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userOutlet = await _context.UserOutlets.SingleOrDefaultAsync(m => m.UserOutletId == id);
            if (userOutlet == null)
            {
                return NotFound();
            }
            ViewData["OutletId"] = new SelectList(_context.Outlets.Distinct(), "OutletId", "OutletName", userOutlet.OutletId);
            ViewData["Id"] = new SelectList(_context.ApplicationUsers.Distinct(), "Id", "FullName", userOutlet.Id);
            return View(userOutlet);
        }

        // POST: UserOutlets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int UserOutletId, [Bind("UserOutletId,OutletId,Id,DefaultOutlet")] UserOutlet userOutlet)
        {
            if (UserOutletId != userOutlet.UserOutletId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userOutlet);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserOutletExists(userOutlet.UserOutletId))
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
            ViewData["OutletId"] = new SelectList(_context.Outlets.Distinct(), "OutletId", "OutletName", userOutlet.OutletId);
            return View(userOutlet);
        }

        // GET: UserOutlets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userOutlet = await _context.UserOutlets
                .Include(u => u.Outlet)
                .SingleOrDefaultAsync(m => m.UserOutletId == id);
            if (userOutlet == null)
            {
                return NotFound();
            }

            return View(userOutlet);
        }

        // POST: UserOutlets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userOutlet = await _context.UserOutlets.SingleOrDefaultAsync(m => m.UserOutletId == id);
            _context.UserOutlets.Remove(userOutlet);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserOutletExists(int id)
        {
            return _context.UserOutlets.Any(e => e.UserOutletId == id);
        }
    }
}
