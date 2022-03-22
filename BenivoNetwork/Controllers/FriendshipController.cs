using BenivoNetwork.BLL.Services;
using BenivoNetwork.Common.Models;
using System;
using System.Web.Http;

namespace BenivoNetwork.Controllers
{
    public class FriendshipController : BaseApiController
    {
        private readonly IFriendshipService _friendshipService;

        public FriendshipController(IFriendshipService friendshipService)
        {
            _friendshipService = friendshipService;
        }

        //TODO: get simpler model
        public IHttpActionResult Post(UserModel model)
        {
            try
            {
                _friendshipService.Add(model.ID);
            }
            catch (Exception ex)
            {
                return Error(ex);
            }

            return Ok();
        }

        //TODO: get simpler model
        public IHttpActionResult Put(UserModel model)
        {
            try
            {
                _friendshipService.Accept(model.ID);
            }
            catch (Exception ex)
            {
                return Error(ex);
            }

            return Ok();
        }

        public IHttpActionResult Delete(int id)
        {
            try
            {
                _friendshipService.Remove(id);
            }
            catch (Exception ex)
            {
                return Error(ex);
            }

            return Ok();
        }
    }
}
