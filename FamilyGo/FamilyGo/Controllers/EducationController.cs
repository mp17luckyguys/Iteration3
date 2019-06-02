using FamilyGo.Models;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace FamilyGo.Controllers
{
    public class EducationController : Controller
    {
        private Melkidsthrive123_DBEntities db = new Melkidsthrive123_DBEntities();
        // GET: the view of education 
        public ActionResult Index()
        {
            return View();
        }
        // get the jason from file path
        public ActionResult GetJsonFromFile(string path)
        {
            try
            {
                string filepath = Server.MapPath(path);
                string json = GetFileJson(filepath);
                return Content(json);
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }
        //read the data of jason file
        public string GetFileJson(string filepath)
        {
            string json = string.Empty;
            using (FileStream fs = new FileStream(filepath, FileMode.Open, System.IO.FileAccess.Read, FileShare.ReadWrite))
            {
                using (StreamReader sr = new StreamReader(fs, Encoding.GetEncoding("gb2312")))
                {
                    json = sr.ReadToEnd().ToString();
                }
            }
            return json;
        }

        // get the latitude and longitude of each suburb
        public ActionResult GetJsonSuburb(string suburbName)
        {
            List<Suburb> suburbs = db.Suburbs.ToList();
            Suburb theSuburb = new Suburb();
            
            foreach (Suburb suburb in suburbs)
            {
                if (String.Equals(suburbName, suburb.SuburbName))
                {
                    theSuburb = suburb;
                }
            }
            string json = JsonConvert.SerializeObject(theSuburb);
            return Content(json);
        }

        //add the schools in each suburb
        public ActionResult GetJsonSchools(string suburbName)
        {
            List<School> schools = db.Schools.ToList();
            List<School> schoolsInSuburb = new List<School>();
            foreach (School school in schools)
            {
                if (String.Equals(suburbName, school.Suburb.ToUpper()))
                {
                    schoolsInSuburb.Add(school);
                }
            }
            string json = JsonConvert.SerializeObject(schoolsInSuburb);
            return Content(json);
        }
    }
}