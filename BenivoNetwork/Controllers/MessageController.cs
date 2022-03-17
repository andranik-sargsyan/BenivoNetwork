using BenivoNetwork.BLL.Services;
using BenivoNetwork.Common.Models;
using BenivoNetwork.Models;
using System;
using System.Web.Http;

namespace BenivoNetwork.Controllers
{
    public class MessageController : BaseApiController
    {
        private readonly IMessageService _messageService;

        public MessageController(IMessageService messageService)
        {
            _messageService = messageService;
        }

        public IHttpActionResult Get(int id, [FromUri] int lastMessageID)
        {
            var hasNewMessages = _messageService.HasNewMessages(id, lastMessageID);

            return Success(hasNewMessages);
        }

        public IHttpActionResult Post(MessageModel model)
        {
            if (!ModelState.IsValid)
            {
                //TODO: remove error handling?
                return Error();
            }

            try
            {
                _messageService.Add(model);
            }
            catch (Exception ex)
            {
                //TODO: remove error handling
                return Error(ex.Message);
            }

            return Success();
        }
    }
}
