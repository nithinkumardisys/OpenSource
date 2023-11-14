//------------------------------------------------------------------------------
// <copyright file="DTOEditDamageRequest.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------
namespace ADIDAS.Model.DTO
{
    /// <summary>
    /// Edit Damage Request.
    /// </summary>
    public class DtoEditDamageRequest
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
        /// Gets or Sets Assigned_District_Ids.
        /// </summary>
        public string Assigned_District_Ids { get; set; }

        /// <summary>
        /// Gets or Sets Year.
        /// </summary>
        public int Year { get; set; }
    }
}
