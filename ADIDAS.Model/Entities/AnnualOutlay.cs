//------------------------------------------------------------------------------
// <copyright file="AnnualOutlay.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------

namespace ADIDAS.Model.Entities
{
    using System;

    /// <summary>
    /// Annual Outlay.
    /// </summary>
    public class AnnualOutlay
    {
        /// <summary>
        /// Gets or Sets SchemeName.
        /// </summary>
        public string SchemeName { get; set; }

        /// <summary>
        /// Gets or Sets SchemeType.
        /// </summary>
        public string SchemeType { get; set; }

        /// <summary>
        /// Gets or Sets SchemePeriod.
        /// </summary>
        public string SchemePeriod { get; set; }

        /// <summary>
        /// Gets or Sets TotalOutlay.
        /// </summary>
        public decimal? TotalOutlay { get; set; }

        /// <summary>
        /// Gets or Sets CentralShare.
        /// </summary>
        public decimal? CentralShare { get; set; }

        /// <summary>
        /// Gets or Sets StateShare.
        /// </summary>
        public decimal? StateShare { get; set; }

        /// <summary>
        /// Gets or Sets ScspCentral.
        /// </summary>
        public decimal? ScspCentral { get; set; }

        /// <summary>
        /// Gets or Sets ScspState.
        /// </summary>
        public decimal? ScspState { get; set; }

        /// <summary>
        /// Gets or Sets TspCentral.
        /// </summary>
        public decimal? TspCentral { get; set; }

        /// <summary>
        /// Gets or Sets TspState.
        /// </summary>
        public decimal? TspState { get; set; }

        /// <summary>
        /// Gets or Sets RecCreatedUserid.
        /// </summary>
        public string RecCreatedUserid { get; set; }

        /// <summary>
        /// Gets or Sets RecCreatedDate.
        /// </summary>
        public DateTime? RecCreatedDate { get; set; }

        /// <summary>
        /// Gets or Sets RecUpdatedUserid.
        /// </summary>
        public string RecUpdatedUserid { get; set; }

        /// <summary>
        /// Gets or Sets RecUpdatedDate.
        /// </summary>
        public DateTime? RecUpdatedDate { get; set; }
    }
}
