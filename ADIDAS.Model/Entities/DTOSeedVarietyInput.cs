//------------------------------------------------------------------------------
// <copyright file="DTOSeedVarietyInput.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//-----------------------------------------------------------------------------
namespace ADIDAS.Model.Entities
{
    using System;

    /// <summary>
    /// DtoSeedVarietyInput.
    /// </summary>
    public class DtoSeedVarietyInput
    {
        /// <summary>
        /// Gets or Sets Season_id.
        /// </summary>
        public int Season_id { get; set; }

        /// <summary>
        /// Gets or Sets Crop_id.
        /// </summary>
        public int Crop_id { get; set; }

        /// <summary>
        /// Gets or Sets Seed_Id.
        /// </summary>
        public int Seed_Id { get; set; }

        /// <summary>
        /// Gets or Sets Seed_variety.
        /// </summary>
        public string Seed_variety { get; set; }

        /// <summary>
        /// Gets or Sets Crop_Variety_ID.
        /// </summary>
        public int Crop_Variety_ID { get; set; }

        /// <summary>
        /// Gets or Sets Seed_used_qty.
        /// </summary>
        public decimal Seed_used_qty { get; set; }

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
