//------------------------------------------------------------------------------
// <copyright file="FarmersOutreachServiceRepository.cs" company="Government of Bihar">
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
    using System.Linq;
    using ADIDAS.Model.DTO;
    using ADIDAS.Model.Entities;
    using ADIDAS.Repository.Helper;
    using ADIDAS.Repository.Interfaces;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Options;

    /// <summary>
    /// FarmersOutreachServiceRepository.
    /// </summary>
    public class FarmersOutreachServiceRepository : BaseRepository, IFarmersOutreachServiceRepository
    {
        private static string spGetDataFos = "Usp_getdata_FOS";
        private static string spUspInsertFosSingleFarmerDtls = "Usp_insert_FOS_single_farmer_dtls";
        private static string spUspInsertFosGroupFarmerDtls = "Usp_insert_FOS_group_farmer_dtls";
        private static string qnGetGenderList = "getGenderList";
        private static string qnGetFarmerDetails = "getFarmerDetails";
        private static string qnGetCategory = "GetCategory";
        private static string qnGetFarmerCaste = "GetFarmerCaste";
        private static string qnGetFarmerType = "GetFarmerType";
        private static string qnGetTypeofInteraction = "GetTypeofInteraction";
        private static string qnGetFosData = "GetFOSData";

        private readonly string istDate;
        private readonly IOptions<DBSettings> options;
        private readonly string istStrDate = "select CAST(DATEADD(HOUR, 5, DATEADD(MINUTE, 30, GETUTCDATE())) as DATE)";

        /// <summary>
        /// Initializes a new instance of the <see cref="FarmersOutreachServiceRepository"/> class.
        /// </summary>
        /// <param name="config">config.</param>
        /// <param name="options">options.</param>
        public FarmersOutreachServiceRepository(IConfiguration config, IOptions<DBSettings> options)
            : base(config)
        {
            this.options = options;
            this.istDate = this.GetDateFromServer();
            SqlHelper.SetConnectionString(this.options.Value.ConnectionString);
        }

        /// <summary>
        /// GetDateFromServer.
        /// </summary>
        /// <returns>string.</returns>
        public string GetDateFromServer()
        {
            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(this.istStrDate, null, SqlHelper.ExecutionType.Query);
            return dt.Rows[0][0].ToString();
        }

        /// <summary>
        /// GetGenderList.
        /// </summary>
        /// <returns>GenderList.</returns>
        public List<GenderList> GetGenderList()
        {
            List<GenderList> result = new List<GenderList>();
            List<DbParameter> parameters = new List<DbParameter>();
            parameters.Add(new SqlParameter { ParameterName = "@Query_name", Value = qnGetGenderList, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(spGetDataFos, parameters, SqlHelper.ExecutionType.Procedure);
            if (dt != null && dt.Rows.Count > 0)
            {
                result = (from DataRow row in dt.Rows
                          select new GenderList()
                          {
                              Gender_id = Convert.ToInt32(row["gender_id"]),
                              Gender_name = row["eng_gender_name"].ToString(),
                          }).ToList();
            }

            return result;
        }

        /// <summary>
        /// GetCategory.
        /// </summary>
        /// <returns>CategoryList.</returns>
        public List<Category> GetCategory()
        {
            List<Category> list = new List<Category>();
            List<DbParameter> parameters = new List<DbParameter>();
            parameters.Add(new SqlParameter { ParameterName = "@Query_name", Value = qnGetCategory, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(spGetDataFos, parameters, SqlHelper.ExecutionType.Procedure);
            if (dt != null && dt.Rows.Count > 0)
            {
                list = SqlHelper.ConvertDataTableToList<Category>(dt).GroupBy(x => new { x.Category_id, x.Category_name }).Select(x => x.First()).ToList();
                List<SubCategoryInput> listDTO = SqlHelper.ConvertDataTableToList<SubCategoryInput>(dt).GroupBy(x => new { x.Category_id, x.Category_name, x.Sub_category_id, x.Sub_category_name }).Select(x => x.First()).ToList();
                foreach (var item in list)
                {
                    item.Sub_category_list = listDTO.Where(x => x.Category_id == item.Category_id && x.Category_name == item.Category_name && !string.IsNullOrEmpty(x.Sub_category_name)).GroupBy(x => new { x.Sub_category_id, x.Sub_category_name }).Select(x => x.First()).Select(x => new SubCategory { Sub_category_id = x.Sub_category_id, Sub_category_name = x.Sub_category_name }).ToList();
                }
            }

            return list;
        }

        /// <summary>
        /// GetFarmerCaste.
        /// </summary>
        /// <returns>FarmerCasteList.</returns>
        public List<FarmerCaste> GetFarmerCaste()
        {
            List<FarmerCaste> farmerCaste = new List<FarmerCaste>();
            List<DbParameter> parameters = new List<DbParameter>();
            parameters.Add(new SqlParameter { ParameterName = "@Query_name", Value = qnGetFarmerCaste, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(spGetDataFos, parameters, SqlHelper.ExecutionType.Procedure);
            if (dt != null && dt.Rows.Count > 0)
            {
                farmerCaste = SqlHelper.ConvertDataTableToList<FarmerCaste>(dt);
            }

            return farmerCaste;
        }

        /// <summary>
        /// GetFarmerType.
        /// </summary>
        /// <returns>FarmerTypeList.</returns>
        public List<FarmerTypes> GetFarmerType()
        {
            List<FarmerTypes> farmerType = new List<FarmerTypes>();
            List<DbParameter> parameters = new List<DbParameter>();
            parameters.Add(new SqlParameter { ParameterName = "@Query_name", Value = qnGetFarmerType, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(spGetDataFos, parameters, SqlHelper.ExecutionType.Procedure);
            if (dt != null && dt.Rows.Count > 0)
            {
                farmerType = SqlHelper.ConvertDataTableToList<FarmerTypes>(dt);
            }

            return farmerType;
        }

        /// <summary>
        /// GetTypeOfInteraction.
        /// </summary>
        /// <returns>TypeOfInteractionList.</returns>
        public List<TypeOfInteraction> GetTypeOfInteraction()
        {
            List<TypeOfInteraction> typeOfInteraction = new List<TypeOfInteraction>();
            List<DbParameter> parameters = new List<DbParameter>();
            parameters.Add(new SqlParameter { ParameterName = "@Query_name", Value = qnGetTypeofInteraction, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(spGetDataFos, parameters, SqlHelper.ExecutionType.Procedure);
            if (dt != null && dt.Rows.Count > 0)
            {
                typeOfInteraction = SqlHelper.ConvertDataTableToList<TypeOfInteraction>(dt);
            }

            return typeOfInteraction;
        }

        /// <summary>
        /// GetFosData.
        /// </summary>
        /// <param name="user_id">user_id.</param>
        /// <param name="date_range_start">date_range_start.</param>
        /// <param name="date_range_end">date_range_end.</param>
        /// <param name="isResolved">isResolved.</param>
        /// <returns>FosData.</returns>
        public FosData GetFosData(int user_id, DateTime date_range_start, DateTime date_range_end, string isResolved)
        {
            FosData fosData = new FosData();
            List<FosFarmerData> fosFarmerData;
            List<DbParameter> parameters = new List<DbParameter>();
            parameters.Add(new SqlParameter { ParameterName = "@Query_name", Value = qnGetFosData, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@user_id", Value = user_id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@date_range_start", Value = date_range_start, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@date_range_end", Value = date_range_end, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@is_Resolved", Value = isResolved, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(spGetDataFos, parameters, SqlHelper.ExecutionType.Procedure);

            fosFarmerData = SqlHelper.ConvertDataTableToList<FosFarmerData>(dt);
            fosData.SingleFarmer = fosFarmerData.Where(x => x.Is_Group == "N").ToList();
            fosData.GroupOfFarmer = fosFarmerData.Where(x => x.Is_Group == "Y").ToList();

            return fosData;
        }

        /// <summary>
        /// PostSingleFarmerData.
        /// </summary>
        /// <param name="fosFarmerData">fosFarmerData.</param>
        /// <returns>output.</returns>
        public int PostSingleFarmerData(FosFarmerData fosFarmerData)
        {
            int insertRowsCount = 0;
            List<DbParameter> dbParams = new List<DbParameter>();

            dbParams.Add(new SqlParameter { ParameterName = "@mobile_number", Value = fosFarmerData.Mobile_number, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbParams.Add(new SqlParameter { ParameterName = "@dbt_number", Value = fosFarmerData.Dbt_number, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbParams.Add(new SqlParameter { ParameterName = "@query_id", Value = fosFarmerData.Query_id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            dbParams.Add(new SqlParameter { ParameterName = "@date", Value = fosFarmerData.Date, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
            dbParams.Add(new SqlParameter { ParameterName = "@interaction_id", Value = fosFarmerData.Interaction_id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            dbParams.Add(new SqlParameter { ParameterName = "@farmer_name", Value = fosFarmerData.Farmer_name, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbParams.Add(new SqlParameter { ParameterName = "@gender_name", Value = fosFarmerData.Gender_name, SqlDbType = SqlDbType.NVarChar, Direction = ParameterDirection.Input });
            dbParams.Add(new SqlParameter { ParameterName = "@district_id", Value = fosFarmerData.District_id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            dbParams.Add(new SqlParameter { ParameterName = "@block_id", Value = fosFarmerData.Block_id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            dbParams.Add(new SqlParameter { ParameterName = "@panchayat_id", Value = fosFarmerData.Panchayat_id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            dbParams.Add(new SqlParameter { ParameterName = "@village_name", Value = fosFarmerData.Village_name, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbParams.Add(new SqlParameter { ParameterName = "@farmer_type_name", Value = fosFarmerData.Farmer_type_name, SqlDbType = SqlDbType.NVarChar, Direction = ParameterDirection.Input });
            dbParams.Add(new SqlParameter { ParameterName = "@caste", Value = fosFarmerData.Caste, SqlDbType = SqlDbType.NVarChar, Direction = ParameterDirection.Input });
            dbParams.Add(new SqlParameter { ParameterName = "@category_id", Value = fosFarmerData.Category_id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            dbParams.Add(new SqlParameter { ParameterName = "@sub_category_id", Value = fosFarmerData.Sub_category_id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            dbParams.Add(new SqlParameter { ParameterName = "@query", Value = fosFarmerData.Query, SqlDbType = SqlDbType.NVarChar, Direction = ParameterDirection.Input });
            dbParams.Add(new SqlParameter { ParameterName = "@suggestion", Value = fosFarmerData.Suggestion, SqlDbType = SqlDbType.NVarChar, Direction = ParameterDirection.Input });
            dbParams.Add(new SqlParameter { ParameterName = "@isQueryResolved", Value = fosFarmerData.Is_Query_Resolved, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbParams.Add(new SqlParameter { ParameterName = "@isManualEntry", Value = fosFarmerData.Is_Manual_Entry, SqlDbType = SqlDbType.Bit, Direction = ParameterDirection.Input });
            dbParams.Add(new SqlParameter { ParameterName = "@group", Value = fosFarmerData.Is_Group, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbParams.Add(new SqlParameter { ParameterName = "@is_Resolved", Value = fosFarmerData.IsResolved, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbParams.Add(new SqlParameter { ParameterName = "@user_id", Value = fosFarmerData.Rec_created_userid, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });

            Dictionary<string, dynamic> result = SqlHelper.ExecuteNonQuery<SqlConnection>(spUspInsertFosSingleFarmerDtls, dbParams, SqlHelper.ExecutionType.Procedure);

            insertRowsCount = result["RowsAffected"];
            return insertRowsCount;
        }

        /// <summary>
        /// PostGroupData.
        /// </summary>
        /// <param name="fosFarmerData">fosFarmerData.</param>
        /// <returns>output.</returns>
        public int PostGroupData(FosFarmerData fosFarmerData)
        {
            int insertRowsCount = 0;
            List<DbParameter> dbParams = new List<DbParameter>();

            dbParams.Add(new SqlParameter { ParameterName = "@mobile_number", Value = fosFarmerData.Mobile_number, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbParams.Add(new SqlParameter { ParameterName = "@query_id", Value = fosFarmerData.Query_id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            dbParams.Add(new SqlParameter { ParameterName = "@no_of_farmer", Value = fosFarmerData.No_of_farmer, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            dbParams.Add(new SqlParameter { ParameterName = "@date", Value = fosFarmerData.Date, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
            dbParams.Add(new SqlParameter { ParameterName = "@interaction_id", Value = fosFarmerData.Interaction_id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            dbParams.Add(new SqlParameter { ParameterName = "@meeting_details", Value = fosFarmerData.Meeting_details, SqlDbType = SqlDbType.NVarChar, Direction = ParameterDirection.Input });
            dbParams.Add(new SqlParameter { ParameterName = "@category_id", Value = fosFarmerData.Category_id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            dbParams.Add(new SqlParameter { ParameterName = "@sub_category_id", Value = fosFarmerData.Sub_category_id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            dbParams.Add(new SqlParameter { ParameterName = "@query", Value = fosFarmerData.Query, SqlDbType = SqlDbType.NVarChar, Direction = ParameterDirection.Input });
            dbParams.Add(new SqlParameter { ParameterName = "@suggestion", Value = fosFarmerData.Suggestion, SqlDbType = SqlDbType.NVarChar, Direction = ParameterDirection.Input });
            dbParams.Add(new SqlParameter { ParameterName = "@isQueryResolved", Value = fosFarmerData.Is_Query_Resolved, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbParams.Add(new SqlParameter { ParameterName = "@group", Value = fosFarmerData.Is_Group, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbParams.Add(new SqlParameter { ParameterName = "@is_Resolved", Value = fosFarmerData.IsResolved, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbParams.Add(new SqlParameter { ParameterName = "@user_id", Value = fosFarmerData.Rec_created_userid, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });

            Dictionary<string, dynamic> result = SqlHelper.ExecuteNonQuery<SqlConnection>(spUspInsertFosGroupFarmerDtls, dbParams, SqlHelper.ExecutionType.Procedure);

            insertRowsCount = result["RowsAffected"];
            return insertRowsCount;
        }
    }
}
