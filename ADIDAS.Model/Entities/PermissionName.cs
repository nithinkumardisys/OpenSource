// <copyright file="PermissionName.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//----------------------------------------------------------------------------
namespace ADIDAS.Model.Entities
{
    using System.Collections.Generic;

    /// <summary>
    /// PermissionName.
    /// </summary>
    public class PermissionName
    {
        /// <summary>
        /// PermissionName.
        /// </summary>
        public PermissionName()
        {
            this.PermissionTypes = new List<PermissionType>();
        }

        /// <summary>
        /// Gets or Sets Permission_name.
        /// </summary>
        public string Permission_name { get; set; }

        /// <summary>
        /// Gets or Sets PermissionTypes.
        /// </summary>
        public List<PermissionType> PermissionTypes { get; set; }
    }
}
