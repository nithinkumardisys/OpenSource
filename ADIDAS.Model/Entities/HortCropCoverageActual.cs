//------------------------------------------------------------------------------
// <copyright file="HortCropCoverageActual.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//-----------------------------------------------------------------------------
namespace ADIDAS.Model.Entities
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// HortCropCoverageActual.
    /// </summary>
    public class HortCropCoverageActual
    {
        /// <summary>
        /// Gets or Sets Reported_date.
        /// </summary>
        public DateTime? Reported_date { get; set; }

        /// <summary>
        /// Gets or Sets Season_Id.
        /// </summary>
        public int Season_Id { get; set; }

        /// <summary>
        /// Gets or Sets Season_Name.
        /// </summary>
        public string Season_Name { get; set; }

        /// <summary>
        /// Gets or Sets Crop_Id.
        /// </summary>
        public int Crop_Id { get; set; }

        /// <summary>
        /// Gets or Sets Crop_Category.
        /// </summary>
        public string Crop_Category { get; set; }

        /// <summary>
        /// Gets or Sets Crop_Name.
        /// </summary>
        public string Crop_Name { get; set; }

        /// <summary>
        /// Gets or Sets District_Id.
        /// </summary>
        public int District_Id { get; set; }

        /// <summary>
        /// Gets or Sets District_Name.
        /// </summary>
        public string District_Name { get; set; }

        /// <summary>
        /// Gets or Sets Area_Target.
        /// </summary>
        public decimal? Area_Target { get; set; }

        /// <summary>
        /// Gets or Sets Cumm_Area_Prev.
        /// </summary>
        public decimal? Cumm_Area_Prev { get; set; }

        /// <summary>
        /// Gets or Sets Cumm_Area_Curr.
        /// </summary>
        public decimal? Cumm_Area_Curr { get; set; }

        /// <summary>
        /// Gets or Sets CreatedBy.
        /// </summary>
        public string CreatedBy { get; set; }

        /// <summary>
        /// Gets or Sets UpdatedBy.
        /// </summary>
        public string UpdatedBy { get; set; }

        /// <summary>
        /// Gets or Sets Refreshed_date.
        /// </summary>
        public DateTime? Refreshed_date { get; set; }

        /// <summary>
        /// Gets or Sets Refreshed_userid.
        /// </summary>
        public int? Refreshed_userid { get; set; }

        /// <summary>
        /// Gets or Sets BlockList.
        /// </summary>
        public List<HortCoverageBlock> BlockList { get; set; }
    }

    /// <summary>
    /// HortCoverageBlock.
    /// </summary>
    public class HortCoverageBlock
    {
        /// <summary>
        /// Gets or Sets Reported_date.
        /// </summary>
        public DateTime? Reported_date { get; set; }

        /// <summary>
        /// Gets or Sets Block_Id.
        /// </summary>
        public int Block_Id { get; set; }

        /// <summary>
        /// Gets or Sets Block_Name.
        /// </summary>
        public string Block_Name { get; set; }

        /// <summary>
        /// Gets or Sets Area_Target.
        /// </summary>
        public decimal? Area_Target { get; set; }

        /// <summary>
        /// Gets or Sets Cumm_Area_Prev.
        /// </summary>
        public decimal? Cumm_Area_Prev { get; set; }

        /// <summary>
        /// Gets or Sets Cumm_Area_Curr.
        /// </summary>
        public decimal? Cumm_Area_Curr { get; set; }

        /// <summary>
        /// Gets or Sets CreatedBy.
        /// </summary>
        public string CreatedBy { get; set; }

        /// <summary>
        /// Gets or Sets UpdatedBy.
        /// </summary>
        public string UpdatedBy { get; set; }

        /// <summary>
        /// Gets or Sets Refreshed_date.
        /// </summary>
        public DateTime? Refreshed_date { get; set; }

        /// <summary>
        /// Gets or Sets Refreshed_userid.
        /// </summary>
        public int? Refreshed_userid { get; set; }

        /// <summary>
        /// Gets or Sets ADH_Approval_flag.
        /// </summary>
        public string ADH_Approval_flag { get; set; }

        /// <summary>
        /// Gets or Sets ADH_Approval_Reason.
        /// </summary>
        public string ADH_Approval_Reason { get; set; }

        /// <summary>
        /// Gets or Sets ADH_Approved_date.
        /// </summary>
        public DateTime? ADH_Approved_date { get; set; }

        /// <summary>
        /// Gets or Sets ADH_Approved_userid.
        /// </summary>
        public int? ADH_Approved_userid { get; set; }

        /// <summary>
        /// Gets or Sets Refreshed_username.
        /// </summary>
        public string Refreshed_username { get; set; }

        /// <summary>
        /// Gets or Sets Adh_approved_username.
        /// </summary>
        public string Adh_approved_username { get; set; }

        /// <summary>
        /// Gets or Sets PanchayatList.
        /// </summary>
        public List<HortCoveragePanchayat> PanchayatList { get; set; }
    }

    /// <summary>
    /// HortCoveragePanchayat.
    /// </summary>
    public class HortCoveragePanchayat
    {
        /// <summary>
        /// Gets or Sets Reported_date.
        /// </summary>
        public DateTime? Reported_date { get; set; }

        /// <summary>
        /// Gets or Sets Panchayat_Id.
        /// </summary>
        public int Panchayat_Id { get; set; }

        /// <summary>
        /// Gets or Sets Panchayat_Name.
        /// </summary>
        public string Panchayat_Name { get; set; }

        /// <summary>
        /// Gets or Sets Area_Target.
        /// </summary>
        public decimal? Area_Target { get; set; }

        /// <summary>
        /// Gets or Sets Cumm_Area_Prev.
        /// </summary>
        public decimal? Cumm_Area_Prev { get; set; }

        /// <summary>
        /// Gets or Sets Cumm_Area_Curr.
        /// </summary>
        public decimal? Cumm_Area_Curr { get; set; }

        /// <summary>
        /// Gets or Sets CreatedBy.
        /// </summary>
        public string CreatedBy { get; set; }

        /// <summary>
        /// Gets or Sets UpdatedBy.
        /// </summary>
        public string UpdatedBy { get; set; }

        /// <summary>
        /// Gets or Sets Submission_source.
        /// </summary>
        public string Submission_source { get; set; }

        /// <summary>
        /// Gets or Sets AC_Submitted_date.
        /// </summary>
        public DateTime? AC_Submitted_date { get; set; }

        /// <summary>
        /// Gets or Sets AC_Submitted_userid.
        /// </summary>
        public int? AC_Submitted_userid { get; set; }

        /// <summary>
        /// Gets or Sets BHO_Approval_flag.
        /// </summary>
        public string BHO_Approval_flag { get; set; }

        /// <summary>
        /// Gets or Sets BHO_Approval_Reason.
        /// </summary>
        public string BHO_Approval_Reason { get; set; }

        /// <summary>
        /// Gets or Sets BHO_Approved_date.
        /// </summary>
        public DateTime? BHO_Approved_date { get; set; }

        /// <summary>
        /// Gets or Sets BHO_Approved_userid.
        /// </summary>
        public int? BHO_Approved_userid { get; set; }

        /// <summary>
        /// Gets or Sets ADH_Approval_flag.
        /// </summary>
        public string ADH_Approval_flag { get; set; }

        /// <summary>
        /// Gets or Sets ADH_Approval_Reason.
        /// </summary>
        public string ADH_Approval_Reason { get; set; }

        /// <summary>
        /// Gets or Sets ADH_Approved_date.
        /// </summary>
        public DateTime? ADH_Approved_date { get; set; }

        /// <summary>
        /// Gets or Sets ADH_Approved_userid.
        /// </summary>
        public int? ADH_Approved_userid { get; set; }

        /// <summary>
        /// Gets or Sets Ac_submitted_username.
        /// </summary>
        public string Ac_submitted_username { get; set; }

        /// <summary>
        /// Gets or Sets Bho_approved_username.
        /// </summary>
        public string Bho_approved_username { get; set; }

        /// <summary>
        /// Gets or Sets Adh_approved_username.
        /// </summary>
        public string Adh_approved_username { get; set; }

        /// <summary>
        /// Gets or Sets Ac_submit_flag.
        /// </summary>
        public string Ac_submit_flag { get; set; }

        /// <summary>
        /// Gets or Sets Bho_add_edit_flag.
        /// </summary>
        public string Bho_add_edit_flag { get; set; }

        /// <summary>
        /// Gets or Sets Adh_add_edit_flag.
        /// </summary>
        public string Adh_add_edit_flag { get; set; }

        /// <summary>
        /// Gets or Sets Final_cvrg_flg.
        /// </summary>
        public string Final_cvrg_flg { get; set; }

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
