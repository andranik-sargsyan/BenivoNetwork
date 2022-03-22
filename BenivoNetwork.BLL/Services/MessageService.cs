using BenivoNetwork.BLL.Extensions;
using BenivoNetwork.Common.Helpers;
using BenivoNetwork.Common.Models;
using BenivoNetwork.DAL;
using BenivoNetwork.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BenivoNetwork.BLL.Services
{
    public class MessageService : IMessageService
    {
        private readonly IUnitOfWork _unitOfWork;

        public MessageService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<ConversationModel> GetConversations(int? userID)
        {
            var claimID = ClaimHelper.ID;

            var users = _unitOfWork.UserRepository.Get(u => u.SentMessages.Any(m => m.ToUserID == claimID) || u.ReceivedMessages.Any(m => m.FromUserID == claimID), includeProperties: "SentMessages,ReceivedMessages");

            var conversations = new List<ConversationModel>();
            foreach (var user in users)
            {
                var lastMessage = user.ReceivedMessages.Where(m => m.FromUserID == claimID)
                        .Concat(user.SentMessages.Where(m => m.ToUserID == claimID))
                        .OrderByDescending(m => m.DateSent)
                        .FirstOrDefault()
                        .MapTo<MessageModel>();

                if (lastMessage != null)
                {
                    lastMessage.IsFromUser = lastMessage.FromUserID == claimID;
                }

                conversations.Add(new ConversationModel
                {
                    User = user.MapTo<UserModel>(),
                    LastMessage = lastMessage
                });
            }

            conversations = conversations.OrderByDescending(c => c.LastMessage.DateSent).ToList();

            if (userID.HasValue && userID.Value > 0)
            {
                var conversation = conversations.FirstOrDefault(c => c.User.ID == userID.Value);
                if (conversation != null)
                {
                    conversation.IsSelected = true;
                }
                else
                {
                    var user = _unitOfWork.UserRepository.GetByID(userID.Value);
                    if (user != null)
                    {
                        conversations.Insert(0, new ConversationModel
                        {
                            User = user.MapTo<UserModel>(),
                            IsSelected = true,
                            LastMessage = new MessageModel { Text = "..." }
                        });
                    }
                }
            }

            return conversations;
        }

        public IEnumerable<MessageModel> GetByUserID(int id)
        {
            var claimID = ClaimHelper.ID;

            //TODO: later bring only last 10 messages
            //TODO: order by date or ID
            var messages = _unitOfWork.MessageRepository
                .Get(m => m.FromUserID == id && m.ToUserID == claimID || m.FromUserID == claimID && m.ToUserID == id);

            var messageModels = new List<MessageModel>();

            foreach (var message in messages)
            {
                var messageModel = message.MapTo<MessageModel>();

                messageModel.IsFromUser = message.FromUserID == claimID;

                messageModels.Add(messageModel);
            }

            return messageModels;
        }

        public void Add(MessageModel model)
        {
            var claimID = ClaimHelper.ID;

            var message = new Message
            {
                FromUserID = claimID,
                ToUserID = model.ToUserID,
                Text = model.Text,
                DateSent = DateTime.UtcNow
            };

            _unitOfWork.MessageRepository.Insert(message);
            _unitOfWork.Commit();
        }

        public bool HasNewMessages(int fromUserID, int lastMessageID)
        {
            var claimID = ClaimHelper.ID;

            return _unitOfWork.MessageRepository.Any(m => m.FromUserID == fromUserID && m.ToUserID == claimID && m.ID > lastMessageID);
        }
    }
}
