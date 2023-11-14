//------------------------------------------------------------------------------
// <copyright file="CropDamageEntity.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//-----------------------------------------------------------------------------
namespace ADIDAS.Model.Entities
{
    using System;

    /// <summary>
    /// Crop Damage Entity.
    /// </summary>
    public class CropDamageEntity
    {
        /// <summary>
        /// Gets or Sets Damage_reason_id.
        /// </summary>
        public int Damage_reason_id { get; set; }

        /// <summary>
        /// Gets or Sets Damage_reason_name.
        /// </summary>
        public string Damage_reason_name { get; set; }

        /// <summary>
        /// Gets or Sets Ac_flag.
        /// </summary>
        public string Ac_flag { get; set; }

        /// <summary>
        /// Gets or Sets District_IDs.
        /// </summary>
        public string District_IDs { get; set; }

        /// <summary>
        /// Gets or Sets Assigned_Crop_Id.
        /// </summary>
        public string Assigned_Crop_Id { get; set; }

        /// <summary>
        /// Gets or Sets Crop_Name.
        /// </summary>
        public string Crop_Name { get; set; }

        /// <summary>
        /// Gets or Sets District_Name.
        /// </summary>
        public string District_Name { get; set; }

        /// <summary>
        /// Gets or Sets Estd_Crop_Damage.
        /// </summary>
        public string Estd_Crop_Damage { get; set; }

        /// <summary>
        /// Gets or Sets Status_flg.
        /// </summary>
        public string Status_flg { get; set; }

        /// <summary>
        /// Gets or Sets Conflict_Flag.
        /// </summary>
        public string Conflict_Flag { get; set; }

        /// <summary>
        /// Gets or Sets Rec_created_date.
        /// </summary>
        public DateTime Rec_created_date { get; set; }
    }
}
