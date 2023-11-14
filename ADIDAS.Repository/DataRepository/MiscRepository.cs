//------------------------------------------------------------------------------
// <copyright file="MiscRepository.cs" company="Government of Bihar">
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
    using System.Globalization;
    using System.Linq;
    using ADIDAS.Model.DTO;
    using ADIDAS.Model.Entities;
    using ADIDAS.Repository.Helper;
    using ADIDAS.Repository.Interfaces;
    using DepartmentOfAgriculture.Admin.Models.Models;
    using DIDAS.Model.Entities;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Options;

    /// <summary>
    /// MiscRepository.
    /// </summary>
    public class MiscRepository : BaseRepository, IMiscRepository
    {
        private readonly IOptions<DBSettings> options;
        private readonly ICropRepository cropRepository;

        private static string qnGetReports = "GetReports";
        private static string spGetReports = "usp_misc_cntrlr";
        private static string qnGetSeasonInfo = "GetBRBNSeason";
        private static string sp_GetSeasonInfo = "usp_dim_cntrlr";
        private static string qnGetAppLink = "GetAppLink";
        private static string qnGetAllDesignation = "GetAllDesignation";
        private static string qnGetSource = "GetSource";
        private static string spInsertScheme = "USP_Insert_brbn_scheme_Dim";
        private static string spInsertbrbnApplication = "Usp_insert_brbn_application_dim";
        private static string spInsertFarmerInfo = "USP_Trun_dbt_farmer_castecategory";
        private static string spInsertFarmerCate = "USP_Trun_dbt_farmers_category";
        private static string spInsertFarmerType = "USP_Trun_dbt_farmers_type";
        private static string tblInsertFarmerInfo = "dbt_farmers_castecategory";
        private static string tblInsertFarmerCate = "dbt_farmers_category";
        private static string tbl_InsertFarmerType = "dbt_farmers_type";
        private static string spmrgefarmers = "USP_Merge_dbt_farmers";
        private static string tblInsertbrbnApplicationStatus = "brbrn_application_status_temp";
        private static string spInsertbrbnApplicationStatus = "USP_Insert_brbn_application_status";
        private static string spGetdistrictCode = "usp_getdata_dir_district";
        private static string spGetOfmasSchemes = "usp_getdata_ofmas_scheme";
        private static string spGetOfmasBlock = "usp_getdata_ofmas_block";
        private static string spInsertSubsidyReport = "usp_dbt_subsidy_report";
        private static string tblInsertSubsidyReport = "DBT_Input_Subsidy_Report";
        private static string qnPostSourceSel = "PostSourceSel";
        private static string qnPostSourceIns = "PostSourceIns";
        private static string attrGamificationDistrictConfig = "GamificationDistrictConfig";
        private static string qnPostGamificationConfig = "PostGamificationConfig";
        private static string attrGamificationBlockConfig = "GamificationBlockConfig";
        private static string attrGamificationPanchConfig = "GamificationPanchConfig";
        private static string qnGetNotification = "GetNotification";
        private static string spGetNotificationAudits = "usp_notify_cntrlr";
        private static string spGetDOAInstructions = "USP_DOA_instruction_Info_Get";
        private static string qnPushNotification = "PushNotification";
        private static string qnDeleteNotification = "DeleteNotification";
        private static string qnDeleteNotificationAuditsAll = "DeleteNotificationAudits_All";
        private static string spFarmNameListAsync = "usp_insert_Govt_Farm_Mngmt";
        private static string spInsecticideListAsync = "USP_Insert_Pesticide_License_Info";
        private static string spInsertDistrictDim = "dbo.USP_Insert_District_Dim";
        private static string spInsertBlockDim = "dbo.USP_Insert_Block_Dim";
        private static string spInsertPanchayatDim = "dbo.USP_Insert_Panchayat_Dim";
        private static string spInsertVillageDim = "USP_Insert_Village_Dim";
        private static string spInsertOFMASScheme = "USP_Farm_Mech_Scheme";
        private static string spInsertOFMASAMRTDist = "USP_farm_mech_amrt_district";
        private static string spInsertOFMASBGREIDist = "USP_Farm_Mech_BGREI_district";
        private static string spInsertOFMASBGREIBlk = "USP_Farm_Mech_BGREI_block";
        private static string spInsertOFMASBGREIPAN = "USP_Farm_Mech_BGREI_panchayat";
        private static string spInsertOFMASNFSMDist = "USP_Farm_Mech_NFSM_district";
        private static string spInsertOFMASNFSMBlk = "USP_Farm_Mech_NFSM_block";
        private static string spInsertOFMASNFSMPAN = "USP_Farm_Mech_NFSM_panchayat";
        private static string spInsertOFMASNFSMOILSEEDSDist = "USP_Farm_Mech_NFSM_oilseeds_District";
        private static string spInsertOFMASNFSMOILSEEDSBlk = "USP_Farm_Mech_NFSM_oilseeds_block";
        private static string spInsertOFMASNFSMOILSEEDSPAN = "USP_Farm_Mech_NFSM_oilseeds_panchayat";
        private static string spInsertOFMASSMAMDist = "USP_Farm_Mech_SMAM_district";
        private static string spInsertOFMASSMAMBlk = "USP_Farm_Mech_SMAM_block";
        private static string spInsertOFMASSMAMPAN = "USP_Farm_Mech_SMAM_panchayat";
        private static string spInsertOFMASSMAMSCHCDist = "USP_Farm_Mech_SMAM_SCHC_district";
        private static string spInsertOFMASSMAMSCHCBlk = "USP_Farm_Mech_SMAM_SCHC_block";
        private static string spInsertOFMASSMAMSCHCPAN = "USP_Farm_Mech_SMAM_SCHC_panchayat";
        private static string spInsertOFMASKKADist = "USP_Farm_Mech_KKARt_District";
        private static string spInsertMIDetails = "usp_Insert_PMKSY_appln_data";
        private static string spInsertOFMAKKABlk = "USP_Farm_Mech_KKARt_block";
        private static string spInsertOFMASAMRTBlk = "USP_Farm_Mech_amrt_block";
        private static string spInsertOFMASAMRTPan = "USP_Farm_Mech_amrt_panchayat";
        private static string spInsertOFMAKKAPANk = "USP_Farm_Mech_KKARt_panchayat";
        private static string spGetReleaseData = "usp_getdata_release";
        private static string spGetBihanGuidlines = "usp_getdata_bihan_guidelines";
        private static string spGetDataFAQ = "usp_getdata_faq";
        private static string spGetPanchayatdim = "usp_dbt_subsidy_status";
        private static string spInsertSubsidyStatus = "USP_Insert_dbt_subsidy_status";
        private static string spGetPesticideLicenseInfo = "USP_Pesticide_License_Info_Get";
        private static string spInsertParaliDetails = "usp_insert_dbt_crop_residue";
        private static string spInsertSoilHealth = "usp_insert_SHC_district_metrics";
        private static string spInsertFarmer = "USP_Insert_Farmer";
        private static string spInsertbassoca = "usp_insert_dbt_BASSOCA_data";
        private static string spInsertHortiHybridSeedDetails = "usp_insert_dbt_seed_performance_hort";
        private static string spInsertFertilizerSeedLicense = "usp_Insert_Fertilizer_Seed_License";

        /// <summary>
        /// Initializes a new instance of the <see cref="MiscRepository"/> class.
        /// To initialize configuration options.
        /// </summary>
        /// <param name="config">config.</param>
        /// <param name="options">options.</param>
        public MiscRepository(IConfiguration config, IOptions<DBSettings> options, ICropRepository cropRepository)
        : base(config)
        {
            this.options = options;
            SqlHelper.SetConnectionString(this.options.Value.ConnectionString);
            this.cropRepository = cropRepository;
        }

        /// <summary>
        /// Get Reports.
        /// </summary>
        /// <returns>AppReport List.</returns>
        public List<AppReport> GetReports()
        {
            List<AppReport> appReport = new List<AppReport>();

            List<DbParameter> sqlParams = new List<DbParameter>();
            sqlParams.Add(new SqlParameter { ParameterName = "@query_name", Value = qnGetReports, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            sqlParams.Add(new SqlParameter { ParameterName = "@Attribute_Name", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            sqlParams.Add(new SqlParameter { ParameterName = "@Attribute_Value", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            sqlParams.Add(new SqlParameter { ParameterName = "@rec_created_userid", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            sqlParams.Add(new SqlParameter { ParameterName = "@rec_created_date", Value = DBNull.Value, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
            sqlParams.Add(new SqlParameter { ParameterName = "@rec_updated_userid", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            sqlParams.Add(new SqlParameter { ParameterName = "@rec_updated_date", Value = DBNull.Value, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });

            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(spGetReports, sqlParams, SqlHelper.ExecutionType.Procedure);

            if (dt != null && dt.Rows.Count > 0)
            {
                appReport = SqlHelper.ConvertDataTableToList<AppReport>(dt);
            }

            return appReport;
        }

        /// <summary>
        /// Get Season Info.
        /// </summary>
        /// <param name="season">season.</param>
        /// <returns>SeasonInfo list.</returns>
        public List<SeasonInfo> GetSeasonInfo(string season)
        {
            List<SeasonInfo> dTOSeasonInfo = new List<SeasonInfo>();

            List<DbParameter> sqlParams = new List<DbParameter>();
            sqlParams.Add(new SqlParameter { ParameterName = "@query_name", Value = qnGetSeasonInfo, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });

            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(sp_GetSeasonInfo, sqlParams, SqlHelper.ExecutionType.Procedure);

            if (dt != null && dt.Rows.Count > 0)
            {
                if (season == "Current")
                {
                    dTOSeasonInfo = SqlHelper.ConvertDataTableToList<SeasonInfo>(dt).Where(x => x.Season_Status == "Current Season").ToList();
                }
                else
                {
                    dTOSeasonInfo = SqlHelper.ConvertDataTableToList<SeasonInfo>(dt);
                }
            }

            return dTOSeasonInfo;
        }

        /// <summary>
        /// Get Farmer Info.
        /// </summary>
        /// <param name="mobileno">mobileno.</param>
        /// <returns>FarmerInfo list.</returns>
        public List<FarmerInfo> GetFarmerInfo(string mobileno)
        {
            List<FarmerInfo> dTOFarmerInfo = new List<FarmerInfo>();

            return dTOFarmerInfo;
        }

        /// <summary>
        /// Get App Link.
        /// </summary>
        /// <returns>AppLink list.</returns>
        public List<AppLink> GetAppLink()
        {
            List<AppLink> appLink = new List<AppLink>();

            List<DbParameter> sqlParams = new List<DbParameter>();
            sqlParams.Add(new SqlParameter { ParameterName = "@query_name", Value = qnGetAppLink, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            sqlParams.Add(new SqlParameter { ParameterName = "@Attribute_Name", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            sqlParams.Add(new SqlParameter { ParameterName = "@Attribute_Value", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            sqlParams.Add(new SqlParameter { ParameterName = "@rec_created_userid", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            sqlParams.Add(new SqlParameter { ParameterName = "@rec_created_date", Value = DBNull.Value, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
            sqlParams.Add(new SqlParameter { ParameterName = "@rec_updated_userid", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            sqlParams.Add(new SqlParameter { ParameterName = "@rec_updated_date", Value = DBNull.Value, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(spGetReports, sqlParams, SqlHelper.ExecutionType.Procedure);

            if (dt != null && dt.Rows.Count > 0)
            {
                appLink = SqlHelper.ConvertDataTableToList<AppLink>(dt);
            }

            return appLink;
        }

        /// <summary>
        /// Get All Designations.
        /// </summary>
        /// <returns>MobileAttributeConfig list.</returns>
        public List<MobileAttributeConfig> GetAllDesignations()
        {
            List<MobileAttributeConfig> list = new List<MobileAttributeConfig>();
            List<DbParameter> sqlParams = new List<DbParameter>();
            sqlParams.Add(new SqlParameter { ParameterName = "@query_name", Value = qnGetAllDesignation, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });

            sqlParams.Add(new SqlParameter { ParameterName = "@Attribute_Name", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            sqlParams.Add(new SqlParameter { ParameterName = "@Attribute_Value", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            sqlParams.Add(new SqlParameter { ParameterName = "@rec_created_userid", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            sqlParams.Add(new SqlParameter { ParameterName = "@rec_created_date", Value = DBNull.Value, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
            sqlParams.Add(new SqlParameter { ParameterName = "@rec_updated_userid", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            sqlParams.Add(new SqlParameter { ParameterName = "@rec_updated_date", Value = DBNull.Value, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });

            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(spGetReports, sqlParams, SqlHelper.ExecutionType.Procedure);
            if (dt != null && dt.Rows.Count > 0)
            {
                list = SqlHelper.ConvertDataTableToList<MobileAttributeConfig>(dt);
            }

            return list;
        }

        /// <summary>
        /// GetReceipientDesignations.
        /// </summary>
        /// <returns>List.</returns>
        public List<MobileAttributeConfig> GetReceipientDesignations()
        {
            List<MobileAttributeConfig> list = new List<MobileAttributeConfig>();
            List<DbParameter> sqlParams = new List<DbParameter>();
            sqlParams.Add(new SqlParameter { ParameterName = "@query_name", Value = "Recipientdesignations", SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });

            sqlParams.Add(new SqlParameter { ParameterName = "@Attribute_Name", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            sqlParams.Add(new SqlParameter { ParameterName = "@Attribute_Value", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            sqlParams.Add(new SqlParameter { ParameterName = "@rec_created_userid", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            sqlParams.Add(new SqlParameter { ParameterName = "@rec_created_date", Value = DBNull.Value, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
            sqlParams.Add(new SqlParameter { ParameterName = "@rec_updated_userid", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            sqlParams.Add(new SqlParameter { ParameterName = "@rec_updated_date", Value = DBNull.Value, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });

            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>("usp_misc_cntrlr", sqlParams, SqlHelper.ExecutionType.Procedure);

            if (dt != null && dt.Rows.Count > 0)
            {
                list = SqlHelper.ConvertDataTableToList<MobileAttributeConfig>(dt);
            }

            return list;
        }

        /// <summary>
        /// Get Source.
        /// </summary>
        /// <param name="attributeName">attributeName.</param>
        /// <returns>MobileAttributeConfig list.</returns>
        public List<MobileAttributeConfig> GetSource(string attributeName)
        {
            List<MobileAttributeConfig> list = new List<MobileAttributeConfig>();
            List<DbParameter> sqlParams = new List<DbParameter>();
            sqlParams.Add(new SqlParameter { ParameterName = "@query_name", Value = qnGetSource, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            sqlParams.Add(new SqlParameter { ParameterName = "@Attribute_Name", Value = attributeName, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            sqlParams.Add(new SqlParameter { ParameterName = "@Attribute_Value", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            sqlParams.Add(new SqlParameter { ParameterName = "@rec_created_userid", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            sqlParams.Add(new SqlParameter { ParameterName = "@rec_created_date", Value = DBNull.Value, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
            sqlParams.Add(new SqlParameter { ParameterName = "@rec_updated_userid", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            sqlParams.Add(new SqlParameter { ParameterName = "@rec_updated_date", Value = DBNull.Value, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });

            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(spGetReports, sqlParams, SqlHelper.ExecutionType.Procedure);
            if (dt != null && dt.Rows.Count > 0)
            {
                list = SqlHelper.ConvertDataTableToList<MobileAttributeConfig>(dt);
            }

            return list;
        }

        /// <summary>
        /// Insert Scheme.
        /// </summary>
        /// <param name="schemes">schemes.</param>
        /// <returns>no. of rows affected in integer.</returns>
        public int InsertScheme(List<Scheme> schemes)
        {
            Dictionary<string, dynamic> result;
            if (schemes.Any())
            {
                DataTable dtscheme = new DataTable();

                dtscheme.Columns.Add("scheme_code", typeof(string));
                dtscheme.Columns.Add("session_name", typeof(string));
                dtscheme.Columns.Add("scheme_type", typeof(string));
                dtscheme.Columns.Add("scheme_name", typeof(string));

                dtscheme.Columns.Add("district_lg_code", typeof(int));
                dtscheme.Columns.Add("block_lg_code", typeof(int));
                dtscheme.Columns.Add("panchayat_lg_code", typeof(int));

                foreach (var item in schemes)
                {
                    if (item.DistrictLGCode != string.Empty && item.BlockLGCode != string.Empty && item.PanchyatLGCode != string.Empty)
                    {
                        dtscheme.Rows.Add(item.SchemeCode.Trim(), item.SessionName.Trim(), item.SchemeType.Trim(), item.SchemeName.Trim(), Convert.ToInt32(item.DistrictLGCode), Convert.ToInt32(item.BlockLGCode), Convert.ToInt32(item.PanchyatLGCode));
                    }
                }

                if (dtscheme != null && dtscheme.Rows.Count > 0)
                {
                    string truncate = "TRUNCATE TABLE brbn_scheme_temp";
                    result = SqlHelper.ExecuteNonQuery<SqlConnection>(truncate, null, SqlHelper.ExecutionType.Query);

                    using (SqlConnection conn = new SqlConnection(this.options.Value.ConnectionString))
                    {
                        SqlTransaction transaction;
                        conn.Open();
                        transaction = conn.BeginTransaction();
                        using (SqlBulkCopy bulkCopy = new SqlBulkCopy(conn, SqlBulkCopyOptions.KeepIdentity, transaction))
                        {
                            bulkCopy.DestinationTableName = "brbn_scheme_temp";
                            bulkCopy.BulkCopyTimeout = 600;
                            bulkCopy.WriteToServer(dtscheme);
                        }

                        transaction.Commit();
                        conn.Close();
                    }

                    result = SqlHelper.ExecuteNonQuery<SqlConnection>(spInsertScheme, null, SqlHelper.ExecutionType.Procedure, 600);

                    return 1;
                }
            }

            return 0;
        }

        /// <summary>
        /// Insert Application.
        /// </summary>
        /// <param name="app">app.</param>
        /// <returns>no. of rows affected in integer.</returns>
        public int InsertbrbnApplication(List<BrbnApplication> app)
        {
            Dictionary<string, dynamic> result;
            if (app.Any())
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("application_no", typeof(long));
                dt.Columns.Add("scheme_code", typeof(string));
                dt.Columns.Add("session_name", typeof(string));
                dt.Columns.Add("scheme_type", typeof(string));
                dt.Columns.Add("scheme_name", typeof(string));

                dt.Columns.Add("district_lg_code", typeof(int));
                dt.Columns.Add("block_lg_code", typeof(int));
                dt.Columns.Add("panchayat_lg_code", typeof(int));
                dt.Columns.Add("registration_no", typeof(string));
                dt.Columns.Add("farmer_name", typeof(string));
                dt.Columns.Add("crop_season", typeof(string));
                dt.Columns.Add("crop_name", typeof(string));
                dt.Columns.Add("variety", typeof(string));
                dt.Columns.Add("distribution_dt", typeof(DateTime));
                dt.Columns.Add("seed_type", typeof(string));
                dt.Columns.Add("application_status", typeof(string));
                dt.Columns.Add("distributed_qty", typeof(decimal));

                dt.Columns.Add("delivery_type", typeof(string));
                dt.Columns.Add("requested_qty", typeof(decimal));
                dt.Columns.Add("phone_num", typeof(string));
                app.Where(x => x.ApplicationNo != string.Empty && x.DistrictLGCode != string.Empty && x.BlockLGCode != string.Empty && x.PanchyatLGCode != string.Empty).ToList().ForEach(item =>
                dt.Rows.Add(
                    Convert.ToInt64(item.ApplicationNo),
                    item.SchemeCode.Trim(),
                    item.SessionName.Trim(),
                    item.SchemeType.Trim(),
                    item.SchemeName.Trim(),
                    Convert.ToInt32(item.DistrictLGCode),
                    Convert.ToInt32(item.BlockLGCode),
                    Convert.ToInt32(item.PanchyatLGCode),
                    item.Registrationno.Trim(),
                    item.FarmerName.Trim(),
                    item.CropSeason.Trim(),
                    item.Crop.Trim(),
                    item.Verity.Trim(),
                    Convert.ToDateTime(DateTime.ParseExact(item.DistributionDate, "dd-MM-yyyy HH:mm:ss", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss")),
                    item.SeedType.Trim(),
                    item.Status.Trim(),
                    Convert.ToDecimal(item.Distributed),
                    item.DType.Trim(),
                    Convert.ToDecimal(item.Requested),
                    item.MobileNo.Trim()));

                if (dt != null && dt.Rows.Count > 0)
                {
                    string truncate = "TRUNCATE TABLE brbn_application_temp";
                    result = SqlHelper.ExecuteNonQuery<SqlConnection>(truncate, null, SqlHelper.ExecutionType.Query);
                    using (SqlConnection conn = new SqlConnection(this.options.Value.ConnectionString))
                    {
                        SqlTransaction transaction;
                        conn.Open();
                        transaction = conn.BeginTransaction();
                        using (SqlBulkCopy bulkCopy = new SqlBulkCopy(conn, SqlBulkCopyOptions.KeepIdentity, transaction))
                        {
                            bulkCopy.DestinationTableName = "brbn_application_temp";
                            bulkCopy.BulkCopyTimeout = 6000;
                            bulkCopy.BatchSize = 500;
                            bulkCopy.WriteToServer(dt);
                        }

                        transaction.Commit();
                        conn.Close();
                    }

                    result = SqlHelper.ExecuteNonQuery<SqlConnection>(spInsertbrbnApplication, null, SqlHelper.ExecutionType.Procedure, 600);

                    return 1;
                }
            }

            return 0;
        }

        /// <summary>
        /// Insert Farmer Info.
        /// </summary>
        /// <param name="dtCast">dtCast.</param>
        /// <param name="dtGender">dtGender.</param>
        /// <param name="dtType">dtType.</param>
        /// <returns>no. of rows affected in integer.</returns>
        public int InsertFarmerInfo(DataTable dtCast, DataTable dtGender, DataTable dtType)
        {
            Dictionary<string, dynamic> result;
            if (dtCast != null && dtCast.Rows.Count > 0 && dtGender != null && dtGender.Rows.Count > 0 && dtType != null && dtType.Rows.Count > 0)
            {
                using (SqlConnection conn = new SqlConnection(this.options.Value.ConnectionString))
                {
                    conn.Open();
                    result = SqlHelper.ExecuteNonQuery<SqlConnection>(spInsertFarmerInfo, null, SqlHelper.ExecutionType.Procedure);
                    result = SqlHelper.ExecuteNonQuery<SqlConnection>(spInsertFarmerCate, null, SqlHelper.ExecutionType.Procedure);
                    result = SqlHelper.ExecuteNonQuery<SqlConnection>(spInsertFarmerType, null, SqlHelper.ExecutionType.Procedure);

                    using (SqlBulkCopy bulkCopy = new SqlBulkCopy(conn))
                    {
                        bulkCopy.DestinationTableName = tblInsertFarmerInfo;
                        bulkCopy.BulkCopyTimeout = 600;

                        bulkCopy.WriteToServer(dtCast);
                    }

                    using (SqlBulkCopy bulkCopy = new SqlBulkCopy(conn))
                    {
                        bulkCopy.DestinationTableName = tblInsertFarmerCate;
                        bulkCopy.BulkCopyTimeout = 600;

                        bulkCopy.WriteToServer(dtGender);
                    }

                    using (SqlBulkCopy bulkCopy = new SqlBulkCopy(conn))
                    {
                        bulkCopy.DestinationTableName = tbl_InsertFarmerType;
                        bulkCopy.BulkCopyTimeout = 600;

                        bulkCopy.WriteToServer(dtType);
                    }

                    result = SqlHelper.ExecuteNonQuery<SqlConnection>(spmrgefarmers, null, SqlHelper.ExecutionType.Procedure, 600);
                }

                result = SqlHelper.ExecuteNonQuery<SqlConnection>(spmrgefarmers, null, SqlHelper.ExecutionType.Procedure, 600);

                return 1;
            }

            return 0;
        }

        /// <summary>
        /// Insert Application Status.
        /// </summary>
        /// <param name="appstatus">appstatus.</param>
        /// <returns>no. of rows affected in integer.</returns>
        public int InsertbrbnApplicationStatus(List<BrBnApplicationStatus> appstatus)
        {
            Dictionary<string, dynamic> result;
            if (appstatus.Any())
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("scheme", typeof(string));
                dt.Columns.Add("sub_scheme", typeof(string));

                dt.Columns.Add("district_name", typeof(string));
                dt.Columns.Add("block_name", typeof(string));
                dt.Columns.Add("panchayat_name", typeof(string));

                dt.Columns.Add("delivery_type", typeof(int));
                dt.Columns.Add("total_app_cnt", typeof(int));
                dt.Columns.Add("total_app_qty", typeof(decimal));
                dt.Columns.Add("total_approved_cnt", typeof(int));
                dt.Columns.Add("total_approved_qty", typeof(decimal));
                dt.Columns.Add("total_reject_cnt", typeof(int));
                dt.Columns.Add("total_reject_qty", typeof(decimal));
                dt.Columns.Add("total_distribution_cnt", typeof(int));
                dt.Columns.Add("total_distribution_qty", typeof(decimal));
                dt.Columns.Add("session_name", typeof(string));

                appstatus.ForEach(item =>
                dt.Rows.Add(
                  item.Scheme,
                  item.SUBScheme,
                  item.District,
                  item.Block,
                  item.Panchyat,
                  Convert.ToInt32(item.DType == "1" ? item.DType : "0"),
                  Convert.ToInt32(item.TotalApp),
                  Convert.ToDecimal(item.TotalAppQty),
                  Convert.ToInt32(item.TotalApproved),
                  Convert.ToDecimal(item.TotalApprovedQty),
                  Convert.ToInt32(item.TotalReject),
                  Convert.ToDecimal(item.TotalRejectQty),
                  Convert.ToInt32(item.TotalDistribution),
                  Convert.ToDecimal(item.TotalDistributionQty),
                  item.Session_name));

                if (dt != null && dt.Rows.Count > 0)
                {
                    string truncate = "TRUNCATE TABLE brbrn_application_status_temp";
                    result = SqlHelper.ExecuteNonQuery<SqlConnection>(truncate, null, SqlHelper.ExecutionType.Query);

                    using (SqlConnection conn = new SqlConnection(this.options.Value.ConnectionString))
                    {
                        SqlTransaction transaction;
                        conn.Open();
                        transaction = conn.BeginTransaction();
                        using (SqlBulkCopy bulkCopy = new SqlBulkCopy(conn, SqlBulkCopyOptions.KeepIdentity, transaction))
                        {
                            bulkCopy.DestinationTableName = tblInsertbrbnApplicationStatus;
                            bulkCopy.BulkCopyTimeout = 600;
                            bulkCopy.BatchSize = 50000;
                            bulkCopy.WriteToServer(dt);
                        }

                        transaction.Commit();

                        conn.Close();
                    }

                    result = SqlHelper.ExecuteNonQuery<SqlConnection>(spInsertbrbnApplicationStatus, null, SqlHelper.ExecutionType.Procedure, 600);

                    return 1;
                }
            }

            return 0;
        }

        /// <summary>
        /// Get district Code.
        /// </summary>
        /// <returns>District list.</returns>
        public List<District> GetdistrictCode()
        {
            List<District> resultset = new List<District>();

            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(spGetdistrictCode, null, SqlHelper.ExecutionType.Procedure);

            if (dt != null && dt.Rows.Count > 0)
            {
                resultset = SqlHelper.ConvertDataTableToList<District>(dt);
            }

            return resultset;
        }

        /// <summary>
        /// Get Ofmas Schemes.
        /// </summary>
        /// <returns>OfmasScheme list.</returns>
        public List<OfmasScheme> GetOfmasSchemes()
        {
            List<OfmasScheme> resultset = new List<OfmasScheme>();

            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(spGetOfmasSchemes, null, SqlHelper.ExecutionType.Procedure);

            if (dt != null && dt.Rows.Count > 0)
            {
                resultset = SqlHelper.ConvertDataTableToList<OfmasScheme>(dt);
            }

            return resultset;
        }

        /// <summary>
        /// Get Ofmas Block.
        /// </summary>
        /// <returns>PanchayatData list.</returns>
        public List<PanchayatData> GetOfmasBlock()
        {
            List<PanchayatData> resultset = new List<PanchayatData>();

            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(spGetOfmasBlock, null, SqlHelper.ExecutionType.Procedure);

            if (dt != null && dt.Rows.Count > 0)
            {
                resultset = SqlHelper.ConvertDataTableToList<PanchayatData>(dt);
            }

            return resultset;
        }

        /// <summary>
        /// Insert Subsidy Report.
        /// </summary>
        /// <param name="subsidyReport">subsidyReport.</param>
        /// <returns>no. of rows affected in integer.</returns>
        public int InsertSubsidyReport(dynamic subsidyReport)
        {
            if (subsidyReport != null)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("district_lg_code", typeof(int));
                dt.Columns.Add("block_lg_code", typeof(int));
                dt.Columns.Add("panchayat_lg_code", typeof(int));
                dt.Columns.Add("TotalNoFarmer", typeof(int));
                dt.Columns.Add("TotalNoApplied", typeof(int));
                dt.Columns.Add("TotalNoApproved", typeof(int));
                dt.Columns.Add("TotalNoPending", typeof(int));
                dt.Columns.Add("TotalNoDisbursed", typeof(int));
                dt.Columns.Add("TotalNoRejected", typeof(int));
                dt.Columns.Add("Rec_created_date", typeof(DateTime));
                dt.Columns.Add("Rec_created_user_id", typeof(int));

                foreach (var item in subsidyReport)
                {
                    dt.Rows.Add(
                                Convert.ToInt32(item.DistLgCode),
                                Convert.ToInt32(item.BlockLgCode),
                                Convert.ToInt32(item.PanchayatLgCode),
                                Convert.ToInt32(item.TotalNoFarmer),
                                Convert.ToInt32(item.TotalNoApplied),
                                Convert.ToInt32(item.TotalNoApproved),
                                Convert.ToInt32(item.TotalNoPending),
                                Convert.ToInt32(item.TotalNoDisbursed),
                                Convert.ToInt32(item.TotalNoRejected),
                                DateTime.Now,
                                null);
                }

                if (dt != null && dt.Rows.Count > 0)
                {
                    SqlHelper.ExecuteNonQuery<SqlConnection>(spInsertSubsidyReport, null, SqlHelper.ExecutionType.Procedure, 600);

                    using (SqlConnection conn = new SqlConnection(this.options.Value.ConnectionString))
                    {
                        SqlTransaction transaction;
                        conn.Open();
                        transaction = conn.BeginTransaction();
                        SqlCommand cmd = conn.CreateCommand();
                        cmd.Transaction = transaction;
                        using (SqlBulkCopy bulkCopy = new SqlBulkCopy(conn, SqlBulkCopyOptions.KeepIdentity, transaction))
                        {
                            bulkCopy.DestinationTableName = tblInsertSubsidyReport;
                            bulkCopy.BulkCopyTimeout = 600;

                            bulkCopy.WriteToServer(dt);
                        }

                        transaction.Commit();

                        conn.Close();
                    }
                }
            }

            return 1;
        }

        /// <summary>
        /// Post Source.
        /// </summary>
        /// <param name="appConfig">appConfig.</param>
        /// <returns>no. of rows affected in integer.</returns>
        public int PostSource(AppConfig appConfig)
        {
            List<DbParameter> dbparamsSource = new List<DbParameter>();
            List<DbParameter> sqlParamsPostsourceSel = new List<DbParameter>();
            sqlParamsPostsourceSel.Add(new SqlParameter { ParameterName = "@query_name", Value = qnPostSourceSel, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            sqlParamsPostsourceSel.Add(new SqlParameter { ParameterName = "@attribute_name", Value = appConfig.Attribute_Name, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            sqlParamsPostsourceSel.Add(new SqlParameter { ParameterName = "@attribute_Value", Value = appConfig.Attribute_Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            sqlParamsPostsourceSel.Add(new SqlParameter { ParameterName = "@rec_created_userid", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            sqlParamsPostsourceSel.Add(new SqlParameter { ParameterName = "@rec_created_date", Value = DBNull.Value, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
            sqlParamsPostsourceSel.Add(new SqlParameter { ParameterName = "@rec_updated_userid", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            sqlParamsPostsourceSel.Add(new SqlParameter { ParameterName = "@rec_updated_date", Value = DBNull.Value, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });

            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>("usp_misc_cntrlr", sqlParamsPostsourceSel, SqlHelper.ExecutionType.Procedure);

            if (dt == null || dt.Rows.Count == 0)
            {
                dbparamsSource.Add(new SqlParameter { ParameterName = "@query_name", Value = qnPostSourceIns, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsSource.Add(new SqlParameter { ParameterName = "@attribute_name", Value = appConfig.Attribute_Name, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsSource.Add(new SqlParameter { ParameterName = "@attribute_Value", Value = appConfig.Attribute_Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsSource.Add(new SqlParameter { ParameterName = "@rec_created_userid", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsSource.Add(new SqlParameter { ParameterName = "@rec_created_date", Value = DBNull.Value, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
                dbparamsSource.Add(new SqlParameter { ParameterName = "@rec_updated_userid", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsSource.Add(new SqlParameter { ParameterName = "@rec_updated_date", Value = DBNull.Value, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });

                Dictionary<string, dynamic> result = SqlHelper.ExecuteNonQuery<SqlConnection>(spGetReports, dbparamsSource, SqlHelper.ExecutionType.Procedure);

                return result["RowsAffected"];
            }

            return 0;
        }

        /// <summary>
        /// Post Gamification Config.
        /// </summary>
        /// <param name="config">config.</param>
        /// <returns>result integer.</returns>
        public int PostGamificationConfig(GamificationConfigDto config)
        {
            Dictionary<string, dynamic> result;
            List<DbParameter> dbparamsSource = new List<DbParameter>();

            dbparamsSource.Add(new SqlParameter { ParameterName = "@query_name", Value = qnPostGamificationConfig, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsSource.Add(new SqlParameter { ParameterName = "@attribute_name", Value = attrGamificationDistrictConfig, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsSource.Add(new SqlParameter { ParameterName = "@attribute_Value", Value = config.District, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsSource.Add(new SqlParameter { ParameterName = "@rec_created_userid", Value = config.UserId, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsSource.Add(new SqlParameter { ParameterName = "@rec_created_date", Value = config.Reccreateddate, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
            dbparamsSource.Add(new SqlParameter { ParameterName = "@rec_updated_userid", Value = config.UpdateduserId, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsSource.Add(new SqlParameter { ParameterName = "@rec_updated_date", Value = config.UpdatedDate, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });

            result = SqlHelper.ExecuteNonQuery<SqlConnection>(spGetReports, dbparamsSource, SqlHelper.ExecutionType.Procedure);

            List<DbParameter> dbparamsBlock = new List<DbParameter>();

            dbparamsBlock.Add(new SqlParameter { ParameterName = "@query_name", Value = qnPostGamificationConfig, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsBlock.Add(new SqlParameter { ParameterName = "@attribute_name", Value = attrGamificationBlockConfig, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsBlock.Add(new SqlParameter { ParameterName = "@attribute_Value", Value = config.Block, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsBlock.Add(new SqlParameter { ParameterName = "@rec_created_userid", Value = config.UserId, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsBlock.Add(new SqlParameter { ParameterName = "@rec_created_date", Value = config.Reccreateddate, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
            dbparamsBlock.Add(new SqlParameter { ParameterName = "@rec_updated_userid", Value = config.UpdateduserId, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsBlock.Add(new SqlParameter { ParameterName = "@rec_updated_date", Value = config.UpdatedDate, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });

            result = SqlHelper.ExecuteNonQuery<SqlConnection>("usp_misc_cntrlr", dbparamsBlock, SqlHelper.ExecutionType.Procedure);

            List<DbParameter> dbparamsPanch = new List<DbParameter>();

            dbparamsPanch.Add(new SqlParameter { ParameterName = "@query_name", Value = qnPostGamificationConfig, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsPanch.Add(new SqlParameter { ParameterName = "@attribute_name", Value = attrGamificationPanchConfig, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsPanch.Add(new SqlParameter { ParameterName = "@attribute_Value", Value = config.Panchayat, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsPanch.Add(new SqlParameter { ParameterName = "@rec_created_userid", Value = config.UserId, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsPanch.Add(new SqlParameter { ParameterName = "@rec_created_date", Value = config.Reccreateddate, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
            dbparamsPanch.Add(new SqlParameter { ParameterName = "@rec_updated_userid", Value = config.UpdateduserId, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsPanch.Add(new SqlParameter { ParameterName = "@rec_updated_date", Value = config.UpdatedDate, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });

            result = SqlHelper.ExecuteNonQuery<SqlConnection>(spGetReports, dbparamsPanch, SqlHelper.ExecutionType.Procedure);

            return 1;
        }

        /// <summary>
        /// Get Notification Audits.
        /// </summary>
        /// <returns>NotificationEntity list.</returns>
        public List<NotificationEntity> GetNotificationAudits()
        {
            List<NotificationEntity> list = new List<NotificationEntity>();
            List<DbParameter> dbparamsNotifInfo = new List<DbParameter>();
            dbparamsNotifInfo.Add(new SqlParameter { ParameterName = "@query_name", Value = qnGetNotification, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsNotifInfo.Add(new SqlParameter { ParameterName = "@Notification_id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsNotifInfo.Add(new SqlParameter { ParameterName = "@Name", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsNotifInfo.Add(new SqlParameter { ParameterName = "@Message", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsNotifInfo.Add(new SqlParameter { ParameterName = "@Mode", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsNotifInfo.Add(new SqlParameter { ParameterName = "@Tags", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsNotifInfo.Add(new SqlParameter { ParameterName = "@Type", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsNotifInfo.Add(new SqlParameter { ParameterName = "@Date", Value = DBNull.Value, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });

            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(spGetNotificationAudits, dbparamsNotifInfo, SqlHelper.ExecutionType.Procedure);
            if (dt != null && dt.Rows.Count > 0)
            {
                list = SqlHelper.ConvertDataTableToList<NotificationEntity>(dt);
            }

            return list;
        }

        /// <summary>
        /// Get DOA Instructions.
        /// </summary>
        /// <returns>DOAInstructions list.</returns>
        public List<DoaInstructions> GetDOAInstructions()
        {
            List<DoaInstructions> list = new List<DoaInstructions>();

            List<DbParameter> dbparamsNotifInfo = new List<DbParameter>();

            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(spGetDOAInstructions, dbparamsNotifInfo, SqlHelper.ExecutionType.Procedure);
            if (dt != null && dt.Rows.Count > 0)
            {
                list = SqlHelper.ConvertDataTableToList<DoaInstructions>(dt);
            }

            return list;
        }

        /// <summary>
        /// Insert Notification Audits.
        /// </summary>
        /// <param name="entity">entity.</param>
        /// <returns>result integer.</returns>
        public int InsertNotificationAudits(NotificationEntity entity)
        {
            List<DbParameter> dbparamsNotifInfo = new List<DbParameter>();
            dbparamsNotifInfo.Add(new SqlParameter { ParameterName = "@query_name", Value = qnPushNotification, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsNotifInfo.Add(new SqlParameter { ParameterName = "@Notification_id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsNotifInfo.Add(new SqlParameter { ParameterName = "@Name", Value = entity.Name, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsNotifInfo.Add(new SqlParameter { ParameterName = "@Message", Value = entity.Message, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsNotifInfo.Add(new SqlParameter { ParameterName = "@Mode", Value = entity.Mode, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsNotifInfo.Add(new SqlParameter { ParameterName = "@Tags", Value = entity.Tags, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsNotifInfo.Add(new SqlParameter { ParameterName = "@Type", Value = entity.Type, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsNotifInfo.Add(new SqlParameter { ParameterName = "@Date", Value = entity.Date, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });

            Dictionary<string, dynamic> result = SqlHelper.ExecuteNonQuery<SqlConnection>(spGetNotificationAudits, dbparamsNotifInfo, SqlHelper.ExecutionType.Procedure);
            int insertRowsCount = result["RowsAffected"];
            return insertRowsCount;
        }

        /// <summary>
        /// Delete Notification.
        /// </summary>
        /// <param name="id">id.</param>
        /// <returns>result integer.</returns>
        public int DeleteNotification(int id)
        {
            List<DbParameter> dbparamsNotifInfo = new List<DbParameter>();
            dbparamsNotifInfo.Add(new SqlParameter { ParameterName = "@query_name", Value = qnDeleteNotification, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsNotifInfo.Add(new SqlParameter { ParameterName = "@Notification_id", Value = id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsNotifInfo.Add(new SqlParameter { ParameterName = "@Name", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsNotifInfo.Add(new SqlParameter { ParameterName = "@Message", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsNotifInfo.Add(new SqlParameter { ParameterName = "@Mode", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsNotifInfo.Add(new SqlParameter { ParameterName = "@Tags", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsNotifInfo.Add(new SqlParameter { ParameterName = "@Type", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsNotifInfo.Add(new SqlParameter { ParameterName = "@Date", Value = DBNull.Value, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });

            Dictionary<string, dynamic> result = SqlHelper.ExecuteNonQuery<SqlConnection>(spGetNotificationAudits, dbparamsNotifInfo, SqlHelper.ExecutionType.Procedure);
            int insertRowsCount = result["RowsAffected"];

            return insertRowsCount;
        }

        /// <summary>
        /// Delete ALL Notifications.
        /// </summary>
        /// <returns>result integer.</returns>
        public int DeleteALLNotifications()
        {
            List<DbParameter> dbparamsNotifInfo = new List<DbParameter>();
            dbparamsNotifInfo.Add(new SqlParameter { ParameterName = "@query_name", Value = qnDeleteNotificationAuditsAll, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsNotifInfo.Add(new SqlParameter { ParameterName = "@Notification_id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsNotifInfo.Add(new SqlParameter { ParameterName = "@Name", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsNotifInfo.Add(new SqlParameter { ParameterName = "@Message", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsNotifInfo.Add(new SqlParameter { ParameterName = "@Mode", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsNotifInfo.Add(new SqlParameter { ParameterName = "@Tags", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsNotifInfo.Add(new SqlParameter { ParameterName = "@Type", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsNotifInfo.Add(new SqlParameter { ParameterName = "@Date", Value = DBNull.Value, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });

            Dictionary<string, dynamic> result = SqlHelper.ExecuteNonQuery<SqlConnection>(spGetNotificationAudits, dbparamsNotifInfo, SqlHelper.ExecutionType.Procedure);
            int insertRowsCount = result["RowsAffected"];

            return insertRowsCount;
        }

        /// <summary>
        /// Insecticide List Async.
        /// </summary>
        /// <param name="insecticideLicenceModels">insecticideLicenceModels.</param>
        /// <returns>result integer.</returns>
        public int InsecticideListAsync(List<InsecticideLicenceModel> insecticideLicenceModels)
        {
            Dictionary<string, dynamic> result = new Dictionary<string, dynamic>();
            if (insecticideLicenceModels.Any())
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("firmName", typeof(string));
                dt.Columns.Add("licenceNo", typeof(string));
                dt.Columns.Add("LicenceDate", typeof(DateTime));
                dt.Columns.Add("distcode", typeof(int));
                dt.Columns.Add("distname", typeof(string));

                foreach (var item in insecticideLicenceModels)
                {
                    dt.Rows.Add(item.FirmName, item.LicenceNo, item.LicenceDate, Convert.ToInt32(item.Distcode), item.DistName);
                }

                List<DbParameter> dbparam = new List<DbParameter>();
                dbparam.Add(new SqlParameter { ParameterName = "@PestlicenseDetails", Value = dt, SqlDbType = SqlDbType.Structured, Direction = ParameterDirection.Input });
                result = SqlHelper.ExecuteNonQuery<SqlConnection>(spInsecticideListAsync, dbparam, SqlHelper.ExecutionType.Procedure);
            }

            return 1;
        }

        /// <summary>
        /// Farm Name List Async.
        /// </summary>
        /// <param name="farmNameModels">farmNameModels.</param>
        /// <returns>result integer.</returns>
        public int FarmNameListAsync(List<FarmNameModel> farmNameModels)
        {
            Dictionary<string, dynamic> result = new Dictionary<string, dynamic>();
            if (farmNameModels.Any())
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("distname", typeof(string));
                dt.Columns.Add("farmName", typeof(string));
                foreach (var item in farmNameModels)
                {
                    dt.Rows.Add(item.DistName, item.FarmName);
                }

                List<DbParameter> dbparam = new List<DbParameter>();
                dbparam.Add(new SqlParameter { ParameterName = "@Govt_Farm_Mngmt", Value = dt, SqlDbType = SqlDbType.Structured, Direction = ParameterDirection.Input });
                result = SqlHelper.ExecuteNonQuery<SqlConnection>(spFarmNameListAsync, dbparam, SqlHelper.ExecutionType.Procedure);
            }

            return 1;
        }

        /// <summary>
        /// Insert District Dim.
        /// </summary>
        /// <param name="districtLGs">districtLGs.</param>
        /// <returns>result integer.</returns>
        public int InsertDistrictDim(List<DistrictLG> districtLGs)
        {
            Dictionary<string, dynamic> result = new Dictionary<string, dynamic>();
            if (districtLGs.Any())
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("district_lg_code", typeof(int));
                dt.Columns.Add("district_name", typeof(string));

                foreach (var item in districtLGs)
                {
                    dt.Rows.Add(item.DistLgCode, item.DistName);
                }

                List<DbParameter> dbparam = new List<DbParameter>();
                dbparam.Add(new SqlParameter { ParameterName = "@DistrictDetails", Value = dt, SqlDbType = SqlDbType.Structured, Direction = ParameterDirection.Input });
                result = SqlHelper.ExecuteNonQuery<SqlConnection>(spInsertDistrictDim, dbparam, SqlHelper.ExecutionType.Procedure);
            }

            return result["RowsAffected"];
        }

        /// <summary>
        /// Insert Block Dim.
        /// </summary>
        /// <param name="blockLG">blockLG.</param>
        /// <returns>result integer.</returns>
        public int InsertBlockDim(List<BlockLG> blockLG)
        {
            Dictionary<string, dynamic> result = new Dictionary<string, dynamic>();
            if (blockLG.Any())
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("district_id", typeof(int));
                dt.Columns.Add("district_lg_code", typeof(int));
                dt.Columns.Add("block_lg_code", typeof(int));
                dt.Columns.Add("block_name", typeof(string));

                foreach (var item in blockLG)
                {
                    dt.Rows.Add(DBNull.Value, item.DistLgCode, item.BlockLgCode, item.BlockName);
                }

                List<DbParameter> dbparam = new List<DbParameter>();
                dbparam.Add(new SqlParameter { ParameterName = "@BlockDetails", Value = dt, SqlDbType = SqlDbType.Structured, Direction = ParameterDirection.Input });
                result = SqlHelper.ExecuteNonQuery<SqlConnection>(spInsertBlockDim, dbparam, SqlHelper.ExecutionType.Procedure);
            }

            return result["RowsAffected"];
        }

        /// <summary>
        /// Insert Panchayat Dim.
        /// </summary>
        /// <param name="panchayatLG">panchayatLG.</param>
        /// <returns>result integer.</returns>
        public int InsertPanchayatDim(List<PanchayatLG> panchayatLG)
        {
            Dictionary<string, dynamic> result = new Dictionary<string, dynamic>();
            if (panchayatLG.Any())
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("district_id", typeof(int));
                dt.Columns.Add("block_id", typeof(int));
                dt.Columns.Add("district_lg_code", typeof(int));
                dt.Columns.Add("dist_name", typeof(string));
                dt.Columns.Add("block_lg_code", typeof(int));
                dt.Columns.Add("block_name", typeof(string));
                dt.Columns.Add("panchayat_lg_code", typeof(int));
                dt.Columns.Add("panchayat_name", typeof(string));

                foreach (var item in panchayatLG)
                {
                    dt.Rows.Add(DBNull.Value, DBNull.Value, item.DistLgCode, item.DistName, item.BlockLgCode, item.BlockName, item.PanchayatLgCode, item.PanchayatName);
                }

                //DataTable dtvillage = new DataTable();
                //dtvillage.Columns.Add("district_id", typeof(int));
                //dtvillage.Columns.Add("block_id", typeof(int));
                //dtvillage.Columns.Add("panchayat_id", typeof(int));
                //dtvillage.Columns.Add("district_lg_code", typeof(int));
                //dtvillage.Columns.Add("dist_name", typeof(string));
                //dtvillage.Columns.Add("block_lg_code", typeof(int));
                //dtvillage.Columns.Add("block_name", typeof(string));
                //dtvillage.Columns.Add("panchayat_lg_code", typeof(int));
                //dtvillage.Columns.Add("panchayat_name", typeof(string));
                //dtvillage.Columns.Add("village_lg_code", typeof(long));
                //dtvillage.Columns.Add("village_name", typeof(string));

                //foreach (var item in panchayatLG)
                //{
                //    dtvillage.Rows.Add(0, 0, 0, item.DistLgCode, item.DistName, item.BlockLgCode, item.BlockName, item.PanchayatLgCode, item.PanchayatName, item.Villagelgcode, item.Villagename);
                //}

                List<DbParameter> dbparams = new List<DbParameter>();
                dbparams.Add(new SqlParameter { ParameterName = "@PanchayatDetails", Value = dt, SqlDbType = SqlDbType.Structured, Direction = ParameterDirection.Input });
                result = SqlHelper.ExecuteNonQuery<SqlConnection>(spInsertPanchayatDim, dbparams, SqlHelper.ExecutionType.Procedure, 120000);

                //List<DbParameter> dbparam = new List<DbParameter>();
                //dbparam.Add(new SqlParameter { ParameterName = "@VillageDetails", Value = dtvillage, SqlDbType = SqlDbType.Structured, Direction = ParameterDirection.Input });
                //result = SqlHelper.ExecuteNonQuery<SqlConnection>(spInsertVillageDim, dbparam, SqlHelper.ExecutionType.Procedure, 120000);
            }

            return result["RowsAffected"];
        }

        /// <summary>
        /// Insert Village Dim.
        /// </summary>
        /// <param name="villageLG">villageLG.</param>
        /// <returns>result integer.</returns>
        public int InsertVillageDim(List<VillageLG> villageLG)
        {
            string lgCode = string.Empty;
            Dictionary<string, dynamic> result = new Dictionary<string, dynamic>();
            if (villageLG.Any())
            {
                DataTable dtvillage = new DataTable();
                dtvillage.Columns.Add("district_id", typeof(int));
                dtvillage.Columns.Add("block_id", typeof(int));
                dtvillage.Columns.Add("panchayat_id", typeof(int));
                dtvillage.Columns.Add("district_lg_code", typeof(int));
                dtvillage.Columns.Add("dist_name", typeof(string));
                dtvillage.Columns.Add("block_lg_code", typeof(int));
                dtvillage.Columns.Add("block_name", typeof(string));
                dtvillage.Columns.Add("panchayat_lg_code", typeof(int));
                dtvillage.Columns.Add("panchayat_name", typeof(string));
                dtvillage.Columns.Add("village_lg_code", typeof(long));
                dtvillage.Columns.Add("village_name", typeof(string));

                villageLG.ForEach(item =>
                {
                    if (item.DistLgCode != string.Empty && item.BlockLgCode != string.Empty && item.PanchayatLgCode != string.Empty && item.PanchayatLgCode != string.Empty)
                    {
                        dtvillage.Rows.Add(0, 0, 0, Convert.ToInt32(item.DistLgCode), item.DistName, Convert.ToInt32(item.BlockLgCode), item.BlockName, Convert.ToInt32(item.PanchayatLgCode), item.PanchayatName, Convert.ToInt64(item.Villagelgcode), item.Villagename);
                    }
                });

                List<DbParameter> dbparam = new List<DbParameter>();
                dbparam.Add(new SqlParameter { ParameterName = "@VillageDetails", Value = dtvillage, SqlDbType = SqlDbType.Structured, Direction = ParameterDirection.Input });
                result = SqlHelper.ExecuteNonQuery<SqlConnection>(spInsertVillageDim, dbparam, SqlHelper.ExecutionType.Procedure, 120000);
            }

            return result["RowsAffected"];
        }

        /// <summary>
        /// Insert OFMAS Scheme.
        /// </summary>
        /// <param name="dt">dt.</param>
        /// <returns>no. of rows affected in integer.</returns>
        public int InsertOFMASScheme(DataTable dt)
        {
            List<DbParameter> dbparams = new List<DbParameter>();
            dbparams.Add(new SqlParameter { ParameterName = "@Scheme_Wise_Details", Value = dt, SqlDbType = SqlDbType.Structured, Direction = ParameterDirection.Input });
            Dictionary<string, dynamic> result = SqlHelper.ExecuteNonQuery<SqlConnection>(spInsertOFMASScheme, dbparams, SqlHelper.ExecutionType.Procedure);

            return result["RowsAffected"];
        }

        /// <summary>
        /// Insert OFMASAMRT Dist.
        /// </summary>
        /// <param name="dt">dt.</param>
        /// <returns>no. of rows affected in integer.</returns>
        public int InsertOFMASAMRTDist(DataTable dt)
        {
            List<DbParameter> dbparams = new List<DbParameter>();
            dbparams.Add(new SqlParameter { ParameterName = "@farm_mech_amrt_district", Value = dt, SqlDbType = SqlDbType.Structured, Direction = ParameterDirection.Input });
            Dictionary<string, dynamic> result = SqlHelper.ExecuteNonQuery<SqlConnection>(spInsertOFMASAMRTDist, dbparams, SqlHelper.ExecutionType.Procedure);

            return result["RowsAffected"];
        }

        /// <summary>
        /// Insert OFMASBGREI Dist.
        /// </summary>
        /// <param name="dt">dt.</param>
        /// <returns>no. of rows affected in integer.</returns>
        public int InsertOFMASBGREIDist(DataTable dt)
        {
            List<DbParameter> dbparams = new List<DbParameter>();
            dbparams.Add(new SqlParameter { ParameterName = "@BGREI_district_Wise_Details", Value = dt, SqlDbType = SqlDbType.Structured, Direction = ParameterDirection.Input });
            Dictionary<string, dynamic> result = SqlHelper.ExecuteNonQuery<SqlConnection>(spInsertOFMASBGREIDist, dbparams, SqlHelper.ExecutionType.Procedure);

            return result["RowsAffected"];
        }

        /// <summary>
        /// Insert OFMASBGREI Bulk.
        /// </summary>
        /// <param name="dt">dt.</param>
        /// <returns>no. of rows affected in integer.</returns>
        public int InsertOFMASBGREIBlk(DataTable dt)
        {
            List<DbParameter> dbparams = new List<DbParameter>();
            dbparams.Add(new SqlParameter { ParameterName = "@BGREI_block_Wise_Details", Value = dt, SqlDbType = SqlDbType.Structured, Direction = ParameterDirection.Input });
            Dictionary<string, dynamic> result = SqlHelper.ExecuteNonQuery<SqlConnection>(spInsertOFMASBGREIBlk, dbparams, SqlHelper.ExecutionType.Procedure);

            return result["RowsAffected"];
        }

        /// <summary>
        /// Insert OFMASBGREIPAN.
        /// </summary>
        /// <param name="dt">dt.</param>
        /// <returns>no. of rows affected in integer.</returns>
        public int InsertOFMASBGREIPAN(DataTable dt)
        {
            List<DbParameter> dbparams = new List<DbParameter>();
            dbparams.Add(new SqlParameter { ParameterName = "@BGREI_panchayat_Wise_Details", Value = dt, SqlDbType = SqlDbType.Structured, Direction = ParameterDirection.Input });
            Dictionary<string, dynamic> result = SqlHelper.ExecuteNonQuery<SqlConnection>(spInsertOFMASBGREIPAN, dbparams, SqlHelper.ExecutionType.Procedure);

            return result["RowsAffected"];
        }

        /// <summary>
        /// Insert OFMASNFSM Dist.
        /// </summary>
        /// <param name="dt">dt.</param>
        /// <returns>no. of rows affected in integer.</returns>
        public int InsertOFMASNFSMDist(DataTable dt)
        {
            List<DbParameter> dbparams = new List<DbParameter>();
            dbparams.Add(new SqlParameter { ParameterName = "@NFSM_district_Wise_Details", Value = dt, SqlDbType = SqlDbType.Structured, Direction = ParameterDirection.Input });
            Dictionary<string, dynamic> result = SqlHelper.ExecuteNonQuery<SqlConnection>(spInsertOFMASNFSMDist, dbparams, SqlHelper.ExecutionType.Procedure);

            return result["RowsAffected"];
        }

        /// <summary>
        /// Insert OFMASNFSM Bulk.
        /// </summary>
        /// <param name="dt">dt.</param>
        /// <returns>no. of rows affected in integer.</returns>
        public int InsertOFMASNFSMBlk(DataTable dt)
        {
            List<DbParameter> dbparams = new List<DbParameter>();
            dbparams.Add(new SqlParameter { ParameterName = "@NFSM_block_Wise_Details", Value = dt, SqlDbType = SqlDbType.Structured, Direction = ParameterDirection.Input });
            Dictionary<string, dynamic> result = SqlHelper.ExecuteNonQuery<SqlConnection>(spInsertOFMASNFSMBlk, dbparams, SqlHelper.ExecutionType.Procedure);

            return result["RowsAffected"];
        }

        /// <summary>
        /// Insert OFMASNFSMPAN.
        /// </summary>
        /// <param name="dt">dt.</param>
        /// <returns>no. of rows affected in integer.</returns>
        public int InsertOFMASNFSMPAN(DataTable dt)
        {
            List<DbParameter> dbparams = new List<DbParameter>();
            dbparams.Add(new SqlParameter { ParameterName = "@NFSM_panchayat_Wise_Details", Value = dt, SqlDbType = SqlDbType.Structured, Direction = ParameterDirection.Input });
            Dictionary<string, dynamic> result = SqlHelper.ExecuteNonQuery<SqlConnection>(spInsertOFMASNFSMPAN, dbparams, SqlHelper.ExecutionType.Procedure);

            return result["RowsAffected"];
        }

        /// <summary>
        /// Insert OFMASNFSMOILSEEDSDist.
        /// </summary>
        /// <param name="dt">dt.</param>
        /// <returns>no. of rows affected in integer.</returns>
        public int InsertOFMASNFSMOILSEEDSDist(DataTable dt)
        {
            List<DbParameter> dbparams = new List<DbParameter>();
            dbparams.Add(new SqlParameter { ParameterName = "@NFSM_oilseeds_District_Wise_Details", Value = dt, SqlDbType = SqlDbType.Structured, Direction = ParameterDirection.Input });
            Dictionary<string, dynamic> result = SqlHelper.ExecuteNonQuery<SqlConnection>(spInsertOFMASNFSMOILSEEDSDist, dbparams, SqlHelper.ExecutionType.Procedure);

            return result["RowsAffected"];
        }

        /// <summary>
        /// Insert OFMASNFSMOILSEEDSBulk.
        /// </summary>
        /// <param name="dt">dt.</param>
        /// <returns>no. of rows affected in integer.</returns>
        public int InsertOFMASNFSMOILSEEDSBlk(DataTable dt)
        {
            List<DbParameter> dbparams = new List<DbParameter>();
            dbparams.Add(new SqlParameter { ParameterName = "@NFSM_oilseeds_block_Wise_Details", Value = dt, SqlDbType = SqlDbType.Structured, Direction = ParameterDirection.Input });
            Dictionary<string, dynamic> result = SqlHelper.ExecuteNonQuery<SqlConnection>(spInsertOFMASNFSMOILSEEDSBlk, dbparams, SqlHelper.ExecutionType.Procedure);

            return result["RowsAffected"];
        }

        /// <summary>
        /// Insert OFMASNFSMOILSEEDSPAN.
        /// </summary>
        /// <param name="dt">dt.</param>
        /// <returns>no. of rows affected in integer.</returns>
        public int InsertOFMASNFSMOILSEEDSPAN(DataTable dt)
        {
            List<DbParameter> dbparams = new List<DbParameter>();
            dbparams.Add(new SqlParameter { ParameterName = "@NFSM_oilseeds_panchayat_Wise_Details", Value = dt, SqlDbType = SqlDbType.Structured, Direction = ParameterDirection.Input });
            Dictionary<string, dynamic> result = SqlHelper.ExecuteNonQuery<SqlConnection>(spInsertOFMASNFSMOILSEEDSPAN, dbparams, SqlHelper.ExecutionType.Procedure);

            return result["RowsAffected"];
        }

        /// <summary>
        /// Insert OFMASSMAMDist.
        /// </summary>
        /// <param name="dt">dt.</param>
        /// <returns>no. of rows affected in integer.</returns>
        public int InsertOFMASSMAMDist(DataTable dt)
        {
            List<DbParameter> dbparams = new List<DbParameter>();
            dbparams.Add(new SqlParameter { ParameterName = "@SMAM_District_Wise_Details", Value = dt, SqlDbType = SqlDbType.Structured, Direction = ParameterDirection.Input });
            Dictionary<string, dynamic> result = SqlHelper.ExecuteNonQuery<SqlConnection>(spInsertOFMASSMAMDist, dbparams, SqlHelper.ExecutionType.Procedure);

            return result["RowsAffected"];
        }

        /// <summary>
        /// Insert OFMASSMAMBulk.
        /// </summary>
        /// <param name="dt">dt.</param>
        /// <returns>no. of rows affected in integer.</returns>
        public int InsertOFMASSMAMBlk(DataTable dt)
        {
            List<DbParameter> dbparams = new List<DbParameter>();
            dbparams.Add(new SqlParameter { ParameterName = "@SMAM_block_Wise_Details", Value = dt, SqlDbType = SqlDbType.Structured, Direction = ParameterDirection.Input });
            Dictionary<string, dynamic> result = SqlHelper.ExecuteNonQuery<SqlConnection>(spInsertOFMASSMAMBlk, dbparams, SqlHelper.ExecutionType.Procedure);

            return result["RowsAffected"];
        }

        /// <summary>
        /// Insert OFMASSMAMPAN.
        /// </summary>
        /// <param name="dt">dt.</param>
        /// <returns>no. of rows affected in integer.</returns>
        public int InsertOFMASSMAMPAN(DataTable dt)
        {
            List<DbParameter> dbparams = new List<DbParameter>();
            dbparams.Add(new SqlParameter { ParameterName = "@SMAM_panchayat_Wise_Details", Value = dt, SqlDbType = SqlDbType.Structured, Direction = ParameterDirection.Input });
            Dictionary<string, dynamic> result = SqlHelper.ExecuteNonQuery<SqlConnection>(spInsertOFMASSMAMPAN, dbparams, SqlHelper.ExecutionType.Procedure);

            return result["RowsAffected"];
        }

        /// <summary>
        /// Insert OFMASSMAMSCHCDist.
        /// </summary>
        /// <param name="dt">dt.</param>
        /// <returns>no. of rows affected in integer.</returns>
        public int InsertOFMASSMAMSCHCDist(DataTable dt)
        {
            List<DbParameter> dbparams = new List<DbParameter>();
            dbparams.Add(new SqlParameter { ParameterName = "@SMAM_SCHC_District_Wise_Details", Value = dt, SqlDbType = SqlDbType.Structured, Direction = ParameterDirection.Input });
            Dictionary<string, dynamic> result = SqlHelper.ExecuteNonQuery<SqlConnection>(spInsertOFMASSMAMSCHCDist, dbparams, SqlHelper.ExecutionType.Procedure);

            return result["RowsAffected"];
        }

        /// <summary>
        /// Insert OFMASSMAMSCHCBulk.
        /// </summary>
        /// <param name="dt">dt.</param>
        /// <returns>no. of rows affected in integer.</returns>
        public int InsertOFMASSMAMSCHCBlk(DataTable dt)
        {
            List<DbParameter> dbparams = new List<DbParameter>();
            dbparams.Add(new SqlParameter { ParameterName = "@SMAM_SCHC_block_Wise_Details", Value = dt, SqlDbType = SqlDbType.Structured, Direction = ParameterDirection.Input });
            Dictionary<string, dynamic> result = SqlHelper.ExecuteNonQuery<SqlConnection>(spInsertOFMASSMAMSCHCBlk, dbparams, SqlHelper.ExecutionType.Procedure);

            return result["RowsAffected"];
        }

        /// <summary>
        /// Insert OFMASSMAMSCHCPAN.
        /// </summary>
        /// <param name="dt">dt.</param>
        /// <returns>no. of rows affected in integer.</returns>
        public int InsertOFMASSMAMSCHCPAN(DataTable dt)
        {
            List<DbParameter> dbparams = new List<DbParameter>();
            dbparams.Add(new SqlParameter { ParameterName = "@SMAM_SCHC_panchayat_Wise_Details", Value = dt, SqlDbType = SqlDbType.Structured, Direction = ParameterDirection.Input });
            Dictionary<string, dynamic> result = SqlHelper.ExecuteNonQuery<SqlConnection>(spInsertOFMASSMAMSCHCPAN, dbparams, SqlHelper.ExecutionType.Procedure);

            return result["RowsAffected"];
        }

        /// <summary>
        /// Insert OFMASKKADist.
        /// </summary>
        /// <param name="dt">dt.</param>
        /// <returns>no. of rows affected in integer.</returns>
        public int InsertOFMASKKADist(DataTable dt)
        {
            List<DbParameter> dbparams = new List<DbParameter>();
            dbparams.Add(new SqlParameter { ParameterName = "@KKARt_District_Wise_Details", Value = dt, SqlDbType = SqlDbType.Structured, Direction = ParameterDirection.Input });
            Dictionary<string, dynamic> result = SqlHelper.ExecuteNonQuery<SqlConnection>(spInsertOFMASKKADist, dbparams, SqlHelper.ExecutionType.Procedure);

            return result["RowsAffected"];
        }

        /// <summary>
        /// Insert MI Details.
        /// </summary>
        /// <param name="dt">dt.</param>
        /// <returns>no. of rows affected in integer.</returns>
        public int InsertMIDetails(DataTable dt)
        {
            List<DbParameter> dbparams = new List<DbParameter>();
            dbparams.Add(new SqlParameter { ParameterName = "@PMKSY_application", Value = dt, SqlDbType = SqlDbType.Structured, Direction = ParameterDirection.Input });
            Dictionary<string, dynamic> result = SqlHelper.ExecuteNonQuery<SqlConnection>(spInsertMIDetails, dbparams, SqlHelper.ExecutionType.Procedure);

            return result["RowsAffected"];
        }

        /// <summary>
        /// Insert OFMAKKABlk.
        /// </summary>  
        /// <param name="dt">dt.</param>
        /// <returns>no. of rows affected in integer.</returns>
        public int InsertOFMAKKABlk(DataTable dt)
        {
            List<DbParameter> dbparams = new List<DbParameter>();
            dbparams.Add(new SqlParameter { ParameterName = "@KKARt_block_Wise_Details", Value = dt, SqlDbType = SqlDbType.Structured, Direction = ParameterDirection.Input });
            Dictionary<string, dynamic> result = SqlHelper.ExecuteNonQuery<SqlConnection>(spInsertOFMAKKABlk, dbparams, SqlHelper.ExecutionType.Procedure);

            return result["RowsAffected"];
        }

        /// <summary>
        /// Insert OFMASAMRTBlk.
        /// </summary>
        /// <param name="dt">dt.</param>
        /// <returns>no. of rows affected in integer.</returns>
        public int InsertOFMASAMRTBlk(DataTable dt)
        {
            List<DbParameter> dbparams = new List<DbParameter>();
            dbparams.Add(new SqlParameter { ParameterName = "@amrt_block_Wise_Details", Value = dt, SqlDbType = SqlDbType.Structured, Direction = ParameterDirection.Input });
            Dictionary<string, dynamic> result = SqlHelper.ExecuteNonQuery<SqlConnection>(spInsertOFMASAMRTBlk, dbparams, SqlHelper.ExecutionType.Procedure);

            return result["RowsAffected"];
        }

        /// <summary>
        /// Insert OFMASNFSMOILSEEDSPan.
        /// </summary>
        /// <param name="dt">dt.</param>
        /// <returns>no. of rows affected in integer.</returns>
        public int InsertOFMASAMRTPan(DataTable dt)
        {
            List<DbParameter> dbparams = new List<DbParameter>();
            dbparams.Add(new SqlParameter { ParameterName = "@amrt_panchayat_Wise_Details", Value = dt, SqlDbType = SqlDbType.Structured, Direction = ParameterDirection.Input });
            Dictionary<string, dynamic> result = SqlHelper.ExecuteNonQuery<SqlConnection>(spInsertOFMASAMRTPan, dbparams, SqlHelper.ExecutionType.Procedure);

            return result["RowsAffected"];
        }

        /// <summary>
        /// Insert OFMAKKAPANK.
        /// </summary>
        /// <param name="dt">dt.</param>
        /// <returns>no. of rows affected in integer.</returns>
        public int InsertOFMAKKAPANk(DataTable dt)
        {
            List<DbParameter> dbparams = new List<DbParameter>();
            dbparams.Add(new SqlParameter { ParameterName = "@KKARt_panchayat_Wise_Details", Value = dt, SqlDbType = SqlDbType.Structured, Direction = ParameterDirection.Input });
            Dictionary<string, dynamic> result = SqlHelper.ExecuteNonQuery<SqlConnection>(spInsertOFMAKKAPANk, dbparams, SqlHelper.ExecutionType.Procedure);

            return result["RowsAffected"];
        }

        /// <summary>
        /// Get Release Data.
        /// </summary>
        /// <returns>Release.</returns>
        public Release GetReleaseData()
        {
            Release release = new Release();
            DataSet dataSet = SqlHelper.ExecuteDataSet<SqlConnection>(spGetReleaseData, null, SqlHelper.ExecutionType.Procedure);

            if (dataSet != null && dataSet.Tables.Count > 0)
            {
                release = SqlHelper.ConvertDataTableToList<Release>(dataSet.Tables[0]).FirstOrDefault();
            }

            return release;
        }

        /// <summary>
        /// Get Bihan Guidlines.
        /// </summary>
        /// <returns>BihanGuidelineModel list.</returns>
        public List<BihanGuidelineModel> GetBihanGuidlines()
        {
            List<BihanGuidelineModel> result = new List<BihanGuidelineModel>();
            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(spGetBihanGuidlines, null, SqlHelper.ExecutionType.Procedure);

            if (dt != null && dt.Rows.Count > 0)
            {
                result = SqlHelper.ConvertDataTableToList<BihanGuidelineModel>(dt);
            }

            return result;
        }

        /// <summary>
        /// Get Data FAQ.
        /// </summary>
        /// <returns>dynamic object.</returns>
        public dynamic GetDataFAQ()
        {
            dynamic result = null;
            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(spGetDataFAQ, null, SqlHelper.ExecutionType.Procedure);

            if (dt != null && dt.Rows.Count > 0)
            {
                result = SqlHelper.ConvertDataTableToList<Faq>(dt);
            }

            return result;
        }

        /// <summary>
        /// Get Panchayat dim.
        /// </summary>
        /// <returns>PanchayatData list.</returns>
        public List<PanchayatData> GetPanchayatdim()
        {
            List<PanchayatData> resultset = new List<PanchayatData>();

            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(spGetPanchayatdim, null, SqlHelper.ExecutionType.Procedure);

            if (dt != null && dt.Rows.Count > 0)
            {
                resultset = SqlHelper.ConvertDataTableToList<PanchayatData>(dt);
            }

            return resultset;
        }

        /// <summary>
        /// Insert Subsidy Status.
        /// </summary>
        /// <param name="dt">dt.</param>
        /// <returns>no. of rows affected in integer.</returns>
        public int InsertSubsidyStatus(DataTable dt)
        {
            List<DbParameter> dbparams = new List<DbParameter>();
            dbparams.Add(new SqlParameter { ParameterName = "@subsidy_details", Value = dt, SqlDbType = SqlDbType.Structured, Direction = ParameterDirection.Input });
            Dictionary<string, dynamic> result = SqlHelper.ExecuteNonQuery<SqlConnection>(spInsertSubsidyStatus, dbparams, SqlHelper.ExecutionType.Procedure, 120000);

            return result["RowsAffected"];
        }

        /// <summary>
        /// Get Pesticide License Info.
        /// </summary>
        /// <returns>PesticideInfo list.</returns>
        public List<PesticideInfo> GetPesticideLicenseInfo()
        {
            List<PesticideInfo> info = new List<PesticideInfo>();

            List<DbParameter> sqlParams = new List<DbParameter>();

            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(spGetPesticideLicenseInfo, sqlParams, SqlHelper.ExecutionType.Procedure);

            if (dt != null && dt.Rows.Count > 0)
            {
                info = SqlHelper.ConvertDataTableToList<PesticideInfo>(dt);
            }

            return info;
        }

        /// <summary>
        /// Insert Parali Details.
        /// </summary>
        /// <param name="dt">dt.</param>
        /// <returns>no. of rows affected in integer.</returns>
        public int InsertParaliDetails(DataTable dt)
        {
            List<DbParameter> dbparams = new List<DbParameter>();
            dbparams.Add(new SqlParameter { ParameterName = "@dbt_crop_residue", Value = dt, SqlDbType = SqlDbType.Structured, Direction = ParameterDirection.Input });
            Dictionary<string, dynamic> result = SqlHelper.ExecuteNonQuery<SqlConnection>(spInsertParaliDetails, dbparams, SqlHelper.ExecutionType.Procedure, 120000);


            return 1;
        }

        /// <summary>
        /// Insert Soil Health.
        /// </summary>
        /// <param name="dt">dt.</param>
        /// <returns>no. of rows affected in integer.</returns>
        public int InsertSoilHealth(DataTable dt)
        {
            List<DbParameter> dbparams = new List<DbParameter>();
            dbparams.Add(new SqlParameter { ParameterName = "@SHC_district_metrics", Value = dt, SqlDbType = SqlDbType.Structured, Direction = ParameterDirection.Input });
            Dictionary<string, dynamic> result = SqlHelper.ExecuteNonQuery<SqlConnection>(spInsertSoilHealth, dbparams, SqlHelper.ExecutionType.Procedure, 120000);


            return 1;
        }

        /// <summary>
        /// Insert Soil Health.
        /// </summary>
        /// <param name="dt">dt.</param>
        /// <returns>no. of rows affected in integer.</returns>
        public int InsertFarmer(DataTable dt)
        {
            List<DbParameter> dbparams = new List<DbParameter>();
            dbparams.Add(new SqlParameter { ParameterName = "@Mobile_Wise_Details", Value = dt, SqlDbType = SqlDbType.Structured, Direction = ParameterDirection.Input });
            Dictionary<string, dynamic> result = SqlHelper.ExecuteNonQuery<SqlConnection>(spInsertFarmer, dbparams, SqlHelper.ExecutionType.Procedure);

            return result["RowsAffected"];
        }

        /// <summary>
        /// Insert bassoca.
        /// </summary>
        /// <param name="dt">dt.</param>
        /// <returns>no. of rows affected in integer.</returns>
        public int Insertbassoca(DataTable dt)
        {
            List<DbParameter> dbparams = new List<DbParameter>();
            dbparams.Add(new SqlParameter { ParameterName = "@BASSOCA_dbt", Value = dt, SqlDbType = SqlDbType.Structured, Direction = ParameterDirection.Input });
            Dictionary<string, dynamic> result = SqlHelper.ExecuteNonQuery<SqlConnection>(spInsertbassoca, dbparams, SqlHelper.ExecutionType.Procedure, 120000);

            return 1;
        }

        /// <summary>
        /// Insert Horti HybridSeed Details.
        /// </summary>
        /// <param name="details">dynamic object details.</param>
        /// <returns>no. of rows affected in integer.</returns>
        public int InsertHortiHybridSeedDetails(dynamic details)
        {
            List<DbParameter> dbparams = new List<DbParameter>();
            dbparams.Add(new SqlParameter { ParameterName = "@scheme_name", Value = details._SchemeName, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@season_name", Value = details._Season, SqlDbType = SqlDbType.NVarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@application_no", Value = details._ApplicationId, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@registration_no", Value = Convert.ToInt64(details._DBTRegNo), SqlDbType = SqlDbType.BigInt, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@farmer_name", Value = details._Name, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@Mobile_no", Value = details._MobileNo, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@district_lg_code", Value = Convert.ToInt32(details._DistCode1), SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@district_name", Value = details._DistName1, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@block_lg_code", Value = Convert.ToInt32(details._BlockCode1), SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@block_name", Value = details._BlockName1, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@panchayat_lg_code", Value = Convert.ToInt32(details._PanchayatCode1), SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@panchayat_name", Value = details._PanchayatName1, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@Company_name", Value = details._DealerName, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@crop_name", Value = details._CropName, SqlDbType = SqlDbType.NVarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@Area_in_ha", Value = Convert.ToDecimal(details._Area_in_ha), SqlDbType = SqlDbType.Decimal, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@Seed_qty_grams", Value = Convert.ToDecimal(details._SeedQty_in_grams), SqlDbType = SqlDbType.Decimal, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@Response_flag", SqlDbType = SqlDbType.VarChar, Size = 100, Direction = ParameterDirection.Output });
            Dictionary<string, dynamic> result = SqlHelper.ExecuteNonQuery<SqlConnection>(spInsertHortiHybridSeedDetails, dbparams, SqlHelper.ExecutionType.Procedure);


            return 1;
        }

        /// <summary>
        /// OFMAS State Plan scheme panchayat data.
        /// </summary>
        /// <param name="dt">dt</param>
        /// <returns>InsertOFMASStatePlanPan</returns>
        public int InsertOFMASStatePlanPan(DataTable dt)
        {
            List<DbParameter> dbparams = new List<DbParameter>();
            dbparams.Add(new SqlParameter { ParameterName = "@State_Plan_panchayat_Wise_Details", Value = dt, SqlDbType = SqlDbType.Structured, Direction = ParameterDirection.Input });
            Dictionary<string, dynamic> result = SqlHelper.ExecuteNonQuery<SqlConnection>("USP_Farm_Mech_State_Plan_panchayat", dbparams, SqlHelper.ExecutionType.Procedure);

            return result["RowsAffected"];
        }

        /// <summary>
        /// InsertLicenseHolder.
        /// </summary>
        /// <param name="licenseHolders">licenseHolders.</param>
        /// <param name="queryName">queryName.</param>
        /// <returns>result integer.</returns>
        public int InsertLicenseHolder(List<InsecticideLicenceModel> licenseHolders, string queryName)
        {
            if (licenseHolders.Any())
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("FirmName", typeof(string));
                dt.Columns.Add("LicenceNo", typeof(string));
                dt.Columns.Add("LicenceDate", typeof(DateTime));
                dt.Columns.Add("Distcode", typeof(int));
                dt.Columns.Add("DistName", typeof(string));

                foreach (var item in licenseHolders)
                {
                    if ((!string.IsNullOrEmpty(item.FirmName)) && (!string.IsNullOrEmpty(item.LicenceNo)) && (!string.IsNullOrEmpty(item.LicenceDate)))
                    {
                        dt.Rows.Add(item.FirmName, item.LicenceNo, item.LicenceDate, Convert.ToInt32(item.Distcode), item.DistName);
                    }
                }

                List<DbParameter> dbparam = new List<DbParameter>();
                dbparam.Add(new SqlParameter { ParameterName = "@FertilizerSeedLicense", Value = dt, SqlDbType = SqlDbType.Structured, Direction = ParameterDirection.Input });
                dbparam.Add(new SqlParameter { ParameterName = "@QueryName", Value = queryName, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                SqlHelper.ExecuteNonQuery<SqlConnection>(spInsertFertilizerSeedLicense, dbparam, SqlHelper.ExecutionType.Procedure);
            }

            return 1;
        }
        /// <summary>
        /// OFMAS State Plan scheme panchayat data.
        /// </summary>
        /// <param name="dt">dt</param>
        /// <returns>InsertNHM_DataD</returns>
        public int InsertNHM_Data(DataTable dt, string schemecode)
        {
            Dictionary<string, dynamic> result = new Dictionary<string, dynamic>();
            if (schemecode == "22")
            {
                List<DbParameter> dbparams = new List<DbParameter>();
                dbparams.Add(new SqlParameter { ParameterName = "@NHM_scheme_Details", Value = dt, SqlDbType = SqlDbType.Structured, Direction = ParameterDirection.Input });
                result = SqlHelper.ExecuteNonQuery<SqlConnection>("USP_Farm_Mech_NHM_scheme_Details", dbparams, SqlHelper.ExecutionType.Procedure);
                return result["RowsAffected"];
            }
            else if (schemecode == "21")
            {
                List<DbParameter> dbparams = new List<DbParameter>();
                dbparams.Add(new SqlParameter { ParameterName = "@CMHM_scheme_Details", Value = dt, SqlDbType = SqlDbType.Structured, Direction = ParameterDirection.Input });
                result = SqlHelper.ExecuteNonQuery<SqlConnection>("USP_Farm_Mech_CMHM_scheme_Details", dbparams, SqlHelper.ExecutionType.Procedure);
                return result["RowsAffected"];
            }
            else if (schemecode == null)
            {
                List<DbParameter> dbparams = new List<DbParameter>();
                dbparams.Add(new SqlParameter { ParameterName = "@Nalkoop_scheme_Details", Value = dt, SqlDbType = SqlDbType.Structured, Direction = ParameterDirection.Input });
                result = SqlHelper.ExecuteNonQuery<SqlConnection>("USP_Farm_Mech_Nalkoop_scheme_Details", dbparams, SqlHelper.ExecutionType.Procedure);
                return result["RowsAffected"];
            }
            else if (schemecode == "16")
            {
                List<DbParameter> dbparams = new List<DbParameter>();
                dbparams.Add(new SqlParameter { ParameterName = "@SMAM_CHC_scheme_Details", Value = dt, SqlDbType = SqlDbType.Structured, Direction = ParameterDirection.Input });
                result = SqlHelper.ExecuteNonQuery<SqlConnection>("USP_Farm_Mech_SMAM_CHC_scheme_Details", dbparams, SqlHelper.ExecutionType.Procedure);
                return result["RowsAffected"];
            }
            else if (schemecode == "15")
            {
                List<DbParameter> dbparams = new List<DbParameter>();
                dbparams.Add(new SqlParameter { ParameterName = "@SMAM_FMB_scheme_Details", Value = dt, SqlDbType = SqlDbType.Structured, Direction = ParameterDirection.Input });
                result = SqlHelper.ExecuteNonQuery<SqlConnection>("USP_Farm_Mech_SMAM_FMB_scheme_Details", dbparams, SqlHelper.ExecutionType.Procedure);
                return result["RowsAffected"];
            }
            else if (schemecode == "17")
            {
                List<DbParameter> dbparams = new List<DbParameter>();
                dbparams.Add(new SqlParameter { ParameterName = "@TRFA_Oilseeds_scheme_Details", Value = dt, SqlDbType = SqlDbType.Structured, Direction = ParameterDirection.Input });
                result = SqlHelper.ExecuteNonQuery<SqlConnection>("USP_Farm_Mech_TRFA_Oilseeds_scheme_Details", dbparams, SqlHelper.ExecutionType.Procedure);
                return result["RowsAffected"];
            }
            else if (schemecode == "18")
            {
                List<DbParameter> dbparams = new List<DbParameter>();
                dbparams.Add(new SqlParameter { ParameterName = "@TRFA_Pulses_scheme_Details", Value = dt, SqlDbType = SqlDbType.Structured, Direction = ParameterDirection.Input });
                result = SqlHelper.ExecuteNonQuery<SqlConnection>("USP_Farm_Mech_TRFA_Pulses_scheme_Details", dbparams, SqlHelper.ExecutionType.Procedure);
                return result["RowsAffected"];
            }
            else if (schemecode == "14")
            {
                List<DbParameter> dbparams = new List<DbParameter>();
                dbparams.Add(new SqlParameter { ParameterName = "@RKVY_RAFTAAR_scheme_Details", Value = dt, SqlDbType = SqlDbType.Structured, Direction = ParameterDirection.Input });
                result = SqlHelper.ExecuteNonQuery<SqlConnection>("USP_Farm_Mech_RKVY_RAFTAAR_scheme_Details", dbparams, SqlHelper.ExecutionType.Procedure);
                return result["RowsAffected"];
            }
            else if (schemecode == "PMKSY")
            {
                List<DbParameter> dbparams = new List<DbParameter>();
                dbparams.Add(new SqlParameter { ParameterName = "@PMKSY_scheme_Details", Value = dt, SqlDbType = SqlDbType.Structured, Direction = ParameterDirection.Input });
                result = SqlHelper.ExecuteNonQuery<SqlConnection>("USP_Farm_Mech_PMKSY_scheme_Details", dbparams, SqlHelper.ExecutionType.Procedure);
                return result["RowsAffected"];
            }
            else if (schemecode == "11")
            {
                List<DbParameter> dbparams = new List<DbParameter>();
                dbparams.Add(new SqlParameter { ParameterName = "@KKA_scheme_Details", Value = dt, SqlDbType = SqlDbType.Structured, Direction = ParameterDirection.Input });
                result = SqlHelper.ExecuteNonQuery<SqlConnection>("USP_Farm_Mech_KKA_scheme_Details", dbparams, SqlHelper.ExecutionType.Procedure);
                return result["RowsAffected"];
            }

            else if (schemecode == "12")
            {
                List<DbParameter> dbparams = new List<DbParameter>();
                dbparams.Add(new SqlParameter { ParameterName = "@SMAM_SCHC_scheme_Details", Value = dt, SqlDbType = SqlDbType.Structured, Direction = ParameterDirection.Input });
                result = SqlHelper.ExecuteNonQuery<SqlConnection>("USP_Farm_Mech_SMAM_SCHC_scheme_Details", dbparams, SqlHelper.ExecutionType.Procedure);
                return result["RowsAffected"];
            }
            else if (schemecode == "1")
            {
                List<DbParameter> dbparams = new List<DbParameter>();
                dbparams.Add(new SqlParameter { ParameterName = "@NFSM_Scheme_Details", Value = dt, SqlDbType = SqlDbType.Structured, Direction = ParameterDirection.Input });
                result = SqlHelper.ExecuteNonQuery<SqlConnection>("USP_Farm_Mech_NFSM_Scheme_Details", dbparams, SqlHelper.ExecutionType.Procedure);
                return result["RowsAffected"];
            }

            else if (schemecode == "10")
            {
                List<DbParameter> dbparams = new List<DbParameter>();
                dbparams.Add(new SqlParameter { ParameterName = "@NFSM_Oilseeds_Scheme_Details", Value = dt, SqlDbType = SqlDbType.Structured, Direction = ParameterDirection.Input });
                result = SqlHelper.ExecuteNonQuery<SqlConnection>("USP_Farm_Mech_NFSM_Oilseeds_Scheme_Details", dbparams, SqlHelper.ExecutionType.Procedure);
                return result["RowsAffected"];
            }
            else if (schemecode == "6")
            {
                List<DbParameter> dbparams = new List<DbParameter>();
                dbparams.Add(new SqlParameter { ParameterName = "@State_Plan_Scheme_Details", Value = dt, SqlDbType = SqlDbType.Structured, Direction = ParameterDirection.Input });
                result = SqlHelper.ExecuteNonQuery<SqlConnection>("USP_Farm_Mech_State_Plan_Scheme_Details", dbparams, SqlHelper.ExecutionType.Procedure);
                return result["RowsAffected"];
            }
            return result["RowsAffected"];
        }
    }
}
