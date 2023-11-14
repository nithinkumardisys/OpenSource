// <copyright file="NotificationEntity.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//----------------------------------------------------------------------------
namespace ADIDAS.Model.Entities
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// NotificationEntity.
    /// </summary>
    public class NotificationEntity
    {
        /// <summary>
        /// Gets or Sets Name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or Sets Message.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Gets or Sets TagList.
        /// </summary>
        public List<string> TagList { get; set; }

        /// <summary>
        /// Gets or Sets Tags.
        /// </summary>
        public string Tags { get; set; }

        /// <summary>
        /// Gets or Sets Mode.
        /// </summary>
        public string Mode { get; set; }

        /// <summary>
        /// Gets or Sets Type.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Gets or Sets Date.
        /// </summary>
        public DateTime? Date { get; set; }

        /// <summary>
        /// Gets or Sets ID.
        /// </summary>
        public int ID { get; set; }
    }
}
