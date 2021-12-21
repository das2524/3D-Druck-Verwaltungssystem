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
    public class LoginController : BaseController
    {

        // GET: Login
        public ActionResult Index()
        {
            //ViewBag.CurrentUser = db.PersonSet.Find(Session[UserIdKey]);

            return View(db.PersonSet.ToList());
        }

        // GET: Login/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Person person = db.PersonSet.Find(id);
            if (person == null)
            {
                return HttpNotFound();
            }
            return View(person);
        }

        public ActionResult Select(int? id)
        {
            if (id == null)
            {
                return new
                         HttpStatusCodeResult(
                             HttpStatusCode.BadRequest);
            }
            Person user = db.PersonSet.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            setUserId(id.Value);
            return RedirectToAction("Index");
        }


    }
}
