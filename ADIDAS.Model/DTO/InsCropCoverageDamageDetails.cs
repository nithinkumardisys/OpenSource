//------------------------------------------------------------------------------
// <copyright file="InsCropCoverageDamageDetails.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------

namespace ADIDAS.Model.DTO
{
    /// <summary>
    /// Ins CropCoverage Damage Details.
    /// </summary>
    public class InsCropCoverageDamageDetails
    {
        /// <summary>
        /// Gets or Sets User_Id.
        /// </summary>
        public string User_Id { get; set; }

        /// <summary>
        /// Gets or Sets Panchayat_id.
        /// </summary>
        public int Panchayat_id { get; set; }

        /// <summary>
        /// Gets or Sets Damage_id.
        /// </summary>
        public int Damage_id { get; set; }

        /// <summary>
        /// Gets or Sets Damage_reason_id.
        /// </summary>
        public int Damage_reason_id { get; set; }

        /// <summary>
        /// Gets or Sets Crop_id.
        /// </summary>
        public int Crop_id { get; set; }

        /// <summary>
        /// Gets or Sets Damage_area.
        /// </summary>
        public decimal Damage_area { get; set; }
    }
}
