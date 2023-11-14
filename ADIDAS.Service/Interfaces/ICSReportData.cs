//------------------------------------------------------------------------------
// <copyright file="ICSReportData.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------

namespace ADIDAS.Service.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using ADIDAS.Model.DTO;
    using ADIDAS.Model.Entities;

    /// <summary>
    /// ICSReportData.
    /// </summary>
    public interface ICSReportData
    {
        /// <summary>
        /// GetTotalUsersCount.
        /// </summary>
        /// <returns>DtoUserCount list.</returns>
        List<DtoUserCount> GetTotalUsersCount();

        /// <summary>
        /// GetUsersLoggedInCount.
        /// </summary>
        /// <returns>DtoUserCount list.</returns>
        List<DtoUserCount> GetUsersLoggedInCount();

        /// <summary>
        /// GetUsersNotLoggedInCount.
        /// </summary>
        /// <returns>DtoUserCount list.</returns>
        List<DtoUserCount> GetUsersNotLoggedInCount();

        /// <summary>
        /// GetBihanStats.
        /// </summary>
        /// <returns>DtoBihanStats list.</returns>
        List<DtoBihanStats> GetBihanStats();

        /// <summary>
        /// GetNotLoggedInUserList.
        /// </summary>
        /// <returns>NotLoggedInUserList list.</returns>
        List<NotLoggedInUserList> GetNotLoggedInUserList();

        /// <summary>
        /// GetACNotSubmittedTargetList.
        /// </summary>
        /// <returns>ACNotSubmittedTargetList list.</returns>
        List<ACNotSubmittedTargetList> GetACNotSubmittedTargetList();

        /// <summary>
        /// GetCSVReports.
        /// </summary>
        /// <param name="id">id.</param>
        /// <returns>dynamic object.</returns>
        dynamic GetCSVReports(string id);

        /// <summary>
        /// GetConsolidatedCSReports.
        /// </summary>
        /// <returns>CSReportConsolidated.</returns>
        CSReportConsolidated GetConsolidatedCSReports();
    }
}
