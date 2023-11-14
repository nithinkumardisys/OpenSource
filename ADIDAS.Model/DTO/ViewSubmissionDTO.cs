//------------------------------------------------------------------------------
// <copyright file="ViewSubmissionDTO.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------
namespace ADIDAS.Model.DTO
{
    using System;

    /// <summary>
    /// Dto View Submission AcPanchayat.
    /// </summary>
    public class DtoViewSubmissionAcPanchayat
    {
        /// <summary>
        /// Gets or Sets Damage_reason_id.
        /// </summary>
        public int? Damage_reason_id { get; set; }

        /// <summary>
        /// Gets or Sets Damage_reason_name.
        /// </summary>
        public string Damage_reason_name { get; set; }

        /// <summary>
        /// Gets or Sets Damage_id.
        /// </summary>
        public int? Damage_id { get; set; }

        /// <summary>
        /// Gets or Sets Panchayat_id.
        /// </summary>
        public int? Panchayat_id { get; set; }

        /// <summary>
        /// Gets or Sets Panchayat_name.
        /// </summary>
        public string Panchayat_name { get; set; }

        /// <summary>
        /// Gets or Sets AC_Submitted_date.
        /// </summary>
        public DateTime? AC_Submitted_date { get; set; }

        /// <summary>
        /// Gets or Sets BAO_Approval_flag.
        /// </summary>
        public string BAO_Approval_flag { get; set; }

        /// <summary>
        /// Gets or Sets BAO_Approved_date.
        /// </summary>
        public DateTime? BAO_Approved_date { get; set; }

        /// <summary>
        /// Gets or Sets BAO_Approved_userid.
        /// </summary>
        public int? BAO_Approved_userid { get; set; }

        /// <summary>
        /// Gets or Sets DAO_Approval_flag.
        /// </summary>
        public string DAO_Approval_flag { get; set; }

        /// <summary>
        /// Gets or Sets DM_Final_Approval_Flag.
        /// </summary>
        public string DM_Final_Approval_Flag { get; set; }

        /// <summary>
        /// Gets or Sets DAO_Approved_date.
        /// </summary>
        public DateTime? DAO_Approved_date { get; set; }

        /// <summary>
        /// Gets or Sets Bao_comments.
        /// </summary>
        public string Bao_comments { get; set; }

        /// <summary>
        /// Gets or Sets Dao_comments.
        /// </summary>
        public string Dao_comments { get; set; }

        /// <summary>
        /// Gets or Sets Ac_submitted_username.
        /// </summary>
        public string Ac_submitted_username { get; set; }

        /// <summary>
        /// Gets or Sets Bao_submitted_username.
        /// </summary>
        public string Bao_submitted_username { get; set; }

        /// <summary>
        /// Gets or Sets Dao_submitted_username.
        /// </summary>
        public string Dao_submitted_username { get; set; }

        /// <summary>
        /// Gets or Sets Ac_submit_flag.
        /// </summary>
        public string Ac_submit_flag { get; set; }

        /// <summary>
        /// Gets or Sets AC_Submitted_userid.
        /// </summary>
        public int? AC_Submitted_userid { get; set; }

        /// <summary>
        /// Gets or Sets DAO_Approved_userid.
        /// </summary>
        public int? DAO_Approved_userid { get; set; }

        /// <summary>
        /// Gets or Sets Bao_add_edit_flag.
        /// </summary>
        public string Bao_add_edit_flag { get; set; }

        /// <summary>
        /// Gets or Sets Dao_add_edit_flag.
        /// </summary>
        public string Dao_add_edit_flag { get; set; }

        /// <summary>
        /// Gets or Sets Rec_updated_userid.
        /// </summary>
        public int? Rec_updated_userid { get; set; }

        /// <summary>
        /// Gets or Sets Rec_updated_date.
        /// </summary>
        public DateTime? Rec_updated_date { get; set; }

        /// <summary>
        /// Gets or Sets Rec_updated_username.
        /// </summary>
        public string Rec_updated_username { get; set; }

        /// <summary>
        /// Gets or Sets Irrigated_area.
        /// </summary>
        public decimal? Irrigated_area { get; set; }

        /// <summary>
        /// Gets or Sets Nonirrigated_area.
        /// </summary>
        public decimal? Nonirrigated_area { get; set; }

