//------------------------------------------------------------------------------
// <copyright file="SoilConservationController.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------
namespace ADIDAS.API.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.ServiceModel.Security;
    using ADIDAS.Model.DTO;
    using ADIDAS.Model.Entities;
    using ADIDAS.Service.Interfaces;
    using BiharAgriWebService;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;

    /// <summary>
    /// Soil Conservation Controller.
    /// </summary>
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class SoilConservationController : ControllerBase
    {
        /// <summary>
        /// ISoilConservationService
        /// </summary>
        private readonly ISoilConservationService soilConservationService;
        private readonly ILogger<SoilConservationController> logger;
        private readonly IOptions<ExternalApi> config;

        /// <summary>
        /// SoilConservationController
        /// </summary>
        /// <param name="SoilConservationService">SoilConservationService.</param>
        /// <param name="config">config.</param>
        /// <param name="logger">logger.</param>
        public SoilConservationController(ISoilConservationService soilConservationService, IOptions<ExternalApi> config, ILogger<SoilConservationController> logger)
        {
            this.soilConservationService = soilConservationService;
            this.config = config;
            this.logger = logger;
        }

        /// <summary>
        /// Get Yojna Number List
        /// </summary>
        /// <returns> YojnaNumberList</returns>
        [HttpGet("GetYojnaNumberList")]
        public IActionResult GetYojnaNumberList()
        {
            try
            {
                return this.Ok(this.soilConservationService.GetYojnaNumberList());
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound("Not Found");
            }
        }

        /// <summary>
        /// Get Yojna Name List
        /// </summary>
        /// <returns> YojnaNameList</returns>
        [HttpGet("GetYojnaNameList")]
        public IActionResult GetYojnaNameList()
        {
            try
            {
                return this.Ok(this.soilConservationService.GetYojnaNameList());
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound("Not Found");
            }
        }

        /// <summary>
        /// Get Dbt Mobile Number.
        /// </summary>
        /// <param name="registrationNumber">registrationNumber.</param>
        /// <returns> Dbt Mobile Number Details</returns>
        [HttpGet("GetSoilConservationAgriFarmerDetails/{registrationNumber}")]
        public IActionResult GetSoilConservationAgriFarmerDetails(string registrationNumber)
        {
            try
            {
                BiharAgariWebServiceSoapClient client = new BiharAgariWebServiceSoapClient(BiharAgariWebServiceSoapClient.EndpointConfiguration.BiharAgariWebServiceSoap);
                client.ClientCredentials.ServiceCertificate.SslCertificateAuthentication =
                new X509ServiceCertificateAuthentication()
                {
                    CertificateValidationMode = X509CertificateValidationMode.None,
                    RevocationMode = System.Security.Cryptography.X509Certificates.X509RevocationMode.NoCheck
                };
                List<BiharAgriWebService.DBTFarmerRegistration> data = client.GetAgriFarmerDetailsAsync(registrationNumber, this.config.Value.Agriuserid, this.config.Value.Agripassword).Result.ToList();

                List<SoilConservationDtbNumberResponse> result = new List<SoilConservationDtbNumberResponse>();
                if (data != null && data.Any())
                {
                    result = data.Select(x => new SoilConservationDtbNumberResponse()
                    {
                        DbtNumber = x.Registration_ID,
                        Name_of_principal_beneficiary = x.FarmerName,
                        District_id = x.DistrictCode_LG,
                        District_name = x.DistrictName,
                        Block_id = x.BlockCode_LG,
                        Block_name = x.BlockName,
                        Panchayat_id = x.PanchayatCode_LG,
                        Panchayat_name = x.PanchayatName,
                        Village_id = x.VillageCode_LG,
                        Village_name = x.VillageName,
                        Mobile_number = x.MobileNumber,
                        IsValid = true
                    }).ToList();
                }
                else
                {
                    result.Add(new SoilConservationDtbNumberResponse()
                    {
                        IsValid = false
                    });
                }

                return this.Ok(result.FirstOrDefault());
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound("Not Found");
            }
        }

        /// <summary>
        /// Get Structure Type Soil Conservation.
        /// </summary>
        /// <returns> Structure Type Soil Conservation Info.</returns>
        [HttpGet("GetStructureTypeSoilConservation")]
        public IActionResult GetStructureTypeSoilConservation()
        {
            try
            {
                return this.Ok(this.soilConservationService.GetStructureTypeSoilConservation());
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound("Not Found");
            }
        }

        /// <summary>
        /// Get Plant Soil Conservation.
        /// </summary>
        /// <returns> Plant Soil Conservation Info. </returns>
        [HttpGet("GetPlantSoilConservation")]
        public IActionResult GetPlantSoilConservation()
        {
            try
            {
                return this.Ok(this.soilConservationService.GetPlantSoilConservation());
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound("Not Found");
            }
        }

        /// <summary>
        /// Get Crop Soil Conservation.
        /// </summary>
        /// <returns> Crop Soil Conservation Info. </returns>
        [HttpGet("GetCropSoilConservation")]
        public IActionResult GetCropSoilConservation()
        {
            try
            {
                return this.Ok(this.soilConservationService.GetCropSoilConservation());
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound("Not Found");
            }
        }

        /// <summary>
        /// Get Input Type Soil Conservation.
        /// </summary>
        /// <returns> InputType Soil Conservation Info. </returns>
        [HttpGet("GetInputTypeSoilConservation")]
        public IActionResult GetInputTypeSoilConservation()
        {
            try
            {
                return this.Ok(this.soilConservationService.GetInputTypeSoilConservation());
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound("Not Found");
            }
        }

        /// <summary>
        /// Get Topic Name Soil Conservation
        /// </summary>
        /// <returns> Topic Name Soil Conservation Info </returns>
        [HttpGet("GetTopicNameSoilConservation")]
        public IActionResult GetTopicNameSoilConservation()
        {
            try
            {
                return this.Ok(this.soilConservationService.GetTopicNameSoilConservation());
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound("Not Found");
            }
        }

        /// <summary>
        /// Get Activity SubActivity Details
        /// </summary>
        /// <returns> Activity SubActivity List</returns>
        [HttpGet("GetActivitySubActivityDetails")]
        public IActionResult GetActivitySubActivityDetails()
        {
            try
            {
                return this.Ok(this.soilConservationService.GetActivitySubActivityDetails());
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound("Not Found");
            }
        }

        /// <summary>
        /// Get Soil Conservation Submitted Data
        /// </summary>
        /// <param name="panchayat_id">panchayat_id</param>
        /// <param name="scheme_id">scheme_id</param>
        /// <returns>GetSoilConservationSubmittedData</returns>
        [HttpGet("GetSoilConservationSubmittedData/{panchayat_id}/{scheme_id}")]
        public IActionResult GetSoilConservationSubmittedData(int panchayat_id, int scheme_id)
        {
            try
            {
                return this.Ok(this.soilConservationService.GetSoilConservationSubmittedData(panchayat_id, scheme_id));
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound("Not Found");
            }
        }

        /// <summary>
        /// GetSoilConservationNotification
        /// </summary>
        /// <param name="panchayat_id">panchayat_id</param>
        /// <returns>result</returns>
        [HttpGet("GetSoilConservationNotification/{panchayat_id}")]
        public IActionResult GetSoilConservationNotification(string panchayat_id)
        {
            try
            {
                return this.Ok(this.soilConservationService.GetSoilConservationNotification(panchayat_id));
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound("Not Found");
            }
        }

        /// <summary>
        /// Get Existing Physical Financial Target.
        /// </summary>
        /// <param name="panchayat_id">panchayat_id.</param>
        /// <returns>result.</returns>
        [HttpGet("GetExistingPhysicalFinancialTarget/{panchayat_id}")]
        public IActionResult GetExistingPhysicalFinancialTarget(int panchayat_id)
        {
            try
            {
                return this.Ok(this.soilConservationService.GetExistingPhysicalFinancialTarget(panchayat_id));
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound("Not Found");
            }
        }

        /// <summary>
        /// Post Soil Conservation.
        /// </summary>
        /// <param name="postSoilConservation">postSoilConservation.</param>
        /// <returns>result.</returns>
        [HttpPost("PostSoilConservation")]
        public IActionResult PostSoilConservation([FromBody] List<SoilConservationCreateRequest> postSoilConservation)
        {
            try
            {
                bool result = this.soilConservationService.PostSoilConservation(postSoilConservation);

                if (!result)
                {
                    return this.NotFound("{\"status\": \"Faild\" , \"reason\":\"Insertion Faild\"}");
                }
                else
                {
                    return this.Ok("{\"status\": \"Success\" , \"reason\":\"Data submitted successfully\"}");
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound("{\"status\": \"Faild\" , \"reason\":\"Insertion Faild\"}");
            }
        }
    }
}
