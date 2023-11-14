//------------------------------------------------------------------------------
// <copyright file="HortCropCoverageActualBlock.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//-----------------------------------------------------------------------------
namespace ADIDAS.Model.Entities
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// HortCropCoverageActualBlock.
    /// </summary>
    public class HortCropCoverageActualBlock
    {
        /// <summary>
        /// Gets or Sets Reported_Date.
        /// </summary>
        public DateTime? Reported_Date { get; set; }

        /// <summary>
        /// Gets or Sets District_Id.
        /// </summary>
        public int District_Id { get; set; }

        /// <summary>
        /// Gets or Sets District_Name.
        /// </summary>
        public string District_Name { get; set; }

        /// <summary>
        /// Gets or Sets Block_Id.
        /// </summary>
        public int Block_Id { get; set; }

        /// <summary>
        /// Gets or Sets Block_Name.
        /// </summary>
        public string Block_Name { get; set; }

        /// <summary>
        /// Gets or Sets Season_Id.
        /// </summary>
        public int Season_Id { get; set; }

        /// <summary>
        /// Gets or Sets Season_Name.
        /// </summary>
        public string Season_Name { get; set; }

        /// <summary>
        /// Gets or Sets CropList.
        /// </summary>
        public List<HortCropCvrgEntity> CropList { get; set; }

        /// <summary>
        /// Gets or Sets Refreshed_date.
        /// </summary>
        public DateTime? Refreshed_date { get; set; }

        /// <summary>
        /// Gets or Sets Refreshed_userid.
        /// </summary>
        public int? Refreshed_userid { get; set; }

        /// <summary>
        /// Gets or Sets ADH_Approval_flag.
        /// </summary>
        public string ADH_Approval_flag { get; set; }

        /// <summary>
        /// Gets or Sets ADH_Approval_Reason.
        /// </summary>
        public string ADH_Approval_Reason { get; set; }

        /// <summary>
        /// Gets or Sets ADH_Approved_date.
        /// </summary>
        public DateTime? ADH_Approved_date { get; set; }

        /// <summary>
        /// Gets or Sets ADH_Approved_userid.
        /// </summary>
        public int? ADH_Approved_userid { get; set; }

        /// <summary>
        /// Gets or Sets Refreshed_username.
        /// </summary>
        public string Refreshed_username { get; set; }

        /// <summary>
        /// Gets or Sets Adh_approved_username.
        /// </summary>
        public string Adh_approved_username { get; set; }
    }
}
