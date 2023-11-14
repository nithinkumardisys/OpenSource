//------------------------------------------------------------------------------
// <copyright file="BihanGuidelineModel.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------
namespace ADIDAS.Model.DTO
{
    using System;

    /// <summary>
    /// Bihan Guideline Model.
    /// </summary>
    public class BihanGuidelineModel
    {
        /// <summary>
        /// Gets or Sets Guideln_id.
        /// </summary>
        public int Guideln_id { get; set; }

        /// <summary>
        /// Gets or Sets Guideln_title.
        /// </summary>
        public string Guideln_title { get; set; }

        /// <summary>
        /// Gets or Sets Data_entry.
        /// </summary>
        public string Data_entry { get; set; }

        /// <summary>
        /// Gets or Sets Data_forward.
        /// </summary>
        public string Data_forward { get; set; }

        /// <summary>
        /// Gets or Sets Guideln_description.
        /// </summary>
        public string Guideln_description { get; set; }

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
