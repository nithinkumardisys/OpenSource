//------------------------------------------------------------------------------
// <copyright file="CropDamageDetails.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//-----------------------------------------------------------------------------
namespace ADIDAS.Model.Entities
{
    /// <summary>
    /// Crop Damage Details.
    /// </summary>
    public class CropDamageDetails
    {
        /// <summary>
        /// Gets or Sets Damage_id.
        /// </summary>
        public int Damage_id { get; set; }

        /// <summary>
        /// Gets or Sets Irrigated_area_dmg.
        /// </summary>
        public int Irrigated_area_dmg { get; set; }

        /// <summary>
        /// Gets or Sets Irrigated_cost_dmg.
        /// </summary>
        public int Irrigated_cost_dmg { get; set; }

        /// <summary>
        /// Gets or Sets Nonirrigated_area_dmg.
        /// </summary>
        public int Nonirrigated_area_dmg { get; set; }

        /// <summary>
        /// Gets or Sets Nonirrigated_cost_dmg.
        /// </summary>
        public int Nonirrigated_cost_dmg { get; set; }

        /// <summary>
        /// Gets or Sets Total_area_dmg.
        /// </summary>
        public int Total_area_dmg { get; set; }

        /// <summary>
        /// Gets or Sets Total_cost_dmg.
        /// </summary>
        public int Total_cost_dmg { get; set; }

        /// <summary>
        /// Gets or Sets Perennial_horti_dmg.
        /// </summary>
        public int Perennial_horti_dmg { get; set; }

        /// <summary>
        /// Gets or Sets Perennial_sugarcane_dmg.
        /// </summary>
        public int Perennial_sugarcane_dmg { get; set; }

        /// <summary>
        /// Gets or Sets Grand_total_area_dmg.
        /// </summary>
        public int Grand_total_area_dmg { get; set; }

        /// <summary>
        /// Gets or Sets Grand_total_cost_dmg.
        /// </summary>
        public int Grand_total_cost_dmg { get; set; }
    }
}
