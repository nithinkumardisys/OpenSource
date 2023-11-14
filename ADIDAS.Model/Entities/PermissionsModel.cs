// <copyright file="PermissionsModel.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//----------------------------------------------------------------------------

namespace ADIDAS.Model.Entities
{
    using System.Collections.Generic;

    /// <summary>
    /// PermissionsModel.
    /// </summary>
    public class PermissionsModel
    {
        /// <summary>
        /// PermissionsModel.
        /// </summary>
        public PermissionsModel()
        {
            this.Permissions = new List<PermissionName>();
        }

        /// <summary>
        /// Permissions.
        /// </summary>
        public List<PermissionName> Permissions { get; set; }
    }
}
