//------------------------------------------------------------------------------
// <copyright file="SoilConservationRepository.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using ADIDAS.Model.DTO;
using ADIDAS.Model.Entities;
using ADIDAS.Repository.Helper;
using ADIDAS.Repository.Interfaces;
using Azure;
using Azure.Storage.Blobs;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using static Google.Apis.Requests.BatchRequest;

namespace ADIDAS.Repository.DataRepository
{
    /// <summary>
    /// SoilConservationRepository.
    /// </summary>
    public class SoilConservationRepository : BaseRepository, ISoilConservationRepository
    {
        /// <summary>
        /// IOptions
        /// </summary>
        private readonly IOptions<DBSettings> options;

        /// <summary>
        /// blobconfig
        /// </summary>
        private readonly IOptions<BlobConfig> blobconfig;

        /// <summary>
        /// Initializes a new instance of the <see cref="SoilConservationRepository"/> class.
        /// </summary>
        /// <param name="config">config</param>
        /// <param name="blobconfig">blobconfig</param>
        /// <param name="options">options</param>
        public SoilConservationRepository(IConfiguration config, IOptions<BlobConfig> blobconfig, IOptions<DBSettings> options)
            : base(config)
        {
            this.options = options;
            this.blobconfig = blobconfig;
            SqlHelper.SetConnectionString(this.options.Value.ConnectionString);
        }

