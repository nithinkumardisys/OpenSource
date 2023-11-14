//------------------------------------------------------------------------------
// <copyright file="DtoSeedVarietyIndent.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------

namespace ADIDAS.Model.DTO
{
    using System;

    /// <summary>
    /// DtoSeedVarietyIndent.
    /// </summary>
    public class DtoSeedVarietyIndent
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
        /// Gets or Sets Seed_indent_id.
        /// </summary>
        public int Seed_indent_id { get; set; }

        /// <summary>
        /// Gets or Sets Seed_used_qty.
        /// </summary>
        public decimal? Seed_demand { get; set; }

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
