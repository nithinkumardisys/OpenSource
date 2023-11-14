//------------------------------------------------------------------------------
// <copyright file="DTODAOUsersModel.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------
namespace ADIDAS.Model.DTO
{
    using System;

    /// <summary>
    /// Dao Users Model.
    /// </summary>
    public class DtodaoUsersModel
    {
        /// <summary>
        /// Gets or Sets User_id.
        /// </summary>
        public int User_id { get; set; }

        /// <summary>
        /// Gets or Sets Email_id.
        /// </summary>
        public string Email_id { get; set; }

        /// <summary>
        /// Gets or Sets User_name.
        /// </summary>
        public string User_name { get; set; }

        /// <summary>
        /// Gets or Sets First_name.
        /// </summary>
        public string First_name { get; set; }

        /// <summary>
        /// Gets or Sets Last_name.
        /// </summary>
        public string Last_name { get; set; }

        /// <summary>
        /// Gets or Sets Phone_num.
        /// </summary>
        public string Phone_num { get; set; }

        /// <summary>
        /// Gets or Sets Designation.
        /// </summary>
        public string Designation { get; set; }

        /// <summary>
        /// Gets or Sets Department.
        /// </summary>
        public string Department { get; set; }

        /// <summary>
        /// Gets or Sets District_id.
        /// </summary>
        public int District_id { get; set; }

        /// <summary>
        /// Gets or Sets District_name.
        /// </summary>
        public string District_name { get; set; }

        /// <summary>
        /// Gets or Sets Block_id.
        /// </summary>
        public int? Block_id { get; set; }

        /// <summary>
        /// Gets or Sets Block_name.
        /// </summary>
        public string Block_name { get; set; }

        /// <summary>
        /// Gets or Sets Gender.
        /// </summary>
        public string Gender { get; set; }

        /// <summary>
        /// Gets or Sets Panchayat_id.
        /// </summary>
        public int? Panchayat_id { get; set; }

        /// <summary>
        /// Gets or Sets Panchayat_name.
        /// </summary>
        public string Panchayat_name { get; set; }

        /// <summary>
        /// Gets or Sets Division_id.
        /// </summary>
        public int Division_id { get; set; }

        /// <summary>
        /// Gets or Sets Division_name.
        /// </summary>
        public string Division_name { get; set; }

        /// <summary>
        /// Gets or Sets Sub_Division_id.
        /// </summary>
        public int Sub_Division_id { get; set; }

        /// <summary>
        /// Gets or Sets Sub_Division_name.
        /// </summary>
        public string Sub_Division_name { get; set; }

        /// <summary>
        /// Gets or Sets Date_applied.
        /// </summary>
        public DateTime Date_applied { get; set; }

        /// <summary>
        /// Gets or Sets Date_approved.
        /// </summary>
        public DateTime Date_approved { get; set; }

        /// <summary>
        /// Gets or Sets COMMENTS.
        /// </summary>
        public string COMMENTS { get; set; }

        /// <summary>
        /// Gets or Sets Approval_status.
        /// </summary>
        public string Approval_status { get; set; }

        /// <summary>
        /// Gets or Sets Email_verified.
        /// </summary>
        public string Email_verified { get; set; }

        /// <summary>
        /// Gets or Sets Conflict_flag.
        /// </summary>
        public string Conflict_flag { get; set; }

        /// <summary>
        /// Gets or Sets TotalUsersCount.
        /// </summary>
        public int TotalUsersCount { get; set; }

        /// <summary>
        /// Gets or Sets User_Count_using_v1.
        /// </summary>
        public int User_Count_using_v1 { get; set; }

        /// <summary>
        /// Gets or Sets Count_of_users_submitted_target.
        /// </summary>
        public int Count_of_users_submitted_target { get; set; }

        /// <summary>
        /// Gets or Sets Count_of_users_submitted_actl.
        /// </summary>
        public int Count_of_users_submitted_actl { get; set; }

        /// <summary>
        /// Gets or Sets Data_entry_flg.
        /// </summary>
        public string Data_entry_flg { get; set; }

        /// <summary>
        /// Gets or Sets New_district_id.
        /// </summary>
        public int New_district_id { get; set; }

        /// <summary>
        /// Gets or Sets New_block_id.
        /// </summary>
        public int? New_block_id { get; set; }

        /// <summary>
        /// Gets or Sets New_division_id.
        /// </summary>
        public int New_division_id { get; set; }

        /// <summary>
        /// Gets or Sets New_panchayat_id.
        /// </summary>
        public int? New_panchayat_id { get; set; }

        /// <summary>
        /// Gets or Sets New_sub_division_id.
        /// </summary>
        public int New_sub_division_id { get; set; }

        /// <summary>
        /// Gets or Sets NewBlockName.
        /// </summary>
        public string NewBlockName { get; set; }

        /// <summary>
        /// Gets or Sets NewPanchayatName.
        /// </summary>
        public string NewPanchayatName { get; set; }
    }
}
