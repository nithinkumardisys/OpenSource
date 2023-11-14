//------------------------------------------------------------------------------
// <copyright file="ApproveUserStatus.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//-----------------------------------------------------------------------------
namespace DepartmentOfAgriculture.Admin.Models.DTO
{
    using System.Collections.Generic;

    /// <summary>
    /// Approve User Status.
    /// </summary>
    public class ApproveUserStatus
    {
        /// <summary>
        /// Gets or Sets UserID.
        /// </summary>
        public int UserID { get; set; }

        /// <summary>
        /// Gets or Sets FirstName.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or Sets LastName.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or Sets PhoneNumber.
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Gets or Sets Districts.
        /// </summary>
        public string Districts { get; set; }

        /// <summary>
        /// Gets or Sets Divisions.
        /// </summary>
        public string Divisions { get; set; }

        /// <summary>
        /// Gets or Sets SubDivisions.
        /// </summary>
        public string SubDivisions { get; set; }

        /// <summary>
        /// Gets or Sets Blocks.
        /// </summary>
        public string Blocks { get; set; }

        /// <summary>
        /// Gets or Sets Panchayats.
        /// </summary>
        public string Panchayats { get; set; }

        /// <summary>
        /// Gets or Sets Department.
        /// </summary>
        public string Department { get; set; }

        /// <summary>
        /// Gets or Sets Designation.
        /// </summary>
        public string Designation { get; set; }

        /// <summary>
        /// Gets or Sets Status.
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Gets or Sets PrevStatus.
        /// </summary>
        public string PrevStatus { get; set; }

        /// <summary>
        /// Gets or Sets PanchayatLevel.
        /// </summary>
        public string PanchayatLevel { get; set; }
    }

    /// <summary>
    /// Approve User Status Request.
    /// </summary>
    public class ApproveUserStatusRequest
    {
        /// <summary>
        /// Gets or Sets UserList.
        /// </summary>
        public List<ApproveUserStatus> UserList { get; set; }

        /// <summary>
        /// Gets or Sets Comments.
        /// </summary>
        public string Comments { get; set; }

        /// <summary>
        /// Gets or Sets DaoId.
        /// </summary>
        public string DaoId { get; set; }
    }
}
