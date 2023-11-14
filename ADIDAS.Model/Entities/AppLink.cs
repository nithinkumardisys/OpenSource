//------------------------------------------------------------------------------
// <copyright file="AppLink.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------
namespace ADIDAS.Model.Entities
{
    using System;

    /// <summary>
    /// Application Link.
    /// </summary>
    public class AppLink
    {
        /// <summary>
        /// Gets or Sets Link_id.
        /// </summary>
        public int Link_id { get; set; }

        /// <summary>
        /// Gets or Sets App_name.
        /// </summary>
        public string App_name { get; set; }

        /// <summary>
        /// Gets or Sets Link_url.
        /// </summary>
        public string Link_url { get; set; }

        /// <summary>
        /// Gets or Sets Img_file_name.
        /// </summary>
        public string Img_file_name { get; set; }

        /// <summary>
        /// Gets or Sets Img_file_location.
        /// </summary>
        public string Img_file_location { get; set; }

        /// <summary>
        /// Gets or Sets Rec_created_userid.
        /// </summary>
        public string Rec_created_userid { get; set; }

        /// <summary>
        /// Gets or Sets Rec_created_date.
        /// </summary>
        public DateTime? Rec_created_date { get; set; }

        /// <summary>
        /// Gets or Sets Rec_updated_userid.
        /// </summary>
        public string Rec_updated_userid { get; set; }

        /// <summary>
        /// Gets or Sets Rec_updated_date.
        /// </summary>
        public DateTime? Rec_updated_date { get; set; }
    }
}
