//------------------------------------------------------------------------------
// <copyright file="BiaPreviousInspection.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------
namespace ADIDAS.Model.DTO
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// BiaPreviousInspection.
    /// </summary>
    public class BiaPreviousInspection
    {
        /// <summary>
        /// Gets or Sets Previous_Comments.
        /// </summary>
        public string Previous_Comments { get; set; }

        /// <summary>
        /// Gets or Sets Previous_is_Verified.
        /// </summary>
        public string Previous_is_Verified { get; set; }

        /// <summary>
        /// Gets or Sets Previous_Verification_date.
        /// </summary>
        public DateTime Previous_Verification_date { get; set; }

        /// <summary>
        /// Gets or Sets Previous_Assigned_by_designation.
        /// </summary>
        public string Previous_Assigned_by_designation { get; set; }

        /// <summary>
        /// Gets or Sets Previous_Assigned_by_name.
        /// </summary>
        public string Previous_Assigned_by_name { get; set; }

        /// <summary>
        /// Gets or Sets Previous_Assigneer_User_Id .
        /// </summary>
        public int Previous_Assigneer_User_Id { get; set; }
    }
}
