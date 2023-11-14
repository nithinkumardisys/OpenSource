//------------------------------------------------------------------------------
// <copyright file="BiaPostBeneficiaryDetail.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------
namespace ADIDAS.Model.Entities
{
    using System;

    /// <summary>
    /// BiaPostBeneficiaryDetail.
    /// </summary>
    public class BiaPostBeneficiaryDetail
    {
        /// <summary>
        /// Gets or Sets Id.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or Sets Verify_date.
        /// </summary>
        public DateTime Verify_date { get; set; }

        /// <summary>
        /// Gets or Sets Verify_completion_date.
        /// </summary>
        public DateTime Verify_completion_date { get; set; }

        /// <summary>
        /// Gets or Sets Is_Inspection_Verified.
        /// </summary>
        public string Is_Inspection_Verified { get; set; }

        /// <summary>
        /// Gets or Sets Is_Verified.
        /// </summary>
        public string Is_Verified { get; set; }

        /// <summary>
        /// Gets or Sets Current_comments.
        /// </summary>
        public string Current_comments { get; set; }

        /// <summary>
        /// Gets or Sets Assigned_by_name.
        /// </summary>
        public string Assigned_by_name { get; set; }

        /// <summary>
        /// Gets or Sets Assigned_by_designation.
        /// </summary>
        public string Assigned_by_designation { get; set; }

        /// <summary>
        /// Gets or Sets Userid.
        /// </summary>
        public int Userid { get; set; }

        /// <summary>
        /// Gets or Sets Rec_created_date.
        /// </summary>
        public DateTime Rec_created_date { get; set; }

        /// <summary>
        /// Gets or Sets Rec_updated_date.
        /// </summary>
        public DateTime Rec_updated_date { get; set; }
    }
}