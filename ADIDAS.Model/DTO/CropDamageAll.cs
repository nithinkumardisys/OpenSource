//------------------------------------------------------------------------------
// <copyright file="CropDamageAll.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------
namespace ADIDAS.Model.DTO
{
    using System.Collections.Generic;

    /// <summary>
    /// Crop Damage All.
    /// </summary>
    public class CropDamageAll
    {
        /// <summary>
        /// Gets or Sets CropDamageAll.
        /// </summary>
        public CropDamageAll()
        {
            this.CropdamageCoverages = new List<CropDamageCoverage>();

            this.CropdamageDetailsModels = new List<CropDamageDetailsModel>();

            this.CropdamageImpactModels = new List<CropDamageImpactModel>();
        }

        /// <summary>
        /// Gets or Sets CropdamageCoverages.
        /// </summary>
        public List<CropDamageCoverage> CropdamageCoverages { get; set; }

        /// <summary>
        /// Gets or Sets CropdamageDetailsModels.
        /// </summary>
        public List<CropDamageDetailsModel> CropdamageDetailsModels { get; set; }

        /// <summary>
        /// Gets or Sets CropdamageImpactModels.
        /// </summary>
        public List<CropDamageImpactModel> CropdamageImpactModels { get; set; }
    }
}
