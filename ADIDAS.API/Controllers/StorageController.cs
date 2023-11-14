//------------------------------------------------------------------------------
// <copyright file="StorageController.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------

namespace ADIDAS.API.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Data.SqlClient;
    using System.IO;
    using System.IO.Compression;
    using System.Linq;
    using System.Net;
    using System.Net.Http.Headers;
    using System.Text;
    using System.Threading.Tasks;
    using ADIDAS.Model.DTO;
    using ADIDAS.Model.Entities;
    using ADIDAS.Repository.Helper;
    using Azure;
    using Azure.Storage.Blobs;
    using Azure.Storage.Blobs.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;

    /// <summary>
    /// Storage Controller.
    /// </summary>
    //[Authorize]
    [Route("/[controller]")]
    [ApiController]
    public class StorageController : ControllerBase
    {
        private static readonly char[] InvalidFilenameChars = Path.GetInvalidFileNameChars();
        private readonly BlobServiceClient blobServiceClient;
        private readonly IOptions<BlobConfig> blobConfig;
        private readonly IOptions<DBSettings> options;
        private readonly ILogger<StorageController> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="StorageController"/> class.
        /// Storage Controller.
        /// </summary>
        /// <param name="blobConfig">blobConfig.</param>
        /// <param name="options">options.</param>
        /// <param name="logger">logger.</param>
        public StorageController(IOptions<BlobConfig> blobConfig, IOptions<DBSettings> options, ILogger<StorageController> logger)
        {
            this.blobConfig = blobConfig;
            this.options = options;
            SqlHelper.SetConnectionString(this.options.Value.ConnectionString);
            blobServiceClient = new BlobServiceClient(this.blobConfig.Value.BlobConnection);
            this.logger = logger;
        }

        /// <summary>
        /// List All Files.
        /// </summary>
        /// <returns>Action Result.</returns>
        [HttpGet("ListAllFiles")]
        public async Task<IActionResult> ListAllFiles()
        {
            try
            {
                BlobContainerClient containerClient = this.blobServiceClient.GetBlobContainerClient("mobileapp");

                AsyncPageable<BlobItem> blobs = containerClient.GetBlobsAsync(BlobTraits.Metadata);

                List<BlobListEntity> list = new List<BlobListEntity>();
                await foreach (BlobItem blobItem in blobs)
                {
                    BlobListEntity blobEntity = new BlobListEntity();
                    blobEntity.BlobName = blobItem.Name;
                    blobEntity.Metadata = blobItem.Metadata;

                    list.Add(blobEntity);
                }

                return this.Ok(list);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound("Not Found");
            }
        }

        /// <summary>
        /// List Files.
        /// </summary>
        /// <param name="directoryName">directoryName.</param>
        /// <param name="folderName">folderName.</param>
        /// <returns>Action Result.</returns>
        [HttpGet("ListFiles/{DirectoryName}/{FolderName}/")]
        public async Task<IActionResult> ListFiles(string directoryName, string folderName)
        {
            try
            {
                BlobContainerClient containerClient = this.blobServiceClient.GetBlobContainerClient("mobileapp");
                string path = directoryName + Path.AltDirectorySeparatorChar + folderName;
                AsyncPageable<BlobItem> blobs = containerClient.GetBlobsAsync(BlobTraits.Metadata, prefix: path);

                List<BlobListEntity> list = new List<BlobListEntity>();
                await foreach (BlobItem blobItem in blobs)
                {
                    BlobListEntity blobEntity = new BlobListEntity();
                    blobEntity.BlobName = blobItem.Name;
                    blobEntity.Metadata = blobItem.Metadata;
                    list.Add(blobEntity);
                }

                return this.Ok(list);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound("Not Found");
            }
        }

        /// <summary>
        /// Get Blob Meta Data.
        /// </summary>
        /// <param name="fileType">fileType.</param>
        /// <returns>Action Result.</returns>
        [HttpGet("GetBlobMetaData/{fileType}")]
        public IActionResult GetBlobMetaData(string fileType)
        {
            try
            {
                List<DbParameter> parameters = new List<DbParameter>();

                List<BlobMetaData> list = new List<BlobMetaData>();

                parameters.Add(new SqlParameter { ParameterName = "@file_type", Value = fileType, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>("usp_storage_cntrlr", parameters, SqlHelper.ExecutionType.Procedure);
                if (dt != null && dt.Rows.Count > 0)
                {
                    list = SqlHelper.ConvertDataTableToList<BlobMetaData>(dt);
                }

                if (list.Any())
                {
                    return this.Ok(list);
                }
                else
                {
                    return this.NotFound();
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound("Not Found");
            }
        }

        /// <summary>
        /// Get Blob Async.
        /// </summary>
        /// <param name="directoryName">directoryName.</param>
        /// <param name="folderName">folderName.</param>
        /// <returns>BlobDownloadInfo.</returns>
        [HttpGet("GetBlobAsync/{DirectoryName}/{FolderName}/")]
        public async Task<BlobDownloadInfo> GetBlobAsync(string directoryName, string folderName)
        {
            BlobListEntity blobEntity = new BlobListEntity();
            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient("mobileapp");
            string path = directoryName + Path.AltDirectorySeparatorChar + folderName;
            AsyncPageable<BlobItem> blobs = containerClient.GetBlobsAsync(BlobTraits.Metadata, prefix: path);
            await foreach (BlobItem blobItem in blobs)
            {
                blobEntity.BlobName = blobItem.Name;
                blobEntity.Metadata = blobItem.Metadata;
            }

            var blobClient = containerClient.GetBlobClient(blobEntity.BlobName);
            var blobDownloadInfo = await blobClient.DownloadAsync();
            return blobDownloadInfo.Value;
        }

        /// <summary>
        /// Upload File.
        /// </summary>
        /// <param name="blobEntity">blobEntity.</param>
        /// <returns>Action Result.</returns>
        [HttpPost("UploadFile")]
        public async Task<IActionResult> UploadFile([FromBody] BlobEntity blobEntity)
        {
            try
            {
                BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient("mobileapp");
                string blobPath = blobEntity.DirectoryName + Path.AltDirectorySeparatorChar + blobEntity.FolderName + Path.AltDirectorySeparatorChar + System.IO.Path.GetFileName(blobEntity.LocalFilePath);
                BlobClient blobClient = containerClient.GetBlobClient(blobPath);
                if (blobEntity.LocalFilePath.IndexOfAny(InvalidFilenameChars) >= 0)
                {
                    return new StatusCodeResult((int)HttpStatusCode.BadRequest);
                }

                // Open the file and upload its data
                using FileStream uploadFileStream = System.IO.File.OpenRead(blobEntity.LocalFilePath);
                await blobClient.UploadAsync(uploadFileStream, overwrite: true);
                uploadFileStream.Close();
                return this.Ok(string.Empty);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound("Not Found");
            }
        }

        /// <summary>
        /// Upload File Stream.
        /// </summary>
        /// <param name="blobEntity">blobEntity.</param>
        /// <returns>Action Result.</returns>
        [HttpPost("UploadFileStream")]
        public async Task<IActionResult> UploadFileStream([FromBody] BlobEntity blobEntity)
        {
            try
            {
                BlobContainerClient containerClient = this.blobServiceClient.GetBlobContainerClient("mobileapp");
                string blobPath = blobEntity.DirectoryName + Path.AltDirectorySeparatorChar + blobEntity.FolderName;
                BlobClient blobClient = containerClient.GetBlobClient(blobPath);
                var bytes = Encoding.UTF8.GetBytes(blobEntity.ByteArray);
                await using var memoryStream = new MemoryStream(bytes);
                await blobClient.UploadAsync(memoryStream, overwrite: true);
                return this.Ok(string.Empty);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound("Not Found");
            }
        }

        /// <summary>
        /// Upload File Stream Data.
        /// </summary>
        /// <param name="blobEntity">blobEntity.</param>
        /// <returns>Action Result.</returns>
        [HttpPost("UploadFileStreamData")]
        public async Task<IActionResult> UploadFileStreamData([FromBody] BlobDetails blobEntity)
        {
            BlobResponse response = new BlobResponse();
            bool isValidate = false;

            // upload file data
            try
            {
                if (!string.IsNullOrEmpty(blobEntity.Directory_Name) && !string.IsNullOrEmpty(blobEntity.File_name))
                {
                    if (blobEntity.File_status == "Active" && blobEntity.Uploaded_user_id != 0 && blobEntity.Uploaded_user_id != null)
                    {
                        if (!string.IsNullOrEmpty(blobEntity.File_type) && !string.IsNullOrEmpty(blobEntity.Title) && !string.IsNullOrEmpty(blobEntity.Description))
                        {
                            isValidate = true;
                            BlobContainerClient containerClient = this.blobServiceClient.GetBlobContainerClient("mobileapp");

                            string blobPath = blobEntity.Directory_Name + Path.AltDirectorySeparatorChar + blobEntity.Folder_Name + Path.AltDirectorySeparatorChar + blobEntity.File_name;

                            string filelocation = this.blobConfig.Value.BlobBaseUrl + blobEntity.Directory_Name + Path.AltDirectorySeparatorChar + blobEntity.Folder_Name + Path.AltDirectorySeparatorChar;

                            BlobClient blobClient = containerClient.GetBlobClient(blobPath);

                            var bytes = Convert.FromBase64String(blobEntity.ByteArray);

                            await using var memoryStream = new MemoryStream(bytes);

                            await blobClient.UploadAsync(memoryStream, overwrite: true);

                            List<DbParameter> dbparamsBlobInfo = new List<DbParameter>();

                            dbparamsBlobInfo.Add(new SqlParameter { ParameterName = "@blob_id", Value = blobEntity.Blob_id == null ? DBNull.Value : (object)blobEntity.Blob_id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });

                            dbparamsBlobInfo.Add(new SqlParameter { ParameterName = "@title", Value = string.IsNullOrEmpty(blobEntity.Title) ? DBNull.Value : (object)blobEntity.Title, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });

                            dbparamsBlobInfo.Add(new SqlParameter { ParameterName = "@description", Value = string.IsNullOrEmpty(blobEntity.Description) ? DBNull.Value : (object)blobEntity.Description, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });

                            dbparamsBlobInfo.Add(new SqlParameter { ParameterName = "@file_type", Value = string.IsNullOrEmpty(blobEntity.File_type) ? DBNull.Value : (object)blobEntity.File_type, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });

                            dbparamsBlobInfo.Add(new SqlParameter { ParameterName = "@file_name", Value = string.IsNullOrEmpty(blobEntity.File_name) ? DBNull.Value : (object)blobEntity.File_name, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });

                            dbparamsBlobInfo.Add(new SqlParameter { ParameterName = "@file_location", Value = string.IsNullOrEmpty(filelocation) ? DBNull.Value : (object)filelocation, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });

                            dbparamsBlobInfo.Add(new SqlParameter { ParameterName = "@file_status", Value = string.IsNullOrEmpty(blobEntity.File_status) ? DBNull.Value : (object)blobEntity.File_status, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });

                            dbparamsBlobInfo.Add(new SqlParameter { ParameterName = "@recp_group_id", Value = string.IsNullOrEmpty(blobEntity.Recp_group_id) ? DBNull.Value : (object)blobEntity.Recp_group_id, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });

                            dbparamsBlobInfo.Add(new SqlParameter { ParameterName = "@uploaded_datetime", Value = blobEntity.Uploaded_datetime == null ? DBNull.Value : (object)blobEntity.Uploaded_datetime, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });

                            dbparamsBlobInfo.Add(new SqlParameter { ParameterName = "@uploaded_user_id", Value = blobEntity.Uploaded_user_id == null ? DBNull.Value : (object)blobEntity.Uploaded_user_id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });

                            dbparamsBlobInfo.Add(new SqlParameter { ParameterName = "@deleted_user_id", Value = blobEntity.Deleted_user_id == null ? DBNull.Value : (object)blobEntity.Deleted_user_id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });

                            dbparamsBlobInfo.Add(new SqlParameter { ParameterName = "@deleted_datetime", Value = blobEntity.Deleted_datetime == null ? DBNull.Value : (object)blobEntity.Deleted_datetime, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });

                            dbparamsBlobInfo.Add(new SqlParameter { ParameterName = "@response_status", SqlDbType = SqlDbType.NVarChar, Size = 200, Direction = ParameterDirection.Output });

                            Dictionary<string, dynamic> result = SqlHelper.ExecuteNonQuery<SqlConnection>("usp_blob_master", dbparamsBlobInfo, SqlHelper.ExecutionType.Procedure);

                            string spOut = DBNull.Value.Equals(result["@response_status"]) ? string.Empty : result["@response_status"];

                            if (!string.IsNullOrEmpty(spOut))
                            {
                                foreach (var keyvaluepair in spOut.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries))
                                {
                                    string[] splitteddata = keyvaluepair.Split(new string[] { ":" }, StringSplitOptions.RemoveEmptyEntries);
                                    if (splitteddata[0].Trim().Equals("Status"))
                                    {
                                        response.Status = splitteddata[1];
                                    }
                                    else if (splitteddata[0].Trim().Equals("Reason"))
                                    {
                                        response.Reason = splitteddata[1];
                                    }
                                    else if (splitteddata[0].Trim().Equals("File_Name"))
                                    {
                                        response.File_Name = splitteddata[1];
                                    }
                                }
                            }
                            else
                            {
                                response.Status = "Failed";
                                response.Reason = "SomeErrorOccured";
                                response.File_Name = blobEntity.File_name;
                            }
                        }
                    }
                }
                if (!isValidate)
                {
                    response.Status = "ValidationFailed";
                    response.Reason = "Validation Failed for the Input Passed";
                    response.File_Name = string.Empty;
                }

                return this.Ok(response);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                response.Status = "Failed";
                response.Reason = ex.Message;
                response.File_Name = blobEntity.File_name;

                return this.NotFound(response);
            }
        }

        /// <summary>
        /// Upload Image.
        /// </summary>
        /// <param name="blobEntity">blobEntity.</param>
        /// <returns>int.</returns>
        [HttpPost("UploadImage")]
        public async Task<int> UploadImage([FromBody] BlobEntity blobEntity)
        {
            try
            {
                BlobContainerClient containerClient = this.blobServiceClient.GetBlobContainerClient("mobileapp");

                string blobPath = blobEntity.DirectoryName + Path.AltDirectorySeparatorChar + blobEntity.FolderName;

                BlobClient blobClient = containerClient.GetBlobClient(blobPath);

                byte[] bytes1 = Convert.FromBase64String(blobEntity.ByteArray);
                Stream stream = new MemoryStream(bytes1);
                await blobClient.UploadAsync(stream, true);
                return 1;
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return 0;
            }
        }

        /// <summary>
        /// Compress String.
        /// </summary>
        /// <param name="text">text.</param>
        /// <returns>buffer string.</returns>
        public static string CompressString(string text)
        {
            try
            {
                byte[] buffer = Encoding.UTF8.GetBytes(text);
                var memoryStream = new MemoryStream();
                using (var gZipStream = new GZipStream(memoryStream, CompressionMode.Compress, true))
                {
                    gZipStream.Write(buffer, 0, buffer.Length);
                }

                memoryStream.Position = 0;

                var compressedData = new byte[memoryStream.Length];
                memoryStream.Read(compressedData, 0, compressedData.Length);

                var gZipBuffer = new byte[compressedData.Length + 4];
                Buffer.BlockCopy(compressedData, 0, gZipBuffer, 4, compressedData.Length);
                Buffer.BlockCopy(BitConverter.GetBytes(buffer.Length), 0, gZipBuffer, 0, 4);
                return Convert.ToBase64String(gZipBuffer);
            }
            catch (Exception ex)
            {
                return "Error Occured Compress String";
            }
        }

        /// <summary>
        /// Decompress String.
        /// </summary>
        /// <param name="compressedText">compressedText.</param>
        /// <returns>buffer string.</returns>
        public static string DecompressString(string compressedText)
        {
            try
            {
                byte[] gZipBuffer = Convert.FromBase64String(compressedText);
                using (var memoryStream = new MemoryStream())
                {
                    int dataLength = BitConverter.ToInt32(gZipBuffer, 0);
                    memoryStream.Write(gZipBuffer, 4, gZipBuffer.Length - 4);

                    var buffer = new byte[dataLength];

                    memoryStream.Position = 0;
                    using (var gZipStream = new GZipStream(memoryStream, CompressionMode.Decompress))
                    {
                        gZipStream.Read(buffer, 0, buffer.Length);
                    }

                    return Encoding.UTF32.GetString(buffer);
                }
            }
            catch (Exception ex)
            {
                return "Error Occured Decompress String";
            }
        }

        /// <summary>
        /// Download file.
        /// </summary>
        /// <param name="blobEntity">blobEntity.</param>
        /// <returns>Action Result.</returns>
        [HttpPost("Downloadfile")]
        public async Task<IActionResult> Downloadfile([FromBody] BlobEntity blobEntity)
        {
            try
            {
                BlobContainerClient containerClient = this.blobServiceClient.GetBlobContainerClient("mobileapp");

                string blobFilePath = blobEntity.DirectoryName + Path.AltDirectorySeparatorChar + blobEntity.FolderName + Path.AltDirectorySeparatorChar + blobEntity.BlobName;

                BlobClient blobClient = containerClient.GetBlobClient(blobFilePath);

                BlobDownloadInfo download = await blobClient.DownloadAsync();

                string path = blobEntity.LocalFolderPath + "\\" + blobEntity.BlobName;

                if (path.IndexOfAny(InvalidFilenameChars) >= 0)
                {
                    return new StatusCodeResult((int)HttpStatusCode.BadRequest);
                }

                using (FileStream downloadFileStream = System.IO.File.OpenWrite(path))
                {
                    await download.Content.CopyToAsync(downloadFileStream);
                    downloadFileStream.Close();
                }

                return this.Ok(string.Empty);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound("Not Found");
            }
        }

        /// <summary>
        /// Download File Stream.
        /// </summary>
        /// <param name="blobEntity">blobEntity.</param>
        /// <returns>Action Result.</returns>
        [HttpPost("DownloadFileStream")]
        public async Task<IActionResult> DownloadFileStream([FromBody] BlobEntity blobEntity)
        {
            try
            {
                BlobContainerClient containerClient = this.blobServiceClient.GetBlobContainerClient("mobileapp");

                string blobFilePath = blobEntity.DirectoryName + Path.AltDirectorySeparatorChar + blobEntity.FolderName + Path.AltDirectorySeparatorChar + blobEntity.BlobName;

                BlobClient blobClient = containerClient.GetBlobClient(blobFilePath);

                BlobDownloadInfo download = await blobClient.DownloadAsync();

                FileStreamResult fileStreamResult = null;

                if (Path.GetExtension(blobFilePath) == ".mp4")
                {
                    fileStreamResult = this.File(download.Content, contentType: new MediaTypeHeaderValue("video/mp4").MediaType, enableRangeProcessing: true);
                }
                else if (Path.GetExtension(blobFilePath) == ".jpeg" || Path.GetExtension(blobFilePath) == ".jpg" || Path.GetExtension(blobFilePath) == ".png" || Path.GetExtension(blobFilePath) == ".bmp")
                {
                    fileStreamResult = this.File(download.Content, "image/jpeg");
                }
                else if (Path.GetExtension(blobFilePath) == ".txt")
                {
                    fileStreamResult = this.File(download.Content, "text/plain");
                }
                else
                {
                    return this.Ok(download.Content);
                }
                return this.Ok(fileStreamResult);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound("Not Found");
            }
        }

        /// <summary>
        /// Delete File.
        /// </summary>
        /// <param name="blobEntity">blobEntity.</param>
        /// <returns>Action Result.</returns>
        [HttpDelete]
        public async Task<IActionResult> DeleteFile(BlobDetails blobEntity)
        {
            BlobResponse response = new BlobResponse();
            int insertRowsCount = 0;
            bool isValidate = false;
            try
            {
                if (!string.IsNullOrEmpty(blobEntity.Directory_Name) && !string.IsNullOrEmpty(blobEntity.Folder_Name) && !string.IsNullOrEmpty(blobEntity.File_name))
                {
                    if (blobEntity.File_status == "Deleted" && blobEntity.Blob_id != 0 && blobEntity.Blob_id != null)
                    {
                        if (blobEntity.Deleted_user_id != 0 && blobEntity.Deleted_user_id != null && blobEntity.Deleted_datetime != null)
                        {
                            isValidate = true;

                            BlobContainerClient containerClient = this.blobServiceClient.GetBlobContainerClient("mobileapp");

                            string blobFilePath = blobEntity.Directory_Name + Path.AltDirectorySeparatorChar + blobEntity.Folder_Name + Path.AltDirectorySeparatorChar + blobEntity.File_name;

                            BlobClient blobClient = containerClient.GetBlobClient(blobFilePath);

                            await blobClient.DeleteIfExistsAsync();

                            List<DbParameter> dbparamsBlobInfo = new List<DbParameter>();

                            dbparamsBlobInfo.Add(new SqlParameter { ParameterName = "@blob_id", Value = blobEntity.Blob_id == null ? DBNull.Value : (object)blobEntity.Blob_id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });

                            dbparamsBlobInfo.Add(new SqlParameter { ParameterName = "@title", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });

                            dbparamsBlobInfo.Add(new SqlParameter { ParameterName = "@description", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });

                            dbparamsBlobInfo.Add(new SqlParameter { ParameterName = "@file_type", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });

                            dbparamsBlobInfo.Add(new SqlParameter { ParameterName = "@file_name", Value = string.IsNullOrEmpty(blobEntity.File_name) ? DBNull.Value : (object)blobEntity.File_name, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });

                            dbparamsBlobInfo.Add(new SqlParameter { ParameterName = "@file_location", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });

                            dbparamsBlobInfo.Add(new SqlParameter { ParameterName = "@file_status", Value = string.IsNullOrEmpty(blobEntity.File_status) ? DBNull.Value : (object)blobEntity.File_status, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });

                            dbparamsBlobInfo.Add(new SqlParameter { ParameterName = "@recp_group_id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });

                            dbparamsBlobInfo.Add(new SqlParameter { ParameterName = "@uploaded_datetime", Value = DBNull.Value, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });

                            dbparamsBlobInfo.Add(new SqlParameter { ParameterName = "@uploaded_user_id", Value = DBNull.Value, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });

                            dbparamsBlobInfo.Add(new SqlParameter { ParameterName = "@deleted_user_id", Value = blobEntity.Deleted_user_id == null ? DBNull.Value : (object)blobEntity.Deleted_user_id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });

                            dbparamsBlobInfo.Add(new SqlParameter { ParameterName = "@deleted_datetime", Value = blobEntity.Deleted_datetime == null ? DBNull.Value : (object)blobEntity.Deleted_datetime, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });

                            dbparamsBlobInfo.Add(new SqlParameter { ParameterName = "@response_status", SqlDbType = SqlDbType.NVarChar, Size = 200, Direction = ParameterDirection.Output });

                            Dictionary<string, dynamic> result = SqlHelper.ExecuteNonQuery<SqlConnection>("usp_blob_master", dbparamsBlobInfo, SqlHelper.ExecutionType.Procedure);

                            insertRowsCount += result["RowsAffected"];

                            string spOut = DBNull.Value.Equals(result["@response_status"]) ? string.Empty : result["@response_status"];

                            if (!string.IsNullOrEmpty(spOut))
                            {
                                foreach (var keyvaluepair in spOut.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries))
                                {
                                    string[] splitteddata = keyvaluepair.Split(new string[] { ":" }, StringSplitOptions.RemoveEmptyEntries);
                                    if (splitteddata[0].Trim().Equals("Status"))
                                    {
                                        response.Status = splitteddata[1];
                                    }
                                    else if (splitteddata[0].Trim().Equals("Reason"))
                                    {
                                        response.Reason = splitteddata[1];
                                    }
                                    else if (splitteddata[0].Trim().Equals("File_Name"))
                                    {
                                        response.File_Name = splitteddata[1];
                                    }
                                }
                            }
                            else
                            {
                                response.Status = "Failed";
                                response.Reason = "SomeErrorOccured";
                                response.File_Name = blobEntity.File_name;
                            }
                        }
                    }
                }
                if (!isValidate)
                {
                    response.Status = "ValidationFailed";
                    response.Reason = "SomeErrorOccured";
                    response.File_Name = string.Empty;
                }

                return this.Ok(response);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                response.Status = "Failed";
                response.Reason = "SomeErrorOccured";
                response.File_Name = blobEntity.File_name;
                return this.NotFound(response);
            }
        }
    }
}
