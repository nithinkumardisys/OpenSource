//------------------------------------------------------------------------------
// <copyright file="CropDamageGetAll.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------

namespace ADIDAS.Model.DTO
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// CropDamage Get All.
    /// </summary>
    public class CropDamageGetAll
    {
        /// <summary>
        /// Gets or Sets PanchayatId.
        /// </summary>
        public int PanchayatId { get; set; }

        /// <summary>
        /// Gets or Sets DamageReasonId.
        /// </summary>
        public int DamageReasonId { get; set; }

        /// <summary>
        /// Gets or Sets CropDamagePercentage.
        /// </summary>
        public string CropDamagePercentage { get; set; }

        /// <summary>
        /// Gets or Sets NetSownArea.
        /// </summary>
        public decimal? NetSownArea { get; set; }

        /// <summary>
        /// Gets or Sets Ac_Submitted_date.
        /// </summary>
        public DateTime? Ac_Submitted_date { get; set; }

        /// <summary>
        /// Gets or Sets Ac_Submitted_userid.
        /// </summary>
        public int Ac_Submitted_userid { get; set; }

        /// <summary>
        /// Gets or Sets Bao_Approval_flag.
        /// </summary>
        public string Bao_Approval_flag { get; set; }

        /// <summary>
        /// Gets or Sets Bao_Approval_Reason.
        /// </summary>
        public string Bao_Approval_Reason { get; set; }

        /// <summary>
        /// Gets or Sets Bao_Approved_date.
        /// </summary>
        public DateTime? Bao_Approved_date { get; set; }

        /// <summary>
        /// Gets or Sets Bao_Approved_userid.
        /// </summary>
        public int Bao_Approved_userid { get; set; }

        /// <summary>
        /// Gets or Sets Dao_Approval_flag.
        /// </summary>
        public string Dao_Approval_flag { get; set; }

        /// <summary>
        /// Gets or Sets Dao_Approval_Reason.
        /// </summary>
        public string Dao_Approval_Reason { get; set; }

        /// <summary>
        /// Gets or Sets Dao_Approved_date.
        /// </summary>
        public DateTime? Dao_Approved_date { get; set; }

        /// <summary>
        /// Gets or Sets Dao_Approved_userid.
        /// </summary>
        public int Dao_Approved_userid { get; set; }

        /// <summary>
        /// Gets or Sets Ac_submitted_username.
        /// </summary>
        public string Ac_submitted_username { get; set; }

        /// <summary>
        /// Gets or Sets Bao_approved_username.
        /// </summary>
        public string Bao_approved_username { get; set; }

        /// <summary>
        /// Gets or Sets Dao_approved_username.
        /// </summary>
        public string Dao_approved_username { get; set; }

        /// <summary>
        /// Gets or Sets Dm_final_approve.
        /// </summary>
        public string Dm_final_approve { get; set; }

        /// <summary>
        /// Gets or Sets Submission_source.
        /// </summary>
        public string Submission_source { get; set; }

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
        public int Rec_updated_userid { get; set; }

        /// <summary>
        /// Gets or Sets Rec_updated_date.
        /// </summary>
        public DateTime? Rec_updated_date { get; set; }

        /// <summary>
        /// Gets or Sets UpdatedBy.
        /// </summary>
        public string UpdatedBy { get; set; }

        /// <summary>
        /// Gets or Sets coveredArea.
        /// </summary>
        public CoveredArea CoveredArea { get; set; }

        /// <summary>
        /// Gets or Sets AffectedArea.
        /// </summary>
        public AffectedArea AffectedArea { get; set; }

        /// <summary>
        /// Gets or Sets DamageArea.
        /// </summary>
        public DamageArea DamageArea { get; set; }
    }

    /// <summary>
    /// covered Area.
    /// </summary>
    public class CoveredArea
    {
        /// <summary>
        /// Gets or Sets Irrigated_Dmg_Area.
        /// </summary>
        public decimal? Irrigated_Dmg_Area { get; set; }

        /// <summary>
        /// Gets or Sets Non_irrigated_Dmg_Area.
        /// </summary>
        public decimal? Non_irrigated_Dmg_Area { get; set; }

        /// <summary>
        /// Gets or Sets Perennial_horticulture.
        /// </summary>
        public decimal? Perennial_horticulture { get; set; }

        /// <summary>
        /// Gets or Sets Perennial_sugarcane.
        /// </summary>
        public decimal? Perennial_sugarcane { get; set; }

        /// <summary>
        /// Gets or Sets Total.
        /// </summary>
        public decimal? Total { get; set; }

        /// <summary>
        /// Gets or Sets GrandTotal.
        /// </summary>
        public decimal? GrandTotal { get; set; }
    }

    /// <summary>
    /// Affected Area.
    /// </summary>
    public class AffectedArea
    {
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
        /// Gets or Sets CropName.
        /// </summary>
        public List<CropName> CropName { get; set; }
    }

    /// <summary>
    /// cropName.
    /// </summary>
    public class CropName
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
        /// Gets or Sets Crop_value.
        /// </summary>
        public decimal? Crop_value { get; set; }
    }

    /// <summary>
    /// Damage Area.
    /// </summary>
    public class DamageArea
    {
        /// <summary>
        /// Gets or Sets Irrigated_Dmg_Area.
        /// </summary>
        public decimal? Irrigated_Dmg_Area { get; set; }

        /// <summary>
        /// Gets or Sets Irrigated_Dmg_Area_Estimated.
        /// </summary>
        public decimal? Irrigated_Dmg_Area_Estimated { get; set; }

        /// <summary>
        /// Gets or Sets Non_irrigated_Dmg_Area.
        /// </summary>
        public decimal? Non_irrigated_Dmg_Area { get; set; }

        /// <summary>
        /// Gets or Sets Non_irrigated_Dmg_Area_Estimated.
        /// </summary>
        public decimal? Non_irrigated_Dmg_Area_Estimated { get; set; }

        /// <summary>
        /// Gets or Sets Perennial_horticulture.
        /// </summary>
        public decimal? Perennial_horticulture { get; set; }

        /// <summary>
        /// Gets or Sets Perennial_horticulture_Estimated.
        /// </summary>
        public decimal? Perennial_horticulture_Estimated { get; set; }

        /// <summary>
        /// Gets or Sets Perennial_sugarcane.
        /// </summary>
        public decimal? Perennial_sugarcane { get; set; }

        /// <summary>
        /// Gets or Sets Perennial_sugarcane_Estimated.
        /// </summary>
        public decimal? Perennial_sugarcane_Estimated { get; set; }

        /// <summary>
        /// Gets or Sets TotalArea.
        /// </summary>
        public decimal? TotalArea { get; set; }

        /// <summary>
        /// Gets or Sets GrandTotalArea.
        /// </summary>
        public decimal? GrandTotalArea { get; set; }

        /// <summary>
        /// Gets or Sets TotalAmount.
        /// </summary>
        public decimal? TotalAmount { get; set; }

        /// <summary>
        /// Gets or Sets GrandTotalAmount.
        /// </summary>
        public decimal? GrandTotalAmount { get; set; }
    }
}
