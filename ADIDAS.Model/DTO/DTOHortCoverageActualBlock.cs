//-----------------------------------------------------------------------------------
// <copyright file="DTOHortCoverageActualBlock.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//----------------------------------------------------------------------------------
namespace ADIDAS.Model.DTO
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Hort Coverage Actual Block.
    /// </summary>
    public class DtoHortCoverageActualBlock
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DtoHortCoverageActualBlock"/> class.
        /// </summary>
        public DtoHortCoverageActualBlock()
        {
            this.PanchayatList = new List<DtoHortCoverageActualPanchayat>();
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
        public List<DtoHortCoverageActualPanchayat> PanchayatList { get; set; }
    }

    /// <summary>
    /// Hort Coverage Actual Panchayat.
    /// </summary>
    public class DtoHortCoverageActualPanchayat
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
        /// Gets or Sets Refreshed_date.
        /// </summary>
        public DateTime? Refreshed_date { get; set; }

        /// <summary>
        /// Gets or Sets Refreshed_userid.
        /// </summary>
        public int? Refreshed_userid { get; set; }
    }
}
