//------------------------------------------------------------------------------
// <copyright file="AnnualPlanOutlayRepsitory.cs" company="Government of Bihar">
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

    /// <summary>
    /// AnnualPlanOutlay Repository.
    /// </summary>
    public class AnnualPlanOutlayRepository : BaseRepository, IAnnualPlanOutlayRepository
    {
        private static string spInsertAnnualPlanOutlay = "USP_Insert_annual_plan_utilization";
        private static string qnGetAnnualPlanOutlaySchemes = "GetSchemes";
        private static string qnGetBudgetHeadBySchemeForAllotment = "GetBudgetHeadBySchemeForAllotment";
        private static string qnGetAnnualPlanOutlayBudegt = "GetBudgetHeadByScheme";
        private static string spGetAnnualPlanOutlay = "usp_getdata_annual_plan_utilization";
        private static string qnGetAnnualPlanOutlaySummary = "GetSchemesSummary";
        private static string qnGetDrawingDisperseOfficers = "GetDrawingDisperseOfficers";
        private static string qnGetAnnualPlanOutlayBySchemeBudget = "GetAllotmentByScheme";

        private readonly IOptions<DBSettings> options;

        /// <summary>
        /// Initializes a new instance of the <see cref="AnnualPlanOutlayRepository"/> class.
        /// </summary>
        /// <param name="config">config.</param>
        /// <param name="options">options.</param>
        public AnnualPlanOutlayRepository(IConfiguration config, IOptions<DBSettings> options)
            : base(config)
        {
            this.options = options;
            SqlHelper.SetConnectionString(this.options.Value.ConnectionString);
        }

        /// <summary>
        /// InsertAnnualPlanOutlay.
        /// </summary>
        /// <param name="reqObj">reqObj.</param>
        /// <returns>int.</returns>
        public int InsertAnnualPlanOutlay(AnnualPlanOutlayModel reqObj)
        {
            List<DbParameter> dbParams = new List<DbParameter>();

            dbParams.Add(new SqlParameter { ParameterName = "@district_id", Value = reqObj.District_id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            dbParams.Add(new SqlParameter { ParameterName = "@scheme_ID", Value = reqObj.Scheme_ID, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            dbParams.Add(new SqlParameter { ParameterName = "@budget_head_id", Value = reqObj.Budget_head_id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            dbParams.Add(new SqlParameter { ParameterName = "@total_allotment", Value = reqObj.Total_allotment, SqlDbType = SqlDbType.Decimal, Direction = ParameterDirection.Input });
            dbParams.Add(new SqlParameter { ParameterName = "@total_expense", Value = reqObj.Total_expense, SqlDbType = SqlDbType.Decimal, Direction = ParameterDirection.Input });
            dbParams.Add(new SqlParameter { ParameterName = "@total_balance", Value = reqObj.Total_balance, SqlDbType = SqlDbType.Decimal, Direction = ParameterDirection.Input });
            dbParams.Add(new SqlParameter { ParameterName = "@rec_created_userid", Value = reqObj.Rec_created_userid, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            dbParams.Add(new SqlParameter { ParameterName = "@Drawing_Officer_ID", Value = reqObj.Rec_created_userid, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            dbParams.Add(new SqlParameter { ParameterName = "@flag", Value = reqObj.Flag, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbParams.Add(new SqlParameter { ParameterName = "@Response_flag", Size = 10, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Output });
            Dictionary<string, dynamic> result = SqlHelper.ExecuteNonQuery<SqlConnection>(spInsertAnnualPlanOutlay, dbParams, SqlHelper.ExecutionType.Procedure);

            string conflict = result["@Response_flag"];

            if (conflict == "E")
            {
                return 0;
            }
            else
            {
                return 1;
            }
        }

        /// <summary>
        /// GetAnnualPlanOutlaySchemes.
        /// </summary>
        /// <returns>List.</returns>
        public List<AnnualPlanOutlaySchemes> GetAnnualPlanOutlaySchemes()
        {
            List<AnnualPlanOutlaySchemes> list = new List<AnnualPlanOutlaySchemes>();

            List<DbParameter> parameters = new List<DbParameter>();

            parameters.Add(new SqlParameter { ParameterName = "@query_name", Value = qnGetAnnualPlanOutlaySchemes, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });

            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(spGetAnnualPlanOutlay, parameters, SqlHelper.ExecutionType.Procedure);
            if (dt != null && dt.Rows.Count > 0)
            {
                list = SqlHelper.ConvertDataTableToList<AnnualPlanOutlaySchemes>(dt);
            }

            return list;
        }

        /// <summary>
        /// GetBudgetHeadBySchemeForAllotment.
        /// </summary>
        /// <param name="schemeId">schemeId.</param>
        /// <param name="user_id">user_id.</param>
        /// <returns>List.</returns>
        public List<AnnualPlanOutlayBudgetHead> GetBudgetHeadBySchemeForAllotment(int schemeId, int user_id)
        {
            List<AnnualPlanOutlayBudgetHead> list = new List<AnnualPlanOutlayBudgetHead>();

            List<DbParameter> parameters = new List<DbParameter>();

            parameters.Add(new SqlParameter { ParameterName = "@query_name", Value = qnGetBudgetHeadBySchemeForAllotment, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@Drawing_Officer_ID", Value = user_id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@scheme_ID", Value = schemeId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });

            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(spGetAnnualPlanOutlay, parameters, SqlHelper.ExecutionType.Procedure);
            if (dt != null && dt.Rows.Count > 0)
            {
                list = SqlHelper.ConvertDataTableToList<AnnualPlanOutlayBudgetHead>(dt);
            }

            return list;
        }

        /// <summary>
        /// GetAnnualPlanOutlayBudegt.
        /// </summary>
        /// <param name="schemeId">schemeId.</param>
        /// <param name="user_id">user_id.</param>
        /// <returns>List.</returns>
        public List<AnnualPlanOutlayBudgetHead> GetAnnualPlanOutlayBudegt(int schemeId, int user_id)
        {
            List<AnnualPlanOutlayBudgetHead> list = new List<AnnualPlanOutlayBudgetHead>();

            List<DbParameter> parameters = new List<DbParameter>();

            parameters.Add(new SqlParameter { ParameterName = "@query_name", Value = qnGetAnnualPlanOutlayBudegt, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@Drawing_Officer_ID", Value = user_id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@scheme_ID", Value = schemeId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });

            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(spGetAnnualPlanOutlay, parameters, SqlHelper.ExecutionType.Procedure);
            if (dt != null && dt.Rows.Count > 0)
            {
                list = SqlHelper.ConvertDataTableToList<AnnualPlanOutlayBudgetHead>(dt);
            }

            return list;
        }

        /// <summary>
        /// GetAnnualPlanOutlaySummary.
        /// </summary>
        /// <param name="user_id">user_id.</param>
        /// <param name="district_id">district_id.</param>
        /// <returns>List.</returns>
        public List<AnnualPlanOutlaySummary> GetAnnualPlanOutlaySummary(int user_id, int district_id)
        {
            List<AnnualPlanOutlaySummary> list = new List<AnnualPlanOutlaySummary>();

            List<DbParameter> parameters = new List<DbParameter>();

            parameters.Add(new SqlParameter { ParameterName = "@query_name", Value = qnGetAnnualPlanOutlaySummary, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@Drawing_Officer_ID", Value = user_id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@district_id", Value = district_id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(spGetAnnualPlanOutlay, parameters, SqlHelper.ExecutionType.Procedure);
            if (dt != null && dt.Rows.Count > 0)
            {
                list = SqlHelper.ConvertDataTableToList<AnnualPlanOutlaySummary>(dt);
            }

            return list;
        }

        /// <summary>
        /// GetDrawingDisperseOfficers.
        /// </summary>
        /// <returns>List.</returns>
        public List<GetDrawingDisperseOfficersModel> GetDrawingDisperseOfficers()
        {
            List<GetDrawingDisperseOfficersModel> list = new List<GetDrawingDisperseOfficersModel>();

            List<DbParameter> parameters = new List<DbParameter>();

            parameters.Add(new SqlParameter { ParameterName = "@query_name", Value = qnGetDrawingDisperseOfficers, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });

            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(spGetAnnualPlanOutlay, parameters, SqlHelper.ExecutionType.Procedure);
            if (dt != null && dt.Rows.Count > 0)
            {
                list = SqlHelper.ConvertDataTableToList<GetDrawingDisperseOfficersModel>(dt);
            }

            return list;
        }

        /// <summary>
        /// GetAnnualPlanOutlayBySchemeBudget.
        /// </summary>
        /// <param name="schemeId">schemeId.</param>
        /// <param name="budgetHead">budgetHead.</param>
        /// <param name="user_id">user_id.</param>
        /// <returns>List.</returns>
        public AnnualPlanOutlaySummary GetAnnualPlanOutlayBySchemeBudget(int schemeId, int budgetHead, int user_id)
        {
            AnnualPlanOutlaySummary obj = null;

            List<DbParameter> parameters = new List<DbParameter>();

            parameters.Add(new SqlParameter { ParameterName = "@Drawing_Officer_ID", Value = user_id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@scheme_ID", Value = schemeId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@budget_head_ID", Value = budgetHead, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@query_name", Value = qnGetAnnualPlanOutlayBySchemeBudget, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(spGetAnnualPlanOutlay, parameters, SqlHelper.ExecutionType.Procedure);
            if (dt != null && dt.Rows.Count > 0)
            {
                obj = SqlHelper.ConvertDataTableToList<AnnualPlanOutlaySummary>(dt)[0];
            }

            return obj;
        }

        /// <summary>
        /// IsAnnualPlanUser.
        /// </summary>
        /// <param name="user_id">user_id.</param>
        /// <returns>List.</returns>
        public List<DisburseEntity> IsAnnualPlanUser(int user_id)
        {
            List<DisburseEntity> enitity = new List<DisburseEntity>();

            List<DbParameter> parameters = new List<DbParameter>();

            parameters.Add(new SqlParameter { ParameterName = "@user_id", Value = user_id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });

            parameters.Add(new SqlParameter { ParameterName = "@query_name", Value = qnGetDrawingDisperseOfficers, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(spGetAnnualPlanOutlay, parameters, SqlHelper.ExecutionType.Procedure);
            if (dt != null && dt.Rows.Count > 0)
            {
                enitity = SqlHelper.ConvertDataTableToList<DisburseEntity>(dt);
            }

            return enitity;
        }

        /// <summary>
        /// GetDisburseOficers.
        /// </summary>
        /// <param name="user_id">user_id.</param>
        /// <param name="designation">designation.</param>
        /// <returns>List.</returns>
        public List<DisburseEntity> GetDisburseOficers(int user_id, string designation)
        {
            List<DisburseEntity> enitity = new List<DisburseEntity>();

            List<DbParameter> parameters = new List<DbParameter>();

            parameters.Add(new SqlParameter { ParameterName = "@user_id", Value = user_id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@Designation", Value = designation, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@query_name", Value = qnGetDrawingDisperseOfficers, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(spGetAnnualPlanOutlay, parameters, SqlHelper.ExecutionType.Procedure);
            if (dt != null && dt.Rows.Count > 0)
            {
                enitity = SqlHelper.ConvertDataTableToList<DisburseEntity>(dt);
            }

            return enitity;
        }
    }
}
