//------------------------------------------------------------------------------
// <copyright file="GetCropCvrgTargetDataReportModel.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------

namespace ADIDAS.Model.DTO
{
    /// <summary>
    /// Get CropCoverage Target Data Report Model.
    /// </summary>
    public class GetCropCvrgTargetDataReportModel
    {
        /// <summary>
        /// Gets or Sets User_Id.
        /// </summary>
        public string User_Id { get; set; }

        /// <summary>
        /// Gets or Sets Year.
        /// </summary>
        public int Year { get; set; }

        /// <summary>
        /// Gets or Sets Season_id.
        /// </summary>
        public string Season_id { get; set; }

        /// <summary>
        /// Gets or Sets Crop_activity.
        /// </summary>
        public string Crop_activity { get; set; }

        /// <summary>
        /// Gets or Sets Crop_type.
        /// </summary>
        public string Crop_type { get; set; }

        /// <summary>
        /// Gets or Sets Crop_Name_Like.
        /// </summary>
        public string Crop_Name_Like { get; set; }

        /// <summary>
        /// Gets or Sets Date.
        /// </summary>
        public string Date { get; set; }

        /// <summary>
        /// Gets or Sets Approval_Status.
        /// </summary>
        public string Approval_Status { get; set; }

        /// <summary>
        /// Gets or Sets Crop_ID.
        /// </summary>
        public string Crop_ID { get; set; }

        /// <summary>
        /// Gets or Sets District_ID.
        /// </summary>
        public string District_ID { get; set; }

        /// <summary>
        /// Gets or Sets Block_ID.
        /// </summary>
        public int Block_ID { get; set; }

        /// <summary>
        /// Gets or Sets Panchayat_ID.
        /// </summary>
        public int Panchayat_ID { get; set; }

        /// <summary>
        /// Gets or Sets CropCategory.
        /// </summary>
        public string CropCategory { get; set; }

        /// <summary>
        /// Gets or Sets FruitPerennial.
        /// </summary>
        public string FruitPerennial { get; set; }
    }
}
