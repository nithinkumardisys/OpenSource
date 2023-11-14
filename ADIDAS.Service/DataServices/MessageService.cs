//------------------------------------------------------------------------------
// <copyright file="MessageService.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------
namespace ADIDAS.Service.DataServices
{
    using System.Collections.Generic;
    using ADIDAS.Model.Entities;
    using ADIDAS.Repository.Interfaces;
    using ADIDAS.Service.Interfaces;

    public class MessageService : IMessageService
    {
        private readonly IMessageRepository messageRepository;

        public MessageService(IMessageRepository messageRepository)
        {
            this.messageRepository = messageRepository;
        }

        public List<MessageEntity> GetMessage()
        {
            return this.messageRepository.GetMessage();
        }

        public int PostMessage(MessageEntity message)
        {
            return this.messageRepository.PostMessage(message);
        }

        public int DeleteMessage(MessageEntity message)
        {
            return this.messageRepository.DeleteMessage(message);
        }
    }
}
