//------------------------------------------------------------------------------
// <copyright file="BaoCropdamageGetModel.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------
namespace ADIDAS.Model.DTO
{
    using System.Collections.Generic;

    /// <summary>
    /// Crop damage Get Model.
    /// </summary>
    public class BaoCropdamageGetModel
    {
        /// <summary>
        /// Gets or Sets Reported_date.
        /// </summary>
        public string Reported_date { get; set; }

        /// <summary>
        /// Gets or Sets Damage_Reason.
        /// </summary>
        public string Damage_Reason { get; set; }

        /// <summary>
        /// Gets or Sets DamageReasonId.
        /// </summary>
        public int DamageReasonId { get; set; }

        /// <summary>
        /// Gets or Sets District_Id.
        /// </summary>
        public string District_Id { get; set; }

        /// <summary>
        /// Gets or Sets District_Name.
        /// </summary>
        public string District_Name { get; set; }

        /// <summary>
        /// Gets or Sets CreatedBy.
        /// </summary>
        public string CreatedBy { get; set; }

        /// <summary>
        /// Gets or Sets UpdatedBy.
        /// </summary>
        public string UpdatedBy { get; set; }

        /// <summary>
        /// Gets or Sets Refreshed_date.
        /// </summary>
        public string Refreshed_date { get; set; }

        /// <summary>
        /// Gets or Sets Refreshed_userid.
        /// </summary>
        public int Refreshed_userid { get; set; }

        /// <summary>
        /// Gets or Sets BlockList.
        /// </summary>
        public BlockList BlockList { get; set; }
    }

    /// <summary>
    /// Covered Area Get.
    /// </summary>
    public class BAOcoveredAreaGet
    {
        /// <summary>
        /// Gets or Sets Irrigated_Dmg_Area.
        /// </summary>
        public decimal Irrigated_Dmg_Area { get; set; }

        /// <summary>
        /// Gets or Sets Non_irrigated_Dmg_Area.
        /// </summary>
        public decimal Non_irrigated_Dmg_Area { get; set; }

        /// <summary>
        /// Gets or Sets Perennial_horticulture.
        /// </summary>
        public decimal Perennial_horticulture { get; set; }

        /// <summary>
        /// Gets or Sets Perennial_sugarcane.
        /// </summary>
        public decimal Perennial_sugarcane { get; set; }

        /// <summary>
        /// Gets or Sets Total.
        /// </summary>
        public decimal Total { get; set; }

        /// <summary>
        /// Gets or Sets GrandTotal.
        /// </summary>
        public decimal GrandTotal { get; set; }
    }

    /// <summary>
    /// Affected Area Get.
    /// </summary>
    public class BaoAffectedAreaGet
    {
        /// <summary>
        /// Gets or Sets Perennial_horti_impact.
        /// </summary>
        public decimal Perennial_horti_impact { get; set; }

        /// <summary>
        /// Gets or Sets Perennial_sugarcane_impact.
        /// </summary>
        public decimal Perennial_sugarcane_impact { get; set; }

        /// <summary>
        /// Gets or Sets Total_impact_area.
        /// </summary>
        public decimal Total_impact_area { get; set; }

        /// <summary>
        /// Gets or Sets Grand_total_impact.
        /// </summary>
        public decimal Grand_total_impact { get; set; }

        /// <summary>
        /// Gets or Sets CropName.
        /// </summary>
        public List<CropNameGet> CropName { get; set; }
    }

    /// <summary>
    /// get crop name.
    /// </summary>
    public class BAOcropNameGet
    {
        /// <summary>
        /// Gets or Sets Crop_id.
        /// </summary>
        public int Crop_id { get; set; }

        /// <summary>
        /// Gets or Sets Crop_name.
        /// </summary>
        public string Crop_name { get; set; }

        /// <summary>
        /// Gets or Sets Crop_value.
        /// </summary>
        public decimal Crop_value { get; set; }
    }

    /// <summary>
    /// To get damaged area.
    /// </summary>
    public class BaoDamageAreaGet
    {
        /// <summary>
        /// Gets or Sets Irrigated_Dmg_Area.
        /// </summary>
        public decimal Irrigated_Dmg_Area { get; set; }

        /// <summary>
        /// Gets or Sets Irrigated_Dmg_Area_Estimated.
        /// </summary>
        public decimal Irrigated_Dmg_Area_Estimated { get; set; }

        /// <summary>
        /// Gets or Sets Non_irrigated_Dmg_Area.
        /// </summary>
        public decimal Non_irrigated_Dmg_Area { get; set; }

        /// <summary>
        /// Gets or Sets Non_irrigated_Dmg_Area_Estimated.
        /// </summary>
        public decimal Non_irrigated_Dmg_Area_Estimated { get; set; }

        /// <summary>
        /// Gets or Sets Perennial_horticulture.
        /// </summary>
        public decimal Perennial_horticulture { get; set; }

        /// <summary>
        /// Gets or Sets Perennial_horticulture_Estimated.
        /// </summary>
        public decimal Perennial_horticulture_Estimated { get; set; }

        /// <summary>
        /// Gets or Sets Perennial_sugarcane.
        /// </summary>
        public decimal Perennial_sugarcane { get; set; }

        /// <summary>
        /// Gets or Sets Perennial_sugarcane_Estimated.
        /// </summary>
        public decimal Perennial_sugarcane_Estimated { get; set; }

        /// <summary>
        /// Gets or Sets TotalArea.
        /// </summary>
        public decimal TotalArea { get; set; }

        /// <summary>
        /// Gets or Sets GrandTotalArea.
        /// </summary>
        public decimal GrandTotalArea { get; set; }

        /// <summary>
        /// Gets or Sets TotalAmount.
        /// </summary>
        public decimal TotalAmount { get; set; }

        /// <summary>
        /// Gets or Sets GrandTotalAmount.
        /// </summary>
        public decimal GrandTotalAmount { get; set; }
    }
}
