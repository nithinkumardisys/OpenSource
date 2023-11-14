//------------------------------------------------------------------------------
// <copyright file="PesticideSurveillanceOffline.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------

namespace ADIDAS.Model.DTO
{
    using System;

    /// <summary>
    /// Pesticide Surveillance Offline.
    /// </summary>
    public class PesticideSurveillanceOffline
    {
        /// <summary>
        /// Gets or Sets Mm_year.
        /// </summary>
        public string Mm_year { get; set; }

        /// <summary>
        /// Gets or Sets District_id.
        /// </summary>
        public int District_id { get; set; }

        /// <summary>
        /// Gets or Sets Crop_id.
        /// </summary>
        public int Crop_id { get; set; }

        /// <summary>
        /// Gets or Sets Crop_category.
        /// </summary>
        public string Crop_category { get; set; }

        /// <summary>
        /// Gets or Sets Crop_name.
        /// </summary>
        public string Crop_name { get; set; }

        /// <summary>
        /// Gets or Sets Crop_state.
        /// </summary>
        public string Crop_state { get; set; }

        /// <summary>
        /// Gets or Sets Covered_area.
        /// </summary>
        public decimal Covered_area { get; set; }

        /// <summary>
        /// Gets or Sets Insect_disease_name.
        /// </summary>
        public string Insect_disease_name { get; set; }

        /// <summary>
        /// Gets or Sets Affected_area.
        /// </summary>
        public decimal Affected_area { get; set; }

        /// <summary>
        /// Gets or Sets Treated_area.
        /// </summary>
        public decimal Treated_area { get; set; }

        /// <summary>
        /// Gets or Sets Incedence_percent.
        /// </summary>
        public decimal Incedence_percent { get; set; }

        /// <summary>
        /// Gets or Sets Insect_intensity.
        /// </summary>
        public string Insect_intensity { get; set; }

        /// <summary>
        /// Gets or Sets Submitted_date.
        /// </summary>
        public DateTime? Submitted_date { get; set; }

        /// <summary>
        /// Gets or Sets Submitted_by.
        /// </summary>
        public int Submitted_by { get; set; }

        /// <summary>
        /// Gets or Sets Technique_used.
        /// </summary>
        public string Technique_used { get; set; }
    }
}
