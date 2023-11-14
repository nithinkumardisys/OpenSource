//------------------------------------------------------------------------------
// <copyright file="AssetManagementRepository.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------
namespace ADIDAS.Repository.DataRepository
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Data.SqlClient;
    using ADIDAS.Model.DTO;
    using ADIDAS.Model.Entities;
    using ADIDAS.Repository.Helper;
    using ADIDAS.Repository.Interfaces;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Options;
    using static ADIDAS.Model.Entities.Gamification;

    /// <summary>
    /// AssetManagementRepository.
    /// </summary>
    public class AssetManagementRepository : BaseRepository, IAssetManagementRepository
    {
        private readonly IOptions<DBSettings> options;

        private static string qnGetAgriAssetNoFacilityData = "GetAgriAssetNoFacilityData";
        private static string qnGetViewLatestSubFarmMachinery = "GetViewLatestSubFarmMachinery";
        private static string spGetDataFarmMachinery = "usp_getdata_farm_machinery";
        private static string qnGetOfflineFacilityDetails = "GetOfflineFacilityDetails";
        private static string qnGetLatestQtyByMachineryName = "GetLatestQtyByMachineryName";
        private static string qnGetAgriStructures = "GetAllStructures";
        private static string qnGetReportStucture = "GetAllFacilityDetails";
        private static string qnGetAllFarmMachineries = "GetAllFarmMachineries";
        private static string spPostReportfarmMachinery = "usp_insert_report_farm_machinery";
        private static string spPostReportStrcture = "USP_merge_report_structures";
        private static string spPostAgriAsset = "usp_Insert_Agri_assets";
        /// <summary>
        /// AssetManagementRepository.
        /// </summary>
        public AssetManagementRepository(IConfiguration config, IOptions<DBSettings> options)
            : base(config)
        {
            this.options = options;
            SqlHelper.SetConnectionString(this.options.Value.ConnectionString);
        }

        /// <summary>
        /// GetViewLatestSubFarmMachinery.
        /// </summary>
        /// <param name="panchayat_id">panchayat_id</param>
        /// <returns>List</returns>
        public List<Machinery> GetViewLatestSubFarmMachinery(int panchayat_id)
        {
            List<Machinery> list = new List<Machinery>();

            List<DbParameter> parameters = new List<DbParameter>();

            parameters.Add(new SqlParameter { ParameterName = "@query_name", Value = qnGetViewLatestSubFarmMachinery, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@panchayat_id", Value = panchayat_id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(spGetDataFarmMachinery, parameters, SqlHelper.ExecutionType.Procedure);
            if (dt != null && dt.Rows.Count > 0)
            {
                list = SqlHelper.ConvertDataTableToList<Machinery>(dt);
            }

            return list;
        }

        /// <summary>
        /// GetAgriAssetNoFacilityData.
        /// </summary>
        /// <param name="block_id">block_id</param>
        /// <returns>List</returns>
        public List<AgriAssetNoFacilityData> GetAgriAssetNoFacilityData(int block_id)
        {
            List<AgriAssetNoFacilityData> list = new List<AgriAssetNoFacilityData>();

            List<DbParameter> parameters = new List<DbParameter>();

            parameters.Add(new SqlParameter { ParameterName = "@query_name", Value = qnGetAgriAssetNoFacilityData, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@block_id", Value = block_id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });

            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(spGetDataFarmMachinery, parameters, SqlHelper.ExecutionType.Procedure);
            if (dt != null && dt.Rows.Count > 0)
            {
                list = SqlHelper.ConvertDataTableToList<AgriAssetNoFacilityData>(dt);
            }

            return list;
        }

        /// <summary>
        /// GetOfflineFacilityDetails.
        /// </summary>
        /// <param name="panchayat_id">panchayat_id</param>
        /// <returns>List</returns>
        public List<InsReportStructureModel> GetOfflineFacilityDetails(int panchayat_id)
        {
            List<InsReportStructureModel> list = new List<InsReportStructureModel>();

            List<DbParameter> parameters = new List<DbParameter>();

            parameters.Add(new SqlParameter { ParameterName = "@district_id", Value = DBNull.Value, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@block_id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@panchayat_id", Value = panchayat_id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@machinery_ID", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@Structure_ID", Value = DBNull.Value, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@query_name", Value = qnGetOfflineFacilityDetails, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });

            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(spGetDataFarmMachinery, parameters, SqlHelper.ExecutionType.Procedure);
            if (dt != null && dt.Rows.Count > 0)
            {
                list = SqlHelper.ConvertDataTableToList<InsReportStructureModel>(dt);
            }

            return list;
        }

        /// <summary>
        /// GetLatestQtyByMachineryName.
        /// </summary>
        public List<Machinery> GetLatestQtyByMachineryName(int panchayat_id)
        {
            List<Machinery> list = new List<Machinery>();

            List<DbParameter> parameters = new List<DbParameter>();

            parameters.Add(new SqlParameter { ParameterName = "@query_name", Value = qnGetLatestQtyByMachineryName, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@panchayat_id", Value = panchayat_id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@machinery_ID", Value = DBNull.Value, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });

            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(spGetDataFarmMachinery, parameters, SqlHelper.ExecutionType.Procedure);
            if (dt != null && dt.Rows.Count > 0)
            {
                list = SqlHelper.ConvertDataTableToList<Machinery>(dt);
            }

            return list;
        }

        /// <summary>
        /// GetAgriStructures.
        /// </summary>
        public List<AgriStructure> GetAgriStructures()
        {
            List<AgriStructure> list = new List<AgriStructure>();

            List<DbParameter> parameters = new List<DbParameter>();

            parameters.Add(new SqlParameter { ParameterName = "@query_name", Value = qnGetAgriStructures, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });

            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(spGetDataFarmMachinery, parameters, SqlHelper.ExecutionType.Procedure);
            if (dt != null && dt.Rows.Count > 0)
            {
                list = SqlHelper.ConvertDataTableToList<AgriStructure>(dt);
            }

            return list;
        }

        /// <summary>
        /// GetReportStucture.
        /// </summary>
        public List<InsReportStructureModel> GetReportStucture(int panchayat_id, int structure_ID)
        {
            List<InsReportStructureModel> list = new List<InsReportStructureModel>();

            List<DbParameter> parameters = new List<DbParameter>();

            parameters.Add(new SqlParameter { ParameterName = "@district_id", Value = DBNull.Value, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@block_id", Value = DBNull.Value, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@panchayat_id", Value = panchayat_id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@machinery_ID", Value = DBNull.Value, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@Structure_ID", Value = structure_ID, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@query_name", Value = qnGetReportStucture, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(spGetDataFarmMachinery, parameters, SqlHelper.ExecutionType.Procedure);
            if (dt != null && dt.Rows.Count > 0)
            {
                list = SqlHelper.ConvertDataTableToList<InsReportStructureModel>(dt);
            }

            return list;
        }

        /// <summary>
        /// GetAllFarmMachineries.
        /// </summary>
        /// <returns>List</returns>
        public List<AgriMachinery> GetAllFarmMachineries()
        {
            List<AgriMachinery> list = new List<AgriMachinery>();

            List<DbParameter> parameters = new List<DbParameter>();

            parameters.Add(new SqlParameter { ParameterName = "@query_name", Value = qnGetAllFarmMachineries, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(spGetDataFarmMachinery, parameters, SqlHelper.ExecutionType.Procedure);
            if (dt != null && dt.Rows.Count > 0)
            {
                list = SqlHelper.ConvertDataTableToList<AgriMachinery>(dt);
            }

            return list;
        }

        /// <summary>
        /// PostReportfarmMachinery.
        /// </summary>
        /// <param name="reportfarmMachineryModel">reportfarmMachineryModel</param>
        /// <returns>List</returns>
        public int PostReportfarmMachinery(List<ReportfarmMachineryModel> reportfarmMachineryModel)
        {
            string insertspOut = string.Empty;
            foreach (var item in reportfarmMachineryModel)
            {
                List<DbParameter> dbparams = new List<DbParameter>();

                dbparams.Add(new SqlParameter { ParameterName = "@district_id", Value = item.District_id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                dbparams.Add(new SqlParameter { ParameterName = "@block_id", Value = item.Block_id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                dbparams.Add(new SqlParameter { ParameterName = "@panchayat_id", Value = item.Panchayat_id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                dbparams.Add(new SqlParameter { ParameterName = "@machinery_ID", Value = item.Machinery_ID, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                dbparams.Add(new SqlParameter { ParameterName = "@machinery_qty", Value = item.Machinery_qty, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                dbparams.Add(new SqlParameter { ParameterName = "@rec_created_userid", Value = item.Rec_created_userid, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                dbparams.Add(new SqlParameter { ParameterName = "@rec_updated_userid", Value = item.Rec_updated_userid, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                dbparams.Add(new SqlParameter { ParameterName = "@response_status", SqlDbType = SqlDbType.NVarChar, Size = 1000, Direction = ParameterDirection.Output });

                Dictionary<string, dynamic> result = SqlHelper.ExecuteNonQuery<SqlConnection>(spPostReportfarmMachinery, dbparams, SqlHelper.ExecutionType.Procedure);

                insertspOut = DBNull.Value.Equals(result["@response_status"]) ? string.Empty : result["@response_status"];
            }

            if (insertspOut == "I")
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// PostReportStrcture
        /// </summary>
        /// <param name="insReportStructureModel">insReportStructureModel</param>
        /// <returns>List</returns>
        public int PostReportStrcture(InsReportStructureModel insReportStructureModel)
        {
            string insertspOut = string.Empty;

            List<DbParameter> dbparams = new List<DbParameter>();
            dbparams.Add(new SqlParameter { ParameterName = "@facility_id", Value = insReportStructureModel.Facility_id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@facility_name", Value = insReportStructureModel.Facility_name, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@facility_add", Value = insReportStructureModel.Facility_add, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@capacity", Value = insReportStructureModel.Capacity, SqlDbType = SqlDbType.Decimal, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@pincode", Value = insReportStructureModel.Pincode, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@unit_of_measure", Value = insReportStructureModel.Unit_of_measure, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@image_file_name1", Value = insReportStructureModel.Image_file_name1, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@image_file_location1", Value = insReportStructureModel.Image_file_location1, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@image_file_name2", Value = insReportStructureModel.Image_file_name2, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@image_file_location2", Value = insReportStructureModel.Image_file_location2, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@image_name1_latitude", Value = insReportStructureModel.Image_name1_latitude, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@image_name1_longitude", Value = insReportStructureModel.Image_name1_longitude, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@image_name2_latitude", Value = insReportStructureModel.Image_name2_latitude, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@image_name2_longitude", Value = insReportStructureModel.Image_name2_longitude, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@structure_ID", Value = insReportStructureModel.Structure_ID, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@Panchayat_ID", Value = insReportStructureModel.Panchayat_ID, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@rec_created_userid", Value = insReportStructureModel.Rec_created_userid, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@rec_updated_userid", Value = insReportStructureModel.Rec_updated_userid, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@is_facility_added", Value = insReportStructureModel.Is_facility_added, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@period", Value = insReportStructureModel.Period, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@response_status", SqlDbType = SqlDbType.VarChar, Size = 50, Direction = ParameterDirection.Output });

            Dictionary<string, dynamic> result = SqlHelper.ExecuteNonQuery<SqlConnection>(spPostReportStrcture, dbparams, SqlHelper.ExecutionType.Procedure);
           
            insertspOut = DBNull.Value.Equals(result["@response_status"]) ? string.Empty : result["@response_status"];
            if (insertspOut == "Y")
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// PostAgriAsset.
        /// </summary>
        /// <param name="insAgriAssetModel">insAgriAssetModel</param>
        /// <returns>List</returns>
        public int PostAgriAsset(InsAgriAssetModel insAgriAssetModel)
        {
            List<DbParameter> dbparams = new List<DbParameter>();
            dbparams.Add(new SqlParameter { ParameterName = "@submission_source", Value = insAgriAssetModel.Submission_source, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@machinery_type", Value = insAgriAssetModel.Type, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@machinery_name", Value = insAgriAssetModel.Name, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@rec_created_userid", Value = insAgriAssetModel.Rec_created_userid, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@Structure_desc", Value = insAgriAssetModel.Structure_desc, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@Is_Capacity_Mandatory_flg", Value = insAgriAssetModel.Capacitymandatoryflag, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@unit_of_measure", Value = insAgriAssetModel.UnitofMeasure, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });

            Dictionary<string, dynamic> result = SqlHelper.ExecuteNonQuery<SqlConnection>(spPostAgriAsset, dbparams, SqlHelper.ExecutionType.Procedure);

            return 1;
        }
    }
}
