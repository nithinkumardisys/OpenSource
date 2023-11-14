//------------------------------------------------------------------------------
// <copyright file="FarmersOutreachServiceController.cs" company="Government of Bihar">
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
    /// FarmersOutreachServiceController.
    /// </summary>
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class FarmersOutreachServiceController : ControllerBase
    {
        private readonly IFarmersOutreachService farmersOutreachService;
        private readonly ILogger<BavasController> logger;
        private readonly IOptions<ExternalApi> config;

        /// <summary>
        /// Initializes a new instance of the <see cref="FarmersOutreachServiceController"/> class.
        /// AgmarknetController.
        /// </summary>
        /// <param name="farmersOutreachService">farmersOutreachService.</param>
        /// <param name="logger">logger.</param>
        /// <param name="config">config.</param>
        public FarmersOutreachServiceController(IFarmersOutreachService farmersOutreachService, ILogger<BavasController> logger, IOptions<ExternalApi> config)
        {
            this.logger = logger;
            this.farmersOutreachService = farmersOutreachService;
            this.config = config;
        }

        /// <summary>
        /// GetGenderList.
        /// </summary>
        /// <returns>GenderList.</returns>
        [HttpGet("GetGenderList")]
        public IActionResult GetGenderList()
        {
            try
            {
                List<GenderList> result = this.farmersOutreachService.GetGenderList();
                return this.Ok(result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound();
            }
        }

        /// <summary>
        /// GetFarmerDetails.
        /// </summary>
        /// <param name="regNo">regNo.</param>
        /// <returns>FarmerDetails.</returns>
        [HttpGet("GetFarmerDetails/{regNo}")]
        public IActionResult GetFarmerDetails(long regNo)
        {
            try
            {
                BiharAgariWebServiceSoapClient client = new BiharAgariWebServiceSoapClient(BiharAgariWebServiceSoapClient.EndpointConfiguration.BiharAgariWebServiceSoap);
                client.ClientCredentials.ServiceCertificate.SslCertificateAuthentication =
                new X509ServiceCertificateAuthentication()
                {
                    CertificateValidationMode = X509CertificateValidationMode.None,
                    RevocationMode = System.Security.Cryptography.X509Certificates.X509RevocationMode.NoCheck,
                };
                List<BiharAgriWebService.DBTFarmerRegistration> data = client.GetAgriFarmerDetailsAsync(regNo.ToString(), this.config.Value.Agriuserid, this.config.Value.Agripassword).Result.ToList();
                FarmerDetails farmerDetails = new FarmerDetails();
                if (data.Any())
                {
                    List<FarmerDetails> result = data.Select(x => new FarmerDetails()
                    {
                        Dbt_number = x.Registration_ID,
                        Farmer_name = x.FarmerName,
                        Gender_name = x.Gender,
                        Farmer_type_name = x.Farmertype,
                        Caste = x.CastCateogary,
                        District_name = x.DistrictName,
                        District_lg_code = x.DistrictCode_LG,
                        Block_name = x.BlockName,
                        Block_lg_code = x.BlockCode_LG,
                        Panchayat_name = x.PanchayatName,
                        Panchayat_lg_code = x.PanchayatCode_LG,
                        Village_name = x.VillageName,
                        Village_lg_code = x.VillageCode_LG,
                        Mobile_number = x.MobileNumber,
                        Is_available = true,
                    }).ToList();

                    switch (result[0].Gender_name.ToLower())
                    {
                        case "पुरुष":
                        case "male":
                            result[0].Gender_name = "male";
                            break;
                        case "स्त्री":
                        case "female":
                            result[0].Gender_name = "female";
                            break;
                        default:
                            result[0].Farmer_type_name = "other gender";
                            break;
                    }

                    switch (result[0].Caste.ToLower())
                    {
                        case "अनुसूचित जनजाति":
                        case "scheduled tribe":
                            result[0].Caste = "scheduled tribe";
                            break;
                        case "अनुसूचित जाति":
                        case "scheduled caste":
                            result[0].Caste = "scheduled caste";
                            break;
                        case "सामान्य":
                        case "general":
                            result[0].Caste = "general";
                            break;
                        case "अल्पसंख्यक":
                        case "minority":
                            result[0].Caste = "minority";
                            break;
                        case "अति पिछड़ा वर्ग":
                        case "most backward class":
                            result[0].Caste = "most backward class";
                            break;
                        case "पिछड़ा वर्ग":
                        case "backward class":
                            result[0].Caste = "backward class";
                            break;
                    }

                    if(result[0].Farmer_type_name.ToLower().Contains("सीमांत किसान"))
                    {
                        result[0].Farmer_type_name = "marginal Farmer";
                    }
                    else if (result[0].Farmer_type_name.ToLower().Contains("बृहत किसान"))
                    {
                        result[0].Farmer_type_name = "large Farmer";
                    }
                    else if (result[0].Farmer_type_name.ToLower().Contains("लघु किसान"))
                    {
                        result[0].Farmer_type_name = "small Farmer";
                    }

                    return this.Ok(result.FirstOrDefault());
                }
                else
                {
                    farmerDetails.Is_available = false;
                    return this.Ok(farmerDetails);
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound();
            }
        }

        /// <summary>
        /// GetCategory.
        /// </summary>
        /// <returns>CategoryList.</returns>
        [HttpGet("GetCategory")]
        public IActionResult GetCategory()
        {
            try
            {
                List<Category> result = this.farmersOutreachService.GetCategory();
                return this.Ok(result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound();
            }
        }

        /// <summary>
        /// GetFarmerCaste.
        /// </summary>
        /// <returns>FarmerCasteList.</returns>
        [HttpGet("GetFarmerCaste")]
        public IActionResult GetFarmerCaste()
        {
            try
            {
                List<FarmerCaste> result = this.farmersOutreachService.GetFarmerCaste();
                return this.Ok(result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound();
            }
        }

        /// <summary>
        /// GetFarmerType.
        /// </summary>
        /// <returns>FarmerTypeList.</returns>
        [HttpGet("GetFarmerType")]
        public IActionResult GetFarmerType()
        {
            try
            {
                List<FarmerTypes> result = this.farmersOutreachService.GetFarmerType();
                return this.Ok(result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound();
            }
        }

        /// <summary>
        /// GetTypeOfInteraction.
        /// </summary>
        /// <returns>TypeOfInteractionList.</returns>
        [HttpGet("GetTypeOfInteraction")]
        public IActionResult GetTypeOfInteraction()
        {
            try
            {
                List<TypeOfInteraction> result = this.farmersOutreachService.GetTypeOfInteraction();
                return this.Ok(result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound();
            }
        }

        /// <summary>
        /// GetFosData.
        /// </summary>
        /// <param name="user_id">user_id.</param>
        /// <param name="date_range_start">date_range_start.</param>
        /// <param name="date_range_end">date_range_end.</param>
        /// <param name="isResolved">isResolved.</param>
        /// <returns>FosData.</returns>
        [HttpGet("GetFosData/{user_id}/{date_range_start}/{date_range_end}/{isResolved}")]
        public IActionResult GetFosData(int user_id, DateTime date_range_start, DateTime date_range_end, string isResolved)
        {
            try
            {
                FosData result = this.farmersOutreachService.GetFosData(user_id, date_range_start, date_range_end, isResolved);
                return this.Ok(result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound();
            }
        }

        /// <summary>
        /// PostSingleFarmerData.
        /// </summary>
        /// <param name="fosFarmerData">fosFarmerData.</param>
        /// <returns>output.</returns>
        [HttpPost("PostSingleFarmerData")]
        public IActionResult PostSingleFarmerData([FromBody] FosFarmerData fosFarmerData)
        {
            try
            {
                int result = this.farmersOutreachService.PostSingleFarmerData(fosFarmerData);
                if (result > 0)
                {
                    return this.Ok("{\"status\": \"success\", \"reason\": \"Data submitted successfully\"}");
                }
                else
                {
                    return this.NotFound("{\"status\": \"Insertion Failed\"}");
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound("{\"status\": \"Insertion Failed\"}");
            }
        }

        /// <summary>
        /// PostGroupData.
        /// </summary>
        /// <param name="fosFarmerData">fosFarmerData.</param>
        /// <returns>output.</returns>
        [HttpPost("PostGroupData")]
        public IActionResult PostGroupData([FromBody] FosFarmerData fosFarmerData)
        {
            try
            {
                int result = this.farmersOutreachService.PostGroupData(fosFarmerData);
                if (result > 0)
                {
                    return this.Ok("{\"status\": \"success\", \"reason\": \"Data submitted successfully\"}");
                }
                else
                {
                    return this.NotFound("{\"status\": \"Insertion Failed\"}");
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound("{\"status\": \"Insertion Failed\"}");
            }
        }
    }
}
