//------------------------------------------------------------------------------
// <copyright file="CropDamageImpact.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//-----------------------------------------------------------------------------
namespace ADIDAS.Model.Entities
{
    /// <summary>
    /// CropDamageImpact.
    /// </summary>
    public class CropDamageImpact
    {
        /// <summary>
        /// Gets or Sets Damage_id.
        /// </summary>
        public int Damage_id { get; set; }

        /// <summary>
        /// Gets or Sets Crop_id.
        /// </summary>
        public int Crop_id { get; set; }

        /// <summary>
        /// Gets or Sets Damage_area.
        /// </summary>
        public int Damage_area { get; set; }
    }
}
