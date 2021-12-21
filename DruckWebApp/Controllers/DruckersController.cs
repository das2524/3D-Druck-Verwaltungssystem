using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DruckLib;

namespace DruckWebApp.Controllers
{
    public class DruckersController : BaseController
    {

        // GET: Druckers
        public ActionResult Index()
        {
            var druckerSet = db.DruckerSet.Include(d => d.Person);
            return View(druckerSet.ToList());
        }

        // GET: Druckers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Drucker drucker = db.DruckerSet.Find(id);
            if (drucker == null)
            {
                return HttpNotFound();
            }
            return View(drucker);
        }

        // GET: Druckers/Create
        public ActionResult Create()
        {
            if (!hasUser())
            {
                TempData["alertMessage"] = "You have to be Logged in to perform this action";
                return RedirectToAction("Index", "Login");
            }

            //ViewBag.PersonId = new SelectList(db.PersonSet, "Id", "Vorname");
            return View();
        }

        // POST: Druckers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Besitzer,Bauraum,VerfuegbareMaterialen")] Drucker drucker)
        {
            drucker.Besitzer = LoggedInUser.Id;

            if (ModelState.IsValid)
            {
                db.DruckerSet.Add(drucker);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PersonId = new SelectList(db.PersonSet, "Id", "Vorname", drucker.Besitzer);
            return View(drucker);
        }

        // GET: Druckers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Drucker drucker = db.DruckerSet.Find(id);
            if (drucker == null)
            {
                return HttpNotFound();
            }
            ViewBag.PersonId = new SelectList(db.PersonSet, "Id", "Vorname", drucker.Besitzer);
            return View(drucker);
        }

        // POST: Druckers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Besitzer,VerfuegbareMaterialen")] Drucker drucker)
        {
            if (ModelState.IsValid)
            {
                db.Entry(drucker).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PersonId = new SelectList(db.PersonSet, "Id", "Vorname", drucker.Besitzer);
            return View(drucker);
        }

        // GET: Druckers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Drucker drucker = db.DruckerSet.Find(id);
            if (drucker == null)
            {
                return HttpNotFound();
            }
            return View(drucker);
        }

        // POST: Druckers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Drucker drucker = db.DruckerSet.Find(id);
            db.DruckerSet.Remove(drucker);
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
