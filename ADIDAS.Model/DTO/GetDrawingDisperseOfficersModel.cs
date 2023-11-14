//------------------------------------------------------------------------------
// <copyright file="GetDrawingDisperseOfficersModel.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------

namespace ADIDAS.Model.DTO
{
    /// <summary>
    /// Get Drawing Disperse Officers Model.
    /// </summary>
    public class GetDrawingDisperseOfficersModel
    {
        /// <summary>
        /// Gets or Sets Desgn_Dist.
        /// </summary>
        public string Desgn_Dist { get; set; }

        /// <summary>
        /// Gets or Sets District_id.
        /// </summary>
        public int District_id { get; set; }

        /// <summary>
        /// Gets or Sets Code.
        /// </summary>
        public int? Code { get; set; }
    }
}
