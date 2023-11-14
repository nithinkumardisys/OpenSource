//--------------------------------------------------------------------------------
// <copyright file="ErrorController.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//--------------------------------------------------------------------------------
namespace ADIDAS.API.Controllers
{
    using System;
    using ADIDAS.Model.DTO;
    using ADIDAS.Service.Interfaces;
    using Google.Apis.Logging;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// ErrorController.
    /// </summary>
    [AllowAnonymous]
    [ApiController]
    [Route("[controller]")]
    public class ErrorController : Controller
    {
        private readonly IErrorService errorService;
        private readonly ILogger<ErrorController> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorController"/> class.
        /// ErrorController.
        /// </summary>
        /// <param name="errorService">errorService.</param>
        /// <param name="logger">logger.</param>
        public ErrorController(IErrorService errorService, ILogger<ErrorController> logger)
        {
            this.errorService = errorService;
            this.logger = logger;
        }

        /// <summary>
        /// PostAuditErrorLog.
        /// </summary>
        /// <param name="errorModel">errorModel.</param>
        /// <returns>PostAuditErrorLog Response.</returns>
        [HttpPost("PostAuditErrorLog")]
        public IActionResult PostAuditErrorLog([FromBody] ErrorModel errorModel)
        {
            try
            {
                bool result = this.errorService.AuditErrorLog(errorModel);
                return this.Ok(result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.Problem(ex.Message);
            }
        }
    }
}
