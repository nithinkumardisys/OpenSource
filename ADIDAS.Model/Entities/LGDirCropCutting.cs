//------------------------------------------------------------------------------
// <copyright file="LGDirCropCutting.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//-----------------------------------------------------------------------------
namespace ADIDAS.Model.Entities
{
    /// <summary>
    /// LgDirCropCutting.
    /// </summary>
    public class LgDirCropCutting
    {
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
        /// Gets or Sets Panchayat_id.
        /// </summary>
        public int Panchayat_id { get; set; }

        /// <summary>
        /// Gets or Sets Panchayat_name.
        /// </summary>
        public string Panchayat_name { get; set; }

        /// <summary>
        /// Gets or Sets Area_Target.
        /// </summary>
        public decimal? Area_Target { get; set; }

        /// <summary>
        /// Gets or Sets Area_Covered.
        /// </summary>
        public decimal? Area_Covered { get; set; }

        /// <summary>
        /// Gets or Sets Cumm_Yield_Prev.
        /// </summary>
        public decimal? Cumm_Yield_Prev { get; set; }

        /// <summary>
        /// Gets or Sets Cumm_Yield_Curr.
        /// </summary>
        public decimal? Cumm_Yield_Curr { get; set; }

        /// <summary>
        /// Gets or Sets Cumm_Production_Prev.
        /// </summary>
        public decimal? Cumm_Production_Prev { get; set; }

        /// <summary>
        /// Gets or Sets Cumm_Production_Curr.
        /// </summary>
        public decimal? Cumm_Production_Curr { get; set; }

        /// <summary>
        /// Gets or Sets Approval_Flag.
        /// </summary>
        public string Approval_Flag { get; set; }
    }
}
