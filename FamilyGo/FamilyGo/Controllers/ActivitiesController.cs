﻿using System;
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


        private FamilyGoiteration2_dbEntities1 db = new FamilyGoiteration2_dbEntities1();

        // GET: Activities
        public ActionResult Index()
        {
            // ViewBag.activities = db.Activities.ToList();
            //  return View(db.Activities.ToList());
            var age = from a in db.Activities
                      select a;
            if (!String.IsNullOrEmpty("12"))
            {
                age = age.Where(a => a.Description.Contains("12"));
            }
            return View(age.ToList());
        }

        public ActionResult Information()
        {

            var age = from a in db.Activities
                      select a;
            if (!String.IsNullOrEmpty("1")) {
                age = age.Where(a => a.Description.Contains("1"));
            }
           
            List<Activity> activityList = age.ToList();
            var newlist = activityList.OrderBy(x => x.Name).ToList();
            return View(newlist);
        }

        public ActionResult InformationTwo()
        {
            var age = from a in db.Activities
                      select a;
            if (!String.IsNullOrEmpty("2"))
            {
                age = age.Where(a => a.Description.Contains("2"));
            }

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
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
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
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
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
