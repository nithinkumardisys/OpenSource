//------------------------------------------------------------------------------
// <copyright file="CropDamageDetailsModel.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------
namespace ADIDAS.Model.DTO
{
    /// <summary>
    /// CropDamage Details Model.
    /// </summary>
    public class CropDamageDetailsModel
    {
        /// <summary>
        /// Gets or Sets Damage_id.
        /// </summary>
        public int? Damage_id { get; set; }

        /// <summary>
        /// Gets or Sets Damage_reason_id.
        /// </summary>
        public int? Damage_reason_id { get; set; }

        /// <summary>
        /// Gets or Sets Irrigated_area_dmg.
        /// </summary>
        public decimal? Irrigated_area_dmg { get; set; }

        /// <summary>
        /// Gets or Sets Irrigated_cost_dmg.
        /// </summary>
        public decimal? Irrigated_cost_dmg { get; set; }

        /// <summary>
        /// Gets or Sets Nonirrigated_area_dmg.
        /// </summary>
        public decimal? Nonirrigated_area_dmg { get; set; }

        /// <summary>
        /// Gets or Sets Nonirrigated_cost_dmg.
        /// </summary>
        public decimal? Nonirrigated_cost_dmg { get; set; }

        /// <summary>
        /// Gets or Sets Total_area_dmg.
        /// </summary>
        public decimal? Total_area_dmg { get; set; }

        /// <summary>
        /// Gets or Sets Total_cost_dmg.
        /// </summary>
        public decimal? Total_cost_dmg { get; set; }

        /// <summary>
        /// Gets or Sets Perennial_horti_dmg.
        /// </summary>
        public decimal? Perennial_horti_dmg { get; set; }

        /// <summary>
        /// Gets or Sets Perennial_sugarcane_dmg.
        /// </summary>
        public decimal? Perennial_sugarcane_dmg { get; set; }

        /// <summary>
        /// Gets or Sets Grand_total_area_dmg.
        /// </summary>
        public decimal? Grand_total_area_dmg { get; set; }

        /// <summary>
        /// Gets or Sets Grand_total_cost_dmg.
        /// </summary>
        public decimal? Grand_total_cost_dmg { get; set; }

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
        /// Gets or Sets Damage_reason_name.
        /// </summary>
        public string Damage_reason_name { get; set; }

        /// <summary>
        /// Gets or Sets Perennial_horti_cost_dmg.
        /// </summary>
        public decimal? Perennial_horti_cost_dmg { get; set; }

        /// <summary>
        /// Gets or Sets Perennial_sugarcane_cost_dmg.
        /// </summary>
        public decimal? Perennial_sugarcane_cost_dmg { get; set; }
    }
}
