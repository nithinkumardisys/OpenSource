//------------------------------------------------------------------------------
// <copyright file="ExternalController.cs" company="Government of Bihar">
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
    using Google.Apis.Logging;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// ExternalController.
    /// </summary>
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class ExternalController : Controller
    {
        private readonly ILgDirService lgDirService;
        private readonly ILogger<ErrorController> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExternalController"/> class.
        /// ExternalController.
        /// </summary>
        /// <param name="lgDirService">lgDirService.</param>
        /// <param name="logger">logger.</param>
        public ExternalController(ILgDirService lgDirService, ILogger<ErrorController> logger)
        {
            this.lgDirService = lgDirService;
            this.logger = logger;
        }

        /// <summary>
        /// GetDistrictData.
        /// </summary>
        /// <returns>DistrictResponse.</returns>
        [HttpGet("GetDistrictData")]
        public DistrictResponse GetDistrictData()
        {
            DistrictResponse districtResponse = new DistrictResponse();
            try
            {
                List<District> districts = this.lgDirService.GetLGDirectoryDistrictData();

                if (districts.Any())
                {
                    districtResponse.Status = "200";
                    districtResponse.Message = "Success";
                    districtResponse.Districts = districts.Select(x => new DtoLgDistrict { DistrictCode = x.District_lg_code.ToString(), DistrictName = x.District_name }).ToList();
                }
                else
                {
                    districtResponse.Status = "100 ";
                    districtResponse.Message = "Invalid Input Parameters";
                }
            }
            catch (Exception ex)
            {
                districtResponse.Status = "101";
                districtResponse.Message = "Exception/Server error";
                this.logger.LogError(ex.Message);

                return districtResponse;
            }

            return districtResponse;
        }

        /// <summary>
        /// GetBlockData.
        /// </summary>
        /// <param name="districtCode">districtCode.</param>
        /// <returns>BlockResponse.</returns>
        [HttpGet("GetBlockData/{DistrictCode}")]
        public BlockResponse GetBlockData(string districtCode)
        {
            BlockResponse blocks = null;
            try
            {
                blocks = this.lgDirService.GetLGDirectoryBlockData(districtCode);

                if (blocks.Blocks != null)
                {
                    blocks.Status = "200";
                    blocks.Message = "Success";
                }
                else
                {
                    blocks.Status = "100 ";
                    blocks.Message = "Invalid Input Parameters";
                }
            }
            catch (Exception ex)
            {
                blocks.Status = "101";
                blocks.Message = "Exception/Server error";
                this.logger.LogError(ex.Message);
                return blocks;
            }

            return blocks;
        }

        /// <summary>
        /// GetPanchayatData.
        /// </summary>
        /// <param name="districtCode">districtCode.</param>
        /// <param name="blockCode">blockCode.</param>
        /// <returns>PanchayatResponse.</returns>
        [HttpGet("GetPanchayatData/{DistrictCode}/{BlockCode}")]
        public PanchayatResponse GetPanchayatData(string districtCode, string blockCode)
        {
            PanchayatResponse panchayatResponse = null;
            try
            {
                panchayatResponse = this.lgDirService.GetLGDirectoryPanchayatData(districtCode, blockCode);

                if (panchayatResponse.Panchayats != null)
                {
                    panchayatResponse.Status = "200";
                    panchayatResponse.Message = "Success";
                }
                else
                {
                    panchayatResponse.Status = "100 ";
                    panchayatResponse.Message = "Invalid Input Parameters";
                }
            }
            catch (Exception ex)
            {
                panchayatResponse.Status = "101";
                panchayatResponse.Message = "Exception/Server error";

                this.logger.LogError(ex.Message);

                return panchayatResponse;
            }

            return panchayatResponse;
        }

        /*/// <summary>
        /// GetVillageData.
        /// </summary>
        /// <param name="districtCode">districtCode.</param>
        /// <param name="blockCode">blockCode.</param>
        /// <param name="panchayatCode">panchayatCode.</param>
        /// <returns>VillageResponse.</returns>
        [HttpGet("GetVillageData/{districtCode}/{blockCode}/{panchayatCode}")]
        public VillageResponse GetVillageData(string districtCode, string blockCode, string panchayatCode)
        {
            VillageResponse villageResponse = null;
            try
            {
                villageResponse = this.lgDirService.GetLGDirectoryVillageData(districtCode, blockCode, panchayatCode);

                if (villageResponse.Villages != null)
                {
                    villageResponse.Status = "200";
                    villageResponse.Message = "Success";
                }
                else
                {
                    villageResponse.Status = "100 ";
                    villageResponse.Message = "Invalid Input Parameters";
                }
            }
            catch (Exception ex)
            {
                villageResponse.Status = "101";
                villageResponse.Message = "Exception/Server error";

                this.logger.LogError(ex.Message);

                return villageResponse;
            }

            return villageResponse;
        }*/
    }
}
