//------------------------------------------------------------------------------
// <copyright file="CropCoverageTargetBlockApproval.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------
namespace ADIDAS.Model.DTO
{
    using System.Collections.Generic;
    using ADIDAS.Model.Entities;

    /// <summary>
    /// CropCoverage Target Block Approval.
    /// </summary>
    public class CropCoverageTargetBlockApproval
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CropCoverageTargetBlockApproval"/> class.
        /// Gets or Sets Panchayatresponse.
        /// </summary>
        public CropCoverageTargetBlockApproval()
        {
            this.Panchayatresponse = new List<CropCoverageTargetPanchytApprovalResponse>();
        }

        /// <summary>
        /// Gets or Sets Status.
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Gets or Sets Reason.
        /// </summary>
        public string Reason { get; set; }

        /// <summary>
        /// Gets or Sets BlockId.
        /// </summary>
        public string BlockId { get; set; }

        /// <summary>
        /// Gets or Sets CropId.
        /// </summary>
        public string CropId { get; set; }

        /// <summary>
        /// Gets or Sets SeasonId.
        /// </summary>
        public string SeasonId { get; set; }

        /// <summary>
        /// Gets or Sets Panchayatresponse.
        /// </summary>
        public List<CropCoverageTargetPanchytApprovalResponse> Panchayatresponse { get; set; }
    }
}
