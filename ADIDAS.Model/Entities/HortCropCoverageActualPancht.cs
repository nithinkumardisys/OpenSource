//------------------------------------------------------------------------------
// <copyright file="HortCropCoverageActualPancht.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//-----------------------------------------------------------------------------
namespace ADIDAS.Model.Entities
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// HortCropCoverageActualPancht.
    /// </summary>
    public class HortCropCoverageActualPancht
    {
        /// <summary>
        /// Gets or Sets Reported_Date.
        /// </summary>
        public DateTime? Reported_Date { get; set; }

        /// <summary>
        /// Gets or Sets District_Id.
        /// </summary>
        public int District_Id { get; set; }

        /// <summary>
        /// Gets or Sets District_Name.
        /// </summary>
        public string District_Name { get; set; }

        /// <summary>
        /// Gets or Sets Block_Id.
        /// </summary>
        public int Block_Id { get; set; }

        /// <summary>
        /// Gets or Sets Block_Name.
        /// </summary>
        public string Block_Name { get; set; }

        /// <summary>
        /// Gets or Sets Panchayat_Id.
        /// </summary>
        public int Panchayat_Id { get; set; }

        /// <summary>
        /// Gets or Sets Panchayat_Name.
        /// </summary>
        public string Panchayat_Name { get; set; }

        /// <summary>
        /// Gets or Sets Season_Id.
        /// </summary>
        public int Season_Id { get; set; }

        /// <summary>
        /// Gets or Sets Season_Name.
        /// </summary>
        public string Season_Name { get; set; }

        /// <summary>
        /// Gets or Sets CropList.
        /// </summary>
        public List<HortCropCvrgEntity> CropList { get; set; }

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
    }

    /// <summary>
    /// HortProduceActualApproval.
    /// </summary>
    public class HortProduceActualApproval
    {
        /// <summary>
        /// Gets or Sets BlockList.
        /// </summary>
        public List<HortProduceActualBlockApproval> BlockList { get; set; }
    }

    /// <summary>
    /// HortProduceActualBlkApproval.
    /// </summary>
    public class HortProduceActualBlkApproval
    {
        /// <summary>
        /// Gets or Sets Reported_date.
        /// </summary>
        public DateTime Reported_date { get; set; }

        /// <summary>
        /// Gets or Sets Season_id.
        /// </summary>
        public int Season_id { get; set; }

        /// <summary>
        /// Gets or Sets Crop_id.
        /// </summary>
        public int Crop_id { get; set; }

        /// <summary>
        /// Gets or Sets Block_id.
        /// </summary>
        public int Block_id { get; set; }

        /// <summary>
        /// Gets or Sets Block_Name.
        /// </summary>
        public string Block_Name { get; set; }

        /// <summary>
        /// Gets or Sets Production.
        /// </summary>
        public decimal Production { get; set; }

        /// <summary>
        /// Gets or Sets Produce_prev.
        /// </summary>
        public decimal Produce_prev { get; set; }

        /// <summary>
        /// Gets or Sets Produce_curr.
        /// </summary>
        public decimal Produce_curr { get; set; }

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
        public DateTime Refreshed_date { get; set; }

        /// <summary>
        /// Gets or Sets Refreshed_userid.
        /// </summary>
        public int Refreshed_userid { get; set; }

        /// <summary>
        /// Gets or Sets AdH_Approval_flag.
        /// </summary>
        public string AdH_Approval_flag { get; set; }

        /// <summary>
        /// Gets or Sets AdH_Approval_Reason.
        /// </summary>
        public string AdH_Approval_Reason { get; set; }

        /// <summary>
        /// Gets or Sets AdH_Approved_date.
        /// </summary>
        public DateTime AdH_Approved_date { get; set; }

        /// <summary>
        /// Gets or Sets AdH_Approved_userid.
        /// </summary>
        public int AdH_Approved_userid { get; set; }

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
        public List<HortProduceActualPanchayatApproval> PanchayatList { get; set; }
    }

    /// <summary>
    /// HortProduceActualPanchayatApproval.
    /// </summary>
    public class HortProduceActualPanchayatApproval
    {
        /// <summary>
        /// Gets or Sets Reported_date.
        /// </summary>
        public DateTime Reported_date { get; set; }

        /// <summary>
        /// Gets or Sets Season_id.
        /// </summary>
        public int Season_id { get; set; }

        /// <summary>
        /// Gets or Sets Panchayat_Id.
        /// </summary>
        public int Panchayat_Id { get; set; }

        /// <summary>
        /// Gets or Sets Panchayat_Name.
        /// </summary>
        public string Panchayat_Name { get; set; }

        /// <summary>
        /// Gets or Sets Crop_id.
        /// </summary>
        public int Crop_id { get; set; }

        /// <summary>
        /// Gets or Sets Production.
        /// </summary>
        public decimal Production { get; set; }

        /// <summary>
        /// Gets or Sets Produce_prev.
        /// </summary>
        public decimal Produce_prev { get; set; }

        /// <summary>
        /// Gets or Sets Produce_curr.
        /// </summary>
        public decimal Produce_curr { get; set; }

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
        /// Gets or Sets Ac_Submitted_date.
        /// </summary>
        public DateTime Ac_Submitted_date { get; set; }

        /// <summary>
        /// Gets or Sets Ac_Submitted_userid.
        /// </summary>
        public int Ac_Submitted_userid { get; set; }

        /// <summary>
        /// Gets or Sets BhO_Approval_flag.
        /// </summary>
        public string BhO_Approval_flag { get; set; }

        /// <summary>
        /// Gets or Sets BhO_Approval_Reason.
        /// </summary>
        public string BhO_Approval_Reason { get; set; }

        /// <summary>
        /// Gets or Sets BhO_Approved_date.
        /// </summary>
        public DateTime BhO_Approved_date { get; set; }

        /// <summary>
        /// Gets or Sets BhO_Approved_userid.
        /// </summary>
        public int BhO_Approved_userid { get; set; }

        /// <summary>
        /// Gets or Sets AdH_Approval_flag.
        /// </summary>
        public string AdH_Approval_flag { get; set; }

        /// <summary>
        /// Gets or Sets AdH_Approval_Reason.
        /// </summary>
        public string AdH_Approval_Reason { get; set; }

        /// <summary>
        /// Gets or Sets AdH_Approved_date.
        /// </summary>
        public DateTime AdH_Approved_date { get; set; }

        /// <summary>
        /// Gets or Sets AdH_Approved_userid.
        /// </summary>
        public int AdH_Approved_userid { get; set; }

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
        public DateTime Rec_updated_date { get; set; }

        /// <summary>
        /// Gets or Sets Rec_updated_userid.
        /// </summary>
        public int Rec_updated_userid { get; set; }
    }

    /// <summary>
    /// HortProduceActlApproval.
    /// </summary>
    public class HortProduceActlApproval
    {
        /// <summary>
        /// Gets or Sets Reported_date.
        /// </summary>
        public DateTime Reported_date { get; set; }

        /// <summary>
        /// Gets or Sets Season_id.
        /// </summary>
        public int Season_id { get; set; }

        /// <summary>
        /// Gets or Sets Season_name.
        /// </summary>
        public string Season_name { get; set; }

        /// <summary>
        /// Gets or Sets Crop_id.
        /// </summary>
        public int Crop_id { get; set; }

        /// <summary>
        /// Gets or Sets Crop_Category.
        /// </summary>
        public string Crop_Category { get; set; }

        /// <summary>
        /// Gets or Sets Crop_Name.
        /// </summary>
        public string Crop_Name { get; set; }

        /// <summary>
        /// Gets or Sets District_id.
        /// </summary>
        public int District_id { get; set; }

        /// <summary>
        /// Gets or Sets District_Name.
        /// </summary>
        public string District_Name { get; set; }

        /// <summary>
        /// Gets or Sets Production.
        /// </summary>
        public decimal Production { get; set; }

        /// <summary>
        /// Gets or Sets Produce_Curr.
        /// </summary>
        public decimal Produce_Curr { get; set; }

        /// <summary>
        /// Gets or Sets Produce_prev.
        /// </summary>
        public decimal Produce_prev { get; set; }

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
        public DateTime Refreshed_date { get; set; }

        /// <summary>
        /// Gets or Sets Refreshed_userid.
        /// </summary>
        public int Refreshed_userid { get; set; }

        /// <summary>
        /// Gets or Sets BlockList.
        /// </summary>
        public List<HortProduceActualBlkApproval> BlockList { get; set; }
    }

    /// <summary>
    /// HortProduceActPanchayat.
    /// </summary>
    public class HortProduceActPanchayat
    {
        /// <summary>
        /// Gets or Sets Reported_date.
        /// </summary>
        public DateTime Reported_date { get; set; }

        /// <summary>
        /// Gets or Sets Season_id.
        /// </summary>
        public int Season_id { get; set; }

        /// <summary>
        /// Gets or Sets Panchayat_id.
        /// </summary>
        public int Panchayat_id { get; set; }

        /// <summary>
        /// Gets or Sets Crop_id.
        /// </summary>
        public int Crop_id { get; set; }

        /// <summary>
        /// Gets or Sets Produce_prev.
        /// </summary>
        public decimal Produce_prev { get; set; }

        /// <summary>
        /// Gets or Sets Produce_curr.
        /// </summary>
        public decimal Produce_curr { get; set; }

        /// <summary>
        /// Gets or Sets ADH_approval_flag.
        /// </summary>
        public string ADH_approval_flag { get; set; }

        /// <summary>
        /// Gets or Sets ADH_approval_reason.
        /// </summary>
        public string ADH_approval_reason { get; set; }

        /// <summary>
        /// Gets or Sets ADH_approved_userid.
        /// </summary>
        public int ADH_approved_userid { get; set; }

        /// <summary>
        /// Gets or Sets ADH_approved_date.
        /// </summary>
        public DateTime ADH_approved_date { get; set; }

        /// <summary>
        /// Gets or Sets BHO_approval_flag.
        /// </summary>
        public string BHO_approval_flag { get; set; }

        /// <summary>
        /// Gets or Sets BHO_approval_reason.
        /// </summary>
        public string BHO_approval_reason { get; set; }

        /// <summary>
        /// Gets or Sets BHO_approved_userid.
        /// </summary>
        public int BHO_approved_userid { get; set; }

        /// <summary>
        /// Gets or Sets BHO_approved_date.
        /// </summary>
        public DateTime BHO_approved_date { get; set; }

        /// <summary>
        /// Gets or Sets Ac_submitted_userid.
        /// </summary>
        public int Ac_submitted_userid { get; set; }

        /// <summary>
        /// Gets or Sets Ac_submitted_date.
        /// </summary>
        public DateTime Ac_submitted_date { get; set; }

        /// <summary>
        /// Gets or Sets Submission_source.
        /// </summary>
        public string Submission_source { get; set; }

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
        /// Gets or Sets Rec_updated_userid.
        /// </summary>
        public int Rec_updated_userid { get; set; }

        /// <summary>
        /// Gets or Sets Rec_updated_date.
        /// </summary>
        public DateTime Rec_updated_date { get; set; }
    }

    /// <summary>
    /// HortProduceActualBlockApproval.
    /// </summary>
    public class HortProduceActualBlockApproval
    {
        /// <summary>
        /// Gets or Sets Reported_date.
        /// </summary>
        public DateTime Reported_date { get; set; }

        /// <summary>
        /// Gets or Sets Season_Id.
        /// </summary>
        public int Season_Id { get; set; }

        /// <summary>
        /// Gets or Sets Block_Id.
        /// </summary>
        public int Block_Id { get; set; }

        /// <summary>
        /// Gets or Sets Crop_Id.
        /// </summary>
        public int Crop_Id { get; set; }

        /// <summary>
        /// Gets or Sets ADH_approval_flag.
        /// </summary>
        public string ADH_approval_flag { get; set; }

        /// <summary>
        /// Gets or Sets ADH_approval_reason.
        /// </summary>
        public string ADH_approval_reason { get; set; }

        /// <summary>
        /// Gets or Sets ADH_approved_userid.
        /// </summary>
        public int ADH_approved_userid { get; set; }

        /// <summary>
        /// Gets or Sets ADH_approved_date.
        /// </summary>
        public DateTime ADH_approved_date { get; set; }

        /// <summary>
        /// Gets or Sets PanchayatList.
        /// </summary>
        public List<HortProduceActPanchayat> PanchayatList { get; set; }
    }

    /// <summary>
    /// HortProduceActualPanchyt.
    /// </summary>
    public class HortProduceActualPanchyt
    {
        /// <summary>
        /// Gets or Sets Reported_date.
        /// </summary>
        public DateTime Reported_date { get; set; }

        /// <summary>
        /// Gets or Sets Season_id.
        /// </summary>
        public int Season_id { get; set; }

        /// <summary>
        /// Gets or Sets Panchayat_id.
        /// </summary>
        public int Panchayat_id { get; set; }

        /// <summary>
        /// Gets or Sets Crop_id.
        /// </summary>
        public int Crop_id { get; set; }

        /// <summary>
        /// Gets or Sets Produce_prev.
        /// </summary>
        public decimal Produce_prev { get; set; }

        /// <summary>
        /// Gets or Sets Produce_curr.
        /// </summary>
        public decimal Produce_curr { get; set; }

        /// <summary>
        /// Gets or Sets Production.
        /// </summary>
        public decimal Production { get; set; }

        /// <summary>
        /// Gets or Sets ADH_approval_flag.
        /// </summary>
        public string ADH_approval_flag { get; set; }

        /// <summary>
        /// Gets or Sets ADH_approval_reason.
        /// </summary>
        public string ADH_approval_reason { get; set; }

        /// <summary>
        /// Gets or Sets ADH_approved_userid.
        /// </summary>
        public int ADH_approved_userid { get; set; }

        /// <summary>
        /// Gets or Sets ADH_approved_date.
        /// </summary>
        public DateTime ADH_approved_date { get; set; }

        /// <summary>
        /// Gets or Sets BHO_approval_flag.
        /// </summary>
        public string BHO_approval_flag { get; set; }

        /// <summary>
        /// Gets or Sets BHO_approval_reason.
        /// </summary>
        public string BHO_approval_reason { get; set; }

        /// <summary>
        /// Gets or Sets BHO_approved_userid.
        /// </summary>
        public int BHO_approved_userid { get; set; }

        /// <summary>
        /// Gets or Sets BHO_approved_date.
        /// </summary>
        public DateTime BHO_approved_date { get; set; }

        /// <summary>
        /// Gets or Sets Ac_submitted_userid.
        /// </summary>
        public int Ac_submitted_userid { get; set; }

        /// <summary>
        /// Gets or Sets Ac_submitted_date.
        /// </summary>
        public DateTime Ac_submitted_date { get; set; }

        /// <summary>
        /// Gets or Sets Submission_source.
        /// </summary>
        public string Submission_source { get; set; }

        /// <summary>
        /// Gets or Sets Ac_submit_flag.
        /// </summary>
        public string Ac_submit_flag { get; set; }

        /// <summary>
        /// Gets or Sets BHO_add_edit_flag.
        /// </summary>
        public string BHO_add_edit_flag { get; set; }

        /// <summary>
        /// Gets or Sets ADH_add_edit_flag.
        /// </summary>
        public string ADH_add_edit_flag { get; set; }

        /// <summary>
        /// Gets or Sets Final_cvrg_flg.
        /// </summary>
        public string Final_cvrg_flg { get; set; }

        /// <summary>
        /// Gets or Sets Rec_updated_userid.
        /// </summary>
        public string Rec_updated_userid { get; set; }

        /// <summary>
        /// Gets or Sets Rec_updated_date.
        /// </summary>
        public DateTime Rec_updated_date { get; set; }
    }

    /// <summary>
    /// Hort Produce Aggregate Actual.
    /// </summary>
    public class HortProduceAggregateActual
    {
        /// <summary>
        /// Gets or Sets Reported_date.
        /// </summary>
        public DateTime Reported_date { get; set; }

        /// <summary>
        /// Gets or Sets Season_id.
        /// </summary>
        public int Season_id { get; set; }

        /// <summary>
        /// Gets or Sets Panchayat_id.
        /// </summary>
        public int Panchayat_id { get; set; }

        /// <summary>
        /// Gets or Sets Crop_id.
        /// </summary>
        public int Crop_id { get; set; }
    }
}
