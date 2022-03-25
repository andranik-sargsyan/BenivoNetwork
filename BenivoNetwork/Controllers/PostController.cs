using BenivoNetwork.BLL.Services;
using BenivoNetwork.Common.Models;
using System;
using System.Web.Http;

namespace BenivoNetwork.Controllers
{
    public class PostController : BaseApiController
    {
        private readonly IPostService _postService;

        public PostController(IPostService postService)
        {
            _postService = postService;
        }

        public IHttpActionResult Post(PostModel model)
        {
            if (!ModelState.IsValid)
            {
                return Error(ModelErrors);
            }

            try
            {
                _postService.Add(model);
            }
            catch (Exception ex)
            {
                //TODO: remove error handling
                return Error(ex);
            }

            return Success();
        }
    }
}
