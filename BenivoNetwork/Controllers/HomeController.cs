using BenivoNetwork.BLL.Services;
using BenivoNetwork.Common.Models;
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

        [HttpGet]
        public ActionResult UserProfile(int? id) //TODO: param? //alternate username with ? param
        {
            if (!id.HasValue)
            {
                //TODO: fix or add not found page
                //TODO: or maybe use global exception handling (with try/catch for now - but explain it with Global.asax _OnError~)
                return View("Error");
            }

            var model = _userService.GetUser(id.Value);

            return View(model);
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

            return View(new WelcomeModel(Request.QueryString["ReturnUrl"]));
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Welcome", new WelcomeModel(model));
            }

            var isSuccessful = _accountService.Login(model);

            if (isSuccessful)
            {
                if (!string.IsNullOrWhiteSpace(model.ReturnUrl))
                {
                    return Redirect(model.ReturnUrl);
                }

                return RedirectToAction("Index");
            }

            return View("Welcome", new WelcomeModel(model));
        }

        [HttpPost]
        [AllowAnonymous]
        public JsonResult Logout()
        {
            _accountService.Logout();

            return JsonNet("OK");
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Register(RegisterModel model)
        {
            //TODO: check Register and Login together

            //TODO: validate via ModelState

            var registerResult = _accountService.Register(model);

            //TODO: error message
            if (!registerResult.IsSuccessful)
            {
                return RedirectToAction("Welcome", new { ReturnUrl = model.ReturnUrl });
            }

            //TODO: pass model's sub model
            var isSuccessful = _accountService.Login(new LoginModel
            {
                Login = model.Email,
                Password = model.Password
            });

            //FIX
            if (isSuccessful)
            {
                //TODO: fix ID
                return RedirectToAction("UserProfile", new { id = registerResult.ID });
            }

            return View("Welcome", new WelcomeModel(model));
        }
    }
}