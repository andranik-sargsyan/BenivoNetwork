using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BenivoNetwork.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult UserProfile(int? id)
        {
            ViewBag.FullName = "Andranik Sargsyan";

            //Request["key"]

            //read Request.Headers
            //read Request.Cookies

            //Response.Headers.Add
            //Response.Cookies.Add

            return View();
        }
    }
}