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
    public class MemberRolesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MemberRolesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: MemberRoles
        public async Task<IActionResult> Index()
        {
            return View(await _context.MemberRoles.ToListAsync());
        }

        // GET: MemberRoles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var memberRole = await _context.MemberRoles
                .SingleOrDefaultAsync(m => m.MemberRoleId == id);
            if (memberRole == null)
            {
                return NotFound();
            }

            return View(memberRole);
        }

        // GET: MemberRoles/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MemberRoles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MemberRoleId,MemberRoleName")] MemberRole memberRole)
        {
            if (ModelState.IsValid)
            {
                _context.Add(memberRole);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(memberRole);
        }

        // GET: MemberRoles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var memberRole = await _context.MemberRoles.SingleOrDefaultAsync(m => m.MemberRoleId == id);
            if (memberRole == null)
            {
                return NotFound();
            }
            return View(memberRole);
        }

        // POST: MemberRoles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MemberRoleId,MemberRoleName")] MemberRole memberRole)
        {
            if (id != memberRole.MemberRoleId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(memberRole);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MemberRoleExists(memberRole.MemberRoleId))
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
            return View(memberRole);
        }

        // GET: MemberRoles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var memberRole = await _context.MemberRoles
                .SingleOrDefaultAsync(m => m.MemberRoleId == id);
            if (memberRole == null)
            {
                return NotFound();
            }

            return View(memberRole);
        }

        // POST: MemberRoles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var memberRole = await _context.MemberRoles.SingleOrDefaultAsync(m => m.MemberRoleId == id);
            _context.MemberRoles.Remove(memberRole);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MemberRoleExists(int id)
        {
            return _context.MemberRoles.Any(e => e.MemberRoleId == id);
        }
    }
}
