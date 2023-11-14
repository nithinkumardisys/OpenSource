// <copyright file="PostTargetSetting.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//----------------------------------------------------------------------------

namespace ADIDAS.Model.Entities
{
    /// <summary>
    /// PostTargetSetting.
    /// </summary>
    public class PostTargetSetting
    {
        /// <summary>
        /// Gets or Sets DistrictId.
        /// </summary
        public int DistrictId { get; set; }

        /// <summary>
        /// Gets or Sets SchemeId.
        /// </summary
        public int SchemeId { get; set; }

        /// <summary>
        /// Gets or Sets ActivityId.
        /// </summary
        public int ActivityId { get; set; }

        /// <summary>
        /// Gets or Sets Period.
        /// </summary
        public string Period { get; set; }

        /// <summary>
        /// Gets or Sets Targetqty.
        /// </summary
        public int Targetqty { get; set; }

        /// <summary>
        /// Gets or Sets Uom.
        /// </summary
        public string Uom { get; set; }

        /// <summary>
        /// Gets or Sets UserId.
        /// </summary
        public int UserId { get; set; }
    }
}
