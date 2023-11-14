//------------------------------------------------------------------------------
// <copyright file="CSReportConsolidated.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//-----------------------------------------------------------------------------
namespace ADIDAS.Model.Entities
{
    using System.Collections.Generic;
    using ADIDAS.Model.DTO;

    /// <summary>
    /// CSReportConsolidated.
    /// </summary>
    public class CSReportConsolidated
    {
        /// <summary>
        /// CSReportConsolidated.
        /// </summary>
        public CSReportConsolidated()
        {
            this.AllCounts = new List<AllCount>();

            this.NotLoggedInUserLists = new List<NotLoggedInUserList>();

            this.ACNotSubmittedTargetLists = new List<ACNotSubmittedTargetList>();

            this.NoCoverageUserLists = new List<NoCoverageUserList>();

            this.CoverageExceed150Lists = new List<CoverageExceed150List>();

            this.Target50BlkAvgLists = new List<Target50BlkAvgList>();

            this.Target50PanchytAvgLists = new List<Target50PanchytAvgList>();
        }

        /// <summary>
        /// Gets or Sets AllCounts.
        /// </summary>
        public List<AllCount> AllCounts { get; set; }

        /// <summary>
        /// Gets or Sets NotLoggedInUserLists.
        /// </summary>
        public List<NotLoggedInUserList> NotLoggedInUserLists { get; set; }

        /// <summary>
        /// Gets or Sets ACNotSubmittedTargetLists.
        /// </summary>
        public List<ACNotSubmittedTargetList> ACNotSubmittedTargetLists { get; set; }

        /// <summary>
        /// Gets or Sets NoCoverageUserLists.
        /// </summary>
        public List<NoCoverageUserList> NoCoverageUserLists { get; set; }

        /// <summary>
        /// Gets or Sets CoverageExceed150Lists.
        /// </summary>
        public List<CoverageExceed150List> CoverageExceed150Lists { get; set; }

        /// <summary>
        /// Gets or Sets Target50BlkAvgLists.
        /// </summary>
        public List<Target50BlkAvgList> Target50BlkAvgLists { get; set; }

        /// <summary>
        /// Gets or Sets Target50PanchytAvgLists.
        /// </summary>
        public List<Target50PanchytAvgList> Target50PanchytAvgLists { get; set; }
    }
}
