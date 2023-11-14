//------------------------------------------------------------------------------
// <copyright file="CropCoverageActualBlockApprovalResponse.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------
namespace ADIDAS.Model.DTO
{
    using System.Collections.Generic;

    /// <summary>
    /// Crop Coverage ActualBlock Approval Response.
    /// </summary>
    public class CropCoverageActualBlockApprovalResponse
    {
        /// <summary>
        /// Gets or Sets Panchayatresponse.
        /// </summary>
        public CropCoverageActualBlockApprovalResponse()
        {
            this.Panchayatresponse = new List<CropCoverageActualPanchayatApprovalResponse>();
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
        public List<CropCoverageActualPanchayatApprovalResponse> Panchayatresponse { get; set; }
    }
}
