//------------------------------------------------------------------------------
// <copyright file="DimensionController.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------
namespace ADIDAS.API.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using ADIDAS.Model.Entities;
    using ADIDAS.Service.Interfaces;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// DimensionController.
    /// </summary>
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class DimensionController : ControllerBase
    {
        private readonly ICropService cropService;
        private readonly ILgDirService lgdimService;
        private readonly ILogger<DimensionController> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="DimensionController"/> class.
        /// DimensionController.
        /// </summary>
        /// <param name="cropService">cropService.</param>
        /// <param name="lgservice">cropService.</param>
        /// <param name="logger">logger.</param>
        public DimensionController(ICropService cropService, ILgDirService lgservice, ILogger<DimensionController> logger)
        {
            this.cropService = cropService;
            this.lgdimService = lgservice;
            this.logger = logger;
        }

        /// <summary>
        /// GetCrop.
        /// </summary>
        /// <param name="seasonId">seasonId.</param>
        /// <param name="districtId">districtId.</param>
        /// <returns>GetCrop List.</returns>
        [AllowAnonymous]
        [HttpGet("GetCrop/{seasonId}/{districtId}")]
        public IActionResult GetCrop(string seasonId, string districtId)
        {
            try
            {
                List<CropSeasonEntity> result = this.cropService.GetCrop(seasonId, districtId);
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
        /// GetDistinctCrop.
        /// </summary>
        /// <param name="districtId">districtId.</param>
        /// <returns>GetDistinctCrop List.</returns>
        [HttpGet("GetDistinctCrop/{districtId}")]
        public IActionResult GetDistinctCrop(string districtId)
        {
            try
            {
                List<CropSeasonEntity> result = this.cropService.GetDistinctCrop(districtId);
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
        /// GetCropCategory.
        /// </summary>
        /// <param name="crop_Type">crop_Type.</param>
        /// <returns>GetCropCategory List.</returns>
        [HttpGet("GetCropCategory/{Crop_Type}")]
        public IActionResult GetCropCategory(string Crop_Type)
        {
            try
            {
                var result = this.cropService.GetCropCategory(Crop_Type);
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
        /// GetSeason.
        /// </summary>
        /// <returns>GetSeason List.</returns>
        [AllowAnonymous]
        [HttpGet("GetSeason")]
        public IActionResult GetSeason()
        {
            try
            {
                List<SeasonDim> result = this.cropService.GetSeason();
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
        /// GetLGDirectory.
        /// </summary>
        /// <returns>LgDirectoryPanchayatDim.</returns>
        [AllowAnonymous]
        [HttpGet("GetLGDirectory")]
        public IActionResult GetLGDirectory()
        {
            try
            {
                List<LgDirectoryPanchayatDim> result = this.cropService.GetLGDirectory();
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
        /// GetLGDirectoryDistrictsBlocks.
        /// </summary>
        /// <param name="districtId">districtId.</param>
        /// <returns>LgDirectoryPanchayatDim.</returns>
        [AllowAnonymous]
        [HttpGet("GetLGDirectoryDistrictsBlocks/{districtId}")]
        public IActionResult GetLGDirectoryDistrictsBlocks(int districtId)
        {
            try
            {
                List<LgDirectoryPanchayatDim> lGDirectoryPanchayatDim = this.cropService.GetLGDirectory(); // Collecting all the district, blocks loaded into List object

                // Grouping and get the districts and blocks which are associate with districts.
                List<DistrictsBlocks> result = lGDirectoryPanchayatDim.AsEnumerable().GroupBy(x => new { x.District_Id }).
                                                        Select(o => new DistrictsBlocks
                                                        {
                                                            // Populate Districts to class object
                                                            District_id = o.FirstOrDefault()?.District_Id,
                                                            District_name = o.FirstOrDefault()?.District_Name,

                                                            // Populate Blocks based on each districts filteration
                                                            Blocks = lGDirectoryPanchayatDim.AsEnumerable().GroupBy(x => new { x.Block_Id, x.District_Id }).
                                                                             Where(x => x.First().District_Id == o.FirstOrDefault()?.District_Id).
                                                                                 Select(o => new BlocksList
                                                                                 {
                                                                                     District_id = o.FirstOrDefault()?.District_Id,
                                                                                     Block_id = o.FirstOrDefault()?.Block_Id,
                                                                                     Block_name = o.FirstOrDefault()?.Block_Name,
                                                                                 }).OrderBy(x => x.Block_id)?.ToList(),
                                                        }).ToList();

                // We can filter the result by API parameter value of districtid, if we are passing no value then
                // returning all data
                if (districtId > 0)
                {
                    result = result.Where(x => x.District_id == districtId).ToList();
                }

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
        /// GetLGDirectoryDistrict.
        /// </summary>
        /// <param name="districtId">districtId.</param>
        /// <returns>LgDirectoryPanchayatDim.</returns>
        [AllowAnonymous]
        [HttpGet("GetLGDirectoryDistirct/{districtId}")]
        public IActionResult GetLGDirectoryDistrict(string districtId)
        {
            try
            {
                List<LgDirectoryPanchayatDim> result = this.cropService.GetLGDirectoryDistrict(districtId);
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
        /// GetLGDirectoryBlock.
        /// </summary>
        /// <param name="blockId">blockId.</param>
        /// <returns>LgDirectoryPanchayatDim.</returns>
        [AllowAnonymous]
        [HttpGet("GetLGDirectoryBlock/{blockId}")]
        public IActionResult GetLGDirectoryBlock(string blockId)
        {
            try
            {
                List<LgDirectoryPanchayatDim> result = this.cropService.GetLGDirectoryBlock(blockId);
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
        /// GetLGDirectoryPancht.
        /// </summary>
        /// <param name="panchayatId">panchayatId.</param>
        /// <returns>GetLGDirectoryPancht.</returns>
        [AllowAnonymous]
        [HttpGet("GetLGDirectoryPancht/{panchayatId}")]
        public IActionResult GetLGDirectoryPancht(string panchayatId)
        {
            try
            {
                List<LgDirectoryPanchayatDim> result = this.cropService.GetLGDirectoryPancht(panchayatId);
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
        /// GetLGDirectoryVillage.
        /// </summary>
        /// <param name="panchayatId">panchayatId.</param>
        /// <returns>LgDirectoryVillageDim List.</returns>
        [AllowAnonymous]
        [HttpGet("GetLGDirectoryVillage/{panchayatId}")]
        public IActionResult GetLGDirectoryVillage(int panchayatId)
        {
            try
            {
                List<LgDirectoryVillageDim> result = this.cropService.GetLGDirectoryVillage(panchayatId);
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
        /// GetLGDirectoryVillageByUserId.
        /// </summary>
        /// <param name="userId">userId.</param>
        /// <param name="panchayatId">panchayatId.</param>
        /// <returns>LgDirectoryVillageDim List.</returns>
        [AllowAnonymous]
        [HttpGet("GetLGDirectoryVillageByUserId/{userId}/{panchayatId}")]
        public IActionResult GetLGDirectoryVillageByUserId(int userId, int panchayatId)
        {
            try
            {
                List<LgDirectoryVillageDim> result = this.cropService.GetLGDirectoryVillageByUserId(userId, panchayatId);
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
        /// PostCropDim.
        /// </summary>
        /// <param name="crop">crop.</param>
        /// <returns>PostCropDim List.</returns>
        [HttpPost("PostCropDim")]
        public IActionResult PostCropDim([FromBody] CropSeasonEntity crop)
        {
            try
            {
                int result = this.cropService.InsertCropDim(crop);

                if (result == 0)
                {
                    return this.NotFound("{\"status\": \"Crop Addition Failed\"}");
                }
                else
                {
                    return this.Ok("{\"status\": \"Crop Added Successfully\"}");
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound("{\"status\": \"Crop Addition Failed\"}");
            }
        }

        /// <summary>
        /// GetLGDirectoryUserForm
        /// </summary>
        /// <returns> GetLGDirectoryUserForm List</returns>
        [AllowAnonymous]
        [HttpGet("GetLGDirectoryUserForm")]
        public IActionResult GetLGDirectoryUserForm()
        {
            try
            {
                List<LgDirectoryPanchayatDim> result = this.lgdimService.GetLGDirectoryUserForm();
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
    }
}
