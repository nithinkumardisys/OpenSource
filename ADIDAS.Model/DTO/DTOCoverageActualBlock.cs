//------------------------------------------------------------------------------
// <copyright file="DTOCoverageActualBlock.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------
namespace ADIDAS.Model.DTO
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Coverage Actual Block.
    /// </summary>
    public class DtoCoverageActualBlock
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DtoCoverageActualBlock"/> class.
        /// </summary>
        public DtoCoverageActualBlock()
        {
            this.PanchayatList = new List<DtoCoverageActualPanchayat>();
        }

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
        /// Gets or Sets Existing area.
        /// </summary>
        public decimal? Existing_area { get; set; }

        /// <summary>
        /// Gets or Sets Cumm_Area_Curr.
        /// </summary>
        public decimal? Cumm_Area_Curr { get; set; }

        /// <summary>
        /// Gets or Sets Approval_Flag.
        /// </summary>
        public string Approval_Flag { get; set; }

        /// <summary>
        /// Gets or Sets Approval_reason.
        /// </summary>
        public string Approval_reason { get; set; }

        /// <summary>
        /// Gets or Sets Rec_Created_Userid.
        /// </summary>
        public string Rec_Created_Userid { get; set; }

        /// <summary>
        /// Gets or Sets Rec_Created_Date.
        /// </summary>
        public DateTime? Rec_Created_Date { get; set; }

        /// <summary>
        /// Gets or Sets Crop_id.
        /// </summary>
        public int? Crop_id { get; set; }

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
        public List<DtoCoverageActualPanchayat> PanchayatList { get; set; }
    }

    /// <summary>
    /// Coverage Actual Panchayat.
    /// </summary>
    public class DtoCoverageActualPanchayat
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DtoCoverageActualPanchayat"/> class.
        /// </summary>
        public DtoCoverageActualPanchayat()
        {
            this.VillageList = new List<DtoCoverageActualVillage>();
        }

        /// <summary>
        /// Gets or Sets Reported_date.
        /// </summary>
        public DateTime? Reported_date { get; set; }

        /// <summary>
        /// Gets or Sets Block_Id.
        /// </summary>
        public int Block_Id { get; set; }

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
        /// Gets or Sets Approval_Flag.
        /// </summary>
        public string Approval_Flag { get; set; }

        /// <summary>
        /// Gets or Sets Approval_reason.
        /// </summary>
        public string Approval_reason { get; set; }

        /// <summary>
        /// Gets or Sets Rec_Created_Userid.
        /// </summary>
        public string Rec_Created_Userid { get; set; }

        /// <summary>
        /// Gets or Sets Rec_Created_Date.
        /// </summary>
        public DateTime? Rec_Created_Date { get; set; }

        /// <summary>
        /// Gets or Sets Rec_Updated_Userid.
        /// </summary>
        public int? Rec_Updated_Userid { get; set; }

        /// <summary>
        /// Gets or Sets Rec_Updated_Date.
        /// </summary>
        public DateTime? Rec_Updated_Date { get; set; }

        /// <summary>
        /// Gets or Sets UpdatedBy.
        /// </summary>
        public string UpdatedBy { get; set; }

        /// <summary>
        /// Gets or Sets Crop_id.
        /// </summary>
        public int? Crop_id { get; set; }

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
        /// Gets or Sets Refreshed_date.
        /// </summary>
        public DateTime? Refreshed_date { get; set; }

        /// <summary>
        /// Gets or Sets Refreshed_userid.
        /// </summary>
        public int? Refreshed_userid { get; set; }

        /// <summary>
        /// Gets or Sets VillageList.
        /// </summary>
        public List<DtoCoverageActualVillage> VillageList { get; set; }
    }

    /// <summary>
    /// Coverage Actual Village.
    /// </summary>
    public class DtoCoverageActualVillage
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
        /// Gets or Sets Village_Id.
        /// </summary>
        public int Village_Id { get; set; }

        /// <summary>
        /// Gets or Sets Village_Name.
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
        /// Gets or Sets Approval_Flag.
        /// </summary>
        public string Approval_Flag { get; set; }

        /// <summary>
        /// Gets or Sets Approval_reason.
        /// </summary>
        public string Approval_reason { get; set; }

        /// <summary>
        /// Gets or Sets Rec_Created_Userid.
        /// </summary>
        public string Rec_Created_Userid { get; set; }

        /// <summary>
        /// Gets or Sets Rec_Created_Date.
        /// </summary>
        public DateTime? Rec_Created_Date { get; set; }

        /// <summary>
        /// Gets or Sets Rec_Updated_Userid.
        /// </summary>
        public int? Rec_Updated_Userid { get; set; }

        /// <summary>
        /// Gets or Sets Rec_Updated_Date.
        /// </summary>
        public DateTime? Rec_Updated_Date { get; set; }

        /// <summary>
        /// Gets or Sets UpdatedBy.
        /// </summary>
        public string UpdatedBy { get; set; }

        /// <summary>
        /// Gets or Sets Crop_id.
        /// </summary>
        public int? Crop_id { get; set; }

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
        /// Gets or Sets Refreshed_date.
        /// </summary>
        public DateTime? Refreshed_date { get; set; }

        /// <summary>
        /// Gets or Sets Refreshed_userid.
        /// </summary>
        public int? Refreshed_userid { get; set; }
    }
}
