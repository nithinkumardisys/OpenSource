//------------------------------------------------------------------------------
// <copyright file="BiaAssignBeneficiaryInput.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------
namespace ADIDAS.Model.Entities
{
    using System;

    /// <summary>
    /// BiaAssignBeneficiaryInput.
    /// </summary>
    public class BiaAssignBeneficiaryInput
    {
        /// <summary>
        /// Gets or Sets Id.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or Sets UserId.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or Sets Status.
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Gets or Sets Due_date.
        /// </summary>
        public DateTime Due_date { get; set; }
    }
}
