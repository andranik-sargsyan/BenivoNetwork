using BenivoNetwork.BLL;
using BenivoNetwork.Enums;
using BenivoNetwork.Models;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace BenivoNetwork.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult UserProfile(int? id)
        {
            ViewBag.FullName = "Andranik Sargsyan";

            return View();
        }

        #region Learning Purposes

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
        public JsonResult Test(HomeModel model)
        {
            Thread.Sleep(3000);

            return Json("OK", JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Users()
        {
            var model = UserService.GetUsers();

            return View(model);
        }

        #endregion
    }
}