//------------------------------------------------------------------------------
// <copyright file="BavasController.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//-------
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
    /// BavasController.
    /// </summary>
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class BavasController : ControllerBase
    {
        private readonly IBavasService bavasService;
        private readonly ILogger<BavasController> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="BavasController"/> class.
        /// BavasController.
        /// </summary>
        /// <param name="bavasService">bavasService.</param>
        public BavasController(IBavasService bavasService, ILogger<BavasController> logger)
        {
            this.bavasService = bavasService;
            this.logger = logger;
        }

        /// <summary>
        /// PostNewNode.
        /// </summary>
        /// <param name="node">node.</param>
        /// <returns>PostNewNode Response.</returns>
        [HttpPost("PostNewNode")]
        public IActionResult PostNewNode([FromBody] Node node)
        {
            try
            {
                int result = this.bavasService.InsertNewNode(node);
                if (result > 0)
                {
                    return this.Ok("{\"status\": \"Record Submitted Successfully\"}");
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
        /// PostBavasStructure.
        /// </summary>
        /// <param name="structureBavas">structureBavas.</param>
        /// <returns>PostBavasStructure Response.</returns>
        [HttpPost("PostBavasStructure")]
        public IActionResult PostBavasStructure([FromBody] List<StructureBavas> structureBavas)
        {
            try
            {
                int result = this.bavasService.InsertBavasStructure(structureBavas);

                if (result > 0)
                {
                    return this.Ok("{\"status\": \"Record Submitted Successfully\"}");
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
        /// PostBavasCropIntelligence.
        /// </summary>
        /// <param name="intelligenceCrops">intelligenceCrops.</param>
        /// <returns>PostBavasCropIntelligence Response.</returns>
        [HttpPost("PostBavasCropIntelligence")]
        public IActionResult PostBavasCropIntelligence([FromBody] List<BavasIntelligenceCrops> intelligenceCrops)
        {
            try
            {
                int result = this.bavasService.InsertBavasCropsIntelligence(intelligenceCrops);
                if (result > 0)
                {
                    return this.Ok("{\"status\": \"Record Submitted Successfully\"}");
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
        /// PostBavasfarmerproducerOrg.
        /// </summary>
        /// <param name="fpo">fpo.</param>
        /// <returns>PostBavasfarmerproducerOrg Response.</returns>
        [HttpPost("PostBavasfarmerproducerOrg")]
        public IActionResult PostBavasfarmerproducerOrg([FromBody] List<FarmerProduceOrg> fpo)
        {
            try
            {
                List<FpoResponse> result = this.bavasService.InsertBavasfarmerproducerOrg(fpo);

                return this.Ok(result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.Problem(ex.Message);
            }
        }

        /// <summary>
        /// PostBavasPriceIntelligence.
        /// </summary>
        /// <param name="intelligence">intelligence.</param>
        /// <returns>PostBavasPriceIntelligence Response.</returns>
        [HttpPost("PostBavasPriceIntelligence")]
        public IActionResult PostBavasPriceIntelligence([FromBody] List<DtoPriceIntelligenceInsert> intelligence)
        {
            try
            {
                int result = this.bavasService.InsertBavasPriceIntelligence(intelligence);
                if (result > 0)
                {
                    return this.Ok("{\"status\": \"Record Submitted Successfully\"}");
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
        /// GetUnitofMeasure.
        /// </summary>
        /// <returns>GetUnitofMeasure List.</returns>
        [HttpGet("GetUnitofMeasure")]
        public IActionResult GetUnitofMeasure()
        {
            try
            {
                List<MeasureBavas> result = this.bavasService.GetUnitofMeasure();

                if (result.Any())
                {
                    return this.Ok(result);
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
        /// GetAgriHortiCrops.
        /// </summary>
        /// <returns>GetAgriHortiCrops.</returns>
        [HttpGet("GetAgriHortiCrops")]
        public IActionResult GetAgriHortiCrops()
        {
            try
            {
                List<AgriHortiCrops> result = this.bavasService.GetAgriHortiCrops();

                if (result.Any())
                {
                    return this.Ok(result);
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
        /// GetAllPriceIntelligenceCrops.
        /// </summary>
        /// <param name="districtId">districtId.</param>
        /// <returns>GetAllPriceIntelligenceCrops list.</returns>
        [HttpGet("GetAllPriceIntelligenceCrops/{districtId}")]
        public IActionResult GetAllPriceIntelligenceCrops(int districtId)
        {
            try
            {
                List<DtoPriceIntelligenceCrops> result = this.bavasService.GetAllPriceIntelligenceCrops(districtId);

                if (result.Any())
                {
                    return this.Ok(result);
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
        /// GetLastSevenDaysPriceIntelligenceDetails.
        /// </summary>
        /// <param name="panchayatId">panchayatId/</param>
        /// <returns>GetLastSevenDaysPriceIntelligenceDetails List.</returns>
        [HttpGet("GetLastSevenDaysPriceIntelligenceDetails/{panchayatId}")]
        public IActionResult GetLastSevenDaysPriceIntelligenceDetails(int panchayatId)
        {
            try
            {
                List<PriceIntelligenceDetails> result = this.bavasService.GetPriceIntelligenceDetailsforPastSevenDays(panchayatId);
                if (result.Any())
                {
                    return this.Ok(result);
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
        /// GetAllFarmerProducerOrgByPanchayat.
        /// </summary>
        /// <param name="panchayatId">panchayatId.</param>
        /// <returns>GetAllFarmerProducerOrgByPanchayat List.</returns>
        [HttpGet("GetAllFarmerProducerOrgByPanchayat/{panchayatId}")]
        public IActionResult GetAllFarmerProducerOrgByPanchayat(int panchayatId)
        {
            try
            {
                List<DtoFpo> result = this.bavasService.GetAllFarmerProducerOrgByPanchayat(panchayatId);

                if (result.Any())
                {
                    return this.Ok(result);
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
        /// GetSpecificFarmerProducerOrg.
        /// </summary>
        /// <param name="fpoId">fpoId.</param>
        /// <returns>GetSpecificFarmerProducerOrg List.</returns>
        [HttpGet("GetSpecificFarmerProducerOrg/{fpoId}")]
        public IActionResult GetSpecificFarmerProducerOrg(int fpoId)
        {
            try
            {
                DtoFpoCrops result = this.bavasService.GetSpecificFarmerProducerOrg(fpoId);

                if (result != null)
                {
                    return this.Ok(result);
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
        /// GetAllContractFarmingDetails.
        /// </summary>
        /// <param name="panchayatId">panchayatId.</param>
        /// <param name="seasonId">seasonId.</param>
        /// <returns>GetAllContractFarmingDetails List.</returns>
        [HttpGet("GetAllContractFarmingDetails/{panchayatId}/{SeasonId}")]
        public IActionResult GetAllContractFarmingDetails(int panchayatId, int seasonId)
        {
            try
            {
                List<DtoFarmerContractingDetails> result = this.bavasService.GetAllContractFarmingDetails(panchayatId, seasonId);

                if (result.Any())
                {
                    return this.Ok(result);
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
        /// GetSpecificContractFarmingDetail.
        /// </summary>
        /// <param name="regno">regno.</param>
        /// <returns>GetSpecificContractFarmingDetail List.</returns>
        [HttpGet("GetSpecificContractFarmingDetail/{regno}")]
        public IActionResult GetSpecificContractFarmingDetail(string regno)
        {
            try
            {
                DtoFarmerContractingDetails result = this.bavasService.GetSpecificContractFarmingDetail(regno);

                if (result != null)
                {
                    return this.Ok(result);
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
        /// GetAllNodesByDistrict.
        /// </summary>
        /// <param name="districtId">districtId.</param>
        /// <returns>GetAllNodesByDistrict List.</returns>
        [HttpGet("GetAllNodesByDistrict/{districtId}")]
        public IActionResult GetAllNodesByDistrict(int districtId)
        {
            try
            {
                List<DtoNodes> result = this.bavasService.GetAllNodesByDistrictId(districtId);

                if (result.Any())
                {
                    return this.Ok(result);
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
        /// GetSeasonalCropsbyDistrict.
        /// </summary>
        /// <param name="districtId">districtId.</param>
        /// <returns>GetSeasonalCropsbyDistrict List.</returns>
        [HttpGet("GetSeasonalCropsbyDistrict/{districtId}")]
        public IActionResult GetSeasonalCropsbyDistrict(int districtId)
        {
            try
            {
                List<DtoSeasonalCrops> result = this.bavasService.GETSEASONALCropsByDistrictId(districtId);

                if (result.Any())
                {
                    return this.Ok(result);
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
        /// GetAllStructureByDistrict.
        /// </summary>
        /// <param name="districtId">districtId.</param>
        /// <returns>GetAllStructureByDistrict List.</returns>
        [HttpGet("GetAllStructureByDistrict/{districtId}")]
        public IActionResult GetAllStructureByDistrict(int districtId)
        {
            try
            {
                List<DtoStructure> result = this.bavasService.GetAllStructureByDistrictId(districtId);

                if (result.Any())
                {
                    return this.Ok(result);
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
        /// PostBavasContractForming.
        /// </summary>
        /// <param name="insBavasContractfarming">insBavasContractfarming.</param>
        /// <returns>PostBavasContractForming Response.</returns>
        [HttpPost("PostBavasContractForming")]
        public IActionResult PostBavasContractForming([FromBody] List<InsBavasContractfarming> insBavasContractfarming)
        {
            try
            {
                int result = this.bavasService.PostBavasContractForming(insBavasContractfarming);
                if (result >= 1)
                {
                    return this.Ok(result);
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
        /// PostBavasMarkettingInfo.
        /// </summary>
        /// <param name="markInfo">markInfo.</param>
        /// <returns>PostBavasMarkettingInfo Response.</returns>
        [HttpPost("PostBavasMarkettingInfo")]
        public IActionResult PostBavasMarkettingInfo([FromBody] List<DtoMarketingInfra> markInfo)
        {
            try
            {
                int result = this.bavasService.InsertBavasMarkettingInfra(markInfo);
                if (result >= 1)
                {
                    return this.Ok(new { data = "Record Processed Successfully" });
                }
                else
                {
                    return this.Ok(new { data = "Data Not Processed" });
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.Ok(new { data = "Data Not Processed" });
            }
        }

        /// <summary>
        /// GetAllFacilityDetails.
        /// </summary>
        /// <param name="structureId">structureId.</param>
        /// <param name="panchayatId">panchayatId.</param>
        /// <returns>GetAllFacilityDetails List.</returns>
        [HttpGet("GetAllFacilityDetails/{structureId}/{panchayatId}")]
        public IActionResult GetAllFacilityDetails(int structureId, int panchayatId)
        {
            try
            {
                List<DtoFacilityDetails> result = this.bavasService.GetAllFacilityDetails(structureId, panchayatId);

                if (result.Any())
                {
                    return this.Ok(result);
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
        /// GetDirectorMarketingInfraStructure.
        /// </summary>
        /// <param name="structureId">structureId.</param>
        /// <param name="panchayatId">panchayatId.</param>
        /// <returns>GetDirectorMarketingInfraStructure List.</returns>
        [HttpGet("GetDirectorMarketingInfraStructure/{structureId}/{panchayatId}")]
        public IActionResult GetDirectorMarketingInfraStructure(int structureId, int panchayatId)
        {
            try
            {
                List<MarkettingInfrastructure> result = this.bavasService.GetDirectorMarketingInfraStructure(structureId, panchayatId);
                if (result.Any())
                {
                    return this.Ok(result);
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
        /// GetDirectorMarketingInfraStructurebyDistrict.
        /// </summary>
        /// <param name="structureId">structureId.</param>
        /// <param name="districtId">districtId.</param>
        /// <returns>GetDirectorMarketingInfraStructurebyDistrict List.</returns>
        [HttpGet("GetDirectorMarketingInfraStructurebyDistrict/{structureId}/{districtId}")]
        public IActionResult GetDirectorMarketingInfraStructurebyDistrict(int structureId, int districtId)
        {
            try
            {
                List<MarkettingInfrastructure> result = this.bavasService.GetDirectorMarketingInfraStructureByDistrict(structureId, districtId);
                if (result.Any())
                {
                    return this.Ok(result);
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
        /// GetMarketingInfoNoFacilityData.
        /// </summary>
        /// <param name="blockid">blockid.</param>
        /// <returns>GetMarketingInfoNoFacilityData List.</returns>
        [HttpGet("GetMarketingInfoNoFacilityData/{blockid}")]
        public IActionResult GetMarketingInfoNoFacilityData(int blockid)
        {
            try
            {
                List<MarketInfoData> result = this.bavasService.GetMarketingInfoNoFacilityData(blockid);
                if (result.Any())
                {
                    return this.Ok(result);
                }

                return this.NotFound();
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound();
            }
        }
    }
}
