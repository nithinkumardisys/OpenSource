//------------------------------------------------------------------------------
// <copyright file="MiscController.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------
namespace ADIDAS.API.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.IO;
    using System.Linq;
    using System.Net.Http;
    using System.Net.Http.Json;
    using System.ServiceModel.Security;
    using System.Text;
    using System.Threading.Tasks;
    using ADIDAS.Model.DTO;
    using ADIDAS.Model.Entities;
    using ADIDAS.Repository.Helper;
    using ADIDAS.Repository.Interfaces;
    using ADIDAS.Service.Interfaces;
    using Basocca;
    using BiharAgriWebService;
    using DepartmentOfAgriculture.Admin.Models.Models;
    using DIDAS.Model.Entities;
    using HorticultureService;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Http.Logging;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using Newtonsoft.Json;
    using OFMAS_Report;
    using ShareCare.Models.Models;
    using Swashbuckle.Swagger;
    using BlockLG = ADIDAS.Model.Entities.BlockLG;
    using DistrictLG = ADIDAS.Model.Entities.DistrictLG;
    using PanchayatLG = ADIDAS.Model.Entities.PanchayatLG;

    /// <summary>
    /// Misc Controller.
    /// </summary>
    //[Authorize]
    [Route("/[controller]")]
    [ApiController]
    public class MiscController : ControllerBase
    {
        private readonly IMiscService miscService;
        private readonly IMiscRepository miscRepository;
        private readonly IOptions<ExternalApi> config;
        private readonly ICropRepository cropRepository;
        private readonly IStorageService storageController;
        private readonly ILogger<MiscController> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="MiscController"/> class.
        /// MiscController.
        /// </summary>
        /// <param name="storageController">storageController.</param>
        /// <param name="miscService">miscService.</param>
        /// <param name="miscRepository">miscRepository.</param>
        /// <param name="config">config.</param>
        /// <param name="cropRepository">cropRepository.</param>
        /// <param name="logger">logger.</param>
        public MiscController(IStorageService storageController, IMiscService miscService, IMiscRepository miscRepository, IOptions<ExternalApi> config, ICropRepository cropRepository, ILogger<MiscController> logger)
        {
            this.miscService = miscService;
            this.miscRepository = miscRepository;
            this.config = config;
            this.cropRepository = cropRepository;
            this.storageController = storageController;
            this.logger = logger;
        }

        /// <summary>
        /// Get Reports.
        /// </summary>
        /// <returns>Action Result.</returns>
        [HttpGet("GetReports")]
        public IActionResult GetReports()
        {
            try
            {
                List<AppReport> result = this.miscService.GetReports();
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
        /// Get Pesticide License Info.
        /// </summary>
        /// <returns>Action Result.</returns>
        [HttpGet("GetPesticideLicenseInfo")]
        public IActionResult GetPesticideLicenseInfo()
        {
            try
            {
                List<PesticideInfo> result = this.miscService.GetPesticideLicenseInfo();
                if (!result.Any())
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
        /// Get DOA Instructions Info.
        /// </summary>
        /// <returns>Action Result.</returns>
        [HttpGet("GetDOAInstructionsInfo")]
        public IActionResult GetDOAInstructionsInfo()
        {
            try
            {
                List<DoaInstructions> result = this.miscService.GetDOAInstructions();
                if (!result.Any())
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
        /// Get Data FAQ.
        /// </summary>
        /// <returns>Action Result.</returns>
        [HttpGet("GetDataFAQ")]
        public IActionResult GetDataFAQ()
        {
            try
            {
                dynamic result = this.miscService.GetDataFAQ();
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
        /// Get Bihan Guidlines.
        /// </summary>
        /// <returns>Action Result.</returns>
        [HttpGet("GetBihanGuidlines")]
        public IActionResult GetBihanGuidlines()
        {
            try
            {
                List<BihanGuidelineModel> result = this.miscService.GetBihanGuidlines();
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
        /// Get App Link.
        /// </summary>
        /// <returns>Action Result.</returns>
        [AllowAnonymous]
        [HttpGet("GetAppLink")]
        public IActionResult GetAppLink()
        {
            try
            {
                List<AppLink> result = this.miscService.GetAppLink();
                if (!result.Any())
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
        /// Get Source.
        /// </summary>
        /// <param name="attributeName">attributeName.</param>
        /// <returns>Action Result.</returns>
        [AllowAnonymous]
        [HttpGet("GetSource/{attributeName}")]
        public IActionResult GetSource(string attributeName)
        {
            try
            {
                List<MobileAttributeConfig> result = this.miscService.GetSource(attributeName);
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
        /// Get All Designations.
        /// </summary>
        /// <returns>Action Result.</returns>
        [AllowAnonymous]
        [HttpGet("GetAllDesignations")]
        public IActionResult GetAllDesignations()
        {
            try
            {
                List<MobileAttributeConfig> result = this.miscService.GetAllDesignations();
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
        /// Get Release.
        /// </summary>
        /// <returns>Action Result.</returns>
        [AllowAnonymous]
        [HttpGet("GetReceipientDesignations")]
        public IActionResult GetReceipientDesignations()
        {
            try
            {
                List<MobileAttributeConfig> result = this.miscService.GetReceipientDesignations();
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
        /// GetRelease
        /// </summary>
        /// <returns>Action Result.</returns>
        [AllowAnonymous]
        [HttpGet("GetRelease")]
        public IActionResult GetRelease()
        {
            try
            {
                Release result = this.miscService.GetReleaseData();
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
        /// Post Source.
        /// </summary>
        /// <param name="appConfig">appConfig.</param>
        /// <returns>Action Result.</returns>
        [HttpPost("PostSource")]
        public IActionResult PostSource(AppConfig appConfig)
        {
            try
            {
                int result = this.miscService.PostSource(appConfig);
                if (result == 0)
                {
                    return this.NotFound("{\"status\": \"Insertion Failed\"}");
                }
                else
                {
                    return this.Ok("{\"status\": \"Data Successfully Inserted\"}");
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound("{\"status\": \"Insertion Failed\"}");
            }
        }

        /// <summary>
        /// Post Gamification Config.
        /// </summary>
        /// <param name="appConfig">appConfig.</param>
        /// <returns>Action Result.</returns>
        [HttpPost("PostGamificationConfig")]
        public IActionResult PostGamificationConfig(GamificationConfigDto appConfig)
        {
            try
            {
                int result = miscService.PostGamificationConfig(appConfig);
                if (result == 0)
                {
                    return this.NotFound("{\"status\": \"Insertion Failed\"}");
                }
                else
                {
                    return this.Ok("{\"status\": \"Data Successfully Inserted\"}");
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound("{\"status\": \"Insertion Failed\"}");
            }
        }

        /// <summary>
        /// Insert District Dim.
        /// </summary>
        /// <param name="passcode">passcode.</param>
        /// <returns>Action Result.</returns>
        [AllowAnonymous]
        [HttpPost("InsertDistrictDim")]
        public IActionResult InsertDistrictDim()
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
                List<DistrictLG> districtLG = client.DistBiharLgAsync(config.Value.Passcode).Result.Select(x => new DistrictLG { DistLgCode = Convert.ToInt32(x.DistLgCode), DistName = x.DistName }).ToList();

                int result = this.miscService.InsertDistrictDim(districtLG);

                if (result == -1)
                {
                    return this.NotFound("{\"status\": \"Insertion Failed\"}");
                }
                else
                {
                    return this.Ok("{\"status\": \"Data Successfully Inserted/Updated\"}");
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound("{\"status\": \"Insertion Failed\"}");
            }
        }

        /// <summary>
        /// Insert AParali Details.
        /// </summary>
        /// <returns>Action Result.</returns>
        [AllowAnonymous]
        [HttpPost("InsertAParaliDetails")]
        public IActionResult InsertAParaliDetails()
        {
            try
            {
                DataTable dt = new DataTable();
                BiharAgariWebServiceSoapClient client = new BiharAgariWebServiceSoapClient(BiharAgariWebServiceSoapClient.EndpointConfiguration.BiharAgariWebServiceSoap);

                client.ClientCredentials.ServiceCertificate.SslCertificateAuthentication =
                new X509ServiceCertificateAuthentication()
                {
                    CertificateValidationMode = X509CertificateValidationMode.None,
                    RevocationMode = System.Security.Cryptography.X509Certificates.X509RevocationMode.NoCheck,
                };

                List<PraliMaster> paraliMaster = client.GetAParaliDetailsAsync("10", this.config.Value.Passcode).Result.ToList();

                dt.Columns.Add("registration_ID", typeof(long));
                dt.Columns.Add("farmer_name", typeof(string));
                dt.Columns.Add("burning_date", typeof(DateTime));
                dt.Columns.Add("district_lg_code", typeof(int));
                dt.Columns.Add("district_name", typeof(string));
                dt.Columns.Add("block_lg_code", typeof(int));
                dt.Columns.Add("block_Name", typeof(string));
                dt.Columns.Add("panchayat_lg_code", typeof(int));
                dt.Columns.Add("panchayat_name", typeof(string));
                dt.Columns.Add("village_lg_code", typeof(long));
                dt.Columns.Add("village_name", typeof(string));
                dt.Columns.Add("season_name", typeof(string));
                dt.Columns.Add("crop_name", typeof(string));

                foreach (var item in paraliMaster)
                {
                    dt.Rows.Add(
                        Convert.ToInt64(item.Registration_ID),
                        item.FarmerName,
                        Convert.ToDateTime(item.BurningDate),
                        Convert.ToInt32(item.DistrictCode),
                        item.DistrictName,
                        Convert.ToInt32(item.BlockCode),
                        item.BlockName,
                        Convert.ToInt32(item.PanchayatCode),
                        item.PanchayatName,
                        Convert.ToInt64(item.VillageCode),
                        item.VillageName,
                        string.Empty,
                        item.CropName);
                }

                int result = this.miscService.InsertParaliDetails(dt);

                if (result == -1)
                {
                    return this.NotFound("{\"status\": \"Insertion Failed\"}");
                }
                else
                {
                    return this.Ok("{\"status\": \"Data Successfully Inserted/Updated\"}");
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound("{\"status\": \"Insertion Failed\"}");
            }
        }

        /// <summary>
        /// Insecticide List Async.
        /// </summary>
        /// <returns>Action Result.</returns>
        [AllowAnonymous]
        [HttpPost("InsecticideListAsync")]
        public IActionResult InsecticideListAsync()
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
                List<InsecticideLicenceModel> insecticideLicence = client.InsecticideListAsync(this.config.Value.Passcode).Result.Select(x => new InsecticideLicenceModel { FirmName = x.FirmName, DistName = x.DistName, Distcode = x.Distcode, LicenceDate = x.LicenceDate, LicenceNo = x.LicenceNo }).ToList();

                int result = this.miscService.InsecticideListAsync(insecticideLicence);

                if (result == -1)
                {
                    return this.NotFound("{\"status\": \"Insertion Failed\"}");
                }
                else
                {
                    return this.Ok("{\"status\": \"Data Successfully Inserted/Updated\"}");
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound("{\"status\": \"Insertion Failed\"}");
            }
        }

        /// <summary>
        /// Farm List Async.
        /// </summary>
        /// <returns>Action Result.</returns>
        [AllowAnonymous]
        [HttpPost("FarmListAsync")]
        public IActionResult FarmListAsync()
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
                List<FarmNameModel> farmMaster = client.FarmDetailsAsync(this.config.Value.Passcode).Result.Select(x => new FarmNameModel { DistName = x.District, FarmName = x.FarmName }).ToList();

                int result = this.miscService.FarmNameListAsync(farmMaster);

                if (result == -1)
                {
                    return this.NotFound("{\"status\": \"Insertion Failed\"}");
                }
                else
                {
                    return this.Ok("{\"status\": \"Data Successfully Inserted/Updated\"}");
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound("{\"status\": \"Insertion Failed\"}");
            }
        }

        /// <summary>
        /// Insert Block Dim.
        /// </summary>
        /// <param name="passcode">passcode.</param>
        /// <returns>Action Result.</returns>
        [AllowAnonymous]
        [HttpPost("InsertBlockDim")]
        public IActionResult InsertBlockDim()
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
                List<BlockLG> blockLG = client.BlockBiharLgAsync(config.Value.Passcode).Result.Select(x => new BlockLG { DistLgCode = Convert.ToInt32(x.DistLgCode), BlockLgCode = Convert.ToInt32(x.BlockLgCode), BlockName = x.BlockName }).ToList();

                int result = this.miscService.InsertBlockDim(blockLG);

                if (result == 0)
                {
                    return this.NotFound("{\"status\": \"Insertion Failed\"}");
                }
                else
                {
                    return this.Ok("{\"status\": \"Data Successfully Inserted/Updated\"}");
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound("{\"status\": \"Insertion Failed\"}");
            }
        }

        /// <summary>
        /// Insert Panchayat Dim.
        /// </summary>
        /// <param name="passcode">passcode.</param>
        /// <returns>Action Result.</returns>
        [AllowAnonymous]
        [HttpPost("InsertPanchayatDim")]
        public IActionResult InsertPanchayatDim()
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
                List<PanchayatLG> panchayatLG = client.PanchayatBiharLgAsync(config.Value.Passcode).Result.Select(x => new PanchayatLG { DistLgCode = Convert.ToInt32(x.DistLgCode), DistName = x.DistName, BlockLgCode = Convert.ToInt32(x.BlockLgCode), BlockName = x.BlockName, PanchayatLgCode = Convert.ToInt32(x.PanchayatLgCode), PanchayatName = x.PanchayatName, Villagelgcode = Convert.ToInt64(x.VillLgCode), Villagename = x.VillName }).ToList();

                int result = this.miscService.InsertPanchayatDim(panchayatLG);

                if (result == 0)
                {
                    return this.NotFound("{\"status\": \"Insertion Failed\"}");
                }
                else
                {
                    return this.Ok("{\"status\": \"Data Successfully Inserted/Updated\"}");
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound("{\"status\": \"Insertion Failed\"}");
            }
        }

        /// <summary>
        /// Insert Village Dim.
        /// </summary>
        /// <param name="passcode">passcode.</param>
        /// <returns>Action Result.</returns>
        [AllowAnonymous]
        [HttpPost("InsertVillageDim")]
        public IActionResult InsertVillageDim()
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
                List<VillageLG> villageLG = client.RevenewVillageAsync(this.config.Value.Passcode).Result.Select(x => new VillageLG { DistLgCode = x.DistcodeLg, DistName = x.Districtname, BlockLgCode = x.BlockLg, BlockName = x.Blockname, PanchayatLgCode = x.PanchayatCodeLG, PanchayatName = x.panchayatname, Villagelgcode = x.VillageCodeLg, Villagename = x.RVillageName }).ToList();

                int result = this.miscService.InsertVillageDim(villageLG);

                if (result == 0)
                {
                    return this.NotFound("{\"status\": \"Insertion Failed\"}");
                }
                else
                {
                    return this.Ok("{\"status\": \"Data Successfully Inserted/Updated\"}");
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound("{\"status\": \"Insertion Failed\"}");
            }
        }

        /// <summary>
        /// Get Subsidy Status.
        /// </summary>
        /// <param name="districtLgCode">districtLgCode.</param>
        /// <param name="blockLgCode">blockLgCode.</param>
        /// <param name="panchayatLgCode">panchayatLgCode.</param>
        /// <returns>Action Result.</returns>
        [AllowAnonymous]
        [HttpGet("GetSubsidyStatus/{DistrictLgCode}/{BlockLgCode}/{PanchayatLgCode}")]
        public IActionResult GetSubsidyStatus(int districtLgCode, int blockLgCode, int panchayatLgCode)
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
                List<BiharAgriWebService.SubsidyClass> status = client.InputSubsidyStatusAsync(districtLgCode, blockLgCode, panchayatLgCode, this.config.Value.Passcode).Result.ToList();

                if (status.Any())
                {
                    return this.Ok(status);
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
        /// Insert Subsidy Status.
        /// </summary>
        /// <returns>Action Result.</returns>
        [AllowAnonymous]
        [HttpPost("InsertSubsidyStatus")]
        public IActionResult InsertSubsidyStatus()
        {
            try
            {
                List<PanchayatData> panchData = this.miscService.GetPanchayatdim();

                DataTable dt = new DataTable();

                if (panchData.Any())
                {
                    dt.Columns.Add("district_lg_code", typeof(int));
                    dt.Columns.Add("block_lg_code", typeof(int));
                    dt.Columns.Add("panchayat_lg_code", typeof(int));
                    dt.Columns.Add("Application_ID", typeof(string));
                    dt.Columns.Add("Registration_ID", typeof(string));
                    dt.Columns.Add("Application_Status", typeof(string));

                    foreach (var data in panchData)
                    {
                        BiharAgariWebServiceSoapClient client = new BiharAgariWebServiceSoapClient(BiharAgariWebServiceSoapClient.EndpointConfiguration.BiharAgariWebServiceSoap);
                        client.ClientCredentials.ServiceCertificate.SslCertificateAuthentication =
                    new X509ServiceCertificateAuthentication()
                    {
                        CertificateValidationMode = X509CertificateValidationMode.None,
                        RevocationMode = System.Security.Cryptography.X509Certificates.X509RevocationMode.NoCheck,
                    };
                        List<BiharAgriWebService.SubsidyClass> status = client.InputSubsidyStatusAsync(data.District_lg_code, data.Block_lg_code, data.Panchayat_lg_code, this.config.Value.Passcode).Result.ToList();

                        if (status.Any())
                        {
                            foreach (var subsidy in status)
                            {
                                dt.Rows.Add(data.District_lg_code, data.Block_lg_code, data.Panchayat_lg_code, subsidy.ApplicationID, subsidy.RegistrationID, subsidy.ApplicationStatus);
                            }
                        }
                    }
                }

                int result = miscService.InsertSubsidyStatus(dt);

                if (result == 0)
                {
                    return this.NotFound("{\"status\": \"Insertion Failed\"}");
                }
                else
                {
                    return this.Ok("{\"status\": \"Data Successfully Inserted/Updated\"}");
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound("{\"status\": \"Insertion Failed\"}");
            }
        }

        /// <summary>
        /// Insert Scheme.
        /// </summary>
        /// <returns>Action Result.</returns>
        [AllowAnonymous]
        [HttpPost("InsertScheme")]
        public async Task<IActionResult> InsertScheme()
        {
            try
            {
                int result = 0;

                MobileAttributeConfig sessonStatus = this.miscService.GetSource("BRBN Season").FirstOrDefault();

                List<SeasonInfo> info = this.miscService.GetSeasonInfo(sessonStatus?.Attribute_Value);

                if (info.Any())
                {
                    List<Scheme> schemeslst = new List<Scheme>();

                    foreach (var item in info)
                    {
                        var client = new HttpClient();

                        var request = new HttpRequestMessage
                        {
                            Method = HttpMethod.Get,
                            RequestUri = new Uri(this.config.Value.SchemeAPI + item.Season_name),
                        };
                        client.Timeout = TimeSpan.FromMinutes(10);
                        var response = await client.SendAsync(request).ConfigureAwait(true);
                        if (response.IsSuccessStatusCode)
                        {
                            var schemDTO = await response.Content.ReadFromJsonAsync<List<Scheme>>();

                            if (schemDTO != null && schemDTO.Any())
                            {
                                schemeslst.AddRange(schemDTO);
                            }
                        }
                    }

                    if (schemeslst.Any())
                    {
                        result = this.miscRepository.InsertScheme(schemeslst);
                    }
                    else
                    {
                        return this.Ok("{\"status\": \"No Schemes was found\"}");
                    }

                    if (result == -1)
                    {
                        return this.NotFound("{\"status\": \"Insertion Failed\"}");
                    }
                    else
                    {
                        return this.Ok("{\"status\": \"Data Successfully Inserted/Updated\"}");
                    }
                }
                else
                {
                    return this.Ok("{\"status\": \"No Season was found\"}");
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound("{\"status\": \"Insertion Failed\"}");
            }
        }

        /// <summary>
        /// Insert Subsidy Report.
        /// </summary>
        /// <returns>Action Result.</returns>
        [AllowAnonymous]
        [HttpPost("InsertSubsidyReport")]
        public IActionResult InsertSubsidyReport()
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
                List<SchemeCount> scheme = client.InputSubsidyReportAsync(this.config.Value.Passcode, "Crop Input Subsidy", "1920").Result.ToList();

                int result = this.miscService.InsertSubsidyReport(scheme);

                if (result == 0 || result == -1)
                {
                    return this.NotFound("{\"status\": \"Insertion Failed\"}");
                }
                else
                {
                    return this.Ok("{\"status\": \"Data Successfully Inserted/Updated\"}");
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound("{\"status\": \"Insertion Failed\"}");
            }
        }

        /// <summary>
        /// InsertOFMAS Scheme.
        /// </summary>
        /// <returns>Action Result.</returns>
        [AllowAnonymous]
        [HttpPost("InsertOFMAS_Scheme")]
        public IActionResult InsertOFMAS_Scheme()
        {
            try
            {
                OFMASReportWebServiceSoapClient client = new OFMASReportWebServiceSoapClient(OFMASReportWebServiceSoapClient.EndpointConfiguration.OFMASReportWebServiceSoap);
                client.ClientCredentials.ServiceCertificate.SslCertificateAuthentication =
                new X509ServiceCertificateAuthentication()
                {
                    CertificateValidationMode = X509CertificateValidationMode.None,
                    RevocationMode = System.Security.Cryptography.X509Certificates.X509RevocationMode.NoCheck,
                };
                List<OFMAS_Scheme> scheme = client.getOFMAS_SchemeAsync(this.config.Value.OfmasPasscode).Result.ToList();

                int result = 0;

                if (scheme.Any())
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add("scheme_id", typeof(int));
                    dt.Columns.Add("financial_Year", typeof(string));
                    dt.Columns.Add("scheme_name", typeof(string));
                    dt.Columns.Add("short_scheme", typeof(string));
                    dt.Columns.Add("phase", typeof(int));
                    dt.Columns.Add("rec_created_userid", typeof(string));
                    dt.Columns.Add("rec_created_date", typeof(DateTime));

                    foreach (var item in scheme)
                    {
                        dt.Rows.Add(item.Sid, item.F_Year, item.Scheme, item.ShortSch, item.Phase, null, DateTime.Now);
                    }

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        result = this.miscService.InsertOFMASScheme(dt);
                    }
                }

                if (result > 0)
                {
                    return this.Ok();
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
        /// InsertOFMAS AMTRPTDist.
        /// </summary>
        /// <returns>Action Result.</returns>
        [AllowAnonymous]
        [HttpPost("InsertOFMAS_AMTRPTDist")]
        public IActionResult InsertOFMAS_AMTRPTDist()
        {
            try
            {
                int result = 0;
                var finyear = string.Empty;
                OFMASReportWebServiceSoapClient client = new OFMASReportWebServiceSoapClient(OFMASReportWebServiceSoapClient.EndpointConfiguration.OFMASReportWebServiceSoap);
                client.ClientCredentials.ServiceCertificate.SslCertificateAuthentication =
                new X509ServiceCertificateAuthentication()
                {
                    CertificateValidationMode = X509CertificateValidationMode.None,
                    RevocationMode = System.Security.Cryptography.X509Certificates.X509RevocationMode.NoCheck,
                };
                List<OFMAS_Scheme> scheme = client.getOFMAS_SchemeAsync(this.config.Value.OfmasPasscode).Result.ToList();

                if (scheme.Any())
                {
                    List<OFMAS_AMT_RptDist> dist = new List<OFMAS_AMT_RptDist>();
                    foreach (var item in scheme)
                    {
                        if (item.Scheme == "Agricultural Machinery Repair Training")
                        {
                            finyear = item.F_Year;
                            var data = client.getOFMAS_AMTRptDistAsync(this.config.Value.OfmasPasscode, item.F_Year, Convert.ToInt64(item.Sid)).Result.ToList();

                            if (data != null && data.Any())
                            {
                                dist.AddRange(data);
                            }
                        }
                    }

                    if (dist.Any())
                    {
                        DataTable dt = new DataTable();
                        dt.Columns.Add("district_lg_code", typeof(int));
                        dt.Columns.Add("district_name", typeof(string));
                        dt.Columns.Add("total_appln", typeof(int));
                        dt.Columns.Add("final_appln", typeof(int));
                        dt.Columns.Add("pending_AC", typeof(int));
                        dt.Columns.Add("Verify_AC", typeof(int));
                        dt.Columns.Add("Pending_ADAE", typeof(int));
                        dt.Columns.Add("Verify_ADAE", typeof(int));
                        dt.Columns.Add("Selected_Merit_list", typeof(int));
                        dt.Columns.Add("Fin_Year", typeof(string));
                        dt.Columns.Add("rec_created_userid", typeof(string));
                        dt.Columns.Add("rec_created_date", typeof(DateTime));

                        foreach (var item in dist)
                        {
                            dt.Rows.Add(
                                Convert.ToInt32(item.dist_code),
                                item.dist_name,
                                Convert.ToInt32(item.totalapp),
                                Convert.ToInt32(item.finalapp),
                                Convert.ToInt32(item.pendingAC),
                                Convert.ToInt32(item.VerifyAC),
                                Convert.ToInt32(item.PendingADAE),
                                Convert.ToInt32(item.VerifyADAE),
                                Convert.ToInt32(item.SelMeritlist),
                                finyear,
                                null,
                                DateTime.Now);
                        }

                        if (dt != null && dt.Rows.Count > 0)
                        {
                            result = this.miscService.InsertOFMASAMRTDist(dt);
                        }
                    }

                    if (result > 0)
                    {
                        return this.Ok();
                    }

                    return this.NotFound();
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
        /// InsertOFMAS AMTRPTBlk.
        /// </summary>
        /// <returns>Action Result.</returns>
        [AllowAnonymous]
        [HttpPost("InsertOFMAS_AMTRPTBlk")]
        public IActionResult InsertOFMAS_AMTRPTBlk()
        {
            try
            {
                int result = 0;
                var finyear = string.Empty;
                OFMASReportWebServiceSoapClient client = new OFMASReportWebServiceSoapClient(OFMASReportWebServiceSoapClient.EndpointConfiguration.OFMASReportWebServiceSoap);
                client.ClientCredentials.ServiceCertificate.SslCertificateAuthentication =
                new X509ServiceCertificateAuthentication()
                {
                    CertificateValidationMode = X509CertificateValidationMode.None,
                    RevocationMode = System.Security.Cryptography.X509Certificates.X509RevocationMode.NoCheck,
                };
                List<OFMAS_Scheme> scheme = client.getOFMAS_SchemeAsync(this.config.Value.OfmasPasscode).Result.ToList();

                if (scheme.Any())
                {
                    List<OFMAS_AMT_RptDist> dist = new List<OFMAS_AMT_RptDist>();
                    foreach (var item in scheme)
                    {
                        if (item.Scheme == "Agricultural Machinery Repair Training")
                        {
                            var data = client.getOFMAS_AMTRptDistAsync(this.config.Value.OfmasPasscode, item.F_Year, Convert.ToInt64(item.Sid)).Result.ToList();
                            finyear = item.F_Year;
                            if (data != null && data.Any())
                            {
                                dist.AddRange(data);
                            }
                        }
                    }

                    if (dist.Any())
                    {
                        List<OFMAS_AMT_RptBlk> blk = new List<OFMAS_AMT_RptBlk>();
                        foreach (var sch in scheme)
                        {
                            if (sch.Scheme == "Agricultural Machinery Repair Training")
                            {
                                foreach (var distrct in dist)
                                {
                                    var data = client.getOFMAS_AMTRptBlkAsync(this.config.Value.OfmasPasscode, sch.F_Year, Convert.ToInt64(sch.Sid), distrct.dist_code).Result.ToList();

                                    if (data != null && data.Any())
                                    {
                                        blk.AddRange(data);
                                    }
                                }
                            }
                        }

                        if (blk.Any())
                        {
                            DataTable dt = new DataTable();

                            dt.Columns.Add("block_lg_code", typeof(int));
                            dt.Columns.Add("block_name", typeof(string));
                            dt.Columns.Add("total_appln", typeof(int));
                            dt.Columns.Add("final_appln", typeof(int));
                            dt.Columns.Add("pending_AC", typeof(int));
                            dt.Columns.Add("Verify_AC", typeof(int));
                            dt.Columns.Add("Pending_ADAE", typeof(int));
                            dt.Columns.Add("Verify_ADAE", typeof(int));
                            dt.Columns.Add("Selected_Merit_list", typeof(int));
                            dt.Columns.Add("Fin_Year", typeof(string));
                            dt.Columns.Add("rec_created_userid", typeof(string));
                            dt.Columns.Add("rec_created_date", typeof(DateTime));
                            foreach (var item in blk)
                            {
                                dt.Rows.Add(Convert.ToInt32(item.block_code), item.block_name, Convert.ToInt32(item.totalapp), Convert.ToInt32(item.finalapp), Convert.ToInt32(item.pendingAC), Convert.ToInt32(item.VerifyAC), Convert.ToInt32(item.PendingADAE), Convert.ToInt32(item.VerifyADAE), Convert.ToInt32(item.SelMeritlist), finyear, null, DateTime.Now);
                            }

                            if (dt != null && dt.Rows.Count > 0)
                            {
                                result = this.miscService.InsertOFMASAMRTBlk(dt);
                            }
                        }
                        else
                        {
                            return this.NotFound();
                        }
                    }

                    if (result > 0)
                    {
                        return this.Ok();
                    }

                    return this.NotFound();
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
        /// InsertOFMAS AMTRPTPanch.
        /// </summary>
        /// <returns>Action Result.</returns>
        [AllowAnonymous]
        [HttpPost("InsertOFMAS_AMTRPTPanch")]
        public IActionResult InsertOFMAS_AMTRPTPanch()
        {
            try
            {
                int result = 0;
                var finyear = string.Empty;
                OFMASReportWebServiceSoapClient client = new OFMASReportWebServiceSoapClient(OFMASReportWebServiceSoapClient.EndpointConfiguration.OFMASReportWebServiceSoap);
                client.ClientCredentials.ServiceCertificate.SslCertificateAuthentication =
                new X509ServiceCertificateAuthentication()
                {
                    CertificateValidationMode = X509CertificateValidationMode.None,
                    RevocationMode = System.Security.Cryptography.X509Certificates.X509RevocationMode.NoCheck,
                };
                List<OFMAS_Scheme> scheme = client.getOFMAS_SchemeAsync(this.config.Value.OfmasPasscode).Result.ToList();
                if (scheme.Any())
                {
                    List<District> dist = this.miscRepository.GetdistrictCode();

                    if (dist.Any())
                    {
                        List<PanchayatData> blk = this.miscRepository.GetOfmasBlock();

                        if (blk.Any())
                        {
                            foreach (var sch in scheme)
                            {
                                finyear = sch.F_Year;
                                if (sch.Scheme == "Agricultural Machinery Repair Training")
                                {
                                    foreach (var blks in blk)
                                    {
                                        OFMASReportWebServiceSoapClient clientPanch = new OFMASReportWebServiceSoapClient(OFMASReportWebServiceSoapClient.EndpointConfiguration.OFMASReportWebServiceSoap);
                                        clientPanch.ClientCredentials.ServiceCertificate.SslCertificateAuthentication =
                                        new X509ServiceCertificateAuthentication()
                                        {
                                            CertificateValidationMode = X509CertificateValidationMode.None,
                                            RevocationMode = System.Security.Cryptography.X509Certificates.X509RevocationMode.NoCheck,
                                        };

                                        var data = clientPanch.getOFMAS_AMTRptPanAsync(this.config.Value.OfmasPasscode, sch.F_Year, Convert.ToInt64(sch.Sid), blks.Block_lg_code.ToString()).Result.ToList();
                                        List<OFMAS_AMT_RptPan> panch = new List<OFMAS_AMT_RptPan>();
                                        if (data != null && data.Any())
                                        {
                                            panch.AddRange(data);
                                        }

                                        if (panch.Any())
                                        {
                                            DataTable dt = new DataTable();
                                            dt.Columns.Add("pancode", typeof(long));
                                            dt.Columns.Add("pan_name", typeof(string));
                                            dt.Columns.Add("totalapp", typeof(int));
                                            dt.Columns.Add("finalapp", typeof(int));
                                            dt.Columns.Add("pendingAC", typeof(int));
                                            dt.Columns.Add("VerifyAC", typeof(int));
                                            dt.Columns.Add("PendingADAE", typeof(int));
                                            dt.Columns.Add("VerifyADAE", typeof(int));
                                            dt.Columns.Add("SelMeritlist", typeof(int));
                                            dt.Columns.Add("Fin_Year", typeof(string));
                                            dt.Columns.Add("rec_created_userid", typeof(string));
                                            dt.Columns.Add("rec_created_date", typeof(DateTime));

                                            foreach (var item in panch)
                                            {
                                                dt.Rows.Add(
                                                    Convert.ToInt64(item.pan_code),
                                                    item.pan_name,
                                                    Convert.ToInt32(item.totalapp),
                                                    Convert.ToInt32(item.finalapp),
                                                    Convert.ToInt32(item.pendingAC),
                                                    Convert.ToInt32(item.VerifyAC),
                                                    Convert.ToInt32(item.PendingADAE),
                                                    Convert.ToInt32(item.VerifyADAE),
                                                    Convert.ToInt32(item.SelMeritlist),
                                                    finyear, null,
                                                    DateTime.Now);
                                            }

                                            if (dt != null && dt.Rows.Count > 0)
                                            {
                                                result = result + this.miscService.InsertOFMASAMRTPan(dt);
                                            }
                                        }
                                        else
                                        {
                                            return this.NotFound("No Panchayat Found");
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            return this.NotFound("No Block Found");
                        }
                    }
                    else
                    {
                        return this.NotFound("District Not Found");
                    }
                    if (result > 0)
                    {
                        return this.Ok(result);
                    }

                    return this.NotFound("No rows Affected");
                }
                else
                {
                    return this.NotFound("No Schemes Found");
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.Problem(ex.Message);
            }
        }

        /// <summary>
        /// Insert BRBN Application.
        /// </summary>
        /// <returns>Action Result.</returns>
        [AllowAnonymous]
        [HttpPost("InsertBRBNApplication")]
        public async Task<IActionResult> InsertBRBNApplication()
        {
            try
            {
                int result = 0;

                MobileAttributeConfig sessonStatus = this.miscService.GetSource("BRBN Season").FirstOrDefault();
                List<SeasonInfo> info = this.miscService.GetSeasonInfo(sessonStatus?.Attribute_Value);

                if (info.Any())
                {
                    List<BrbnApplication> brbnapp = new List<BrbnApplication>();

                    foreach (var item in info)
                    {
                        var client = new HttpClient();

                        var request = new HttpRequestMessage
                        {
                            Method = HttpMethod.Get,
                            RequestUri = new Uri(this.config.Value.ApplicationAPI + item.Season_name),
                        };
                        client.Timeout = TimeSpan.FromMinutes(150);
                        var response = await client.SendAsync(request).ConfigureAwait(true);
                        if (response.IsSuccessStatusCode)
                        {
                            var brnbnDTO = await response.Content.ReadFromJsonAsync<List<BrbnApplication>>();

                            if (brnbnDTO != null && brnbnDTO.Any())
                            {
                                brbnapp.AddRange(brnbnDTO);
                            }
                        }
                    }

                    if (brbnapp.Any())
                    {
                        result = this.miscRepository.InsertbrbnApplication(brbnapp);
                    }
                    else
                    {
                        return this.Ok("{\"status\": \"No Applications was found\"}");
                    }

                    if (result == -1)
                    {
                        return this.NotFound("{\"status\": \"Insertion Failed\"}");
                    }
                    else
                    {
                        return this.Ok("{\"status\": \"Data Successfully Inserted/Updated\"}");
                    }
                }
                else
                {
                    return this.Ok("{\"status\": \"No Season was found\"}");
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound("{\"status\": \"Insertion Failed\"}");
            }
        }

        /// <summary>
        /// Insert BRBN Application Status.
        /// </summary>
        /// <returns>Action Result.</returns>
        [AllowAnonymous]
        [HttpPost("InsertBRBNApplicationStatus")]
        public async Task<IActionResult> InsertBRBNApplicationStatus()
        {
            try
            {
                int result = 0;

                MobileAttributeConfig sessonStatus = this.miscService.GetSource("BRBN Season").FirstOrDefault();
                List<SeasonInfo> info = this.miscService.GetSeasonInfo(sessonStatus?.Attribute_Value);

                if (info.Any())
                {
                    foreach (var item in info)
                    {
                        List<BrBnApplicationStatus> brbnappStatus = new List<BrBnApplicationStatus>();
                        var client = new HttpClient();

                        var request = new HttpRequestMessage
                        {
                            Method = HttpMethod.Get,
                            RequestUri = new Uri(this.config.Value.ApplicationStatusAPI + item.Season_name),
                        };
                        client.Timeout = TimeSpan.FromMinutes(120);
                        var response = await client.SendAsync(request).ConfigureAwait(true);
                        if (response.IsSuccessStatusCode)
                        {
                            var brnbnDTO = await response.Content.ReadFromJsonAsync<List<BrBnApplicationStatus>>();

                            if (brnbnDTO != null && brnbnDTO.Any())
                            {
                                foreach (var dto in brnbnDTO)
                                {
                                    dto.Session_name = item.Season_name;
                                }

                                brbnappStatus.AddRange(brnbnDTO);
                            }
                        }

                        if (brbnappStatus.Any())
                        {
                            List<DistrictList> lgdir = this.cropRepository.GetAllDistrict();

                            if (lgdir != null && lgdir.Any())
                            {
                                foreach (var districts in lgdir)
                                {
                                    List<BrBnApplicationStatus> frDistricts = brbnappStatus.Where(x => x.District.ToUpper() == districts.District_name.ToUpper()).ToList();

                                    if (frDistricts != null && frDistricts.Any())
                                    {
                                        result = this.miscRepository.InsertbrbnApplicationStatus(frDistricts);
                                    }
                                }
                            }
                            else
                            {
                                return this.NotFound();
                            }
                        }
                        else
                        {
                            return this.Ok("{\"status\": \"No Applications was found\"}");
                        }
                    }

                    if (result == -1)
                    {
                        return this.NotFound("{\"status\": \"Insertion Failed\"}");
                    }
                    else
                    {
                        return this.Ok("{\"status\": \"Data Successfully Inserted/Updated\"}");
                    }
                }
                else
                {
                    return this.Ok("{\"status\": \"No Season was found\"}");
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound("{\"status\": \"Insertion Failed\"}");
            }
        }

        /// <summary>
        /// Insert Farmer Info.
        /// </summary>
        /// <returns>Action Result.</returns>
        [AllowAnonymous]
        [HttpPost("InsertFarmerInfo")]
        public IActionResult InsertFarmerInfo()
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
                List<FarmerCat> farmerCastCat = new List<FarmerCat>();
                List<Farmergender> farmergenders = new List<Farmergender>();
                List<District> districts = this.miscService.GetdistrictCode();
                List<FarmerType> farmerTypes = new List<FarmerType>();
                foreach (var district in districts)
                {
                    var farmcat = client.FarmerCastCategoryAsync(district.District_lg_code.ToString(), this.config.Value.Passcode).Result.ToList();

                    foreach (var item in farmcat)
                    {
                        item.DistrictName = district.District_name;
                    }

                    farmerCastCat.AddRange(farmcat);
                    var farmGen = client.FarmerCategoryAsync(district.District_lg_code.ToString(), this.config.Value.Passcode).Result.ToList();

                    foreach (var item in farmGen)
                    {
                        item.Districtname = district.District_name;
                    }

                    farmergenders.AddRange(farmGen);
                    var farmType = client.FarmerTypeAsync(district.District_lg_code.ToString(), this.config.Value.Passcode).Result.ToList();

                    foreach (var item in farmType)
                    {
                        item.DistrictName = district.District_name;
                    }

                    farmerTypes.AddRange(farmType);
                }

                DataTable dtcastCategory = new DataTable();
                DataTable dtGender = new DataTable();
                DataTable dtFarmerTypes = new DataTable();
                if (farmerCastCat.Any())
                {
                    dtcastCategory.Columns.Add("DistrictName", typeof(string));
                    dtcastCategory.Columns.Add("BlockName", typeof(string));
                    dtcastCategory.Columns.Add("PanchayatName", typeof(string));
                    dtcastCategory.Columns.Add("caste_Sc", typeof(int));
                    dtcastCategory.Columns.Add("caste_St", typeof(int));
                    dtcastCategory.Columns.Add("caste_BC", typeof(int));
                    dtcastCategory.Columns.Add("caste_EBC", typeof(int));
                    dtcastCategory.Columns.Add("caste_General", typeof(int));
                    dtcastCategory.Columns.Add("caste_Minority", typeof(int));

                    foreach (var item in farmerCastCat)
                    {
                        dtcastCategory.Rows.Add(item.DistrictName, item.BlockName, item.Panchaytname, Convert.ToInt32(item.Sc), Convert.ToInt32(item.St), Convert.ToInt32(item.BC), Convert.ToInt32(item.EBC), Convert.ToInt32(item.General), Convert.ToInt32(item.Minority));
                    }
                }

                if (farmergenders.Any())
                {
                    dtGender.Columns.Add("DistrictName", typeof(string));
                    dtGender.Columns.Add("BlockName", typeof(string));
                    dtGender.Columns.Add("PanchayatName", typeof(string));
                    dtGender.Columns.Add("Male_farmers", typeof(int));
                    dtGender.Columns.Add("Female_farmers", typeof(int));
                    dtGender.Columns.Add("Other_gender", typeof(int));

                    foreach (var item in farmergenders)
                    {
                        dtGender.Rows.Add(item.Districtname, item.BlockName, item.Panchaytname, Convert.ToInt32(item.Male), Convert.ToInt32(item.Female), Convert.ToInt32(item.Other));
                    }
                }

                if (farmerTypes.Any())
                {
                    dtFarmerTypes.Columns.Add("DistrictName", typeof(string));
                    dtFarmerTypes.Columns.Add("BlockName", typeof(string));
                    dtFarmerTypes.Columns.Add("PanchayatName", typeof(string));
                    dtFarmerTypes.Columns.Add("Large_farmers", typeof(int));
                    dtFarmerTypes.Columns.Add("Small_farmers", typeof(int));
                    dtFarmerTypes.Columns.Add("Marginal_farmers", typeof(int));

                    foreach (var item in farmerTypes)
                    {
                        dtFarmerTypes.Rows.Add(item.DistrictName, item.BlockName, item.Panchaytname, Convert.ToInt32(item.Large), Convert.ToInt32(item.Small), Convert.ToInt32(item.Marginal));
                    }
                }

                int result = this.miscService.InsertFarmerInfo(dtcastCategory, dtGender, dtFarmerTypes);

                if (result == 1)
                {
                    return this.Ok("{\"status\": \"Data Successfully Inserted/Updated\"}");
                }
                else
                {
                    return this.NotFound("{\"status\": \"Data Insertion Failed\"}");
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound("{\"status\": \"Data Insertion Failed\"}");
            }
        }

        /// <summary>
        /// Get DOA Instructions.
        /// </summary>
        /// <param name="letterType">letterType.</param>
        /// <returns>ActionResult.</returns>
        [HttpGet("GetDOAInstructions/{letterType}")]
        public async Task<IActionResult> GetDOAInstructions(string letterType)
        {
            try
            {
                List<QRCode> letterData;
                List<DoaInstructions> result = this.miscService.GetDOAInstructions();

                string blobfileName = string.Empty;

                if (result.Any())
                {
                    blobfileName = result.Where(x => x.Letter_Type_Code == letterType).Select(x => x.Blob_Filename).FirstOrDefault();
                }

                if (!string.IsNullOrEmpty(blobfileName))
                {
                    var blobInfo = this.storageController.GetBlobAsync("DOA_Instructions", blobfileName);

                    if (blobInfo.Result.ContentLength != 0 && blobInfo.Result.Details.LastModified.Date == DateTime.Now.Date)
                    {
                        StringBuilder stringBuilder = new StringBuilder();
                        using (var streamReader = new StreamReader(blobInfo.Result.Content))
                        {
                            while (!streamReader.EndOfStream)
                            {
                                var line = await streamReader.ReadLineAsync();
                                stringBuilder.Append(line);
                            }
                        }

                        letterData = JsonConvert.DeserializeObject<List<QRCode>>(stringBuilder.ToString());
                        return this.Ok(letterData);
                    }

                    BlobEntity blobEntity = new BlobEntity();
                    blobEntity.DirectoryName = "DOA_Instructions";
                    blobEntity.FolderName = blobfileName;

                    BiharAgariWebServiceSoapClient client = new BiharAgariWebServiceSoapClient(BiharAgariWebServiceSoapClient.EndpointConfiguration.BiharAgariWebServiceSoap);

                    client.ClientCredentials.ServiceCertificate.SslCertificateAuthentication =
                new X509ServiceCertificateAuthentication()
                {
                    CertificateValidationMode = X509CertificateValidationMode.None,
                    RevocationMode = System.Security.Cryptography.X509Certificates.X509RevocationMode.NoCheck,
                };
                    letterData = client.AgriLetterAsync(letterType, this.config.Value.Passcode).Result.ToList();

                    blobEntity.ByteArray = JsonConvert.SerializeObject(letterData);

                    await this.storageController.UploadFileStream(blobEntity);
                    return this.Ok(letterData);
                }

                return null;
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound();
            }
        }

        /// <summary>
        /// InsertOFMAS KKARPTDist.
        /// </summary>
        /// <returns>Action Result.</returns>
        [AllowAnonymous]
        [HttpPost("InsertOFMAS_KKARPTDist")]
        public IActionResult InsertOFMAS_KKARPTDist()
        {
            try
            {
                int result = 0;
                OFMASReportWebServiceSoapClient client = new OFMASReportWebServiceSoapClient(OFMASReportWebServiceSoapClient.EndpointConfiguration.OFMASReportWebServiceSoap);
                client.ClientCredentials.ServiceCertificate.SslCertificateAuthentication =
                new X509ServiceCertificateAuthentication()
                {
                    CertificateValidationMode = X509CertificateValidationMode.None,
                    RevocationMode = System.Security.Cryptography.X509Certificates.X509RevocationMode.NoCheck,
                };

                List<OFMAS_Scheme> scheme = client.getOFMAS_SchemeAsync(this.config.Value.OfmasPasscode).Result.ToList();

                if (scheme.Any())
                {
                    List<OFMAS_KKA_RptDist> dist = new List<OFMAS_KKA_RptDist>();
                    foreach (var item in scheme)
                    {
                        if (item.Scheme == "KKA")
                        {
                            var data = client.getOFMAS_KKARptDistAsync(this.config.Value.OfmasPasscode, item.F_Year, Convert.ToInt64(item.Phase), Convert.ToInt64(item.Sid)).Result.ToList();

                            if (data != null && data.Any())
                            {
                                dist.AddRange(data);
                            }
                        }
                    }

                    if (dist.Any())
                    {
                        DataTable dt = new DataTable();
                        dt.Columns.Add("District_lg_code", typeof(int));
                        dt.Columns.Add("District_name", typeof(string));
                        dt.Columns.Add("registered_usr", typeof(int));
                        dt.Columns.Add("total_appln", typeof(int));
                        dt.Columns.Add("final_appln", typeof(int));
                        dt.Columns.Add("pending_AC", typeof(int));
                        dt.Columns.Add("Verify_AC", typeof(int));
                        dt.Columns.Add("pending_BAO", typeof(int));
                        dt.Columns.Add("Verify_BAO", typeof(int));
                        dt.Columns.Add("Pending_ADAE", typeof(int));
                        dt.Columns.Add("Verify_ADAE", typeof(int));
                        dt.Columns.Add("Reject_ADAE", typeof(int));
                        dt.Columns.Add("Pending_DLEC", typeof(int));
                        dt.Columns.Add("Verify_DLEC", typeof(int));
                        dt.Columns.Add("Reject_DLEC", typeof(int));
                        dt.Columns.Add("Target_District", typeof(int));
                        dt.Columns.Add("Total_Permit_Gen", typeof(int));
                        dt.Columns.Add("Dealer_imp_dlv", typeof(int));
                        dt.Columns.Add("Confirm_by_Appl", typeof(int));
                        dt.Columns.Add("Total_Phy_Verify", typeof(int));
                        dt.Columns.Add("Total_Claim_Gen", typeof(int));
                        dt.Columns.Add("CC_By_ADAE", typeof(int));
                        dt.Columns.Add("Total_Sub_Real", typeof(int));
                        dt.Columns.Add("Total_Sub_Amt_Real", typeof(decimal));
                        dt.Columns.Add("Deactivate_Permit", typeof(int));
                        dt.Columns.Add("Fin_Year", typeof(string));
                        dt.Columns.Add("rec_created_userid", typeof(string));
                        dt.Columns.Add("rec_created_date", typeof(DateTime));

                        foreach (var item in dist)
                        {
                            dt.Rows.Add(
                                Convert.ToInt32(item.dist_code),
                                item.dist_name,
                                Convert.ToInt32(item.registerusr),
                                Convert.ToInt32(item.totalChcProject),
                                Convert.ToInt32(item.finalCHCProject),
                                Convert.ToInt32(item.pendingAC),
                                Convert.ToInt32(item.VerifyAC),
                                Convert.ToInt32(item.pendingBAO),
                                Convert.ToInt32(item.VerifyBAO),
                                Convert.ToInt32(item.PendingADAE),
                                Convert.ToInt32(item.VerifyADAE),
                                Convert.ToInt32(item.RejectADAE),
                                Convert.ToInt32(item.PendingDLEC),
                                Convert.ToInt32(item.VerifyDLEC),
                                Convert.ToInt32(item.RejectDLEC),
                                Convert.ToInt32(item.Targetdist),
                                Convert.ToInt32(item.TotalGenPermit),
                                Convert.ToInt32(item.dealerimpdlv),
                                Convert.ToInt32(item.ConfirbyApplicant),
                                Convert.ToInt32(item.totphyVerif),
                                Convert.ToInt32(item.totClaimGen),
                                Convert.ToInt32(item.CCbyADAE),
                                Convert.ToInt32(item.totSubReal),
                                Convert.ToDecimal(item.totSubAmntReal),
                                Convert.ToInt32(item.deacivatepermitS),
                                Convert.ToString(item.F_year),
                                null,
                                DateTime.Now);
                        }

                        if (dt != null && dt.Rows.Count > 0)
                        {
                            result = this.miscService.InsertOFMASKKADist(dt);
                        }
                    }

                    if (result > 0)
                    {
                        return this.Ok();
                    }

                    return this.NotFound();
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
        /// InsertOFMAS BGREIRPTDist.
        /// </summary>
        /// <returns>Action Result.</returns>
        [AllowAnonymous]
        [HttpPost("InsertOFMAS_BGREIRPTDist")]

        public IActionResult InsertOFMAS_BGREIRPTDist()
        {
            try
            {
                int result = 0;

                OFMASReportWebServiceSoapClient client = new OFMASReportWebServiceSoapClient(OFMASReportWebServiceSoapClient.EndpointConfiguration.OFMASReportWebServiceSoap);
                client.ClientCredentials.ServiceCertificate.SslCertificateAuthentication =
                new X509ServiceCertificateAuthentication()
                {
                    CertificateValidationMode = X509CertificateValidationMode.None,
                    RevocationMode = System.Security.Cryptography.X509Certificates.X509RevocationMode.NoCheck,
                };
                List<OFMAS_Scheme> scheme = client.getOFMAS_SchemeAsync(this.config.Value.OfmasPasscode).Result.ToList();

                if (scheme.Any())
                {
                    List<OFMAS_Single_RptDist> dist = new List<OFMAS_Single_RptDist>();
                    foreach (var item in scheme)
                    {
                        if (item.Scheme == "BGREI")
                        {
                            var data = client.getOFMAS_SingleRptDistAsync(this.config.Value.OfmasPasscode, item.F_Year, item.Sid).Result.ToList();

                            if (data != null && data.Any())
                            {
                                dist.AddRange(data);
                            }
                        }
                    }

                    if (dist.Any())
                    {
                        DataTable dt = new DataTable();
                        dt.Columns.Add("District_lg_code", typeof(int));
                        dt.Columns.Add("District_name", typeof(string));
                        dt.Columns.Add("totalapp", typeof(int));
                        dt.Columns.Add("finalapp", typeof(int));
                        dt.Columns.Add("VerifyAC", typeof(int));
                        dt.Columns.Add("VerifyBAO", typeof(int));
                        dt.Columns.Add("VerifyDAO", typeof(int));
                        dt.Columns.Add("TotalTarget", typeof(decimal));
                        dt.Columns.Add("PermitGen", typeof(int));
                        dt.Columns.Add("Deacivatepermit", typeof(int));
                        dt.Columns.Add("SuppUpdDealer", typeof(int));
                        dt.Columns.Add("SuppUpdateADAE", typeof(int));
                        dt.Columns.Add("PhyInsbyAc", typeof(int));
                        dt.Columns.Add("ConfbyADAE", typeof(int));
                        dt.Columns.Add("SubRelease", typeof(int));
                        dt.Columns.Add("SubReleaseamt", typeof(decimal));
                        dt.Columns.Add("Fin_Year", typeof(string));
                        dt.Columns.Add("rec_created_userid", typeof(string));
                        dt.Columns.Add("rec_created_date", typeof(DateTime));

                        foreach (var item in dist)
                        {
                            dt.Rows.Add(
                                Convert.ToInt32(item.dist_code),
                                item.dist_name,
                                Convert.ToInt32(item.totalapp),
                                Convert.ToInt32(item.finalapp),
                                Convert.ToInt32(item.VerifyAC),
                                Convert.ToInt32(item.VerifyBAO),
                                Convert.ToInt32(item.VerifyDAO),
                                Convert.ToDecimal(item.TotalTarget),
                                Convert.ToInt32(item.PermitGen),
                                Convert.ToInt32(item.Deacivatepermit),
                                Convert.ToInt32(item.SuppUpdDealer),
                                Convert.ToInt32(item.SuppUpdateADAE),
                                Convert.ToInt32(item.PhyInsbyAc),
                                Convert.ToInt32(item.ConfbyADAE),
                                Convert.ToInt32(item.SubRelease),
                                Convert.ToDecimal(item.SubReleaseamt),
                                Convert.ToString(item.F_year),
                                null,
                                DateTime.Now);
                        }

                        if (dt != null && dt.Rows.Count > 0)
                        {
                            result = this.miscService.InsertOFMASBGREIDist(dt);
                        }
                    }

                    if (result > 0)
                    {
                        return this.Ok();
                    }

                    return this.NotFound();
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
        /// InsertOFMAS BGREIRPTBlk.
        /// </summary>
        /// <returns>Action Result.</returns>
        [AllowAnonymous]
        [HttpPost("InsertOFMAS_BGREIRPTBlk")]

        public IActionResult InsertOFMAS_BGREIRPTBlk()
        {
            try
            {
                int result = 0;

                OFMASReportWebServiceSoapClient client = new OFMASReportWebServiceSoapClient(OFMASReportWebServiceSoapClient.EndpointConfiguration.OFMASReportWebServiceSoap);

                client.ClientCredentials.ServiceCertificate.SslCertificateAuthentication =
                new X509ServiceCertificateAuthentication()
                {
                    CertificateValidationMode = X509CertificateValidationMode.None,
                    RevocationMode = System.Security.Cryptography.X509Certificates.X509RevocationMode.NoCheck,
                };

                List<OFMAS_Scheme> scheme = client.getOFMAS_SchemeAsync(this.config.Value.OfmasPasscode).Result.ToList();

                if (scheme.Any())
                {
                    List<OFMAS_Single_RptDist> dist = new List<OFMAS_Single_RptDist>();
                    foreach (var item in scheme)
                    {
                        if (item.Scheme == "BGREI")
                        {
                            var data = client.getOFMAS_SingleRptDistAsync(this.config.Value.OfmasPasscode, item.F_Year, item.Sid).Result.ToList();

                            if (data != null && data.Any())
                            {
                                dist.AddRange(data);
                            }
                        }
                    }

                    if (dist.Any())
                    {
                        List<OFMAS_Single_RptBlk> blk = new List<OFMAS_Single_RptBlk>();
                        foreach (var sch in scheme)
                        {
                            if (sch.Scheme == "BGREI")
                            {
                                foreach (var distrct in dist)
                                {
                                    var data = client.getOFMAS_SingleRptBlkAsync(this.config.Value.OfmasPasscode, sch.F_Year, distrct.dist_code, sch.Sid).Result.ToList();
                                    if (data != null && data.Any())
                                    {
                                        blk.AddRange(data);
                                    }
                                }
                            }
                        }

                        if (blk.Any())
                        {
                            DataTable dt = new DataTable();
                            dt.Columns.Add("block_lg_code", typeof(int));
                            dt.Columns.Add("block_name", typeof(string));
                            dt.Columns.Add("totalapp", typeof(int));
                            dt.Columns.Add("finalapp", typeof(int));
                            dt.Columns.Add("VerifyAC", typeof(int));
                            dt.Columns.Add("VerifyBAO", typeof(int));
                            dt.Columns.Add("VerifyDAO", typeof(int));
                            dt.Columns.Add("TotalTarget", typeof(decimal));
                            dt.Columns.Add("PermitGen", typeof(int));
                            dt.Columns.Add("Deacivatepermit", typeof(int));
                            dt.Columns.Add("SuppUpdDealer", typeof(int));
                            dt.Columns.Add("SuppUpdateADAE", typeof(int));
                            dt.Columns.Add("PhyInsbyAc", typeof(int));
                            dt.Columns.Add("ConfbyADAE", typeof(int));
                            dt.Columns.Add("SubRelease", typeof(int));
                            dt.Columns.Add("SubReleaseamt", typeof(decimal));
                            dt.Columns.Add("Fin_Year", typeof(string));
                            dt.Columns.Add("rec_created_userid", typeof(string));
                            dt.Columns.Add("rec_created_date", typeof(DateTime));
                            foreach (var item in blk)
                            {
                                dt.Rows.Add(
                                Convert.ToInt32(item.block_code),
                                item.block_name,
                                Convert.ToInt32(item.totalapp),
                                Convert.ToInt32(item.finalapp),
                                Convert.ToInt32(item.VerifyAC),
                                Convert.ToInt32(item.VerifyBAO),
                                Convert.ToInt32(item.VerifyDAO),
                                Convert.ToDecimal(item.TotalTarget),
                                Convert.ToInt32(item.PermitGen),
                                Convert.ToInt32(item.Deacivatepermit),
                                Convert.ToInt32(item.SuppUpdDealer),
                                Convert.ToInt32(item.SuppUpdateADAE),
                                Convert.ToInt32(item.PhyInsbyAc),
                                Convert.ToInt32(item.ConfbyADAE),
                                Convert.ToInt32(item.SubRelease),
                                Convert.ToDecimal(item.SubReleaseamt),
                                Convert.ToString(item.F_year),
                                null,
                                DateTime.Now);
                            }

                            if (dt != null && dt.Rows.Count > 0)
                            {
                                result = this.miscService.InsertOFMASBGREIBlk(dt);
                            }
                        }
                        else
                        {
                            return this.NotFound();
                        }
                    }

                    if (result > 0)
                    {
                        return this.Ok();
                    }

                    return this.NotFound();
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
        /// InsertOFMAS BGREIRPTPAN.
        /// </summary>
        /// <returns>Action Result.</returns>
        [AllowAnonymous]
        [HttpPost("InsertOFMAS_BGREIRPTPAN")]
        public IActionResult InsertOFMAS_BGREIRPTPAN()
        {
            try
            {
                int result = 0;
                OFMASReportWebServiceSoapClient client = new OFMASReportWebServiceSoapClient(OFMASReportWebServiceSoapClient.EndpointConfiguration.OFMASReportWebServiceSoap);
                client.ClientCredentials.ServiceCertificate.SslCertificateAuthentication =
                new X509ServiceCertificateAuthentication()
                {
                    CertificateValidationMode = X509CertificateValidationMode.None,
                    RevocationMode = System.Security.Cryptography.X509Certificates.X509RevocationMode.NoCheck,
                };
                List<OFMAS_Scheme> scheme = client.getOFMAS_SchemeAsync(this.config.Value.OfmasPasscode).Result.ToList();

                if (scheme.Any())
                {
                    List<District> dist = this.miscRepository.GetdistrictCode();

                    if (dist.Any())
                    {
                        List<PanchayatData> blk = this.miscRepository.GetOfmasBlock();

                        if (blk.Any())
                        {
                            foreach (var sch in scheme)
                            {
                                if (sch.Scheme == "BGREI")
                                {
                                    foreach (var blks in blk)
                                    {
                                        OFMASReportWebServiceSoapClient clientPanch = new OFMASReportWebServiceSoapClient(OFMASReportWebServiceSoapClient.EndpointConfiguration.OFMASReportWebServiceSoap);
                                        clientPanch.ClientCredentials.ServiceCertificate.SslCertificateAuthentication =
                                        new X509ServiceCertificateAuthentication()
                                        {
                                            CertificateValidationMode = X509CertificateValidationMode.None,
                                            RevocationMode = System.Security.Cryptography.X509Certificates.X509RevocationMode.NoCheck,
                                        };
                                        var data = clientPanch.getOFMAS_SingleRptPANAsync(this.config.Value.OfmasPasscode, sch.F_Year, blks.Block_lg_code.ToString(), sch.Sid).Result.ToList();
                                        List<OFMAS_Single_RptPAN> panch = new List<OFMAS_Single_RptPAN>();
                                        if (data != null && data.Any())
                                        {
                                            panch.AddRange(data);
                                        }

                                        if (panch.Any())
                                        {
                                            DataTable dt = new DataTable();
                                            dt.Columns.Add("pancode", typeof(long));
                                            dt.Columns.Add("pan_name", typeof(string));
                                            dt.Columns.Add("totalapp", typeof(int));
                                            dt.Columns.Add("finalapp", typeof(int));
                                            dt.Columns.Add("VerifyAC", typeof(int));
                                            dt.Columns.Add("VerifyBAO", typeof(int));
                                            dt.Columns.Add("VerifyDAO", typeof(int));
                                            dt.Columns.Add("TotalTarget", typeof(decimal));
                                            dt.Columns.Add("PermitGen", typeof(int));
                                            dt.Columns.Add("Deacivatepermit", typeof(int));
                                            dt.Columns.Add("SuppUpdDealer", typeof(int));
                                            dt.Columns.Add("SuppUpdateADAE", typeof(int));
                                            dt.Columns.Add("PhyInsbyAc", typeof(int));
                                            dt.Columns.Add("ConfbyADAE", typeof(int));
                                            dt.Columns.Add("SubRelease", typeof(int));
                                            dt.Columns.Add("SubReleaseamt", typeof(decimal));
                                            dt.Columns.Add("Fin_Year", typeof(string));
                                            dt.Columns.Add("rec_created_userid", typeof(string));
                                            dt.Columns.Add("rec_created_date", typeof(DateTime));

                                            foreach (var item in panch)
                                            {
                                                dt.Rows.Add(
                                                Convert.ToInt64(item.pan_code),
                                                item.pan_name,
                                                Convert.ToInt32(item.totalapp),
                                                Convert.ToInt32(item.finalapp),
                                                Convert.ToInt32(item.VerifyAC),
                                                Convert.ToInt32(item.VerifyBAO),
                                                Convert.ToInt32(item.VerifyDAO),
                                                Convert.ToDecimal(item.TotalTarget),
                                                Convert.ToInt32(item.PermitGen),
                                                Convert.ToInt32(item.Deacivatepermit),
                                                Convert.ToInt32(item.SuppUpdDealer),
                                                Convert.ToInt32(item.SuppUpdateADAE),
                                                Convert.ToInt32(item.PhyInsbyAc),
                                                Convert.ToInt32(item.ConfbyADAE),
                                                Convert.ToInt32(item.SubRelease),
                                                Convert.ToDecimal(item.SubReleaseamt),
                                                Convert.ToString(item.F_year),
                                                null,
                                                DateTime.Now);
                                            }

                                            if (dt != null && dt.Rows.Count > 0)
                                            {
                                                result = result + this.miscService.InsertOFMASBGREIPAN(dt);
                                            }
                                        }
                                        else
                                        {
                                            return this.NotFound("No Panchayat Found");
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            return this.NotFound("No Block Found");
                        }
                    }
                    else
                    {
                        return this.NotFound("District Not Found");
                    }

                    if (result > 0)
                    {
                        return this.Ok(result);
                    }

                    return this.NotFound("No rows Affected");
                }
                else
                {
                    return this.NotFound("No Schemes Found");
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.Problem(ex.Message);
            }
        }

        /// <summary>
        /// InsertOFMAS NFSMRPTDist.
        /// </summary>
        /// <returns>Action Result.</returns>
        [AllowAnonymous]
        [HttpPost("InsertOFMAS_NFSMRPTDist")]

        public IActionResult InsertOFMAS_NFSMRPTDist()
        {
            try
            {
                int result = 0;

                OFMASReportWebServiceSoapClient client = new OFMASReportWebServiceSoapClient(OFMASReportWebServiceSoapClient.EndpointConfiguration.OFMASReportWebServiceSoap);
                client.ClientCredentials.ServiceCertificate.SslCertificateAuthentication =
                new X509ServiceCertificateAuthentication()
                {
                    CertificateValidationMode = X509CertificateValidationMode.None,
                    RevocationMode = System.Security.Cryptography.X509Certificates.X509RevocationMode.NoCheck,
                };
                List<OFMAS_Scheme> scheme = client.getOFMAS_SchemeAsync(this.config.Value.OfmasPasscode).Result.ToList();
                if (scheme.Any())
                {
                    List<OFMAS_Single_RptDist> dist = new List<OFMAS_Single_RptDist>();
                    foreach (var item in scheme)
                    {
                        if (item.Scheme == "NFSM")
                        {
                            var data = client.getOFMAS_SingleRptDistAsync(this.config.Value.OfmasPasscode, item.F_Year, item.Sid).Result.ToList();

                            if (data != null && data.Any())
                            {
                                dist.AddRange(data);
                            }
                        }
                    }

                    if (dist.Any())
                    {
                        DataTable dt = new DataTable();
                        dt.Columns.Add("District_lg_code", typeof(int));
                        dt.Columns.Add("District_name", typeof(string));
                        dt.Columns.Add("totalapp", typeof(int));
                        dt.Columns.Add("finalapp", typeof(int));
                        dt.Columns.Add("VerifyAC", typeof(int));
                        dt.Columns.Add("VerifyBAO", typeof(int));
                        dt.Columns.Add("VerifyDAO", typeof(int));
                        dt.Columns.Add("TotalTarget", typeof(decimal));
                        dt.Columns.Add("PermitGen", typeof(int));
                        dt.Columns.Add("Deacivatepermit", typeof(int));
                        dt.Columns.Add("SuppUpdDealer", typeof(int));
                        dt.Columns.Add("SuppUpdateADAE", typeof(int));
                        dt.Columns.Add("PhyInsbyAc", typeof(int));
                        dt.Columns.Add("ConfbyADAE", typeof(int));
                        dt.Columns.Add("SubRelease", typeof(int));
                        dt.Columns.Add("SubReleaseamt", typeof(decimal));
                        dt.Columns.Add("Fin_Year", typeof(string));
                        dt.Columns.Add("rec_created_userid", typeof(string));
                        dt.Columns.Add("rec_created_date", typeof(DateTime));

                        foreach (var item in dist)
                        {
                            dt.Rows.Add(
                                Convert.ToInt32(item.dist_code),
                                item.dist_name,
                                Convert.ToInt32(item.totalapp),
                                Convert.ToInt32(item.finalapp),
                                Convert.ToInt32(item.VerifyAC),
                                Convert.ToInt32(item.VerifyBAO),
                                Convert.ToInt32(item.VerifyDAO),
                                Convert.ToDecimal(item.TotalTarget),
                                Convert.ToInt32(item.PermitGen),
                                Convert.ToInt32(item.Deacivatepermit),
                                Convert.ToInt32(item.SuppUpdDealer),
                                Convert.ToInt32(item.SuppUpdateADAE),
                                Convert.ToInt32(item.PhyInsbyAc),
                                Convert.ToInt32(item.ConfbyADAE),
                                Convert.ToInt32(item.SubRelease),
                                Convert.ToDecimal(item.SubReleaseamt),
                                Convert.ToString(item.F_year),
                                null,
                                DateTime.Now);
                        }

                        if (dt != null && dt.Rows.Count > 0)
                        {
                            result = this.miscService.InsertOFMASNFSMDist(dt);
                        }
                    }

                    if (result > 0)
                    {
                        return this.Ok();
                    }

                    return this.NotFound();
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
        /// InsertOFMAS NFSMRPTBlk.
        /// </summary>
        /// <returns>Action Result.</returns>
        [AllowAnonymous]
        [HttpPost("InsertOFMAS_NFSMRPTBlk")]

        public IActionResult InsertOFMAS_NFSMRPTBlk()
        {
            try
            {
                int result = 0;

                OFMASReportWebServiceSoapClient client = new OFMASReportWebServiceSoapClient(OFMASReportWebServiceSoapClient.EndpointConfiguration.OFMASReportWebServiceSoap);
                client.ClientCredentials.ServiceCertificate.SslCertificateAuthentication =
                new X509ServiceCertificateAuthentication()
                {
                    CertificateValidationMode = X509CertificateValidationMode.None,
                    RevocationMode = System.Security.Cryptography.X509Certificates.X509RevocationMode.NoCheck,
                };
                List<OFMAS_Scheme> scheme = client.getOFMAS_SchemeAsync(this.config.Value.OfmasPasscode).Result.ToList();
                if (scheme.Any())
                {
                    List<OFMAS_Single_RptDist> dist = new List<OFMAS_Single_RptDist>();
                    foreach (var item in scheme)
                    {
                        if (item.Scheme == "NFSM")
                        {
                            var data = client.getOFMAS_SingleRptDistAsync(this.config.Value.OfmasPasscode, item.F_Year, item.Sid).Result.ToList();

                            if (data != null && data.Any())
                            {
                                dist.AddRange(data);
                            }
                        }
                    }

                    if (dist.Any())
                    {
                        List<OFMAS_Single_RptBlk> blk = new List<OFMAS_Single_RptBlk>();
                        foreach (var sch in scheme)
                        {
                            if (sch.Scheme == "NFSM")
                            {
                                foreach (var distrct in dist)
                                {
                                    var data = client.getOFMAS_SingleRptBlkAsync(this.config.Value.OfmasPasscode, sch.F_Year, distrct.dist_code, sch.Sid).Result.ToList();
                                    if (data != null && data.Any())
                                    {
                                        blk.AddRange(data);
                                    }
                                }
                            }
                        }

                        if (blk.Any())
                        {
                            DataTable dt = new DataTable();
                            dt.Columns.Add("block_lg_code", typeof(int));
                            dt.Columns.Add("block_name", typeof(string));
                            dt.Columns.Add("totalapp", typeof(int));
                            dt.Columns.Add("finalapp", typeof(int));
                            dt.Columns.Add("VerifyAC", typeof(int));
                            dt.Columns.Add("VerifyBAO", typeof(int));
                            dt.Columns.Add("VerifyDAO", typeof(int));
                            dt.Columns.Add("TotalTarget", typeof(decimal));
                            dt.Columns.Add("PermitGen", typeof(int));
                            dt.Columns.Add("Deacivatepermit", typeof(int));
                            dt.Columns.Add("SuppUpdDealer", typeof(int));
                            dt.Columns.Add("SuppUpdateADAE", typeof(int));
                            dt.Columns.Add("PhyInsbyAc", typeof(int));
                            dt.Columns.Add("ConfbyADAE", typeof(int));
                            dt.Columns.Add("SubRelease", typeof(int));
                            dt.Columns.Add("SubReleaseamt", typeof(decimal));
                            dt.Columns.Add("Fin_Year", typeof(string));
                            dt.Columns.Add("rec_created_userid", typeof(string));
                            dt.Columns.Add("rec_created_date", typeof(DateTime));
                            foreach (var item in blk)
                            {
                                dt.Rows.Add(
                                Convert.ToInt32(item.block_code),
                                item.block_name,
                                Convert.ToInt32(item.totalapp),
                                Convert.ToInt32(item.finalapp),
                                Convert.ToInt32(item.VerifyAC),
                                Convert.ToInt32(item.VerifyBAO),
                                Convert.ToInt32(item.VerifyDAO),
                                Convert.ToDecimal(item.TotalTarget),
                                Convert.ToInt32(item.PermitGen),
                                Convert.ToInt32(item.Deacivatepermit),
                                Convert.ToInt32(item.SuppUpdDealer),
                                Convert.ToInt32(item.SuppUpdateADAE),
                                Convert.ToInt32(item.PhyInsbyAc),
                                Convert.ToInt32(item.ConfbyADAE),
                                Convert.ToInt32(item.SubRelease),
                                Convert.ToDecimal(item.SubReleaseamt),
                                Convert.ToString(item.F_year),
                                null,
                                DateTime.Now);
                            }

                            if (dt != null && dt.Rows.Count > 0)
                            {
                                result = this.miscService.InsertOFMASNFSMBlk(dt);
                            }
                        }
                        else
                        {
                            return this.NotFound();
                        }
                    }

                    if (result > 0)
                    {
                        return this.Ok();
                    }

                    return this.NotFound();
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
        /// InsertOFMAS NFSMRPTPAN.
        /// </summary>
        /// <returns>Action Result.</returns>
        [AllowAnonymous]
        [HttpPost("InsertOFMAS_NFSMRPTPAN")]
        public IActionResult InsertOFMAS_NFSMRPTPAN()
        {
            try
            {
                int result = 0;
                OFMASReportWebServiceSoapClient client = new OFMASReportWebServiceSoapClient(OFMASReportWebServiceSoapClient.EndpointConfiguration.OFMASReportWebServiceSoap);
                client.ClientCredentials.ServiceCertificate.SslCertificateAuthentication =
                new X509ServiceCertificateAuthentication()
                {
                    CertificateValidationMode = X509CertificateValidationMode.None,
                    RevocationMode = System.Security.Cryptography.X509Certificates.X509RevocationMode.NoCheck,
                };
                List<OFMAS_Scheme> scheme = client.getOFMAS_SchemeAsync(this.config.Value.OfmasPasscode).Result.ToList();
                if (scheme.Any())
                {
                    List<District> dist = this.miscRepository.GetdistrictCode();

                    if (dist.Any())
                    {
                        List<PanchayatData> blk = this.miscRepository.GetOfmasBlock();

                        if (blk.Any())
                        {
                            foreach (var sch in scheme)
                            {
                                if (sch.Scheme == "NFSM")
                                {
                                    foreach (var blks in blk)
                                    {
                                        OFMASReportWebServiceSoapClient clientPanch = new OFMASReportWebServiceSoapClient(OFMASReportWebServiceSoapClient.EndpointConfiguration.OFMASReportWebServiceSoap);
                                        clientPanch.ClientCredentials.ServiceCertificate.SslCertificateAuthentication =
                                        new X509ServiceCertificateAuthentication()
                                        {
                                            CertificateValidationMode = X509CertificateValidationMode.None,
                                            RevocationMode = System.Security.Cryptography.X509Certificates.X509RevocationMode.NoCheck,
                                        };

                                        var data = clientPanch.getOFMAS_SingleRptPANAsync(this.config.Value.OfmasPasscode, sch.F_Year, blks.Block_lg_code.ToString(), sch.Sid).Result.ToList();
                                        List<OFMAS_Single_RptPAN> panch = new List<OFMAS_Single_RptPAN>();
                                        if (data != null && data.Any())
                                        {
                                            panch.AddRange(data);
                                        }

                                        if (panch.Any())
                                        {
                                            DataTable dt = new DataTable();
                                            dt.Columns.Add("pancode", typeof(long));
                                            dt.Columns.Add("pan_name", typeof(string));
                                            dt.Columns.Add("totalapp", typeof(int));
                                            dt.Columns.Add("finalapp", typeof(int));
                                            dt.Columns.Add("VerifyAC", typeof(int));
                                            dt.Columns.Add("VerifyBAO", typeof(int));
                                            dt.Columns.Add("VerifyDAO", typeof(int));
                                            dt.Columns.Add("TotalTarget", typeof(decimal));
                                            dt.Columns.Add("PermitGen", typeof(int));
                                            dt.Columns.Add("Deacivatepermit", typeof(int));
                                            dt.Columns.Add("SuppUpdDealer", typeof(int));
                                            dt.Columns.Add("SuppUpdateADAE", typeof(int));
                                            dt.Columns.Add("PhyInsbyAc", typeof(int));
                                            dt.Columns.Add("ConfbyADAE", typeof(int));
                                            dt.Columns.Add("SubRelease", typeof(int));
                                            dt.Columns.Add("SubReleaseamt", typeof(decimal));
                                            dt.Columns.Add("Fin_Year", typeof(string));
                                            dt.Columns.Add("rec_created_userid", typeof(string));
                                            dt.Columns.Add("rec_created_date", typeof(DateTime));

                                            foreach (var item in panch)
                                            {
                                                dt.Rows.Add(
                                                Convert.ToInt64(item.pan_code),
                                                item.pan_name,
                                                Convert.ToInt32(item.totalapp),
                                                Convert.ToInt32(item.finalapp),
                                                Convert.ToInt32(item.VerifyAC),
                                                Convert.ToInt32(item.VerifyBAO),
                                                Convert.ToInt32(item.VerifyDAO),
                                                Convert.ToDecimal(item.TotalTarget),
                                                Convert.ToInt32(item.PermitGen),
                                                Convert.ToInt32(item.Deacivatepermit),
                                                Convert.ToInt32(item.SuppUpdDealer),
                                                Convert.ToInt32(item.SuppUpdateADAE),
                                                Convert.ToInt32(item.PhyInsbyAc),
                                                Convert.ToInt32(item.ConfbyADAE),
                                                Convert.ToInt32(item.SubRelease),
                                                Convert.ToDecimal(item.SubReleaseamt),
                                                Convert.ToString(item.F_year),
                                                null,
                                                DateTime.Now);
                                            }

                                            if (dt != null && dt.Rows.Count > 0)
                                            {
                                                result = result + this.miscService.InsertOFMASNFSMPAN(dt);
                                            }
                                        }
                                        else
                                        {
                                            return this.NotFound("No Panchayat Found");
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            return this.NotFound("No Block Found");
                        }
                    }
                    else
                    {
                        return this.NotFound("District Not Found");
                    }
                    if (result > 0)
                    {
                        return this.Ok(result);
                    }

                    return this.NotFound("No rows Affected");
                }
                else
                {
                    return this.NotFound("No Schemes Found");
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.Problem(ex.Message);
            }
        }

        /// <summary>
        /// InsertOFMAS NFSMOILSEEDSRPTDist.
        /// </summary>
        /// <returns>Action Result.</returns>
        [AllowAnonymous]
        [HttpPost("InsertOFMAS_NFSMOILSEEDSRPTDist")]

        public IActionResult InsertOFMAS_NFSMOILSEEDSRPTDist()
        {
            try
            {
                int result = 0;

                OFMASReportWebServiceSoapClient client = new OFMASReportWebServiceSoapClient(OFMASReportWebServiceSoapClient.EndpointConfiguration.OFMASReportWebServiceSoap);
                client.ClientCredentials.ServiceCertificate.SslCertificateAuthentication =
                new X509ServiceCertificateAuthentication()
                {
                    CertificateValidationMode = X509CertificateValidationMode.None,
                    RevocationMode = System.Security.Cryptography.X509Certificates.X509RevocationMode.NoCheck,
                };
                List<OFMAS_Scheme> scheme = client.getOFMAS_SchemeAsync(this.config.Value.OfmasPasscode).Result.ToList();
                if (scheme.Any())
                {
                    List<OFMAS_Single_RptDist> dist = new List<OFMAS_Single_RptDist>();
                    foreach (var item in scheme)
                    {
                        if (item.Scheme == "NFSM-oilseeds")
                        {
                            var data = client.getOFMAS_SingleRptDistAsync(this.config.Value.OfmasPasscode, item.F_Year, item.Sid).Result.ToList();

                            if (data != null && data.Any())
                            {
                                dist.AddRange(data);
                            }
                        }
                    }

                    if (dist.Any())
                    {
                        DataTable dt = new DataTable();
                        dt.Columns.Add("District_lg_code", typeof(int));
                        dt.Columns.Add("District_name", typeof(string));
                        dt.Columns.Add("totalapp", typeof(int));
                        dt.Columns.Add("finalapp", typeof(int));
                        dt.Columns.Add("VerifyAC", typeof(int));
                        dt.Columns.Add("VerifyBAO", typeof(int));
                        dt.Columns.Add("VerifyDAO", typeof(int));
                        dt.Columns.Add("TotalTarget", typeof(decimal));
                        dt.Columns.Add("PermitGen", typeof(int));
                        dt.Columns.Add("Deacivatepermit", typeof(int));
                        dt.Columns.Add("SuppUpdDealer", typeof(int));
                        dt.Columns.Add("SuppUpdateADAE", typeof(int));
                        dt.Columns.Add("PhyInsbyAc", typeof(int));
                        dt.Columns.Add("ConfbyADAE", typeof(int));
                        dt.Columns.Add("SubRelease", typeof(int));
                        dt.Columns.Add("SubReleaseamt", typeof(decimal));
                        dt.Columns.Add("Fin_Year", typeof(string));
                        dt.Columns.Add("rec_created_userid", typeof(string));
                        dt.Columns.Add("rec_created_date", typeof(DateTime));

                        foreach (var item in dist)
                        {
                            dt.Rows.Add(
                                Convert.ToInt32(item.dist_code),
                                item.dist_name,
                                Convert.ToInt32(item.totalapp),
                                Convert.ToInt32(item.finalapp),
                                Convert.ToInt32(item.VerifyAC),
                                Convert.ToInt32(item.VerifyBAO),
                                Convert.ToInt32(item.VerifyDAO),
                                Convert.ToDecimal(item.TotalTarget),
                                Convert.ToInt32(item.PermitGen),
                                Convert.ToInt32(item.Deacivatepermit),
                                Convert.ToInt32(item.SuppUpdDealer),
                                Convert.ToInt32(item.SuppUpdateADAE),
                                Convert.ToInt32(item.PhyInsbyAc),
                                Convert.ToInt32(item.ConfbyADAE),
                                Convert.ToInt32(item.SubRelease),
                                Convert.ToDecimal(item.SubReleaseamt),
                                Convert.ToString(item.F_year),
                                null,
                                DateTime.Now);
                        }

                        if (dt != null && dt.Rows.Count > 0)
                        {
                            result = this.miscService.InsertOFMASNFSMOILSEEDSDist(dt);
                        }
                    }

                    if (result > 0)
                    {
                        return this.Ok();
                    }

                    return this.NotFound();
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
        /// InsertOFMAS NFSMOILSEEDSRPTBlk.
        /// </summary>
        /// <returns> Action Result.</returns>
        [AllowAnonymous]
        [HttpPost("InsertOFMAS_NFSMOILSEEDSRPTBlk")]
        public IActionResult InsertOFMAS_NFSMOILSEEDSRPTBlk()
        {
            try
            {
                int result = 0;

                OFMASReportWebServiceSoapClient client = new OFMASReportWebServiceSoapClient(OFMASReportWebServiceSoapClient.EndpointConfiguration.OFMASReportWebServiceSoap);
                client.ClientCredentials.ServiceCertificate.SslCertificateAuthentication =
                new X509ServiceCertificateAuthentication()
                {
                    CertificateValidationMode = X509CertificateValidationMode.None,
                    RevocationMode = System.Security.Cryptography.X509Certificates.X509RevocationMode.NoCheck,
                };
                List<OFMAS_Scheme> scheme = client.getOFMAS_SchemeAsync(this.config.Value.OfmasPasscode).Result.ToList();

                if (scheme.Any())
                {
                    List<OFMAS_Single_RptDist> dist = new List<OFMAS_Single_RptDist>();
                    foreach (var item in scheme)
                    {
                        if (item.Scheme == "NFSM-oilseeds")
                        {
                            var data = client.getOFMAS_SingleRptDistAsync(this.config.Value.OfmasPasscode, item.F_Year, item.Sid).Result.ToList();

                            if (data != null && data.Any())
                            {
                                dist.AddRange(data);
                            }
                        }
                    }

                    if (dist.Any())
                    {
                        List<OFMAS_Single_RptBlk> blk = new List<OFMAS_Single_RptBlk>();
                        foreach (var sch in scheme)
                        {
                            if (sch.Scheme == "NFSM-oilseeds")
                            {
                                foreach (var distrct in dist)
                                {
                                    var data = client.getOFMAS_SingleRptBlkAsync(this.config.Value.OfmasPasscode, sch.F_Year, distrct.dist_code, sch.Sid).Result.ToList();
                                    if (data != null && data.Any())
                                    {
                                        blk.AddRange(data);
                                    }
                                }
                            }
                        }

                        if (blk.Any())
                        {
                            DataTable dt = new DataTable();
                            dt.Columns.Add("block_lg_code", typeof(int));
                            dt.Columns.Add("block_name", typeof(string));
                            dt.Columns.Add("totalapp", typeof(int));
                            dt.Columns.Add("finalapp", typeof(int));
                            dt.Columns.Add("VerifyAC", typeof(int));
                            dt.Columns.Add("VerifyBAO", typeof(int));
                            dt.Columns.Add("VerifyDAO", typeof(int));
                            dt.Columns.Add("TotalTarget", typeof(decimal));
                            dt.Columns.Add("PermitGen", typeof(int));
                            dt.Columns.Add("Deacivatepermit", typeof(int));
                            dt.Columns.Add("SuppUpdDealer", typeof(int));
                            dt.Columns.Add("SuppUpdateADAE", typeof(int));
                            dt.Columns.Add("PhyInsbyAc", typeof(int));
                            dt.Columns.Add("ConfbyADAE", typeof(int));
                            dt.Columns.Add("SubRelease", typeof(int));
                            dt.Columns.Add("SubReleaseamt", typeof(decimal));
                            dt.Columns.Add("Fin_Year", typeof(string));
                            dt.Columns.Add("rec_created_userid", typeof(string));
                            dt.Columns.Add("rec_created_date", typeof(DateTime));

                            foreach (var item in blk)
                            {
                                dt.Rows.Add(
                                Convert.ToInt32(item.block_code),
                                item.block_name,
                                Convert.ToInt32(item.totalapp),
                                Convert.ToInt32(item.finalapp),
                                Convert.ToInt32(item.VerifyAC),
                                Convert.ToInt32(item.VerifyBAO),
                                Convert.ToInt32(item.VerifyDAO),
                                Convert.ToDecimal(item.TotalTarget),
                                Convert.ToInt32(item.PermitGen),
                                Convert.ToInt32(item.Deacivatepermit),
                                Convert.ToInt32(item.SuppUpdDealer),
                                Convert.ToInt32(item.SuppUpdateADAE),
                                Convert.ToInt32(item.PhyInsbyAc),
                                Convert.ToInt32(item.ConfbyADAE),
                                Convert.ToInt32(item.SubRelease),
                                Convert.ToDecimal(item.SubReleaseamt),
                                Convert.ToString(item.F_year),
                                null,
                                DateTime.Now);
                            }

                            if (dt != null && dt.Rows.Count > 0)
                            {
                                result = this.miscService.InsertOFMASNFSMOILSEEDSBlk(dt);
                            }
                        }
                        else
                        {
                            return this.NotFound();
                        }
                    }

                    if (result > 0)
                    {
                        return this.Ok();
                    }

                    return this.NotFound();
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
        /// InsertOFMAS NFSMOILSEEDSRPTPAN.
        /// </summary>
        /// <returns>Action Result.</returns>
        [AllowAnonymous]
        [HttpPost("InsertOFMAS_NFSMOILSEEDSRPTPAN")]
        public IActionResult InsertOFMAS_NFSMOILSEEDSRPTPAN()
        {
            try
            {
                int result = 0;
                OFMASReportWebServiceSoapClient client = new OFMASReportWebServiceSoapClient(OFMASReportWebServiceSoapClient.EndpointConfiguration.OFMASReportWebServiceSoap);
                client.ClientCredentials.ServiceCertificate.SslCertificateAuthentication =
                new X509ServiceCertificateAuthentication()
                {
                    CertificateValidationMode = X509CertificateValidationMode.None,
                    RevocationMode = System.Security.Cryptography.X509Certificates.X509RevocationMode.NoCheck,
                };
                List<OFMAS_Scheme> scheme = client.getOFMAS_SchemeAsync(this.config.Value.OfmasPasscode).Result.ToList();
                if (scheme.Any())
                {
                    List<District> dist = this.miscRepository.GetdistrictCode();

                    if (dist.Any())
                    {
                        List<PanchayatData> blk = this.miscRepository.GetOfmasBlock();

                        if (blk.Any())
                        {
                            foreach (var sch in scheme)
                            {
                                if (sch.Scheme == "NFSM-oilseeds")
                                {
                                    foreach (var blks in blk)
                                    {
                                        OFMASReportWebServiceSoapClient clientPanch = new OFMASReportWebServiceSoapClient(OFMASReportWebServiceSoapClient.EndpointConfiguration.OFMASReportWebServiceSoap);
                                        clientPanch.ClientCredentials.ServiceCertificate.SslCertificateAuthentication =
                                        new X509ServiceCertificateAuthentication()
                                        {
                                            CertificateValidationMode = X509CertificateValidationMode.None,
                                            RevocationMode = System.Security.Cryptography.X509Certificates.X509RevocationMode.NoCheck,
                                        };

                                        var data = clientPanch.getOFMAS_SingleRptPANAsync(this.config.Value.OfmasPasscode, sch.F_Year, blks.Block_lg_code.ToString(), sch.Sid).Result.ToList();
                                        List<OFMAS_Single_RptPAN> panch = new List<OFMAS_Single_RptPAN>();
                                        if (data != null && data.Any())
                                        {
                                            panch.AddRange(data);
                                        }

                                        if (panch.Any())
                                        {
                                            DataTable dt = new DataTable();
                                            dt.Columns.Add("pancode", typeof(long));
                                            dt.Columns.Add("pan_name", typeof(string));
                                            dt.Columns.Add("totalapp", typeof(int));
                                            dt.Columns.Add("finalapp", typeof(int));
                                            dt.Columns.Add("VerifyAC", typeof(int));
                                            dt.Columns.Add("VerifyBAO", typeof(int));
                                            dt.Columns.Add("VerifyDAO", typeof(int));
                                            dt.Columns.Add("TotalTarget", typeof(decimal));
                                            dt.Columns.Add("PermitGen", typeof(int));
                                            dt.Columns.Add("Deacivatepermit", typeof(int));
                                            dt.Columns.Add("SuppUpdDealer", typeof(int));
                                            dt.Columns.Add("SuppUpdateADAE", typeof(int));
                                            dt.Columns.Add("PhyInsbyAc", typeof(int));
                                            dt.Columns.Add("ConfbyADAE", typeof(int));
                                            dt.Columns.Add("SubRelease", typeof(int));
                                            dt.Columns.Add("SubReleaseamt", typeof(decimal));
                                            dt.Columns.Add("Fin_Year", typeof(string));
                                            dt.Columns.Add("rec_created_userid", typeof(string));
                                            dt.Columns.Add("rec_created_date", typeof(DateTime));

                                            foreach (var item in panch)
                                            {
                                                dt.Rows.Add(
                                                Convert.ToInt64(item.pan_code),
                                                item.pan_name,
                                                Convert.ToInt32(item.totalapp),
                                                Convert.ToInt32(item.finalapp),
                                                Convert.ToInt32(item.VerifyAC),
                                                Convert.ToInt32(item.VerifyBAO),
                                                Convert.ToInt32(item.VerifyDAO),
                                                Convert.ToDecimal(item.TotalTarget),
                                                Convert.ToInt32(item.PermitGen),
                                                Convert.ToInt32(item.Deacivatepermit),
                                                Convert.ToInt32(item.SuppUpdDealer),
                                                Convert.ToInt32(item.SuppUpdateADAE),
                                                Convert.ToInt32(item.PhyInsbyAc),
                                                Convert.ToInt32(item.ConfbyADAE),
                                                Convert.ToInt32(item.SubRelease),
                                                Convert.ToDecimal(item.SubReleaseamt),
                                                Convert.ToString(item.F_year),
                                                null,
                                                DateTime.Now);
                                            }

                                            if (dt != null && dt.Rows.Count > 0)
                                            {
                                                result = result + this.miscService.InsertOFMASNFSMOILSEEDSPAN(dt);
                                            }
                                        }
                                        else
                                        {
                                            return this.NotFound("No Panchayat Found");
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            return this.NotFound("No Block Found");
                        }
                    }
                    else
                    {
                        return this.NotFound("District Not Found");
                    }
                    if (result > 0)
                    {
                        return this.Ok(result);
                    }
                    return this.NotFound("No rows Affected");
                }
                else
                {
                    return this.NotFound("No Schemes Found");
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.Problem(ex.Message);
            }
        }

        /// <summary>
        /// InsertOFMAS SMAMRPTDist.
        /// </summary>
        /// <returns>Action Result.</returns>
        [AllowAnonymous]
        [HttpPost("InsertOFMAS_SMAMRPTDist")]

        public IActionResult InsertOFMAS_SMAMRPTDist()
        {
            try
            {
                int result = 0;

                OFMASReportWebServiceSoapClient client = new OFMASReportWebServiceSoapClient(OFMASReportWebServiceSoapClient.EndpointConfiguration.OFMASReportWebServiceSoap);
                client.ClientCredentials.ServiceCertificate.SslCertificateAuthentication =
                new X509ServiceCertificateAuthentication()
                {
                    CertificateValidationMode = X509CertificateValidationMode.None,
                    RevocationMode = System.Security.Cryptography.X509Certificates.X509RevocationMode.NoCheck,
                };
                List<OFMAS_Scheme> scheme = client.getOFMAS_SchemeAsync(this.config.Value.OfmasPasscode).Result.ToList();

                if (scheme.Any())
                {
                    List<OFMAS_Single_RptDist> dist = new List<OFMAS_Single_RptDist>();
                    foreach (var item in scheme)
                    {
                        if (item.Scheme == "SMAM(21-22)Cont22-23")
                        {
                            var data = client.getOFMAS_SingleRptDistAsync(this.config.Value.OfmasPasscode, item.F_Year, item.Sid).Result.ToList();

                            if (data != null && data.Any())
                            {
                                dist.AddRange(data);
                            }
                        }
                    }

                    if (dist.Any())
                    {
                        DataTable dt = new DataTable();
                        dt.Columns.Add("District_lg_code", typeof(int));
                        dt.Columns.Add("District_name", typeof(string));
                        dt.Columns.Add("totalapp", typeof(int));
                        dt.Columns.Add("finalapp", typeof(int));
                        dt.Columns.Add("VerifyAC", typeof(int));
                        dt.Columns.Add("VerifyBAO", typeof(int));
                        dt.Columns.Add("VerifyDAO", typeof(int));
                        dt.Columns.Add("TotalTarget", typeof(decimal));
                        dt.Columns.Add("PermitGen", typeof(int));
                        dt.Columns.Add("Deacivatepermit", typeof(int));
                        dt.Columns.Add("SuppUpdDealer", typeof(int));
                        dt.Columns.Add("SuppUpdateADAE", typeof(int));
                        dt.Columns.Add("PhyInsbyAc", typeof(int));
                        dt.Columns.Add("ConfbyADAE", typeof(int));
                        dt.Columns.Add("SubRelease", typeof(int));
                        dt.Columns.Add("SubReleaseamt", typeof(decimal));
                        dt.Columns.Add("Fin_Year", typeof(string));
                        dt.Columns.Add("rec_created_userid", typeof(string));
                        dt.Columns.Add("rec_created_date", typeof(DateTime));

                        foreach (var item in dist)
                        {
                            dt.Rows.Add(
                                Convert.ToInt32(item.dist_code),
                                item.dist_name,
                                Convert.ToInt32(item.totalapp),
                                Convert.ToInt32(item.finalapp),
                                Convert.ToInt32(item.VerifyAC),
                                Convert.ToInt32(item.VerifyBAO),
                                Convert.ToInt32(item.VerifyDAO),
                                Convert.ToDecimal(item.TotalTarget),
                                Convert.ToInt32(item.PermitGen),
                                Convert.ToInt32(item.Deacivatepermit),
                                Convert.ToInt32(item.SuppUpdDealer),
                                Convert.ToInt32(item.SuppUpdateADAE),
                                Convert.ToInt32(item.PhyInsbyAc),
                                Convert.ToInt32(item.ConfbyADAE),
                                Convert.ToInt32(item.SubRelease),
                                Convert.ToDecimal(item.SubReleaseamt),
                                Convert.ToString(item.F_year),
                                null,
                                DateTime.Now);
                        }

                        if (dt != null && dt.Rows.Count > 0)
                        {
                            result = this.miscService.InsertOFMASSMAMDist(dt);
                        }
                    }

                    if (result > 0)
                    {
                        return this.Ok();
                    }

                    return this.NotFound();
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
        /// InsertOFMAS SMAMRPTBlk.
        /// </summary>
        /// <returns>Action Result.</returns>
        [AllowAnonymous]
        [HttpPost("InsertOFMAS_SMAMRPTBlk")]

        public IActionResult InsertOFMAS_SMAMRPTBlk()
        {
            try
            {
                int result = 0;

                OFMASReportWebServiceSoapClient client = new OFMASReportWebServiceSoapClient(OFMASReportWebServiceSoapClient.EndpointConfiguration.OFMASReportWebServiceSoap);
                client.ClientCredentials.ServiceCertificate.SslCertificateAuthentication =
                new X509ServiceCertificateAuthentication()
                {
                    CertificateValidationMode = X509CertificateValidationMode.None,
                    RevocationMode = System.Security.Cryptography.X509Certificates.X509RevocationMode.NoCheck,
                };
                List<OFMAS_Scheme> scheme = client.getOFMAS_SchemeAsync(this.config.Value.OfmasPasscode).Result.ToList();
                if (scheme.Any())
                {
                    List<OFMAS_Single_RptDist> dist = new List<OFMAS_Single_RptDist>();
                    foreach (var item in scheme)
                    {
                        if (item.Scheme == "SMAM(21-22)Cont22-23")
                        {
                            var data = client.getOFMAS_SingleRptDistAsync(this.config.Value.OfmasPasscode, item.F_Year, item.Sid).Result.ToList();

                            if (data != null && data.Any())
                            {
                                dist.AddRange(data);
                            }
                        }
                    }

                    if (dist.Any())
                    {
                        List<OFMAS_Single_RptBlk> blk = new List<OFMAS_Single_RptBlk>();
                        foreach (var sch in scheme)
                        {
                            if (sch.Scheme == "SMAM(21-22)Cont22-23")
                            {
                                foreach (var distrct in dist)
                                {
                                    var data = client.getOFMAS_SingleRptBlkAsync(this.config.Value.OfmasPasscode, sch.F_Year, distrct.dist_code, sch.Sid).Result.ToList();
                                    if (data != null && data.Any())
                                    {
                                        blk.AddRange(data);
                                    }
                                }
                            }
                        }

                        if (blk.Any())
                        {
                            DataTable dt = new DataTable();
                            dt.Columns.Add("block_lg_code", typeof(int));
                            dt.Columns.Add("block_name", typeof(string));
                            dt.Columns.Add("totalapp", typeof(int));
                            dt.Columns.Add("finalapp", typeof(int));
                            dt.Columns.Add("VerifyAC", typeof(int));
                            dt.Columns.Add("VerifyBAO", typeof(int));
                            dt.Columns.Add("VerifyDAO", typeof(int));
                            dt.Columns.Add("TotalTarget", typeof(decimal));
                            dt.Columns.Add("PermitGen", typeof(int));
                            dt.Columns.Add("Deacivatepermit", typeof(int));
                            dt.Columns.Add("SuppUpdDealer", typeof(int));
                            dt.Columns.Add("SuppUpdateADAE", typeof(int));
                            dt.Columns.Add("PhyInsbyAc", typeof(int));
                            dt.Columns.Add("ConfbyADAE", typeof(int));
                            dt.Columns.Add("SubRelease", typeof(int));
                            dt.Columns.Add("SubReleaseamt", typeof(decimal));
                            dt.Columns.Add("Fin_Year", typeof(string));
                            dt.Columns.Add("rec_created_userid", typeof(string));
                            dt.Columns.Add("rec_created_date", typeof(DateTime));
                            foreach (var item in blk)
                            {
                                dt.Rows.Add(
                                Convert.ToInt32(item.block_code),
                                item.block_name,
                                Convert.ToInt32(item.totalapp),
                                Convert.ToInt32(item.finalapp),
                                Convert.ToInt32(item.VerifyAC),
                                Convert.ToInt32(item.VerifyBAO),
                                Convert.ToInt32(item.VerifyDAO),
                                Convert.ToDecimal(item.TotalTarget),
                                Convert.ToInt32(item.PermitGen),
                                Convert.ToInt32(item.Deacivatepermit),
                                Convert.ToInt32(item.SuppUpdDealer),
                                Convert.ToInt32(item.SuppUpdateADAE),
                                Convert.ToInt32(item.PhyInsbyAc),
                                Convert.ToInt32(item.ConfbyADAE),
                                Convert.ToInt32(item.SubRelease),
                                Convert.ToDecimal(item.SubReleaseamt),
                                Convert.ToString(item.F_year),
                                null,
                                DateTime.Now);
                            }

                            if (dt != null && dt.Rows.Count > 0)
                            {
                                result = this.miscService.InsertOFMASSMAMBlk(dt);
                            }
                        }
                        else
                        {
                            return this.NotFound();
                        }
                    }

                    if (result > 0)
                    {
                        return this.Ok();
                    }

                    return this.NotFound();
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
        /// InsertOFMAS SMAMRPTPAN.
        /// </summary>
        /// <returns>Action Result.</returns>
        [AllowAnonymous]
        [HttpPost("InsertOFMAS_SMAMRPTPAN")]
        public IActionResult InsertOFMAS_SMAMRPTPAN()
        {
            try
            {
                int result = 0;
                OFMASReportWebServiceSoapClient client = new OFMASReportWebServiceSoapClient(OFMASReportWebServiceSoapClient.EndpointConfiguration.OFMASReportWebServiceSoap);
                client.ClientCredentials.ServiceCertificate.SslCertificateAuthentication =
                new X509ServiceCertificateAuthentication()
                {
                    CertificateValidationMode = X509CertificateValidationMode.None,
                    RevocationMode = System.Security.Cryptography.X509Certificates.X509RevocationMode.NoCheck,
                };
                List<OFMAS_Scheme> scheme = client.getOFMAS_SchemeAsync(this.config.Value.OfmasPasscode).Result.ToList();
                if (scheme.Any())
                {
                    List<District> dist = this.miscRepository.GetdistrictCode();

                    if (dist.Any())
                    {
                        List<PanchayatData> blk = this.miscRepository.GetOfmasBlock();

                        if (blk.Any())
                        {
                            foreach (var sch in scheme)
                            {
                                if (sch.Scheme == "SMAM(21-22)Cont22-23")
                                {
                                    foreach (var blks in blk)
                                    {
                                        OFMASReportWebServiceSoapClient clientPanch = new OFMASReportWebServiceSoapClient(OFMASReportWebServiceSoapClient.EndpointConfiguration.OFMASReportWebServiceSoap);
                                        clientPanch.ClientCredentials.ServiceCertificate.SslCertificateAuthentication =
                                        new X509ServiceCertificateAuthentication()
                                        {
                                            CertificateValidationMode = X509CertificateValidationMode.None,
                                            RevocationMode = System.Security.Cryptography.X509Certificates.X509RevocationMode.NoCheck,
                                        };

                                        var data = clientPanch.getOFMAS_SingleRptPANAsync(this.config.Value.OfmasPasscode, sch.F_Year, blks.Block_lg_code.ToString(), sch.Sid).Result.ToList();
                                        List<OFMAS_Single_RptPAN> panch = new List<OFMAS_Single_RptPAN>();
                                        if (data != null && data.Any())
                                        {
                                            panch.AddRange(data);
                                        }

                                        if (panch.Any())
                                        {
                                            DataTable dt = new DataTable();
                                            dt.Columns.Add("pancode", typeof(long));
                                            dt.Columns.Add("pan_name", typeof(string));
                                            dt.Columns.Add("totalapp", typeof(int));
                                            dt.Columns.Add("finalapp", typeof(int));
                                            dt.Columns.Add("VerifyAC", typeof(int));
                                            dt.Columns.Add("VerifyBAO", typeof(int));
                                            dt.Columns.Add("VerifyDAO", typeof(int));
                                            dt.Columns.Add("TotalTarget", typeof(decimal));
                                            dt.Columns.Add("PermitGen", typeof(int));
                                            dt.Columns.Add("Deacivatepermit", typeof(int));
                                            dt.Columns.Add("SuppUpdDealer", typeof(int));
                                            dt.Columns.Add("SuppUpdateADAE", typeof(int));
                                            dt.Columns.Add("PhyInsbyAc", typeof(int));
                                            dt.Columns.Add("ConfbyADAE", typeof(int));
                                            dt.Columns.Add("SubRelease", typeof(int));
                                            dt.Columns.Add("SubReleaseamt", typeof(decimal));
                                            dt.Columns.Add("Fin_Year", typeof(string));
                                            dt.Columns.Add("rec_created_userid", typeof(string));
                                            dt.Columns.Add("rec_created_date", typeof(DateTime));

                                            foreach (var item in panch)
                                            {
                                                dt.Rows.Add(
                                                Convert.ToInt64(item.pan_code),
                                                item.pan_name,
                                                Convert.ToInt32(item.totalapp),
                                                Convert.ToInt32(item.finalapp),
                                                Convert.ToInt32(item.VerifyAC),
                                                Convert.ToInt32(item.VerifyBAO),
                                                Convert.ToInt32(item.VerifyDAO),
                                                Convert.ToDecimal(item.TotalTarget),
                                                Convert.ToInt32(item.PermitGen),
                                                Convert.ToInt32(item.Deacivatepermit),
                                                Convert.ToInt32(item.SuppUpdDealer),
                                                Convert.ToInt32(item.SuppUpdateADAE),
                                                Convert.ToInt32(item.PhyInsbyAc),
                                                Convert.ToInt32(item.ConfbyADAE),
                                                Convert.ToInt32(item.SubRelease),
                                                Convert.ToDecimal(item.SubReleaseamt),
                                                Convert.ToString(item.F_year),
                                                null,
                                                DateTime.Now);
                                            }

                                            if (dt != null && dt.Rows.Count > 0)
                                            {
                                                result = result + this.miscService.InsertOFMASSMAMPAN(dt);
                                            }
                                        }
                                        else
                                        {
                                            return this.NotFound("No Panchayat Found");
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            return this.NotFound("No Block Found");
                        }
                    }
                    else
                    {
                        return this.NotFound("District Not Found");
                    }
                    if (result > 0)
                    {
                        return this.Ok(result);
                    }
                    return this.NotFound("No rows Affected");
                }
                else
                {
                    return this.NotFound("No Schemes Found");
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.Problem(ex.Message);
            }
        }

        /// <summary>
        /// InsertOFMAS SMAMSCHCRPTDist.
        /// </summary>
        /// <returns>Action Result.</returns>
        [AllowAnonymous]
        [HttpPost("InsertOFMAS_SMAMSCHCRPTDist")]

        public IActionResult InsertOFMAS_SMAMSCHCRPTDist()
        {
            try
            {
                int result = 0;

                OFMASReportWebServiceSoapClient client = new OFMASReportWebServiceSoapClient(OFMASReportWebServiceSoapClient.EndpointConfiguration.OFMASReportWebServiceSoap);
                client.ClientCredentials.ServiceCertificate.SslCertificateAuthentication =
                new X509ServiceCertificateAuthentication()
                {
                    CertificateValidationMode = X509CertificateValidationMode.None,
                    RevocationMode = System.Security.Cryptography.X509Certificates.X509RevocationMode.NoCheck,
                };
                List<OFMAS_Scheme> scheme = client.getOFMAS_SchemeAsync(this.config.Value.OfmasPasscode).Result.ToList();
                if (scheme.Any())
                {
                    List<OFMAS_SCHC_RptDist> dist = new List<OFMAS_SCHC_RptDist>();
                    foreach (var item in scheme)
                    {
                        if (item.Scheme == "SMAM-SCHC")
                        {
                            var data = client.getOFMAS_SCHCRptDistAsync(this.config.Value.OfmasPasscode, item.F_Year, Convert.ToInt64(item.Phase), Convert.ToInt64(item.Sid)).Result.ToList();

                            if (data != null && data.Any())
                            {
                                dist.AddRange(data);
                            }
                        }
                    }

                    if (dist.Any())
                    {
                        DataTable dt = new DataTable();
                        dt.Columns.Add("District_lg_code", typeof(int));
                        dt.Columns.Add("District_name", typeof(string));
                        dt.Columns.Add("registered_usr", typeof(int));
                        dt.Columns.Add("total_appln", typeof(int));
                        dt.Columns.Add("final_appln", typeof(int));
                        dt.Columns.Add("pending_AC", typeof(int));
                        dt.Columns.Add("Verify_AC", typeof(int));
                        dt.Columns.Add("pending_BAO", typeof(int));
                        dt.Columns.Add("Verify_BAO", typeof(int));
                        dt.Columns.Add("Pending_ADAE", typeof(int));
                        dt.Columns.Add("Verify_ADAE", typeof(int));
                        dt.Columns.Add("Reject_ADAE", typeof(int));
                        dt.Columns.Add("Pending_DLEC", typeof(int));
                        dt.Columns.Add("Verify_DLEC", typeof(int));
                        dt.Columns.Add("Reject_DLEC", typeof(int));
                        dt.Columns.Add("Target_District", typeof(decimal));
                        dt.Columns.Add("PermitGen", typeof(int));
                        dt.Columns.Add("SuppUpdDealer", typeof(int));
                        dt.Columns.Add("Confby_Grp_Indv", typeof(int));
                        dt.Columns.Add("PhyInsby_BAO", typeof(int));
                        dt.Columns.Add("Total_Claim_Gen", typeof(int));
                        dt.Columns.Add("Claim_ConfBy_ADAE", typeof(int));
                        dt.Columns.Add("Subsidy_Rel", typeof(int));
                        dt.Columns.Add("Subsidy_Rel_Amt", typeof(decimal));
                        dt.Columns.Add("No_Deactivate_Permit", typeof(int));
                        dt.Columns.Add("Fin_Year", typeof(string));
                        dt.Columns.Add("rec_created_userid", typeof(string));
                        dt.Columns.Add("rec_created_date", typeof(DateTime));

                        foreach (var item in dist)
                        {
                            dt.Rows.Add(
                                Convert.ToInt32(item.dist_code),
                                item.dist_name,
                                Convert.ToInt32(item.registerusr),
                                Convert.ToInt32(item.totalChcProject),
                                Convert.ToInt32(item.finalCHCProject),
                                Convert.ToInt32(item.pendingAC),
                                Convert.ToInt32(item.VerifyAC),
                                Convert.ToInt32(item.pendingBAO),
                                Convert.ToInt32(item.VerifyBAO),
                                Convert.ToInt32(item.PendingADAE),
                                Convert.ToInt32(item.VerifyADAE),
                                Convert.ToInt32(item.RejectADAE),
                                Convert.ToInt32(item.PendingDLEC),
                                Convert.ToInt32(item.VerifyDLEC),
                                Convert.ToInt32(item.RejectDLEC),
                                Convert.ToDecimal(item.Targetdist),
                                Convert.ToInt32(item.TotalGenPermit),
                                Convert.ToInt32(item.dealerimpdlv),
                                Convert.ToInt32(item.ConfirbyApplicant),
                                Convert.ToInt32(item.totphyVerif),
                                Convert.ToInt32(item.totClaimGen),
                                Convert.ToInt32(item.CCbyADAE),
                                Convert.ToInt32(item.totSubReal),
                                Convert.ToDecimal(item.totSubAmntReal),
                                Convert.ToInt32(item.deacivatepermitS),
                                Convert.ToString(item.F_year),
                                null,
                                DateTime.Now);
                        }

                        if (dt != null && dt.Rows.Count > 0)
                        {
                            result = this.miscService.InsertOFMASSMAMSCHCDist(dt);
                        }
                    }

                    if (result > 0)
                    {
                        return this.Ok();
                    }

                    return this.NotFound();
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
        /// InsertOFMAS SMAMSCHCRPTBlk.
        /// </summary>
        /// <returns>Action Result.</returns>
        [AllowAnonymous]
        [HttpPost("InsertOFMAS_SMAMSCHCRPTBlk")]
        public IActionResult InsertOFMAS_SMAMSCHCRPTBlk()
        {
            try
            {
                int result = 0;

                OFMASReportWebServiceSoapClient client = new OFMASReportWebServiceSoapClient(OFMASReportWebServiceSoapClient.EndpointConfiguration.OFMASReportWebServiceSoap);
                client.ClientCredentials.ServiceCertificate.SslCertificateAuthentication =
                new X509ServiceCertificateAuthentication()
                {
                    CertificateValidationMode = X509CertificateValidationMode.None,
                    RevocationMode = System.Security.Cryptography.X509Certificates.X509RevocationMode.NoCheck,
                };
                List<OFMAS_Scheme> scheme = client.getOFMAS_SchemeAsync(this.config.Value.OfmasPasscode).Result.ToList();
                if (scheme.Any())
                {
                    List<OFMAS_SCHC_RptDist> dist = new List<OFMAS_SCHC_RptDist>();
                    foreach (var item in scheme)
                    {
                        if (item.Scheme == "SMAM-SCHC")
                        {
                            var data = client.getOFMAS_SCHCRptDistAsync(this.config.Value.OfmasPasscode, item.F_Year, Convert.ToInt64(item.Phase), Convert.ToInt64(item.Sid)).Result.ToList();

                            if (data != null && data.Any())
                            {
                                dist.AddRange(data);
                            }
                        }
                    }

                    if (dist.Any())
                    {
                        List<OFMAS_SCHC_RptBlk> blk = new List<OFMAS_SCHC_RptBlk>();
                        foreach (var sch in scheme)
                        {
                            if (sch.Scheme == "SMAM-SCHC")
                            {
                                foreach (var distrct in dist)
                                {
                                    var data = client.getOFMAS_SCHCRptBlkAsync(this.config.Value.OfmasPasscode, sch.F_Year, Convert.ToInt64(sch.Sid), Convert.ToInt64(sch.Phase), distrct.dist_code).Result.ToList();

                                    if (data != null && data.Any())
                                    {
                                        blk.AddRange(data);
                                    }
                                }
                            }
                        }

                        if (blk.Any())
                        {
                            DataTable dt = new DataTable();
                            dt.Columns.Add("block_lg_code", typeof(int));

                            dt.Columns.Add("block_name", typeof(string));
                            dt.Columns.Add("registered_usr", typeof(int));
                            dt.Columns.Add("total_appln", typeof(int));
                            dt.Columns.Add("final_appln", typeof(int));
                            dt.Columns.Add("pending_AC", typeof(int));
                            dt.Columns.Add("Verify_AC", typeof(int));
                            dt.Columns.Add("pending_BAO", typeof(int));
                            dt.Columns.Add("Verify_BAO", typeof(int));
                            dt.Columns.Add("Pending_ADAE", typeof(int));
                            dt.Columns.Add("Verify_ADAE", typeof(int));
                            dt.Columns.Add("Reject_ADAE", typeof(int));
                            dt.Columns.Add("Pending_DLEC", typeof(int));
                            dt.Columns.Add("Verify_DLEC", typeof(int));
                            dt.Columns.Add("Reject_DLEC", typeof(int));
                            dt.Columns.Add("Targetdist", typeof(int));
                            dt.Columns.Add("TotalGenPermit", typeof(int));
                            dt.Columns.Add("dealerimpdlv", typeof(int));
                            dt.Columns.Add("ConfirbyApplicant", typeof(int));
                            dt.Columns.Add("totphyVerif", typeof(int));
                            dt.Columns.Add("totClaimGen", typeof(int));
                            dt.Columns.Add("CCbyADAE", typeof(int));
                            dt.Columns.Add("totSubReal", typeof(int));
                            dt.Columns.Add("totSubAmntReal", typeof(decimal));
                            dt.Columns.Add("deacivatepermitS", typeof(int));
                            dt.Columns.Add("Fin_Year", typeof(string));
                            dt.Columns.Add("rec_created_userid", typeof(string));
                            dt.Columns.Add("rec_created_date", typeof(DateTime));
                            foreach (var item in blk)
                            {
                                dt.Rows.Add(
                                Convert.ToInt32(item.block_code),
                                item.block_name,
                                Convert.ToInt32(item.registerusr),
                                Convert.ToInt32(item.totalChcProject),
                                Convert.ToInt32(item.finalCHCProject),
                                Convert.ToInt32(item.pendingAC),
                                Convert.ToInt32(item.VerifyAC),
                                Convert.ToInt32(item.pendingBAO),
                                Convert.ToInt32(item.VerifyBAO),
                                Convert.ToInt32(item.PendingADAE),
                                Convert.ToInt32(item.VerifyADAE),
                                Convert.ToInt32(item.RejectADAE),
                                Convert.ToInt32(item.PendingDLEC),
                                Convert.ToInt32(item.VerifyDLEC),
                                Convert.ToInt32(item.RejectDLEC),
                                Convert.ToInt32(item.Targetdist),
                                Convert.ToInt32(item.TotalGenPermit),
                                Convert.ToInt32(item.dealerimpdlv),
                                Convert.ToInt32(item.ConfirbyApplicant),
                                Convert.ToInt32(item.totphyVerif),
                                Convert.ToInt32(item.totClaimGen),
                                Convert.ToInt32(item.CCbyADAE),
                                Convert.ToInt32(item.totSubReal),
                                Convert.ToDecimal(item.totSubAmntReal),
                                Convert.ToInt32(item.deacivatepermitS),
                                Convert.ToString(item.F_year),
                                null,
                                DateTime.Now);
                            }

                            if (dt != null && dt.Rows.Count > 0)
                            {
                                result = this.miscService.InsertOFMASSMAMSCHCBlk(dt);
                            }
                        }
                        else
                        {
                            return this.NotFound();
                        }
                    }

                    if (result > 0)
                    {
                        return this.Ok();
                    }

                    return this.NotFound();
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
        /// Insert OFMAS SMAMSCHCRPTPAN.
        /// </summary>
        /// <returns>Action Result.</returns>
        [AllowAnonymous]
        [HttpPost("InsertOFMAS_SMAMSCHCRPTPAN")]
        public IActionResult InsertOFMAS_SMAMSCHCRPTPAN()
        {
            try
            {
                int result = 0;
                OFMASReportWebServiceSoapClient client = new OFMASReportWebServiceSoapClient(OFMASReportWebServiceSoapClient.EndpointConfiguration.OFMASReportWebServiceSoap);
                client.ClientCredentials.ServiceCertificate.SslCertificateAuthentication =
                new X509ServiceCertificateAuthentication()
                {
                    CertificateValidationMode = X509CertificateValidationMode.None,
                    RevocationMode = System.Security.Cryptography.X509Certificates.X509RevocationMode.NoCheck,
                };
                List<OFMAS_Scheme> scheme = client.getOFMAS_SchemeAsync(this.config.Value.OfmasPasscode).Result.ToList();
                if (scheme.Any())
                {
                    List<OFMAS_SCHC_RptDist> dist = new List<OFMAS_SCHC_RptDist>();
                    foreach (var item in scheme)
                    {
                        if (item.Scheme == "SMAM-SCHC")
                        {
                            var data = client.getOFMAS_SCHCRptDistAsync(this.config.Value.OfmasPasscode, item.F_Year, Convert.ToInt64(item.Phase), Convert.ToInt64(item.Sid)).Result.ToList();
                            if (data != null && data.Any())
                            {
                                dist.AddRange(data);
                            }
                        }
                    }
                    if (dist.Any())
                    {
                        List<OFMAS_SCHC_RptBlk> blk = new List<OFMAS_SCHC_RptBlk>();
                        foreach (var sch in scheme)
                        {
                            if (sch.Scheme == "SMAM-SCHC")
                            {
                                foreach (var distrct in dist)
                                {
                                    var data = client.getOFMAS_SCHCRptBlkAsync(this.config.Value.OfmasPasscode, sch.F_Year, Convert.ToInt64(sch.Sid), Convert.ToInt64(sch.Phase), distrct.dist_code.ToString()).Result.ToList();
                                    if (data != null && data.Any())
                                    {
                                        blk.AddRange(data);
                                    }
                                }
                            }
                        }

                        if (blk.Any())
                        {
                            List<OFMAS_SCHC_RptPAN> panch = new List<OFMAS_SCHC_RptPAN>();
                            foreach (var sch in scheme)
                            {
                                if (sch.Scheme == "SMAM-SCHC")
                                {
                                    foreach (var blks in blk)
                                    {
                                        var data = client.getOFMAS_SCHCRptPANAsync(this.config.Value.OfmasPasscode, sch.F_Year, Convert.ToInt64(sch.Sid), Convert.ToInt64(sch.Phase), blks.block_code.ToString()).Result.ToList();
                                        if (data != null && data.Any())
                                        {
                                            panch.AddRange(data);
                                        }
                                    }
                                }
                            }

                            if (panch.Any())
                            {
                                DataTable dt = new DataTable();
                                dt.Columns.Add("pan_lg_code", typeof(int));
                                dt.Columns.Add("pan_name", typeof(string));
                                dt.Columns.Add("registered_usr", typeof(int));
                                dt.Columns.Add("total_appln", typeof(int));
                                dt.Columns.Add("final_appln", typeof(int));
                                dt.Columns.Add("pending_AC", typeof(int));
                                dt.Columns.Add("Verify_AC", typeof(int));
                                dt.Columns.Add("pending_BAO", typeof(int));
                                dt.Columns.Add("Verify_BAO", typeof(int));
                                dt.Columns.Add("Pending_ADAE", typeof(int));
                                dt.Columns.Add("Verify_ADAE", typeof(int));
                                dt.Columns.Add("Reject_ADAE", typeof(int));
                                dt.Columns.Add("Pending_DLEC", typeof(int));
                                dt.Columns.Add("Verify_DLEC", typeof(int));
                                dt.Columns.Add("Reject_DLEC", typeof(int));
                                dt.Columns.Add("Targetdist", typeof(int));
                                dt.Columns.Add("TotalGenPermit", typeof(int));
                                dt.Columns.Add("dealerimpdlv", typeof(int));
                                dt.Columns.Add("ConfirbyApplicant", typeof(int));
                                dt.Columns.Add("totphyVerif", typeof(int));
                                dt.Columns.Add("totClaimGen", typeof(int));
                                dt.Columns.Add("CCbyADAE", typeof(int));
                                dt.Columns.Add("totSubReal", typeof(int));
                                dt.Columns.Add("totSubAmntReal", typeof(decimal));
                                dt.Columns.Add("deacivatepermitS", typeof(int));
                                dt.Columns.Add("Fin_Year", typeof(string));
                                dt.Columns.Add("rec_created_userid", typeof(string));
                                dt.Columns.Add("rec_created_date", typeof(DateTime));

                                foreach (var item in panch)
                                {
                                    dt.Rows.Add(
                                Convert.ToInt32(item.pan_code),
                                item.pan_name,
                                Convert.ToInt32(item.registerusr),
                                Convert.ToInt32(item.totalChcProject),
                                Convert.ToInt32(item.finalCHCProject),
                                Convert.ToInt32(item.pendingAC),
                                Convert.ToInt32(item.VerifyAC),
                                Convert.ToInt32(item.pendingBAO),
                                Convert.ToInt32(item.VerifyBAO),
                                Convert.ToInt32(item.PendingADAE),
                                Convert.ToInt32(item.VerifyADAE),
                                Convert.ToInt32(item.RejectADAE),
                                Convert.ToInt32(item.PendingDLEC),
                                Convert.ToInt32(item.VerifyDLEC),
                                Convert.ToInt32(item.RejectDLEC),
                                Convert.ToInt32(item.Targetdist),
                                Convert.ToInt32(item.TotalGenPermit),
                                Convert.ToInt32(item.dealerimpdlv),
                                Convert.ToInt32(item.ConfirbyApplicant),
                                Convert.ToInt32(item.totphyVerif),
                                Convert.ToInt32(item.totClaimGen),
                                Convert.ToInt32(item.CCbyADAE),
                                Convert.ToInt32(item.totSubReal),
                                Convert.ToDecimal(item.totSubAmntReal),
                                Convert.ToInt32(item.deacivatepermitS),
                                Convert.ToString(item.F_year),
                                null,
                                DateTime.Now);
                                }
                                if (dt != null && dt.Rows.Count > 0)
                                {
                                    result = this.miscService.InsertOFMASSMAMSCHCPAN(dt);
                                }
                            }
                            else
                            {
                                return this.NotFound("No Panchayat Found");
                            }
                        }
                        else
                        {
                            return this.NotFound("No Block Found");
                        }
                    }
                    else
                    {
                        return this.NotFound("District Not Found");
                    }

                    if (result > 0)
                    {
                        return this.Ok(result);
                    }

                    return this.NotFound("No rows Affected");
                }
                else
                {
                    return this.NotFound("No Schemes Found");
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.Problem();
            }
        }

        /// <summary>
        /// Insert OFMAS KKABlk.
        /// </summary>
        /// <returns>Action Result.</returns>
        [AllowAnonymous]
        [HttpPost("InsertOFMAS_KKABlk")]
        public IActionResult InsertOFMAS_KKABlk()
        {
            try
            {
                int result = 0;

                OFMASReportWebServiceSoapClient client = new OFMASReportWebServiceSoapClient(OFMASReportWebServiceSoapClient.EndpointConfiguration.OFMASReportWebServiceSoap);
                client.ClientCredentials.ServiceCertificate.SslCertificateAuthentication =
                new X509ServiceCertificateAuthentication()
                {
                    CertificateValidationMode = X509CertificateValidationMode.None,
                    RevocationMode = System.Security.Cryptography.X509Certificates.X509RevocationMode.NoCheck,
                };
                List<OFMAS_Scheme> scheme = client.getOFMAS_SchemeAsync(this.config.Value.OfmasPasscode).Result.ToList();

                if (scheme.Any())
                {
                    List<OFMAS_KKA_RptDist> dist = new List<OFMAS_KKA_RptDist>();
                    foreach (var item in scheme)
                    {
                        if (item.Scheme == "KKA")
                        {
                            var data = client.getOFMAS_KKARptDistAsync(this.config.Value.OfmasPasscode, item.F_Year, Convert.ToInt64(item.Phase), Convert.ToInt64(item.Sid)).Result.ToList();

                            if (data != null && data.Any())
                            {
                                dist.AddRange(data);
                            }
                        }
                    }

                    if (dist.Any())
                    {
                        List<OFMAS_KKA_RptBlk> blk = new List<OFMAS_KKA_RptBlk>();
                        foreach (var sch in scheme)
                        {
                            if (sch.Scheme == "KKA")
                            {
                                foreach (var distrct in dist)
                                {
                                    var data = client.getOFMAS_KKARptBlkAsync(this.config.Value.OfmasPasscode, sch.F_Year, Convert.ToInt64(sch.Sid), Convert.ToInt64(sch.Phase), distrct.dist_code).Result.ToList();
                                    if (data != null && data.Any())
                                    {
                                        blk.AddRange(data);
                                    }
                                }
                            }
                        }

                        if (blk.Any())
                        {
                            DataTable dt = new DataTable();
                            dt.Columns.Add("block_lg_code", typeof(int));

                            dt.Columns.Add("block_name", typeof(string));
                            dt.Columns.Add("registered_usr", typeof(int));
                            dt.Columns.Add("total_appln", typeof(int));
                            dt.Columns.Add("final_appln", typeof(int));
                            dt.Columns.Add("pending_AC", typeof(int));
                            dt.Columns.Add("Verify_AC", typeof(int));
                            dt.Columns.Add("pending_BAO", typeof(int));
                            dt.Columns.Add("Verify_BAO", typeof(int));
                            dt.Columns.Add("Pending_ADAE", typeof(int));
                            dt.Columns.Add("Verify_ADAE", typeof(int));
                            dt.Columns.Add("Reject_ADAE", typeof(int));
                            dt.Columns.Add("Pending_DLEC", typeof(int));
                            dt.Columns.Add("Verify_DLEC", typeof(int));
                            dt.Columns.Add("Reject_DLEC", typeof(int));
                            dt.Columns.Add("Targetdist", typeof(int));
                            dt.Columns.Add("TotalGenPermit", typeof(int));
                            dt.Columns.Add("dealerimpdlv", typeof(int));
                            dt.Columns.Add("ConfirbyApplicant", typeof(int));
                            dt.Columns.Add("totphyVerif", typeof(int));
                            dt.Columns.Add("totClaimGen", typeof(int));
                            dt.Columns.Add("CCbyADAE", typeof(int));
                            dt.Columns.Add("totSubReal", typeof(int));
                            dt.Columns.Add("totSubAmntReal", typeof(decimal));
                            dt.Columns.Add("deacivatepermitS", typeof(int));
                            dt.Columns.Add("Fin_Year", typeof(string));
                            dt.Columns.Add("rec_created_userid", typeof(string));
                            dt.Columns.Add("rec_created_date", typeof(DateTime));

                            foreach (var item in blk)
                            {
                                dt.Rows.Add(
                                Convert.ToInt32(item.block_code),
                                item.block_name,
                                Convert.ToInt32(item.registerusr),
                                Convert.ToInt32(item.totalChcProject),
                                Convert.ToInt32(item.finalCHCProject),
                                Convert.ToInt32(item.pendingAC),
                                Convert.ToInt32(item.VerifyAC),
                                Convert.ToInt32(item.pendingBAO),
                                Convert.ToInt32(item.VerifyBAO),
                                Convert.ToInt32(item.PendingADAE),
                                Convert.ToInt32(item.VerifyADAE),
                                Convert.ToInt32(item.RejectADAE),
                                Convert.ToInt32(item.PendingDLEC),
                                Convert.ToInt32(item.VerifyDLEC),
                                Convert.ToInt32(item.RejectDLEC),
                                Convert.ToInt32(item.Targetdist),
                                Convert.ToInt32(item.TotalGenPermit),
                                Convert.ToInt32(item.dealerimpdlv),
                                Convert.ToInt32(item.ConfirbyApplicant),
                                Convert.ToInt32(item.totphyVerif),
                                Convert.ToInt32(item.totClaimGen),
                                Convert.ToInt32(item.CCbyADAE),
                                Convert.ToInt32(item.totSubReal),
                                Convert.ToDecimal(item.totSubAmntReal),
                                Convert.ToInt32(item.deacivatepermitS),
                                Convert.ToString(item.F_year),
                                null,
                                DateTime.Now);
                            }

                            if (dt != null && dt.Rows.Count > 0)
                            {
                                result = this.miscService.InsertOFMAKKABlk(dt);
                            }
                        }
                        else
                        {
                            return this.NotFound();
                        }
                    }

                    if (result > 0)
                    {
                        return this.Ok();
                    }

                    return this.NotFound();
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
        /// Insert OFMAS KKAPanch.
        /// </summary>
        /// <returns>Action Result.</returns>
        [AllowAnonymous]
        [HttpPost("InsertOFMAS_KKAPanch")]
        public IActionResult InsertOFMAS_KKAPanch()
        {
            try
            {
                int result = 0;
                OFMASReportWebServiceSoapClient client = new OFMASReportWebServiceSoapClient(OFMASReportWebServiceSoapClient.EndpointConfiguration.OFMASReportWebServiceSoap);
                client.ClientCredentials.ServiceCertificate.SslCertificateAuthentication =
                new X509ServiceCertificateAuthentication()
                {
                    CertificateValidationMode = X509CertificateValidationMode.None,
                    RevocationMode = System.Security.Cryptography.X509Certificates.X509RevocationMode.NoCheck,
                };
                List<OFMAS_Scheme> scheme = client.getOFMAS_SchemeAsync(this.config.Value.OfmasPasscode).Result.ToList();
                if (scheme.Any())
                {
                    List<OFMAS_KKA_RptDist> dist = new List<OFMAS_KKA_RptDist>();
                    foreach (var item in scheme)
                    {
                        if (item.Scheme == "KKA")
                        {
                            OFMASReportWebServiceSoapClient distclient = new OFMASReportWebServiceSoapClient(OFMASReportWebServiceSoapClient.EndpointConfiguration.OFMASReportWebServiceSoap);
                            distclient.ClientCredentials.ServiceCertificate.SslCertificateAuthentication =
                new X509ServiceCertificateAuthentication()
                {
                    CertificateValidationMode = X509CertificateValidationMode.None,
                    RevocationMode = System.Security.Cryptography.X509Certificates.X509RevocationMode.NoCheck,
                };
                            var data = distclient.getOFMAS_KKARptDistAsync(this.config.Value.OfmasPasscode, item.F_Year, Convert.ToInt64(item.Phase), Convert.ToInt64(item.Sid)).Result.ToList();

                            if (data != null && data.Any())
                            {
                                dist.AddRange(data);
                            }
                        }
                    }
                    if (dist.Any())
                    {
                        List<OFMAS_KKA_RptBlk> blk = new List<OFMAS_KKA_RptBlk>();
                        foreach (var sch in scheme)
                        {
                            if (sch.Scheme == "KKA")
                            {
                                foreach (var distrct in dist)
                                {
                                    OFMASReportWebServiceSoapClient blkclient = new OFMASReportWebServiceSoapClient(OFMASReportWebServiceSoapClient.EndpointConfiguration.OFMASReportWebServiceSoap);
                                    blkclient.ClientCredentials.ServiceCertificate.SslCertificateAuthentication =
                new X509ServiceCertificateAuthentication()
                {
                    CertificateValidationMode = X509CertificateValidationMode.None,
                    RevocationMode = System.Security.Cryptography.X509Certificates.X509RevocationMode.NoCheck,
                };
                                    var data = blkclient.getOFMAS_KKARptBlkAsync(this.config.Value.OfmasPasscode, sch.F_Year, Convert.ToInt64(sch.Sid), Convert.ToInt64(sch.Phase), distrct.dist_code.ToString()).Result.ToList();

                                    if (data != null && data.Any())
                                    {
                                        blk.AddRange(data);
                                    }
                                }
                            }
                        }

                        if (blk.Any())
                        {
                            List<OFMAS_KKA_RptPAN> panch = new List<OFMAS_KKA_RptPAN>();
                            foreach (var sch in scheme)
                            {
                                if (sch.Scheme == "KKA")
                                {
                                    foreach (var blks in blk)
                                    {
                                        OFMASReportWebServiceSoapClient client1 = new OFMASReportWebServiceSoapClient(OFMASReportWebServiceSoapClient.EndpointConfiguration.OFMASReportWebServiceSoap);
                                        client1.ClientCredentials.ServiceCertificate.SslCertificateAuthentication =
                                        new X509ServiceCertificateAuthentication()
                                        {
                                            CertificateValidationMode = X509CertificateValidationMode.None,
                                            RevocationMode = System.Security.Cryptography.X509Certificates.X509RevocationMode.NoCheck,
                                        };
                                        var data = client1.getOFMAS_KKARptPANAsync(this.config.Value.OfmasPasscode, sch.F_Year, Convert.ToInt64(sch.Sid), Convert.ToInt64(sch.Phase), blks.block_code.ToString()).Result.ToList();

                                        if (data != null && data.Any())
                                        {
                                            panch.AddRange(data);
                                        }
                                    }
                                }
                            }
                            if (panch.Any())
                            {
                                DataTable dt = new DataTable();
                                dt.Columns.Add("panchayat_lg_code", typeof(int));
                                dt.Columns.Add("panchayat_name", typeof(string));
                                dt.Columns.Add("registered_usr", typeof(int));
                                dt.Columns.Add("total_appln", typeof(int));
                                dt.Columns.Add("final_appln", typeof(int));
                                dt.Columns.Add("pending_AC", typeof(int));
                                dt.Columns.Add("Verify_AC", typeof(int));
                                dt.Columns.Add("pending_BAO", typeof(int));
                                dt.Columns.Add("Verify_BAO", typeof(int));
                                dt.Columns.Add("Pending_ADAE", typeof(int));
                                dt.Columns.Add("Verify_ADAE", typeof(int));
                                dt.Columns.Add("Reject_ADAE", typeof(int));
                                dt.Columns.Add("Pending_DLEC", typeof(int));
                                dt.Columns.Add("Verify_DLEC", typeof(int));
                                dt.Columns.Add("Reject_DLEC", typeof(int));
                                dt.Columns.Add("Targetdist", typeof(int));
                                dt.Columns.Add("TotalGenPermit", typeof(int));
                                dt.Columns.Add("dealerimpdlv", typeof(int));
                                dt.Columns.Add("ConfirbyApplicant", typeof(int));
                                dt.Columns.Add("totphyVerif", typeof(int));
                                dt.Columns.Add("totClaimGen", typeof(int));
                                dt.Columns.Add("CCbyADAE", typeof(int));
                                dt.Columns.Add("totSubReal", typeof(int));
                                dt.Columns.Add("totSubAmntReal", typeof(decimal));
                                dt.Columns.Add("deacivatepermitS", typeof(int));
                                dt.Columns.Add("Fin_Year", typeof(string));
                                dt.Columns.Add("rec_created_userid", typeof(string));
                                dt.Columns.Add("rec_created_date", typeof(DateTime));

                                foreach (var item in panch)
                                {
                                    dt.Rows.Add(
                                    Convert.ToInt32(item.pan_code),
                                    item.pan_name,
                                    Convert.ToInt32(item.registerusr),
                                    Convert.ToInt32(item.totalChcProject),
                                    Convert.ToInt32(item.finalCHCProject),
                                    Convert.ToInt32(item.pendingAC),
                                    Convert.ToInt32(item.VerifyAC),
                                    Convert.ToInt32(item.pendingBAO),
                                    Convert.ToInt32(item.VerifyBAO),
                                    Convert.ToInt32(item.PendingADAE),
                                    Convert.ToInt32(item.VerifyADAE),
                                    Convert.ToInt32(item.RejectADAE),
                                    Convert.ToInt32(item.PendingDLEC),
                                    Convert.ToInt32(item.VerifyDLEC),
                                    Convert.ToInt32(item.RejectDLEC),
                                    Convert.ToInt32(item.Targetdist),
                                    Convert.ToInt32(item.TotalGenPermit),
                                    Convert.ToInt32(item.dealerimpdlv),
                                    Convert.ToInt32(item.ConfirbyApplicant),
                                    Convert.ToInt32(item.totphyVerif),
                                    Convert.ToInt32(item.totClaimGen),
                                    Convert.ToInt32(item.CCbyADAE),
                                    Convert.ToInt32(item.totSubReal),
                                    Convert.ToDecimal(item.totSubAmntReal),
                                    Convert.ToInt32(item.deacivatepermitS),
                                    Convert.ToString(item.F_year),
                                    null,
                                    DateTime.Now);
                                }
                                if (dt != null && dt.Rows.Count > 0)
                                {
                                    result = this.miscService.InsertOFMAKKAPANk(dt);
                                }
                            }
                            else
                            {
                                return this.NotFound("No Panchayat Found");
                            }
                        }
                        else
                        {
                            return this.NotFound("No Block Found");
                        }
                    }
                    else
                    {
                        return this.NotFound("District Not Found");
                    }

                    if (result > 0)
                    {
                        return this.Ok(result);
                    }

                    return this.NotFound("No rows Affected");
                }
                else
                {
                    return this.NotFound("No Schemes Found");
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.Problem();
            }
        }

        /// <summary>
        /// POST MI Details.
        /// </summary>
        /// <returns>Action Result.</returns>
        [AllowAnonymous]
        [HttpPost("POSTMIDetails")]
        public IActionResult POSTMIDetails()
        {
            try
            {
                int result = 0;

                WebService_BihanAppSoapClient client = new WebService_BihanAppSoapClient(WebService_BihanAppSoapClient.EndpointConfiguration.WebService_BihanAppSoap);
                client.ClientCredentials.ServiceCertificate.SslCertificateAuthentication =
                new X509ServiceCertificateAuthentication()
                {
                    CertificateValidationMode = X509CertificateValidationMode.None,
                    RevocationMode = System.Security.Cryptography.X509Certificates.X509RevocationMode.NoCheck,
                };

                List<ApplicationDetails> data = client.GetMIDetailsAsync(this.config.Value.FinYear, "pmksy@bihan").Result.ToList();
                DataTable dt = new DataTable();
                dt.Columns.Add("fin_year", typeof(string));
                dt.Columns.Add("district_id", typeof(int));
                dt.Columns.Add("block_id", typeof(int));
                dt.Columns.Add("panchayat_id", typeof(int));
                dt.Columns.Add("district_name", typeof(string));
                dt.Columns.Add("block_name", typeof(string));
                dt.Columns.Add("panchayat_name", typeof(string));
                dt.Columns.Add("Total_Approved_Drip", typeof(decimal));
                dt.Columns.Add("Total_Approved_Mini", typeof(decimal));
                dt.Columns.Add("Total_Approved_Micro", typeof(decimal));
                dt.Columns.Add("Total_Approved_Drip_Area", typeof(decimal));
                dt.Columns.Add("Total_Approved_Mini_Area", typeof(decimal));
                dt.Columns.Add("Total_Approved_Micro_Area", typeof(decimal));
                dt.Columns.Add("Total_WO_Drip", typeof(decimal));
                dt.Columns.Add("Total_WO_Mini", typeof(decimal));
                dt.Columns.Add("Total_WO_Micro", typeof(decimal));
                dt.Columns.Add("Total_Drip_WO_Area", typeof(decimal));
                dt.Columns.Add("Total_Mini_WO_Area", typeof(decimal));
                dt.Columns.Add("Total_Micro_WO_Area", typeof(decimal));
                dt.Columns.Add("Total_Installed_Drip", typeof(decimal));
                dt.Columns.Add("Total_Installed_Mini", typeof(decimal));
                dt.Columns.Add("Total_Installed_Micro", typeof(decimal));
                dt.Columns.Add("Total_Installed_Area_Drip", typeof(decimal));
                dt.Columns.Add(",Total_Installed_Area_Mini", typeof(decimal));
                dt.Columns.Add("Total_Installed_Area_Micro", typeof(decimal));
                dt.Columns.Add("Total_Payment_Done_Drip", typeof(decimal));
                dt.Columns.Add("Total_Payment_Done_Mini", typeof(decimal));
                dt.Columns.Add("Total_Payment_Done_Micro", typeof(decimal));
                dt.Columns.Add("rec_created_date", typeof(DateTime));
                if (data.Any())
                {
                    foreach (var item in data)
                    {
                        dt.Rows.Add(
                            this.config.Value.FinYear,
                            Convert.ToInt32(item._DistCode),
                            Convert.ToInt32(item._BlockCode),
                            Convert.ToInt32(item._PanchayatCode),
                            item._DistName,
                            item._BlockName,
                            item._PanchayatName,
                            Convert.ToDecimal(item._TotalAppDrip),
                            Convert.ToDecimal(item._TotalAppMini),
                            Convert.ToDecimal(item._TotalAppMicro),
                            item._TotalAppDripArea == "NA" ? 0 : Convert.ToDecimal(item._TotalAppDripArea),
                            item._TotalAppMiniArea == "NA" ? 0 : Convert.ToDecimal(item._TotalAppMiniArea),
                            item._TotalAppMicroArea == "NA" ? 0 : Convert.ToDecimal(item._TotalAppMicroArea),
                            item._TotalWODrip == "NA" ? 0 : Convert.ToDecimal(item._TotalWODrip),
                            item._TotalWOMini == "NA" ? 0 : Convert.ToDecimal(item._TotalWOMini),
                            item._TotalWOMicro == "NA" ? 0 : Convert.ToDecimal(item._TotalWOMicro),
                            item._TotalDripWOArea == "NA" ? 0 : Convert.ToDecimal(item._TotalDripWOArea),
                            item._TotalMiniWOArea == "NA" ? 0 : Convert.ToDecimal(item._TotalMiniWOArea),
                            item._TotalMicroWOArea == "NA" ? 0 : Convert.ToDecimal(item._TotalMicroWOArea),
                            item._TotalInstalledDrip == "NA" ? 0 : Convert.ToDecimal(item._TotalInstalledDrip),
                            item._TotalInstalledMini == "NA" ? 0 : Convert.ToDecimal(item._TotalInstalledMini),
                            item._TotalInstalledMicro == "NA" ? 0 : Convert.ToDecimal(item._TotalInstalledMicro),
                            item._TotalInstalledAreaDrip == "NA" ? 0 : Convert.ToDecimal(item._TotalInstalledAreaDrip),
                            item._TotalInstalledAreaMini == "NA" ? 0 : Convert.ToDecimal(item._TotalInstalledAreaMini),
                            item._TotalInstalledAreaMicro == "NA" ? 0 : Convert.ToDecimal(item._TotalInstalledAreaMicro),
                            item._TotalPaymentDoneDrip == "NA" ? 0 : Convert.ToDecimal(item._TotalPaymentDoneDrip),
                            item._TotalPaymentDoneMini == "NA" ? 0 : Convert.ToDecimal(item._TotalPaymentDoneMini),
                            item._TotalPaymentDoneMicro == "NA" ? 0 : Convert.ToDecimal(item._TotalPaymentDoneMicro),
                            DateTime.Now);
                    }

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        result = this.miscService.InsertMIDetails(dt);
                    }
                }

                if (result > 0)
                {
                    return this.Ok();
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
        /// Post Bassoca Data.
        /// </summary>
        /// <returns>Action Result.</returns>
        [AllowAnonymous]
        [HttpPost("PostBassocaData")]
        public IActionResult PostBassocaData()
        {
            try
            {
                int result = 0;

                BSSCAServiceSoapClient client = new BSSCAServiceSoapClient(BSSCAServiceSoapClient.EndpointConfiguration.BSSCAServiceSoap);
                client.ClientCredentials.ServiceCertificate.SslCertificateAuthentication =
                new X509ServiceCertificateAuthentication()
                {
                    CertificateValidationMode = X509CertificateValidationMode.None,
                    RevocationMode = System.Security.Cryptography.X509Certificates.X509RevocationMode.NoCheck,
                };

                List<BasoccaModel> data = JsonConvert.DeserializeObject<List<BasoccaModel>>(client.GetRegistredDataAsync("0", "0", "0", "BSS1234").Result.Body.GetRegistredDataResult);
                DataTable dt = new DataTable();
                dt.Columns.Add("district_name", typeof(string));
                dt.Columns.Add("block_name", typeof(string));
                dt.Columns.Add("crop_name", typeof(string));
                dt.Columns.Add("Season_name", typeof(string));
                dt.Columns.Add("year", typeof(string));
                dt.Columns.Add("total_reg_farmer", typeof(int));
                dt.Columns.Add("total_reg_area", typeof(decimal));
                dt.Columns.Add("total_approved_qty", typeof(decimal));
                if (data.Any())
                {
                    foreach (var item in data)
                    {
                        dt.Rows.Add(
                            item.DistrictName,
                            item.BlockName,
                            item.Crop,
                            item.Season,
                            item.Year,
                            item.TotalRegistredFarmer,
                            item.TotalRegistredArea,
                            item.TotalApprovedQuantity);
                    }

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        result = this.miscService.Insertbassoca(dt);
                    }
                }

                if (result > 0)
                {
                    return this.Ok();
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
        /// Insert Farmer.
        /// </summary>
        /// <param name="mobileno">mobileno.</param>
        /// <returns>Action Result.</returns>
        [AllowAnonymous]
        [HttpPost("InsertFarmer/{mobileno}")]
        public IActionResult InsertFarmer(string mobileno)
        {
            try
            {
                DataTable dt = new DataTable();
                int result = 0;
                BiharAgariWebServiceSoapClient client = new BiharAgariWebServiceSoapClient(BiharAgariWebServiceSoapClient.EndpointConfiguration.BiharAgariWebServiceSoap);
                client.ClientCredentials.ServiceCertificate.SslCertificateAuthentication =
                new X509ServiceCertificateAuthentication()
                {
                    CertificateValidationMode = X509CertificateValidationMode.None,
                    RevocationMode = System.Security.Cryptography.X509Certificates.X509RevocationMode.NoCheck,
                };
                List<DBTFarmerRegistration> farmerdata = client.GetAgriFarmerDetailsAsync(mobileno, this.config.Value.Agriuserid, this.config.Value.Agripassword).Result.ToList();

                if (farmerdata != null && farmerdata.Any())
                {
                    dt.Columns.Add("Registration_ID", typeof(string));
                    dt.Columns.Add("FarmerName", typeof(string));
                    dt.Columns.Add("Father_Husband_Name", typeof(string));
                    dt.Columns.Add("DistrictName", typeof(string));
                    dt.Columns.Add("BlockName", typeof(string));
                    dt.Columns.Add("PanchayatName", typeof(string));
                    dt.Columns.Add("MobileNumber", typeof(string));
                    dt.Columns.Add("FPOflag", typeof(string));
                    dt.Columns.Add("rec_created_userid", typeof(string));
                    dt.Columns.Add("rec_created_date", typeof(DateTime));

                    foreach (var item in farmerdata)
                    {
                        dt.Rows.Add(
                            item.Registration_ID,
                            item.FarmerName,
                            item.Father_Husband_Name,
                            item.DistrictName,
                            item.BlockName,
                            item.PanchayatName,
                            item.MobileNumber,
                            "N",
                            null,
                            DateTime.Now);
                    }
                }

                if (dt != null && dt.Rows.Count > 0)
                {
                    result = this.miscService.InsertFarmer(dt);
                }

                if (result > 0)
                {
                    return this.Ok("{\"status\": \"Data Successfully Inserted/Updated\"}");
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
        /// Get Farmer.
        /// </summary>
        /// <param name="mobileno">mobileno.</param>
        /// <returns>Farmer Info.</returns>
        [AllowAnonymous]
        [HttpGet("GetFarmer/{mobileno}")]
        public List<FarmerInfo> GetFarmer(string mobileno)
        {
            List<FarmerInfo> dTOFarmerInfo = new List<FarmerInfo>();
            DataTable dt = new DataTable();
            BiharAgariWebServiceSoapClient client = new BiharAgariWebServiceSoapClient(BiharAgariWebServiceSoapClient.EndpointConfiguration.BiharAgariWebServiceSoap);
            client.ClientCredentials.ServiceCertificate.SslCertificateAuthentication =
            new X509ServiceCertificateAuthentication()
            {
                CertificateValidationMode = X509CertificateValidationMode.None,
                RevocationMode = System.Security.Cryptography.X509Certificates.X509RevocationMode.NoCheck,
            };
            List<DBTFarmerRegistration> farmerdata = client.GetAgriFarmerDetailsAsync(mobileno, this.config.Value.Agriuserid, this.config.Value.Agripassword).Result.ToList();

            if (farmerdata != null && farmerdata.Any())
            {
                dt.Columns.Add("Registration_ID", typeof(string));
                dt.Columns.Add("FarmerName", typeof(string));
                dt.Columns.Add("Father_Husband_Name", typeof(string));
                dt.Columns.Add("DistrictName", typeof(string));
                dt.Columns.Add("BlockName", typeof(string));
                dt.Columns.Add("PanchayatName", typeof(string));
                dt.Columns.Add("MobileNumber", typeof(string));
                dt.Columns.Add("VillageName", typeof(string));
                dt.Columns.Add("FPOflag", typeof(string));
                dt.Columns.Add("rec_created_userid", typeof(string));
                dt.Columns.Add("rec_created_date", typeof(DateTime));

                foreach (var item in farmerdata)
                {
                    dt.Rows.Add(
                        item.Registration_ID,
                        item.FarmerName,
                        item.Father_Husband_Name,
                        item.DistrictName,
                        item.BlockName,
                        item.PanchayatName,
                        item.MobileNumber,
                        item.VillageName,
                        "N",
                        null,
                        DateTime.Now);
                }
            }

            if (dt != null && dt.Rows.Count > 0)
            {
                dTOFarmerInfo = SqlHelper.ConvertDataTableToList<FarmerInfo>(dt);
            }

            return dTOFarmerInfo;
        }

        /// <summary>
        /// Insert Hybrid Seed Beneficiary Details.
        /// </summary>
        /// <returns>Action Result.</returns>
        [AllowAnonymous]
        [HttpPost("InsertHybridSeedBeneficiaryDetails")]
        public IActionResult InsertHybridSeedBeneficiaryDetails()
        {
            try
            {
                int result = 0;

                WebService_BihanAppSoapClient client = new WebService_BihanAppSoapClient(WebService_BihanAppSoapClient.EndpointConfiguration.WebService_BihanAppSoap);
                client.ClientCredentials.ServiceCertificate.SslCertificateAuthentication =
                new X509ServiceCertificateAuthentication()
                {
                    CertificateValidationMode = X509CertificateValidationMode.None,
                    RevocationMode = System.Security.Cryptography.X509Certificates.X509RevocationMode.NoCheck,
                };

                List<HybridVegStateSchemeDetails> data = client.GetHybridSeedBeneficiaryDetailsAsync(this.config.Value.HybridHortiFinYear, "hs@bihan").Result.ToList();
                foreach (var item in data)
                {
                    result += this.miscService.InsertHortiHybridSeedDetails(item);
                }

                if (result > 0)
                {
                    return this.Ok();
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
        /// Insert Soil Health.
        /// </summary>
        /// <returns>Action Result.</returns>
        [AllowAnonymous]
        [HttpPost("InsertSoilHealth")]
        public async Task<IActionResult> InsertSoilHealth()
        {
            try
            {
                List<SoilHealth> health = new List<SoilHealth>();
                DateTime date = DateTime.Now;
                var finYearStart = date.Month >= 4 ? date.Year : date.Year - 1;
                for (int i = 2020; i <= finYearStart; i++)
                {
                    string finYear = (i % 2000).ToString() + ((i + 1) % 2000).ToString();

                    var client = new HttpClient();
                    var request = new HttpRequestMessage
                    {
                        Method = HttpMethod.Get,
                        RequestUri = new Uri(this.config.Value.SoilHealth + finYear),
                    };
                    client.Timeout = TimeSpan.FromMinutes(10);
                    var response = await client.SendAsync(request).ConfigureAwait(true);
                    if (response.IsSuccessStatusCode)
                    {
                        health = await response.Content.ReadFromJsonAsync<List<SoilHealth>>();
                    }
                    if (health.Any())
                    {
                        string sFinYear = "FY " + i.ToString() + "-" + ((i + 1) % 2000).ToString();
                        DataTable dt = new DataTable();
                        dt.Columns.Add("district_lg_code", typeof(int));
                        dt.Columns.Add("district_name", typeof(string));
                        dt.Columns.Add("target_value", typeof(int));
                        dt.Columns.Add("collected_value", typeof(int));
                        dt.Columns.Add("received_in_lab", typeof(int));
                        dt.Columns.Add("reported_qty", typeof(int));
                        dt.Columns.Add("distributed_qty", typeof(int));
                        dt.Columns.Add("Years", typeof(string));

                        foreach (var item in health)
                        {
                            dt.Rows.Add(
                            Convert.ToInt32(item.DistCode),
                            item.DistrictName,
                            Convert.ToInt32(item.Target),
                            Convert.ToInt32(item.Collected),
                            Convert.ToInt32(item.ReceivedInLab),
                            Convert.ToInt32(item.Reported),
                            Convert.ToInt32(item.Distributed),
                            sFinYear);
                        }

                        if (dt != null && dt.Rows.Count > 0)
                        {
                            this.miscService.InsertSoilHealth(dt);
                        }
                    }
                    else
                    {
                        return this.NotFound();
                    }
                }

                return this.Ok();
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound();
            }
        }

        /// <summary>
        /// GetAgriFarmerDetails
        /// </summary>
        /// <param name="registrationIDOrMobileNo"></param>
        /// <returns>OrgFarmAgriFarmerDetails List</returns>
        [AllowAnonymous]
        [HttpGet("GetAgriFarmerDetails/{registrationIDOrMobileNo}")]
        public IActionResult GetAgriFarmerDetails(string registrationIDOrMobileNo)
        {
            BiharAgariWebServiceSoapClient client = new BiharAgariWebServiceSoapClient(BiharAgariWebServiceSoapClient.EndpointConfiguration.BiharAgariWebServiceSoap);
            client.ClientCredentials.ServiceCertificate.SslCertificateAuthentication =
            new X509ServiceCertificateAuthentication()
            {
                CertificateValidationMode = X509CertificateValidationMode.None,
                RevocationMode = System.Security.Cryptography.X509Certificates.X509RevocationMode.NoCheck,
            };
            List<BiharAgriWebService.DBTFarmerRegistration> data = client.GetAgriFarmerDetailsAsync(registrationIDOrMobileNo, this.config.Value.Agriuserid, this.config.Value.Agripassword).Result.ToList();
            if (data.Any())
            {
                List<OrgFarmAgriFarmerDetails> result = data.Select(x => new OrgFarmAgriFarmerDetails()
                {
                    RegistrationID = x.Registration_ID,
                    FarmerName = x.FarmerName,
                    FatherHusbandName = x.Father_Husband_Name,
                    DOB = x.DOB,
                    Gender = x.Gender,
                    Farmertype = x.Farmertype,
                    CastCateogary = x.CastCateogary,
                    DistrictCodeLG = x.DistrictCode_LG,
                    DistrictName = x.DistrictName,
                    BlockCodeLG = x.BlockCode_LG,
                    BlockName = x.BlockName,
                    PanchayatCodeLG = x.PanchayatCode_LG,
                    PanchayatName = x.PanchayatName,
                    VillageCodeLG = x.VillageCode_LG,
                    VillageName = x.VillageName,
                    MobileNumber = x.MobileNumber,
                }).ToList();

                return this.Ok(result.FirstOrDefault());
            }

            return this.NotFound();
        }

        /// <summary>
        /// OFMAS State Plan scheme panchayat data. 
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("InsertOFMAS_StatePlanRPTPAN")]
        public IActionResult InsertOFMAS_StatePlanRPTPAN()
        {
            try
            {
                int result = 0;
                OFMASReportWebServiceSoapClient client = new OFMASReportWebServiceSoapClient(OFMASReportWebServiceSoapClient.EndpointConfiguration.OFMASReportWebServiceSoap);
                client.ClientCredentials.ServiceCertificate.SslCertificateAuthentication =
                new X509ServiceCertificateAuthentication()
                {
                    CertificateValidationMode = X509CertificateValidationMode.None,
                    RevocationMode = System.Security.Cryptography.X509Certificates.X509RevocationMode.NoCheck,
                };
                List<OFMAS_Scheme> scheme = client.getOFMAS_SchemeAsync(this.config.Value.OfmasPasscode).Result.ToList();
                if (scheme.Any())
                {
                    List<District> dist = this.miscRepository.GetdistrictCode();

                    if (dist.Any())
                    {
                        List<PanchayatData> blk = this.miscRepository.GetOfmasBlock();

                        if (blk.Any())
                        {
                            foreach (var sch in scheme)
                            {
                                if (sch.Scheme == "State Plan")
                                {
                                    foreach (var blks in blk)
                                    {
                                        OFMASReportWebServiceSoapClient clientPanch = new OFMASReportWebServiceSoapClient(OFMASReportWebServiceSoapClient.EndpointConfiguration.OFMASReportWebServiceSoap);
                                        clientPanch.ClientCredentials.ServiceCertificate.SslCertificateAuthentication =
                                        new X509ServiceCertificateAuthentication()
                                        {
                                            CertificateValidationMode = X509CertificateValidationMode.None,
                                            RevocationMode = System.Security.Cryptography.X509Certificates.X509RevocationMode.NoCheck,
                                        };

                                        var data = clientPanch.getOFMAS_SingleRptPANAsync(this.config.Value.OfmasPasscode, sch.F_Year, blks.Block_lg_code.ToString(), sch.Sid).Result.ToList();
                                        List<OFMAS_Single_RptPAN> panch = new List<OFMAS_Single_RptPAN>();
                                        if (data != null && data.Any())
                                        {
                                            panch.AddRange(data);
                                        }

                                        if (panch.Any())
                                        {
                                            DataTable dt = new DataTable();
                                            dt.Columns.Add("pancode", typeof(long));
                                            dt.Columns.Add("pan_name", typeof(string));
                                            dt.Columns.Add("totalapp", typeof(int));
                                            dt.Columns.Add("finalapp", typeof(int));
                                            dt.Columns.Add("VerifyAC", typeof(int));
                                            dt.Columns.Add("VerifyBAO", typeof(int));
                                            dt.Columns.Add("VerifyDAO", typeof(int));
                                            dt.Columns.Add("TotalTarget", typeof(decimal));
                                            dt.Columns.Add("PermitGen", typeof(int));
                                            dt.Columns.Add("Deacivatepermit", typeof(int));
                                            dt.Columns.Add("SuppUpdDealer", typeof(int));
                                            dt.Columns.Add("SuppUpdateADAE", typeof(int));
                                            dt.Columns.Add("PhyInsbyAc", typeof(int));
                                            dt.Columns.Add("ConfbyADAE", typeof(int));
                                            dt.Columns.Add("SubRelease", typeof(int));
                                            dt.Columns.Add("SubReleaseamt", typeof(decimal));
                                            dt.Columns.Add("Fin_Year", typeof(string));
                                            dt.Columns.Add("rec_created_userid", typeof(string));
                                            dt.Columns.Add("rec_created_date", typeof(DateTime));

                                            foreach (var item in panch)
                                            {
                                                dt.Rows.Add(
                                                Convert.ToInt64(item.pan_code),
                                                item.pan_name,
                                                Convert.ToInt32(item.totalapp),
                                                Convert.ToInt32(item.finalapp),
                                                Convert.ToInt32(item.VerifyAC),
                                                Convert.ToInt32(item.VerifyBAO),
                                                Convert.ToInt32(item.VerifyDAO),
                                                Convert.ToDecimal(item.TotalTarget),
                                                Convert.ToInt32(item.PermitGen),
                                                Convert.ToInt32(item.Deacivatepermit),
                                                Convert.ToInt32(item.SuppUpdDealer),
                                                Convert.ToInt32(item.SuppUpdateADAE),
                                                Convert.ToInt32(item.PhyInsbyAc),
                                                Convert.ToInt32(item.ConfbyADAE),
                                                Convert.ToInt32(item.SubRelease),
                                                Convert.ToDecimal(item.SubReleaseamt),
                                                Convert.ToString(item.F_year),
                                                null,
                                                DateTime.Now);
                                            }

                                            if (dt != null && dt.Rows.Count > 0)
                                            {
                                                result = result + this.miscService.InsertOFMASStatePlanPan(dt);
                                            }
                                        }
                                        else
                                        {
                                            return this.NotFound("No Panchayat Found");
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            return this.NotFound("No Block Found");
                        }
                    }
                    else
                    {
                        return this.NotFound("District Not Found");
                    }

                    if (result > 0)
                    {
                        return this.Ok(result);
                    }

                    return this.NotFound("No rows Affected");
                }
                else
                {
                    return this.NotFound("No Schemes Found");
                }
            }
            catch (Exception ex)
            {
                return this.Problem(ex.Message);
            }
        }

        /// <summary>
        /// FertiliserLicenceListAsync.
        /// </summary>
        /// <param name="licenceFor">licenceFor.</param>
        /// <returns>Action Result.</returns>
        [AllowAnonymous]
        [HttpPost("FertiliserLicenceListAsync")]
        public IActionResult FertiliserLicenceListAsync()
        {
            try
            {
                int result = 0;
                BiharAgariWebServiceSoapClient client = new BiharAgariWebServiceSoapClient(BiharAgariWebServiceSoapClient.EndpointConfiguration.BiharAgariWebServiceSoap);
                client.ClientCredentials.ServiceCertificate.SslCertificateAuthentication =
                new X509ServiceCertificateAuthentication()
                {
                    CertificateValidationMode = X509CertificateValidationMode.None,
                    RevocationMode = System.Security.Cryptography.X509Certificates.X509RevocationMode.NoCheck,
                };
                string[] FertiliserLicence = this.config.Value.licenceFor.Split(',');

                foreach (string item in FertiliserLicence)
                {
                    List<InsecticideLicenceModel> fertilizerLicenceState = client.FertiliserLicenceListAsync(this.config.Value.Passcode, item).Result.Select(x => new InsecticideLicenceModel { FirmName = x.FirmName, DistName = x.DistName, Distcode = x.Distcode, LicenceDate = x.LicenceDate, LicenceNo = x.LicenceNo }).ToList();
                    result = this.miscService.InsertLicenseHolder(fertilizerLicenceState, "Fertilizer");
                }

                if (result == -1)
                {
                    return this.NotFound("{\"status\": \"Insertion Failed\"}");
                }
                else
                {
                    return this.Ok("{\"status\": \"Data Successfully Inserted/Updated\"}");
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound("{\"status\": \"Insertion Failed\"}");
            }
        }

        /// <summary>
        /// SeedLicenceListAsync.
        /// </summary>
        /// <param name="licenceFor">licenceFor.</param>
        /// <returns>Action Result.</returns>
        [AllowAnonymous]
        [HttpPost("SeedLicenceListAsync")]
        public IActionResult SeedLicenceListAsync()
        {
            try
            {
                int result = 0;
                BiharAgariWebServiceSoapClient client = new BiharAgariWebServiceSoapClient(BiharAgariWebServiceSoapClient.EndpointConfiguration.BiharAgariWebServiceSoap);
                client.ClientCredentials.ServiceCertificate.SslCertificateAuthentication =
                new X509ServiceCertificateAuthentication()
                {
                    CertificateValidationMode = X509CertificateValidationMode.None,
                    RevocationMode = System.Security.Cryptography.X509Certificates.X509RevocationMode.NoCheck,
                };
                string[] seedLicenceList = this.config.Value.licenceFor.Split(',');

                foreach (string item in seedLicenceList)
                {
                    List<InsecticideLicenceModel> seedLicenceState = client.SeedLicenceListAsync(this.config.Value.Passcode, item).Result.Select(x => new InsecticideLicenceModel { FirmName = x.FirmName, DistName = x.DistName, Distcode = x.Distcode, LicenceDate = x.LicenceDate, LicenceNo = x.LicenceNo }).ToList();
                    result = this.miscService.InsertLicenseHolder(seedLicenceState, "Seed");
                }

                if (result == -1)
                {
                    return this.NotFound("{\"status\": \"Insertion Failed\"}");
                }
                else
                {
                    return this.Ok("{\"status\": \"Data Successfully Inserted/Updated\"}");
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound("{\"status\": \"Insertion Failed\"}");
            }
        }

        /// <summary>
        /// BihanAppWebService data.
        /// </summary>
        /// <returns>Result.</returns>
        [AllowAnonymous]
        [HttpPost("BihanAppWebServicePMKSY")]
        public IActionResult BihanAppWebServicePMKSY()
        {
            try
            {
                int result = 0;

                WebService_BihanAppSoapClient client = new WebService_BihanAppSoapClient(WebService_BihanAppSoapClient.EndpointConfiguration.WebService_BihanAppSoap);
                client.ClientCredentials.ServiceCertificate.SslCertificateAuthentication =
                new X509ServiceCertificateAuthentication()
                {
                    CertificateValidationMode = X509CertificateValidationMode.None,
                    RevocationMode = System.Security.Cryptography.X509Certificates.X509RevocationMode.NoCheck,
                };

                List<PMKSY_ApplicationDetails> pmksy = new List<PMKSY_ApplicationDetails>();

                var data = client.Get_PMKSY_ApplicationDetailsAsync(this.config.Value.PMKSY).Result.ToList();
                string schemecode = "PMKSY";
                pmksy.AddRange(data);
                List<BihanAppServiceInputDataPMKSY> bihanAppList = pmksy.Select(x => new BihanAppServiceInputDataPMKSY()
                {
                    DBT_Registration_No = x._DBTRegistrationNo,
                    Application_Id = x._ApplicationId,
                    Farmer_Name = x._FarmerName,
                    Father_Husband_Name = x._FatherHusbandName,
                    Mobile_No = x._MobileNo,
                    Aadhar_Masked = x._AadharMasked,
                    District_name = x._District,
                    District_lg_Code = x._DistrictCode,
                    Block_name = x._Block,
                    Block_lg_Code = x._BlockCode,
                    Panchayat_name = x._Panchayat,
                    Panchayat_lg_Code = x._PanchayatCode,
                    Village_name = x._Village,
                    Village_lg_Code = x._VillageCode,
                    Transaction_Date = x._TransactionDate,
                    Amount_benefited = x._Amountbenefited,
                    Bank_Name = x._BankName,
                    Bank_Account_No = x._BankAccountNo,
                    rec_created_userid = null,
                    rec_created_date = DateTime.Now,
                }).ToList();
                DataTable dt = SqlHelper.LinqToDataTable(bihanAppList.AsEnumerable());
                result = this.miscService.InsertNHMData(dt, schemecode);
                if (result > 0)
                {
                    return this.Ok();
                }
                else
                {
                    return this.NotFound("No Schemes Found");
                }
            }
            catch (Exception ex)
            {
                return this.Problem(ex.Message);
            }
        }

        /// <summary>
        /// BihanAppWebServiceNHM data.
        /// </summary>
        /// <returns>Result.</returns>
        [AllowAnonymous]
        [HttpPost("BihanAppWebServiceNHM")]
        public IActionResult BihanAppWebServiceNHM()
        {
            try
            {
                int result = 0;

                WebService_BihanAppSoapClient client = new WebService_BihanAppSoapClient(WebService_BihanAppSoapClient.EndpointConfiguration.WebService_BihanAppSoap);
                client.ClientCredentials.ServiceCertificate.SslCertificateAuthentication =
                new X509ServiceCertificateAuthentication()
                {
                    CertificateValidationMode = X509CertificateValidationMode.None,
                    RevocationMode = System.Security.Cryptography.X509Certificates.X509RevocationMode.NoCheck,
                };
                List<NHM_CMHM_ApplicationDetails> nhm_cmhm_Applications = new List<NHM_CMHM_ApplicationDetails>();
                List<BihanAppServiceInputData> bihanAppList = new List<BihanAppServiceInputData>();
                List<District> dist = this.miscRepository.GetdistrictCode();
                foreach (District district in dist)
                {
                    var data = client.Get_NHM_CMHM_ApplicationDetailsAsync(this.config.Value.NHM_CNHM, this.config.Value.NHM_SchemeID, Convert.ToString(district.District_lg_code)).Result.ToList();
                    nhm_cmhm_Applications.AddRange(data);
                    bihanAppList = nhm_cmhm_Applications.Select(x => new BihanAppServiceInputData()
                    {
                        DBT_Registration_No = x._DBTRegistrationNo,
                        Application_Id = x._ApplicationId,
                        Farmer_Name = x._FarmerName,
                        Father_Husband_Name = x._FatherHusbandName,
                        Mobile_No = x._MobileNo,
                        Aadhar_Masked = x._AadharMasked,
                        District_name = x._District,
                        District_lg_Code = x._DistrictCode,
                        Block_name = x._Block,
                        Block_lg_Code = x._BlockCode,
                        Panchayat_name = x._Panchayat,
                        Panchayat_lg_Code = x._PanchayatCode,
                        Village_name = x._Village,
                        Village_lg_Code = x._VillageCode,
                        Transaction_Date = x._TransactionDate,
                        Amount_benefited = x._Amountbenefited,
                        Bank_Name = x._BankName,
                        Bank_Account_No = x._BankAccountNo,
                        rec_created_userid = null,
                        rec_created_date = DateTime.Now,
                    }).ToList();
                }

                DataTable dt = SqlHelper.LinqToDataTable(bihanAppList.AsEnumerable());
                result = this.miscService.InsertNHMData(dt, this.config.Value.NHM_SchemeID);
                if (result > 0)
                {
                    return this.Ok();
                }
                else
                {
                    return this.NotFound("No Schemes Found");
                }
            }
            catch (Exception ex)
            {
                return this.Problem(ex.Message);
            }
        }

        /// <summary>
        /// BihanAppWebServiceNHM data.
        /// </summary>
        /// <returns> Result.</returns>
        [AllowAnonymous]
        [HttpPost("BihanAppWebServiceCMHM")]
        public IActionResult BihanAppWebServiceCMHM()
        {
            try
            {
                int result = 0;

                WebService_BihanAppSoapClient client = new WebService_BihanAppSoapClient(WebService_BihanAppSoapClient.EndpointConfiguration.WebService_BihanAppSoap);
                client.ClientCredentials.ServiceCertificate.SslCertificateAuthentication =
                new X509ServiceCertificateAuthentication()
                {
                    CertificateValidationMode = X509CertificateValidationMode.None,
                    RevocationMode = System.Security.Cryptography.X509Certificates.X509RevocationMode.NoCheck,
                };
                List<NHM_CMHM_ApplicationDetails> nhm_cmhm_Applications = new List<NHM_CMHM_ApplicationDetails>();
                List<BihanAppServiceInputData> bihanAppList = new List<BihanAppServiceInputData>();
                List<District> dist = this.miscRepository.GetdistrictCode();
                foreach (District district in dist)
                {
                    var data = client.Get_NHM_CMHM_ApplicationDetailsAsync(this.config.Value.NHM_CNHM, this.config.Value.CMHM_SchemeID, Convert.ToString(district.District_lg_code)).Result.ToList();
                    nhm_cmhm_Applications.AddRange(data);
                    bihanAppList = nhm_cmhm_Applications.Select(x => new BihanAppServiceInputData()
                    {
                        DBT_Registration_No = x._DBTRegistrationNo,
                        Application_Id = x._ApplicationId,
                        Farmer_Name = x._FarmerName,
                        Father_Husband_Name = x._FatherHusbandName,
                        Mobile_No = x._MobileNo,
                        Aadhar_Masked = x._AadharMasked,
                        District_name = x._District,
                        District_lg_Code = x._DistrictCode,
                        Block_name = x._Block,
                        Block_lg_Code = x._BlockCode,
                        Panchayat_name = x._Panchayat,
                        Panchayat_lg_Code = x._PanchayatCode,
                        Village_name = x._Village,
                        Village_lg_Code = x._VillageCode,
                        Transaction_Date = x._TransactionDate,
                        Amount_benefited = x._Amountbenefited,
                        Bank_Name = x._BankName,
                        Bank_Account_No = x._BankAccountNo,
                        rec_created_date = DateTime.Now,
                        rec_created_userid = null,
                    }).ToList();
                }

                DataTable dt = SqlHelper.LinqToDataTable(bihanAppList.AsEnumerable());
                result = this.miscService.InsertNHMData(dt, this.config.Value.CMHM_SchemeID);
                if (result > 0)
                {
                    return this.Ok();
                }
                else
                {
                    return this.NotFound("No Schemes Found");
                }
            }
            catch (Exception ex)
            {
                return this.Problem(ex.Message);
            }
        }

        /// <summary>
        /// BihanAppWebServiceNHM data.
        /// </summary>
        /// <returns>Result.</returns>
        [AllowAnonymous]
        [HttpPost("BihanAppWebServiceNalkoop")]
        public IActionResult BihanAppWebServiceNalkoop()
        {
            try
            {
                int result = 0;

                WebService_BihanAppSoapClient client = new WebService_BihanAppSoapClient(WebService_BihanAppSoapClient.EndpointConfiguration.WebService_BihanAppSoap);
                client.ClientCredentials.ServiceCertificate.SslCertificateAuthentication =
                new X509ServiceCertificateAuthentication()
                {
                    CertificateValidationMode = X509CertificateValidationMode.None,
                    RevocationMode = System.Security.Cryptography.X509Certificates.X509RevocationMode.NoCheck,
                };
                List<BihanAppServiceInputData> bihanAppList = new List<BihanAppServiceInputData>();
                List<Nalkoop_ApplicationDetails> nalkoop = new List<Nalkoop_ApplicationDetails>();
                string schemecode = null;
                List<District> dist = this.miscRepository.GetdistrictCode();
                foreach (District district in dist)
                {
                    var data = client.Get_Nalkoop_ApplicationDetailsAsync(this.config.Value.Nalkoop, Convert.ToString(district.District_lg_code)).Result.ToList();
                    nalkoop.AddRange(data);
                    bihanAppList = nalkoop.Select(x => new BihanAppServiceInputData()
                    {
                        DBT_Registration_No = x._DBTRegistrationNo,
                        Application_Id = x._ApplicationId,
                        Farmer_Name = x._FarmerName,
                        Father_Husband_Name = x._FatherHusbandName,
                        Mobile_No = x._MobileNo,
                        Aadhar_Masked = x._AadharMasked,
                        District_name = x._District,
                        District_lg_Code = x._DistrictCode,
                        Block_name = x._Block,
                        Block_lg_Code = x._BlockCode,
                        Panchayat_name = x._Panchayat,
                        Panchayat_lg_Code = x._PanchayatCode,
                        Village_name = x._Village,
                        Village_lg_Code = x._VillageCode,
                        Transaction_Date = x._TransactionDate,
                        Amount_benefited = x._Amountbenefited,
                        Bank_Name = x._BankName,
                        Bank_Account_No = x._BankAccountNo,
                        rec_created_userid = null,
                        rec_created_date = DateTime.Now,
                    }).ToList();
                }

                DataTable dt = SqlHelper.LinqToDataTable(bihanAppList.AsEnumerable());
                result = this.miscService.InsertNHMData(dt, schemecode);
                if (result > 0)
                {
                    return this.Ok();
                }
                else
                {
                    return this.NotFound("No Schemes Found");
                }
            }
            catch (Exception ex)
            {
                return this.Problem(ex.Message);
            }
        }

        /// <summary>
        /// OFMAS State Plan scheme panchayat data.
        /// </summary>
        /// <returns>Result</returns>
        [AllowAnonymous]
        [HttpPost("Insert_OFMAS_ApplicationDetailsFMB_CHC_SCHC_KKA")]
        public IActionResult Insert_OFMAS_ApplicationDetailsFMB_CHC_SCHC_KKA()
        {
            try
            {
                int result = 0;

                OFMAS_Report.OFMASReportWebServiceSoapClient ofmasclient = new OFMAS_Report.OFMASReportWebServiceSoapClient(OFMAS_Report.OFMASReportWebServiceSoapClient.EndpointConfiguration.OFMASReportWebServiceSoap);
                ofmasclient.ClientCredentials.ServiceCertificate.SslCertificateAuthentication =
                new X509ServiceCertificateAuthentication()
                {
                    CertificateValidationMode = X509CertificateValidationMode.None,
                    RevocationMode = System.Security.Cryptography.X509Certificates.X509RevocationMode.NoCheck,
                };
                List<OFMAS_Scheme> scheme = ofmasclient.getOFMAS_SchemeAsync(this.config.Value.OfmasPasscode).Result.ToList();
                List<OFMAS_CHCBeneficiaryList> chcBeneficiaryList = new List<OFMAS_CHCBeneficiaryList>();
                List<OFMAS_CHCBeneficiaryList> fmbBeneficiaryList = new List<OFMAS_CHCBeneficiaryList>();
                List<OFMAS_CHCBeneficiaryList> kkaBeneficiaryList = new List<OFMAS_CHCBeneficiaryList>();
                List<OFMAS_CHCBeneficiaryList> schcBeneficiaryList = new List<OFMAS_CHCBeneficiaryList>();
                List<CHCBeneficiaryList> bihanAppCHCBeneficiaryList = new List<CHCBeneficiaryList>();

                string schemecode = "0";
                foreach (OFMAS_Scheme schemes in scheme)
                {

                    if (schemes.Scheme == "SMAM-CHC")
                    {

                        schemecode = schemes.Sid;
                        var chcDATA = ofmasclient.getOFMAS_CHCBeneficiaryListAsync(this.config.Value.OfmasPasscode, Convert.ToString(schemes.F_Year), Convert.ToInt64(schemes.Sid), Convert.ToInt64(schemes.Phase)).Result.ToList();
                        chcBeneficiaryList.AddRange(chcDATA);
                        bihanAppCHCBeneficiaryList = chcBeneficiaryList.Select(x => new CHCBeneficiaryList()
                        {
                            check1 = x.check1,
                            Finacial_Year = x.F_Year,
                            Scheme = x.Scheme,
                            phase = x.phase,
                            Beneficiary_Name = x.BeneficiaryName,
                            Beneficiary_Type = x.BeneficiaryType,
                            District_lg_Code = x.dist_code,
                            District_name = x.dist_name,
                            Block_lg_Code = x.block_code,
                            Block_name = x.block_name,
                            Panchayat_lg_Code = x.pan_code,
                            Panchayat_name = x.pan_name,
                            Village_lg_Code = x.villg_code,
                            Village_name = x.villg_name,
                            fullcost = Convert.ToDecimal(x.fullcost),
                            subsidy_amt = Convert.ToDecimal(x.subsidy_amt),
                            subsidy_date = x.subsidy_date,
                            rec_created_date = DateTime.Now,
                            rec_created_userid = null,
                        }).ToList();
                        DataTable dt = SqlHelper.LinqToDataTable(bihanAppCHCBeneficiaryList.AsEnumerable());
                        result = this.miscService.InsertNHMData(dt, schemecode);
                    }

                    if (schemes.Scheme == "SMAM-SCHC")
                    {
                        schemecode = schemes.Sid;
                        var fab_DATA = ofmasclient.getOFMAS_CHCBeneficiaryListAsync(this.config.Value.OfmasPasscode, Convert.ToString(schemes.F_Year), Convert.ToInt64(schemes.Sid), Convert.ToInt64(schemes.Phase)).Result.ToList();
                        schcBeneficiaryList.AddRange(fab_DATA);
                        bihanAppCHCBeneficiaryList = schcBeneficiaryList.Select(x => new CHCBeneficiaryList()
                        {
                            check1 = x.check1,
                            Finacial_Year = x.F_Year,
                            Scheme = x.Scheme,
                            phase = x.phase,
                            Beneficiary_Name = x.BeneficiaryName,
                            Beneficiary_Type = x.BeneficiaryType,
                            District_name = x.dist_name,
                            District_lg_Code = x.dist_code,
                            Block_name = x.block_name,
                            Block_lg_Code = x.block_code,
                            Panchayat_name = x.pan_name,
                            Panchayat_lg_Code = x.pan_code,
                            Village_name = x.villg_name,
                            Village_lg_Code = x.villg_code,
                            fullcost = Convert.ToDecimal(x.fullcost),
                            subsidy_amt = Convert.ToDecimal(x.subsidy_amt),
                            subsidy_date = x.subsidy_date,
                            rec_created_date = DateTime.Now,
                            rec_created_userid = null,
                        }).ToList();
                        DataTable dt = SqlHelper.LinqToDataTable(bihanAppCHCBeneficiaryList.AsEnumerable());
                        result = this.miscService.InsertNHMData(dt, schemecode);
                    }
                    if (schemes.Scheme == "SMAM-FMB")
                    {
                        schemecode = schemes.Sid;
                        var fab_DATA = ofmasclient.getOFMAS_CHCBeneficiaryListAsync(this.config.Value.OfmasPasscode, Convert.ToString(schemes.F_Year), Convert.ToInt64(schemes.Sid), Convert.ToInt64(schemes.Phase)).Result.ToList();
                        fmbBeneficiaryList.AddRange(fab_DATA);
                        bihanAppCHCBeneficiaryList = fmbBeneficiaryList.Select(x => new CHCBeneficiaryList()
                        {
                            check1 = x.check1,
                            Finacial_Year = x.F_Year,
                            Scheme = x.Scheme,
                            phase = x.phase,
                            Beneficiary_Name = x.BeneficiaryName,
                            Beneficiary_Type = x.BeneficiaryType,
                            District_name = x.dist_name,
                            District_lg_Code = x.dist_code,
                            Block_name = x.block_name,
                            Block_lg_Code = x.block_code,
                            Panchayat_name = x.pan_name,
                            Panchayat_lg_Code = x.pan_code,
                            Village_name = x.villg_name,
                            Village_lg_Code = x.villg_code,
                            fullcost = Convert.ToDecimal(x.fullcost),
                            subsidy_amt = Convert.ToDecimal(x.subsidy_amt),
                            subsidy_date = x.subsidy_date,
                            rec_created_date = DateTime.Now,
                            rec_created_userid = null,
                        }).ToList();
                        DataTable dt = SqlHelper.LinqToDataTable(bihanAppCHCBeneficiaryList.AsEnumerable());
                        result = this.miscService.InsertNHMData(dt, schemecode);
                    }
                    if (schemes.Scheme == "KKA")
                    {
                        schemecode = schemes.Sid;
                        var fab_DATA = ofmasclient.getOFMAS_CHCBeneficiaryListAsync(this.config.Value.OfmasPasscode, Convert.ToString(schemes.F_Year), Convert.ToInt64(schemes.Sid), Convert.ToInt64(schemes.Phase)).Result.ToList();
                        kkaBeneficiaryList.AddRange(fab_DATA);
                        bihanAppCHCBeneficiaryList = kkaBeneficiaryList.Select(x => new CHCBeneficiaryList()
                        {
                            check1 = x.check1,
                            Finacial_Year = x.F_Year,
                            Scheme = x.Scheme,
                            phase = x.phase,
                            Beneficiary_Name = x.BeneficiaryName,
                            Beneficiary_Type = x.BeneficiaryType,
                            District_name = x.dist_name,
                            District_lg_Code = x.dist_code,
                            Block_name = x.block_name,
                            Block_lg_Code = x.block_code,
                            Panchayat_name = x.pan_name,
                            Panchayat_lg_Code = x.pan_code,
                            Village_name = x.villg_name,
                            Village_lg_Code = x.villg_code,
                            fullcost = Convert.ToDecimal(x.fullcost),
                            subsidy_amt = Convert.ToDecimal(x.subsidy_amt),
                            subsidy_date = x.subsidy_date,
                            rec_created_date = DateTime.Now,
                            rec_created_userid = null,
                        }).ToList();
                        DataTable dt = SqlHelper.LinqToDataTable(bihanAppCHCBeneficiaryList.AsEnumerable());
                        result = this.miscService.InsertNHMData(dt, schemecode);
                    }
                }

                if (result > 0)
                {
                    return this.Ok();
                }
                else
                {
                    return this.NotFound("No Schemes Found");
                }
            }
            catch (Exception ex)
            {
                return this.Problem(ex.Message);
            }
        }

        /// <summary>
        /// OFMAS State Plan scheme panchayat data.
        /// </summary>
        /// <returns>Result.</returns>
        [AllowAnonymous]
        [HttpPost("Insert_OFMAS_ApplicationDetails_Pulses")]
        public IActionResult Insert_OFMAS_ApplicationDetails_Pulses()
        {
            try
            {
                int result = 0;

                OFMAS_Report.OFMASReportWebServiceSoapClient ofmasclient = new OFMAS_Report.OFMASReportWebServiceSoapClient(OFMAS_Report.OFMASReportWebServiceSoapClient.EndpointConfiguration.OFMASReportWebServiceSoap);
                ofmasclient.ClientCredentials.ServiceCertificate.SslCertificateAuthentication =
                new X509ServiceCertificateAuthentication()
                {
                    CertificateValidationMode = X509CertificateValidationMode.None,
                    RevocationMode = System.Security.Cryptography.X509Certificates.X509RevocationMode.NoCheck,
                };
                List<OFMAS_Scheme> scheme = ofmasclient.getOFMAS_SchemeAsync(this.config.Value.OfmasPasscode).Result.ToList();
                List<OFMAS_BeneficiaryList> beneficiaryList = new List<OFMAS_BeneficiaryList>();
                List<PanchayatData> blk = this.miscRepository.GetOfmasBlock();

                string schemecode = "0";
                foreach (OFMAS_Scheme schemes in scheme)
                {
                    if (schemes.Scheme == "TRFA-Pulses")
                    {
                        schemecode = schemes.Sid;

                        foreach (PanchayatData block in blk)
                        {
                            var beneficiaryLists = ofmasclient.getOFMAS_BeneficiaryListAsync(this.config.Value.OfmasPasscode, Convert.ToString(schemes.F_Year), Convert.ToInt64(schemes.Sid), Convert.ToString(block.Block_lg_code)).Result.ToList();

                            if (beneficiaryLists[0].check1 == "1")
                            {
                                beneficiaryList.AddRange(beneficiaryLists);
                            }
                        }

                        List<BeneficiaryList> bihanAppBeneficiaryListPulses = beneficiaryList.Select(x => new BeneficiaryList()
                        {
                            check1 = x.check1,
                            Finacial_Year = x.F_Year,
                            Scheme = x.Scheme,
                            Farmer_RegID = x.Farmer_RegID,
                            Beneficiary_Name = x.BeneficiaryName,
                            Beneficiary_Type = x.BeneficiaryCategory,
                            District_name = x.dist_name,
                            District_lg_Code = x.dist_code,
                            Block_name = x.block_name,
                            Block_lg_Code = x.block_code,
                            Panchayat_name = x.pan_name,
                            Panchayat_lg_Code = x.pan_code,
                            Village_name = x.villg_name,
                            Village_lg_Code = x.villg_code,
                            Implement = x.Implement,
                            fullcost = Convert.ToDecimal(x.fullcost),
                            subsidy_amt = Convert.ToDecimal(x.subsidy_amt),
                            subsidy_date = x.subsidy_date,
                            rec_created_date = DateTime.Now,
                            rec_created_userid = null,
                        }).ToList();
                        DataTable dt = SqlHelper.LinqToDataTable(bihanAppBeneficiaryListPulses.AsEnumerable());
                        result = this.miscService.InsertNHMData(dt, schemecode);
                    }
                }

                if (result > 0)
                {
                    return this.Ok();
                }
                else
                {
                    return this.NotFound("No Schemes Found");
                }
            }
            catch (Exception ex)
            {
                return this.Problem(ex.Message);
            }
        }

        /// <summary>
        /// OFMAS State Plan scheme panchayat data.
        /// </summary>
        /// <returns>Result.</returns>
        [AllowAnonymous]
        [HttpPost("Insert_OFMAS_ApplicationDetails_Oilseed")]
        public IActionResult Insert_OFMAS_ApplicationDetails_Oilseed()
        {
            try
            {
                int result = 0;

                OFMAS_Report.OFMASReportWebServiceSoapClient ofmasclient = new OFMAS_Report.OFMASReportWebServiceSoapClient(OFMAS_Report.OFMASReportWebServiceSoapClient.EndpointConfiguration.OFMASReportWebServiceSoap);
                ofmasclient.ClientCredentials.ServiceCertificate.SslCertificateAuthentication =
                new X509ServiceCertificateAuthentication()
                {
                    CertificateValidationMode = X509CertificateValidationMode.None,
                    RevocationMode = System.Security.Cryptography.X509Certificates.X509RevocationMode.NoCheck,
                };
                List<OFMAS_Scheme> scheme = ofmasclient.getOFMAS_SchemeAsync(this.config.Value.OfmasPasscode).Result.ToList();
                List<OFMAS_BeneficiaryList> beneficiaryList = new List<OFMAS_BeneficiaryList>();
                List<PanchayatData> blk = this.miscRepository.GetOfmasBlock();

                foreach (OFMAS_Scheme schemes in scheme)
                {
                    if (schemes.Scheme == "TRFA-Oilseeds")
                    {
                        string schemecode = schemes.Sid;
                        foreach (PanchayatData block in blk)
                        {
                            if (schemecode == "17")
                            {
                                var beneficiaryLists = ofmasclient.getOFMAS_BeneficiaryListAsync(this.config.Value.OfmasPasscode, Convert.ToString(schemes.F_Year), Convert.ToInt64(schemes.Sid), Convert.ToString(block.Block_lg_code)).Result.ToList();

                                if (beneficiaryLists[0].check1 == "1")
                                {
                                    beneficiaryList.AddRange(beneficiaryLists);
                                }
                            }
                        }

                        List<BeneficiaryList> bihanAppBeneficiaryListOilseeds = beneficiaryList.Select(x => new BeneficiaryList()
                            {
                                check1 = x.check1,
                                Finacial_Year = x.F_Year,
                                Scheme = x.Scheme,
                                Farmer_RegID = x.Farmer_RegID,
                                Beneficiary_Name = x.BeneficiaryName,
                                Beneficiary_Type = x.BeneficiaryCategory,
                                District_name = x.dist_name,
                                District_lg_Code = x.dist_code,
                                Block_name = x.block_name,
                                Block_lg_Code = x.block_code,
                                Panchayat_name = x.pan_name,
                                Panchayat_lg_Code = x.pan_code,
                                Village_name = x.villg_name,
                                Village_lg_Code = x.villg_code,
                                Implement = x.Implement,
                                fullcost = Convert.ToDecimal(x.fullcost),
                                subsidy_amt = Convert.ToDecimal(x.subsidy_amt),
                                subsidy_date = x.subsidy_date,
                                rec_created_date = DateTime.Now,
                                rec_created_userid = null,
                            }).ToList();
                        DataTable dt = SqlHelper.LinqToDataTable(bihanAppBeneficiaryListOilseeds.AsEnumerable());
                        result = this.miscService.InsertNHMData(dt, schemecode);
                    }
                }

                if (result > 0)
                {
                    return this.Ok();
                }
                else
                {
                    return this.NotFound("No Schemes Found");
                }
            }
            catch (Exception ex)
            {
                return this.Problem(ex.Message);
            }
        }

        /// <summary>
        /// OFMAS State Plan scheme panchayat data.
        /// </summary>
        /// <returns>Result.</returns>
        [AllowAnonymous]
        [HttpPost("Insert_OFMAS_ApplicationDetailsRAFTAAR")]
        public IActionResult Insert_OFMAS_ApplicationDetailsRAFTAAR()
        {
            try
            {
                int result = 0;

                OFMAS_Report.OFMASReportWebServiceSoapClient ofmasclient = new OFMAS_Report.OFMASReportWebServiceSoapClient(OFMAS_Report.OFMASReportWebServiceSoapClient.EndpointConfiguration.OFMASReportWebServiceSoap);
                ofmasclient.ClientCredentials.ServiceCertificate.SslCertificateAuthentication =
                new X509ServiceCertificateAuthentication()
                {
                    CertificateValidationMode = X509CertificateValidationMode.None,
                    RevocationMode = System.Security.Cryptography.X509Certificates.X509RevocationMode.NoCheck,
                };
                List<OFMAS_Scheme> scheme = ofmasclient.getOFMAS_SchemeAsync(this.config.Value.OfmasPasscode).Result.ToList();
                List<OFMAS_BeneficiaryList> beneficiaryList = new List<OFMAS_BeneficiaryList>();
                List<PanchayatData> blk = this.miscRepository.GetOfmasBlock();
                string schemecode = "0";
                foreach (OFMAS_Scheme schemes in scheme)
                {
                    if (schemes.Scheme == "RKVY-RAFTAAR")
                    {
                         schemecode = schemes.Sid;

                         foreach (PanchayatData block in blk)
                        {
                            var beneficiaryLists = ofmasclient.getOFMAS_BeneficiaryListAsync(this.config.Value.OfmasPasscode, Convert.ToString(schemes.F_Year), Convert.ToInt64(schemes.Sid), Convert.ToString(block.Block_lg_code)).Result.ToList();

                            if (beneficiaryLists[0].check1 == "1")
                            {
                                beneficiaryList.AddRange(beneficiaryLists);
                            }
                        }

                         List<BeneficiaryList> bihanAppBeneficiaryListRAFTAR = beneficiaryList.Select(x => new BeneficiaryList()
                        {
                            check1 = x.check1,
                            Finacial_Year = x.F_Year,
                            Scheme = x.Scheme,
                            Farmer_RegID = x.Farmer_RegID,
                            Beneficiary_Name = x.BeneficiaryName,
                            Beneficiary_Type = x.BeneficiaryCategory,
                            District_name = x.dist_name,
                            District_lg_Code = x.dist_code,
                            Block_name = x.block_name,
                            Block_lg_Code = x.block_code,
                            Panchayat_name = x.pan_name,
                            Village_name = x.villg_name,
                            Village_lg_Code = x.villg_code,
                            Implement = x.Implement,
                            fullcost = Convert.ToDecimal(x.fullcost),
                            subsidy_amt = Convert.ToDecimal(x.subsidy_amt),
                            subsidy_date = x.subsidy_date,
                            rec_created_date = DateTime.Now,
                            rec_created_userid = null,
                        }).ToList();
                         DataTable dt = SqlHelper.LinqToDataTable(bihanAppBeneficiaryListRAFTAR.AsEnumerable());
                         result = this.miscService.InsertNHMData(dt, schemecode);
                    }
                }

                if (result > 0)
                {
                    return this.Ok();
                }
                else
                {
                    return this.NotFound("No Schemes Found");
                }
            }
            catch (Exception ex)
            {
                return this.Problem(ex.Message);
            }
        }
        /// <summary>
        /// Insert_OFMAS_ApplicationDetails_NFSM_Oilseeds.
        /// </summary>
        /// <returns>Result.</returns>
        [AllowAnonymous]
        [HttpPost("Insert_OFMAS_ApplicationDetails_NFSM_Oilseeds")]
        public IActionResult Insert_OFMAS_ApplicationDetails_NFSM_Oilseeds()
        {
            try
            {
                int result = 0;

                OFMAS_Report.OFMASReportWebServiceSoapClient ofmasclient = new OFMAS_Report.OFMASReportWebServiceSoapClient(OFMAS_Report.OFMASReportWebServiceSoapClient.EndpointConfiguration.OFMASReportWebServiceSoap);
                ofmasclient.ClientCredentials.ServiceCertificate.SslCertificateAuthentication =
                new X509ServiceCertificateAuthentication()
                {
                    CertificateValidationMode = X509CertificateValidationMode.None,
                    RevocationMode = System.Security.Cryptography.X509Certificates.X509RevocationMode.NoCheck,
                };
                List<OFMAS_Scheme> scheme = ofmasclient.getOFMAS_SchemeAsync(this.config.Value.OfmasPasscode).Result.ToList();
                List<OFMAS_BeneficiaryList> beneficiaryList = new List<OFMAS_BeneficiaryList>();
                List<BeneficiaryList> bihanAppBeneficiaryListOilseeds = new List<BeneficiaryList>();
                List<PanchayatData> blk = this.miscRepository.GetOfmasBlock();

                foreach (OFMAS_Scheme schemes in scheme)
                {
                    if (schemes.Scheme == "NFSM-oilseeds")
                    {
                        string schemecode = schemes.Sid;
                        foreach (PanchayatData block in blk)
                        {
                            if (schemecode == "10")
                            {
                                var beneficiaryLists = ofmasclient.getOFMAS_BeneficiaryListAsync(this.config.Value.OfmasPasscode, Convert.ToString(schemes.F_Year), Convert.ToInt64(schemes.Sid), Convert.ToString(block.Block_lg_code)).Result.ToList();

                                if (beneficiaryLists[0].check1 == "1")
                                {
                                    beneficiaryList.AddRange(beneficiaryLists);
                                }
                            }
                        }
                        bihanAppBeneficiaryListOilseeds = beneficiaryList.Select(x => new BeneficiaryList()
                        {
                            check1 = x.check1,
                            Finacial_Year = x.F_Year,
                            Scheme = x.Scheme,
                            Farmer_RegID = x.Farmer_RegID,
                            Beneficiary_Name = x.BeneficiaryName,
                            Beneficiary_Type = x.BeneficiaryCategory,
                            District_name = x.dist_name,
                            District_lg_Code = x.dist_code,
                            Block_name = x.block_name,
                            Block_lg_Code = x.block_code,
                            Panchayat_name = x.pan_name,
                            Panchayat_lg_Code = x.pan_code,
                            Village_name = x.villg_name,
                            Village_lg_Code = x.villg_code,
                            Implement = x.Implement,
                            fullcost = Convert.ToDecimal(x.fullcost),
                            subsidy_amt = Convert.ToDecimal(x.subsidy_amt),
                            subsidy_date = x.subsidy_date,
                            rec_created_date = DateTime.Now,
                            rec_created_userid = null,
                        }).ToList();
                        DataTable dt = SqlHelper.LinqToDataTable(bihanAppBeneficiaryListOilseeds.AsEnumerable());
                        result = this.miscService.InsertNHMData(dt, schemecode);
                    }
                }

                if (result > 0)
                {
                    return this.Ok();
                }
                else
                {
                    return this.NotFound("No Schemes Found");
                }
            }
            catch (Exception ex)
            {
                return this.Problem(ex.Message);
            }
        }

        /// <summary>
        /// Insert_OFMAS_ApplicationDetails_NFSM_Oilseeds.
        /// </summary>
        /// <returns>Result.</returns>
        [AllowAnonymous]
        [HttpPost("Insert_OFMAS_ApplicationDetails_NFSM")]
        public IActionResult Insert_OFMAS_ApplicationDetails_NFSM()
        {
            try
            {
                int result = 0;

                OFMAS_Report.OFMASReportWebServiceSoapClient ofmasclient = new OFMAS_Report.OFMASReportWebServiceSoapClient(OFMAS_Report.OFMASReportWebServiceSoapClient.EndpointConfiguration.OFMASReportWebServiceSoap);
                ofmasclient.ClientCredentials.ServiceCertificate.SslCertificateAuthentication =
                new X509ServiceCertificateAuthentication()
                {
                    CertificateValidationMode = X509CertificateValidationMode.None,
                    RevocationMode = System.Security.Cryptography.X509Certificates.X509RevocationMode.NoCheck,
                };
                List<OFMAS_Scheme> scheme = ofmasclient.getOFMAS_SchemeAsync(this.config.Value.OfmasPasscode).Result.ToList();
                List<OFMAS_BeneficiaryList> beneficiaryList = new List<OFMAS_BeneficiaryList>();
                List<BeneficiaryList> bihanAppBeneficiaryListNFSM = new List<BeneficiaryList>();
                List<PanchayatData> blk = this.miscRepository.GetOfmasBlock();

                foreach (OFMAS_Scheme schemes in scheme)
                {
                    if (schemes.Scheme == "NFSM")
                    {
                        string schemecode = schemes.Sid;
                        foreach (PanchayatData block in blk)
                        {
                            if (schemecode == "1")
                            {
                                var beneficiaryLists = ofmasclient.getOFMAS_BeneficiaryListAsync(this.config.Value.OfmasPasscode, Convert.ToString(schemes.F_Year), Convert.ToInt64(schemes.Sid), Convert.ToString(block.Block_lg_code)).Result.ToList();

                                if (beneficiaryLists[0].check1 == "1")
                                {
                                    beneficiaryList.AddRange(beneficiaryLists);
                                }
                            }
                        }
                        bihanAppBeneficiaryListNFSM = beneficiaryList.Select(x => new BeneficiaryList()
                        {
                            check1 = x.check1,
                            Finacial_Year = x.F_Year,
                            Scheme = x.Scheme,
                            Farmer_RegID = x.Farmer_RegID,
                            Beneficiary_Name = x.BeneficiaryName,
                            Beneficiary_Type = x.BeneficiaryCategory,
                            District_name = x.dist_name,
                            District_lg_Code = x.dist_code,
                            Block_name = x.block_name,
                            Block_lg_Code = x.block_code,
                            Panchayat_name = x.pan_name,
                            Panchayat_lg_Code = x.pan_code,
                            Village_name = x.villg_name,
                            Village_lg_Code = x.villg_code,
                            Implement = x.Implement,
                            fullcost = Convert.ToDecimal(x.fullcost),
                            subsidy_amt = Convert.ToDecimal(x.subsidy_amt),
                            subsidy_date = x.subsidy_date,
                            rec_created_date = DateTime.Now,
                            rec_created_userid = null,
                        }).ToList();
                        DataTable dt = SqlHelper.LinqToDataTable(bihanAppBeneficiaryListNFSM.AsEnumerable());
                        result = this.miscService.InsertNHMData(dt, schemecode);
                    }
                }

                if (result > 0)
                {
                    return this.Ok();
                }
                else
                {
                    return this.NotFound("No Schemes Found");
                }
            }
            catch (Exception ex)
            {
                return this.Problem(ex.Message);
            }
        }

        /// <summary>
        /// Insert_OFMAS_ApplicationDetails_State_Plan.
        /// </summary>
        /// <returns>Result.</returns>
        [AllowAnonymous]
        [HttpPost("Insert_OFMAS_ApplicationDetails_State_Plan")]
        public IActionResult Insert_OFMAS_ApplicationDetails_State_Plan()
        {
            try
            {
                int result = 0;

                OFMAS_Report.OFMASReportWebServiceSoapClient ofmasclient = new OFMAS_Report.OFMASReportWebServiceSoapClient(OFMAS_Report.OFMASReportWebServiceSoapClient.EndpointConfiguration.OFMASReportWebServiceSoap);
                ofmasclient.ClientCredentials.ServiceCertificate.SslCertificateAuthentication =
                new X509ServiceCertificateAuthentication()
                {
                    CertificateValidationMode = X509CertificateValidationMode.None,
                    RevocationMode = System.Security.Cryptography.X509Certificates.X509RevocationMode.NoCheck,
                };
                List<OFMAS_Scheme> scheme = ofmasclient.getOFMAS_SchemeAsync(this.config.Value.OfmasPasscode).Result.ToList();
                List<OFMAS_BeneficiaryList> beneficiaryList = new List<OFMAS_BeneficiaryList>();
                List<BeneficiaryList> bihanAppBeneficiaryListStatePlan = new List<BeneficiaryList>();
                List<PanchayatData> blk = this.miscRepository.GetOfmasBlock();

                foreach (OFMAS_Scheme schemes in scheme)
                {
                    if (schemes.Scheme == "State Plan")
                    {
                        string schemecode = schemes.Sid;
                        foreach (PanchayatData block in blk)
                        {
                            if (schemecode == "6")
                            {
                                var beneficiaryLists = ofmasclient.getOFMAS_BeneficiaryListAsync(this.config.Value.OfmasPasscode, Convert.ToString(schemes.F_Year), Convert.ToInt64(schemes.Sid), Convert.ToString(block.Block_lg_code)).Result.ToList();

                                if (beneficiaryLists[0].check1 == "1")
                                {
                                    beneficiaryList.AddRange(beneficiaryLists);
                                }
                            }
                        }
                        bihanAppBeneficiaryListStatePlan = beneficiaryList.Select(x => new BeneficiaryList()
                        {
                            check1 = x.check1,
                            Finacial_Year = x.F_Year,
                            Scheme = x.Scheme,
                            Farmer_RegID = x.Farmer_RegID,
                            Beneficiary_Name = x.BeneficiaryName,
                            Beneficiary_Type = x.BeneficiaryCategory,
                            District_name = x.dist_name,
                            District_lg_Code = x.dist_code,
                            Block_name = x.block_name,
                            Block_lg_Code = x.block_code,
                            Panchayat_name = x.pan_name,
                            Panchayat_lg_Code = x.pan_code,
                            Village_name = x.villg_name,
                            Village_lg_Code = x.villg_code,
                            Implement = x.Implement,
                            fullcost = Convert.ToDecimal(x.fullcost),
                            subsidy_amt = Convert.ToDecimal(x.subsidy_amt),
                            subsidy_date = x.subsidy_date,
                            rec_created_date = DateTime.Now,
                            rec_created_userid = null,
                        }).ToList();
                        DataTable dt = SqlHelper.LinqToDataTable(bihanAppBeneficiaryListStatePlan.AsEnumerable());
                        result = this.miscService.InsertNHMData(dt, schemecode);
                    }
                }

                if (result > 0)
                {
                    return this.Ok();
                }
                else
                {
                    return this.NotFound("No Schemes Found");
                }
            }
            catch (Exception ex)
            {
                return this.Problem(ex.Message);
            }
        }
    }
}