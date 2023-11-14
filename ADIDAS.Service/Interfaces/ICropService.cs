//------------------------------------------------------------------------------
// <copyright file="ICropService.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------

namespace ADIDAS.Service.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ADIDAS.Model.DTO;
    using ADIDAS.Model.Entities;

    /// <summary>
    /// ICropService.
    /// </summary>
    public interface ICropService
    {
        /// <summary>
        /// GetSeason.
        /// </summary>
        /// <returns>SeasonDim list.</returns>
        List<SeasonDim> GetSeason();

        /// <summary>
        /// GetCrop.
        /// </summary>
        /// <param name="seasonName">seasonName.</param>
        /// <param name="districtId">districtId.</param>
        /// <returns>CropSeasonEntity list.</returns>
        List<CropSeasonEntity> GetCrop(string seasonName, string districtId);

        /// <summary>
        /// GetDistinctCrop.
        /// </summary>
        /// <param name="districtId">districtId.</param>
        /// <returns>CropSeasonEntity list.</returns>
        List<CropSeasonEntity> GetDistinctCrop(string districtId);

        /// <summary>
        /// GetCropCategory.
        /// </summary>
        /// <param name="crop_Type">crop_Type.</param>
        /// <returns>CropCategoryEntity list.</returns>
        List<CropCategoryEntity> GetCropCategory(string crop_Type);

        /// <summary>
        /// GetCoverageSubmittedCropsBySeasonBAO.
        /// </summary>
        /// <param name="districtId">districtId.</param>
        /// <returns>CoverageSubmittedCropsSeason list.</returns>
        List<CoverageSubmittedCropsSeason> GetCoverageSubmittedCropsBySeasonBAO(int districtId);

        /// <summary>
        /// GetTargetSubmittedCropsBySeasonBAO.
        /// </summary>
        /// <param name="districtId">districtId.</param>
        /// <returns>CoverageSubmittedCropsSeason list.</returns>
        List<CoverageSubmittedCropsSeason> GetTargetSubmittedCropsBySeasonBAO(int districtId);

        /// <summary>
        /// GetCoverageSubmittedCropsBySeasonDAO.
        /// </summary>
        /// <param name="districtId">districtId.</param>
        /// <returns>CoverageSubmittedCropsSeason list.</returns>
        List<CoverageSubmittedCropsSeason> GetCoverageSubmittedCropsBySeasonDAO(int districtId);

        /// <summary>
        /// GetTargetSubmittedCropsBySeasonDAO.
        /// </summary>
        /// <param name="districtId">districtId.</param>
        /// <returns>CoverageSubmittedCropsSeason list.</returns>
        List<CoverageSubmittedCropsSeason> GetTargetSubmittedCropsBySeasonDAO(int districtId);

        /// <summary>
        /// GetCoverageSubmittedCropsBySeason.
        /// </summary>
        /// <param name="districtId">districtId.</param>
        /// <returns>CoverageSubmittedCropsSeason list.</returns>
        List<CoverageSubmittedCropsSeason> GetCoverageSubmittedCropsBySeason(int districtId);

        /// <summary>
        /// GetTargetSubmittedCropsBySeason.
        /// </summary>
        /// <param name="districtId">districtId.</param>
        /// <returns>CoverageSubmittedCropsSeason list.</returns>
        List<CoverageSubmittedCropsSeason> GetTargetSubmittedCropsBySeason(int districtId);

        /// <summary>
        /// GetLGDirectory.
        /// </summary>
        /// <returns>LgDirectoryPanchayatDim list.</returns>
        List<LgDirectoryPanchayatDim> GetLGDirectory();

        /// <summary>
        /// GetRainfallDistricts.
        /// </summary>
        /// <returns>DistrictList.</returns>
        List<DistrictList> GetRainfallDistricts();

        /// <summary>
        /// GetLGDirectoryDistrict.
        /// </summary>
        /// <param name="districtId">districtId.</param>
        /// <returns>LgDirectoryPanchayatDim list.</returns>
        List<LgDirectoryPanchayatDim> GetLGDirectoryDistrict(string districtId);

        /// <summary>
        /// GetLGDirectoryBlock.
        /// </summary>
        /// <param name="blockId">blockId.</param>
        /// <returns>LgDirectoryPanchayatDim.</returns>
        List<LgDirectoryPanchayatDim> GetLGDirectoryBlock(string blockId);

        /// <summary>
        /// GetLGDirectoryPancht.
        /// </summary>
        /// <param name="panchayatId">panchayatId.</param>
        /// <returns>LgDirectoryPanchayatDim list.</returns>
        List<LgDirectoryPanchayatDim> GetLGDirectoryPancht(string panchayatId);

        /// <summary>
        /// GetLGDirectoryVillage.
        /// </summary>
        /// <param name="panchayatId">panchayatId.</param>
        /// <returns>LgDirectoryVillageDim list.</returns>
        List<LgDirectoryVillageDim> GetLGDirectoryVillage(int panchayatId);

        /// <summary>
        /// GetLGDirectoryVillageByUserId.
        /// </summary>
        /// <param name="userId">userId.</param>
        /// <param name="panchayatId">panchayatId.</param>
        /// <returns>LgDirectoryVillageDim list.</returns>
        List<LgDirectoryVillageDim> GetLGDirectoryVillageByUserId(int userId, int panchayatId);

        /// <summary>
        /// GetSource.
        /// </summary>
        /// <param name="attributeName">attributeName.</param>
        /// <returns>MobileAttributeConfig.</returns>
        List<MobileAttributeConfig> GetSource(string attributeName);

        /// <summary>
        /// GetCropCoverageAimPancht.
        /// </summary>
        /// <param name="seasonId">seasonId.</param>
        /// <param name="panchayatId">panchayatId.</param>
        /// <returns>CropCoverageAimPancht list.</returns>
        List<CropCoverageAimPancht> GetCropCoverageAimPancht(string seasonId, string panchayatId);

        /// <summary>
        /// GetCropCoverageAimVillage.
        /// </summary>
        /// <param name="seasonId">seasonId.</param>
        /// <param name="panchayatId">panchayatId</param>
        /// <param name="villageId">villageId</param>
        /// <returns>List Values.</returns>
        List<CropCoverageAimVillage> GetCropCoverageAimVillage(string seasonId, string panchayatId, string villageId);

        /// <summary>
        /// GetCropCoverageAimBlock.
        /// </summary>
        /// <param name="blockId">blockId.</param>
        /// <returns>CropCoverageAimBlock list.</returns>
        List<CropCoverageAimBlock> GetCropCoverageAimBlock(string blockId);

        /// <summary>
        /// GetCropCoverageAimDistrict.
        /// </summary>
        /// <param name="districtId">districtId.</param>
        /// <returns>CropCoverageAimDistrict list.</returns>
        List<CropCoverageAimDistrict> GetCropCoverageAimDistrict(string districtId);

        /// <summary>
        /// GetCropCoverageActualPancht.
        /// </summary>
        /// <param name="seasonId">seasonId.</param>
        /// <param name="panchayatId">panchayatId.</param>
        /// <returns>CropCoverageActualPancht.</returns>
        List<CropCoverageActualVillage> GetCropCoverageActualPancht(string seasonId, string panchayatId);

        /// <summary>
        /// GetCropCoverageActualVillage.
        /// </summary>
        /// <param name="seasonId">seasonId.</param>
        /// <param name="panchayatId">panchayatId.</param>
        /// <param name="villageId">villageId.</param>
        /// <returns>CropCoverageActualVillage.</returns>
        List<CropCoverageActualVillage> GetCropCoverageActualVillage(string seasonId, string panchayatId, string villageId);

        /// <summary>
        /// GetCropCoverageActualPanchtCurrDate.
        /// </summary>
        /// <param name="seasonId">seasonId.</param>
        /// <param name="panchayatId">panchayatId.</param>
        /// <returns>CropCoverageActualPancht.</returns>
        List<CropCoverageActualVillage> GetCropCoverageActualPanchtCurrDate(string seasonId, string panchayatId);

        /// <summary>
        /// GetCropCoverageActualVillageCurrDate.
        /// </summary>
        /// <param name="seasonId">seasonId.</param>
        /// <param name="panchayatId">panchayatId.</param>
        /// <param name="villageId">villageId.</param>
        /// <returns>List.</returns>
        List<CropCoverageActualVillage> GetCropCoverageActualVillageCurrDate(string seasonId, string panchayatId, string villageId);

        /// <summary>
        /// GetCropCoverageActualBlock.
        /// </summary>
        /// <param name="blockId">blockId.</param>
        /// <returns>CropCoverageActualBlock list.</returns>
        List<CropCoverageActualBlock> GetCropCoverageActualBlock(string blockId);

        /// <summary>
        /// GetCropCoverageActualDistrict.
        /// </summary>
        /// <param name="districtId">districtId.</param>
        /// <returns>CropCoverageActualDistrict list.</returns>
        List<CropCoverageActualDistrict> GetCropCoverageActualDistrict(string districtId);

        /// <summary>
        /// GetCropDamagePancht.
        /// </summary>
        /// <param name="seasonId">seasonId.</param>
        /// <param name="panchayatId">panchayatId.</param>
        /// <returns>CropDamagePancht list.</returns>
        List<CropDamagePancht> GetCropDamagePancht(string seasonId, string panchayatId);

        /// <summary>
        /// GetCropDamageBlock.
        /// </summary>
        /// <param name="blockId">blockId.</param>
        /// <returns>CropDamageBlock list.</returns>
        List<CropDamageBlock> GetCropDamageBlock(string blockId);

        /// <summary>
        /// GetCropDamageDistrict.
        /// </summary>
        /// <param name="districtId">districtId.</param>
        /// <returns>CropDamageDistrict list.</returns>
        List<CropDamageDistrict> GetCropDamageDistrict(string districtId);

        /// <summary>
        /// GetCropCoverageTargetDAO.
        /// </summary>
        /// <param name="district_id">district_id.</param>
        /// <param name="season_id">season_id.</param>
        /// <param name="crop_id">crop_id.</param>
        /// <returns>CropCoverageAim.</returns>
        CropCoverageAim GetCropCoverageTargetDAO(int district_id, int season_id, int crop_id);

        /// <summary>
        /// GetCropCoverageActualDAO.
        /// </summary>
        /// <param name="district_id">district_id.</param>
        /// <param name="season_id">season_id.</param>
        /// <param name="crop_id">crop_id.</param>
        /// <returns>CropCoverageActual.</returns>
        CropCoverageActual GetCropCoverageActualDAO(int district_id, int season_id, int crop_id);

        /// <summary>
        /// GetCropDamageDAO.
        /// </summary>
        /// <param name="district_id">district_id.</param>
        /// <param name="season_id">season_id.</param>
        /// <param name="crop_id">crop_id.</param>
        /// <returns>CropDamage list.</returns>
        CropDamage GetCropDamageDAO(int district_id, int season_id, int crop_id);

        /// <summary>
        /// GetCropCoverageTargetBAO.
        /// </summary>
        /// <param name="district_id">district_id.</param>
        /// <param name="block_id">block_id.</param>
        /// <param name="season_id">season_id.</param>
        /// <param name="crop_id">crop_id.</param>
        /// <returns>CropCoverageAim.</returns>
        CropCoverageAim GetCropCoverageTargetBAO(int district_id, int block_id, int season_id, int crop_id);

        /// <summary>
        /// GetCropCoverageActualBAO.
        /// </summary>
        /// <param name="district_id">district_id.</param>
        /// <param name="block_id">block_id.</param>
        /// <param name="season_id">season_id.</param>
        /// <param name="crop_id">crop_id.</param>
        /// <returns>CropCoverageActual.</returns>
        CropCoverageActual GetCropCoverageActualBAO(int district_id, int block_id, int season_id, int crop_id);

        /// <summary>
        /// GetCropDamageBAO.
        /// </summary>
        /// <param name="district_id">district_id.</param>
        /// <param name="block_id">block_id.</param>
        /// <param name="season_id">season_id.</param>
        /// <param name="crop_id">crop_id.</param>
        /// <returns>CropDamage.</returns>
        CropDamage GetCropDamageBAO(int district_id, int block_id, int season_id, int crop_id);

        /// <summary>
        /// GetCropCoverageTargetDAODelta.
        /// </summary>
        /// <param name="district_id">district_id.</param>
        /// <param name="season_id">season_id.</param>
        /// <param name="last_refresh_time">last_refresh_time.</param>
        /// <returns>CropCoverageAim list.</returns>
        List<CropCoverageAim> GetCropCoverageTargetDAODelta(int district_id, int season_id, DateTime last_refresh_time);

        /// <summary>
        /// GetCropCoverageTargetBAODelta.
        /// </summary>
        /// <param name="district_id">district_id.</param>
        /// <param name="block_id">block_id.</param>
        /// <param name="season_id">season_id.</param>
        /// <param name="last_refresh_time">last_refresh_time.</param>
        /// <returns>CropCoverageAim list.</returns>
        List<CropCoverageAim> GetCropCoverageTargetBAODelta(int district_id, int block_id, int season_id, DateTime last_refresh_time);

        /// <summary>
        /// GetCropCoverageActualDAODelta.
        /// </summary>
        /// <param name="district_id">district_id.</param>
        /// <param name="season_id">season_id.</param>
        /// <param name="last_refresh_time">last_refresh_time.</param>
        /// <returns>CropCoverageActual list.</returns>
        List<CropCoverageActual> GetCropCoverageActualDAODelta(int district_id, int season_id, DateTime last_refresh_time);

        /// <summary>
        /// GetCropCoverageActualBAODelta.
        /// </summary>
        /// <param name="district_id">district_id.</param>
        /// <param name="block_id">block_id.</param>
        /// <param name="season_id">season_id.</param>
        /// <param name="last_refresh_time">last_refresh_time.</param>
        /// <returns>CropCoverageActual list.</returns>
        List<CropCoverageActual> GetCropCoverageActualBAODelta(int district_id, int block_id, int season_id, DateTime last_refresh_time);

        /// <summary>
        /// GetCropDamagePanchtDelta.
        /// </summary>
        /// <param name="seasonId">seasonId.</param>
        /// <param name="panchayatId">panchayatId.</param>
        /// <param name="lastrefreshedTime">lastrefreshedTime.</param>
        /// <returns>CropDamagePancht list.</returns>
        List<CropDamagePancht> GetCropDamagePanchtDelta(string seasonId, string panchayatId, DateTime lastrefreshedTime);

        /// <summary>
        /// GetCropCoverageAimPanchtDelta.
        /// </summary>
        /// <param name="seasonId">seasonId.</param>
        /// <param name="panchayatId">panchayatId.</param>
        /// <param name="lastrefreshedTime">lastrefreshedTime.</param>
        /// <returns>CropCoverageAimPancht list.</returns>
        List<CropCoverageAimPancht> GetCropCoverageAimPanchtDelta(string seasonId, string panchayatId, DateTime lastrefreshedTime);

        /// <summary>
        /// GetCropCoverageActualPanchtDelta.
        /// </summary>
        /// <param name="seasonId">seasonId.</param>
        /// <param name="panchayatId">panchayatId.</param>
        /// <param name="lastrefreshedTime">lastrefreshedTime.</param>
        /// <returns>CropCoverageActualPancht list.</returns>
        List<CropCoverageActualPancht> GetCropCoverageActualPanchtDelta(string seasonId, string panchayatId, DateTime lastrefreshedTime);

        /// <summary>
        /// GetCropDamageDAOOffline.
        /// </summary>
        /// <param name="district_id">district_id.</param>
        /// <param name="season_id">season_id.</param>
        /// <param name="crop_ids">crop_ids.</param>
        /// <returns>CropDamage.</returns>
        List<CropDamage> GetCropDamageDAOOffline(int district_id, int season_id, string crop_ids);

        /// <summary>
        /// GetCropDamageBAOOffline.
        /// </summary>
        /// <param name="district_id">district_id.</param>
        /// <param name="block_id">block_id.</param>
        /// <param name="season_id">season_id.</param>
        /// <param name="crop_ids">crop_ids.</param>
        /// <returns>CropDamage list.</returns>
        List<CropDamage> GetCropDamageBAOOffline(int district_id, int block_id, int season_id, string crop_ids);

        /// <summary>
        /// GetCropDamageBAOOfflineDelta.
        /// </summary>
        /// <param name="district_id">district_id.</param>
        /// <param name="block_id">block_id.</param>
        /// <param name="season_id">season_id.</param>
        /// <param name="last_refresh_time">last_refresh_time.</param>
        /// <returns>CropDamage list.</returns>
        List<CropDamage> GetCropDamageBAOOfflineDelta(int district_id, int block_id, int season_id, DateTime last_refresh_time);

        /// <summary>
        /// GetCropDamageDAOOfflineDelta.
        /// </summary>
        /// <param name="district_id">district_id.</param>
        /// <param name="season_id">season_id.</param>
        /// <param name="last_refresh_time">last_refresh_time.</param>
        /// <returns>CropDamage list.</returns>
        List<CropDamage> GetCropDamageDAOOfflineDelta(int district_id, int season_id, DateTime last_refresh_time);

        /// <summary>
        /// InsertCropDim.
        /// </summary>
        /// <param name="crop">crop.</param>
        /// <returns>integer.</returns>
        int InsertCropDim(CropSeasonEntity crop);

        /// <summary>
        /// InsertCropCoverageAimPancht.
        /// </summary>
        /// <param name="crop">crop.</param>
        /// <returns>CropCoverageTargetPanchytApprovalResponse list.</returns>
        List<CropCoverageTargetPanchytApprovalResponse> InsertCropCoverageAimPancht(List<CropCoverageAimPancht> crop);

        /// <summary>
        /// InsertCropCoverageAimVillage.
        /// </summary>
        /// <param name="crop">crop.</param>
        /// <returns>CropCoverageTargetPanchytApprovalResponse list.</returns>
        List<CropCoverageTargetVillageApprovalResponse> InsertCropCoverageAimVillage(List<CropCoverageAimVillage> crop);

        /// <summary>
        /// InsertCropCoverageActualPancht.
        /// </summary>
        /// <param name="crop">crop.</param>
        /// <returns>CropCoverageActualPanchayatApprovalResponse.</returns>
        List<CropCoverageActualPanchayatApprovalResponse> InsertCropCoverageActualPancht(List<CropCoverageActualVillage> crop);

        /// <summary>
        /// InsertCropCoverageActualVillage.
        /// </summary>
        /// <param name="crop">crop.</param>
        /// <returns>List.</returns>
        List<CropCoverageActualVillageApprovalResponse> InsertCropCoverageActualVillage(List<CropCoverageActualVillage> crop);

        /// <summary>
        /// InsertCropDamagePancht.
        /// </summary>
        /// <param name="crop">crop.</param>
        /// <returns>CropDamagePanchayatResponse.</returns>
        List<CropDamagePanchayatResponse> InsertCropDamagePancht(List<CropDamagePancht> crop);

        /// <summary>
        /// InsertCropCoverageActualApproval.
        /// </summary>
        /// <param name="cropCoverageActual">cropCoverageActual.</param>
        /// <returns>CropCoverageActualBlockApprovalResponse.</returns>
        List<CropCoverageActualBlockApprovalResponse> InsertCropCoverageActualApproval(CropCoverageActual cropCoverageActual);

        /// <summary>
        /// InsertCropCoverageTargetApproval.
        /// </summary>
        /// <param name="cropCoverageTarget">cropCoverageTarget.</param>
        /// <returns>CropCoverageTargetBlockApproval.</returns>
        List<CropCoverageTargetBlockApproval> InsertCropCoverageTargetApproval(CropCoverageAim cropCoverageTarget);

        /// <summary>
        /// InsertCropDamageApproval.
        /// </summary>
        /// <param name="cropDamage">cropDamage.</param>
        /// <returns>CropDamageBlockApprovalResponse.</returns>
        List<CropDamageBlockApprovalResponse> InsertCropDamageApproval(CropDamage cropDamage);

        /// <summary>
        /// GetCropCoverageTargetBAOOffline.
        /// </summary>
        /// <param name="district_id">district_id.</param>
        /// <param name="block_id">block_id.</param>
        /// <param name="season_id">season_id.</param>
        /// <param name="crop_ids">crop_ids.</param>
        /// <returns>CropCoverageAim list.</returns>
        List<CropCoverageAim> GetCropCoverageTargetBAOOffline(int district_id, int block_id, int season_id, string crop_ids);

        /// <summary>
        /// GetCropCoverageTargetDAOOffline.
        /// </summary>
        /// <param name="district_id">district_id.</param>
        /// <param name="season_id">season_id.</param>
        /// <param name="crop_ids">crop_ids.</param>
        /// <returns>CropCoverageAim list.</returns>
        List<CropCoverageAim> GetCropCoverageTargetDAOOffline(int district_id, int season_id, string crop_ids);

        /// <summary>
        /// GetCropCoverageActualBAOOffline.
        /// </summary>
        /// <param name="district_id">district_id.</param>
        /// <param name="block_id">block_id.</param>
        /// <param name="season_id">season_id.</param>
        /// <param name="crop_ids">crop_ids.</param>
        /// <returns>CropCoverageActual list.</returns>
        List<CropCoverageActual> GetCropCoverageActualBAOOffline(int district_id, int block_id, int season_id, string crop_ids);

        /// <summary>
        /// GetCropCoverageActualDAOOffline.
        /// </summary>
        /// <param name="district_id">district_id.</param>
        /// <param name="season_id">season_id.</param>
        /// <param name="crop_ids">crop_ids.</param>
        /// <returns>CropCoverageActual.</returns>
        List<CropCoverageActual> GetCropCoverageActualDAOOffline(int district_id, int season_id, string crop_ids);

        /// <summary>
        /// InsertSeedPerformance.
        /// </summary>
        /// <param name="seedPerformance">seedPerformance.</param>
        /// <returns>InsertSeedPerformanceResponse.</returns>
        Task<InsertSeedPerformanceResponse> InsertSeedPerformance(SeedPerformance seedPerformance);

        /// <summary>
        /// GetSeedPerformance.
        /// </summary>
        /// <param name="panchayatId">panchayatId.</param>
        /// <param name="seasonId">seasonId.</param>
        /// <returns>DtoSeedPerformance.</returns>
        List<DtoSeedPerformance> GetSeedPerformance(int panchayatId, int seasonId);

        /// <summary>
        /// GetSeedPerformanceAgriculture.
        /// </summary>
        /// <param name="districtId">districtId.</param>
        /// <param name="blockId">blockId.</param>
        /// <param name="panchayatId">panchayatId.</param>
        /// <param name="seasonId">seasonId.</param>
        /// <returns>DtoSeedPerformanceAgriculture.</returns>
        List<DtoSeedPerformanceAgriculture> GetSeedPerformanceAgriculture(string districtId, string blockId, string panchayatId, string seasonId);

        /// <summary>
        /// GetSchemesByPanchayat.
        /// </summary>
        /// <param name="panchayatId">panchayatId.</param>
        /// <returns>SchemeModel.</returns>
        List<SchemeModel> GetSchemesByPanchayat(int panchayatId);

        /// <summary>
        /// GetBAONotSubmittedByBlock.
        /// </summary>
        /// <param name="block_id">block_id.</param>
        /// <returns>CropDamageDetailsGet.</returns>
        List<CropDamageDetailsGet> GetBAONotSubmittedByBlock(int block_id);

        /// <summary>
        /// GetAllColdStorage.
        /// </summary>
        /// <param name="getHortiReportPHMS">getHortiReportPHMS.</param>
        /// <returns>GetGrpdwnColdStorageModel.</returns>
        List<GetGrpdwnColdStorageModel> GetAllColdStorage(GetHortiReportPhmsModel getHortiReportPHMS);

        /// <summary>
        /// GetDAONotSubmittedByDistrict.
        /// </summary>
        /// <param name="district_Id">district_Id.</param>
        /// <returns>CropDamageDetailsGet.</returns>
        List<CropDamageDetailsGet> GetDAONotSubmittedByDistrict(int district_Id);

        /// <summary>
        /// GetHorticultureSubmittedDetails.
        /// </summary>
        /// <param name="seasonId">seasonId.</param>
        /// <param name="districtId">districtId.</param>
        /// <returns>HorticultureDetails.</returns>
        List<HorticultureDetails> GetHorticultureSubmittedDetails(int seasonId, int districtId);

        /// <summary>
        /// GetHorticultureCrop.
        /// </summary>
        /// <param name="districtId">districtId.</param>
        /// <param name="seasonId">seasonId.</param>
        /// <returns>HorticultureCrop.</returns>
        List<HorticultureCrop> GetHorticultureCrop(int districtId, int seasonId);

        /// <summary>
        /// GetAllListsOfDamageReasons.
        /// </summary>
        /// <param name="year">year.</param>
        /// <returns>CropDamageEntity.</returns>
        List<CropDamageEntity> GetAllListsOfDamageReasons(int year);

        /// <summary>
        /// GetSpecificDamageReason.
        /// </summary>
        /// <param name="damageDetails">damageDetails.</param>
        /// <returns>DtoEditDamage.</returns>
        DtoEditDamage GetSpecificDamageReason(DtoEditDamageRequest damageDetails);

        /// <summary>
        /// GetDamageReasonNames.
        /// </summary>
        /// <returns>CropDamageReasonNames.</returns>
        List<CropDamageReasonNames> GetDamageReasonNames();

        /// <summary>
        /// GetDamageCropList.
        /// </summary>
        /// <returns>CropNames.</returns>
        List<CropNames> GetDamageCropList();

        /// <summary>
        /// GetEstdCropDamagePercnt.
        /// </summary>
        /// <param name="cropDamageReasonModel">cropDamageReasonModel.</param>
        /// <returns>CropDamageGetModel.</returns>
        List<CropDamageGetModel> GetEstdCropDamagePercnt(CropDamageGetModel cropDamageReasonModel);

        /// <summary>
        /// GetAllDistrict.
        /// </summary>
        /// <returns>DistrictList.</returns>
        List<DistrictList> GetAllDistrict();

        /// <summary>
        /// GetACCropCvrgCoveredAreaPancht.
        /// </summary>
        /// <param name="panchayat_Id">panchayat_Id.</param>
        /// <param name="damage_id">damage_id.</param>
        /// <returns>CropDamageDetailsGet.</returns>
        CropDamageDetailsGet GetACCropCvrgCoveredAreaPancht(string panchayat_Id, int damage_id);

        /// <summary>
        /// GetBAOCropDamageDetailsBlock.
        /// </summary>
        /// <param name="districtId">districtId.</param>
        /// <param name="blockid">blockid.</param>
        /// <param name="damage_id">damage_id.</param>
        /// <returns>InsBaoCropdmgModel.</returns>
        InsBaoCropdmgModel GetBAOCropDamageDetailsBlock(int districtId, int blockid, int damage_id);

        /// <summary>
        /// GetDAOCropDamageDetailsBlock.
        /// </summary>
        /// <param name="district_Id">district_Id.</param>
        /// <param name="damage_id">damage_id.</param>
        /// <returns>InsBaoCropdmgModel.</returns>
        InsBaoCropdmgModel GetDAOCropDamageDetailsBlock(string district_Id, int damage_id);

        /// <summary>
        /// GetACCropCvrgCoveredAreaPanchtOffline.
        /// </summary>
        /// <param name="panchayat_Id">panchayat_Id.</param>
        /// <returns>CropDamageDetailsGet.</returns>
        List<CropDamageDetailsGet> GetACCropCvrgCoveredAreaPanchtOffline(string panchayat_Id);

        /// <summary>
        /// GetBAOCropDamageDetailsBlockOffline.
        /// </summary>
        /// <param name="district_Id">district_Id.</param>
        /// <param name="blockId">blockId.</param>
        /// <returns>InsBaoCropdmgModel list.</returns>
        List<InsBaoCropdmgModel> GetBAOCropDamageDetailsBlockOffline(int district_Id, int blockId);

        /// <summary>
        /// GetDAOCropDamageDetailsBlockOffline.
        /// </summary>
        /// <param name="districtId">districtId.</param>
        /// <returns>InsBaoCropdmgModel list.</returns>
        List<InsBaoCropdmgModel> GetDAOCropDamageDetailsBlockOffline(string districtId);

        /// <summary>
        /// GetBAOApprovedCropDamageDetailsBlock.
        /// </summary>
        /// <param name="district_Id">district_Id.</param>
        /// <param name="block_id">block_id.</param>
        /// <param name="panchayat_Id">panchayat_Id.</param>
        /// <param name="damage_id">damage_id.</param>
        /// <returns>CropDamageAll.</returns>
        CropDamageAll GetBAOApprovedCropDamageDetailsBlock(string district_Id, string block_id, string panchayat_Id, int damage_id);

        /// <summary>
        /// GetDAOApprovedCropDamageDetailsDistrict.
        /// </summary>
        /// <param name="district_Id">district_Id.</param>
        /// <param name="block_id">block_id.</param>
        /// <param name="panchayat_Id">panchayat_Id.</param>
        /// <param name="damage_id">damage_id.</param>
        /// <returns>CropDamageAll.</returns>
        CropDamageAll GetDAOApprovedCropDamageDetailsDistrict(string district_Id, string block_id, string panchayat_Id, int damage_id);

        /// <summary>
        /// GetCropDamageReasonDrpdwn.
        /// </summary>
        /// <param name="district_Id">district_Id.</param>
        /// <returns>GetCropDamageReasonDrpDwn list.</returns>
        List<GetCropDamageReasonDrpDwn> GetCropDamageReasonDrpdwn(string district_Id);

        /// <summary>
        /// POSTHorticultureproductivity.
        /// </summary>
        /// <param name="horticultureproductivity">horticultureproductivity.</param>
        /// <returns>integer.</returns>
        int POSTHorticultureproductivity(List<Horticultureproductivity> horticultureproductivity);

        /// <summary>
        /// PostCropDamageReason.
        /// </summary>
        /// <param name="damageReason">damageReason.</param>
        /// <returns>integer.</returns>
        int PostCropDamageReason(DamageReasonPost damageReason);

        /// <summary>
        /// PostCropDamageName.
        /// </summary>
        /// <param name="cropPost">cropPost.</param>
        /// <returns>integer.</returns>
        int PostCropDamageName(CropPost cropPost);

        /// <summary>
        /// PostDelDamageReasonList.
        /// </summary>
        /// <param name="damage">damage.</param>
        /// <returns>integer.</returns>
        int PostDelDamageReasonList(DtoEditDamageRequest damage);

        /// <summary>
        /// PostUpdateDamageReasonStatus.
        /// </summary>
        /// <param name="cropDamageGetModel">cropDamageGetModel.</param>
        /// <returns>integer.</returns>
        int PostUpdateDamageReasonStatus(DtoDamageStatusDetails cropDamageGetModel);

        /// <summary>
        /// PostCropCoverageDamageDetails.
        /// </summary>
        /// <param name="insCropCoverageDamageDetails">insCropCoverageDamageDetails.</param>
        /// <returns>integer.</returns>
        int PostCropCoverageDamageDetails(InsCropCoverageDamageDetails insCropCoverageDamageDetails);

        /// <summary>
        /// PostDamageDetails.
        /// </summary>
        /// <param name="damageDetails">damageDetails.</param>
        /// <returns>DtoPostDamageResponse list.</returns>
        List<DtoPostDamageResponse> PostDamageDetails(DamageDetails damageDetails);

        /// <summary>
        /// PostCropCvgDamagePancytApproval.
        /// </summary>
        /// <param name="cropDamageGetAll">cropDamageGetAll.</param>
        /// <returns>CropResponce.</returns>
        CropResponce PostCropCvgDamagePancytApproval(CropDamageGetAll cropDamageGetAll);

        /// <summary>
        /// PostBAOCropDamageApproval.
        /// </summary>
        /// <param name="insBAOCropdmgModel">insBAOCropdmgModel.</param>
        /// <returns>CropResponce.</returns>
        CropResponce PostBAOCropDamageApproval(InsBaoCropdmgModel insBAOCropdmgModel);

        /// <summary>
        /// PostDAOCropDamageApproval.
        /// </summary>
        /// <param name="insBAOCropdmgModel">insBAOCropdmgModel.</param>
        /// <returns>CropResponce.</returns>
        CropResponce PostDAOCropDamageApproval(InsBaoCropdmgModel insBAOCropdmgModel);

        /// <summary>
        /// GetDamageConstantCropList.
        /// </summary>
        /// <returns>CropNames.</returns>
        List<CropNames> GetDamageConstantCropList();

        /// <summary>
        /// PostEditDamageDetails.
        /// </summary>
        /// <param name="damageDetails">damageDetails.</param>
        /// <returns>DtoPostDamageResponse.</returns>
        List<DtoPostDamageResponse> PostEditDamageDetails(EditDamageDetails damageDetails);

        /// <summary>
        /// GetOnlineACViewSubmissionPancht.
        /// </summary>
        /// <param name="panchayat_Id">panchayat_Id.</param>
        /// <param name="damage_reason_id">damage_reason_id.</param>
        /// <returns>ViewSubmissionAcPanchayat.</returns>
        ViewSubmissionAcPanchayat GetOnlineACViewSubmissionPancht(string panchayat_Id, int damage_reason_id);

        /// <summary>
        /// GetOfflineACViewSubmissionPancht.
        /// </summary>
        /// <param name="panchayat_Id">panchayat_Id.</param>
        /// <returns>ViewSubmissionAcPanchayat list.</returns>
        List<ViewSubmissionAcPanchayat> GetOfflineACViewSubmissionPancht(string panchayat_Id);

        /// <summary>
        /// GetCropDamageReportData.
        /// </summary>
        /// <param name="year">year.</param>
        /// <param name="district_ID">district_ID.</param>
        /// <param name="user_id">user_id.</param>
        /// <param name="damageReason_Id">damageReason_Id.</param>
        /// <returns>string list.</returns>
        List<string> GetCropDamageReportData(int year, string district_ID, int user_id, string damageReason_Id);

        /// <summary>
        /// GetRainfallReportData.
        /// </summary>
        /// <param name="month_year">month_year.</param>
        /// <param name="district_name">district_name.</param>
        /// <returns>string list.</returns>
        List<string> GetRainfallReportData(string month_year, string district_name);

        /// <summary>
        /// GetSeasonByYear.
        /// </summary>
        /// <param name="year">year.</param>
        /// <param name="crop_type">crop_type.</param>
        /// <returns>GetSeasonByYearModel list.</returns>
        List<GetSeasonByYearModel> GetSeasonByYear(int year, string crop_type);

        /// <summary>
        /// GetCropBySeasonID.
        /// </summary>
        /// <param name="season_id">season_id.</param>
        /// <param name="crop_type">crop_type.</param>
        /// <returns>GetCropBySeasonIdModel list.</returns>
        List<GetCropBySeasonIdModel> GetCropBySeasonID(string season_id, string crop_type);

        /// <summary>
        /// GetCropCvrgTargetDataReport.
        /// </summary>
        /// <param name="cropCvrgTargetDataReportModel">cropCvrgTargetDataReportModel.</param>
        /// <returns>string list.</returns>
        List<string> GetCropCvrgTargetDataReport(GetCropCvrgTargetDataReportModel cropCvrgTargetDataReportModel);

        /// <summary>
        /// Implemented for Target Vs coverage.
        /// </summary>
        /// <param name="targetVsCoverage">Collection of input parameters.</param>
        /// <returns>output received in Json format.</returns>
        List<string> GetCropCvrgTargetVsCoverageDataReport(TargetVsCoverageModel targetVsCoverage);

        /// <summary>
        /// GetCropCvrgTargetProductivityDataReport.
        /// </summary>
        /// <param name="cropCvrgTargetDataReportModel">cropCvrgTargetDataReportModel.</param>
        /// <returns>string list.</returns>
        List<string> GetCropCvrgTargetProductivityDataReport(GetCropCvrgTargetDataReportModel cropCvrgTargetDataReportModel);

        /// <summary>
        /// GetHortiProduceDataReport.
        /// </summary>
        /// <param name="cropCvrgTargetDataReportModel">cropCvrgTargetDataReportModel.</param>
        /// <returns>string list.</returns>
        List<string> GetHortiProduceDataReport(GetCropCvrgTargetDataReportModel cropCvrgTargetDataReportModel);

        /// <summary>
        /// InsertSeedUsedInput.
        /// </summary>
        /// <param name="input">input.</param>
        /// <returns>integer.</returns>
        int InsertSeedUsedInput(SeedUsedInput input);

        /// <summary>
        /// GetSeedusedIputViewSubmission.
        /// </summary>
        /// <param name="seasonId">seasonId.</param>
        /// <param name="cropId">cropId.</param>
        /// <param name="district_id">district_id.</param>
        /// <returns>DtoSeedUserInput.</returns>
        DtoSeedUserInput GetSeedusedIputViewSubmission(int seasonId, int cropId, int district_id);

        /// <summary>
        /// GetSeedUsedVarietyname.
        /// </summary>
        /// <param name="seasonId">seasonId.</param>
        /// <param name="cropId">cropId.</param>
        /// <param name="district_id">district_id.</param>
        /// <returns>DtoSeedUserInput.</returns>
        DtoSeedUserInput GetSeedUsedVarietyname(int seasonId, int cropId, int district_id);

        /// <summary>
        /// GetSeedusedinputViewSubmissionOffline.
        /// </summary>
        /// <param name="district_id">district_id.</param>
        /// <returns>DtoSeedUserInput list.</returns>
        List<DtoSeedUserInput> GetSeedusedinputViewSubmissionOffline(int district_id);

        /// <summary>
        /// GetAllColdStorageByDistrict.
        /// </summary>
        /// <param name="getHortiReportPHMS">getHortiReportPHMS.</param>
        /// <returns>GetGrpdwnColdStorageModel list.</returns>
        List<GetGrpdwnColdStorageModel> GetAllColdStorageByDistrict(GetHortiReportPhmsModel getHortiReportPHMS);

        /// <summary>
        /// GetAgricultureBlockkPanchayatByDistrict.
        /// </summary>
        /// <param name="districtId">districtId.</param>
        /// <returns>BlockPanchayatByDistrict.</returns>
        BlockPanchayatByDistrict GetAgricultureBlockkPanchayatByDistrict(string districtId);

        /// <summary>
        /// GetSeedPerformanceDataReport.
        /// </summary>
        /// <param name="year">year.</param>
        /// <param name="season">season.</param>
        /// <param name="scheme">scheme.</param>
        /// <param name="activity">activity.</param>
        /// <param name="status">status.</param>
        /// <param name="district">district.</param>
        /// <param name="block">block.</param>
        /// <param name="panchayat">panchayat.</param>
        /// <param name="crop_type">crop_type.</param>
        /// <returns>List.</returns>
        List<string> GetSeedPerformanceDataReport(int year, int season, string scheme, string activity, string status, string district, string block, string panchayat, string crop_type);

        /// <summary>
        /// To Get Combine Pass Data Report.
        /// </summary>
        /// <param name="combinePassModel">combinePassModel.</param>
        /// <returns>list of string.</returns>
        List<string> GetCombinePassDataReport(CombinePassInputModel combinePassModel);

        /// <summary>
        /// To Get Agri Asset Data Report.
        /// </summary>
        /// <param name="agriasstmodel">agriasstmodel.</param>
        /// <returns>list of string.</returns>
        List<string> GetAgriAsstDataReport(AgricultureAssetManagement agriasstmodel);

        /// <summary>
        /// GetPiasDataReport.
        /// </summary>
        /// <param name="piasDataReportInputModel">piasDataReportInputModel.</param>
        /// <returns>list of string.</returns>
        List<string> GetPiasDataReport(GetPiasDataReportInputModel piasDataReportInputModel);

        /// <summary>
        /// PostSeedDemandAc.
        /// </summary>
        /// <param name="input">input.</param>
        /// <returns>Response.</returns>
        int PostSeedDemandAc(SeedIndentInput input);

        /// <summary>
        /// GetSeedDemandACViewSubmission.
        /// </summary>
        /// <param name="seasonId">seasonId.</param>
        /// <param name="cropId">cropId.</param>
        /// <param name="districtId">districtId.</param>
        /// <param name="panchayatId">panchayatId.</param>
        /// <returns>DtoSeedUserInput.</returns>
        DtoSeedIndentInput GetSeedDemandACViewSubmission(int seasonId, int cropId, int districtId, int panchayatId);

        /// <summary>
        /// GetSeedDemandAcViewSubmissionOffline.
        /// </summary>
        /// <param name="districtId">districtId.</param>
        /// <param name="panchayatId">panchayatId.</param>
        /// <returns>DtoSeedUserInput.</returns>
        List<DtoSeedIndentInput> GetSeedDemandAcViewSubmissionOffline(int districtId, int panchayatId);

        /// <summary>
        /// GetSeedUsedVarietynameAC.
        /// </summary>
        /// <param name="seasonId">seasonId.</param>
        /// <param name="cropId">cropId.</param>
        /// <param name="districtId">districtId.</param>
        /// <param name="panchayatId">panchayatId.</param>
        /// <returns>DtoSeedIndentInput.</returns>
        DtoSeedIndentInput GetSeedUsedVarietynameAC(int seasonId, int cropId, int districtId, int panchayatId);

        /// <summary>
        /// GetAllMarketByDistrict.
        /// </summary>
        /// <param name="districtId">districtId.</param>
        /// <returns>MarketData List.</returns>
        List<MarketData> GetAllMarketByDistrict(string districtId);

        /// <summary>
        /// PostPlantIndent.
        /// </summary>
        /// <param name="input">input.</param>
        /// <returns>Response.</returns>
        int PostPlantIndent(PlantIndentInput input);

        /// <summary>
        /// GetSeedDemandBHOViewSubmission.
        /// </summary>
        /// <param name="seasonId">seasonId.</param>
        /// <param name="cropId">cropId.</param>
        /// <param name="blockId">blockId.</param>
        /// <param name="panchayatId">panchayatId.</param>
        /// <returns>DtoPlantIndentInput.</returns>
        DtoPlantIndentInput GetSeedDemandBHOViewSubmission(int seasonId, int cropId, string blockId, int panchayatId);

        /// <summary>
        /// GetSeedDemandBHOViewSubmissionOffline.
        /// </summary>
        /// <param name="districtId">districtId.</param>
        /// <param name="seasonId">seasonId.</param>
        /// <param name="blockId">blockId.</param>
        /// <returns>DtoPlantIndentInput List.</returns>
        List<DtoPlantIndentInput> GetSeedDemandBHOViewSubmissionOffline(int districtId, int seasonId, string blockId);

        /// <summary>
        /// GetSeedUsedVarietyNameBHO.
        /// </summary>
        /// <param name="seasonId">seasonId.</param>
        /// <param name="cropId">cropId.</param>
        /// <param name="districtId">districtId.</param>
        /// <param name="blockId">blockId.</param>
        /// <param name="panchayatId">panchayatId.</param>
        /// <returns>Values.</returns>
        DtoPlantIndentInput GetSeedUsedVarietyNameBHO(int seasonId, int cropId, int districtId, string blockId, int panchayatId);

        /// <summary>
        /// GetFutureSeason.
        /// </summary>
        /// <returns>FutureSeason List.</returns>
        List<FutureSeason> GetFutureSeason();

        /// <summary>
        /// GetCropName.
        /// </summary>
        /// <param name="seasonId">seasonId.</param>
        /// <returns>Crop Name List.</returns>
        List<CropNames> GetCropName(int seasonId);

        /// <summary>
        /// GetPlantName.
        /// </summary>
        /// <param name="plantCategory">plantCategory.</param>
        /// <param name="seasonId">seasonId.</param>
        /// <returns>Plant Name List.</returns>
        List<PlantNames> GetPlantName(string plantCategory, int seasonId);

        /// <summary>
        /// GetCropSeedVariety.
        /// </summary>
        /// <param name="cropId">cropId.</param>
        /// <param name="seasonId">seasonId.</param>
        /// <returns>Seed Variety List.</returns>
        List<SeedVarietyList> GetCropSeedVariety(int cropId, int seasonId);

        /// <summary>
        /// GetPlantSeedVariety.
        /// </summary>
        /// <param name="cropId">cropId.</param>
        /// <param name="seasonId">seasonId.</param>
        /// <returns>Seed Variety List.</returns>
        List<SeedVarietyList> GetPlantSeedVariety(int cropId, int seasonId);

        /// <summary>
        /// GetCropCategoryBySeason.
        /// </summary>
        /// <param name="seasonId">seasonId.</param>
        /// <returns>Crop Category List.</returns>
        List<CropCategoryEntity> GetCropCategoryBySeason(int seasonId);

        /// <summary>
        /// GetInputIndentDataReport.
        /// </summary>
        /// <param name="year">year.</param>
        /// <param name="seasonId">season.</param>
        /// <param name="activity">activity.</param>
        /// <param name="cropVarietyId">cropVarietyId.</param>
        /// <param name="plantCategory">plantCategory.</param>
        /// <param name="cropId">cropId.</param>
        /// <param name="district">district.</param>
        /// <param name="block">block.</param>
        /// <param name="panchayat">panchayat.</param>
        /// <returns>list.</returns>
        List<string> GetInputIndentDataReport(int year, string seasonId, string activity, string cropVarietyId, string plantCategory, string cropId, string district, string block, string panchayat);
    }
}
