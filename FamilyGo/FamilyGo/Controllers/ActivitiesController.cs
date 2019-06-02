using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FamilyGo.Models;

namespace FamilyGo.Controllers
{
    public class ActivitiesController : Controller
    {


        private Melkidsthrive123_DBEntities db = new Melkidsthrive123_DBEntities();

        // GET: Activities for all age group 
        public ActionResult Index()
        {
            // select corresponding activities from database
            var age = from a in db.Activities
                      select a;
            if (!String.IsNullOrEmpty("12"))
            {
                age = age.Where(a => a.Description.Contains("12"));
            }
            return View(age.ToList());
        }
        // activities for 3-6 years old 
        public ActionResult Information()
        {
            // get the activity from database which correspongding to 3-6 years old
            var age = from a in db.Activities
                      select a;
            if (!String.IsNullOrEmpty("1")) {
                age = age.Where(a => a.Description.Contains("1"));
            }
           //arrange the name of activities in alphabetical order
            List<Activity> activityList = age.ToList();
            var newlist = activityList.OrderBy(x => x.Name).ToList();
            return View(newlist);
        }

        public ActionResult InformationTwo()
        {
            // get the activity from database which correspongding to 7-12 years old
            var age = from a in db.Activities
                      select a;
            if (!String.IsNullOrEmpty("2"))
            {
                age = age.Where(a => a.Description.Contains("2"));
            }
            //arrange the name of activities in alphabetical order
            List<Activity> activityList = age.ToList();
            var newlist = activityList.OrderBy(x => x.Name).ToList();
            return View(newlist);
        }

        // GET: Activities/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Activity activity = db.Activities.Find(id);
            if (activity == null)
            {
                return HttpNotFound();
            }
            return View(activity);
        }

        // GET: Activities/Create
        public ActionResult Create()
        {
            return View();
        }

        

        // POST: Activities/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ActivityId,Name,Description")] Activity activity)
        {
            if (ModelState.IsValid)
            {
                db.Activities.Add(activity);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(activity);
        }

        // GET: Activities/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Activity activity = db.Activities.Find(id);
            if (activity == null)
            {
                return HttpNotFound();
            }
            return View(activity);
        }

        // POST: Activities/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ActivityId,Name,Description")] Activity activity)
        {
            if (ModelState.IsValid)
            {
                db.Entry(activity).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(activity);
        }

        // GET: Activities/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Activity activity = db.Activities.Find(id);
            if (activity == null)
            {
                return HttpNotFound();
            }
            return View(activity);
        }

        // POST: Activities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Activity activity = db.Activities.Find(id);
            db.Activities.Remove(activity);
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
