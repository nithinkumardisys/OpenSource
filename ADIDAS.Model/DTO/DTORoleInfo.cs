//------------------------------------------------------------------------------
// <copyright file="DTORoleInfo.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------

namespace ADIDAS.Model.DTO
{
    using System;
    using System.Collections.Generic;
    using ADIDAS.Model.Entities;

    /// <summary>
    /// Role Info.
    /// </summary>
    public class DtoRoleInfo
    {
        /// <summary>
        /// Gets or Sets DtoRoleInfo.
        /// </summary>
        public DtoRoleInfo()
        {
            this.Permissions = new List<PermissionType>();
        }

        /// <summary>
        /// Gets or Sets Role_id.
        /// </summary>
        public int Role_id { get; set; }

        /// <summary>
        /// Gets or Sets Role_name.
        /// </summary>
        public string Role_name { get; set; }

        /// <summary>
        /// Gets or Sets Role_description.
        /// </summary>
        public string Role_description { get; set; }

        /// <summary>
        /// Gets or Sets Permissions.
        /// </summary>
        public List<PermissionType> Permissions { get; set; }

        /// <summary>
        /// Gets or Sets Department.
        /// </summary>
        public string Department { get; set; }

        /// <summary>
        /// Gets or Sets Designation.
        /// </summary>
        public string Designation { get; set; }

        /// <summary>
        /// Gets or Sets District_id.
        /// </summary>
        public string District_id { get; set; }

        /// <summary>
        /// Gets or Sets Block_id.
        /// </summary>
        public string Block_id { get; set; }

        /// <summary>
        /// Gets or Sets Panchayat_id.
        /// </summary>
        public string Panchayat_id { get; set; }

        /// <summary>
        /// Gets or Sets User_id.
        /// </summary>
        public string User_id { get; set; }

        /// <summary>
        /// Gets or Sets Status.
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Gets or Sets Created_by.
        /// </summary>
        public int Created_by { get; set; }

        /// <summary>
        /// Gets or Sets Created_date.
        /// </summary>
        public DateTime? Created_date { get; set; }

        /// <summary>
        /// Gets or Sets Updated_by.
        /// </summary>
        public int Updated_by { get; set; }

        /// <summary>
        /// Gets or Sets Updated_date.
        /// </summary>
        public DateTime? Updated_date { get; set; }

        /// <summary>
        /// Gets or Sets Division_id.
        /// </summary>
        public string Division_id { get; set; }

        /// <summary>
        /// Gets or Sets Sub_division_id.
        /// </summary>
        public string Sub_division_id { get; set; }
    }
}
