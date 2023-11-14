// <copyright file="UserRole.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//----------------------------------------------------------------------------
namespace ADIDAS.Model.Entities
{
    /// <summary>
    /// UserRole.
    /// </summary>
    public partial class UserRole
    {
        /// <summary>
        /// Gets or Sets UserId.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or Sets RoleId.
        /// </summary>
        public int RoleId { get; set; }

        /// <summary>
        /// Gets or Sets Role.
        /// </summary>
        public virtual AppRole Role { get; set; }

        /// <summary>
        /// Gets or Sets User.
        /// </summary>
        public virtual UserEntity User { get; set; }
    }
}
