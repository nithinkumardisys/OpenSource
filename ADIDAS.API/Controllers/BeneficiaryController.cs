//------------------------------------------------------------------------------
// <copyright file="BeneficiaryController.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------
namespace ADIDAS.API.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.ServiceModel.Security;
    using ADIDAS.Model.DTO;
    using ADIDAS.Model.Entities;
    using ADIDAS.Repository.Helper;
    using ADIDAS.Repository.Interfaces;
    using ADIDAS.Service.DataServices;
    using ADIDAS.Service.Interfaces;
    using BiharAgriWebService;
    using HorticultureService;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using OFMAS_Report;
    using Swashbuckle.Swagger;

    /// <summary>
    /// BeneficiaryController.
    /// </summary>
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class BeneficiaryController : ControllerBase
    {
        private readonly IBeneficiaryService beneficiaryService;
        private readonly ISoilConservationService soilConservationService;
        private readonly ILogger<BeneficiaryController> logger;
        private readonly IHorticultureService horticultureService;
        private readonly IOptions<ExternalApi> config;

        /// <summary>
        /// Initializes a new instance of the <see cref="BeneficiaryController"/> class.
        /// </summary>
        /// <param name="beneficiaryService">beneficiaryService.</param>
        /// <param name="soilConservationService">soilConservationService.</param>
        /// <param name="logger">logger.</param>
        /// <param name="horticultureService">horticultureService.</param>
        /// <param name="config">config.</param>
        public BeneficiaryController(IBeneficiaryService beneficiaryService, ISoilConservationService soilConservationService, ILogger<BeneficiaryController> logger, IHorticultureService horticultureService, IOptions<ExternalApi> config)
        {
            this.logger = logger;
            this.beneficiaryService = beneficiaryService;
            this.soilConservationService = soilConservationService;
            this.horticultureService = horticultureService;
            this.config = config;
        }

        /// <summary>
        /// GetBIAUserDetails.
        /// </summary>
        /// <param name="userid">userid.</param>
        /// <returns>User Details.</returns>
        [HttpGet("GetBIAUserDetails/{userid}")]
        public IActionResult GetBIAUserDetails(int userid)
        {
            try
            {
                var result = this.beneficiaryService.GetBIAUserDetails(userid);
                return this.Ok(result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound();
            }
        }

        /// <summary>
        /// GetBIADirectorateList.
        /// </summary>
        /// <returns>Directorate List.</returns>
        [HttpGet("GetBIADirectorateList")]
        public IActionResult GetBIADirectorateList()
        {
            try
            {
                var result = this.beneficiaryService.GetBIADirectorateList();
                return this.Ok(result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound();
            }
        }

        /// <summary>
        /// GetBIASchemeNames.
        /// </summary>
        /// <param name="directorate">directorate.</param>
        /// <returns>Scheme List.</returns>
        [HttpGet("GetBIASchemeNames/{directorate}")]
        public IActionResult GetBIASchemeNames(string directorate)
        {
            try
            {
                var result = this.beneficiaryService.GetBIASchemeNames(directorate);
                return this.Ok(result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound();
            }
        }

        /// <summary>
        /// GetBIACount.
        /// </summary>
        /// <param name="biaWebFilters">biaWebFilters.</param>
        /// <returns>BIA Count.</returns>
        [HttpGet("GetBIACount")]
        public ActionResult GetBIACount(BiaWebFilters biaWebFilters)
        {
            try
            {
                List<BiaWebGridDetails> biaWebGridDetails = new List<BiaWebGridDetails>();
                BiaWebFilters biaWebFiltersInput = new BiaWebFilters();
                BiaCount biaCount = new BiaCount();
                int distId = biaWebFilters.DistrictId.Value;
                List<BlockPanchayatLgCodes> blockPanchayatLgCodes = this.horticultureService.GetBlockPanchaytLgCodes(distId);
                int distLgCode = blockPanchayatLgCodes.Where(x => x.District_id == biaWebFilters.DistrictId).Select(x => x.District_lg_code).FirstOrDefault();
                List<BiaScheme> biaScheme = this.beneficiaryService.GetBIASchemeNames(biaWebFilters.Directorate);
                int schemeId = biaScheme.Where(x => x.Scheme_name == biaWebFilters.SchemeName).Select(x => x.Scheme_id).FirstOrDefault() ?? 0;
                int blockLgCode = blockPanchayatLgCodes.Where(x => x.Block_id == biaWebFilters.BlockId).Select(x => x.Block_lg_code).FirstOrDefault();
                int panchayatLgCode = blockPanchayatLgCodes.Where(x => x.Panchayat_id == biaWebFilters.PanchayatId).Select(x => x.Panchayat_lg_code).FirstOrDefault();

                WebService_BihanAppSoapClient client = new WebService_BihanAppSoapClient(WebService_BihanAppSoapClient.EndpointConfiguration.WebService_BihanAppSoap);
                client.ClientCredentials.ServiceCertificate.SslCertificateAuthentication =
                new X509ServiceCertificateAuthentication()
                {
                    CertificateValidationMode = X509CertificateValidationMode.None,
                    RevocationMode = System.Security.Cryptography.X509Certificates.X509RevocationMode.NoCheck,
                };

                BiharAgariWebServiceSoapClient biharAgriWebServiceclient = new BiharAgariWebServiceSoapClient(BiharAgariWebServiceSoapClient.EndpointConfiguration.BiharAgariWebServiceSoap);
                client.ClientCredentials.ServiceCertificate.SslCertificateAuthentication =
                new X509ServiceCertificateAuthentication()
                {
                    CertificateValidationMode = X509CertificateValidationMode.None,
                    RevocationMode = System.Security.Cryptography.X509Certificates.X509RevocationMode.NoCheck,
                };

                OFMAS_Report.OFMASReportWebServiceSoapClient ofmasclient = new OFMAS_Report.OFMASReportWebServiceSoapClient(OFMAS_Report.OFMASReportWebServiceSoapClient.EndpointConfiguration.OFMASReportWebServiceSoap);
                ofmasclient.ClientCredentials.ServiceCertificate.SslCertificateAuthentication =
                new X509ServiceCertificateAuthentication()
                {
                    CertificateValidationMode = X509CertificateValidationMode.None,
                    RevocationMode = System.Security.Cryptography.X509Certificates.X509RevocationMode.NoCheck,
                };
                List<OFMAS_Scheme> scheme = ofmasclient.getOFMAS_SchemeAsync(this.config.Value.OfmasPasscode).Result.ToList();

                List<NHM_CMHM_ApplicationDetails> nhm_cmhm_Applications = new List<NHM_CMHM_ApplicationDetails>();
                List<PMKSY_ApplicationDetails> pmksy = new List<PMKSY_ApplicationDetails>();
                List<Nalkoop_ApplicationDetails> nalkoop = new List<Nalkoop_ApplicationDetails>();
                List<SoilConservationSubmittedDataResponse> soilConservation = new List<SoilConservationSubmittedDataResponse>();
                List<OFMAS_CHCBeneficiaryList> kkaBeneficiaryList = new List<OFMAS_CHCBeneficiaryList>();
                List<OFMAS_CHCBeneficiaryList> schcBeneficiaryList = new List<OFMAS_CHCBeneficiaryList>();
                List<OFMAS_CHCBeneficiaryList> chcBeneficiaryList = new List<OFMAS_CHCBeneficiaryList>();
                List<OFMAS_CHCBeneficiaryList> fmbBeneficiaryList = new List<OFMAS_CHCBeneficiaryList>();
                List<OFMAS_BeneficiaryList> beneficiaryList = new List<OFMAS_BeneficiaryList>();
                List<beneficiaryDetails> pmkisanBeneficiary = new List<beneficiaryDetails>();

                if (biaWebFilters.SchemeName.Equals("NHM"))
                {
                    var data = client.Get_NHM_CMHM_ApplicationDetailsAsync(this.config.Value.NHM_CNHM, this.config.Value.NHM_SchemeID, Convert.ToString(distLgCode)).Result.ToList();
                    nhm_cmhm_Applications.AddRange(data);
                    biaWebGridDetails = nhm_cmhm_Applications.Select(x => new BiaWebGridDetails()
                    {
                        SchemeName = biaWebFilters.SchemeName,
                        RegistrationNo = x._DBTRegistrationNo,
                        ApplicationNo = x._ApplicationId,
                        BeneficiaryName = x._FarmerName,
                        MobileNo = x._MobileNo,
                        DistrictName = x._District,
                        BlockName = x._Block,
                        PanchayatName = x._Panchayat,
                        TransactionDate = x._TransactionDate,
                        SubsidyReceived = x._Amountbenefited,
                    }).ToList();

                    biaWebFiltersInput.SchemeName = biaWebFilters.SchemeName;
                    biaWebFiltersInput.DistrictId = biaWebFilters.DistrictId;
                }
                else if (biaWebFilters.SchemeName.Equals("CMHM"))
                {
                    var data = client.Get_NHM_CMHM_ApplicationDetailsAsync(this.config.Value.NHM_CNHM, this.config.Value.CMHM_SchemeID, Convert.ToString(distLgCode)).Result.ToList();
                    nhm_cmhm_Applications.AddRange(data);
                    biaWebGridDetails = nhm_cmhm_Applications.Select(x => new BiaWebGridDetails()
                    {
                        SchemeName = biaWebFilters.SchemeName,
                        RegistrationNo = x._DBTRegistrationNo,
                        ApplicationNo = x._ApplicationId,
                        BeneficiaryName = x._FarmerName,
                        MobileNo = x._MobileNo,
                        DistrictName = x._District,
                        BlockName = x._Block,
                        PanchayatName = x._Panchayat,
                        TransactionDate = x._TransactionDate,
                        SubsidyReceived = x._Amountbenefited,
                    }).ToList();

                    biaWebFiltersInput.SchemeName = biaWebFilters.SchemeName;
                    biaWebFiltersInput.DistrictId = biaWebFilters.DistrictId;
                }
                else if (biaWebFilters.SchemeName.Equals("PMKSY"))
                {
                    var data = client.Get_PMKSY_ApplicationDetailsAsync(this.config.Value.PMKSY).Result.ToList();
                    pmksy.AddRange(data);
                    biaWebGridDetails = pmksy.Select(x => new BiaWebGridDetails()
                    {
                        SchemeName = biaWebFilters.SchemeName,
                        RegistrationNo = x._DBTRegistrationNo,
                        ApplicationNo = x._ApplicationId,
                        BeneficiaryName = x._FarmerName,
                        MobileNo = x._MobileNo,
                        DistrictName = x._District,
                        BlockName = x._Block,
                        PanchayatName = x._Panchayat,
                        TransactionDate = x._TransactionDate,
                        SubsidyReceived = x._Amountbenefited,
                    }).ToList();

                    biaWebFiltersInput.SchemeName = biaWebFilters.SchemeName;
                }
                else if (biaWebFilters.SchemeName.Equals("Nalkoop"))
                {
                    var data = client.Get_Nalkoop_ApplicationDetailsAsync(this.config.Value.Nalkoop, Convert.ToString(distLgCode)).Result.ToList();
                    nalkoop.AddRange(data);
                    biaWebGridDetails = nalkoop.Select(x => new BiaWebGridDetails()
                    {
                        SchemeName = biaWebFilters.SchemeName,
                        RegistrationNo = x._DBTRegistrationNo,
                        ApplicationNo = x._ApplicationId,
                        BeneficiaryName = x._FarmerName,
                        MobileNo = x._MobileNo,
                        DistrictName = x._District,
                        BlockName = x._Block,
                        PanchayatName = x._Panchayat,
                        TransactionDate = x._TransactionDate,
                        SubsidyReceived = x._Amountbenefited,
                    }).ToList();

                    biaWebFiltersInput.SchemeName = biaWebFilters.SchemeName;
                    biaWebFiltersInput.DistrictId = biaWebFilters.DistrictId;
                }
                else if (biaWebFilters.SchemeName.Equals("Soil Conservation"))
                {
                    int panchId = biaWebFilters.PanchayatId ?? 0;
                    soilConservation = this.soilConservationService.GetSoilConservationSubmittedData(panchId, schemeId);
                    int i = 2;
                    if (soilConservation != null)
                    {
                        foreach (SoilConservationSubmittedDataResponse soilConservationSubmittedDataResponse in soilConservation)
                        {
                            foreach (SubmittedActivityList activityList in soilConservationSubmittedDataResponse.Activity_list)
                            {
                                foreach (SubmittedSubActivityList subActivityList in activityList.Sub_activity_list)
                                {
                                    foreach (SubActivityDetail subActivityDetail in subActivityList.Sub_activity_details)
                                    {
                                        if (subActivityDetail.Is_final_submission == true)
                                        {
                                            biaWebGridDetails.Add(new BiaWebGridDetails
                                            {
                                                SchemeName = biaWebFilters.SchemeName,
                                                RegistrationNo = !string.IsNullOrEmpty(subActivityDetail.Registration_no) ?
                                                (long.Parse(subActivityDetail.Registration_no) * i).ToString().Substring(0, 10) : (i * 9999999).ToString(),
                                                ApplicationNo = string.Empty,
                                                BeneficiaryName = !string.IsNullOrEmpty(subActivityDetail.Beneficiary_name) ? subActivityDetail.Beneficiary_name : string.Empty,
                                                MobileNo = !string.IsNullOrEmpty(subActivityDetail.Mobile_number) ? subActivityDetail.Mobile_number : string.Empty,
                                                DistrictName = !string.IsNullOrEmpty(subActivityDetail.Name_Of_district) ? subActivityDetail.Name_Of_district : string.Empty,
                                                BlockName = !string.IsNullOrEmpty(subActivityDetail.Name_Of_block) ? subActivityDetail.Name_Of_block : string.Empty,
                                                PanchayatName = !string.IsNullOrEmpty(subActivityDetail.Name_Of_panchayat) ? subActivityDetail.Name_Of_panchayat : string.Empty,
                                                TransactionDate = subActivityDetail.Actual_date_of_completion.HasValue ? DateTime.Parse(subActivityDetail.Actual_date_of_completion.ToString()).ToString("dd/MM/yyyy") : string.Empty,
                                                SubsidyReceived = !string.IsNullOrEmpty(subActivityDetail.Structure_name) ? subActivityDetail.Structure_name : string.Empty,
                                            });
                                            i++;
                                        }
                                    }
                                }
                            }
                        }
                    }

                    biaWebFiltersInput.SchemeName = biaWebFilters.SchemeName;
                    biaWebFiltersInput.PanchayatId = biaWebFilters.PanchayatId;
                }
                else if (biaWebFilters.SchemeName == "KKA")
                {
                    var fab_DATA = ofmasclient.getOFMAS_CHCBeneficiaryListAsync(this.config.Value.OfmasPasscode, Convert.ToString(biaWebFilters.FinYear), Convert.ToInt64(scheme.Where(x => x.Scheme == "KKA").Select(x => x.Sid).FirstOrDefault()), Convert.ToInt64(scheme.Where(x => x.Scheme == "KKA").Select(x => x.Phase).FirstOrDefault())).Result.ToList();
                    kkaBeneficiaryList.AddRange(fab_DATA);
                    biaWebGridDetails = kkaBeneficiaryList.Select(x => new BiaWebGridDetails()
                    {
                        SchemeName = biaWebFilters.SchemeName,
                        RegistrationNo = x.BeneficiaryName.Substring(4) + x.subsidy_amt,
                        ApplicationNo = " ",
                        BeneficiaryName = x.BeneficiaryName,
                        MobileNo = " ",
                        BeneficiaryType = x.BeneficiaryType,
                        DistrictName = x.dist_name,
                        BlockName = x.block_name,
                        PanchayatName = x.pan_name,
                        TransactionDate = x.subsidy_date,
                        SubsidyReceived = x.subsidy_amt,
                    }).ToList();

                    biaWebFiltersInput.SchemeName = biaWebFilters.SchemeName;
                    biaWebFiltersInput.FinYear = biaWebFilters.FinYear;
                }
                else if (biaWebFilters.SchemeName == "SMAM-CHC")
                {
                    var fab_DATA = ofmasclient.getOFMAS_CHCBeneficiaryListAsync(this.config.Value.OfmasPasscode, Convert.ToString(biaWebFilters.FinYear), Convert.ToInt64(scheme.Where(x => x.Scheme == "SMAM-CHC").Select(x => x.Sid).FirstOrDefault()), Convert.ToInt64(scheme.Where(x => x.Scheme == "SMAM-CHC").Select(x => x.Phase).FirstOrDefault())).Result.ToList();
                    chcBeneficiaryList.AddRange(fab_DATA);
                    biaWebGridDetails = chcBeneficiaryList.Select(x => new BiaWebGridDetails()
                    {
                        SchemeName = biaWebFilters.SchemeName,
                        RegistrationNo = x.BeneficiaryName.Substring(4) + x.subsidy_amt,
                        ApplicationNo = " ",
                        BeneficiaryName = x.BeneficiaryName,
                        MobileNo = " ",
                        BeneficiaryType = x.BeneficiaryType,
                        DistrictName = x.dist_name,
                        BlockName = x.block_name,
                        PanchayatName = x.pan_name,
                        TransactionDate = x.subsidy_date,
                        SubsidyReceived = x.subsidy_amt,
                    }).ToList();

                    biaWebFiltersInput.SchemeName = biaWebFilters.SchemeName;
                    biaWebFiltersInput.FinYear = biaWebFilters.FinYear;
                }
                else if (biaWebFilters.SchemeName == "SMAM-SCHC")
                {
                    var fab_DATA = ofmasclient.getOFMAS_CHCBeneficiaryListAsync(this.config.Value.OfmasPasscode, Convert.ToString(biaWebFilters.FinYear), Convert.ToInt64(scheme.Where(x => x.Scheme == "SMAM-SCHC").Select(x => x.Sid).FirstOrDefault()), Convert.ToInt64(scheme.Where(x => x.Scheme == "SMAM-SCHC").Select(x => x.Phase).FirstOrDefault())).Result.ToList();
                    schcBeneficiaryList.AddRange(fab_DATA);
                    biaWebGridDetails = schcBeneficiaryList.Select(x => new BiaWebGridDetails()
                    {
                        SchemeName = biaWebFilters.SchemeName,
                        RegistrationNo = x.BeneficiaryName.Substring(4) + x.subsidy_amt,
                        ApplicationNo = " ",
                        BeneficiaryName = x.BeneficiaryName,
                        MobileNo = " ",
                        BeneficiaryType = x.BeneficiaryType,
                        DistrictName = x.dist_name,
                        BlockName = x.block_name,
                        PanchayatName = x.pan_name,
                        TransactionDate = x.subsidy_date,
                        SubsidyReceived = x.subsidy_amt,
                    }).ToList();

                    biaWebFiltersInput.SchemeName = biaWebFilters.SchemeName;
                    biaWebFiltersInput.FinYear = biaWebFilters.FinYear;
                }
                else if (biaWebFilters.SchemeName == "SMAM-FMB")
                {
                    var fab_DATA = ofmasclient.getOFMAS_CHCBeneficiaryListAsync(this.config.Value.OfmasPasscode, Convert.ToString(biaWebFilters.FinYear), Convert.ToInt64(scheme.Where(x => x.Scheme == "SMAM-FMB").Select(x => x.Sid).FirstOrDefault()), Convert.ToInt64(scheme.Where(x => x.Scheme == "SMAM-FMB").Select(x => x.Phase).FirstOrDefault())).Result.ToList();
                    fmbBeneficiaryList.AddRange(fab_DATA);
                    biaWebGridDetails = fmbBeneficiaryList.Select(x => new BiaWebGridDetails()
                    {
                        SchemeName = biaWebFilters.SchemeName,
                        RegistrationNo = x.BeneficiaryName.Substring(4) + x.subsidy_amt,
                        ApplicationNo = " ",
                        BeneficiaryName = x.BeneficiaryName,
                        MobileNo = " ",
                        BeneficiaryType = x.BeneficiaryType,
                        DistrictName = x.dist_name,
                        BlockName = x.block_name,
                        PanchayatName = x.pan_name,
                        TransactionDate = x.subsidy_date,
                        SubsidyReceived = x.subsidy_amt,
                    }).ToList();

                    biaWebFiltersInput.SchemeName = biaWebFilters.SchemeName;
                    biaWebFiltersInput.FinYear = biaWebFilters.FinYear;
                }
                else if (biaWebFilters.SchemeName == "NFSM")
                {
                    var beneficiaryLists = ofmasclient.getOFMAS_BeneficiaryListAsync(this.config.Value.OfmasPasscode, Convert.ToString(biaWebFilters.FinYear), Convert.ToInt64(scheme.Where(x => x.Scheme == "NFSM").Select(x => x.Sid).FirstOrDefault()), Convert.ToString(blockLgCode)).Result.ToList();
                    if (beneficiaryLists[0].check1 == "1")
                    {
                        beneficiaryList.AddRange(beneficiaryLists);
                    }

                    biaWebGridDetails = beneficiaryList.Select(x => new BiaWebGridDetails()
                    {
                        SchemeName = biaWebFilters.SchemeName,
                        RegistrationNo = x.Farmer_RegID,
                        ApplicationNo = " ",
                        BeneficiaryName = x.BeneficiaryName,
                        MobileNo = " ",
                        BeneficiaryType = x.BeneficiaryCategory,
                        DistrictName = x.dist_name,
                        BlockName = x.block_name,
                        PanchayatName = x.pan_name,
                        TransactionDate = x.subsidy_date,
                        SubsidyReceived = x.subsidy_amt,
                    }).ToList();

                    biaWebFiltersInput.SchemeName = biaWebFilters.SchemeName;
                    biaWebFiltersInput.FinYear = biaWebFilters.FinYear;
                    biaWebFiltersInput.BlockId = biaWebFilters.BlockId;
                }
                else if (biaWebFilters.SchemeName == "NFSM-oilseeds")
                {
                    var beneficiaryLists = ofmasclient.getOFMAS_BeneficiaryListAsync(this.config.Value.OfmasPasscode, Convert.ToString(biaWebFilters.FinYear), Convert.ToInt64(scheme.Where(x => x.Scheme == "NFSM-oilseeds").Select(x => x.Sid).FirstOrDefault()), Convert.ToString(blockLgCode)).Result.ToList();
                    if (beneficiaryLists[0].check1 == "1")
                    {
                        beneficiaryList.AddRange(beneficiaryLists);
                    }

                    biaWebGridDetails = beneficiaryList.Select(x => new BiaWebGridDetails()
                    {
                        SchemeName = biaWebFilters.SchemeName,
                        RegistrationNo = x.Farmer_RegID,
                        ApplicationNo = " ",
                        BeneficiaryName = x.BeneficiaryName,
                        MobileNo = " ",
                        BeneficiaryType = x.BeneficiaryCategory,
                        DistrictName = x.dist_name,
                        BlockName = x.block_name,
                        PanchayatName = x.pan_name,
                        TransactionDate = x.subsidy_date,
                        SubsidyReceived = x.subsidy_amt,
                    }).ToList();

                    biaWebFiltersInput.SchemeName = biaWebFilters.SchemeName;
                    biaWebFiltersInput.FinYear = biaWebFilters.FinYear;
                    biaWebFiltersInput.BlockId = biaWebFilters.BlockId;
                }
                else if (biaWebFilters.SchemeName == "State Plan")
                {
                    var beneficiaryLists = ofmasclient.getOFMAS_BeneficiaryListAsync(this.config.Value.OfmasPasscode, Convert.ToString(biaWebFilters.FinYear), Convert.ToInt64(scheme.Where(x => x.Scheme == "State Plan").Select(x => x.Sid).FirstOrDefault()), Convert.ToString(blockLgCode)).Result.ToList();
                    if (beneficiaryLists[0].check1 == "1")
                    {
                        beneficiaryList.AddRange(beneficiaryLists);
                    }

                    biaWebGridDetails = beneficiaryList.Select(x => new BiaWebGridDetails()
                    {
                        SchemeName = biaWebFilters.SchemeName,
                        RegistrationNo = x.Farmer_RegID,
                        ApplicationNo = " ",
                        BeneficiaryName = x.BeneficiaryName,
                        MobileNo = " ",
                        BeneficiaryType = x.BeneficiaryCategory,
                        DistrictName = x.dist_name,
                        BlockName = x.block_name,
                        PanchayatName = x.pan_name,
                        TransactionDate = x.subsidy_date,
                        SubsidyReceived = x.subsidy_amt,
                    }).ToList();

                    biaWebFiltersInput.SchemeName = biaWebFilters.SchemeName;
                    biaWebFiltersInput.FinYear = biaWebFilters.FinYear;
                    biaWebFiltersInput.BlockId = biaWebFilters.BlockId;
                }
                else if (biaWebFilters.SchemeName == "TRFA-Oilseeds")
                {
                    var beneficiaryLists = ofmasclient.getOFMAS_BeneficiaryListAsync(this.config.Value.OfmasPasscode, Convert.ToString(biaWebFilters.FinYear), Convert.ToInt64(scheme.Where(x => x.Scheme == "TRFA-Oilseeds").Select(x => x.Sid).FirstOrDefault()), Convert.ToString(blockLgCode)).Result.ToList();
                    if (beneficiaryLists[0].check1 == "1")
                    {
                        beneficiaryList.AddRange(beneficiaryLists);
                    }

                    biaWebGridDetails = beneficiaryList.Select(x => new BiaWebGridDetails()
                    {
                        SchemeName = biaWebFilters.SchemeName,
                        RegistrationNo = x.Farmer_RegID,
                        ApplicationNo = " ",
                        BeneficiaryName = x.BeneficiaryName,
                        MobileNo = " ",
                        BeneficiaryType = x.BeneficiaryCategory,
                        DistrictName = x.dist_name,
                        BlockName = x.block_name,
                        PanchayatName = x.pan_name,
                        TransactionDate = x.subsidy_date,
                        SubsidyReceived = x.subsidy_amt,
                    }).ToList();

                    biaWebFiltersInput.SchemeName = biaWebFilters.SchemeName;
                    biaWebFiltersInput.FinYear = biaWebFilters.FinYear;
                    biaWebFiltersInput.BlockId = biaWebFilters.BlockId;
                }
                else if (biaWebFilters.SchemeName == "TRFA-Pulses")
                {
                    var beneficiaryLists = ofmasclient.getOFMAS_BeneficiaryListAsync(this.config.Value.OfmasPasscode, Convert.ToString(biaWebFilters.FinYear), Convert.ToInt64(scheme.Where(x => x.Scheme == "TRFA-Pulses").Select(x => x.Sid).FirstOrDefault()), Convert.ToString(blockLgCode)).Result.ToList();
                    if (beneficiaryLists[0].check1 == "1")
                    {
                        beneficiaryList.AddRange(beneficiaryLists);
                    }

                    biaWebGridDetails = beneficiaryList.Select(x => new BiaWebGridDetails()
                    {
                        SchemeName = biaWebFilters.SchemeName,
                        RegistrationNo = x.Farmer_RegID,
                        ApplicationNo = " ",
                        BeneficiaryName = x.BeneficiaryName,
                        MobileNo = " ",
                        BeneficiaryType = x.BeneficiaryCategory,
                        DistrictName = x.dist_name,
                        BlockName = x.block_name,
                        PanchayatName = x.pan_name,
                        TransactionDate = x.subsidy_date,
                        SubsidyReceived = x.subsidy_amt,
                    }).ToList();

                    biaWebFiltersInput.SchemeName = biaWebFilters.SchemeName;
                    biaWebFiltersInput.FinYear = biaWebFilters.FinYear;
                    biaWebFiltersInput.BlockId = biaWebFilters.BlockId;
                }
                else if (biaWebFilters.SchemeName == "RKVY-RAFTAAR")
                {
                    var beneficiaryLists = ofmasclient.getOFMAS_BeneficiaryListAsync(this.config.Value.OfmasPasscode, Convert.ToString(biaWebFilters.FinYear), Convert.ToInt64(scheme.Where(x => x.Scheme == "RKVY-RAFTAAR").Select(x => x.Sid).FirstOrDefault()), Convert.ToString(blockLgCode)).Result.ToList();
                    if (beneficiaryLists[0].check1 == "1")
                    {
                        beneficiaryList.AddRange(beneficiaryLists);
                    }

                    biaWebGridDetails = beneficiaryList.Select(x => new BiaWebGridDetails()
                    {
                        SchemeName = biaWebFilters.SchemeName,
                        RegistrationNo = x.Farmer_RegID,
                        ApplicationNo = " ",
                        BeneficiaryName = x.BeneficiaryName,
                        MobileNo = " ",
                        BeneficiaryType = x.BeneficiaryCategory,
                        DistrictName = x.dist_name,
                        BlockName = x.block_name,
                        PanchayatName = x.pan_name,
                        TransactionDate = x.subsidy_date,
                        SubsidyReceived = x.subsidy_amt,
                    }).ToList();

                    biaWebFiltersInput.SchemeName = biaWebFilters.SchemeName;
                    biaWebFiltersInput.FinYear = biaWebFilters.FinYear;
                    biaWebFiltersInput.BlockId = biaWebFilters.BlockId;
                }
                else if (biaWebFilters.SchemeName == "PM-KISAN")
                {
                    var beneficiaryLists = biharAgriWebServiceclient.PmkisanBeneficiaryAsync(Convert.ToString(panchayatLgCode), this.config.Value.Passcode).Result.ToList();
                    pmkisanBeneficiary.AddRange(beneficiaryLists);
                    biaWebGridDetails = pmkisanBeneficiary.Select(x => new BiaWebGridDetails()
                    {
                        SchemeName = biaWebFilters.SchemeName,
                        RegistrationNo = x.Registration,
                        ApplicationNo = x.ApplicationId,
                        BeneficiaryName = x.FarmerName,
                        MobileNo = " ",
                        BeneficiaryType = " ",
                        DistrictName = x.Districtname,
                        BlockName = x.Blockname,
                        PanchayatName = x.panchayatname,
                        TransactionDate = x.sendactiondate,
                        SubsidyReceived = x.SanctionAmount + "rs",
                    }).ToList();

                    biaWebFiltersInput.SchemeName = biaWebFilters.SchemeName;
                    biaWebFiltersInput.PanchayatId = biaWebFilters.PanchayatId;
                }

                List<BiaWebGridDetails> biaWebGridDetailsAssignedAndPending = this.beneficiaryService.GetAssignedAndPendingData(biaWebFiltersInput);
                List<BiaWebGridDetails> biaWebGridDetailsVerified = this.beneficiaryService.GetVerifiedData(biaWebFiltersInput);

                biaCount.StateLevelBeneficiaryCount = biaWebGridDetails.Count;
                biaCount.NotAssignedCount = biaWebGridDetails.Count - (biaWebGridDetailsAssignedAndPending.Count + biaWebGridDetailsVerified.Count);
                biaCount.AssignedAndPendingCount = biaWebGridDetailsAssignedAndPending.Count;
                biaCount.Verified = biaWebGridDetailsVerified.Count;

                if (biaWebFilters.BeneficiaryNumber != null)
                {
                    List<BiaWebGridDetails> biaWebGridDetailsVerified1 = biaWebGridDetailsVerified.Where(x => x.RegistrationNo == biaWebFilters.BeneficiaryNumber).ToList();
                    List<BiaWebGridDetails> biaWebGridDetailsAssignedAndPending1 = biaWebGridDetailsAssignedAndPending.Where(x => x.RegistrationNo == biaWebFilters.BeneficiaryNumber).ToList();
                    List<BiaWebGridDetails> biaWebGridDetails1 = biaWebGridDetails.Where(x => x.RegistrationNo == biaWebFilters.BeneficiaryNumber).ToList();
                    if (biaWebGridDetailsVerified1.Count > 0 || biaWebGridDetailsAssignedAndPending1.Count > 0)
                    {
                        biaCount.StateLevelBeneficiaryCount = biaWebGridDetailsAssignedAndPending1.Count + biaWebGridDetailsVerified1.Count;
                        biaCount.NotAssignedCount = 0;
                        biaCount.AssignedAndPendingCount = biaWebGridDetailsAssignedAndPending1.Count;
                        biaCount.Verified = biaWebGridDetailsVerified1.Count;
                    }
                    else
                    {
                        biaCount.StateLevelBeneficiaryCount = biaWebGridDetails1.Count + biaWebGridDetailsAssignedAndPending1.Count + biaWebGridDetailsVerified1.Count;
                        biaCount.NotAssignedCount = biaWebGridDetails1.Count;
                        biaCount.AssignedAndPendingCount = biaWebGridDetailsAssignedAndPending1.Count;
                        biaCount.Verified = biaWebGridDetailsVerified1.Count;
                    }
                }

                if (biaWebFilters.Status != null && biaWebFilters.Status != "ALL")
                {
                    List<BiaWebGridDetails> biaWebGridDetailsVerified2 = biaWebGridDetailsVerified.Where(x => x.Status == biaWebFilters.Status).ToList();
                    biaCount.Verified = biaWebGridDetailsVerified2.Count;
                    biaCount.StateLevelBeneficiaryCount = biaWebGridDetails.Count - (biaWebGridDetailsVerified.Count - biaWebGridDetailsVerified2.Count);
                }

                if (biaWebFilters.BeneficiaryNumber != null && biaWebFilters.Status != null && biaWebFilters.Status != "ALL")
                {
                    List<BiaWebGridDetails> biaWebGridDetailsVerified1 = biaWebGridDetailsVerified.Where(x => x.RegistrationNo == biaWebFilters.BeneficiaryNumber).ToList();
                    biaWebGridDetailsVerified1 = biaWebGridDetailsVerified1.Where(x => x.Status == biaWebFilters.Status).ToList();

                    biaCount.StateLevelBeneficiaryCount = biaWebGridDetailsVerified1.Count;
                    biaCount.NotAssignedCount = 0;
                    biaCount.AssignedAndPendingCount = 0;
                    biaCount.Verified = biaWebGridDetailsVerified1.Count;
                }

                return this.Ok(biaCount);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound();
            }
        }

        /// <summary>
        /// GetBIADetailsBasedOnClick.
        /// </summary>
        /// <param name="biaWebFilters">biaWebFilters.</param>
        /// <returns>Bia Details List.</returns>
        [HttpGet("GetBIADetailsBasedOnClick")]
        public ActionResult GetBIADetailsBasedOnClick(BiaWebFilters biaWebFilters)
        {
            try
            {
                List<BiaWebGridDetails> biaWebGridDetails = new List<BiaWebGridDetails>();
                BiaWebFilters biaWebFiltersInput = new BiaWebFilters();
                int distId = biaWebFilters.DistrictId.Value;
                List<BlockPanchayatLgCodes> blockPanchayatLgCodes = this.horticultureService.GetBlockPanchaytLgCodes(distId);
                int distLgCode = blockPanchayatLgCodes.Where(x => x.District_id == biaWebFilters.DistrictId).Select(x => x.District_lg_code).FirstOrDefault();
                List<BiaScheme> biaScheme = this.beneficiaryService.GetBIASchemeNames(biaWebFilters.Directorate);
                int schemeId = biaScheme.Where(x => x.Scheme_name == biaWebFilters.SchemeName).Select(x => x.Scheme_id).FirstOrDefault() ?? 0;
                int blockLgCode = blockPanchayatLgCodes.Where(x => x.Block_id == biaWebFilters.BlockId).Select(x => x.Block_lg_code).FirstOrDefault();
                int panchayatLgCode = blockPanchayatLgCodes.Where(x => x.Panchayat_id == biaWebFilters.PanchayatId).Select(x => x.Panchayat_lg_code).FirstOrDefault();

                WebService_BihanAppSoapClient client = new WebService_BihanAppSoapClient(WebService_BihanAppSoapClient.EndpointConfiguration.WebService_BihanAppSoap);
                client.ClientCredentials.ServiceCertificate.SslCertificateAuthentication =
                new X509ServiceCertificateAuthentication()
                {
                    CertificateValidationMode = X509CertificateValidationMode.None,
                    RevocationMode = System.Security.Cryptography.X509Certificates.X509RevocationMode.NoCheck,
                };

                BiharAgariWebServiceSoapClient biharAgriWebServiceclient = new BiharAgariWebServiceSoapClient(BiharAgariWebServiceSoapClient.EndpointConfiguration.BiharAgariWebServiceSoap);
                client.ClientCredentials.ServiceCertificate.SslCertificateAuthentication =
                new X509ServiceCertificateAuthentication()
                {
                    CertificateValidationMode = X509CertificateValidationMode.None,
                    RevocationMode = System.Security.Cryptography.X509Certificates.X509RevocationMode.NoCheck,
                };

                OFMAS_Report.OFMASReportWebServiceSoapClient ofmasclient = new OFMAS_Report.OFMASReportWebServiceSoapClient(OFMAS_Report.OFMASReportWebServiceSoapClient.EndpointConfiguration.OFMASReportWebServiceSoap);
                ofmasclient.ClientCredentials.ServiceCertificate.SslCertificateAuthentication =
                new X509ServiceCertificateAuthentication()
                {
                    CertificateValidationMode = X509CertificateValidationMode.None,
                    RevocationMode = System.Security.Cryptography.X509Certificates.X509RevocationMode.NoCheck,
                };
                List<OFMAS_Scheme> scheme = ofmasclient.getOFMAS_SchemeAsync(this.config.Value.OfmasPasscode).Result.ToList();

                List<NHM_CMHM_ApplicationDetails> nhm_cmhm_Applications = new List<NHM_CMHM_ApplicationDetails>();
                List<PMKSY_ApplicationDetails> pmksy = new List<PMKSY_ApplicationDetails>();
                List<Nalkoop_ApplicationDetails> nalkoop = new List<Nalkoop_ApplicationDetails>();
                List<SoilConservationSubmittedDataResponse> soilConservation = new List<SoilConservationSubmittedDataResponse>();
                List<OFMAS_CHCBeneficiaryList> kkaBeneficiaryList = new List<OFMAS_CHCBeneficiaryList>();
                List<OFMAS_CHCBeneficiaryList> schcBeneficiaryList = new List<OFMAS_CHCBeneficiaryList>();
                List<OFMAS_CHCBeneficiaryList> chcBeneficiaryList = new List<OFMAS_CHCBeneficiaryList>();
                List<OFMAS_CHCBeneficiaryList> fmbBeneficiaryList = new List<OFMAS_CHCBeneficiaryList>();
                List<OFMAS_BeneficiaryList> beneficiaryList = new List<OFMAS_BeneficiaryList>();
                List<beneficiaryDetails> pmkisanBeneficiary = new List<beneficiaryDetails>();

                if (biaWebFilters.SchemeName.Equals("NHM"))
                {
                    var data = client.Get_NHM_CMHM_ApplicationDetailsAsync(this.config.Value.NHM_CNHM, this.config.Value.NHM_SchemeID, Convert.ToString(distLgCode)).Result.ToList();
                    nhm_cmhm_Applications.AddRange(data);
                    biaWebGridDetails = nhm_cmhm_Applications.Select(x => new BiaWebGridDetails()
                    {
                        SchemeName = biaWebFilters.SchemeName,
                        RegistrationNo = x._DBTRegistrationNo,
                        ApplicationNo = x._ApplicationId,
                        BeneficiaryName = x._FarmerName,
                        MobileNo = x._MobileNo,
                        DistrictName = x._District,
                        BlockName = x._Block,
                        PanchayatName = x._Panchayat,
                        TransactionDate = x._TransactionDate,
                        SubsidyReceived = x._Amountbenefited,
                    }).ToList();

                    biaWebFiltersInput.SchemeName = biaWebFilters.SchemeName;
                    biaWebFiltersInput.DistrictId = biaWebFilters.DistrictId;
                }
                else if (biaWebFilters.SchemeName.Equals("CMHM"))
                {
                    var data = client.Get_NHM_CMHM_ApplicationDetailsAsync(this.config.Value.NHM_CNHM, this.config.Value.CMHM_SchemeID, Convert.ToString(distLgCode)).Result.ToList();
                    nhm_cmhm_Applications.AddRange(data);
                    biaWebGridDetails = nhm_cmhm_Applications.Select(x => new BiaWebGridDetails()
                    {
                        SchemeName = biaWebFilters.SchemeName,
                        RegistrationNo = x._DBTRegistrationNo,
                        ApplicationNo = x._ApplicationId,
                        BeneficiaryName = x._FarmerName,
                        MobileNo = x._MobileNo,
                        DistrictName = x._District,
                        BlockName = x._Block,
                        PanchayatName = x._Panchayat,
                        TransactionDate = x._TransactionDate,
                        SubsidyReceived = x._Amountbenefited,
                    }).ToList();

                    biaWebFiltersInput.SchemeName = biaWebFilters.SchemeName;
                    biaWebFiltersInput.DistrictId = biaWebFilters.DistrictId;
                }
                else if (biaWebFilters.SchemeName.Equals("PMKSY"))
                {
                    var data = client.Get_PMKSY_ApplicationDetailsAsync(this.config.Value.PMKSY).Result.ToList();
                    pmksy.AddRange(data);
                    biaWebGridDetails = pmksy.Select(x => new BiaWebGridDetails()
                    {
                        SchemeName = biaWebFilters.SchemeName,
                        RegistrationNo = x._DBTRegistrationNo,
                        ApplicationNo = x._ApplicationId,
                        BeneficiaryName = x._FarmerName,
                        MobileNo = x._MobileNo,
                        DistrictName = x._District,
                        BlockName = x._Block,
                        PanchayatName = x._Panchayat,
                        TransactionDate = x._TransactionDate,
                        SubsidyReceived = x._Amountbenefited,
                    }).ToList();

                    biaWebFiltersInput.SchemeName = biaWebFilters.SchemeName;
                }
                else if (biaWebFilters.SchemeName.Equals("Nalkoop"))
                {
                    var data = client.Get_Nalkoop_ApplicationDetailsAsync(this.config.Value.Nalkoop, Convert.ToString(distLgCode)).Result.ToList();
                    nalkoop.AddRange(data);
                    biaWebGridDetails = nalkoop.Select(x => new BiaWebGridDetails()
                    {
                        SchemeName = biaWebFilters.SchemeName,
                        RegistrationNo = x._DBTRegistrationNo,
                        ApplicationNo = x._ApplicationId,
                        BeneficiaryName = x._FarmerName,
                        MobileNo = x._MobileNo,
                        DistrictName = x._District,
                        BlockName = x._Block,
                        PanchayatName = x._Panchayat,
                        TransactionDate = x._TransactionDate,
                        SubsidyReceived = x._Amountbenefited,
                    }).ToList();

                    biaWebFiltersInput.SchemeName = biaWebFilters.SchemeName;
                    biaWebFiltersInput.DistrictId = biaWebFilters.DistrictId;
                }
                else if (biaWebFilters.SchemeName.Equals("Soil Conservation"))
                {
                    int panchId = biaWebFilters.PanchayatId ?? 0;
                    soilConservation = this.soilConservationService.GetSoilConservationSubmittedData(panchId, schemeId);
                    int i = 2;
                    if (soilConservation != null)
                    {
                        foreach (SoilConservationSubmittedDataResponse soilConservationSubmittedDataResponse in soilConservation)
                        {
                            foreach (SubmittedActivityList activityList in soilConservationSubmittedDataResponse.Activity_list)
                            {
                                foreach (SubmittedSubActivityList subActivityList in activityList.Sub_activity_list)
                                {
                                    foreach (SubActivityDetail subActivityDetail in subActivityList.Sub_activity_details)
                                    {
                                        if (subActivityDetail.Is_final_submission == true)
                                        {
                                            biaWebGridDetails.Add(new BiaWebGridDetails
                                            {
                                                SchemeName = biaWebFilters.SchemeName,
                                                RegistrationNo = !string.IsNullOrEmpty(subActivityDetail.Registration_no) ?
                                                (long.Parse(subActivityDetail.Registration_no) * i).ToString().Substring(0, 10) : (i * 9999999).ToString(),
                                                ApplicationNo = string.Empty,
                                                BeneficiaryName = !string.IsNullOrEmpty(subActivityDetail.Beneficiary_name) ? subActivityDetail.Beneficiary_name : string.Empty,
                                                MobileNo = !string.IsNullOrEmpty(subActivityDetail.Mobile_number) ? subActivityDetail.Mobile_number : string.Empty,
                                                DistrictName = !string.IsNullOrEmpty(subActivityDetail.Name_Of_district) ? subActivityDetail.Name_Of_district : string.Empty,
                                                BlockName = !string.IsNullOrEmpty(subActivityDetail.Name_Of_block) ? subActivityDetail.Name_Of_block : string.Empty,
                                                PanchayatName = !string.IsNullOrEmpty(subActivityDetail.Name_Of_panchayat) ? subActivityDetail.Name_Of_panchayat : string.Empty,
                                                TransactionDate = subActivityDetail.Actual_date_of_completion.HasValue ? DateTime.Parse(subActivityDetail.Actual_date_of_completion.ToString()).ToString("dd/MM/yyyy") : string.Empty,
                                                SubsidyReceived = !string.IsNullOrEmpty(subActivityDetail.Structure_name) ? subActivityDetail.Structure_name : string.Empty,
                                            });
                                            i++;
                                        }
                                    }
                                }
                            }
                        }
                    }

                    biaWebFiltersInput.SchemeName = biaWebFilters.SchemeName;
                    biaWebFiltersInput.PanchayatId = biaWebFilters.PanchayatId;
                }
                else if (biaWebFilters.SchemeName == "KKA")
                {
                    var fab_DATA = ofmasclient.getOFMAS_CHCBeneficiaryListAsync(this.config.Value.OfmasPasscode, Convert.ToString(biaWebFilters.FinYear), Convert.ToInt64(scheme.Where(x => x.Scheme == "KKA").Select(x => x.Sid).FirstOrDefault()), Convert.ToInt64(scheme.Where(x => x.Scheme == "KKA").Select(x => x.Phase).FirstOrDefault())).Result.ToList();
                    kkaBeneficiaryList.AddRange(fab_DATA);
                    biaWebGridDetails = kkaBeneficiaryList.Select(x => new BiaWebGridDetails()
                    {
                        SchemeName = biaWebFilters.SchemeName,
                        RegistrationNo = x.BeneficiaryName.Substring(4) + x.subsidy_amt,
                        ApplicationNo = " ",
                        BeneficiaryName = x.BeneficiaryName,
                        MobileNo = " ",
                        BeneficiaryType = x.BeneficiaryType,
                        DistrictName = x.dist_name,
                        BlockName = x.block_name,
                        PanchayatName = x.pan_name,
                        TransactionDate = x.subsidy_date,
                        SubsidyReceived = x.subsidy_amt,
                    }).ToList();

                    biaWebFiltersInput.SchemeName = biaWebFilters.SchemeName;
                    biaWebFiltersInput.FinYear = biaWebFilters.FinYear;
                }
                else if (biaWebFilters.SchemeName == "SMAM-CHC")
                {
                    var fab_DATA = ofmasclient.getOFMAS_CHCBeneficiaryListAsync(this.config.Value.OfmasPasscode, Convert.ToString(biaWebFilters.FinYear), Convert.ToInt64(scheme.Where(x => x.Scheme == "SMAM-CHC").Select(x => x.Sid).FirstOrDefault()), Convert.ToInt64(scheme.Where(x => x.Scheme == "SMAM-CHC").Select(x => x.Phase).FirstOrDefault())).Result.ToList();
                    chcBeneficiaryList.AddRange(fab_DATA);
                    biaWebGridDetails = chcBeneficiaryList.Select(x => new BiaWebGridDetails()
                    {
                        SchemeName = biaWebFilters.SchemeName,
                        RegistrationNo = x.BeneficiaryName.Substring(4) + x.subsidy_amt,
                        ApplicationNo = " ",
                        BeneficiaryName = x.BeneficiaryName,
                        MobileNo = " ",
                        BeneficiaryType = x.BeneficiaryType,
                        DistrictName = x.dist_name,
                        BlockName = x.block_name,
                        PanchayatName = x.pan_name,
                        TransactionDate = x.subsidy_date,
                        SubsidyReceived = x.subsidy_amt,
                    }).ToList();

                    biaWebFiltersInput.SchemeName = biaWebFilters.SchemeName;
                    biaWebFiltersInput.FinYear = biaWebFilters.FinYear;
                }
                else if (biaWebFilters.SchemeName == "SMAM-SCHC")
                {
                    var fab_DATA = ofmasclient.getOFMAS_CHCBeneficiaryListAsync(this.config.Value.OfmasPasscode, Convert.ToString(biaWebFilters.FinYear), Convert.ToInt64(scheme.Where(x => x.Scheme == "SMAM-SCHC").Select(x => x.Sid).FirstOrDefault()), Convert.ToInt64(scheme.Where(x => x.Scheme == "SMAM-SCHC").Select(x => x.Phase).FirstOrDefault())).Result.ToList();
                    schcBeneficiaryList.AddRange(fab_DATA);
                    biaWebGridDetails = schcBeneficiaryList.Select(x => new BiaWebGridDetails()
                    {
                        SchemeName = biaWebFilters.SchemeName,
                        RegistrationNo = x.BeneficiaryName.Substring(4) + x.subsidy_amt,
                        ApplicationNo = " ",
                        BeneficiaryName = x.BeneficiaryName,
                        MobileNo = " ",
                        BeneficiaryType = x.BeneficiaryType,
                        DistrictName = x.dist_name,
                        BlockName = x.block_name,
                        PanchayatName = x.pan_name,
                        TransactionDate = x.subsidy_date,
                        SubsidyReceived = x.subsidy_amt,
                    }).ToList();

                    biaWebFiltersInput.SchemeName = biaWebFilters.SchemeName;
                    biaWebFiltersInput.FinYear = biaWebFilters.FinYear;
                }
                else if (biaWebFilters.SchemeName == "SMAM-FMB")
                {
                    var fab_DATA = ofmasclient.getOFMAS_CHCBeneficiaryListAsync(this.config.Value.OfmasPasscode, Convert.ToString(biaWebFilters.FinYear), Convert.ToInt64(scheme.Where(x => x.Scheme == "SMAM-FMB").Select(x => x.Sid).FirstOrDefault()), Convert.ToInt64(scheme.Where(x => x.Scheme == "SMAM-FMB").Select(x => x.Phase).FirstOrDefault())).Result.ToList();
                    fmbBeneficiaryList.AddRange(fab_DATA);
                    biaWebGridDetails = fmbBeneficiaryList.Select(x => new BiaWebGridDetails()
                    {
                        SchemeName = biaWebFilters.SchemeName,
                        RegistrationNo = x.BeneficiaryName.Substring(4) + x.subsidy_amt,
                        ApplicationNo = " ",
                        BeneficiaryName = x.BeneficiaryName,
                        MobileNo = " ",
                        BeneficiaryType = x.BeneficiaryType,
                        DistrictName = x.dist_name,
                        BlockName = x.block_name,
                        PanchayatName = x.pan_name,
                        TransactionDate = x.subsidy_date,
                        SubsidyReceived = x.subsidy_amt,
                    }).ToList();

                    biaWebFiltersInput.SchemeName = biaWebFilters.SchemeName;
                    biaWebFiltersInput.FinYear = biaWebFilters.FinYear;
                }
                else if (biaWebFilters.SchemeName == "NFSM")
                {
                    var beneficiaryLists = ofmasclient.getOFMAS_BeneficiaryListAsync(this.config.Value.OfmasPasscode, Convert.ToString(biaWebFilters.FinYear), Convert.ToInt64(scheme.Where(x => x.Scheme == "NFSM").Select(x => x.Sid).FirstOrDefault()), Convert.ToString(blockLgCode)).Result.ToList();
                    if (beneficiaryLists[0].check1 == "1")
                    {
                        beneficiaryList.AddRange(beneficiaryLists);
                    }

                    biaWebGridDetails = beneficiaryList.Select(x => new BiaWebGridDetails()
                    {
                        SchemeName = biaWebFilters.SchemeName,
                        RegistrationNo = x.Farmer_RegID,
                        ApplicationNo = " ",
                        BeneficiaryName = x.BeneficiaryName,
                        MobileNo = " ",
                        BeneficiaryType = x.BeneficiaryCategory,
                        DistrictName = x.dist_name,
                        BlockName = x.block_name,
                        PanchayatName = x.pan_name,
                        TransactionDate = x.subsidy_date,
                        SubsidyReceived = x.subsidy_amt,
                    }).ToList();

                    biaWebFiltersInput.SchemeName = biaWebFilters.SchemeName;
                    biaWebFiltersInput.FinYear = biaWebFilters.FinYear;
                    biaWebFiltersInput.BlockId = biaWebFilters.BlockId;
                }
                else if (biaWebFilters.SchemeName == "NFSM-oilseeds")
                {
                    var beneficiaryLists = ofmasclient.getOFMAS_BeneficiaryListAsync(this.config.Value.OfmasPasscode, Convert.ToString(biaWebFilters.FinYear), Convert.ToInt64(scheme.Where(x => x.Scheme == "NFSM-oilseeds").Select(x => x.Sid).FirstOrDefault()), Convert.ToString(blockLgCode)).Result.ToList();
                    if (beneficiaryLists[0].check1 == "1")
                    {
                        beneficiaryList.AddRange(beneficiaryLists);
                    }

                    biaWebGridDetails = beneficiaryList.Select(x => new BiaWebGridDetails()
                    {
                        SchemeName = biaWebFilters.SchemeName,
                        RegistrationNo = x.Farmer_RegID,
                        ApplicationNo = " ",
                        BeneficiaryName = x.BeneficiaryName,
                        MobileNo = " ",
                        BeneficiaryType = x.BeneficiaryCategory,
                        DistrictName = x.dist_name,
                        BlockName = x.block_name,
                        PanchayatName = x.pan_name,
                        TransactionDate = x.subsidy_date,
                        SubsidyReceived = x.subsidy_amt,
                    }).ToList();

                    biaWebFiltersInput.SchemeName = biaWebFilters.SchemeName;
                    biaWebFiltersInput.FinYear = biaWebFilters.FinYear;
                    biaWebFiltersInput.BlockId = biaWebFilters.BlockId;
                }
                else if (biaWebFilters.SchemeName == "State Plan")
                {
                    var beneficiaryLists = ofmasclient.getOFMAS_BeneficiaryListAsync(this.config.Value.OfmasPasscode, Convert.ToString(biaWebFilters.FinYear), Convert.ToInt64(scheme.Where(x => x.Scheme == "State Plan").Select(x => x.Sid).FirstOrDefault()), Convert.ToString(blockLgCode)).Result.ToList();
                    if (beneficiaryLists[0].check1 == "1")
                    {
                        beneficiaryList.AddRange(beneficiaryLists);
                    }

                    biaWebGridDetails = beneficiaryList.Select(x => new BiaWebGridDetails()
                    {
                        SchemeName = biaWebFilters.SchemeName,
                        RegistrationNo = x.Farmer_RegID,
                        ApplicationNo = " ",
                        BeneficiaryName = x.BeneficiaryName,
                        MobileNo = " ",
                        BeneficiaryType = x.BeneficiaryCategory,
                        DistrictName = x.dist_name,
                        BlockName = x.block_name,
                        PanchayatName = x.pan_name,
                        TransactionDate = x.subsidy_date,
                        SubsidyReceived = x.subsidy_amt,
                    }).ToList();

                    biaWebFiltersInput.SchemeName = biaWebFilters.SchemeName;
                    biaWebFiltersInput.FinYear = biaWebFilters.FinYear;
                    biaWebFiltersInput.BlockId = biaWebFilters.BlockId;
                }
                else if (biaWebFilters.SchemeName == "TRFA-Oilseeds")
                {
                    var beneficiaryLists = ofmasclient.getOFMAS_BeneficiaryListAsync(this.config.Value.OfmasPasscode, Convert.ToString(biaWebFilters.FinYear), Convert.ToInt64(scheme.Where(x => x.Scheme == "TRFA-Oilseeds").Select(x => x.Sid).FirstOrDefault()), Convert.ToString(blockLgCode)).Result.ToList();
                    if (beneficiaryLists[0].check1 == "1")
                    {
                        beneficiaryList.AddRange(beneficiaryLists);
                    }

                    biaWebGridDetails = beneficiaryList.Select(x => new BiaWebGridDetails()
                    {
                        SchemeName = biaWebFilters.SchemeName,
                        RegistrationNo = x.Farmer_RegID,
                        ApplicationNo = " ",
                        BeneficiaryName = x.BeneficiaryName,
                        MobileNo = " ",
                        BeneficiaryType = x.BeneficiaryCategory,
                        DistrictName = x.dist_name,
                        BlockName = x.block_name,
                        PanchayatName = x.pan_name,
                        TransactionDate = x.subsidy_date,
                        SubsidyReceived = x.subsidy_amt,
                    }).ToList();

                    biaWebFiltersInput.SchemeName = biaWebFilters.SchemeName;
                    biaWebFiltersInput.FinYear = biaWebFilters.FinYear;
                    biaWebFiltersInput.BlockId = biaWebFilters.BlockId;
                }
                else if (biaWebFilters.SchemeName == "TRFA-Pulses")
                {
                    var beneficiaryLists = ofmasclient.getOFMAS_BeneficiaryListAsync(this.config.Value.OfmasPasscode, Convert.ToString(biaWebFilters.FinYear), Convert.ToInt64(scheme.Where(x => x.Scheme == "TRFA-Pulses").Select(x => x.Sid).FirstOrDefault()), Convert.ToString(blockLgCode)).Result.ToList();
                    if (beneficiaryLists[0].check1 == "1")
                    {
                        beneficiaryList.AddRange(beneficiaryLists);
                    }

                    biaWebGridDetails = beneficiaryList.Select(x => new BiaWebGridDetails()
                    {
                        SchemeName = biaWebFilters.SchemeName,
                        RegistrationNo = x.Farmer_RegID,
                        ApplicationNo = " ",
                        BeneficiaryName = x.BeneficiaryName,
                        MobileNo = " ",
                        BeneficiaryType = x.BeneficiaryCategory,
                        DistrictName = x.dist_name,
                        BlockName = x.block_name,
                        PanchayatName = x.pan_name,
                        TransactionDate = x.subsidy_date,
                        SubsidyReceived = x.subsidy_amt,
                    }).ToList();

                    biaWebFiltersInput.SchemeName = biaWebFilters.SchemeName;
                    biaWebFiltersInput.FinYear = biaWebFilters.FinYear;
                    biaWebFiltersInput.BlockId = biaWebFilters.BlockId;
                }
                else if (biaWebFilters.SchemeName == "RKVY-RAFTAAR")
                {
                    var beneficiaryLists = ofmasclient.getOFMAS_BeneficiaryListAsync(this.config.Value.OfmasPasscode, Convert.ToString(biaWebFilters.FinYear), Convert.ToInt64(scheme.Where(x => x.Scheme == "RKVY-RAFTAAR").Select(x => x.Sid).FirstOrDefault()), Convert.ToString(blockLgCode)).Result.ToList();
                    if (beneficiaryLists[0].check1 == "1")
                    {
                        beneficiaryList.AddRange(beneficiaryLists);
                    }

                    biaWebGridDetails = beneficiaryList.Select(x => new BiaWebGridDetails()
                    {
                        SchemeName = biaWebFilters.SchemeName,
                        RegistrationNo = x.Farmer_RegID,
                        ApplicationNo = " ",
                        BeneficiaryName = x.BeneficiaryName,
                        MobileNo = " ",
                        BeneficiaryType = x.BeneficiaryCategory,
                        DistrictName = x.dist_name,
                        BlockName = x.block_name,
                        PanchayatName = x.pan_name,
                        TransactionDate = x.subsidy_date,
                        SubsidyReceived = x.subsidy_amt,
                    }).ToList();

                    biaWebFiltersInput.SchemeName = biaWebFilters.SchemeName;
                    biaWebFiltersInput.FinYear = biaWebFilters.FinYear;
                    biaWebFiltersInput.BlockId = biaWebFilters.BlockId;
                }
                else if (biaWebFilters.SchemeName == "PM-KISAN")
                {
                    var beneficiaryLists = biharAgriWebServiceclient.PmkisanBeneficiaryAsync(Convert.ToString(panchayatLgCode), this.config.Value.Passcode).Result.ToList();
                    pmkisanBeneficiary.AddRange(beneficiaryLists);
                    biaWebGridDetails = pmkisanBeneficiary.Select(x => new BiaWebGridDetails()
                    {
                        SchemeName = biaWebFilters.SchemeName,
                        RegistrationNo = x.Registration,
                        ApplicationNo = x.ApplicationId,
                        BeneficiaryName = x.FarmerName,
                        MobileNo = " ",
                        BeneficiaryType = " ",
                        DistrictName = x.Districtname,
                        BlockName = x.Blockname,
                        PanchayatName = x.panchayatname,
                        TransactionDate = x.sendactiondate,
                        SubsidyReceived = x.SanctionAmount,
                    }).ToList();

                    biaWebFiltersInput.SchemeName = biaWebFilters.SchemeName;
                    biaWebFiltersInput.PanchayatId = biaWebFilters.PanchayatId;
                }

                List<BiaWebGridDetails> biaWebGridDetailsAssignedAndPending = this.beneficiaryService.GetAssignedAndPendingData(biaWebFiltersInput);
                List<BiaWebGridDetails> biaWebGridDetailsVerified = this.beneficiaryService.GetVerifiedData(biaWebFiltersInput);
                List<BiaWebGridDetails> biaWebGridDetailsVerifiedHistory = this.beneficiaryService.GetVerifiedHistoryData(biaWebFiltersInput);
                biaWebGridDetails = biaWebGridDetails.Where(a => !biaWebGridDetailsAssignedAndPending.Any(b => b.RegistrationNo == a.RegistrationNo)).ToList();
                biaWebGridDetails = biaWebGridDetails.Where(a => !biaWebGridDetailsVerified.Any(b => b.RegistrationNo == a.RegistrationNo)).ToList();

                if (biaWebFilters.DataToShow.Equals("Verified"))
                {
                    if (biaWebFilters.BeneficiaryNumber != null && biaWebFilters.IsVerifiedBeneficiaryPopUp == "Y")
                    {
                        List<BiaWebGridDetails> biaWebGridDetailsVerified1 = biaWebGridDetailsVerified.Where(x => x.RegistrationNo == biaWebFilters.BeneficiaryNumber).ToList();
                        List<BiaWebGridDetails> biaWebGridDetailsVerifiedHistory1 = biaWebGridDetailsVerifiedHistory.Where(x => x.RegistrationNo == biaWebFilters.BeneficiaryNumber).ToList();
                        if (biaWebGridDetailsVerifiedHistory1.Count > 0)
                        {
                            biaWebGridDetailsVerified1.AddRange(biaWebGridDetailsVerifiedHistory1);
                        }

                        return this.Ok(biaWebGridDetailsVerified1);
                    }

                    if (biaWebFilters.BeneficiaryNumber != null && biaWebFilters.Status != null && biaWebFilters.Status != "ALL" && biaWebFilters.IsVerifiedBeneficiaryPopUp == "Y")
                    {
                        List<BiaWebGridDetails> biaWebGridDetailsVerified1 = biaWebGridDetailsVerified.Where(x => x.RegistrationNo == biaWebFilters.BeneficiaryNumber).ToList();
                        biaWebGridDetailsVerified1 = biaWebGridDetailsVerified1.Where(x => x.Status == biaWebFilters.Status).ToList();
                        List<BiaWebGridDetails> biaWebGridDetailsVerifiedHistory1 = biaWebGridDetailsVerifiedHistory.Where(x => x.RegistrationNo == biaWebFilters.BeneficiaryNumber).ToList();
                        if (biaWebGridDetailsVerifiedHistory1.Count > 0)
                        {
                            biaWebGridDetailsVerified1.AddRange(biaWebGridDetailsVerifiedHistory1);
                        }

                        return this.Ok(biaWebGridDetailsVerified1);
                    }

                    if (biaWebFilters.BeneficiaryNumber != null)
                    {
                        List<BiaWebGridDetails> biaWebGridDetailsVerified1 = biaWebGridDetailsVerified.Where(x => x.RegistrationNo == biaWebFilters.BeneficiaryNumber).ToList();
                        return this.Ok(biaWebGridDetailsVerified1);
                    }

                    if (biaWebFilters.Status != null && biaWebFilters.Status != "ALL")
                    {
                        List<BiaWebGridDetails> biaWebGridDetailsVerified2 = biaWebGridDetailsVerified.Where(x => x.Status == biaWebFilters.Status).ToList();
                        return this.Ok(biaWebGridDetailsVerified2);
                    }

                    if (biaWebFilters.BeneficiaryNumber != null && biaWebFilters.Status != null && biaWebFilters.Status != "ALL")
                    {
                        List<BiaWebGridDetails> biaWebGridDetailsVerified1 = biaWebGridDetailsVerified.Where(x => x.RegistrationNo == biaWebFilters.BeneficiaryNumber).ToList();
                        biaWebGridDetailsVerified1 = biaWebGridDetailsVerified1.Where(x => x.Status == biaWebFilters.Status).ToList();
                        return this.Ok(biaWebGridDetailsVerified1);
                    }

                    return this.Ok(biaWebGridDetailsVerified);
                }
                else if (biaWebFilters.DataToShow.Equals("AssignedAndPending"))
                {
                    if (biaWebFilters.BeneficiaryNumber != null)
                    {
                        List<BiaWebGridDetails> biaWebGridDetailsAssignedAndPending1 = biaWebGridDetailsAssignedAndPending.Where(x => x.RegistrationNo == biaWebFilters.BeneficiaryNumber).ToList();
                        return this.Ok(biaWebGridDetailsAssignedAndPending1);
                    }

                    return this.Ok(biaWebGridDetailsAssignedAndPending);
                }
                else
                {
                    if (biaWebFilters.BeneficiaryNumber != null)
                    {
                        List<BiaWebGridDetails> biaWebGridDetails1 = biaWebGridDetails.Where(x => x.RegistrationNo == biaWebFilters.BeneficiaryNumber).ToList();
                        return this.Ok(biaWebGridDetails1);
                    }

                    return this.Ok(biaWebGridDetails);
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound();
            }
        }

        /// <summary>
        /// GetAgricultureOfficerDetails.
        /// </summary>
        /// <param name="biaWebAgricultureOfficerFilters">biaWebAgricultureOfficerFilters.</param>
        /// <returns>AO List.</returns>
        [HttpGet("GetAgricultureOfficerDetails")]
        public ActionResult GetAgricultureOfficerDetails(BiaWebAgricultureOfficerFilters biaWebAgricultureOfficerFilters)
        {
            try
            {
                List<BiaWebAoUserDetails> biaWebAoUserDetails = this.beneficiaryService.GetAgricultureOfficerDetails(biaWebAgricultureOfficerFilters);
                return this.Ok(biaWebAoUserDetails);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound();
            }
        }

        /// <summary>
        /// GetAgricultureOfficerTaskDetails.
        /// </summary>
        /// <param name="userId">userId.</param>
        /// <returns>AO Task List.</returns>
        [HttpGet("GetAgricultureOfficerTaskDetails/{userId}")]
        public ActionResult GetAgricultureOfficerTaskDetails(int userId)
        {
            try
            {
                List<BiaWebAoUserDetails> biaWebAoUserDetails = this.beneficiaryService.GetAgricultureOfficerTaskDetails(userId);
                return this.Ok(biaWebAoUserDetails);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound();
            }
        }

        /// <summary>
        /// PostAssignTask.
        /// </summary>
        /// <param name="biaWebGridDetails">biaWebGridDetails.</param>
        /// <returns>output.</returns>
        [HttpPost("PostAssignTask")]
        public IActionResult PostAssignTask([FromBody] List<BiaWebGridDetails> biaWebGridDetails)
        {
            try
            {
                int result = this.beneficiaryService.PostAssignTask(biaWebGridDetails);
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
        /// GetFinancialYear.
        /// </summary>
        /// <returns>Fin Year.</returns>
        [HttpGet("GetFinancialYear")]
        public ActionResult GetFinancialYear()
        {
            try
            {
                List<FinancialYear> financialYear = this.beneficiaryService.GetFinancialYear();
                return this.Ok(financialYear);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound();
            }
        }

        /// <summary>
        /// GetDirectorateSchemeDetail.
        /// </summary>
        /// <returns>DirectorateSchemeDetails List.</returns>
        [HttpGet("GetDirectorateSchemeDetail")]
        public ActionResult GetDirectorateSchemeDetail()
        {
            try
            {
                List<DirectorateSchemeDetails> directorateSchemeDetails = this.beneficiaryService.GetDirectorateSchemeDetail();
                return this.Ok(directorateSchemeDetails);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound();
            }
        }

        /// <summary>
        /// GetStatus.
        /// </summary>
        /// <returns>Status List.</returns>
        [HttpGet("GetStatus")]
        public ActionResult GetStatus()
        {
            try
            {
                List<BiaStatus> biaStatus = this.beneficiaryService.GetStatus();
                return this.Ok(biaStatus);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound();
            }
        }

        /// <summary>
        /// GetBeneficiaryRecord.
        /// </summary>
        /// <param name="userId">userId.</param>
        /// <returns>BeneficiaryRecord List.</returns>
        [HttpGet("GetBeneficiaryRecord/{userId}")]
        public ActionResult GetBeneficiaryRecord(int userId)
        {
            try
            {
                List<BiaBeneficiaryRecords> biaStatus = this.beneficiaryService.GetBeneficiaryRecord(userId);
                return this.Ok(biaStatus);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound();
            }
        }

        /// <summary>
        /// PostDeleteBeneficiary.
        /// </summary>
        /// <param name="biaDeleteBeneficiaryInput">biaDeleteBeneficiaryInput.</param>
        /// <returns>output.</returns>
        [HttpPost("PostDeleteBeneficiary")]
        public IActionResult PostDeleteBeneficiary(List<BiaDeleteBeneficiaryInput> biaDeleteBeneficiaryInput)
        {
            try
            {
                int result = this.beneficiaryService.PostDeleteBeneficiary(biaDeleteBeneficiaryInput);
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
        /// PostAssignBeneficiary.
        /// </summary>
        /// <param name="biaAssignBeneficiaryInput">biaAssignBeneficiaryInput.</param>
        /// <returns>output.</returns>
        [HttpPost("PostAssignBeneficiary")]
        public IActionResult PostAssignBeneficiary(List<BiaAssignBeneficiaryInput> biaAssignBeneficiaryInput)
        {
            try
            {
                int result = this.beneficiaryService.PostAssignBeneficiary(biaAssignBeneficiaryInput);
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
        /// PostBeneficiaryDetail.
        /// </summary>
        /// <param name="biaPostBeneficiaryDetail">biaPostBeneficiaryDetail.</param>
        /// <returns>output.</returns>
        [HttpPost("PostBeneficiaryDetail")]
        public IActionResult PostBeneficiaryDetail([FromBody] BiaPostBeneficiaryDetail biaPostBeneficiaryDetail)
        {
            try
            {
                int result = this.beneficiaryService.PostBeneficiaryDetail(biaPostBeneficiaryDetail);
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
        /// GetNotificationAudits.
        /// </summary>
        /// <param name="userId">userId.</param>
        /// <returns>NotificationAudits.</returns>
        [HttpGet("GetNotificationAudits/{userId}")]
        public ActionResult GetNotificationAudits(int userId)
        {
            try
            {
                List<BiaNotificationAudits> biaNotificationAudits = this.beneficiaryService.GetNotificationAudits(userId);
                return this.Ok(biaNotificationAudits);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound();
            }
        }

        /// <summary>
        /// GetBeneficiaryNotification.
        /// </summary>
        /// <param name="userId">userId.</param>
        /// <returns>BeneficiaryNotification.</returns>
        [HttpGet("GetBeneficiaryNotification/{userId}")]
        public ActionResult GetBeneficiaryNotification(int userId)
        {
            try
            {
                List<BiaNotificationAudits> biaNotificationAudits = this.beneficiaryService.GetBeneficiaryNotification(userId);
                return this.Ok(biaNotificationAudits);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound();
            }
        }

        /// <summary>
        /// GetUnassignedBeneficiaryDetails.
        /// </summary>
        /// <param name="biaWebFilters">biaWebFilters.</param>
        /// <returns>Bia Details List.</returns>
        [HttpPost("GetUnassignedBeneficiaryDetails")]
        public ActionResult GetUnassignedBeneficiaryDetails([FromBody] BiaWebFilters biaWebFilters)
        {
            try
            {
                List<BiaWebGridDetails> biaWebGridDetails = new List<BiaWebGridDetails>();
                BiaWebFilters biaWebFiltersInput = new BiaWebFilters();
                int distId = biaWebFilters.DistrictId.Value;
                List<BlockPanchayatLgCodes> blockPanchayatLgCodes = this.horticultureService.GetBlockPanchaytLgCodes(distId);
                int distLgCode = blockPanchayatLgCodes.Where(x => x.District_id == biaWebFilters.DistrictId).Select(x => x.District_lg_code).FirstOrDefault();
                List<BiaScheme> biaScheme = this.beneficiaryService.GetBIASchemeNames(biaWebFilters.Directorate);
                int schemeId = biaScheme.Where(x => x.Scheme_name == biaWebFilters.SchemeName).Select(x => x.Scheme_id).FirstOrDefault() ?? 0;
                int blockLgCode = blockPanchayatLgCodes.Where(x => x.Block_id == biaWebFilters.BlockId).Select(x => x.Block_lg_code).FirstOrDefault();
                int panchayatLgCode = blockPanchayatLgCodes.Where(x => x.Panchayat_id == biaWebFilters.PanchayatId).Select(x => x.Panchayat_lg_code).FirstOrDefault();

                WebService_BihanAppSoapClient client = new WebService_BihanAppSoapClient(WebService_BihanAppSoapClient.EndpointConfiguration.WebService_BihanAppSoap);
                client.ClientCredentials.ServiceCertificate.SslCertificateAuthentication =
                new X509ServiceCertificateAuthentication()
                {
                    CertificateValidationMode = X509CertificateValidationMode.None,
                    RevocationMode = System.Security.Cryptography.X509Certificates.X509RevocationMode.NoCheck,
                };

                BiharAgariWebServiceSoapClient biharAgriWebServiceclient = new BiharAgariWebServiceSoapClient(BiharAgariWebServiceSoapClient.EndpointConfiguration.BiharAgariWebServiceSoap);
                client.ClientCredentials.ServiceCertificate.SslCertificateAuthentication =
                new X509ServiceCertificateAuthentication()
                {
                    CertificateValidationMode = X509CertificateValidationMode.None,
                    RevocationMode = System.Security.Cryptography.X509Certificates.X509RevocationMode.NoCheck,
                };

                OFMAS_Report.OFMASReportWebServiceSoapClient ofmasclient = new OFMAS_Report.OFMASReportWebServiceSoapClient(OFMAS_Report.OFMASReportWebServiceSoapClient.EndpointConfiguration.OFMASReportWebServiceSoap);
                ofmasclient.ClientCredentials.ServiceCertificate.SslCertificateAuthentication =
                new X509ServiceCertificateAuthentication()
                {
                    CertificateValidationMode = X509CertificateValidationMode.None,
                    RevocationMode = System.Security.Cryptography.X509Certificates.X509RevocationMode.NoCheck,
                };
                List<OFMAS_Scheme> scheme = ofmasclient.getOFMAS_SchemeAsync(this.config.Value.OfmasPasscode).Result.ToList();

                List<NHM_CMHM_ApplicationDetails> nhm_cmhm_Applications = new List<NHM_CMHM_ApplicationDetails>();
                List<PMKSY_ApplicationDetails> pmksy = new List<PMKSY_ApplicationDetails>();
                List<Nalkoop_ApplicationDetails> nalkoop = new List<Nalkoop_ApplicationDetails>();
                List<SoilConservationSubmittedDataResponse> soilConservation = new List<SoilConservationSubmittedDataResponse>();
                List<OFMAS_CHCBeneficiaryList> kkaBeneficiaryList = new List<OFMAS_CHCBeneficiaryList>();
                List<OFMAS_CHCBeneficiaryList> schcBeneficiaryList = new List<OFMAS_CHCBeneficiaryList>();
                List<OFMAS_CHCBeneficiaryList> chcBeneficiaryList = new List<OFMAS_CHCBeneficiaryList>();
                List<OFMAS_CHCBeneficiaryList> fmbBeneficiaryList = new List<OFMAS_CHCBeneficiaryList>();
                List<OFMAS_BeneficiaryList> beneficiaryList = new List<OFMAS_BeneficiaryList>();
                List<beneficiaryDetails> pmkisanBeneficiary = new List<beneficiaryDetails>();

                if (biaWebFilters.SchemeName.Equals("NHM"))
                {
                    var data = client.Get_NHM_CMHM_ApplicationDetailsAsync(this.config.Value.NHM_CNHM, this.config.Value.NHM_SchemeID, Convert.ToString(distLgCode)).Result.ToList();
                    nhm_cmhm_Applications.AddRange(data);
                    biaWebGridDetails = nhm_cmhm_Applications.Select(x => new BiaWebGridDetails()
                    {
                        SchemeName = biaWebFilters.SchemeName,
                        RegistrationNo = x._DBTRegistrationNo,
                        ApplicationNo = x._ApplicationId,
                        BeneficiaryName = x._FarmerName,
                        MobileNo = x._MobileNo,
                        DistrictName = x._District,
                        BlockName = x._Block,
                        PanchayatName = x._Panchayat,
                        TransactionDate = x._TransactionDate,
                        SubsidyReceived = x._Amountbenefited,
                    }).ToList();

                    biaWebFiltersInput.SchemeName = biaWebFilters.SchemeName;
                    biaWebFiltersInput.DistrictId = biaWebFilters.DistrictId;
                }
                else if (biaWebFilters.SchemeName.Equals("CMHM"))
                {
                    var data = client.Get_NHM_CMHM_ApplicationDetailsAsync(this.config.Value.NHM_CNHM, this.config.Value.CMHM_SchemeID, Convert.ToString(distLgCode)).Result.ToList();
                    nhm_cmhm_Applications.AddRange(data);
                    biaWebGridDetails = nhm_cmhm_Applications.Select(x => new BiaWebGridDetails()
                    {
                        SchemeName = biaWebFilters.SchemeName,
                        RegistrationNo = x._DBTRegistrationNo,
                        ApplicationNo = x._ApplicationId,
                        BeneficiaryName = x._FarmerName,
                        MobileNo = x._MobileNo,
                        DistrictName = x._District,
                        BlockName = x._Block,
                        PanchayatName = x._Panchayat,
                        TransactionDate = x._TransactionDate,
                        SubsidyReceived = x._Amountbenefited,
                    }).ToList();

                    biaWebFiltersInput.SchemeName = biaWebFilters.SchemeName;
                    biaWebFiltersInput.DistrictId = biaWebFilters.DistrictId;
                }
                else if (biaWebFilters.SchemeName.Equals("PMKSY"))
                {
                    var data = client.Get_PMKSY_ApplicationDetailsAsync(this.config.Value.PMKSY).Result.ToList();
                    pmksy.AddRange(data);
                    biaWebGridDetails = pmksy.Select(x => new BiaWebGridDetails()
                    {
                        SchemeName = biaWebFilters.SchemeName,
                        RegistrationNo = x._DBTRegistrationNo,
                        ApplicationNo = x._ApplicationId,
                        BeneficiaryName = x._FarmerName,
                        MobileNo = x._MobileNo,
                        DistrictName = x._District,
                        BlockName = x._Block,
                        PanchayatName = x._Panchayat,
                        TransactionDate = x._TransactionDate,
                        SubsidyReceived = x._Amountbenefited,
                    }).ToList();

                    biaWebFiltersInput.SchemeName = biaWebFilters.SchemeName;
                }
                else if (biaWebFilters.SchemeName.Equals("Nalkoop"))
                {
                    var data = client.Get_Nalkoop_ApplicationDetailsAsync(this.config.Value.Nalkoop, Convert.ToString(distLgCode)).Result.ToList();
                    nalkoop.AddRange(data);
                    biaWebGridDetails = nalkoop.Select(x => new BiaWebGridDetails()
                    {
                        SchemeName = biaWebFilters.SchemeName,
                        RegistrationNo = x._DBTRegistrationNo,
                        ApplicationNo = x._ApplicationId,
                        BeneficiaryName = x._FarmerName,
                        MobileNo = x._MobileNo,
                        DistrictName = x._District,
                        BlockName = x._Block,
                        PanchayatName = x._Panchayat,
                        TransactionDate = x._TransactionDate,
                        SubsidyReceived = x._Amountbenefited,
                    }).ToList();

                    biaWebFiltersInput.SchemeName = biaWebFilters.SchemeName;
                    biaWebFiltersInput.DistrictId = biaWebFilters.DistrictId;
                }
                else if (biaWebFilters.SchemeName.Equals("Soil Conservation"))
                {
                    int panchId = biaWebFilters.PanchayatId ?? 0;
                    soilConservation = this.soilConservationService.GetSoilConservationSubmittedData(panchId, schemeId);
                    int i = 2;
                    if (soilConservation != null)
                    {
                        foreach (SoilConservationSubmittedDataResponse soilConservationSubmittedDataResponse in soilConservation)
                        {
                            foreach (SubmittedActivityList activityList in soilConservationSubmittedDataResponse.Activity_list)
                            {
                                foreach (SubmittedSubActivityList subActivityList in activityList.Sub_activity_list)
                                {
                                    foreach (SubActivityDetail subActivityDetail in subActivityList.Sub_activity_details)
                                    {
                                        if (subActivityDetail.Is_final_submission == true)
                                        {
                                            biaWebGridDetails.Add(new BiaWebGridDetails
                                            {
                                                SchemeName = biaWebFilters.SchemeName,
                                                RegistrationNo = !string.IsNullOrEmpty(subActivityDetail.Registration_no) ?
                                                (long.Parse(subActivityDetail.Registration_no) * i).ToString().Substring(0, 10) : (i * 9999999).ToString(),
                                                ApplicationNo = string.Empty,
                                                BeneficiaryName = !string.IsNullOrEmpty(subActivityDetail.Beneficiary_name) ? subActivityDetail.Beneficiary_name : string.Empty,
                                                MobileNo = !string.IsNullOrEmpty(subActivityDetail.Mobile_number) ? subActivityDetail.Mobile_number : string.Empty,
                                                DistrictName = !string.IsNullOrEmpty(subActivityDetail.Name_Of_district) ? subActivityDetail.Name_Of_district : string.Empty,
                                                BlockName = !string.IsNullOrEmpty(subActivityDetail.Name_Of_block) ? subActivityDetail.Name_Of_block : string.Empty,
                                                PanchayatName = !string.IsNullOrEmpty(subActivityDetail.Name_Of_panchayat) ? subActivityDetail.Name_Of_panchayat : string.Empty,
                                                TransactionDate = subActivityDetail.Actual_date_of_completion.HasValue ? DateTime.Parse(subActivityDetail.Actual_date_of_completion.ToString()).ToString("dd/MM/yyyy") : string.Empty,
                                                SubsidyReceived = !string.IsNullOrEmpty(subActivityDetail.Structure_name) ? subActivityDetail.Structure_name : string.Empty,
                                            });
                                            i++;
                                        }
                                    }
                                }
                            }
                        }
                    }

                    biaWebFiltersInput.SchemeName = biaWebFilters.SchemeName;
                    biaWebFiltersInput.PanchayatId = biaWebFilters.PanchayatId;
                }
                else if (biaWebFilters.SchemeName == "KKA")
                {
                    var fab_DATA = ofmasclient.getOFMAS_CHCBeneficiaryListAsync(this.config.Value.OfmasPasscode, Convert.ToString(biaWebFilters.FinYear), Convert.ToInt64(scheme.Where(x => x.Scheme == "KKA").Select(x => x.Sid).FirstOrDefault()), Convert.ToInt64(scheme.Where(x => x.Scheme == "KKA").Select(x => x.Phase).FirstOrDefault())).Result.ToList();
                    kkaBeneficiaryList.AddRange(fab_DATA);
                    biaWebGridDetails = kkaBeneficiaryList.Select(x => new BiaWebGridDetails()
                    {
                        SchemeName = biaWebFilters.SchemeName,
                        RegistrationNo = x.BeneficiaryName.Substring(4) + x.subsidy_amt,
                        ApplicationNo = null,
                        BeneficiaryName = x.BeneficiaryName,
                        MobileNo = null,
                        BeneficiaryType = x.BeneficiaryType,
                        DistrictName = x.dist_name,
                        BlockName = x.block_name,
                        PanchayatName = x.pan_name,
                        TransactionDate = x.subsidy_date,
                        SubsidyReceived = x.subsidy_amt,
                    }).ToList();

                    biaWebFiltersInput.SchemeName = biaWebFilters.SchemeName;
                    biaWebFiltersInput.FinYear = biaWebFilters.FinYear;
                }
                else if (biaWebFilters.SchemeName == "SMAM-CHC")
                {
                    var fab_DATA = ofmasclient.getOFMAS_CHCBeneficiaryListAsync(this.config.Value.OfmasPasscode, Convert.ToString(biaWebFilters.FinYear), Convert.ToInt64(scheme.Where(x => x.Scheme == "SMAM-CHC").Select(x => x.Sid).FirstOrDefault()), Convert.ToInt64(scheme.Where(x => x.Scheme == "SMAM-CHC").Select(x => x.Phase).FirstOrDefault())).Result.ToList();
                    chcBeneficiaryList.AddRange(fab_DATA);
                    biaWebGridDetails = chcBeneficiaryList.Select(x => new BiaWebGridDetails()
                    {
                        SchemeName = biaWebFilters.SchemeName,
                        RegistrationNo = x.BeneficiaryName.Substring(4) + x.subsidy_amt,
                        ApplicationNo = null,
                        BeneficiaryName = x.BeneficiaryName,
                        MobileNo = null,
                        BeneficiaryType = x.BeneficiaryType,
                        DistrictName = x.dist_name,
                        BlockName = x.block_name,
                        PanchayatName = x.pan_name,
                        TransactionDate = x.subsidy_date,
                        SubsidyReceived = x.subsidy_amt,
                    }).ToList();

                    biaWebFiltersInput.SchemeName = biaWebFilters.SchemeName;
                    biaWebFiltersInput.FinYear = biaWebFilters.FinYear;
                }
                else if (biaWebFilters.SchemeName == "SMAM-SCHC")
                {
                    var fab_DATA = ofmasclient.getOFMAS_CHCBeneficiaryListAsync(this.config.Value.OfmasPasscode, Convert.ToString(biaWebFilters.FinYear), Convert.ToInt64(scheme.Where(x => x.Scheme == "SMAM-SCHC").Select(x => x.Sid).FirstOrDefault()), Convert.ToInt64(scheme.Where(x => x.Scheme == "SMAM-SCHC").Select(x => x.Phase).FirstOrDefault())).Result.ToList();
                    schcBeneficiaryList.AddRange(fab_DATA);
                    biaWebGridDetails = schcBeneficiaryList.Select(x => new BiaWebGridDetails()
                    {
                        SchemeName = biaWebFilters.SchemeName,
                        RegistrationNo = x.BeneficiaryName.Substring(4) + x.subsidy_amt,
                        ApplicationNo = null,
                        BeneficiaryName = x.BeneficiaryName,
                        MobileNo = null,
                        BeneficiaryType = x.BeneficiaryType,
                        DistrictName = x.dist_name,
                        BlockName = x.block_name,
                        PanchayatName = x.pan_name,
                        TransactionDate = x.subsidy_date,
                        SubsidyReceived = x.subsidy_amt,
                    }).ToList();

                    biaWebFiltersInput.SchemeName = biaWebFilters.SchemeName;
                    biaWebFiltersInput.FinYear = biaWebFilters.FinYear;
                }
                else if (biaWebFilters.SchemeName == "SMAM-FMB")
                {
                    var fab_DATA = ofmasclient.getOFMAS_CHCBeneficiaryListAsync(this.config.Value.OfmasPasscode, Convert.ToString(biaWebFilters.FinYear), Convert.ToInt64(scheme.Where(x => x.Scheme == "SMAM-FMB").Select(x => x.Sid).FirstOrDefault()), Convert.ToInt64(scheme.Where(x => x.Scheme == "SMAM-FMB").Select(x => x.Phase).FirstOrDefault())).Result.ToList();
                    fmbBeneficiaryList.AddRange(fab_DATA);
                    biaWebGridDetails = fmbBeneficiaryList.Select(x => new BiaWebGridDetails()
                    {
                        SchemeName = biaWebFilters.SchemeName,
                        RegistrationNo = x.BeneficiaryName.Substring(4) + x.subsidy_amt,
                        ApplicationNo = null,
                        BeneficiaryName = x.BeneficiaryName,
                        MobileNo = null,
                        BeneficiaryType = x.BeneficiaryType,
                        DistrictName = x.dist_name,
                        BlockName = x.block_name,
                        PanchayatName = x.pan_name,
                        TransactionDate = x.subsidy_date,
                        SubsidyReceived = x.subsidy_amt,
                    }).ToList();

                    biaWebFiltersInput.SchemeName = biaWebFilters.SchemeName;
                    biaWebFiltersInput.FinYear = biaWebFilters.FinYear;
                }
                else if (biaWebFilters.SchemeName == "NFSM")
                {
                    var beneficiaryLists = ofmasclient.getOFMAS_BeneficiaryListAsync(this.config.Value.OfmasPasscode, Convert.ToString(biaWebFilters.FinYear), Convert.ToInt64(scheme.Where(x => x.Scheme == "NFSM").Select(x => x.Sid).FirstOrDefault()), Convert.ToString(blockLgCode)).Result.ToList();
                    if (beneficiaryLists[0].check1 == "1")
                    {
                        beneficiaryList.AddRange(beneficiaryLists);
                    }

                    biaWebGridDetails = beneficiaryList.Select(x => new BiaWebGridDetails()
                    {
                        SchemeName = biaWebFilters.SchemeName,
                        RegistrationNo = x.Farmer_RegID,
                        ApplicationNo = null,
                        BeneficiaryName = x.BeneficiaryName,
                        MobileNo = null,
                        BeneficiaryType = x.BeneficiaryCategory,
                        DistrictName = x.dist_name,
                        BlockName = x.block_name,
                        PanchayatName = x.pan_name,
                        TransactionDate = x.subsidy_date,
                        SubsidyReceived = x.subsidy_amt,
                    }).ToList();

                    biaWebFiltersInput.SchemeName = biaWebFilters.SchemeName;
                    biaWebFiltersInput.FinYear = biaWebFilters.FinYear;
                    biaWebFiltersInput.BlockId = biaWebFilters.BlockId;
                }
                else if (biaWebFilters.SchemeName == "NFSM-oilseeds")
                {
                    var beneficiaryLists = ofmasclient.getOFMAS_BeneficiaryListAsync(this.config.Value.OfmasPasscode, Convert.ToString(biaWebFilters.FinYear), Convert.ToInt64(scheme.Where(x => x.Scheme == "NFSM-oilseeds").Select(x => x.Sid).FirstOrDefault()), Convert.ToString(blockLgCode)).Result.ToList();
                    if (beneficiaryLists[0].check1 == "1")
                    {
                        beneficiaryList.AddRange(beneficiaryLists);
                    }

                    biaWebGridDetails = beneficiaryList.Select(x => new BiaWebGridDetails()
                    {
                        SchemeName = biaWebFilters.SchemeName,
                        RegistrationNo = x.Farmer_RegID,
                        ApplicationNo = null,
                        BeneficiaryName = x.BeneficiaryName,
                        MobileNo = null,
                        BeneficiaryType = x.BeneficiaryCategory,
                        DistrictName = x.dist_name,
                        BlockName = x.block_name,
                        PanchayatName = x.pan_name,
                        TransactionDate = x.subsidy_date,
                        SubsidyReceived = x.subsidy_amt,
                    }).ToList();

                    biaWebFiltersInput.SchemeName = biaWebFilters.SchemeName;
                    biaWebFiltersInput.FinYear = biaWebFilters.FinYear;
                    biaWebFiltersInput.BlockId = biaWebFilters.BlockId;
                }
                else if (biaWebFilters.SchemeName == "State Plan")
                {
                    var beneficiaryLists = ofmasclient.getOFMAS_BeneficiaryListAsync(this.config.Value.OfmasPasscode, Convert.ToString(biaWebFilters.FinYear), Convert.ToInt64(scheme.Where(x => x.Scheme == "State Plan").Select(x => x.Sid).FirstOrDefault()), Convert.ToString(blockLgCode)).Result.ToList();
                    if (beneficiaryLists[0].check1 == "1")
                    {
                        beneficiaryList.AddRange(beneficiaryLists);
                    }

                    biaWebGridDetails = beneficiaryList.Select(x => new BiaWebGridDetails()
                    {
                        SchemeName = biaWebFilters.SchemeName,
                        RegistrationNo = x.Farmer_RegID,
                        ApplicationNo = null,
                        BeneficiaryName = x.BeneficiaryName,
                        MobileNo = null,
                        BeneficiaryType = x.BeneficiaryCategory,
                        DistrictName = x.dist_name,
                        BlockName = x.block_name,
                        PanchayatName = x.pan_name,
                        TransactionDate = x.subsidy_date,
                        SubsidyReceived = x.subsidy_amt,
                    }).ToList();

                    biaWebFiltersInput.SchemeName = biaWebFilters.SchemeName;
                    biaWebFiltersInput.FinYear = biaWebFilters.FinYear;
                    biaWebFiltersInput.BlockId = biaWebFilters.BlockId;
                }
                else if (biaWebFilters.SchemeName == "TRFA-Oilseeds")
                {
                    var beneficiaryLists = ofmasclient.getOFMAS_BeneficiaryListAsync(this.config.Value.OfmasPasscode, Convert.ToString(biaWebFilters.FinYear), Convert.ToInt64(scheme.Where(x => x.Scheme == "TRFA-Oilseeds").Select(x => x.Sid).FirstOrDefault()), Convert.ToString(blockLgCode)).Result.ToList();
                    if (beneficiaryLists[0].check1 == "1")
                    {
                        beneficiaryList.AddRange(beneficiaryLists);
                    }

                    biaWebGridDetails = beneficiaryList.Select(x => new BiaWebGridDetails()
                    {
                        SchemeName = biaWebFilters.SchemeName,
                        RegistrationNo = x.Farmer_RegID,
                        ApplicationNo = null,
                        BeneficiaryName = x.BeneficiaryName,
                        MobileNo = null,
                        BeneficiaryType = x.BeneficiaryCategory,
                        DistrictName = x.dist_name,
                        BlockName = x.block_name,
                        PanchayatName = x.pan_name,
                        TransactionDate = x.subsidy_date,
                        SubsidyReceived = x.subsidy_amt,
                    }).ToList();

                    biaWebFiltersInput.SchemeName = biaWebFilters.SchemeName;
                    biaWebFiltersInput.FinYear = biaWebFilters.FinYear;
                    biaWebFiltersInput.BlockId = biaWebFilters.BlockId;
                }
                else if (biaWebFilters.SchemeName == "TRFA-Pulses")
                {
                    var beneficiaryLists = ofmasclient.getOFMAS_BeneficiaryListAsync(this.config.Value.OfmasPasscode, Convert.ToString(biaWebFilters.FinYear), Convert.ToInt64(scheme.Where(x => x.Scheme == "TRFA-Pulses").Select(x => x.Sid).FirstOrDefault()), Convert.ToString(blockLgCode)).Result.ToList();
                    if (beneficiaryLists[0].check1 == "1")
                    {
                        beneficiaryList.AddRange(beneficiaryLists);
                    }

                    biaWebGridDetails = beneficiaryList.Select(x => new BiaWebGridDetails()
                    {
                        SchemeName = biaWebFilters.SchemeName,
                        RegistrationNo = x.Farmer_RegID,
                        ApplicationNo = null,
                        BeneficiaryName = x.BeneficiaryName,
                        MobileNo = null,
                        BeneficiaryType = x.BeneficiaryCategory,
                        DistrictName = x.dist_name,
                        BlockName = x.block_name,
                        PanchayatName = x.pan_name,
                        TransactionDate = x.subsidy_date,
                        SubsidyReceived = x.subsidy_amt,
                    }).ToList();

                    biaWebFiltersInput.SchemeName = biaWebFilters.SchemeName;
                    biaWebFiltersInput.FinYear = biaWebFilters.FinYear;
                    biaWebFiltersInput.BlockId = biaWebFilters.BlockId;
                }
                else if (biaWebFilters.SchemeName == "RKVY-RAFTAAR")
                {
                    var beneficiaryLists = ofmasclient.getOFMAS_BeneficiaryListAsync(this.config.Value.OfmasPasscode, Convert.ToString(biaWebFilters.FinYear), Convert.ToInt64(scheme.Where(x => x.Scheme == "RKVY-RAFTAAR").Select(x => x.Sid).FirstOrDefault()), Convert.ToString(blockLgCode)).Result.ToList();
                    if (beneficiaryLists[0].check1 == "1")
                    {
                        beneficiaryList.AddRange(beneficiaryLists);
                    }

                    biaWebGridDetails = beneficiaryList.Select(x => new BiaWebGridDetails()
                    {
                        SchemeName = biaWebFilters.SchemeName,
                        RegistrationNo = x.Farmer_RegID,
                        ApplicationNo = null,
                        BeneficiaryName = x.BeneficiaryName,
                        MobileNo = null,
                        BeneficiaryType = x.BeneficiaryCategory,
                        DistrictName = x.dist_name,
                        BlockName = x.block_name,
                        PanchayatName = x.pan_name,
                        TransactionDate = x.subsidy_date,
                        SubsidyReceived = x.subsidy_amt,
                    }).ToList();

                    biaWebFiltersInput.SchemeName = biaWebFilters.SchemeName;
                    biaWebFiltersInput.FinYear = biaWebFilters.FinYear;
                    biaWebFiltersInput.BlockId = biaWebFilters.BlockId;
                }
                else if (biaWebFilters.SchemeName == "PM-KISAN")
                {
                    var beneficiaryLists = biharAgriWebServiceclient.PmkisanBeneficiaryAsync(Convert.ToString(panchayatLgCode), this.config.Value.Passcode).Result.ToList();
                    pmkisanBeneficiary.AddRange(beneficiaryLists);
                    biaWebGridDetails = pmkisanBeneficiary.Select(x => new BiaWebGridDetails()
                    {
                        SchemeName = biaWebFilters.SchemeName,
                        RegistrationNo = x.Registration,
                        ApplicationNo = x.ApplicationId,
                        BeneficiaryName = x.FarmerName,
                        MobileNo = null,
                        BeneficiaryType = null,
                        DistrictName = x.Districtname,
                        BlockName = x.Blockname,
                        PanchayatName = x.panchayatname,
                        TransactionDate = x.sendactiondate,
                        SubsidyReceived = x.SanctionAmount,
                    }).ToList();

                    biaWebFiltersInput.SchemeName = biaWebFilters.SchemeName;
                    biaWebFiltersInput.PanchayatId = biaWebFilters.PanchayatId;
                }

                List<BiaWebGridDetails> biaWebGridDetailsAssignedAndPending = this.beneficiaryService.GetAssignedAndPendingData(biaWebFiltersInput);
                List<BiaWebGridDetails> biaWebGridDetailsVerified = this.beneficiaryService.GetVerifiedData(biaWebFiltersInput);
                List<BiaWebGridDetails> biaWebGridDetailsVerifiedHistory = this.beneficiaryService.GetVerifiedHistoryData(biaWebFiltersInput);
                biaWebGridDetails = biaWebGridDetails.Where(a => !biaWebGridDetailsAssignedAndPending.Any(b => b.RegistrationNo == a.RegistrationNo)).ToList();
                biaWebGridDetails = biaWebGridDetails.Where(a => !biaWebGridDetailsVerified.Any(b => b.RegistrationNo == a.RegistrationNo)).ToList();

                if (biaWebFilters.DataToShow.Equals("Verified"))
                {
                    if (biaWebFilters.BeneficiaryNumber != null && biaWebFilters.IsVerifiedBeneficiaryPopUp == "Y")
                    {
                        List<BiaWebGridDetails> biaWebGridDetailsVerified1 = biaWebGridDetailsVerified.Where(x => x.RegistrationNo == biaWebFilters.BeneficiaryNumber).ToList();
                        List<BiaWebGridDetails> biaWebGridDetailsVerifiedHistory1 = biaWebGridDetailsVerifiedHistory.Where(x => x.RegistrationNo == biaWebFilters.BeneficiaryNumber).ToList();
                        if (biaWebGridDetailsVerifiedHistory1.Count > 0)
                        {
                            biaWebGridDetailsVerified1.AddRange(biaWebGridDetailsVerifiedHistory1);
                        }

                        return this.Ok(biaWebGridDetailsVerified1);
                    }

                    if (biaWebFilters.BeneficiaryNumber != null && biaWebFilters.Status != null && biaWebFilters.Status != "ALL" && biaWebFilters.IsVerifiedBeneficiaryPopUp == "Y")
                    {
                        List<BiaWebGridDetails> biaWebGridDetailsVerified1 = biaWebGridDetailsVerified.Where(x => x.RegistrationNo == biaWebFilters.BeneficiaryNumber).ToList();
                        biaWebGridDetailsVerified1 = biaWebGridDetailsVerified1.Where(x => x.Status == biaWebFilters.Status).ToList();
                        List<BiaWebGridDetails> biaWebGridDetailsVerifiedHistory1 = biaWebGridDetailsVerifiedHistory.Where(x => x.RegistrationNo == biaWebFilters.BeneficiaryNumber).ToList();
                        if (biaWebGridDetailsVerifiedHistory1.Count > 0)
                        {
                            biaWebGridDetailsVerified1.AddRange(biaWebGridDetailsVerifiedHistory1);
                        }

                        return this.Ok(biaWebGridDetailsVerified1);
                    }

                    if (biaWebFilters.BeneficiaryNumber != null)
                    {
                        List<BiaWebGridDetails> biaWebGridDetailsVerified1 = biaWebGridDetailsVerified.Where(x => x.RegistrationNo == biaWebFilters.BeneficiaryNumber).ToList();
                        return this.Ok(biaWebGridDetailsVerified1);
                    }

                    if (biaWebFilters.Status != null && biaWebFilters.Status != "ALL")
                    {
                        List<BiaWebGridDetails> biaWebGridDetailsVerified2 = biaWebGridDetailsVerified.Where(x => x.Status == biaWebFilters.Status).ToList();
                        return this.Ok(biaWebGridDetailsVerified2);
                    }

                    if (biaWebFilters.BeneficiaryNumber != null && biaWebFilters.Status != null && biaWebFilters.Status != "ALL")
                    {
                        List<BiaWebGridDetails> biaWebGridDetailsVerified1 = biaWebGridDetailsVerified.Where(x => x.RegistrationNo == biaWebFilters.BeneficiaryNumber).ToList();
                        biaWebGridDetailsVerified1 = biaWebGridDetailsVerified1.Where(x => x.Status == biaWebFilters.Status).ToList();
                        return this.Ok(biaWebGridDetailsVerified1);
                    }

                    return this.Ok(biaWebGridDetailsVerified);
                }
                else if (biaWebFilters.DataToShow.Equals("AssignedAndPending"))
                {
                    if (biaWebFilters.BeneficiaryNumber != null)
                    {
                        List<BiaWebGridDetails> biaWebGridDetailsAssignedAndPending1 = biaWebGridDetailsAssignedAndPending.Where(x => x.RegistrationNo == biaWebFilters.BeneficiaryNumber).ToList();
                        return this.Ok(biaWebGridDetailsAssignedAndPending1);
                    }

                    return this.Ok(biaWebGridDetailsAssignedAndPending);
                }
                else
                {
                    if (!string.IsNullOrEmpty(biaWebFilters.BeneficiaryNumber))
                    {
                        List<BiaWebGridDetails> biaWebGridDetails1 = biaWebGridDetails.Where(x => x.RegistrationNo == biaWebFilters.BeneficiaryNumber).ToList();
                        return this.Ok(biaWebGridDetails1);
                    }

                    return this.Ok(biaWebGridDetails);
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
