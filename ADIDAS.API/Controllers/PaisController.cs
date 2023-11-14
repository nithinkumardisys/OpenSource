//------------------------------------------------------------------------------
// <copyright file="PaisController.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------

namespace ADIDAS.API.Controllers
{
    using System;
    using System.Collections.Generic;
    using ADIDAS.Model.DTO;
    using ADIDAS.Model.Entities;
    using ADIDAS.Service.Interfaces;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class PaisController : ControllerBase
    {
        private readonly IPaisService paisService;
        private readonly ILogger<PaisController> logger;

        public PaisController(IPaisService paisService, ILogger<PaisController> logger)
        {
            this.paisService = paisService;
            this.logger = logger;
        }

        /// <summary>
        /// Get Market
        /// </summary>
        /// <param name="userId">userId.</param>
        /// <returns>result.</returns>
        [HttpGet("GetMarket/{userId}")]
        public IActionResult GetMarket(string userId)
        {
            try
            {
                var result = this.paisService.GetMarket(userId);
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
        /// Get Commodity Group
        /// </summary>
        /// <returns>result.</returns>
        [HttpGet("GetCommodityGroup")]
        public IActionResult GetCommodityGroup()
        {
            try
            {
                var result = this.paisService.GetCommodityGroup();
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
        /// Get Units
        /// </summary>
        /// <returns>result.</returns>
        [HttpGet("GetUnits")]
        public IActionResult GetUnits()
        {
            try
            {
                var result = this.paisService.GetUnits();
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
        /// Get Commodity
        /// </summary>
        /// <param name="commodityGroupId">commodityGroupId.</param>
        /// <returns>result</returns>
        [HttpGet("GetCommodity/{commodityGroupId}")]
        public IActionResult GetCommodity(string commodityGroupId)
        {
            try
            {
                var result = this.paisService.GetCommodity(commodityGroupId);
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
        /// GetVariety
        /// </summary>
        /// <param name="commodityGroupId">commodityGroupId.</param>
        /// <param name="commodityId">commodityId.</param>
        /// <returns>result</returns>
        [HttpGet("GetVariety/{commodityGroupId}/{commodityId}")]
        public IActionResult GetVariety(string commodityGroupId, string commodityId)
        {
            try
            {
                var result = this.paisService.GetVariety(commodityGroupId, commodityId);
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
        /// Get Submitted Data
        /// </summary>
        /// <param name="marketId">marketId.</param>
        /// <param name="commodityGroupId">commodityGroupId.</param>
        /// <returns>result</returns>
        [HttpGet("GetSubmittedData/{marketId}/{commodityGroupId}")]
        public IActionResult GetSubmittedData(string marketId, string commodityGroupId)
        {
            try
            {
                var result = this.paisService.GetSubmittedData(marketId, commodityGroupId);
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
        /// Get Submitted Data Offline
        /// </summary>
        /// <param name="marketId">marketId.</param>
        /// <returns>result</returns>
        [HttpGet("GetSubmittedDataOffline/{marketId}")]
        public IActionResult GetSubmittedDataOffline(string marketId)
        {
            try
            {
                var result = this.paisService.GetSubmittedDataOffline(marketId);
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
        /// Get Arrival Details
        /// </summary>
        /// <param name="marketId">marketId.</param>
        /// <param name="selectedDate">selectedDate.</param>
        /// <param name="userId">userId.</param>
        /// <returns>result</returns>
        [HttpGet("GetArrivalDetails/{marketId}/{selectedDate}/{userId}")]
        public IActionResult GetArrivalDetails(string marketId, DateTime selectedDate, string userId)
        {
            try
            {
                var result = this.paisService.GetArrivalDetails(marketId, selectedDate, userId);
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
        /// Get Arrival Details Offline
        /// </summary>
        /// <param name="marketId">marketId.</param>
        /// <param name="userId">userId.</param>
        /// <returns>result</returns>
        [HttpGet("GetArrivalDetailsOffline/{marketId}/{userId}")]
        public IActionResult GetArrivalDetailsOffline(string marketId, string userId)
        {
            try
            {
                var result = this.paisService.GetArrivalDetailsOffline(marketId, userId);
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
        /// Get View Submission Data
        /// </summary>
        /// <param name="marketId">marketId.</param>
        /// <param name="selectedDate">selectedDate.</param>
        /// <param name="userId">userId.</param>
        /// <returns>result</returns>
        [HttpGet("GetViewSubmissionData/{marketId}/{selectedDate}/{userId}")]
        public IActionResult GetViewSubmissionData(string marketId, DateTime selectedDate, string userId)
        {
            try
            {
                var result = this.paisService.GetViewSubmissionData(marketId, selectedDate, userId);
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
        /// Get Anamolus Date
        /// </summary>
        /// <param name="marketId">marketId.</param>
        /// <param name="currentYear">currentYear.</param>
        /// <param name="userId">userId.</param>
        /// <returns>result</returns>
        [HttpGet("GetAnamolusDate/{marketId}/{currentYear}/{userId}")]
        public IActionResult GetAnamolusDate(string marketId, string currentYear, string userId)
        {
            try
            {
                var result = this.paisService.GetAnamolusDate(marketId, currentYear, userId);
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
        /// Get Anamolus Date Offline
        /// </summary>
        /// <param name="marketId">marketId.</param>
        /// <param name="userId">userId.</param>
        /// <returns>result</returns>
        [HttpGet("GetAnamolusDateOffline/{marketId}/{userId}")]
        public IActionResult GetAnamolusDateOffline(string marketId, string userId)
        {
            try
            {
                var result = this.paisService.GetAnamolusDateOffline(marketId, userId);
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
        /// Get Edit Price Data Anamolus
        /// </summary>
        /// <param name="marketId">marketId.</param>
        /// <param name="selectedDate">selectedDate.</param>
        /// <param name="userId">userId</param>
        /// <returns>result</returns>
        [HttpGet("GetEditPriceDataAnamolus/{marketId}/{selectedDate}/{userId}")]
        public IActionResult GetEditPriceDataAnamolus(string marketId, string selectedDate, string userId)
        {
            try
            {
                var result = this.paisService.GetEditPriceDataAnamolus(marketId, selectedDate, userId);
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
        /// Get Edit Price Data Anamolus Offline
        /// </summary>
        /// <param name="marketId">marketId.</param>
        /// <param name="userId">userId.</param>
        /// <returns>result</returns>
        [HttpGet("GetEditPriceDataAnamolusOffline/{marketId}/{userId}")]
        public IActionResult GetEditPriceDataAnamolusOffline(string marketId, string userId)
        {
            try
            {
                var result = this.paisService.GetEditPriceDataAnamolusOffline(marketId, userId);
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
        /// Post Add Commodity Variety
        /// </summary>
        /// <param name="paisLocalPrefencesInfo">paisLocalPrefencesInfo.</param>
        /// <returns>result</returns>
        [HttpPost("PostAddCommodityVariety")]
        public IActionResult PostAddCommodityVariety([FromBody] List<PaisLocalPrefencesInfo> paisLocalPrefencesInfo)
        {
            try
            {
                bool result = this.paisService.InsertCommodityVariety(paisLocalPrefencesInfo);
                if (!result)
                {
                    return this.NotFound("{\"status\": \"Commodity Variety Addition Failed\"}");
                }
                else
                {
                    return this.Ok("{\"status\": \"Commodity Variety Added Successfully\"}");
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound("{\"status\": \"Commodity Variety Addition Failed\"}");
            }
        }

        /// <summary>
        /// Post Delete Commodity
        /// </summary>
        /// <param name="deleteCommodities">deleteCommodities.</param>
        /// <returns>result</returns>
        [HttpPost("PostDeleteCommodity")]
        public IActionResult PostDeleteCommodity([FromBody] List<DeleteCommodityOrVariety> deleteCommodities)
        {
            try
            {
                bool result = this.paisService.DeleteCommodity(deleteCommodities);

                if (!result)
                {
                    return this.NotFound("{\"status\": \"Commodity Deletion Failed\"}");
                }
                else
                {
                    return this.Ok("{\"status\": \"Commodity Deleted Successfully\"}");
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound("{\"status\": \"Commodity Deletion Failed\"}");
            }
        }

        /// <summary>
        /// Post Delete Variety
        /// </summary>
        /// <param name="deleteCommodityOrVarieties">deleteCommodityOrVarieties.</param>
        /// <returns>result</returns>
        [HttpPost("PostDeleteVariety")]
        public IActionResult PostDeleteVariety([FromBody] List<DeleteCommodityOrVariety> deleteCommodityOrVarieties)
        {
            try
            {
                bool result = this.paisService.DeleteVariety(deleteCommodityOrVarieties);

                if (!result)
                {
                    return this.NotFound("{\"status\": \"Commodity Deletion Failed\"}");
                }
                else
                {
                    return this.Ok("{\"status\": \"Commodity Deleted Successfully\"}");
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound("{\"status\": \"Commodity Deletion Failed\"}");
            }
        }

        /// <summary>
        /// Post Add Arrival Data
        /// </summary>
        /// <param name="arrivalDetails">arrival Details.</param>
        /// <returns>result</returns>
        [HttpPost("PostAddArrivalData")]
        public IActionResult PostAddArrivalData([FromBody] List<ArrivalDetails> arrivalDetails)
        {
            try
            {
                bool result = this.paisService.InsertArrivalData(arrivalDetails);

                if (!result)
                {
                    return this.NotFound("{\"status\": \"Data Arrivals Addition Failed\"}");
                }
                else
                {
                    return this.Ok("{\"status\": \"Data Arrivals Added Successfully\"}");
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound("{\"status\": \"Data Arrivals Addition Failed\"}");
            }
        }

        /// <summary>
        /// Post Edit Price Data
        /// </summary>
        /// <param name="arrivalDetails">arrivalDetails.</param>
        /// <returns>result</returns>
        [HttpPost("PostEditPriceData")]
        public IActionResult PostEditPriceData([FromBody] List<ArrivalDetails> arrivalDetails)
        {
            try
            {
                bool result = this.paisService.EditPriceData(arrivalDetails);

                if (!result)
                {
                    return this.NotFound("{\"status\": \"Data price Addition Failed\"}");
                }
                else
                {
                    return this.Ok("{\"status\": \"Data Arrivals Added Successfully\"}");
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound("Not Found");
            }
        }

        /// <summary>
        /// Post Nil Transaction
        /// </summary>
        /// <param name="NilTransaction">NilTransaction.</param>
        /// <returns>result</returns>
        [HttpPost("PostNilTransaction")]
        public IActionResult PostNilTransaction([FromBody] List<NilTransaction> nilTransaction)
        {
            try
            {
                bool result = this.paisService.InsertNilTransaction(nilTransaction);

                if (!result)
                {
                    return this.NotFound("{\"status\": \"Nil Transaction Addition Failed\"}");
                }
                else
                {
                    return this.Ok("{\"status\": \"Nil Transaction Added Successfully\"}");
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound("{\"status\": \"Nil Transaction Addition Failed\"}");
            }
        }
    }
}

