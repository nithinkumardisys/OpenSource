//------------------------------------------------------------------------------
// <copyright file="BiaWebGridFilters.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------
namespace ADIDAS.Model.Entities
{
    /// <summary>
    /// BiaWebGridFilters.
    /// </summary>
    public class BiaWebGridFilters
    {
        /// <summary>
        /// Gets or Sets SelectCount.
        /// </summary>
        public int? SelectCount { get; set; }

        /// <summary>
        /// Gets or Sets DeletedRecordsRegNo.
        /// </summary>
        public string DeletedRecordsRegNo { get; set; }

        /// <summary>
        /// Gets or Sets ExistingRecordsRegNo.
        /// </summary>
        public string ExistingRecordsRegNo { get; set; }

        /// <summary>
        /// Gets or Sets FinYear.
        /// </summary>
        public string FinYear { get; set; }

        /// <summary>
        /// Gets or Sets Directorate.
        /// </summary>
        public string Directorate { get; set; }

        /// <summary>
        /// Gets or Sets SchemeName.
        /// </summary>
        public string SchemeName { get; set; }

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
        /// Gets or Sets Status.
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Gets or Sets BeneficiaryNumber.
        /// </summary>
        public string BeneficiaryNumber { get; set; }

        /// <summary>
        /// Gets or Sets DetailsToShow.
        /// </summary>
        public string DetailsToShow { get; set; }
    }
}
