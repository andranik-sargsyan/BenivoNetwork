using BenivoNetwork.BLL.Services;
using BenivoNetwork.Common.Models;
using System.Web.Http;

namespace BenivoNetwork.Controllers
{
    public class UserController : ApiController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        public IHttpActionResult Put(UserModel model)
        {
            //ModelState.IsValid is not gonna work here (explain why)

            //TODO: validations (both data and permission)

            //TODO: handle response errors with custom response

            return Ok();
        }
    }
}
