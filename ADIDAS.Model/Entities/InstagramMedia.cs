//------------------------------------------------------------------------------
// <copyright file="InstagramMedia.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//-----------------------------------------------------------------------------
namespace ADIDAS.Model.Entities
{
    using System;

    /// <summary>
    /// InstagramMedia.
    /// </summary>
    public class InstagramMedia
    {
        /// <summary>
        /// Gets or Sets Media_Url.
        /// </summary>
        public string Media_Url { get; set; }

        /// <summary>
        /// Gets or Sets Media_Type.
        /// </summary>
        public string Media_Type { get; set; }

        /// <summary>
        /// Gets or Sets Caption.
        /// </summary>
        public string Caption { get; set; }

        /// <summary>
        /// Gets or Sets ID.
        /// </summary>
        public Int64 ID { get; set; }

        /// <summary>
        /// Gets or Sets TimeStamp.
        /// </summary>
        public DateTime TimeStamp { get; set; }
    }
}