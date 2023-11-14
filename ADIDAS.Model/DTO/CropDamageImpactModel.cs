//------------------------------------------------------------------------------
// <copyright file="CropDamageImpactModel.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------
namespace ADIDAS.Model.DTO
{
    /// <summary>
    /// CropDamage Impact Model.
    /// </summary>
    public class CropDamageImpactModel
    {
        /// <summary>
        /// Gets or Sets Damage_id.
        /// </summary>
        public int? Damage_id { get; set; }

        /// <summary>
        /// Gets or Sets Damage_area.
        /// </summary>
        public decimal? Damage_area { get; set; }

        /// <summary>
        /// Gets or Sets Crop_id.
        /// </summary>
        public int? Crop_id { get; set; }

        /// <summary>
        /// Gets or Sets Crop_name.
        /// </summary>
        public string Crop_name { get; set; }

        /// <summary>
        /// Gets or Sets Panchayat_id.
        /// </summary>
        public int? Panchayat_id { get; set; }

        /// <summary>
        /// Gets or Sets Panchayat_name.
        /// </summary>
        public string Panchayat_name { get; set; }

        /// <summary>
        /// Gets or Sets District_name.
        /// </summary>
        public string District_name { get; set; }

        /// <summary>
        /// Gets or Sets District_id.
        /// </summary>
        public int? District_id { get; set; }

        /// <summary>
        /// Gets or Sets Block_id.
        /// </summary>
        public int? Block_id { get; set; }

        /// <summary>
        /// Gets or Sets Block_name.
        /// </summary>
        public string Block_name { get; set; }

        /// <summary>
        /// Gets or Sets Damage_reason_id.
        /// </summary>
        public int? Damage_reason_id { get; set; }

        /// <summary>
        /// Gets or Sets Damage_reason_name.
        /// </summary>
        public string Damage_reason_name { get; set; }

        /// <summary>
        /// Gets or Sets Perennial_horti_impact.
        /// </summary>
        public decimal? Perennial_horti_impact { get; set; }

        /// <summary>
        /// Gets or Sets Perennial_sugarcane_impact.
        /// </summary>
        public decimal? Perennial_sugarcane_impact { get; set; }

        /// <summary>
        /// Gets or Sets Total_impact_area.
        /// </summary>
        public decimal? Total_impact_area { get; set; }

        /// <summary>
        /// Gets or Sets Grand_total_impact.
        /// </summary>
        public decimal? Grand_total_impact { get; set; }
    }
}
