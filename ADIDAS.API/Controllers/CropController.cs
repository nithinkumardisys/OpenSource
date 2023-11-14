//------------------------------------------------------------------------------
// <copyright file="CropController.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------
namespace ADIDAS.API.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using ADIDAS.Model.DTO;
    using ADIDAS.Model.Entities;
    using ADIDAS.Service.Interfaces;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// CropController.
    /// </summary>
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class CropController : Controller
    {
        private readonly ICropService cropService;
        private readonly IHorticultureService horticultureService;
        private readonly ILogger<CropController> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="CropController"/> class.
        /// CropController.
        /// </summary>
        /// <param name="cropService">cropService.</param>
        /// <param name="horticultureService">horticultureService.</param>
        /// <param name="logger">logger.</param>
        public CropController(ICropService cropService, IHorticultureService horticultureService, ILogger<CropController> logger)
        {
            this.cropService = cropService;
            this.horticultureService = horticultureService;
            this.logger = logger;
        }

        /// <summary>
        /// GetCropCoverageAimPancht.
        /// </summary>
        /// <param name="seasonId">seasonId.</param>
        /// <param name="panchayatId">panchayatId.</param>
        /// <returns>GetCropCoverageAimPancht List.</returns>
        [HttpGet("GetCropCoverageAimPancht/{seasonId}/{panchayatId}")]
        public IActionResult GetCropCoverageAimPancht(string seasonId, string panchayatId)
        {
            try
            {
                List<CropCoverageAimPancht> result = this.cropService.GetCropCoverageAimPancht(seasonId, panchayatId);
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
        /// GetCropCoverageAimVillage.
        /// </summary>
        /// <param name="seasonId">seasonId.</param>
        /// <param name="panchayatId">panchayatId.</param>
        /// <param name="villageId">villageId.</param>
        /// <returns>GetCropCoverageAimVillage List.</returns>
        [HttpGet("GetCropCoverageAimVillage/{seasonId}/{panchayatId}/{villageId}")]
        public IActionResult GetCropCoverageAimVillage(string seasonId, string panchayatId, string villageId)
        {
            try
            {
                List<CropCoverageAimVillage> result = this.cropService.GetCropCoverageAimVillage(seasonId, panchayatId, villageId);
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
        /// GetSeedusedIputViewSubmission.
        /// </summary>
        /// <param name="seasonId">seasonId.</param>
        /// <param name="cropId">cropId.</param>
        /// <param name="district_id">district_id.</param>
        /// <returns>GetSeedusedIputViewSubmission List.</returns>
        [HttpGet("GetSeedusedIputViewSubmission/{seasonId}/{cropId}/{district_id}")]
        public IActionResult GetSeedusedIputViewSubmission(string seasonId, string cropId, string district_id)
        {
            try
            {
                DtoSeedUserInput result = this.cropService.GetSeedusedIputViewSubmission(Convert.ToInt32(seasonId), Convert.ToInt32(cropId), Convert.ToInt32(district_id));

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
        /// GetSeedUsedVarietyname.
        /// </summary>
        /// <param name="seasonId">seasonId.</param>
        /// <param name="cropId">cropId.</param>
        /// <param name="district_id">district_id.</param>
        /// <returns>GetSeedUsedVarietyname List.</returns>
        [HttpGet("GetSeedUsedVarietyname/{seasonId}/{cropId}/{district_id}")]
        public IActionResult GetSeedUsedVarietyname(string seasonId, string cropId, string district_id)
        {
            try
            {
                DtoSeedUserInput result = this.cropService.GetSeedUsedVarietyname(Convert.ToInt32(seasonId), Convert.ToInt32(cropId), Convert.ToInt32(district_id));

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
        /// GetSeedusedinputViewSubmissionOffline.
        /// </summary>
        /// <param name="district_id">district_id.</param>
        /// <returns>GetSeedusedinputViewSubmissionOffline List.</returns>
        [HttpGet("GetSeedusedinputViewSubmissionOffline/{district_id}")]
        public IActionResult GetSeedusedinputViewSubmissionOffline(string district_id)
        {
            try
            {
                List<DtoSeedUserInput> result = this.cropService.GetSeedusedinputViewSubmissionOffline(Convert.ToInt32(district_id));

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

        /// <summary>
        /// GetCoverageSubmittedCropsBySeason.
        /// </summary>
        /// <param name="districtId">districtId.</param>
        /// <returns>GetCoverageSubmittedCropsBySeason List.</returns>
        [HttpGet("GetCoverageSubmittedCropsBySeason/{districtId}")]
        public IActionResult GetCoverageSubmittedCropsBySeason(string districtId)
        {
            try
            {
                List<CoverageSubmittedCropsSeason> result = this.cropService.GetCoverageSubmittedCropsBySeason(Convert.ToInt32(districtId));
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
        /// GetTargetSubmittedCropsBySeason.
        /// </summary>
        /// <param name="districtId">districtId.</param>
        /// <returns>GetTargetSubmittedCropsBySeason List.</returns>
        [HttpGet("GetTargetSubmittedCropsBySeason/{districtId}")]
        public IActionResult GetTargetSubmittedCropsBySeason(string districtId)
        {
            try
            {
                List<CoverageSubmittedCropsSeason> result = this.cropService.GetTargetSubmittedCropsBySeason(Convert.ToInt32(districtId));
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
        /// GetCoverageSubmittedCropsBySeasonBAO.
        /// </summary>
        /// <param name="districtId">districtId.</param>
        /// <returns>GetCoverageSubmittedCropsBySeasonBAO List.</returns>
        [HttpGet("GetCoverageSubmittedCropsBySeasonBAO/{districtId}")]
        public IActionResult GetCoverageSubmittedCropsBySeasonBAO(string districtId)
        {
            try
            {
                List<CoverageSubmittedCropsSeason> result = this.cropService.GetCoverageSubmittedCropsBySeasonBAO(Convert.ToInt32(districtId));
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
        /// GetTargetSubmittedCropsBySeasonBAO.
        /// </summary>
        /// <param name="districtId">districtId.</param>
        /// <returns>GetTargetSubmittedCropsBySeasonBAO List.</returns>
        [HttpGet("GetTargetSubmittedCropsBySeasonBAO/{districtId}")]
        public IActionResult GetTargetSubmittedCropsBySeasonBAO(string districtId)
        {
            try
            {
                List<CoverageSubmittedCropsSeason> result = this.cropService.GetTargetSubmittedCropsBySeasonBAO(Convert.ToInt32(districtId));
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
        /// GetCoverageSubmittedCropsBySeasonDAO.
        /// </summary>
        /// <param name="districtId">districtId.</param>
        /// <returns>GetCoverageSubmittedCropsBySeasonDAO List.</returns>
        [HttpGet("GetCoverageSubmittedCropsBySeasonDAO/{districtId}")]
        public IActionResult GetCoverageSubmittedCropsBySeasonDAO(string districtId)
        {
            try
            {
                List<CoverageSubmittedCropsSeason> result = this.cropService.GetCoverageSubmittedCropsBySeasonDAO(Convert.ToInt32(districtId));
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
        /// GetTargetSubmittedCropsBySeasonDAO.
        /// </summary>
        /// <param name="districtId">districtId.</param>
        /// <returns>GetTargetSubmittedCropsBySeasonDAO List.</returns>
        [HttpGet("GetTargetSubmittedCropsBySeasonDAO/{districtId}")]
        public IActionResult GetTargetSubmittedCropsBySeasonDAO(string districtId)
        {
            try
            {
                List<CoverageSubmittedCropsSeason> result = this.cropService.GetTargetSubmittedCropsBySeasonDAO(Convert.ToInt32(districtId));
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
        /// GetCropCoverageAimPanchtDelta.
        /// </summary>
        /// <param name="seasonId">seasonId.</param>
        /// <param name="panchayatId">panchayatId.</param>
        /// <param name="refreshedTime">refreshedTime.</param>
        /// <returns>GetCropCoverageAimPanchtDelta List.</returns>
        [HttpGet("GetCropCoverageAimPanchtDelta/{seasonId}/{panchayatId}/{refreshedTime}")]
        public IActionResult GetCropCoverageAimPanchtDelta(string seasonId, string panchayatId, DateTime refreshedTime)
        {
            try
            {
                List<CropCoverageAimPancht> result = this.cropService.GetCropCoverageAimPanchtDelta(seasonId, panchayatId, refreshedTime);
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
        /// GetCropCoverageAimBlock.
        /// </summary>
        /// <param name="blockId">blockId.</param>
        /// <returns>GetCropCoverageAimBlock List.</returns>
        [HttpGet("GetCropCoverageAimBlock/{blockId}")]
        public IActionResult GetCropCoverageAimBlock(string blockId)
        {
            try
            {
                List<CropCoverageAimBlock> result = this.cropService.GetCropCoverageAimBlock(blockId);
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
        /// GetCropCoverageAimDistrict.
        /// </summary>
        /// <param name="districtId">districtId.</param>
        /// <returns>GetCropCoverageAimDistrict List.</returns>
        [HttpGet("GetCropCoverageAimDistrict/{districtId}")]
        public IActionResult GetCropCoverageAimDistrict(string districtId)
        {
            try
            {
                List<CropCoverageAimDistrict> result = this.cropService.GetCropCoverageAimDistrict(districtId);
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
        /// GetCropCoverageActualPancht.
        /// </summary>
        /// <param name="seasonId">seasonId.</param>
        /// <param name="panchayatId">panchayatId.</param>
        /// <returns>GetCropCoverageActualPancht List.</returns>
        [HttpGet("GetCropCoverageActualPancht/{seasonId}/{panchayatId}")]
        public IActionResult GetCropCoverageActualPancht(string seasonId, string panchayatId)
        {
            try
            {
                List<CropCoverageActualVillage> result = this.cropService.GetCropCoverageActualPancht(seasonId, panchayatId);
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
        /// GetCropCoverageActualVillage.
        /// </summary>
        /// <param name="seasonId">seasonId.</param>
        /// <param name="panchayatId">panchayatId.</param>
        /// <param name="villageId">villageId.</param>
        /// <returns>GetCropCoverageActualVillage List.</returns>
        [HttpGet("GetCropCoverageActualVillage/{seasonId}/{panchayatId}/{villageId}")]
        public IActionResult GetCropCoverageActualVillage(string seasonId, string panchayatId, string villageId)
        {
            try
            {
                List<CropCoverageActualVillage> result = this.cropService.GetCropCoverageActualVillage(seasonId, panchayatId, villageId);
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
        /// GetCropCoverageActualPanchtDelta.
        /// </summary>
        /// <param name="seasonId">seasonId.</param>
        /// <param name="panchayatId">panchayatId.</param>
        /// <param name="refreshedtime">refreshedtime.</param>
        /// <returns>GetCropCoverageActualPanchtDelta List.</returns>
        [HttpGet("GetCropCoverageActualPanchtDelta/{seasonId}/{panchayatId}/{refreshedtime}")]
        public IActionResult GetCropCoverageActualPanchtDelta(string seasonId, string panchayatId, DateTime refreshedtime)
        {
            try
            {
                List<CropCoverageActualPancht> result = this.cropService.GetCropCoverageActualPanchtDelta(seasonId, panchayatId, refreshedtime);
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
        /// GetCropCoverageActualPanchtCurrDate.
        /// </summary>
        /// <param name="seasonId">seasonId.</param>
        /// <param name="panchayatId">panchayatId.</param>
        /// <returns>GetCropCoverageActualPanchtCurrDate List.</returns>
        [HttpGet("GetCropCoverageActualPanchtCurrDate/{seasonId}/{panchayatId}")]
        public IActionResult GetCropCoverageActualPanchtCurrDate(string seasonId, string panchayatId)
        {
            try
            {
                List<CropCoverageActualVillage> result = this.cropService.GetCropCoverageActualPanchtCurrDate(seasonId, panchayatId);
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
        /// GetCropCoverageActualVillageCurrDate.
        /// </summary>
        /// <param name="seasonId">seasonId.</param>
        /// <param name="panchayatId">panchayatId.</param>
        /// <param name="villageId">villageId.</param>
        /// <returns>GetCropCoverageActualVillageCurrDate List.</returns>
        [HttpGet("GetCropCoverageActualVillageCurrDate/{seasonId}/{panchayatId}/{villageId}")]
        public IActionResult GetCropCoverageActualVillageCurrDate(string seasonId, string panchayatId, string villageId)
        {
            try
            {
                List<CropCoverageActualVillage> result = this.cropService.GetCropCoverageActualVillageCurrDate(seasonId, panchayatId, villageId);
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
        /// GetCropCoverageActualBlock.
        /// </summary>
        /// <param name="blockId">blockId.</param>
        /// <returns>GetCropCoverageActualBlock List.</returns>
        [HttpGet("GetCropCoverageActualBlock/{blockId}")]
        public IActionResult GetCropCoverageActualBlock(string blockId)
        {
            try
            {
                List<CropCoverageActualBlock> result = this.cropService.GetCropCoverageActualBlock(blockId);
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
        /// GetCropCoverageActualDistrict.
        /// </summary>
        /// <param name="districtId">districtId.</param>
        /// <returns>GetCropCoverageActualDistrict List.</returns>
        [HttpGet("GetCropCoverageActualDistrict/{districtId}")]
        public IActionResult GetCropCoverageActualDistrict(string districtId)
        {
            try
            {
                List<CropCoverageActualDistrict> result = this.cropService.GetCropCoverageActualDistrict(districtId);
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
        /// GetCropDamagePancht.
        /// </summary>
        /// <param name="seasonId">seasonId.</param>
        /// <param name="panchayatId">panchayatId.</param>
        /// <returns>GetCropDamagePancht List.</returns>
        [HttpGet("GetCropDamagePancht/{seasonId}/{panchayatId}")]
        public IActionResult GetCropDamagePancht(string seasonId, string panchayatId)
        {
            try
            {
                List<CropDamagePancht> result = this.cropService.GetCropDamagePancht(seasonId, panchayatId);
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
        /// GetCropDamagePanchtDelta.
        /// </summary>
        /// <param name="seasonId">seasonId.</param>
        /// <param name="panchayatId">panchayatId.</param>
        /// <param name="lastrefreshedtime">lastrefreshedtime.</param>
        /// <returns>GetCropDamagePanchtDelta List.</returns>
        [HttpGet("GetCropDamagePanchtDelta/{seasonId}/{panchayatId}/{lastrefreshedtime}")]
        public IActionResult GetCropDamagePanchtDelta(string seasonId, string panchayatId, DateTime lastrefreshedtime)
        {
            try
            {
                List<CropDamagePancht> result = this.cropService.GetCropDamagePanchtDelta(seasonId, panchayatId, lastrefreshedtime);
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
        /// GetCropDamageBlock.
        /// </summary>
        /// <param name="blockId">blockId.</param>
        /// <returns>GetCropDamageBlock List.</returns>
        [HttpGet("GetCropDamageBlock/{blockId}")]
        public IActionResult GetCropDamageBlock(string blockId)
        {
            try
            {
                List<CropDamageBlock> result = this.cropService.GetCropDamageBlock(blockId);
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
        /// GetCropDamageDistrict.
        /// </summary>
        /// <param name="districtId">districtId.</param>
        /// <returns>GetCropDamageDistrict List.</returns>
        [HttpGet("GetCropDamageDistrict/{districtId}")]
        public IActionResult GetCropDamageDistrict(string districtId)
        {
            try
            {
                List<CropDamageDistrict> result = this.cropService.GetCropDamageDistrict(districtId);
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
        /// GetCropCoverageTargetDAO.
        /// </summary>
        /// <param name="district_id">district_id.</param>
        /// <param name="season_id">season_id.</param>
        /// <param name="crop_id">crop_id.</param>
        /// <returns>GetCropCoverageTargetDAO List.</returns>
        [HttpGet("GetCropCoverageTargetDAO/{district_id}/{season_id}/{crop_id}")]
        public IActionResult GetCropCoverageTargetDAO(int district_id, int season_id, int crop_id)
        {
            try
            {
                CropCoverageAim result = this.cropService.GetCropCoverageTargetDAO(district_id, season_id, crop_id);
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
        /// GetCropCoverageTargetDAOOffline.
        /// </summary>
        /// <param name="district_id">district_id.</param>
        /// <param name="season_id">season_id.</param>
        /// <param name="crop_ids">crop_ids.</param>
        /// <returns>GetCropCoverageTargetDAOOffline List.</returns>
        [HttpGet("GetCropCoverageTargetDAOOffline/{district_id}/{season_id}/{crop_ids}")]
        public IActionResult GetCropCoverageTargetDAOOffline(int district_id, int season_id, string crop_ids)
        {
            try
            {
                List<CropCoverageAim> result = this.cropService.GetCropCoverageTargetDAOOffline(district_id, season_id, crop_ids);
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
        /// GetCropDamageDAOOffline.
        /// </summary>
        /// <param name="district_id">district_id.</param>
        /// <param name="season_id">season_id.</param>
        /// <param name="crop_ids">crop_ids.</param>
        /// <returns>GetCropDamageDAOOffline List.</returns>
        [HttpGet("GetCropDamageDAOOffline/{District_id}/{Season_id}/{Crop_ids}")]
        public IActionResult GetCropDamageDAOOffline(int district_id, int season_id, string crop_ids)
        {
            try
            {
                List<CropDamage> result = this.cropService.GetCropDamageDAOOffline(district_id, season_id, crop_ids);
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
        /// GetCropCoverageTargetDAODelta.
        /// </summary>
        /// <param name="district_id">district_id.</param>
        /// <param name="season_id">season_id.</param>
        /// <param name="last_refresh_time">last_refresh_time.</param>
        /// <returns>GetCropCoverageTargetDAODelta List.</returns>
        [HttpGet("GetCropCoverageTargetDAODelta/{District_id}/{Season_id}/{last_refresh_time}")]
        public IActionResult GetCropCoverageTargetDAODelta(int district_id, int season_id, DateTime last_refresh_time)
        {
            try
            {
                List<CropCoverageAim> result = this.cropService.GetCropCoverageTargetDAODelta(district_id, season_id, last_refresh_time);
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
        /// GetCropCoverageActualDAODelta.
        /// </summary>
        /// <param name="district_id">district_id.</param>
        /// <param name="season_id">season_id.</param>
        /// <param name="last_refresh_time">last_refresh_time.</param>
        /// <returns>GetCropCoverageActualDAODelta List.</returns>
        [HttpGet("GetCropCoverageActualDAODelta/{District_id}/{Season_id}/{last_refresh_time}")]
        public IActionResult GetCropCoverageActualDAODelta(int district_id, int season_id, DateTime last_refresh_time)
        {
            try
            {
                List<CropCoverageActual> result = this.cropService.GetCropCoverageActualDAODelta(district_id, season_id, last_refresh_time);
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
        /// GetCropCoverageActualBAODelta.
        /// </summary>
        /// <param name="district_id">district_id.</param>
        /// <param name="block_id">block_id.</param>
        /// <param name="season_id">season_id.</param>
        /// <param name="last_refresh_time">last_refresh_time.</param>
        /// <returns>GetCropCoverageActualBAODelta List.</returns>
        [HttpGet("GetCropCoverageActualBAODelta/{district_id}/{block_id}/{season_id}/{last_refresh_time}")]
        public IActionResult GetCropCoverageActualBAODelta(int district_id, int block_id, int season_id, DateTime last_refresh_time)
        {
            try
            {
                List<CropCoverageActual> result = this.cropService.GetCropCoverageActualBAODelta(district_id, block_id, season_id, last_refresh_time);
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
        /// GetCropDamageBAODelta.
        /// </summary>
        /// <param name="district_id">district_id.</param>
        /// <param name="block_id">block_id.</param>
        /// <param name="season_id">season_id.</param>
        /// <param name="last_refresh_time">last_refresh_time.</param>
        /// <returns>GetCropDamageBAODelta List.</returns>
        [HttpGet("GetCropDamageBAODelta/{District_id}/{Block_id}/{Season_id}/{last_refresh_time}")]
        public IActionResult GetCropDamageBAODelta(int district_id, int block_id, int season_id, DateTime last_refresh_time)
        {
            try
            {
                List<CropDamage> result = this.cropService.GetCropDamageBAOOfflineDelta(district_id, block_id, season_id, last_refresh_time);
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
        /// GetCropDamageDAODelta.
        /// </summary>
        /// <param name="district_id">district_id.</param>
        /// <param name="season_id">season_id.</param>
        /// <param name="last_refresh_time">last_refresh_time.</param>
        /// <returns>GetCropDamageDAODelta List.</returns>
        [HttpGet("GetCropDamageDAODelta/{District_id}/{Season_id}/{last_refresh_time}")]
        public IActionResult GetCropDamageDAODelta(int district_id, int season_id, DateTime last_refresh_time)
        {
            try
            {
                List<CropDamage> result = this.cropService.GetCropDamageDAOOfflineDelta(district_id, season_id, last_refresh_time);
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
        /// GetCropCoverageActualBAOOffline.
        /// </summary>
        /// <param name="district_id">district_id.</param>
        /// <param name="block_id">block_id.</param>
        /// <param name="season_id">season_id.</param>
        /// <param name="crop_ids">crop_ids.</param>
        /// <returns>GetCropCoverageActualBAOOffline List.</returns>
        [HttpGet("GetCropCoverageActualBAOOffline/{district_id}/{block_id}/{season_id}/{crop_ids}")]
        public IActionResult GetCropCoverageActualBAOOffline(int district_id, int block_id, int season_id, string crop_ids)
        {
            try
            {
                List<CropCoverageActual> result = this.cropService.GetCropCoverageActualBAOOffline(district_id, block_id, season_id, crop_ids);
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
        /// GetCropDamageBAOOffline.
        /// </summary>
        /// <param name="district_id">district_id.</param>
        /// <param name="block_id">block_id.</param>
        /// <param name="season_id">season_id.</param>
        /// <param name="crop_ids">crop_ids.</param>
        /// <returns>GetCropDamageBAOOffline List.</returns>
        [HttpGet("GetCropDamageBAOOffline/{District_id}/{Block_id}/{Season_id}/{Crop_ids}")]
        public IActionResult GetCropDamageBAOOffline(int district_id, int block_id, int season_id, string crop_ids)
        {
            try
            {
                List<CropDamage> result = this.cropService.GetCropDamageBAOOffline(district_id, block_id, season_id, crop_ids);
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
        /// GetCropCoverageActualDAOOffline.
        /// </summary>
        /// <param name="district_id">district_id.</param>
        /// <param name="season_id">season_id.</param>
        /// <param name="crop_ids">crop_ids.</param>
        /// <returns>GetCropCoverageActualDAOOffline List.</returns>
        [HttpGet("GetCropCoverageActualDAOOffline/{district_id}/{season_id}/{crop_ids}")]
        public IActionResult GetCropCoverageActualDAOOffline(int district_id, int season_id, string crop_ids)
        {
            try
            {
                if (crop_ids is null)
                {
                    throw new ArgumentNullException(nameof(crop_ids));
                }

                List<CropCoverageActual> result = this.cropService.GetCropCoverageActualDAOOffline(district_id, season_id, crop_ids);
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
        /// GetCropCoverageTargetBAODelta.
        /// </summary>
        /// <param name="district_id">district_id.</param>
        /// <param name="block_id">block_id.</param>
        /// <param name="season_id">season_id.</param>
        /// <param name="last_refresh_time">last_refresh_time.</param>
        /// <returns>GetCropCoverageTargetBAODelta List.</returns>
        [HttpGet("GetCropCoverageTargetBAODelta/{District_id}/{Block_id}/{Season_id}/{last_refresh_time}")]
        public IActionResult GetCropCoverageTargetBAODelta(int district_id, int block_id, int season_id, DateTime last_refresh_time)
        {
            try
            {
                List<CropCoverageAim> result = this.cropService.GetCropCoverageTargetBAODelta(district_id, block_id, season_id, last_refresh_time);
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
        /// GetCropCoverageActualDAO.
        /// </summary>
        /// <param name="district_id">district_id.</param>
        /// <param name="season_id">season_id.</param>
        /// <param name="crop_id">crop_id.</param>
        /// <returns>GetCropCoverageActualDAO List.</returns>
        [HttpGet("GetCropCoverageActualDAO/{district_id}/{season_id}/{crop_id}")]
        public IActionResult GetCropCoverageActualDAO(int district_id, int season_id, int crop_id)
        {
            try
            {
                CropCoverageActual result = this.cropService.GetCropCoverageActualDAO(district_id, season_id, crop_id);
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
        /// GetCropDamageDAO.
        /// </summary>
        /// <param name="district_id">district_id.</param>
        /// <param name="season_id">season_id.</param>
        /// <param name="crop_id">crop_id.</param>
        /// <returns>GetCropDamageDAO List.</returns>
        [HttpGet("GetCropDamageDAO/{District_id}/{Season_id}/{Crop_id}")]
        public IActionResult GetCropDamageDAO(int district_id, int season_id, int crop_id)
        {
            try
            {
                CropDamage result = this.cropService.GetCropDamageDAO(district_id, season_id, crop_id);
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
        /// GetCropCoverageTargetBAO.
        /// </summary>
        /// <param name="district_id">district_id.</param>
        /// <param name="block_id">block_id.</param>
        /// <param name="season_id">season_id.</param>
        /// <param name="crop_id">crop_id.</param>
        /// <returns>GetCropCoverageTargetBAO List.</returns>
        [HttpGet("GetCropCoverageTargetBAO/{district_id}/{block_id}/{season_id}/{crop_id}")]
        public IActionResult GetCropCoverageTargetBAO(int district_id, int block_id, int season_id, int crop_id)
        {
            try
            {
                CropCoverageAim result = this.cropService.GetCropCoverageTargetBAO(district_id, block_id, season_id, crop_id);
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
        /// GetCropCoverageTargetBAOOffline.
        /// </summary>
        /// <param name="district_id">district_id.</param>
        /// <param name="block_id">block_id.</param>
        /// <param name="season_id">season_id.</param>
        /// <param name="crop_ids">crop_ids.</param>
        /// <returns>GetCropCoverageTargetBAOOffline List.</returns>
        [HttpGet("GetCropCoverageTargetBAOOffline/{district_id}/{block_id}/{season_id}/{crop_ids}")]
        public IActionResult GetCropCoverageTargetBAOOffline(int district_id, int block_id, int season_id, string crop_ids)
        {
            try
            {
                List<CropCoverageAim> result = this.cropService.GetCropCoverageTargetBAOOffline(district_id, block_id, season_id, crop_ids);
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
        /// GetCropCoverageActualBAO.
        /// </summary>
        /// <param name="district_id">district_id.</param>
        /// <param name="block_id">block_id.</param>
        /// <param name="season_id">season_id.</param>
        /// <param name="crop_id">crop_id.</param>
        /// <returns>GetCropCoverageActualBAO list.</returns>
        [HttpGet("GetCropCoverageActualBAO/{district_id}/{block_id}/{season_id}/{crop_id}")]
        public IActionResult GetCropCoverageActualBAO(int district_id, int block_id, int season_id, int crop_id)
        {
            try
            {
                CropCoverageActual result = this.cropService.GetCropCoverageActualBAO(district_id, block_id, season_id, crop_id);
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
        /// GetCropDamageBAO.
        /// </summary>
        /// <param name="district_id">district_id.</param>
        /// <param name="block_id">block_id.</param>
        /// <param name="season_id">season_id.</param>
        /// <param name="crop_id">crop_id.</param>
        /// <returns>GetCropDamageBAO List.</returns>
        [HttpGet("GetCropDamageBAO/{District_id}/{Block_id}/{Season_id}/{Crop_id}")]
        public IActionResult GetCropDamageBAO(int district_id, int block_id, int season_id, int crop_id)
        {
            try
            {
                CropDamage result = this.cropService.GetCropDamageBAO(district_id, block_id, season_id, crop_id);
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
        /// GetSeedPerformance.
        /// </summary>
        /// <param name="panchayatId">panchayatId.</param>
        /// <param name="seasonId">seasonId.</param>
        /// <returns>GetSeedPerformance List.</returns>
        [HttpGet("GetSeedPerformance/{panchayatId}/{seasonId}")]
        public IActionResult GetSeedPerformance(int panchayatId, int seasonId)
        {
            try
            {
                List<DtoSeedPerformance> result = this.cropService.GetSeedPerformance(panchayatId, seasonId);

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
        /// GetAgricultureBlockPanchayatByDistrict.
        /// </summary>
        /// <param name="districtId">districtId.</param>
        /// <returns>GetAgricultureBlockPanchayatByDistrict List.</returns>
        [HttpGet("GetAgricultureBlockPanchayatByDistrict/{districtId}")]
        public IActionResult GetAgricultureBlockPanchayatByDistrict(string districtId)
        {
            try
            {
                BlockPanchayatByDistrict result = this.cropService.GetAgricultureBlockkPanchayatByDistrict(districtId);
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
        /// GetSeedPerformanceAgriculture.
        /// </summary>
        /// <param name="districtId">districtId.</param>
        /// <param name="blockId">blockId.</param>
        /// <param name="panchayatId">panchayatId.</param>
        /// <param name="seasonId">seasonId.</param>
        /// <returns>GetSeedPerformanceAgriculture List.</returns>
        [HttpGet("GetSeedPerformanceAgriculture/{districtId}/{blockId}/{panchayatId}/{seasonId}")]
        public IActionResult GetSeedPerformanceAgriculture(string districtId, string blockId, string panchayatId, string seasonId)
        {
            try
            {
                List<DtoSeedPerformanceAgriculture> result = this.cropService.GetSeedPerformanceAgriculture(districtId, blockId, panchayatId, seasonId);

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
        /// GetAllListsOfDamageReasons.
        /// </summary>
        /// <param name="year">year.</param>
        /// <returns>GetAllListsOfDamageReasons List.</returns>
        [HttpGet("GetAllListsOfDamageReasons/{year}")]
        public IActionResult GetAllListsOfDamageReasons(string year)
        {
            try
            {
                List<CropDamageEntity> result = this.cropService.GetAllListsOfDamageReasons(Convert.ToInt32(year));
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
        /// GetSpecificDamageReason.
        /// </summary>
        /// <param name="damage">damage.</param>
        /// <returns>GetSpecificDamageReason List.</returns>
        [HttpPost("GetSpecificDamageReason")]
        public IActionResult GetSpecificDamageReason(DtoEditDamageRequest damage)
        {
            try
            {
                DtoEditDamage result = this.cropService.GetSpecificDamageReason(damage);

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
        /// GetDamageReasonNames.
        /// </summary>
        /// <returns>GetDamageReasonNames List.</returns>
        [HttpGet("GetDamageReasonNames")]
        public IActionResult GetDamageReasonNames()
        {
            try
            {
                List<CropDamageReasonNames> result = this.cropService.GetDamageReasonNames();
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
        /// GetDamageCropList.
        /// </summary>
        /// <returns>GetDamageCropList List.</returns>
        [HttpGet("GetDamageCropList")]
        public IActionResult GetDamageCropList()
        {
            try
            {
                List<CropNames> result = this.cropService.GetDamageCropList();
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
        /// GetDamageConstantCrops.
        /// </summary>
        /// <returns>GetDamageConstantCrops List.</returns>
        [HttpGet("GetDamageConstantCrops")]
        public IActionResult GetDamageConstantCrops()
        {
            try
            {
                List<CropNames> result = this.cropService.GetDamageConstantCropList();
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
        /// GetEstdCropDamagePercnt.
        /// </summary>
        /// <param name="cropDamageReasonModel">cropDamageReasonModel.</param>
        /// <returns>GetEstdCropDamagePercnt List.</returns>
        [HttpGet("GetEstdCropDamagePercnt")]
        public IActionResult GetEstdCropDamagePercnt(CropDamageGetModel cropDamageReasonModel)
        {
            try
            {
                List<CropDamageGetModel> result = this.cropService.GetEstdCropDamagePercnt(cropDamageReasonModel);
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
        /// GetHorticultureSubmittedDetails.
        /// </summary>
        /// <param name="seasonId">seasonId.</param>
        /// <param name="districtId">districtId.</param>
        /// <returns>GetHorticultureSubmittedDetails List.</returns>
        [HttpGet("GetHorticultureSubmittedDetails/{SeasonId}/{DistrictId}")]
        public IActionResult GetHorticultureSubmittedDetails(int seasonId, int districtId)
        {
            try
            {
                List<HorticultureDetails> result = this.cropService.GetHorticultureSubmittedDetails(seasonId, districtId);
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
        /// GetSchemeByPanchayatId.
        /// </summary>
        /// <param name="panchayatId">panchayatId.</param>
        /// <returns>GetSchemeByPanchayatId List.</returns>
        [HttpGet("GetSchemeByPanchayatId/{panchayatId}")]
        public IActionResult GetSchemeByPanchayatId(int panchayatId)
        {
            try
            {
                List<SchemeModel> result = this.cropService.GetSchemesByPanchayat(panchayatId);
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
        /// GetBAONotSubmittedByBlock.
        /// </summary>
        /// <param name="block_id">block_id.</param>
        /// <returns>GetBAONotSubmittedByBlock List.</returns>
        [HttpGet("GetBAONotSubmittedByBlock/{Block_id}")]
        public IActionResult GetBAONotSubmittedByBlock(int block_id)
        {
            try
            {
                List<CropDamageDetailsGet> result = this.cropService.GetBAONotSubmittedByBlock(block_id);
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
        /// GetAllColdStorage.
        /// </summary>
        /// <param name="getHortiReportPHMS">getHortiReportPHMS.</param>
        /// <returns>GetAllColdStorage List.</returns>
        [HttpPost("GetAllColdStorage")]
        public IActionResult GetAllColdStorage([FromBody] GetHortiReportPhmsModel getHortiReportPHMS)
        {
            try
            {
                List<GetGrpdwnColdStorageModel> result = this.cropService.GetAllColdStorage(getHortiReportPHMS);
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
        /// GetAllColdStorageByDistrict.
        /// </summary>
        /// <param name="getHortiReportPHMS">getHortiReportPHMS.</param>
        /// <returns>GetAllColdStorageByDistrict List.</returns>
        [HttpPost("GetAllColdStorageByDistrict")]
        public IActionResult GetAllColdStorageByDistrict([FromBody] GetHortiReportPhmsModel getHortiReportPHMS)
        {
            try
            {
                List<GetGrpdwnColdStorageModel> result = this.cropService.GetAllColdStorageByDistrict(getHortiReportPHMS);
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
        /// GetDAONotSubmittedByDistrict.
        /// </summary>
        /// <param name="district_Id">district_Id.</param>
        /// <returns>GetDAONotSubmittedByDistrict List.</returns>
        [HttpGet("GetDAONotSubmittedByDistrict/{District_Id}")]
        public IActionResult GetDAONotSubmittedByDistrict(int district_Id)
        {
            try
            {
                List<CropDamageDetailsGet> result = this.cropService.GetDAONotSubmittedByDistrict(district_Id);
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
        /// PostCropCoverageActualPanchayat.
        /// </summary>
        /// <param name="crop">crop.</param>
        /// <returns>PostCropCoverageActualPanchayat List.</returns>
        [HttpPost("PostCropCoverageActualPanchayat")]
        public IActionResult PostCropCoverageActualPanchayat([FromBody] List<CropCoverageActualVillage> crop)
        {
            try
            {
                List<CropCoverageActualPanchayatApprovalResponse> result = this.cropService.InsertCropCoverageActualPancht(crop);

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
        /// PostCropCoverageActualVillage.
        /// </summary>
        /// <param name="crop">crop.</param>
        /// <returns>PostCropCoverageActualVillage List.</returns>
        [HttpPost("PostCropCoverageActualVillage")]
        public IActionResult PostCropCoverageActualVillage([FromBody] List<CropCoverageActualVillage> crop)
        {
            try
            {
                List<CropCoverageActualVillageApprovalResponse> result = this.cropService.InsertCropCoverageActualVillage(crop);

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
        /// PostSeedPerformance.
        /// </summary>
        /// <param name="seedPerformance">seedPerformance.</param>
        /// <returns>PostSeedPerformance List.</returns>
        [HttpPost("PostSeedPerformance")]
        public async Task<IActionResult> PostSeedPerformance([FromBody] SeedPerformance seedPerformance)
        {
            try
            {
                InsertSeedPerformanceResponse result = await this.cropService.InsertSeedPerformance(seedPerformance);

                if (result.Status == "Failed")
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
        /// PostCropCoverageTargetPanchayat.
        /// </summary>
        /// <param name="crop">crop.</param>
        /// <returns>PostCropCoverageTargetPanchayat List.</returns>
        [HttpPost("PostCropCoverageTargetPanchayat")]
        public IActionResult PostCropCoverageTargetPanchayat([FromBody] List<CropCoverageAimPancht> crop)
        {
            try
            {
                List<CropCoverageTargetPanchytApprovalResponse> result = this.cropService.InsertCropCoverageAimPancht(crop);

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
        /// PostCropCoverageTargetVillage.
        /// </summary>
        /// <param name="crop">crop.</param>
        /// <returns>PostCropCoverageTargetVillage List.</returns>
        [HttpPost("PostCropCoverageTargetVillage")]
        public IActionResult PostCropCoverageTargetVillage([FromBody] List<CropCoverageAimVillage> crop)
        {
            try
            {
                List<CropCoverageTargetVillageApprovalResponse> result = this.cropService.InsertCropCoverageAimVillage(crop);

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
        /// PostCropDamagePanchayat.
        /// </summary>
        /// <param name="crop">crop.</param>
        /// <returns>PostCropDamagePanchayat List.</returns>
        [HttpPost("PostCropDamagePanchayat")]
        public IActionResult PostCropDamagePanchayat([FromBody] List<CropDamagePancht> crop)
        {
            try
            {
                List<CropDamagePanchayatResponse> result = this.cropService.InsertCropDamagePancht(crop);

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
        /// PostCropCoverageActualApproval.
        /// </summary>
        /// <param name="cropCoverageActual">cropCoverageActual.</param>
        /// <returns>PostCropCoverageActualApproval List.</returns>
        [HttpPost("PostCropCoverageActualApproval")]
        public IActionResult PostCropCoverageActualApproval([FromBody] CropCoverageActual cropCoverageActual)
        {
            try
            {
                List<CropCoverageActualBlockApprovalResponse> result = this.cropService.InsertCropCoverageActualApproval(cropCoverageActual);

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
        /// PostCropCoverageTargetApproval.
        /// </summary>
        /// <param name="cropCoverageTarget">cropCoverageTarget.</param>
        /// <returns>PostCropCoverageTargetApproval List.</returns>
        [HttpPost("PostCropCoverageTargetApproval")]
        public IActionResult PostCropCoverageTargetApproval([FromBody] CropCoverageAim cropCoverageTarget)
        {
            try
            {
                List<CropCoverageTargetBlockApproval> result = this.cropService.InsertCropCoverageTargetApproval(cropCoverageTarget);

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
        /// PostCropDamageApproval.
        /// </summary>
        /// <param name="cropDamage">cropDamage.</param>
        /// <returns>PostCropDamageApproval List.</returns>
        [HttpPost("PostCropDamageApproval")]
        public IActionResult PostCropDamageApproval([FromBody] CropDamage cropDamage)
        {
            try
            {
                List<CropDamageBlockApprovalResponse> result = this.cropService.InsertCropDamageApproval(cropDamage);

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
        /// GetHorticultureCrop.
        /// </summary>
        /// <param name="districtId">districtId.</param>
        /// <param name="seasonId">seasonId.</param>
        /// <returns>GetHorticultureCrop List.</returns>
        [HttpGet("GetHorticultureCrop/{DistrictId}/{SeasonId}")]
        public IActionResult GetHorticultureCrop(int districtId, int seasonId)
        {
            try
            {
                List<HorticultureCrop> result = this.cropService.GetHorticultureCrop(districtId, seasonId);
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
        /// GetAllDistrict.
        /// </summary>
        /// <returns>GetAllDistrict List.</returns>
        [HttpGet("GetAllDistrict")]
        public IActionResult GetAllDistrict()
        {
            try
            {
                List<DistrictList> result = this.cropService.GetAllDistrict();
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
        /// GetACCropDamageDetailsPanchtOnline.
        /// </summary>
        /// <param name="panchayat_Id">panchayat_Id.</param>
        /// <param name="damage_reason_id">damage_reason_id.</param>
        /// <returns>GetACCropDamageDetailsPanchtOnline List.</returns>
        [HttpGet("GetACCropDamageDetailsPanchtOnline/{Panchayat_Id}/{damage_reason_id}")]
        public IActionResult GetACCropDamageDetailsPanchtOnline(string panchayat_Id, int damage_reason_id)
        {
            try
            {
                CropDamageDetailsGet result = this.cropService.GetACCropCvrgCoveredAreaPancht(panchayat_Id, damage_reason_id);
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
        /// GetACCropDamageDetailsPanchtOffline.
        /// </summary>
        /// <param name="panchayat_Id">panchayat_Id.</param>
        /// <returns>GetACCropDamageDetailsPanchtOffline List.</returns>
        [HttpGet("GetACCropDamageDetailsPanchtOffline/{Panchayat_Id}")]
        public IActionResult GetACCropDamageDetailsPanchtOffline(string panchayat_Id)
        {
            try
            {
                List<CropDamageDetailsGet> result = this.cropService.GetACCropCvrgCoveredAreaPanchtOffline(panchayat_Id);
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
        /// GetBAOCropDamageDetailsBlockOnline.
        /// </summary>
        /// <param name="district_Id">district_Id.</param>
        /// <param name="blockid">blockid.</param>
        /// <param name="damage_reason_id">damage_reason_id.</param>
        /// <returns>GetBAOCropDamageDetailsBlockOnline List.</returns>
        [HttpGet("GetBAOCropDamageDetailsBlockOnline/{District_Id}/{blockid}/{damage_reason_id}")]
        public IActionResult GetBAOCropDamageDetailsBlockOnline(string district_Id, int blockid, int damage_reason_id)
        {
            try
            {
                InsBaoCropdmgModel result = this.cropService.GetBAOCropDamageDetailsBlock(Convert.ToInt32(district_Id), blockid, damage_reason_id);

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
        /// GetBAOCropDamageDetailsBlockOffline.
        /// </summary>
        /// <param name="district_Id">district_Id.</param>
        /// <param name="block_Id">block_Id.</param>
        /// <returns>GetBAOCropDamageDetailsBlockOffline List.</returns>
        [HttpGet("GetBAOCropDamageDetailsBlockOffline/{District_Id}/{Block_Id}")]
        public IActionResult GetBAOCropDamageDetailsBlockOffline(int district_Id, int block_Id)
        {
            try
            {
                List<InsBaoCropdmgModel> result = this.cropService.GetBAOCropDamageDetailsBlockOffline(district_Id, block_Id);

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
        /// GetDAOCropDamageDetailsBlockOnline.
        /// </summary>
        /// <param name="district_Id">district_Id.</param>
        /// <param name="damage_Reason_id">damage_Reason_id.</param>
        /// <returns>GetDAOCropDamageDetailsBlockOnline List.</returns>
        [HttpGet("GetDAOCropDamageDetailsBlockOnline/{District_Id}/{damage_Reason_id}")]
        public IActionResult GetDAOCropDamageDetailsBlockOnline(string district_Id, int damage_Reason_id)
        {
            try
            {
                InsBaoCropdmgModel result = this.cropService.GetDAOCropDamageDetailsBlock(district_Id, damage_Reason_id);

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
        /// GetDAOCropDamageDetailsBlockOffline.
        /// </summary>
        /// <param name="district_Id">district_Id.</param>
        /// <returns>GetDAOCropDamageDetailsBlockOffline List.</returns>
        [HttpGet("GetDAOCropDamageDetailsBlockOffline/{District_Id}")]
        public IActionResult GetDAOCropDamageDetailsBlockOffline(string district_Id)
        {
            try
            {
                List<InsBaoCropdmgModel> result = this.cropService.GetDAOCropDamageDetailsBlockOffline(district_Id);
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
        /// GetBAOApprovedCropDamageDetailsBlock.
        /// </summary>
        /// <param name="district_Id">district_Id.</param>
        /// <param name="block_id">block_id.</param>
        /// <param name="panchayat_Id">panchayat_Id.</param>
        /// <param name="damage_id">damage_id.</param>
        /// <returns>GetBAOApprovedCropDamageDetailsBlock List.</returns>
        [HttpGet("GetBAOApprovedCropDamageDetailsBlock")]
        public IActionResult GetBAOApprovedCropDamageDetailsBlock(string district_Id, string block_id, string panchayat_Id, int damage_id)
        {
            try
            {
                CropDamageAll result = this.cropService.GetBAOApprovedCropDamageDetailsBlock(district_Id, block_id, panchayat_Id, damage_id);
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
        /// GetDAOApprovedCropDamageDetailsDistrict.
        /// </summary>
        /// <param name="district_Id">district_Id.</param>
        /// <param name="block_id">block_id.</param>
        /// <param name="panchayat_Id">panchayat_Id.</param>
        /// <param name="damage_id">damage_id.</param>
        /// <returns>GetDAOApprovedCropDamageDetailsDistrict List.</returns>
        [HttpGet("GetDAOApprovedCropDamageDetailsDistrict")]
        public IActionResult GetDAOApprovedCropDamageDetailsDistrict(string district_Id, string block_id, string panchayat_Id, int damage_id)
        {
            try
            {
                CropDamageAll result = this.cropService.GetDAOApprovedCropDamageDetailsDistrict(district_Id, block_id, panchayat_Id, damage_id);
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
        /// GetCropDamageReasonDrpdwn.
        /// </summary>
        /// <param name="district_Id">district_Id.</param>
        /// <returns>GetCropDamageReasonDrpdwn List.</returns>
        [HttpGet("GetCropDamageReasonDrpdwn/{District_Id}")]
        public IActionResult GetCropDamageReasonDrpdwn(string district_Id)
        {
            try
            {
                List<GetCropDamageReasonDrpDwn> result = this.cropService.GetCropDamageReasonDrpdwn(district_Id);
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
        /// GetOnlineACViewSubmissionPancht.
        /// </summary>
        /// <param name="panchayatId">panchayatId.</param>
        /// <param name="damageReasonId">damageReasonId.</param>
        /// <returns>GetOnlineACViewSubmissionPancht List.</returns>
        [HttpGet("GetOnlineACViewSubmissionPancht/{panchayatId}/{damageReasonId}")]
        public IActionResult GetOnlineACViewSubmissionPancht(string panchayatId, int damageReasonId)
        {
            try
            {
                ViewSubmissionAcPanchayat result = this.cropService.GetOnlineACViewSubmissionPancht(panchayatId, damageReasonId);
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
        /// GetOfflineACViewSubmissionPancht.
        /// </summary>
        /// <param name="panchayatId">panchayatId.</param>
        /// <returns>GetOfflineACViewSubmissionPancht List.</returns>
        [HttpGet("GetOfflineACViewSubmissionPancht/{panchayatId}")]
        public IActionResult GetOfflineACViewSubmissionPancht(string panchayatId)
        {
            try
            {
                List<ViewSubmissionAcPanchayat> result = this.cropService.GetOfflineACViewSubmissionPancht(panchayatId);
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
        /// GetCropDamageReportData.
        /// </summary>
        /// <param name="year">year.</param>
        /// <param name="district_ID">district_ID.</param>
        /// <param name="user_id">user_id.</param>
        /// <param name="damageReason_Id">damageReason_Id.</param>
        /// <returns>GetCropDamageReportData List.</returns>
        [HttpGet("GetCropDamageReportData/{year}/{district_ID}/{user_id}/{damageReason_Id}")]
        public IActionResult GetCropDamageReportData(int year, string district_ID, int user_id, string damageReason_Id)
        {
            try
            {
                List<string> result = this.cropService.GetCropDamageReportData(year, district_ID, user_id, damageReason_Id);
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
        /// GetRainfallReportData.
        /// </summary>
        /// <param name="month_year">month_year.</param>
        /// <param name="district_name">district_name.</param>
        /// <returns>GetRainfallReportData List.</returns>
        [HttpGet("GetRainfallReportData/{month_year}/{district_name}")]
        public IActionResult GetRainfallReportData(string month_year, string district_name)
        {
            try
            {
                List<string> result = this.cropService.GetRainfallReportData(month_year, district_name);
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
        /// GetRainfallDistricts.
        /// </summary>
        /// <returns>GetRainfallDistricts List.</returns>
        [HttpGet("GetRainfallDistricts")]
        public IActionResult GetRainfallDistricts()
        {
            try
            {
                List<DistrictList> result = this.cropService.GetRainfallDistricts();
                if (result == null)
                {
                    return this.NotFound("Not Found");
                }

                return this.Ok(result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound("Not Found");
            }
        }

        /// <summary>
        /// GetSeasonByYear.
        /// </summary>
        /// <param name="year">year.</param>
        /// <param name="crop_type">crop_type.</param>
        /// <returns>GetSeasonByYear List.</returns>
        [HttpGet("GetSeasonByYear/{year}/{crop_type}")]
        public IActionResult GetSeasonByYear(int year, string crop_type)
        {
            try
            {
                List<GetSeasonByYearModel> result = this.cropService.GetSeasonByYear(year, crop_type);
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
        /// GetCropBySeasonID.
        /// </summary>
        /// <param name="season_id">season_id.</param>
        /// <param name="crop_type">crop_type.</param>
        /// <returns>GetCropBySeasonID List.</returns>
        [HttpGet("GetCropBySeasonID/{season_id}/{crop_type}")]
        public IActionResult GetCropBySeasonID(string season_id, string crop_type)
        {
            try
            {
                List<GetCropBySeasonIdModel> result = this.cropService.GetCropBySeasonID(season_id, crop_type);

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
        /// GetHortiReportPHMS.
        /// </summary>
        /// <param name="getHortiReportPHMS">getHortiReportPHMS.</param>
        /// <returns>GetHortiReportPHMS List.</returns>
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
        /// <returns>GetHortiReportColdStorage List.</returns>
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
        /// GetCropCvrgTargetDataReport.
        /// </summary>
        /// <param name="cropDamage">cropDamage.</param>
        /// <returns>GetCropCvrgTargetDataReport List.</returns>
        [HttpPost("GetCropCvrgTargetDataReport")]
        public IActionResult GetCropCvrgTargetDataReport([FromBody] GetCropCvrgTargetDataReportModel cropDamage)
        {
            try
            {
                List<string> result = this.cropService.GetCropCvrgTargetDataReport(cropDamage);
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
        /// GetCropCvrgTargetVsCoverageDataReport.
        /// </summary>
        /// <param name="targetVsCoverage">targetVsCoverage.</param>
        /// <returns>list.</returns>
        [HttpPost("GetCropCvrgTargetVsCoverageDataReport")]
        public IActionResult GetCropCvrgTargetVsCoverageDataReport([FromBody] TargetVsCoverageModel targetVsCoverage)
        {
            try
            {
                List<string> result = this.cropService.GetCropCvrgTargetVsCoverageDataReport(targetVsCoverage);
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
        /// GetCropCvrgTargetProductivityDataReport.
        /// </summary>
        /// <param name="cropDamage">cropDamage.</param>
        /// <returns>GetCropCvrgTargetProductivityDataReport List.</returns>
        [HttpPost("GetCropCvrgTargetProductivityDataReport")]
        public IActionResult GetCropCvrgTargetProductivityDataReport([FromBody] GetCropCvrgTargetDataReportModel cropDamage)
        {
            try
            {
                List<string> result = this.cropService.GetCropCvrgTargetProductivityDataReport(cropDamage);
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
        /// GetHortiProduceDataReport.
        /// </summary>
        /// <param name="cropDamage">cropDamage.</param>
        /// <returns>GetHortiProduceDataReport List.</returns>
        [HttpPost("GetHortiProduceDataReport")]
        public IActionResult GetHortiProduceDataReport([FromBody] GetCropCvrgTargetDataReportModel cropDamage)
        {
            try
            {
                List<string> result = this.cropService.GetHortiProduceDataReport(cropDamage);
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
        /// POSTHorticultureproductivity.
        /// </summary>
        /// <param name="horticultureproductivity">horticultureproductivity.</param>
        /// <returns>POSTHorticultureproductivity Response.</returns>
        [HttpPost("Horticultureproductivity")]
        public IActionResult POSTHorticultureproductivity([FromBody] List<Horticultureproductivity> horticultureproductivity)
        {
            try
            {
                int result = this.cropService.POSTHorticultureproductivity(horticultureproductivity);
                if (result == 1)
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
        /// PostCropDamageReason.
        /// </summary>
        /// <param name="damageReasonPost">damageReasonPost.</param>
        /// <returns>PostCropDamageReason Response.</returns>
        [HttpPost("PostCropDamageReason")]
        public IActionResult PostCropDamageReason([FromBody] DamageReasonPost damageReasonPost)
        {
            try
            {
                int result = this.cropService.PostCropDamageReason(damageReasonPost);
                if (result == 1)
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
        /// PostCropDamageName.
        /// </summary>
        /// <param name="cropPost">cropPost.</param>
        /// <returns>PostCropDamageName List.</returns>
        [HttpPost("PostCropDamageName")]
        public IActionResult PostCropDamageName([FromBody] CropPost cropPost)
        {
            try
            {
                int result = this.cropService.PostCropDamageName(cropPost);
                if (result == 1)
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
        /// PostDamageDetails.
        /// </summary>
        /// <param name="damageDetails">damageDetails.</param>
        /// <returns>PostDamageDetails List.</returns>
        [HttpPost("PostDamageDetails")]
        public IActionResult PostDamageDetails([FromBody] DamageDetails damageDetails)
        {
            try
            {
                List<DtoPostDamageResponse> result = this.cropService.PostDamageDetails(damageDetails);

                return this.Ok(result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.Problem(ex.Message);
            }
        }

        /// <summary>
        /// PostEditDamageDetails.
        /// </summary>
        /// <param name="damageDetails">damageDetails.</param>
        /// <returns>PostEditDamageDetails List.</returns>
        [HttpPost("PostEditDamageDetails")]
        public IActionResult PostEditDamageDetails([FromBody] EditDamageDetails damageDetails)
        {
            try
            {
                List<DtoPostDamageResponse> result = this.cropService.PostEditDamageDetails(damageDetails);

                return this.Ok(result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.Problem(ex.Message);
            }
        }

        /// <summary>
        /// PostDelDamageReasonList.
        /// </summary>
        /// <param name="damage">damage.</param>
        /// <returns>PostDelDamageReasonList List.</returns>
        [HttpPost("PostDelDamageReasonList")]
        public IActionResult PostDelDamageReasonList(DtoEditDamageRequest damage)
        {
            try
            {
                int result = this.cropService.PostDelDamageReasonList(damage);
                if (result == 1)
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
        /// PostUpdateDamageReasonStatus.
        /// </summary>
        /// <param name="cropDamageGetModel">cropDamageGetModel.</param>
        /// <returns>PostUpdateDamageReasonStatus List.</returns>
        [HttpPost("PostUpdateDamageReasonStatus")]
        public IActionResult PostUpdateDamageReasonStatus([FromBody] DtoDamageStatusDetails cropDamageGetModel)
        {
            try
            {
                int result = this.cropService.PostUpdateDamageReasonStatus(cropDamageGetModel);
                if (result == 1)
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
        /// PostCropCoverageDamageDetails.
        /// </summary>
        /// <param name="insCropCoverageDamageDetails">insCropCoverageDamageDetails.</param>
        /// <returns>PostCropCoverageDamageDetails List.</returns>
        [HttpPost("PostCropCoverageDamageDetails")]
        public IActionResult PostCropCoverageDamageDetails([FromBody] InsCropCoverageDamageDetails insCropCoverageDamageDetails)
        {
            try
            {
                int result = this.cropService.PostCropCoverageDamageDetails(insCropCoverageDamageDetails);
                if (result == 1)
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
        /// PostCropCvgDamagePancytApproval.
        /// </summary>
        /// <param name="cropDamageGetAll">cropDamageGetAll.</param>
        /// <returns>PostCropCvgDamagePancytApproval List.</returns>
        [HttpPost("PostCropCvgDamagePancytApproval")]
        public IActionResult PostCropCvgDamagePancytApproval([FromBody] CropDamageGetAll cropDamageGetAll)
        {
            try
            {
                CropResponce result = this.cropService.PostCropCvgDamagePancytApproval(cropDamageGetAll);

                return this.Ok(result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.Problem(ex.Message);
            }
        }

        /// <summary>
        /// PostBAOCropDamageApproval.
        /// </summary>
        /// <param name="insBAOCropdmgModel">insBAOCropdmgModel.</param>
        /// <returns>PostBAOCropDamageApproval Response.</returns>
        [HttpPost("PostBAOCropDamageApproval")]
        public IActionResult PostBAOCropDamageApproval([FromBody] InsBaoCropdmgModel insBAOCropdmgModel)
        {
            try
            {
                CropResponce result = this.cropService.PostBAOCropDamageApproval(insBAOCropdmgModel);
                return this.Ok(result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.Problem(ex.Message);
            }
        }

        /// <summary>
        /// PostDAOCropDamageApproval.
        /// </summary>
        /// <param name="insBAOCropdmgModel">insBAOCropdmgModel.</param>
        /// <returns>PostDAOCropDamageApproval List.</returns>
        [HttpPost("PostDAOCropDamageApproval")]
        public IActionResult PostDAOCropDamageApproval([FromBody] InsBaoCropdmgModel insBAOCropdmgModel)
        {
            try
            {
                CropResponce result = this.cropService.PostDAOCropDamageApproval(insBAOCropdmgModel);

                return this.Ok(result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.Problem(ex.Message);
            }
        }

        /// <summary>
        /// PostSeedUserInput.
        /// </summary>
        /// <param name="crop">crop.</param>
        /// <returns>PostSeedUserInput Response.</returns>
        [HttpPost("PostSeedUserInput")]
        public IActionResult PostSeedUserInput([FromBody] SeedUsedInput crop)
        {
            try
            {
                int result = this.cropService.InsertSeedUsedInput(crop);

                if (result == 0)
                {
                    return this.NotFound("{\"status\": \"Insertion Failed\"}");
                }

                return this.Ok("{\"status\": \"Success\"}");
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound("{\"status\": \"Insertion Failed\"}");
            }
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
        /// <returns> GetSeedPerformanceDataReport Response.</returns>
        [HttpGet("GetSeedPerformanceDataReport/{season}/{scheme}/{status}/{district}/{block}/{panchayat}/{activity}/{year}/{crop_type}")]
        public IActionResult GetSeedPerformanceDataReport(int year, int season, string scheme, string activity, string status, string district, string block, string panchayat, string crop_type)
        {
            try
            {
                List<string> result = this.cropService.GetSeedPerformanceDataReport(year, season, scheme, activity, status, district, block, panchayat, crop_type);
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
        /// To Get Combine Pass Data Report.
        /// </summary>
        /// <param name="combinePassModel">combinePassModel.</param>
        /// <returns>list of string.</returns>
        [HttpPost("GetCombinePassDataReport")]
        public IActionResult GetCombinePassDataReport([FromBody] CombinePassInputModel combinePassModel)
        {
            try
            {
                List<string> result = this.cropService.GetCombinePassDataReport(combinePassModel);
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
        /// To Get Agri Asset Data Report.
        /// </summary>
        /// <param name="agriasstmodel">agriasstmodel.</param>
        /// <returns>list of string.</returns>
        [HttpPost("GetAgriAsstDataReport")]
        public IActionResult GetAgriAsstDataReport([FromBody] AgricultureAssetManagement agriasstmodel)
        {
            try
            {
                List<string> result = this.cropService.GetAgriAsstDataReport(agriasstmodel);
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
        /// GetPiasDataReport.
        /// </summary>
        /// <param name="piasDataReportInputModel">piasDataReportInputModel.</param>
        /// <returns>list of string.</returns>
        [HttpPost("GetPiasDataReport")]
        public IActionResult GetPiasDataReport([FromBody] GetPiasDataReportInputModel piasDataReportInputModel)
        {
            try
            {
                List<string> result = this.cropService.GetPiasDataReport(piasDataReportInputModel);
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
        /// PostSeedDemandAc.
        /// </summary>
        /// <param name="crop">crop.</param>
        /// <returns>PostSeedDemandAc Response.</returns>
        [HttpPost("PostSeedDemandAc")]
        public IActionResult PostSeedDemandAc([FromBody] SeedIndentInput crop)
        {
            try
            {
                int result = this.cropService.PostSeedDemandAc(crop);
                if (result > 0)
                {
                    return this.Ok("{\"status\": \"success\", \"reason\": \"Data submitted successfully\"}");
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
        /// GetSeedDemandACViewSubmission.
        /// </summary>
        /// <param name="seasonId">seasonId.</param>
        /// <param name="cropId">cropId.</param>
        /// <param name="districtId">districtId.</param>
        /// <param name="panchayatId">panchayatId.</param>
        /// <returns>DtoSeedIndentInput.</returns>
        [HttpGet("GetSeedDemandACViewSubmission/{seasonId}/{cropId}/{districtId}/{panchayatId}")]
        public IActionResult GetSeedDemandACViewSubmission(int seasonId, int cropId, int districtId, int panchayatId)
        {
            try
            {
                DtoSeedIndentInput result = this.cropService.GetSeedDemandACViewSubmission(seasonId, cropId, districtId, panchayatId);

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
        /// GetSeedDemandAcViewSubmissionOffline.
        /// </summary>
        /// <param name="districtId">districtId.</param>
        /// <param name="panchayatId">panchayatId.</param>
        /// <returns>GetSeedDemandAcViewSubmissionOffline List.</returns>
        [HttpGet("GetSeedDemandAcViewSubmissionOffline/{districtId}/{panchayatId}")]
        public IActionResult GetSeedDemandAcViewSubmissionOffline(int districtId, int panchayatId)
        {
            try
            {
                List<DtoSeedIndentInput> result = this.cropService.GetSeedDemandAcViewSubmissionOffline(districtId, panchayatId);

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

        /// <summary>
        /// GetSeedUsedVarietynameAC.
        /// </summary>
        /// <param name="seasonId">seasonId.</param>
        /// <param name="cropId">cropId.</param>
        /// <param name="districtId">districtId.</param>
        /// <param name="panchayatId">panchayatId.</param>
        /// <returns>GetSeedUsedVarietynameAC List.</returns>
        [HttpGet("GetSeedUsedVarietynameAC/{seasonId}/{cropId}/{districtId}/{panchayatId}")]
        public IActionResult GetSeedUsedVarietynameAC(int seasonId, int cropId, int districtId, int panchayatId)
        {
            try
            {
                DtoSeedIndentInput result = this.cropService.GetSeedUsedVarietynameAC(seasonId, cropId, districtId, panchayatId);

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
        /// GetAllMarketByDistrict.
        /// </summary>
        /// <param name="districtId">districtId.</param>
        /// <returns>MarketData list.</returns>
        [HttpGet("GetAllMarketByDistrict/{districtId}")]
        public IActionResult GetAllMarketByDistrict(string districtId)
        {
            try
            {
                List<MarketData> result = this.cropService.GetAllMarketByDistrict(districtId);

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
        /// PostPlantIndent.
        /// </summary>
        /// <param name="input">input.</param>
        /// <returns>PostPlantIndent Response.</returns>
        [HttpPost("PostPlantIndent")]
        public IActionResult PostPlantIndent([FromBody] PlantIndentInput input)
        {
            try
            {
                int result = this.cropService.PostPlantIndent(input);
                if (result > 0)
                {
                    return Ok("{\"status\": \"success\", \"reason\": \"Data submitted successfully\"}");
                }
                else
                {
                    return NotFound("{\"status\": \"Insertion Failed\"}");
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return NotFound("{\"status\": \"Insertion Failed\"}");
            }
        }

        /// <summary>
        /// GetSeedDemandBHOViewSubmission.
        /// </summary>
        /// <param name="seasonId">seasonId.</param>
        /// <param name="cropId">cropId.</param>
        /// <param name="blockId">blockId.</param>
        /// <param name="panchayatId">panchayatId.</param>
        /// <returns>DtoPlantIndentInput.</returns>
        [HttpGet("GetSeedDemandBHOViewSubmission/{seasonId}/{cropId}/{blockId}/{panchayatId}")]
        public IActionResult GetSeedDemandBHOViewSubmission(int seasonId, int cropId, string blockId, int panchayatId)
        {
            try
            {
                DtoPlantIndentInput result = this.cropService.GetSeedDemandBHOViewSubmission(seasonId, cropId, blockId, panchayatId);

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
        /// GetSeedDemandBHOViewSubmissionOffline.
        /// </summary>
        /// <param name="districtId">districtId.</param>
        /// <param name="seasonId">seasonId.</param>
        /// <param name="blockId">blockId.</param>
        /// <returns>DtoPlantIndentInput List.</returns>
        [HttpGet("GetSeedDemandBHOViewSubmissionOffline/{districtId}/{seasonId}/{blockId}")]
        public IActionResult GetSeedDemandBHOViewSubmissionOffline(int districtId, int seasonId, string blockId)
        {
            try
            {
                List<DtoPlantIndentInput> result = this.cropService.GetSeedDemandBHOViewSubmissionOffline(districtId, seasonId, blockId);

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

        /// <summary>
        /// GetSeedUsedVarietyNameBHO.
        /// </summary>
        /// <param name="seasonId">seasonId.</param>
        /// <param name="cropId">cropId.</param>
        /// <param name="districtId">districtId.</param>
        /// <param name="blockId">blockId.</param>
        /// <param name="panchayatId">panchayatId.</param>
        /// <returns>Values.</returns>
        [HttpGet("GetSeedUsedVarietyNameBHO/{seasonId}/{cropId}/{districtId}/{blockId}/{panchayatId}")]
        public IActionResult GetSeedUsedVarietyNameBHO(int seasonId, int cropId, int districtId, string blockId, int panchayatId)
        {
            try
            {
                DtoPlantIndentInput result = this.cropService.GetSeedUsedVarietyNameBHO(seasonId, cropId, districtId, blockId, panchayatId);

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
        /// GetFutureSeason.
        /// </summary>
        /// <returns>FutureSeason List.</returns>
        [HttpGet("GetFutureSeason")]
        public IActionResult GetFutureSeason()
        {
            try
            {
                List<FutureSeason> result = this.cropService.GetFutureSeason();
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
        /// GetCropName.
        /// </summary>
        /// <param name="seasonId">seasonId.</param>
        /// <returns>Crop Name List.</returns>
        [HttpGet("GetCropName/{seasonId}")]
        public IActionResult GetCropName(int seasonId)
        {
            try
            {
                List<CropNames> result = this.cropService.GetCropName(seasonId);
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
        /// GetPlantName.
        /// </summary>
        /// <param name="plantCategory">plantCategory.</param>
        /// <param name="seasonId">seasonId.</param>
        /// <returns>Plant Name List.</returns>
        [HttpGet("GetPlantName/{plantCategory}/{seasonId}")]
        public IActionResult GetPlantName(string plantCategory, int seasonId)
        {
            try
            {
                List<PlantNames> result = this.cropService.GetPlantName(plantCategory, seasonId);
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
        /// GetCropSeedVariety.
        /// </summary>
        /// <param name="cropId">cropId.</param>
        /// <param name="seasonId">seasonId.</param>
        /// <returns>Seed Variety List.</returns>
        [HttpGet("GetCropSeedVariety/{cropId}/{seasonId}")]
        public IActionResult GetCropSeedVariety(int cropId, int seasonId)
        {
            try
            {
                List<SeedVarietyList> result = this.cropService.GetCropSeedVariety(cropId, seasonId);
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
        /// GetPlantSeedVariety.
        /// </summary>
        /// <param name="cropId">cropId.</param>
        /// <param name="seasonId">seasonId.</param>
        /// <returns>Seed Variety List.</returns>
        [HttpGet("GetPlantSeedVariety/{cropId}/{seasonId}")]
        public IActionResult GetPlantSeedVariety(int cropId, int seasonId)
        {
            try
            {
                List<SeedVarietyList> result = this.cropService.GetPlantSeedVariety(cropId, seasonId);
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
        /// GetCropCategoryBySeason.
        /// </summary>
        /// <param name="seasonId">seasonId.</param>
        /// <returns>Crop Category List.</returns>
        [HttpGet("GetCropCategoryBySeason/{seasonId}")]
        public IActionResult GetCropCategoryBySeason(int seasonId)
        {
            try
            {
                List<CropCategoryEntity> result = this.cropService.GetCropCategoryBySeason(seasonId);
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
        [HttpGet("GetInputIndentDataReport/{year}/{seasonId}/{activity}/{cropVarietyId}/{plantCategory}/{cropId}/{district}/{block}/{panchayat}")]
        public IActionResult GetInputIndentDataReport(int year, string seasonId, string activity, string cropVarietyId, string plantCategory, string cropId, string district, string block, string panchayat)
        {
            try
            {
                List<string> result = this.cropService.GetInputIndentDataReport(year, seasonId, activity, cropVarietyId, plantCategory, cropId, district, block, panchayat);
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
    }
}
