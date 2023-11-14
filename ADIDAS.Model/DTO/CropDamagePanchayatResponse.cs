//------------------------------------------------------------------------------
// <copyright file="CropDamagePanchayatResponse.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------
namespace ADIDAS.Model.DTO
{
    /// <summary>
    /// CropDamage Panchayat Response.
    /// </summary>
    public class CropDamagePanchayatResponse
    {
        /// <summary>
        /// Gets or Sets Status.
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Gets or Sets Reason.
        /// </summary>
        public string Reason { get; set; }

        /// <summary>
        /// Gets or Sets PanchayatId.
        /// </summary>
        public string PanchayatId { get; set; }

        /// <summary>
        /// Gets or Sets CropId.
        /// </summary>
        public string CropId { get; set; }

        /// <summary>
        /// Gets or Sets SeasonId.
        /// </summary>
        public string SeasonId { get; set; }

        /// <summary>
        /// Gets or Sets DamageReason.
        /// </summary>
        public string DamageReason { get; set; }
    }
}
