//------------------------------------------------------------------------------
// <copyright file="HorticultureController.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------
namespace ADIDAS.API.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using ADIDAS.Model.DTO;
    using ADIDAS.Model.Entities;
    using ADIDAS.Service.Interfaces;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// HorticultureController.
    /// </summary>
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class HorticultureController : Controller
    {
        private readonly IHorticultureService horticultureService;
        private readonly ILogger<HorticultureController> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="HorticultureController"/> class.
        /// HorticultureController.
        /// </summary>
        /// <param name="horticultureService">horticultureService.</param>
        /// <param name="logger">logger.</param>
        public HorticultureController(IHorticultureService horticultureService, ILogger<HorticultureController> logger)
        {
            this.horticultureService = horticultureService;
            this.logger = logger;
        }

        /// <summary>
        /// GetHortDamagePancht.
        /// </summary>
        /// <param name="seasonId">seasonId.</param>
        /// <param name="panchayatId">panchayatId.</param>
        /// <returns>CropDamagePancht.</returns>
        [HttpGet("GetHortDamagePancht/{seasonId}/{panchayatId}")]
        public IActionResult GetHortDamagePancht(string seasonId, string panchayatId)
        {
            try
            {
                List<CropDamagePancht> result = this.horticultureService.GetHortDamagePancht(seasonId, panchayatId);
                if (!result.Any())
                {
                    return this.NotFound(this.Json("Not Found"));
                }

                return this.Ok(result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound(this.Json("Not Found"));
            }
        }

        /// <summary>
        /// GetHortDamageBlock.
        /// </summary>
        /// <param name="blockId">blockId.</param>
        /// <returns>GetHortDamageBlock CropDamageBlock.</returns>
        [HttpGet("GetHortDamageBlock/{blockId}")]
        public IActionResult GetHortDamageBlock(string blockId)
        {
            try
            {
                List<CropDamageBlock> result = this.horticultureService.GetHortDamageBlock(blockId);
                if (!result.Any())
                {
                    return this.NotFound(this.Json("Not Found"));
                }

                return this.Ok(result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound(this.Json("Not Found"));
            }
        }

        /// <summary>
        /// GetAllColdStorages.
        /// </summary>
        /// <param name="district_Id">district_Id.</param>
        /// <returns>GetAllColdStorages GetAllColdStorage.</returns>
        [HttpGet("GetAllColdStorages/{district_Id}")]
        public IActionResult GetAllColdStorages(int district_Id)
        {
            try
            {
                List<GetAllColdStorage> result = this.horticultureService.GetAllColdStorages(district_Id);
                if (result == null)
                {
                    return this.NotFound(this.Json("Not Found"));
                }

                return this.Ok(result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound(this.Json("Not Found"));
            }
        }

        /// <summary>
        /// GetAllColdStoragesOffline.
        /// </summary>
        /// <returns>GetAllColdStoragesOffline GetAllColdStorage.</returns>
        [HttpGet("GetAllColdStoragesOffline")]
        public IActionResult GetAllColdStoragesOffline()
        {
            try
            {
                List<GetAllColdStorage> result = this.horticultureService.GetAllColdStoragesOffline();
                if (result == null)
                {
                    return this.NotFound(this.Json("Not Found"));
                }

                return this.Ok(result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound(this.Json("Not Found"));
            }
        }

        /// <summary>
        /// GetAllCropsColdStorage.
        /// </summary>
        /// <param name="district_Id">district_Id.</param>
        /// <returns>GetAllCropsColdStorage GetAllCropColdStorage.</returns>
        [HttpGet("GetAllCropsColdStorage/{district_Id}")]
        public IActionResult GetAllCropsColdStorage(int district_Id)
        {
            try
            {
                List<GetAllCropColdStorage> result = this.horticultureService.GetAllCropsColdStorage(district_Id);
                if (result == null)
                {
                    return this.NotFound(this.Json("Not Found"));
                }

                return this.Ok(result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound(this.Json("Not Found"));
            }
        }

        /// <summary>
        /// GetColdStoragesByStrgID.
        /// </summary>
        /// <param name="crop_id">crop_id.</param>
        /// <param name="district_Id">district_Id.</param>
        /// <param name="strg_ID">strg_ID.</param>
        /// <returns>GetColdStoragesByStrgID GetAllCropColdStorageId.</returns>
        [HttpGet("GetColdStoragesByStrgID/{crop_id}/{district_Id}/{strg_ID}")]
        public IActionResult GetColdStoragesByStrgID(int crop_id, int? district_Id, int strg_ID)
        {
            try
            {
                List<GetAllCropColdStorageId> result = this.horticultureService.GetColdStoragesByStrgID(crop_id, district_Id, strg_ID);
                return this.Ok(result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.Problem(ex.Message);
            }
        }

        /// <summary>
        /// GetColdStoragesByStrgIDOffline.
        /// </summary>
        /// <returns>GetAllCropColdStorageId.</returns>
        [HttpGet("GetColdStoragesByStrgIDOffline")]
        public IActionResult GetColdStoragesByStrgIDOffline()
        {
            try
            {
                List<GetAllCropColdStorageId> result = this.horticultureService.GetColdStoragesByStrgIDOffline();
                return this.Ok(result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.Problem(ex.Message);
            }
        }

        /// <summary>
        /// GetLstEgtWksColdStorages.
        /// </summary>
        /// <param name="district_Id">district_Id.</param>
        /// <returns>LstCpldStorageDets.</returns>
        [HttpGet("GetLstEgtWksColdStorages/{district_Id}")]
        public IActionResult GetLstEgtWksColdStorages(int district_Id)
        {
            try
            {
                List<LstCpldStorageDets> result = this.horticultureService.GetLstEgtWksColdStorages(district_Id);
                if (result == null)
                {
                    return this.NotFound(this.Json("Not Found"));
                }

                return this.Ok(result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound(this.Json("Not Found"));
            }
        }

        /// <summary>
        /// GetFacilitiesOnline.
        /// </summary>
        /// <param name="districtId">districtId.</param>
        /// <param name="panchayatId">panchayatId.</param>
        /// <param name="structureId">structureId.</param>
        /// <returns>FacilityOnline.</returns>
        [HttpGet("GetFacilitiesOnline/{districtId}/{panchayatId}/{structureId}")]
        public IActionResult GetFacilitiesOnline(int districtId, int panchayatId, int structureId)
        {
            try
            {
                List<FacilityOnline> result = this.horticultureService.GetFacilitiesOnline(districtId, panchayatId, structureId);
                if (!result.Any())
                {
                    return this.NotFound(this.Json("Not Found"));
                }

                return this.Ok(result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound(this.Json("Not Found"));
            }
        }

        /// <summary>
        /// GetFacilitiesOffline.
        /// </summary>
        /// <param name="districtId">districtId.</param>
        /// <param name="panchayatId">panchayatId.</param>
        /// <returns>FacilityOnline.</returns>
        [HttpGet("GetFacilitiesOffline/{districtId}/{panchayatId}")]
        public IActionResult GetFacilitiesOffline(int districtId, int panchayatId)
        {
            try
            {
                List<FacilityOnline> result = this.horticultureService.GetFacilitiesOffline(districtId, panchayatId);
                if (!result.Any())
                {
                    return this.NotFound(this.Json("Not Found"));
                }

                return this.Ok(result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound(this.Json("Not Found"));
            }
        }

        /// <summary>
        /// GetAllStructure.
        /// </summary>
        /// <param name="district_Id">district_Id.</param>
        /// <returns>GetStructure.</returns>
        [HttpGet("GetAllStructure/{district_Id}")]
        public IActionResult GetAllStructure(int district_Id)
        {
            try
            {
                List<GetStructure> result = this.horticultureService.GetAllStructure(district_Id);

                if (!result.Any())
                {
                    return this.NotFound();
                }

                return this.Ok(result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound();
            }
        }

        /// <summary>
        /// GetDistinctHorticultureCrop.
        /// </summary>
        /// <param name="district_id">district_id.</param>
        /// <returns>DistinctHorticultureCrop.</returns>
        [HttpGet("GetDistinctHorticultureCrop/{District_id}")]
        public IActionResult GetDistinctHorticultureCrop(int district_id)
        {
            try
            {
                List<DistinctHorticultureCrop> result = this.horticultureService.GetDistinctHorticultureCrop(district_id);
                if (result == null)
                {
                    return this.NotFound();
                }

                return this.Ok(result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound();
            }
        }

        /// <summary>
        /// GetHortProduceSeason.
        /// </summary>
        /// <returns>HortProduceSeasonResponse.</returns>
        [HttpGet("GetHortProduceSeason")]
        public IActionResult GetHortProduceSeason()
        {
            try
            {
                List<HortProduceSeasonResponse> result = this.horticultureService.GetHortProduceSeason();
                if (result == null)
                {
                    return this.NotFound();
                }

                return this.Ok(result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound();
            }
        }

        /// <summary>
        /// GetHortProducePanchayat.
        /// </summary>
        /// <param name="district_id">district_id.</param>
        /// <param name="block_id">block_id.</param>
        /// <returns>HortProducePanchayatResponse.</returns>
        [HttpGet("GetHortProducePanchayat/{district_id}/{block_id}")]
        public IActionResult GetHortProducePanchayat(int district_id, int block_id)
        {
            try
            {
                List<HortProducePanchayatResponse> result = this.horticultureService.GetHortProducePanchayat(district_id, block_id);
                if (result == null)
                {
                    return this.NotFound();
                }

                return this.Ok(result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound();
            }
        }

        /// <summary>
        /// GetHortProduceLatest.
        /// </summary>
        /// <param name="crop_id">crop_id.</param>
        /// <param name="district_id">district_id.</param>
        /// <param name="block_id">block_id.</param>
        /// <param name="panchayat_Id">panchayat_Id.</param>
        /// <returns>GetHortProduceLatest.</returns>
        [HttpGet("GetHortProduceLatest/{crop_id}/{district_id}/{block_id}/{panchayat_Id}")]
        public IActionResult GetHortProduceLatest(int crop_id, int district_id, int block_id, int panchayat_Id)
        {
            try
            {
                List<HortProduceTran> result = this.horticultureService.GetHortProduceLatest(crop_id, district_id, block_id, panchayat_Id);
                if (result == null)
                {
                    return this.NotFound();
                }

                return this.Ok(result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound();
            }
        }

        /// <summary>
        /// GetHortProduceActualPancht.
        /// </summary>
        /// <param name="season_id">season_id.</param>
        /// <param name="panchayat_Id">panchayat_Id.</param>
        /// <returns>HortProduceActualPanchayat.</returns>
        [HttpGet("GetHortProduceActualPancht/{Season_id}/{Panchayat_Id}")]
        public IActionResult GetHortProduceActualPancht(int season_id, int panchayat_Id)
        {
            try
            {
                List<HortProduceActualPanchayat> result = this.horticultureService.GetHortProduceActualPancht(season_id, panchayat_Id);
                if (result == null)
                {
                    return this.NotFound();
                }

                return this.Ok(result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound();
            }
        }

        /// <summary>
        /// GetHortProduceActualPanchtCurrDate.
        /// </summary>
        /// <param name="season_id">season_id.</param>
        /// <param name="panchayat_Id">panchayat_Id.</param>
        /// <returns>HortProduceActualPanchayat.</returns>
        [HttpGet("GetHortProduceActualPanchtCurrDate/{Season_id}/{Panchayat_Id}")]
        public IActionResult GetHortProduceActualPanchtCurrDate(int season_id, int panchayat_Id)
        {
            try
            {
                List<HortProduceActualPanchayat> result = this.horticultureService.GetHortProduceActualPanchtCurrDate(season_id, panchayat_Id);
                if (result == null)
                {
                    return this.NotFound();
                }

                return this.Ok(result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound();
            }
        }

        /// <summary>
        /// GetHortProduceActualBlock.
        /// </summary>
        /// <param name="block_Id">block_Id.</param>
        /// <returns>HortProduceActualBlock.</returns>
        [HttpGet("GetHortProduceActualBlock/{Block_Id}")]
        public IActionResult GetHortProduceActualBlock(int block_Id)
        {
            try
            {
                List<HortProduceActualBlock> result = this.horticultureService.GetHortProduceActualBlock(block_Id);
                if (result == null)
                {
                    return this.NotFound();
                }

                return this.Ok(result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound();
            }
        }

        /// <summary>
        /// GetHortProduceActualDistrict.
        /// </summary>
        /// <param name="district_Id">district_Id.</param>
        /// <returns>HortProduceActualDistrict.</returns>
        [HttpGet("GetHortProduceActualDistrict/{District_Id}")]
        public IActionResult GetHortProduceActualDistrict(int district_Id)
        {
            try
            {
                List<HortProduceActualDistrict> result = this.horticultureService.GetHortProduceActualDistrict(district_Id);
                if (result == null)
                {
                    return this.NotFound();
                }

                return this.Ok(result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound();
            }
        }

        /// <summary>
        /// GetHortiProduceActualBHOOffline.
        /// </summary>
        /// <param name="district_Id">district_Id.</param>
        /// <param name="block_id">block_id.</param>
        /// <param name="season_id">season_id.</param>
        /// <param name="crop_ids">crop_ids.</param>
        /// <returns>HortProduceCoverageActual.</returns>
        [HttpGet("GetHortiProduceActualBHOOffline/{District_Id}/{Block_id}/{Season_id}/{Crop_ids}")]
        public IActionResult GetHortiProduceActualBHOOffline(int district_Id, int block_id, int season_id, string crop_ids)
        {
            try
            {
                List<HortProduceCoverageActual> result = this.horticultureService.GetHortiProduceCoverageActualBHOOffline(district_Id, block_id, season_id, crop_ids);
                if (result == null)
                {
                    return this.NotFound();
                }

                return this.Ok(result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound();
            }
        }

        /// <summary>
        /// GetHortProduceBHO.
        /// </summary>
        /// <param name="district_Id">district_Id.</param>
        /// <param name="block_id">block_id.</param>
        /// <param name="season_id">season_id.</param>
        /// <param name="panchyat_id">panchyat_id.</param>
        /// <returns>GetHortProduceBholstModel.</returns>
        [HttpGet("GetHortProduceBHO/{District_Id}/{Block_id}/{Season_id}/{Panchyat_id}")]
        public IActionResult GetHortProduceBHO(int district_Id, int block_id, int season_id, string panchyat_id)
        {
            try
            {
                List<GetHortProduceBholstModel> result = this.horticultureService.GetHortProduceBHO(district_Id, block_id, season_id, panchyat_id);
                if (result == null)
                {
                    return this.NotFound();
                }

                return this.Ok(result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound();
            }
        }

        /// <summary>
        /// GetHortProduceActualADHOffline.
        /// </summary>
        /// <param name="district_Id">district_Id.</param>
        /// <param name="season_id">season_id.</param>
        /// <param name="crop_ids">crop_ids.</param>
        /// <returns>HortProduceCoverageActual.</returns>
        [HttpGet("GetHortProduceActualADHOffline/{District_Id}/{Season_id}/{Crop_ids}")]
        public IActionResult GetHortProduceActualADHOffline(int district_Id, int season_id, string crop_ids)
        {
            try
            {
                List<HortProduceCoverageActual> result = this.horticultureService.GetHortProduceActualADHOffline(district_Id, season_id, crop_ids);
                if (result == null)
                {
                    return this.NotFound();
                }

                return this.Ok(result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound();
            }
        }

        /// <summary>
        /// GetHortProduceActualADH.
        /// </summary>
        /// <param name="district_Id">district_Id.</param>
        /// <param name="season_id">season_id.</param>
        /// <param name="crop_ids">crop_ids.</param>
        /// <returns>HortProduceCoverageActl.</returns>
        [HttpGet("GetHortProduceActualADH/{District_Id}/{Season_id}/{Crop_ids}")]
        public IActionResult GetHortProduceActualADH(int district_Id, int season_id, string crop_ids)
        {
            try
            {
                HortProduceCoverageActl result = this.horticultureService.GetHortProduceActualADH(district_Id, season_id, crop_ids);
                if (result == null)
                {
                    return this.NotFound();
                }

                return this.Ok(result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound();
            }
        }

        /// <summary>
        /// GetHortProduceActualBHO.
        /// </summary>
        /// <param name="district_Id">district_Id.</param>
        /// <param name="block_id">block_id.</param>
        /// <param name="season_id">season_id.</param>
        /// <param name="crop_ids">crop_ids.</param>
        /// <returns>HortProduceCoverageActl.</returns>
        [HttpGet("GetHortProduceActualBHO/{District_Id}/{Block_id}/{Season_id}/{Crop_ids}")]
        public IActionResult GetHortProduceActualBHO(int district_Id, int block_id, int season_id, string crop_ids)
        {
            try
            {
                HortProduceCoverageActl result = this.horticultureService.GetHortProduceActualBHO(district_Id, block_id, season_id, crop_ids);
                if (result == null)
                {
                    return this.NotFound();
                }

                return this.Ok(result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound();
            }
        }

        /// <summary>
        /// GetSubmitProductivity.
        /// </summary>
        /// <param name="district_Id">district_Id.</param>
        /// <param name="season_id">season_id.</param>
        /// <returns>GetSubmitProductivityModel.</returns>
        [HttpGet("GetSubmitProductivity/{District_Id}/{Season_id}")]
        public IActionResult GetSubmitProductivity(int district_Id, int season_id)
        {
            try
            {
                List<GetSubmitProductivityModel> result = this.horticultureService.GetSubmitProductivity(district_Id, season_id);
                if (result == null)
                {
                    return this.NotFound();
                }

                return this.Ok(result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound();
            }
        }

        /// <summary>
        /// GetAllSeedPerformanceHorticulture.
        /// </summary>
        /// <param name="district_id">district_id.</param>
        /// <param name="block_id">block_id.</param>
        /// <param name="panchayat_id">panchayat_id.</param>
        /// <param name="season_id">season_id.</param>
        /// <returns>SeedPerfHortdbt.</returns>
        [HttpGet("GetAllSeedPerformanceHorticulture/{district_id}/{block_id}/{panchayat_id}/{season_id}")]
        public IActionResult GetAllSeedPerformanceHorticulture(string district_id, string block_id, string panchayat_id, string season_id)
        {
            try
            {
                List<SeedPerfHortdbt> result;

                result = this.horticultureService.GetAllSeedPerformanceHorticultureDBT(Convert.ToInt32(district_id), Convert.ToInt32(block_id), Convert.ToInt32(panchayat_id), Convert.ToInt32(season_id));
                if (!result.Any())
                {
                    return this.NotFound();
                }

                return this.Ok(result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound();
            }
        }

        /// <summary>
        /// GetBlockPanchayatData.
        /// </summary>
        /// <param name="district_id">district_id.</param>
        /// <returns>DbtDistrictWrapper.</returns>
        [HttpGet("GetBlockPanchayatData/{district_id}")]
        public IActionResult GetBlockPanchayatData(string district_id)
        {
            try
            {
                DbtDistrictWrapper result = this.horticultureService.GetBlockPanchayatData(Convert.ToInt32(district_id));
                if (result == null)
                {
                    return this.NotFound();
                }

                return this.Ok(result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound();
            }
        }

        /// <summary>
        /// GetVarietiesByType.
        /// </summary>
        /// <param name="type">type.</param>
        /// <param name="seasonid">seasonid.</param>
        /// <returns>Varities.</returns>
        [HttpGet("GetVarietiesByType/{type}/{seasonid}")]
        public IActionResult GetVarietiesByType(string type, int seasonid)
        {
            try
            {
                List<Varities> result = this.horticultureService.GetVarietiesByType(type, seasonid);
                if (result == null)
                {
                    return this.NotFound();
                }

                return this.Ok(result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound();
            }
        }

        /// <summary>
        /// GetPHMSNoFacilitiesData.
        /// </summary>
        /// <param name="districtId">districtId.</param>
        /// <returns>NoFacility.</returns>
        [HttpGet("GetPHMSNoFacilitiesData/{districtId}")]
        public IActionResult GetPHMSNoFacilitiesData(int districtId)
        {
            try
            {
                List<NoFacility> result = this.horticultureService.GetPHMSNoFacilitiesData(districtId);
                if (result == null)
                {
                    return this.NotFound();
                }

                return this.Ok(result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound();
            }
        }

        /// <summary>
        /// GetHortiReportPHMS.
        /// </summary>
        /// <param name="getHortiReportPHMS">getHortiReportPHMS.</param>
        /// <returns>IActionResult.</returns>
        [HttpPost("GetHortiReportPHMS")]
        public IActionResult GetHortiReportPHMS([FromBody] GetHortiReportPhmsModel getHortiReportPHMS)
        {
            try
            {
                List<string> result = this.horticultureService.GetHortiReportPHMS(getHortiReportPHMS);
                if (result == null || !result.Any())
                {
                    return this.NotFound();
                }

                return this.Ok(result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound();
            }
        }

        /// <summary>
        /// GetHortiReportColdStorage.
        /// </summary>
        /// <param name="getHortiReportPHMS">getHortiReportPHMS.</param>
        /// <returns>IActionResult.</returns>
        [HttpPost("GetHortiReportColdStorage")]
        public IActionResult GetHortiReportColdStorage([FromBody] GetHortiReportPhmsModel getHortiReportPHMS)
        {
            try
            {
                List<string> result = this.horticultureService.GetHortiReportColdStorage(getHortiReportPHMS);
                if (result == null || !result.Any())
                {
                    return this.NotFound();
                }

                return this.Ok(result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound();
            }
        }

        /// <summary>
        /// PostSubmitProductivity.
        /// </summary>
        /// <param name="hortProduce">hortProduce.</param>
        /// <returns>IActionResult.</returns>
        [HttpPost("PostSubmitProductivity")]
        public IActionResult PostSubmitProductivity([FromBody] List<PostSubmitProductivityModel> hortProduce)
        {
            try
            {
                int result = this.horticultureService.PostAgriProductivity(hortProduce);
                if (result == 1)
                {
                    return this.Ok(new { data = "Updated Successfully" });
                }
                else
                {
                    return this.NotFound();
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound();
            }
        }

        /// <summary>
        /// PostColdStorage.
        /// </summary>
        /// <param name="coldStorage">coldStorage.</param>
        /// <returns>IActionResult.</returns>
        [HttpPost("PostColdStorage")]
        public IActionResult PostColdStorage([FromBody] ColdStorage coldStorage)
        {
            try
            {
                int result = this.horticultureService.InsertColdStorage(coldStorage);
                if (result == 1)
                {
                    return this.Ok(new { data = "Inserted Successfully" });
                }
                else
                {
                    return this.NotFound();
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound();
            }
        }

        /// <summary>
        /// PostHortDamageBlockApproval.
        /// </summary>
        /// <param name="cropDamage">cropDamage.</param>
        /// <returns>CropDamageBlockApprovalResponse.</returns>
        [HttpPost("PostHortDamageBlockApproval")]
        public IActionResult PostHortDamageBlockApproval([FromBody] CropDamage cropDamage)
        {
            try
            {
                List<CropDamageBlockApprovalResponse> result = this.horticultureService.InsertHortDamageApproval(cropDamage);
                if (!result.Any())
                {
                    return this.NotFound("{\"status\": \"Insertion Failed\"}");
                }
                else
                {
                    return this.Ok(result);
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound("{\"status\": \"Insertion Failed\"}");
            }
        }

        /// <summary>
        /// PostHortDamagePnchytApproval.
        /// </summary>
        /// <param name="cropDamage">cropDamage.</param>
        /// <returns>CropDamageBlockApprovalResponse.</returns>
        [HttpPost("PostHortDamagePnchytApproval")]
        public IActionResult PostHortDamagePnchytApproval([FromBody] CropDamage cropDamage)
        {
            try
            {
                List<CropDamageBlockApprovalResponse> result = this.horticultureService.InsertHortDamageApproval(cropDamage);
                if (!result.Any())
                {
                    return this.NotFound("{\"status\": \"Insertion Failed\"}");
                }
                else
                {
                    return this.Ok(result);
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound("{\"status\": \"Insertion Failed\"}");
            }
        }

        /// <summary>
        /// PostHortDamagePanchayat.
        /// </summary>
        /// <param name="crop">crop.</param>
        /// <returns>CropDamagePanchayatResponse.</returns>
        [HttpPost("PostHortDamagePanchayat")]
        public IActionResult PostHortDamagePanchayat([FromBody] List<CropDamagePancht> crop)
        {
            try
            {
                List<CropDamagePanchayatResponse> result = this.horticultureService.InsertHortDamagePancht(crop);

                if (!result.Any())
                {
                    return this.NotFound("{\"status\": \"Insertion Failed\"}");
                }
                else
                {
                    return this.Ok(result);
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound("{\"status\": \"Insertion Failed\"}");
            }
        }

        /// <summary>
        /// PostColdStorageDetails.
        /// </summary>
        /// <param name="coldStorageDetails">coldStorageDetails.</param>
        /// <returns>IActionResult.</returns>
        [HttpPost("PostColdStorageDetails")]
        public IActionResult PostColdStorageDetails([FromBody] ColdStorageDetails coldStorageDetails)
        {
            try
            {
                int result = this.horticultureService.InsertColdStorageDetails(coldStorageDetails);
                if (result == 1)
                {
                    return this.Ok(new { data = "Inserted Successfully" });
                }
                else if (result == -2)
                {
                    return this.NotFound(new { data = "Storage Name Already Exists" });
                }
                else
                {
                    return this.NotFound();
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound();
            }
        }

        /// <summary>
        /// PostPHMSFacility.
        /// </summary>
        /// <param name="phmsDetails">phmsDetails.</param>
        /// <returns>IActionResult.</returns>
        [HttpPost("PostPHMSFacility")]
        public IActionResult PostPHMSFacility([FromBody] PhmsDetails phmsDetails)
        {
            try
            {
                int result = this.horticultureService.InsertPHMSFacility(phmsDetails);
                if (result == 1)
                {
                    return this.Ok(new { status = "Success", Message = "Record updated successfully" });
                }
                else
                {
                    return this.Ok(new { status = "Failure", Message = "Record Not updated" });
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.Ok(new { status = "Failure", Message = "Record Not updated" });
            }
        }

        /// <summary>
        /// PosttHortCropCvgtgtPanchayat.
        /// </summary>
        /// <param name="crop">crop.</param>
        /// <returns>HortCropCoverageTargetPanchytApprovalResponse.</returns>
        [HttpPost("PosttHortCropCvgtgtPanchayat")]
        public IActionResult PosttHortCropCvgtgtPanchayat([FromBody] List<HortCropCoverageAimPancht> crop)
        {
            try
            {
                List<HortCropCoverageTargetPanchytApprovalResponse> result = this.horticultureService.InsertHortCropCoveragetargetPanchayat(crop);
                if (!result.Any())
                {
                    return this.NotFound("{\"status\": \"Insertion Failed\"}");
                }

                return this.Ok(result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound("{\"status\": \"Insertion Failed\"}");
            }
        }

        /// <summary>
        /// PostHortAggCropCvgActual.
        /// </summary>
        /// <param name="hortAggCropCoverageActual">hortAggCropCoverageActual.</param>
        /// <returns>IActionResult.</returns>
        [HttpPost("PostHortAggCropCvgActual")]
        public IActionResult PostHortAggCropCvgActual([FromBody] HortAggCropCoverageActual hortAggCropCoverageActual)
        {
            try
            {
                int result = this.horticultureService.InsertHortAggCropCoverageActual(hortAggCropCoverageActual);
                if (result == 1)
                {
                    return this.Ok(new { data = "Inserted Successfully" });
                }
                else
                {
                    return this.NotFound();
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound();
            }
        }

        /// <summary>
        /// PostHortAutoApprovalCvgActlBlk.
        /// </summary>
        /// <returns>IActionResult.</returns>
        [HttpPost("PostHortAutoApprovalCvgActlBlk")]
        public IActionResult PostHortAutoApprovalCvgActlBlk()
        {
            try
            {
                int result = this.horticultureService.HortAutoApprovalCoverageActlBlock();
                if (result == 1)
                {
                    return this.Ok(new { data = "Updated Successfully" });
                }
                else
                {
                    return this.NotFound();
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound();
            }
        }

        /// <summary>
        /// PostPHMSStructure.
        /// </summary>
        /// <param name="pHmsStructure">pHMSStructure.</param>
        /// <returns>IActionResult.</returns>
        [HttpPost("PostPHMSStructure")]
        public IActionResult PostPHMSStructure([FromBody] PhmsStructure pHmsStructure)
        {
            try
            {
                int result = this.horticultureService.InsertPHMSStructure(pHmsStructure);
                if (result == 1)
                {
                    return this.Ok(new { data = "Inserted Successfully" });
                }
                else
                {
                    return this.NotFound();
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound();
            }
        }

        /// <summary>
        /// PostHortNewCrop.
        /// </summary>
        /// <param name="hortNewCrop">hortNewCrop.</param>
        /// <returns>IActionResult.</returns>
        [HttpPost("PostHortNewCrop")]
        public IActionResult PostHortNewCrop([FromBody] HortNewCrop hortNewCrop)
        {
            try
            {
                int result = this.horticultureService.InsertHortNewCrop(hortNewCrop);
                if (result == 1)
                {
                    return this.Ok(new { data = "Inserted Successfully" });
                }
                else
                {
                    return this.NotFound();
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound();
            }
        }

        /// <summary>
        /// PostHortProduceTranApproval.
        /// </summary>
        /// <param name="hortProduceTranApprovals">hortProduceTranApprovals.</param>
        /// <returns>CropCoverageTargetPanchytApprovalResponse.</returns>
        [HttpPost("PostHortProduceTranApproval")]
        public IActionResult PostHortProduceTranApproval([FromBody] List<HortProduceTranApproval> hortProduceTranApprovals)
        {
            try
            {
                List<CropCoverageTargetPanchytApprovalResponse> result = this.horticultureService.InsertHortProduceTranApproval(hortProduceTranApprovals);
                if (result.Any())
                {
                    return this.Ok(result);
                }
                else
                {
                    return this.NotFound();
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound();
            }
        }

        /// <summary>
        /// PostHortProduceActualApproval.
        /// </summary>
        /// <param name="hortProduce">hortProduce.</param>
        /// <returns>CropCoverageActualBlockApprovalResponse.</returns>
        [HttpPost("PostHortProduceActualApproval")]
        public IActionResult PostHortProduceActualApproval([FromBody] HortProduceActlApproval hortProduce)
        {
            try
            {
                List<CropCoverageActualBlockApprovalResponse> result = this.horticultureService.InsertHortCoverageActualApproval(hortProduce);
                if (result.Any())
                {
                    return this.Ok(result);
                }
                else
                {
                    return this.NotFound();
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound();
            }
        }

        /// <summary>
        /// PostHortProduceActualPnchyt.
        /// </summary>
        /// <param name="hortProduce">hortProduce.</param>
        /// <returns>CropCoverageActualPanchayatApprovalResponse.</returns>
        [HttpPost("PostHortProduceActualPnchyt")]
        public IActionResult PostHortProduceActualPnchyt([FromBody] List<HortProduceActualPanchayat> hortProduce)
        {
            try
            {
                List<CropCoverageActualPanchayatApprovalResponse> result = this.horticultureService.InsertHortProduceActualPnchyt(hortProduce);
                if (result.Any())
                {
                    return this.Ok(result);
                }
                else
                {
                    return this.NotFound();
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound();
            }
        }

        /// <summary>
        /// PostHortProduceAutoApprovalActlBlock.
        /// </summary>
        /// <returns>IActionResult.</returns>
        [HttpPost("PostHortProduceAutoApprovalActlBlock")]
        public IActionResult PostHortProduceAutoApprovalActlBlock()
        {
            try
            {
                int result = this.horticultureService.HortProduceAutoApprovalActlBlock();
                if (result == 1)
                {
                    return this.Ok(new { data = "Updated Successfully" });
                }
                else
                {
                    return this.NotFound();
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound();
            }
        }

        /// <summary>
        /// PostHortProduceAutoApprovalActlPanchayat.
        /// </summary>
        /// <returns>PostHortProduceAutoApprovalActlPanchayat Response.</returns>
        [HttpPost("PostHortProduceAutoApprovalActlPanchayat")]
        public IActionResult PostHortProduceAutoApprovalActlPanchayat()
        {
            try
            {
                int result = this.horticultureService.HortProduceAutoApprovalActlPanchayat();
                if (result == 1)
                {
                    return this.Ok(new { data = "Updated Successfully" });
                }
                else
                {
                    return this.NotFound();
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound();
            }
        }

        /// <summary>
        /// PostHortProduceActlDataCorrection.
        /// </summary>
        /// <returns>PostHortProduceActlDataCorrection Response.</returns>
        [HttpPost("PostHortProduceActlDataCorrection")]
        public IActionResult PostHortProduceActlDataCorrection()
        {
            try
            {
                int result = this.horticultureService.HortProduceActlDataCorrection();
                if (result == 1)
                {
                    return this.Ok(new { data = "Updated Successfully" });
                }
                else
                {
                    return this.NotFound();
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound();
            }
        }

        /// <summary>
        /// PostSeedPerformanceHorticulture.
        /// </summary>
        /// <param name="perf">perf.</param>
        /// <returns>PostSeedPerformanceHorticulture Response.</returns>
        [HttpPost("PostSeedPerformanceHorticulture")]
        public IActionResult PostSeedPerformanceHorticulture([FromBody] InsertHorticultureSeedPerf perf)
        {
            try
            {
                int result = this.horticultureService.InsertHortiSeedPerformance(perf).Result;
                if (result == 1)
                {
                    return this.Ok("{\"status\": \"Insertion Success\"}");
                }
                else
                {
                    return this.NotFound("{\"status\": \"Insertion Failed\"}");
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound("{\"status\": \"Insertion Failed\"}");
            }
        }

        /// <summary>
        /// GetBlockPanchaytLgCodes.
        /// </summary>
        /// <param name="districtId">districtId.</param>
        /// <returns>BlockPanchayatLgCodes.</returns>
        [HttpGet("GetBlockPanchaytLgCodes/{districtId}")]
        public IActionResult GetBlockPanchaytLgCodes(int districtId)
        {
            try
            {
                List<BlockPanchayatLgCodes> result = this.horticultureService.GetBlockPanchaytLgCodes(districtId);

                if (!result.Any() || result == null)
                {
                    return this.NotFound(this.Json("Not Found"));
                }

                return this.Ok(result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound(this.Json("Not Found"));
            }
        }
    }
}
