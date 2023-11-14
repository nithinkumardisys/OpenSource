//------------------------------------------------------------------------------
// <copyright file="NotificationController.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------
namespace ADIDAS.API.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Threading.Tasks;
    using ADIDAS.Model.DTO;
    using ADIDAS.Model.Entities;
    using ADIDAS.Service.Interfaces;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Azure.NotificationHubs;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;

    /// <summary>
    /// Notification Controller.
    /// </summary>
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly IMiscService miscService;
        private readonly IOptions<NotificationConfig> config;
        private readonly ILogger<NotificationController> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="NotificationController"/> class.
        /// Notification Controller.
        /// </summary>
        /// <param name="service">service.</param>
        /// <param name="config">config.</param>
        /// <param name="logger">logger.</param>
        public NotificationController(IMiscService service, IOptions<NotificationConfig> config, ILogger<NotificationController> logger)
        {
            this.miscService = service;
            this.config = config;
            this.logger = logger;
        }

        /// <summary>
        /// PUSH Notification To All Devices.
        /// </summary>
        /// <param name="entity">entity.</param>
        /// <returns>Action Result.</returns>
        [HttpPost("PUSHNotificationToAllDevices")]
        public async Task<IActionResult> PUSHNotificationToAllDevices(NotificationEntity entity)
        {
            try
            {
                string aps = '"' + "aps" + '"';
                string alert = '"' + "alert" + '"';
                string message = '"' + entity.Message + '"';

                string jsonApplePayload = @"{" + aps + ":{" + alert + ":" + message + "}}";

                string jsonAndroidPayload = @"{'data':{'message':'" + entity.Message + "'}}";
                NotificationHubClient hub = NotificationHubClient.CreateClientFromConnectionString(this.config.Value.ConnectionString, this.config.Value.HubName, false);
                if (entity.TagList != null)
                {
                    if (entity.TagList.Count != 0)
                    {
                        foreach (string tag in entity.TagList)
                        {
                            await hub.SendFcmNativeNotificationAsync(jsonAndroidPayload, tag);
                            await hub.SendAppleNativeNotificationAsync(jsonApplePayload, tag);
                            entity.Tags = tag;
                            this.miscService.InsertNotificationAudits(entity);
                        }
                    }
                    else
                    {
                        await hub.SendFcmNativeNotificationAsync(jsonAndroidPayload);
                        await hub.SendAppleNativeNotificationAsync(jsonApplePayload);
                        this.miscService.InsertNotificationAudits(entity);
                    }
                }
                else
                {
                    await hub.SendFcmNativeNotificationAsync(jsonAndroidPayload);
                    await hub.SendAppleNativeNotificationAsync(jsonApplePayload);
                    this.miscService.InsertNotificationAudits(entity);
                }

                return this.Ok("{status: Notification Pushed Successfully}");
            }
            catch (ConfigurationErrorsException ex)
            {
                this.logger.LogError(ex.Message);
                return this.Problem(ex.Message);
            }
        }

        /// <summary>
        /// PUSH Notification To Adroid.
        /// </summary>
        /// <param name="entity">entity.</param>
        /// <returns>Action Result.</returns>
        [HttpPost("PUSHNotificationToAdroid")]
        public async Task<IActionResult> PUSHNotificationToAdroid(NotificationEntity entity)
        {
            try
            {
                string jsonPayload = @"{'data':{'message':'" + entity.Message + "'}}";
                NotificationHubClient hub = NotificationHubClient.CreateClientFromConnectionString(this.config.Value.ConnectionString, this.config.Value.HubName, false);
                if (entity.TagList != null)
                {
                    if (entity.TagList.Count != 0)
                    {
                        foreach (string tag in entity.TagList)
                        {
                            await hub.SendFcmNativeNotificationAsync(jsonPayload, tag);
                            entity.Tags = tag;
                            this.miscService.InsertNotificationAudits(entity);
                        }
                    }
                    else
                    {
                        await hub.SendFcmNativeNotificationAsync(jsonPayload);
                        this.miscService.InsertNotificationAudits(entity);
                    }
                }
                else
                {
                    await hub.SendFcmNativeNotificationAsync(jsonPayload);
                    this.miscService.InsertNotificationAudits(entity);
                }

                return this.Ok("{status: Notification Pushed Successfully}");
            }
            catch (ConfigurationErrorsException ex)
            {
                this.logger.LogError(ex.Message);
                return this.Problem(ex.Message);
            }
        }

        /// <summary>
        /// PUSH Notification To Apple.
        /// </summary>
        /// <param name="entity">entity.</param>
        /// <returns>Action Result.</returns>
        [HttpPost("PUSHNotificationToApple")]
        public async Task<IActionResult> PUSHNotificationToApple(NotificationEntity entity)
        {
            try
            {
                string aps = '"' + "aps" + '"';
                string alert = '"' + "alert" + '"';
                string message = '"' + entity.Message + '"';

                string jsonPayload = @"{" + aps + ":{" + alert + ":" + message + "}}";
                NotificationHubClient hub = NotificationHubClient.CreateClientFromConnectionString(this.config.Value.ConnectionString, this.config.Value.HubName);

                if (entity.TagList != null)
                {
                    if (entity.TagList.Count != 0)
                    {
                        foreach (string tag in entity.TagList)
                        {
                            await hub.SendAppleNativeNotificationAsync(jsonPayload, tag);
                            entity.Tags = tag;
                            this.miscService.InsertNotificationAudits(entity);
                        }
                    }
                    else
                    {
                        await hub.SendAppleNativeNotificationAsync(jsonPayload);
                        this.miscService.InsertNotificationAudits(entity);
                    }
                }
                else
                {
                    await hub.SendAppleNativeNotificationAsync(jsonPayload);
                    this.miscService.InsertNotificationAudits(entity);
                }

                return this.Ok("{status: Notification Pushed Successfully}");
            }
            catch (ConfigurationErrorsException ex)
            {
                this.logger.LogError(ex.Message);
                return this.Problem(ex.Message);
            }
        }

        /// <summary>
        /// Get Notification Audits.
        /// </summary>
        /// <returns>Action Result.</returns>
        [HttpGet("GetNotificationAudits")]
        public IActionResult GetNotificationAudits()
        {
            try
            {
                List<NotificationEntity> result = this.miscService.GetNotificationAudits();
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
        /// Get All Registered Devices Async.
        /// </summary>
        /// <returns>Action Result.</returns>
        [HttpGet("GetRegisteredDevicesList")]
        public async Task<IActionResult> GetAllRegisteredDevicesAsync()
        {
            try
            {
                NotificationHubClient hub = NotificationHubClient.CreateClientFromConnectionString(this.config.Value.ConnectionString, this.config.Value.HubName);
                var allRegistrations = await hub.GetAllRegistrationsAsync(0);
                var continuationToken = allRegistrations.ContinuationToken;
                var registrationDescriptionsList = new List<RegistrationDescription>(allRegistrations);
                while (!string.IsNullOrWhiteSpace(continuationToken))
                {
                    var otherRegistrations = await hub.GetAllRegistrationsAsync(continuationToken, 0);
                    registrationDescriptionsList.AddRange(otherRegistrations);
                    continuationToken = otherRegistrations.ContinuationToken;
                }

                return this.Ok(registrationDescriptionsList);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.Problem(ex.Message);
            }
        }

        /// <summary>
        /// Delete Notification Audits.
        /// </summary>
        /// <param name="id">id.</param>
        /// <returns>Action Result.</returns>
        [HttpDelete("DeleteNotificationAudits")]
        public IActionResult DeleteNotificationAudits(int id)
        {
            try
            {
                int result = this.miscService.DeleteNotification(id);
                if (result == 0)
                {
                    return this.NotFound("{status: Delete Command Failed}");
                }
                else
                {
                    return this.Ok("{status: Data Successfully Deleted ");
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound("{status: Delete Command Failed}");
            }
        }

        /// <summary>
        /// Delete Notification Audits All.
        /// </summary>
        /// <returns>Action Result.</returns>
        [HttpDelete("DeleteNotificationAudits_All")]
        public IActionResult DeleteNotificationAudits_All()
        {
            try
            {
                int result = this.miscService.DeleteAllNotification();
                if (result == 0)
                {
                    return this.NotFound("{status: Delete Command Failed}");
                }
                else
                {
                    return this.Ok("{status: Data Successfully Deleted ");
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound("{status: Delete Command Failed}");
            }
        }
    }
}
