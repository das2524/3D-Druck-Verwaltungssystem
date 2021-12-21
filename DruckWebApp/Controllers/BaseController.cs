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
    }
}