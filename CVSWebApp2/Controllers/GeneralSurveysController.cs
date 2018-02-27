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
using Microsoft.AspNetCore.Hosting;

namespace CVSWebApp2.Controllers
{
    public class GeneralSurveysController : Controller
    {
        private readonly ApplicationDbContext _context;



        private readonly IHostingEnvironment _hostingEnvironment;



        public GeneralSurveysController(ApplicationDbContext context, IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
            _context = context;
        }


        // GET: GeneralSurveys
        [Route("/GeneralSurveys/TestData")]
        public async Task<IActionResult> TestData()
        {

            string contentRootPath = _hostingEnvironment.ContentRootPath;
            var JSON = System.IO.File.ReadAllText(contentRootPath + "/json.json");
            return Json(JSON);
        }



        // GET: GeneralSurveys

        public async Task<IActionResult> Index()
        {


            var gsList = new List<GeneralSurvey>();


            var resultTable = new RawSQLDataProvider().Execute("SELECT * FROM [dbo].[GeneralSurveyReport]");


            foreach (DataRow row in resultTable.Rows)
            {
                GeneralSurvey gs = new GeneralSurvey();


                gs.Action = row["Action"].ToString();
                gs.AmbienceComment = row["AmbienceComment"].ToString();
                gs.AmbienceRate = int.Parse(row["AmbienceRate"].ToString());


                gs.CheckNo = row["CheckNo"].ToString();
                gs.CompanyId = int.Parse(row["CompanyId"].ToString());
                gs.CountryId = int.Parse(row["CountryId"].ToString());
                gs.Customer = row["Customer"].ToString();
                gs.DateTime = DateTime.Parse(row["DateTime"].ToString());
                gs.Email = row["Email"].ToString();


                gs.MemberEmail = row["MemberEmail"].ToString();
                gs.MemberFirstName = row["MemberFirstName"].ToString();
                gs.MemberId = int.Parse(row["MemberId"].ToString());
                gs.MemberLastName = row["MemberLastName"].ToString();


                gs.ManagerEmail = row["ManagerEmail"].ToString();
                gs.ManagerFirstName = row["ManagerFirstName"].ToString();
                gs.ManagerId = int.Parse(row["ManagerId"].ToString());
                gs.ManagerLastName = row["ManagerLastName"].ToString();


                gs.LastVisit = row["LastVisit"].ToString();
                gs.LastVisitComment = row["LastVisitComment"].ToString();
                gs.ManagerId = int.Parse(row["ManagerId"].ToString());
                gs.MobileNo = row["MobileNo"].ToString();
                gs.OutletAddress = row["OutletAddress"].ToString();
                gs.OutletId = int.Parse(row["OutletId"].ToString());
                gs.OutletName = row["OutletName"].ToString();
                gs.QualityComment = row["QualityComment"].ToString();
                gs.QualityRate = int.Parse(row["QualityRate"].ToString());
                gs.RecommendImprovements = row["RecommendImprovements"].ToString();
                gs.RecommendPoorArea = row["RecommendPoorArea"].ToString();
                gs.RecommendRate = int.Parse(row["RecommendRate"].ToString());
                gs.RecommendSuggestions = row["RecommendSuggestions"].ToString();


                int rsolutionLogId = 0;
                int.TryParse(row["ResolutionLogId"].ToString(), out rsolutionLogId);
                gs.ResolutionLogId = rsolutionLogId;


                gs.ServiceComment = row["ServiceComment"].ToString();
                gs.ServiceRate = int.Parse(row["ServiceRate"].ToString());
                gs.StaffId = row["StaffId"].ToString();
                gs.Status = row["Status"].ToString();

                string status2 = "";
                if (row["Status2"] != null) { status2 = row["Status2"].ToString(); }
                gs.Status2 = status2;


                gs.SurveyId = int.Parse(row["SurveyId"].ToString());
                gs.TableNo = row["TableNo"].ToString();
                gs.ValueComment = row["ValueComment"].ToString();
                gs.ValueRate = int.Parse(row["ValueRate"].ToString());



                float tmpAverage = 0;
                float.TryParse(row["Average"].ToString(), out tmpAverage);
                gs.Average = tmpAverage;


                gs.Complaint = int.Parse(row["Complaint"].ToString());
                gs.Feedback = int.Parse(row["Feedback"].ToString());
                gs.Compliment = int.Parse(row["Compliment"].ToString());

                gs.Country = row["Country"].ToString();

                gsList.Add(gs);
            }

            return View(gsList);
        }


    }
}
