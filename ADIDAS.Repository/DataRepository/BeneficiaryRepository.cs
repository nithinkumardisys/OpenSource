//------------------------------------------------------------------------------
// <copyright file="BeneficiaryRepository.cs" company="Government of Bihar">
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
    using Microsoft.VisualBasic;

    /// <summary>
    /// BeneficiaryRepository.
    /// </summary>
    public class BeneficiaryRepository : BaseRepository, IBeneficiaryRepository
    {
        private static string spGetBIAWebDtls = "usp_BIA_get_web_dtls";
        private static string qnGetBIAUserDetail = "GetBIAUserDetail";
        private static string qnGetBIADirectorates = "GetBIADirectorates";
        private static string qnGetBIASchemeNames = "GetBIASchemeNames";
        private static string qnGetAssignedAndPendingData = "GetAssignedAndPendingData";
        private static string qnGetVerifiedData = "GetVerifiedData";
        private static string qnGethistorydetails = "Gethistorydetails";
        private static string qnGetdatatoshow = "Getdatatoshow";
        private static string qnGetAgricultureOfficers = "GetAgricultureOfficers";
        private static string qnGetAgricultureOfficerDetails = "GetAgricultureOfficerDetails";
        private static string spPostBIAWebDtls = "Usp_insert_BIA_Assignedbyhq_beneficiary_dtls";
        private static string spGetBIADtls = "usp_get_BIA_dtls";
        private static string qnGetFinancialYear = "GetFinancialYear";
        private static string qnGetDirectorateSchemeDetail = "GetDirectorateSchemeDetail";
        private static string qnGetStatus = "GetStatus";
        private static string qnGetBeneficiaryRecord = "GetBeneficiaryRecord";
        private static string qnGetBeneficiaryNotification = "GetBeneficiaryNotification";
        private static string spPostDeleteBeneficiaryDetails = "Usp_insert_delete_beneficiery_dtls";
        private static string spPostAssignBeneficiaryDetails = "Usp_insert_BIA_self_assigned_beneficiery_dtls";
        private static string spPostBeneficiaryDetails = "Usp_insert_BIA_beneficiery_dtls";

        private readonly IOptions<DBSettings> options;

        /// <summary>
        /// Initializes a new instance of the <see cref="BeneficiaryRepository"/> class.
        /// </summary>
        /// <param name="config">config.</param>
        /// <param name="options">options.</param>
        public BeneficiaryRepository(IConfiguration config, IOptions<DBSettings> options)
            : base(config)
        {
            this.options = options;
            SqlHelper.SetConnectionString(this.options.Value.ConnectionString);
        }

        /// <summary>
        /// GetBIAUserDetails.
        /// </summary>
        /// <param name="userid">userid.</param>
        /// <returns>User Details.</returns>
        public List<DisburseEntity> GetBIAUserDetails(int userid)
        {
            List<DisburseEntity> entities = new List<DisburseEntity>();
            List<DbParameter> parameters = new List<DbParameter>();
            parameters.Add(new SqlParameter { ParameterName = "@Query_name", Value = qnGetBIAUserDetail, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@userid", Value = userid, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(spGetBIAWebDtls, parameters, SqlHelper.ExecutionType.Procedure);
            if (dt != null && dt.Rows.Count > 0)
            {
                entities = SqlHelper.ConvertDataTableToList<DisburseEntity>(dt);
            }

            return entities;
        }

        /// <summary>
        /// GetBIADirectorateList.
        /// </summary>
        /// <returns>Directorate List.</returns>
        public List<DirectorateSchemeDetails> GetBIADirectorateList()
        {
            List<DirectorateSchemeDetails> directorateDetails = new List<DirectorateSchemeDetails>();
            List<DbParameter> parameters = new List<DbParameter>();
            parameters.Add(new SqlParameter { ParameterName = "@Query_name", Value = qnGetBIADirectorates, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(spGetBIAWebDtls, parameters, SqlHelper.ExecutionType.Procedure);
            if (dt != null && dt.Rows.Count > 0)
            {
                directorateDetails = SqlHelper.ConvertDataTableToList<DirectorateSchemeDetails>(dt);
            }

            return directorateDetails;
        }

        /// <summary>
        /// GetBIASchemeNames.
        /// </summary>
        /// <param name="directorate">directorate.</param>
        /// <returns>Scheme List.</returns>
        public List<BiaScheme> GetBIASchemeNames(string directorate)
        {
            List<BiaScheme> schemeDetails = new List<BiaScheme>();
            List<DbParameter> parameters = new List<DbParameter>();
            parameters.Add(new SqlParameter { ParameterName = "@Query_name", Value = qnGetBIASchemeNames, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@directorate", Value = directorate, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(spGetBIAWebDtls, parameters, SqlHelper.ExecutionType.Procedure);
            if (dt != null && dt.Rows.Count > 0)
            {
                schemeDetails = SqlHelper.ConvertDataTableToList<BiaScheme>(dt);
            }

            return schemeDetails;
        }

        /// <summary>
        /// GetAssignedAndPendingData.
        /// </summary>
        /// <param name="biaWebFilters">biaWebFilters.</param>
        /// <returns>AssignedAndPendingData.</returns>
        public List<BiaWebGridDetails> GetAssignedAndPendingData(BiaWebFilters biaWebFilters)
        {
            List<BiaWebGridDetails> biaWebGridDetails = new List<BiaWebGridDetails>();
            List<DbParameter> parameters = new List<DbParameter>();
            parameters.Add(new SqlParameter { ParameterName = "@Query_name", Value = qnGetAssignedAndPendingData, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@finYear", Value = biaWebFilters.FinYear, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@directorate", Value = biaWebFilters.Directorate, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@schemeName", Value = biaWebFilters.SchemeName, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@divisionId", Value = biaWebFilters.DivisionId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@districtId", Value = biaWebFilters.DistrictId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@subDivisionId", Value = biaWebFilters.SubDivisionId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@blockId", Value = biaWebFilters.BlockId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@panchayatId", Value = biaWebFilters.PanchayatId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(spGetBIAWebDtls, parameters, SqlHelper.ExecutionType.Procedure);
            if (dt != null && dt.Rows.Count > 0)
            {
                biaWebGridDetails = SqlHelper.ConvertDataTableToList<BiaWebGridDetails>(dt);
            }

            return biaWebGridDetails;
        }

        /// <summary>
        /// GetVerifiedData.
        /// </summary>
        /// <param name="biaWebFilters">biaWebFilters.</param>
        /// <returns>VerifiedData.</returns>
        public List<BiaWebGridDetails> GetVerifiedData(BiaWebFilters biaWebFilters)
        {
            List<BiaWebGridDetails> biaWebGridDetails = new List<BiaWebGridDetails>();
            List<DbParameter> parameters = new List<DbParameter>();
            parameters.Add(new SqlParameter { ParameterName = "@Query_name", Value = qnGetVerifiedData, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@finYear", Value = biaWebFilters.FinYear, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@directorate", Value = biaWebFilters.Directorate, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@schemeName", Value = biaWebFilters.SchemeName, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@divisionId", Value = biaWebFilters.DivisionId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@districtId", Value = biaWebFilters.DistrictId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@subDivisionId", Value = biaWebFilters.SubDivisionId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@blockId", Value = biaWebFilters.BlockId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@panchayatId", Value = biaWebFilters.PanchayatId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(spGetBIAWebDtls, parameters, SqlHelper.ExecutionType.Procedure);
            if (dt != null && dt.Rows.Count > 0)
            {
                biaWebGridDetails = SqlHelper.ConvertDataTableToList<BiaWebGridDetails>(dt);
            }

            return biaWebGridDetails;
        }

        /// <summary>
        /// GetVerifiedHistoryData.
        /// </summary>
        /// <param name="biaWebFilters">biaWebFilters.</param>
        /// <returns>VerifiedDataHistory.</returns>
        public List<BiaWebGridDetails> GetVerifiedHistoryData(BiaWebFilters biaWebFilters)
        {
            List<BiaWebGridDetails> biaWebGridDetails = new List<BiaWebGridDetails>();
            List<DbParameter> parameters = new List<DbParameter>();
            parameters.Add(new SqlParameter { ParameterName = "@Query_name", Value = qnGethistorydetails, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@finYear", Value = biaWebFilters.FinYear, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@directorate", Value = biaWebFilters.Directorate, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@schemeName", Value = biaWebFilters.SchemeName, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@divisionId", Value = biaWebFilters.DivisionId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@districtId", Value = biaWebFilters.DistrictId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@subDivisionId", Value = biaWebFilters.SubDivisionId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@blockId", Value = biaWebFilters.BlockId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@panchayatId", Value = biaWebFilters.PanchayatId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(spGetBIAWebDtls, parameters, SqlHelper.ExecutionType.Procedure);
            if (dt != null && dt.Rows.Count > 0)
            {
                biaWebGridDetails = SqlHelper.ConvertDataTableToList<BiaWebGridDetails>(dt);
            }

            return biaWebGridDetails;
        }

        /// <summary>
        /// GetBIADetailsBasedOnClick.
        /// </summary>
        /// <param name="biaWebGridFilters">biaWebGridFilters.</param>
        /// <returns>Bia Details List.</returns>
        public List<BiaWebGridDetails> GetBIADetailsBasedOnClick(BiaWebGridFilters biaWebGridFilters)
        {
            List<BiaWebGridDetails> biaWebGridDetails = new List<BiaWebGridDetails>();
            List<DbParameter> parameters = new List<DbParameter>();
            parameters.Add(new SqlParameter { ParameterName = "@Query_name", Value = qnGetdatatoshow, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@finYear", Value = biaWebGridFilters.FinYear, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@directorate", Value = biaWebGridFilters.Directorate, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@schemeName", Value = biaWebGridFilters.SchemeName, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@divisionId", Value = biaWebGridFilters.DivisionId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@districtId", Value = biaWebGridFilters.DistrictId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@subDivisionId", Value = biaWebGridFilters.SubDivisionId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@blockId", Value = biaWebGridFilters.BlockId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@panchayatId", Value = biaWebGridFilters.PanchayatId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@status", Value = biaWebGridFilters.Status, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@detailsToShow", Value = biaWebGridFilters.DetailsToShow, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(spGetBIAWebDtls, parameters, SqlHelper.ExecutionType.Procedure);
            if (dt != null && dt.Rows.Count > 0)
            {
                biaWebGridDetails = SqlHelper.ConvertDataTableToList<BiaWebGridDetails>(dt);
            }

            return biaWebGridDetails;
        }

        /// <summary>
        /// GetAgricultureOfficerDetails.
        /// </summary>
        /// <param name="biaWebAgricultureOfficerFilters">biaWebAgricultureOfficerFilters.</param>
        /// <returns>AO List.</returns>
        public List<BiaWebAoUserDetails> GetAgricultureOfficerDetails(BiaWebAgricultureOfficerFilters biaWebAgricultureOfficerFilters)
        {
            List<BiaWebAoUserDetails> biaWebAoUserDetails = new List<BiaWebAoUserDetails>();
            List<DbParameter> parameters = new List<DbParameter>();
            parameters.Add(new SqlParameter { ParameterName = "@Query_name", Value = qnGetAgricultureOfficers, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@directorate", Value = biaWebAgricultureOfficerFilters.Directorate, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@divisionId", Value = biaWebAgricultureOfficerFilters.DivisionId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@districtId", Value = biaWebAgricultureOfficerFilters.DistrictId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@subDivisionId", Value = biaWebAgricultureOfficerFilters.SubDivisionId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@blockId", Value = biaWebAgricultureOfficerFilters.BlockId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@panchayatId", Value = biaWebAgricultureOfficerFilters.PanchayatId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@nameOrDesignation", Value = biaWebAgricultureOfficerFilters.NameOrDesignation, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(spGetBIAWebDtls, parameters, SqlHelper.ExecutionType.Procedure);
            if (dt != null && dt.Rows.Count > 0)
            {
                biaWebAoUserDetails = SqlHelper.ConvertDataTableToList<BiaWebAoUserDetails>(dt);
            }

            return biaWebAoUserDetails;
        }

        /// <summary>
        /// GetAgricultureOfficerTaskDetails.
        /// </summary>
        /// <param name="userId">userId.</param>
        /// <returns>AO Task List.</returns>
        public List<BiaWebAoUserDetails> GetAgricultureOfficerTaskDetails(int userId)
        {
            List<BiaWebAoUserDetails> biaWebAoUserDetails = new List<BiaWebAoUserDetails>();
            List<DbParameter> parameters = new List<DbParameter>();
            parameters.Add(new SqlParameter { ParameterName = "@Query_name", Value = qnGetAgricultureOfficerDetails, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@userid", Value = userId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(spGetBIAWebDtls, parameters, SqlHelper.ExecutionType.Procedure);
            if (dt != null && dt.Rows.Count > 0)
            {
                biaWebAoUserDetails = SqlHelper.ConvertDataTableToList<BiaWebAoUserDetails>(dt).GroupBy(x => new { x.User_Id, x.Name, x.Designation }).Select(x => x.First()).ToList();
                List<BiaWebAoPendingTasks> biaWebAoPendingTasks = SqlHelper.ConvertDataTableToList<BiaWebAoPendingTasks>(dt).GroupBy(x => new { x.Scheme, x.BeneficiaryName, x.BeneficiaryId, x.RegistrationNo, x.DistrictName }).Select(x => x.First()).ToList();
                foreach (var item in biaWebAoUserDetails)
                {
                    item.PendingTasks = biaWebAoPendingTasks.GroupBy(x => new { x.Scheme, x.BeneficiaryName }).Select(x => x.First()).Select(x => new BiaWebAoPendingTasks { Scheme = x.Scheme, BeneficiaryName = x.BeneficiaryName, RegistrationNo = x.RegistrationNo, DistrictName = x.DistrictName }).ToList();
                }
            }

            return biaWebAoUserDetails;
        }

        /// <summary>
        /// PostAssignTask.
        /// </summary>
        /// <param name="biaWebGridDetails">biaWebGridDetails.</param>
        /// <returns>output.</returns>
        public int PostAssignTask(List<BiaWebGridDetails> biaWebGridDetails)
        {
            int insertRowsCount = 0;
            for (int i = 0; i < biaWebGridDetails.Count; i++)
            {
                List<DbParameter> dbParams = new List<DbParameter>();

                dbParams.Add(new SqlParameter { ParameterName = "@schemeName", Value = biaWebGridDetails[i].SchemeName, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbParams.Add(new SqlParameter { ParameterName = "@districtName", Value = biaWebGridDetails[i].DistrictName, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbParams.Add(new SqlParameter { ParameterName = "@blockName", Value = biaWebGridDetails[i].BlockName, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbParams.Add(new SqlParameter { ParameterName = "@panchayatName", Value = biaWebGridDetails[i].PanchayatName, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbParams.Add(new SqlParameter { ParameterName = "@beneficaryName", Value = biaWebGridDetails[i].BeneficiaryName, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbParams.Add(new SqlParameter { ParameterName = "@beneficaryType", Value = biaWebGridDetails[i].BeneficiaryType, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbParams.Add(new SqlParameter { ParameterName = "@registrationNo", Value = biaWebGridDetails[i].RegistrationNo, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbParams.Add(new SqlParameter { ParameterName = "@applicationNo", Value = biaWebGridDetails[i].ApplicationNo, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbParams.Add(new SqlParameter { ParameterName = "@mobileNo", Value = biaWebGridDetails[i].MobileNo, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbParams.Add(new SqlParameter { ParameterName = "@subsidyReceived", Value = biaWebGridDetails[i].SubsidyReceived, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbParams.Add(new SqlParameter { ParameterName = "@AO_name", Value = biaWebGridDetails[i].AssignAoName, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbParams.Add(new SqlParameter { ParameterName = "@designation", Value = biaWebGridDetails[i].Designation, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbParams.Add(new SqlParameter { ParameterName = "@target_date", Value = biaWebGridDetails[i].TargetDateOfCompletion, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
                dbParams.Add(new SqlParameter { ParameterName = "@transacation_date", Value = biaWebGridDetails[i].TransactionDate, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbParams.Add(new SqlParameter { ParameterName = "@Hq_userid", Value = biaWebGridDetails[i].AssignorOfficerUserId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                dbParams.Add(new SqlParameter { ParameterName = "@AO_userid", Value = biaWebGridDetails[i].AssignAoID, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                dbParams.Add(new SqlParameter { ParameterName = "@comments", Value = biaWebGridDetails[i].Comments, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });

                Dictionary<string, dynamic> result = SqlHelper.ExecuteNonQuery<SqlConnection>(spPostBIAWebDtls, dbParams, SqlHelper.ExecutionType.Procedure);

                insertRowsCount = insertRowsCount + result["RowsAffected"];
            }

            return insertRowsCount;
        }

        #region Mobile APIs

        /// <summary>
        /// GetFinancialYear.
        /// </summary>
        /// <returns>Fin Year.</returns>
        public List<FinancialYear> GetFinancialYear()
        {
            List<FinancialYear> financialYear = new List<FinancialYear>();
            List<DbParameter> parameters = new List<DbParameter>();
            DateTime date = DateTime.Now;
            var currentYear = date.Month >= 4 ? date.Year : date.Year - 1;
            parameters.Add(new SqlParameter { ParameterName = "@Query_name", Value = qnGetFinancialYear, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(spGetBIADtls, parameters, SqlHelper.ExecutionType.Procedure);
            if (dt != null && dt.Rows.Count > 0)
            {
                financialYear = SqlHelper.ConvertDataTableToList<FinancialYear>(dt);
            }

            for (int i = 0; i < financialYear.Count; i++)
            {
                if (financialYear[i].Financial_Year.Contains(currentYear.ToString()))
                {
                    financialYear[i].IsCurrent = true;
                }
                else
                {
                    financialYear[i].IsCurrent = false;
                }
            }

            return financialYear;
        }

        /// <summary>
        /// GetDirectorateSchemeDetail.
        /// </summary>
        /// <returns>DirectorateSchemeDetails List.</returns>
        public List<DirectorateSchemeDetails> GetDirectorateSchemeDetail()
        {
            List<DirectorateSchemeDetails> directorateSchemeDetails = new List<DirectorateSchemeDetails>();
            List<DbParameter> parameters = new List<DbParameter>();
            parameters.Add(new SqlParameter { ParameterName = "@Query_name", Value = qnGetDirectorateSchemeDetail, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(spGetBIADtls, parameters, SqlHelper.ExecutionType.Procedure);
            if (dt != null && dt.Rows.Count > 0)
            {
                directorateSchemeDetails = SqlHelper.ConvertDataTableToList<DirectorateSchemeDetails>(dt).GroupBy(x => new { x.Directorate_id, x.Directorate_name }).Select(x => x.First()).ToList();
                List<BiaSchemeInput> biaScheme = SqlHelper.ConvertDataTableToList<BiaSchemeInput>(dt).GroupBy(x => new { x.Directorate_id, x.Directorate_name, x.Scheme_id, x.Scheme_name }).Select(x => x.First()).ToList();
                foreach (var item in directorateSchemeDetails)
                {
                    item.Scheme_list = biaScheme.Where(x => x.Directorate_id == item.Directorate_id && x.Directorate_name == item.Directorate_name && !string.IsNullOrEmpty(x.Scheme_name)).GroupBy(x => new { x.Scheme_id, x.Scheme_name }).Select(x => x.First()).Select(x => new BiaScheme { Scheme_id = x.Scheme_id, Scheme_name = x.Scheme_name }).ToList();
                }
            }

            return directorateSchemeDetails;
        }

        /// <summary>
        /// GetStatus.
        /// </summary>
        /// <returns>Status List.</returns>
        public List<BiaStatus> GetStatus()
        {
            List<BiaStatus> biaStatus = new List<BiaStatus>();
            List<DbParameter> parameters = new List<DbParameter>();
            parameters.Add(new SqlParameter { ParameterName = "@Query_name", Value = qnGetStatus, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(spGetBIADtls, parameters, SqlHelper.ExecutionType.Procedure);
            if (dt != null && dt.Rows.Count > 0)
            {
                biaStatus = SqlHelper.ConvertDataTableToList<BiaStatus>(dt);
            }



            return biaStatus;
        }

        /// <summary>
        /// GetBeneficiaryRecord.
        /// </summary>
        /// <param name="userId">userId.</param>
        /// <returns>BeneficiaryRecord List.</returns>
        public List<BiaBeneficiaryRecords> GetBeneficiaryRecord(int userId)
        {
            List<BiaBeneficiaryRecords> biaBeneficiaryRecords = new List<BiaBeneficiaryRecords>();
            List<DbParameter> parameters = new List<DbParameter>();
            parameters.Add(new SqlParameter { ParameterName = "@Query_name", Value = qnGetBeneficiaryRecord, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@user_id", Value = userId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(spGetBIADtls, parameters, SqlHelper.ExecutionType.Procedure);
            if (dt != null && dt.Rows.Count > 0)
            {
                biaBeneficiaryRecords = SqlHelper.ConvertDataTableToList<BiaBeneficiaryRecords>(dt).GroupBy(x => new { x.Application_number, x.Beneficiary_id, x.Beneficiary_name }).Select(x => x.First()).ToList();
                List<BiaPreviousInspectionInput> biaPreviousInspectionInput = SqlHelper.ConvertDataTableToList<BiaPreviousInspectionInput>(dt).GroupBy(x => new { x.Application_number, x.Beneficiary_id, x.Beneficiary_name, x.Previous_Comments, x.Previous_is_Verified, x.Previous_Verification_date, x.Previous_Assigned_by_designation, x.Previous_Assigned_by_name, x.Previous_Assigneer_User_Id }).Select(x => x.First()).ToList();
                foreach (var item in biaBeneficiaryRecords)
                {
                    item.Previous_Inspection = biaPreviousInspectionInput.Where(x => x.Application_number == item.Application_number && x.Beneficiary_id.Equals(item.Beneficiary_id) && x.Beneficiary_name == item.Beneficiary_name && x.Previous_is_Verified != null && x.Previous_Assigned_by_name != null && x.Previous_Assigned_by_designation != null).Select(x => new BiaPreviousInspection { Previous_Comments = x.Previous_Comments, Previous_is_Verified = x.Previous_is_Verified, Previous_Verification_date = x.Previous_Verification_date, Previous_Assigned_by_designation = x.Previous_Assigned_by_designation, Previous_Assigned_by_name = x.Previous_Assigned_by_name, Previous_Assigneer_User_Id = x.Previous_Assigneer_User_Id }).ToList();
                }
            }

            return biaBeneficiaryRecords;
        }

        /// <summary>
        /// PostDeleteBeneficiary.
        /// </summary>
        /// <param name="biaDeleteBeneficiaryInput">biaDeleteBeneficiaryInput.</param>
        /// <returns>output.</returns>
        public int PostDeleteBeneficiary(List<BiaDeleteBeneficiaryInput> biaDeleteBeneficiaryInput)
        {
            int insertRowsCount = 0;
            List<DbParameter> dbParams = new List<DbParameter>();

            for (int i = 0; i < biaDeleteBeneficiaryInput.Count; i++)
            {
                dbParams.Add(new SqlParameter { ParameterName = "@user_id", Value = biaDeleteBeneficiaryInput[i].UserId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                dbParams.Add(new SqlParameter { ParameterName = "@ID", Value = biaDeleteBeneficiaryInput[i].Id, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });

                Dictionary<string, dynamic> result = SqlHelper.ExecuteNonQuery<SqlConnection>(spPostDeleteBeneficiaryDetails, dbParams, SqlHelper.ExecutionType.Procedure);

                insertRowsCount = result["RowsAffected"];
            }

            return insertRowsCount;
        }

        /// <summary>
        /// PostAssignBeneficiary.
        /// </summary>
        /// <param name="biaAssignBeneficiaryInput">biaAssignBeneficiaryInput.</param>
        /// <returns>output.</returns>
        public int PostAssignBeneficiary(List<BiaAssignBeneficiaryInput> biaAssignBeneficiaryInput)
        {
            int insertRowsCount = 0;
            List<DbParameter> dbParams = new List<DbParameter>();

            for (int i = 0; i < biaAssignBeneficiaryInput.Count; i++)
            {
                dbParams.Add(new SqlParameter { ParameterName = "@user_id", Value = biaAssignBeneficiaryInput[i].UserId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                dbParams.Add(new SqlParameter { ParameterName = "@ID", Value = biaAssignBeneficiaryInput[i].Id, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbParams.Add(new SqlParameter { ParameterName = "@status", Value = biaAssignBeneficiaryInput[i].Status, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbParams.Add(new SqlParameter { ParameterName = "@due_date", Value = biaAssignBeneficiaryInput[i].Due_date, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });

                Dictionary<string, dynamic> result = SqlHelper.ExecuteNonQuery<SqlConnection>(spPostAssignBeneficiaryDetails, dbParams, SqlHelper.ExecutionType.Procedure);

                insertRowsCount = result["RowsAffected"];
            }

            return insertRowsCount;
        }


        /// <summary>
        /// PostBeneficiaryDetail.
        /// </summary>
        /// <param name="biaPostBeneficiaryDetail">biaPostBeneficiaryDetail.</param>
        /// <returns>output.</returns>
        public int PostBeneficiaryDetail(BiaPostBeneficiaryDetail biaPostBeneficiaryDetail)
        {
            int insertRowsCount = 0;
            List<DbParameter> dbParams = new List<DbParameter>();

            dbParams.Add(new SqlParameter { ParameterName = "@user_id", Value = biaPostBeneficiaryDetail.Userid, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            dbParams.Add(new SqlParameter { ParameterName = "@ID", Value = biaPostBeneficiaryDetail.Id, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbParams.Add(new SqlParameter { ParameterName = "@verify_date", Value = biaPostBeneficiaryDetail.Verify_date, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
            dbParams.Add(new SqlParameter { ParameterName = "@verify_completion_date", Value = biaPostBeneficiaryDetail.Verify_completion_date, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
            dbParams.Add(new SqlParameter { ParameterName = "@is_Inspection_Verified", Value = biaPostBeneficiaryDetail.Is_Inspection_Verified, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbParams.Add(new SqlParameter { ParameterName = "@is_Verified", Value = biaPostBeneficiaryDetail.Is_Verified, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbParams.Add(new SqlParameter { ParameterName = "@current_comments", Value = biaPostBeneficiaryDetail.Current_comments, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbParams.Add(new SqlParameter { ParameterName = "@Assigned_by_name", Value = biaPostBeneficiaryDetail.Assigned_by_name, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbParams.Add(new SqlParameter { ParameterName = "@Assigned_by_designation", Value = biaPostBeneficiaryDetail.Assigned_by_designation, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });

            Dictionary<string, dynamic> result = SqlHelper.ExecuteNonQuery<SqlConnection>(spPostBeneficiaryDetails, dbParams, SqlHelper.ExecutionType.Procedure);

            insertRowsCount = result["RowsAffected"];
            return insertRowsCount;
        }

        /// <summary>
        /// GetNotificationAudits.
        /// </summary>
        /// <param name="userId">userId.</param>
        /// <returns>NotificationAudits.</returns>
        public List<BiaNotificationAudits> GetNotificationAudits(int userId)
        {
            List<BiaNotificationAudits> biaNotificationAudits = new List<BiaNotificationAudits>();
            List<DbParameter> parameters = new List<DbParameter>();
            parameters.Add(new SqlParameter { ParameterName = "@Query_name", Value = qnGetDirectorateSchemeDetail, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(spGetBIADtls, parameters, SqlHelper.ExecutionType.Procedure);
            if (dt != null && dt.Rows.Count > 0)
            {
                biaNotificationAudits = SqlHelper.ConvertDataTableToList<BiaNotificationAudits>(dt);
            }

            return biaNotificationAudits;
        }

        /// <summary>
        /// GetBeneficiaryNotification.
        /// </summary>
        /// <param name="userId">userId.</param>
        /// <returns>NotificationAudits.</returns>
        public List<BiaNotificationAudits> GetBeneficiaryNotification(int userId)
        {
            List<BiaNotificationAudits> biaNotificationAudits = new List<BiaNotificationAudits>();
            List<DbParameter> parameters = new List<DbParameter>();
            parameters.Add(new SqlParameter { ParameterName = "@Query_name", Value = qnGetBeneficiaryNotification, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@user_id", Value = userId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(spGetBIADtls, parameters, SqlHelper.ExecutionType.Procedure);
            if (dt != null && dt.Rows.Count > 0)
            {
                biaNotificationAudits = SqlHelper.ConvertDataTableToList<BiaNotificationAudits>(dt);
            }

            return biaNotificationAudits;
        }

        #endregion
    }
}
