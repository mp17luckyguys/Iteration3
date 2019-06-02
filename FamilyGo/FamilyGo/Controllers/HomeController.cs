using FamilyGo.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//using static FamilyGo.Utils.ExcelToDBLS;
//using static FamilyGo.Utils.WriteSuburbToDB;
//using static FamilyGo.Utils.ExcelToDBActivity;
//using static FamilyGo.Utils.ExcelToDBOtherActivity;
//using static FamilyGo.Utils.ExcelToDBParks;
//using static FamilyGo.Utils.ExcelToDBSportPlace;

namespace FamilyGo.Controllers
{
    public class HomeController : Controller
    {
        // The password view for the project.
        public ActionResult Password()
        {
            return View();
        }
        // home page of project 
        public ActionResult Index()
        {
            //insert a shiny application to home page 
            ViewBag.IFrameSrc = "https://melkidsthrive.shinyapps.io/migrant/";
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            // import the data to local database
            //ExcelToDB ex = new ExcelToDB();
            //ex.Page_Load();
            //ExcelToDBPark ex1 = new ExcelToDBPark();
            //ex1.Page_Load();
            //ExcelToDB ex = new ExcelToDB();
            //ex.Page_Load();
            //ExcelToDBOtherActicityPlaces ex = new ExcelToDBOtherActicityPlaces();
            //ex.Page_Load();
            //ExcelToDBSportPlaces ex2 = new ExcelToDBSportPlaces();
            //ex2.Page_Load();
            //ExcelToDBPSchool ex = new ExcelToDBPSchool();
            //ex.Page_Load();
            //ExcelToDBLSS ex = new ExcelToDBLSS();
            //ex.Page_Load();
            
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}