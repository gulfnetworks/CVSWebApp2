using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CVSWebApp2.Data;
using CVSWebApp2.Models;
using Microsoft.AspNetCore.Identity;
using CVSWebApp2.Services;
using Microsoft.Extensions.Logging;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Authorization;

namespace CVSWebApp2.Controllers
{
    [AllowAnonymous]
    //[Authorize(Roles = "Admin")]
    public class ApplicationUsersController : Controller
    {
        private readonly ApplicationDbContext _context;



        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly ILogger _logger;

        public ApplicationUsersController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IEmailSender emailSender,
            ILogger<AccountController> logger,
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _logger = logger;
            _context = context;
        }



        // GET: ApplicationUsers
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ApplicationUsers.Include(a => a.Company).Include(a => a.MemberRole);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ApplicationUsers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicationUser = await _context.ApplicationUsers
                .Include(a => a.Company)
                .Include(a => a.MemberRole)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (applicationUser == null)
            {
                return NotFound();
            }

            return View(applicationUser);
        }

        // GET: ApplicationUsers/Create
        public IActionResult Create()
        {
            ViewData["CompanyId"] = new SelectList(_context.Companies, "CompanyId", "CompanyName");
            ViewData["MemberRoleId"] = new SelectList(_context.MemberRoles, "MemberRoleId", "MemberRoleName");
            return View();
        }

        // POST: ApplicationUsers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserName,FirstName,LastName,MobileNo,Active,RememberMe,Password,ConfirmPassword,MemberRoleId,CompanyId,Id,NormalizedUserName,Email,NormalizedEmail,EmailConfirmed,PasswordHash,SecurityStamp,ConcurrencyStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEnd,LockoutEnabled,AccessFailedCount")] ApplicationUser applicationUser)
        {
            if (ModelState.IsValid)
            {
                applicationUser.AccessFailedCount = 0;
                applicationUser.UserName = applicationUser.Email;

                var user = new ApplicationUser { UserName = applicationUser.Email, Email = applicationUser.Email };
                var result = await _userManager.CreateAsync(applicationUser, applicationUser.Password);
                if (result.Succeeded)
                {
                    var roleName =  _context.MemberRoles.Find(applicationUser.MemberRoleId).MemberRoleName;
                    await _userManager.AddToRoleAsync(applicationUser, roleName);
                    _logger.LogInformation("User created a new account with password.");
                    return RedirectToAction(nameof(Index));
                }

                ModelState.AddModelError(string.Empty, result.Errors.ToString());

                //_context.Add(applicationUser);
                //await _context.SaveChangesAsync();
                //return RedirectToAction(nameof(Index));
            }
            ViewData["CompanyId"] = new SelectList(_context.Companies, "CompanyId", "CompanyName", applicationUser.CompanyId);
            ViewData["MemberRoleId"] = new SelectList(_context.MemberRoles, "MemberRoleId", "MemberRoleName", applicationUser.MemberRoleId);
            return View(applicationUser);
        }

        // GET: ApplicationUsers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicationUser = await _context.ApplicationUsers.SingleOrDefaultAsync(m => m.Id == id);
            if (applicationUser == null)
            {
                return NotFound();
            }
            ViewData["CompanyId"] = new SelectList(_context.Companies, "CompanyId", "CompanyName", applicationUser.CompanyId);
            ViewData["MemberRoleId"] = new SelectList(_context.MemberRoles, "MemberRoleId", "MemberRoleName", applicationUser.MemberRoleId);
            return View(applicationUser);
        }

        // POST: ApplicationUsers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserName,FirstName,LastName,MobileNo,Active,RememberMe,Password,ConfirmPassword,MemberRoleId,CompanyId,Id,NormalizedUserName,Email,NormalizedEmail,EmailConfirmed,PasswordHash,SecurityStamp,ConcurrencyStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEnd,LockoutEnabled,AccessFailedCount")] ApplicationUser applicationUser)
        {
            if (id != applicationUser.Id)
            {
                return NotFound();
            }


            if (ModelState.IsValid)
            {
                try
                {
                    applicationUser.SecurityStamp = "";
                    applicationUser.AccessFailedCount = 0;
                    applicationUser.UserName = applicationUser.Email;

                    //_context.Entry(applicationUser).State = EntityState.Modified;

                    //_context.ChangeTracker.AutoDetectChangesEnabled = false;


                    int roleId = _context.ApplicationUsers.Find(applicationUser.Id).MemberRoleId;

                    var _role = _context.MemberRoles.Find(roleId);

                    string oldRoleName = _role.MemberRoleName;
                    string newoldRoleName = _context.MemberRoles.Find(applicationUser.MemberRoleId).MemberRoleName;


                    await _userManager.RemoveFromRoleAsync(applicationUser, oldRoleName);

                    var code = await _userManager.GeneratePasswordResetTokenAsync(applicationUser);

                    var result = await _userManager.ResetPasswordAsync(applicationUser, code, applicationUser.Password);

                    var res = _context.Database.ExecuteSqlCommand("UPDATE AspNetUsers SET FirstName = @FirstName , LastName = @LastName, CompanyId = @CompanyId, Email = @Email, MobileNo = @MobileNo, MemberRoleId = @MemberRoleId, UserName = @UserName WHERE Id = @Id;",
                            new SqlParameter("FirstName", applicationUser.FirstName),
                            new SqlParameter("LastName", applicationUser.LastName),
                            new SqlParameter("CompanyId", applicationUser.CompanyId),
                            new SqlParameter("Email", applicationUser.Email),
                            new SqlParameter("MobileNo", applicationUser.MobileNo),
                            new SqlParameter("MemberRoleId", applicationUser.MemberRoleId),
                            new SqlParameter("UserName", applicationUser.UserName),
                            new SqlParameter("Id", applicationUser.Id));
                  

                    //_context.ApplicationUsers.Update(applicationUser);

                    await _userManager.AddToRoleAsync(applicationUser, newoldRoleName);

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApplicationUserExists(applicationUser.Id))
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
            ViewData["CompanyId"] = new SelectList(_context.Companies, "CompanyId", "CompanyId", applicationUser.CompanyId);
            ViewData["MemberRoleId"] = new SelectList(_context.MemberRoles, "MemberRoleId", "MemberRoleId", applicationUser.MemberRoleId);
            return View(applicationUser);
        }

        // GET: ApplicationUsers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicationUser = await _context.ApplicationUsers
                .Include(a => a.Company)
                .Include(a => a.MemberRole)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (applicationUser == null)
            {
                return NotFound();
            }

            return View(applicationUser);
        }

        // POST: ApplicationUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var applicationUser = await _context.ApplicationUsers.SingleOrDefaultAsync(m => m.Id == id);
            _context.ApplicationUsers.Remove(applicationUser);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ApplicationUserExists(int id)
        {
            return _context.ApplicationUsers.Any(e => e.Id == id);
        }
    }
}
