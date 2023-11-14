//---------------------------------------------------------------------------------
// <copyright file="CSReportDataController.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//---------------------------------------------------------------------------------

namespace ADIDAS.API.Controllers
{
    using System;
    using ADIDAS.Service.Interfaces;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// CSReportDataController.
    /// </summary>
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CSReportDataController : ControllerBase
    {
        private readonly ICSReportData csReportDataServices;
        private readonly ILogger<CSReportDataController> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="CSReportDataController"/> class.
        /// CSReportDataController.
        /// </summary>
        /// <param name="csReportDataServices">csReportDataServices.</param>
        public CSReportDataController(ICSReportData csReportDataServices, ILogger<CSReportDataController> logger)
        {
            this.csReportDataServices = csReportDataServices;
            this.logger = logger;
        }

        /// <summary>
        /// GetCSReportData.
        /// </summary>
        /// <param name="id">id.</param>
        /// <returns>GetCSReportData List.</returns>
        [HttpGet("GetCSReportData/{id}")]
        public IActionResult GetCSReportData(string id)
        {
            try
            {
                if (id == "ConsolidatedReport")
                {
                    var result = this.csReportDataServices.GetConsolidatedCSReports();
                    return this.Ok(result);
                }
                else
                {
                    var result = this.csReportDataServices.GetCSVReports(id);
                    return this.Ok(result);
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound("Not Found");
            }
        }
    }
}
