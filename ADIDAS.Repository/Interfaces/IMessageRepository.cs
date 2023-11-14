//------------------------------------------------------------------------------
// <copyright file="IMessageRepository.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//----------------------------------------------------------------------------
namespace ADIDAS.Repository.Interfaces
{
    using System.Collections.Generic;
    using ADIDAS.Model.Entities;

    /// <summary>
    /// IMessageRepository.
    /// </summary>
    public interface IMessageRepository
    {
        /// <summary>
        /// GetMessage.
        /// </summary>
        /// <returns>MessageEntity.</returns>
        List<MessageEntity> GetMessage();

        /// <summary>
        /// PostMessage.
        /// </summary>
        /// <param name="message">message.</param>
        /// <returns>Response.</returns>
        int PostMessage(MessageEntity message);

        /// <summary>
        /// DeleteMessage.
        /// </summary>
        /// <param name="message">message.</param>
        /// <returns>Response.</returns>
        int DeleteMessage(MessageEntity message);
    }
}
