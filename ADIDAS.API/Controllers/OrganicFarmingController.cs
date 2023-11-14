//------------------------------------------------------------------------------
// <copyright file="OrganicFarmingController.cs" company="Government of Bihar">
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
    /// Organic Farming Controller
    /// </summary>
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class OrganicFarmingController : Controller
    {
        /// <summary>
        /// IOrganicFarmingService
        /// </summary>
        private readonly IOrganicFarmingService organicFarmingService;
        private readonly ILogger<OrganicFarmingController> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="OrganicFarmingController"/> class.
        /// </summary>
        /// <param name="organicFarmingService">organicFarmingService.</param>
        /// <param name="logger">logger.</param>
        public OrganicFarmingController(IOrganicFarmingService organicFarmingService, ILogger<OrganicFarmingController> logger)
        {
            this.organicFarmingService = organicFarmingService;
            this.logger = logger;
        }

        /// <summary>
        /// Get FPO Details
        /// </summary>
        /// <param name="fpoId">fpoId.</param>
        /// <returns>List FPODetails.</returns>
        [HttpGet("GetFPODetails/{fpoId}")]
        public IActionResult GetFPODetails(int fpoId)
        {
            try
            {
                List<FPODetails> result = this.organicFarmingService.GetFPODetails(fpoId);
                if (result == null)
                {
                    return this.NotFound(this.Json("Not Found"));
                }

                return this.Ok(result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound(this.Json("Not Found"));
            }
        }

        /// <summary>
        /// GetNPOPFarmerDetails.
        /// </summary>
        /// <param name="farmer_dbt_reg_no">farmer_dbt_reg_no.</param>
        /// <returns>NpopFarmerDetailsModel.</returns>
        [HttpGet("GetNPOPFarmerDetails/{farmer_dbt_reg_no}")]
        public IActionResult GetFarmerDetails(long farmer_dbt_reg_no)
        {
            try
            {
                NpopFarmerDetailsModel result = this.organicFarmingService.GetNPOPFarmerDetails(farmer_dbt_reg_no);
                if (result == null)
                {
                    return this.NotFound(this.Json("Not Found"));
                }

                return this.Ok(result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound(this.Json("Not Found"));
            }
        }

        /// <summary>
        /// Get Scheme Details.
        /// </summary>
        /// <returns>List NPOPScheme.</returns>
        [HttpGet("GetSchemeDetails")]
        public IActionResult GetSchemeDetails()
        {
            try
            {
                List<NpopSchemeModel> result = this.organicFarmingService.GetSchemeDetails();
                if (result == null)
                {
                    return this.NotFound(this.Json("Not Found"));
                }

                return this.Ok(result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound(this.Json("Not Found"));
            }
        }

        /// <summary>
        /// GetNPOPFarmDetails.
        /// </summary>
        /// <param name="farmer_dbt_reg_no">farmer_dbt_reg_no.</param>
        /// <returns>IActionResult.</returns>
        [HttpGet("GetNPOPFarmDetails/{farmer_dbt_reg_no}")]
        public IActionResult GetNPOPFarmDetails(long farmer_dbt_reg_no)
        {
            try
            {
                List<NpopFarmDetailsModel> result = this.organicFarmingService.GetNPOPFarmDetails(farmer_dbt_reg_no);
                if (result == null)
                {
                    return this.NotFound(this.Json("Not Found"));
                }

                return this.Ok(result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound(this.Json("Not Found"));
            }
        }

        /// <summary>
        /// GetNPOPCropDetails.
        /// </summary>
        /// <param name="farmerDbtRegNo">farmerDbtRegNo.</param>
        /// <param name="farmId">farmId.</param>
        /// <param name="seasonId">seasonId.</param>
        /// <returns>IActionResult.</returns>
        [HttpGet("GetNPOPCropDetails/{farmerDbtRegNo}/{farmId}/{seasonId}")]
        public IActionResult GetNPOPCropDetails(long farmerDbtRegNo, int farmId, int seasonId)
        {
            try
            {
                List<NpopCropDetailsModel> result = this.organicFarmingService.GetNPOPCropDetails(farmerDbtRegNo, farmId, seasonId);
                if (result == null)
                {
                    return this.NotFound(this.Json("Not Found"));
                }

                return this.Ok(result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound(this.Json("Not Found"));
            }
        }

        /// <summary>
        /// GetNPOPDetails.
        /// </summary>
        /// <param name="districtId">districtId.</param>
        /// <param name="blockId">blockId.</param>
        /// <param name="panchayatId">panchayatId.</param>
        /// <param name="seasonId">seasonId.</param>
        /// <param name="status">status.</param>
        /// <returns>IActionResult.</returns>
        [HttpGet("GetNPOPDetails/{districtId}/{blockId}/{panchayatId}/{seasonId}/{status}")]
        public IActionResult GetNPOPDetails(int districtId, string blockId, string panchayatId, int seasonId, string status)
        {
            try
            {
                List<NpopDetailsListModel> result = this.organicFarmingService.GetNPOPDetails(districtId, blockId, panchayatId, seasonId, status);
                if (result == null)
                {
                    return this.NotFound(this.Json("Not Found"));
                }

                return this.Ok(result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound(this.Json("Not Found"));
            }
        }

        /// <summary>
        /// Post NPOP Details.
        /// </summary>
        /// <param name="createNpopDetails">createNpopDetails.</param>
        /// <returns>int.</returns>
        [HttpPost("PostNPOPDetails")]
        public int PostNPOPDetails([FromBody] NpopDetailsCreateModel createNpopDetails)
        {
            var result = 0;
            try
            {
                result = this.organicFarmingService.PostNPOPDetails(createNpopDetails);
                return result;
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return result;
            }
        }

        /// <summary>
        /// GetFarmerGroupTable.
        /// </summary>
        /// <param name="districtId">districtId.</param>
        /// <returns>result.</returns>
        [HttpGet("GetFarmerGroupTable/{districtId}")]
        public IActionResult GetFarmerGroupTable(int districtId)
        {
            try
            {
                var result = this.organicFarmingService.GetFarmerGroupTable(districtId);
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
        /// GetFarmerGroupSchemeNames.
        /// </summary>
        /// <returns>result.</returns>
        [HttpGet("GetFarmerGroupSchemeNames")]
        public IActionResult GetSchemeNames()
        {
            try
            {
                var result = this.organicFarmingService.GetPGSSchemeNames();
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
        /// PostFarmerGroupDetails.
        /// </summary>
        /// <param name="pgsFarmerGroupDtls">pgsFarmerGroupDtls.</param>
        /// <returns>IActionResult.</returns>
        [HttpPost("PostFarmerGroupDetails")]
        public IActionResult PostFarmerGroupDetails([FromBody] PGSFarmerGroupDetails pgsFarmerGroupDtls)
        {
            try
            {
                int result = this.organicFarmingService.InsertFarmerGroupDetails(pgsFarmerGroupDtls);
                if (result == 1)
                {
                    return this.Ok(new { data = "Inserted Successfully" });
                }
                else
                {
                    return this.NotFound();
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound(this.Json("Not Found"));
            }
        }

        /// <summary>
        /// GetAssociatedFarmerDetails.
        /// </summary>
        /// <param name="districtId">districtId.</param>
        /// <param name="blockId">blockId.</param>
        /// <param name="panchayatId">panchayatId.</param>
        /// <param name="seasonId">seasonId.</param>
        /// <param name="groupId">groupId.</param>
        /// <param name="status">status.</param>
        /// <returns>List GetAssociatedFarmerDetails.</returns>
        [HttpGet("GetAssociatedFarmerDetails/{districtId}/{blockId}/{panchayatId}/{seasonId}/{groupId}/{status}")]
        public IActionResult GetAssociatedFarmerDetails(int districtId, int blockId, int panchayatId, int seasonId, int groupId, string status)
        {
            try
            {
                List<AssociatedFarmerDetailsListModel> result = this.organicFarmingService.GetAssociatedFarmerDetails(districtId, blockId, panchayatId, seasonId, groupId, status);
                if (result == null)
                {
                    return this.NotFound(this.Json("Not Found"));
                }

                return this.Ok(result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound(this.Json("Not Found"));
            }
        }

        /// <summary>
        /// GetGroupNames.
        /// </summary>
        /// <param name="districtId">districtId.</param>
        /// <returns>IActionResult.</returns>
        [HttpGet("GetAssociatedFarmerGroupNames/{districtId}")]
        public IActionResult GetGroupNames(int districtId)
        {
            try
            {
                var result = this.organicFarmingService.GetPGSGroupNames(districtId);
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
        /// GetFarmerGroupDetails.
        /// </summary>
        /// <param name="Regno">Regno.</param>
        /// <returns>IActionResult.</returns>
        [HttpGet("GetFarmerGroupDetails/{Regno}")]
        public IActionResult GetFarmerGroupDetails(string Regno)
        {
            try
            {
                List<PGSFarmerGroupDetailsId> result = this.organicFarmingService.GetFarmerGroupDetails(Regno);
                if (result == null)
                {
                    return this.NotFound(this.Json("Not Found"));
                }

                return this.Ok(result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound(this.Json("Not Found"));
            }
        }

        /// <summary>
        /// GetPGSMajorTownNames
        /// </summary>
        /// <returns>IActionResult.</returns>
        [HttpGet("GetPGSMajorTownNames")]
        public IActionResult GetPGSMajorTownNames()
        {
            try
            {
                var result = this.organicFarmingService.GetPGSMajorTownNames();
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
        /// GetPGSMajorCropNames.
        /// </summary>
        /// <returns>IActionResult.</returns>
        [HttpGet("GetPGSMajorCropNames")]
        public IActionResult GetPGSMajorCropNames()
        {
            try
            {
                var result = this.organicFarmingService.GetPGSMajorCropNames();
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
        /// Get Farmer Details.
        /// </summary>
        /// <param name="farmer_dbt_reg_no">farmer_dbt_reg_no.</param>
        /// <returns>PGSFarmerInfo.</returns>
        [HttpGet("GetPGSFarmerDetails/{farmer_dbt_reg_no}")]
        public IActionResult GetPGSFarmerDetails(long farmer_dbt_reg_no)
        {
            try
            {
                PgsFarmerDetailsModel result = this.organicFarmingService.GetPGSFarmerDetails(farmer_dbt_reg_no);
                if (result == null)
                {
                    return this.NotFound(this.Json("Not Found"));
                }

                return this.Ok(result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound(this.Json("Not Found"));
            }
        }

        /// <summary>
        /// Get PGS Farm Details.
        /// </summary>
        /// <param name="farmer_dbt_reg_no">farmer_dbt_reg_no.</param>
        /// <returns>PGSFarmInfo.</returns>
        [HttpGet("GetPGSFarmDetails/{farmer_dbt_reg_no}")]
        public IActionResult GetPGSFarmDetails(long farmer_dbt_reg_no)
        {
            try
            {
                List<PgsFarmDetailsModel> result = this.organicFarmingService.GetPGSFarmDetails(farmer_dbt_reg_no);
                if (result == null)
                {
                    return this.NotFound(this.Json("Not Found"));
                }

                return this.Ok(result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound(this.Json("Not Found"));
            }
        }

        /// <summary>
        /// Get PGS Crop Details.
        /// </summary>
        /// <param name="farmerDbtRegNo">farmerDbtRegNo.</param>
        /// <param name="farmId">farmId.</param>
        /// <param name="seasonId">seasonId.</param>
        /// <returns>PgsCropDetailsModel.</returns>
        [HttpGet("GetPGSCropDetails/{farmerDbtRegNo}/{farmId}/{seasonId}")]
        public IActionResult GetPGSCropDetails(long farmerDbtRegNo, int farmId, int seasonId)
        {
            try
            {
                List<PgsCropDetailsModel> result = this.organicFarmingService.GetPGSCropDetails(farmerDbtRegNo, farmId, seasonId);
                if (result == null)
                {
                    return this.NotFound(this.Json("Not Found"));
                }

                return this.Ok(result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound(this.Json("Not Found"));
            }
        }

        /// <summary>
        /// Post PGS Details
        /// </summary>
        /// <param name="createPgsDetails">createPgsDetails.</param>
        /// <returns>int.</returns>
        [HttpPost("PostPGSDetails")]
        public int PostPGSDetails([FromBody] PgsDetailsCreateModel createPgsDetails)
        {
            int result = 0;
            try
            {
                result = this.organicFarmingService.PostPGSDetails(createPgsDetails);
                return result;
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return result;
            }
        }

        /// <summary>
        /// GetGroupNamesAndDbtNo.
        /// </summary>
        /// <param name="districtId">districtId.</param>
        /// <returns>IActionResult.</returns>
        [HttpGet("GetGroupNamesAndDbtNo/{districtId}")]
        public IActionResult GetGroupNamesAndDbtNo(int districtId)
        {
            try
            {
                var result = this.organicFarmingService.GetGroupNamesAndDbtNo(districtId);
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
        /// Getting Pgs User values.
        /// </summary>
        /// <param name="userid">userid.</param>
        /// <returns>User Details.</returns>
        [AllowAnonymous]
        [HttpGet("IsPgsUser/{userid}")]
        public IActionResult IsPgsUser(string userid)
        {
            try
            {
                var result = this.organicFarmingService.IsPgsUser(Convert.ToInt32(userid));

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
        /// Getting Npop User values.
        /// </summary>
        /// <param name="userid">userid.</param>
        /// <param name="designation">designation.</param>
        /// <returns>User Details.</returns>
        [AllowAnonymous]
        [HttpGet("IsNpopUser/{userid}")]
        public IActionResult IsNpopUser(string userid, string designation = null)
        {
            try
            {
                var result = this.organicFarmingService.IsNpopUser(Convert.ToInt32(userid), designation);

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
        /// Get Npop Major Town Names.
        /// </summary>
        /// <returns>Town Names.</returns>
        [HttpGet("GetNpopMajorTownNames")]
        public IActionResult GetNpopMajorTownNames()
        {
            try
            {
                var result = this.organicFarmingService.GetNpopMajorTownNames();
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
        /// Get Npop Major Crop Names.
        /// </summary>
        /// <returns>Crop Names.</returns>
        [HttpGet("GetNpopMajorCropNames")]
        public IActionResult GetNpopMajorCropNames()
        {
            try
            {
                var result = this.organicFarmingService.GetNpopMajorCropNames();
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
        /// PostAgriFarmerDetails.
        /// </summary>
        /// <param name="orgFarmAgriFarmerDetails">orgFarmAgriFarmerDetails.</param>
        /// <returns>OrgFarmAgriFarmerDetails Result.</returns>
        [HttpPost("PostAgriFarmerDetails")]
        public IActionResult PostAgriFarmerDetails([FromBody] OrgFarmAgriFarmerDetails orgFarmAgriFarmerDetails)
        {
            try
            {
                var result = this.organicFarmingService.InsertAgriFarmerDetails(orgFarmAgriFarmerDetails);
                if (result == 1)
                {
                    return this.Ok(new { data = "Inserted Successfully" });
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
    }
}
