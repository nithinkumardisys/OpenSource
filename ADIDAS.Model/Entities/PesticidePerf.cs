// <copyright file="PesticidePerf.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//----------------------------------------------------------------------------
namespace ADIDAS.Model.Entities
{
    using System;

    /// <summary>
    /// PesticidePerf.
    /// </summary>
    public class PesticidePerf
    {
        /// <summary>
        /// Gets or Sets Pesticide_id.
        /// </summary
        public int Pesticide_id { get; set; }

        /// <summary>
        /// Gets or Sets Pesticide_name.
        /// </summary
        public string Pesticide_name { get; set; }

        /// <summary>
        /// Gets or Sets No_of_formulations.
        /// </summary
        public int? No_of_formulations { get; set; }

        /// <summary>
        /// Gets or Sets Formulation_id.
        /// </summary
        public int Formulation_id { get; set; }

        /// <summary>
        /// Gets or Sets Formulation_registered.
        /// </summary
        public string Formulation_registered { get; set; }

        /// <summary>
        /// Gets or Sets Mm_year.
        /// </summary
        public string Mm_year { get; set; }

        /// <summary>
        /// Gets or Sets Prev_balance.
        /// </summary
        public decimal? Prev_balance { get; set; }

        /// <summary>
        /// Gets or Sets Supply.
        /// </summary
        public decimal? Supply { get; set; }

        /// <summary>
        /// Gets or Sets Consumption.
        /// </summary
        public decimal? Consumption { get; set; }

        /// <summary>
        /// Gets or Sets Balance.
        /// </summary
        public decimal? Balance { get; set; }

        /// <summary>
        /// Gets or Sets District_id.
        /// </summary
        public int District_id { get; set; }

        /// <summary>
        /// Gets or Sets Submitted_by.
        /// </summary
        public int? Submitted_by { get; set; }

        /// <summary>
        /// Gets or Sets Submitted_date.
        /// </summary
        public DateTime? Submitted_date { get; set; }
    }
}
