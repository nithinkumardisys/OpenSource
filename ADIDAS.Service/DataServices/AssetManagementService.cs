//------------------------------------------------------------------------------
// <copyright file="AssetManagementService.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------

namespace ADIDAS.Service.DataServices
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using ADIDAS.Model.DTO;
    using ADIDAS.Model.Entities;
    using ADIDAS.Repository.Interfaces;
    using ADIDAS.Service.Interfaces;

    /// <summary>
    /// Asset Management Service.
    /// </summary>
    public class AssetManagementService : IAssetManagementService
    {
        private readonly IAssetManagementRepository assetManagementRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="AssetManagementService"/> class.
        /// </summary>
        /// <param name="assetManagementRepository">assetManagementRepository.</param>
        public AssetManagementService(IAssetManagementRepository assetManagementRepository)
        {
            this.assetManagementRepository = assetManagementRepository;
        }

        /// <summary>
        /// Get Agri Structures.
        /// </summary>
        /// <returns>List Agri Structure.</returns>
        public List<AgriStructure> GetAgriStructures()
        {
            return this.assetManagementRepository.GetAgriStructures();
        }

        /// <summary>
        /// Get All Farm Machineries.
        /// </summary>
        /// <returns>List AgriMachinery.</returns>
        public List<AgriMachinery> GetAllFarmMachineries()
        {
            return this.assetManagementRepository.GetAllFarmMachineries();
        }

        /// <summary>
        /// GetReportStucture.
        /// </summary>
        /// <param name="panchayat_id">panchayat_id.</param>
        /// <param name="structure_ID">structure_ID.</param>
        /// <returns>InsReportStructureModel.</returns>
        public List<InsReportStructureModel> GetReportStucture(int panchayat_id, int structure_ID)
        {
            return this.assetManagementRepository.GetReportStucture(panchayat_id, structure_ID);
        }

        /// <summary>
        /// GetLatestQtyByMachineryName.
        /// </summary>
        /// <param name="panchayat_id">panchayat_id.</param>
        /// <returns>Machinery.</returns>
        public List<Machinery> GetLatestQtyByMachineryName(int panchayat_id)
        {
            return this.assetManagementRepository.GetLatestQtyByMachineryName(panchayat_id);
        }

        /// <summary>
        /// GetViewLatestSubFarmMachinery.
        /// </summary>
        /// <param name="panchayat_id">panchayat_id.</param>
        /// <returns>Machinery.</returns>
        public List<Machinery> GetViewLatestSubFarmMachinery(int panchayat_id)
        {
            return this.assetManagementRepository.GetViewLatestSubFarmMachinery(panchayat_id);
        }

        /// <summary>
        /// GetOfflineFacilityDetails.
        /// </summary>
        /// <param name="panchayat_id">panchayat_id.</param>
        /// <returns>InsReportStructureModel.</returns>
        public List<InsReportStructureModel> GetOfflineFacilityDetails(int panchayat_id)
        {
            return this.assetManagementRepository.GetOfflineFacilityDetails(panchayat_id);
        }

        /// <summary>
        /// PostAgriAsset.
        /// </summary>
        /// <param name="insAgriAssetModel">insAgriAssetModel.</param>
        /// <returns>int.</returns>
        public int PostAgriAsset(InsAgriAssetModel insAgriAssetModel)
        {
            return this.assetManagementRepository.PostAgriAsset(insAgriAssetModel);
        }

        /// <summary>
        /// PostReportfarmMachinery.
        /// </summary>
        /// <param name="reportfarmMachineryModel">reportfarmMachineryModel.</param>
        /// <returns>int.</returns>
        public int PostReportfarmMachinery(List<ReportfarmMachineryModel> reportfarmMachineryModel)
        {
            return this.assetManagementRepository.PostReportfarmMachinery(reportfarmMachineryModel);
        }

        /// <summary>
        /// PostReportStrcture.
        /// </summary>
        /// <param name="insReportStructureModel">insReportStructureModel.</param>
        /// <returns>int.</returns>
        public int PostReportStrcture(InsReportStructureModel insReportStructureModel)
        {
            return this.assetManagementRepository.PostReportStrcture(insReportStructureModel);
        }

        /// <summary>
        /// GetAgriAssetNoFacilityData.
        /// </summary>
        /// <param name="panchayat_id">panchayat_id.</param>
        /// <returns>AgriAssetNoFacilityData.</returns>
        public List<AgriAssetNoFacilityData> GetAgriAssetNoFacilityData(int panchayat_id)
        {
            return this.assetManagementRepository.GetAgriAssetNoFacilityData(panchayat_id);
        }
    }
}
