//------------------------------------------------------------------------------
// <copyright file="AppRole.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------
namespace ADIDAS.Model.Entities
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Application Role.
    /// </summary>
    public partial class AppRole
    {
        /// <summary>
        /// Gets or Sets AppRole.
        /// </summary>
        public AppRole()
        {
            this.RolePermission = new HashSet<RolePermission>();
            this.UserRole = new HashSet<UserRole>();
        }

        /// <summary>
        /// Gets or Sets RoleId.
        /// </summary>
        public int RoleId { get; set; }

        /// <summary>
        /// Gets or Sets RoleName.
        /// </summary>
        public string RoleName { get; set; }

        /// <summary>
        /// Gets or Sets RoleDescription.
        /// </summary>
        public string RoleDescription { get; set; }

        /// <summary>
        /// Gets or Sets RecCreatedUserid.
        /// </summary>
        public string RecCreatedUserid { get; set; }

        /// <summary>
        /// Gets or Sets RecCreatedDate.
        /// </summary>
        public DateTime? RecCreatedDate { get; set; }

        /// <summary>
        /// Gets or Sets RecUpdatedUserid.
        /// </summary>
        public string RecUpdatedUserid { get; set; }

        /// <summary>
        /// Gets or Sets RecUpdatedDate.
        /// </summary>
        public DateTime? RecUpdatedDate { get; set; }

        /// <summary>
        /// Gets or Sets RolePermission.
        /// </summary>
        public virtual ICollection<RolePermission> RolePermission { get; set; }

        /// <summary>
        /// Gets or Sets UserRole.
        /// </summary>
        public virtual ICollection<UserRole> UserRole { get; set; }
    }
}
