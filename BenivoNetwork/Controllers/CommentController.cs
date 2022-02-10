using BenivoNetwork.BLL.Services;
using System.Web.Http;

namespace BenivoNetwork.Controllers
{
    public class CommentController : ApiController
    {
        private readonly IUserService _userService;

        public CommentController(IUserService userService)
        {
            _userService = userService;
        }

        public IHttpActionResult Get()
        {
            return Ok(_userService.SearchUsers("ar"));
        }
    }
}
