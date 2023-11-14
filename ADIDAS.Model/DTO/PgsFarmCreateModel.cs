//------------------------------------------------------------------------------
// <copyright file="PgsFarmCreateModel.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------
namespace ADIDAS.Model.DTO
{
    using System;

    /// <summary>
    /// Create Pgs FarmDetails.
    /// </summary>
    public class PgsFarmCreateModel
    {
        /// <summary>
        /// Gets or Sets Farm_id.
        /// </summary>
        public int Farm_id { get; set; }

        /// <summary>
        /// Gets or Sets Farmer_dbt_reg_no.
        /// </summary>
        public long Farmer_dbt_reg_no { get; set; }

        /// <summary>
        /// Gets or Sets Farm_area.
        /// </summary>
        public int Farm_area { get; set; }

        /// <summary>
        /// Gets or Sets Latitude.
        /// </summary>
        public string Latitude { get; set; }

        /// <summary>
        /// Gets or Sets Langitude.
        /// </summary>
        public string Langitude { get; set; }

        /// <summary>
        /// Gets or Sets Rec_created_userid.
        /// </summary>
        public int Rec_created_userid { get; set; }

        /// <summary>
        /// Gets or Sets Rec_created_date.
        /// </summary>
        public DateTime Rec_created_date { get; set; }

        /// <summary>
        /// Gets or Sets Rec_updated_userid.
        /// </summary>
        public int Rec_updated_userid { get; set; }

        /// <summary>
        /// Gets or Sets Rec_updated_date.
        /// </summary>
        public DateTime Rec_updated_date { get; set; }
    }
}
