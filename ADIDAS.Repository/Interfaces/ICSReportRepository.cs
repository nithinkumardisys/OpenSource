//------------------------------------------------------------------------------
// <copyright file="ICSReportRepository.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------
namespace ADIDAS.Repository.Interfaces
{
    using System.Collections.Generic;
    using ADIDAS.Model.DTO;
    using ADIDAS.Model.Entities;

    /// <summary>
    /// ICSReportRepository.
    /// </summary>
    public interface ICSReportRepository
    {
        /// <summary>
        /// GetTotalUsersCount.
        /// </summary>
        /// <returns>DtoUserCount.</returns>
        List<DtoUserCount> GetTotalUsersCount();

        /// <summary>
        /// GetUsersLoggedInCount.
        /// </summary>
        /// <returns>DtoUserCount.</returns>
        List<DtoUserCount> GetUsersLoggedInCount();

        /// <summary>
        /// GetUsersNotLoggedInCount.
        /// </summary>
        /// <returns>DtoUserCount.</returns>
        List<DtoUserCount> GetUsersNotLoggedInCount();

        /// <summary>
        /// GetBihanStats.
        /// </summary>
        /// <returns>DtoBihanStats.</returns>
        List<DtoBihanStats> GetBihanStats();

        /// <summary>
        /// GetNotLoggedInUserList.
        /// </summary>
        /// <returns>NotLoggedInUserList.</returns>
        List<NotLoggedInUserList> GetNotLoggedInUserList();

        /// <summary>
        /// GetACNotSubmittedTargetList.
        /// </summary>
        /// <returns>ACNotSubmittedTargetList.</returns>
        List<ACNotSubmittedTargetList> GetACNotSubmittedTargetList();

        /// <summary>
        /// GetCSVReports.
        /// </summary>
        /// <param name="id">id.</param>
        /// <returns>Values.</returns>
        dynamic GetCSVReports(string id);

        /// <summary>
        /// GetConsolidatedCSReports.
        /// </summary>
        /// <returns>GetConsolidatedCSReports Result.</returns>
        CSReportConsolidated GetConsolidatedCSReports();
    }
}
