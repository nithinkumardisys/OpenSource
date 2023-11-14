//------------------------------------------------------------------------------
// <copyright file="AppPermission.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------

namespace ADIDAS.Model.Entities
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// App Permission.
    /// </summary>
    public partial class AppPermission
    {
        /// <summary>
        /// Gets or Sets AppPermission.
        /// </summary>
        public AppPermission()
        {
            this.RolePermission = new HashSet<RolePermission>();
        }

        /// <summary>
        /// Gets or Sets PermissionId.
        /// </summary>
        public int PermissionId { get; set; }

        /// <summary>
        /// Gets or Sets PermissionName.
        /// </summary>
        public string PermissionName { get; set; }

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
    }
}
