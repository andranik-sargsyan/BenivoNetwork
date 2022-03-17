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
                //TODO: fix to ResponseModel with OK later, remove error handling
                return BadRequest(ModelState);
            }

            try
            {
                _userService.Update(model);
            }
            catch (Exception ex)
            {
                //TODO: fix to ResponseModel with OK later, remove error handling
                return BadRequest(ex.Message);
            }

            return Ok();
        }
    }
}