        /// <summary>
        /// Gets or Sets Total_area.
        /// </summary>
        public decimal? Total_area { get; set; }

        /// <summary>
        /// Gets or Sets Grand_total_area.
        /// </summary>
        public decimal? Grand_total_area { get; set; }

        /// <summary>
        /// Gets or Sets Perennial_horti.
        /// </summary>
        public decimal? Perennial_horti { get; set; }

        /// <summary>
        /// Gets or Sets Perennial_sugarcane.
        /// </summary>
        public decimal? Perennial_sugarcane { get; set; }
    }

    /// <summary>
    /// Dto View Submission Affected Area.
    /// </summary>
    public class DtoViewSubmissionAffectedArea
    {
        /// <summary>
        /// Gets or Sets Damage_reason_id.
        /// </summary>
        public int? Damage_reason_id { get; set; }

        /// <summary>
        /// Gets or Sets Panchayat_id.
        /// </summary>
        public int? Panchayat_id { get; set; }

        /// <summary>
        /// Gets or Sets Damage_reason_name.
        /// </summary>
        public string Damage_reason_name { get; set; }

        /// <summary>
        /// Gets or Sets Perennial_horti_impact.
        /// </summary>
        public decimal? Perennial_horti_impact { get; set; }

        /// <summary>
        /// Gets or Sets Perennial_sugarcane_impact.
        /// </summary>
        public decimal? Perennial_sugarcane_impact { get; set; }

        /// <summary>
        /// Gets or Sets Total_impact_area.
        /// </summary>
        public decimal? Total_impact_area { get; set; }

        /// <summary>
        /// Gets or Sets Grand_total_impact.
        /// </summary>
        public decimal? Grand_total_impact { get; set; }

        /// <summary>
        /// Gets or Sets Crop_id.
        /// </summary>
        public int Crop_id { get; set; }

        /// <summary>
        /// Gets or Sets Crop_name.
        /// </summary>
        public string Crop_name { get; set; }

        /// <summary>
        /// Gets or Sets Damage_area.
        /// </summary>
        public decimal? Damage_area { get; set; }
    }

    /// <summary>
    /// Dto ViewSubmission Damage Area.
    /// </summary>
    public class DtoViewSubmissionDamageArea
    {
        /// <summary>
        /// Gets or Sets Damage_reason_id.
        /// </summary>
        public int? Damage_reason_id { get; set; }

        /// <summary>
        /// Gets or Sets Irrigated_area_dmg.
        /// </summary>
        public decimal? Irrigated_area_dmg { get; set; }

        /// <summary>
        /// Gets or Sets Nonirrigated_area_dmg.
        /// </summary>
        public decimal? Nonirrigated_area_dmg { get; set; }

        /// <summary>
        /// Gets or Sets Total_area_dmg.
        /// </summary>
        public decimal? Total_area_dmg { get; set; }

        /// <summary>
        /// Gets or Sets Grand_total_area_dmg.
        /// </summary>
        public decimal? Grand_total_area_dmg { get; set; }

        /// <summary>
        /// Gets or Sets Perennial_horti_dmg.
        /// </summary>
        public decimal? Perennial_horti_dmg { get; set; }

        /// <summary>
        /// Gets or Sets Perennial_sugarcane_dmg.
        /// </summary>
        public decimal? Perennial_sugarcane_dmg { get; set; }

        /// <summary>
        /// Gets or Sets Irrigated_cost_dmg.
        /// </summary>
        public decimal? Irrigated_cost_dmg { get; set; }

        /// <summary>
        /// Gets or Sets Nonirrigated_cost_dmg.
        /// </summary>
        public decimal? Nonirrigated_cost_dmg { get; set; }

        /// <summary>
        /// Gets or Sets Total_cost_dmg.
        /// </summary>
        public decimal? Total_cost_dmg { get; set; }

        /// <summary>
        /// Gets or Sets Grand_total_cost_dmg.
        /// </summary>
        public decimal? Grand_total_cost_dmg { get; set; }

        /// <summary>
        /// Gets or Sets Perennial_horti_cost_dmg.
        /// </summary>
        public decimal? Perennial_horti_cost_dmg { get; set; }

        /// <summary>
        /// Gets or Sets Perennial_sugarcane_cost_dmg.
        /// </summary>
        public decimal? Perennial_sugarcane_cost_dmg { get; set; }
    }
}
