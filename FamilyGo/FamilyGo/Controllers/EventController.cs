using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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

            return View();
        }







        public ActionResult GetEvents()
        {
            /*
             * category: <id>11</id>< name > Exhibitions </ name >
             * <id>73</id>< name >< ![CDATA[A & P Shows, Field Days]] ></ name >
             * <id>5</id><name>Children, Kids, Holidays</name>
             * <id>52</id><name>Family Entertainment</name>
             * <id>30</id><name>Festivals</name>
             * <id>321</id><name>Film</name>
             * <id>49</id><name>Games, Hobbies</name>
             * <id>192</id><name>Lifestyle Shows, Expos</name>
             * <id>1</id><name>Performing Arts</name>
            
    */
            var username = "familygo2";
            var password = "gr6rmmwzjtwd";
            HttpWebRequest request = WebRequest.Create("http://api.eventfinda.com.au/v2/events.json?rows=20&q=concert&order=date&radius=20&point=-37.813611,144.963056&category=11,73,5,52,30,321,49,192,1") as HttpWebRequest;
            request.Method = "GET";
            // Add authentication to request  
            request.Credentials = new NetworkCredential(username, password);
            string tmp = "";
            JObject jo;
            // Get response  
            using (WebResponse response = request.GetResponse())
            {
                // Get the response stream  
                StreamReader reader = new StreamReader(response.GetResponseStream());
                tmp = reader.ReadToEnd();
                jo = (JObject)JsonConvert.DeserializeObject(tmp);
                // Console application output  
                Console.WriteLine(reader.ReadToEnd());
            }
            ViewBag.a = jo;


            return Content(jo.ToString());
        }
    }
}