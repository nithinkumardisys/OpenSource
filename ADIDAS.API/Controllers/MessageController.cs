//------------------------------------------------------------------------------
// <copyright file="MessageController.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------

namespace ADIDAS.API.Controllers
{
    using System;
    using System.Collections.Generic;
    using ADIDAS.Model.Entities;
    using ADIDAS.Service.Interfaces;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// MessageController.
    /// </summary>
    [Authorize]
    [Route("/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IMessageService messageService;
        private readonly ILogger<MessageController> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="MessageController"/> class.
        /// MessageController.
        /// </summary>
        /// <param name="messageService">messageService.</param>
        /// <param name="logger">logger.</param>
        public MessageController(IMessageService messageService, ILogger<MessageController> logger)
        {
            this.messageService = messageService;
            this.logger = logger;
        }

        /// <summary>
        /// GetMessage.
        /// </summary>
        /// <returns>MessageEntity.</returns>
        [HttpGet("GetMessage")]
        public IActionResult GetMessage()
        {
            try
            {
                List<MessageEntity> result = this.messageService.GetMessage();
                if (result == null)
                {
                    return this.Unauthorized("Authentication Failed");
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
        /// PostMessage.
        /// </summary>
        /// <param name="message">message.</param>
        /// <returns>messageService Result.</returns>
        [HttpPost("PostMessage")]
        public IActionResult PostMessage([FromBody] MessageEntity message)
        {
            try
            {
                int result = this.messageService.PostMessage(message);

                if (result == 0)
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
        /// DeleteMessage.
        /// </summary>
        /// <param name="message">message.</param>
        /// <returns>DeleteMessage Response.</returns>
        [HttpDelete("DeleteMessage")]
        public IActionResult DeleteMessage([FromBody] MessageEntity message)
        {
            try
            {
                int result = this.messageService.DeleteMessage(message);

                if (result == 0)
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
