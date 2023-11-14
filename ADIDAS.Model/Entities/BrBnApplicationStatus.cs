//------------------------------------------------------------------------------
// <copyright file="BrBnApplicationStatus.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------
namespace ADIDAS.Model.Entities
{
    /// <summary>
    /// BrBn Application Status.
    /// </summary>
    public class BrBnApplicationStatus
    {
        /// <summary>
        /// Gets or Sets SUBScheme.
        /// </summary>
        public string SUBScheme { get; set; }

        /// <summary>
        /// Gets or Sets Scheme.
        /// </summary>
        public string Scheme { get; set; }

        /// <summary>
        /// Gets or Sets Session_name.
        /// </summary>
        public string Session_name { get; set; }

        /// <summary>
        /// Gets or Sets District.
        /// </summary>
        public string District { get; set; }

        /// <summary>
        /// Gets or Sets Block.
        /// </summary>
        public string Block { get; set; }

        /// <summary>
        /// Gets or Sets Panchyat.
        /// </summary>
        public string Panchyat { get; set; }

        /// <summary>
        /// Gets or Sets DType.
        /// </summary>
        public string DType { get; set; }

        /// <summary>
        /// Gets or Sets TotalApp.
        /// </summary>
        public string TotalApp { get; set; }

        /// <summary>
        /// Gets or Sets TotalAppQty.
        /// </summary>
        public string TotalAppQty { get; set; }

        /// <summary>
        /// Gets or Sets TotalApproved.
        /// </summary>
        public string TotalApproved { get; set; }

        /// <summary>
        /// Gets or Sets TotalApprovedQty.
        /// </summary>
        public string TotalApprovedQty { get; set; }

        /// <summary>
        /// Gets or Sets TotalReject.
        /// </summary>
        public string TotalReject { get; set; }

        /// <summary>
        /// Gets or Sets TotalRejectQty.
        /// </summary>
        public string TotalRejectQty { get; set; }

        /// <summary>
        /// Gets or Sets TotalDistribution.
        /// </summary>
        public string TotalDistribution { get; set; }

        /// <summary>
        /// Gets or Sets TotalDistributionQty.
        /// </summary>
        public string TotalDistributionQty { get; set; }
    }
}
