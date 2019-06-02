using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FamilyGo.Models;
using FamilyGo.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FamilyGo.Controllers
{
    public class PlacesController : Controller
    {
        private Melkidsthrive123_DBEntities db = new Melkidsthrive123_DBEntities();

        // get the data from database and return the view of places page.
        public ActionResult Index(string i,string age)
        {
            // filter the places based on selected activities
            ViewBag.ageGroup = age;
            ViewBag.place = i;
            var places = db.Places.Include(p => p.Activity);
            List<Place> placesList = places.ToList();
            List<Place> newPlacesList = new List<Place>();
            foreach (Place place in placesList)
            {
                if (string.Equals(place.Activity.Name,i))
                    newPlacesList.Add(place);
            }
            //arrange the name of places in alphabetical order
            ViewBag.activityName = i;
            var newL = newPlacesList.OrderBy(x => x.Name).ToList();

            var a = ViewBag.ageGroup;
            return View(newL);
        }

        // GET: Places/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Place place = db.Places.Find(id);
            if (place == null)
            {
                return HttpNotFound();
            }
            return View(place);
        }


        public ActionResult GetDetail(int? id)
        {
            Dictionary<string, string> detail = new Dictionary<string, string>();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Place place = db.Places.Find(id);
            if (place == null)
            {
                return HttpNotFound();
            }
           // find the detail informaiton using google API by place name
            string googleTextSearchReasult = HttpUtils.Get("https://maps.googleapis.com/maps/api/place/textsearch/json?query=" + place.Name + "&key=AIzaSyDJeTABC7AwjSI-x7dm2cVlbHvA3yN65HA");
            detail.Add("name", place.Name);
            JObject jo = (JObject)JsonConvert.DeserializeObject(googleTextSearchReasult);
            if (jo["status"].ToString() != "ZERO_RESULTS")
            {
                string placeId = jo["results"][0]["place_id"].ToString();
                if (jo["results"][0]["photos"] != null)
                {
                    string joPhotoRef = jo["results"][0]["photos"][0]["photo_reference"].ToString();
                    // get the image from google API of different places
                    string imageUrl = "https://maps.googleapis.com/maps/api/place/photo?maxwidth=400&photoreference=" + joPhotoRef + "& sensor=false&key=AIzaSyDJeTABC7AwjSI-x7dm2cVlbHvA3yN65HA";
                    ViewBag.imUrl = imageUrl;
                    detail.Add("imUrl", imageUrl);
                }
                // get the detail information from google API
                string detailUrl = "https://maps.googleapis.com/maps/api/place/details/json?placeid=" + placeId + "& fields=name,rating,formatted_phone_number,reviews,website,opening_hours&key=AIzaSyDJeTABC7AwjSI-x7dm2cVlbHvA3yN65HA";
                string googleDetailReasult = HttpUtils.Get(detailUrl);
                JObject joDetail = (JObject)JsonConvert.DeserializeObject(googleDetailReasult);
                // get the rating, address, phone number etc information
                if (joDetail["result"]["rating"] != null)
                { ViewBag.rating = joDetail["result"]["rating"]; detail.Add("rating", joDetail["result"]["rating"].ToString()); }
                else { ViewBag.rating = "No rating avaliable."; detail.Add("rating", "No rating avaliable."); }
                if (joDetail["result"]["formatted_address"] != null) { ViewBag.address = joDetail["result"]["formatted_address"]; detail.Add("address", joDetail["result"]["formatted_address"].ToString()); } else { ViewBag.address = "No address avaliable."; detail.Add("address", "No address avaliable."); }
                if (joDetail["result"]["formatted_phone_number"] != null) { ViewBag.phoneNumber = joDetail["result"]["formatted_phone_number"]; detail.Add("phoneNumber", joDetail["result"]["formatted_phone_number"].ToString()); } else { ViewBag.phoneNumber = "No phone number avaliable."; detail.Add("phoneNumber", "No phone number avaliable."); }
                if (joDetail["result"]["website"] != null) { ViewBag.website = joDetail["result"]["website"]; detail.Add("website", joDetail["result"]["website"].ToString()); } else { ViewBag.website = "No website avaliable."; detail.Add("website", "No website avaliable."); }
                if (joDetail["result"]["opening_hours"] != null) { if (joDetail["result"]["opening_hours"]["open_now"].ToString() == "true") { ViewBag.openingNow = "Opening"; detail.Add("openingNow", "Opening"); } else { ViewBag.openingNow = "Closed"; detail.Add("openingNow", "Closed"); } } else { ViewBag.openingNow = "Unknown"; detail.Add("openingNow", "Unknown"); }
                if (joDetail["result"]["opening_hours"] != null) { ViewBag.weekday = joDetail["result"]["opening_hours"]["weekday_text"]; detail.Add("weekday", joDetail["result"]["opening_hours"]["weekday_text"].ToString()); } else { ViewBag.weekday = "No weekday information avaliable."; detail.Add("weekday", "No weekday information avaliable."); }
                if (joDetail["result"]["reviews"] != null) { ViewBag.review = joDetail["result"]["reviews"]; detail.Add("review", joDetail["result"]["reviews"].ToString()); } else { ViewBag.review = "No review avaliable."; detail.Add("review", "No review avaliable."); }
            }
            else { ViewBag.imUrl = "../../Image/noPhoto.png"; detail.Add("imUrl", "../../Image/noPhoto.png"); ViewBag.openingNow = "Unknown"; detail.Add("openingNow", "Unknown"); ViewBag.website = "No website avaliable."; detail.Add("website", "No website avaliable."); ViewBag.phoneNumber = "No phone number avaliable."; detail.Add("phoneNumber", "No phone number avaliable."); ViewBag.rating = "No rating avaliable."; detail.Add("rating", "No rating avaliable."); ViewBag.weekday = "No opening hours avaliable."; detail.Add("weekday", "No weekday information avaliable."); ViewBag.review = "No review avaliable."; detail.Add("review", "No review avaliable."); }

            
            return Content(JsonConvert.SerializeObject(detail).ToString());
        }
        // GET: Places/Create
        public ActionResult Create()
        {
            ViewBag.ActivityActivityId = new SelectList(db.Activities, "ActivityId", "Name");
            return View();
        }

        // POST: Places/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PlaceId,Name,Address,Facility,Lat,Lon,Suburb,ActivityActivityId")] Place place)
        {
            if (ModelState.IsValid)
            {
                db.Places.Add(place);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ActivityActivityId = new SelectList(db.Activities, "ActivityId", "Name", place.ActivityActivityId);
            return View(place);
        }

        // GET: Places/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Place place = db.Places.Find(id);
            if (place == null)
            {
                return HttpNotFound();
            }
            ViewBag.ActivityActivityId = new SelectList(db.Activities, "ActivityId", "Name", place.ActivityActivityId);
            return View(place);
        }

        // POST: Places/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PlaceId,Name,Address,Facility,Lat,Lon,Suburb,ActivityActivityId")] Place place)
        {
            if (ModelState.IsValid)
            {
                db.Entry(place).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ActivityActivityId = new SelectList(db.Activities, "ActivityId", "Name", place.ActivityActivityId);
            return View(place);
        }

        // GET: Places/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Place place = db.Places.Find(id);
            if (place == null)
            {
                return HttpNotFound();
            }
            return View(place);
        }

        // POST: Places/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Place place = db.Places.Find(id);
            db.Places.Remove(place);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
