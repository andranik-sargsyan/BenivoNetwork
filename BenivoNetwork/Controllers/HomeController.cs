using BenivoNetwork.BLL.Services;
using BenivoNetwork.Common.Models;
using BenivoNetwork.Filters;
using BenivoNetwork.Models;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Web.Mvc;

namespace BenivoNetwork.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IAccountService _accountService;
        private readonly IUserService _userService;
        private readonly IMessageService _messageService;

        public HomeController(
            IAccountService accountService,
            IUserService userService,
            IMessageService messageService)
        {
            _accountService = accountService;
            _userService = userService;
            _messageService = messageService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult UserProfile(string id)
        {
            var model = _userService.GetUser(id);

            return model == null
                ? View("_NotFound")
                : View(model);
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
        public ActionResult Messages(int? id)
        {
            var model = _messageService.GetConversations(id);

            return View(model);
        }

        [HttpGet]
        public ActionResult MessagesPartial(int id)
        {
            var model = _messageService.GetByUserID(id);

            return PartialView("_Messages", model);
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
        [FormName("Login")]
        public ActionResult Welcome(LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(new WelcomeModel(model));
            }

            var loginResult = _accountService.Login(model);
            if (!loginResult.IsSuccessful)
            {
                var returnModel = new WelcomeModel(model)
                {
                    ErrorMessage = loginResult.Message
                };

                return View(returnModel);
            }

            if (!string.IsNullOrWhiteSpace(model.ReturnUrl))
            {
                return Redirect(model.ReturnUrl);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        [AllowAnonymous]
        [FormName("Register")]
        public ActionResult Welcome(RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(new WelcomeModel(model));
            }

            var registerResult = _accountService.Register(model);
            if (!registerResult.IsSuccessful)
            {
                var returnModel = new WelcomeModel(model)
                {
                    ErrorMessage = registerResult.Message
                };

                return View(returnModel);
            }

            var loginResult = _accountService.Login(new LoginModel
            {
                Login = model.Email,
                Password = model.Password
            });

            if (!loginResult.IsSuccessful)
            {
                var returnModel = new WelcomeModel(model)
                {
                    ErrorMessage = loginResult.Message
                };

                return View(returnModel);
            }

            return RedirectToAction("UserProfile", new { id = registerResult.ID });
        }

        [HttpPost]
        [AllowAnonymous]
        public JsonResult Logout()
        {
            _accountService.Logout();

            return JsonNet(new ResponseModel { IsSuccessful = true });
        }
    }
}