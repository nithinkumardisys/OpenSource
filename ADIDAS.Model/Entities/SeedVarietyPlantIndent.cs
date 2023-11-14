//----------------------------------------------------------------------------
// <copyright file="SeedVarietyPlantIndent.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//----------------------------------------------------------------------------
namespace ADIDAS.Model.Entities
{
    /// <summary>
    /// SeedVarietyPlantIndent.
    /// </summary>
    public class SeedVarietyPlantIndent
    {
        /// <summary>
        /// Gets or Sets Seed_variety.
        /// </summary>
        public string Seed_variety { get; set; }

        /// <summary>
        /// Gets or Sets Crop_variety_id.
        /// </summary>
        public int Crop_variety_id { get; set; }

        /// <summary>
        /// Gets or Sets Crop_category.
        /// </summary>
        public string Crop_category { get; set; }

        /// <summary>
        /// Gets or Sets Plant_demand.
        /// </summary>
        public int Plant_demand { get; set; }
    }
}
