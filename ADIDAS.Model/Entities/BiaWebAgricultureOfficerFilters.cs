//------------------------------------------------------------------------------
// <copyright file="BiaWebAgricultureOfficerFilters.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------
namespace ADIDAS.Model.Entities
{
    /// <summary>
    /// BiaWebAgricultureOfficerFilters.
    /// </summary>
    public class BiaWebAgricultureOfficerFilters
    {
        /// <summary>
        /// Gets or Sets Directorate.
        /// </summary>
        public string Directorate { get; set; }

        /// <summary>
        /// Gets or Sets DivisionId.
        /// </summary>
        public int? DivisionId { get; set; }

        /// <summary>
        /// Gets or Sets DistrictId.
        /// </summary>
        public int? DistrictId { get; set; }

        /// <summary>
        /// Gets or Sets SubDivisionId.
        /// </summary>
        public int? SubDivisionId { get; set; }

        /// <summary>
        /// Gets or Sets BlockId.
        /// </summary>
        public int? BlockId { get; set; }

        /// <summary>
        /// Gets or Sets PanchayatId.
        /// </summary>
        public int? PanchayatId { get; set; }

        /// <summary>
        /// Gets or Sets NameOrDesignation.
        /// </summary>
        public string NameOrDesignation { get; set; }
    }
}