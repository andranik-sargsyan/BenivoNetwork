using BenivoNetwork.BLL.Services;
using BenivoNetwork.Common.Models;
using System;
using System.Web.Http;

namespace BenivoNetwork.Controllers
{
    public class UserController : BaseApiController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        public IHttpActionResult Put(UserModel model)
        {
            if (!ModelState.IsValid)
            {
                return Error(ModelErrors);
            }

            try
            {
                _userService.Update(model);
            }
            catch (Exception ex)
            {
                return Error(ex);
            }

            return Success();
        }
    }
}
