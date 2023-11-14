//------------------------------------------------------------------------------
// <copyright file="AnnualPlanOutlayController.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//-------------------------------------------------------------------------------
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
    /// AnnualPlanOutlayController.
    /// </summary>
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class AnnualPlanOutlayController : ControllerBase
    {
        private readonly IAnnualPlanOutlayService annualPlanOutlayService;

        private readonly ILogger<AnnualPlanOutlayController> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="AnnualPlanOutlayController"/> class.
        /// AnnualPlanOutlayController.
        /// </summary>
        /// <param name="annualPlanOutlayService">annualPlanOutlayService.</param>
        /// <param name="logger">logger.</param>
        public AnnualPlanOutlayController(IAnnualPlanOutlayService annualPlanOutlayService, ILogger<AnnualPlanOutlayController> logger)
        {
            this.annualPlanOutlayService = annualPlanOutlayService;
            this.logger = logger;
        }

        /// <summary>
        /// PostAnnualPlanOutlay.
        /// </summary>
        /// <param name="obj">obj.</param>
        /// <returns>IActionResult.</returns>
        [HttpPost("PostAnnualPlanOutlay")]
        public IActionResult PostAnnualPlanOutlay([FromBody] AnnualPlanOutlayModel obj)
        {
            try
            {
                int result = this.annualPlanOutlayService.InsertAnnualPlanOutlay(obj);

                if (result > 0)
                {
                    return this.Ok("{\"status\": \"Record Submitted Successfully\"}");
                }

                return this.NotFound("{\"status\": \"Insertion Failed\"}");
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound("{\"status\": \"Insertion Failed\"}");
            }
        }

        /// <summary>
        /// GetAnnualPlanOutplaySchemes.
        /// </summary>
        /// <returns>IActionResult.</returns>
        [HttpGet("GetAnnualPlanOutplaySchemes")]
        public IActionResult GetAnnualPlanOutplaySchemes()
        {
            try
            {
                List<AnnualPlanOutlaySchemes> result = this.annualPlanOutlayService.GetAnnualPlanOutlaySchemes();

                if (result.Any())
                {
                    return this.Ok(result);
                }

                return this.NotFound();
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound();
            }
        }

        /// <summary>
        /// GetAnnualPlanOutplayBudget.
        /// </summary>
        /// <param name="schemeId">schemeId.</param>
        /// <param name="user_id">user_id.</param>
        /// <returns>GetAnnualPlanOutplayBudget Values.</returns>
        [HttpGet("GetAnnualPlanOutplayBudget/{schemeId}/{user_id}")]
        public IActionResult GetAnnualPlanOutplayBudget(string schemeId, int user_id)
        {
            try
            {
                List<AnnualPlanOutlayBudgetHead> result = this.annualPlanOutlayService.GetAnnualPlanOutlayBudegt(Convert.ToInt32(schemeId), user_id);

                if (result.Any())
                {
                    return this.Ok(result);
                }

                return this.NotFound();
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound();
            }
        }

        /// <summary>
        /// IsAnnualPlanOutlayUser.
        /// </summary>
        /// <param name="user_id">user_id.</param>
        /// <returns>IsAnnualPlanOutlayUser IActionResult.</returns>
        [AllowAnonymous]
        [HttpGet("IsAnnualPlanOutlayUser/{user_id}")]
        public IActionResult IsAnnualPlanOutlayUser(string user_id)
        {
            try
            {
                List<DisburseEntity> result = this.annualPlanOutlayService.IsAnnualPlanUser(Convert.ToInt32(user_id));

                if (result.Any())
                {
                    return this.Ok(result);
                }

                return this.NotFound();
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound();
            }
        }

        /// <summary>
        /// GetAnnualPlanOfficerBasedonId.
        /// </summary>
        /// <param name="user_id">user_id.</param>
        /// <param name="designation">designation.</param>
        /// <returns>GetAnnualPlanOfficerBasedonId Values.</returns>
        [HttpGet("GetAnnualPlanOfficerBasedonId/{user_id}/{designation}")]
        public IActionResult GetAnnualPlanOfficerBasedonId(string user_id, string designation)
        {
            try
            {
                List<DisburseEntity> result = this.annualPlanOutlayService.GetDisburseOficers(Convert.ToInt32(user_id), designation);

                if (result.Any())
                {
                    return this.Ok(result);
                }

                return this.NotFound();
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound();
            }
        }

        /// <summary>
        /// GetBudgetHeadBySchemeForAllotment.
        /// </summary>
        /// <param name="schemeId">schemeId.</param>
        /// <param name="user_id">user_id.</param>
        /// <returns>GetBudgetHeadBySchemeForAllotment Values.</returns>
        [HttpGet("GetBudgetHeadBySchemeForAllotment/{schemeId}/{user_id}")]
        public IActionResult GetBudgetHeadBySchemeForAllotment(string schemeId, int user_id)
        {
            try
            {
                List<AnnualPlanOutlayBudgetHead> result = this.annualPlanOutlayService.GetBudgetHeadBySchemeForAllotment(Convert.ToInt32(schemeId), user_id);

                if (result.Any())
                {
                    return this.Ok(result);
                }

                return this.NotFound();
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound();
            }
        }

        /// <summary>
        /// GetAnnualPlanOutplaySummary.
        /// </summary>
        /// <param name="user_id">user_id.</param>
        /// <param name="district_id">district_id.</param>
        /// <returns>GetAnnualPlanOutplaySummary Values.</returns>
        [HttpGet("GetAnnualPlanOutplaySummary/{user_id}/{district_id}")]
        public IActionResult GetAnnualPlanOutplaySummary(int user_id, int district_id)
        {
            try
            {
                List<AnnualPlanOutlaySummary> result = this.annualPlanOutlayService.GetAnnualPlanOutlaySummary(user_id, district_id);

                if (result.Any())
                {
                    return this.Ok(result);
                }

                return this.NotFound();
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound();
            }
        }

        /// <summary>
        /// GetSpecificAnnualPlanBudgetScheme.
        /// </summary>
        /// <param name="schemeId">schemeId.</param>
        /// <param name="budgetId">budgetId.</param>
        /// <param name="user_id">user_id.</param>
        /// <returns>GetSpecificAnnualPlanBudgetScheme List.</returns>
        [HttpGet("GetSpecificAnnualPlanBudgetScheme/{schemeId}/{budgetId}/{user_id}")]
        public IActionResult GetSpecificAnnualPlanBudgetScheme(string schemeId, string budgetId, int user_id)
        {
            try
            {
                AnnualPlanOutlaySummary result = this.annualPlanOutlayService.GetAnnualPlanOutlayBySchemeBudget(Convert.ToInt32(schemeId), Convert.ToInt32(budgetId), user_id);
                if (result != null)
                {
                    return this.Ok(result);
                }

                return this.NotFound();
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound();
            }
        }

        /// <summary>
        /// GetDrawingDisperseOfficers.
        /// </summary>
        /// <returns>IActionResult.</returns>
        [AllowAnonymous]
        [HttpGet("GetDrawingDisperseOfficers")]
        public IActionResult GetDrawingDisperseOfficers()
        {
            try
            {
                List<GetDrawingDisperseOfficersModel> result = this.annualPlanOutlayService.GetDrawingDisperseOfficers();

                if (result != null)
                {
                    return this.Ok(result);
                }

                return this.NotFound();
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound();
            }
        }
    }
}
