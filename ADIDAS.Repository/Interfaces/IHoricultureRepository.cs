//------------------------------------------------------------------------------
// <copyright file="IHoricultureRepository.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------
namespace ADIDAS.Repository.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ADIDAS.Model.DTO;
    using ADIDAS.Model.Entities;

    /// <summary>
    /// IHoricultureRepository.
    /// </summary>
    public interface IHoricultureRepository
    {
        /// <summary>
        /// InsertColdStorage.
        /// </summary>
        /// <param name="coldStorage">coldStorage.</param>
        /// <returns>Response.</returns>
        int InsertColdStorage(ColdStorage coldStorage);

        /// <summary>
        /// GetAllColdStorages.
        /// </summary>
        /// <param name="district_Id">district_Id.</param>
        /// <returns>GetAllColdStorage.</returns>
        List<GetAllColdStorage> GetAllColdStorages(int district_Id);

        /// <summary>
        /// GetAllColdStoragesOffline.
        /// </summary>
        /// <returns>GetAllColdStorage.</returns>
        List<GetAllColdStorage> GetAllColdStoragesOffline();

        /// <summary>
        /// GetAllCropsColdStorage.
        /// </summary>
        /// <param name="district_Id">district_Id.</param>
        /// <returns>GetAllCropColdStorage.</returns>
        List<GetAllCropColdStorage> GetAllCropsColdStorage(int district_Id);

        /// <summary>
        /// GetColdStoragesByStrgID.
        /// </summary>
        /// <param name="crop_id">crop_id.</param>
        /// <param name="district_Id">district_Id.</param>
        /// <param name="strg_ID">strg_ID.</param>
        /// <returns>GetAllCropColdStorageId.</returns>
        List<GetAllCropColdStorageId> GetColdStoragesByStrgID(int crop_id, int? district_Id, int strg_ID);

        /// <summary>
        /// GetColdStoragesByStrgIDOffline.
        /// </summary>
        /// <returns>GetAllCropColdStorageId.</returns>
        List<GetAllCropColdStorageId> GetColdStoragesByStrgIDOffline();

        /// <summary>
        /// GetLstEgtWksColdStorages.
        /// </summary>
        /// <param name="district_Id">district_Id.</param>
        /// <returns>LstCpldStorageDets.</returns>
        List<LstCpldStorageDets> GetLstEgtWksColdStorages(int district_Id);

        /// <summary>
        /// InsertHortDamageApproval.
        /// </summary>
        /// <param name="cropDamage">cropDamage.</param>
        /// <returns>CropDamageBlockApprovalResponse.</returns>
        List<CropDamageBlockApprovalResponse> InsertHortDamageApproval(CropDamage cropDamage);

        /// <summary>
        /// InsertHortDamagePancht.
        /// </summary>
        /// <param name="crop">crop.</param>
        /// <returns>CropDamagePanchayatResponse.</returns>
        List<CropDamagePanchayatResponse> InsertHortDamagePancht(List<CropDamagePancht> crop);

        /// <summary>
        /// GetHortDamagePancht.
        /// </summary>
        /// <param name="seasonId">seasonId.</param>
        /// <param name="panchayatId">panchayatId.</param>
        /// <returns>CropDamagePancht.</returns>
        List<CropDamagePancht> GetHortDamagePancht(string seasonId, string panchayatId);

        /// <summary>
        /// GetHortDamageBlock.
        /// </summary>
        /// <param name="blockId">blockId.</param>
        /// <returns>CropDamageBlock.</returns>
        List<CropDamageBlock> GetHortDamageBlock(string blockId);

        /// <summary>
        /// InsertColdStorageDetails.
        /// </summary>
        /// <param name="coldStorageDetails">coldStorageDetails.</param>
        /// <returns>Response.</returns>
        int InsertColdStorageDetails(ColdStorageDetails coldStorageDetails);

        /// <summary>
        /// InsertPHMSFacility.
        /// </summary>
        /// <param name="phmsDetails">phmsDetails.</param>
        /// <returns>Response.</returns>
        int InsertPHMSFacility(PhmsDetails phmsDetails);

        /// <summary>
        /// GetFacilitiesOnline.
        /// </summary>
        /// <param name="districtId">districtId.</param>
        /// <param name="panchayatId">panchayatId.</param>
        /// <param name="structureId">structureId.</param>
        /// <returns>FacilityOnline.</returns>
        public List<FacilityOnline> GetFacilitiesOnline(int districtId, int panchayatId, int structureId);

        /// <summary>
        /// GetFacilitiesOffline.
        /// </summary>
        /// <param name="districtId">districtId.</param>
        /// <param name="panchayatId">panchayatId.</param>
        /// <returns>FacilityOnline.</returns>
        public List<FacilityOnline> GetFacilitiesOffline(int districtId, int panchayatId);

        /// <summary>
        /// InsertHortCropCoveragetargetPanchayat.
        /// </summary>
        /// <param name="crop">crop.</param>
        /// <returns>HortCropCoverageTargetPanchytApprovalResponse.</returns>
        List<HortCropCoverageTargetPanchytApprovalResponse> InsertHortCropCoveragetargetPanchayat(List<HortCropCoverageAimPancht> crop);

        /// <summary>
        /// InsertHortCropCoveragetargetPanchayatApproval.
        /// </summary>
        /// <param name="cropCoverageTarget">cropCoverageTarget.</param>
        /// <returns>HortiCropCoverageTargetBlockApproval.</returns>
        List<HortiCropCoverageTargetBlockApproval> InsertHortCropCoveragetargetPanchayatApproval(HortCropCoverageAim cropCoverageTarget);

        /// <summary>
        /// InsertHortCropCoveragetargetBlock.
        /// </summary>
        /// <param name="hortCrpCvgTgtBlock">hortCrpCvgTgtBlock.</param>
        /// <returns>Response.</returns>
        string InsertHortCropCoveragetargetBlock(HortCropCoverageTargetBlockApproval hortCrpCvgTgtBlock);

        /// <summary>
        /// InsertHortAggCropCoverageActual.
        /// </summary>
        /// <param name="hortAggCropCoverageActual">hortAggCropCoverageActual.</param>
        /// <returns>Response.</returns>
        int InsertHortAggCropCoverageActual(HortAggCropCoverageActual hortAggCropCoverageActual);

        /// <summary>
        /// HortAutoApprovalCoverageActlBlock.
        /// </summary>
        /// <returns>Response.</returns>
        int HortAutoApprovalCoverageActlBlock();

        /// <summary>
        /// HortAutoApprovalCoverageActlPnchyt.
        /// </summary>
        /// <returns>Response.</returns>
        int HortAutoApprovalCoverageActlPnchyt();

        /// <summary>
        /// GetHortCropCoverageActualPancht.
        /// </summary>
        /// <param name="seasonId">seasonId.</param>
        /// <param name="panchayatId">panchayatId.</param>
        /// <returns>HortCropCoverageActualPancht.</returns>
        List<HortCropCoverageActualPancht> GetHortCropCoverageActualPancht(string seasonId, string panchayatId);

        /// <summary>
        /// GetHortCropCoverageActualPanchtDelta.
        /// </summary>
        /// <param name="seasonId">seasonId.</param>
        /// <param name="panchayatId">panchayatId.</param>
        /// <param name="lastrefreshedTime">lastrefreshedTime.</param>
        /// <returns>HortCropCoverageActualPancht.</returns>
        List<HortCropCoverageActualPancht> GetHortCropCoverageActualPanchtDelta(string seasonId, string panchayatId, DateTime lastrefreshedTime);

        /// <summary>
        /// GetHortCropCoverageActualPanchtCurrDate.
        /// </summary>
        /// <param name="seasonId">seasonId.</param>
        /// <param name="panchayatId">panchayatId.</param>
        /// <returns>HortCropCoverageActualPancht.</returns>
        List<HortCropCoverageActualPancht> GetHortCropCoverageActualPanchtCurrDate(string seasonId, string panchayatId);

        /// <summary>
        /// GetHortCropCoverageActualBlock.
        /// </summary>
        /// <param name="blockId">blockId.</param>
        /// <returns>HortCropCoverageActualBlock.</returns>
        List<HortCropCoverageActualBlock> GetHortCropCoverageActualBlock(string blockId);

        /// <summary>
        /// GetHortCropCoverageActualDistrict.
        /// </summary>
        /// <param name="districtId">districtId.</param>
        /// <returns>HortCropCoverageActualDistrict.</returns>
        List<HortCropCoverageActualDistrict> GetHortCropCoverageActualDistrict(string districtId);

        /// <summary>
        /// GetHortCropCoverageActualADHDelta.
        /// </summary>
        /// <param name="district_id">district_id.</param>
        /// <param name="season_id">season_id.</param>
        /// <param name="last_refresh_time">last_refresh_time.</param>
        /// <returns>HortCropCoverageActual.</returns>
        List<HortCropCoverageActual> GetHortCropCoverageActualADHDelta(int district_id, int season_id, DateTime last_refresh_time);

        /// <summary>
        /// GetHortCropCoverageActualBHODelta.
        /// </summary>
        /// <param name="district_id">district_id.</param>
        /// <param name="block_id">block_id.</param>
        /// <param name="season_id">season_id.</param>
        /// <param name="last_refresh_time">last_refresh_time.</param>
        /// <returns>HortCropCoverageActual.</returns>
        List<HortCropCoverageActual> GetHortCropCoverageActualBHODelta(int district_id, int block_id, int season_id, DateTime last_refresh_time);

        /// <summary>
        /// GetHortCropCoverageActualBHOOffline.
        /// </summary>
        /// <param name="district_id">district_id.</param>
        /// <param name="block_id">block_id.</param>
        /// <param name="season_id">season_id.</param>
        /// <param name="crop_ids">crop_ids.</param>
        /// <returns>HortCropCoverageActual.</returns>
        List<HortCropCoverageActual> GetHortCropCoverageActualBHOOffline(int district_id, int block_id, int season_id, string crop_ids);

        /// <summary>
        /// GetHortCropCoverageActualADHOffline.
        /// </summary>
        /// <param name="district_id">district_id.</param>
        /// <param name="season_id">season_id.</param>
        /// <param name="crop_ids">crop_ids.</param>
        /// <returns>HortCropCoverageActual.</returns>
        List<HortCropCoverageActual> GetHortCropCoverageActualADHOffline(int district_id, int season_id, string crop_ids);

        /// <summary>
        /// GetHortCropCoverageActualADH.
        /// </summary>
        /// <param name="district_id">district_id.</param>
        /// <param name="season_id">season_id.</param>
        /// <param name="crop_id">crop_id.</param>
        /// <returns>HortCropCoverageActual.</returns>
        HortCropCoverageActual GetHortCropCoverageActualADH(int district_id, int season_id, int crop_id);

        /// <summary>
        /// GetHortCropCoverageActualBHO.
        /// </summary>
        /// <param name="district_id">district_id.</param>
        /// <param name="block_id">block_id.</param>
        /// <param name="season_id">season_id.</param>
        /// <param name="crop_id">crop_id.</param>
        /// <returns>HortCropCoverageActual.</returns>
        HortCropCoverageActual GetHortCropCoverageActualBHO(int district_id, int block_id, int season_id, int crop_id);

        /// <summary>
        /// HortAutoCropCvrgActlDataCorrection.
        /// </summary>
        /// <returns>Response.</returns>
        int HortAutoCropCvrgActlDataCorrection();

        /// <summary>
        /// InsertHortCropCoverageActualApproval.
        /// </summary>
        /// <param name="cropCoverageActual">cropCoverageActual.</param>
        /// <returns>CropCoverageActualBlockApprovalResponse.</returns>
        List<CropCoverageActualBlockApprovalResponse> InsertHortCropCoverageActualApproval(HortCropCoverageActual cropCoverageActual);

        /// <summary>
        /// InsertHortCropCoverageActualPancht.
        /// </summary>
        /// <param name="crop">crop.</param>
        /// <returns>CropCoverageActualPanchayatApprovalResponse.</returns>
        List<CropCoverageActualPanchayatApprovalResponse> InsertHortCropCoverageActualPancht(List<HortCropCoverageActualPancht> crop);

        /// <summary>
        /// InsertHortCropCoverageActualPanchayatApproval.
        /// </summary>
        /// <param name="cropCoverageActual">cropCoverageActual.</param>
        /// <returns>CropCoverageActualBlockApprovalResponse.</returns>
        List<CropCoverageActualBlockApprovalResponse> InsertHortCropCoverageActualPanchayatApproval(HortCropCoverageActual cropCoverageActual);

        /// <summary>
        /// GetAllStructure.
        /// </summary>
        /// <param name="district_Id">district_Id.</param>
        /// <returns>GetStructure.</returns>
        List<GetStructure> GetAllStructure(int district_Id);

        /// <summary>
        /// InsertPHMSStructure.
        /// </summary>
        /// <param name="pHMSStructure">pHMSStructure.</param>
        /// <returns>Response.</returns>
        int InsertPHMSStructure(PhmsStructure pHMSStructure);

        /// <summary>
        /// InsertHortNewCrop.
        /// </summary>
        /// <param name="hortNewCrop">hortNewCrop.</param>
        /// <returns>Response.</returns>
        int InsertHortNewCrop(HortNewCrop hortNewCrop);

        /// <summary>
        /// GetDistinctHorticultureCrop.
        /// </summary>
        /// <param name="district_id">district_id.</param>
        /// <returns>DistinctHorticultureCrop.</returns>
        List<DistinctHorticultureCrop> GetDistinctHorticultureCrop(int district_id);

        /// <summary>
        /// GetHortProduceSeason.
        /// </summary>
        /// <returns>HortProduceSeasonResponse.</returns>
        List<HortProduceSeasonResponse> GetHortProduceSeason();

        /// <summary>
        /// GetHortProducePanchayat.
        /// </summary>
        /// <param name="district_id">district_id.</param>
        /// <param name="block_id">block_id.</param>
        /// <returns>HortProducePanchayatResponse.</returns>
        List<HortProducePanchayatResponse> GetHortProducePanchayat(int district_id, int block_id);

        /// <summary>
        /// GetHortProduceLatest.
        /// </summary>
        /// <param name="crop_id">crop_id.</param>
        /// <param name="district_id">district_id.</param>
        /// <param name="block_id">block_id.</param>
        /// <param name="panchayat_Id">panchayat_Id.</param>
        /// <returns>HortProduceTran.</returns>
        List<HortProduceTran> GetHortProduceLatest(int crop_id, int district_id, int block_id, int panchayat_Id);

        /// <summary>
        /// InsertHortProduceTranApproval.
        /// </summary>
        /// <param name="hortProduceTranApprovals">hortProduceTranApprovals.</param>
        /// <returns>CropCoverageTargetPanchytApprovalResponse.</returns>
        List<CropCoverageTargetPanchytApprovalResponse> InsertHortProduceTranApproval(List<HortProduceTranApproval> hortProduceTranApprovals);

        /// <summary>
        /// InsertHortCoverageActualApproval.
        /// </summary>
        /// <param name="cropCoverageActual">cropCoverageActual.</param>
        /// <returns>CropCoverageActualBlockApprovalResponse.</returns>
        public List<CropCoverageActualBlockApprovalResponse> InsertHortCoverageActualApproval(HortProduceActlApproval cropCoverageActual);

        /// <summary>
        /// InsertHortProduceActualPnchyt.
        /// </summary>
        /// <param name="hortProduce">hortProduce.</param>
        /// <returns>CropCoverageActualPanchayatApprovalResponse.</returns>
        List<CropCoverageActualPanchayatApprovalResponse> InsertHortProduceActualPnchyt(List<HortProduceActualPanchayat> hortProduce);

        /// <summary>
        /// HortProduceAutoApprovalActlBlock.
        /// </summary>
        /// <returns>Response.</returns>
        int HortProduceAutoApprovalActlBlock();

        /// <summary>
        /// HortProduceAutoApprovalActlPanchayat.
        /// </summary>
        /// <returns>Response.</returns>
        int HortProduceAutoApprovalActlPanchayat();

        /// <summary>
        /// HortProduceActlDataCorrection.
        /// </summary>
        /// <returns>Response.</returns>
        int HortProduceActlDataCorrection();

        /// <summary>
        /// GetHortProduceActualPancht.
        /// </summary>
        /// <param name="season_id">season_id.</param>
        /// <param name="panchayat_Id">panchayat_Id.</param>
        /// <returns>HortProduceActualPanchayat.</returns>
        List<HortProduceActualPanchayat> GetHortProduceActualPancht(int season_id, int panchayat_Id);

        /// <summary>
        /// GetHortProduceActualPanchtCurrDate.
        /// </summary>
        /// <param name="season_id">season_id.</param>
        /// <param name="panchayat_Id">panchayat_Id.</param>
        /// <returns>HortProduceActualPanchayat.</returns>
        List<HortProduceActualPanchayat> GetHortProduceActualPanchtCurrDate(int season_id, int panchayat_Id);

        /// <summary>
        /// GetHortProduceActualBlock.
        /// </summary>
        /// <param name="block_Id">block_Id.</param>
        /// <returns>HortProduceActualBlock.</returns>
        List<HortProduceActualBlock> GetHortProduceActualBlock(int block_Id);

        /// <summary>
        /// PostAgriProductivity.
        /// </summary>
        /// <param name="hortProduce">hortProduce.</param>
        /// <returns>Response.</returns>
        int PostAgriProductivity(List<PostSubmitProductivityModel> hortProduce);

        /// <summary>
        /// GetSubmitProductivity.
        /// </summary>
        /// <param name="district_id">district_id.</param>
        /// <param name="season_id">season_id.</param>
        /// <returns>GetSubmitProductivityModel.</returns>
        List<GetSubmitProductivityModel> GetSubmitProductivity(int district_id, int season_id);

        /// <summary>
        /// GetHortiReportPHMS.
        /// </summary>
        /// <param name="getHortiReportPHMS">getHortiReportPHMS.</param>
        /// <returns>List.</returns>
        List<string> GetHortiReportPHMS(GetHortiReportPhmsModel getHortiReportPHMS);

        /// <summary>
        /// GetHortiReportColdStorage.
        /// </summary>
        /// <param name="getHortiReportPHMS">getHortiReportPHMS.</param>
        /// <returns>List.</returns>
        List<string> GetHortiReportColdStorage(GetHortiReportPhmsModel getHortiReportPHMS);

        /// <summary>
        /// GetHortProduceActualDistrict.
        /// </summary>
        /// <param name="district_Id">district_Id.</param>
        /// <returns>HortProduceActualDistrict.</returns>
        List<HortProduceActualDistrict> GetHortProduceActualDistrict(int district_Id);

        /// <summary>
        /// GetHortProduceBHO.
        /// </summary>
        /// <param name="district_id">district_id.</param>
        /// <param name="block_id">block_id.</param>
        /// <param name="season_id">season_id.</param>
        /// <param name="panchyat_id">panchyat_id.</param>
        /// <returns>GetHortProduceBholstModel.</returns>
        List<GetHortProduceBholstModel> GetHortProduceBHO(int district_id, int block_id, int season_id, string panchyat_id);

        /// <summary>
        /// GetHortiProduceCoverageActualBHOOffline.
        /// </summary>
        /// <param name="district_id">district_id.</param>
        /// <param name="block_id">block_id.</param>
        /// <param name="season_id">season_id.</param>
        /// <param name="crop_ids">crop_ids.</param>
        /// <returns>List</returns>
        List<HortProduceCoverageActual> GetHortiProduceCoverageActualBHOOffline(int district_id, int block_id, int season_id, string crop_ids);

        /// <summary>
        /// GetHortProduceActualADHOffline.
        /// </summary>
        /// <param name="district_id">district_id.</param>
        /// <param name="season_id">season_id</param>
        /// <param name="crop_ids">crop_ids.</param>
        /// <returns>List</returns>
        List<HortProduceCoverageActual> GetHortProduceActualADHOffline(int district_id, int season_id, string crop_ids);

        /// <summary>
        /// GetHortProduceActualADH.
        /// </summary>
        /// <param name="district_id">district_id.</param>
        /// <param name="season_id">season_id.</param>
        /// <param name="crop_ids">crop_ids.</param>
        /// <returns>GetHortProduceActualADH</returns>
        HortProduceCoverageActl GetHortProduceActualADH(int district_id, int season_id, string crop_ids);

        /// <summary>
        /// GetHortProduceActualBHO.
        /// </summary>
        /// <param name="district_id">district_id.</param>
        /// <param name="block_id">block_id.</param>
        /// <param name="season_id">season_id.</param>
        /// <param name="crop_ids">crop_ids.</param>
        /// <returns>HortProduceCoverageActl.</returns>
        HortProduceCoverageActl GetHortProduceActualBHO(int district_id, int block_id, int season_id, string crop_ids);

        /// <summary>
        /// GetAllSeedPerformanceHorticultureDBT.
        /// </summary>
        /// <param name="district_id">district_id.</param>
        /// <param name="block_id">block_id.</param>
        /// <param name="panchayat_id">panchayat_id.</param>
        /// <param name="season_id">season_id.</param>
        /// <returns>SeedPerfHortdbt.</returns>
        List<SeedPerfHortdbt> GetAllSeedPerformanceHorticultureDBT(int district_id, int block_id, int panchayat_id, int season_id);

        /// <summary>
        /// GetBlockPanchayatData.
        /// </summary>
        /// <param name="district_id">district_id.</param>
        /// <returns>DbtDistrictWrapper.</returns>
        DbtDistrictWrapper GetBlockPanchayatData(int district_id);

        /// <summary>
        /// InsertHortiSeedPerformance.
        /// </summary>
        /// <param name="seedPerformance">seedPerformance.</param>
        /// <returns>Response.</returns>
        Task<int> InsertHortiSeedPerformance(InsertHorticultureSeedPerf seedPerformance);

        /// <summary>
        /// GetVarietiesByType.
        /// </summary>
        /// <param name="type">type.</param>
        /// <param name="seasonid">seasonid.</param>
        /// <returns>List</returns>
        List<Varities> GetVarietiesByType(string type, int seasonid);

        /// <summary>
        /// GetPHMSNoFacilitiesData.
        /// </summary>
        /// <param name="districtId">districtId.</param>
        /// <returns>NoFacility.</returns>
        List<NoFacility> GetPHMSNoFacilitiesData(int districtId);

        /// <summary>
        /// GetBlockPanchaytLgCodes.
        /// </summary>
        /// <param name="districtId">districtId.</param>
        /// <returns>BlockPanchayatLgCodes List.</returns>
        List<BlockPanchayatLgCodes> GetBlockPanchaytLgCodes(int districtId);
    }
}
