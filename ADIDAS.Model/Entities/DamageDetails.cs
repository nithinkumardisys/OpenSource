//------------------------------------------------------------------------------
// <copyright file="DamageDetails.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//-----------------------------------------------------------------------------
namespace ADIDAS.Model.Entities
{
    /// <summary>
    /// DamageDetails.
    /// </summary>
    public class DamageDetails
    {
        /// <summary>
        /// Gets or Sets UserId.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or Sets Year.
        /// </summary>
        public int Year { get; set; }

        /// <summary>
        /// Gets or Sets DamageReasonId.
        /// </summary>
        public int DamageReasonId { get; set; }

        /// <summary>
        /// Gets or Sets AssignedCropId.
        /// </summary>
        public string AssignedCropId { get; set; }

        /// <summary>
        /// Gets or Sets AssigneddistrictId.
        /// </summary>
        public string AssigneddistrictId { get; set; }

        /// <summary>
        /// Gets or Sets EstdCropDamage.
        /// </summary>
        public string EstdCropDamage { get; set; }

        /// <summary>
        /// Gets or Sets StatusFlag.
        /// </summary>
        public string StatusFlag { get; set; }
    }
}
