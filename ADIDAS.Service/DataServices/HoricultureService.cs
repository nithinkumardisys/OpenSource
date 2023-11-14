//------------------------------------------------------------------------------
// <copyright file="HoricultureService.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------

namespace ADIDAS.Service.DataServices
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using ADIDAS.Model.DTO;
    using ADIDAS.Model.Entities;
    using ADIDAS.Repository.Interfaces;
    using ADIDAS.Service.Interfaces;

    public class HoricultureService : IHorticultureService
    {
        private readonly IHoricultureRepository horicultureRepository;

        public HoricultureService(IHoricultureRepository horicultureRepository)
        {
            this.horicultureRepository = horicultureRepository;
        }

        public List<GetAllColdStorage> GetAllColdStorages(int district_Id)
        {
            return this.horicultureRepository.GetAllColdStorages(district_Id);
        }

        public List<GetAllColdStorage> GetAllColdStoragesOffline()
        {
            return this.horicultureRepository.GetAllColdStoragesOffline();
        }

        public List<CropDamageBlockApprovalResponse> InsertCropDamagePanchtApproval(CropDamage cropDamage)
        {
            return this.horicultureRepository.InsertHortDamageApproval(cropDamage);
        }

        public List<GetAllCropColdStorage> GetAllCropsColdStorage(int district_Id)
        {
            return this.horicultureRepository.GetAllCropsColdStorage(district_Id);
        }

        public List<GetAllCropColdStorageId> GetColdStoragesByStrgID(int crop_id, int? district_Id, int strg_ID)
        {
            return this.horicultureRepository.GetColdStoragesByStrgID(crop_id, district_Id, strg_ID);
        }

        public List<GetAllCropColdStorageId> GetColdStoragesByStrgIDOffline()
        {
            return this.horicultureRepository.GetColdStoragesByStrgIDOffline();
        }

        public List<CropDamageBlockApprovalResponse> InsertHortDamageApproval(CropDamage cropDamage)
        {
            return this.horicultureRepository.InsertHortDamageApproval(cropDamage);
        }

        public List<CropDamagePancht> GetHortDamagePancht(string seasonId, string panchayatId)
        {
            return this.horicultureRepository.GetHortDamagePancht(seasonId, panchayatId);
        }

        public List<CropDamageBlock> GetHortDamageBlock(string blockId)
        {
            return this.horicultureRepository.GetHortDamageBlock(blockId);
        }

        public List<LstCpldStorageDets> GetLstEgtWksColdStorages(int district_Id)
        {
            return this.horicultureRepository.GetLstEgtWksColdStorages(district_Id);
        }

        public List<CropDamagePanchayatResponse> InsertHortDamagePancht(List<CropDamagePancht> crop)
        {
            return this.horicultureRepository.InsertHortDamagePancht(crop);
        }

        public int InsertColdStorage(ColdStorage coldStorage)
        {
            return this.horicultureRepository.InsertColdStorage(coldStorage);
        }

        public int InsertColdStorageDetails(ColdStorageDetails coldStorageDetails)
        {
            return this.horicultureRepository.InsertColdStorageDetails(coldStorageDetails);
        }

        public int InsertPHMSFacility(PhmsDetails phmsDetails)
        {
            return this.horicultureRepository.InsertPHMSFacility(phmsDetails);
        }

        public List<FacilityOnline> GetFacilitiesOnline(int districtId, int panchayatId, int structureId)
        {
            return this.horicultureRepository.GetFacilitiesOnline(districtId, panchayatId, structureId);
        }

        public List<FacilityOnline> GetFacilitiesOffline(int districtId, int panchayatId)
        {
            return this.horicultureRepository.GetFacilitiesOffline(districtId, panchayatId);
        }

        public List<HortCropCoverageTargetPanchytApprovalResponse> InsertHortCropCoveragetargetPanchayat(List<HortCropCoverageAimPancht> crop)
        {
            return this.horicultureRepository.InsertHortCropCoveragetargetPanchayat(crop);
        }

        public List<HortiCropCoverageTargetBlockApproval> InsertHortCropCoveragetargetPanchayatApproval(HortCropCoverageAim cropCoverageTarget)
        {
            return this.horicultureRepository.InsertHortCropCoveragetargetPanchayatApproval(cropCoverageTarget);
        }

        public string InsertHortCropCoveragetargetBlock(HortCropCoverageTargetBlockApproval hortCrpCvgTgtBlock)
        {
            return this.horicultureRepository.InsertHortCropCoveragetargetBlock(hortCrpCvgTgtBlock);
        }

        public int InsertHortAggCropCoverageActual(HortAggCropCoverageActual hortAggCropCoverageActual)
        {
            return this.horicultureRepository.InsertHortAggCropCoverageActual(hortAggCropCoverageActual);
        }

        public int HortAutoApprovalCoverageActlBlock()
        {
            return this.horicultureRepository.HortAutoApprovalCoverageActlBlock();
        }

        public int HortAutoApprovalCoverageActlPnchyt()
        {
            return this.horicultureRepository.HortAutoApprovalCoverageActlPnchyt();
        }

        public List<HortCropCoverageActualPancht> GetHortCropCoverageActualPancht(string seasonId, string panchayatId)
        {
            return this.horicultureRepository.GetHortCropCoverageActualPancht(seasonId, panchayatId);
        }

        public List<HortCropCoverageActualPancht> GetHortCropCoverageActualPanchtDelta(string seasonId, string panchayatId, DateTime lastrefreshedTime)
        {
            return this.horicultureRepository.GetHortCropCoverageActualPanchtDelta(seasonId, panchayatId, lastrefreshedTime);
        }

        public List<HortCropCoverageActualPancht> GetHortCropCoverageActualPanchtCurrDate(string seasonId, string panchayatId)
        {
            return this.horicultureRepository.GetHortCropCoverageActualPanchtCurrDate(seasonId, panchayatId);
        }

        public List<HortCropCoverageActualBlock> GetHortCropCoverageActualBlock(string blockId)
        {
            return this.horicultureRepository.GetHortCropCoverageActualBlock(blockId);
        }

        public List<HortCropCoverageActualDistrict> GetHortCropCoverageActualDistrict(string districtId)
        {
            return this.horicultureRepository.GetHortCropCoverageActualDistrict(districtId);
        }

        public List<HortCropCoverageActual> GetHortCropCoverageActualADHDelta(int district_id, int season_id, DateTime last_refresh_time)
        {
            return this.horicultureRepository.GetHortCropCoverageActualADHDelta(district_id, season_id, last_refresh_time);
        }

        public List<HortCropCoverageActual> GetHortCropCoverageActualBHODelta(int district_id, int block_id, int season_id, DateTime last_refresh_time)
        {
            return this.horicultureRepository.GetHortCropCoverageActualBHODelta(district_id, block_id, season_id, last_refresh_time);
        }

        public List<HortCropCoverageActual> GetHortCropCoverageActualBHOOffline(int district_id, int block_id, int season_id, string crop_ids)
        {
            return this.horicultureRepository.GetHortCropCoverageActualBHOOffline(district_id, block_id, season_id, crop_ids);
        }

        public List<HortCropCoverageActual> GetHortCropCoverageActualADHOffline(int district_id, int season_id, string crop_ids)
        {
            return this.horicultureRepository.GetHortCropCoverageActualADHOffline(district_id, season_id, crop_ids);
        }

        public HortCropCoverageActual GetHortCropCoverageActualADH(int district_id, int season_id, int crop_id)
        {
            return this.horicultureRepository.GetHortCropCoverageActualADH(district_id, season_id, crop_id);
        }

        public HortCropCoverageActual GetHortCropCoverageActualBHO(int district_id, int block_id, int season_id, int crop_id)
        {
            return this.horicultureRepository.GetHortCropCoverageActualBHO(district_id, block_id, season_id, crop_id);
        }

        public int HortAutoCropCvrgActlDataCorrection()
        {
            return this.horicultureRepository.HortAutoCropCvrgActlDataCorrection();
        }

        public List<CropCoverageActualBlockApprovalResponse> InsertHortCropCoverageActualApproval(HortCropCoverageActual cropCoverageActual)
        {
            return this.horicultureRepository.InsertHortCropCoverageActualApproval(cropCoverageActual);
        }

        public List<CropCoverageActualPanchayatApprovalResponse> InsertHortCropCoverageActualPancht(List<HortCropCoverageActualPancht> crop)
        {
            return this.horicultureRepository.InsertHortCropCoverageActualPancht(crop);
        }

        public List<CropCoverageActualBlockApprovalResponse> InsertHortCropCoverageActualPanchayatApproval(HortCropCoverageActual cropCoverageActual)
        {
            return this.horicultureRepository.InsertHortCropCoverageActualPanchayatApproval(cropCoverageActual);
        }

        public List<GetStructure> GetAllStructure(int district_Id)
        {
            return this.horicultureRepository.GetAllStructure(district_Id);
        }

        public int InsertPHMSStructure(PhmsStructure pHMSStructure)
        {
            return this.horicultureRepository.InsertPHMSStructure(pHMSStructure);
        }

        public int InsertHortNewCrop(HortNewCrop hortNewCrop)
        {
            return this.horicultureRepository.InsertHortNewCrop(hortNewCrop);
        }

        public List<DistinctHorticultureCrop> GetDistinctHorticultureCrop(int district_id)
        {
            return this.horicultureRepository.GetDistinctHorticultureCrop(district_id);
        }

        public List<HortProduceSeasonResponse> GetHortProduceSeason()
        {
            return this.horicultureRepository.GetHortProduceSeason();
        }

        public List<HortProducePanchayatResponse> GetHortProducePanchayat(int district_id, int block_id)
        {
            return this.horicultureRepository.GetHortProducePanchayat(district_id, block_id);
        }

        public List<HortProduceTran> GetHortProduceLatest(int crop_id, int district_id, int block_id, int panchayat_Id)
        {
            return this.horicultureRepository.GetHortProduceLatest(crop_id, district_id, block_id, panchayat_Id);
        }

        public List<CropCoverageTargetPanchytApprovalResponse> InsertHortProduceTranApproval(List<HortProduceTranApproval> hortProduceTranApprovals)
        {
            return this.horicultureRepository.InsertHortProduceTranApproval(hortProduceTranApprovals);
        }

        public List<CropCoverageActualBlockApprovalResponse> InsertHortCoverageActualApproval(HortProduceActlApproval cropCoverageActual)
        {
            return this.horicultureRepository.InsertHortCoverageActualApproval(cropCoverageActual);
        }

        public List<CropCoverageActualPanchayatApprovalResponse> InsertHortProduceActualPnchyt(List<HortProduceActualPanchayat> hortProduce)
        {
            return this.horicultureRepository.InsertHortProduceActualPnchyt(hortProduce);
        }

        public int HortProduceAutoApprovalActlBlock()
        {
            return this.horicultureRepository.HortProduceAutoApprovalActlBlock();
        }

        public int HortProduceAutoApprovalActlPanchayat()
        {
            return this.horicultureRepository.HortProduceAutoApprovalActlPanchayat();
        }

        public int HortProduceActlDataCorrection()
        {
            return this.horicultureRepository.HortProduceActlDataCorrection();
        }

        public List<HortProduceActualPanchayat> GetHortProduceActualPancht(int season_id, int panchayat_Id)
        {
            return this.horicultureRepository.GetHortProduceActualPancht(season_id, panchayat_Id);
        }

        public List<HortProduceActualPanchayat> GetHortProduceActualPanchtCurrDate(int season_id, int panchayat_Id)
        {
            return this.horicultureRepository.GetHortProduceActualPanchtCurrDate(season_id, panchayat_Id);
        }

        public List<HortProduceActualBlock> GetHortProduceActualBlock(int block_Id)
        {
            return this.horicultureRepository.GetHortProduceActualBlock(block_Id);
        }

        public List<string> GetHortiReportPHMS(GetHortiReportPhmsModel getHortiReportPHMS)
        {
            return this.horicultureRepository.GetHortiReportPHMS(getHortiReportPHMS);
        }

        public List<string> GetHortiReportColdStorage(GetHortiReportPhmsModel getHortiReportPHMS)
        {
            return this.horicultureRepository.GetHortiReportColdStorage(getHortiReportPHMS);
        }

        public List<GetSubmitProductivityModel> GetSubmitProductivity(int district_id, int season_id)
        {
            return this.horicultureRepository.GetSubmitProductivity(district_id, season_id);
        }

        public int PostAgriProductivity(List<PostSubmitProductivityModel> hortProduce)
        {
            return this.horicultureRepository.PostAgriProductivity(hortProduce);
        }

        public List<HortProduceActualDistrict> GetHortProduceActualDistrict(int district_Id)
        {
            return this.horicultureRepository.GetHortProduceActualDistrict(district_Id);
        }

        public List<GetHortProduceBholstModel> GetHortProduceBHO(int district_id, int block_id, int season_id, string panchyat_id)
        {
            return this.horicultureRepository.GetHortProduceBHO(district_id, block_id, season_id, panchyat_id);
        }

        public List<HortProduceCoverageActual> GetHortiProduceCoverageActualBHOOffline(int district_id, int block_id, int season_id, string crop_ids)
        {
            return this.horicultureRepository.GetHortiProduceCoverageActualBHOOffline(district_id, block_id, season_id, crop_ids);
        }

        public List<HortProduceCoverageActual> GetHortProduceActualADHOffline(int district_id, int season_id, string crop_ids)
        {
            return this.horicultureRepository.GetHortProduceActualADHOffline(district_id,  season_id, crop_ids);
        }

        public HortProduceCoverageActl GetHortProduceActualADH(int district_id, int season_id, string crop_ids)
        {
            return this.horicultureRepository.GetHortProduceActualADH(district_id, season_id, crop_ids);
        }

        public HortProduceCoverageActl GetHortProduceActualBHO(int district_id, int block_id, int season_id, string crop_ids)
        {
            return this.horicultureRepository.GetHortProduceActualBHO(district_id, block_id, season_id, crop_ids);
        }

        public List<SeedPerfHortdbt> GetAllSeedPerformanceHorticultureDBT(int district_id, int block_id, int panchayat_id, int season_id)
        {
            return this.horicultureRepository.GetAllSeedPerformanceHorticultureDBT(district_id, block_id, panchayat_id, season_id);
        }

        public DbtDistrictWrapper GetBlockPanchayatData(int district_id)
        {
            return this.horicultureRepository.GetBlockPanchayatData(district_id);
        }

        public async Task<int> InsertHortiSeedPerformance(InsertHorticultureSeedPerf seedPerformance)
        {
            return await this.horicultureRepository.InsertHortiSeedPerformance(seedPerformance);
        }

        public List<Varities> GetVarietiesByType(string type, int seasonid)
        {
            return this.horicultureRepository.GetVarietiesByType(type, seasonid);
        }

        public List<NoFacility> GetPHMSNoFacilitiesData(int districtId)
        {
            return this.horicultureRepository.GetPHMSNoFacilitiesData(districtId);
        }

        /// <summary>
        /// GetBlockPanchaytLgCodes.
        /// </summary>
        /// <param name="districtId">districtId.</param>
        /// <returns>BlockPanchayatLgCodes List.</returns>
        public List<BlockPanchayatLgCodes> GetBlockPanchaytLgCodes(int districtId)
        {
            return this.horicultureRepository.GetBlockPanchaytLgCodes(districtId);
        }
    }
}
