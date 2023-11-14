//------------------------------------------------------------------------------
// <copyright file="IMessageService.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------

namespace ADIDAS.Service.Interfaces
{
    using System;
    using System.Collections.Generic;
    using ADIDAS.Model.Entities;

    /// <summary>
    /// IMessageService.
    /// </summary>
    public interface IMessageService
    {
        /// <summary>
        /// GetMessage.
        /// </summary>
        /// <returns>MessageEntity list.</returns>
        List<MessageEntity> GetMessage();

        /// <summary>
        /// PostMessage.
        /// </summary>
        /// <param name="message">message.</param>
        /// <returns>integer.</returns>
        int PostMessage(MessageEntity message);

        /// <summary>
        /// DeleteMessage.
        /// </summary>
        /// <param name="message">message.</param>
        /// <returns>integer.</returns>
        int DeleteMessage(MessageEntity message);
    }
}
