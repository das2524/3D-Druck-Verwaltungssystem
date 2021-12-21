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
    public class DruckauftragsController : BaseController
    {
        
        // GET: Druckauftrags
        public ActionResult Index()
        {
            var offen = db.DruckauftragSet.Where(d => d.gestartet == null).Include(d => d.Erstellt).Include(d => d.Bearbeitet);

            //var filteredResult = from d in offen
                                // where d.gestartet == null
                               //  select d;

            return View(offen.ToList());
        }

        public ActionResult Bearbeitet()
        {
            var bearbeitet = db.DruckauftragSet.Where(d => d.gestartet != null).Include(d => d.Erstellt).Include(d => d.Bearbeitet);

            //var filteredResult = from d in druckauftragSet
            //where d.gestartet != null
            // select d;

            return View(bearbeitet.ToList());
        }

        // GET: Druckauftrags/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Druckauftrag druckauftrag = db.DruckauftragSet.Find(id);
            if (druckauftrag == null)
            {
                return HttpNotFound();
            }
            return View(druckauftrag);
        }

        // GET: Druckauftrags/Create
        public ActionResult Create()
        {
            if (!hasUser())
            {
                TempData["alertMessage"] = "You have to be Logged in to perform this action";
                return RedirectToAction("Index", "Login");
            }

            //ViewBag.PersonId = new SelectList(db.PersonSet, "Id", "Vorname");
            //ViewBag.PersonId1 = new SelectList(db.PersonSet, "Id", "Vorname");
            
            return View();
        }

        // POST: Druckauftrags/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,BauteilURL,Material")] Druckauftrag druckauftrag)
        {

            druckauftrag.ersteller = LoggedInUser.Id;

            if (ModelState.IsValid)
            {
                db.DruckauftragSet.Add(druckauftrag);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PersonId = new SelectList(db.PersonSet, "Id", "Vorname", druckauftrag.ersteller);
            ViewBag.PersonId1 = new SelectList(db.PersonSet, "Id", "Vorname", druckauftrag.bearbeiter);
            return View(druckauftrag);
        }

        // GET: Druckauftrags/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Druckauftrag druckauftrag = db.DruckauftragSet.Find(id);
            if (druckauftrag == null)
            {
                return HttpNotFound();
            }
            ViewBag.PersonId = new SelectList(db.PersonSet, "Id", "Vorname", druckauftrag.ersteller);
            ViewBag.PersonId1 = new SelectList(db.PersonSet, "Id", "Vorname", druckauftrag.bearbeiter);
            return View(druckauftrag);
        }

        // POST: Druckauftrags/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,BauteilURL,Ersteller,Bearbeiter,gestartet")] Druckauftrag druckauftrag)
        {
            if (ModelState.IsValid)
            {
                db.Entry(druckauftrag).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ersteller = new SelectList(db.PersonSet, "Id", "Vorname", druckauftrag.ersteller);
            ViewBag.PersonId1 = new SelectList(db.PersonSet, "Id", "Vorname", druckauftrag.bearbeiter);
            return View(druckauftrag);
        }

        // GET: Druckauftrags/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Druckauftrag druckauftrag = db.DruckauftragSet.Find(id);
            if (druckauftrag == null)
            {
                return HttpNotFound();
            }
            return View(druckauftrag);
        }

        // POST: Druckauftrags/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Druckauftrag druckauftrag = db.DruckauftragSet.Find(id);
            db.DruckauftragSet.Remove(druckauftrag);
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

        public ActionResult  Start(int? id)
        {
            if (!hasUser())
            {
                TempData["alertMessage"] = "You have to be Logged in to perform this action";
                return RedirectToAction("Index", "Login");
            }
            
            if (hasDrucker())
            {
                TempData["alertMessage"] = "This User has no 3D-Printer registered. Please register a Machine first.";
                return RedirectToAction("Index", "Druckers");
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Druckauftrag druckauftrag = db.DruckauftragSet.Find(id);
            if (druckauftrag == null)
            {
                return HttpNotFound();
            }
            //ViewBag.PersonId = new SelectList(db.PersonSet, "Id", "Vorname", druckauftrag.PersonId);
            //ViewBag.PersonId1 = new SelectList(db.PersonSet, "Id", "Vorname", druckauftrag.PersonId1);
            return View(druckauftrag);
        }

        // POST: Druckauftrags/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Start")]
        [ValidateAntiForgeryToken]
        public ActionResult StartConfirmed(int id)
        {
            Druckauftrag druckauftrag = db.DruckauftragSet.Find(id);

            druckauftrag.gestartet = DateTime.Now;

            druckauftrag.bearbeiter = LoggedInUser.Id;

            if (ModelState.IsValid)
            {
                db.Entry(druckauftrag).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Bearbeitet");
            }
            ViewBag.PersonId = new SelectList(db.PersonSet, "Id", "Vorname", druckauftrag.ersteller);
            ViewBag.PersonId1 = new SelectList(db.PersonSet, "Id", "Vorname", druckauftrag.bearbeiter);
            return View(druckauftrag);
        }

        public ActionResult ClearAll()
        {

            db.DruckauftragSet.RemoveRange(db.DruckauftragSet);
            db.SaveChanges();

            return RedirectToAction("Index", "Home");
        }


    }
}
