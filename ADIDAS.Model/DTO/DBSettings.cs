//------------------------------------------------------------------------------
// <copyright file="DBSettings.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------
namespace ADIDAS.Model.DTO
{
    /// <summary>
    /// Settings.
    /// </summary>
    public class DBSettings
    {
        /// <summary>
        /// Gets or Sets ConnectionString.
        /// </summary>
        public string ConnectionString { get; set; }
    }

    /// <summary>
    /// Notifications.
    /// </summary>
    public class Notifications
    {
        /// <summary>
        /// Gets or Sets ServerKey.
        /// </summary>
        public string ServerKey { get; set; }

        /// <summary>
        /// Gets or Sets NotificationUrl.
        /// </summary>
        public string NotificationUrl { get; set; }
    }
}
