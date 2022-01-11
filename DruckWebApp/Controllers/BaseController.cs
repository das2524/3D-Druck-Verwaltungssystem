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
    public class BaseController : Controller
    {
        protected PrintingContainer db =
                new PrintingContainer();


        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            base.Initialize(requestContext);

            if (!hasUser())
            {
                ViewBag.User = "Nicht Angemeldet";
            }
            else
            {
                ViewBag.User = LoggedInUser.Vorname;
            }

        }

        private static String UserIdKey = "UserId";
        public Person setUser()
        {
            int? userId = Session[UserIdKey] as int?;
            if (userId == null) return null;
            Person res = db.PersonSet.Find(userId);
            if (res == null) return null;
            ViewBag.User = res;
            _LoggedInUser = res;
            return res;
        }

        private Person _LoggedInUser = null;
        protected Person LoggedInUser => _LoggedInUser ?? setUser();

        protected bool hasUser()
        {
            return LoggedInUser != null;
        }

        protected bool hasDrucker()
        {
            if (LoggedInUser?.Drucker.Count != 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        protected void setUserId(int userId)
        {
            Session[UserIdKey] = userId;
        }

        public ActionResult Data()
        {
            var User0 = new Person();
            var User1 = new Person();
            var User2 = new Person();

            User0.Vorname = "Hans";
            User1.Vorname = "Peter";
            User2.Vorname = "Gregor";

            User0.Nachname = "Müller";
            User1.Nachname = "Schweiger";
            User2.Nachname = "Klaus";

            User0.EMail = "MHans@yahoo.de";
            User1.EMail = "PS@gmail.com";
            User2.EMail = "Klausi98@hotmail.de";

            db.PersonSet.Add(User0);
            db.SaveChanges();

            db.PersonSet.Add(User1);
            db.SaveChanges();

            db.PersonSet.Add(User2);
            db.SaveChanges();

            var drucker = new Drucker();
            var drucker2 = new Drucker();

            drucker.Name = "Ender 3";
            drucker2.Name = "Prusa i3";

            drucker.Bauraum = "220x220x300";
            drucker2.Bauraum = "450x450x300";

            drucker.VerfuegbareMaterialen = "PLA, PETG";
            drucker2.VerfuegbareMaterialen = "PLA, PETG, ASA, TPU";

            drucker.Person = User0;
            drucker2.Person = User1;

            db.DruckerSet.Add(drucker);
            db.SaveChanges();

            db.DruckerSet.Add(drucker2);
            db.SaveChanges();

            var Auftrag0 = new Druckauftrag();
            var Auftrag1 = new Druckauftrag();

            Auftrag0.BauteilURL = "www.thingiverse.com";
            Auftrag1.BauteilURL = "www.drive.google.de";

            Auftrag0.Material = "PLA";
            Auftrag1.Material = "PETG";

            Auftrag0.Erstellt = User2;
            Auftrag1.Erstellt = User1;

            Auftrag1.Bearbeitet = User0;

            Auftrag1.gestartet = DateTime.Now;

            db.DruckauftragSet.Add(Auftrag0);
            db.SaveChanges();

            db.DruckauftragSet.Add(Auftrag1);
            db.SaveChanges();

            return RedirectToAction("Index", "Home");

        }

        public void Leeren ()
        {

            db.DruckerSet.RemoveRange(db.DruckerSet);
            db.SaveChanges();

            db.DruckauftragSet.RemoveRange(db.DruckauftragSet);
            db.SaveChanges();

            db.PersonSet.RemoveRange(db.PersonSet);
            db.SaveChanges();

        }


    }
}