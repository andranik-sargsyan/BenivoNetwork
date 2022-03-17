using BenivoNetwork.Models;
using System.Web.Http;

namespace BenivoNetwork.Controllers
{
    [Authorize]
    public class BaseApiController : ApiController
    {
        protected IHttpActionResult Success(string message = default)
        {
            return Ok(new ResponseModel(true, message));
        }

        protected IHttpActionResult Success<T>(T data, string message = default)
        {
            return Ok(new ResponseModel<T>(true, message, data));
        }

        protected IHttpActionResult Error(string message = default)
        {
            return Ok(new ResponseModel(false, message));
        }
    }
}
