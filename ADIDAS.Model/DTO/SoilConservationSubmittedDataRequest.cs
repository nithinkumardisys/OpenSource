//------------------------------------------------------------------------------
// <copyright file="SoilConservationSubmittedDataRequest.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------
namespace ADIDAS.Model.DTO
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Create Soil Conservation Submitted Data Request.
    /// </summary>
    public class SoilConservationSubmittedDataRequest
    {
        /// <summary>
        /// Gets or Sets Panchayat_id.
        /// </summary>
        public int Panchayat_id { get; set; }

        /// <summary>
        /// Gets or Sets Scheme_id.
        /// </summary>
        public int Scheme_id { get; set; }
    }
}