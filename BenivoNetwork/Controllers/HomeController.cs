using BenivoNetwork.BLL;
using BenivoNetwork.Common.Models;
using BenivoNetwork.Enums;
using BenivoNetwork.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace BenivoNetwork.Controllers
{
    public class HomeController : BaseController
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult UserProfile(int id)
        {
            ViewBag.FullName = "Andranik Sargsyan";

            return View();
        } //TODO: param? //alternate username with ? param

        [HttpGet]
        public ActionResult Search(string term)
        {
            var result = UserService.SearchUsers(term);

            return View(new SearchModel
            {
                Term = term,
                Results = result
            });
        }

        [HttpGet]
        public ActionResult Messages() //TODO: param?
        {
            return View();
        }

        [HttpGet]
        public ActionResult Notifications() //TODO: param?
        {
            return View();
        }

        [HttpGet]
        public ActionResult Post(int id) //TODO: param?
        {
            return View();
        }

        [HttpGet]
        public ActionResult Welcome()
        {
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
            string s = "Hello";
            char c = s[10];

            return JsonNet("OK");
        }

        [HttpGet]
        public JsonResult GetUsers(string term)
        {
            var users = UserService.GetUsers();

            term = term.ToLower();

            var data = users.Where(u => u.FirstName.ToLower().Contains(term));

            return JsonNet(data);
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