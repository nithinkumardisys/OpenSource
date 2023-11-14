//------------------------------------------------------------------------------
// <copyright file="CombinePassHarvesterController.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------

namespace ADIDAS.API.Controllers
{
    using System;
    using System.Collections.Generic;
    using ADIDAS.Model.DTO;
    using ADIDAS.Service.Interfaces;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// CombinePassHarvesterController.
    /// </summary>
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class CombinePassHarvesterController : Controller
    {
        /// <summary>
        /// ICombinePassHarvesterService.
        /// </summary>
        private ICombinePassHarvesterService _combinePassHarvesterService;

        /// <summary>
        /// logger.
        /// </summary>
        private readonly ILogger<AssetManagementController> logger;

        /// <summary>
        /// CombinePassHarvesterService.
        /// </summary>
        /// <param name="combinePassHarvesterRepository">combinePassHarvesterRepository.</param>
        /// <param name="logger">combinePassHarvesterRepository.</param>
        public CombinePassHarvesterController(ICombinePassHarvesterService combinePassHarvesterService, ILogger<AssetManagementController> logger)
        {
            _combinePassHarvesterService = combinePassHarvesterService;
            this.logger = logger;
        }

        /// <summary>
        /// GetCombinePassHarvester.
        /// </summary>
        /// <param name="seasonId">seasonId.</param>
        /// <param name="districtId">districtId.</param>
        /// <param name="machineTypeId">machineTypeId.</param>
        /// <returns>CombinePassHarvesterModelList.</returns>
        [HttpGet("GetCombinePassHarvester/{seasonId}/{districtId}/{machineTypeId}")]
        public IActionResult GetCombinePassHarvester(string seasonId, int districtId, int machineTypeId)
        {
            try
            {
                List<CombinePassHarvesterModel> result = _combinePassHarvesterService.GetCombinePassHarvester(seasonId, districtId, machineTypeId);
                if (result == null)
                {
                    return NotFound(Json("Not Found"));
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return NotFound(Json("Not Found"));
            }
        }

        /// <summary>
        /// GetMachineType.
        /// </summary>
        /// <returns>CombinePassMachinery.</returns>
        [HttpGet("GetMachineType")]
        public IActionResult GetMachineType()
        {
            try
            {
                List<CombinePassMachinery> result = _combinePassHarvesterService.GetMachineType();
                if (result == null)
                {
                    return NotFound(Json("Not Found"));
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return NotFound(Json("Not Found"));
            }
        }

        /// <summary>
        /// PostCombinePassHarvester.
        /// </summary>
        /// <param name="combinePassHarvesterModel"></param>
        /// <returns>int.</returns>
        [HttpPost("PostCombinePassHarvester")]
        public IActionResult PostCombinePassHarvester([FromBody] List<CombinePassHarvesterModel> combinePassHarvesterModel)
        {
            try
            {
                int result = _combinePassHarvesterService.PostCombinePassHarvester(combinePassHarvesterModel);
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
    }
}
