//------------------------------------------------------------------------------
// <copyright file="PHMSDetails.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------
namespace ADIDAS.Model.DTO
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// PhmsDetails.
    /// </summary>
    public class PhmsDetails
    {
        /// <summary>
        /// Gets or Sets Structure_id.
        /// </summary>
        public int Structure_id { get; set; }

        /// <summary>
        /// Gets or Sets District_id.
        /// </summary>
        public int District_id { get; set; }

        /// <summary>
        /// Gets or Sets Panchayat_id.
        /// </summary>
        public int Panchayat_id { get; set; }

        /// <summary>
        /// Gets or Sets Is_Facility_Added.
        /// </summary>
        public string Is_Facility_Added { get; set; }

        /// <summary>
        /// Gets or Sets Period.
        /// </summary>
        public string Period { get; set; }

        /// <summary>
        /// Gets or Sets Rec_created_userid.
        /// </summary>
        public int Rec_created_userid { get; set; }

        /// <summary>
        /// Gets or Sets Rec_updated_userid.
        /// </summary>
        public int Rec_updated_userid { get; set; }

        /// <summary>
        /// Gets or Sets Facility_list.
        /// </summary>
        public List<Facility> Facility_list { get; set; }
    }

    /// <summary>
    /// Facility.
    /// </summary>
    public class Facility
    {
        /// <summary>
        /// Gets or Sets Facility_id.
        /// </summary>
        public int Facility_id { get; set; }

        /// <summary>
        /// Gets or Sets Facility_name.
        /// </summary>
        public string Facility_name { get; set; }

        /// <summary>
        /// Gets or Sets Facility_address.
        /// </summary>
        public string Facility_address { get; set; }

        /// <summary>
        /// Gets or Sets Capacity.
        /// </summary>
        public string Capacity { get; set; }

        /// <summary>
        /// Gets or Sets Period.
        /// </summary>
        public string Period { get; set; }

        /// <summary>
        /// Gets or Sets Rec_created_userid.
        /// </summary>
        public int Rec_created_userid { get; set; }

        /// <summary>
        /// Gets or Sets Rec_updated_userid.
        /// </summary>
        public int Rec_updated_userid { get; set; }
    }

    /// <summary>
    /// Panchayat Details.
    /// </summary>
    public class PanchayatDetails
    {
        /// <summary>
        /// Gets or Sets Panchayat_id.
        /// </summary>
        public int Panchayat_id { get; set; }

        /// <summary>
        /// Gets or Sets Panchayat_name.
        /// </summary>
        public string Panchayat_name { get; set; }

        /// <summary>
        /// Gets or Sets User_id.
        /// </summary>
        public int User_id { get; set; }
    }

    /// <summary>
    /// Structure Details.
    /// </summary>
    public class StructureDetails
    {
        /// <summary>
        /// Gets or Sets Facility_id.
        /// </summary>
        public int Facility_id { get; set; }

        /// <summary>
        /// Gets or Sets Facility_name.
        /// </summary>
        public string Facility_name { get; set; }

        /// <summary>
        /// Gets or Sets Facility_add.
        /// </summary>
        public string Facility_add { get; set; }

        /// <summary>
        /// Gets or Sets Panchayat_id.
        /// </summary>
        public int Panchayat_id { get; set; }

        /// <summary>
        /// Gets or Sets Panchayat_name.
        /// </summary>
        public string Panchayat_name { get; set; }

        /// <summary>
        /// Gets or Sets Capacity.
        /// </summary>
        public string Capacity { get; set; }

        /// <summary>
        /// Gets or Sets Struct_id.
        /// </summary>
        public int Struct_id { get; set; }

        /// <summary>
        /// Gets or Sets Rec_created_ts.
        /// </summary>
        public DateTime? Rec_created_ts { get; set; }

        /// <summary>
        /// Gets or Sets Rec_created_userid.
        /// </summary>
        public int Rec_created_userid { get; set; }

        /// <summary>
        /// Gets or Sets Rec_updated_ts.
        /// </summary>
        public DateTime? Rec_updated_ts { get; set; }

        /// <summary>
        /// Gets or Sets Rec_updated_userid.
        /// </summary>
        public int Rec_updated_userid { get; set; }

        /// <summary>
        /// Gets or Sets Is_Facility_Added.
        /// </summary>
        public string Is_Facility_Added { get; set; }

        /// <summary>
        /// Gets or Sets Period.
        /// </summary>
        public string Period { get; set; }

        /// <summary>
        /// Gets or Sets Struct_type.
        /// </summary>
        public string Struct_type { get; set; }
    }

    /// <summary>
    /// Facility Online.
    /// </summary>
    public class FacilityOnline
    {
        /// <summary>
        /// Gets or Sets Structure_id.
        /// </summary>
        public int Structure_id { get; set; }

        /// <summary>
        /// Gets or Sets Structure_name.
        /// </summary>
        public string Structure_name { get; set; }

        /// <summary>
        /// Gets or Sets Panchayat_id.
        /// </summary>
        public int Panchayat_id { get; set; }

        /// <summary>
        /// Gets or Sets Panchayat_name.
        /// </summary>
        public string Panchayat_name { get; set; }

        /// <summary>
        /// Gets or Sets Is_Facility_Added.
        /// </summary>
        public string Is_Facility_Added { get; set; }

        /// <summary>
        /// Gets or Sets Facility_list.
        /// </summary>
        public List<Facility> Facility_list { get; set; }
    }

    /// <summary>
    /// No Facility.
    /// </summary>
    public class NoFacility
    {
        /// <summary>
        /// Gets or Sets District_id.
        /// </summary>
        public int District_id { get; set; }

        /// <summary>
        /// Gets or Sets Is_Facility_Added.
        /// </summary>
        public string Is_Facility_Added { get; set; }

        /// <summary>
        /// Gets or Sets Period.
        /// </summary>
        public string Period { get; set; }
    }

    /// <summary>
    /// Horticulture Crop Coverage Target Panchayat.
    /// </summary>
    public class HortCropCoverageTargetPanchayat
    {
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
        /// Gets or Sets Area_target.
        /// </summary>
        public decimal Area_target { get; set; }

        /// <summary>
        /// Gets or Sets Adh_approval_flag.
        /// </summary>
        public string Adh_approval_flag { get; set; }

        /// <summary>
        /// Gets or Sets Adh_approval_reason.
        /// </summary>
        public string Adh_approval_reason { get; set; }

        /// <summary>
        /// Gets or Sets Adh_approved_userid.
        /// </summary>
        public int Adh_approved_userid { get; set; }

        /// <summary>
        /// Gets or Sets Adh_approved_date.
        /// </summary>
        public DateTime Adh_approved_date { get; set; }

        /// <summary>
        /// Gets or Sets Bho_approval_flag.
        /// </summary>
        public string Bho_approval_flag { get; set; }

        /// <summary>
        /// Gets or Sets Bho_approval_reason.
        /// </summary>
        public string Bho_approval_reason { get; set; }

        /// <summary>
        /// Gets or Sets Bho_approved_userid.
        /// </summary>
        public int Bho_approved_userid { get; set; }

        /// <summary>
        /// Gets or Sets Bho_approved_date.
        /// </summary>
        public DateTime Bho_approved_date { get; set; }

        /// <summary>
        /// Gets or Sets Ac_submitted_userid.
        /// </summary>
        public int Ac_submitted_userid { get; set; }

        /// <summary>
        /// Gets or Sets Ac_submitted_date.
        /// </summary>
        public DateTime Ac_submitted_date { get; set; }

        /// <summary>
        /// Gets or Sets Rec_updated_userid.
        /// </summary>
        public int? Rec_updated_userid { get; set; }

        /// <summary>
        /// Gets or Sets Rec_updated_date.
        /// </summary>
        public DateTime? Rec_updated_date { get; set; }

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
    }

    /// <summary>
    /// Horticulture Crop Coverage Target Panchayat Approval.
    /// </summary>
    public class HortCropCoverageTargetPanchayatApproval
    {
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
        /// Gets or Sets Area_target.
        /// </summary>
        public decimal Area_target { get; set; }

        /// <summary>
        /// Gets or Sets Adh_approval_flag.
        /// </summary>
        public string Adh_approval_flag { get; set; }

        /// <summary>
        /// Gets or Sets Adh_approval_reason.
        /// </summary>
        public string Adh_approval_reason { get; set; }

        /// <summary>
        /// Gets or Sets Adh_approved_userid.
        /// </summary>
        public int Adh_approved_userid { get; set; }

        /// <summary>
        /// Gets or Sets Adh_approved_date.
        /// </summary>
        public DateTime Adh_approved_date { get; set; }

        /// <summary>
        /// Gets or Sets Bho_approval_flag.
        /// </summary>
        public string Bho_approval_flag { get; set; }

        /// <summary>
        /// Gets or Sets Bho_approval_reason.
        /// </summary>
        public string Bho_approval_reason { get; set; }

        /// <summary>
        /// Gets or Sets Bho_approved_userid.
        /// </summary>
        public int Bho_approved_userid { get; set; }

        /// <summary>
        /// Gets or Sets Bho_approved_date.
        /// </summary>
        public DateTime Bho_approved_date { get; set; }

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
    }

    /// <summary>
    /// Horticulture Crop Coverage Target Block Approval.
    /// </summary>
    public class HortCropCoverageTargetBlockApproval
    {
        /// <summary>
        /// Gets or Sets Season_id.
        /// </summary>
        public int Season_id { get; set; }

        /// <summary>
        /// Gets or Sets Block_id.
        /// </summary>
        public int Block_id { get; set; }

        /// <summary>
        /// Gets or Sets Crop_id.
        /// </summary>
        public int Crop_id { get; set; }

        /// <summary>
        /// Gets or Sets Adh_approval_flag.
        /// </summary>
        public string Adh_approval_flag { get; set; }

        /// <summary>
        /// Gets or Sets Adh_approval_reason.
        /// </summary>
        public string Adh_approval_reason { get; set; }

        /// <summary>
        /// Gets or Sets Adh_approved_userid.
        /// </summary>
        public int Adh_approved_userid { get; set; }

        /// <summary>
        /// Gets or Sets Adh_approved_date.
        /// </summary>
        public DateTime Adh_approved_date { get; set; }
    }

    /// <summary>
    /// Horticulture Aggregate Crop Damage.
    /// </summary>
    public class HortAggregateCropDamage
    {
        /// <summary>
        /// Gets or Sets Season_id.
        /// </summary>
        public int Season_id { get; set; }

        /// <summary>
        /// Gets or Sets Block_id.
        /// </summary>
        public int Block_id { get; set; }

        /// <summary>
        /// Gets or Sets Crop_id.
        /// </summary>
        public int Crop_id { get; set; }

        /// <summary>
        /// Gets or Sets Adh_approval_flag.
        /// </summary>
        public string Adh_approval_flag { get; set; }

        /// <summary>
        /// Gets or Sets Adh_approval_reason.
        /// </summary>
        public string Adh_approval_reason { get; set; }

        /// <summary>
        /// Gets or Sets Adh_approved_userid.
        /// </summary>
        public int Adh_approved_userid { get; set; }

        /// <summary>
        /// Gets or Sets Adh_approved_date.
        /// </summary>
        public DateTime Adh_approved_date { get; set; }
    }
}
