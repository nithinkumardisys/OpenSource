//------------------------------------------------------------------------------
// <copyright file="DTOSeedVariety.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------

namespace ADIDAS.Model.DTO
{
    using System;

    /// <summary>
    /// Seed Variety.
    /// </summary>
    public class DtoSeedVariety
    {
        /// <summary>
        /// Gets or Sets Seed_Id.
        /// </summary>
        public int Seed_Id { get; set; }

        /// <summary>
        /// Gets or Sets District_id.
        /// </summary>
        public int District_id { get; set; }

        /// <summary>
        /// Gets or Sets Seed_variety.
        /// </summary>
        public string Seed_variety { get; set; }

        /// <summary>
        /// Gets or Sets Crop_category.
        /// </summary>
        public string Crop_category { get; set; }

        /// <summary>
        /// Gets or Sets Crop_variety_id.
        /// </summary>
        public int Crop_variety_id { get; set; }

        /// <summary>
        /// Gets or Sets Seed_used_qty.
        /// </summary>
        public decimal Seed_used_qty { get; set; }

        /// <summary>
        /// Gets or Sets Variety_flg.
        /// </summary>
        public string Variety_flg { get; set; }

        /// <summary>
        /// Gets or Sets Rec_created_date.
        /// </summary>
        public DateTime? Rec_created_date { get; set; }

        /// <summary>
        /// Gets or Sets Rec_created_userid.
        /// </summary>
        public int? Rec_created_userid { get; set; }

        /// <summary>
        /// Gets or Sets Rec_updated_date.
        /// </summary>
        public DateTime? Rec_updated_date { get; set; }

        /// <summary>
        /// Gets or Sets Rec_updated_userid.
        /// </summary>
        public int? Rec_updated_userid { get; set; }
    }
}
