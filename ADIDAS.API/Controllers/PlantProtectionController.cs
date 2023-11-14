//------------------------------------------------------------------------------
// <copyright file="PlantProtectionController.cs" company="Government of Bihar">
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
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Plant Protection Controller.
    /// </summary>
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PlantProtectionController : Controller
    {
        private readonly IPlantProtectionService plantProtectionService;
        private readonly ILogger<PlantProtectionController> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="PlantProtectionController"/> class.
        /// Plant Protection Controller.
        /// </summary>
        /// <param name="plantProtectionService">plantProtectionService.</param>
        /// <param name="logger">logger.</param>
        public PlantProtectionController(IPlantProtectionService plantProtectionService, ILogger<PlantProtectionController> logger)
        {
            this.plantProtectionService = plantProtectionService;
            this.logger = logger;
        }

        /// <summary>
        /// Get Pesticide Data.
        /// </summary>
        /// <returns>Action Result.</returns>
        [HttpGet("GetPesticideData")]
        public IActionResult GetPesticideData()
        {
            try
            {
                List<PesticideModel> result = this.plantProtectionService.GetPesticideData();

                if (!result.Any())
                {
                    return this.NotFound();
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
        /// Get Pesticide PerfData.
        /// </summary>
        /// <param name="district_Id">district_Id.</param>
        /// <param name="pesticide_Id">pesticide_Id.</param>
        /// <param name="formulation_Id">formulation_Id.</param>
        /// <returns>Action Result.</returns>
        [HttpGet("GetPesticidePerfData/{district_Id}/{pesticide_Id}/{formulation_Id}")]
        public IActionResult GetPesticidePerfData(string district_Id, string pesticide_Id, string formulation_Id)
        {
            try
            {
                List<PesticidePerf> result = this.plantProtectionService.GetPesticidePerfData(district_Id, pesticide_Id, formulation_Id);

                if (!result.Any())
                {
                    return this.NotFound();
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
        /// Get Pesticide Surveillance PerfData.
        /// </summary>
        /// <param name="district_Id">district_Id.</param>
        /// <param name="crop_Id">crop_Id.</param>
        /// <returns>Action Result.</returns>
        [HttpGet("GetPesticideSurveillancePerfData/{district_Id}/{crop_Id}")]
        public IActionResult GetPesticideSurveillancePerfData(string district_Id, string crop_Id)
        {
            try
            {
                List<PestSurveillancePerf> result = this.plantProtectionService.GetPesticideSurveillancePerfData(district_Id, crop_Id);

                if (!result.Any())
                {
                    return this.NotFound();
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
        /// Get Pesticide PerfData Monthly.
        /// </summary>
        /// <param name="district_Id">district_Id.</param>
        /// <param name="mm_year">mm_year.</param>
        /// <returns>Action Result.</returns>
        [HttpGet("GetPesticidePerfDataMonthly/{district_Id}/{mm_year}")]
        public IActionResult GetPesticidePerfDataMonthly(string district_Id, string mm_year)
        {
            try
            {
                List<PesticidePerf> result = this.plantProtectionService.GetPesticidePerfDataMonthly(district_Id, mm_year);

                if (!result.Any())
                {
                    return this.NotFound();
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
        /// Get CombPesticide Data.
        /// </summary>
        /// <returns>ActionResult.</returns>
        [HttpGet("GetCombPesticideData")]
        public IActionResult GetCombPesticideData()
        {
            try
            {
                List<CombPesticide> result = this.plantProtectionService.GetCombPesticideData();

                if (!result.Any())
                {
                    return this.NotFound();
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
        /// Get Pesticide CombPerf Data.
        /// </summary>
        /// <param name="district_Id">district_Id.</param>
        /// <param name="comb_Pesticide_Id">comb_Pesticide_Id.</param>
        /// <returns>Action Result.</returns>
        [HttpGet("GetPesticideCombPerfData/{district_Id}/{comb_Pesticide_Id}")]
        public IActionResult GetPesticideCombPerfData(string district_Id, string comb_Pesticide_Id)
        {
            try
            {
                List<PesticideCombPerf> result = this.plantProtectionService.GetPesticideCombPerfData(district_Id, comb_Pesticide_Id);

                if (!result.Any())
                {
                    return this.NotFound();
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
        /// Get Pesticide CombPerf Data Montly.
        /// </summary>
        /// <param name="district_Id">district_Id.</param>
        /// <param name="mm_year">mm_year.</param>
        /// <returns>ActionResult.</returns>
        [HttpGet("GetPesticideCombPerfDataMontly/{district_Id}/{mm_year}")]
        public IActionResult GetPesticideCombPerfDataMontly(string district_Id, string mm_year)
        {
            try
            {
                List<PesticideCombPerf> result = this.plantProtectionService.GetPesticideCombPerfDataMonthly(district_Id, mm_year);

                if (!result.Any())
                {
                    return this.NotFound();
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
        /// Get Pesticide Surviellance Monthly.
        /// </summary>
        /// <param name="district_Id">district_Id.</param>
        /// <param name="mm_year">mm_year.</param>
        /// <returns>Action Result.</returns>
        [HttpGet("GetPesticideSurviellanceMonthly/{district_Id}/{mm_year}")]
        public IActionResult GetPesticideSurviellanceMonthly(string district_Id, string mm_year)
        {
            try
            {
                PestSurviellanceDisease result = this.plantProtectionService.GetPesticideSurveillanceMonthly(district_Id, mm_year);

                if (result.Mm_year == null)
                {
                    return this.NotFound();
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
        /// Post Pesticide Perf.
        /// </summary>
        /// <param name="dTOPesticideperf">dTOPesticideperf.</param>
        /// <returns>Action Result.</returns>
        [HttpPost("PostPesticidePerf")]
        public IActionResult PostPesticidePerf(DtoPesticideperf dTOPesticideperf)
        {
            try
            {
                int result = this.plantProtectionService.InsertPesticidePerf(dTOPesticideperf);

                if (result == 0)
                {
                    return this.NotFound("{\"status\": \"Insertion Failed\"}");
                }

                return this.Ok("{\"status\": \"Data Successfully Inserted/Updated\"}");
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound("{\"status\": \"Insertion Failed\"}");
            }
        }

        /// <summary>
        /// Post Pesticide Perf Comb.
        /// </summary>
        /// <param name="dTOPesticideperf">dTOPesticideperf.</param>
        /// <returns>Action Result.</returns>
        [HttpPost("PostPesticidePerfComb")]
        public IActionResult PostPesticidePerfComb(DtoPesticidePerfComb dTOPesticideperf)
        {
            try
            {
                int result = this.plantProtectionService.InsertPesticidePerfComb(dTOPesticideperf);

                if (result == 0)
                {
                    return this.NotFound("{\"status\": \"Insertion Failed\"}");
                }

                return this.Ok("{\"status\": \"Data Successfully Inserted/Updated\"}");
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound("{\"status\": \"Insertion Failed\"}");
            }
        }

        /// <summary>
        /// Post Pest Surveillance.
        /// </summary>
        /// <param name="surviellance">surviellance.</param>
        /// <returns>Action Result.</returns>
        [HttpPost("PostPestSurveillance")]
        public IActionResult PostPestSurveillance(PestSurviellance surviellance)
        {
            try
            {
                int result = this.plantProtectionService.InsertPestSurveillance(surviellance);

                if (result == 0)
                {
                    return this.NotFound("{\"status\": \"Insertion Failed\"}");
                }

                return this.Ok("{\"status\": \"Data Successfully Inserted/Updated\"}");
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound("{\"status\": \"Insertion Failed\"}");
            }
        }

        /// <summary>
        /// Get Pesticide Perf Offline.
        /// </summary>
        /// <param name="district_Id">district_Id.</param>
        /// <returns>Action Result.</returns>
        [HttpGet("GetPesticidePerfOffline/{district_Id}")]
        public IActionResult GetPesticidePerfOffline(string district_Id)
        {
            List<PesticidePerfOffline> result = new List<PesticidePerfOffline>();

            try
            {
                result = this.plantProtectionService.GetPesticidePerfOffline(district_Id);

                if (!result.Any())
                {
                    return this.NotFound();
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
        /// Get Pesticide Surveillance Disease.
        /// </summary>
        /// <returns>Action Result.</returns>
        [HttpGet("GetPesticideSurveillanceDisease")]
        public IActionResult GetPesticideSurveillanceDisease()
        {
            List<PestSurveillanceDisease> result = new List<PestSurveillanceDisease>();

            try
            {
                result = this.plantProtectionService.GetPesticideSurveillanceDisease();

                if (!result.Any())
                {
                    return this.NotFound();
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
        /// Get Crop Stage.
        /// </summary>
        /// <returns>Action Result.</returns>
        [HttpGet("GetCropStage")]
        public IActionResult GetCropStage()
        {
            List<CropStageName> result = new List<CropStageName>();

            try
            {
                result = this.plantProtectionService.GetCropStage();

                if (!result.Any())
                {
                    return this.NotFound();
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
        /// Get Pesticide surveillance Offline.
        /// </summary>
        /// <param name="district_Id">district_Id.</param>
        /// <returns>Action Result.</returns>
        [HttpGet("GetPesticideSurveillanceOffline/{district_Id}")]
        public IActionResult GetPesticidesurveillanceOffline(string district_Id)
        {
            List<PestSurviellanceDisease> result = new List<PestSurviellanceDisease>();
            try
            {
                result = this.plantProtectionService.GetPesticidesurveillanceOffline(district_Id);

                if (!result.Any())
                {
                    return this.NotFound();
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
        /// Get CombPesticidePerf Offline.
        /// </summary>
        /// <param name="district_Id">district_Id.</param>
        /// <returns>Action Result.</returns>
        [HttpGet("GetCombPesticidePerfOffline/{district_Id}")]
        public IActionResult GetCombPesticidePerfOffline(string district_Id)
        {
            List<CombPesticidePerfOffline> result = new List<CombPesticidePerfOffline>();

            try
            {
                result = this.plantProtectionService.GetCombPesticidePerfOffline(district_Id);

                if (!result.Any())
                {
                    return this.NotFound();
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
        /// Get Approved Area Coverage.
        /// </summary>
        /// <param name="district_Id">district_Id.</param>
        /// <param name="crop_Id">crop_Id.</param>
        /// <param name="season_Id">season_Id.</param>
        /// <returns>Action Result.</returns>
        [HttpGet("GetApprovedAreaCoverage/{district_Id}/{crop_Id}/{season_Id}")]
        public IActionResult GetApprovedAreaCoverage(string district_Id, string crop_Id, string season_Id)
        {
            try
            {
                List<ApprovedAreaCoverageRes> res = this.plantProtectionService.GetApprovedAreaCoverage(district_Id, crop_Id, season_Id);
                if (!res.Any())
                {
                    return this.NotFound();
                }

                return this.Ok(res);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound("Not Found");
            }
        }
    }
}
