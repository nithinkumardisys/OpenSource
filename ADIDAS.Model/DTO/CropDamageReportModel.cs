//------------------------------------------------------------------------------
// <copyright file="CropDamageReportModel.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------
namespace ADIDAS.Model.DTO
{
    /// <summary>
    /// Crop Damage Report Model.
    /// </summary>
    public class CropDamageReportModel
    {
        /// <summary>
        /// Gets or Sets Panchayat_id.
        /// </summary>
        public int Panchayat_id { get; set; }

        /// <summary>
        /// Gets or Sets Panchayat_name.
        /// </summary>
        public string Panchayat_name { get; set; }

        /// <summary>
        /// Gets or Sets District_id.
        /// </summary>
        public int District_id { get; set; }

        /// <summary>
        /// Gets or Sets District_name.
        /// </summary>
        public string District_name { get; set; }

        /// <summary>
        /// Gets or Sets Block_id.
        /// </summary>
        public int Block_id { get; set; }

        /// <summary>
        /// Gets or Sets Block_name.
        /// </summary>
        public string Block_name { get; set; }

        /// <summary>
        /// Gets or Sets Covered_Net_SOWN_AREA.
        /// </summary>
        public decimal Covered_Net_SOWN_AREA { get; set; }

        /// <summary>
        /// Gets or Sets Covered_Irrigated_Area.
        /// </summary>
        public decimal Covered_Irrigated_Area { get; set; }

        /// <summary>
        /// Gets or Sets Covered_Non_Irrigated_Area.
        /// </summary>
        public decimal Covered_Non_Irrigated_Area { get; set; }

        /// <summary>
        /// Gets or Sets Covered_Total_Area.
        /// </summary>
        public decimal Covered_Total_Area { get; set; }

        /// <summary>
        /// Gets or Sets Covered_Perennial.
        /// </summary>
        public decimal Covered_Perennial { get; set; }

        /// <summary>
        /// Gets or Sets Covered_Grand_Total_Area.
        /// </summary>
        public decimal Covered_Grand_Total_Area { get; set; }

        /// <summary>
        /// Gets or Sets Total_Net_SOWN.
        /// </summary>
        public decimal Total_Net_SOWN { get; set; }

        /// <summary>
        /// Gets or Sets Total_Covered_Irrigated_Area.
        /// </summary>
        public decimal Total_Covered_Irrigated_Area { get; set; }

        /// <summary>
        /// Gets or Sets Total_Covered_Non_Irrigated_Area.
        /// </summary>
        public decimal Total_Covered_Non_Irrigated_Area { get; set; }

        /// <summary>
        /// Gets or Sets Total_Covered_Total_Area.
        /// </summary>
        public decimal Total_Covered_Total_Area { get; set; }

        /// <summary>
        /// Gets or Sets Total_Covered_Perennial.
        /// </summary>
        public decimal Total_Covered_Perennial { get; set; }

        /// <summary>
        /// Gets or Sets Total_Covered_Grand_Total_Area.
        /// </summary>
        public decimal Total_Covered_Grand_Total_Area { get; set; }
    }

    /// <summary>
    /// Crop Damage Report Sum.
    /// </summary>
    public class CropDamageReportSum
    {
        /// <summary>
        /// Gets or Sets Panchayat_id.
        /// </summary>
        public int Panchayat_id { get; set; }

        /// <summary>
        /// Gets or Sets Panchayat_name.
        /// </summary>
        public string Panchayat_name { get; set; }

        /// <summary>
        /// Gets or Sets District_id.
        /// </summary>
        public int District_id { get; set; }

        /// <summary>
        /// Gets or Sets District_name.
        /// </summary>
        public string District_name { get; set; }

        /// <summary>
        /// Gets or Sets Block_id.
        /// </summary>
        public int Block_id { get; set; }

        /// <summary>
        /// Gets or Sets Block_name.
        /// </summary>
        public string Block_name { get; set; }

        /// <summary>
        /// Gets or Sets Damage_Irrigated_Area.
        /// </summary>
        public decimal Damage_Irrigated_Area { get; set; }

        /// <summary>
        /// Gets or Sets Damage_NonIrrigated_Area.
        /// </summary>
        public decimal Damage_NonIrrigated_Area { get; set; }

        /// <summary>
        /// Gets or Sets Damage_Total_Area.
        /// </summary>
        public decimal Damage_Total_Area { get; set; }

        /// <summary>
        /// Gets or Sets Damage_Perennial_Area.
        /// </summary>
        public decimal Damage_Perennial_Area { get; set; }

        /// <summary>
        /// Gets or Sets Damage_GrandTotal_Area.
        /// </summary>
        public decimal Damage_GrandTotal_Area { get; set; }

        /// <summary>
        /// Gets or Sets Damage_Irrigated_Cost.
        /// </summary>
        public decimal Damage_Irrigated_Cost { get; set; }

        /// <summary>
        /// Gets or Sets Damage_NonIrrigated_Cost.
        /// </summary>
        public decimal Damage_NonIrrigated_Cost { get; set; }

        /// <summary>
        /// Gets or Sets Damage_Total_Cost.
        /// </summary>
        public decimal Damage_Total_Cost { get; set; }

        /// <summary>
        /// Gets or Sets Damage_Perennial_Cost.
        /// </summary>
        public decimal Damage_Perennial_Cost { get; set; }

        /// <summary>
        /// Gets or Sets Damage_GrandTotal_Cost.
        /// </summary>
        public decimal Damage_GrandTotal_Cost { get; set; }
    }

    /// <summary>
    /// Crop Model.
    /// </summary>
    public class CropModel
    {
        /// <summary>
        /// Gets or Sets Crop_id.
        /// </summary>
        public int Crop_id { get; set; }

        /// <summary>
        /// Gets or Sets Crop_name.
        /// </summary>
        public string Crop_name { get; set; }
    }
}
