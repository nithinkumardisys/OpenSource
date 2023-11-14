//------------------------------------------------------------------------------
// <copyright file="CropCoverageActual.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------

namespace ADIDAS.Model.Entities
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Crop Coverage Actual.
    /// </summary>
    public class CropCoverageActual
    {
        /// <summary>
        /// Gets or Sets Bao_designation.
        /// </summary>
        public DateTime? Reported_date { get; set; }

        /// <summary>
        /// Gets or Sets Bao_designation.
        /// </summary>
        public int Season_Id { get; set; }

        /// <summary>
        /// Gets or Sets Bao_designation.
        /// </summary>
        public string Season_Name { get; set; }

        /// <summary>
        /// Gets or Sets Bao_designation.
        /// </summary>
        public int Crop_Id { get; set; }

        /// <summary>
        /// Gets or Sets Bao_designation.
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
        public List<CoverageBlock> BlockList { get; set; }
    }

    /// <summary>
    /// CoverageBlock.
    /// </summary>
    public class CoverageBlock
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
        /// Gets or Sets Block_Id.
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
        /// Gets or Sets DAO_Approval_flag.
        /// </summary>
        public string DAO_Approval_flag { get; set; }

        /// <summary>
        /// Gets or Sets DAO_Approval_Reason.
        /// </summary>
        public string DAO_Approval_Reason { get; set; }

        /// <summary>
        /// Gets or Sets DAO_Approved_date.
        /// </summary>
        public DateTime? DAO_Approved_date { get; set; }

        /// <summary>
        /// Gets or Sets DAO_Approved_userid.
        /// </summary>
        public int? DAO_Approved_userid { get; set; }

        /// <summary>
        /// Gets or Sets Refreshed_username.
        /// </summary>
        public string Refreshed_username { get; set; }

        /// <summary>
        /// Gets or Sets Dao_approved_username.
        /// </summary>
        public string Dao_approved_username { get; set; }

        /// <summary>
        /// Gets or Sets PanchayatList.
        /// </summary>
        public List<CoveragePanchayat> PanchayatList { get; set; }
    }

    /// <summary>
    /// CoveragePanchayat.
    /// </summary>
    public class CoveragePanchayat
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
        /// Gets or Sets Existing area.
        /// </summary>
        public decimal? Existing_area { get; set; }

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
        /// Gets or Sets BAO_Approval_flag.
        /// </summary>
        public string BAO_Approval_flag { get; set; }

        /// <summary>
        /// Gets or Sets BAO_Approval_Reason.
        /// </summary>
        public string BAO_Approval_Reason { get; set; }

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
        /// Gets or Sets DAO_Approval_Reason.
        /// </summary>
        public string DAO_Approval_Reason { get; set; }

        /// <summary>
        /// Gets or Sets DAO_Approved_date.
        /// </summary>
        public DateTime? DAO_Approved_date { get; set; }

        /// <summary>
        /// Gets or Sets DAO_Approved_userid.
        /// </summary>
        public int? DAO_Approved_userid { get; set; }

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

        /// <summary>
        /// Gets or Sets CoverageVillage.
        /// </summary>
        public List<CoverageVillage> VillageList { get; set; }
    }

    /// <summary>
    /// CoverageVillage.
    /// </summary>
    public class CoverageVillage
    {
        /// <summary>
        /// Gets or Sets Reported_date.
        /// </summary>
        public DateTime? Reported_date { get; set; }

        /// <summary>
        /// Gets or Sets Panchayat_Id.
        /// </summary>
        public int Village_Id { get; set; }

        /// <summary>
        /// Gets or Sets Panchayat_Name.
        /// </summary>
        public string Village_Name { get; set; }

        /// <summary>
        /// Gets or Sets Area_Target.
        /// </summary>
        public decimal? Area_Target { get; set; }

        /// <summary>
        /// Gets or Sets Cumm_Area_Prev.
        /// </summary>
        public decimal? Cumm_Area_Prev { get; set; }

        /// <summary>
        /// Gets or Sets Existing area.
        /// </summary>
        public decimal? Existing_area { get; set; }

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
        /// Gets or Sets BAO_Approval_flag.
        /// </summary>
        public string BAO_Approval_flag { get; set; }

        /// <summary>
        /// Gets or Sets BAO_Approval_Reason.
        /// </summary>
        public string BAO_Approval_Reason { get; set; }

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
        /// Gets or Sets DAO_Approval_Reason.
        /// </summary>
        public string DAO_Approval_Reason { get; set; }

        /// <summary>
        /// Gets or Sets DAO_Approved_date.
        /// </summary>
        public DateTime? DAO_Approved_date { get; set; }

        /// <summary>
        /// Gets or Sets DAO_Approved_userid.
        /// </summary>
        public int? DAO_Approved_userid { get; set; }

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
