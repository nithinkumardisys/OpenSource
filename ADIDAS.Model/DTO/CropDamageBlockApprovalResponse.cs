//------------------------------------------------------------------------------
// <copyright file="CropDamageBlockApprovalResponse.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------
namespace ADIDAS.Model.DTO
{
    using System.Collections.Generic;

    /// <summary>
    /// Crop DamageBlock Approval Response.
    /// </summary>
    public class CropDamageBlockApprovalResponse
    {
        /// <summary>
        /// Gets or Sets CropDamageBlockApprovalResponse.
        /// </summary>
        public CropDamageBlockApprovalResponse()
        {
            this.PanchayatResponse = new List<CropDamagePanchayatResponse>();
        }

        /// <summary>
        /// Gets or Sets BlockId.
        /// </summary>
        public string BlockId { get; set; }

        /// <summary>
        /// Gets or Sets Crop_Id.
        /// </summary>
        public string Crop_Id { get; set; }

        /// <summary>
        /// Gets or Sets SeasonId.
        /// </summary>
        public string SeasonId { get; set; }

        /// <summary>
        /// Gets or Sets DamageReason.
        /// </summary>
        public string DamageReason { get; set; }

        /// <summary>
        /// Gets or Sets PanchayatResponse.
        /// </summary>
        public List<CropDamagePanchayatResponse> PanchayatResponse { get; set; }
    }
}
