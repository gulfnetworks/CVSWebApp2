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
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

namespace CVSWebApp2.Controllers
{
    [AllowAnonymous]
    public class SurveysController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly ILogger _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly IServiceProvider _serviceProvider;

        public SurveysController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IEmailSender emailSender,
            ILogger<AccountController> logger,
            ApplicationDbContext context,
            IHttpContextAccessor httpContextAccessor,
            IServiceProvider serviceProvider)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _logger = logger;
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _serviceProvider = serviceProvider;
        }

        // GET: Surveys
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Surveys.Include(s => s.Outlet).Include(s => s.UserManager).Include(s => s.UserStaff);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Surveys/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var survey = await _context.Surveys
                .Include(s => s.Outlet)
                .Include(s => s.UserManager)
                .Include(s => s.UserStaff)
                .SingleOrDefaultAsync(m => m.SurveyId == id);
            if (survey == null)
            {
                return NotFound();
            }

            return View(survey);
        }


        // GET: Surveys/Complete
        public IActionResult Complete()
        {
            return View();
        }


        // GET: Surveys/Create
        public IActionResult Create(string Id)
        {
            if (Id == null) return View("NotFound");

            var curCompany = CurrentSession.GetCurrentCompanyByCode(_context, Id);
            if (curCompany == null) return View("NotFound");

            ViewData["CountryId"] = new SelectList(_context.Countries, "CountryId", "Description");


            //_httpContextAccessor.HttpContext.Session.SetString("CompanyName", tempCompany.CompanyName);
            //HttpContext.Session.SetString("CompanyName", "");
            ViewData["Company"] = curCompany;
            return View();
        }

        // POST: Surveys/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("SurveyId,QualityRate,QualityComment,ValueRate,ValueComment,ServiceRate,ServiceComment,AmbienceRate,AmbienceComment,RecommendRate,RecommendPoorArea,RecommendImprovements,RecommendSuggestions,LastVisit,LastVisitComment,Action,Status,DateTime,Customer,MobileNo,Email,CheckNo,TableNo,OutletId,ManagerId,StaffId")] Survey survey)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(survey);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["OutletId"] = new SelectList(_context.Outlets, "OutletId", "OutletId", survey.OutletId);
        //    ViewData["ManagerId"] = new SelectList(_context.ApplicationUsers, "Id", "Password", survey.ManagerId);
        //    ViewData["StaffId"] = new SelectList(_context.ApplicationUsers, "Id", "Password", survey.StaffId);
       
        //    return View(survey);
        //}

        [HttpPost]
        [Route("/Surveys/Create")]
        public async Task<IActionResult> Create([FromBody] Survey survey)
        {
            if (ModelState.IsValid)
            {
                survey.DateTime = DateTime.UtcNow;
                _context.Add(survey);
                await _context.SaveChangesAsync();
                return Json("success");
            }
            ViewData["OutletId"] = new SelectList(_context.Outlets, "OutletId", "OutletId", survey.OutletId);
            ViewData["ManagerId"] = new SelectList(_context.ApplicationUsers, "Id", "Password", survey.ManagerId);
            ViewData["StaffId"] = new SelectList(_context.ApplicationUsers, "Id", "Password", survey.StaffId);

            return View(survey);
        }


        // GET: Surveys/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var survey = await _context.Surveys.SingleOrDefaultAsync(m => m.SurveyId == id);
            if (survey == null)
            {
                return NotFound();
            }
            ViewData["OutletId"] = new SelectList(_context.Outlets, "OutletId", "OutletId", survey.OutletId);
            ViewData["ManagerId"] = new SelectList(_context.ApplicationUsers, "Id", "Password", survey.ManagerId);
            ViewData["StaffId"] = new SelectList(_context.ApplicationUsers, "Id", "Password", survey.StaffId);
            return View(survey);
        }

        // POST: Surveys/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SurveyId,QualityRate,QualityComment,ValueRate,ValueComment,ServiceRate,ServiceComment,AmbienceRate,AmbienceComment,RecommendRate,RecommendPoorArea,RecommendImprovements,RecommendSuggestions,LastVisit,LastVisitComment,Action,Status,DateTime,Customer,MobileNo,Email,CheckNo,TableNo,OutletId,ManagerId,StaffId")] Survey survey)
        {
            if (id != survey.SurveyId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(survey);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SurveyExists(survey.SurveyId))
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
            ViewData["OutletId"] = new SelectList(_context.Outlets, "OutletId", "OutletId", survey.OutletId);
            ViewData["ManagerId"] = new SelectList(_context.ApplicationUsers, "Id", "Password", survey.ManagerId);
            ViewData["StaffId"] = new SelectList(_context.ApplicationUsers, "Id", "Password", survey.StaffId);
            return View(survey);
        }

        // GET: Surveys/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var survey = await _context.Surveys
                .Include(s => s.Outlet)
                .Include(s => s.UserManager)
                .Include(s => s.UserStaff)
                .SingleOrDefaultAsync(m => m.SurveyId == id);
            if (survey == null)
            {
                return NotFound();
            }

            return View(survey);
        }

        // POST: Surveys/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var survey = await _context.Surveys.SingleOrDefaultAsync(m => m.SurveyId == id);
            _context.Surveys.Remove(survey);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SurveyExists(int id)
        {
            return _context.Surveys.Any(e => e.SurveyId == id);
        }


        public async Task<IActionResult> StaffsByOutlet(int? id)
        {

            var staffs = await _userManager.GetUsersInRoleAsync("Member");
            //var staff = await _userManager.GetUsersInRoleAsync("Member");
            //var countries = await _context.Countries.ToListAsync();
            //var outlets = await _context.Outlets.ToListAsync();

            var _staffs = (from a in staffs
                             join b in _context.UserOutlets on a.Id equals b.Id
                             join c in _context.Outlets on b.OutletId equals c.OutletId
                             where b.OutletId == id
                             select new Select2 { disabled = false, id = a.Id, selected = false, text = a.FullName.ToProperCase() }).Distinct().ToList();

            return Json(_staffs);
        }



        public async Task<IActionResult> ManagersByOutlet(int? id)
        {

            var managers = await _userManager.GetUsersInRoleAsync("Manager");
            //var staff = await _userManager.GetUsersInRoleAsync("Member");
            //var countries = await _context.Countries.ToListAsync();
            //var outlets = await _context.Outlets.ToListAsync();

            var _managers = (from a in managers
                       join b in _context.UserOutlets on a.Id equals b.Id
                       join c in _context.Outlets on b.OutletId equals c.OutletId
                       where b.OutletId == id
                       select new Select2 { disabled = false, id = a.Id, selected = false, text = a.FullName.ToProperCase() }).Distinct().ToList();



            return Json(_managers);
        }


        public async Task<IActionResult> OutletByCountry(int? id, int? companyId)
        {
            var outlets = await (from a in _context.Outlets
                                 join b in _context.Countries on a.CountryId equals b.CountryId
                                 where a.CountryId == id && a.CompanyId == companyId
                                 select new Select2 { disabled =false, id = a.OutletId , selected = false , text = a.OutletName.ToProperCase() }).Distinct().ToListAsync();


            //ViewData["OutletId"] = new SelectList(outlets, "OutletId", "OutletName");

            return Json(outlets);
        }

    }
}
