//------------------------------------------------------------------------------
// <copyright file="IAssetManagementService.cs" company="Government of Bihar">
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

    public interface IAssetManagementService
    {
        List<Machinery> GetViewLatestSubFarmMachinery(int panchayat_id);

        List<Machinery> GetLatestQtyByMachineryName(int panchayat_id);

        List<AgriStructure> GetAgriStructures();

        List<AgriMachinery> GetAllFarmMachineries();

        List<InsReportStructureModel> GetReportStucture(int panchayat_id, int structure_ID);

        int PostAgriAsset(InsAgriAssetModel insAgriAssetModel);

        int PostReportfarmMachinery(List<ReportfarmMachineryModel> reportfarmMachineryModel);

        int PostReportStrcture(InsReportStructureModel insReportStructureModel);

        List<InsReportStructureModel> GetOfflineFacilityDetails(int panchayat_id);

        List<AgriAssetNoFacilityData> GetAgriAssetNoFacilityData(int panchayat_id);
    }
}
