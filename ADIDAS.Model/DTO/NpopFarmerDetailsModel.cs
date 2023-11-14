//------------------------------------------------------------------------------
// <copyright file="NpopFarmerDetailsModel.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------
namespace ADIDAS.Model.DTO
{
    using System;

    /// <summary>
    /// Get NPOP Farmer Details.
    /// </summary>
    public class NpopFarmerDetailsModel
    {
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
        /// Gets or Sets Is_belongs_to_FPO.
        /// </summary>
        public string Is_belongs_to_FPO { get; set; }

        /// <summary>
        /// Gets or Sets Fpo_id.
        /// </summary>
        public int Fpo_id { get; set; }

        /// <summary>
        /// Gets or Sets Fpo_name.
        /// </summary>
        public string Fpo_name { get; set; }

        /// <summary>
        /// Gets or Sets Fpo_address.
        /// </summary>
        public string Fpo_address { get; set; }

        /// <summary>
        /// Gets or Sets Fpo_contact_person.
        /// </summary>
        public string Fpo_contact_person { get; set; }

        /// <summary>
        /// Gets or Sets Fpo_phone_num.
        /// </summary>
        public string Fpo_phone_num { get; set; }

        /// <summary>
        /// Gets or Sets Scheme_id.
        /// </summary>
        public int Scheme_id { get; set; }

        /// <summary>
        /// Gets or Sets Scheme_name.
        /// </summary>
        public string Scheme_name { get; set; }

        /// <summary>
        /// Gets or Sets Is_facility_added.
        /// </summary>
        public string Is_facility_added { get; set; }

        /// <summary>
        /// Gets or Sets Major_town_name.
        /// </summary>
        public string Major_town_name { get; set; }

        /// <summary>
        /// Gets or Sets Major_crop_name.
        /// </summary>
        public string Major_crop_name { get; set; }

        /// <summary>
        /// Gets or Sets Subsidy_date_1.
        /// </summary>
        public DateTime? Subsidy_date_1 { get; set; }

        /// <summary>
        /// Gets or Sets Subsidy_amount_1.
        /// </summary>
        public Decimal? Subsidy_amount_1 { get; set; }

        /// <summary>
        /// Gets or Sets Subsidy_date_2.
        /// </summary>
        public DateTime? Subsidy_date_2 { get; set; }

        /// <summary>
        /// Gets or Sets Subsidy_amount_2.
        /// </summary>
        public Decimal? Subsidy_amount_2 { get; set; }

        /// <summary>
        /// Gets or Sets Subsidy_date_3.
        /// </summary>
        public DateTime? Subsidy_date_3 { get; set; }

        /// <summary>
        /// Gets or Sets Subsidy_amount_3.
        /// </summary>
        public Decimal? Subsidy_amount_3 { get; set; }

        /// <summary>
        /// Gets or Sets Certification.
        /// </summary>
        public string Certification { get; set; }
    }
}
