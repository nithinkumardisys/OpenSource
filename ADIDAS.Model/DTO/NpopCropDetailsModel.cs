//------------------------------------------------------------------------------
// <copyright file="NpopCropDetailsModel.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------
namespace ADIDAS.Model.DTO
{
    using System;

    /// <summary>
    /// NPOP Crop Info.
    /// </summary>
    public class NpopCropDetailsModel
    {
        /// <summary>
        /// Gets or Sets Farmer_dbt_reg_no.
        /// </summary>
        public long Farmer_dbt_reg_no { get; set; }

        /// <summary>
        /// Gets or Sets Farm_id.
        /// </summary>
        public int Farm_id { get; set; }

        /// <summary>
        /// Gets or Sets Season_id.
        /// </summary>
        public int Season_id { get; set; }

        /// <summary>
        /// Gets or Sets Season_name.
        /// </summary>
        public string Season_name { get; set; }

        /// <summary>
        /// Gets or Sets Crop_id.
        /// </summary>
        public int Crop_id { get; set; }

        /// <summary>
        /// Gets or Sets Crop_name.
        /// </summary>
        public string Crop_name { get; set; }

        /// <summary>
        /// Gets or Sets Crop_cvrg.
        /// </summary>
        public decimal Crop_cvrg { get; set; }

        /// <summary>
        /// Gets or Sets Harvest.
        /// </summary>
        public decimal? Harvest { get; set; }

        /// <summary>
        /// Gets or Sets Productivity.
        /// </summary>
        public decimal Productivity { get; set; }

        /// <summary>
        /// Gets or Sets Final_submission_flg.
        /// </summary>
        public string Final_submission_flg { get; set; }

        /// <summary>
        /// Gets or Sets Is_inspection_happened.
        /// </summary>
        public string Is_inspection_happened { get; set; }

        /// <summary>
        /// Gets or Sets Inspection_date.
        /// </summary>
        public DateTime? Inspection_date { get; set; }
    }
}
