//------------------------------------------------------------------------------
// <copyright file="HoricultureRepository.cs" company="Government of Bihar">
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
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using ADIDAS.Model.DTO;
    using ADIDAS.Model.Entities;
    using ADIDAS.Repository.Helper;
    using ADIDAS.Repository.Interfaces;
    using Azure.Storage.Blobs;
    using Microsoft.Extensions.Options;

    /// <summary>
    /// HoricultureRepository.
    /// </summary>
    public class HoricultureRepository : IHoricultureRepository
    {
        private readonly string istStrDate = "select CAST(DATEADD(HOUR, 5, DATEADD(MINUTE, 30, GETUTCDATE())) as DATE)";
        private readonly string istDate = string.Empty;
        private readonly IOptions<DBSettings> options;
        private readonly IOptions<BlobConfig> blobconfig;
        private static string qnGetAllColdStorages = "GetAllCldStrgs";
        private static string spGetAllColdStorages = "usp_getdata_cold_storage_wk";
        private static string qnGetAllColdStoragesOffline = "GetAllOfflineCldStrgs";
        private static string qnGetAllCropsColdStorage = "GetAllCrop_CldStrgs";
        private static string qnGetCldStrgs_ByStrg_ID = "GetCldStrgs_ByStrg_ID";
        private static string qnGetColdStoragesByStrgIDOffline = "GetCldStrgsOffline_ByStrg_ID";
        private static string spInsertHortDamagePancht = "usp_hort_crop_damage_pnchyt";
        private static string qnaggrcrpdmg = "hort_Aggregate_crop_damage";
        private static string qnGetLstEgtWksColdStorages = "GetLstEgtWks_Agg_CldStrgs";
        private static string spInsertColdStorage = "usp_insert_cold_storage_wk";
        private static string spInsertColdStorageDetails = "usp_insert_cold_storage_dtls";
        private static string spInsertPHMSFacility = "USP_Insert_phms_facility";
        private static string qnGetFacilitiesOffline = "GetFacilitiesOfflineByPnchytID";
        private static string spGetFacilitiesOffline = "usp_getdata_phms";
        private static string qnGetPHMSNoFacilitiesData = "GetPHMSNoFacilityData";
        private static string qnGetFacilitiesOnline = "GetFacilitiesOnlineByPnchytID";
        private static string spInsertHortCropCoveragetargetPanchayat = "usp_hort_crop_cvrg_tgt_pnchyt";
        private static string spInsertHortCropCoveragetargetPanchayatApproval = "usp_hort_crop_cvrg_target_block_approval";
        private static string spcropcvrgtargetpnchytappr = "usp_hort_crop_cvrg_target_pnchyt_approval";
        private static string spInsertHortDamageApproval = "usp_hort_crop_damage_block_approval";
        private static string spcropdmgpnchytappr = "usp_hort_crop_damage_pnchyt_approval";
        private static string qnGetHortDamagePancht = "GetCropDamagePancht";
        private static string spGetHortDamagePancht = "usp_hort_crop_cntrlr_dmg_get";
        private static string qnGetHortDamageBlock = "GetCropDamageBlock";
        private static string spGetHortDamageBlock = "usp_hort_crop_cntrlr_dmg_get";
        private static string spInsertHortAggCropCoverageActual = "hort_Aggregate_crop_coverage_actual";
        private static string spHortAutoApprovalCoverageActlBlock = "usp_hort_auto_approval_coverage_actl_block";
        private static string spHortAutoApprovalCoverageActlPnchyt = "usp_hort_auto_approval_coverage_actl_pnchyt";
        private static string qnGetHortCropCoverageActualPancht = "GetCropCoverageActualPancht";
        private static string spGetHortCropCoverageActualPancht = "usp_hort_crop_cntrlr_cvrg_get";
        private static string qnGetHortCropCoverageActualPanchtDelta = "GetCropCoverageActualPanchtDelta";
        private static string qnGetHortCropCoverageActualPanchtCurrDate = "GetCropCoverageActualPanchtCurrDate";
        private static string qnGetHortCropCoverageActualBlock = "GetCropCoverageActualBlock";
        private static string qnGetHortCropCoverageActualDistrict = "GetCropCoverageActualDistrict";
        private static string qnGetHortCropCoverageActualADHDelta = "GetCropCoverageActualDAODeltaDist";
        private static string qnGetHortCropCoverageActualADHDeltaBlk = "GetCropCoverageActualDAODeltaBlk";
        private static string qnGetCropCoverageActualDAODeltaPanchyt = "GetCropCoverageActualDAODeltaPanchyt";
        private static string qnGetHortCropCoverageActualBHODelta = "GetCropCoverageActualBAODeltaDist";
        private static string qnGetCropCoverageActualBAODeltaBlk = "GetCropCoverageActualBAODeltaBlk";
        private static string qnGetCropCoverageActualBAODeltaPanchyt = "GetCropCoverageActualBAODeltaPanchyt";
        private static string qnGetHortCropCoverageActualBHOOfflineDist = "GetCropCoverageActualBAOOfflineDist";
        private static string qnGetHortCropCoverageActualBHOOfflineBlk = "GetCropCoverageActualBAOOfflineBlk";
        private static string qnGetHortCropCoverageActualBHOOfflinePanch = "GetCropCoverageActualBAOOfflinePanchyt";
        private static string qnGetHortCropCoverageActualADHOfflineDist = "GetCropCoverageActualDAOOfflineDist";
        private static string qnGetHortCropCoverageActualADHOfflineBlk = "GetCropCoverageActualDAOOfflineBlk";
        private static string qnGetHortCropCoverageActualADHOfflinePanch = "GetCropCoverageActualDAOOfflinePanchyt";
        private static string qnGetHortCropCoverageActualADHDist = "GetCropCoverageActualDAODist";
        private static string qnGetHortCropCoverageActualADHBlk = "GetCropCoverageActualDAOBlk";
        private static string qnGetHortCropCoverageActualADHpanch = "GetCropCoverageActualDAOPanchyt";
        private static string qnGetHortCropCoverageActualBHODist = "GetCropCoverageActualBAODist";
        private static string qnGetHortCropCoverageActualBHOBlk = "GetCropCoverageActualBAOBlk";
        private static string qnGetHortCropCoverageActualBHOPanch = "GetCropCoverageActualBAOPanchyt";
        private static string spHortAutoCropCvrgActlDataCorrection = "usp_hort_crop_cvrg_actl_data_correction";
        private static string spInsertHortCropCoverageActualApproval = "usp_hort_crop_cvrg_actual_block_approval";
        private static string spInsertHortCropCoverageActualApprovalpanch = "usp_hort_crop_cvrg_actual_pnchyt_approval";
        private static string spInsertHortCropCoverageActualPancht = "usp_hort_crop_cvrg_actual_pnchyt";
        private static string qnhortaggrcrpactl = "hort_aggregate_crop_coverage_actual";
        private static string qnGetAllStructure = "GetAllStructure";
        private static string spGetAllStructure = "usp_getdata_phms";
        private static string qnInsertPHMSStructure = "PostPHMSStructureMerge";
        private static string qnInsertHortNewCrop = "PostHorticultureCropDimMerge";
        private static string spInsertHortNewCrop = "usp_hort_dim_cntrlr";
        private static string qnPostHorticultureCropDimIns = "PostHorticultureCropDimIns";
        private static string qnGetDistinctHorticultureCrop = "GetDistinctHorticultureCrop";
        private static string qnGetHortProduceSeason = "GetHortProduceSeason";
        private static string spGetHortProduceSeason = "usp_getdata_hort_Produce";
        private static string qnGetHortProducePanchayat = "GetHortProducePanchayat";
        private static string spGetHortProducePanchayat = "usp_getdata_hort_Produce";
        private static string qnGetHortProduceLatest = "GetHortProduceLatest";
        private static string spInsertHortProduceTranApproval = "usp_hort_produce_tran_approval";
        private static string spInsertHortCoverageActualApproval = "usp_hort_produce_actual_block_approval";
        private static string spInsertHortCoverageActualApprovalpanch = "usp_hort_produce_actual_pnchyt_approval";
        private static string spInsertHortProduceActualPnchyt = "usp_hort_produce_actual_pnchyt";
        private static string sphortprdceaggractl = "hort_produce_Aggregate_actual";
        private static string spHortProduceAutoApprovalActlBlock = "usp_hort_produce_auto_approval_actl_block";
        private static string spHortProduceAutoApprovalActlPanchayat = "usp_hort_produce_auto_approval_actl_pnchyt";
        private static string spHortProduceActlDataCorrection = "usp_hort_produce_actl_data_correction";
        private static string qnGetHortProduceActualPancht = "GetHortProduceActualPancht";
        private static string spGetHortProduceActualPanch = "usp_hort_produce_cntrlr_get";
        private static string qnGetHortProduceActualPanchtCurrDate = "GetHortProduceActualPanchtCurrDate";
        private static string spPostAgriProductivity = "usp_insert_agri_productivity";

        // private static string spGetHortiReportColdStorage = "usp_horticulture_report_data";
        private static string qnGetSubmitProductivity = "GetAgricultureSubmitProductivity";
        private static string spGetSubmitProductivity = "usp_dim_cntrlr";
        private static string qnGetHortProduceActualBlock = "GetHortProduceActualBlock";
        private static string qnGetHortProduceActualDistrict = "GetHortProduceActualDistrict";
        private static string qnGetHortProduceBHO = "GetHortProduceBHO";
        private static string qnGetHortiProduceCoverageActualBHOOffline = "GetHortProduceActualBHOOfflineDist";
        private static string qnGetHortiProduceCoverageActualBHOOfflineBlk = "GetHortProduceActualBHOOfflineBlk";
        private static string qnGetHortiProduceCoverageActualBHOOfflinePanch = "GetHortProduceActualBHOOfflinePanchyt";
        private static string qnGetHortProduceActualADHOffline = "GetHortProduceActualADHOfflineDist";
        private static string qnGetHortProduceActualADHOfflineBlk = "GetHortProduceActualADHOfflineBlk";
        private static string qnGetHortProduceActualADHOfflinePanch = "GetHortProduceActualADHOfflinePanchyt";
        private static string qnGetHortProduceActualADH = "GetHortProduceActualADHDist";
        private static string qnGetHortProduceActualADHBlk = "GetHortProduceActualADHBlk";
        private static string qnGetHortProduceActualADHPanch = "GetHortProduceActualADHPanchyt";
        private static string qnGetHortProduceActualBHO = "GetHortProduceActualBHODist";
        private static string qnGetHortProduceActualBHOBlk = "GetHortProduceActualBHOBlk";
        private static string qnGetHortProduceActualBHOPanch = "GetHortProduceActualBHOPanchyt";
        private static string qnGetAllSeedPerformanceHorticultureDBT = "GetAllSeedPerformanceHorticulture";
        private static string qnGetblockPanchaytLgCodes = "Getblkpanchaytlgcodes";
        private static string spGetAllSeedPerformanceHorticultureDBT = "usp_getdata_seed_performance_hort";
        private static string qnGetBlockPanchayatData = "GetSeedPerformanceHorticultureData";
        private static string spInsertHortiSeedPerformance = "usp_insert_seed_performance_hort";
        private static string qnGetVarietiesByType = "GetHortProduceCrops";
        private static string spGetVarietiesByType = "usp_crop_cvrg_Target_report_data";

        /// <summary>
        /// Initializes a new instance of the <see cref="HoricultureRepository"/> class.
        /// HoricultureRepository.
        /// </summary>
        /// <param name="options">options.</param>
        /// <param name="blobconfig">blobconfig.</param>
        public HoricultureRepository(IOptions<DBSettings> options, IOptions<BlobConfig> blobconfig)
        {
            this.options = options;
            this.blobconfig = blobconfig;
            SqlHelper.SetConnectionString(this.options.Value.ConnectionString);
            this.istDate = this.GetDateFromServer();
        }

        /// <summary>
        /// GetDateFromServer.
        /// </summary>
        /// <returns>Datetable values.</returns>
        public string GetDateFromServer()
        {
            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(this.istStrDate, null, SqlHelper.ExecutionType.Query);
            return dt.Rows[0][0].ToString();
        }

        /// <summary>
        /// GetAllColdStorages.
        /// </summary>
        /// <param name="district_Id">district_Id.</param>
        /// <returns>List.</returns>
        public List<GetAllColdStorage> GetAllColdStorages(int district_Id)
        {
            List<GetAllColdStorage> list = new List<GetAllColdStorage>();
            List<DbParameter> dbparams = new List<DbParameter>();
            dbparams.Add(new SqlParameter { ParameterName = "@query_name", Value = qnGetAllColdStorages, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@crop_id", Value = DBNull.Value, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@District_Id", Value = district_Id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@Strg_ID", Value = DBNull.Value, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });

            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(spGetAllColdStorages, dbparams, SqlHelper.ExecutionType.Procedure);
            if (dt != null && dt.Rows.Count > 0)
            {
                list = SqlHelper.ConvertDataTableToList<GetAllColdStorage>(dt);
                for (int i = 0; i <= list.Count - 1; i++)
                {
                    if (list[i].Stor_Name_Address != string.Empty)
                    {
                        list[i].Stor_Name = list[i].Stor_Name_Address.Split(',')[0].ToString().Trim();

                        string[] addressArr = list[i].Stor_Name_Address.Split(',');
                        addressArr = addressArr.Skip(1).ToArray();
                        list[i].Stor_Address = string.Join(",", addressArr).Trim();
                    }
                }
            }

            return list;
        }

        /// <summary>
        /// GetAllColdStoragesOffline.
        /// </summary>
        /// <returns>List.</returns>
        public List<GetAllColdStorage> GetAllColdStoragesOffline()
        {
            List<GetAllColdStorage> list = new List<GetAllColdStorage>();
            List<DbParameter> dbparams = new List<DbParameter>();
            dbparams.Add(new SqlParameter { ParameterName = "@query_name", Value = qnGetAllColdStoragesOffline, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@crop_id", Value = DBNull.Value, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@District_Id", Value = DBNull.Value, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@Strg_ID", Value = DBNull.Value, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });

            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(spGetAllColdStorages, dbparams, SqlHelper.ExecutionType.Procedure);
            if (dt != null && dt.Rows.Count > 0)
            {
                list = SqlHelper.ConvertDataTableToList<GetAllColdStorage>(dt);
                for (int i = 0; i <= list.Count - 1; i++)
                {
                    if (list[i].Stor_Name_Address != string.Empty)
                    {
                        list[i].Stor_Name = list[i].Stor_Name_Address.Split(',')[0].ToString().Trim();

                        string[] addressArr = list[i].Stor_Name_Address.Split(',');
                        addressArr = addressArr.Skip(1).ToArray();
                        list[i].Stor_Address = string.Join(",", addressArr).Trim();
                    }
                }
            }

            return list;
        }

        /// <summary>
        /// GetAllCropsColdStorage.
        /// </summary>
        /// <param name="district_Id">district_Id.</param>
        /// <returns>List.</returns>
        public List<GetAllCropColdStorage> GetAllCropsColdStorage(int district_Id)
        {
            List<GetAllCropColdStorage> list = new List<GetAllCropColdStorage>();
            List<DbParameter> dbparams = new List<DbParameter>();
            dbparams.Add(new SqlParameter { ParameterName = "@query_name", Value = qnGetAllCropsColdStorage, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@crop_id", Value = DBNull.Value, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@District_Id", Value = district_Id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@Crop_name", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@Strg_ID", Value = DBNull.Value, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });

            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(spGetAllColdStorages, dbparams, SqlHelper.ExecutionType.Procedure);
            if (dt != null && dt.Rows.Count > 0)
            {
                list = SqlHelper.ConvertDataTableToList<GetAllCropColdStorage>(dt);
            }

            return list;
        }

        /// <summary>
        /// GetColdStoragesByStrgID.
        /// </summary>
        /// <param name="crop_id">crop_id.</param>
        /// <param name="district_Id">district_Id.</param>
        /// <param name="strg_ID">strg_ID.</param>
        /// <returns>GetAllCropColdStorage Lists.</returns>
        public List<GetAllCropColdStorageId> GetColdStoragesByStrgID(int crop_id, int? district_Id, int strg_ID)
        {
            List<GetAllCropColdStorageId> list = new List<GetAllCropColdStorageId>();
            List<DbParameter> dbparams = new List<DbParameter>();
            dbparams.Add(new SqlParameter { ParameterName = "@query_name", Value = qnGetCldStrgs_ByStrg_ID, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@crop_id", Value = crop_id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@District_Id", Value = district_Id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@Strg_ID", Value = strg_ID, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });

            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(spGetAllColdStorages, dbparams, SqlHelper.ExecutionType.Procedure);
            if (dt != null && dt.Rows.Count > 0)
            {
                list = SqlHelper.ConvertDataTableToList<GetAllCropColdStorageId>(dt);
            }

            return list;
        }

        /// <summary>
        /// GetColdStoragesByStrgIDOffline.
        /// </summary>
        /// <returns>List.</returns>
        public List<GetAllCropColdStorageId> GetColdStoragesByStrgIDOffline()
        {
            List<GetAllCropColdStorageId> list = null;
            List<DbParameter> dbparams = new List<DbParameter>();
            dbparams.Add(new SqlParameter { ParameterName = "@query_name", Value = qnGetColdStoragesByStrgIDOffline, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@crop_id", Value = DBNull.Value, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@District_Id", Value = DBNull.Value, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@Strg_ID", Value = DBNull.Value, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });

            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(spGetAllColdStorages, dbparams, SqlHelper.ExecutionType.Procedure);
            if (dt != null && dt.Rows.Count > 0)
            {
                list = SqlHelper.ConvertDataTableToList<GetAllCropColdStorageId>(dt);
            }

            return list;
        }

        /// <summary>
        /// InsertHortDamagePancht.
        /// </summary>
        /// <param name="crop">crop.</param>
        /// <returns>Response Result.</returns>
        public List<CropDamagePanchayatResponse> InsertHortDamagePancht(List<CropDamagePancht> crop)
        {
            int insertRowsCount = 0;
            List<CropDamagePanchayatResponse> response = new List<CropDamagePanchayatResponse>();

            foreach (CropDamagePancht cdlist in crop)
            {
                foreach (var cpvalue in cdlist.DmgCropList)
                {
                    foreach (var damagedetail in cpvalue.Damagedetails)
                    {
                        List<DbParameter> dbparams = new List<DbParameter>();

                        dbparams.Add(new SqlParameter { ParameterName = "@season_id", Value = cdlist.Season_Id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                        dbparams.Add(new SqlParameter { ParameterName = "@panchayat_id", Value = cdlist.Panchayat_Id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                        dbparams.Add(new SqlParameter { ParameterName = "@crop_id", Value = cpvalue.Crop_Id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                        dbparams.Add(new SqlParameter { ParameterName = "@reported_date", Value = damagedetail.Reported_Date, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
                        dbparams.Add(new SqlParameter { ParameterName = "@irrigated_dmg_area", Value = damagedetail.Irrigated_Dmg_Area, SqlDbType = SqlDbType.Decimal, Direction = ParameterDirection.Input });
                        dbparams.Add(new SqlParameter { ParameterName = "@damage_reason", Value = string.IsNullOrEmpty(damagedetail.Damage_Reason) ? DBNull.Value : (object)damagedetail.Damage_Reason, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                        dbparams.Add(new SqlParameter { ParameterName = "@dao_approval_flag", Value = string.IsNullOrEmpty(damagedetail.DAO_Approval_flag) ? DBNull.Value : (object)damagedetail.DAO_Approval_flag, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                        dbparams.Add(new SqlParameter { ParameterName = "@dao_approval_reason", Value = string.IsNullOrEmpty(damagedetail.DAO_Approval_Reason) ? DBNull.Value : (object)damagedetail.DAO_Approval_Reason, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                        dbparams.Add(new SqlParameter { ParameterName = "@dao_approved_userid", Value = damagedetail.DAO_Approved_userid == 0 ? DBNull.Value : (object)damagedetail.DAO_Approved_userid, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                        dbparams.Add(new SqlParameter { ParameterName = "@dao_approved_date", Value = damagedetail.DAO_Approved_date == DateTime.MinValue ? DBNull.Value : (object)damagedetail.DAO_Approved_date, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
                        dbparams.Add(new SqlParameter { ParameterName = "@bao_approval_flag", Value = string.IsNullOrEmpty(damagedetail.BAO_Approval_flag) ? DBNull.Value : (object)damagedetail.BAO_Approval_flag, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                        dbparams.Add(new SqlParameter { ParameterName = "@bao_approval_reason", Value = string.IsNullOrEmpty(damagedetail.BAO_Approval_Reason) ? DBNull.Value : (object)damagedetail.BAO_Approval_Reason, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                        dbparams.Add(new SqlParameter { ParameterName = "@bao_approved_userid", Value = damagedetail.BAO_Approved_userid == 0 ? DBNull.Value : (object)damagedetail.BAO_Approved_userid, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                        dbparams.Add(new SqlParameter { ParameterName = "@bao_approved_date", Value = damagedetail.BAO_Approved_date == DateTime.MinValue ? DBNull.Value : (object)damagedetail.BAO_Approved_date, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
                        dbparams.Add(new SqlParameter { ParameterName = "@ac_submitted_userid", Value = damagedetail.AC_Submitted_userid == 0 ? DBNull.Value : (object)damagedetail.AC_Submitted_userid, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                        dbparams.Add(new SqlParameter { ParameterName = "@ac_submitted_date", Value = damagedetail.AC_Submitted_date == DateTime.MinValue ? DBNull.Value : (object)damagedetail.AC_Submitted_date, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
                        dbparams.Add(new SqlParameter { ParameterName = "@submission_source", Value = string.IsNullOrEmpty(damagedetail.Submission_source) ? DBNull.Value : (object)damagedetail.Submission_source, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                        dbparams.Add(new SqlParameter { ParameterName = "@response_status", SqlDbType = SqlDbType.NVarChar, Size = 200, Direction = ParameterDirection.Output });

                        Dictionary<string, dynamic> result = SqlHelper.ExecuteNonQuery<SqlConnection>(spInsertHortDamagePancht, dbparams, SqlHelper.ExecutionType.Procedure);
                        insertRowsCount += result["RowsAffected"];

                        string spOut = DBNull.Value.Equals(result["@response_status"]) ? string.Empty : result["@response_status"];
                        if (!string.IsNullOrEmpty(spOut))
                        {
                            CropDamagePanchayatResponse respobj = new CropDamagePanchayatResponse();
                            foreach (var keyvaluepair in spOut.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries))
                            {
                                string[] splitteddata = keyvaluepair.Split(new string[] { ":" }, StringSplitOptions.RemoveEmptyEntries);

                                if (splitteddata[0].Trim().Equals("Status"))
                                {
                                    respobj.Status = splitteddata[1].ToString();
                                }
                                else if (splitteddata[0].Trim().Equals("Reason"))
                                {
                                    respobj.Reason = splitteddata[1].ToString();
                                }
                                else if (splitteddata[0].Trim().Equals("PanchayatId"))
                                {
                                    respobj.PanchayatId = splitteddata[1].ToString();
                                }
                                else if (splitteddata[0].Trim().Equals("CropId"))
                                {
                                    respobj.CropId = splitteddata[1].ToString();
                                }
                                else if (splitteddata[0].Trim().Equals("SeasonId"))
                                {
                                    respobj.SeasonId = splitteddata[1].ToString();
                                }
                                else if (splitteddata[0].Trim().Equals("DamageReason"))
                                {
                                    respobj.DamageReason = splitteddata[1].ToString();
                                }
                            }

                            response.Add(respobj);
                        }

                        if (result["RowsAffected"] != 0)
                        {
                            List<DbParameter> spparams = new List<DbParameter>();
                            spparams.Add(new SqlParameter { ParameterName = "@panchayat_id", Value = cdlist.Season_Id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                            spparams.Add(new SqlParameter { ParameterName = "@season_id", Value = cdlist.Panchayat_Id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                            spparams.Add(new SqlParameter { ParameterName = "@crop_id", Value = cpvalue.Crop_Id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                            spparams.Add(new SqlParameter { ParameterName = "@damage_reason", Value = string.IsNullOrEmpty(damagedetail.Damage_Reason) ? DBNull.Value : (object)damagedetail.Damage_Reason, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });

                            string sp_query = qnaggrcrpdmg;
                            result = SqlHelper.ExecuteNonQuery<SqlConnection>(sp_query, spparams, SqlHelper.ExecutionType.Procedure);
                        }
                    }
                }
            }

            return response;
        }

        /// <summary>
        /// GetLstEgtWksColdStorages.
        /// </summary>
        /// <param name="district_Id">district_Id.</param>
        /// <returns>List.</returns>
        public List<LstCpldStorageDets> GetLstEgtWksColdStorages(int district_Id)
        {
            List<LstCpldStorage> list;
            List<LstCpldStorageDets> lists = new List<LstCpldStorageDets>();
            List<DbParameter> dbparams = new List<DbParameter>();
            dbparams.Add(new SqlParameter { ParameterName = "@query_name", Value = qnGetLstEgtWksColdStorages, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@crop_id", Value = DBNull.Value, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@District_Id", Value = district_Id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@Strg_ID", Value = DBNull.Value, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });

            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(spGetAllColdStorages, dbparams, SqlHelper.ExecutionType.Procedure);
            if (dt != null && dt.Rows.Count > 0)
            {
                list = SqlHelper.ConvertDataTableToList<LstCpldStorage>(dt);
                var lstDist = list.Select(x => new { x.Week_nm }).Distinct().ToList();

                foreach (var item in lstDist)
                {
                    List<StorageDet> lstStorageDet = new List<StorageDet>();
                    var tempsList = list.Where(x => x.Week_nm == item.Week_nm).Select(x => new { x.Strg_ID, x.Week_nm }).Distinct().ToList();

                    LstCpldStorageDets lstCpldStorageDets = new LstCpldStorageDets();
                    lstCpldStorageDets.Week_nm = item.Week_nm;
                    lstCpldStorageDets.Week_Start = list.Where(x => x.Week_nm == item.Week_nm).Select(x => x.Week_Start.Date).FirstOrDefault();
                    lstCpldStorageDets.Week_end = list.Where(x => x.Week_nm == item.Week_nm).Select(x => x.Week_end.Date).FirstOrDefault();
                    lstCpldStorageDets.District_Id = list.Where(x => x.Week_nm == item.Week_nm).Select(x => x.District_Id).FirstOrDefault();
                    foreach (var tmp in tempsList)
                    {
                        var data = list.Where(x => x.Strg_ID == tmp.Strg_ID && x.Week_nm == tmp.Week_nm).Select(x => new { x.Strg_ID, x.Stor_Name_Address, x.District_Name }).Distinct().ToList();

                        foreach (var items in data)
                        {
                            StorageDet storageDet = new StorageDet();
                            storageDet.Strg_ID = items.Strg_ID;
                            storageDet.StorageName = (!string.IsNullOrEmpty(items.Stor_Name_Address)) ? items.Stor_Name_Address.Split(',')[0].ToString().Trim() : string.Empty;
                            if (string.IsNullOrEmpty(items.Stor_Name_Address))
                            {
                                storageDet.StorageAddress = string.Empty;
                            }
                            else
                            {
                                string[] addressArr = items.Stor_Name_Address.Split(',');
                                addressArr = addressArr.Skip(1).ToArray();
                                storageDet.StorageAddress = string.Join(",", addressArr).Trim();
                            }

                            storageDet.District_Name = items.District_Name;
                            var croplstDets = list.Where(x => x.Strg_ID == items.Strg_ID && x.Week_nm == item.Week_nm).ToList();
                            List<CroplstDet> lstcroplstDet = new List<CroplstDet>();
                            foreach (var cropItem in croplstDets)
                            {
                                CroplstDet croplstDet = new CroplstDet();
                                croplstDet.Crop_id = cropItem.Crop_id;
                                croplstDet.Crop_name = cropItem.Crop_name;
                                croplstDet.Crop_category = cropItem.Crop_category;
                                croplstDet.Agg_Week_Open_Bal = cropItem.Agg_Week_Open_Bal;
                                croplstDet.Agg_Curr_Open_Bal = cropItem.Agg_Curr_Open_Bal;
                                croplstDet.Agg_Deposit = cropItem.Agg_Deposit;
                                croplstDet.Agg_Release = cropItem.Agg_Release;
                                croplstDet.Agg_Curr_Balance = cropItem.Agg_Curr_Balance;
                                croplstDet.Capacity_mt = cropItem.Capacity_mt;
                                croplstDet.SubmittedDate = cropItem.Rec_created_ts;
                                lstcroplstDet.Add(croplstDet);
                            }

                            storageDet.Crop_list = lstcroplstDet;
                            lstStorageDet.Add(storageDet);
                        }
                    }

                    lstCpldStorageDets.Strg_list = lstStorageDet;
                    lists.Add(lstCpldStorageDets);
                }
            }

            return lists;
        }

        /// <summary>
        /// InsertColdStorage.
        /// </summary>
        /// <param name="coldStorage">coldStorage.</param>
        /// <returns>Responce Status Values.</returns>
        public int InsertColdStorage(ColdStorage coldStorage)
        {
            int insertRowsCount = 0;
            if (coldStorage != null)
            {
                List<DbParameter> dbparams = new List<DbParameter>();
                dbparams.Add(new SqlParameter { ParameterName = "@crop_id", Value = coldStorage.Crop_Id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                dbparams.Add(new SqlParameter { ParameterName = "@District_Id", Value = coldStorage.District_Id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                dbparams.Add(new SqlParameter { ParameterName = "@Crop_name", Value = coldStorage.Crop_name, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparams.Add(new SqlParameter { ParameterName = "@Strg_ID", Value = coldStorage.Strg_ID, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                dbparams.Add(new SqlParameter { ParameterName = "@week_open_bal", Value = coldStorage.Week_open_bal, SqlDbType = SqlDbType.Decimal, Direction = ParameterDirection.Input });
                dbparams.Add(new SqlParameter { ParameterName = "@curr_open_bal", Value = coldStorage.Curr_open_bal, SqlDbType = SqlDbType.Decimal, Direction = ParameterDirection.Input });
                dbparams.Add(new SqlParameter { ParameterName = "@deposit", Value = coldStorage.Deposit, SqlDbType = SqlDbType.Decimal, Direction = ParameterDirection.Input });
                dbparams.Add(new SqlParameter { ParameterName = "@release", Value = coldStorage.Release, SqlDbType = SqlDbType.Decimal, Direction = ParameterDirection.Input });
                dbparams.Add(new SqlParameter { ParameterName = "@Submitted_date", Value = coldStorage.Submitted_date == DateTime.MinValue ? DBNull.Value : (object)coldStorage.Submitted_date, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
                dbparams.Add(new SqlParameter { ParameterName = "@Submitted_userid", Value = coldStorage.Submitted_userid, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                Dictionary<string, dynamic> result = SqlHelper.ExecuteNonQuery<SqlConnection>(spInsertColdStorage, dbparams, SqlHelper.ExecutionType.Procedure);
                insertRowsCount += result["RowsAffected"];
            }

            if (insertRowsCount != 0)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// InsertColdStorageDetails.
        /// </summary>
        /// <param name="coldStorageDetails">coldStorageDetails.</param>
        /// <returns>DB Insert. Count Results.</returns>
        public int InsertColdStorageDetails(ColdStorageDetails coldStorageDetails)
        {
            int insertRowsCount = 0;
            if (coldStorageDetails != null)
            {
                List<DbParameter> dbparams = new List<DbParameter>();
                dbparams.Add(new SqlParameter { ParameterName = "@District_ID", Value = coldStorageDetails.District_Id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                dbparams.Add(new SqlParameter { ParameterName = "@District_Name", Value = coldStorageDetails.District_Name, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparams.Add(new SqlParameter { ParameterName = "@Strg_Name", Value = coldStorageDetails.Strg_Name, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparams.Add(new SqlParameter { ParameterName = "@Strg_Address", Value = coldStorageDetails.Strg_Address, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparams.Add(new SqlParameter { ParameterName = "@license_no", Value = coldStorageDetails.License_no, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparams.Add(new SqlParameter { ParameterName = "@capacity_mt", Value = coldStorageDetails.Capacity_mt, SqlDbType = SqlDbType.Decimal, Direction = ParameterDirection.Input });
                dbparams.Add(new SqlParameter { ParameterName = "@rec_created_userid", Value = coldStorageDetails.Rec_created_userid, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                dbparams.Add(new SqlParameter { ParameterName = "@rec_created_ts", Value = coldStorageDetails.Rec_created_ts == DateTime.MinValue ? DBNull.Value : (object)coldStorageDetails.Rec_created_ts, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
                DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(spInsertColdStorageDetails, dbparams, SqlHelper.ExecutionType.Procedure);
                insertRowsCount = Convert.ToInt32(dt.Rows[0][0]);
            }

            return insertRowsCount;
        }

        /// <summary>
        /// InsertPHMSFacility.
        /// </summary>
        /// <param name="phmsDetails">phmsDetails.</param>
        /// <returns>DB Insert and Status Response Values.</returns>
        public int InsertPHMSFacility(PhmsDetails phmsDetails)
        {
            int insertRowsCount = 0;
            string insertspOut = string.Empty;
            if (phmsDetails != null)
            {
                Dictionary<string, dynamic> result;
                if (phmsDetails.Facility_list.Count == 0 || phmsDetails.Facility_list == null)
                {
                    List<DbParameter> dbparams = new List<DbParameter>();
                    dbparams.Add(new SqlParameter { ParameterName = "@facility_name", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@facility_add", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@capacity", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@struct_id", Value = phmsDetails.Structure_id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@district_id", Value = phmsDetails.District_id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@panchayat_id", Value = phmsDetails.Panchayat_id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@Is_Facility_Added", Value = phmsDetails.Is_Facility_Added, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@period", Value = phmsDetails.Period, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@rec_created_ts", Value = DBNull.Value, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@rec_created_userid", Value = phmsDetails.Rec_created_userid, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@rec_updated_ts", Value = DBNull.Value, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@rec_updated_userid", Value = phmsDetails.Rec_updated_userid, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@response_status", SqlDbType = SqlDbType.VarChar, Size = 50, Direction = ParameterDirection.Output });

                    result = SqlHelper.ExecuteNonQuery<SqlConnection>(spInsertPHMSFacility, dbparams, SqlHelper.ExecutionType.Procedure);

                    insertspOut = DBNull.Value.Equals(result["@response_status"]) ? string.Empty : result["@response_status"];
                }
                else
                {
                    for (int i = 0; i <= phmsDetails.Facility_list.Count - 1; i++)
                    {
                        List<DbParameter> dbparams = new List<DbParameter>();
                        dbparams.Add(new SqlParameter { ParameterName = "@facility_id", Value = phmsDetails.Facility_list[i].Facility_id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                        dbparams.Add(new SqlParameter { ParameterName = "@facility_name", Value = phmsDetails.Facility_list[i].Facility_name, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                        dbparams.Add(new SqlParameter { ParameterName = "@facility_add", Value = phmsDetails.Facility_list[i].Facility_address, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                        dbparams.Add(new SqlParameter { ParameterName = "@capacity", Value = phmsDetails.Facility_list[i].Capacity, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                        dbparams.Add(new SqlParameter { ParameterName = "@struct_id", Value = phmsDetails.Structure_id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                        dbparams.Add(new SqlParameter { ParameterName = "@district_id", Value = phmsDetails.District_id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                        dbparams.Add(new SqlParameter { ParameterName = "@panchayat_id", Value = phmsDetails.Panchayat_id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                        dbparams.Add(new SqlParameter { ParameterName = "@Is_Facility_Added", Value = phmsDetails.Is_Facility_Added, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                        dbparams.Add(new SqlParameter { ParameterName = "@period", Value = phmsDetails.Facility_list[i].Period, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                        dbparams.Add(new SqlParameter { ParameterName = "@rec_created_ts", Value = DBNull.Value, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
                        dbparams.Add(new SqlParameter { ParameterName = "@rec_created_userid", Value = phmsDetails.Facility_list[i].Rec_created_userid, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                        dbparams.Add(new SqlParameter { ParameterName = "@rec_updated_ts", Value = DBNull.Value, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
                        dbparams.Add(new SqlParameter { ParameterName = "@rec_updated_userid", Value = phmsDetails.Facility_list[i].Rec_updated_userid, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                        dbparams.Add(new SqlParameter { ParameterName = "@response_status", SqlDbType = SqlDbType.VarChar, Size = 50, Direction = ParameterDirection.Output });

                        result = SqlHelper.ExecuteNonQuery<SqlConnection>(spInsertPHMSFacility, dbparams, SqlHelper.ExecutionType.Procedure);
                        insertRowsCount += result["RowsAffected"];
                        insertspOut = DBNull.Value.Equals(result["@response_status"]) ? string.Empty : result["@response_status"];
                    }
                }
            }

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
        /// GetFacilitiesOffline.
        /// </summary>
        /// <param name="districtId">districtId.</param>
        /// <param name="panchayatId">panchayatId.</param>
        /// <returns>List Values.</returns>
        public List<FacilityOnline> GetFacilitiesOffline(int districtId, int panchayatId)
        {
            List<FacilityOnline> facilityOnlinelist = new List<FacilityOnline>();

            List<StructureDetails> list;
            List<StructureDetails> checkList;
            List<DbParameter> dbparams = new List<DbParameter>();
            dbparams.Add(new SqlParameter { ParameterName = "@query_name", Value = qnGetFacilitiesOffline, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@user_id", Value = DBNull.Value, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@panchayat_id", Value = panchayatId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@district_id", Value = districtId, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@facility_id", Value = DBNull.Value, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@struct_id", Value = DBNull.Value, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@struct_type", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@period", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });

            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(spGetFacilitiesOffline, dbparams, SqlHelper.ExecutionType.Procedure);
            if (dt != null && dt.Rows.Count > 0)
            {
                list = SqlHelper.ConvertDataTableToList<StructureDetails>(dt);

                int[] structId = list.Select(x => x.Struct_id).Distinct().ToArray();

                for (int i = 0; i <= structId.Length - 1; i++)
                {
                    checkList = list.Where(x => x.Struct_id == structId[i]).ToList();
                    FacilityOnline facilityOnline = new FacilityOnline();
                    List<Facility> facilityList = new List<Facility>();
                    facilityOnline.Structure_id = checkList[0].Struct_id;
                    facilityOnline.Structure_name = checkList[0].Struct_type;
                    facilityOnline.Panchayat_id = checkList[0].Panchayat_id;
                    facilityOnline.Panchayat_name = checkList[0].Panchayat_name;
                    facilityOnline.Is_Facility_Added = checkList[0].Is_Facility_Added;
                    for (int j = 0; j <= checkList.Count - 1; j++)
                    {
                        Facility facility = new Facility();
                        facility.Facility_id = checkList[j].Facility_id;
                        facility.Facility_name = checkList[j].Facility_name;
                        facility.Facility_address = checkList[j].Facility_add;
                        facility.Capacity = checkList[j].Capacity;
                        facility.Rec_created_userid = checkList[j].Rec_created_userid;
                        facility.Rec_updated_userid = checkList[j].Rec_updated_userid;
                        facility.Period = checkList[j].Period;
                        facilityList.Add(facility);
                    }

                    facilityOnline.Facility_list = facilityList;
                    facilityOnlinelist.Add(facilityOnline);
                }
            }

            return facilityOnlinelist;
        }

        /// <summary>
        /// GetPHMSNoFacilitiesData.
        /// </summary>
        /// <param name="districtId">districtId.</param>
        /// <returns>List Result.</returns>
        public List<NoFacility> GetPHMSNoFacilitiesData(int districtId)
        {
            List<NoFacility> res = new List<NoFacility>();

            List<DbParameter> dbparams = new List<DbParameter>();
            dbparams.Add(new SqlParameter { ParameterName = "@query_name", Value = qnGetPHMSNoFacilitiesData, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@user_id", Value = DBNull.Value, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@panchayat_id", Value = DBNull.Value, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@district_id", Value = districtId, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@facility_id", Value = DBNull.Value, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@struct_id", Value = DBNull.Value, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@struct_type", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@period", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });

            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(spGetFacilitiesOffline, dbparams, SqlHelper.ExecutionType.Procedure);
            if (dt != null && dt.Rows.Count > 0)
            {
                res = SqlHelper.ConvertDataTableToList<NoFacility>(dt);
            }

            return res;
        }

        /// <summary>
        /// GetFacilitiesOnline.
        /// </summary>
        /// <param name="districtId">districtId.</param>
        /// <param name="panchayatId">panchayatId.</param>
        /// <param name="structureId">structureId.</param>
        /// <returns>List Values.</returns>
        public List<FacilityOnline> GetFacilitiesOnline(int districtId, int panchayatId, int structureId)
        {
            List<FacilityOnline> facilityOnlinelist = new List<FacilityOnline>();

            List<StructureDetails> list;
            List<StructureDetails> checkList;
            List<DbParameter> dbparams = new List<DbParameter>();
            dbparams.Add(new SqlParameter { ParameterName = "@query_name", Value = qnGetFacilitiesOnline, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@user_id", Value = DBNull.Value, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@panchayat_id", Value = panchayatId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@district_id", Value = districtId, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@facility_id", Value = DBNull.Value, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@struct_id", Value = structureId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@struct_type", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@period", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });

            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(spGetFacilitiesOffline, dbparams, SqlHelper.ExecutionType.Procedure);
            if (dt != null && dt.Rows.Count > 0)
            {
                list = SqlHelper.ConvertDataTableToList<StructureDetails>(dt);

                int[] structId = list.Select(x => x.Struct_id).Distinct().ToArray();

                for (int i = 0; i <= structId.Length - 1; i++)
                {
                    checkList = list.Where(x => x.Struct_id == structId[i]).ToList();
                    FacilityOnline facilityOnline = new FacilityOnline();
                    List<Facility> facilityList = new List<Facility>();
                    facilityOnline.Structure_id = checkList[0].Struct_id;
                    facilityOnline.Structure_name = checkList[0].Struct_type;
                    facilityOnline.Panchayat_id = checkList[0].Panchayat_id;
                    facilityOnline.Panchayat_name = checkList[0].Panchayat_name;
                    facilityOnline.Is_Facility_Added = checkList[0].Is_Facility_Added;
                    for (int j = 0; j <= checkList.Count - 1; j++)
                    {
                        Facility facility = new Facility();
                        facility.Facility_id = checkList[j].Facility_id;
                        facility.Facility_name = checkList[j].Facility_name;
                        facility.Facility_address = checkList[j].Facility_add;
                        facility.Capacity = checkList[j].Capacity;
                        facility.Rec_created_userid = checkList[j].Rec_created_userid;
                        facility.Rec_updated_userid = checkList[j].Rec_updated_userid;
                        facility.Period = checkList[j].Period;
                        facilityList.Add(facility);
                    }

                    facilityOnline.Facility_list = facilityList;
                    facilityOnlinelist.Add(facilityOnline);
                }
            }

            return facilityOnlinelist;
        }

        /// <summary>
        /// InsertHortCropCoveragetargetPanchayat.
        /// </summary>
        /// <param name="crop">crop.</param>
        /// <returns>Response.</returns>
        public List<HortCropCoverageTargetPanchytApprovalResponse> InsertHortCropCoveragetargetPanchayat(List<HortCropCoverageAimPancht> crop)
        {
            Dictionary<string, dynamic> result;
            int insertRowsCount = 0;

            List<HortCropCoverageTargetPanchytApprovalResponse> response = new List<HortCropCoverageTargetPanchytApprovalResponse>();
            foreach (HortCropCoverageAimPancht cclist in crop)
            {
                foreach (HortCropTgtEntity cpvalue in cclist.CropValues)
                {
                    List<DbParameter> dbparams = new List<DbParameter>();

                    dbparams.Add(new SqlParameter { ParameterName = "@season_id", Value = cclist.Season_Id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@panchayat_id", Value = cclist.Panchayat_Id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@crop_id", Value = cpvalue.Crop_Id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@area_target", Value = cpvalue.Area_Target, SqlDbType = SqlDbType.Decimal, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@adh_approval_flag", Value = string.IsNullOrEmpty(cpvalue.Adh_approval_flag) ? DBNull.Value : (object)cpvalue.Adh_approval_flag, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@adh_approval_reason", Value = string.IsNullOrEmpty(cpvalue.Adh_approval_reason) ? DBNull.Value : (object)cpvalue.Adh_approval_reason, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@adh_approved_userid", Value = cpvalue.Adh_approved_userid == 0 ? DBNull.Value : (object)cpvalue.Adh_approved_userid, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@adh_approved_date", Value = cpvalue.Adh_approved_date == DateTime.MinValue ? DBNull.Value : (object)cpvalue.Adh_approved_date, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@bho_approval_flag", Value = string.IsNullOrEmpty(cpvalue.Bho_approval_flag) ? DBNull.Value : (object)cpvalue.Bho_approval_flag, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@bho_approval_reason", Value = string.IsNullOrEmpty(cpvalue.Bho_approval_reason) ? DBNull.Value : (object)cpvalue.Bho_approval_reason, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@bho_approved_userid", Value = cpvalue.Bho_approved_userid == 0 ? DBNull.Value : (object)cpvalue.Bho_approved_userid, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@bho_approved_date", Value = cpvalue.Bho_approved_date == DateTime.MinValue ? DBNull.Value : (object)cpvalue.Bho_approved_date, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@ac_submitted_userid", Value = cpvalue.Ac_submitted_userid == 0 ? DBNull.Value : (object)cpvalue.Ac_submitted_userid, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@ac_submitted_date", Value = cpvalue.Ac_submitted_date == DateTime.MinValue ? DBNull.Value : (object)cpvalue.Ac_submitted_date, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@response_status", SqlDbType = SqlDbType.NVarChar, Size = 1000, Direction = ParameterDirection.Output });
                    dbparams.Add(new SqlParameter { ParameterName = "@submission_source", Value = string.IsNullOrEmpty(cpvalue.Submission_source) ? DBNull.Value : (object)cpvalue.Submission_source, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@rec_updated_userid", Value = cpvalue.Rec_updated_userid == 0 ? DBNull.Value : (object)cpvalue.Rec_updated_userid, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@rec_updated_date", Value = cpvalue.Rec_updated_date == DateTime.MinValue ? DBNull.Value : (object)cpvalue.Rec_updated_date, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@ac_submit_flag", Value = string.IsNullOrEmpty(cpvalue.Ac_submit_flag) ? DBNull.Value : (object)cpvalue.Ac_submit_flag, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@bho_add_edit_flag", Value = string.IsNullOrEmpty(cpvalue.Bho_add_edit_flag) ? DBNull.Value : (object)cpvalue.Bho_add_edit_flag, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@adh_add_edit_flag", Value = string.IsNullOrEmpty(cpvalue.Adh_add_edit_flag) ? DBNull.Value : (object)cpvalue.Adh_add_edit_flag, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });

                    result = SqlHelper.ExecuteNonQuery<SqlConnection>(spInsertHortCropCoveragetargetPanchayat, dbparams, SqlHelper.ExecutionType.Procedure);
                    insertRowsCount += result["RowsAffected"];

                    string spOut = DBNull.Value.Equals(result["@response_status"]) ? string.Empty : result["@response_status"];

                    if (!string.IsNullOrEmpty(spOut))
                    {
                        HortCropCoverageTargetPanchytApprovalResponse respobj = new HortCropCoverageTargetPanchytApprovalResponse();

                        foreach (var keyvaluepair in spOut.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries))
                        {
                            string[] splitteddata = keyvaluepair.Split(new string[] { ":" }, StringSplitOptions.RemoveEmptyEntries);

                            if (splitteddata[0].Trim().Equals("Status"))
                            {
                                respobj.Status = splitteddata[1].ToString();
                            }
                            else if (splitteddata[0].Trim().Equals("Reason"))
                            {
                                respobj.Reason = splitteddata[1].ToString();
                            }
                            else if (splitteddata[0].Trim().Equals("PanchayatId"))
                            {
                                respobj.PanchayatId = splitteddata[1].ToString();
                            }
                            else if (splitteddata[0].Trim().Equals("CropId"))
                            {
                                respobj.CropId = splitteddata[1].ToString();
                            }
                            else if (splitteddata[0].Trim().Equals("SeasonId"))
                            {
                                respobj.SeasonId = splitteddata[1].ToString();
                            }
                        }

                        response.Add(respobj);
                    }
                }
            }

            return response;
        }

        /// <summary>
        /// InsertHortCropCoveragetargetPanchayatApproval.
        /// </summary>
        /// <param name="cropCoverageTarget">cropCoverageTarget.</param>
        /// <returns>List Response .</returns>
        public List<HortiCropCoverageTargetBlockApproval> InsertHortCropCoveragetargetPanchayatApproval(HortCropCoverageAim cropCoverageTarget)
        {
            int insertRowsCount = 0;

            List<HortiCropCoverageTargetBlockApproval> response = new List<HortiCropCoverageTargetBlockApproval>();

            Dictionary<string, dynamic> result;

            if (cropCoverageTarget != null)
            {
                List<DbParameter> dbparamsBlocks = new List<DbParameter>();
                List<DbParameter> dbparamsPanchayat = new List<DbParameter>();
                if (cropCoverageTarget.BlockList != null)
                {
                    foreach (var block in cropCoverageTarget.BlockList)
                    {
                        HortiCropCoverageTargetBlockApproval respblkobj = new HortiCropCoverageTargetBlockApproval();
                        if (block.ADH_Approved_userid != 0 && block.ADH_Approval_flag != "N")
                        {
                            dbparamsBlocks.Add(new SqlParameter { ParameterName = "@Season_Id", Value = cropCoverageTarget.Season_Id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                            dbparamsBlocks.Add(new SqlParameter { ParameterName = "@Block_Id", Value = block.Block_Id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                            dbparamsBlocks.Add(new SqlParameter { ParameterName = "@Crop_Id", Value = cropCoverageTarget.Crop_Id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                            dbparamsBlocks.Add(new SqlParameter { ParameterName = "@ADH_approval_flag", Value = string.IsNullOrEmpty(block.ADH_Approval_flag) ? DBNull.Value : (object)block.ADH_Approval_flag, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                            dbparamsBlocks.Add(new SqlParameter { ParameterName = "@ADH_approval_reason", Value = string.IsNullOrEmpty(block.ADH_Approval_Reason) ? DBNull.Value : (object)block.ADH_Approval_Reason, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                            dbparamsBlocks.Add(new SqlParameter { ParameterName = "@ADH_approved_userid", Value = block.ADH_Approved_userid == 0 ? DBNull.Value : (object)block.ADH_Approved_userid, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                            dbparamsBlocks.Add(new SqlParameter { ParameterName = "@ADH_approved_date", Value = block.ADH_Approved_date == DateTime.MinValue ? DBNull.Value : (object)block.ADH_Approved_date, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
                            dbparamsBlocks.Add(new SqlParameter { ParameterName = "@response_status", SqlDbType = SqlDbType.NVarChar, Size = 1000, Direction = ParameterDirection.Output });

                            result = SqlHelper.ExecuteNonQuery<SqlConnection>(spInsertHortCropCoveragetargetPanchayatApproval, dbparamsBlocks, SqlHelper.ExecutionType.Procedure);
                            insertRowsCount += result["RowsAffected"];

                            string spBlockOut = DBNull.Value.Equals(result["@response_status"]) ? string.Empty : result["@response_status"];

                            if (!string.IsNullOrEmpty(spBlockOut))
                            {
                                foreach (var keyvaluepairblk in spBlockOut.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries))
                                {
                                    string[] splittedblockdata = keyvaluepairblk.Split(new string[] { ":" }, StringSplitOptions.RemoveEmptyEntries);

                                    if (splittedblockdata[0].Trim().Equals("Status"))
                                    {
                                        respblkobj.Status = splittedblockdata[1].ToString();
                                    }
                                    else if (splittedblockdata[0].Trim().Equals("Reason"))
                                    {
                                        respblkobj.Reason = splittedblockdata[1].ToString();
                                    }
                                    else if (splittedblockdata[0].Trim().Equals("BlockId"))
                                    {
                                        respblkobj.BlockId = splittedblockdata[1].ToString();
                                    }
                                    else if (splittedblockdata[0].Trim().Equals("CropId"))
                                    {
                                        respblkobj.CropId = splittedblockdata[1].ToString();
                                    }
                                    else if (splittedblockdata[0].Trim().Equals("SeasonId"))
                                    {
                                        respblkobj.SeasonId = splittedblockdata[1].ToString();
                                    }
                                }
                            }

                            dbparamsBlocks.Clear();
                        }

                        if (block.PanchayatList != null)
                        {
                            foreach (var panchayat in block.PanchayatList)
                            {
                                dbparamsPanchayat.Add(new SqlParameter { ParameterName = "@season_id", Value = cropCoverageTarget.Season_Id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                                dbparamsPanchayat.Add(new SqlParameter { ParameterName = "@panchayat_id", Value = panchayat.Panchayat_Id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                                dbparamsPanchayat.Add(new SqlParameter { ParameterName = "@crop_id", Value = cropCoverageTarget.Crop_Id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                                dbparamsPanchayat.Add(new SqlParameter { ParameterName = "@area_target", Value = panchayat.Area_Target, SqlDbType = SqlDbType.Decimal, Direction = ParameterDirection.Input });
                                dbparamsPanchayat.Add(new SqlParameter { ParameterName = "@adh_approval_flag", Value = string.IsNullOrEmpty(panchayat.ADH_Approval_flag) ? DBNull.Value : (object)panchayat.ADH_Approval_flag, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                                dbparamsPanchayat.Add(new SqlParameter { ParameterName = "@adh_approval_reason", Value = string.IsNullOrEmpty(panchayat.ADH_Approval_Reason) ? DBNull.Value : (object)panchayat.ADH_Approval_Reason, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                                dbparamsPanchayat.Add(new SqlParameter { ParameterName = "@adh_approved_userid", Value = panchayat.ADH_Approved_userid == 0 ? DBNull.Value : (object)panchayat.ADH_Approved_userid, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                                dbparamsPanchayat.Add(new SqlParameter { ParameterName = "@adh_approved_date", Value = panchayat.ADH_Approved_date == DateTime.MinValue ? DBNull.Value : (object)panchayat.ADH_Approved_date, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
                                dbparamsPanchayat.Add(new SqlParameter { ParameterName = "@bho_approval_flag", Value = string.IsNullOrEmpty(panchayat.BHO_Approval_flag) ? DBNull.Value : (object)panchayat.BHO_Approval_flag, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                                dbparamsPanchayat.Add(new SqlParameter { ParameterName = "@bho_approval_reason", Value = string.IsNullOrEmpty(panchayat.BHO_Approval_Reason) ? DBNull.Value : (object)panchayat.BHO_Approval_Reason, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                                dbparamsPanchayat.Add(new SqlParameter { ParameterName = "@bho_approved_userid", Value = panchayat.BHO_Approved_userid == 0 ? DBNull.Value : (object)panchayat.BHO_Approved_userid, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                                dbparamsPanchayat.Add(new SqlParameter { ParameterName = "@bho_approved_date", Value = panchayat.BHO_Approved_date == DateTime.MinValue ? DBNull.Value : (object)panchayat.BHO_Approved_date, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
                                dbparamsPanchayat.Add(new SqlParameter { ParameterName = "@ac_submitted_userid", Value = panchayat.AC_Submitted_userid == 0 ? DBNull.Value : (object)panchayat.AC_Submitted_userid, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                                dbparamsPanchayat.Add(new SqlParameter { ParameterName = "@ac_submitted_date", Value = panchayat.AC_Submitted_date == DateTime.MinValue ? DBNull.Value : (object)panchayat.AC_Submitted_date, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
                                dbparamsPanchayat.Add(new SqlParameter { ParameterName = "@response_status", SqlDbType = SqlDbType.NVarChar, Size = 1000, Direction = ParameterDirection.Output });
                                dbparamsPanchayat.Add(new SqlParameter { ParameterName = "@submission_source", Value = string.IsNullOrEmpty(panchayat.Submission_source) ? DBNull.Value : (object)panchayat.Submission_source, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                                dbparamsPanchayat.Add(new SqlParameter { ParameterName = "@ac_submit_flag", Value = string.IsNullOrEmpty(panchayat.Ac_submit_flag) ? DBNull.Value : (object)panchayat.Ac_submit_flag, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                                dbparamsPanchayat.Add(new SqlParameter { ParameterName = "@bho_add_edit_flag", Value = string.IsNullOrEmpty(panchayat.Bho_add_edit_flag) ? DBNull.Value : (object)panchayat.Bho_add_edit_flag, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                                dbparamsPanchayat.Add(new SqlParameter { ParameterName = "@bho_add_edit_flag", Value = string.IsNullOrEmpty(panchayat.Adh_add_edit_flag) ? DBNull.Value : (object)panchayat.Adh_add_edit_flag, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                                result = SqlHelper.ExecuteNonQuery<SqlConnection>(spcropcvrgtargetpnchytappr, dbparamsPanchayat, SqlHelper.ExecutionType.Procedure);
                                insertRowsCount += result["RowsAffected"];
                                string spOut = DBNull.Value.Equals(result["@response_status"]) ? string.Empty : result["@response_status"];

                                if (!string.IsNullOrEmpty(spOut))
                                {
                                    HortiCropCoverageTargetPanchytApprovalResponse respobj = new HortiCropCoverageTargetPanchytApprovalResponse();

                                    foreach (var keyvaluepair in spOut.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries))
                                    {
                                        string[] splitteddata = keyvaluepair.Split(new string[] { ":" }, StringSplitOptions.RemoveEmptyEntries);

                                        if (splitteddata[0].Trim().Equals("Status"))
                                        {
                                            respobj.Status = splitteddata[1].ToString();
                                        }
                                        else if (splitteddata[0].Trim().Equals("Reason"))
                                        {
                                            respobj.Reason = splitteddata[1].ToString();
                                        }
                                        else if (splitteddata[0].Trim().Equals("PanchayatId"))
                                        {
                                            respobj.PanchayatId = splitteddata[1].ToString();
                                        }
                                        else if (splitteddata[0].Trim().Equals("CropId"))
                                        {
                                            respobj.CropId = splitteddata[1].ToString();
                                        }
                                        else if (splitteddata[0].Trim().Equals("SeasonId"))
                                        {
                                            respobj.SeasonId = splitteddata[1].ToString();
                                        }
                                    }

                                    respblkobj.PanchayatResponse.Add(respobj);
                                }

                                dbparamsPanchayat.Clear();
                            }

                            response.Add(respblkobj);
                        }
                    }
                }
            }

            return response;
        }

        /// <summary>
        /// InsertHortCropCoveragetargetBlock.
        /// </summary>
        /// <param name="hortCrpCvgTgtBlock">hortCrpCvgTgtBlock.</param>
        /// <returns>DB Success Response and List.</returns>
        public string InsertHortCropCoveragetargetBlock(HortCropCoverageTargetBlockApproval hortCrpCvgTgtBlock)
        {
            string spOut = string.Empty;
            if (hortCrpCvgTgtBlock != null)
            {
                Dictionary<string, dynamic> result;

                List<DbParameter> dbparams = new List<DbParameter>();
                dbparams.Add(new SqlParameter { ParameterName = "@Season_Id", Value = hortCrpCvgTgtBlock.Season_id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                dbparams.Add(new SqlParameter { ParameterName = "@Block_Id", Value = hortCrpCvgTgtBlock.Block_id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                dbparams.Add(new SqlParameter { ParameterName = "@Crop_Id", Value = hortCrpCvgTgtBlock.Crop_id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                dbparams.Add(new SqlParameter { ParameterName = "@ADH_approval_flag", Value = hortCrpCvgTgtBlock.Adh_approval_flag, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparams.Add(new SqlParameter { ParameterName = "@ADH_approval_reason", Value = hortCrpCvgTgtBlock.Adh_approval_reason, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparams.Add(new SqlParameter { ParameterName = "@ADH_approved_userid", Value = hortCrpCvgTgtBlock.Adh_approved_userid, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                dbparams.Add(new SqlParameter { ParameterName = "@ADH_approved_date", Value = hortCrpCvgTgtBlock.Adh_approved_date, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
                dbparams.Add(new SqlParameter { ParameterName = "@response_status", SqlDbType = SqlDbType.NVarChar, Size = 200, Direction = ParameterDirection.Output });
                result = SqlHelper.ExecuteNonQuery<SqlConnection>(spInsertHortCropCoveragetargetPanchayatApproval, dbparams, SqlHelper.ExecutionType.Procedure);

                spOut = DBNull.Value.Equals(result["@response_status"]) ? string.Empty : result["@response_status"];
            }

            return spOut;
        }

        /// <summary>
        /// InsertHortDamageApproval.
        /// </summary>
        /// <param name="cropDamage">cropDamage.</param>
        /// <returns>Insert Response and List.</returns>
        public List<CropDamageBlockApprovalResponse> InsertHortDamageApproval(CropDamage cropDamage)
        {
            int insertRowsCount = 0;

            List<CropDamageBlockApprovalResponse> response = new List<CropDamageBlockApprovalResponse>();
            Dictionary<string, dynamic> result;
            if (cropDamage != null)
            {
                List<DbParameter> dbparamsBlocks = new List<DbParameter>();

                List<DbParameter> dbparamsPanchayat = new List<DbParameter>();
                if (cropDamage.BlockList != null)
                {
                    foreach (var block in cropDamage.BlockList)
                    {
                        CropDamageBlockApprovalResponse respblkobj = new CropDamageBlockApprovalResponse();
                        if (block.DAO_Approved_userid != 0 && block.DAO_Approval_flag != "N")
                        {
                            dbparamsBlocks.Add(new SqlParameter { ParameterName = "@Season_Id", Value = cropDamage.Season_Id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                            dbparamsBlocks.Add(new SqlParameter { ParameterName = "@Block_Id", Value = block.Block_Id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                            dbparamsBlocks.Add(new SqlParameter { ParameterName = "@Crop_Id", Value = cropDamage.Crop_Id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                            dbparamsBlocks.Add(new SqlParameter { ParameterName = "@damage_reason", Value = block.Damage_Reason, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });

                            dbparamsBlocks.Add(new SqlParameter { ParameterName = "@dao_approval_flag", Value = string.IsNullOrEmpty(block.DAO_Approval_flag) ? DBNull.Value : (object)block.DAO_Approval_flag, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                            dbparamsBlocks.Add(new SqlParameter { ParameterName = "@dao_approval_reason", Value = string.IsNullOrEmpty(block.DAO_Approval_Reason) ? DBNull.Value : (object)block.DAO_Approval_Reason, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                            dbparamsBlocks.Add(new SqlParameter { ParameterName = "@dao_approved_userid", Value = block.DAO_Approved_userid == 0 ? DBNull.Value : (object)block.DAO_Approved_userid, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                            dbparamsBlocks.Add(new SqlParameter { ParameterName = "@dao_approved_date", Value = block.DAO_Approved_date == DateTime.MinValue ? DBNull.Value : (object)block.DAO_Approved_date, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
                            dbparamsBlocks.Add(new SqlParameter { ParameterName = "@response_status", SqlDbType = SqlDbType.NVarChar, Size = 200, Direction = ParameterDirection.Output });

                            result = SqlHelper.ExecuteNonQuery<SqlConnection>(spInsertHortDamageApproval, dbparamsBlocks, SqlHelper.ExecutionType.Procedure);
                            insertRowsCount += result["RowsAffected"];

                            string spBlockOut = DBNull.Value.Equals(result["@response_status"]) ? string.Empty : result["@response_status"];

                            if (!string.IsNullOrEmpty(spBlockOut))
                            {
                                foreach (var keyvaluepairblk in spBlockOut.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries))
                                {
                                    string[] splittedblockdata = keyvaluepairblk.Split(new string[] { ":" }, StringSplitOptions.RemoveEmptyEntries);

                                    if (splittedblockdata[0].Trim().Equals("BlockId"))
                                    {
                                        respblkobj.BlockId = splittedblockdata[1].ToString();
                                    }
                                    else if (splittedblockdata[0].Trim().Equals("Crop_Id"))
                                    {
                                        respblkobj.Crop_Id = splittedblockdata[1].ToString();
                                    }
                                    else if (splittedblockdata[0].Trim().Equals("SeasonId"))
                                    {
                                        respblkobj.SeasonId = splittedblockdata[1].ToString();
                                    }
                                    else if (splittedblockdata[0].Trim().Equals("DamageReason"))
                                    {
                                        respblkobj.DamageReason = splittedblockdata[1].ToString();
                                    }
                                }
                            }

                            dbparamsBlocks.Clear();
                        }

                        if (block.PanchayatList != null)
                        {
                            foreach (var panchayat in block.PanchayatList)
                            {
                                foreach (var damage in panchayat.DamageList)
                                {
                                    dbparamsPanchayat.Add(new SqlParameter { ParameterName = "@reported_date", Value = panchayat.Reported_date == DateTime.MinValue ? DBNull.Value : (object)panchayat.Reported_date, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
                                    dbparamsPanchayat.Add(new SqlParameter { ParameterName = "@season_id", Value = cropDamage.Season_Id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                                    dbparamsPanchayat.Add(new SqlParameter { ParameterName = "@panchayat_id", Value = panchayat.Panchayat_Id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                                    dbparamsPanchayat.Add(new SqlParameter { ParameterName = "@crop_id", Value = cropDamage.Crop_Id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                                    dbparamsPanchayat.Add(new SqlParameter { ParameterName = "@irrigated_dmg_area", Value = damage.Irrigated_Dmg_Area, SqlDbType = SqlDbType.Decimal, Direction = ParameterDirection.Input });
                                    dbparamsPanchayat.Add(new SqlParameter { ParameterName = "@damage_reason", Value = string.IsNullOrEmpty(damage.Damage_Reason) ? DBNull.Value : (object)damage.Damage_Reason, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                                    dbparamsPanchayat.Add(new SqlParameter { ParameterName = "@dao_approval_flag", Value = string.IsNullOrEmpty(damage.DAO_Approval_flag) ? DBNull.Value : (object)damage.DAO_Approval_flag, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                                    dbparamsPanchayat.Add(new SqlParameter { ParameterName = "@dao_approval_reason", Value = string.IsNullOrEmpty(damage.DAO_Approval_Reason) ? DBNull.Value : (object)damage.DAO_Approval_Reason, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                                    dbparamsPanchayat.Add(new SqlParameter { ParameterName = "@dao_approved_userid", Value = damage.DAO_Approved_userid == 0 ? DBNull.Value : (object)damage.DAO_Approved_userid, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                                    dbparamsPanchayat.Add(new SqlParameter { ParameterName = "@dao_approved_date", Value = damage.DAO_Approved_date == DateTime.MinValue ? DBNull.Value : (object)damage.DAO_Approved_date, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
                                    dbparamsPanchayat.Add(new SqlParameter { ParameterName = "@bao_approval_flag", Value = string.IsNullOrEmpty(damage.BAO_Approval_flag) ? DBNull.Value : (object)damage.BAO_Approval_flag, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                                    dbparamsPanchayat.Add(new SqlParameter { ParameterName = "@bao_approval_reason", Value = string.IsNullOrEmpty(damage.BAO_Approval_Reason) ? DBNull.Value : (object)damage.BAO_Approval_Reason, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                                    dbparamsPanchayat.Add(new SqlParameter { ParameterName = "@bao_approved_userid", Value = damage.BAO_Approved_userid == 0 ? DBNull.Value : (object)damage.BAO_Approved_userid, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                                    dbparamsPanchayat.Add(new SqlParameter { ParameterName = "@bao_approved_date", Value = damage.BAO_Approved_date == DateTime.MinValue ? DBNull.Value : (object)damage.BAO_Approved_date, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
                                    dbparamsPanchayat.Add(new SqlParameter { ParameterName = "@ac_submitted_userid", Value = damage.AC_Submitted_userid == 0 ? DBNull.Value : (object)damage.AC_Submitted_userid, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                                    dbparamsPanchayat.Add(new SqlParameter { ParameterName = "@ac_submitted_date", Value = damage.AC_Submitted_date == DateTime.MinValue ? DBNull.Value : (object)damage.AC_Submitted_date, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
                                    dbparamsPanchayat.Add(new SqlParameter { ParameterName = "@submission_source", Value = string.IsNullOrEmpty(damage.Submission_source) ? DBNull.Value : (object)damage.Submission_source, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                                    dbparamsPanchayat.Add(new SqlParameter { ParameterName = "@response_status", SqlDbType = SqlDbType.NVarChar, Size = 200, Direction = ParameterDirection.Output });
                                    result = SqlHelper.ExecuteNonQuery<SqlConnection>(spcropdmgpnchytappr, dbparamsPanchayat, SqlHelper.ExecutionType.Procedure);
                                    insertRowsCount += result["RowsAffected"];

                                    string spOut = DBNull.Value.Equals(result["@response_status"]) ? string.Empty : result["@response_status"];
                                    if (!string.IsNullOrEmpty(spOut))
                                    {
                                        CropDamagePanchayatResponse respobj = new CropDamagePanchayatResponse();

                                        foreach (var keyvaluepair in spOut.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries))
                                        {
                                            string[] splitteddata = keyvaluepair.Split(new string[] { ":" }, StringSplitOptions.RemoveEmptyEntries);

                                            if (splitteddata[0].Trim().Equals("Status"))
                                            {
                                                respobj.Status = splitteddata[1].ToString();
                                            }
                                            else if (splitteddata[0].Trim().Equals("Reason"))
                                            {
                                                respobj.Reason = splitteddata[1].ToString();
                                            }
                                            else if (splitteddata[0].Trim().Equals("PanchayatId"))
                                            {
                                                respobj.PanchayatId = splitteddata[1].ToString();
                                            }
                                            else if (splitteddata[0].Trim().Equals("CropId"))
                                            {
                                                respobj.CropId = splitteddata[1].ToString();
                                            }
                                            else if (splitteddata[0].Trim().Equals("SeasonId"))
                                            {
                                                respobj.SeasonId = splitteddata[1].ToString();
                                            }
                                            else if (splitteddata[0].Trim().Equals("DamageReason"))
                                            {
                                                respobj.DamageReason = splitteddata[1].ToString();
                                            }
                                        }

                                        respblkobj.PanchayatResponse.Add(respobj);
                                    }

                                    dbparamsPanchayat.Clear();
                                }
                            }

                            response.Add(respblkobj);
                        }
                    }
                }
            }

            return response;
        }

        /// <summary>
        /// GetHortDamagePancht.
        /// </summary>
        /// <param name="seasonId">seasonId.</param>
        /// <param name="panchayatId">panchayatId.</param>
        /// <returns>List Values.</returns>
        public List<CropDamagePancht> GetHortDamagePancht(string seasonId, string panchayatId)
        {
            List<DtoCropDamageDetails> dTOCropDamageDetails = new List<DtoCropDamageDetails>();
            List<CropDamagePancht> list = new List<CropDamagePancht>();
            CropDamagePancht cropDamagePancht = new CropDamagePancht();
            cropDamagePancht.DmgCropList = new List<CropDetailsPanchayat>();

            List<DbParameter> dbparamsCropInfo = new List<DbParameter>();
            dbparamsCropInfo.Add(new SqlParameter { ParameterName = "@query_name", Value = qnGetHortDamagePancht, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfo.Add(new SqlParameter { ParameterName = "@District_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfo.Add(new SqlParameter { ParameterName = "@Block_id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfo.Add(new SqlParameter { ParameterName = "@Panchayat_Id", Value = string.IsNullOrEmpty(panchayatId) ? DBNull.Value : (object)panchayatId, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfo.Add(new SqlParameter { ParameterName = "@Season_Id", Value = string.IsNullOrEmpty(seasonId) ? DBNull.Value : (object)seasonId, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfo.Add(new SqlParameter { ParameterName = "@Crop_id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfo.Add(new SqlParameter { ParameterName = "@Last_Refresh_Ts", Value = DBNull.Value, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });

            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(spGetHortDamagePancht, dbparamsCropInfo, SqlHelper.ExecutionType.Procedure);

            if (dt != null && dt.Rows.Count > 0)
            {
                cropDamagePancht = SqlHelper.ConvertDataTableToList<CropDamagePancht>(dt)[0];
                cropDamagePancht.DmgCropList = SqlHelper.ConvertDataTableToList<CropDetailsPanchayat>(dt).GroupBy(x => x.Crop_Id).Select(x => x.First()).ToList();
                dTOCropDamageDetails = SqlHelper.ConvertDataTableToList<DtoCropDamageDetails>(dt);
            }

            if (dTOCropDamageDetails.Any())
            {
                for (int i = 0; i < cropDamagePancht.DmgCropList.Count; i++)
                {
                    var damageDetails = dTOCropDamageDetails.Where(x => x.Crop_Id == cropDamagePancht.DmgCropList[i].Crop_Id).ToList();
                    foreach (var damage in damageDetails)
                    {
                        cropDamagePancht.DmgCropList[i].Damagedetails.Add(new DamageEntity
                        {
                            Reported_Date = damage.Reported_Date,
                            Irrigated_Dmg_Area = damage.Irrigated_Dmg_Area,
                            Damage_Reason = damage.Damage_Reason,
                            AC_Submitted_date = damage.AC_Submitted_date,
                            AC_Submitted_userid = damage.AC_Submitted_userid,
                            BAO_Approval_flag = damage.BAO_Approval_flag,
                            BAO_Approval_Reason = damage.BAO_Approval_Reason,
                            BAO_Approved_date = damage.BAO_Approved_date,
                            BAO_Approved_userid = damage.BAO_Approved_userid,
                            DAO_Approval_flag = damage.DAO_Approval_flag,
                            DAO_Approval_Reason = damage.DAO_Approval_Reason,
                            DAO_Approved_date = damage.DAO_Approved_date,
                            DAO_Approved_userid = damage.DAO_Approved_userid,
                            Ac_submitted_username = damage.Ac_submitted_username,
                            Bao_approved_username = damage.Bao_approved_username,
                            Dao_approved_username = damage.Dao_approved_username,
                        });
                    }
                }

                list.Add(cropDamagePancht);
            }

            return list;
        }

        /// <summary>
        /// GetHortDamageBlock.
        /// </summary>
        /// <param name="blockId">blockId.</param>
        /// <returns>List Details.</returns>
        public List<CropDamageBlock> GetHortDamageBlock(string blockId)
        {
            List<CropDamageBlock> list = new List<CropDamageBlock>();

            List<DbParameter> dbparamsCropInfo = new List<DbParameter>();
            dbparamsCropInfo.Add(new SqlParameter { ParameterName = "@query_name", Value = qnGetHortDamageBlock, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfo.Add(new SqlParameter { ParameterName = "@District_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfo.Add(new SqlParameter { ParameterName = "@Block_id", Value = string.IsNullOrEmpty(blockId) ? DBNull.Value : (object)blockId, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfo.Add(new SqlParameter { ParameterName = "@Panchayat_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfo.Add(new SqlParameter { ParameterName = "@Season_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfo.Add(new SqlParameter { ParameterName = "@Crop_id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfo.Add(new SqlParameter { ParameterName = "@Last_Refresh_Ts", Value = DBNull.Value, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });

            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(spGetHortDamageBlock, dbparamsCropInfo, SqlHelper.ExecutionType.Procedure);
            CropDamageBlock cropDamageBlock;
            if (dt != null && dt.Rows.Count > 0)
            {
                cropDamageBlock = SqlHelper.ConvertDataTableToList<CropDamageBlock>(dt)[0];
                cropDamageBlock.DmgCropList = new List<CropDmgEntity>();
                cropDamageBlock.DmgCropList = SqlHelper.ConvertDataTableToList<CropDmgEntity>(dt);
                list.Add(cropDamageBlock);
            }

            return list;
        }

        /// <summary>
        /// InsertHortAggregateCropDamage.
        /// </summary>
        /// <param name="coldStorage">coldStorage.</param>
        /// <returns>DB Insert Success Response.</returns>
        public int InsertHortAggregateCropDamage(ColdStorage coldStorage)
        {
            int insertRowsCount = 0;

            if (coldStorage != null)
            {
                List<DbParameter> dbparams = new List<DbParameter>();
                dbparams.Add(new SqlParameter { ParameterName = "@crop_id", Value = coldStorage.Crop_Id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                dbparams.Add(new SqlParameter { ParameterName = "@District_Id", Value = coldStorage.District_Id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                dbparams.Add(new SqlParameter { ParameterName = "@Crop_name", Value = coldStorage.Crop_name, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparams.Add(new SqlParameter { ParameterName = "@Strg_ID", Value = coldStorage.Strg_ID, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                dbparams.Add(new SqlParameter { ParameterName = "@week_open_bal", Value = coldStorage.Week_open_bal, SqlDbType = SqlDbType.Decimal, Direction = ParameterDirection.Input });
                dbparams.Add(new SqlParameter { ParameterName = "@curr_open_bal", Value = coldStorage.Curr_open_bal, SqlDbType = SqlDbType.Decimal, Direction = ParameterDirection.Input });
                dbparams.Add(new SqlParameter { ParameterName = "@deposit", Value = coldStorage.Deposit, SqlDbType = SqlDbType.Decimal, Direction = ParameterDirection.Input });
                dbparams.Add(new SqlParameter { ParameterName = "@release", Value = coldStorage.Release, SqlDbType = SqlDbType.Decimal, Direction = ParameterDirection.Input });
                dbparams.Add(new SqlParameter { ParameterName = "@Submitted_date", Value = coldStorage.Submitted_date == DateTime.MinValue ? DBNull.Value : (object)coldStorage.Submitted_date, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
                dbparams.Add(new SqlParameter { ParameterName = "@Submitted_userid", Value = coldStorage.Submitted_userid, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                Dictionary<string, dynamic> result = SqlHelper.ExecuteNonQuery<SqlConnection>(spInsertColdStorage, dbparams, SqlHelper.ExecutionType.Procedure);

                insertRowsCount += result["RowsAffected"];
            }

            if (insertRowsCount != 0)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// InsertHortAggCropCoverageActual.
        /// </summary>
        /// <param name="hortAggCropCoverageActual">hortAggCropCoverageActual.</param>
        /// <returns>Insert Response.</returns>
        public int InsertHortAggCropCoverageActual(HortAggCropCoverageActual hortAggCropCoverageActual)
        {
            int insertRowsCount = 0;
            if (hortAggCropCoverageActual != null)
            {
                List<DbParameter> dbparams = new List<DbParameter>();
                dbparams.Add(new SqlParameter { ParameterName = "@reported_date", Value = hortAggCropCoverageActual.Reported_date, SqlDbType = SqlDbType.Date, Direction = ParameterDirection.Input });
                dbparams.Add(new SqlParameter { ParameterName = "@panchayat_id", Value = hortAggCropCoverageActual.Panchayat_id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                dbparams.Add(new SqlParameter { ParameterName = "@season_id", Value = hortAggCropCoverageActual.Season_id, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparams.Add(new SqlParameter { ParameterName = "@crop_id", Value = hortAggCropCoverageActual.Crop_id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                Dictionary<string, dynamic> result = SqlHelper.ExecuteNonQuery<SqlConnection>(spInsertHortAggCropCoverageActual, dbparams, SqlHelper.ExecutionType.Procedure);
                insertRowsCount += result["RowsAffected"];
            }

            if (insertRowsCount != 0)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// HortAutoApprovalCoverageActlBlock.
        /// </summary>
        /// <returns>Insert Success Status.</returns>
        public int HortAutoApprovalCoverageActlBlock()
        {
            int insertRowsCount = 0;
            Dictionary<string, dynamic> result = SqlHelper.ExecuteNonQuery<SqlConnection>(spHortAutoApprovalCoverageActlBlock, null, SqlHelper.ExecutionType.Procedure);
            insertRowsCount += result["RowsAffected"];

            if (insertRowsCount != 0)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// HortAutoApprovalCoverageActlPnchyt.
        /// </summary>
        /// <returns>Insert Success Status.</returns>
        public int HortAutoApprovalCoverageActlPnchyt()
        {
            int insertRowsCount = 0;
            Dictionary<string, dynamic> result = SqlHelper.ExecuteNonQuery<SqlConnection>(spHortAutoApprovalCoverageActlPnchyt, null, SqlHelper.ExecutionType.Procedure);
            insertRowsCount += result["RowsAffected"];

            if (insertRowsCount != 0)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// GetHortCropCoverageActualPancht.
        /// </summary>
        /// <param name="seasonId">seasonId.</param>
        /// <param name="panchayatId">panchayatId.</param>
        /// <returns>List Values.</returns>
        public List<HortCropCoverageActualPancht> GetHortCropCoverageActualPancht(string seasonId, string panchayatId)
        {
            List<HortCropCoverageActualPancht> list = new List<HortCropCoverageActualPancht>();

            List<DbParameter> dbparamsCropInfo = new List<DbParameter>();
            dbparamsCropInfo.Add(new SqlParameter { ParameterName = "@query_name", Value = qnGetHortCropCoverageActualPancht, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfo.Add(new SqlParameter { ParameterName = "@District_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfo.Add(new SqlParameter { ParameterName = "@Block_id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfo.Add(new SqlParameter { ParameterName = "@Panchayat_Id", Value = string.IsNullOrEmpty(panchayatId) ? DBNull.Value : (object)panchayatId, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfo.Add(new SqlParameter { ParameterName = "@Season_Id", Value = string.IsNullOrEmpty(seasonId) ? DBNull.Value : (object)seasonId, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfo.Add(new SqlParameter { ParameterName = "@Crop_id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfo.Add(new SqlParameter { ParameterName = "@Last_Refresh_Ts", Value = DBNull.Value, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
            dbparamsCropInfo.Add(new SqlParameter { ParameterName = "@Ist_Date", Value = DBNull.Value, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });

            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(spGetHortCropCoverageActualPancht, dbparamsCropInfo, SqlHelper.ExecutionType.Procedure);

            HortCropCoverageActualPancht cropCoverageActualPancht;
            if (dt != null && dt.Rows.Count > 0)
            {
                cropCoverageActualPancht = SqlHelper.ConvertDataTableToList<HortCropCoverageActualPancht>(dt)[0];
                cropCoverageActualPancht.CropList = new List<HortCropCvrgEntity>();
                cropCoverageActualPancht.CropList = SqlHelper.ConvertDataTableToList<HortCropCvrgEntity>(dt);
                list.Add(cropCoverageActualPancht);
            }

            return list;
        }

        /// <summary>
        /// GetHortCropCoverageActualPanchtDelta.
        /// </summary>
        /// <param name="seasonId">seasonId.</param>
        /// <param name="panchayatId">panchayatId.</param>
        /// <param name="lastrefreshedTime">lastrefreshedTime.</param>
        /// <returns>List Values.</returns>
        public List<HortCropCoverageActualPancht> GetHortCropCoverageActualPanchtDelta(string seasonId, string panchayatId, DateTime lastrefreshedTime)
        {
            DateTime lastrefreshtime = Convert.ToDateTime(lastrefreshedTime);

            List<HortCropCoverageActualPancht> list = new List<HortCropCoverageActualPancht>();

            List<DbParameter> dbparamsCropInfo = new List<DbParameter>();
            dbparamsCropInfo.Add(new SqlParameter { ParameterName = "@query_name", Value = qnGetHortCropCoverageActualPanchtDelta, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfo.Add(new SqlParameter { ParameterName = "@District_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfo.Add(new SqlParameter { ParameterName = "@Block_id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfo.Add(new SqlParameter { ParameterName = "@Panchayat_Id", Value = string.IsNullOrEmpty(panchayatId) ? DBNull.Value : (object)panchayatId, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfo.Add(new SqlParameter { ParameterName = "@Season_Id", Value = string.IsNullOrEmpty(seasonId) ? DBNull.Value : (object)seasonId, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfo.Add(new SqlParameter { ParameterName = "@Crop_id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfo.Add(new SqlParameter { ParameterName = "@Last_Refresh_Ts", Value = lastrefreshtime, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
            dbparamsCropInfo.Add(new SqlParameter { ParameterName = "@Ist_Date", Value = DBNull.Value, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });

            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(spGetHortCropCoverageActualPancht, dbparamsCropInfo, SqlHelper.ExecutionType.Procedure);

            HortCropCoverageActualPancht cropCoverageActualPancht;
            if (dt != null && dt.Rows.Count > 0)
            {
                cropCoverageActualPancht = SqlHelper.ConvertDataTableToList<HortCropCoverageActualPancht>(dt)[0];
                cropCoverageActualPancht.CropList = new List<HortCropCvrgEntity>();
                cropCoverageActualPancht.CropList = SqlHelper.ConvertDataTableToList<HortCropCvrgEntity>(dt);
                list.Add(cropCoverageActualPancht);
            }

            return list;
        }

        /// <summary>
        /// GetHortCropCoverageActualPanchtCurrDate.
        /// </summary>
        /// <param name="seasonId">seasonId.</param>
        /// <param name="panchayatId">panchayatId.</param>
        /// <returns>List Values.</returns>
        public List<HortCropCoverageActualPancht> GetHortCropCoverageActualPanchtCurrDate(string seasonId, string panchayatId)
        {
            List<HortCropCoverageActualPancht> list = new List<HortCropCoverageActualPancht>();

            List<DbParameter> dbparamsCropInfo = new List<DbParameter>();
            dbparamsCropInfo.Add(new SqlParameter { ParameterName = "@query_name", Value = qnGetHortCropCoverageActualPanchtCurrDate, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfo.Add(new SqlParameter { ParameterName = "@District_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfo.Add(new SqlParameter { ParameterName = "@Block_id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfo.Add(new SqlParameter { ParameterName = "@Panchayat_Id", Value = string.IsNullOrEmpty(panchayatId) ? DBNull.Value : (object)panchayatId, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfo.Add(new SqlParameter { ParameterName = "@Season_Id", Value = string.IsNullOrEmpty(seasonId) ? DBNull.Value : (object)seasonId, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfo.Add(new SqlParameter { ParameterName = "@Crop_id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfo.Add(new SqlParameter { ParameterName = "@Last_Refresh_Ts", Value = DBNull.Value, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
            dbparamsCropInfo.Add(new SqlParameter { ParameterName = "@Ist_Date", Value = this.istDate, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });

            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(spGetHortCropCoverageActualPancht, dbparamsCropInfo, SqlHelper.ExecutionType.Procedure);

            HortCropCoverageActualPancht cropCoverageActualPancht;
            if (dt != null && dt.Rows.Count > 0)
            {
                cropCoverageActualPancht = SqlHelper.ConvertDataTableToList<HortCropCoverageActualPancht>(dt)[0];
                cropCoverageActualPancht.CropList = new List<HortCropCvrgEntity>();
                cropCoverageActualPancht.CropList = SqlHelper.ConvertDataTableToList<HortCropCvrgEntity>(dt);
                list.Add(cropCoverageActualPancht);
            }

            return list;
        }

        /// <summary>
        /// GetHortCropCoverageActualBlock.
        /// </summary>
        /// <param name="blockId">blockId.</param>
        /// <returns>List.</returns>
        public List<HortCropCoverageActualBlock> GetHortCropCoverageActualBlock(string blockId)
        {
            List<HortCropCoverageActualBlock> list = new List<HortCropCoverageActualBlock>();

            List<DbParameter> dbparamsCropInfo = new List<DbParameter>();
            dbparamsCropInfo.Add(new SqlParameter { ParameterName = "@query_name", Value = qnGetHortCropCoverageActualBlock, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfo.Add(new SqlParameter { ParameterName = "@District_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfo.Add(new SqlParameter { ParameterName = "@Block_id", Value = string.IsNullOrEmpty(blockId) ? DBNull.Value : (object)blockId, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfo.Add(new SqlParameter { ParameterName = "@Panchayat_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfo.Add(new SqlParameter { ParameterName = "@Season_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfo.Add(new SqlParameter { ParameterName = "@Crop_id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfo.Add(new SqlParameter { ParameterName = "@Last_Refresh_Ts", Value = DBNull.Value, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
            dbparamsCropInfo.Add(new SqlParameter { ParameterName = "@Ist_Date", Value = DBNull.Value, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });

            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(spGetHortCropCoverageActualPancht, dbparamsCropInfo, SqlHelper.ExecutionType.Procedure);

            HortCropCoverageActualBlock cropCoverageActualBlock;
            if (dt != null && dt.Rows.Count > 0)
            {
                cropCoverageActualBlock = SqlHelper.ConvertDataTableToList<HortCropCoverageActualBlock>(dt)[0];
                cropCoverageActualBlock.CropList = new List<HortCropCvrgEntity>();
                cropCoverageActualBlock.CropList = SqlHelper.ConvertDataTableToList<HortCropCvrgEntity>(dt);
                list.Add(cropCoverageActualBlock);
            }

            return list;
        }

        /// <summary>
        /// GetHortCropCoverageActualDistrict.
        /// </summary>
        /// <param name="districtId">districtId.</param>
        /// <returns>List.</returns>
        public List<HortCropCoverageActualDistrict> GetHortCropCoverageActualDistrict(string districtId)
        {
            List<HortCropCoverageActualDistrict> list = new List<HortCropCoverageActualDistrict>();

            List<DbParameter> dbparamsCropInfo = new List<DbParameter>();
            dbparamsCropInfo.Add(new SqlParameter { ParameterName = "@query_name", Value = qnGetHortCropCoverageActualDistrict, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfo.Add(new SqlParameter { ParameterName = "@District_Id", Value = string.IsNullOrEmpty(districtId) ? DBNull.Value : (object)districtId, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfo.Add(new SqlParameter { ParameterName = "@Block_id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfo.Add(new SqlParameter { ParameterName = "@Panchayat_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfo.Add(new SqlParameter { ParameterName = "@Season_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfo.Add(new SqlParameter { ParameterName = "@Crop_id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfo.Add(new SqlParameter { ParameterName = "@Last_Refresh_Ts", Value = DBNull.Value, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
            dbparamsCropInfo.Add(new SqlParameter { ParameterName = "@Ist_Date", Value = DBNull.Value, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });

            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(spGetHortCropCoverageActualPancht, dbparamsCropInfo, SqlHelper.ExecutionType.Procedure);
            HortCropCoverageActualDistrict cropCoverageActualDistrict;
            if (dt != null && dt.Rows.Count > 0)
            {
                cropCoverageActualDistrict = SqlHelper.ConvertDataTableToList<HortCropCoverageActualDistrict>(dt)[0];
                cropCoverageActualDistrict.CropList = new List<HortCropCvrgEntity>();
                cropCoverageActualDistrict.CropList = SqlHelper.ConvertDataTableToList<HortCropCvrgEntity>(dt);
                list.Add(cropCoverageActualDistrict);
            }

            return list;
        }

        /// <summary>
        /// GetHortCropCoverageActualADHDelta.
        /// </summary>
        /// <param name="district_id">district_id.</param>
        /// <param name="season_id">season_id.</param>
        /// <param name="last_refresh_time">last_refresh_time.</param>
        /// <returns>List.</returns>
        public List<HortCropCoverageActual> GetHortCropCoverageActualADHDelta(int district_id, int season_id, DateTime last_refresh_time)
        {
            DateTime lastrefreshtime = Convert.ToDateTime(last_refresh_time);

            List<HortCropCoverageActual> cropCoverageActualResponse;

            DataTable dtDistricts;
            DataTable dtBlocks;
            DataTable dtPanchayats;

            List<DbParameter> dbparamsCropInfoDAODeltaDist = new List<DbParameter>();
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@query_name", Value = qnGetHortCropCoverageActualADHDelta, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@District_Id", Value = string.IsNullOrEmpty(district_id.ToString()) ? DBNull.Value : (object)district_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@Block_id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@Panchayat_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@Season_Id", Value = string.IsNullOrEmpty(season_id.ToString()) ? DBNull.Value : (object)season_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@Crop_id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@Last_Refresh_Ts", Value = lastrefreshtime, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@Ist_Date", Value = this.istDate, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });

            dtDistricts = SqlHelper.ExecuteSelect<SqlConnection>(spGetHortCropCoverageActualPancht, dbparamsCropInfoDAODeltaDist, SqlHelper.ExecutionType.Procedure);

            if (dtDistricts.Rows.Count > 0)
            {
                var cropCoverageAct = dtDistricts.AsEnumerable().Any() ? SqlHelper.ConvertDataTableToList<HortCropCoverageActual>(dtDistricts) : null;

                List<DbParameter> dbparamsCropInfoDAODeltaBlk = new List<DbParameter>();
                dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@query_name", Value = qnGetHortCropCoverageActualADHDeltaBlk, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@District_Id", Value = string.IsNullOrEmpty(district_id.ToString()) ? DBNull.Value : (object)district_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@Block_id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@Panchayat_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@Season_Id", Value = string.IsNullOrEmpty(season_id.ToString()) ? DBNull.Value : (object)season_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@Crop_id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@Last_Refresh_Ts", Value = lastrefreshtime, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@Ist_Date", Value = this.istDate, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });

                List<DbParameter> dbparamsCropInfoDAODeltaPanchyt = new List<DbParameter>();
                dbparamsCropInfoDAODeltaPanchyt.Add(new SqlParameter { ParameterName = "@query_name", Value = qnGetCropCoverageActualDAODeltaPanchyt, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaPanchyt.Add(new SqlParameter { ParameterName = "@District_Id", Value = string.IsNullOrEmpty(district_id.ToString()) ? DBNull.Value : (object)district_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaPanchyt.Add(new SqlParameter { ParameterName = "@Block_id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaPanchyt.Add(new SqlParameter { ParameterName = "@Panchayat_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaPanchyt.Add(new SqlParameter { ParameterName = "@Season_Id", Value = string.IsNullOrEmpty(season_id.ToString()) ? DBNull.Value : (object)season_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaPanchyt.Add(new SqlParameter { ParameterName = "@Crop_id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaPanchyt.Add(new SqlParameter { ParameterName = "@Last_Refresh_Ts", Value = lastrefreshtime, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaPanchyt.Add(new SqlParameter { ParameterName = "@Ist_Date", Value = this.istDate, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });

                dtBlocks = SqlHelper.ExecuteSelect<SqlConnection>(spGetHortCropCoverageActualPancht, dbparamsCropInfoDAODeltaBlk, SqlHelper.ExecutionType.Procedure);

                dtPanchayats = SqlHelper.ExecuteSelect<SqlConnection>(spGetHortCropCoverageActualPancht, dbparamsCropInfoDAODeltaPanchyt, SqlHelper.ExecutionType.Procedure);

                if (cropCoverageAct != null && dtPanchayats.Rows.Count > 0)
                {
                    var actBlocks = SqlHelper.ConvertDataTableToList<DtoHortCoverageActualBlock>(dtBlocks);
                    var actPanchayats = SqlHelper.ConvertDataTableToList<DtoHortCoverageActualPanchayat>(dtPanchayats);
                    cropCoverageActualResponse = cropCoverageAct;

                    if (actBlocks.Any())
                    {
                        foreach (var item in actBlocks)
                        {
                            item.PanchayatList = actPanchayats.Where(x => x.Block_Id == item.Block_Id && x.Crop_id == item.Crop_id).ToList();
                        }

                        foreach (var item in cropCoverageActualResponse)
                        {
                            item.BlockList = actBlocks.Where(x => x.Crop_id == item.Crop_Id).Select(x =>
                         new HortCoverageBlock
                         {
                             Reported_date = x.Reported_date,
                             Block_Id = x.Block_Id,
                             Block_Name = x.Block_Name,
                             Area_Target = x.Area_Target,
                             Cumm_Area_Prev = x.Cumm_Area_Prev,
                             Cumm_Area_Curr = x.Cumm_Area_Curr,
                             Refreshed_date = x.Refreshed_date,
                             Refreshed_userid = x.Refreshed_userid,
                             ADH_Approval_flag = x.ADH_Approval_flag,
                             ADH_Approval_Reason = x.ADH_Approval_Reason,
                             ADH_Approved_date = x.ADH_Approved_date,
                             ADH_Approved_userid = x.ADH_Approved_userid,
                             Refreshed_username = x.Refreshed_username,
                             Adh_approved_username = x.Adh_approved_username,

                             PanchayatList = x.PanchayatList.Any() ? x.PanchayatList.Select(x =>
                               new HortCoveragePanchayat
                               {
                                   Reported_date = x.Reported_date,
                                   Panchayat_Id = x.Panchayat_Id,
                                   Panchayat_Name = x.Panchayat_Name,
                                   Area_Target = x.Area_Target,
                                   Cumm_Area_Prev = x.Cumm_Area_Prev,
                                   Cumm_Area_Curr = x.Cumm_Area_Curr,
                                   AC_Submitted_date = x.AC_Submitted_date,
                                   AC_Submitted_userid = x.AC_Submitted_userid,
                                   BHO_Approval_flag = x.BHO_Approval_flag,
                                   BHO_Approval_Reason = x.BHO_Approval_Reason,
                                   BHO_Approved_date = x.BHO_Approved_date,
                                   BHO_Approved_userid = x.BHO_Approved_userid,
                                   ADH_Approval_flag = x.ADH_Approval_flag,
                                   ADH_Approval_Reason = x.ADH_Approval_Reason,
                                   ADH_Approved_date = x.ADH_Approved_date,
                                   ADH_Approved_userid = x.ADH_Approved_userid,
                                   Ac_submitted_username = x.Ac_submitted_username,
                                   Bho_approved_username = x.Bho_approved_username,
                                   Adh_approved_username = x.Adh_approved_username,
                               })
                         .ToList() : null,
                         }).ToList();
                        }
                    }

                    return cropCoverageActualResponse;
                }
            }

            return null;
        }

        /// <summary>
        /// GetHortCropCoverageActualBHODelta.
        /// </summary>
        /// <param name="district_id">district_id.</param>
        /// <param name="block_id">block_id.</param>
        /// <param name="season_id">season_id.</param>
        /// <param name="last_refresh_time">last_refresh_time.</param>
        /// <returns>List Values.</returns>
        public List<HortCropCoverageActual> GetHortCropCoverageActualBHODelta(int district_id, int block_id, int season_id, DateTime last_refresh_time)
        {
            DateTime lastrefreshtime = Convert.ToDateTime(last_refresh_time);

            List<HortCropCoverageActual> cropCoverageActualResponse;
            DataTable dtDistricts;
            DataTable dtBlocks;
            DataTable dtPanchayats;

            List<DbParameter> dbparamsCropInfoDAODeltaDist = new List<DbParameter>();
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@query_name", Value = qnGetHortCropCoverageActualBHODelta, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@District_Id", Value = string.IsNullOrEmpty(district_id.ToString()) ? DBNull.Value : (object)district_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@Block_id", Value = string.IsNullOrEmpty(block_id.ToString()) ? DBNull.Value : (object)block_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@Panchayat_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@Season_Id", Value = string.IsNullOrEmpty(season_id.ToString()) ? DBNull.Value : (object)season_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@Crop_id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@Last_Refresh_Ts", Value = lastrefreshtime, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@Ist_Date", Value = this.istDate, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });

            dtDistricts = SqlHelper.ExecuteSelect<SqlConnection>(spGetHortCropCoverageActualPancht, dbparamsCropInfoDAODeltaDist, SqlHelper.ExecutionType.Procedure);

            if (dtDistricts.Rows.Count > 0)
            {
                var cropCoverageAct = dtDistricts.AsEnumerable().Any() ? SqlHelper.ConvertDataTableToList<HortCropCoverageActual>(dtDistricts) : null;

                List<DbParameter> dbparamsCropInfoDAODeltaBlk = new List<DbParameter>();
                dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@query_name", Value = qnGetCropCoverageActualBAODeltaBlk, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@District_Id", Value = string.IsNullOrEmpty(district_id.ToString()) ? DBNull.Value : (object)district_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@Block_id", Value = string.IsNullOrEmpty(block_id.ToString()) ? DBNull.Value : (object)block_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@Panchayat_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@Season_Id", Value = string.IsNullOrEmpty(season_id.ToString()) ? DBNull.Value : (object)season_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@Crop_id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@Last_Refresh_Ts", Value = lastrefreshtime, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@Ist_Date", Value = this.istDate, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });

                List<DbParameter> dbparamsCropInfoDAODeltaPanchyt = new List<DbParameter>();
                dbparamsCropInfoDAODeltaPanchyt.Add(new SqlParameter { ParameterName = "@query_name", Value = qnGetCropCoverageActualBAODeltaPanchyt, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaPanchyt.Add(new SqlParameter { ParameterName = "@District_Id", Value = string.IsNullOrEmpty(district_id.ToString()) ? DBNull.Value : (object)district_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaPanchyt.Add(new SqlParameter { ParameterName = "@Block_id", Value = string.IsNullOrEmpty(block_id.ToString()) ? DBNull.Value : (object)block_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaPanchyt.Add(new SqlParameter { ParameterName = "@Panchayat_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaPanchyt.Add(new SqlParameter { ParameterName = "@Season_Id", Value = string.IsNullOrEmpty(season_id.ToString()) ? DBNull.Value : (object)season_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaPanchyt.Add(new SqlParameter { ParameterName = "@Crop_id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaPanchyt.Add(new SqlParameter { ParameterName = "@Last_Refresh_Ts", Value = lastrefreshtime, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaPanchyt.Add(new SqlParameter { ParameterName = "@Ist_Date", Value = this.istDate, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });

                dtBlocks = SqlHelper.ExecuteSelect<SqlConnection>(spGetHortCropCoverageActualPancht, dbparamsCropInfoDAODeltaBlk, SqlHelper.ExecutionType.Procedure);
                dtPanchayats = SqlHelper.ExecuteSelect<SqlConnection>(spGetHortCropCoverageActualPancht, dbparamsCropInfoDAODeltaPanchyt, SqlHelper.ExecutionType.Procedure);

                if (cropCoverageAct != null && dtPanchayats.Rows.Count > 0)
                {
                    var actBlocks = SqlHelper.ConvertDataTableToList<DtoHortCoverageActualBlock>(dtBlocks);
                    var actPanchayats = SqlHelper.ConvertDataTableToList<DtoHortCoverageActualPanchayat>(dtPanchayats);
                    cropCoverageActualResponse = cropCoverageAct;

                    if (actBlocks.Any())
                    {
                        foreach (var item in actBlocks)
                        {
                            item.PanchayatList = actPanchayats.Where(x => x.Block_Id == item.Block_Id && x.Crop_id == item.Crop_id).ToList();
                        }

                        foreach (var item in cropCoverageActualResponse)
                        {
                            item.BlockList = actBlocks.Where(x => x.Crop_id == item.Crop_Id).Select(x =>
                             new HortCoverageBlock
                             {
                                 Reported_date = x.Reported_date,
                                 Block_Id = x.Block_Id,
                                 Block_Name = x.Block_Name,
                                 Area_Target = x.Area_Target,
                                 Cumm_Area_Prev = x.Cumm_Area_Prev,
                                 Cumm_Area_Curr = x.Cumm_Area_Curr,
                                 Refreshed_date = x.Refreshed_date,
                                 Refreshed_userid = x.Refreshed_userid,
                                 ADH_Approval_flag = x.ADH_Approval_flag,
                                 ADH_Approval_Reason = x.ADH_Approval_Reason,
                                 ADH_Approved_date = x.ADH_Approved_date,
                                 ADH_Approved_userid = x.ADH_Approved_userid,
                                 Refreshed_username = x.Refreshed_username,
                                 Adh_approved_username = x.Adh_approved_username,

                                 PanchayatList = x.PanchayatList.Any() ? x.PanchayatList.Select(x =>
                                   new HortCoveragePanchayat
                                   {
                                       Reported_date = x.Reported_date,
                                       Panchayat_Id = x.Panchayat_Id,
                                       Panchayat_Name = x.Panchayat_Name,
                                       Area_Target = x.Area_Target,
                                       Cumm_Area_Prev = x.Cumm_Area_Prev,
                                       Cumm_Area_Curr = x.Cumm_Area_Curr,
                                       AC_Submitted_date = x.AC_Submitted_date,
                                       AC_Submitted_userid = x.AC_Submitted_userid,
                                       BHO_Approval_flag = x.BHO_Approval_flag,
                                       BHO_Approval_Reason = x.BHO_Approval_Reason,
                                       BHO_Approved_date = x.BHO_Approved_date,
                                       BHO_Approved_userid = x.BHO_Approved_userid,
                                       ADH_Approval_flag = x.ADH_Approval_flag,
                                       ADH_Approval_Reason = x.ADH_Approval_Reason,
                                       ADH_Approved_date = x.ADH_Approved_date,
                                       ADH_Approved_userid = x.ADH_Approved_userid,
                                       Ac_submitted_username = x.Ac_submitted_username,
                                       Bho_approved_username = x.Bho_approved_username,
                                       Adh_approved_username = x.Adh_approved_username,
                                   })
                             .ToList() : null,
                             }).ToList();
                        }
                    }

                    return cropCoverageActualResponse;
                }
            }

            return null;
        }

        /// <summary>
        /// GetHortCropCoverageActualBHOOffline.
        /// </summary>
        /// <param name="district_id">district_id.</param>
        /// <param name="block_id">block_id.</param>
        /// <param name="season_id">season_id.</param>
        /// <param name="crop_ids">crop_ids.</param>
        /// <returns>List Values.</returns>
        public List<HortCropCoverageActual> GetHortCropCoverageActualBHOOffline(int district_id, int block_id, int season_id, string crop_ids)
        {
            List<HortCropCoverageActual> cropCoverageActualResponse;
            DataTable dtDistricts;
            DataTable dtBlocks;
            DataTable dtPanchayats;

            List<DbParameter> dbparamsCropInfoDAODeltaDist = new List<DbParameter>();
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@query_name", Value = qnGetHortCropCoverageActualBHOOfflineDist, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@District_Id", Value = string.IsNullOrEmpty(district_id.ToString()) ? DBNull.Value : (object)district_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@Block_id", Value = string.IsNullOrEmpty(block_id.ToString()) ? DBNull.Value : (object)block_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@Panchayat_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@Season_Id", Value = string.IsNullOrEmpty(season_id.ToString()) ? DBNull.Value : (object)season_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@Crop_id", Value = string.IsNullOrEmpty(crop_ids.ToString()) ? DBNull.Value : (object)crop_ids.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@Last_Refresh_Ts", Value = DBNull.Value, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@Ist_Date", Value = this.istDate, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });

            dtDistricts = SqlHelper.ExecuteSelect<SqlConnection>(spGetHortCropCoverageActualPancht, dbparamsCropInfoDAODeltaDist, SqlHelper.ExecutionType.Procedure);

            if (dtDistricts.Rows.Count > 0)
            {
                var cropCoverageAct = dtDistricts.AsEnumerable().Any() ? SqlHelper.ConvertDataTableToList<HortCropCoverageActual>(dtDistricts) : null;

                List<DbParameter> dbparamsCropInfoDAODeltaBlk = new List<DbParameter>();
                dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@query_name", Value = qnGetHortCropCoverageActualBHOOfflineBlk, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@District_Id", Value = string.IsNullOrEmpty(district_id.ToString()) ? DBNull.Value : (object)district_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@Block_id", Value = string.IsNullOrEmpty(block_id.ToString()) ? DBNull.Value : (object)block_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@Panchayat_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@Season_Id", Value = string.IsNullOrEmpty(season_id.ToString()) ? DBNull.Value : (object)season_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@Crop_id", Value = string.IsNullOrEmpty(crop_ids.ToString()) ? DBNull.Value : (object)crop_ids.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@Last_Refresh_Ts", Value = DBNull.Value, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@Ist_Date", Value = this.istDate, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });

                List<DbParameter> dbparamsCropInfoDAODeltaPanchyt = new List<DbParameter>();
                dbparamsCropInfoDAODeltaPanchyt.Add(new SqlParameter { ParameterName = "@query_name", Value = qnGetHortCropCoverageActualBHOOfflinePanch, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaPanchyt.Add(new SqlParameter { ParameterName = "@District_Id", Value = string.IsNullOrEmpty(district_id.ToString()) ? DBNull.Value : (object)district_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaPanchyt.Add(new SqlParameter { ParameterName = "@Block_id", Value = string.IsNullOrEmpty(block_id.ToString()) ? DBNull.Value : (object)block_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaPanchyt.Add(new SqlParameter { ParameterName = "@Panchayat_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaPanchyt.Add(new SqlParameter { ParameterName = "@Season_Id", Value = string.IsNullOrEmpty(season_id.ToString()) ? DBNull.Value : (object)season_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaPanchyt.Add(new SqlParameter { ParameterName = "@Crop_id", Value = string.IsNullOrEmpty(crop_ids.ToString()) ? DBNull.Value : (object)crop_ids.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaPanchyt.Add(new SqlParameter { ParameterName = "@Last_Refresh_Ts", Value = DBNull.Value, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaPanchyt.Add(new SqlParameter { ParameterName = "@Ist_Date", Value = this.istDate, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });

                dtBlocks = SqlHelper.ExecuteSelect<SqlConnection>(spGetHortCropCoverageActualPancht, dbparamsCropInfoDAODeltaBlk, SqlHelper.ExecutionType.Procedure);
                dtPanchayats = SqlHelper.ExecuteSelect<SqlConnection>(spGetHortCropCoverageActualPancht, dbparamsCropInfoDAODeltaPanchyt, SqlHelper.ExecutionType.Procedure);

                if (cropCoverageAct != null && dtPanchayats.Rows.Count > 0)
                {
                    var actBlocks = SqlHelper.ConvertDataTableToList<DtoHortCoverageActualBlock>(dtBlocks);
                    var actPanchayats = SqlHelper.ConvertDataTableToList<DtoHortCoverageActualPanchayat>(dtPanchayats);
                    cropCoverageActualResponse = cropCoverageAct;

                    if (actBlocks.Any())
                    {
                        foreach (var item in actBlocks)
                        {
                            item.PanchayatList = actPanchayats.Where(x => x.Block_Id == item.Block_Id && x.Crop_id == item.Crop_id).ToList();
                        }

                        foreach (var item in cropCoverageActualResponse)
                        {
                            item.BlockList = actBlocks.Where(x => x.Crop_id == item.Crop_Id).Select(x =>
                             new HortCoverageBlock
                             {
                                 Reported_date = x.Reported_date,
                                 Block_Id = x.Block_Id,
                                 Block_Name = x.Block_Name,
                                 Area_Target = x.Area_Target,
                                 Cumm_Area_Prev = x.Cumm_Area_Prev,
                                 Cumm_Area_Curr = x.Cumm_Area_Curr,
                                 Refreshed_date = x.Refreshed_date,
                                 Refreshed_userid = x.Refreshed_userid,
                                 ADH_Approval_flag = x.ADH_Approval_flag,
                                 ADH_Approval_Reason = x.ADH_Approval_Reason,
                                 ADH_Approved_date = x.ADH_Approved_date,
                                 ADH_Approved_userid = x.ADH_Approved_userid,
                                 Refreshed_username = x.Refreshed_username,
                                 Adh_approved_username = x.Adh_approved_username,

                                 PanchayatList = x.PanchayatList.Any() ? x.PanchayatList.Select(x =>
                                   new HortCoveragePanchayat
                                   {
                                       Reported_date = x.Reported_date,
                                       Panchayat_Id = x.Panchayat_Id,
                                       Panchayat_Name = x.Panchayat_Name,
                                       Area_Target = x.Area_Target,
                                       Cumm_Area_Prev = x.Cumm_Area_Prev,
                                       Cumm_Area_Curr = x.Cumm_Area_Curr,
                                       AC_Submitted_date = x.AC_Submitted_date,
                                       AC_Submitted_userid = x.AC_Submitted_userid,
                                       BHO_Approval_flag = x.BHO_Approval_flag,
                                       BHO_Approval_Reason = x.BHO_Approval_Reason,
                                       BHO_Approved_date = x.BHO_Approved_date,
                                       BHO_Approved_userid = x.BHO_Approved_userid,
                                       ADH_Approval_flag = x.ADH_Approval_flag,
                                       ADH_Approval_Reason = x.ADH_Approval_Reason,
                                       ADH_Approved_date = x.ADH_Approved_date,
                                       ADH_Approved_userid = x.ADH_Approved_userid,
                                       Ac_submitted_username = x.Ac_submitted_username,
                                       Bho_approved_username = x.Bho_approved_username,
                                       Adh_approved_username = x.Adh_approved_username,
                                       Ac_submit_flag = x.Ac_submit_flag,
                                       Rec_updated_userid = x.Rec_Updated_Userid,
                                       Rec_updated_date = x.Rec_Updated_Date,
                                       UpdatedBy = x.UpdatedBy,
                                   })
                             .ToList() : null,
                             }).ToList();
                        }
                    }

                    return cropCoverageActualResponse;
                }
            }

            return null;
        }

        /// <summary>
        /// GetHortCropCoverageActualADHOffline.
        /// </summary>
        /// <param name="district_id">district_id.</param>
        /// <param name="season_id">season_id.</param>
        /// <param name="crop_ids">crop_ids.</param>
        /// <returns>List.</returns>
        public List<HortCropCoverageActual> GetHortCropCoverageActualADHOffline(int district_id, int season_id, string crop_ids)
        {
            List<HortCropCoverageActual> cropCoverageActualResponse;

            DataTable dtDistricts;
            DataTable dtBlocks;
            DataTable dtPanchayats;

            List<DbParameter> dbparamsCropInfoDAODeltaDist = new List<DbParameter>();
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@query_name", Value = qnGetHortCropCoverageActualADHOfflineDist, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@District_Id", Value = string.IsNullOrEmpty(district_id.ToString()) ? DBNull.Value : (object)district_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@Block_id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@Panchayat_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@Season_Id", Value = string.IsNullOrEmpty(season_id.ToString()) ? DBNull.Value : (object)season_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@Crop_id", Value = string.IsNullOrEmpty(crop_ids.ToString()) ? DBNull.Value : (object)crop_ids.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@Last_Refresh_Ts", Value = DBNull.Value, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@Ist_Date", Value = this.istDate, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });

            dtDistricts = SqlHelper.ExecuteSelect<SqlConnection>(spGetHortCropCoverageActualPancht, dbparamsCropInfoDAODeltaDist, SqlHelper.ExecutionType.Procedure);

            if (dtDistricts.Rows.Count > 0)
            {
                var cropCoverageAct = dtDistricts.AsEnumerable().Any() ? SqlHelper.ConvertDataTableToList<HortCropCoverageActual>(dtDistricts) : null;

                List<DbParameter> dbparamsCropInfoDAODeltaBlk = new List<DbParameter>();
                dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@query_name", Value = qnGetHortCropCoverageActualADHOfflineBlk, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@District_Id", Value = string.IsNullOrEmpty(district_id.ToString()) ? DBNull.Value : (object)district_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@Block_id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@Panchayat_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@Season_Id", Value = string.IsNullOrEmpty(season_id.ToString()) ? DBNull.Value : (object)season_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@Crop_id", Value = string.IsNullOrEmpty(crop_ids.ToString()) ? DBNull.Value : (object)crop_ids.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@Last_Refresh_Ts", Value = DBNull.Value, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@Ist_Date", Value = this.istDate, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });

                List<DbParameter> dbparamsCropInfoDAODeltaPanchyt = new List<DbParameter>();
                dbparamsCropInfoDAODeltaPanchyt.Add(new SqlParameter { ParameterName = "@query_name", Value = qnGetHortCropCoverageActualADHOfflinePanch, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaPanchyt.Add(new SqlParameter { ParameterName = "@District_Id", Value = string.IsNullOrEmpty(district_id.ToString()) ? DBNull.Value : (object)district_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaPanchyt.Add(new SqlParameter { ParameterName = "@Block_id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaPanchyt.Add(new SqlParameter { ParameterName = "@Panchayat_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaPanchyt.Add(new SqlParameter { ParameterName = "@Season_Id", Value = string.IsNullOrEmpty(season_id.ToString()) ? DBNull.Value : (object)season_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaPanchyt.Add(new SqlParameter { ParameterName = "@Crop_id", Value = string.IsNullOrEmpty(crop_ids.ToString()) ? DBNull.Value : (object)crop_ids.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaPanchyt.Add(new SqlParameter { ParameterName = "@Last_Refresh_Ts", Value = DBNull.Value, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaPanchyt.Add(new SqlParameter { ParameterName = "@Ist_Date", Value = this.istDate, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });

                dtBlocks = SqlHelper.ExecuteSelect<SqlConnection>(spGetHortCropCoverageActualPancht, dbparamsCropInfoDAODeltaBlk, SqlHelper.ExecutionType.Procedure);

                dtPanchayats = SqlHelper.ExecuteSelect<SqlConnection>(spGetHortCropCoverageActualPancht, dbparamsCropInfoDAODeltaPanchyt, SqlHelper.ExecutionType.Procedure);

                if (cropCoverageAct != null && dtPanchayats.Rows.Count > 0)
                {
                    var actBlocks = SqlHelper.ConvertDataTableToList<DtoHortCoverageActualBlock>(dtBlocks);
                    var actPanchayats = SqlHelper.ConvertDataTableToList<DtoHortCoverageActualPanchayat>(dtPanchayats);
                    cropCoverageActualResponse = cropCoverageAct;

                    if (actBlocks.Any())
                    {
                        foreach (var item in actBlocks)
                        {
                            item.PanchayatList = actPanchayats.Where(x => x.Block_Id == item.Block_Id && x.Crop_id == item.Crop_id).ToList();
                        }

                        foreach (var item in cropCoverageActualResponse)
                        {
                            item.BlockList = actBlocks.Where(x => x.Crop_id == item.Crop_Id).Select(x =>
                         new HortCoverageBlock
                         {
                             Reported_date = x.Reported_date,
                             Block_Id = x.Block_Id,
                             Block_Name = x.Block_Name,
                             Area_Target = x.Area_Target,
                             Cumm_Area_Prev = x.Cumm_Area_Prev,
                             Cumm_Area_Curr = x.Cumm_Area_Curr,
                             Refreshed_date = x.Refreshed_date,
                             Refreshed_userid = x.Refreshed_userid,
                             ADH_Approval_flag = x.ADH_Approval_flag,
                             ADH_Approval_Reason = x.ADH_Approval_Reason,
                             ADH_Approved_date = x.ADH_Approved_date,
                             ADH_Approved_userid = x.ADH_Approved_userid,
                             Refreshed_username = x.Refreshed_username,
                             Adh_approved_username = x.Adh_approved_username,

                             PanchayatList = x.PanchayatList.Any() ? x.PanchayatList.Select(x =>
                               new HortCoveragePanchayat
                               {
                                   Reported_date = x.Reported_date,
                                   Panchayat_Id = x.Panchayat_Id,
                                   Panchayat_Name = x.Panchayat_Name,
                                   Area_Target = x.Area_Target,
                                   Cumm_Area_Prev = x.Cumm_Area_Prev,
                                   Cumm_Area_Curr = x.Cumm_Area_Curr,
                                   AC_Submitted_date = x.AC_Submitted_date,
                                   AC_Submitted_userid = x.AC_Submitted_userid,
                                   BHO_Approval_flag = x.BHO_Approval_flag,
                                   BHO_Approval_Reason = x.BHO_Approval_Reason,
                                   BHO_Approved_date = x.BHO_Approved_date,
                                   BHO_Approved_userid = x.BHO_Approved_userid,
                                   ADH_Approval_flag = x.ADH_Approval_flag,
                                   ADH_Approval_Reason = x.ADH_Approval_Reason,
                                   ADH_Approved_date = x.ADH_Approved_date,
                                   ADH_Approved_userid = x.ADH_Approved_userid,
                                   Ac_submitted_username = x.Ac_submitted_username,
                                   Bho_approved_username = x.Bho_approved_username,
                                   Adh_approved_username = x.Adh_approved_username,
                                   Ac_submit_flag = x.Ac_submit_flag,
                                   Rec_updated_date = x.Rec_Updated_Date,
                                   Rec_updated_userid = x.Rec_Updated_Userid,
                                   UpdatedBy = x.UpdatedBy,
                               })
                         .ToList() : null,
                         }).ToList();
                        }
                    }

                    return cropCoverageActualResponse;
                }
            }

            return null;
        }

        /// <summary>
        /// GetHortCropCoverageActualADH.
        /// </summary>
        /// <param name="district_id">district_id.</param>
        /// <param name="season_id">season_id.</param>
        /// <param name="crop_id">crop_id.</param>
        /// <returns>List Values.</returns>
        public HortCropCoverageActual GetHortCropCoverageActualADH(int district_id, int season_id, int crop_id)
        {
            HortCropCoverageActual cropCoverageActualResponse;

            DataTable dtDistricts;
            DataTable dtBlocks;
            DataTable dtPanchayats;

            List<DbParameter> dbparamsCropInfoDAODeltaDist = new List<DbParameter>();
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@query_name", Value = qnGetHortCropCoverageActualADHDist, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@District_Id", Value = string.IsNullOrEmpty(district_id.ToString()) ? DBNull.Value : (object)district_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@Block_id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@Panchayat_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@Season_Id", Value = string.IsNullOrEmpty(season_id.ToString()) ? DBNull.Value : (object)season_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@Crop_id", Value = string.IsNullOrEmpty(crop_id.ToString()) ? DBNull.Value : (object)crop_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@Last_Refresh_Ts", Value = DBNull.Value, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@Ist_Date", Value = this.istDate, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });

            dtDistricts = SqlHelper.ExecuteSelect<SqlConnection>(spGetHortCropCoverageActualPancht, dbparamsCropInfoDAODeltaDist, SqlHelper.ExecutionType.Procedure);

            if (dtDistricts.Rows.Count > 0)
            {
                var cropCoverageAct = dtDistricts.AsEnumerable().Any() ? SqlHelper.ConvertDataTableToList<HortCropCoverageActual>(dtDistricts)[0] : null;

                List<DbParameter> dbparamsCropInfoDAODeltaBlk = new List<DbParameter>();
                dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@query_name", Value = qnGetHortCropCoverageActualADHBlk, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@District_Id", Value = string.IsNullOrEmpty(district_id.ToString()) ? DBNull.Value : (object)district_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@Block_id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@Panchayat_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@Season_Id", Value = string.IsNullOrEmpty(season_id.ToString()) ? DBNull.Value : (object)season_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@Crop_id", Value = string.IsNullOrEmpty(crop_id.ToString()) ? DBNull.Value : (object)crop_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@Last_Refresh_Ts", Value = DBNull.Value, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@Ist_Date", Value = this.istDate, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });

                List<DbParameter> dbparamsCropInfoDAODeltaPanchayat = new List<DbParameter>();
                dbparamsCropInfoDAODeltaPanchayat.Add(new SqlParameter { ParameterName = "@query_name", Value = qnGetHortCropCoverageActualADHpanch, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaPanchayat.Add(new SqlParameter { ParameterName = "@District_Id", Value = string.IsNullOrEmpty(district_id.ToString()) ? DBNull.Value : (object)district_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaPanchayat.Add(new SqlParameter { ParameterName = "@Block_id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaPanchayat.Add(new SqlParameter { ParameterName = "@Panchayat_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaPanchayat.Add(new SqlParameter { ParameterName = "@Season_Id", Value = string.IsNullOrEmpty(season_id.ToString()) ? DBNull.Value : (object)season_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaPanchayat.Add(new SqlParameter { ParameterName = "@Crop_id", Value = string.IsNullOrEmpty(crop_id.ToString()) ? DBNull.Value : (object)crop_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaPanchayat.Add(new SqlParameter { ParameterName = "@Last_Refresh_Ts", Value = DBNull.Value, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaPanchayat.Add(new SqlParameter { ParameterName = "@Ist_Date", Value = this.istDate, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });

                dtBlocks = SqlHelper.ExecuteSelect<SqlConnection>(spGetHortCropCoverageActualPancht, dbparamsCropInfoDAODeltaBlk, SqlHelper.ExecutionType.Procedure);

                dtPanchayats = SqlHelper.ExecuteSelect<SqlConnection>(spGetHortCropCoverageActualPancht, dbparamsCropInfoDAODeltaPanchayat, SqlHelper.ExecutionType.Procedure);

                if (cropCoverageAct != null && dtPanchayats.Rows.Count > 0)
                {
                    var actBlocks = SqlHelper.ConvertDataTableToList<DtoHortCoverageActualBlock>(dtBlocks);
                    var actPanchayats = SqlHelper.ConvertDataTableToList<DtoHortCoverageActualPanchayat>(dtPanchayats);
                    cropCoverageActualResponse = cropCoverageAct;

                    if (actBlocks.Any())
                    {
                        cropCoverageActualResponse.BlockList = new List<HortCoverageBlock>();

                        foreach (var item in actBlocks)
                        {
                            item.PanchayatList = actPanchayats.Where(x => x.Block_Id == item.Block_Id).ToList();
                        }

                        cropCoverageActualResponse.BlockList = actBlocks.Select(x =>
                         new HortCoverageBlock
                         {
                             Reported_date = x.Reported_date,
                             Block_Id = x.Block_Id,
                             Block_Name = x.Block_Name,
                             Area_Target = x.Area_Target,
                             Cumm_Area_Prev = x.Cumm_Area_Prev,
                             Cumm_Area_Curr = x.Cumm_Area_Curr,
                             Refreshed_date = x.Refreshed_date,
                             Refreshed_userid = x.Refreshed_userid,
                             ADH_Approval_flag = x.ADH_Approval_flag,
                             ADH_Approval_Reason = x.ADH_Approval_Reason,
                             ADH_Approved_date = x.ADH_Approved_date,
                             ADH_Approved_userid = x.ADH_Approved_userid,
                             Refreshed_username = x.Refreshed_username,
                             Adh_approved_username = x.Adh_approved_username,

                             PanchayatList = x.PanchayatList.Any() ? x.PanchayatList.Select(x =>
                               new HortCoveragePanchayat
                               {
                                   Reported_date = x.Reported_date,
                                   Panchayat_Id = x.Panchayat_Id,
                                   Panchayat_Name = x.Panchayat_Name,
                                   Area_Target = x.Area_Target,
                                   Cumm_Area_Prev = x.Cumm_Area_Prev,
                                   Cumm_Area_Curr = x.Cumm_Area_Curr,
                                   AC_Submitted_date = x.AC_Submitted_date,
                                   AC_Submitted_userid = x.AC_Submitted_userid,
                                   BHO_Approval_flag = x.BHO_Approval_flag,
                                   BHO_Approval_Reason = x.BHO_Approval_Reason,
                                   BHO_Approved_date = x.BHO_Approved_date,
                                   BHO_Approved_userid = x.BHO_Approved_userid,
                                   ADH_Approval_flag = x.ADH_Approval_flag,
                                   ADH_Approval_Reason = x.ADH_Approval_Reason,
                                   ADH_Approved_date = x.ADH_Approved_date,
                                   ADH_Approved_userid = x.ADH_Approved_userid,
                                   Ac_submitted_username = x.Ac_submitted_username,
                                   Bho_approved_username = x.Bho_approved_username,
                                   Adh_approved_username = x.Adh_approved_username,
                                   Ac_submit_flag = x.Ac_submit_flag,
                                   Bho_add_edit_flag = x.Bho_add_edit_flag,
                                   Adh_add_edit_flag = x.Adh_add_edit_flag,
                                   Final_cvrg_flg = x.Final_cvrg_flg,
                                   Rec_updated_userid = x.Rec_Updated_Userid,
                                   Rec_updated_date = x.Rec_Updated_Date,
                                   UpdatedBy = x.UpdatedBy,
                               })
                         .ToList() : null,
                         }).ToList();
                    }

                    return cropCoverageActualResponse;
                }
            }

            return null;
        }

        /// <summary>
        /// GetHortCropCoverageActualBHO.
        /// </summary>
        /// <param name="district_id">district_id.</param>
        /// <param name="block_id">block_id.</param>
        /// <param name="season_id">season_id.</param>
        /// <param name="crop_id">crop_id.</param>
        /// <returns>List Values.</returns>
        public HortCropCoverageActual GetHortCropCoverageActualBHO(int district_id, int block_id, int season_id, int crop_id)
        {
            HortCropCoverageActual cropCoverageActualResponse;
            DataTable dtDistricts;
            DataTable dtBlocks;
            DataTable dtPanchayats;
            List<DbParameter> dbparamsCropInfoDAODeltaDist = new List<DbParameter>();
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@query_name", Value = qnGetHortCropCoverageActualBHODist, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@District_Id", Value = string.IsNullOrEmpty(district_id.ToString()) ? DBNull.Value : (object)district_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@Block_id", Value = string.IsNullOrEmpty(block_id.ToString()) ? DBNull.Value : (object)block_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@Panchayat_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@Season_Id", Value = string.IsNullOrEmpty(season_id.ToString()) ? DBNull.Value : (object)season_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@Crop_id", Value = string.IsNullOrEmpty(crop_id.ToString()) ? DBNull.Value : (object)crop_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@Last_Refresh_Ts", Value = DBNull.Value, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@Ist_Date", Value = this.istDate, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });

            dtDistricts = SqlHelper.ExecuteSelect<SqlConnection>(spGetHortCropCoverageActualPancht, dbparamsCropInfoDAODeltaDist, SqlHelper.ExecutionType.Procedure);

            if (dtDistricts.Rows.Count > 0)
            {
                var cropCoverageAct = dtDistricts.AsEnumerable().Any() ? SqlHelper.ConvertDataTableToList<HortCropCoverageActual>(dtDistricts)[0] : null;

                List<DbParameter> dbparamsCropInfoDAODeltaBlk = new List<DbParameter>();
                dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@query_name", Value = qnGetHortCropCoverageActualBHOBlk, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@District_Id", Value = string.IsNullOrEmpty(district_id.ToString()) ? DBNull.Value : (object)district_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@Block_id", Value = string.IsNullOrEmpty(block_id.ToString()) ? DBNull.Value : (object)block_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@Panchayat_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@Season_Id", Value = string.IsNullOrEmpty(season_id.ToString()) ? DBNull.Value : (object)season_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@Crop_id", Value = string.IsNullOrEmpty(crop_id.ToString()) ? DBNull.Value : (object)crop_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@Last_Refresh_Ts", Value = DBNull.Value, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@Ist_Date", Value = this.istDate, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });

                List<DbParameter> dbparamsCropInfoDAODeltaPanchyt = new List<DbParameter>();
                dbparamsCropInfoDAODeltaPanchyt.Add(new SqlParameter { ParameterName = "@query_name", Value = qnGetHortCropCoverageActualBHOPanch, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaPanchyt.Add(new SqlParameter { ParameterName = "@District_Id", Value = string.IsNullOrEmpty(district_id.ToString()) ? DBNull.Value : (object)district_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaPanchyt.Add(new SqlParameter { ParameterName = "@Block_id", Value = string.IsNullOrEmpty(block_id.ToString()) ? DBNull.Value : (object)block_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaPanchyt.Add(new SqlParameter { ParameterName = "@Panchayat_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaPanchyt.Add(new SqlParameter { ParameterName = "@Season_Id", Value = string.IsNullOrEmpty(season_id.ToString()) ? DBNull.Value : (object)season_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaPanchyt.Add(new SqlParameter { ParameterName = "@Crop_id", Value = string.IsNullOrEmpty(crop_id.ToString()) ? DBNull.Value : (object)crop_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaPanchyt.Add(new SqlParameter { ParameterName = "@Last_Refresh_Ts", Value = DBNull.Value, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaPanchyt.Add(new SqlParameter { ParameterName = "@Ist_Date", Value = this.istDate, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });

                dtBlocks = SqlHelper.ExecuteSelect<SqlConnection>(spGetHortCropCoverageActualPancht, dbparamsCropInfoDAODeltaBlk, SqlHelper.ExecutionType.Procedure);
                dtPanchayats = SqlHelper.ExecuteSelect<SqlConnection>(spGetHortCropCoverageActualPancht, dbparamsCropInfoDAODeltaPanchyt, SqlHelper.ExecutionType.Procedure);

                if (cropCoverageAct != null && dtPanchayats.Rows.Count > 0)
                {
                    var actBlocks = SqlHelper.ConvertDataTableToList<DtoHortCoverageActualBlock>(dtBlocks);
                    var actPanchayats = SqlHelper.ConvertDataTableToList<DtoHortCoverageActualPanchayat>(dtPanchayats);
                    cropCoverageActualResponse = cropCoverageAct;

                    if (actBlocks.Any())
                    {
                        cropCoverageActualResponse.BlockList = new List<HortCoverageBlock>();

                        foreach (var item in actBlocks)
                        {
                            item.PanchayatList = actPanchayats.Where(x => x.Block_Id == item.Block_Id).ToList();
                        }

                        cropCoverageActualResponse.BlockList = actBlocks.Select(x =>
                         new HortCoverageBlock
                         {
                             Reported_date = x.Reported_date,
                             Block_Id = x.Block_Id,
                             Block_Name = x.Block_Name,
                             Area_Target = x.Area_Target,
                             Cumm_Area_Prev = x.Cumm_Area_Prev,
                             Cumm_Area_Curr = x.Cumm_Area_Curr,
                             Refreshed_date = x.Refreshed_date,
                             Refreshed_userid = x.Refreshed_userid,
                             ADH_Approval_flag = x.ADH_Approval_flag,
                             ADH_Approval_Reason = x.ADH_Approval_Reason,
                             ADH_Approved_date = x.ADH_Approved_date,
                             ADH_Approved_userid = x.ADH_Approved_userid,
                             Refreshed_username = x.Refreshed_username,
                             Adh_approved_username = x.Adh_approved_username,

                             PanchayatList = x.PanchayatList.Any() ? x.PanchayatList.Select(x =>
                               new HortCoveragePanchayat
                               {
                                   Reported_date = x.Reported_date,
                                   Panchayat_Id = x.Panchayat_Id,
                                   Panchayat_Name = x.Panchayat_Name,
                                   Area_Target = x.Area_Target,
                                   Cumm_Area_Prev = x.Cumm_Area_Prev,
                                   Cumm_Area_Curr = x.Cumm_Area_Curr,
                                   AC_Submitted_date = x.AC_Submitted_date,
                                   AC_Submitted_userid = x.AC_Submitted_userid,
                                   BHO_Approval_flag = x.BHO_Approval_flag,
                                   BHO_Approval_Reason = x.BHO_Approval_Reason,
                                   BHO_Approved_date = x.BHO_Approved_date,
                                   BHO_Approved_userid = x.BHO_Approved_userid,
                                   ADH_Approval_flag = x.ADH_Approval_flag,
                                   ADH_Approval_Reason = x.ADH_Approval_Reason,
                                   ADH_Approved_date = x.ADH_Approved_date,
                                   ADH_Approved_userid = x.ADH_Approved_userid,
                                   Ac_submitted_username = x.Ac_submitted_username,
                                   Bho_approved_username = x.Bho_approved_username,
                                   Adh_approved_username = x.Adh_approved_username,
                                   Ac_submit_flag = x.Ac_submit_flag,
                                   Rec_updated_date = x.Rec_Updated_Date,
                                   Rec_updated_userid = x.Rec_Updated_Userid,
                                   UpdatedBy = x.UpdatedBy,
                               })
                         .ToList() : null,
                         }).ToList();
                    }

                    return cropCoverageActualResponse;
                }
            }

            return null;
        }

        /// <summary>
        /// HortAutoCropCvrgActlDataCorrection.
        /// </summary>
        /// <returns>Insert Response Status.</returns>
        public int HortAutoCropCvrgActlDataCorrection()
        {
            int insertRowsCount = 0;
            Dictionary<string, dynamic> result = SqlHelper.ExecuteNonQuery<SqlConnection>(spHortAutoCropCvrgActlDataCorrection, null, SqlHelper.ExecutionType.Procedure);
            insertRowsCount += result["RowsAffected"];

            if (insertRowsCount != 0)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// InsertHortCropCoverageActualApproval.
        /// </summary>
        /// <param name="cropCoverageActual">cropCoverageActual.</param>
        /// <returns>Insert Response Status.</returns>
        public List<CropCoverageActualBlockApprovalResponse> InsertHortCropCoverageActualApproval(HortCropCoverageActual cropCoverageActual)
        {
            List<CropCoverageActualBlockApprovalResponse> response = new List<CropCoverageActualBlockApprovalResponse>();

            int insertRowsCount = 0;
            Dictionary<string, dynamic> result;

            if (cropCoverageActual != null)
            {
                List<DbParameter> dbparamsBlocks = new List<DbParameter>();

                List<DbParameter> dbparamsPanchayat = new List<DbParameter>();
                if (cropCoverageActual.BlockList != null)
                {
                    foreach (var block in cropCoverageActual.BlockList)
                    {
                        CropCoverageActualBlockApprovalResponse respblkobj = new CropCoverageActualBlockApprovalResponse();
                        if (block.ADH_Approved_userid != 0 && block.ADH_Approval_flag != "N")
                        {
                            dbparamsBlocks.Add(new SqlParameter { ParameterName = "@Season_Id", Value = cropCoverageActual.Season_Id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                            dbparamsBlocks.Add(new SqlParameter { ParameterName = "@Block_Id", Value = block.Block_Id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                            dbparamsBlocks.Add(new SqlParameter { ParameterName = "@Crop_Id", Value = cropCoverageActual.Crop_Id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                            dbparamsBlocks.Add(new SqlParameter { ParameterName = "@ADH_approval_flag", Value = string.IsNullOrEmpty(block.ADH_Approval_flag) ? DBNull.Value : (object)block.ADH_Approval_flag, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                            dbparamsBlocks.Add(new SqlParameter { ParameterName = "@ADH_approval_reason", Value = string.IsNullOrEmpty(block.ADH_Approval_Reason) ? DBNull.Value : (object)block.ADH_Approval_Reason, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                            dbparamsBlocks.Add(new SqlParameter { ParameterName = "@ADH_approved_userid", Value = block.ADH_Approved_userid == 0 ? DBNull.Value : (object)block.ADH_Approved_userid, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                            dbparamsBlocks.Add(new SqlParameter { ParameterName = "@ADH_approved_date", Value = block.ADH_Approved_date == DateTime.MinValue ? DBNull.Value : (object)block.ADH_Approved_date, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
                            dbparamsBlocks.Add(new SqlParameter { ParameterName = "@reported_date", Value = block.Reported_date, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
                            dbparamsBlocks.Add(new SqlParameter { ParameterName = "@response_status", SqlDbType = SqlDbType.NVarChar, Size = 1000, Direction = ParameterDirection.Output });

                            result = SqlHelper.ExecuteNonQuery<SqlConnection>(spInsertHortCropCoverageActualApproval, dbparamsBlocks, SqlHelper.ExecutionType.Procedure);
                            insertRowsCount += result["RowsAffected"];
                            string spBlockOut = DBNull.Value.Equals(result["@response_status"]) ? string.Empty : result["@response_status"];

                            if (!string.IsNullOrEmpty(spBlockOut))
                            {
                                foreach (var keyvaluepairblk in spBlockOut.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries))
                                {
                                    string[] splittedblockdata = keyvaluepairblk.Split(new string[] { ":" }, StringSplitOptions.RemoveEmptyEntries);

                                    if (splittedblockdata[0].Trim().Equals("Status"))
                                    {
                                        respblkobj.Status = splittedblockdata[1].ToString();
                                    }
                                    else if (splittedblockdata[0].Trim().Equals("Reason"))
                                    {
                                        respblkobj.Reason = splittedblockdata[1].ToString();
                                    }
                                    else if (splittedblockdata[0].Trim().Equals("BlockId"))
                                    {
                                        respblkobj.BlockId = splittedblockdata[1].ToString();
                                    }
                                    else if (splittedblockdata[0].Trim().Equals("CropId"))
                                    {
                                        respblkobj.CropId = splittedblockdata[1].ToString();
                                    }
                                    else if (splittedblockdata[0].Trim().Equals("SeasonId"))
                                    {
                                        respblkobj.SeasonId = splittedblockdata[1].ToString();
                                    }
                                }
                            }

                            dbparamsBlocks.Clear();
                        }

                        if (block.PanchayatList != null)
                        {
                            foreach (var panchayat in block.PanchayatList)
                            {
                                dbparamsPanchayat.Add(new SqlParameter { ParameterName = "@reported_date", Value = panchayat.Reported_date, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
                                dbparamsPanchayat.Add(new SqlParameter { ParameterName = "@season_id", Value = cropCoverageActual.Season_Id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                                dbparamsPanchayat.Add(new SqlParameter { ParameterName = "@panchayat_id", Value = panchayat.Panchayat_Id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                                dbparamsPanchayat.Add(new SqlParameter { ParameterName = "@crop_id", Value = cropCoverageActual.Crop_Id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                                dbparamsPanchayat.Add(new SqlParameter { ParameterName = "@cumm_area_prev", Value = panchayat.Cumm_Area_Prev, SqlDbType = SqlDbType.Decimal, Direction = ParameterDirection.Input });
                                dbparamsPanchayat.Add(new SqlParameter { ParameterName = "@cumm_area_curr", Value = panchayat.Cumm_Area_Curr, SqlDbType = SqlDbType.Decimal, Direction = ParameterDirection.Input });
                                dbparamsPanchayat.Add(new SqlParameter { ParameterName = "@ADH_approval_flag", Value = string.IsNullOrEmpty(panchayat.ADH_Approval_flag) ? DBNull.Value : (object)panchayat.ADH_Approval_flag, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                                dbparamsPanchayat.Add(new SqlParameter { ParameterName = "@ADH_approval_reason", Value = string.IsNullOrEmpty(panchayat.ADH_Approval_Reason) ? DBNull.Value : (object)panchayat.ADH_Approval_Reason, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                                dbparamsPanchayat.Add(new SqlParameter { ParameterName = "@ADH_approved_userid", Value = panchayat.ADH_Approved_userid == 0 ? DBNull.Value : (object)panchayat.ADH_Approved_userid, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                                dbparamsPanchayat.Add(new SqlParameter { ParameterName = "@ADH_approved_date", Value = panchayat.ADH_Approved_date == DateTime.MinValue ? DBNull.Value : (object)panchayat.ADH_Approved_date, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
                                dbparamsPanchayat.Add(new SqlParameter { ParameterName = "@bho_approval_flag", Value = string.IsNullOrEmpty(panchayat.BHO_Approval_flag) ? DBNull.Value : (object)panchayat.BHO_Approval_flag, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                                dbparamsPanchayat.Add(new SqlParameter { ParameterName = "@bho_approval_reason", Value = string.IsNullOrEmpty(panchayat.BHO_Approval_Reason) ? DBNull.Value : (object)panchayat.BHO_Approval_Reason, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                                dbparamsPanchayat.Add(new SqlParameter { ParameterName = "@bho_approved_userid", Value = panchayat.BHO_Approved_userid == 0 ? DBNull.Value : (object)panchayat.BHO_Approved_userid, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                                dbparamsPanchayat.Add(new SqlParameter { ParameterName = "@bho_approved_date", Value = panchayat.BHO_Approved_date == DateTime.MinValue ? DBNull.Value : (object)panchayat.BHO_Approved_date, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
                                dbparamsPanchayat.Add(new SqlParameter { ParameterName = "@ac_submitted_userid", Value = panchayat.AC_Submitted_userid == 0 ? DBNull.Value : (object)panchayat.AC_Submitted_userid, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                                dbparamsPanchayat.Add(new SqlParameter { ParameterName = "@ac_submitted_date", Value = panchayat.AC_Submitted_date == DateTime.MinValue ? DBNull.Value : (object)panchayat.AC_Submitted_date, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
                                dbparamsPanchayat.Add(new SqlParameter { ParameterName = "@submission_source", Value = string.IsNullOrEmpty(panchayat.Submission_source) ? DBNull.Value : (object)panchayat.Submission_source, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                                dbparamsPanchayat.Add(new SqlParameter { ParameterName = "@response_status", SqlDbType = SqlDbType.NVarChar, Size = 1000, Direction = ParameterDirection.Output });

                                result = SqlHelper.ExecuteNonQuery<SqlConnection>(spInsertHortCropCoverageActualApprovalpanch, dbparamsPanchayat, SqlHelper.ExecutionType.Procedure);

                                insertRowsCount += result["RowsAffected"];

                                string spOut = DBNull.Value.Equals(result["@response_status"]) ? string.Empty : result["@response_status"];

                                if (!string.IsNullOrEmpty(spOut))
                                {
                                    CropCoverageActualPanchayatApprovalResponse respobj = new CropCoverageActualPanchayatApprovalResponse();

                                    foreach (var keyvaluepair in spOut.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries))
                                    {
                                        string[] splitteddata = keyvaluepair.Split(new string[] { ":" }, StringSplitOptions.RemoveEmptyEntries);

                                        if (splitteddata[0].Trim().Equals("Status"))
                                        {
                                            respobj.Status = splitteddata[1].ToString();
                                        }
                                        else if (splitteddata[0].Trim().Equals("Reason"))
                                        {
                                            respobj.Reason = splitteddata[1].ToString();
                                        }
                                        else if (splitteddata[0].Trim().Equals("PanchayatId"))
                                        {
                                            respobj.PanchayatId = splitteddata[1].ToString();
                                        }
                                        else if (splitteddata[0].Trim().Equals("CropId"))
                                        {
                                            respobj.CropId = splitteddata[1].ToString();
                                        }
                                        else if (splitteddata[0].Trim().Equals("SeasonId"))
                                        {
                                            respobj.SeasonId = splitteddata[1].ToString();
                                        }
                                    }

                                    respblkobj.Panchayatresponse.Add(respobj);
                                }

                                dbparamsPanchayat.Clear();
                            }

                            response.Add(respblkobj);
                        }
                    }
                }
            }

            return response;
        }

        /// <summary>
        /// InsertHortCropCoverageActualPancht.
        /// </summary>
        /// <param name="crop">crop.</param>
        /// <returns>Insert Response status and List.</returns>
        public List<CropCoverageActualPanchayatApprovalResponse> InsertHortCropCoverageActualPancht(List<HortCropCoverageActualPancht> crop)
        {
            Dictionary<string, dynamic> result;

            int insertRowsCount = 0;
            List<CropCoverageActualPanchayatApprovalResponse> response = new List<CropCoverageActualPanchayatApprovalResponse>();

            foreach (HortCropCoverageActualPancht cclist in crop)
            {
                foreach (HortCropCvrgEntity cpvalue in cclist.CropList)
                {
                    List<DbParameter> dbparams = new List<DbParameter>();

                    dbparams.Add(new SqlParameter { ParameterName = "@reported_date", Value = cclist.Reported_Date, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@season_id", Value = cclist.Season_Id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@panchayat_id", Value = cclist.Panchayat_Id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@crop_id", Value = cpvalue.Crop_Id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@cumm_area_prev", Value = cpvalue.Cumm_Area_Prev, SqlDbType = SqlDbType.Decimal, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@cumm_area_curr", Value = cpvalue.Cumm_Area_Curr, SqlDbType = SqlDbType.Decimal, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@ADH_approval_flag", Value = string.IsNullOrEmpty(cpvalue.ADH_Approval_flag) ? DBNull.Value : (object)cpvalue.ADH_Approval_flag, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@ADH_approval_reason", Value = string.IsNullOrEmpty(cpvalue.ADH_Approval_Reason) ? DBNull.Value : (object)cpvalue.ADH_Approval_Reason, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@ADH_approved_userid", Value = cpvalue.ADH_Approved_userid == 0 ? DBNull.Value : (object)cpvalue.ADH_Approved_userid, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@ADH_approved_date", Value = cpvalue.ADH_Approved_date == DateTime.MinValue ? DBNull.Value : (object)cpvalue.ADH_Approved_date, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@BHO_approval_flag", Value = string.IsNullOrEmpty(cpvalue.BHO_Approval_flag) ? DBNull.Value : (object)cpvalue.BHO_Approval_flag, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@BHO_approval_reason", Value = string.IsNullOrEmpty(cpvalue.BHO_Approval_Reason) ? DBNull.Value : (object)cpvalue.BHO_Approval_Reason, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@BHO_approved_userid", Value = cpvalue.BHO_Approved_userid == 0 ? DBNull.Value : (object)cpvalue.BHO_Approved_userid, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@BHO_approved_date", Value = cpvalue.BHO_Approved_date == DateTime.MinValue ? DBNull.Value : (object)cpvalue.BHO_Approved_date, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@ac_submitted_userid", Value = cpvalue.AC_Submitted_userid == 0 ? DBNull.Value : (object)cpvalue.AC_Submitted_userid, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@ac_submitted_date", Value = cpvalue.AC_Submitted_date == DateTime.MinValue ? DBNull.Value : (object)cpvalue.AC_Submitted_date, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@submission_source", Value = string.IsNullOrEmpty(cpvalue.Submission_source) ? DBNull.Value : (object)cpvalue.Submission_source, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@ac_submit_flag", Value = string.IsNullOrEmpty(cpvalue.Ac_submit_flag) ? DBNull.Value : (object)cpvalue.Ac_submit_flag, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@BHO_add_edit_flag", Value = string.IsNullOrEmpty(cpvalue.Bho_add_edit_flag) ? DBNull.Value : (object)cpvalue.Bho_add_edit_flag, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@ADH_add_edit_flag", Value = string.IsNullOrEmpty(cpvalue.Adh_add_edit_flag) ? DBNull.Value : (object)cpvalue.Adh_add_edit_flag, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@final_cvrg_flg", Value = string.IsNullOrEmpty(cpvalue.Final_cvrg_flg) ? DBNull.Value : (object)cpvalue.Final_cvrg_flg, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@rec_updated_userid", Value = string.IsNullOrEmpty(cpvalue.Rec_updated_userid) ? DBNull.Value : (object)cpvalue.Rec_updated_userid, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@rec_updated_date", Value = cpvalue.Rec_updated_date == DateTime.MinValue ? DBNull.Value : (object)cpvalue.Rec_updated_date, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@response_status", SqlDbType = SqlDbType.NVarChar, Size = 1000, Direction = ParameterDirection.Output });

                    result = SqlHelper.ExecuteNonQuery<SqlConnection>(spInsertHortCropCoverageActualPancht, dbparams, SqlHelper.ExecutionType.Procedure);

                    insertRowsCount += result["RowsAffected"];

                    string spOut = DBNull.Value.Equals(result["@response_status"]) ? string.Empty : result["@response_status"];
                    if (!string.IsNullOrEmpty(spOut))
                    {
                        CropCoverageActualPanchayatApprovalResponse respobj = new CropCoverageActualPanchayatApprovalResponse();

                        foreach (var keyvaluepair in spOut.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries))
                        {
                            string[] splitteddata = keyvaluepair.Split(new string[] { ":" }, StringSplitOptions.RemoveEmptyEntries);

                            if (splitteddata[0].Trim().Equals("Status"))
                            {
                                respobj.Status = splitteddata[1].ToString();
                            }
                            else if (splitteddata[0].Trim().Equals("Reason"))
                            {
                                respobj.Reason = splitteddata[1].ToString();
                            }
                            else if (splitteddata[0].Trim().Equals("PanchayatId"))
                            {
                                respobj.PanchayatId = splitteddata[1].ToString();
                            }
                            else if (splitteddata[0].Trim().Equals("CropId"))
                            {
                                respobj.CropId = splitteddata[1].ToString();
                            }
                            else if (splitteddata[0].Trim().Equals("SeasonId"))
                            {
                                respobj.SeasonId = splitteddata[1].ToString();
                            }
                        }

                        response.Add(respobj);
                    }

                    if (result["RowsAffected"] != 0)
                    {
                        List<DbParameter> spparams = new List<DbParameter>();
                        spparams.Add(new SqlParameter { ParameterName = "@reported_date", Value = cclist.Reported_Date, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
                        spparams.Add(new SqlParameter { ParameterName = "@season_id", Value = cclist.Season_Id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                        spparams.Add(new SqlParameter { ParameterName = "@panchayat_id", Value = cclist.Panchayat_Id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                        spparams.Add(new SqlParameter { ParameterName = "@crop_id", Value = cpvalue.Crop_Id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });

                        string sp_query = qnhortaggrcrpactl;
                        result = SqlHelper.ExecuteNonQuery<SqlConnection>(sp_query, spparams, SqlHelper.ExecutionType.Procedure);
                    }
                }
            }

            return response;
        }

        /// <summary>
        /// InsertHortCropCoverageActualPanchayatApproval.
        /// </summary>
        /// <param name="cropCoverageActual">cropCoverageActual.</param>
        /// <returns>Insert Response and Status.</returns>
        public List<CropCoverageActualBlockApprovalResponse> InsertHortCropCoverageActualPanchayatApproval(HortCropCoverageActual cropCoverageActual)
        {
            List<CropCoverageActualBlockApprovalResponse> response = new List<CropCoverageActualBlockApprovalResponse>();

            int insertRowsCount = 0;
            Dictionary<string, dynamic> result;

            if (cropCoverageActual != null)
            {
                List<DbParameter> dbparamsBlocks = new List<DbParameter>();

                List<DbParameter> dbparamsPanchayat = new List<DbParameter>();
                if (cropCoverageActual.BlockList != null)
                {
                    foreach (var block in cropCoverageActual.BlockList)
                    {
                        CropCoverageActualBlockApprovalResponse respblkobj = new CropCoverageActualBlockApprovalResponse();
                        if (block.ADH_Approved_userid != 0 && block.ADH_Approval_flag != "N")
                        {
                            dbparamsBlocks.Add(new SqlParameter { ParameterName = "@Season_Id", Value = cropCoverageActual.Season_Id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                            dbparamsBlocks.Add(new SqlParameter { ParameterName = "@Block_Id", Value = block.Block_Id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                            dbparamsBlocks.Add(new SqlParameter { ParameterName = "@Crop_Id", Value = cropCoverageActual.Crop_Id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                            dbparamsBlocks.Add(new SqlParameter { ParameterName = "@ADH_approval_flag", Value = string.IsNullOrEmpty(block.ADH_Approval_flag) ? DBNull.Value : (object)block.ADH_Approval_flag, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                            dbparamsBlocks.Add(new SqlParameter { ParameterName = "@ADH_approval_reason", Value = string.IsNullOrEmpty(block.ADH_Approval_Reason) ? DBNull.Value : (object)block.ADH_Approval_Reason, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                            dbparamsBlocks.Add(new SqlParameter { ParameterName = "@ADH_approved_userid", Value = block.ADH_Approved_userid == 0 ? DBNull.Value : (object)block.ADH_Approved_userid, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                            dbparamsBlocks.Add(new SqlParameter { ParameterName = "@ADH_approved_date", Value = block.ADH_Approved_date == DateTime.MinValue ? DBNull.Value : (object)block.ADH_Approved_date, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
                            dbparamsBlocks.Add(new SqlParameter { ParameterName = "@reported_date", Value = block.Reported_date, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
                            dbparamsBlocks.Add(new SqlParameter { ParameterName = "@response_status", SqlDbType = SqlDbType.NVarChar, Size = 1000, Direction = ParameterDirection.Output });

                            result = SqlHelper.ExecuteNonQuery<SqlConnection>(spInsertHortCropCoverageActualApproval, dbparamsBlocks, SqlHelper.ExecutionType.Procedure);

                            insertRowsCount += result["RowsAffected"];
                            string spBlockOut = DBNull.Value.Equals(result["@response_status"]) ? string.Empty : result["@response_status"];

                            if (!string.IsNullOrEmpty(spBlockOut))
                            {
                                foreach (var keyvaluepairblk in spBlockOut.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries))
                                {
                                    string[] splittedblockdata = keyvaluepairblk.Split(new string[] { ":" }, StringSplitOptions.RemoveEmptyEntries);

                                    if (splittedblockdata[0].Trim().Equals("Status"))
                                    {
                                        respblkobj.Status = splittedblockdata[1].ToString();
                                    }
                                    else if (splittedblockdata[0].Trim().Equals("Reason"))
                                    {
                                        respblkobj.Reason = splittedblockdata[1].ToString();
                                    }
                                    else if (splittedblockdata[0].Trim().Equals("BlockId"))
                                    {
                                        respblkobj.BlockId = splittedblockdata[1].ToString();
                                    }
                                    else if (splittedblockdata[0].Trim().Equals("CropId"))
                                    {
                                        respblkobj.CropId = splittedblockdata[1].ToString();
                                    }
                                    else if (splittedblockdata[0].Trim().Equals("SeasonId"))
                                    {
                                        respblkobj.SeasonId = splittedblockdata[1].ToString();
                                    }
                                }
                            }

                            dbparamsBlocks.Clear();
                        }

                        if (block.PanchayatList != null)
                        {
                            foreach (var panchayat in block.PanchayatList)
                            {
                                dbparamsPanchayat.Add(new SqlParameter { ParameterName = "@reported_date", Value = panchayat.Reported_date, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
                                dbparamsPanchayat.Add(new SqlParameter { ParameterName = "@season_id", Value = cropCoverageActual.Season_Id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                                dbparamsPanchayat.Add(new SqlParameter { ParameterName = "@panchayat_id", Value = panchayat.Panchayat_Id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                                dbparamsPanchayat.Add(new SqlParameter { ParameterName = "@crop_id", Value = cropCoverageActual.Crop_Id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                                dbparamsPanchayat.Add(new SqlParameter { ParameterName = "@cumm_area_prev", Value = panchayat.Cumm_Area_Prev, SqlDbType = SqlDbType.Decimal, Direction = ParameterDirection.Input });
                                dbparamsPanchayat.Add(new SqlParameter { ParameterName = "@cumm_area_curr", Value = panchayat.Cumm_Area_Curr, SqlDbType = SqlDbType.Decimal, Direction = ParameterDirection.Input });
                                dbparamsPanchayat.Add(new SqlParameter { ParameterName = "@ADH_approval_flag", Value = string.IsNullOrEmpty(panchayat.ADH_Approval_flag) ? DBNull.Value : (object)panchayat.ADH_Approval_flag, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                                dbparamsPanchayat.Add(new SqlParameter { ParameterName = "@ADH_approval_reason", Value = string.IsNullOrEmpty(panchayat.ADH_Approval_Reason) ? DBNull.Value : (object)panchayat.ADH_Approval_Reason, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                                dbparamsPanchayat.Add(new SqlParameter { ParameterName = "@ADH_approved_userid", Value = panchayat.ADH_Approved_userid == 0 ? DBNull.Value : (object)panchayat.ADH_Approved_userid, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                                dbparamsPanchayat.Add(new SqlParameter { ParameterName = "@ADH_approved_date", Value = panchayat.ADH_Approved_date == DateTime.MinValue ? DBNull.Value : (object)panchayat.ADH_Approved_date, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
                                dbparamsPanchayat.Add(new SqlParameter { ParameterName = "@BHO_approval_flag", Value = string.IsNullOrEmpty(panchayat.BHO_Approval_flag) ? DBNull.Value : (object)panchayat.BHO_Approval_flag, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                                dbparamsPanchayat.Add(new SqlParameter { ParameterName = "@BHO_approval_reason", Value = string.IsNullOrEmpty(panchayat.BHO_Approval_Reason) ? DBNull.Value : (object)panchayat.BHO_Approval_Reason, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                                dbparamsPanchayat.Add(new SqlParameter { ParameterName = "@BHO_approved_userid", Value = panchayat.BHO_Approved_userid == 0 ? DBNull.Value : (object)panchayat.BHO_Approved_userid, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                                dbparamsPanchayat.Add(new SqlParameter { ParameterName = "@BHO_approved_date", Value = panchayat.BHO_Approved_date == DateTime.MinValue ? DBNull.Value : (object)panchayat.BHO_Approved_date, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
                                dbparamsPanchayat.Add(new SqlParameter { ParameterName = "@ac_submitted_userid", Value = panchayat.AC_Submitted_userid == 0 ? DBNull.Value : (object)panchayat.AC_Submitted_userid, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                                dbparamsPanchayat.Add(new SqlParameter { ParameterName = "@ac_submitted_date", Value = panchayat.AC_Submitted_date == DateTime.MinValue ? DBNull.Value : (object)panchayat.AC_Submitted_date, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
                                dbparamsPanchayat.Add(new SqlParameter { ParameterName = "@submission_source", Value = string.IsNullOrEmpty(panchayat.Submission_source) ? DBNull.Value : (object)panchayat.Submission_source, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                                dbparamsPanchayat.Add(new SqlParameter { ParameterName = "@response_status", SqlDbType = SqlDbType.NVarChar, Size = 1000, Direction = ParameterDirection.Output });

                                result = SqlHelper.ExecuteNonQuery<SqlConnection>(spInsertHortCropCoverageActualApprovalpanch, dbparamsPanchayat, SqlHelper.ExecutionType.Procedure);

                                insertRowsCount += result["RowsAffected"];

                                string spOut = DBNull.Value.Equals(result["@response_status"]) ? string.Empty : result["@response_status"];

                                if (!string.IsNullOrEmpty(spOut))
                                {
                                    CropCoverageActualPanchayatApprovalResponse respobj = new CropCoverageActualPanchayatApprovalResponse();

                                    foreach (var keyvaluepair in spOut.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries))
                                    {
                                        string[] splitteddata = keyvaluepair.Split(new string[] { ":" }, StringSplitOptions.RemoveEmptyEntries);

                                        if (splitteddata[0].Trim().Equals("Status"))
                                        {
                                            respobj.Status = splitteddata[1].ToString();
                                        }
                                        else if (splitteddata[0].Trim().Equals("Reason"))
                                        {
                                            respobj.Reason = splitteddata[1].ToString();
                                        }
                                        else if (splitteddata[0].Trim().Equals("PanchayatId"))
                                        {
                                            respobj.PanchayatId = splitteddata[1].ToString();
                                        }
                                        else if (splitteddata[0].Trim().Equals("CropId"))
                                        {
                                            respobj.CropId = splitteddata[1].ToString();
                                        }
                                        else if (splitteddata[0].Trim().Equals("SeasonId"))
                                        {
                                            respobj.SeasonId = splitteddata[1].ToString();
                                        }
                                    }

                                    respblkobj.Panchayatresponse.Add(respobj);
                                }

                                dbparamsPanchayat.Clear();
                            }

                            response.Add(respblkobj);
                        }
                    }
                }
            }

            return response;
        }

        /// <summary>
        /// GetAllStructure.
        /// </summary>
        /// <param name="district_Id">district_Id.</param>
        /// <returns>List Values.</returns>
        public List<GetStructure> GetAllStructure(int district_Id)
        {
            List<GetStructure> list = new List<GetStructure>();
            List<DbParameter> dbparams = new List<DbParameter>();
            dbparams.Add(new SqlParameter { ParameterName = "@query_name", Value = qnGetAllStructure, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@user_id", Value = DBNull.Value, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@panchayat_id", Value = DBNull.Value, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@district_id", Value = district_Id, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@facility_id", Value = DBNull.Value, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@struct_id", Value = DBNull.Value, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@struct_type", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });

            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(spGetAllStructure, dbparams, SqlHelper.ExecutionType.Procedure);
            if (dt != null && dt.Rows.Count > 0)
            {
                list = SqlHelper.ConvertDataTableToList<GetStructure>(dt);
            }

            return list;
        }

        /// <summary>
        /// InsertPHMSStructure.
        /// </summary>
        /// <param name="pHMSStructure">pHMSStructure.</param>
        /// <returns>Insert Response Status.</returns>
        public int InsertPHMSStructure(PhmsStructure pHMSStructure)
        {
            int insertRowsCount = 0;
            Dictionary<string, dynamic> result;
            List<DbParameter> dbparams = new List<DbParameter>();
            dbparams.Add(new SqlParameter { ParameterName = "@query_name", Value = qnInsertPHMSStructure, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@user_id", Value = pHMSStructure.UserId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@panchayat_id", Value = DBNull.Value, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@district_id", Value = pHMSStructure.DistrictId, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@facility_id", Value = DBNull.Value, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@struct_id", Value = DBNull.Value, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@struct_type", Value = pHMSStructure.StructType, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });

            dbparams.Add(new SqlParameter { ParameterName = "@Is_Name_Mandatory_flg", Value = pHMSStructure.Is_Name_Mandatory_flg, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@Is_Addr_Mandatory_flg", Value = pHMSStructure.Is_Addr_Mandatory_flg, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@Is_Capacity_Mandatory_flg", Value = pHMSStructure.Is_Capacity_Mandatory_flg, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@unit_of_measure", Value = pHMSStructure.Unit_of_measure, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });

            result = SqlHelper.ExecuteNonQuery<SqlConnection>(spGetAllStructure, dbparams, SqlHelper.ExecutionType.Procedure);
            insertRowsCount += result["RowsAffected"];

            if (insertRowsCount != 0)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// InsertHortNewCrop.
        /// </summary>
        /// <param name="hortNewCrop">hortNewCrop.</param>
        /// <returns>Response Status.</returns>
        public int InsertHortNewCrop(HortNewCrop hortNewCrop)
        {
            int insertRowsCount = 0;
            Dictionary<string, dynamic> result;
            Dictionary<string, dynamic> resultCropDim;
            List<DbParameter> dbparams = new List<DbParameter>();
            dbparams.Add(new SqlParameter { ParameterName = "@query_name", Value = qnInsertHortNewCrop, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@District_Id", Value = hortNewCrop.DistrictId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@Season_Id", Value = hortNewCrop.SeasonId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@Crop_Id", Value = DBNull.Value, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@Crop_name", Value = hortNewCrop.CropName, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@Crop_Type", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@Crop_Category", Value = hortNewCrop.CropCategory, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@unit_of_measure", Value = hortNewCrop.UnitOfMeasure, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            result = SqlHelper.ExecuteNonQuery<SqlConnection>(spInsertHortNewCrop, dbparams, SqlHelper.ExecutionType.Procedure);
            insertRowsCount += result.Count;

            if (insertRowsCount > 0)
            {
                List<DbParameter> dbparamExternal = new List<DbParameter>();
                dbparamExternal.Add(new SqlParameter { ParameterName = "@query_name", Value = qnPostHorticultureCropDimIns, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamExternal.Add(new SqlParameter { ParameterName = "@District_Id", Value = DBNull.Value, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                dbparamExternal.Add(new SqlParameter { ParameterName = "@Season_Id", Value = hortNewCrop.SeasonId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                dbparamExternal.Add(new SqlParameter { ParameterName = "@Crop_Id", Value = DBNull.Value, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                dbparamExternal.Add(new SqlParameter { ParameterName = "@Crop_name", Value = hortNewCrop.CropName, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamExternal.Add(new SqlParameter { ParameterName = "@Crop_Type", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamExternal.Add(new SqlParameter { ParameterName = "@Crop_Category", Value = hortNewCrop.CropCategory, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamExternal.Add(new SqlParameter { ParameterName = "@unit_of_measure", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                resultCropDim = SqlHelper.ExecuteNonQuery<SqlConnection>(spInsertHortNewCrop, dbparamExternal, SqlHelper.ExecutionType.Procedure);

                insertRowsCount = 0;
                insertRowsCount += resultCropDim.Count;
            }

            if (insertRowsCount != 0)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// GetDistinctHorticultureCrop.
        /// </summary>
        /// <param name="district_id">district_id.</param>
        /// <returns>List Values.</returns>
        public List<DistinctHorticultureCrop> GetDistinctHorticultureCrop(int district_id)
        {
            List<DistinctHorticultureCrop> list = new List<DistinctHorticultureCrop>();
            List<DbParameter> dbparams = new List<DbParameter>();
            dbparams.Add(new SqlParameter { ParameterName = "@query_name", Value = qnGetDistinctHorticultureCrop, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@District_Id", Value = district_id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@Season_Id", Value = district_id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@Crop_Id", Value = DBNull.Value, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@Crop_name", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@Crop_Type", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@Crop_Category", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@unit_of_measure", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(spInsertHortNewCrop, dbparams, SqlHelper.ExecutionType.Procedure);
            if (dt != null && dt.Rows.Count > 0)
            {
                list = SqlHelper.ConvertDataTableToList<DistinctHorticultureCrop>(dt);
            }

            return list;
        }

        /// <summary>
        /// GetHortProduceSeason.
        /// </summary>
        /// <returns>List Values.</returns>
        public List<HortProduceSeasonResponse> GetHortProduceSeason()
        {
            List<HortProduceSeasonResponse> list = new List<HortProduceSeasonResponse>();
            List<DbParameter> dbparams = new List<DbParameter>();
            dbparams.Add(new SqlParameter { ParameterName = "@query_name", Value = qnGetHortProduceSeason, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@season_id", Value = DBNull.Value, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@District_Id", Value = DBNull.Value, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@Crop_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@Block_Id", Value = DBNull.Value, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@Panchayat_Id", Value = DBNull.Value, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });

            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(spGetHortProduceSeason, dbparams, SqlHelper.ExecutionType.Procedure);
            if (dt != null && dt.Rows.Count > 0)
            {
                list = SqlHelper.ConvertDataTableToList<HortProduceSeasonResponse>(dt);
            }

            return list;
        }

        /// <summary>
        /// GetHortProducePanchayat.
        /// </summary>
        /// <param name="district_id">district_id.</param>
        /// <param name="block_id">block_id.</param>
        /// <returns>List Values.</returns>
        public List<HortProducePanchayatResponse> GetHortProducePanchayat(int district_id, int block_id)
        {
            List<HortProducePanchayatResponse> list = new List<HortProducePanchayatResponse>();
            List<DbParameter> dbparams = new List<DbParameter>();
            dbparams.Add(new SqlParameter { ParameterName = "@query_name", Value = qnGetHortProducePanchayat, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@season_id", Value = DBNull.Value, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@District_Id", Value = district_id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@Crop_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@Block_Id", Value = block_id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@Panchayat_Id", Value = DBNull.Value, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });

            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(spGetHortProducePanchayat, dbparams, SqlHelper.ExecutionType.Procedure);
            if (dt != null && dt.Rows.Count > 0)
            {
                list = SqlHelper.ConvertDataTableToList<HortProducePanchayatResponse>(dt);
            }

            return list;
        }

        /// <summary>
        /// GetHortProduceLatest.
        /// </summary>
        /// <param name="crop_id">crop_id.</param>
        /// <param name="district_id">district_id.</param>
        /// <param name="block_id">block_id.</param>
        /// <param name="panchayat_Id">panchayat_Id.</param>
        /// <returns>List Values.</returns>
        public List<HortProduceTran> GetHortProduceLatest(int crop_id, int district_id, int block_id, int panchayat_Id)
        {
            List<HortProduceTran> list = new List<HortProduceTran>();
            List<DbParameter> dbparams = new List<DbParameter>();
            dbparams.Add(new SqlParameter { ParameterName = "@query_name", Value = qnGetHortProduceLatest, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@season_id", Value = DBNull.Value, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@District_Id", Value = district_id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@Crop_Id", Value = crop_id, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@Block_Id", Value = block_id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@Panchayat_Id", Value = panchayat_Id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });

            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(spGetHortProduceSeason, dbparams, SqlHelper.ExecutionType.Procedure);
            if (dt != null && dt.Rows.Count > 0)
            {
                list = SqlHelper.ConvertDataTableToList<HortProduceTran>(dt);
            }

            return list;
        }

        /// <summary>
        /// InsertHortProduceTranApproval.
        /// </summary>
        /// <param name="hortProduceTranApprovals">hortProduceTranApprovals.</param>
        /// <returns>Insert Response Status.</returns>
        public List<CropCoverageTargetPanchytApprovalResponse> InsertHortProduceTranApproval(List<HortProduceTranApproval> hortProduceTranApprovals)
        {
            int insertRowsCount = 0;

            List<CropCoverageTargetPanchytApprovalResponse> response = new List<CropCoverageTargetPanchytApprovalResponse>();
            foreach (HortProduceTranApproval hortProduceTranApproval in hortProduceTranApprovals)
            {
                List<DbParameter> dbparams = new List<DbParameter>();
                dbparams.Add(new SqlParameter { ParameterName = "@reported_date", Value = hortProduceTranApproval.Reported_date, SqlDbType = SqlDbType.Date, Direction = ParameterDirection.Input });
                dbparams.Add(new SqlParameter { ParameterName = "@season_id", Value = hortProduceTranApproval.Season_id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                dbparams.Add(new SqlParameter { ParameterName = "@District_id", Value = hortProduceTranApproval.District_id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                dbparams.Add(new SqlParameter { ParameterName = "@Block_Id", Value = hortProduceTranApproval.Block_id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                dbparams.Add(new SqlParameter { ParameterName = "@panchayat_id", Value = hortProduceTranApproval.Panchayat_id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                dbparams.Add(new SqlParameter { ParameterName = "@crop_id", Value = hortProduceTranApproval.Crop_id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                dbparams.Add(new SqlParameter { ParameterName = "@Produce_prev", Value = hortProduceTranApproval.Produce_prev, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                dbparams.Add(new SqlParameter { ParameterName = "@Produce_Curr", Value = hortProduceTranApproval.Produce_Curr, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                dbparams.Add(new SqlParameter { ParameterName = "@ADH_approval_flag", Value = hortProduceTranApproval.ADH_approval_flag, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparams.Add(new SqlParameter { ParameterName = "@ADH_approval_reason", Value = hortProduceTranApproval.ADH_approval_reason, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparams.Add(new SqlParameter { ParameterName = "@ADH_approved_userid", Value = hortProduceTranApproval.ADH_approved_userid, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                dbparams.Add(new SqlParameter { ParameterName = "@ADH_approved_date", Value = hortProduceTranApproval.ADH_approved_date, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
                dbparams.Add(new SqlParameter { ParameterName = "@bho_approval_flag", Value = hortProduceTranApproval.Bho_approval_flag, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparams.Add(new SqlParameter { ParameterName = "@bho_approval_reason", Value = hortProduceTranApproval.Bho_approval_reason, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparams.Add(new SqlParameter { ParameterName = "@bho_approved_userid", Value = hortProduceTranApproval.Bho_approved_userid, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                dbparams.Add(new SqlParameter { ParameterName = "@bho_approved_date", Value = hortProduceTranApproval.Bho_approved_date, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
                dbparams.Add(new SqlParameter { ParameterName = "@ac_submitted_userid", Value = hortProduceTranApproval.Ac_submitted_userid, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                dbparams.Add(new SqlParameter { ParameterName = "@ac_submitted_date", Value = hortProduceTranApproval.Ac_submitted_date, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
                dbparams.Add(new SqlParameter { ParameterName = "@submission_source", Value = hortProduceTranApproval.Submission_source, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparams.Add(new SqlParameter { ParameterName = "@response_status", SqlDbType = SqlDbType.NVarChar, Size = 200, Direction = ParameterDirection.Output });

                Dictionary<string, dynamic> result = SqlHelper.ExecuteNonQuery<SqlConnection>(spInsertHortProduceTranApproval, dbparams, SqlHelper.ExecutionType.Procedure);
                insertRowsCount += result["RowsAffected"];

                string spOut = DBNull.Value.Equals(result["@response_status"]) ? string.Empty : result["@response_status"];

                if (!string.IsNullOrEmpty(spOut))
                {
                    CropCoverageTargetPanchytApprovalResponse respobj = new CropCoverageTargetPanchytApprovalResponse();

                    foreach (var keyvaluepair in spOut.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        string[] splitteddata = keyvaluepair.Split(new string[] { ":" }, StringSplitOptions.RemoveEmptyEntries);

                        if (splitteddata[0].Trim().Equals("Status"))
                        {
                            respobj.Status = splitteddata[1].ToString();
                        }
                        else if (splitteddata[0].Trim().Equals("Reason"))
                        {
                            respobj.Reason = splitteddata[1].ToString();
                        }
                        else if (splitteddata[0].Trim().Equals("PanchayatId"))
                        {
                            respobj.PanchayatId = splitteddata[1].ToString();
                        }
                        else if (splitteddata[0].Trim().Equals("CropId"))
                        {
                            respobj.CropId = splitteddata[1].ToString();
                        }
                        else if (splitteddata[0].Trim().Equals("SeasonId"))
                        {
                            respobj.SeasonId = splitteddata[1].ToString();
                        }
                    }

                    response.Add(respobj);
                }
            }

            return response;
        }

        /// <summary>
        /// InsertHortCoverageActualApproval.
        /// </summary>
        /// <param name="cropCoverageActual">cropCoverageActual.</param>
        /// <returns>Insert Response Status.</returns>
        public List<CropCoverageActualBlockApprovalResponse> InsertHortCoverageActualApproval(HortProduceActlApproval cropCoverageActual)
        {
            List<CropCoverageActualBlockApprovalResponse> response = new List<CropCoverageActualBlockApprovalResponse>();

            int insertRowsCount = 0;
            Dictionary<string, dynamic> result;

            if (cropCoverageActual != null)
            {
                List<DbParameter> dbparamsBlocks = new List<DbParameter>();

                List<DbParameter> dbparamsPanchayat = new List<DbParameter>();
                if (cropCoverageActual.BlockList != null)
                {
                    foreach (var block in cropCoverageActual.BlockList)
                    {
                        CropCoverageActualBlockApprovalResponse respblkobj = new CropCoverageActualBlockApprovalResponse();
                        if (block.AdH_Approved_userid != 0 && block.AdH_Approval_flag != "N")
                        {
                            dbparamsBlocks.Add(new SqlParameter { ParameterName = "@reported_date", Value = block.Reported_date, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
                            dbparamsBlocks.Add(new SqlParameter { ParameterName = "@Season_Id", Value = block.Season_id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                            dbparamsBlocks.Add(new SqlParameter { ParameterName = "@Block_Id", Value = block.Block_id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                            dbparamsBlocks.Add(new SqlParameter { ParameterName = "@Crop_Id", Value = block.Crop_id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                            dbparamsBlocks.Add(new SqlParameter { ParameterName = "@ADH_approval_flag", Value = string.IsNullOrEmpty(block.AdH_Approval_flag) ? DBNull.Value : (object)block.AdH_Approval_flag, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                            dbparamsBlocks.Add(new SqlParameter { ParameterName = "@ADH_approval_reason", Value = string.IsNullOrEmpty(block.AdH_Approval_Reason) ? DBNull.Value : (object)block.AdH_Approval_Reason, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                            dbparamsBlocks.Add(new SqlParameter { ParameterName = "@ADH_approved_userid", Value = block.AdH_Approved_userid == 0 ? DBNull.Value : (object)block.AdH_Approved_userid, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                            dbparamsBlocks.Add(new SqlParameter { ParameterName = "@ADH_approved_date", Value = block.AdH_Approved_date == DateTime.MinValue ? DBNull.Value : (object)block.AdH_Approved_date, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
                            dbparamsBlocks.Add(new SqlParameter { ParameterName = "@response_status", SqlDbType = SqlDbType.NVarChar, Size = 1000, Direction = ParameterDirection.Output });

                            result = SqlHelper.ExecuteNonQuery<SqlConnection>(spInsertHortCoverageActualApproval, dbparamsBlocks, SqlHelper.ExecutionType.Procedure);

                            insertRowsCount += result["RowsAffected"];
                            string spBlockOut = DBNull.Value.Equals(result["@response_status"]) ? string.Empty : result["@response_status"];

                            if (!string.IsNullOrEmpty(spBlockOut))
                            {
                                foreach (var keyvaluepairblk in spBlockOut.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries))
                                {
                                    string[] splittedblockdata = keyvaluepairblk.Split(new string[] { ":" }, StringSplitOptions.RemoveEmptyEntries);

                                    if (splittedblockdata[0].Trim().Equals("Status"))
                                    {
                                        respblkobj.Status = splittedblockdata[1].ToString();
                                    }
                                    else if (splittedblockdata[0].Trim().Equals("Reason"))
                                    {
                                        respblkobj.Reason = splittedblockdata[1].ToString();
                                    }
                                    else if (splittedblockdata[0].Trim().Equals("BlockId"))
                                    {
                                        respblkobj.BlockId = splittedblockdata[1].ToString();
                                    }
                                    else if (splittedblockdata[0].Trim().Equals("CropId"))
                                    {
                                        respblkobj.CropId = splittedblockdata[1].ToString();
                                    }
                                    else if (splittedblockdata[0].Trim().Equals("SeasonId"))
                                    {
                                        respblkobj.SeasonId = splittedblockdata[1].ToString();
                                    }
                                }
                            }

                            dbparamsBlocks.Clear();
                        }

                        if (block.PanchayatList != null)
                        {
                            foreach (var cclist in block.PanchayatList)
                            {
                                dbparamsPanchayat.Add(new SqlParameter { ParameterName = "@reported_date", Value = cclist.Reported_date, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
                                dbparamsPanchayat.Add(new SqlParameter { ParameterName = "@season_id", Value = cclist.Season_id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                                dbparamsPanchayat.Add(new SqlParameter { ParameterName = "@panchayat_id", Value = cclist.Panchayat_Id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                                dbparamsPanchayat.Add(new SqlParameter { ParameterName = "@crop_id", Value = cclist.Crop_id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                                dbparamsPanchayat.Add(new SqlParameter { ParameterName = "@production", Value = cclist.Production, SqlDbType = SqlDbType.Decimal, Direction = ParameterDirection.Input });
                                dbparamsPanchayat.Add(new SqlParameter { ParameterName = "@produce_prev", Value = cclist.Produce_prev, SqlDbType = SqlDbType.Decimal, Direction = ParameterDirection.Input });
                                dbparamsPanchayat.Add(new SqlParameter { ParameterName = "@produce_curr", Value = cclist.Produce_curr, SqlDbType = SqlDbType.Decimal, Direction = ParameterDirection.Input });
                                dbparamsPanchayat.Add(new SqlParameter { ParameterName = "@ADH_approval_flag", Value = string.IsNullOrEmpty(cclist.AdH_Approval_flag) ? DBNull.Value : (object)cclist.AdH_Approval_flag, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                                dbparamsPanchayat.Add(new SqlParameter { ParameterName = "@ADH_approval_reason", Value = string.IsNullOrEmpty(cclist.AdH_Approval_Reason) ? DBNull.Value : (object)cclist.AdH_Approval_Reason, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                                dbparamsPanchayat.Add(new SqlParameter { ParameterName = "@ADH_approved_userid", Value = cclist.AdH_Approved_userid == 0 ? DBNull.Value : (object)cclist.AdH_Approved_userid, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                                dbparamsPanchayat.Add(new SqlParameter { ParameterName = "@ADH_approved_date", Value = cclist.AdH_Approved_date == DateTime.MinValue ? DBNull.Value : (object)cclist.AdH_Approved_date, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
                                dbparamsPanchayat.Add(new SqlParameter { ParameterName = "@BHO_approval_flag", Value = string.IsNullOrEmpty(cclist.BhO_Approval_flag) ? DBNull.Value : (object)cclist.BhO_Approval_flag, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                                dbparamsPanchayat.Add(new SqlParameter { ParameterName = "@BHO_approval_reason", Value = string.IsNullOrEmpty(cclist.BhO_Approval_Reason) ? DBNull.Value : (object)cclist.BhO_Approval_Reason, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                                dbparamsPanchayat.Add(new SqlParameter { ParameterName = "@BHO_approved_userid", Value = cclist.BhO_Approved_userid == 0 ? DBNull.Value : (object)cclist.BhO_Approved_userid, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                                dbparamsPanchayat.Add(new SqlParameter { ParameterName = "@BHO_approved_date", Value = cclist.BhO_Approved_date == DateTime.MinValue ? DBNull.Value : (object)cclist.BhO_Approved_date, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
                                dbparamsPanchayat.Add(new SqlParameter { ParameterName = "@ac_submitted_userid", Value = cclist.Ac_Submitted_userid == 0 ? DBNull.Value : (object)cclist.Ac_Submitted_userid, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                                dbparamsPanchayat.Add(new SqlParameter { ParameterName = "@ac_submitted_date", Value = cclist.Ac_Submitted_date == DateTime.MinValue ? DBNull.Value : (object)cclist.Ac_Submitted_date, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
                                dbparamsPanchayat.Add(new SqlParameter { ParameterName = "@submission_source", Value = string.IsNullOrEmpty(cclist.Submission_source) ? DBNull.Value : (object)cclist.Submission_source, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                                dbparamsPanchayat.Add(new SqlParameter { ParameterName = "@ac_submit_flag", Value = string.IsNullOrEmpty(cclist.Ac_submit_flag) ? DBNull.Value : (object)cclist.Ac_submit_flag, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                                dbparamsPanchayat.Add(new SqlParameter { ParameterName = "@bho_add_edit_flag", Value = string.IsNullOrEmpty(cclist.Bho_add_edit_flag) ? DBNull.Value : (object)cclist.Bho_add_edit_flag, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                                dbparamsPanchayat.Add(new SqlParameter { ParameterName = "@adh_add_edit_flag", Value = string.IsNullOrEmpty(cclist.Adh_add_edit_flag) ? DBNull.Value : (object)cclist.Adh_add_edit_flag, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                                dbparamsPanchayat.Add(new SqlParameter { ParameterName = "@final_cvrg_flg", Value = string.IsNullOrEmpty(cclist.Final_cvrg_flg) ? DBNull.Value : (object)cclist.Final_cvrg_flg, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                                dbparamsPanchayat.Add(new SqlParameter { ParameterName = "@rec_updated_userid", Value = cclist.Rec_updated_userid, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                                dbparamsPanchayat.Add(new SqlParameter { ParameterName = "@rec_updated_date", Value = cclist.Rec_updated_date == DateTime.MinValue ? DBNull.Value : (object)cclist.Rec_updated_date, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
                                dbparamsPanchayat.Add(new SqlParameter { ParameterName = "@response_status", SqlDbType = SqlDbType.NVarChar, Size = 1000, Direction = ParameterDirection.Output });

                                result = SqlHelper.ExecuteNonQuery<SqlConnection>(spInsertHortCoverageActualApprovalpanch, dbparamsPanchayat, SqlHelper.ExecutionType.Procedure);

                                insertRowsCount += result["RowsAffected"];

                                string spOut = DBNull.Value.Equals(result["@response_status"]) ? string.Empty : result["@response_status"];

                                if (!string.IsNullOrEmpty(spOut))
                                {
                                    CropCoverageActualPanchayatApprovalResponse respobj = new CropCoverageActualPanchayatApprovalResponse();

                                    foreach (var keyvaluepair in spOut.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries))
                                    {
                                        string[] splitteddata = keyvaluepair.Split(new string[] { ":" }, StringSplitOptions.RemoveEmptyEntries);

                                        if (splitteddata[0].Trim().Equals("Status"))
                                        {
                                            respobj.Status = splitteddata[1].ToString();
                                        }
                                        else if (splitteddata[0].Trim().Equals("Reason"))
                                        {
                                            respobj.Reason = splitteddata[1].ToString();
                                        }
                                        else if (splitteddata[0].Trim().Equals("PanchayatId"))
                                        {
                                            respobj.PanchayatId = splitteddata[1].ToString();
                                        }
                                        else if (splitteddata[0].Trim().Equals("CropId"))
                                        {
                                            respobj.CropId = splitteddata[1].ToString();
                                        }
                                        else if (splitteddata[0].Trim().Equals("SeasonId"))
                                        {
                                            respobj.SeasonId = splitteddata[1].ToString();
                                        }
                                    }

                                    respblkobj.Panchayatresponse.Add(respobj);
                                }

                                dbparamsPanchayat.Clear();
                            }

                            response.Add(respblkobj);
                        }
                    }
                }
            }

            return response;
        }

        /// <summary>
        /// InsertHortProduceActualPnchyt.
        /// </summary>
        /// <param name="hortProduce">hortProduce.</param>
        /// <returns>Insert Response and List.</returns>
        public List<CropCoverageActualPanchayatApprovalResponse> InsertHortProduceActualPnchyt(List<HortProduceActualPanchayat> hortProduce)
        {
            Dictionary<string, dynamic> result;

            int insertRowsCount = 0;
            List<CropCoverageActualPanchayatApprovalResponse> response = new List<CropCoverageActualPanchayatApprovalResponse>();

            foreach (HortProduceActualPanchayat cclists in hortProduce)
            {
                foreach (HortProduceActualPanchtCrop cclist in cclists.CropList)
                {
                    List<DbParameter> dbparams = new List<DbParameter>();
                    dbparams.Add(new SqlParameter { ParameterName = "@reported_date", Value = cclist.Reported_date == DateTime.MinValue ? DBNull.Value : (object)cclist.Reported_date, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@season_id", Value = cclists.Season_id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@panchayat_id", Value = cclists.Panchayat_id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@crop_id", Value = cclist.Crop_id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@produce_prev", Value = cclist.Produce_prev, SqlDbType = SqlDbType.Decimal, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@produce_curr", Value = cclist.Produce_curr, SqlDbType = SqlDbType.Decimal, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@production", Value = cclist.Production, SqlDbType = SqlDbType.Decimal, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@ADH_approval_flag", Value = string.IsNullOrEmpty(cclist.Adh_approval_flag) ? DBNull.Value : (object)cclist.Adh_approval_flag, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@ADH_approval_reason", Value = string.IsNullOrEmpty(cclist.Adh_approval_reason) ? DBNull.Value : (object)cclist.Adh_approval_reason, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@ADH_approved_userid", Value = cclist.Adh_approved_userid == 0 ? DBNull.Value : (object)cclist.Adh_approved_userid, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@ADH_approved_date", Value = cclist.Adh_approved_date == DateTime.MinValue ? DBNull.Value : (object)cclist.Adh_approved_date, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@BHO_approval_flag", Value = string.IsNullOrEmpty(cclist.Bho_approval_flag) ? DBNull.Value : (object)cclist.Bho_approval_flag, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@BHO_approval_reason", Value = string.IsNullOrEmpty(cclist.Bho_approval_reason) ? DBNull.Value : (object)cclist.Bho_approval_reason, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@BHO_approved_userid", Value = cclist.Bho_approved_userid == 0 ? DBNull.Value : (object)cclist.Bho_approved_userid, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@BHO_approved_date", Value = cclist.Bho_approved_date == DateTime.MinValue ? DBNull.Value : (object)cclist.Bho_approved_date, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@ac_submitted_userid", Value = cclist.Ac_submitted_userid == 0 ? DBNull.Value : (object)cclist.Ac_submitted_userid, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@ac_submitted_date", Value = cclist.Ac_submitted_date == DateTime.MinValue ? DBNull.Value : (object)cclist.Ac_submitted_date, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@submission_source", Value = string.IsNullOrEmpty(cclist.Submission_source) ? DBNull.Value : (object)cclist.Submission_source, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@ac_submit_flag", Value = string.IsNullOrEmpty(cclist.Ac_submit_flag) ? DBNull.Value : (object)cclist.Ac_submit_flag, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@BHO_add_edit_flag", Value = string.IsNullOrEmpty(cclist.Bho_add_edit_flag) ? DBNull.Value : (object)cclist.Bho_add_edit_flag, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@ADH_add_edit_flag", Value = string.IsNullOrEmpty(cclist.Adh_add_edit_flag) ? DBNull.Value : (object)cclist.Adh_add_edit_flag, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@final_cvrg_flg", Value = string.IsNullOrEmpty(cclist.Final_cvrg_flg) ? DBNull.Value : (object)cclist.Final_cvrg_flg, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@rec_updated_userid", Value = cclist.Rec_Updated_Userid == 0 ? DBNull.Value : (object)cclist.Rec_Updated_Userid, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@rec_updated_date", Value = cclist.Rec_Updated_Date == DateTime.MinValue ? DBNull.Value : (object)cclist.Rec_Updated_Date, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@response_status", SqlDbType = SqlDbType.NVarChar, Size = 1000, Direction = ParameterDirection.Output });

                    result = SqlHelper.ExecuteNonQuery<SqlConnection>(spInsertHortProduceActualPnchyt, dbparams, SqlHelper.ExecutionType.Procedure);

                    insertRowsCount += result["RowsAffected"];

                    string spOut = DBNull.Value.Equals(result["@response_status"]) ? string.Empty : result["@response_status"];
                    if (!string.IsNullOrEmpty(spOut))
                    {
                        CropCoverageActualPanchayatApprovalResponse respobj = new CropCoverageActualPanchayatApprovalResponse();
                        foreach (var keyvaluepair in spOut.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries))
                        {
                            string[] splitteddata = keyvaluepair.Split(new string[] { ":" }, StringSplitOptions.RemoveEmptyEntries);

                            if (splitteddata[0].Trim().Equals("Status"))
                            {
                                respobj.Status = splitteddata[1].ToString();
                            }
                            else if (splitteddata[0].Trim().Equals("Reason"))
                            {
                                respobj.Reason = splitteddata[1].ToString();
                            }
                            else if (splitteddata[0].Trim().Equals("PanchayatId"))
                            {
                                respobj.PanchayatId = splitteddata[1].ToString();
                            }
                            else if (splitteddata[0].Trim().Equals("CropId"))
                            {
                                respobj.CropId = splitteddata[1].ToString();
                            }
                            else if (splitteddata[0].Trim().Equals("SeasonId"))
                            {
                                respobj.SeasonId = splitteddata[1].ToString();
                            }
                        }

                        response.Add(respobj);
                    }

                    if (result["RowsAffected"] != 0)
                    {
                        List<DbParameter> spparams = new List<DbParameter>();
                        spparams.Add(new SqlParameter { ParameterName = "@reported_date", Value = cclist.Reported_date, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
                        spparams.Add(new SqlParameter { ParameterName = "@season_id", Value = cclists.Season_id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                        spparams.Add(new SqlParameter { ParameterName = "@panchayat_id", Value = cclists.Panchayat_id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                        spparams.Add(new SqlParameter { ParameterName = "@crop_id", Value = cclist.Crop_id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });

                        string sp_query = sphortprdceaggractl;
                        result = SqlHelper.ExecuteNonQuery<SqlConnection>(sp_query, spparams, SqlHelper.ExecutionType.Procedure);
                    }
                }
            }

            return response;
        }

        /// <summary>
        /// HortProduceAutoApprovalActlBlock.
        /// </summary>
        /// <returns>Insert Response.</returns>
        public int HortProduceAutoApprovalActlBlock()
        {
            int insertRowsCount = 0;
            Dictionary<string, dynamic> result = SqlHelper.ExecuteNonQuery<SqlConnection>(spHortProduceAutoApprovalActlBlock, null, SqlHelper.ExecutionType.Procedure);
            insertRowsCount += result["RowsAffected"];

            if (insertRowsCount != 0)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// HortProduceAutoApprovalActlPanchayat.
        /// </summary>
        /// <returns>Insert Response.</returns>
        public int HortProduceAutoApprovalActlPanchayat()
        {
            int insertRowsCount = 0;

            Dictionary<string, dynamic> result = SqlHelper.ExecuteNonQuery<SqlConnection>(spHortProduceAutoApprovalActlPanchayat, null, SqlHelper.ExecutionType.Procedure);
            insertRowsCount += result["RowsAffected"];

            if (insertRowsCount != 0)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// HortProduceActlDataCorrection.
        /// </summary>
        /// <returns>List.</returns>
        public int HortProduceActlDataCorrection()
        {
            int insertRowsCount = 0;
            Dictionary<string, dynamic> result = SqlHelper.ExecuteNonQuery<SqlConnection>(spHortProduceActlDataCorrection, null, SqlHelper.ExecutionType.Procedure);
            insertRowsCount += result["RowsAffected"];

            if (insertRowsCount != 0)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// GetHortProduceActualPancht.
        /// </summary>
        /// <param name="season_id">season_id.</param>
        /// <param name="panchayat_Id">panchayat_Id.</param>
        /// <returns>List Details.</returns>
        public List<HortProduceActualPanchayat> GetHortProduceActualPancht(int season_id, int panchayat_Id)
        {
            List<HortProduceActualPanchayat> list = new List<HortProduceActualPanchayat>();

            List<DbParameter> dbparams = new List<DbParameter>();
            dbparams.Add(new SqlParameter { ParameterName = "@query_name", Value = qnGetHortProduceActualPancht, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@District_Id", Value = DBNull.Value, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@Block_id", Value = DBNull.Value, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@Panchayat_Id", Value = panchayat_Id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@Season_id", Value = season_id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@Crop_id", Value = DBNull.Value, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@Last_Refresh_Ts", Value = DBNull.Value, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@Ist_Date", Value = DBNull.Value, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });

            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(spGetHortProduceActualPanch, dbparams, SqlHelper.ExecutionType.Procedure);
            HortProduceActualPanchayat hortProduceActualPancht;
            if (dt != null && dt.Rows.Count > 0)
            {
                hortProduceActualPancht = SqlHelper.ConvertDataTableToList<HortProduceActualPanchayat>(dt)[0];
                hortProduceActualPancht.CropList = new List<HortProduceActualPanchtCrop>();
                hortProduceActualPancht.CropList = SqlHelper.ConvertDataTableToList<HortProduceActualPanchtCrop>(dt);
                list.Add(hortProduceActualPancht);
            }

            return list;
        }

        /// <summary>
        /// GetHortProduceActualPanchtCurrDate.
        /// </summary>
        /// <param name="season_id">season_id.</param>
        /// <param name="panchayat_Id">panchayat_Id.</param>
        /// <returns>List Values.</returns>
        public List<HortProduceActualPanchayat> GetHortProduceActualPanchtCurrDate(int season_id, int panchayat_Id)
        {
            List<HortProduceActualPanchayat> list = new List<HortProduceActualPanchayat>();
            List<DbParameter> dbparams = new List<DbParameter>();
            dbparams.Add(new SqlParameter { ParameterName = "@query_name", Value = qnGetHortProduceActualPanchtCurrDate, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@District_Id", Value = DBNull.Value, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@Block_id", Value = DBNull.Value, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@Panchayat_Id", Value = panchayat_Id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@Season_id", Value = season_id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@Crop_id", Value = DBNull.Value, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@Last_Refresh_Ts", Value = DBNull.Value, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@Ist_Date", Value = this.istDate, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });

            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(spGetHortProduceActualPanch, dbparams, SqlHelper.ExecutionType.Procedure);
            HortProduceActualPanchayat hortProduceActualPancht;
            if (dt != null && dt.Rows.Count > 0)
            {
                hortProduceActualPancht = SqlHelper.ConvertDataTableToList<HortProduceActualPanchayat>(dt)[0];
                hortProduceActualPancht.CropList = new List<HortProduceActualPanchtCrop>();
                hortProduceActualPancht.CropList = SqlHelper.ConvertDataTableToList<HortProduceActualPanchtCrop>(dt);
                list.Add(hortProduceActualPancht);
            }

            return list;
        }

        /// <summary>
        /// PostAgriProductivity.
        /// </summary>
        /// <param name="hortProduce">hortProduce.</param>
        /// <returns>DB Insert and Status response.</returns>
        public int PostAgriProductivity(List<PostSubmitProductivityModel> hortProduce)
        {
            int insertRowsCount = 0;
            string spOut = string.Empty;
            if (hortProduce.Any())
            {
                foreach (var glbData in hortProduce)
                {
                    foreach (var collecTdata in glbData.Croplist)
                    {
                        spOut = string.Empty;
                        Dictionary<string, dynamic> result;
                        List<DbParameter> dbparams = new List<DbParameter>();
                        dbparams.Add(new SqlParameter { ParameterName = "@crop_id", Value = collecTdata.Crop_id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                        dbparams.Add(new SqlParameter { ParameterName = "@Season_Id", Value = collecTdata.Season_id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });

                        dbparams.Add(new SqlParameter { ParameterName = "@District_Id", Value = glbData.District_id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                        dbparams.Add(new SqlParameter { ParameterName = "@productivity", Value = collecTdata.Productivity, SqlDbType = SqlDbType.Decimal, Direction = ParameterDirection.Input });
                        dbparams.Add(new SqlParameter { ParameterName = "@Is_Deleted", Value = collecTdata.Is_Deleted, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                        dbparams.Add(new SqlParameter { ParameterName = "@userid", Value = collecTdata.Rec_created_userid, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                        dbparams.Add(new SqlParameter { ParameterName = "@response_status", SqlDbType = SqlDbType.NVarChar, Size = 200, Direction = ParameterDirection.Output });

                        result = SqlHelper.ExecuteNonQuery<SqlConnection>(spPostAgriProductivity, dbparams, SqlHelper.ExecutionType.Procedure);
                        insertRowsCount += result["RowsAffected"];

                        spOut = DBNull.Value.Equals(result["@response_status"]) ? string.Empty : result["@response_status"];
                    }
                }

                if (!string.IsNullOrEmpty(spOut))
                {
                    if (spOut == "S")
                    {
                        return 1;
                    }
                    else
                    {
                        return 0;
                    }
                }
            }

            if (insertRowsCount != 0)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// GetHortiReportColdStorage.
        /// </summary>
        /// <param name="getHortiReportPHMS">getHortiReportPHMS.</param>
        /// <returns>List Details.</returns>
        public List<string> GetHortiReportColdStorage(GetHortiReportPhmsModel getHortiReportPHMS)
        {
            List<string> response = new List<string>();
            List<DbParameter> parameters = new List<DbParameter>();
            parameters.Add(new SqlParameter { ParameterName = "@User_Id", Value = getHortiReportPHMS.User_Id, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@query_name", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@Year", Value = getHortiReportPHMS.Year, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@Month", Value = getHortiReportPHMS.Month, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@Crop_activity", Value = getHortiReportPHMS.Crop_activity, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@struct_type", Value = getHortiReportPHMS.Struct_type, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@date", Value = getHortiReportPHMS.Date, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@Approval_Status", Value = getHortiReportPHMS.Approval_Status, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@District_ID", Value = getHortiReportPHMS.District_ID, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@block_ID", Value = getHortiReportPHMS.Block_ID, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@Panchayat_ID", Value = getHortiReportPHMS.Panchayat_ID, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@Crop_id", Value = getHortiReportPHMS.Crop_id, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@Stor_Name_Address", Value = getHortiReportPHMS.Stor_Name_Address, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@strg_id", Value = getHortiReportPHMS.Strg_Id, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });

            DataSet dt = SqlHelper.ExecuteDataSet<SqlConnection>("usp_horticulture_report_data", parameters, SqlHelper.ExecutionType.Procedure);
            if (dt != null && dt.Tables.Count > 0)
            {
                if (dt.Tables[0].Rows.Count > 0)
                {
                    string crpdmgmdl = this.DataTableToJsonObj(dt.Tables[0]);
                    response.Add(crpdmgmdl);
                }

                return response;
            }

            return null;
        }

        /// <summary>
        /// GetHortiReportPHMS.
        /// </summary>
        /// <param name="getHortiReportPHMS">getHortiReportPHMS.</param>
        /// <returns>List Details.</returns>
        public List<string> GetHortiReportPHMS(GetHortiReportPhmsModel getHortiReportPHMS)
        {
            List<string> response = new List<string>();
            List<DbParameter> parameters = new List<DbParameter>();
            parameters.Add(new SqlParameter { ParameterName = "@User_Id", Value = getHortiReportPHMS.User_Id, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@query_name", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@Year", Value = getHortiReportPHMS.Year, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@Month", Value = getHortiReportPHMS.Month, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@Crop_activity", Value = getHortiReportPHMS.Crop_activity, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@struct_type", Value = getHortiReportPHMS.Struct_type, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@Approval_Status", Value = getHortiReportPHMS.Approval_Status, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@District_ID", Value = getHortiReportPHMS.District_ID, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@block_ID", Value = getHortiReportPHMS.Block_ID, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@Panchayat_ID", Value = getHortiReportPHMS.Panchayat_ID, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@Crop_id", Value = getHortiReportPHMS.Crop_id, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@Stor_Name_Address", Value = getHortiReportPHMS.Stor_Name_Address, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@strg_id", Value = getHortiReportPHMS.Strg_Id, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });

            DataSet dt = SqlHelper.ExecuteDataSet<SqlConnection>("usp_horticulture_report_data", parameters, SqlHelper.ExecutionType.Procedure);
            if (dt != null && dt.Tables.Count > 0)
            {
                if (dt.Tables[0].Rows.Count > 0)
                {
                    string crpdmgmdl = this.DataTableToJsonObj(dt.Tables[0]);
                    response.Add(crpdmgmdl);
                }

                if (dt.Tables[1] != null && dt.Tables[1].Rows.Count > 0)
                {
                    string crpdmgmdl = this.DataTableToJsonObj(dt.Tables[1]);
                    response.Add(crpdmgmdl);
                }

                return response;
            }

            return null;
        }

        /// <summary>
        /// DataTable To JsonObj.
        /// </summary>
        /// <param name="dt">dt.</param>
        /// <returns>Return Values .</returns>
        private string DataTableToJsonObj(DataTable dt)
        {
            DataSet ds = new DataSet();
            ds.Merge(dt);
            StringBuilder jsonString = new StringBuilder();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                jsonString.Append("[");
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    jsonString.Append("{");
                    for (int j = 0; j < ds.Tables[0].Columns.Count; j++)
                    {
                        if (j < ds.Tables[0].Columns.Count - 1)
                        {
                            jsonString.Append("\"" + ds.Tables[0].Columns[j].ColumnName.ToString() + "\":" + "\"" + ds.Tables[0].Rows[i][j].ToString() + "\",");
                        }
                        else if (j == ds.Tables[0].Columns.Count - 1)
                        {
                            jsonString.Append("\"" + ds.Tables[0].Columns[j].ColumnName.ToString() + "\":" + "\"" + ds.Tables[0].Rows[i][j].ToString() + "\"");
                        }
                    }

                    if (i == ds.Tables[0].Rows.Count - 1)
                    {
                        jsonString.Append("}");
                    }
                    else
                    {
                        jsonString.Append("},");
                    }
                }

                jsonString.Append("]");
                return jsonString.ToString();
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// GetSubmitProductivity.
        /// </summary>
        /// <param name="district_id">district_id.</param>
        /// <param name="season_id">season_id.</param>
        /// <returns>List Values.</returns>
        public List<GetSubmitProductivityModel> GetSubmitProductivity(int district_id, int season_id)
        {
            List<GetSubmitProductivityModel> list = new List<GetSubmitProductivityModel>();

            List<DbParameter> dbparamsCropInfo = new List<DbParameter>();
            dbparamsCropInfo.Add(new SqlParameter { ParameterName = "@query_name", Value = qnGetSubmitProductivity, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfo.Add(new SqlParameter { ParameterName = "@Season_Id", Value = season_id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            dbparamsCropInfo.Add(new SqlParameter { ParameterName = "@District_Id", Value = district_id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            dbparamsCropInfo.Add(new SqlParameter { ParameterName = "@Block_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfo.Add(new SqlParameter { ParameterName = "@Panchayat_Id", Value = DBNull.Value, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            dbparamsCropInfo.Add(new SqlParameter { ParameterName = "@Crop_name", Value = DBNull.Value, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            dbparamsCropInfo.Add(new SqlParameter { ParameterName = "@Crop_Category", Value = DBNull.Value, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            dbparamsCropInfo.Add(new SqlParameter { ParameterName = "@Crop_Type", Value = DBNull.Value, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });

            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(spGetSubmitProductivity, dbparamsCropInfo, SqlHelper.ExecutionType.Procedure);
            GetSubmitProductivityModel postSubmitProductivityModel = new GetSubmitProductivityModel();
            GetSubmitProductivityModel postSubmitProductivityModels;

            if (dt != null && dt.Rows.Count > 0)
            {
                postSubmitProductivityModels = SqlHelper.ConvertDataTableToList<GetSubmitProductivityModel>(dt)[0];
                postSubmitProductivityModel.District_id = postSubmitProductivityModels.District_id;
                postSubmitProductivityModel.District_name = postSubmitProductivityModels.District_name;

                for (var i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    CroplistGet croplistGet = SqlHelper.ConvertDataTableToList<CroplistGet>(dt)[i];
                    postSubmitProductivityModel.Croplist.Add(croplistGet);
                }

                list.Add(postSubmitProductivityModel);
            }

            return list;
        }

        /// <summary>
        /// GetHortProduceActualBlock.
        /// </summary>
        /// <param name="block_Id">block_Id.</param>
        /// <returns>List Details.</returns>
        public List<HortProduceActualBlock> GetHortProduceActualBlock(int block_Id)
        {
            List<HortProduceActualBlock> list = new List<HortProduceActualBlock>();
            List<DbParameter> dbparams = new List<DbParameter>();
            dbparams.Add(new SqlParameter { ParameterName = "@query_name", Value = qnGetHortProduceActualBlock, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@District_Id", Value = DBNull.Value, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@Block_id", Value = block_Id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@Panchayat_Id", Value = DBNull.Value, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@Season_id", Value = DBNull.Value, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@Crop_id", Value = DBNull.Value, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@Last_Refresh_Ts", Value = DBNull.Value, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@Ist_Date", Value = DBNull.Value, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });

            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(spGetHortProduceActualPanch, dbparams, SqlHelper.ExecutionType.Procedure);

            if (dt != null && dt.Rows.Count > 0)
            {
                list = SqlHelper.ConvertDataTableToList<HortProduceActualBlock>(dt);
            }

            return list;
        }

        /// <summary>
        /// GetHortProduceActualDistrict.
        /// </summary>
        /// <param name="district_Id">district_Id.</param>
        /// <returns>List Values.</returns>
        public List<HortProduceActualDistrict> GetHortProduceActualDistrict(int district_Id)
        {
            List<HortProduceActualDistrict> list = new List<HortProduceActualDistrict>();
            List<DbParameter> dbparams = new List<DbParameter>();
            dbparams.Add(new SqlParameter { ParameterName = "@query_name", Value = qnGetHortProduceActualDistrict, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@District_Id", Value = district_Id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@Block_id", Value = DBNull.Value, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@Panchayat_Id", Value = DBNull.Value, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@Season_id", Value = DBNull.Value, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@Crop_id", Value = DBNull.Value, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@Last_Refresh_Ts", Value = DBNull.Value, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@Ist_Date", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });

            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(spGetHortProduceActualPanch, dbparams, SqlHelper.ExecutionType.Procedure);

            if (dt != null && dt.Rows.Count > 0)
            {
                list = SqlHelper.ConvertDataTableToList<HortProduceActualDistrict>(dt);
            }

            return list;
        }

        /// <summary>
        /// GetHortProduceBHO.
        /// </summary>
        /// <param name="district_id">district_id.</param>
        /// <param name="block_id">block_id.</param>
        /// <param name="season_id">season_id.</param>
        /// <param name="panchyat_id">panchyat_id.</param>
        /// <returns>List Values.</returns>
        public List<GetHortProduceBholstModel> GetHortProduceBHO(int district_id, int block_id, int season_id, string panchyat_id)
        {
            List<GetHortProduceBhoModel> cropCoverageActualResponse;

            List<DbParameter> dbparamsCropInfoDAODeltaDist = new List<DbParameter>();
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@query_name", Value = qnGetHortProduceBHO, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@District_Id", Value = district_id, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@Block_id", Value = block_id, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@Panchayat_Id", Value = panchyat_id, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@Season_id", Value = season_id, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@Crop_id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@Last_Refresh_Ts", Value = DBNull.Value, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@Ist_Date", Value = DBNull.Value, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });

            DataTable lst = SqlHelper.ExecuteSelect<SqlConnection>(spGetHortProduceActualPanch, dbparamsCropInfoDAODeltaDist, SqlHelper.ExecutionType.Procedure);
            var cropCoverageAct = lst.AsEnumerable().Any() ? SqlHelper.ConvertDataTableToList<GetHortProduceBhoModel>(lst) : null;

            List<GetHortProduceBholstModel> getHortProduceBholstModel = new List<GetHortProduceBholstModel>();
            if (cropCoverageAct.Count > 0)
            {
                cropCoverageActualResponse = cropCoverageAct.Where(a => a.Panchayat_id == Convert.ToInt32(panchyat_id) && a.Block_id == Convert.ToInt32(block_id) && a.Season_id == Convert.ToInt32(season_id) && a.District_id == Convert.ToInt32(district_id)).ToList();
                GetHortProduceBholstModel produceBHOlstModel = new GetHortProduceBholstModel();
                foreach (var item in cropCoverageActualResponse)
                {
                    produceBHOlstModel.Block_id = item.Block_id;
                    produceBHOlstModel.Block_name = item.Block_name;
                    produceBHOlstModel.Panchayat_id = item.Panchayat_id;
                    produceBHOlstModel.Panchayat_name = item.Panchayat_name;
                    produceBHOlstModel.District_id = item.District_id;
                    produceBHOlstModel.District_name = item.District_name;
                    produceBHOlstModel.Reported_date = item.Reported_date;
                    produceBHOlstModel.Season_id = item.Season_id;
                    produceBHOlstModel.Season_name = item.Season_name;

                    GetHortProduceBhoCrplstModel crpModel = new GetHortProduceBhoCrplstModel()
                    {
                        Reported_date = item.Reported_date,
                        Crop_id = item.Crop_id,
                        Ac_submitted_date = item.Ac_submitted_date,
                        Ac_submitted_userid = item.Ac_submitted_userid,
                        Ac_submitted_username = item.Ac_submitted_username,
                        Ac_submit_flag = item.Ac_submit_flag,
                        Unit_of_measure = item.Unit_of_measure,
                        Submission_source = string.Empty,
                        Adh_add_edit_flag = item.Adh_add_edit_flag,
                        Adh_approval_flag = item.Adh_approval_flag,
                        Adh_approval_reason = item.Adh_approval_reason,
                        Adh_approved_date = item.Adh_approved_date,
                        Adh_approved_userid = item.Adh_approved_userid,
                        Adh_approved_username = item.Adh_approved_username,
                        Bho_add_edit_flag = item.Bho_add_edit_flag,
                        Bho_approval_flag = item.Bho_approval_flag,
                        Bho_approval_reason = item.Bho_approval_reason,
                        Bho_approved_date = item.Bho_approved_date,
                        Bho_approved_userid = item.Bho_approved_userid,
                        Bho_approved_username = item.Bho_approved_username,
                        Crop_category = item.Crop_category,
                        Crop_name = item.Crop_name,
                        Final_cvrg_flg = item.Final_cvrg_flg,
                        Produce_curr = item.Produce_curr,
                        Produce_prev = item.Produce_prev,
                        Production = item.Production,
                        Rec_Created_Date = item.Rec_Created_Date,
                        Rec_Created_Userid = item.Rec_Created_Userid,
                        Rec_Updated_Date = item.Rec_Updated_Date,
                        Rec_Updated_Userid = item.Rec_Updated_Userid,
                        Updatedby = item.Updatedby,
                        Pending_adh_value = item.Pending_adh_value,
                        Approved_adh_value = item.Approved_adh_value,
                    };

                    produceBHOlstModel.CropList.Add(crpModel);
                }

                getHortProduceBholstModel.Add(produceBHOlstModel);

                return getHortProduceBholstModel;
            }

            return getHortProduceBholstModel;
        }

        /// <summary>
        /// GetHortiProduceCoverageActualBHOOffline.
        /// </summary>
        /// <param name="district_id">district_id.</param>
        /// <param name="block_id">block_id.</param>
        /// <param name="season_id">season_id.</param>
        /// <param name="crop_ids">crop_ids.</param>
        /// <returns>List Values.</returns>
        public List<HortProduceCoverageActual> GetHortiProduceCoverageActualBHOOffline(int district_id, int block_id, int season_id, string crop_ids)
        {
            List<HortProduceCoverageActual> cropCoverageActualResponse;
            DataTable dtBlocks;
            DataTable dtPanchayats;

            List<DbParameter> dbparamsCropInfoDAODeltaDist = new List<DbParameter>();
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@query_name", Value = qnGetHortiProduceCoverageActualBHOOffline, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@District_Id", Value = string.IsNullOrEmpty(district_id.ToString()) ? DBNull.Value : (object)district_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@Block_id", Value = string.IsNullOrEmpty(block_id.ToString()) ? DBNull.Value : (object)block_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@Panchayat_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@Season_id", Value = string.IsNullOrEmpty(season_id.ToString()) ? DBNull.Value : (object)season_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@Crop_id", Value = string.IsNullOrEmpty(crop_ids) ? DBNull.Value : (object)crop_ids, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@Last_Refresh_Ts", Value = DBNull.Value, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@Ist_Date", Value = this.istDate, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });

            DataTable dtDistricts = SqlHelper.ExecuteSelect<SqlConnection>(spGetHortProduceActualPanch, dbparamsCropInfoDAODeltaDist, SqlHelper.ExecutionType.Procedure);

            if (dtDistricts.Rows.Count > 0)
            {
                var cropCoverageAct = dtDistricts.AsEnumerable().Any() ? SqlHelper.ConvertDataTableToList<HortProduceCoverageActual>(dtDistricts) : null;

                List<DbParameter> dbparamsCropInfoDAODeltaBlk = new List<DbParameter>();
                dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@query_name", Value = qnGetHortiProduceCoverageActualBHOOfflineBlk, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@District_Id", Value = string.IsNullOrEmpty(district_id.ToString()) ? DBNull.Value : (object)district_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@Block_id", Value = string.IsNullOrEmpty(block_id.ToString()) ? DBNull.Value : (object)block_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@Panchayat_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@Season_id", Value = string.IsNullOrEmpty(season_id.ToString()) ? DBNull.Value : (object)season_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@Crop_id", Value = string.IsNullOrEmpty(crop_ids) ? DBNull.Value : (object)crop_ids, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@Last_Refresh_Ts", Value = DBNull.Value, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@Ist_Date", Value = this.istDate, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });

                List<DbParameter> dbparamsCropInfoDAODeltaPanchyt = new List<DbParameter>();
                dbparamsCropInfoDAODeltaPanchyt.Add(new SqlParameter { ParameterName = "@query_name", Value = qnGetHortiProduceCoverageActualBHOOfflinePanch, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaPanchyt.Add(new SqlParameter { ParameterName = "@District_Id", Value = string.IsNullOrEmpty(district_id.ToString()) ? DBNull.Value : (object)district_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaPanchyt.Add(new SqlParameter { ParameterName = "@Block_id", Value = string.IsNullOrEmpty(block_id.ToString()) ? DBNull.Value : (object)block_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaPanchyt.Add(new SqlParameter { ParameterName = "@Panchayat_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaPanchyt.Add(new SqlParameter { ParameterName = "@Season_id", Value = string.IsNullOrEmpty(season_id.ToString()) ? DBNull.Value : (object)season_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaPanchyt.Add(new SqlParameter { ParameterName = "@Crop_id", Value = string.IsNullOrEmpty(crop_ids) ? DBNull.Value : (object)crop_ids, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaPanchyt.Add(new SqlParameter { ParameterName = "@Last_Refresh_Ts", Value = DBNull.Value, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaPanchyt.Add(new SqlParameter { ParameterName = "@Ist_Date", Value = this.istDate, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });

                dtBlocks = SqlHelper.ExecuteSelect<SqlConnection>(spGetHortProduceActualPanch, dbparamsCropInfoDAODeltaBlk, SqlHelper.ExecutionType.Procedure);
                dtPanchayats = SqlHelper.ExecuteSelect<SqlConnection>(spGetHortProduceActualPanch, dbparamsCropInfoDAODeltaPanchyt, SqlHelper.ExecutionType.Procedure);

                if (cropCoverageAct != null && dtPanchayats.Rows.Count > 0)
                {
                    var actBlocks = SqlHelper.ConvertDataTableToList<HortProduceBlock>(dtBlocks);
                    var actPanchayats = SqlHelper.ConvertDataTableToList<HortProduceCoveragePanchayat>(dtPanchayats);
                    cropCoverageActualResponse = cropCoverageAct;

                    if (actBlocks.Any())
                    {
                        foreach (var item in actBlocks)
                        {
                            item.PanchayatList = actPanchayats.Where(x => x.Block_id == item.Block_id && x.Crop_id == item.Crop_id).ToList();
                        }

                        foreach (var item in cropCoverageActualResponse)
                        {
                            item.BlockList = actBlocks.Where(x => x.Crop_id == item.Crop_id).Select(x =>
                             new HortProduceBlock
                             {
                                 Reported_date = x.Reported_date,
                                 Block_id = x.Block_id,
                                 Block_name = x.Block_name,
                                 Production = x.Production,
                                 Produce_prev = x.Produce_prev,
                                 Produce_curr = x.Produce_curr,
                                 Createdby = x.Createdby,
                                 Updatedby = x.Updatedby,
                                 Refreshed_date = x.Refreshed_date,
                                 Refreshed_userid = x.Refreshed_userid,
                                 Adh_approval_flag = x.Adh_approval_flag,
                                 Adh_approval_reason = x.Adh_approval_reason,
                                 Adh_approved_date = x.Adh_approved_date,
                                 Adh_approved_userid = x.Adh_approved_userid,
                                 Refreshed_username = x.Refreshed_username,
                                 Adh_approved_username = x.Adh_approved_username,
                                 PanchayatList = x.PanchayatList.Any() ? x.PanchayatList.Select(x =>
                                   new HortProduceCoveragePanchayat
                                   {
                                       Reported_date = x.Reported_date,
                                       Panchayat_id = x.Panchayat_id,
                                       Panchayat_name = x.Panchayat_name,
                                       Crop_id = x.Crop_id,
                                       Production = x.Production,
                                       Produce_prev = x.Produce_prev,
                                       Produce_curr = x.Produce_curr,
                                       Createdby = x.Createdby,
                                       Updatedby = x.Updatedby,
                                       Submission_source = x.Submission_source,
                                       Ac_submitted_date = x.Ac_submitted_date,
                                       Ac_submitted_userid = x.Ac_submitted_userid,
                                       Bho_approval_flag = x.Bho_approval_flag,
                                       Bho_approval_reason = x.Bho_approval_reason,
                                       Bho_approved_date = x.Bho_approved_date,
                                       Bho_approved_userid = x.Bho_approved_userid,
                                       Adh_approval_flag = x.Adh_approval_flag,
                                       Adh_approval_reason = x.Adh_approval_reason,
                                       Adh_approved_userid = x.Adh_approved_userid,
                                       Ac_submitted_username = x.Ac_submitted_username,
                                       Bho_approved_username = x.Bho_approved_username,
                                       Adh_approved_username = x.Adh_approved_username,
                                       Ac_submit_flag = x.Ac_submit_flag,
                                       Bho_add_edit_flag = x.Bho_add_edit_flag,
                                       Adh_add_edit_flag = x.Adh_add_edit_flag,
                                       Final_cvrg_flg = x.Final_cvrg_flg,
                                       Rec_updated_userid = x.Rec_updated_userid,
                                       Rec_updated_date = x.Rec_updated_date,
                                       Unit_of_measure = x.Unit_of_measure,
                                   })
                             .ToList() : null,
                             }).ToList();
                        }
                    }

                    return cropCoverageActualResponse;
                }
            }

            return null;
        }

        /// <summary>
        /// GetHortProduceActualADHOffline.
        /// </summary>
        /// <param name="district_id">district_id.</param>
        /// <param name="season_id">season_id.</param>
        /// <param name="crop_ids">crop_ids.</param>
        /// <returns>List Values.</returns>
        public List<HortProduceCoverageActual> GetHortProduceActualADHOffline(int district_id, int season_id, string crop_ids)
        {
            List<HortProduceCoverageActual> cropCoverageActualResponse;
            DataTable dtDistricts;
            DataTable dtBlocks;
            DataTable dtPanchayats;

            List<DbParameter> dbparamsCropInfoDAODeltaDist = new List<DbParameter>();
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@query_name", Value = qnGetHortProduceActualADHOffline, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@District_Id", Value = string.IsNullOrEmpty(district_id.ToString()) ? DBNull.Value : (object)district_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@Block_id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@Panchayat_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@Season_id", Value = string.IsNullOrEmpty(season_id.ToString()) ? DBNull.Value : (object)season_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@Crop_id", Value = string.IsNullOrEmpty(crop_ids) ? DBNull.Value : (object)crop_ids, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@Last_Refresh_Ts", Value = DBNull.Value, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@Ist_Date", Value = DBNull.Value, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });

            dtDistricts = SqlHelper.ExecuteSelect<SqlConnection>(spGetHortProduceActualPanch, dbparamsCropInfoDAODeltaDist, SqlHelper.ExecutionType.Procedure);

            if (dtDistricts.Rows.Count > 0)
            {
                var cropCoverageAct = dtDistricts.AsEnumerable().Any() ? SqlHelper.ConvertDataTableToList<HortProduceCoverageActual>(dtDistricts) : null;

                List<DbParameter> dbparamsCropInfoDAODeltaBlk = new List<DbParameter>();
                dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@query_name", Value = qnGetHortProduceActualADHOfflineBlk, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@District_Id", Value = string.IsNullOrEmpty(district_id.ToString()) ? DBNull.Value : (object)district_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@Block_id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@Panchayat_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@Season_id", Value = string.IsNullOrEmpty(season_id.ToString()) ? DBNull.Value : (object)season_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@Crop_id", Value = string.IsNullOrEmpty(crop_ids) ? DBNull.Value : (object)crop_ids, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@Last_Refresh_Ts", Value = DBNull.Value, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@Ist_Date", Value = DBNull.Value, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });

                List<DbParameter> dbparamsCropInfoDAODeltaPanchyt = new List<DbParameter>();
                dbparamsCropInfoDAODeltaPanchyt.Add(new SqlParameter { ParameterName = "@query_name", Value = qnGetHortProduceActualADHOfflinePanch, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaPanchyt.Add(new SqlParameter { ParameterName = "@District_Id", Value = string.IsNullOrEmpty(district_id.ToString()) ? DBNull.Value : (object)district_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaPanchyt.Add(new SqlParameter { ParameterName = "@Block_id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaPanchyt.Add(new SqlParameter { ParameterName = "@Panchayat_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaPanchyt.Add(new SqlParameter { ParameterName = "@Season_id", Value = string.IsNullOrEmpty(season_id.ToString()) ? DBNull.Value : (object)season_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaPanchyt.Add(new SqlParameter { ParameterName = "@Crop_id", Value = string.IsNullOrEmpty(crop_ids) ? DBNull.Value : (object)crop_ids, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaPanchyt.Add(new SqlParameter { ParameterName = "@Last_Refresh_Ts", Value = DBNull.Value, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaPanchyt.Add(new SqlParameter { ParameterName = "@Ist_Date", Value = this.istDate, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });

                dtBlocks = SqlHelper.ExecuteSelect<SqlConnection>(spGetHortProduceActualPanch, dbparamsCropInfoDAODeltaBlk, SqlHelper.ExecutionType.Procedure);
                dtPanchayats = SqlHelper.ExecuteSelect<SqlConnection>(spGetHortProduceActualPanch, dbparamsCropInfoDAODeltaPanchyt, SqlHelper.ExecutionType.Procedure);

                if (cropCoverageAct != null && dtPanchayats.Rows.Count > 0)
                {
                    var actBlocks = SqlHelper.ConvertDataTableToList<HortProduceBlock>(dtBlocks);
                    var actPanchayats = SqlHelper.ConvertDataTableToList<HortProduceCoveragePanchayat>(dtPanchayats);
                    cropCoverageActualResponse = cropCoverageAct;

                    if (actBlocks.Any())
                    {
                        foreach (var item in actBlocks)
                        {
                            item.PanchayatList = actPanchayats.Where(x => x.Block_id == item.Block_id && x.Crop_id == item.Crop_id).ToList();
                        }

                        foreach (var item in cropCoverageActualResponse)
                        {
                            item.BlockList = actBlocks.Where(x => x.Crop_id == item.Crop_id).Select(x =>
                             new HortProduceBlock
                             {
                                 Reported_date = x.Reported_date,
                                 Block_id = x.Block_id,
                                 Block_name = x.Block_name,
                                 Production = x.Production,
                                 Produce_prev = x.Produce_prev,
                                 Produce_curr = x.Produce_curr,
                                 Createdby = x.Createdby,
                                 Updatedby = x.Updatedby,
                                 Refreshed_date = x.Refreshed_date,
                                 Refreshed_userid = x.Refreshed_userid,
                                 Adh_approval_flag = x.Adh_approval_flag,
                                 Adh_approval_reason = x.Adh_approval_reason,
                                 Adh_approved_date = x.Adh_approved_date,
                                 Adh_approved_userid = x.Adh_approved_userid,
                                 Refreshed_username = x.Refreshed_username,
                                 Adh_approved_username = x.Adh_approved_username,
                                 PanchayatList = x.PanchayatList.Any() ? x.PanchayatList.Select(x =>
                                   new HortProduceCoveragePanchayat
                                   {
                                       Reported_date = x.Reported_date,
                                       Panchayat_id = x.Panchayat_id,
                                       Panchayat_name = x.Panchayat_name,
                                       Crop_id = x.Crop_id,
                                       Production = x.Production,
                                       Produce_prev = x.Produce_prev,
                                       Produce_curr = x.Produce_curr,
                                       Createdby = x.Createdby,
                                       Updatedby = x.Updatedby,
                                       Submission_source = x.Submission_source,
                                       Ac_submitted_date = x.Ac_submitted_date,
                                       Ac_submitted_userid = x.Ac_submitted_userid,
                                       Bho_approval_flag = x.Bho_approval_flag,
                                       Bho_approval_reason = x.Bho_approval_reason,
                                       Bho_approved_date = x.Bho_approved_date,
                                       Bho_approved_userid = x.Bho_approved_userid,
                                       Adh_approval_flag = x.Adh_approval_flag,
                                       Adh_approval_reason = x.Adh_approval_reason,
                                       Adh_approved_date = x.Adh_approved_date,
                                       Adh_approved_userid = x.Adh_approved_userid,
                                       Ac_submitted_username = x.Ac_submitted_username,
                                       Bho_approved_username = x.Bho_approved_username,
                                       Adh_approved_username = x.Adh_approved_username,
                                       Ac_submit_flag = x.Ac_submit_flag,
                                       Bho_add_edit_flag = x.Bho_add_edit_flag,
                                       Adh_add_edit_flag = x.Adh_add_edit_flag,
                                       Final_cvrg_flg = x.Final_cvrg_flg,
                                       Rec_updated_userid = x.Rec_updated_userid,
                                       Rec_updated_date = x.Rec_updated_date,
                                       Unit_of_measure = x.Unit_of_measure,
                                   })
                             .ToList() : null,
                             }).ToList();
                        }
                    }

                    return cropCoverageActualResponse;
                }
            }

            return null;
        }

        /// <summary>
        /// GetHortProduceActualADH.
        /// </summary>
        /// <param name="district_id">district_id.</param>
        /// <param name="season_id">season_id.</param>
        /// <param name="crop_ids">crop_ids.</param>
        /// <returns>List Values.</returns>
        public HortProduceCoverageActl GetHortProduceActualADH(int district_id, int season_id, string crop_ids)
        {
            HortProduceCoverageActl cropCoverageActualResponse;
            DataTable dtDistricts;
            DataTable dtBlocks;
            DataTable dtPanchayats;

            List<DbParameter> dbparamsCropInfoDAODeltaDist = new List<DbParameter>();
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@query_name", Value = qnGetHortProduceActualADH, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@District_Id", Value = string.IsNullOrEmpty(district_id.ToString()) ? DBNull.Value : (object)district_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@Block_id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@Panchayat_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@Season_id", Value = string.IsNullOrEmpty(season_id.ToString()) ? DBNull.Value : (object)season_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@Crop_id", Value = string.IsNullOrEmpty(crop_ids) ? DBNull.Value : (object)crop_ids, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@Last_Refresh_Ts", Value = DBNull.Value, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@Ist_Date", Value = this.istDate, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });

            dtDistricts = SqlHelper.ExecuteSelect<SqlConnection>(spGetHortProduceActualPanch, dbparamsCropInfoDAODeltaDist, SqlHelper.ExecutionType.Procedure);

            if (dtDistricts.Rows.Count > 0)
            {
                var cropCoverageAct = dtDistricts.AsEnumerable().Any() ? SqlHelper.ConvertDataTableToList<HortProduceCoverageActl>(dtDistricts)[0] : null;

                List<DbParameter> dbparamsCropInfoDAODeltaBlk = new List<DbParameter>();
                dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@query_name", Value = qnGetHortProduceActualADHBlk, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@District_Id", Value = string.IsNullOrEmpty(district_id.ToString()) ? DBNull.Value : (object)district_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@Block_id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@Panchayat_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@Season_id", Value = string.IsNullOrEmpty(season_id.ToString()) ? DBNull.Value : (object)season_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@Crop_id", Value = string.IsNullOrEmpty(crop_ids) ? DBNull.Value : (object)crop_ids, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@Last_Refresh_Ts", Value = DBNull.Value, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@Ist_Date", Value = this.istDate, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });

                List<DbParameter> dbparamsCropInfoDAODeltaPanchyt = new List<DbParameter>();
                dbparamsCropInfoDAODeltaPanchyt.Add(new SqlParameter { ParameterName = "@query_name", Value = qnGetHortProduceActualADHPanch, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaPanchyt.Add(new SqlParameter { ParameterName = "@District_Id", Value = string.IsNullOrEmpty(district_id.ToString()) ? DBNull.Value : (object)district_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaPanchyt.Add(new SqlParameter { ParameterName = "@Block_id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaPanchyt.Add(new SqlParameter { ParameterName = "@Panchayat_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaPanchyt.Add(new SqlParameter { ParameterName = "@Season_id", Value = string.IsNullOrEmpty(season_id.ToString()) ? DBNull.Value : (object)season_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaPanchyt.Add(new SqlParameter { ParameterName = "@Crop_id", Value = string.IsNullOrEmpty(crop_ids) ? DBNull.Value : (object)crop_ids, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaPanchyt.Add(new SqlParameter { ParameterName = "@Last_Refresh_Ts", Value = DBNull.Value, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaPanchyt.Add(new SqlParameter { ParameterName = "@Ist_Date", Value = this.istDate, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });

                dtBlocks = SqlHelper.ExecuteSelect<SqlConnection>(spGetHortProduceActualPanch, dbparamsCropInfoDAODeltaBlk, SqlHelper.ExecutionType.Procedure);
                dtPanchayats = SqlHelper.ExecuteSelect<SqlConnection>(spGetHortProduceActualPanch, dbparamsCropInfoDAODeltaPanchyt, SqlHelper.ExecutionType.Procedure);

                if (cropCoverageAct != null && dtPanchayats.Rows.Count > 0)
                {
                    var actBlocks = SqlHelper.ConvertDataTableToList<HortProduceBlk>(dtBlocks);
                    var actPanchayats = SqlHelper.ConvertDataTableToList<HortProduceCoveragePanchyt>(dtPanchayats);
                    cropCoverageActualResponse = cropCoverageAct;

                    if (actBlocks.Any())
                    {
                        cropCoverageActualResponse.BlockList = new List<HortProduceBlk>();

                        foreach (var item in actBlocks)
                        {
                            item.PanchayatList = actPanchayats.Where(x => x.Block_id == item.Block_id).ToList();
                        }

                        cropCoverageActualResponse.BlockList = actBlocks.Select(x =>
                       new HortProduceBlk
                       {
                           Reported_date = x.Reported_date,
                           Block_id = x.Block_id,
                           Block_name = x.Block_name,
                           Production = x.Production,
                           Produce_prev = x.Produce_prev,
                           Produce_curr = x.Produce_curr,
                           Createdby = x.Createdby,
                           Updatedby = x.Updatedby,
                           Refreshed_date = x.Refreshed_date,
                           Refreshed_userid = x.Refreshed_userid,
                           Adh_approval_flag = x.Adh_approval_flag,
                           Adh_approval_reason = x.Adh_approval_reason,
                           Adh_approved_date = x.Adh_approved_date,
                           Refreshed_username = x.Refreshed_username,
                           Adh_approved_username = x.Adh_approved_username,

                           PanchayatList = x.PanchayatList.Any() ? x.PanchayatList.Select(x =>
                             new HortProduceCoveragePanchyt
                             {
                                 Reported_date = x.Reported_date,
                                 Panchayat_id = x.Panchayat_id,
                                 Panchayat_name = x.Panchayat_name,
                                 Production = x.Production,
                                 Produce_prev = x.Produce_prev,
                                 Produce_curr = x.Produce_curr,
                                 Createdby = x.Createdby,
                                 Updatedby = x.Updatedby,
                                 Submission_source = x.Submission_source,
                                 Ac_submitted_date = x.Ac_submitted_date,
                                 Ac_submitted_userid = x.Ac_submitted_userid,
                                 Bho_approval_flag = x.Bho_approval_flag,
                                 Bho_approval_reason = x.Bho_approval_reason,
                                 Bho_approved_date = x.Bho_approved_date,
                                 Bho_approved_userid = x.Bho_approved_userid,
                                 Adh_approval_flag = x.Adh_approval_flag,
                                 Adh_approval_reason = x.Adh_approval_reason,
                                 Adh_approved_date = x.Adh_approved_date,
                                 Adh_approved_userid = x.Adh_approved_userid,
                                 Ac_submitted_username = x.Ac_submitted_username,
                                 Bho_approved_username = x.Bho_approved_username,
                                 Adh_approved_username = x.Adh_approved_username,
                                 Ac_submit_flag = x.Ac_submit_flag,
                                 Bho_add_edit_flag = x.Bho_add_edit_flag,
                                 Adh_add_edit_flag = x.Adh_add_edit_flag,
                                 Final_cvrg_flg = x.Final_cvrg_flg,
                                 Rec_updated_userid = x.Rec_updated_userid,
                                 Rec_updated_date = x.Rec_updated_date,
                                 Unit_of_measure = x.Unit_of_measure,
                                 Pending_adh_value = x.Pending_adh_value,
                                 Approved_adh_value = x.Approved_adh_value,
                             })
                       .ToList() : new List<HortProduceCoveragePanchyt>(),
                       }).ToList();
                    }

                    return cropCoverageActualResponse;
                }
            }

            return null;
        }

        /// <summary>
        /// GetHortProduceActualBHO.
        /// </summary>
        /// <param name="district_id">district_id.</param>
        /// <param name="block_id">block_id.</param>
        /// <param name="season_id">season_id.</param>
        /// <param name="crop_ids">crop_ids.</param>
        /// <returns>List Values.</returns>
        public HortProduceCoverageActl GetHortProduceActualBHO(int district_id, int block_id, int season_id, string crop_ids)
        {
            HortProduceCoverageActl cropCoverageActualResponse;
            DataTable dtDistricts;
            DataTable dtBlocks;
            DataTable dtPanchayats;

            List<DbParameter> dbparamsCropInfoDAODeltaDist = new List<DbParameter>();
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@query_name", Value = qnGetHortProduceActualBHO, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@District_Id", Value = string.IsNullOrEmpty(district_id.ToString()) ? DBNull.Value : (object)district_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@Block_id", Value = block_id, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@Panchayat_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@Season_id", Value = string.IsNullOrEmpty(season_id.ToString()) ? DBNull.Value : (object)season_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@Crop_id", Value = string.IsNullOrEmpty(crop_ids) ? DBNull.Value : (object)crop_ids, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@Last_Refresh_Ts", Value = DBNull.Value, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@Ist_Date", Value = this.istDate, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });

            dtDistricts = SqlHelper.ExecuteSelect<SqlConnection>(spGetHortProduceActualPanch, dbparamsCropInfoDAODeltaDist, SqlHelper.ExecutionType.Procedure);

            if (dtDistricts.Rows.Count > 0)
            {
                var cropCoverageAct = dtDistricts.AsEnumerable().Any() ? SqlHelper.ConvertDataTableToList<HortProduceCoverageActl>(dtDistricts)[0] : null;

                List<DbParameter> dbparamsCropInfoDAODeltaBlk = new List<DbParameter>();
                dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@query_name", Value = qnGetHortProduceActualBHOBlk, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@District_Id", Value = string.IsNullOrEmpty(district_id.ToString()) ? DBNull.Value : (object)district_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@Block_id", Value = block_id, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@Panchayat_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@Season_id", Value = season_id, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@Crop_id", Value = crop_ids, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@Last_Refresh_Ts", Value = DBNull.Value, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@Ist_Date", Value = this.istDate, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });

                List<DbParameter> dbparamsCropInfoDAODeltaPanchyt = new List<DbParameter>();
                dbparamsCropInfoDAODeltaPanchyt.Add(new SqlParameter { ParameterName = "@query_name", Value = qnGetHortProduceActualBHOPanch, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaPanchyt.Add(new SqlParameter { ParameterName = "@District_Id", Value = district_id, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaPanchyt.Add(new SqlParameter { ParameterName = "@Block_id", Value = block_id, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaPanchyt.Add(new SqlParameter { ParameterName = "@Panchayat_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaPanchyt.Add(new SqlParameter { ParameterName = "@Season_id", Value = season_id, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaPanchyt.Add(new SqlParameter { ParameterName = "@Crop_id", Value = crop_ids, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaPanchyt.Add(new SqlParameter { ParameterName = "@Last_Refresh_Ts", Value = DBNull.Value, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaPanchyt.Add(new SqlParameter { ParameterName = "@Ist_Date", Value = this.istDate, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });

                dtBlocks = SqlHelper.ExecuteSelect<SqlConnection>(spGetHortProduceActualPanch, dbparamsCropInfoDAODeltaBlk, SqlHelper.ExecutionType.Procedure);
                dtPanchayats = SqlHelper.ExecuteSelect<SqlConnection>(spGetHortProduceActualPanch, dbparamsCropInfoDAODeltaPanchyt, SqlHelper.ExecutionType.Procedure);

                if (cropCoverageAct != null && dtPanchayats.Rows.Count > 0)
                {
                    var actBlocks = SqlHelper.ConvertDataTableToList<HortProduceBlk>(dtBlocks);
                    var actPanchayats = SqlHelper.ConvertDataTableToList<HortProduceCoveragePanchyt>(dtPanchayats);
                    cropCoverageActualResponse = cropCoverageAct;

                    if (actBlocks.Any())
                    {
                        cropCoverageActualResponse.BlockList = new List<HortProduceBlk>();

                        foreach (var item in actBlocks)
                        {
                            item.PanchayatList = actPanchayats.Where(x => x.Block_id == item.Block_id).ToList();
                        }

                        cropCoverageActualResponse.BlockList = actBlocks.Select(x =>
                        new HortProduceBlk
                        {
                            Reported_date = x.Reported_date,
                            Block_id = x.Block_id,
                            Block_name = x.Block_name,
                            Production = x.Production,
                            Produce_prev = x.Produce_prev,
                            Produce_curr = x.Produce_curr,
                            Createdby = x.Createdby,
                            Updatedby = x.Updatedby,
                            Refreshed_date = x.Refreshed_date,
                            Refreshed_userid = x.Refreshed_userid,
                            Adh_approval_flag = x.Adh_approval_flag,
                            Adh_approval_reason = x.Adh_approval_reason,
                            Adh_approved_date = x.Adh_approved_date,
                            Adh_approved_userid = x.Adh_approved_userid,
                            Refreshed_username = x.Refreshed_username,
                            Adh_approved_username = x.Adh_approved_username,

                            PanchayatList = x.PanchayatList.Any() ? x.PanchayatList.Select(x =>
                              new HortProduceCoveragePanchyt
                              {
                                  Reported_date = x.Reported_date,
                                  Panchayat_id = x.Panchayat_id,
                                  Panchayat_name = x.Panchayat_name,
                                  Production = x.Production,
                                  Produce_prev = x.Produce_prev,
                                  Produce_curr = x.Produce_curr,
                                  Createdby = x.Createdby,
                                  Updatedby = x.Updatedby,
                                  Submission_source = x.Submission_source,
                                  Ac_submitted_date = x.Ac_submitted_date,
                                  Ac_submitted_userid = x.Ac_submitted_userid,
                                  Bho_approval_flag = x.Bho_approval_flag,
                                  Bho_approval_reason = x.Bho_approval_reason,
                                  Bho_approved_date = x.Bho_approved_date,
                                  Bho_approved_userid = x.Bho_approved_userid,
                                  Adh_approval_flag = x.Adh_approval_flag,
                                  Adh_approval_reason = x.Adh_approval_reason,
                                  Adh_approved_date = x.Adh_approved_date,
                                  Adh_approved_userid = x.Adh_approved_userid,
                                  Ac_submitted_username = x.Ac_submitted_username,
                                  Bho_approved_username = x.Bho_approved_username,
                                  Adh_approved_username = x.Adh_approved_username,
                                  Ac_submit_flag = x.Ac_submit_flag,
                                  Bho_add_edit_flag = x.Bho_add_edit_flag,
                                  Adh_add_edit_flag = x.Adh_add_edit_flag,
                                  Final_cvrg_flg = x.Final_cvrg_flg,
                                  Rec_updated_userid = x.Rec_updated_userid,
                                  Rec_updated_date = x.Rec_updated_date,
                                  Unit_of_measure = x.Unit_of_measure,
                              })
                        .ToList() : null,
                        }).ToList();
                    }

                    return cropCoverageActualResponse;
                }
            }

            return null;
        }

        /// <summary>
        /// GetAllSeedPerformanceHorticultureDBT.
        /// </summary>
        /// <param name="district_id">district_id.</param>
        /// <param name="block_id">block_id.</param>
        /// <param name="panchayat_id">panchayat_id.</param>
        /// <param name="season_id">season_id.</param>
        /// <returns>List Values.</returns>
        public List<SeedPerfHortdbt> GetAllSeedPerformanceHorticultureDBT(int district_id, int block_id, int panchayat_id, int season_id)
        {
            List<SeedPerfHortdbt> list = new List<SeedPerfHortdbt>();
            List<DbParameter> dbparams = new List<DbParameter>();
            dbparams.Add(new SqlParameter { ParameterName = "@query_name", Value = qnGetAllSeedPerformanceHorticultureDBT, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@district_id", Value = district_id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@season_id", Value = season_id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@block_lg_code", Value = block_id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@panchayat_lg_code", Value = panchayat_id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(spGetAllSeedPerformanceHorticultureDBT, dbparams, SqlHelper.ExecutionType.Procedure);

            if (dt != null && dt.Rows.Count > 0)
            {
                list = SqlHelper.ConvertDataTableToList<SeedPerfHortdbt>(dt);
            }

            return list;
        }

        /// <summary>
        /// GetBlockPanchayatData.
        /// </summary>
        /// <param name="district_id">district_id.</param>
        /// <returns>List Values.</returns>
        public DbtDistrictWrapper GetBlockPanchayatData(int district_id)
        {
            DbtDistrictWrapper response = null;
            List<DbParameter> dbparams = new List<DbParameter>();
            dbparams.Add(new SqlParameter { ParameterName = "@query_name", Value = qnGetBlockPanchayatData, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@district_id", Value = district_id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });

            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(spGetAllSeedPerformanceHorticultureDBT, dbparams, SqlHelper.ExecutionType.Procedure);

            if (dt != null && dt.Rows.Count > 0)
            {
                response = SqlHelper.ConvertDataTableToList<DbtDistrictWrapper>(dt)[0];

                List<DbtBlkPanch> resp = SqlHelper.ConvertDataTableToList<DbtBlkPanch>(dt);

                response.Blocks = SqlHelper.ConvertDataTableToList<DbtBlock>(dt).Select(x => new DbtBlock { Block_lg_code = x.Block_lg_code, Block_name = x.Block_name }).GroupBy(x => x.Block_lg_code).Select(x => x.First()).ToList();

                foreach (var item in response.Blocks)
                {
                    item.Panchayat = resp.Where(x => x.Block_lg_code == item.Block_lg_code).Select(x => new DbtPanchayat { Panchayat_lg_code = x.Panchayat_lg_code, Panchayat_name = x.Panchayat_name }).GroupBy(x => x.Panchayat_lg_code).Select(x => x.First()).ToList();
                }
            }

            return response;
        }

        /// <summary>
        /// InsertHortiSeedPerformance.
        /// </summary>
        /// <param name="seedPerformance">seedPerformance.</param>
        /// <returns>INsert Process and Response Status.</returns>
        public async Task<int> InsertHortiSeedPerformance(InsertHorticultureSeedPerf seedPerformance)
        {
            BlobServiceClient blobServiceClient = new BlobServiceClient(this.blobconfig.Value.BlobConnection);

            List<DbParameter> dbparamsUserInfo = new List<DbParameter>();

            if (!string.IsNullOrEmpty(seedPerformance.ImageData))
            {
                BlobEntity blobEntity = new BlobEntity();
                blobEntity.DirectoryName = "Horti_Seed_Performance_Photos";
                blobEntity.FolderName = seedPerformance.Seed_Perf_ID.ToString() + "-" + "HORTISEEDPERFIMAGE" + ".jpg";
                blobEntity.ByteArray = seedPerformance.ImageData;

                BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient("mobileapp");

                string blobPath = blobEntity.DirectoryName + Path.AltDirectorySeparatorChar + blobEntity.FolderName;

                BlobClient blobClient = containerClient.GetBlobClient(blobPath);

                byte[] bytes1 = Convert.FromBase64String(blobEntity.ByteArray);
                Stream stream = new MemoryStream(bytes1);
                await blobClient.UploadAsync(stream, true);

                seedPerformance.Img_file_location = this.blobconfig.Value.BlobSeedPhotoHorti;
                seedPerformance.Img_file_name = blobEntity.FolderName;
            }

            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@Seed_Perf_ID", Value = seedPerformance.Seed_Perf_ID, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });

            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@Crop_production", Value = seedPerformance.Crop_production, SqlDbType = SqlDbType.Decimal, Direction = ParameterDirection.Input });

            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@Crop_productivity", Value = seedPerformance.Crop_productivity, SqlDbType = SqlDbType.Decimal, Direction = ParameterDirection.Input });

            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@latitude", Value = seedPerformance.Latitude, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });

            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@longitude", Value = seedPerformance.Longitude, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });

            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@img_file_location", Value = seedPerformance.Img_file_location, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });

            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@img_file_name", Value = seedPerformance.Img_file_name, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });

            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@userid", Value = seedPerformance.Userid, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });

            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@Response_flag", SqlDbType = SqlDbType.VarChar, Size = 500, Direction = ParameterDirection.Output });

            Dictionary<string, dynamic> result = SqlHelper.ExecuteNonQuery<SqlConnection>(spInsertHortiSeedPerformance, dbparamsUserInfo, SqlHelper.ExecutionType.Procedure);

            string spOut = DBNull.Value.Equals(result["@Response_flag"]) ? string.Empty : result["@Response_flag"];

            if (spOut == "S")
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// GetVarietiesByType.
        /// </summary>
        /// <param name="type">type.</param>
        /// <param name="seasonid">seasonid.</param>
        /// <returns>List DB Values.</returns>
        public List<Varities> GetVarietiesByType(string type, int seasonid)
        {
            List<Varities> list = new List<Varities>();
            List<DbParameter> dbparams = new List<DbParameter>();
            dbparams.Add(new SqlParameter { ParameterName = "@query_name", Value = qnGetVarietiesByType, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@Year", Value = null, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@season_id", Value = seasonid, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@Crop_activity", Value = null, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@Crop_type", Value = null, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@Crop_Name_Like", Value = type, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@date", Value = null, SqlDbType = SqlDbType.Date, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@Approval_Status", Value = string.Empty, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@Crop_ID", Value = "0", SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@District_ID", Value = "0", SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@block_ID", Value = 0, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@Panchayat_ID", Value = 0, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(spGetVarietiesByType, dbparams, SqlHelper.ExecutionType.Procedure);

            if (dt != null && dt.Rows.Count > 0)
            {
                list = SqlHelper.ConvertDataTableToList<Varities>(dt);
            }

            return list;
        }

        /// <summary>
        /// GetBlockPanchaytLgCodes.
        /// </summary>
        /// <param name="districtId">districtId.</param>
        /// <returns>Values.</returns>
        public List<BlockPanchayatLgCodes> GetBlockPanchaytLgCodes(int districtId)
        {
            List<BlockPanchayatLgCodes> list = null;

            List<DbParameter> parameters = new List<DbParameter>();

            parameters.Add(new SqlParameter { ParameterName = "@query_name", Value = qnGetblockPanchaytLgCodes, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@district_id", Value = districtId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });

            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(spGetAllSeedPerformanceHorticultureDBT, parameters, SqlHelper.ExecutionType.Procedure);
            if (dt != null && dt.Rows.Count > 0)
            {
                list = SqlHelper.ConvertDataTableToList<BlockPanchayatLgCodes>(dt);
            }

            return list;
        }
    }
}
