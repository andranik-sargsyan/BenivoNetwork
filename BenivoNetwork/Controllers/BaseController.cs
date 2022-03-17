using BenivoNetwork.Models;
using System.Web.Mvc;

namespace BenivoNetwork.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
        protected JsonResult JsonNet(object data)
        {
            return new JsonNetResult
            {
                Data = data
            };
        }
    }
}