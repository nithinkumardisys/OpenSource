//------------------------------------------------------------------------------
// <copyright file="CropTgtEntity.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//-----------------------------------------------------------------------------
namespace ADIDAS.Model.Entities
{
    using System;

    /// <summary>
    /// CropTgtEntity.
    /// </summary>
    public class CropTgtEntity
    {
        /// <summary>
        /// Gets or Sets Crop_Id.
        /// </summary>
        public int Crop_Id { get; set; }

        /// <summary>
        /// Gets or Sets Crop_Name.
        /// </summary>
        public string Crop_Name { get; set; }

        /// <summary>
        /// Gets or Sets Area_Target.
        /// </summary>
        public decimal? Area_Target { get; set; }

        /// <summary>
        /// Gets or Sets Crop_category.
        /// </summary>
        public string Crop_category { get; set; }

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
    }
}
