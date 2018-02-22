using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CVSWebApp2.Data;
using CVSWebApp2.Models;
using System.Data;

namespace CVSWebApp2.Controllers
{
    public class GeneralSurveysController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GeneralSurveysController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: GeneralSurveys
        public async Task<IActionResult> Index()
        {
            return View(null);
        }

  
    }
}
