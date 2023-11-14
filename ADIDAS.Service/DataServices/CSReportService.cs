//------------------------------------------------------------------------------
// <copyright file="CSReportService.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------

namespace ADIDAS.Service.DataServices
{
    using System.Collections.Generic;
    using ADIDAS.Model.DTO;
    using ADIDAS.Model.Entities;
    using ADIDAS.Repository.Interfaces;
    using ADIDAS.Service.Interfaces;

    /// <summary>
    /// CSReportService.
    /// </summary>
    public class CSReportService : ICSReportData
    {
        private readonly ICSReportRepository csReportRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="CSReportService"/> class.
        /// </summa/ry>
        /// <param name="csReportRepository">csReportRepository.</param>
        public CSReportService(ICSReportRepository csReportRepository)
        {
            this.csReportRepository = csReportRepository;
        }

        /// <summary>
        /// DtoUserCount.
        /// </summary>
        /// <returns>List.</returns>
        public List<DtoUserCount> GetTotalUsersCount()
        {
            return this.csReportRepository.GetTotalUsersCount();
        }

        /// <summary>
        /// DtoUserCount.
        /// </summary>
        /// <returns>List.</returns>
        public List<DtoUserCount> GetUsersLoggedInCount()
        {
            return this.csReportRepository.GetUsersLoggedInCount();
        }

        /// <summary>
        /// DtoUserCount.
        /// </summary>
        /// <returns>List.</returns>
        public List<DtoUserCount> GetUsersNotLoggedInCount()
        {
            return this.csReportRepository.GetUsersNotLoggedInCount();
        }

        /// <summary>
        /// DtoBihanStats.
        /// </summary>
        /// <returns>List.</returns>
        public List<DtoBihanStats> GetBihanStats()
        {
            return this.csReportRepository.GetBihanStats();
        }

        /// <summary>
        /// NotLoggedInUserList.
        /// </summary>
        /// <returns>List.</returns>
        public List<NotLoggedInUserList> GetNotLoggedInUserList()
        {
            return this.csReportRepository.GetNotLoggedInUserList();
        }

        /// <summary>
        /// ACNotSubmittedTargetList.
        /// </summary>
        /// <returns>List.</returns>
        public List<ACNotSubmittedTargetList> GetACNotSubmittedTargetList()
        {
            return this.csReportRepository.GetACNotSubmittedTargetList();
        }

        /// <summary>
        /// GetCSVReports.
        /// </summary>
        /// <returns>List.</returns>
        public dynamic GetCSVReports(string id)
        {
            return this.csReportRepository.GetCSVReports(id);
        }

        /// <summary>
        /// CSReportConsolidated.
        /// </summary>
        /// <returns>List.</returns>
        public CSReportConsolidated GetConsolidatedCSReports()
        {
            return this.csReportRepository.GetConsolidatedCSReports();
        }
    }
}
