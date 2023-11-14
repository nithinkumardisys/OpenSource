//------------------------------------------------------------------------------
// <copyright file="IBavasService.cs" company="Government of Bihar">
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
    /// IBavasService.
    /// </summary>
    public interface IBavasService
    {
        /// <summary>
        /// InsertNewNode.
        /// </summary>
        /// <param name="node">node.</param>
        /// <returns>integer.</returns>
        int InsertNewNode(Node node);

        /// <summary>
        /// InsertBavasStructure.
        /// </summary>
        /// <param name="structureBavas">structureBavas.</param>
        /// <returns>integer.</returns>
        int InsertBavasStructure(List<StructureBavas> structureBavas);

        /// <summary>
        /// InsertBavasCropsIntelligence.
        /// </summary>
        /// <param name="intelligenceCrops">intelligenceCrops.</param>
        /// <returns>integer.</returns>
        int InsertBavasCropsIntelligence(List<BavasIntelligenceCrops> intelligenceCrops);

        /// <summary>
        /// GetUnitofMeasure.
        /// </summary>
        /// <returns>MeasureBavas list.</returns>
        List<MeasureBavas> GetUnitofMeasure();

        /// <summary>
        /// GetAllPriceIntelligenceCrops.
        /// </summary>
        /// <param name="districtId">districtId.</param>
        /// <returns>DtoPriceIntelligenceCrops list.</returns>
        List<DtoPriceIntelligenceCrops> GetAllPriceIntelligenceCrops(int districtId);

        /// <summary>
        /// GetPriceIntelligenceDetailsforPastSevenDays.
        /// </summary>
        /// <param name="panchayatId">panchayatId.</param>
        /// <returns>PriceIntelligenceDetails list.</returns>
        List<PriceIntelligenceDetails> GetPriceIntelligenceDetailsforPastSevenDays(int panchayatId);

        /// <summary>
        /// GetAllNodesByDistrictId.
        /// </summary>
        /// <param name="districtId">districtId.</param>
        /// <returns>DtoNodes list.</returns>
        List<DtoNodes> GetAllNodesByDistrictId(int districtId);

        /// <summary>
        /// GetAllStructureByDistrictId.
        /// </summary>
        /// <param name="districtId">districtId.</param>
        /// <returns>DtoStructure list.</returns>
        List<DtoStructure> GetAllStructureByDistrictId(int districtId);

        /// <summary>
        /// PostBavasContractForming.
        /// </summary>
        /// <param name="insBavasContractfarming">insBavasContractfarming.</param>
        /// <returns>integer.</returns>
        int PostBavasContractForming(List<InsBavasContractfarming> insBavasContractfarming);

        /// <summary>
        /// InsertBavasPriceIntelligence.
        /// </summary>
        /// <param name="intelligencePrice">intelligencePrice.</param>
        /// <returns>integer.</returns>
        int InsertBavasPriceIntelligence(List<DtoPriceIntelligenceInsert> intelligencePrice);

        /// <summary>
        /// GetAgriHortiCrops.
        /// </summary>
        /// <returns>AgriHortiCrops list.</returns>
        List<AgriHortiCrops> GetAgriHortiCrops();

        /// <summary>
        /// GetAllFarmerProducerOrgByPanchayat.
        /// </summary>
        /// <param name="panchayatid">panchayatid.</param>
        /// <returns>DtoFpo list.</returns>
        List<DtoFpo> GetAllFarmerProducerOrgByPanchayat(int panchayatid);

        /// <summary>
        /// GetSpecificFarmerProducerOrg.
        /// </summary>
        /// <param name="fpoId">fpoId.</param>
        /// <returns>DtoFpoCrops.</returns>
        DtoFpoCrops GetSpecificFarmerProducerOrg(int fpoId);

        /// <summary>
        /// GetAllContractFarmingDetails.
        /// </summary>
        /// <param name="panchaytId">panchaytId.</param>
        /// <param name="seasonId">seasonId.</param>
        /// <returns>DtoFarmerContractingDetails list.</returns>
        List<DtoFarmerContractingDetails> GetAllContractFarmingDetails(int panchaytId, int seasonId);

        /// <summary>
        /// GetSpecificContractFarmingDetail.
        /// </summary>
        /// <param name="regno">regno.</param>
        /// <returns>DtoFarmerContractingDetails.</returns>
        DtoFarmerContractingDetails GetSpecificContractFarmingDetail(string regno);

        /// <summary>
        /// GETSEASONALCropsByDistrictId.
        /// </summary>
        /// <param name="districtId">districtId.</param>
        /// <returns>DtoSeasonalCrops list.</returns>
        List<DtoSeasonalCrops> GETSEASONALCropsByDistrictId(int districtId);

        /// <summary>
        /// InsertBavasfarmerproducerOrg.
        /// </summary>
        /// <param name="farmerProduceOrgs">farmerProduceOrgs.</param>
        /// <returns>FpoResponse.</returns>
        List<FpoResponse> InsertBavasfarmerproducerOrg(List<FarmerProduceOrg> farmerProduceOrgs);

        /// <summary>
        /// GetAllFacilityDetails.
        /// </summary>
        /// <param name="structureId">structureId.</param>
        /// <param name="panchayatId">panchayatId.</param>
        /// <returns>DtoFacilityDetails list.</returns>
        List<DtoFacilityDetails> GetAllFacilityDetails(int structureId, int panchayatId);

        /// <summary>
        /// GetDirectorMarketingInfraStructure.
        /// </summary>
        /// <param name="structureId">structureId.</param>
        /// <param name="panchayatId">panchayatId.</param>
        /// <returns>MarkettingInfrastructure list.</returns>
        List<MarkettingInfrastructure> GetDirectorMarketingInfraStructure(int structureId, int panchayatId);

        /// <summary>
        /// InsertBavasMarkettingInfra.
        /// </summary>
        /// <param name="markettingInfra">markettingInfra.</param>
        /// <returns>integer.</returns>
        int InsertBavasMarkettingInfra(List<DtoMarketingInfra> markettingInfra);

        /// <summary>
        /// GetDirectorMarketingInfraStructureByDistrict.
        /// </summary>
        /// <param name="structureId">structureId.</param>
        /// <param name="districtId">districtId.</param>
        /// <returns>MarkettingInfrastructure list.</returns>
        List<MarkettingInfrastructure> GetDirectorMarketingInfraStructureByDistrict(int structureId, int districtId);

        /// <summary>
        /// GetMarketingInfoNoFacilityData.
        /// </summary>
        /// <param name="block_id">block_id.</param>
        /// <returns>MarketInfoData list.</returns>
        List<MarketInfoData> GetMarketingInfoNoFacilityData(int block_id);
    }
}