        /// <summary>
        /// Get Yojna Number List.
        /// </summary>
        /// <returns> YojnaNumberList.</returns>
        public List<SoilConservationYojnaNumberResponse> GetYojnaNumberList()
        {
            List<SoilConservationYojnaNumberResponse> result = null;
            List<DbParameter> parameters = new List<DbParameter>();
            parameters.Add(new SqlParameter { ParameterName = "@query_name", Value = "GetYojnaNumberList", SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@registration_no", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@panchayat_id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>("usp_getdata_Soil_Conservation", parameters, SqlHelper.ExecutionType.Procedure);
            if (dt != null && dt.Rows.Count > 0)
            {
                result = (from DataRow row in dt.Rows
                          select new SoilConservationYojnaNumberResponse()
                          {
                              Yojana_Number_id = Convert.ToInt32("0"),

                              Yojana_number = row["yojana_number"].ToString()
                          }).ToList();
            }

            return result;
        }

        /// <summary>
        /// Get Yojna Name List.
        /// </summary>
        /// <returns> YojnaNameList.</returns>
        public List<SoilConservationYojnaNameResponse> GetYojnaNameList()
        {
            List<SoilConservationYojnaNameResponse> result = null;
            List<DbParameter> parameters = new List<DbParameter>();
            parameters.Add(new SqlParameter { ParameterName = "@query_name", Value = "GetYojnaNameList", SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@registration_no", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@panchayat_id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>("usp_getdata_Soil_Conservation", parameters, SqlHelper.ExecutionType.Procedure);
            if (dt != null && dt.Rows.Count > 0)
            {
                result = (from DataRow row in dt.Rows
                          select new SoilConservationYojnaNameResponse()
                          {
                              Yojana_Name_id = Convert.ToInt32("0"),
                              Yojana_name = row["yojana_name"].ToString()
                          }).ToList();
            }

            return result;
        }



        /// <summary>
        /// Get Structure Type Soil Conservation.
        /// </summary>
        /// <returns> Structure Type Soil Conservation Info.</returns>
        public SoilConservationStructureType GetStructureTypeSoilConservation()
        {
            SoilConservationStructureType result = null;
            List<DbParameter> parameters = new List<DbParameter>();
            parameters.Add(new SqlParameter { ParameterName = "@query_name", Value = "GetStructureTypeSoilConservation", SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@registration_no", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@panchayat_id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@scheme_id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>("usp_getdata_Soil_Conservation", parameters, SqlHelper.ExecutionType.Procedure);
            if (dt != null && dt.Rows.Count > 0)
            {
                result = SqlHelper.ConvertDataTableToList<SoilConservationStructureType>(dt)[0];

                result.CommonList = (from DataRow dr in dt.Rows
                                     where dr["Type"].ToString() == "commonList"
                                     group dr by new
                                     {
                                         Structure_Id = dr["structure_id"] == DBNull.Value ? (int?)null : (int)dr["structure_id"],
                                         Structure_Name = dr["structure_name"] == DBNull.Value ? null : dr["structure_name"].ToString(),
                                     } into commonlist
                                     select new CommonList
                                     {
                                         Structure_Id = commonlist.Key.Structure_Id,
                                         Structure_Name = commonlist.Key.Structure_Name,
                                     }).ToList();
                result.EntryPointList = (from DataRow dr in dt.Rows
                                         where dr["Type"].ToString() == "entryPointList"

                                         group dr by new
                                         {
                                             Structure_Id = dr["structure_id"] == DBNull.Value ? (int?)null : (int)dr["structure_id"],
                                             Structure_Name = dr["structure_name"] == DBNull.Value ? null : dr["structure_name"].ToString(),
                                         }
                                         into commonlist
                                         select new EntryPointList
                                         {
                                             Structure_Id = commonlist.Key.Structure_Id,
                                             Structure_Name = commonlist.Key.Structure_Name,
                                         }).ToList();
            }

            return result;
        }
        /// <summary>
        /// Get Plant Soil Conservation.
        /// </summary>
        /// <returns> Plant Soil Conservation Info. </returns>
        public List<SoilConservationPlantResponse> GetPlantSoilConservation()
        {
            List<SoilConservationPlantResponse> result = null;
            List<DbParameter> parameters = new List<DbParameter>();
            parameters.Add(new SqlParameter { ParameterName = "@query_name", Value = "GetPlantSoilConservation", SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@registration_no", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@panchayat_id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@scheme_id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>("usp_getdata_Soil_Conservation", parameters, SqlHelper.ExecutionType.Procedure);
            if (dt != null && dt.Rows.Count > 0)
            {
                result = SqlHelper.ConvertDataTableToList<SoilConservationPlantResponse>(dt);
            }

            return result;
        }

        /// <summary>
        /// Get Crop Soil Conservation.
        /// </summary>
        /// <returns> Crop Soil Conservation Info </returns>
        public List<SoilConservationCropResponse> GetCropSoilConservation()
        {
            List<SoilConservationCropResponse> result = null;
            List<DbParameter> parameters = new List<DbParameter>();
            parameters.Add(new SqlParameter { ParameterName = "@query_name", Value = "GetCropSoilConservation", SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@registration_no", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@panchayat_id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@scheme_id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>("usp_getdata_Soil_Conservation", parameters, SqlHelper.ExecutionType.Procedure);
            if (dt != null && dt.Rows.Count > 0)
            {
                result = SqlHelper.ConvertDataTableToList<SoilConservationCropResponse>(dt);
            }

            return result;
        }

        /// <summary>
        /// Get Dbt Mobile Number
        /// </summary>
        /// <returns> Dbt Mobile Number Details</returns>
        public List<SoilConservationDtbNumberResponse> GetDbtMobileNumber(int registrationNumber)
        {
            List<SoilConservationDtbNumberResponse> result = null;
            List<DbParameter> parameters = new List<DbParameter>();
            parameters.Add(new SqlParameter { ParameterName = "@query_name", Value = "GetDbtMobileNumber", SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>("INSERTspHERE", parameters, SqlHelper.ExecutionType.Procedure);
            if (dt != null && dt.Rows.Count > 0)
            {
                result = SqlHelper.ConvertDataTableToList<List<SoilConservationDtbNumberResponse>>(dt)[0];
            }

            return result;
        }

        /// <summary>
        /// Get Input Type Soil Conservation.
        /// </summary>
        /// <returns> InputType Soil Conservation Info. </returns>
        public List<SoilConservationInputTypeResponse> GetInputTypeSoilConservation()
        {
            List<SoilConservationInputTypeResponse> result = null;
            List<DbParameter> parameters = new List<DbParameter>();
            parameters.Add(new SqlParameter { ParameterName = "@query_name", Value = "GetInputTypeSoilConservation", SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@registration_no", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@panchayat_id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@scheme_id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>("usp_getdata_Soil_Conservation", parameters, SqlHelper.ExecutionType.Procedure);
            if (dt != null && dt.Rows.Count > 0)
            {
                result = SqlHelper.ConvertDataTableToList<SoilConservationInputTypeResponse>(dt);
            }

            return result;
        }

        /// <summary>
        /// Get Topic Name Soil Conservation.
        /// </summary>
        /// <returns> Topic Name Soil Conservation Info. </returns>
        public List<SoilConservationTopicName> GetTopicNameSoilConservation()
        {
            List<SoilConservationTopicName> result = null;
            List<DbParameter> parameters = new List<DbParameter>();
            parameters.Add(new SqlParameter { ParameterName = "@query_name", Value = "GetTopicNameSoilConservation", SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@registration_no", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@panchayat_id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@scheme_id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>("usp_getdata_Soil_Conservation", parameters, SqlHelper.ExecutionType.Procedure);
            if (dt != null && dt.Rows.Count > 0)
            {
                result = (from DataRow row in dt.Rows
                          select new SoilConservationTopicName()
                          {
                              Topic_id = Convert.ToInt32("0"),

                              Topic_name = row["topic_name"].ToString()
                          }).ToList();
            }

            return result;
        }

        /// <summary>
        /// Get Activity SubActivity Details.
        /// </summary>
        /// <returns> Activity SubActivity List.</returns>
        public List<SoilConservationActivityDetails> GetActivitySubActivityDetails()
        {
            List<SoilConservationActivityDetails> result = new List<SoilConservationActivityDetails>();
            List<DbParameter> parameters = new List<DbParameter>();
            parameters.Add(new SqlParameter { ParameterName = "@query_name", Value = "GetActivitySubActivityDetails", SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@registration_no", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@panchayat_id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@scheme_id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>("usp_getdata_Soil_Conservation", parameters, SqlHelper.ExecutionType.Procedure);
            if (dt != null && dt.Rows.Count > 0)
            {
                result = this.PopulateActivitySubActivityDetails(dt);
                DateTime date = DateTime.Now;
                var tempYear = date.Month >= 4 ? date.Year : date.Year - 1;
                var nextYear = tempYear + 1;
                result[0].Current_year = tempYear.ToString() + "-" + nextYear.ToString();
            }

            return result;
        }

        /// <summary>
        /// Get Soil Conservation Submitted Data.
        /// </summary>
        /// <param name="scheme_id">scheme_id</param>
        /// <param name="panchayat_id">panchayat_id</param>
        /// <returns> Soil Conservation Submitted Data Details.</returns>
        public List<SoilConservationSubmittedDataResponse> GetSoilConservationSubmittedData(int panchayat_id, int scheme_id)
        {
            List<SoilConservationSubmittedDataResponse> result = null;
            List<DbParameter> parameters = new List<DbParameter>();
            parameters.Add(new SqlParameter { ParameterName = "@query_name", Value = "GetSoilConservationSubmittedData", SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@registration_no", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@panchayat_id", Value = panchayat_id, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@scheme_id", Value = scheme_id, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>("usp_getdata_Soil_Conservation", parameters, SqlHelper.ExecutionType.Procedure);
            if (dt != null && dt.Rows.Count > 0)
            {
                result = PopulateSubmittedData(dt);
                result[0].Activity_list[0].Over_all_count = 0;
                result[0].Activity_list[0].Sub_activity_list[0].Sub_activity_count = 0;
            }

            return result;
        }

        /// <summary>
        /// Get Soil Conservation Notification.
        /// </summary>
        /// <param name="panchayat_id">panchayat_id</param>
        /// <returns>Get Soil Conservation Notification Details.</returns>
        public List<SoilConservationNotificationResponse> GetSoilConservationNotification(string panchayat_id)
        {
            {
                List<SoilConservationNotificationResponse> result = null;
                List<DbParameter> parameters = new List<DbParameter>();
                parameters.Add(new SqlParameter { ParameterName = "@query_name", Value = "GetSoilConservationNotification", SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                parameters.Add(new SqlParameter { ParameterName = "@registration_no", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                parameters.Add(new SqlParameter { ParameterName = "@panchayat_id", Value = panchayat_id, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                parameters.Add(new SqlParameter { ParameterName = "@scheme_id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });

                DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>("usp_getdata_Soil_Conservation", parameters, SqlHelper.ExecutionType.Procedure);
                if (dt != null && dt.Rows.Count > 0)
                {
                    result = SqlHelper.ConvertDataTableToList<SoilConservationNotificationResponse>(dt);
                }

                return result;
            }
        }

        /// <summary>
        /// GetExistingPhysicalFinancialTarget.
        /// </summary>
        /// <param name="panchayat_id">panchayat_id</param>
        /// <returns>Get Existing Physical Financial Target.</returns>
        public List<SoilConservationExistingPhysicalFinancialTarget> GetExistingPhysicalFinancialTarget(int panchayat_id)
        {
            List<SoilConservationExistingPhysicalFinancialTarget> result = null;
            List<DbParameter> parameters = new List<DbParameter>();
            parameters.Add(new SqlParameter { ParameterName = "@query_name", Value = "GetExistingPhysicalFinancialTarget", SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@registration_no", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@panchayat_id", Value = panchayat_id, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@scheme_id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });

            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>("usp_getdata_Soil_Conservation", parameters, SqlHelper.ExecutionType.Procedure);
            if (dt != null && dt.Rows.Count > 0)
            {
                result = this.PopulateSoilConservationExistingPhysicalFinancialTarget(dt);
            }

            return result;
        }

        /// <summary>
        /// Get Post Soil Conservation.
        /// </summary>
        /// <param name="PostSoilConservation">PostSoilConservation</param>
        /// <returns> Post Soil Conservation Result. </returns>
        public bool PostSoilConservation(List<SoilConservationCreateRequest> PostSoilConservation)
        {
            int insertRowsCount = 0;
             string blobName = DateTime.Now.ToString("yyyyMMddHHmmss");

            Dictionary<string, dynamic> result = new Dictionary<string, dynamic>();

            List<DbParameter> dbparamsUserInfo = new List<DbParameter>();

            BlobServiceClient blobServiceClient = new BlobServiceClient(this.blobconfig.Value.BlobConnection);

            if (!string.IsNullOrEmpty(PostSoilConservation[0].Pre_construction_image_data))
            {
                BlobEntity blobEntity = new BlobEntity();
                blobEntity.DirectoryName = "Soil_Conservation_Photos";
                blobEntity.FolderName = blobName + "-" + "pre_construction_image_data" + ".jpg";
                blobEntity.ByteArray = PostSoilConservation[0].Pre_construction_image_data;

                BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient("mobileapp");

                string blobPath = blobEntity.DirectoryName + "/" + blobEntity.FolderName;

                BlobClient blobClient = containerClient.GetBlobClient(blobPath);

                Byte[] bytes1 = Convert.FromBase64String(blobEntity.ByteArray);
                Stream stream = new MemoryStream(bytes1);

                var response = blobClient.UploadAsync(stream, true);

                PostSoilConservation[0].Pre_construction_image_location = this.blobconfig.Value.BlobSoilConservationPhoto;
                PostSoilConservation[0].Pre_construction_image_name = blobEntity.FolderName;

            }

            if (!string.IsNullOrEmpty(PostSoilConservation[0].During_construction_image_data))
            {
                BlobEntity blobEntity = new BlobEntity();
                blobEntity.DirectoryName = "Soil_Conservation_Photos";
                blobEntity.FolderName = blobName + "-" + "during_construction_image_data" + ".jpg";
                blobEntity.ByteArray = PostSoilConservation[0].During_construction_image_data;

                BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient("mobileapp");

                string blobPath = blobEntity.DirectoryName + "/" + blobEntity.FolderName;

                BlobClient blobClient = containerClient.GetBlobClient(blobPath);

                Byte[] bytes1 = Convert.FromBase64String(blobEntity.ByteArray);
                Stream stream = new MemoryStream(bytes1);

                var response = blobClient.UploadAsync(stream, true);

                PostSoilConservation[0].During_construction_image_location = this.blobconfig.Value.BlobSoilConservationPhoto;
                PostSoilConservation[0].During_construction_image_name = blobEntity.FolderName;

            }

            if (!string.IsNullOrEmpty(PostSoilConservation[0].Post_construction_image_data))
            {
                BlobEntity blobEntity = new BlobEntity();
                blobEntity.DirectoryName = "Soil_Conservation_Photos";
                blobEntity.FolderName = blobName + "-" + "post_construction_image_data" + ".jpg";
                blobEntity.ByteArray = PostSoilConservation[0].Post_construction_image_data;

                BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient("mobileapp");

                string blobPath = blobEntity.DirectoryName + "/" + blobEntity.FolderName;

                BlobClient blobClient = containerClient.GetBlobClient(blobPath);

                Byte[] bytes1 = Convert.FromBase64String(blobEntity.ByteArray);
                Stream stream = new MemoryStream(bytes1);

                var response = blobClient.UploadAsync(stream, true);

                PostSoilConservation[0].Post_construction_image_location = this.blobconfig.Value.BlobSoilConservationPhoto;
                PostSoilConservation[0].Post_construction_image_name = blobEntity.FolderName;

            }

            if (!string.IsNullOrEmpty(PostSoilConservation[0].Image_data))
            {
                BlobEntity blobEntity = new BlobEntity();
                blobEntity.DirectoryName = "Soil_Conservation_Photos";
                blobEntity.FolderName = blobName + "-" + "image_data" + ".jpg";
                blobEntity.ByteArray = PostSoilConservation[0].Image_data;

                BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient("mobileapp");

                string blobPath = blobEntity.DirectoryName + "/" + blobEntity.FolderName;

                BlobClient blobClient = containerClient.GetBlobClient(blobPath);

                Byte[] bytes1 = Convert.FromBase64String(blobEntity.ByteArray);
                Stream stream = new MemoryStream(bytes1);

                var response = blobClient.UploadAsync(stream, true);

                PostSoilConservation[0].Image_location = this.blobconfig.Value.BlobSoilConservationPhoto;
                PostSoilConservation[0].Image_name = blobEntity.FolderName;

            }

            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@panchayat_id", Value = PostSoilConservation[0].Panchayat_id, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@scheme_id", Value = PostSoilConservation[0].Scheme_id, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@activity_id", Value = PostSoilConservation[0].Activity_id, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@is_activity_submitted", Value = PostSoilConservation[0].Is_activity_submitted == true ? "Y" : "N", SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@is_subActivity_submitted", Value = PostSoilConservation[0].Is_subActivity_submitted == true ? "Y" : "N", SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@sub_activity_id", Value = PostSoilConservation[0].Sub_activity_id.ToString() == null ? DBNull.Value : (object)PostSoilConservation[0].Sub_activity_id, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@structure_id", Value = PostSoilConservation[0].Structure_id.ToString() == null ? DBNull.Value : (object)PostSoilConservation[0].Structure_id, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@is_final_submission", Value = PostSoilConservation[0].Is_final_submission == true ? "Y" : "N", SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@yojana_number", Value = PostSoilConservation[0].Yojana_number, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@yojana_name", Value = PostSoilConservation[0].Yojana_name, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@physical_target", Value = PostSoilConservation[0].Physical_target.ToString() == null ? DBNull.Value : (object)PostSoilConservation[0].Physical_target, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@financial_target", Value = PostSoilConservation[0].Financial_target.ToString() == null ? DBNull.Value : (object)PostSoilConservation[0].Financial_target, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@current_year", Value = PostSoilConservation[0].Current_year.ToString() == null ? DBNull.Value : (object)PostSoilConservation[0].Current_year, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@registration_no", Value = PostSoilConservation[0].Registration_no == null ? DBNull.Value : (object)PostSoilConservation[0].Registration_no, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@dbt_number", Value = PostSoilConservation[0].Dbt_number == null ? DBNull.Value : (object)PostSoilConservation[0].Dbt_number, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@mobile_number", Value = PostSoilConservation[0].Mobile_number == null ? DBNull.Value : (object)PostSoilConservation[0].Mobile_number, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@beneficiary_name", Value = PostSoilConservation[0].Beneficiary_name == null ? DBNull.Value : (object)PostSoilConservation[0].Beneficiary_name, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@name_Of_Village", Value = PostSoilConservation[0].Name_Of_Village == null ? DBNull.Value : (object)PostSoilConservation[0].Name_Of_Village, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@name_Of_district", Value = PostSoilConservation[0].Name_Of_district == null ? DBNull.Value : (object)PostSoilConservation[0].Name_Of_district, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@name_Of_panchayat", Value = PostSoilConservation[0].Name_Of_panchayat == null ? DBNull.Value : (object)PostSoilConservation[0].Name_Of_panchayat, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@name_Of_block", Value = PostSoilConservation[0].Name_Of_block == null ? DBNull.Value : (object)PostSoilConservation[0].Name_Of_block, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@date_of_starting", Value = PostSoilConservation[0].Date_of_starting == null ? DBNull.Value : (object)PostSoilConservation[0].Date_of_starting, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@due_date_of_completion", Value = PostSoilConservation[0].Due_date_of_completion == null ? DBNull.Value : (object)PostSoilConservation[0].Due_date_of_completion, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@estimated_budget", Value = PostSoilConservation[0].Estimated_budget.ToString() == null ? DBNull.Value : (object)PostSoilConservation[0].Estimated_budget, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@project_irrigated_area", Value = PostSoilConservation[0].Project_irrigated_area.ToString() == null ? DBNull.Value : (object)PostSoilConservation[0].Project_irrigated_area, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@pre_construction_image_data", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@pre_construction_image_location", Value = PostSoilConservation[0].Pre_construction_image_location == null ? DBNull.Value : (object)PostSoilConservation[0].Pre_construction_image_location, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@pre_construction_image_name", Value = PostSoilConservation[0].Pre_construction_image_name == null ? DBNull.Value : (object)PostSoilConservation[0].Pre_construction_image_name, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@pre_construction_latitude", Value = PostSoilConservation[0].Pre_construction_latitude == null ? DBNull.Value : (object)PostSoilConservation[0].Pre_construction_latitude, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@pre_construction_longtitude", Value = PostSoilConservation[0].Pre_construction_longtitude == null ? DBNull.Value : (object)PostSoilConservation[0].Pre_construction_longtitude, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@during_construction_image_data", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@during_construction_image_location", Value = PostSoilConservation[0].During_construction_image_location == null ? DBNull.Value : (object)PostSoilConservation[0].During_construction_image_location, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@during_construction_image_name", Value = PostSoilConservation[0].During_construction_image_name == null ? DBNull.Value : (object)PostSoilConservation[0].During_construction_image_name, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@during_construction_latitude", Value = PostSoilConservation[0].During_construction_latitude == null ? DBNull.Value : (object)PostSoilConservation[0].During_construction_latitude, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@during_construction_longtitude", Value = PostSoilConservation[0].During_construction_longtitude == null ? DBNull.Value : (object)PostSoilConservation[0].During_construction_latitude, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@post_construction_image_data", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@post_construction_image_location", Value = PostSoilConservation[0].Post_construction_image_location == null ? DBNull.Value : (object)PostSoilConservation[0].Post_construction_image_location, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@post_construction_image_name", Value = PostSoilConservation[0].Post_construction_image_name == null ? DBNull.Value : (object)PostSoilConservation[0].Post_construction_image_name, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@pos_construction_latitude", Value = PostSoilConservation[0].Pos_construction_latitude == null ? DBNull.Value : (object)PostSoilConservation[0].Pos_construction_latitude, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@post_construction_longtitude", Value = PostSoilConservation[0].Post_construction_longtitude == null ? DBNull.Value : (object)PostSoilConservation[0].Post_construction_longtitude, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@actual_date_of_completion", Value = PostSoilConservation[0].Actual_date_of_completion == null ? DBNull.Value : (object)PostSoilConservation[0].Actual_date_of_completion, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@actual_expenditure", Value = PostSoilConservation[0].Actual_expenditure.ToString() == null ? DBNull.Value : (object)PostSoilConservation[0].Actual_expenditure, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@actual_irrigated_area", Value = PostSoilConservation[0].Actual_irrigated_area.ToString() == null ? DBNull.Value : (object)PostSoilConservation[0].Actual_irrigated_area, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@youtube_story_link", Value = PostSoilConservation[0].Youtube_story_link == null ? DBNull.Value : (object)PostSoilConservation[0].Youtube_story_link, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@date_Of_Activity", Value = PostSoilConservation[0].Date_Of_Activity == null ? DBNull.Value : (object)PostSoilConservation[0].Date_Of_Activity, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@image_data", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@image_location", Value = PostSoilConservation[0].Image_location == null ? DBNull.Value : (object)PostSoilConservation[0].Image_location, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@image_name", Value = PostSoilConservation[0].Image_name == null ? DBNull.Value : (object)PostSoilConservation[0].Image_name, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@latitude", Value = PostSoilConservation[0].Latitude == null ? DBNull.Value : (object)PostSoilConservation[0].Latitude, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@longitutde", Value = PostSoilConservation[0].Longitutde == null ? DBNull.Value : (object)PostSoilConservation[0].Longitutde, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@topic_name", Value = PostSoilConservation[0].Topic_name == null ? DBNull.Value : (object)PostSoilConservation[0].Topic_name, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@totalCoveredArea", Value = PostSoilConservation[0].TotalCoveredArea.ToString() == null ? DBNull.Value : (object)PostSoilConservation[0].TotalCoveredArea, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@plant_id", Value = PostSoilConservation[0].Plant_id.ToString() == null ? DBNull.Value : (object)PostSoilConservation[0].Plant_id, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@plant_survived_one_year", Value = PostSoilConservation[0].Plant_survived_one_year.ToString() == null ? DBNull.Value : (object)PostSoilConservation[0].Plant_survived_one_year, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@plant_survived_two_year", Value = PostSoilConservation[0].Plant_survived_two_year.ToString() == null ? DBNull.Value : (object)PostSoilConservation[0].Plant_survived_two_year, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@no_Of_Plants", Value = PostSoilConservation[0].No_Of_Plants.ToString() == null ? DBNull.Value : (object)PostSoilConservation[0].No_Of_Plants, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@mushroom_productivity", Value = PostSoilConservation[0].Mushroom_productivity.ToString() == null ? DBNull.Value : (object)PostSoilConservation[0].Mushroom_productivity, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@crop_productivity", Value = PostSoilConservation[0].Crop_productivity.ToString() == null ? DBNull.Value : (object)PostSoilConservation[0].Crop_productivity, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@crop_id", Value = PostSoilConservation[0].Crop_id.ToString() == null ? DBNull.Value : (object)PostSoilConservation[0].Crop_id, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@input_type_id", Value = PostSoilConservation[0].Input_type_id.ToString() == null ? DBNull.Value : (object)PostSoilConservation[0].Input_type_id, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@input_quantity", Value = PostSoilConservation[0].Input_quantity == null ? DBNull.Value : (object)PostSoilConservation[0].Input_quantity, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@male_sc", Value = PostSoilConservation[0].Male.Sc.ToString() == null ? DBNull.Value : (object)PostSoilConservation[0].Male.Sc, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@male_st", Value = PostSoilConservation[0].Male.St.ToString() == null ? DBNull.Value : (object)PostSoilConservation[0].Male.St, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@male_general", Value = PostSoilConservation[0].Male.General.ToString() == null ? DBNull.Value : (object)PostSoilConservation[0].Male.General, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@male_bc", Value = PostSoilConservation[0].Male.Bc.ToString() == null ? DBNull.Value : (object)PostSoilConservation[0].Male.Bc, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@male_ebc", Value = PostSoilConservation[0].Male.Ebc.ToString() == null ? DBNull.Value : (object)PostSoilConservation[0].Male.Ebc, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@male_minority", Value = PostSoilConservation[0].Male.Minority.ToString() == null ? DBNull.Value : (object)PostSoilConservation[0].Male.Minority, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@female_sc", Value = PostSoilConservation[0].Female.Sc.ToString() == null ? DBNull.Value : (object)PostSoilConservation[0].Female.Sc, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@female_st", Value = PostSoilConservation[0].Female.St.ToString() == null ? DBNull.Value : (object)PostSoilConservation[0].Female.St, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@female_general", Value = PostSoilConservation[0].Female.General.ToString() == null ? DBNull.Value : (object)PostSoilConservation[0].Female.General, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@female_bc", Value = PostSoilConservation[0].Female.Bc.ToString() == null ? DBNull.Value : (object)PostSoilConservation[0].Female.Bc, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@female_ebc", Value = PostSoilConservation[0].Female.Ebc.ToString() == null ? DBNull.Value : (object)PostSoilConservation[0].Female.Ebc, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@female_minority", Value = PostSoilConservation[0].Female.Minority.ToString() == null ? DBNull.Value : (object)PostSoilConservation[0].Female.Minority, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@others_sc", Value = PostSoilConservation[0].Others.Sc.ToString() == null ? DBNull.Value : (object)PostSoilConservation[0].Others.Sc, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@others_st", Value = PostSoilConservation[0].Others.St.ToString() == null ? DBNull.Value : (object)PostSoilConservation[0].Others.St, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@others_general", Value = PostSoilConservation[0].Others.General.ToString() == null ? DBNull.Value : (object)PostSoilConservation[0].Others.General, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@others_bc", Value = PostSoilConservation[0].Others.Bc.ToString() == null ? DBNull.Value : (object)PostSoilConservation[0].Others.Bc, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@others_ebc", Value = PostSoilConservation[0].Others.Ebc.ToString() == null ? DBNull.Value : (object)PostSoilConservation[0].Others.Ebc, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@rec_created_userid", Value = PostSoilConservation[0].Rec_created_userid.ToString() == null ? DBNull.Value : (object)PostSoilConservation[0].Rec_created_userid, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@rec_updated_userid", Value = PostSoilConservation[0].Rec_updated_userid.ToString() == null ? DBNull.Value : (object)PostSoilConservation[0].Rec_updated_userid, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@recCreatedDate", Value = PostSoilConservation[0].RecCreatedDate == null ? DBNull.Value : (object)PostSoilConservation[0].RecCreatedDate, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@recUpdatedDate", Value = PostSoilConservation[0].RecUpdatedDate == null ? DBNull.Value : (object)PostSoilConservation[0].RecUpdatedDate, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@others_minority", Value = PostSoilConservation[0].Others.Minority.ToString() == null ? DBNull.Value : (object)PostSoilConservation[0].Others.Minority, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });


            if (PostSoilConservation[0].IsRegisterNumberVerified == true)
            {
                dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@isRegisterNumberVerified", Value = PostSoilConservation[0].IsRegisterNumberVerified == true ? "Y" : "N", SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@isRegisterNumberValid", Value = PostSoilConservation[0].IsRegisterNumberValid == true ? "Y" : "N", SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            }
            else
            {
                dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@isRegisterNumberVerified", Value = PostSoilConservation[0].IsRegisterNumberVerified == true ? "Y" : "N", SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@isRegisterNumberValid", Value = PostSoilConservation[0].IsRegisterNumberValid == true ? "N" : "Y", SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            }

            result = SqlHelper.ExecuteNonQuery<SqlConnection>("USP_Insert_soil_conservation", dbparamsUserInfo, SqlHelper.ExecutionType.Procedure);

            insertRowsCount = insertRowsCount + result["RowsAffected"];

            if (insertRowsCount > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Populate Submitted Data.
        /// </summary>
        /// <returns>Submitted Data Details.</returns>
        private List<SoilConservationSubmittedDataResponse> PopulateSubmittedData(DataTable dt)
        {
            DateTime date = DateTime.Now;
            var tempYear = date.Month >= 4 ? date.Year : date.Year - 1;
            var nextYear = tempYear + 1;
            var currentFin_year = tempYear.ToString() + "-" + nextYear.ToString();
            List<SoilConservationSubmittedDataResponse> SubmittedDataDetails = new List<SoilConservationSubmittedDataResponse>();
            SubmittedDataDetails = (from DataRow dRow in dt.Rows
                                    group dRow by new
                                    {
                                        scheme_id = dRow["scheme_id"] == DBNull.Value ? (int?)null : (int)dRow["scheme_id"],
                                        scheme_name = dRow["scheme_name"] == DBNull.Value ? null : dRow["scheme_name"].ToString(),
                                        panchayat_id = dRow["panchayat_id"] == DBNull.Value ? (int?)null : (int)dRow["panchayat_id"],
                                        panchayat_name = dRow["panchayat_name"] == DBNull.Value ? null : dRow["panchayat_name"].ToString(),
                                    }
                                    into groupby
                                    select new SoilConservationSubmittedDataResponse
                                    {
                                        Scheme_id = groupby.Key.scheme_id,
                                        Scheme_name = groupby.Key.scheme_name,
                                        Panchayat_id = groupby.Key.panchayat_id,
                                        Panchayat_name = groupby.Key.panchayat_name

                                    }).ToList();
            SubmittedDataDetails.ForEach(a =>
            {
                a.Activity_list = (from DataRow dRow in dt.Rows
                                   where dRow["panchayat_id"].ToString() == a.Panchayat_id.ToString()
                                   group dRow by new
                                   {
                                       scheme_id = dRow["scheme_id"] == DBNull.Value ? (int?)null : (int)dRow["scheme_id"],
                                       scheme_name = dRow["scheme_name"] == DBNull.Value ? null : dRow["scheme_name"].ToString(),
                                       panchayat_id = dRow["panchayat_id"] == DBNull.Value ? (int?)null : (int)dRow["panchayat_id"],
                                       panchayat_name = dRow["panchayat_name"] == DBNull.Value ? null : dRow["panchayat_name"].ToString(),
                                       activity_id = dRow["activity_id"] == DBNull.Value ? (int?)null : (int)dRow["activity_id"],
                                       activity_name = dRow["activity_name"] == DBNull.Value ? null : dRow["activity_name"].ToString(),
                                       is_activity_submitted = dRow["is_activity_submitted"].ToString() == "Y" ? true : false,
                                   }
                                   into groupbyActivityList
                                   select new SubmittedActivityList
                                   {
                                       Scheme_id = groupbyActivityList.Key.scheme_id,
                                       Scheme_name = groupbyActivityList.Key.scheme_name,
                                       Panchayat_id = groupbyActivityList.Key.panchayat_id,
                                       Panchayat_name = groupbyActivityList.Key.panchayat_name,
                                       Activity_id = groupbyActivityList.Key.activity_id,
                                       Activity_name = groupbyActivityList.Key.activity_name,
                                       Is_activity_submitted = groupbyActivityList.Key.is_activity_submitted,

                                   }).ToList();

                a.Activity_list.ForEach(s =>
                {
                    s.Sub_activity_list = (from DataRow dRow in dt.Rows
                                           where dRow["scheme_id"].ToString() == a.Scheme_id.ToString()
                                           & dRow["activity_id"].ToString() == s.Activity_id.ToString()
                                           group dRow by new
                                           {
                                               sub_activity_id = dRow["sub_activity_id"] == DBNull.Value ? (int?)null : (int)dRow["sub_activity_id"],
                                               sub_activity_name = dRow["sub_activity_name"] == DBNull.Value ? null : dRow["sub_activity_name"].ToString(),
                                               is_subActivity_submitted = dRow["is_subActivity_submitted"].ToString() == "Y" ? true : false,
                                           }
                                           into groupbySubActivityList
                                           select new SubmittedSubActivityList
                                           {

                                               Sub_activity_id = groupbySubActivityList.Key.sub_activity_id,
                                               Sub_activity_name = groupbySubActivityList.Key.sub_activity_name,
                                               Is_subActivity_submitted = groupbySubActivityList.Key.is_subActivity_submitted,
                                           }).ToList();
                    s.Sub_activity_list.ForEach(b =>
                    {
                        b.Sub_activity_details = (from DataRow dRow in dt.Rows
                                                  where dRow["scheme_id"].ToString() == a.Scheme_id.ToString()
                                                  & dRow["activity_id"].ToString() == s.Activity_id.ToString()
                                                  & dRow["sub_activity_id"].ToString() == b.Sub_activity_id.ToString()
                                                  group dRow by new
                                                  {
                                                      is_final_submission = dRow["is_final_submission"].ToString() == "Y" ? true : false,
                                                      structure_id = dRow["structure_id"] == DBNull.Value ? (int?)null : (int)dRow["structure_id"],
                                                      structure_name = dRow["structure_name"] == DBNull.Value ? null : dRow["structure_name"].ToString(),
                                                      yojana_number = dRow["yojana_number"] == DBNull.Value ? null : dRow["yojana_number"].ToString(),
                                                      yojana_name = dRow["yojana_name"] == DBNull.Value ? null : dRow["yojana_name"].ToString(),
                                                      physical_target = dRow["physical_target"] == DBNull.Value ? (decimal?)null : (decimal)dRow["physical_target"],
                                                      financial_target = dRow["financial_target"] == DBNull.Value ? (decimal?)null : (decimal)dRow["financial_target"],
                                                      currentFin_year = dRow["current_year"] == DBNull.Value ? null : dRow["current_year"].ToString(),
                                                      registration_no = dRow["registration_no"] == DBNull.Value ? null : dRow["registration_no"].ToString(),
                                                      isRegisterNumberVerified = dRow["isRegisterNumberVerified"].ToString() == "Y" ? true : false,
                                                      isRegisterNumberValid = dRow["isRegisterNumberValid"].ToString() == "Y" ? true : false,
                                                      dbt_number = dRow["dbt_number"] == DBNull.Value ? null : dRow["dbt_number"].ToString(),
                                                      mobile_number = dRow["mobile_number"] == DBNull.Value ? null : dRow["mobile_number"].ToString(),
                                                      beneficiary_name = dRow["beneficiary_name"] == DBNull.Value ? null : dRow["beneficiary_name"].ToString(),
                                                      name_Of_Village = dRow["name_Of_Village"] == DBNull.Value ? null : dRow["name_Of_Village"].ToString(),
                                                      name_Of_district = dRow["name_Of_district"] == DBNull.Value ? null : dRow["name_Of_district"].ToString(),
                                                      name_Of_panchayat = dRow["name_Of_panchayat"] == DBNull.Value ? null : dRow["name_Of_panchayat"].ToString(),
                                                      name_Of_block = dRow["name_Of_block"] == DBNull.Value ? null : dRow["name_Of_block"].ToString(),
                                                      total_no_of_beneficiaries = dRow["total_no_of_beneficiaries"] == DBNull.Value ? (int?)null : (int)dRow["total_no_of_beneficiaries"],
                                                      sc_total = dRow["sc_total"] == DBNull.Value ? (int?)null : (int)dRow["sc_total"],
                                                      st_total = dRow["st_total"] == DBNull.Value ? (int?)null : (int)dRow["st_total"],
                                                      general_total = dRow["general_total"] == DBNull.Value ? (int?)null : (int)dRow["general_total"],
                                                      bc_total = dRow["bc_total"] == DBNull.Value ? (int?)null : (int)dRow["bc_total"],
                                                      ebc_total = dRow["ebc_total"] == DBNull.Value ? (int?)null : (int)dRow["ebc_total"],
                                                      minority_total = dRow["minority_total"] == DBNull.Value ? (int?)null : (int)dRow["minority_total"],
                                                      date_of_starting = dRow["date_of_starting"] == DBNull.Value ? (DateTime?)null : (DateTime)dRow["date_of_starting"],
                                                      due_date_of_completion = dRow["due_date_of_completion"] == DBNull.Value ? (DateTime?)null : (DateTime)dRow["due_date_of_completion"],
                                                      estimated_budget = dRow["estimated_budget"] == DBNull.Value ? (decimal?)null : (decimal)dRow["estimated_budget"],
                                                      project_irrigated_area = dRow["project_irrigated_area"] == DBNull.Value ? (decimal?)null : (decimal)dRow["project_irrigated_area"],
                                                      pre_construction_image_data = dRow["pre_construction_image_data"] == DBNull.Value ? null : dRow["pre_construction_image_data"].ToString(),
                                                      pre_construction_image_name = dRow["pre_construction_image_name"] == DBNull.Value ? null : dRow["pre_construction_image_name"].ToString(),
                                                      pre_construction_image_location = dRow["pre_construction_image_location"] == DBNull.Value ? null : dRow["pre_construction_image_location"].ToString(),
                                                      pre_construction_latitude = dRow["pre_construction_latitude"] == DBNull.Value ? null : dRow["pre_construction_latitude"].ToString(),
                                                      pre_construction_longtitude = dRow["pre_construction_longtitude"] == DBNull.Value ? null : dRow["pre_construction_longtitude"].ToString(),
                                                      during_construction_image_data = dRow["during_construction_image_data"] == DBNull.Value ? null : dRow["during_construction_image_data"].ToString(),
                                                      during_construction_image_location = dRow["during_construction_image_location"] == DBNull.Value ? null : dRow["during_construction_image_location"].ToString(),
                                                      during_construction_image_name = dRow["during_construction_image_name"] == DBNull.Value ? null : dRow["during_construction_image_name"].ToString(),
                                                      during_construction_latitude = dRow["during_construction_latitude"] == DBNull.Value ? null : dRow["during_construction_latitude"].ToString(),
                                                      during_construction_longtitude = dRow["during_construction_longtitude"] == DBNull.Value ? null : dRow["during_construction_longtitude"].ToString(),
                                                      post_construction_image_data = dRow["post_construction_image_data"] == DBNull.Value ? null : dRow["post_construction_image_data"].ToString(),
                                                      post_construction_image_location = dRow["post_construction_image_location"] == DBNull.Value ? null : dRow["post_construction_image_location"].ToString(),
                                                      post_construction_image_name = dRow["post_construction_image_name"] == DBNull.Value ? null : dRow["post_construction_image_name"].ToString(),
                                                      pos_construction_latitude = dRow["pos_construction_latitude"] == DBNull.Value ? null : dRow["pos_construction_latitude"].ToString(),
                                                      post_construction_longtitude = dRow["post_construction_longtitude"] == DBNull.Value ? null : dRow["post_construction_longtitude"].ToString(),
                                                      actual_date_of_completion = dRow["actual_date_of_completion"] == DBNull.Value ? (DateTime?)null : (DateTime)dRow["actual_date_of_completion"],
                                                      actual_expenditure = dRow["actual_expenditure"] == DBNull.Value ? (decimal?)null : (decimal)dRow["actual_expenditure"],
                                                      actual_irrigated_area = dRow["actual_irrigated_area"] == DBNull.Value ? (decimal?)null : (decimal)dRow["actual_irrigated_area"],
                                                      youtube_story_link = dRow["youtube_story_link"] == DBNull.Value ? null : dRow["youtube_story_link"].ToString(),
                                                      date_Of_Activity = dRow["date_Of_Activity"] == DBNull.Value ? (DateTime?)null : (DateTime)dRow["date_Of_Activity"],
                                                      image_data = dRow["image_data"] == DBNull.Value ? null : dRow["image_data"].ToString(),
                                                      image_location = dRow["image_location"] == DBNull.Value ? null : dRow["image_location"].ToString(),
                                                      image_name = dRow["image_name"] == DBNull.Value ? null : dRow["image_name"].ToString(),
                                                      latitude = dRow["latitude"] == DBNull.Value ? null : dRow["latitude"].ToString(),
                                                      longitutde = dRow["longitutde"] == DBNull.Value ? null : dRow["longitutde"].ToString(),
                                                      // topic_id = dRow["topic_id"] == DBNull.Value ? (int?) null : (int)dRow["topic_id"],

                                                      topic_name = dRow["topic_name"] == DBNull.Value ? null : dRow["topic_name"].ToString(),
                                                      totalCoveredArea = dRow["totalCoveredArea"] == DBNull.Value ? (decimal?)null : (decimal)dRow["totalCoveredArea"],
                                                      plant_id = dRow["plant_id"] == DBNull.Value ? (int?)null : (int)dRow["plant_id"],
                                                      plantType = dRow["Plant_name"] == DBNull.Value ? null : dRow["Plant_name"].ToString(),
                                                      plant_survived_one_year = dRow["plant_survived_one_year"] == DBNull.Value ? (int?)null : (int)dRow["plant_survived_one_year"],
                                                      plant_survived_two_year = dRow["plant_survived_two_year"] == DBNull.Value ? (int?)null : (int)dRow["plant_survived_two_year"],
                                                      no_Of_Plants = dRow["no_Of_Plants"] == DBNull.Value ? (int?)null : (int)dRow["no_Of_Plants"],
                                                      mushroom_productivity = dRow["mushroom_productivity"] == DBNull.Value ? (decimal?)null : (decimal)dRow["mushroom_productivity"],
                                                      crop_productivity = dRow["crop_productivity"] == DBNull.Value ? (decimal?)null : (decimal)dRow["crop_productivity"],
                                                      crop_id = dRow["crop_type_id"] == DBNull.Value ? (int?)null : (int)dRow["crop_type_id"],
                                                      crop_type = dRow["Crop_name"] == DBNull.Value ? null : dRow["Crop_name"].ToString(),
                                                      input_type_id = dRow["input_type_id"] == DBNull.Value ? (int?)null : (int)dRow["input_type_id"],
                                                      input_type_name = dRow["input_type_name"] == DBNull.Value ? null : dRow["input_type_name"].ToString(),
                                                      input_type_value = dRow["input_type_value"] == DBNull.Value ? null : dRow["input_type_value"].ToString(),
                                                      input_quantity = dRow["input_quantity"] == DBNull.Value ? null : dRow["input_quantity"].ToString(),
                                                      rec_created_userid = dRow["rec_created_userid"] == DBNull.Value ? null : dRow["rec_created_userid"].ToString(),
                                                      rec_updated_userid = dRow["rec_updated_userid"] == DBNull.Value ? null : dRow["rec_updated_userid"].ToString(),
                                                      recCreatedDate = dRow["rec_created_date"] == DBNull.Value ? null : dRow["rec_created_date"].ToString(),
                                                      recUpdatedDate = dRow["rec_updated_date"] == DBNull.Value ? null : dRow["rec_updated_date"].ToString(),
                                                  }
                                                  into groupbySubActivityDetails
                                                  select new SubActivityDetail
                                                  {
                                                      Is_final_submission = groupbySubActivityDetails.Key.is_final_submission,
                                                      Structure_id = groupbySubActivityDetails.Key.structure_id,
                                                      Structure_name = groupbySubActivityDetails.Key.structure_name,
                                                      Yojana_number = groupbySubActivityDetails.Key.yojana_number,
                                                      Yojana_name = groupbySubActivityDetails.Key.yojana_name,
                                                      Physical_target = groupbySubActivityDetails.Key.physical_target,
                                                      Financial_target = groupbySubActivityDetails.Key.financial_target,
                                                      Current_year = groupbySubActivityDetails.Key.currentFin_year,
                                                      Registration_no = groupbySubActivityDetails.Key.registration_no,
                                                      IsRegisterNumberVerified = groupbySubActivityDetails.Key.isRegisterNumberVerified,
                                                      IsRegisterNumberValid = groupbySubActivityDetails.Key.isRegisterNumberValid,
                                                      Dbt_number = groupbySubActivityDetails.Key.dbt_number,
                                                      Mobile_number = groupbySubActivityDetails.Key.mobile_number,
                                                      Beneficiary_name = groupbySubActivityDetails.Key.beneficiary_name,
                                                      Name_Of_Village = groupbySubActivityDetails.Key.name_Of_Village,
                                                      Name_Of_district = groupbySubActivityDetails.Key.name_Of_district,
                                                      Name_Of_panchayat = groupbySubActivityDetails.Key.name_Of_panchayat,
                                                      Name_Of_block = groupbySubActivityDetails.Key.name_Of_block,
                                                      Total_no_of_beneficiaries = groupbySubActivityDetails.Key.total_no_of_beneficiaries,
                                                      Sc_total = groupbySubActivityDetails.Key.sc_total,
                                                      St_total = groupbySubActivityDetails.Key.st_total,
                                                      General_total = groupbySubActivityDetails.Key.general_total,
                                                      Bc_total = groupbySubActivityDetails.Key.bc_total,
                                                      Ebc_total = groupbySubActivityDetails.Key.ebc_total,
                                                      Minority_total = groupbySubActivityDetails.Key.minority_total,
                                                      Date_of_starting = groupbySubActivityDetails.Key.date_of_starting,
                                                      Due_date_of_completion = groupbySubActivityDetails.Key.due_date_of_completion,
                                                      Estimated_budget = groupbySubActivityDetails.Key.estimated_budget,
                                                      Project_irrigated_area = groupbySubActivityDetails.Key.project_irrigated_area,
                                                      Pre_construction_image_data = groupbySubActivityDetails.Key.pre_construction_image_data,
                                                      Pre_construction_image_location = groupbySubActivityDetails.Key.pre_construction_image_location,
                                                      Pre_construction_image_name = groupbySubActivityDetails.Key.pre_construction_image_name,
                                                      Pre_construction_latitude = groupbySubActivityDetails.Key.pre_construction_latitude,
                                                      Pre_construction_longtitude = groupbySubActivityDetails.Key.pre_construction_longtitude,
                                                      During_construction_image_data = groupbySubActivityDetails.Key.during_construction_image_data,
                                                      During_construction_image_location = groupbySubActivityDetails.Key.during_construction_image_location,
                                                      During_construction_image_name = groupbySubActivityDetails.Key.during_construction_image_name,
                                                      During_construction_latitude = groupbySubActivityDetails.Key.during_construction_latitude,
                                                      During_construction_longtitude = groupbySubActivityDetails.Key.during_construction_longtitude,
                                                      Post_construction_image_data = groupbySubActivityDetails.Key.post_construction_image_data,
                                                      Post_construction_image_location = groupbySubActivityDetails.Key.post_construction_image_location,
                                                      Post_construction_image_name = groupbySubActivityDetails.Key.post_construction_image_name,
                                                      Pos_construction_latitude = groupbySubActivityDetails.Key.pos_construction_latitude,
                                                      Post_construction_longtitude = groupbySubActivityDetails.Key.post_construction_longtitude,
                                                      Actual_date_of_completion = groupbySubActivityDetails.Key.actual_date_of_completion,
                                                      Actual_expenditure = groupbySubActivityDetails.Key.actual_expenditure,
                                                      Actual_irrigated_area = groupbySubActivityDetails.Key.actual_irrigated_area,
                                                      Youtube_story_link = groupbySubActivityDetails.Key.youtube_story_link,
                                                      Date_Of_Activity = groupbySubActivityDetails.Key.date_Of_Activity,
                                                      Image_data = groupbySubActivityDetails.Key.image_data,
                                                      Image_location = groupbySubActivityDetails.Key.image_location,
                                                      Image_name = groupbySubActivityDetails.Key.image_name,
                                                      Latitude = groupbySubActivityDetails.Key.latitude,
                                                      Longitutde = groupbySubActivityDetails.Key.longitutde,
                                                      // topic_id = groupbySubActivityDetails.Key.topic_id,
                                                      Topic_name = groupbySubActivityDetails.Key.topic_name,
                                                      TotalCoveredArea = groupbySubActivityDetails.Key.totalCoveredArea,
                                                      Plant_id = groupbySubActivityDetails.Key.plant_id,
                                                      PlantType = groupbySubActivityDetails.Key.plantType,
                                                      Plant_survived_one_year = groupbySubActivityDetails.Key.plant_survived_one_year,
                                                      Plant_survived_two_year = groupbySubActivityDetails.Key.plant_survived_two_year,
                                                      No_Of_Plants = groupbySubActivityDetails.Key.no_Of_Plants,
                                                      Mushroom_productivity = groupbySubActivityDetails.Key.mushroom_productivity,
                                                      Crop_productivity = groupbySubActivityDetails.Key.crop_productivity,
                                                      Crop_id = groupbySubActivityDetails.Key.crop_id,
                                                      Crop_type = groupbySubActivityDetails.Key.crop_type,
                                                      Input_type_id = groupbySubActivityDetails.Key.input_type_id,
                                                      Input_type_name = groupbySubActivityDetails.Key.input_type_name,
                                                      Input_type_value = groupbySubActivityDetails.Key.input_type_value,
                                                      Input_quantity = groupbySubActivityDetails.Key.input_quantity,
                                                      Rec_created_userid = groupbySubActivityDetails.Key.rec_created_userid,
                                                      Rec_updated_userid = groupbySubActivityDetails.Key.rec_updated_userid,
                                                      RecCreatedDate = groupbySubActivityDetails.Key.recCreatedDate,
                                                      RecUpdatedDate = groupbySubActivityDetails.Key.recUpdatedDate,
                                                  }).ToList();
                        b.Sub_activity_details.ForEach(c =>
                        {
                            c.Male = (from DataRow dRow in dt.Rows
                                      where dRow["scheme_id"].ToString() == a.Scheme_id.ToString()
                                      & dRow["activity_id"].ToString() == s.Activity_id.ToString()
                                      & dRow["yojana_number"].ToString() == c.Yojana_number
                                      group dRow by new
                                      {
                                          sc = dRow["sc_male"] == DBNull.Value ? (int?)null : (int)dRow["sc_male"],
                                          st = dRow["st_male"] == DBNull.Value ? (int?)null : (int)dRow["st_male"],
                                          general = dRow["general_male"] == DBNull.Value ? (int?)null : (int)dRow["general_male"],
                                          bc = dRow["bc_male"] == DBNull.Value ? (int?)null : (int)dRow["bc_male"],
                                          ebc = dRow["ebc_male"] == DBNull.Value ? (int?)null : (int)dRow["ebc_male"],
                                          minority = dRow["minority_male"] == DBNull.Value ? (int?)null : (int)dRow["minority_male"],
                                          GenderWiseTotal = dRow["GenderWiseTotal_male"] == DBNull.Value ? (int?)null : (int)dRow["GenderWiseTotal_male"],
                                      }
                                      into groupbymale
                                      select new PostMale
                                      {
                                          Sc = groupbymale.Key.sc,
                                          St = groupbymale.Key.st,
                                          General = groupbymale.Key.general,
                                          Bc = groupbymale.Key.bc,
                                          Ebc = groupbymale.Key.ebc,
                                          Minority = groupbymale.Key.minority,
                                          Total = groupbymale.Key.GenderWiseTotal,
                                      }).ToList().FirstOrDefault();

                            c.Female = (from DataRow dRow in dt.Rows
                                        where dRow["scheme_id"].ToString() == a.Scheme_id.ToString()
                                        & dRow["activity_id"].ToString() == s.Activity_id.ToString()
                                        & dRow["yojana_number"].ToString() == c.Yojana_number
                                        group dRow by new

                                        {
                                            sc = dRow["sc_female"] == DBNull.Value ? (int?)null : (int)dRow["sc_female"],
                                            st = dRow["st_female"] == DBNull.Value ? (int?)null : (int)dRow["st_female"],
                                            general = dRow["general_female"] == DBNull.Value ? (int?)null : (int)dRow["general_female"],
                                            bc = dRow["bc_female"] == DBNull.Value ? (int?)null : (int)dRow["bc_female"],
                                            ebc = dRow["ebc_female"] == DBNull.Value ? (int?)null : (int)dRow["ebc_female"],
                                            minority = dRow["minority_female"] == DBNull.Value ? (int?)null : (int)dRow["minority_female"],
                                            GenderWiseTotal = dRow["GenderWiseTotal_female"] == DBNull.Value ? (int?)null : (int)dRow["GenderWiseTotal_female"],
                                        }
                                        into groupbymale

                                        select new PostFemale
                                        {
                                            Sc = groupbymale.Key.sc,
                                            St = groupbymale.Key.st,
                                            General = groupbymale.Key.general,
                                            Bc = groupbymale.Key.bc,
                                            Ebc = groupbymale.Key.ebc,
                                            Minority = groupbymale.Key.minority,
                                            Total = groupbymale.Key.GenderWiseTotal,
                                        }).ToList().FirstOrDefault();
                            c.Others = (from DataRow dRow in dt.Rows
                                        where dRow["scheme_id"].ToString() == a.Scheme_id.ToString()
                                        & dRow["activity_id"].ToString() == s.Activity_id.ToString()
                                        & dRow["yojana_number"].ToString() == c.Yojana_number
                                        group dRow by new
                                        {
                                            sc = dRow["sc_others"] == DBNull.Value ? (int?)null : (int)dRow["sc_others"],
                                            st = dRow["st_others"] == DBNull.Value ? (int?)null : (int)dRow["st_others"],
                                            general = dRow["general_others"] == DBNull.Value ? (int?)null : (int)dRow["general_others"],
                                            bc = dRow["bc_others"] == DBNull.Value ? (int?)null : (int)dRow["bc_others"],
                                            ebc = dRow["ebc_others"] == DBNull.Value ? (int?)null : (int)dRow["ebc_others"],
                                            minority = dRow["minority_others"] == DBNull.Value ? (int?)null : (int)dRow["minority_others"],
                                            GenderWiseTotal = dRow["GenderWiseTotal_others"] == DBNull.Value ? (int?)null : (int)dRow["GenderWiseTotal_others"],
                                        }
                                        into groupbymale
                                        select new PostOthers
                                        {
                                            Sc = groupbymale.Key.sc,
                                            St = groupbymale.Key.st,
                                            General = groupbymale.Key.general,
                                            Bc = groupbymale.Key.bc,
                                            Ebc = groupbymale.Key.ebc,
                                            Minority = groupbymale.Key.minority,
                                            Total = groupbymale.Key.GenderWiseTotal,
                                        }).ToList().FirstOrDefault();
                        });
                    });
                });
            });
            return SubmittedDataDetails;
        }

        /// <summary>
        /// Populate Activity SubActivity Details.
        /// </summary>
        /// <returns>Soil Conservation Activity Detail.</returns>
        private List<SoilConservationActivityDetails> PopulateActivitySubActivityDetails(DataTable dt)
        {
            DateTime date = DateTime.Now;
            var tempYear = date.Month >= 4 ? date.Year : date.Year - 1;
            var nextYear = tempYear + 1;
            var currentFin_year = tempYear.ToString() + "-" + nextYear.ToString();
            List<SoilConservationActivityDetails> soilConservationActivityDetail = new List<SoilConservationActivityDetails>();
            soilConservationActivityDetail = (from DataRow dRow in dt.Rows
                                              group dRow by new
                                              {
                                                  scheme_id = dRow["scheme_id"] == DBNull.Value ? (int?)null : (int)dRow["scheme_id"],
                                                  scheme_name = dRow["scheme_name"] == DBNull.Value ? null : dRow["scheme_name"].ToString(),
                                              }
                                              into groupby
                                              select new SoilConservationActivityDetails
                                              {
                                                  Scheme_id = groupby.Key.scheme_id,
                                                  Scheme_name = groupby.Key.scheme_name,
                                                  Current_year = currentFin_year

                                              }).ToList();
            soilConservationActivityDetail.ForEach(a =>
            {
                a.Activity_list = (from DataRow dRow in dt.Rows
                                   where dRow["scheme_id"].ToString() == a.Scheme_id.ToString()
                                   group dRow by new
                                   {
                                       activity_id = dRow["activity_id"] == DBNull.Value ? (int?)null : (int)dRow["activity_id"],
                                       activity_name = dRow["activity_name"] == DBNull.Value ? null : dRow["activity_name"].ToString()
                                   }
                                   into groupbyActivityList
                                   select new ActivityList
                                   {
                                       Activity_id = groupbyActivityList.Key.activity_id,
                                       Activity_name = groupbyActivityList.Key.activity_name
                                   }).ToList();

                a.Activity_list.ForEach(s =>
                {
                    s.Sub_activity_list = (from DataRow dRow in dt.Rows
                                           where dRow["scheme_id"].ToString() == a.Scheme_id.ToString()
                                           & dRow["activity_id"].ToString() == s.Activity_id.ToString()
                                           select new SubActivityList
                                           {
                                               Sub_activity_id = dRow["sub_activity_id"].ToString() == "0" ? null : (int)dRow["sub_activity_id"],
                                               Sub_activity_name = dRow["sub_activity_name"] == DBNull.Value ? null : dRow["sub_activity_name"].ToString()
                                           }).ToList();
                });
            });
            return soilConservationActivityDetail;
        }

        /// <summary>
        /// Populate SoilConservation Existing Physical Financial Target.
        /// </summary>
        /// <returns>SoilConservation Existing Physical Financial Target.</returns>
        private List<SoilConservationExistingPhysicalFinancialTarget> PopulateSoilConservationExistingPhysicalFinancialTarget(DataTable dt)
        {
            DateTime date = DateTime.Now;
            var tempYear = date.Month >= 4 ? date.Year : date.Year - 1;
            var nextYear = tempYear + 1;
            var currentFin_year = tempYear.ToString() + "-" + nextYear.ToString();
            List<SoilConservationExistingPhysicalFinancialTarget> soilConservationExistingPhysicalFinancialTarget = new List<SoilConservationExistingPhysicalFinancialTarget>();
            soilConservationExistingPhysicalFinancialTarget = (from DataRow dRow in dt.Rows
                                                               group dRow by new
                                                               {
                                                                   scheme_id = dRow["scheme_id"] == DBNull.Value ? (int?)null : (int)dRow["scheme_id"],
                                                                   scheme_name = dRow["scheme_name"] == DBNull.Value ? null : dRow["scheme_name"].ToString(),
                                                                   panchayat_id = dRow["panchayat_id"] == DBNull.Value ? (int?)null : (int)dRow["panchayat_id"],
                                                                   panchayat_name = dRow["panchayat_name"] == DBNull.Value ? null : dRow["panchayat_name"].ToString(),
                                                                   activity_id = dRow["activity_id"] == DBNull.Value ? (int?)null : (int)dRow["activity_id"],
                                                                   activity_name = dRow["activity_name"] == DBNull.Value ? null : dRow["activity_name"].ToString(),
                                                                   sub_activity_id = dRow["sub_activity_id"] == DBNull.Value ? (int?)null : (int)dRow["sub_activity_id"],
                                                                   sub_activity_name = dRow["sub_activity_name"] == DBNull.Value ? null : dRow["sub_activity_name"].ToString(),
                                                                   structure_id = dRow["structure_id"] == DBNull.Value ? (int?)null : (int)dRow["structure_id"],
                                                                   structure_name = dRow["structure_name"] == DBNull.Value ? null : dRow["structure_name"].ToString(),
                                                                   physical_target = dRow["physical_target"] == DBNull.Value ? (decimal?)null : (decimal)dRow["physical_target"],
                                                                   financial_target = dRow["financial_target"] == DBNull.Value ? (decimal?)null : (decimal)dRow["financial_target"],
                                                               }
                                                               into groupby
                                                               select new SoilConservationExistingPhysicalFinancialTarget
                                                               {
                                                                   Scheme_id = groupby.Key.scheme_id,
                                                                   Scheme_name = groupby.Key.scheme_name,
                                                                   Panchayat_id = groupby.Key.panchayat_id,
                                                                   Panchayat_name = groupby.Key.panchayat_name,
                                                                   Activity_id = groupby.Key.activity_id,
                                                                   Activity_name = groupby.Key.activity_name,
                                                                   Sub_activity_id = groupby.Key.sub_activity_id,
                                                                   Sub_activity_name = groupby.Key.sub_activity_name,
                                                                   Structure_id = groupby.Key.structure_id,
                                                                   Structure_name = groupby.Key.structure_name,
                                                                   Current_year = currentFin_year,
                                                                   Physical_target = groupby.Key.physical_target,
                                                                   Financial_target = groupby.Key.financial_target,

                                                               }).ToList();
            return soilConservationExistingPhysicalFinancialTarget;
        }

        private List<CommonList> PopulateCommonList(DataTable dt)
        {
            List<CommonList> commonList = new List<CommonList>();

            commonList = (from DataRow dr in dt.Rows
                          where dr["Type"].ToString() == "commonList"
                          group dr by new
                          {
                              Structure_Id = dr["structure_id"] == DBNull.Value ? 0 : (int)dr["structure_id"],
                              Structure_Name = dr["structure_name"] == DBNull.Value ? "" : dr["structure_name"].ToString(),
                          }
                          into commonlist
                          select new CommonList
                          {
                              Structure_Id = commonlist.Key.Structure_Id,
                              Structure_Name = commonlist.Key.Structure_Name
                          }).ToList();

            return commonList;
        }

        private List<EntryPointList> PopulateEntryPointList(DataTable dt)
        {
            List<EntryPointList> entryPointList = new List<EntryPointList>();

            entryPointList = (from DataRow dr in dt.Rows
                              where dr["Type"].ToString() == "entryPointList"
                              group dr by new
                              {
                                  Structure_Id = dr["structure_id"] == DBNull.Value ? 0 : (int)dr["structure_id"],
                                  Structure_Name = dr["structure_name"] == DBNull.Value ? "" : dr["structure_name"].ToString(),
                              }
                              into commonlist
                              select new EntryPointList
                              {
                                  Structure_Id = commonlist.Key.Structure_Id,
                                  Structure_Name = commonlist.Key.Structure_Name
                              }).ToList();
            return entryPointList;
        }
    }
}
