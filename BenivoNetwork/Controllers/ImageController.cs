using BenivoNetwork.BLL.Services;
using System;
using System.Web;
using System.Web.Http;

namespace BenivoNetwork.Controllers
{
    public class ImageController : BaseApiController
    {
        private readonly IUserService _userService;

        public ImageController(IUserService userService)
        {
            _userService = userService;
        }

        public IHttpActionResult Post()
        {
            try
            {
                var request = HttpContext.Current.Request;

                var imageFile = request.Files["ImageFile"];
                var id = int.Parse(request.Form["ID"]);

                _userService.UpdateImage(imageFile, id);
            }
            catch (Exception ex)
            {
                return Error(ex);
            }

            return Success();
        }

        public IHttpActionResult Delete(int id)
        {
            try
            {
                _userService.RemoveImage(id);
            }
            catch (Exception ex)
            {
                return Error(ex);
            }

            return Success();
        }
    }
}
