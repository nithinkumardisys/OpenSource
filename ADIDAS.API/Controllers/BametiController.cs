//------------------------------------------------------------------------------
// <copyright file="BametiController.cs" company="Government of Bihar">
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
    using DepartmentOfAgriculture.Admin.Models.DTO;
    using DepartmentOfAgriculture.Admin.Models.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// BametiController.
    /// </summary>
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class BametiController : ControllerBase
    {
        private readonly IBametiService bametiService;
        private readonly ILogger<BametiController> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="BametiController"/> class.
        /// BametiController.
        /// </summary>
        /// <param name="bametiService">bametiService.</param>
        /// <param name="logger">logger.</param>
        public BametiController(IBametiService bametiService, ILogger<BametiController> logger)
        {
            this.bametiService = bametiService;
            this.logger = logger;
        }

        /// <summary>
        /// PostBametiTemplate.
        /// </summary>
        /// <param name="obj">obj.</param>
        /// <returns>PostBametiTemplate Response.</returns>
        [HttpPost("PostBametiTemplate")]
        public IActionResult PostBametiTemplate([FromBody] BametiTemplate obj)
        {
            try
            {
                int result = this.bametiService.InsertBametiTemplate(obj);

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
        /// GetBametiScheme.
        /// </summary>
        /// <returns>GetBametiScheme List.</returns>
        [HttpGet("GetBametiScheme")]
        public IActionResult GetBametiScheme()
        {
            try
            {
                List<BametiScheme> result = this.bametiService.GetBametiSchemes();
                return this.Ok(result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.Problem(ex.Message);
            }
        }

        /// <summary>
        /// GetBametiSchemebyDesignation.
        /// </summary>
        /// <param name="designation">GetBametiScheme.</param>
        /// <returns>GetBametiSchemebyDesignation List.</returns>
        [HttpGet("GetBametiSchemebyDesignation/{designation}")]
        public IActionResult GetBametiSchemebyDesignation(string designation)
        {
            try
            {
                List<BametiScheme> result = this.bametiService.GetBametiSchemesbyDesignation(designation);

                return this.Ok(result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.Problem(ex.Message);
            }
        }

        /// <summary>
        /// GetBametiTargetUOM.
        /// </summary>
        /// <param name="schemeId">schemeId.</param>
        /// <param name="activityId">activityId.</param>
        /// <param name="districtId">districtId.</param>
        /// <returns>GetBametiTargetUOM List.</returns>
        [HttpGet("GetBametiTargetUOM/{schemeId}/{activityId}/{districtId}")]
        public IActionResult GetBametiTargetUOM(string schemeId, string activityId, string districtId)
        {
            try
            {
                TargetUom result = this.bametiService.GetBametiTargetUOM(Convert.ToInt32(schemeId), Convert.ToInt32(activityId), districtId);
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
        /// GetUOMBasedonSchemeActivity.
        /// </summary>
        /// <param name="schemeId">schemeId.</param>
        /// <param name="activityId">activityId.</param>
        /// <param name="period">period.</param>
        /// <returns>GetUOMBasedonSchemeActivity List.</returns>
        [HttpGet("GetUOMBasedonSchemeActivity/{schemeId}/{activityId}/{period}")]
        public IActionResult GetUOMBasedonSchemeActivity(string schemeId, string activityId, string period)
        {
            try
            {
                TargetUom result = this.bametiService.GetUOMBasedonSchemeActvity(Convert.ToInt32(schemeId), Convert.ToInt32(activityId), period);
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
        /// PostBametiTarget.
        /// </summary>
        /// <param name="setting">setting.</param>
        /// <returns>PostBametiTarget List.</returns>
        [HttpPost("PostBametiTarget")]
        public IActionResult PostBametiTarget([FromBody] List<PostTargetSetting> setting)
        {
            try
            {
                int result = this.bametiService.InsertTargetSetting(setting);

                if (result == 1)
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
        /// GetBametiActivity.
        /// </summary>
        /// <param name="schemeId">schemeId.</param>
        /// <returns>GetBametiActivity List.</returns>
        [HttpGet("GetBametiActivity/{schemeId}")]
        public IActionResult GetBametiActivity(string schemeId)
        {
            try
            {
                List<BametiActivity> result = this.bametiService.GetBametiActivities(schemeId);
                return this.Ok(result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.Problem(ex.Message);
            }
        }

        /// <summary>
        /// GetBametiTarget.
        /// </summary>
        /// <param name="request">request.</param>
        /// <returns>GetBametiTarget List.</returns>
        [HttpPost("GetBametiTarget")]
        public IActionResult GetBametiTarget([FromBody] BametiTargetRequestDto request)
        {
            try
            {
                List<BametiTargetGet> result = this.bametiService.GetBametiTarget(request);
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
        /// GetBametiActivitybyDesignation.
        /// </summary>
        /// <param name="schemeId">schemeId.</param>
        /// <param name="designation">designation.</param>
        /// <returns>GetBametiActivitybyDesignation List.</returns>
        [HttpGet("GetBametiActivitybyDesignation/{schemeId}/{designation}")]
        public IActionResult GetBametiActivitybyDesignation(string schemeId, string designation)
        {
            try
            {
                List<BametiActivity> result = this.bametiService.GetBametiActivities(schemeId, designation);

                return this.Ok(result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.Problem(ex.Message);
            }
        }

        /// <summary>
        /// GetBametiDesignation.
        /// </summary>
        /// <param name="schemeId">schemeId.</param>
        /// <param name="activityId">activityId.</param>
        /// <returns>GetBametiDesignation List.</returns>
        [HttpGet("GetBametiDesignation/{schemeId}/{activityId}")]
        public IActionResult GetBametiDesignation(string schemeId, string activityId)
        {
            try
            {
                List<string> result = this.bametiService.GetBametiDesignation(schemeId, activityId);

                return this.Ok(result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.Problem(ex.Message);
            }
        }

        /// <summary>
        /// GetBametiDesignation.
        /// </summary>
        /// <param name="schemeId">schemeId.</param>
        /// <param name="activityId">activityId.</param>
        /// <param name="designation">designation.</param>
        /// <returns>GetBametiDesignation List.</returns>
        [HttpGet("GetBametiFields/{schemeId}/{activityId}/{designation}")]
        public IActionResult GetBametiDesignation(string schemeId, string activityId, string designation)
        {
            try
            {
                List<string> result = this.bametiService.GetBametiFields(Convert.ToInt32(schemeId), Convert.ToInt32(activityId), designation);

                return this.Ok(result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.Problem(ex.Message);
            }
        }

        /// <summary>
        /// GetBametiTopic.
        /// </summary>
        /// <param name="designation">designation.</param>
        /// <param name="userId">userId.</param>
        /// <returns>IActionResult.</returns>
        [HttpGet("GetBametiTopic/{designation}/{userId}")]
        public IActionResult GetBametiTopic(string designation, string userId)
        {
            try
            {
                List<TopicField> result = this.bametiService.GetTopic(designation, userId);

                return this.Ok(result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.Problem(ex.Message);
            }
        }

        /// <summary>
        /// GetBametiAllFields.
        /// </summary>
        /// <returns>GetBametiAllFields List.</returns>
        [HttpGet("GetBametiAllFields")]
        public IActionResult GetBametiAllFields()
        {
            try
            {
                List<BametiAllFields> result = this.bametiService.GetBametiAllFields();
                return this.Ok(result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.Problem(ex.Message);
            }
        }

        /// <summary>
        /// DeleteBametiTemplate.
        /// </summary>
        /// <param name="template">template.</param>
        /// <returns>DeleteBametiTemplate Response.</returns>
        [HttpPost("DeleteBametiTemplate")]
        public IActionResult DeleteBametiTemplate(DeleteTemplate template)
        {
            try
            {
                int result = this.bametiService.DeleteBametiTemplate(template);
                if (result == 1)
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
        /// GetBametiTemplateAdmin.
        /// </summary>
        /// <param name="schemeId">schemeId.</param>
        /// <param name="activityId">activityId.</param>
        /// <param name="designation">designation.</param>
        /// <returns>GetBametiTemplateAdmin List.</returns>
        [HttpGet("GetBametiTemplateAdmin/{schemeId}/{activityId}/{designation}")]
        public IActionResult GetBametiTemplateAdmin(string schemeId, string activityId, string designation)
        {
            try
            {
                List<BametiTemplateGet> result = this.bametiService.GetBametiAdminTemplate(Convert.ToInt32(schemeId), Convert.ToInt32(activityId), designation);
                return this.Ok(result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.Problem(ex.Message);
            }
        }

        /// <summary>
        /// GetBametiCreateProgram.
        /// </summary>
        /// <param name="schemeId">schemeId.</param>
        /// <param name="activityId">activityId.</param>
        /// <param name="designation">designation.</param>
        /// <returns>GetBametiCreateProgram List.</returns>
        [HttpGet("GetBametiCreateProgram/{schemeId}/{activityId}/{designation}")]
        public IActionResult GetBametiCreateProgram(string schemeId, string activityId, string designation)
        {
            try
            {
                List<DtoCreateProgram> result = this.bametiService.GetBametiCreateProgram(Convert.ToInt32(schemeId), Convert.ToInt32(activityId), designation);
                return this.Ok(result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.Problem(ex.Message);
            }
        }

        /// <summary>
        /// GetBametiEditProgram.
        /// </summary>
        /// <param name="headerId">headerId.</param>
        /// <returns>GetBametiEditProgram List.</returns>
        [HttpGet("GetBametiEditProgram/{headerId}")]
        public IActionResult GetBametiEditProgram(string headerId)
        {
            try
            {
                BametiViewEditProgram result = this.bametiService.GetBametiEditProgram(headerId);
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
        /// GetBametiCreateProgramBasedonBeneType.
        /// </summary>
        /// <param name="schemeId">schemeId.</param>
        /// <param name="activityId">activityId.</param>
        /// <param name="designation">designation.</param>
        /// <param name="beneType">beneType.</param>
        /// <returns>GetBametiCreateProgramBasedonBeneType List.</returns>
        [HttpGet("GetBametiCreateProgramBasedonBeneType/{schemeId}/{activityId}/{designation}/{beneType}")]
        public IActionResult GetBametiCreateProgramBasedonBeneType(string schemeId, string activityId, string designation, string beneType)
        {
            try
            {
                List<DtoCreateProgram> result = this.bametiService.GetBametiCreateProgramBasedonBeneType(Convert.ToInt32(schemeId), Convert.ToInt32(activityId), designation, beneType);
                return this.Ok(result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.Problem(ex.Message);
            }
        }

        /// <summary>
        /// DeleteBametiGridData.
        /// </summary>
        /// <param name="templateId">templateId.</param>
        /// <param name="headerId">headerId.</param>
        /// <param name="rowno">rowno.</param>
        /// <returns>DeleteBametiGridData Response.</returns>
        [HttpGet("DeleteBametiGridData/{templateId}/{headerId}/{rowno}")]
        public IActionResult DeleteBametiGridData(string templateId, string headerId, string rowno)
        {
            try
            {
                int result = this.bametiService.DeleteBametiData(Convert.ToInt32(templateId), Convert.ToInt32(headerId), Convert.ToInt32(rowno));
                if (result == 1)
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
        /// GetBametiViewProgram.
        /// </summary>
        /// <param name="request">request.</param>
        /// <returns>GetBametiViewProgram List.</returns>
        [HttpPost("GetBametiViewProgram")]
        public IActionResult GetBametiViewProgram(DtoViewProgramRequest request)
        {
            try
            {
                BametiViewEditProgram result = this.bametiService.GetBametiViewProgram(request);
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
        /// GetBametiViewProgramSummary.
        /// </summary>
        /// <param name="request">request.</param>
        /// <returns>GetBametiViewProgramSummary List.</returns>
        [HttpPost("GetBametiViewProgramSummary")]
        public IActionResult GetBametiViewProgramSummary(DtoViewProgramRequest request)
        {
            try
            {
                DtosBametiViewProgram result = this.bametiService.GetBametiSummaryViewProgram(request);
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
        /// PostBametiGridData.
        /// </summary>
        /// <param name="request">request.</param>
        /// <returns>PostBametiGridData Response.</returns>
        [HttpPost("PostBametiGridData")]
        public IActionResult PostBametiGridData(HeaderDetailWrapper request)
        {
            try
            {
                int result = this.bametiService.InsertBametiGridData(request);

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
        /// EditBametiGridData.
        /// </summary>
        /// <param name="request">request.</param>
        /// <returns>EditBametiGridData Response.</returns>
        [HttpPost("EditBametiGridData")]
        public IActionResult EditBametiGridData(EditHeaderDetailWrapper request)
        {
            try
            {
                int result = this.bametiService.EditBametiGridData(request);

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
    }
}
