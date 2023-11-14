//------------------------------------------------------------------------------
// <copyright file="PgsFarmerDetailsModel.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------
namespace ADIDAS.Model.DTO
{
    using System;

    /// <summary>
    /// Get PGS Farmer Details.
    /// </summary>
    public class PgsFarmerDetailsModel
    {
        /// <summary>
        /// Gets or Sets Group_id.
        /// </summary>
        public int Group_id { get; set; }

        /// <summary>
        /// Gets or Sets Farmer_name.
        /// </summary>
        public string Farmer_name { get; set; }

        /// <summary>
        /// Gets or Sets Farmer_dbt_reg_no.
        /// </summary>
        public long Farmer_dbt_reg_no { get; set; }

        /// <summary>
        /// Gets or Sets Father_hus_name.
        /// </summary>
        public string Father_hus_name { get; set; }

        /// <summary>
        /// Gets or Sets Phone_num.
        /// </summary>
        public string Phone_num { get; set; }

        /// <summary>
        /// Gets or Sets District_name.
        /// </summary>
        public string District_name { get; set; }

        /// <summary>
        /// Gets or Sets Block_name.
        /// </summary>
        public string Block_name { get; set; }

        /// <summary>
        /// Gets or Sets Panchayat_name.
        /// </summary>
        public string Panchayat_name { get; set; }

        /// <summary>
        /// Gets or Sets Village_name.
        /// </summary>
        public string Village_name { get; set; }

        /// <summary>
        /// Gets or Sets Subsidy_date_1.
        /// </summary>
        public DateTime? Subsidy_date_1 { get; set; }

        /// <summary>
        /// Gets or Sets Subsidy_amount_1.
        /// </summary>
        public decimal? Subsidy_amount_1 { get; set; }

        /// <summary>
        /// Gets or Sets Subsidy_date_2.
        /// </summary>
        public DateTime? Subsidy_date_2 { get; set; }

        /// <summary>
        /// Gets or Sets Subsidy_amount_2.
        /// </summary>
        public decimal? Subsidy_amount_2 { get; set; }

        /// <summary>
        /// Gets or Sets Subsidy_date_3.
        /// </summary>
        public DateTime? Subsidy_date_3 { get; set; }

        /// <summary>
        /// Gets or Sets Subsidy_amount_3.
        /// </summary>
        public decimal? Subsidy_amount_3 { get; set; }
    }
}
