//------------------------------------------------------------------------------
// <copyright file="BasoccaModel.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------
namespace ADIDAS.Model.Entities
{
    /// <summary>
    /// Basocca Model.
    /// </summary>
    public class BasoccaModel
    {
        /// <summary>
        /// Gets or Sets DistrictName.
        /// </summary>
        public string DistrictName { get; set; }

        /// <summary>
        /// Gets or Sets BlockName.
        /// </summary>
        public string BlockName { get; set; }

        /// <summary>
        /// Gets or Sets Crop.
        /// </summary>
        public string Crop { get; set; }

        /// <summary>
        /// Gets or Sets Season.
        /// </summary>
        public string Season { get; set; }

        /// <summary>
        /// Gets or Sets Year.
        /// </summary>
        public string Year { get; set; }

        /// <summary>
        /// Gets or Sets TotalRegistredFarmer.
        /// </summary>
        public int? TotalRegistredFarmer { get; set; }

        /// <summary>
        /// Gets or Sets TotalRegistredArea.
        /// </summary>
        public decimal? TotalRegistredArea { get; set; }

        /// <summary>
        /// Gets or Sets TotalApprovedQuantity.
        /// </summary>
        public decimal? TotalApprovedQuantity { get; set; }
    }
}
