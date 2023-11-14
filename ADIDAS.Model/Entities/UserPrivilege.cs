// <copyright file="UserPrivilege.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//----------------------------------------------------------------------------
namespace ADIDAS.Model.Entities
{
    /// <summary>
    /// UserPrivilege.
    /// </summary>
    public class UserPrivilege
    {
        /// <summary>
        /// Gets or Sets User_Name.
        /// </summary>
        public string User_Name { get; set; }

        /// <summary>
        /// Gets or Sets Role_Name.
        /// </summary>
        public string Role_Name { get; set; }

        /// <summary>
        /// Gets or Sets Module_Name.
        /// </summary>
        public string Module_Name { get; set; }

        /// <summary>
        /// Gets or Sets Permission_Name.
        /// </summary>
        public string Permission_Name { get; set; }
    }
}
