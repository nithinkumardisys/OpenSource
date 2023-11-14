//------------------------------------------------------------------------------
// <copyright file="IAssetManagementRepository.cs" company="Government of Bihar">
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
    /// IAssetManagementRepository.
    /// </summary>
    public interface IAssetManagementRepository
    {
        /// <summary>
        /// GetViewLatestSubFarmMachinery.
        /// </summary>
        /// <param name="panchayat_id">panchayat_id.</param>
        /// <returns>Machinery.</returns>
        List<Machinery> GetViewLatestSubFarmMachinery(int panchayat_id);

        /// <summary>
        /// GetLatestQtyByMachineryName.
        /// </summary>
        /// <param name="panchayat_id">panchayat_id.</param>
        /// <returns>Machinery.</returns>
        List<Machinery> GetLatestQtyByMachineryName(int panchayat_id);

        /// <summary>
        /// GetOfflineFacilityDetails.
        /// </summary>
        /// <param name="panchayat_id">panchayat_id.</param>
        /// <returns>InsReportStructureModel.</returns>
        List<InsReportStructureModel> GetOfflineFacilityDetails(int panchayat_id);

        /// <summary>
        /// GetAgriStructures.
        /// </summary>
        /// <returns>AgriStructure.</returns>
        List<AgriStructure> GetAgriStructures();

        /// <summary>
        /// GetAllFarmMachineries.
        /// </summary>
        /// <returns>AgriMachinery.</returns>
        List<AgriMachinery> GetAllFarmMachineries();

        /// <summary>
        /// GetReportStucture.
        /// </summary>
        /// <param name="panchayat_id">panchayat_id.</param>
        /// <param name="structure_ID">structure_ID.</param>
        /// <returns>InsReportStructureModel.</returns>
        List<InsReportStructureModel> GetReportStucture(int panchayat_id, int structure_ID);

        /// <summary>
        /// PostAgriAsset.
        /// </summary>
        /// <param name="insAgriAssetModel">insAgriAssetModel.</param>
        /// <returns>Response.</returns>
        int PostAgriAsset(InsAgriAssetModel insAgriAssetModel);

        /// <summary>
        /// PostReportfarmMachinery.
        /// </summary>
        /// <param name="reportfarmMachineryModel">reportfarmMachineryModel.</param>
        /// <returns>Response.</returns>
        int PostReportfarmMachinery(List<ReportfarmMachineryModel> reportfarmMachineryModel);

        /// <summary>
        /// PostReportStrcture.
        /// </summary>
        /// <param name="insReportStructureModel">insReportStructureModel.</param>
        /// <returns>Response.</returns>
        int PostReportStrcture(InsReportStructureModel insReportStructureModel);

        /// <summary>
        /// GetAgriAssetNoFacilityData.
        /// </summary>
        /// <param name="block_id">block_id.</param>
        /// <returns>AgriAssetNoFacilityData.</returns>
        List<AgriAssetNoFacilityData> GetAgriAssetNoFacilityData(int block_id);
    }
}
