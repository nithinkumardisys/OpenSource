//------------------------------------------------------------------------------
// <copyright file="CoverageExceed150List.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------
namespace ADIDAS.Model.Entities
{
    /// <summary>
    /// Coverage Exceed150 List.
    /// </summary>
    public class CoverageExceed150List
    {
        /// <summary>
        /// Gets or Sets District_name.
        /// </summary>
        public string District_name { get; set; }

        /// <summary>
        /// Gets or Sets Dao_name.
        /// </summary>
        public string Dao_name { get; set; }

        /// <summary>
        /// Gets or Sets Dao_designation.
        /// </summary>
        public string Dao_designation { get; set; }

        /// <summary>
        /// Gets or Sets Block_name.
        /// </summary>
        public string Block_name { get; set; }

        /// <summary>
        /// Gets or Sets Bao_name.
        /// </summary>
        public string Bao_name { get; set; }

        /// <summary>
        /// Gets or Sets Bao_designation.
        /// </summary>
        public string Bao_designation { get; set; }

        /// <summary>
        /// Gets or Sets Panchayat_name.
        /// </summary>
        public string Panchayat_name { get; set; }

        /// <summary>
        /// Gets or Sets Ac_name.
        /// </summary>
        public string Ac_name { get; set; }

        /// <summary>
        /// Gets or Sets Ac_designation.
        /// </summary>
        public string Ac_designation { get; set; }

        /// <summary>
        /// Gets or Sets Season_name.
        /// </summary>
        public string Season_name { get; set; }

        /// <summary>
        /// Gets or Sets Crop_name.
        /// </summary>
        public string Crop_name { get; set; }

        /// <summary>
        /// Gets or Sets Area_target.
        /// </summary>
        public decimal Area_target { get; set; }

        /// <summary>
        /// Gets or Sets Cumm_area_curr.
        /// </summary>
        public decimal Cumm_area_curr { get; set; }

        /// <summary>
        /// Gets or Sets Percentage_Diff.
        /// </summary>
        public decimal Percentage_Diff { get; set; }
    }
}
