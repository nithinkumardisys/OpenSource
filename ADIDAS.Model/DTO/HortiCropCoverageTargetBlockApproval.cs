//------------------------------------------------------------------------------
// <copyright file="HortiCropCoverageTargetBlockApproval.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------

namespace ADIDAS.Model.DTO
{
    using System.Collections.Generic;

    /// <summary>
    /// Horti CropCoverage TargetBlock Approval.
    /// </summary>
    public class HortiCropCoverageTargetBlockApproval
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HortiCropCoverageTargetBlockApproval"/> class.
        /// </summary>
        public HortiCropCoverageTargetBlockApproval()
        {
            this.PanchayatResponse = new List<HortiCropCoverageTargetPanchytApprovalResponse>();
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
        /// Gets or Sets PanchayatResponse.
        /// </summary>
        public List<HortiCropCoverageTargetPanchytApprovalResponse> PanchayatResponse { get; set; }
    }
}
