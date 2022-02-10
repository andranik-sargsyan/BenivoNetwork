using BenivoNetwork.BLL.Services;
using BenivoNetwork.Common.Models;
using BenivoNetwork.Enums;
using BenivoNetwork.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace BenivoNetwork.Controllers
{
    [Authorize]
    public class HomeController : BaseController
    {
        private readonly IAccountService _accountService;
        private readonly IUserService _userService;

        public HomeController(
            IAccountService accountService,
            IUserService userService)
        {
            _accountService = accountService;
            _userService = userService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        //TODO: why not visiting this url?
        [HttpGet]
        public ActionResult UserProfile(int id) //TODO: param? //alternate username with ? param
        {
            return View();
        }

        [HttpGet]
        public ActionResult Search(string term)
        {
            var result = _userService.SearchUsers(term);

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
        [AllowAnonymous]
        public ActionResult Welcome()
        {
            if (Request.QueryString["ReturnUrl"] == "/")
            {
                Response.Redirect(Request.Url.AbsolutePath);
            }

            return View(new LoginModel { ReturnUrl = Request.QueryString["ReturnUrl"] });
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(LoginModel model)
        {
            //TODO: validate via ModelState

            var isSuccessful = _accountService.Login(model);

            if (isSuccessful)
            {
                if (!string.IsNullOrWhiteSpace(model.ReturnUrl))
                {
                    return Redirect(model.ReturnUrl);
                }

                return RedirectToAction("Index");
            }

            return RedirectToAction("Welcome", new { ReturnUrl = model.ReturnUrl });
        }

        [HttpPost]
        [AllowAnonymous]
        public JsonResult Logout()
        {
            _accountService.Logout();

            return JsonNet("OK");
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
            var users = _userService.GetUsers();

            term = term.ToLower();

            var data = users.Where(u => u.FirstName.ToLower().Contains(term));

            return JsonNet(data);
        }

        #endregion
    }
}