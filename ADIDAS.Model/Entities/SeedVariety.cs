//----------------------------------------------------------------------------
// <copyright file="SeedVariety.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//----------------------------------------------------------------------------
namespace ADIDAS.Model.Entities
{
    /// <summary>
    /// SeedVariety.
    /// </summary>
    public class SeedVariety
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
        /// Gets or Sets Seed_used_qty.
        /// </summary>
        public decimal Seed_used_qty { get; set; }
    }
}
