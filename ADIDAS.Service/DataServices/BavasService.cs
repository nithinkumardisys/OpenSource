//------------------------------------------------------------------------------
// <copyright file="BavasService.cs" company="Government of Bihar">
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

    public class BavasService : IBavasService
    {
        private readonly IBavasRepository bavasRepository;

        public BavasService(IBavasRepository bavasRepository)
        {
            this.bavasRepository = bavasRepository;
        }

        public List<AgriHortiCrops> GetAgriHortiCrops()
        {
            return this.bavasRepository.GetAgriHortiCrops();
        }

        public List<DtoFarmerContractingDetails> GetAllContractFarmingDetails(int panchaytId, int seasonId)
        {
            return this.bavasRepository.GetAllContractFarmingDetails(panchaytId, seasonId);
        }

        public List<DtoFpo> GetAllFarmerProducerOrgByPanchayat(int panchayatid)
        {
            return this.bavasRepository.GetAllFarmerProducerOrgByPanchayat(panchayatid);
        }

        public List<DtoNodes> GetAllNodesByDistrictId(int districtId)
        {
            return this.bavasRepository.GetAllNodesByDistrictId(districtId);
        }

        public List<DtoPriceIntelligenceCrops> GetAllPriceIntelligenceCrops(int districtId)
        {
            return this.bavasRepository.GetAllPriceIntelligenceCrops(districtId);
        }

        public List<DtoStructure> GetAllStructureByDistrictId(int districtId)
        {
            return this.bavasRepository.GetAllStructureByDistrictId(districtId);
        }

        public int PostBavasContractForming(List<InsBavasContractfarming> insBavasContractfarming)
        {
            return this.bavasRepository.PostBavasContractForming(insBavasContractfarming);
        }

        public List<PriceIntelligenceDetails> GetPriceIntelligenceDetailsforPastSevenDays(int panchayatId)
        {
            return this.bavasRepository.GetPriceIntelligenceDetailsforPastSevenDays(panchayatId);
        }

        public List<DtoSeasonalCrops> GETSEASONALCropsByDistrictId(int districtId)
        {
            return this.bavasRepository.GETSEASONALCropsByDistrictId(districtId);
        }

        public DtoFarmerContractingDetails GetSpecificContractFarmingDetail(string regno)
        {
            return this.bavasRepository.GetSpecificContractFarmingDetail(regno);
        }

        public DtoFpoCrops GetSpecificFarmerProducerOrg(int fpoId)
        {
            return this.bavasRepository.GetSpecificFarmerProducerOrg(fpoId);
        }

        public List<MeasureBavas> GetUnitofMeasure()
        {
            return this.bavasRepository.GetUnitofMeasure();
        }

        public int InsertBavasCropsIntelligence(List<BavasIntelligenceCrops> intelligenceCrops)
        {
            return this.bavasRepository.InsertBavasCropsIntelligence(intelligenceCrops);
        }

        public List<FpoResponse> InsertBavasfarmerproducerOrg(List<FarmerProduceOrg> farmerProduceOrgs)
        {
            return this.bavasRepository.InsertBavasfarmerproducerOrg(farmerProduceOrgs);
        }

        public int InsertBavasPriceIntelligence(List<DtoPriceIntelligenceInsert> intelligencePrice)
        {
            return this.bavasRepository.InsertBavasPriceIntelligence(intelligencePrice);
        }

        public int InsertBavasStructure(List<StructureBavas> structureBavas)
        {
            return this.bavasRepository.InsertBavasStructure(structureBavas);
        }

        public int InsertNewNode(Node node)
        {
            return this.bavasRepository.InsertNewNode(node);
        }

        public List<DtoFacilityDetails> GetAllFacilityDetails(int structureId, int panchayatId)
        {
            return this.bavasRepository.GetAllFacilityDetails(structureId, panchayatId);
        }

        public List<MarkettingInfrastructure> GetDirectorMarketingInfraStructure(int structureId, int panchayatId)
        {
            return this.bavasRepository.GetDirectorMarketingInfraStructure(structureId, panchayatId);
        }

        public int InsertBavasMarkettingInfra(List<DtoMarketingInfra> markettingInfra)
        {
            return this.bavasRepository.InsertBavasMarkettingInfra(markettingInfra);
        }

        public List<MarkettingInfrastructure> GetDirectorMarketingInfraStructureByDistrict(int structureId, int districtId)
        {
            return this.bavasRepository.GetDirectorMarketingInfraStructureByDistrict(structureId, districtId);
        }

        public List<MarketInfoData> GetMarketingInfoNoFacilityData(int block_id)
        {
            return this.bavasRepository.GetMarketingInfoNoFacilityData(block_id);
        }
    }
}
