//------------------------------------------------------------------------------
// <copyright file="BiaCount.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------
namespace ADIDAS.Model.DTO
{
    /// <summary>
    /// BiaCount.
    /// </summary>
    public class BiaCount
    {
        /// <summary>
        /// Gets or Sets StateLevelBeneficiaryCount.
        /// </summary>
        public int? StateLevelBeneficiaryCount { get; set; }

        /// <summary>
        /// Gets or Sets VerifiedCount.
        /// </summary>
        public int? Verified { get; set; }

        /// <summary>
        /// Gets or Sets AssignedAndPendingCount.
        /// </summary>
        public int? AssignedAndPendingCount { get; set; }

        /// <summary>
        /// Gets or Sets NotAssignedCount.
        /// </summary>
        public int? NotAssignedCount { get; set; }
    }
}
