using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace FamilyGo.Controllers
{
    public class EventController : Controller
    {
        // GET: Event
        public ActionResult Index()
        {
            var username = "familygo";
            var password = "4d5xkkb92bcr";
            /* Uri uri = new Uri("http://api.eventfinda.co.nz/v2/events.json");
             HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(uri);
             request.Headers.Add("Authorization", "Basic" + Convert.ToBase64String(new System.Text.ASCIIEncoding().GetBytes(username + ":" + password)));
             HttpWebResponse response = (HttpWebResponse)request.GetResponse();
             StreamReader reader = new StreamReader(response.GetResponseStream());
             string tmp = reader.ReadToEnd();
             response.Close();
             Response.Write(tmp);*/
            HttpWebRequest request = WebRequest.Create("http://api.eventfinda.co.nz/v2/events.json?location=1&category=52") as HttpWebRequest;

            request.Method = "GET";
            // Add authentication to request  
            request.Credentials = new NetworkCredential("familygo", "4d5xkkb92bcr");
            string tmp = "";
            // Get response  
            using (WebResponse response = request.GetResponse())
            {
                // Get the response stream  
                StreamReader reader = new StreamReader(response.GetResponseStream());
                tmp = reader.ReadToEnd();
                // Console application output  
                Console.WriteLine(reader.ReadToEnd());
            }
            ViewBag.a = tmp;
            return View();
        }
    }
}