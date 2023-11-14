//------------------------------------------------------------------------------
// <copyright file="AgmarknetPriceData.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------
namespace ADIDAS.Model.DTO
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Agmarknet Price Data.
    /// </summary>
    public class AgmarknetPriceData
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AgmarknetPriceData"/> class.
        /// Gets or Sets Price_data.
        /// </summary>
        public AgmarknetPriceData()
        {
            this.Price_data = new List<PriceData>();
        }

        /// <summary>
        /// Gets or Sets Price_data.
        /// </summary>
        public List<PriceData> Price_data { get; set; }
    }

    /// <summary>
    /// Agmark Net Price details.
    /// </summary>
    public class PriceData
    {
        /// <summary>
        /// Gets or Sets Market_center_code.
        /// </summary>
        public int Market_center_code { get; set; }

        /// <summary>
        /// Gets or Sets Date_arrival.
        /// </summary>
        public DateTime Date_arrival { get; set; }

        /// <summary>
        /// Gets or Sets Commodity_code.
        /// </summary>
        public int Commodity_code { get; set; }

        /// <summary>
        /// Gets or Sets Variety_code.
        /// </summary>
        public int Variety_code { get; set; }

        /// <summary>
        /// Gets or Sets Grade_code.
        /// </summary>
        public int Grade_code { get; set; }

        /// <summary>
        /// Gets or Sets Minimum_price.
        /// </summary>
        public decimal Minimum_price { get; set; }

        /// <summary>
        /// Gets or Sets Maximum_price.
        /// </summary>
        public decimal Maximum_price { get; set; }

        /// <summary>
        /// Gets or Sets Modal_price.
        /// </summary>
        public decimal Modal_price { get; set; }
    }
}
