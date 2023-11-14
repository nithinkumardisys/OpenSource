//------------------------------------------------------------------------------
// <copyright file="BiaBeneficiaryRecords.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------
namespace ADIDAS.Model.DTO
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// BiaBeneficiaryRecords.
    /// </summary>
    public class BiaBeneficiaryRecords
    {
        /// <summary>
        /// Gets or Sets Application_number.
        /// </summary>
        public string Application_number { get; set; }

        /// <summary>
        /// Gets or Sets Beneficiary_id.
        /// </summary>
        public string Beneficiary_id { get; set; }

        /// <summary>
        /// Gets or Sets Beneficiary_name.
        /// </summary>
        public string Beneficiary_name { get; set; }

        /// <summary>
        /// Gets or Sets Dbt_number.
        /// </summary>
        public string Dbt_number { get; set; }

        /// <summary>
        /// Gets or Sets Scheme.
        /// </summary>
        public string Scheme { get; set; }

        /// <summary>
        /// Gets or Sets Scheme_id.
        /// </summary>
        public int? Scheme_id { get; set; }

        /// <summary>
        /// Gets or Sets District_id.
        /// </summary>
        public int? District_id { get; set; }

        /// <summary>
        /// Gets or Sets District_name.
        /// </summary>
        public string District_name { get; set; }

        /// <summary>
        /// Gets or Sets Block_id.
        /// </summary>
        public int? Block_id { get; set; }

        /// <summary>
        /// Gets or Sets Block_name.
        /// </summary>
        public string Block_name { get; set; }

        /// <summary>
        /// Gets or Sets Panchayat_id.
        /// </summary>
        public int? Panchayat_id { get; set; }

        /// <summary>
        /// Gets or Sets Panchayat_name.
        /// </summary>
        public string Panchayat_name { get; set; }

        /// <summary>
        /// Gets or Sets Directorate_id.
        /// </summary>
        public int? Directorate_id { get; set; }

        /// <summary>
        /// Gets or Sets Directorate_name.
        /// </summary>
        public string Directorate_name { get; set; }

        /// <summary>
        /// Gets or Sets Mobile_number.
        /// </summary>
        public string Mobile_number { get; set; }

        /// <summary>
        /// Gets or Sets Subsidies_value.
        /// </summary>
        public string Subsidies_value { get; set; }

        /// <summary>
        /// Gets or Sets Assign_date.
        /// </summary>
        public DateTime? Assigned_date { get; set; }

        /// <summary>
        /// Gets or Sets Due_date.
        /// </summary>
        public DateTime? Due_date { get; set; }

        /// <summary>
        /// Gets or Sets Verify_date.
        /// </summary>
        public DateTime? Verify_date { get; set; }

        /// <summary>
        /// Gets or Sets Is_Inspection_Verified.
        /// </summary>
        public string Is_Inspection_Verified { get; set; }

        /// <summary>
        /// Gets or Sets Is_Verified.
        /// </summary>
        public string Is_Verified { get; set; }

        /// <summary>
        /// Gets or Sets Current_comments.
        /// </summary>
        public string Current_comments { get; set; }

        /// <summary>
        /// Gets or Sets Assigned_by_name.
        /// </summary>
        public string Assigned_by_name { get; set; }

        /// <summary>
        /// Gets or Sets Assigned_by_designation.
        /// </summary>
        public string Assigned_by_designation { get; set; }

        /// <summary>
        /// Gets or Sets Assigner_User_Id.
        /// </summary>
        public int Assigner_User_Id { get; set; }

        /// <summary>
        /// Gets or Sets Transaction_date.
        /// </summary>
        public string Transaction_date { get; set; }

        /// <summary>
        /// Gets or Sets Previous_Inspection.
        /// </summary>
        public List<BiaPreviousInspection> Previous_Inspection { get; set; }

        /// <summary>
        /// Gets or Sets Beneficiary_type.
        /// </summary>
        public string Beneficiary_type { get; set; }

        /// <summary>
        /// Gets or Sets Status.
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Gets or Sets Financial_Year.
        /// </summary>
        public string Financial_Year { get; set; }

        /// <summary>
        /// Gets or Sets Userid.
        /// </summary>
        public int? Userid { get; set; }

        /// <summary>
        /// Gets or Sets Rec_created_date.
        /// </summary>
        public DateTime Rec_created_date { get; set; }

        /// <summary>
        /// Gets or Sets Rec_updated_date.
        /// </summary>
        public DateTime Rec_updated_date { get; set; }
    }
}
