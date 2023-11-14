//------------------------------------------------------------------------------
// <copyright file="DAOUsersDetails.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//-----------------------------------------------------------------------------
namespace ADIDAS.Model.Entities
{
    using System.Collections.Generic;
    using ADIDAS.Model.DTO;

    /// <summary>
    /// DaoUsersDetails.
    /// </summary>
    public class DaoUsersDetails
    {
        /// <summary>
        /// DaoUsersDetails.
        /// </summary>
        public DaoUsersDetails()
        {
            this.Panchayats = new List<PanchayatModel>();

            this.Blocks = new List<BlockLst>();

            this.Newblocks = new List<BlockLst>();

            this.Newpanchayats = new List<PanchayatModel>();

            this.Subdivs = new List<SubDivision>();

            this.NewSubdivs = new List<SubDivision>();
        }

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
        /// Gets or Sets COMMENTS.
        /// </summary>
        public string COMMENTS { get; set; }

        /// <summary>
        /// Gets or Sets Date_applied.
        /// </summary>
        public string Date_applied { get; set; }

        /// <summary>
        /// Gets or Sets Date_approved.
        /// </summary>
        public string Date_approved { get; set; }

        /// <summary>
        /// Gets or Sets Approval_status.
        /// </summary>
        public string Approval_status { get; set; }

        /// <summary>
        /// Gets or Sets Gender.
        /// </summary>
        public string Gender { get; set; }

        /// <summary>
        /// Gets or Sets Conflict_flag.
        /// </summary>
        public string Conflict_flag { get; set; }

        /// <summary>
        /// Gets or Sets Email_verified.
        /// </summary>
        public string Email_verified { get; set; }

        /// <summary>
        /// Gets or Sets Panchayats.
        /// </summary>
        public List<PanchayatModel> Panchayats { get; set; }

        /// <summary>
        /// Gets or Sets Data_entry_flg.
        /// </summary>
        public string Data_entry_flg { get; set; }

        /// <summary>
        /// Gets or Sets DbEditFlag.
        /// </summary>
        public string DbEditFlag { get; set; }

        /// <summary>
        /// Gets or Sets Blocks.
        /// </summary>
        public List<BlockLst> Blocks { get; set; }

        /// <summary>
        /// Gets or Sets PanchayatLevel.
        /// </summary>
        public string PanchayatLevel { get; set; }

        /// <summary>
        /// Gets or Sets Newblocks.
        /// </summary>
        public List<BlockLst> Newblocks { get; set; }

        /// <summary>
        /// Gets or Sets Newpanchayats.
        /// </summary>
        public List<PanchayatModel> Newpanchayats { get; set; }

        /// <summary>
        /// Gets or Sets New_district_id.
        /// </summary>
        public int New_district_id { get; set; }

        /// <summary>
        /// Gets or Sets New_block_id.
        /// </summary>
        public int? New_block_id { get; set; }

        /// <summary>
        /// Gets or Sets New_panchayat_id.
        /// </summary>
        public int? New_panchayat_id { get; set; }

        /// <summary>
        /// Gets or Sets Blockinserted.
        /// </summary>
        public string Blockinserted { get; set; }

        /// <summary>
        /// Gets or Sets SubdivInserted.
        /// </summary>
        public string SubdivInserted { get; set; }

        /// <summary>
        /// Gets or Sets PanchayatInserted.
        /// </summary>
        public string PanchayatInserted { get; set; }

        /// <summary>
        /// Gets or Sets subdiv.
        /// </summary>
        public string subdiv { get; set; }

        /// <summary>
        /// Gets or Sets Subdivs.
        /// </summary>
        public List<SubDivision> Subdivs { get; set; }

        /// <summary>
        /// Gets or Sets NewSubdivs.
        /// </summary>
        public List<SubDivision> NewSubdivs { get; set; }

        /// <summary>
        /// Gets or Sets New_Division_Id.
        /// </summary>
        public int New_Division_Id { get; set; }

        /// <summary>
        /// Gets or Sets New_Division_Name.
        /// </summary>
        public string New_Division_Name { get; set; }

        /// <summary>
        /// Gets or Sets New_Subdivision_Id.
        /// </summary>
        public int New_Subdivision_Id { get; set; }
    }
}
