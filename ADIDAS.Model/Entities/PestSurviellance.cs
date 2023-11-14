﻿// <copyright file="PestSurviellance.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//----------------------------------------------------------------------------
namespace ADIDAS.Model.Entities
{
    using System;

    /// <summary>
    /// PestSurviellance.
    /// </summary>
    public class PestSurviellance
    {
        /// <summary>
        /// Gets or Sets Mm_year.
        /// </summary
        public string Mm_year { get; set; }

        /// <summary>
        /// Gets or Sets District_id.
        /// </summary
        public int District_id { get; set; }

        /// <summary>
        /// Gets or Sets Crop_id.
        /// </summary
        public int Crop_id { get; set; }

        /// <summary>
        /// Gets or Sets Crop_name.
        /// </summary
        public string Crop_name { get; set; }

        /// <summary>
        /// Gets or Sets Crop_category.
        /// </summary
        public string Crop_category { get; set; }

        /// <summary>
        /// Gets or Sets Crop_state.
        /// </summary
        public string Crop_state { get; set; }

        /// <summary>
        /// Gets or Sets Crop_state_id.
        /// </summary
        public int Crop_state_id { get; set; }

        /// <summary>
        /// Gets or Sets Covered_area.
        /// </summary
        public decimal? Covered_area { get; set; }

        /// <summary>
        /// Gets or Sets Disease_name.
        /// </summary
        public string Disease_name { get; set; }

        /// <summary>
        /// Gets or Sets Disease_id.
        /// </summary
        public int Disease_id { get; set; }

        /// <summary>
        /// Gets or Sets Affected_area.
        /// </summary
        public decimal? Affected_area { get; set; }

        /// <summary>
        /// Gets or Sets Treated_area.
        /// </summary
        public decimal? Treated_area { get; set; }

        /// <summary>
        /// Gets or Sets Incedence_percent.
        /// </summary
        public decimal? Incedence_percent { get; set; }

        /// <summary>
        /// Gets or Sets Insect_intensity.
        /// </summary
        public string Insect_intensity { get; set; }

        /// <summary>
        /// Gets or Sets Submitted_date.
        /// </summary
        public DateTime? Submitted_date { get; set; }

        /// <summary>
        /// Gets or Sets Submitted_by.
        /// </summary
        public int? Submitted_by { get; set; }

        /// <summary>
        /// Gets or Sets Technique_used.
        /// </summary
        public string Technique_used { get; set; }

        /// <summary>
        /// Gets or Sets MSlNom_year.
        /// </summary
        public int SlNo { get; set; }

        /// <summary>
        /// Gets or Sets Season_id.
        /// </summary
        public int Season_id { get; set; }
    }
}
