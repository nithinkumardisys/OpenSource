//----------------------------------------------------------------------------
// <copyright file="UserRoleModel.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//----------------------------------------------------------------------------
namespace ADIDAS.Model.Entities
{
    /// <summary>
    /// UserRoleModel.
    /// </summary>
    public class UserRoleModel
    {
        /// <summary>
        /// Gets or Sets RoleName.
        /// </summary>
        public string RoleName { get; set; }

        /// <summary>
        /// Gets or Sets RoleDescription.
        /// </summary>
        public string RoleDescription { get; set; }

        /// <summary>
        /// Gets or Sets Permissions.
        /// </summary>
        public string Permissions { get; set; }

        /// <summary>
        /// Gets or Sets Status.
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Gets or Sets SelectedDistrict.
        /// </summary>
        public string SelectedDistrict { get; set; }

        /// <summary>
        /// Gets or Sets SelectedDivision.
        /// </summary>
        public string SelectedDivision { get; set; }

        /// <summary>
        /// Gets or Sets SelectedBlocks.
        /// </summary>
        public string SelectedBlocks { get; set; }

        /// <summary>
        /// Gets or Sets SelectedPanchayats.
        /// </summary>
        public string SelectedPanchayats { get; set; }

        /// <summary>
        /// Gets or Sets SelectedDepartment.
        /// </summary>
        public string SelectedDepartment { get; set; }

        /// <summary>
        /// Gets or Sets SelectedDesignation.
        /// </summary>
        public string SelectedDesignation { get; set; }

        /// <summary>
        /// Gets or Sets UserId.
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Gets or Sets RoleId.
        /// </summary>
        public string RoleId { get; set; }

        /// <summary>
        /// Gets or Sets CreatedBy.
        /// </summary>
        public int CreatedBy { get; set; }

        /// <summary>
        /// Gets or Sets UpdatedBy.
        /// </summary>
        public int UpdatedBy { get; set; }
    }
}
