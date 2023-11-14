//------------------------------------------------------------------------------
// <copyright file="CropCoverageActualVillageApprovalResponse.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//-----------------------------------------------------------------------------
namespace ADIDAS.Model.DTO
{
    /// <summary>
    /// CropCoverage Actual Village Approval Response.
    /// </summary>
    public class CropCoverageActualVillageApprovalResponse
    {
        /// <summary>
        /// Gets or Sets Status.
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Gets or Sets Reason.
        /// </summary>
        public string Reason { get; set; }

        /// <summary>
        /// Gets or Sets VillageId.
        /// </summary>
        public string VillageId { get; set; }

        /// <summary>
        /// Gets or Sets CropId.
        /// </summary>
        public string CropId { get; set; }

        /// <summary>
        /// Gets or Sets SeasonId.
        /// </summary>
        public string SeasonId { get; set; }
    }
}
