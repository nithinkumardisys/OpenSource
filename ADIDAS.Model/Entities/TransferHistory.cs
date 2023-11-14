// <copyright file="TransferHistory.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//----------------------------------------------------------------------------
namespace ADIDAS.Model.Entities
{
    using System;
    using System.Collections.Generic;
    using ADIDAS.Model.DTO;

    /// <summary>
    /// TransferHistory.
    /// </summary>
    public class TransferHistory
    {
        /// <summary>
        /// TransferHistory.
        /// </summary>
        public TransferHistory()
        {
            this.Panchayats = new List<PanchayatModel>();

            this.Blocks = new List<BlockLst>();

            this.Newpanchayats = new List<PanchayatModel>();

            this.Newblocks = new List<BlockLst>();
        }

        /// <summary>
        /// Gets or Sets User_id.
        /// </summary>
        public int User_id { get; set; }

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
        /// Gets or Sets Department.
        /// </summary>
        public string Department { get; set; }

        /// <summary>
        /// Gets or Sets Designation.
        /// </summary>
        public string Designation { get; set; }

        /// <summary>
        /// Gets or Sets Division_ID.
        /// </summary>
        public int? Division_ID { get; set; }

        /// <summary>
        /// Gets or Sets Division_name.
        /// </summary>
        public string Division_name { get; set; }

        /// <summary>
        /// Gets or Sets District_ID.
        /// </summary>
        public int? District_ID { get; set; }

        /// <summary>
        /// Gets or Sets District_name.
        /// </summary>
        public string District_name { get; set; }

        /// <summary>
        /// Gets or Sets Block_id.
        /// </summary>
        public int? Block_id { get; set; }

        /// <summary>
        /// Gets or Sets SubDivision_ID.
        /// </summary>
        public int? SubDivision_ID { get; set; }

        /// <summary>
        /// Gets or Sets SubDivision_name.
        /// </summary>
        public string SubDivision_name { get; set; }

        /// <summary>
        /// Gets or Sets Block_name.
        /// </summary>
        public string Block_name { get; set; }

        /// <summary>
        /// Gets or Sets Panchayat_id.
        /// </summary>
        public int? Panchayat_id { get; set; }

        /// <summary>
        /// Gets or Sets Panchayat_name.
        /// </summary>
        public string Panchayat_name { get; set; }

        /// <summary>
        /// Gets or Sets Data_entry_flg.
        /// </summary>
        public string Data_entry_flg { get; set; }

        /// <summary>
        /// Gets or Sets Dao_designation.
        /// </summary>
        public int? New_Division_ID { get; set; }

        /// <summary>
        /// Gets or Sets New_Division_name.
        /// </summary>
        public string New_Division_name { get; set; }

        /// <summary>
        /// Gets or Sets New_District_ID.
        /// </summary>
        public int? New_District_ID { get; set; }

        /// <summary>
        /// Gets or Sets New_district_name.
        /// </summary>
        public string New_district_name { get; set; }

        /// <summary>
        /// Gets or Sets New_block_id.
        /// </summary>
        public int? New_block_id { get; set; }

        /// <summary>
        /// Gets or Sets New_SubDivision_ID.
        /// </summary>
        public int? New_SubDivision_ID { get; set; }

        /// <summary>
        /// Gets or Sets New_SubDivision_name.
        /// </summary>
        public string New_SubDivision_name { get; set; }

        /// <summary>
        /// Gets or Sets New_block_name.
        /// </summary>
        public string New_block_name { get; set; }

        /// <summary>
        /// Gets or Sets New_panchayat_id.
        /// </summary>
        public int? New_panchayat_id { get; set; }

        /// <summary>
        /// Gets or Sets New_panchayat_name.
        /// </summary>
        public string New_panchayat_name { get; set; }

        /// <summary>
        /// Gets or Sets New_data_entry_flg.
        /// </summary>
        public string New_data_entry_flg { get; set; }

        /// <summary>
        /// Gets or Sets Date_applied.
        /// </summary>
        public DateTime? Date_applied { get; set; }

        /// <summary>
        /// Gets or Sets User_applied.
        /// </summary>
        public int? User_applied { get; set; }

        /// <summary>
        /// Gets or Sets Approval_status.
        /// </summary>
        public string Approval_status { get; set; }

        /// <summary>
        /// Gets or Sets Date_approved.
        /// </summary>
        public string Date_approved { get; set; }

        /// <summary>
        /// Gets or Sets User_approved.
        /// </summary>
        public int? User_approved { get; set; }

        /// <summary>
        /// Gets or Sets Panchayats.
        /// </summary>
        public List<PanchayatModel> Panchayats { get; set; }

        /// <summary>
        /// Gets or Sets Blocks.
        /// </summary>
        public List<BlockLst> Blocks { get; set; }

        /// <summary>
        /// Gets or Sets Newpanchayats.
        /// </summary>
        public List<PanchayatModel> Newpanchayats { get; set; }

        /// <summary>
        /// Gets or Sets Newblocks.
        /// </summary>
        public List<BlockLst> Newblocks { get; set; }
    }
}
