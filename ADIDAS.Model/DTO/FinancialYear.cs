//------------------------------------------------------------------------------
// <copyright file="FinancialYear.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------
namespace ADIDAS.Model.DTO
{
    /// <summary>
    /// FinancialYear.
    /// </summary>
    public class FinancialYear
    {
        /// <summary>
        /// Gets or Sets Id.
        /// </summary>
        public int? Id { get; set; }

        /// <summary>
        /// Gets or Sets Financial_Year.
        /// </summary>
        public string Financial_Year { get; set; }

        /// <summary>
        /// Gets or Sets IsCurrent.
        /// </summary>
        public bool? IsCurrent { get; set; }
    }
}
