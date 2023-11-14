// <copyright file="PushNotificationInfo.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//----------------------------------------------------------------------------
namespace ADIDAS.Model.Entities
{
    /// <summary>
    /// PushNotificationInfo.
    /// </summary>
    public class PushNotificationInfo
    {
        /// <summary>
        /// Gets or Sets UserId.
        /// </summary
        public long UserId { get; set; }

        /// <summary>
        /// Gets or Sets NotificationId.
        /// </summary
        public string NotificationId { get; set; }
    }

    /// <summary>
    /// NotificationItem.
    /// </summary>
    public class NotificationItem
    {
        /// <summary>
        /// Gets or Sets GroupId.
        /// </summary
        public string GroupId { get; set; }

        /// <summary>
        /// Gets or Sets UserId.
        /// </summary
        public long UserId { get; set; }

        /// <summary>
        /// Gets or Sets Title.
        /// </summary
        public string Title { get; set; }

        /// <summary>
        /// Gets or Sets Body.
        /// </summary
        public string Body { get; set; }
    }

    /// <summary>
    /// NotificationResponse.
    /// </summary>
    public class NotificationResponse
    {
        /// <summary>
        /// Gets or Sets GroupId.
        /// </summary
        public long GroupId { get; set; }

        /// <summary>
        /// Gets or Sets UserId.
        /// </summary
        public long UserId { get; set; }

        /// <summary>
        /// Gets or Sets Token.
        /// </summary
        public string Token { get; set; }

        /// <summary>
        /// Gets or Sets Response.
        /// </summary
        public string Response { get; set; }
    }
}
