//----------------------------------------------------------------------------
// <copyright file="PlantIndentInput.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//----------------------------------------------------------------------------
namespace ADIDAS.Model.Entities
{
    using System.Collections.Generic;

    /// <summary>
    /// PlantIndentInput.
    /// </summary>
    public class PlantIndentInput
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PlantIndentInput"/> class.
        /// </summary>
        public PlantIndentInput()
        {
            this.SeedVariety = new List<SeedVarietyPlantIndent>();
        }

        /// <summary>
        /// Gets or Sets Season_id.
        /// </summary>
        public int Season_id { get; set; }

        /// <summary>
        /// Gets or Sets Crop_id.
        /// </summary>
        public int Crop_id { get; set; }

        /// <summary>
        /// Gets or Sets Block_id.
        /// </summary>
        public int Block_id { get; set; }

        /// <summary>
        /// Gets or Sets District_id.
        /// </summary>
        public int District_id { get; set; }

        /// <summary>
        /// Gets or Sets Panchayat_id.
        /// </summary>
        public int Panchayat_id { get; set; }

        /// <summary>
        /// Gets or Sets UserId.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or Sets SeedVariety.
        /// </summary>
        public List<SeedVarietyPlantIndent> SeedVariety { get; set; }
    }
}
