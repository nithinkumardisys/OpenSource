//------------------------------------------------------------------------------
// <copyright file="CombPesticidePerfOffline.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//-----------------------------------------------------------------------------
namespace ADIDAS.Model.DTO
{
    using System;

    /// <summary>
    /// Combo Pesticide Perf Offline.
    /// </summary>
    public class CombPesticidePerfOffline
    {
        /// <summary>
        /// Gets or Sets Comb_pesticide_id.
        /// </summary>
        public int Comb_pesticide_id { get; set; }

        /// <summary>
        /// Gets or Sets Pesticide_type.
        /// </summary>
        public string Pesticide_type { get; set; }

        /// <summary>
        /// Gets or Sets Combination_product.
        /// </summary>
        public string Combination_product { get; set; }

        /// <summary>
        /// Gets or Sets Company_name.
        /// </summary>
        public string Company_name { get; set; }

        /// <summary>
        /// Gets or Sets Mm_year.
        /// </summary>
        public string Mm_year { get; set; }

        /// <summary>
        /// Gets or Sets Prev_balance.
        /// </summary>
        public decimal Prev_balance { get; set; }

        /// <summary>
        /// Gets or Sets Supply.
        /// </summary>
        public decimal Supply { get; set; }

        /// <summary>
        /// Gets or Sets Consumption.
        /// </summary>
        public decimal Consumption { get; set; }

        /// <summary>
        /// Gets or Sets Balance.
        /// </summary>
        public decimal Balance { get; set; }

        /// <summary>
        /// Gets or Sets District_id.
        /// </summary>
        public int District_id { get; set; }

        /// <summary>
        /// Gets or Sets Submitted_by.
        /// </summary>
        public int Submitted_by { get; set; }

        /// <summary>
        /// Gets or Sets Submitted_date.
        /// </summary>
        public DateTime? Submitted_date { get; set; }
    }
}
