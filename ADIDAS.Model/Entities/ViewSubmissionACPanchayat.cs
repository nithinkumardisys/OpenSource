// <copyright file="ViewSubmissionAcPanchayat.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//----------------------------------------------------------------------------
namespace ADIDAS.Model.Entities
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// ViewSubmissionAcPanchayat.
    /// </summary>
    public class ViewSubmissionAcPanchayat
    {
        /// <summary>
        /// ViewSubmissionAcPanchayat.
        /// </summary>
        public ViewSubmissionAcPanchayat()
        {
            this.CoveredAreaobj = new ViewSubmissionCoveredArea();

            this.AffectedAreaobj = new ViewSubmissionAffectedArea();

            this.DamageAreaobj = new ViewSubmissionDamageArea();

            this.DamageValueobj = new ViewSubmissionDamageValue();
        }

        /// <summary>
        /// Gets or Sets Damage_reason_id.
        /// </summary>
        public int? Damage_reason_id { get; set; }

        /// <summary>
        /// Gets or Sets Damage_reason_name.
        /// </summary>
        public string Damage_reason_name { get; set; }

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
        /// Gets or Sets BaO_Approval_Reason.
        /// </summary>
        public string BaO_Approval_Reason { get; set; }

        /// <summary>
        /// Gets or Sets DaO_Approval_Reason.
        /// </summary>
        public string DaO_Approval_Reason { get; set; }

        /// <summary>
        /// Gets or Sets DAO_Approved_date.
        /// </summary>
        public DateTime? DAO_Approved_date { get; set; }

        /// <summary>
        /// Gets or Sets Ac_submitted_username.
        /// </summary>
        public string Ac_submitted_username { get; set; }

        /// <summary>
        /// Gets or Sets Bao_approved_username.
        /// </summary>
        public string Bao_approved_username { get; set; }

        /// <summary>
        /// Gets or Sets Dm_finalapprovalFlag.
        /// </summary>
        public string Dm_finalapprovalFlag { get; set; }

        /// <summary>
        /// Gets or Sets AC_Submitted_userid.
        /// </summary>
        public int? AC_Submitted_userid { get; set; }

        /// <summary>
        /// Gets or Sets DAO_Approved_userid.
        /// </summary>
        public int? DAO_Approved_userid { get; set; }

        /// <summary>
        /// Gets or Sets Dao_approved_username.
        /// </summary>
        public string Dao_approved_username { get; set; }

        /// <summary>
        /// Gets or Sets Ac_submit_flag.
        /// </summary>
        public string Ac_submit_flag { get; set; }

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
        /// Gets or Sets UpdatedBy.
        /// </summary>
        public string UpdatedBy { get; set; }

        /// <summary>
        /// Gets or Sets CoveredAreaobj.
        /// </summary>
        public ViewSubmissionCoveredArea CoveredAreaobj { get; set; }

        /// <summary>
        /// Gets or Sets AffectedAreaobj.
        /// </summary>
        public ViewSubmissionAffectedArea AffectedAreaobj { get; set; }

        /// <summary>
        /// Gets or Sets DamageAreaobj.
        /// </summary>
        public ViewSubmissionDamageArea DamageAreaobj { get; set; }

        /// <summary>
        /// Gets or Sets DamageValueobj.
        /// </summary>
        public ViewSubmissionDamageValue DamageValueobj { get; set; }
    }

    /// <summary>
    /// ViewSubmissionCoveredArea.
    /// </summary>
    public class ViewSubmissionCoveredArea
    {
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
    /// ViewSubmissionAffectedArea.
    /// </summary>
    public class ViewSubmissionAffectedArea
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ViewSubmissionAffectedArea"/> class.
        /// ViewSubmissionAffectedArea.
        /// </summary>
        public ViewSubmissionAffectedArea()
        {
            this.Crops = new List<ViewSubmissionCrops>();
        }

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
        /// Gets or Sets Crops.
        /// </summary>
        public List<ViewSubmissionCrops> Crops { get; set; }
    }

    /// <summary>
    /// ViewSubmissionCrops.
    /// </summary>
    public class ViewSubmissionCrops
    {
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
    /// ViewSubmissionDamageArea.
    /// </summary>
    public class ViewSubmissionDamageArea
    {
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
    }

    /// <summary>
    /// ViewSubmissionDamageValue.
    /// </summary>
    public class ViewSubmissionDamageValue
    {
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
