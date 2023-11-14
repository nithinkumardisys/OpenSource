//------------------------------------------------------------------------------
// <copyright file="DTOFarmerContractCropsreg.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------
namespace ADIDAS.Model.DTO
{
    /// <summary>
    /// FarmerContractCropsreg.
    /// </summary>
    public class DtoFarmerContractCropsreg
    {
        /// <summary>
        /// Gets or Sets Farmer_dbt_reg_no.
        /// </summary>
        public string Farmer_dbt_reg_no { get; set; }

        /// <summary>
        /// Gets or Sets Crop_id.
        /// </summary>
        public int Crop_id { get; set; }

        /// <summary>
        /// Gets or Sets Crop_name.
        /// </summary>
        public string Crop_name { get; set; }

        /// <summary>
        /// Gets or Sets Crop_category.
        /// </summary>
        public string Crop_category { get; set; }

        /// <summary>
        /// Gets or Sets Crop_type.
        /// </summary>
        public string Crop_type { get; set; }
    }
}
