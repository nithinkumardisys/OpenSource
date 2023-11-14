//------------------------------------------------------------------------------
// <copyright file="CropDetailsPanchayat.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//-----------------------------------------------------------------------------
namespace ADIDAS.Model.Entities
{
    using System.Collections.Generic;

    /// <summary>
    /// CropDetailsPanchayat.
    /// </summary>
    public class CropDetailsPanchayat
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CropDetailsPanchayat"/> class.
        /// CropDetailsPanchayat.
        /// </summary>
        public CropDetailsPanchayat()
        {
            this.Damagedetails = new List<DamageEntity>();
        }

        /// <summary>
        /// Gets or Sets Crop_Id.
        /// </summary>
        public int Crop_Id { get; set; }

        /// <summary>
        /// Gets or Sets Crop_category.
        /// </summary>
        public string Crop_category { get; set; }

        /// <summary>
        /// Gets or Sets Crop_Name.
        /// </summary>
        public string Crop_Name { get; set; }

        /// <summary>
        /// Gets or Sets Area_Covered.
        /// </summary>
        public decimal? Area_Covered { get; set; }

        /// <summary>
        /// Gets or Sets Dmg_Till_Date.
        /// </summary>
        public decimal? Dmg_Till_Date { get; set; }

        /// <summary>
        /// Gets or Sets Damagedetails.
        /// </summary>
        public List<DamageEntity> Damagedetails { get; set; }
    }
}
