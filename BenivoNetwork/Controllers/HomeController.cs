using BenivoNetwork.Enums;
using BenivoNetwork.Models;
using System;
using System.Collections.Generic;
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

        [HttpGet]
        public ActionResult Test()
        {
            var model = new HomeModel
            {
                Count = 4,
                DayType = TestEnum.Night,
                IsImported = true,
                Text = "Apple",
                Names = new List<string>
                {
                    "Golden",
                    "Simirenko",
                    "Demirchyan"
                }
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult Test(HomeModel model)
        {
            return View(model);
        }
    }
}