//------------------------------------------------------------------------------
// <copyright file="AssetManagementController.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------
namespace ADIDAS.API.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using ADIDAS.Model.DTO;
    using ADIDAS.Model.Entities;
    using ADIDAS.Service.Interfaces;
    using Azure.Storage.Blobs;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;

    /// <summary>
    /// AssetManagementController.
    /// </summary>
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class AssetManagementController : ControllerBase
    {
        private readonly IAssetManagementService assetManagementService;

        private readonly IOptions<BlobConfig> blobconfig;

        private readonly ILogger<AssetManagementController> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="AssetManagementController"/> class.
        /// AssetManagementController.
        /// </summary>
        /// <param name="assetManagementService">assetManagementService.</param>
        /// <param name="blobconfig">blobconfig.</param>
        /// <param name="logger">logger.</param>
        public AssetManagementController(IAssetManagementService assetManagementService, IOptions<BlobConfig> blobconfig, ILogger<AssetManagementController> logger)
        {
            this.assetManagementService = assetManagementService;
            this.blobconfig = blobconfig;
            this.logger = logger;
        }

        /// <summary>
        /// GetViewLatestSubFarmMachinery.
        /// </summary>
        /// <param name="panchaytId">panchaytId.</param>
        /// <returns>GetViewLatestSubFarmMachinery Values.</returns>
        [HttpGet("GetViewLatestSubFarmMachinery/{panchaytId}")]
        public IActionResult GetViewLatestSubFarmMachinery(int panchaytId)
        {
            try
            {
                List<Machinery> result = this.assetManagementService.GetViewLatestSubFarmMachinery(panchaytId);

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
        /// GetReportFormMachinery.
        /// </summary>
        /// <param name="panchaytId">panchaytId.</param>
        /// <returns>GetReportFormMachinery Values.</returns>
        [HttpGet("GetReportFormMachinery/{panchaytId}")]
        public IActionResult GetReportFormMachinery(int panchaytId)
        {
            try
            {
                List<Machinery> result = this.assetManagementService.GetLatestQtyByMachineryName(panchaytId);

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
        /// GetAgriStructures.
        /// </summary>
        /// <returns>IActionResult.</returns>
        [HttpGet("GetAgriStructures")]
        public IActionResult GetAgriStructures()
        {
            try
            {
                List<AgriStructure> result = this.assetManagementService.GetAgriStructures();

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
        /// GetAllFarmMachinery.
        /// </summary>
        /// <returns>IActionResult.</returns>
        [HttpGet("GetAllFarmMachinery")]
        public IActionResult GetAllFarmMachinery()
        {
            try
            {
                List<AgriMachinery> result = this.assetManagementService.GetAllFarmMachineries();

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
        /// GetReportStucture.
        /// </summary>
        /// <param name="panchayat_id">panchayat_id.</param>
        /// <param name="structure_ID">structure_ID.</param>
        /// <returns>GetReportStucture IActionResult.</returns>
        [HttpGet("GetReportStucture/{panchayat_id}/{Structure_ID}")]
        public IActionResult GetReportStucture(int panchayat_id, int structure_ID)
        {
            try
            {
                List<InsReportStructureModel> result = this.assetManagementService.GetReportStucture(panchayat_id, structure_ID);

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
        /// GetOfflineFacilityDetails.
        /// </summary>
        /// <param name="panchayat_id">panchayat_id.</param>
        /// <returns>GetOfflineFacilityDetails IActionResult.</returns>
        [HttpGet("GetOfflineFacilityDetails/{panchayat_id}")]
        public IActionResult GetOfflineFacilityDetails(int panchayat_id)
        {
            try
            {
                List<InsReportStructureModel> result = this.assetManagementService.GetOfflineFacilityDetails(panchayat_id);

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
        /// GetAgriAssetNoFacilityData.
        /// </summary>
        /// <param name="block_id">block_id.</param>
        /// <returns>GetAgriAssetNoFacilityData IActionResult.</returns>
        [HttpGet("GetAgriAssetNoFacilityData/{Block_id}")]
        public IActionResult GetAgriAssetNoFacilityData(int block_id)
        {
            try
            {
                List<AgriAssetNoFacilityData> result = this.assetManagementService.GetAgriAssetNoFacilityData(block_id);

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
        /// AddMachineryAndStructureType.
        /// </summary>
        /// <param name="insAgriAssetModel">insAgriAssetModel.</param>
        /// <returns>AddMachineryAndStructureType IActionResult.</returns>
        [HttpPost("AddMachineryAndStructureType")]
        public IActionResult AddMachineryAndStructureType([FromBody] InsAgriAssetModel insAgriAssetModel)
        {
            try
            {
                int result = this.assetManagementService.PostAgriAsset(insAgriAssetModel);
                if (result == 1)
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
        /// PostReportfarmMachinery.
        /// </summary>
        /// <param name="reportfarmMachineryModel">reportfarmMachineryModel.</param>
        /// <returns>PostReportfarmMachinery Response.</returns>
        [HttpPost("PostReportfarmMachinery")]
        public IActionResult PostReportfarmMachinery([FromBody] List<ReportfarmMachineryModel> reportfarmMachineryModel)
        {
            try
            {
                int result = this.assetManagementService.PostReportfarmMachinery(reportfarmMachineryModel);
                if (result == 1)
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
        /// PostReportStrcture.
        /// </summary>
        /// <param name="insReportStructureModel">insReportStructureModel.</param>
        /// <returns>PostReportStrcture Response.</returns>
        [HttpPost("PostReportStrcture")]
        public async Task<IActionResult> PostReportStrcture([FromBody] InsReportStructureModel insReportStructureModel)
        {
            try
            {
                BlobServiceClient blobServiceClient = new BlobServiceClient(this.blobconfig.Value.BlobConnection);

                if (!string.IsNullOrEmpty(insReportStructureModel.Image1))
                {
                    BlobEntity blobEntity = new BlobEntity();
                    blobEntity.DirectoryName = "Asset_Management_Photos";
                    blobEntity.FolderName = insReportStructureModel.Panchayat_ID.ToString() + "_" + insReportStructureModel.Structure_ID.ToString() + "_" + insReportStructureModel.Facility_name + "-" + "STRUCT1" + ".jpg";
                    blobEntity.ByteArray = insReportStructureModel.Image1;

                    BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient("mobileapp");

                    string blobPath = blobEntity.DirectoryName + Path.AltDirectorySeparatorChar + blobEntity.FolderName;

                    BlobClient blobClient = containerClient.GetBlobClient(blobPath);

                    byte[] bytes1 = Convert.FromBase64String(blobEntity.ByteArray);
                    Stream stream = new MemoryStream(bytes1);

                    await blobClient.UploadAsync(stream, true);
                    insReportStructureModel.Image_file_location1 = this.blobconfig.Value.AssetMgmt;
                    insReportStructureModel.Image_file_name1 = blobEntity.FolderName;
                }

                if (!string.IsNullOrEmpty(insReportStructureModel.Image2))
                {
                    BlobEntity blobEntity = new BlobEntity();
                    blobEntity.DirectoryName = "Asset_Management_Photos";
                    blobEntity.FolderName = insReportStructureModel.Panchayat_ID.ToString() + "_" + insReportStructureModel.Structure_ID.ToString() + "_" + insReportStructureModel.Facility_name + "-" + "STRUCT2" + ".jpg";
                    blobEntity.ByteArray = insReportStructureModel.Image2;

                    BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient("mobileapp");

                    string blobPath = blobEntity.DirectoryName + Path.AltDirectorySeparatorChar + blobEntity.FolderName;

                    BlobClient blobClient = containerClient.GetBlobClient(blobPath);

                    byte[] bytes1 = Convert.FromBase64String(blobEntity.ByteArray);
                    Stream stream = new MemoryStream(bytes1);

                    await blobClient.UploadAsync(stream, true);
                    insReportStructureModel.Image_file_location2 = this.blobconfig.Value.AssetMgmt;
                    insReportStructureModel.Image_file_name2 = blobEntity.FolderName;
                }

                int result = this.assetManagementService.PostReportStrcture(insReportStructureModel);
                if (result == 1)
                {
                    return this.Ok(new { data = "Record Processed Successfully" });
                }
                else
                {
                    return this.Ok(new { data = "Record not Processed" });
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.Ok(new { data = "Record not Processed" });
            }
        }
    }
}
