//------------------------------------------------------------------------------
// <copyright file="CSReportConsolidatedWrapper.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------

namespace ADIDAS.Model.DTO
{
    using System.Collections.Generic;
    using ADIDAS.Model.Entities;

    /// <summary>
    /// CSReport Consolidated Wrapper.
    /// </summary>
    public class CSReportConsolidatedWrapper
    {
        /// <summary>
        /// Gets or Sets CSReportConsolidatedWrapper.
        /// </summary>
        public CSReportConsolidatedWrapper()
        {
            this.CsReportConsolidatedlst = new List<CSReportConsolidated>();
        }

        /// <summary>
        /// Gets or Sets CsReportConsolidatedlst.
        /// </summary>
        public List<CSReportConsolidated> CsReportConsolidatedlst { get; set; }
    }
}
