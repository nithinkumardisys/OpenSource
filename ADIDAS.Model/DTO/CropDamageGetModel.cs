//------------------------------------------------------------------------------
// <copyright file="CropDamageGetModel.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------
namespace ADIDAS.Model.DTO
{
    using System;

    /// <summary>
    /// CropDamage GetModel.
    /// </summary>
    public class CropDamageGetModel
    {
        /// <summary>
        /// Gets or Sets Year.
        /// </summary>
        public int Year { get; set; }

        /// <summary>
        /// Gets or Sets Crop_id.
        /// </summary>
        public int Crop_id { get; set; }

        /// <summary>
        /// Gets or Sets Adv_Search_Str.
        /// </summary>
        public string Adv_Search_Str { get; set; }

        /// <summary>
        /// Gets or Sets Damage_reason_id.
        /// </summary>
        public int Damage_reason_id { get; set; }

        /// <summary>
        /// Gets or Sets Damage_reason_name.
        /// </summary>
        public string Damage_reason_name { get; set; }

        /// <summary>
        /// Gets or Sets Assigned_Crop_Id.
        /// </summary>
        public string Assigned_Crop_Id { get; set; }

        /// <summary>
        /// Gets or Sets Assigned_District_Id.
        /// </summary>
        public string Assigned_District_Id { get; set; }

        /// <summary>
        /// Gets or Sets Estd_Crop_Damage.
        /// </summary>
        public string Estd_Crop_Damage { get; set; }

        /// <summary>
        /// Gets or Sets Status_Flg.
        /// </summary>
        public string Status_Flg { get; set; }

        /// <summary>
        /// Gets or Sets Crop_name.
        /// </summary>
        public string Crop_name { get; set; }

        /// <summary>
        /// Gets or Sets District_name.
        /// </summary>
        public string District_name { get; set; }

        /// <summary>
        /// Gets or Sets Query_Name.
        /// </summary>
        public string Query_Name { get; set; }

        /// <summary>
        /// Gets or Sets Reported_date.
        /// </summary>
        public DateTime Reported_date { get; set; }

        /// <summary>
        /// Gets or Sets Rec_created_userid.
        /// </summary>
        public int Rec_created_userid { get; set; }

        /// <summary>
        /// Gets or Sets Rec_created_date.
        /// </summary>
        public DateTime Rec_created_date { get; set; }

        /// <summary>
        /// Gets or Sets Attribute_name.
        /// </summary>
        public string Attribute_name { get; set; }
    }
}
