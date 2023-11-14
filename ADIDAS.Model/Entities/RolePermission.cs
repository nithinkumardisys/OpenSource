// <copyright file="RolePermission.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//----------------------------------------------------------------------------
namespace ADIDAS.Model.Entities
{
    /// <summary>
    /// RolePermission.
    /// </summary>
    public partial class RolePermission
    {
        /// <summary>
        /// Gets or Sets RoleId.
        /// </summary
        public int RoleId { get; set; }

        /// <summary>
        /// Gets or Sets PermissionId.
        /// </summary
        public int PermissionId { get; set; }

        /// <summary>
        /// Gets or Sets Permission.
        /// </summary
        public virtual AppPermission Permission { get; set; }

        /// <summary>
        /// Gets or Sets Role.
        /// </summary
        public virtual AppRole Role { get; set; }
    }
}
