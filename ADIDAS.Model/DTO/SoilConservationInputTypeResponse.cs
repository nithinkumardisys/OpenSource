//------------------------------------------------------------------------------
// <copyright file="SoilConservationInputTypeResponse.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------
namespace ADIDAS.Model.DTO
{
    /// <summary>
    /// Create Soil Conservation Input Type Response.
    /// </summary>
    public class SoilConservationInputTypeResponse
    {
        /// <summary>
        /// Gets or Sets Panchayat_id.
        /// </summary>
        public int Input_type_id { get; set; }

        /// <summary>
        /// Gets or Sets Panchayat_id.
        /// </summary>
        public string Input_type_name { get; set; }

        /// <summary>
        /// Gets or Sets Panchayat_id.
        /// </summary>
        public string Input_type_value { get; set; }
    }
}
