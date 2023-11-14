//------------------------------------------------------------------------------
// <copyright file="AgmarknetArrivalData.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------
namespace ADIDAS.Model.DTO
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Agmarknet Arrival Data List.
    /// </summary>
    public class AgmarknetArrivalData
    {
        /// <summary>
        /// Gets or Sets Arrival_data.
        /// </summary>
        public AgmarknetArrivalData()
        {
            this.Arrival_data = new List<ArrivalData>();
        }

        /// <summary>
        /// Gets or Sets Arrival_data.
        /// </summary>
        public List<ArrivalData> Arrival_data { get; set; }
    }

    /// <summary>
    /// Arrival Data Details.
    /// </summary>
    public class ArrivalData
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
        /// Gets or Sets Arrival_quantity.
        /// </summary>
        public decimal Arrival_quantity { get; set; }

        /// <summary>
        /// Gets or Sets Arrival_trend_code.
        /// </summary>
        public int Arrival_trend_code { get; set; }
    }
}