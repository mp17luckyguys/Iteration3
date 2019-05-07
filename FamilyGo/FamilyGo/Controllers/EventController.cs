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
             * <id>305</id><name>Creative</name>
             * <id>275</id><name>Dance Classes</name>
             * <id>163</id><name>Education</name>
             * <id>140</id><name><![CDATA[Family & Lifestyle]]></name>
             * <id>205</id><name>Games, Carnivals</name>
             * <id>126</id><name>Hiking, Camping</name>
             * <id>60</id><name>Ballet</name>
             * <id>253</id><name>Cabaret, Burlesque</name>
             * <id>251</id><name>Choir, Vocal Music</name>
             * <id>320</id><name>Circus</name>
             * <id>48</id><name>Comedy</name>
             * <id>22</id><name>Dance</name>
             * <id>25</id><name>Film</name>
             * <id>304</id><name>Magic, Variety</name>
             * <id>23</id><name>Musicals</name>
                  
    */
            var username = "familygo2";
            var password = "gr6rmmwzjtwd";
            HttpWebRequest request = WebRequest.Create("http://api.eventfinda.com.au/v2/events.json?rows=20&order=date&radius=100&point=-37.813611,144.963056&category=23,304,25,22,48,320,251,253,60,11,73,5,52,30,321,49,192,305,275,163,140,205,216") as HttpWebRequest;
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
                return Content(jo.ToString());
        }


    }
}