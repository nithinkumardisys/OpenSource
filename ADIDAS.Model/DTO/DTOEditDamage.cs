//------------------------------------------------------------------------------
// <copyright file="DTOEditDamage.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------
namespace ADIDAS.Model.DTO
{
    using System;

    /// <summary>
    /// Edit Damage.
    /// </summary>
    public class DtoEditDamage
    {
        /// <summary>
        /// Gets or Sets Damage_reason_id.
        /// </summary>
        public int Damage_reason_id { get; set; }

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
        /// Gets or Sets District_IDs.
        /// </summary>
        public string District_IDs { get; set; }

        /// <summary>
        /// Gets or Sets Damage_reason_name.
        /// </summary>
        public string Damage_reason_name { get; set; }

        /// <summary>
        /// Gets or Sets Estd_Crop_Damage.
        /// </summary>
        public string Estd_Crop_Damage { get; set; }

        /// <summary>
        /// Gets or Sets Status_Flg.
        /// </summary>
        public string Status_Flg { get; set; }

        /// <summary>
        /// Gets or Sets Rec_created_userid.
        /// </summary>
        public int Rec_created_userid { get; set; }

        /// <summary>
        /// Gets or Sets Rec_created_date.
        /// </summary>
        public DateTime Rec_created_date { get; set; }
    }
}
