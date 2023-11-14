// <copyright file="PushNotification.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//----------------------------------------------------------------------------
namespace ShareCare.Models.Models
{
    /// <summary>
    /// PushNotification.
    /// </summary>
    public class PushNotification
    {
        /// <summary>
        /// Gets or Sets Userid.
        /// </summary
        public long Userid { get; set; }

        /// <summary>
        /// Gets or Sets Deviceid.
        /// </summary
        public string Deviceid { get; set; }

        /// <summary>
        /// Gets or Sets Fcmtoken.
        /// </summary
        public string Fcmtoken { get; set; }

        /// <summary>
        /// Gets or Sets Profiletype.
        /// </summary
        public string Profiletype { get; set; }
    }

    /// <summary>
    /// Notification.
    /// </summary>
    public class Notification
    {
        /// <summary>
        /// Gets or Sets Body.
        /// </summary
        public string Body { get; set; }

        /// <summary>
        /// Gets or Sets Title.
        /// </summary
        public string Title { get; set; }
    }

    /// <summary>
    /// NotificationInfo.
    /// </summary>
    public class NotificationInfo
    {
        /// <summary>
        /// Gets or Sets Body.
        /// </summary
        public string Body { get; set; }

        /// <summary>
        /// Gets or Sets Title.
        /// </summary
        public string Title { get; set; }

        /// <summary>
        /// Gets or Sets UserId.
        /// </summary
        public long UserId { get; set; }

        /// <summary>
        /// Gets or Sets DeviceId.
        /// </summary
        public string DeviceId { get; set; }

        /// <summary>
        /// Gets or Sets Profiletype.
        /// </summary
        public string Profiletype { get; set; }
    }

    /// <summary>
    /// Data.
    /// </summary>
    public class Data
    {
        /// <summary>
        /// Gets or Sets Click_action.
        /// </summary
        public string Click_action { get; set; }

        /// <summary>
        /// Gets or Sets Id.
        /// </summary
        public string Id { get; set; }

        /// <summary>
        /// Gets or Sets Status.
        /// </summary
        public string Status { get; set; }

        /// <summary>
        /// Gets or Sets Deeplink.
        /// </summary
        public string Deeplink { get; set; }
    }

    /// <summary>
    /// PushNotify.
    /// </summary>
    public class PushNotify
    {
        /// <summary>
        /// Gets or Sets Notification.
        /// </summary
        public Notification Notification { get; set; }

        /// <summary>
        /// Gets or Sets Priority.
        /// </summary
        public string Priority { get; set; }

        /// <summary>
        /// Gets or Sets DistrictId.
        /// </summary
        public Data Data { get; set; }

        /// <summary>
        /// Gets or Sets To.
        /// </summary
        public string To { get; set; }
    }
}
