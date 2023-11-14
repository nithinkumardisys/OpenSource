//------------------------------------------------------------------------------
// <copyright file="SoilConservationNotificationResponse.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------
namespace ADIDAS.Model.DTO
{
    using System;

    /// <summary>
    /// Create Soil Conservation Notification Response.
    /// </summary>
    public class SoilConservationNotificationResponse
    {
        /// <summary>
        /// Gets or Sets Panchayat_Name.
        /// </summary>
        public string Panchayat_Name { get; set; }

        /// <summary>
        /// Gets or Sets Due_date.
        /// </summary>
        public DateTime Due_date { get; set; }

        /// <summary>
        /// Gets or Sets Scheme_name.
        /// </summary>
        public string Scheme_name { get; set; }

        /// <summary>
        /// Gets or Sets Yojana_number.
        /// </summary>
        public string Yojana_number { get; set; }
    }
}