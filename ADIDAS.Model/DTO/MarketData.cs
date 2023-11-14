//------------------------------------------------------------------------------
// <copyright file="MarketData.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------

namespace ADIDAS.Model.DTO
{
    /// <summary>
    /// MarketData.
    /// </summary>
    public class MarketData
    {
        /// <summary>
        /// Gets or Sets Market_id.
        /// </summary>
        public int Mkt_id { get; set; }

        /// <summary>
        /// Gets or Sets Market_name.
        /// </summary>
        public string Mkt_name { get; set; }

        /// <summary>
        /// Gets or Sets District_id.
        /// </summary>
        public int District_id { get; set; }

        /// <summary>
        /// Gets or Sets District_name.
        /// </summary>
        public string District_name { get; set; }
    }
}
