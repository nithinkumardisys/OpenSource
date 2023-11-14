//------------------------------------------------------------------------------
// <copyright file="CropService.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------
namespace ADIDAS.Service.DataServices
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ADIDAS.Model.DTO;
    using ADIDAS.Model.Entities;
    using ADIDAS.Repository.Interfaces;
    using ADIDAS.Service.Interfaces;

    /// <summary>
    /// CropService.
    /// </summary>
    public class CropService : ICropService
    {
        private readonly ICropRepository cropRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="CropService"/> class.
        /// </summary>
        /// <param name="cropRepository">cropRepository.</param>
        public CropService(ICropRepository cropRepository)
        {
            this.cropRepository = cropRepository;
        }

        /// <summary>
        /// GetSeason.
        /// </summary>
        /// <returns>List.</returns>
        public List<SeasonDim> GetSeason()
        {
            return this.cropRepository.GetSeason();
        }

        /// <summary>
        /// GetCrop.
        /// </summary>
        /// <param name="seasonName">seasonName.</param>
        /// <param name="districtId">districtId.</param>
        /// <returns>List.</returns>
        public List<CropSeasonEntity> GetCrop(string seasonName, string districtId)
        {
            return this.cropRepository.GetCrop(seasonName, districtId);
        }

        /// <summary>
        /// GetDistinctCrop.
        /// </summary>
        /// <param name="districtId">districtId.</param>
        /// <returns>List.</returns>
        public List<CropSeasonEntity> GetDistinctCrop(string districtId)
        {
            return this.cropRepository.GetDistinctCrop(districtId);
        }

        /// <summary>
        /// GetCropCategory.
        /// </summary>
        /// <param name="crop_Type">crop_Type.</param>
        /// <returns>List.</returns>
        public List<CropCategoryEntity> GetCropCategory(string crop_Type)
        {
            return this.cropRepository.GetCropCategory(crop_Type);
        }

        /// <summary>
        /// GetLGDirectory.
        /// </summary>
        /// <returns>List.</returns>
        public List<LgDirectoryPanchayatDim> GetLGDirectory()
        {
            return this.cropRepository.GetLGDirectory();
        }

        /// <summary>
        /// GetRainfallDistricts.
        /// </summary>
        /// <returns>List.</returns>
        public List<DistrictList> GetRainfallDistricts()
        {
            return this.cropRepository.GetRainfallDistricts();
        }

        /// <summary>
        /// GetLGDirectoryDistrict.
        /// </summary>
        /// <param name="districtId">districtId.</param>
        /// <returns>List.</returns>
        public List<LgDirectoryPanchayatDim> GetLGDirectoryDistrict(string districtId)
        {
            return this.cropRepository.GetLGDirectoryDistrict(districtId);
        }

        /// <summary>
        /// GetLGDirectoryBlock.
        /// </summary>
        /// <param name="blockId">blockId.</param>
        /// <returns>List.</returns>
        public List<LgDirectoryPanchayatDim> GetLGDirectoryBlock(string blockId)
        {
            return this.cropRepository.GetLGDirectoryBlock(blockId);
        }

        /// <summary>
        /// GetLGDirectoryPancht.
        /// </summary>
        /// <param name="panchayatId">panchayatId.</param>
        /// <returns>List.</returns>
        public List<LgDirectoryPanchayatDim> GetLGDirectoryPancht(string panchayatId)
        {
            return this.cropRepository.GetLGDirectoryPancht(panchayatId);
        }

        /// <summary>
        /// GetLGDirectoryVillage.
        /// </summary>
        /// <param name="panchayatId">panchayatId.</param>
        /// <returns>LgDirectoryVillageDim List.</returns>
        public List<LgDirectoryVillageDim> GetLGDirectoryVillage(int panchayatId)
        {
            return this.cropRepository.GetLGDirectoryVillage(panchayatId);
        }

        /// <summary>
        /// GetLGDirectoryVillage.
        /// </summary>
        /// <param name="userId">userId.</param>
        /// <param name="panchayatId">panchayatId.</param>
        /// <returns>LgDirectoryVillageDim List.</returns>
        public List<LgDirectoryVillageDim> GetLGDirectoryVillageByUserId(int userId, int panchayatId)
        {
            return this.cropRepository.GetLGDirectoryVillageByUserId(userId, panchayatId);
        }

        /// <summary>
        /// GetSource.
        /// </summary>
        /// <param name="attributeName">attributeName.</param>
        /// <returns>List.</returns>
        public List<MobileAttributeConfig> GetSource(string attributeName)
        {
            return this.cropRepository.GetSource(attributeName);
        }

        /// <summary>
        /// GetCropCoverageAimPancht.
        /// </summary>
        /// <param name="seasonId">seasonId.</param>
        /// <param name="panchayatId">panchayatId.</param>
        /// <returns>List.</returns>
        public List<CropCoverageAimPancht> GetCropCoverageAimPancht(string seasonId, string panchayatId)
        {
            return this.cropRepository.GetCropCoverageAimPancht(seasonId, panchayatId);
        }

        /// <summary>
        /// GetCropCoverageAimVillage.
        /// </summary>
        /// <param name="seasonId">seasonId.</param>
        /// <param name="panchayatId">panchayatId.</param>
        /// <param name="villageId">villageId.</param>
        /// <returns>List.</returns>
        public List<CropCoverageAimVillage> GetCropCoverageAimVillage(string seasonId, string panchayatId, string villageId)
        {
            return this.cropRepository.GetCropCoverageAimVillage(seasonId, panchayatId, villageId);
        }

        /// <summary>
        /// GetCropCoverageAimBlock.
        /// </summary>
        /// <param name="blockId">blockId.</param>
        /// <returns>List.</returns>
        public List<CropCoverageAimBlock> GetCropCoverageAimBlock(string blockId)
        {
            return this.cropRepository.GetCropCoverageAimBlock(blockId);
        }

        /// <summary>
        /// GetCropCoverageAimDistrict.
        /// </summary>
        /// <param name="districtId">districtId.</param>
        /// <returns>List.</returns>
        public List<CropCoverageAimDistrict> GetCropCoverageAimDistrict(string districtId)
        {
            return this.cropRepository.GetCropCoverageAimDistrict(districtId);
        }

        /// <summary>
        /// GetCropCoverageActualPancht.
        /// </summary>
        /// <param name="seasonId">seasonId.</param>
        /// <param name="panchayatId">panchayatId.</param>
        /// <returns>List.</returns>
        public List<CropCoverageActualVillage> GetCropCoverageActualPancht(string seasonId, string panchayatId)
        {
            return this.cropRepository.GetCropCoverageActualPancht(seasonId, panchayatId);
        }

        /// <summary>
        /// GetCropCoverageActualVillage.
        /// </summary>
        /// <param name="seasonId">seasonId.</param>
        /// <param name="panchayatId">panchayatId.</param>
        /// <param name="villageId">villageId.</param>
        /// <returns>List.</returns>
        public List<CropCoverageActualVillage> GetCropCoverageActualVillage(string seasonId, string panchayatId, string villageId)
        {
            return this.cropRepository.GetCropCoverageActualVillage(seasonId, panchayatId, villageId);
        }

        /// <summary>
        /// GetCropCoverageActualPanchtCurrDate.
        /// </summary>
        /// <param name="seasonId">seasonId.</param>
        /// <param name="panchayatId">panchayatId.</param>
        /// <returns>List.</returns>
        public List<CropCoverageActualVillage> GetCropCoverageActualPanchtCurrDate(string seasonId, string panchayatId)
        {
            return this.cropRepository.GetCropCoverageActualPanchtCurrDate(seasonId, panchayatId);
        }

        /// <summary>
        /// GetCropCoverageActualVillageCurrDate.
        /// </summary>
        /// <param name="seasonId">seasonId.</param>
        /// <param name="panchayatId">panchayatId.</param>
        /// <param name="villageId">villageId.</param>
        /// <returns>List.</returns>
        public List<CropCoverageActualVillage> GetCropCoverageActualVillageCurrDate(string seasonId, string panchayatId, string villageId)
        {
            return this.cropRepository.GetCropCoverageActualVillageCurrDate(seasonId, panchayatId, villageId);
        }

        /// <summary>
        /// GetCropCoverageActualBlock.
        /// </summary>
        /// <param name="blockId">blockId.</param>
        /// <returns>List.</returns>
        public List<CropCoverageActualBlock> GetCropCoverageActualBlock(string blockId)
        {
            return this.cropRepository.GetCropCoverageActualBlock(blockId);
        }

        /// <summary>
        /// GetCropCoverageActualDistrict.
        /// </summary>
        /// <param name="districtId">districtId.</param>
        /// <returns>List.</returns>
        public List<CropCoverageActualDistrict> GetCropCoverageActualDistrict(string districtId)
        {
            return this.cropRepository.GetCropCoverageActualDistrict(districtId);
        }

        /// <summary>
        /// GetCropDamagePancht.
        /// </summary>
        /// <param name="seasonId">seasonId.</param>
        /// <param name="panchayatId">panchayatId.</param>
        /// <returns>List.</returns>
        public List<CropDamagePancht> GetCropDamagePancht(string seasonId, string panchayatId)
        {
            return this.cropRepository.GetCropDamagePancht(seasonId, panchayatId);
        }

        /// <summary>
        /// GetCropDamageBlock.
        /// </summary>
        /// <param name="blockId">blockId.</param>
        /// <returns>List.</returns>
        public List<CropDamageBlock> GetCropDamageBlock(string blockId)
        {
            return this.cropRepository.GetCropDamageBlock(blockId);
        }

        /// <summary>
        /// GetCropDamageDistrict.
        /// </summary>
        /// <param name="districtId">districtId.</param>
        /// <returns>List.</returns>
        public List<CropDamageDistrict> GetCropDamageDistrict(string districtId)
        {
            return this.cropRepository.GetCropDamageDistrict(districtId);
        }

        /// <summary>
        /// InsertCropDim.
        /// </summary>
        /// <param name="crop">crop.</param>
        /// <returns>List.</returns>
        public int InsertCropDim(CropSeasonEntity crop)
        {
            return this.cropRepository.InsertCropDim(crop);
        }

        /// <summary>
        /// InsertCropCoverageAimPancht.
        /// </summary>
        /// <param name="crop">crop.</param>
        /// <returns>List.</returns>
        public List<CropCoverageTargetPanchytApprovalResponse> InsertCropCoverageAimPancht(List<CropCoverageAimPancht> crop)
        {
            return this.cropRepository.InsertCropCoverageAimPancht(crop);
        }

        /// <summary>
        /// InsertCropCoverageAimVillage.
        /// </summary>
        /// <param name="crop">crop.</param>
        /// <returns>List.</returns>
        public List<CropCoverageTargetVillageApprovalResponse> InsertCropCoverageAimVillage(List<CropCoverageAimVillage> crop)
        {
            return this.cropRepository.InsertCropCoverageAimVillage(crop);
        }

        /// <summary>
        /// InsertCropCoverageActualPancht.
        /// </summary>
        /// <param name="crop">crop.</param>
        /// <returns>List.</returns>
        public List<CropCoverageActualPanchayatApprovalResponse> InsertCropCoverageActualPancht(List<CropCoverageActualVillage> crop)
        {
            return this.cropRepository.InsertCropCoverageActualPancht(crop);
        }

        /// <summary>
        /// InsertCropCoverageActualVillage.
        /// </summary>
        /// <param name="crop">crop.</param>
        /// <returns>List.</returns>
        public List<CropCoverageActualVillageApprovalResponse> InsertCropCoverageActualVillage(List<CropCoverageActualVillage> crop)
        {
            return this.cropRepository.InsertCropCoverageActualVillage(crop);
        }

        /// <summary>
        /// InsertCropDamagePancht.
        /// </summary>
        /// <param name="crop">crop.</param>
        /// <returns>List.</returns>
        public List<CropDamagePanchayatResponse> InsertCropDamagePancht(List<CropDamagePancht> crop)
        {
            return this.cropRepository.InsertCropDamagePancht(crop);
        }

        /// <summary>
        /// InsertCropCoverageActualApproval.
        /// </summary>
        /// <param name="cropCoverageActual">cropCoverageActual.</param>
        /// <returns>List.</returns>
        public List<CropCoverageActualBlockApprovalResponse> InsertCropCoverageActualApproval(CropCoverageActual cropCoverageActual)
        {
            return this.cropRepository.InsertCropCoverageActualApproval(cropCoverageActual);
        }

        /// <summary>
        /// InsertCropCoverageTargetApproval.
        /// </summary>
        /// <param name="cropCoverageTarget">cropCoverageTarget.</param>
        /// <returns>List.</returns>
        public List<CropCoverageTargetBlockApproval> InsertCropCoverageTargetApproval(CropCoverageAim cropCoverageTarget)
        {
            return this.cropRepository.InsertCropCoverageTargetApproval(cropCoverageTarget);
        }

        /// <summary>
        /// GetCropCoverageTargetDAO.
        /// </summary>
        /// <param name="district_id">district_id.</param>
        /// <param name="season_id">season_id.</param>
        /// <param name="crop_id">crop_id.</param>
        /// <returns>List.</returns>
        public CropCoverageAim GetCropCoverageTargetDAO(int district_id, int season_id, int crop_id)
        {
            return this.cropRepository.GetCropCoverageTargetDAO(district_id, season_id, crop_id);
        }

        /// <summary>
        /// GetCropCoverageActualDAO.
        /// </summary>
        /// <param name="district_id">district_id.</param>
        /// <param name="season_id">season_id.</param>
        /// <param name="crop_id">crop_id.</param>
        /// <returns>List.</returns>
        public CropCoverageActual GetCropCoverageActualDAO(int district_id, int season_id, int crop_id)
        {
            return this.cropRepository.GetCropCoverageActualDAO(district_id, season_id, crop_id);
        }

        /// <summary>
        /// GetCropDamageDAO.
        /// </summary>
        /// <param name="district_id">district_id.</param>
        /// <param name="season_id">season_id.</param>
        /// <param name="crop_id">crop_id.</param>
        /// <returns>List.</returns>
        public CropDamage GetCropDamageDAO(int district_id, int season_id, int crop_id)
        {
            return this.cropRepository.GetCropDamageDAO(district_id, season_id, crop_id);
        }

        /// <summary>
        /// GetCropCoverageTargetBAO.
        /// </summary>
        /// <param name="district_id">district_id.</param>
        /// <param name="block_id">block_id.</param>
        /// <param name="season_id">season_id.</param>
        /// <param name="crop_id">crop_id.</param>
        /// <returns>List.</returns>
        public CropCoverageAim GetCropCoverageTargetBAO(int district_id, int block_id, int season_id, int crop_id)
        {
            return this.cropRepository.GetCropCoverageTargetBAO(district_id, block_id, season_id, crop_id);
        }

        /// <summary>
        /// GetCropCoverageActualBAO.
        /// </summary>
        /// <param name="district_id">district_id.</param>
        /// <param name="block_id">block_id.</param>
        /// <param name="season_id">season_id.</param>
        /// <param name="crop_id">crop_id.</param>
        /// <returns>List.</returns>
        public CropCoverageActual GetCropCoverageActualBAO(int district_id, int block_id, int season_id, int crop_id)
        {
            return this.cropRepository.GetCropCoverageActualBAO(district_id, block_id, season_id, crop_id);
        }

        /// <summary>
        /// .
        /// </summary>
        /// <param name="district_id">district_id.</param>
        /// <param name="block_id">block_id.</param>
        /// <param name="season_id">season_id.</param>
        /// <param name="crop_id">crop_id.</param>
        /// <returns>List.</returns>
        public CropDamage GetCropDamageBAO(int district_id, int block_id, int season_id, int crop_id)
        {
            return this.cropRepository.GetCropDamageBAO(district_id, block_id, season_id, crop_id);
        }

        /// <summary>
        /// InsertCropDamageApproval.
        /// </summary>
        /// <param name="cropDamage">cropDamage.</param>
        /// <returns>List.</returns>
        public List<CropDamageBlockApprovalResponse> InsertCropDamageApproval(CropDamage cropDamage)
        {
            return this.cropRepository.InsertCropDamageApproval(cropDamage);
        }

        /// <summary>
        /// GetCropCoverageTargetDAODelta.
        /// </summary>
        /// <param name="district_id">district_id.</param>
        /// <param name="season_id">season_id.</param>
        /// <param name="last_refresh_time">last_refresh_time.</param>
        /// <returns>List.</returns>
        public List<CropCoverageAim> GetCropCoverageTargetDAODelta(int district_id, int season_id, DateTime last_refresh_time)
        {
            return this.cropRepository.GetCropCoverageTargetDAODelta(district_id, season_id, last_refresh_time);
        }

        /// <summary>
        /// GetCropCoverageTargetBAODelta.
        /// </summary>
        /// <param name="district_id">district_id.</param>
        /// <param name="block_id">block_id.</param>
        /// <param name="season_id">season_id.</param>
        /// <param name="last_refresh_time">last_refresh_time.</param>
        /// <returns>List.</returns>
        public List<CropCoverageAim> GetCropCoverageTargetBAODelta(int district_id, int block_id, int season_id, DateTime last_refresh_time)
        {
            return this.cropRepository.GetCropCoverageTargetBAODelta(district_id, block_id, season_id, last_refresh_time);
        }

        /// <summary>
        /// GetCropCoverageActualDAODelta.
        /// </summary>
        /// <param name="district_id">district_id.</param>
        /// <param name="season_id">season_id.</param>
        /// <param name="last_refresh_time">last_refresh_time.</param>
        /// <returns>List.</returns>
        public List<CropCoverageActual> GetCropCoverageActualDAODelta(int district_id, int season_id, DateTime last_refresh_time)
        {
            return this.cropRepository.GetCropCoverageActualDAODelta(district_id, season_id, last_refresh_time);
        }

        /// <summary>
        /// GetCropCoverageActualBAODelta.
        /// </summary>
        /// <param name="district_id">district_id.</param>
        /// <param name="block_id">block_id.</param>
        /// <param name="season_id">season_id.</param>
        /// <param name="last_refresh_time">last_refresh_time.</param>
        /// <returns>List.</returns>
        public List<CropCoverageActual> GetCropCoverageActualBAODelta(int district_id, int block_id, int season_id, DateTime last_refresh_time)
        {
            return this.cropRepository.GetCropCoverageActualBAODelta(district_id, block_id, season_id, last_refresh_time);
        }

        /// <summary>
        /// GetCropDamagePanchtDelta.
        /// </summary>
        /// <param name="seasonId">seasonId.</param>
        /// <param name="panchayatId">panchayatId.</param>
        /// <param name="lastrefreshedTime">lastrefreshedTime.</param>
        /// <returns>List.</returns>
        public List<CropDamagePancht> GetCropDamagePanchtDelta(string seasonId, string panchayatId, DateTime lastrefreshedTime)
        {
            return this.cropRepository.GetCropDamagePanchtDelta(seasonId, panchayatId, lastrefreshedTime);
        }

        /// <summary>
        /// GetCropCoverageAimPanchtDelta.
        /// </summary>
        /// <param name="seasonId">seasonId.</param>
        /// <param name="panchayatId">panchayatId.</param>
        /// <param name="lastrefreshedTime">lastrefreshedTime.</param>
        /// <returns>List.</returns>
        public List<CropCoverageAimPancht> GetCropCoverageAimPanchtDelta(string seasonId, string panchayatId, DateTime lastrefreshedTime)
        {
            return this.cropRepository.GetCropCoverageAimPanchtDelta(seasonId, panchayatId, lastrefreshedTime);
        }

        /// <summary>
        /// GetCropCoverageActualPanchtDelta.
        /// </summary>
        /// <param name="seasonId">seasonId.</param>
        /// <param name="panchayatId">panchayatId.</param>
        /// <param name="lastrefreshedTime">lastrefreshedTime.</param>
        /// <returns>List.</returns>
        public List<CropCoverageActualPancht> GetCropCoverageActualPanchtDelta(string seasonId, string panchayatId, DateTime lastrefreshedTime)
        {
            return this.cropRepository.GetCropCoverageActualPanchtDelta(seasonId, panchayatId, lastrefreshedTime);
        }

        /// <summary>
        /// GetCropCoverageTargetBAOOffline.
        /// </summary>
        /// <param name="district_id">district_id.</param>
        /// <param name="block_id">block_id.</param>
        /// <param name="season_id">season_id.</param>
        /// <param name="crop_ids">crop_ids.</param>
        /// <returns>List.</returns>
        public List<CropCoverageAim> GetCropCoverageTargetBAOOffline(int district_id, int block_id, int season_id, string crop_ids)
        {
            return this.cropRepository.GetCropCoverageTargetBAOOffline(district_id, block_id, season_id, crop_ids);
        }

        /// <summary>
        /// GetCropCoverageTargetDAOOffline.
        /// </summary>
        /// <param name="district_id">district_id.</param>
        /// <param name="season_id">season_id.</param>
        /// <param name="crop_ids">crop_ids.</param>
        /// <returns>List.</returns>
        public List<CropCoverageAim> GetCropCoverageTargetDAOOffline(int district_id, int season_id, string crop_ids)
        {
            return this.cropRepository.GetCropCoverageTargetDAOOffline(district_id, season_id, crop_ids);
        }

        /// <summary>
        /// GetCropCoverageActualBAOOffline.
        /// </summary>
        /// <param name="district_id">district_id.</param>
        /// <param name="block_id">block_id.</param>
        /// <param name="season_id">season_id.</param>
        /// <param name="crop_ids">crop_ids.</param>
        /// <returns>List.</returns>
        public List<CropCoverageActual> GetCropCoverageActualBAOOffline(int district_id, int block_id, int season_id, string crop_ids)
        {
            return this.cropRepository.GetCropCoverageActualBAOOffline(district_id, block_id, season_id, crop_ids);
        }

        /// <summary>
        /// GetCropCoverageActualDAOOffline.
        /// </summary>
        /// <param name="district_id">district_id.</param>
        /// <param name="season_id">season_id.</param>
        /// <param name="crop_ids">crop_ids.</param>
        /// <returns>List.</returns>
        public List<CropCoverageActual> GetCropCoverageActualDAOOffline(int district_id, int season_id, string crop_ids)
        {
            return this.cropRepository.GetCropCoverageActualDAOOffline(district_id, season_id, crop_ids);
        }

        /// <summary>
        /// InsertSeedPerformance.
        /// </summary>
        /// <param name="seedPerformance">seedPerformance.</param>
        /// <returns>List.</returns>
        public async Task<InsertSeedPerformanceResponse> InsertSeedPerformance(SeedPerformance seedPerformance)
        {
            return await this.cropRepository.InsertSeedPerformance(seedPerformance);
        }

        /// <summary>
        /// GetSeedPerformance.
        /// </summary>
        /// <param name="panchayatId">panchayatId.</param>
        /// <param name="seasonId">seasonId.</param>
        /// <returns>List.</returns>
        public List<DtoSeedPerformance> GetSeedPerformance(int panchayatId, int seasonId)
        {
            return this.cropRepository.GetSeedPerformance(panchayatId, seasonId);
        }

        /// <summary>
        /// GetSchemesByPanchayat.
        /// </summary>
        /// <param name="panchayatId">panchayatId.</param>
        /// <returns>List.</returns>
        public List<SchemeModel> GetSchemesByPanchayat(int panchayatId)
        {
            return this.cropRepository.GetSchemesByPanchayat(panchayatId);
        }

        /// <summary>
        /// GetBAONotSubmittedByBlock.
        /// </summary>
        /// <param name="block_id">block_id.</param>
        /// <returns>List.</returns>
        public List<CropDamageDetailsGet> GetBAONotSubmittedByBlock(int block_id)
        {
            return this.cropRepository.GetBAONotSubmittedByBlock(block_id);
        }

        /// <summary>
        /// GetAllColdStorage.
        /// </summary>
        /// <param name="getHortiReportPHMS">getHortiReportPHMS.</param>
        /// <returns>List.</returns>
        public List<GetGrpdwnColdStorageModel> GetAllColdStorage(GetHortiReportPhmsModel getHortiReportPHMS)
        {
            return this.cropRepository.GetAllColdStorage(getHortiReportPHMS);
        }

        /// <summary>
        /// GetDAONotSubmittedByDistrict.
        /// </summary>
        /// <param name="district_Id">district_Id.</param>
        /// <returns>List.</returns>
        public List<CropDamageDetailsGet> GetDAONotSubmittedByDistrict(int district_Id)
        {
            return this.cropRepository.GetDAONotSubmittedByDistrict(district_Id);
        }

        /// <summary>
        /// GetCropDamageDAOOffline.
        /// </summary>
        /// <param name="district_id">district_id.</param>
        /// <param name="season_id">season_id.</param>
        /// <param name="crop_ids">crop_ids.</param>
        /// <returns>List.</returns>
        public List<CropDamage> GetCropDamageDAOOffline(int district_id, int season_id, string crop_ids)
        {
            return this.cropRepository.GetCropDamageDAOOffline(district_id, season_id, crop_ids);
        }

        /// <summary>
        /// GetCropDamageBAOOffline.
        /// </summary>
        /// <param name="district_id">district_id.</param>
        /// <param name="block_id">block_id.</param>
        /// <param name="season_id">season_id.</param>
        /// <param name="crop_ids">crop_ids.</param>
        /// <returns>List.</returns>
        public List<CropDamage> GetCropDamageBAOOffline(int district_id, int block_id, int season_id, string crop_ids)
        {
            return this.cropRepository.GetCropDamageBAOOffline(district_id, block_id, season_id, crop_ids);
        }

        /// <summary>
        /// GetCropDamageBAOOfflineDelta.
        /// </summary>
        /// <param name="district_id">district_id.</param>
        /// <param name="block_id">block_id.</param>
        /// <param name="season_id">season_id.</param>
        /// <param name="last_refresh_time">last_refresh_time.</param>
        /// <returns>List.</returns>
        public List<CropDamage> GetCropDamageBAOOfflineDelta(int district_id, int block_id, int season_id, DateTime last_refresh_time)
        {
            return this.cropRepository.GetCropDamageBAOOfflineDelta(district_id, block_id, season_id, last_refresh_time);
        }

        /// <summary>
        /// GetCropDamageDAOOfflineDelta.
        /// </summary>
        /// <param name="district_id">district_id.</param>
        /// <param name="season_id">season_id.</param>
        /// <param name="last_refresh_time">last_refresh_time.</param>
        /// <returns>List.</returns>
        public List<CropDamage> GetCropDamageDAOOfflineDelta(int district_id, int season_id, DateTime last_refresh_time)
        {
            return this.cropRepository.GetCropDamageDAOOfflineDelta(district_id, season_id, last_refresh_time);
        }

        /// <summary>
        /// GetHorticultureSubmittedDetails.
        /// </summary>
        /// <param name="seasonId">seasonId.</param>
        /// <param name="districtId">districtId.</param>
        /// <returns>List.</returns>
        public List<HorticultureDetails> GetHorticultureSubmittedDetails(int seasonId, int districtId)
        {
            return this.cropRepository.GetHorticultureSubmittedDetails(seasonId, districtId);
        }

        /// <summary>
        /// GetHorticultureCrop.
        /// </summary>
        /// <param name="districtId">districtId.</param>
        /// <param name="seasonId">seasonId.</param>
        /// <returns>List.</returns>
        public List<HorticultureCrop> GetHorticultureCrop(int districtId, int seasonId)
        {
            return this.cropRepository.GetHorticultureCrop(districtId, seasonId);
        }

        /// <summary>
        /// GetEstdCropDamagePercnt.
        /// </summary>
        /// <param name="cropDamageReasonModel">cropDamageReasonModel.</param>
        /// <returns>List.</returns>
        public List<CropDamageGetModel> GetEstdCropDamagePercnt(CropDamageGetModel cropDamageReasonModel)
        {
            return this.cropRepository.GetEstdCropDamagePercnt(cropDamageReasonModel);
        }

        /// <summary>
        /// GetAllDistrict.
        /// </summary>
        /// <returns>List.</returns>
        public List<DistrictList> GetAllDistrict()
        {
            return this.cropRepository.GetAllDistrict();
        }

        /// <summary>
        /// GetACCropCvrgCoveredAreaPancht.
        /// </summary>
        /// <param name="panchayat_Id">panchayat_Id.</param>
        /// <param name="damage_id">damage_id.</param>
        /// <returns>List.</returns>
        public CropDamageDetailsGet GetACCropCvrgCoveredAreaPancht(string panchayat_Id, int damage_id)
        {
            return this.cropRepository.GetACCropCvrgCoveredAreaPancht(panchayat_Id, damage_id);
        }

        /// <summary>
        /// GetBAOCropDamageDetailsBlock.
        /// </summary>
        /// <param name="districtId">districtId.</param>
        /// <param name="blockid">blockid.</param>
        /// <param name="damage_id">damage_id.</param>
        /// <returns>List.</returns>
        public InsBaoCropdmgModel GetBAOCropDamageDetailsBlock(int districtId, int blockid, int damage_id)
        {
            return this.cropRepository.GetBAOCropDamageDetailsBlock(districtId, blockid, damage_id);
        }

        /// <summary>
        /// GetDAOCropDamageDetailsBlock.
        /// </summary>
        /// <param name="district_Id">district_Id.</param>
        /// <param name="damage_id">damage_id.</param>
        /// <returns>List.</returns>
        public InsBaoCropdmgModel GetDAOCropDamageDetailsBlock(string district_Id, int damage_id)
        {
            return this.cropRepository.GetDAOCropDamageDetailsBlock(district_Id, damage_id);
        }

        /// <summary>
        /// GetACCropCvrgCoveredAreaPanchtOffline.
        /// </summary>
        /// <param name="panchayat_Id">panchayat_Id.</param>
        /// <returns>List.</returns>
        public List<CropDamageDetailsGet> GetACCropCvrgCoveredAreaPanchtOffline(string panchayat_Id)
        {
            return this.cropRepository.GetACCropCvrgCoveredAreaPanchtOffline(panchayat_Id);
        }

        /// <summary>
        /// GetBAOCropDamageDetailsBlockOffline.
        /// </summary>
        /// <param name="district_Id">district_Id.</param>
        /// <param name="blockId">blockId.</param>
        /// <returns>List.</returns>
        public List<InsBaoCropdmgModel> GetBAOCropDamageDetailsBlockOffline(int district_Id, int blockId)
        {
            return this.cropRepository.GetBAOCropDamageDetailsBlockOffline(district_Id, blockId);
        }

        /// <summary>
        /// GetDAOCropDamageDetailsBlockOffline.
        /// </summary>
        /// <param name="districtId">districtId.</param>
        /// <returns>List.</returns>
        public List<InsBaoCropdmgModel> GetDAOCropDamageDetailsBlockOffline(string districtId)
        {
            return this.cropRepository.GetDAOCropDamageDetailsBlockOffline(districtId);
        }

        /// <summary>
        /// GetBAOApprovedCropDamageDetailsBlock.
        /// </summary>
        /// <param name="district_Id">district_Id.</param>
        /// <param name="block_id">block_id.</param>
        /// <param name="panchayat_Id">panchayat_Id.</param>
        /// <param name="damage_id">damage_id.</param>
        /// <returns>List.</returns>
        public CropDamageAll GetBAOApprovedCropDamageDetailsBlock(string district_Id, string block_id, string panchayat_Id, int damage_id)
        {
            return this.cropRepository.GetBAOApprovedCropDamageDetailsBlock(district_Id, block_id, panchayat_Id, damage_id);
        }

        /// <summary>
        /// GetDAOApprovedCropDamageDetailsDistrict.
        /// </summary>
        /// <param name="district_Id">district_Id.</param>
        /// <param name="block_id">block_id.</param>
        /// <param name="panchayat_Id">panchayat_Id.</param>
        /// <param name="damage_id">damage_id.</param>
        /// <returns>List.</returns>
        public CropDamageAll GetDAOApprovedCropDamageDetailsDistrict(string district_Id, string block_id, string panchayat_Id, int damage_id)
        {
            return this.cropRepository.GetDAOApprovedCropDamageDetailsDistrict(district_Id, block_id, panchayat_Id, damage_id);
        }

        /// <summary>
        /// GetCropDamageReasonDrpdwn.
        /// </summary>
        /// <param name="district_Id">district_Id.</param>
        /// <returns>List.</returns>
        public List<GetCropDamageReasonDrpDwn> GetCropDamageReasonDrpdwn(string district_Id)
        {
            return this.cropRepository.GetCropDamageReasonDrpdwn(district_Id);
        }

        /// <summary>
        /// POSTHorticultureproductivity.
        /// </summary>
        /// <param name="horticultureproductivity">horticultureproductivity.</param>
        /// <returns>List.</returns>
        public int POSTHorticultureproductivity(List<Horticultureproductivity> horticultureproductivity)
        {
            return this.cropRepository.POSTHorticultureproductivity(horticultureproductivity);
        }

        /// <summary>
        /// PostUpdateDamageReasonStatus.
        /// </summary>
        /// <param name="cropDamageGetModel">cropDamageGetModel.</param>
        /// <returns>List.</returns>
        public int PostUpdateDamageReasonStatus(DtoDamageStatusDetails cropDamageGetModel)
        {
            return this.cropRepository.PostUpdateDamageReasonStatus(cropDamageGetModel);
        }

        /// <summary>
        /// insCropCoverageDamageDetails.
        /// </summary>
        /// <paramref name="insCropCoverageDamageDetails"/>insCropCoverageDamageDetails.</param>
        /// <returns>List.</returns>
        public int PostCropCoverageDamageDetails(InsCropCoverageDamageDetails insCropCoverageDamageDetails)
        {
            return this.cropRepository.PostCropCoverageDamageDetails(insCropCoverageDamageDetails);
        }

        /// <summary>
        /// GetDamageReasonNames.
        /// </summary>
        /// <returns>List.</returns>
        public List<CropDamageReasonNames> GetDamageReasonNames()
        {
            return this.cropRepository.GetDamageReasonNames();
        }

        /// <summary>
        /// GetDamageCropList.
        /// </summary>
        /// <returns>List.</returns>
        public List<CropNames> GetDamageCropList()
        {
            return this.cropRepository.GetDamageCropList();
        }

        /// <summary>
        /// PostCropDamageReason.
        /// </summary>
        /// <param name="damageReason">damageReason.</param>
        /// <returns>List.</returns>
        public int PostCropDamageReason(DamageReasonPost damageReason)
        {
            return this.cropRepository.PostCropDamageReason(damageReason);
        }

        /// <summary>
        /// PostCropDamageName.
        /// </summary>
        /// <param name="cropPost">cropPost.</param>
        /// <returns>List.</returns>
        public int PostCropDamageName(CropPost cropPost)
        {
            return this.cropRepository.PostCropDamageName(cropPost);
        }

        /// <summary>
        /// PostDamageDetails.
        /// </summary>
        /// <param name="damageDetails">damageDetails.</param>
        /// <returns>List.</returns>
        public List<DtoPostDamageResponse> PostDamageDetails(DamageDetails damageDetails)
        {
            return this.cropRepository.PostDamageDetails(damageDetails);
        }

        /// <summary>
        /// PostCropCvgDamagePancytApproval.
        /// </summary>
        /// <param name="cropDamageGetAll">cropDamageGetAll.</param>
        /// <returns>List.</returns>
        public CropResponce PostCropCvgDamagePancytApproval(CropDamageGetAll cropDamageGetAll)
        {
            return this.cropRepository.PostCropCvgDamagePancytApproval(cropDamageGetAll);
        }

        /// <summary>
        /// PostBAOCropDamageApproval.
        /// </summary>
        /// <param name="insBAOCropdmgModel">insBAOCropdmgModel.</param>
        /// <returns>List.</returns>
        public CropResponce PostBAOCropDamageApproval(InsBaoCropdmgModel insBAOCropdmgModel)
        {
            return this.cropRepository.PostBAOCropDamageApproval(insBAOCropdmgModel);
        }

        /// <summary>
        /// PostDAOCropDamageApproval.
        /// </summary>
        /// <param name="insBAOCropdmgModel">insBAOCropdmgModel.</param>
        /// <returns>List.</returns>
        public CropResponce PostDAOCropDamageApproval(InsBaoCropdmgModel insBAOCropdmgModel)
        {
            return this.cropRepository.PostBAOCropDamageApproval(insBAOCropdmgModel);
        }

        /// <summary>
        /// GetAllListsOfDamageReasons.
        /// </summary>
        /// <param name="year">year.</param>
        /// <returns>List.</returns>
        public List<CropDamageEntity> GetAllListsOfDamageReasons(int year)
        {
            return this.cropRepository.GetAllListsOfDamageReasons(year);
        }

        /// <summary>
        /// GetSpecificDamageReason.
        /// </summary>
        /// <param name="damageDetails">damageDetails.</param>
        /// <returns>List.</returns>
        public DtoEditDamage GetSpecificDamageReason(DtoEditDamageRequest damageDetails)
        {
            return this.cropRepository.GetSpecificDamageReason(damageDetails);
        }

        /// <summary>
        /// GetDamageConstantCropList.
        /// </summary>
        /// <returns>List.</returns>
        public List<CropNames> GetDamageConstantCropList()
        {
            return this.cropRepository.GetDamageConstantCropList();
        }

        /// <summary>
        /// PostEditDamageDetails.
        /// </summary>
        /// <param name="damageDetails">damageDetails.</param>
        /// <returns>List.</returns>
        public List<DtoPostDamageResponse> PostEditDamageDetails(EditDamageDetails damageDetails)
        {
            return this.cropRepository.PostEditDamageDetails(damageDetails);
        }

        /// <summary>
        /// PostDelDamageReasonList.
        /// </summary>
        /// <param name="damage">damage.</param>
        /// <returns>List.</returns>
        public int PostDelDamageReasonList(DtoEditDamageRequest damage)
        {
            return this.cropRepository.PostDelDamageReasonList(damage);
        }

        /// <summary>
        /// GetOnlineACViewSubmissionPancht.
        /// </summary>
        /// <param name="panchayat_Id">panchayat_Id.</param>
        /// <param name="damage_reason_id">damage_reason_id.</param>
        /// <returns>List.</returns>
        public ViewSubmissionAcPanchayat GetOnlineACViewSubmissionPancht(string panchayat_Id, int damage_reason_id)
        {
            return this.cropRepository.GetOnlineACViewSubmissionPancht(panchayat_Id, damage_reason_id);
        }

        /// <summary>
        /// GetOfflineACViewSubmissionPancht.
        /// </summary>
        /// <param name="panchayat_Id">panchayat_Id.</param>
        /// <returns>List.</returns>
        public List<ViewSubmissionAcPanchayat> GetOfflineACViewSubmissionPancht(string panchayat_Id)
        {
            return this.cropRepository.GetOfflineACViewSubmissionPancht(panchayat_Id);
        }

        /// <summary>
        /// GetCropDamageReportData.
        /// </summary>
        /// <param name="year">year.</param>
        /// <param name="district_ID">district_ID.</param>
        /// <param name="user_id">user_id.</param>
        /// <param name="damageReason_Id">damageReason_Id.</param>
        /// <returns>List.</returns>
        public List<string> GetCropDamageReportData(int year, string district_ID, int user_id, string damageReason_Id)
        {
            return this.cropRepository.GetCropDamageReportData(year, district_ID, user_id, damageReason_Id);
        }

        /// <summary>
        /// GetRainfallReportData.
        /// </summary>
        /// <param name="month_year">month_year.</param>
        /// <param name="district_name">district_name.</param>
        /// <returns>List.</returns>
        public List<string> GetRainfallReportData(string month_year, string district_name)
        {
            return this.cropRepository.GetRainfallReportData(month_year, district_name);
        }

        /// <summary>
        /// GetSeasonByYear.
        /// </summary>
        /// <param name="year">year.</param>
        /// <param name="crop_type">crop_type.</param>
        /// <returns>List.</returns>
        public List<GetSeasonByYearModel> GetSeasonByYear(int year, string crop_type)
        {
            return this.cropRepository.GetSeasonByYear(year, crop_type);
        }

        /// <summary>
        /// GetCropBySeasonID.
        /// </summary>
        /// <param name="season_id">season_id.</param>
        /// <param name="crop_type">crop_type.</param>
        /// <returns>List.</returns>
        public List<GetCropBySeasonIdModel> GetCropBySeasonID(string season_id, string crop_type)
        {
            return this.cropRepository.GetCropBySeasonID(season_id, crop_type);
        }

        /// <summary>
        /// GetCropCvrgTargetDataReport.
        /// </summary>
        /// <param name="cropCvrgTargetDataReportModel">cropCvrgTargetDataReportModel.</param>
        /// <returns>List.</returns>
        public List<string> GetCropCvrgTargetDataReport(GetCropCvrgTargetDataReportModel cropCvrgTargetDataReportModel)
        {
            return this.cropRepository.GetCropCvrgTargetDataReport(cropCvrgTargetDataReportModel);
        }

        /// <summary>
        /// Implemented for Target Vs coverage.
        /// </summary>
        /// <param name="TargetVsCoverage">Collection of input parameters.</param>
        /// <returns>output received in Json format.</returns>
        public List<string> GetCropCvrgTargetVsCoverageDataReport(TargetVsCoverageModel TargetVsCoverage)
        {
            return this.cropRepository.GetCropCvrgTargetVsCoverageDataReport(TargetVsCoverage);
        }

        /// <summary>
        /// GetCropCvrgTargetProductivityDataReport.
        /// </summary>
        /// <param name="cropCvrgTargetDataReportModel">cropCvrgTargetDataReportModel.</param>
        /// <returns>List.</returns>
        public List<string> GetCropCvrgTargetProductivityDataReport(GetCropCvrgTargetDataReportModel cropCvrgTargetDataReportModel)
        {
            return this.cropRepository.GetCropCvrgTargetProductivityDataReport(cropCvrgTargetDataReportModel);
        }

        /// <summary>
        /// GetHortiProduceDataReport.
        /// </summary>
        /// <param name="cropCvrgTargetDataReportModel">cropCvrgTargetDataReportModel.</param>
        /// <returns>List.</returns>
        public List<string> GetHortiProduceDataReport(GetCropCvrgTargetDataReportModel cropCvrgTargetDataReportModel)
        {
            return this.cropRepository.GetHortiProduceDataReport(cropCvrgTargetDataReportModel);
        }

        /// <summary>
        /// GetCoverageSubmittedCropsBySeasonBAO.
        /// </summary>
        /// <param name="districtId">districtId.</param>
        /// <returns>List.</returns>
        public List<CoverageSubmittedCropsSeason> GetCoverageSubmittedCropsBySeasonBAO(int districtId)
        {
            return this.cropRepository.GetCoverageSubmittedCropsBySeasonBAO(districtId);
        }

        /// <summary>
        /// GetTargetSubmittedCropsBySeasonBAO.
        /// </summary>
        /// <param name="districtId">districtId.</param>
        /// <returns>List.</returns>
        public List<CoverageSubmittedCropsSeason> GetTargetSubmittedCropsBySeasonBAO(int districtId)
        {
            return this.cropRepository.GetTargetSubmittedCropsBySeasonBAO(districtId);
        }

        /// <summary>
        /// GetCoverageSubmittedCropsBySeasonDAO.
        /// </summary>
        /// <param name="districtId">districtId.</param>
        /// <returns>List.</returns>
        public List<CoverageSubmittedCropsSeason> GetCoverageSubmittedCropsBySeasonDAO(int districtId)
        {
            return this.cropRepository.GetCoverageSubmittedCropsBySeasonDAO(districtId);
        }

        /// <summary>
        /// GetTargetSubmittedCropsBySeasonDAO.
        /// </summary>
        /// <param name="districtId">districtId.</param>
        /// <returns>List.</returns>
        public List<CoverageSubmittedCropsSeason> GetTargetSubmittedCropsBySeasonDAO(int districtId)
        {
            return this.cropRepository.GetTargetSubmittedCropsBySeasonDAO(districtId);
        }

        /// <summary>
        /// GetCoverageSubmittedCropsBySeason.
        /// </summary>
        /// <param name="districtId">districtId.</param>
        /// <returns>List.</returns>
        public List<CoverageSubmittedCropsSeason> GetCoverageSubmittedCropsBySeason(int districtId)
        {
            return this.cropRepository.GetCoverageSubmittedCropsBySeason(districtId);
        }

        /// <summary>
        /// GetTargetSubmittedCropsBySeason.
        /// </summary>
        /// <param name="districtId">districtId.</param>
        /// <returns>List.</returns>
        public List<CoverageSubmittedCropsSeason> GetTargetSubmittedCropsBySeason(int districtId)
        {
            return this.cropRepository.GetTargetSubmittedCropsBySeason(districtId);
        }

        /// <summary>
        /// InsertSeedUsedInput.
        /// </summary>
        /// <param name="input">input.</param>
        /// <returns>List.</returns>
        public int InsertSeedUsedInput(SeedUsedInput input)
        {
            return this.cropRepository.InsertSeedUsedInput(input);
        }

        /// <summary>
        /// GetSeedusedIputViewSubmission.
        /// </summary>
        /// <param name="seasonId">seasonId.</param>
        /// <param name="cropId">cropId.</param>
        /// <param name="district_id">district_id.</param>
        /// <returns>List.</returns>
        public DtoSeedUserInput GetSeedusedIputViewSubmission(int seasonId, int cropId, int district_id)
        {
            return this.cropRepository.GetSeedusedIputViewSubmission(seasonId, cropId, district_id);
        }

        /// <summary>
        /// GetSeedUsedVarietyname.
        /// </summary>
        /// <param name="seasonId">seasonId.</param>
        /// <param name="cropId">cropId.</param>
        /// <param name="district_id">district_id.</param>
        /// <returns>List.</returns>
        public DtoSeedUserInput GetSeedUsedVarietyname(int seasonId, int cropId, int district_id)
        {
            return this.cropRepository.GetSeedUsedVarietyname(seasonId, cropId, district_id);
        }

        /// <summary>
        /// GetSeedusedinputViewSubmissionOffline.
        /// </summary>
        /// <param name="district_id">district_id.</param>
        /// <returns>List.</returns>
        public List<DtoSeedUserInput> GetSeedusedinputViewSubmissionOffline(int district_id)
        {
            return this.cropRepository.GetSeedusedinputViewSubmissionOffline(district_id);
        }

        /// <summary>
        /// GetSeedPerformanceAgriculture.
        /// </summary>
        /// <param name="districtId">districtId.</param>
        /// <param name="blockId">blockId.</param>
        /// <param name="panchayatId">panchayatId.</param>
        /// <param name="seasonId">seasonId.</param>
        /// <returns>List.</returns>
        public List<DtoSeedPerformanceAgriculture> GetSeedPerformanceAgriculture(string districtId, string blockId, string panchayatId, string seasonId)
        {
            return this.cropRepository.GetSeedPerformanceAgriculture(districtId, blockId, panchayatId, seasonId);
        }

        /// <summary>
        /// GetAllColdStorageByDistrict.
        /// </summary>
        /// <param name="getHortiReportPHMS">getHortiReportPHMS.</param>
        /// <returns>List.</returns>
        public List<GetGrpdwnColdStorageModel> GetAllColdStorageByDistrict(GetHortiReportPhmsModel getHortiReportPHMS)
        {
            return this.cropRepository.GetAllColdStorageByDistrict(getHortiReportPHMS);
        }

        /// <summary>
        /// GetAgricultureBlockkPanchayatByDistrict.
        /// </summary>
        /// <param name="districtId">districtId.</param>
        /// <returns>List.</returns>
        public BlockPanchayatByDistrict GetAgricultureBlockkPanchayatByDistrict(string districtId)
        {
            return this.cropRepository.GetAgricultureBlockkPanchayatByDistrict(districtId);
        }

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
        public List<string> GetSeedPerformanceDataReport(int year, int season, string scheme, string activity, string status, string district, string block, string panchayat, string crop_type)
        {
            return this.cropRepository.GetSeedPerformanceDataReport(year, season, scheme, activity, status, district, block, panchayat, crop_type);
        }

        /// <summary>
        /// To Get Combine Pass Data Report.
        /// </summary>
        /// <param name="combinePassModel">combinePassModel.</param>
        /// <returns>list of string.</returns>
        public List<string> GetCombinePassDataReport(CombinePassInputModel combinePassModel)
        {
            return this.cropRepository.GetCombinePassDataReport(combinePassModel);
        }

        /// <summary>
        /// To Get Agri Asset Data Report.
        /// </summary>
        /// <param name="agriasstmodel">agriasstmodel.</param>
        /// <returns>list of string.</returns>
        public List<string> GetAgriAsstDataReport(AgricultureAssetManagement agriasstmodel)
        {
            return this.cropRepository.GetAgriAsstDataReport(agriasstmodel);
        }

        /// <summary>
        /// GetPiasDataReport.
        /// </summary>
        /// <param name="piasDataReportInputModel">piasDataReportInputModel.</param>
        /// <returns>list of string.</returns>
        public List<string> GetPiasDataReport(GetPiasDataReportInputModel piasDataReportInputModel)
        {
            return this.cropRepository.GetPiasDataReport(piasDataReportInputModel);
        }

        /// <summary>
        /// PostSeedDemandAc.
        /// </summary>
        /// <param name="input">input.</param>
        /// <returns>List.</returns>
        public int PostSeedDemandAc(SeedIndentInput input)
        {
            return this.cropRepository.PostSeedDemandAc(input);
        }

        /// <summary>
        /// GetSeedDemandACViewSubmission.
        /// </summary>
        /// <param name="seasonId">seasonId.</param>
        /// <param name="cropId">cropId.</param>
        /// <param name="districtId">districtId.</param>
        /// <param name="panchayatId">panchayatId.</param>
        /// <returns>List.</returns>
        public DtoSeedIndentInput GetSeedDemandACViewSubmission(int seasonId, int cropId, int districtId, int panchayatId)
        {
            return this.cropRepository.GetSeedDemandACViewSubmission(seasonId, cropId, districtId, panchayatId);
        }

        /// <summary>
        /// GetSeedDemandAcViewSubmissionOffline.
        /// </summary>
        /// <param name="districtId">districtId.</param>
        /// <param name="panchayatId">panchayatId.</param>
        /// <returns>List.</returns>
        public List<DtoSeedIndentInput> GetSeedDemandAcViewSubmissionOffline(int districtId, int panchayatId)
        {
            return this.cropRepository.GetSeedDemandAcViewSubmissionOffline(districtId, panchayatId);
        }

        /// <summary>
        /// GetSeedUsedVarietynameAC.
        /// </summary>
        /// <param name="seasonId">seasonId.</param>
        /// <param name="cropId">cropId.</param>
        /// <param name="districtId">districtId.</param>
        /// <param name="panchayatId">panchayatId.</param>
        /// <returns>List.</returns>
        public DtoSeedIndentInput GetSeedUsedVarietynameAC(int seasonId, int cropId, int districtId, int panchayatId)
        {
            return this.cropRepository.GetSeedUsedVarietynameAC(seasonId, cropId, districtId, panchayatId);
        }

        /// <summary>
        /// GetAllMarketByDistrict.
        /// </summary>
        /// <param name="districtId">districtId.</param>
        /// <returns>MarketData list.</returns>
        public List<MarketData> GetAllMarketByDistrict(string districtId)
        {
            return this.cropRepository.GetAllMarketByDistrict(districtId);
        }

        /// <summary>
        /// PostPlantIndent.
        /// </summary>
        /// <param name="input">input.</param>
        /// <returns>Response.</returns>
        public int PostPlantIndent(PlantIndentInput input)
        {
            return this.cropRepository.PostPlantIndent(input);
        }

        /// <summary>
        /// GetSeedDemandBHOViewSubmission.
        /// </summary>
        /// <param name="seasonId">seasonId.</param>
        /// <param name="cropId">cropId.</param>
        /// <param name="blockId">blockId.</param>
        /// <param name="panchayatId">panchayatId.</param>
        /// <returns>DtoPlantIndentInput.</returns>
        public DtoPlantIndentInput GetSeedDemandBHOViewSubmission(int seasonId, int cropId, string blockId, int panchayatId)
        {
            return this.cropRepository.GetSeedDemandBHOViewSubmission(seasonId, cropId, blockId, panchayatId);
        }

        /// <summary>
        /// GetSeedDemandBHOViewSubmissionOffline.
        /// </summary>
        /// <param name="districtId">districtId.</param>
        /// <param name="seasonId">seasonId.</param>
        /// <param name="blockId">blockId.</param>
        /// <returns>DtoPlantIndentInput List.</returns>
        public List<DtoPlantIndentInput> GetSeedDemandBHOViewSubmissionOffline(int districtId, int seasonId, string blockId)
        {
            return this.cropRepository.GetSeedDemandBHOViewSubmissionOffline(districtId, seasonId, blockId);
        }

        /// <summary>
        /// GetSeedUsedVarietyNameBHO.
        /// </summary>
        /// <param name="seasonId">seasonId.</param>
        /// <param name="cropId">cropId.</param>
        /// <param name="districtId">districtId.</param>
        /// <param name="blockId">blockId.</param>
        /// <param name="panchayatId">panchayatId.</param>
        /// <returns>Values.</returns>
        public DtoPlantIndentInput GetSeedUsedVarietyNameBHO(int seasonId, int cropId, int districtId, string blockId, int panchayatId)
        {
            return this.cropRepository.GetSeedUsedVarietyNameBHO(seasonId, cropId, districtId, blockId, panchayatId);
        }

        /// <summary>
        /// GetFutureSeason.
        /// </summary>
        /// <returns>FutureSeason List.</returns>
        public List<FutureSeason> GetFutureSeason()
        {
            return this.cropRepository.GetFutureSeason();
        }

        /// <summary>
        /// GetCropName.
        /// </summary>
        /// <param name="seasonId">seasonId.</param>
        /// <returns>Crop Name List.</returns>
        public List<CropNames> GetCropName(int seasonId)
        {
            return this.cropRepository.GetCropName(seasonId);
        }

        /// <summary>
        /// GetPlantName.
        /// </summary>
        /// <param name="plantCategory">plantCategory.</param>
        /// <param name="seasonId">seasonId.</param>
        /// <returns>Plant Name List.</returns>
        public List<PlantNames> GetPlantName(string plantCategory, int seasonId)
        {
            return this.cropRepository.GetPlantName(plantCategory, seasonId);
        }

        /// <summary>
        /// GetCropSeedVariety.
        /// </summary>
        /// <param name="cropId">cropId.</param>
        /// <param name="seasonId">seasonId.</param>
        /// <returns>Seed Variety List.</returns>
        public List<SeedVarietyList> GetCropSeedVariety(int cropId, int seasonId)
        {
            return this.cropRepository.GetCropSeedVariety(cropId, seasonId);
        }

        /// <summary>
        /// GetPlantSeedVariety.
        /// </summary>
        /// <param name="cropId">cropId.</param>
        /// <param name="seasonId">seasonId.</param>
        /// <returns>Seed Variety List.</returns>
        public List<SeedVarietyList> GetPlantSeedVariety(int cropId, int seasonId)
        {
            return this.cropRepository.GetPlantSeedVariety(cropId, seasonId);
        }

        /// <summary>
        /// GetCropCategoryBySeason.
        /// </summary>
        /// <param name="seasonId">seasonId.</param>
        /// <returns>Crop Category List.</returns>
        public List<CropCategoryEntity> GetCropCategoryBySeason(int seasonId)
        {
            return this.cropRepository.GetCropCategoryBySeason(seasonId);
        }

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
        public List<string> GetInputIndentDataReport(int year, string seasonId, string activity, string cropVarietyId, string plantCategory, string cropId, string district, string block, string panchayat)
        {
            return this.cropRepository.GetInputIndentDataReport(year, seasonId, activity, cropVarietyId, plantCategory, cropId, district, block, panchayat);
        }
    }
}
