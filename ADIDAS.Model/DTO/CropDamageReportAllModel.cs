//------------------------------------------------------------------------------
// <copyright file="CropDamageReportAllModel.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------

namespace ADIDAS.Model.DTO
{
    using System.Collections.Generic;

    /// <summary>
    /// Crop Damage Report All Model.
    /// </summary>
    public class CropDamageReportAllModel
    {
        /// <summary>
        /// Gets or Sets CropDamageReportAllModel.
        /// </summary>
        public CropDamageReportAllModel()
        {
            this.CropdamageReportModels = new List<CropDamageReportModel>();

            this.CropdamageReportSums = new List<CropDamageReportSum>();

            this.Cropmodels = new List<CropModel>();
        }

        /// <summary>
        /// Gets or Sets CropdamageReportModels.
        /// </summary>
        public List<CropDamageReportModel> CropdamageReportModels { get; set; }

        /// <summary>
        /// Gets or Sets CropdamageReportSums.
        /// </summary>
        public List<CropDamageReportSum> CropdamageReportSums { get; set; }

        /// <summary>
        /// Gets or Sets Cropmodels.
        /// </summary>
        public List<CropModel> Cropmodels { get; set; }
    }
}
