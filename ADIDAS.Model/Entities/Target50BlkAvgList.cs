// <copyright file="Target50BlkAvgList.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//----------------------------------------------------------------------------
namespace ADIDAS.Model.Entities
{
    /// <summary>
    /// Target50BlkAvgList.
    /// </summary>
    public class Target50BlkAvgList
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
        /// Gets or Sets Season_name.
        /// </summary>
        public string Season_name { get; set; }

        /// <summary>
        /// Gets or Sets Crop_name.
        /// </summary>
        public string Crop_name { get; set; }

        /// <summary>
        /// Gets or Sets Block_Area_Target.
        /// </summary>
        public decimal Block_Area_Target { get; set; }

        /// <summary>
        /// Gets or Sets District_Area_Average.
        /// </summary>
        public decimal District_Area_Average { get; set; }

        /// <summary>
        /// Gets or Sets Percentage_Diff.
        /// </summary>
        public decimal Percentage_Diff { get; set; }
    }
}
