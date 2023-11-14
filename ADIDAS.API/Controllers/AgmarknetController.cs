//------------------------------------------------------------------------------
// <copyright file="AgmarknetController.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------
namespace ADIDAS.API.Controllers
{
    using System;
    using ADIDAS.Service.Interfaces;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// AgmarknetController.
    /// </summary>
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class AgmarknetController : ControllerBase
    {
        private readonly IAgmarknetService agmarknetService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AgmarknetController"/> class.
        /// AgmarknetController.
        /// </summary>
        /// <param name="agmarknetService">agmarknetService.</param>
        public AgmarknetController(IAgmarknetService agmarknetService)
        {
            this.agmarknetService = agmarknetService;
        }

        /// <summary>
        /// Get Agmarknet Arrival Data.
        /// </summary>
        /// <param name="reportedDate">Reporting Date.</param>
        /// <returns>List Of Arrival Data.</returns>
        [HttpGet("GetAgmarknetArrivalData/{reportedDate}")]
        public IActionResult GetAgmarknetArrivalData(DateTime reportedDate)
        {
            try
            {
                return Ok(this.agmarknetService.GetAgmarknetArrivalData(reportedDate));
            }
            catch (Exception ex)
            {
                return this.Problem(ex.Message);
            }
        }

        /// <summary>
        /// Get Agmarknet Price Data.
        /// </summary>
        /// <param name="reportedDate">Reporting Date.</param>
        /// <returns>List Of Price Data.</returns>
        [HttpGet("GetAgmarknetPriceData/{reportedDate}")]
        public IActionResult GetAgmarknetPriceData(DateTime reportedDate)
        {
            try
            {
                return Ok(this.agmarknetService.GetAgmarknetPriceData(reportedDate));
            }
            catch (Exception ex)
            {
                return this.Problem(ex.Message);
            }
        }
    }
}
