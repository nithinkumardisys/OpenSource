//------------------------------------------------------------------------------
// <copyright file="GetCropCvrgTargetProductivityDataReportModel.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------

namespace ADIDAS.Model.DTO
{
    using System.Collections.Generic;

    /// <summary>
    /// Get CropCoverage Target Productivity Data Report Model.
    /// </summary>
    public class GetCropCvrgTargetProductivityDataReportModel
    {
        /// <summary>
        /// Gets or Sets GetCropCvrgTargetProductivityDataReportModel.
        /// </summary>
        public GetCropCvrgTargetProductivityDataReportModel()
        {
            this.Crop_name = new List<string>();
        }

        /// <summary>
        /// Gets or Sets District_name.
        /// </summary>
        public string District_name { get; set; }

        /// <summary>
        /// Gets or Sets District_id.
        /// </summary>
        public int District_id { get; set; }

        /// <summary>
        /// Gets or Sets Productivity.
        /// </summary>
        public decimal Productivity { get; set; }

        /// <summary>
        /// Gets or Sets Crop_name.
        /// </summary>
        public List<string> Crop_name { get; set; }
    }
}
