//------------------------------------------------------------------------------
// <copyright file="NpopCropCreateModel.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------
namespace ADIDAS.Model.DTO
{
    using System;

    /// <summary>
    /// Create Npop Crop Details.
    /// </summary>
    public class NpopCropCreateModel
    {
        /// <summary>
        /// Gets or Sets Farm_id.
        /// </summary>
        public int Farm_id { get; set; }

        /// <summary>
        /// Gets or Sets Farmer_dbt_reg_no.
        /// </summary>
        public long Farmer_dbt_reg_no { get; set; }

        /// <summary>
        /// Gets or Sets Season_id.
        /// </summary>
        public int Season_id { get; set; }

        /// <summary>
        /// Gets or Sets Crop_id.
        /// </summary>
        public int Crop_id { get; set; }

        /// <summary>
        /// Gets or Sets Crop_cvrg.
        /// </summary>
        public Decimal Crop_cvrg { get; set; }

        /// <summary>
        /// Gets or Sets Harvest.
        /// </summary>
        public Decimal? Harvest { get; set; }

        /// <summary>
        /// Gets or Sets Productivity.
        /// </summary>
        public Decimal Productivity { get; set; }

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

        /// <summary>
        /// Gets or Sets Rec_created_userid.
        /// </summary>
        public int Rec_created_userid { get; set; }

        /// <summary>
        /// Gets or Sets Rec_created_date.
        /// </summary>
        public DateTime Rec_created_date { get; set; }

        /// <summary>
        /// Gets or Sets Rec_updated_userid.
        /// </summary>
        public int Rec_updated_userid { get; set; }

        /// <summary>
        /// Gets or Sets Rec_updated_date.
        /// </summary>
        public DateTime Rec_updated_date { get; set; }

        /// <summary>
        /// Gets or Sets Query_name.
        /// </summary>
        public string Query_name { get; set; }
    }
}
