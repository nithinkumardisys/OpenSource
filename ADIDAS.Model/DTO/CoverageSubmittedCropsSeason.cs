//------------------------------------------------------------------------------
// <copyright file="CoverageSubmittedCropsSeason.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------
namespace ADIDAS.Model.DTO
{
    using System;

    /// <summary>
    /// Coverage Submitted Crops Season.
    /// </summary>
    public class CoverageSubmittedCropsSeason
    {
        /// <summary>
        /// Gets or Sets Crop_id.
        /// </summary>
        public int Crop_id { get; set; }

        /// <summary>
        /// Gets or Sets Season_id.
        /// </summary>
        public int Season_id { get; set; }

        /// <summary>
        /// Gets or Sets Season_name.
        /// </summary>
        public string Season_name { get; set; }

        /// <summary>
        /// Gets or Sets District_id.
        /// </summary>
        public int District_id { get; set; }

        /// <summary>
        /// Gets or Sets Crop_category.
        /// </summary>
        public string Crop_category { get; set; }

        /// <summary>
        /// Gets or Sets Crop_name.
        /// </summary>
        public string Crop_name { get; set; }

        /// <summary>
        /// Gets or Sets Variety.
        /// </summary>
        public string Variety { get; set; }

        /// <summary>
        /// Gets or Sets Approval_flag.
        /// </summary>
        public string Approval_flag { get; set; }

        /// <summary>
        /// Gets or Sets Approval_reason.
        /// </summary>
        public string Approval_reason { get; set; }

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
