using FamilyGo.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace FamilyGo.Controllers
{
    public class EventController : Controller
    {
        private Melkidsthrive123_DBEntities db = new Melkidsthrive123_DBEntities();
        // GET: Event and return teh view of event page
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetEvents(string isFree, string point, string start_time, string end_time, string category)
        {
            // get the data from event api
            string basicUrl = "http://api.eventfinda.com.au/v2/events.json?rows=20&order=date";
            // select the events family can come together based on category
            Dictionary<String, String> categoryDic = new Dictionary<string, string>();
            categoryDic.Add("1", "11");
            categoryDic.Add("2", "73,192,1,48,22,60,304,23,251,253,320");
            categoryDic.Add("3","5,52,49,305,140,205");
            categoryDic.Add("4", "30,205");
            categoryDic.Add("5", "275,163");
            categoryDic.Add("6", "321,25");
            categoryDic.Add("7", "126");
            // get the detail information of each event
            if (category != "0")
            { basicUrl = basicUrl + "&category=" + categoryDic[category]; }
            else { basicUrl = basicUrl + "&category=25,11,5,52,30,321,49,305,275,163,140,205,216"; }
            if (point != "0")
            { basicUrl = basicUrl + "&radius=10&point=" + point; }
            else { basicUrl = basicUrl + "&radius=100&point=-37.813611,144.963056"; }
            if (isFree != "0")
            { basicUrl = basicUrl + "&free=" + isFree; }
            if (start_time != "0")
            { basicUrl = basicUrl + "&start_date=" + start_time; }
            if (end_time != "0")
            { basicUrl = basicUrl + "&end_date=" + end_time; }
            /*
             * category: 
             * exhibitions 1
             * <id>11</id>< name > Exhibitions </ name >
             * shows 2
             * <id>73</id>< name >< ![CDATA[A & P Shows, Field Days]] ></ name >
             * <id>192</id><name>Lifestyle Shows, Expos</name>
             * <id>1</id><name>Performing Arts</name>
             * <id>48</id><name>Comedy</name>
             * <id>22</id><name>Dance</name>
             * <id>60</id><name>Ballet</name>
             * <id>304</id><name>Magic, Variety</name>
             * <id>23</id><name>Musicals</name>
             * <id>251</id><name>Choir, Vocal Music</name>
             * <id>253</id><name>Cabaret, Burlesque</name>
             * <id>320</id><name>Circus</name>
             * family 3
             * <id>5</id><name>Children, Kids, Holidays</name>
             * <id>52</id><name>Family Entertainment</name>
             * <id>49</id><name>Games, Hobbies</name>
             * <id>305</id><name>Creative</name>
             * <id>140</id><name><![CDATA[Family & Lifestyle]]></name>
             * <id>205</id><name>Games, Carnivals</name>
             * Festivals and Carnivals 4
             * <id>30</id><name>Festivals</name>
             * <id>205</id><name>Games, Carnivals</name>
             * Education 5
             * <id>275</id><name>Dance Classes</name>
             * <id>163</id><name>Education</name>
             * Film 6
             * <id>321</id><name>Film</name>
             * <id>25</id><name>Film</name>
             * Hiking and Camping 7
             * <id>126</id><name>Hiking, Camping</name>
             * http://api.eventfinda.com.au/v2/events.json?rows=20&order=date&radius=100&point=-37.813611,144.963056&category=23,304,25,22,48,320,251,253,60,11,73,5,52,30,321,49,192,305,275,163,140,205,216
    */
            var username = "familygo2";
            var password = "gr6rmmwzjtwd";
            HttpWebRequest request = WebRequest.Create(basicUrl) as HttpWebRequest;
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
        public ActionResult GetSuburbs()
        {
            // get the suburb information from database
            List<Suburb> suburbs = db.Suburbs.ToList();
            StringBuilder stringBuilder = new StringBuilder();
            JavaScriptSerializer json = new JavaScriptSerializer();
            json.Serialize(suburbs, stringBuilder);
            return Content(stringBuilder.ToString());
        }

    }
}