//------------------------------------------------------------------------------
// <copyright file="HortCropTgtEntity.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------

namespace ADIDAS.Model.DTO
{
    using System;

    /// <summary>
    /// Hort CropTarget Entity.
    /// </summary>
    public class HortCropTgtEntity
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
        /// Gets or Sets Ac_submitted_date.
        /// </summary>
        public DateTime? Ac_submitted_date { get; set; }

        /// <summary>
        /// Gets or Sets Ac_submitted_userid.
        /// </summary>
        public int? Ac_submitted_userid { get; set; }

        /// <summary>
        /// Gets or Sets Bho_approval_flag.
        /// </summary>
        public string Bho_approval_flag { get; set; }

        /// <summary>
        /// Gets or Sets Bho_approval_reason.
        /// </summary>
        public string Bho_approval_reason { get; set; }

        /// <summary>
        /// Gets or Sets Bho_approved_date.
        /// </summary>
        public DateTime? Bho_approved_date { get; set; }

        /// <summary>
        /// Gets or Sets Bho_approved_userid.
        /// </summary>
        public int? Bho_approved_userid { get; set; }

        /// <summary>
        /// Gets or Sets Adh_approval_flag.
        /// </summary>
        public string Adh_approval_flag { get; set; }

        /// <summary>
        /// Gets or Sets Adh_approval_reason.
        /// </summary>
        public string Adh_approval_reason { get; set; }

        /// <summary>
        /// Gets or Sets Adh_approved_date.
        /// </summary>
        public DateTime? Adh_approved_date { get; set; }

        /// <summary>
        /// Gets or Sets Adh_approved_userid.
        /// </summary>
        public int? Adh_approved_userid { get; set; }

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
        /// Gets or Sets Bho_add_edit_flag.
        /// </summary>
        public string Bho_add_edit_flag { get; set; }

        /// <summary>
        /// Gets or Sets Adh_add_edit_flag.
        /// </summary>
        public string Adh_add_edit_flag { get; set; }
    }
}
