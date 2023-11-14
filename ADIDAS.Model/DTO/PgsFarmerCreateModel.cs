//------------------------------------------------------------------------------
// <copyright file="PgsFarmerDetails.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------
namespace ADIDAS.Model.DTO
{
    using System;

    /// <summary>
    /// Create Pgs Farmer Details.
    /// </summary>
    public class PgsFarmerCreateModel
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
        /// Gets or Sets District_lg_code.
        /// </summary>
        public int? District_lg_code { get; set; }

        /// <summary>
        /// Gets or Sets Block_lg_code.
        /// </summary>
        public int? Block_lg_code { get; set; }

        /// <summary>
        /// Gets or Sets Panchayat_lg_code.
        /// </summary>
        public int? Panchayat_lg_code { get; set; }

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