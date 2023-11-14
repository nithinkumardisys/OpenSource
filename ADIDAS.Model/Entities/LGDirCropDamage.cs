//------------------------------------------------------------------------------
// <copyright file="LGDirCropDamage.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//-----------------------------------------------------------------------------
namespace ADIDAS.Model.Entities
{
    /// <summary>
    /// LgDirCropDamage.
    /// </summary>
    public class LgDirCropDamage
    {
        /// <summary>
        /// Gets or Sets District_id.
        /// </summary>
        public int District_id { get; set; }

        /// <summary>
        /// Gets or Sets District_name.
        /// </summary>
        public string District_name { get; set; }

        /// <summary>
        /// Gets or Sets Block_id.
        /// </summary>
        public int Block_id { get; set; }

        /// <summary>
        /// Gets or Sets Block_name.
        /// </summary>
        public string Block_name { get; set; }

        /// <summary>
        /// Gets or Sets Panchayat_id.
        /// </summary>
        public int Panchayat_id { get; set; }

        /// <summary>
        /// Gets or Sets Panchayat_name.
        /// </summary>
        public string Panchayat_name { get; set; }

        /// <summary>
        /// Gets or Sets Area_Covered.
        /// </summary>
        public decimal? Area_Covered { get; set; }

        /// <summary>
        /// Gets or Sets Irrigated_Area_Damaged.
        /// </summary>
        public decimal? Irrigated_Area_Damaged { get; set; }

        /// <summary>
        /// Gets or Sets Damage_Reason.
        /// </summary>
        public string Damage_Reason { get; set; }

        /// <summary>
        /// Gets or Sets Approval_Flag.
        /// </summary>
        public string Approval_Flag { get; set; }
    }
}
