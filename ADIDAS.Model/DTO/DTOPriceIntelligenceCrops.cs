//------------------------------------------------------------------------------
// <copyright file="DTOPriceIntelligenceCrops.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------
namespace ADIDAS.Model.DTO
{
    /// <summary>
    /// Price Intelligence Crops.
    /// </summary>
    public class DtoPriceIntelligenceCrops
    {
        /// <summary>
        /// Gets or Sets Crop_id.
        /// </summary>
        public int Crop_id { get; set; }

        /// <summary>
        /// Gets or Sets District_id.
        /// </summary>
        public int District_id { get; set; }

        /// <summary>
        /// Gets or Sets Crop_name.
        /// </summary>
        public string Crop_name { get; set; }

        /// <summary>
        /// Gets or Sets Crop_category.
        /// </summary>
        public string Crop_category { get; set; }

        /// <summary>
        /// Gets or Sets Crop_type.
        /// </summary>
        public string Crop_type { get; set; }

        /// <summary>
        /// Gets or Sets District_name.
        /// </summary>
        public string District_name { get; set; }
    }
}
