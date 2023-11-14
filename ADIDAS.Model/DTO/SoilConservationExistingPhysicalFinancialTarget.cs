//------------------------------------------------------------------------------
// <copyright file="SoilConservationExistingPhysicalFinancialTarget.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------
namespace ADIDAS.Model.DTO
{
    /// <summary>
    /// Create Soil Conservation Existing Physical Financial Target.
    /// </summary>
    public class SoilConservationExistingPhysicalFinancialTarget
    {
        /// <summary>
        /// Gets or Sets Panchayat_id.
        /// </summary>
        public int? Panchayat_id { get; set; }

        /// <summary>
        /// Gets or Sets Panchayat_name.
        /// </summary>
        public string Panchayat_name { get; set; }

        /// <summary>
        /// Gets or Sets Scheme_id.
        /// </summary>
        public int? Scheme_id { get; set; }

        /// <summary>
        /// Gets or Sets Scheme_name.
        /// </summary>
        public string Scheme_name { get; set; }

        /// <summary>
        /// Gets or Sets Activity_id.
        /// </summary>
        public int? Activity_id { get; set; }

        /// <summary>
        /// Gets or Sets Activity_name.
        /// </summary>
        public string Activity_name { get; set; }

        /// <summary>
        /// Gets or Sets Sub_activity_id.
        /// </summary>
        public int? Sub_activity_id { get; set; }

        /// <summary>
        /// Gets or Sets Sub_activity_name.
        /// </summary>
        public string Sub_activity_name { get; set; }

        /// <summary>
        /// Gets or Sets Structure_id.
        /// </summary>
        public int? Structure_id { get; set; }

        /// <summary>
        /// Gets or Sets Structure_name.
        /// </summary>
        public string Structure_name { get; set; }

        /// <summary>
        /// Gets or Sets Current_year.
        /// </summary>
        public string Current_year { get; set; }

        /// <summary>
        /// Gets or Sets Physical_target.
        /// </summary>
        public decimal? Physical_target { get; set; }

        /// <summary>
        /// Gets or Sets Financial_target.
        /// </summary>
        public decimal? Financial_target { get; set; }
    }
}
