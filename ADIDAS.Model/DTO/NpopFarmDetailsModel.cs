//------------------------------------------------------------------------------
// <copyright file="NpopFarmDetailsModel.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------
namespace ADIDAS.Model.DTO
{
    /// <summary>
    /// Get Npop Farm Details.
    /// </summary>
    public class NpopFarmDetailsModel
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
    }
}
