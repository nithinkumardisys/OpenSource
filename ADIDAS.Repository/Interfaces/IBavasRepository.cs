//------------------------------------------------------------------------------
// <copyright file="IBavasRepository.cs" company="Government of Bihar">
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
    /// IBavasRepository.
    /// </summary>
    public interface IBavasRepository
    {
        /// <summary>
        /// InsertNewNode.
        /// </summary>
        /// <param name="node">node.</param>
        /// <returns>Response.</returns>
        int InsertNewNode(Node node);

        /// <summary>
        /// InsertBavasStructure.
        /// </summary>
        /// <param name="structureBavas">structureBavas.</param>
        /// <returns>Response.</returns>
        int InsertBavasStructure(List<StructureBavas> structureBavas);

        /// <summary>
        /// InsertBavasCropsIntelligence.
        /// </summary>
        /// <param name="intelligenceCrops">intelligenceCrops.</param>
        /// <returns>Response.</returns>
        int InsertBavasCropsIntelligence(List<BavasIntelligenceCrops> intelligenceCrops);

        /// <summary>
        /// GetUnitofMeasure.
        /// </summary>
        /// <returns>MeasureBavas.</returns>
        List<MeasureBavas> GetUnitofMeasure();

        /// <summary>
        /// GetAllPriceIntelligenceCrops.
        /// </summary>
        /// <param name="districtId">districtId.</param>
        /// <returns>DtoPriceIntelligenceCrops.</returns>
        List<DtoPriceIntelligenceCrops> GetAllPriceIntelligenceCrops(int districtId);

        /// <summary>
        /// GetPriceIntelligenceDetailsforPastSevenDays.
        /// </summary>
        /// <param name="panchayatId">panchayatId.</param>
        /// <returns>PriceIntelligenceDetails.</returns>
        List<PriceIntelligenceDetails> GetPriceIntelligenceDetailsforPastSevenDays(int panchayatId);

        /// <summary>
        /// GetAllNodesByDistrictId.
        /// </summary>
        /// <param name="districtId">districtId.</param>
        /// <returns>DtoNodes.</returns>
        List<DtoNodes> GetAllNodesByDistrictId(int districtId);

        /// <summary>
        /// PostBavasContractForming.
        /// </summary>
        /// <param name="insBavasContractfarmings">insBavasContractfarmings.</param>
        /// <returns>Response.</returns>
        int PostBavasContractForming(List<InsBavasContractfarming> insBavasContractfarmings);

        /// <summary>
        /// GetAllStructureByDistrictId.
        /// </summary>
        /// <param name="districtId">districtId.</param>
        /// <returns>DtoStructure.</returns>
        List<DtoStructure> GetAllStructureByDistrictId(int districtId);

        /// <summary>
        /// InsertBavasPriceIntelligence.
        /// </summary>
        /// <param name="intelligencePrice">intelligencePrice.</param>
        /// <returns>Response.</returns>
        int InsertBavasPriceIntelligence(List<DtoPriceIntelligenceInsert> intelligencePrice);

        /// <summary>
        /// GetAgriHortiCrops.
        /// </summary>
        /// <returns>AgriHortiCrops.</returns>
        List<AgriHortiCrops> GetAgriHortiCrops();

        /// <summary>
        /// GetAllFarmerProducerOrgByPanchayat.
        /// </summary>
        /// <param name="panchayatid">panchayatid.</param>
        /// <returns>DtoFpo.</returns>
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
        /// <returns>DtoFarmerContractingDetails.</returns>
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
        /// <returns>DtoSeasonalCrops.</returns>
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
        /// <returns>DtoFacilityDetails.</returns>
        List<DtoFacilityDetails> GetAllFacilityDetails(int structureId, int panchayatId);

        /// <summary>
        /// GetDirectorMarketingInfraStructure.
        /// </summary>
        /// <param name="structureId">structureId.</param>
        /// <param name="panchayatId">panchayatId.</param>
        /// <returns>MarkettingInfrastructure.</returns>
        List<MarkettingInfrastructure> GetDirectorMarketingInfraStructure(int structureId, int panchayatId);

        /// <summary>
        /// InsertBavasMarkettingInfra.
        /// </summary>
        /// <param name="markettingInfra">markettingInfra.</param>
        /// <returns>Response.</returns>
        int InsertBavasMarkettingInfra(List<DtoMarketingInfra> markettingInfra);

        /// <summary>
        /// GetDirectorMarketingInfraStructureByDistrict.
        /// </summary>
        /// <param name="structureId">structureId.</param>
        /// <param name="districtId">districtId.</param>
        /// <returns>MarkettingInfrastructure.</returns>
        List<MarkettingInfrastructure> GetDirectorMarketingInfraStructureByDistrict(int structureId, int districtId);

        /// <summary>
        /// GetMarketingInfoNoFacilityData.
        /// </summary>
        /// <param name="blockid">blockid.</param>
        /// <returns>MarketInfoData.</returns>
        List<MarketInfoData> GetMarketingInfoNoFacilityData(int blockid);
    }
}
