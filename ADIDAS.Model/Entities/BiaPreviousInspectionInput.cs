//------------------------------------------------------------------------------
// <copyright file="BiaPreviousInspectionInput.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------
namespace ADIDAS.Model.DTO
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// BiaPreviousInspectionInput.
    /// </summary>
    public class BiaPreviousInspectionInput
    {
        /// <summary>
        /// Gets or Sets Application_number.
        /// </summary>
        public string Application_number { get; set; }

        /// <summary>
        /// Gets or Sets Beneficiary_id.
        /// </summary>
        public string Beneficiary_id { get; set; }

        /// <summary>
        /// Gets or Sets Beneficiary_name.
        /// </summary>
        public string Beneficiary_name { get; set; }

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
