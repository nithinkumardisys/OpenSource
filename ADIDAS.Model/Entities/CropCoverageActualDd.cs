//------------------------------------------------------------------------------
// <copyright file="CropCoverageActualDd.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------

namespace ADIDAS.Model.Entities
{
    using System;

    /// <summary>
    /// Crop Coverage Actual Dd.
    /// </summary>
    public partial class CropCoverageActualDd
    {
        /// <summary>
        /// Gets or Sets ReportedDate.
        /// </summary>
        public DateTime ReportedDate { get; set; }

        /// <summary>
        /// Gets or Sets DistrictName.
        /// </summary>
        public string DistrictName { get; set; }

        /// <summary>
        /// Gets or Sets BlockName.
        /// </summary>
        public string BlockName { get; set; }

        /// <summary>
        /// Gets or Sets PanchayatName.
        /// </summary>
        public string PanchayatName { get; set; }

        /// <summary>
        /// Gets or Sets NoOfRevenueVillages.
        /// </summary>
        public int? NoOfRevenueVillages { get; set; }

        /// <summary>
        /// Gets or Sets CropName.
        /// </summary>
        public string CropName { get; set; }

        /// <summary>
        /// Gets or Sets AreaActual.
        /// </summary>
        public decimal? AreaActual { get; set; }

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
