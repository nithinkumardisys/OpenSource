//------------------------------------------------------------------------------
// <copyright file="CropRepository.cs" company="Government of Bihar">
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
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using ADIDAS.Model.DTO;
    using ADIDAS.Model.Entities;
    using ADIDAS.Repository.Helper;
    using ADIDAS.Repository.Interfaces;
    using Azure.Storage.Blobs;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Options;

    /// <summary>
    /// CropRepository.
    /// </summary>
    public class CropRepository : BaseRepository, ICropRepository
    {
        private readonly IOptions<BlobConfig> blobconfig;
        private readonly IOptions<DBSettings> options;
        private readonly string istStrDate = "select CAST(DATEADD(HOUR, 5, DATEADD(MINUTE, 30, GETUTCDATE())) as DATE)";
        private readonly string istDate = string.Empty;

        private static string qnGetSeason = "GetSeason";
        private static string sp_dim_cntrlr = "usp_dim_cntrlr";
        private static string qn_GetCoverageSubmittedCropsBySeason = "GetCoverageSubmittedCropsBySeason";
        private static string qn_GetTargetSubmittedCropsBySeason = "GetTargetSubmittedCropsBySeason";
        private static string qn_GetCoverageSubmittedCropsBySeasonBAO = "GetCoverageSubmittedCropsBySeasonBAO";
        private static string qn_GetCoverageSubmittedCropsBySeasonDAO = "GetCoverageSubmittedCropsBySeasonDAO";
        private static string qn_GetTargetSubmittedCropsBySeasonBAO = "GetTargetSubmittedCropsBySeasonBAO";
        private static string qn_GetTargetSubmittedCropsBySeasonDAO = "GetTargetSubmittedCropsBySeasonDAO";
        private static string qn_GetCrop = "GetCrop";
        private static string qn_GetCropBySeason = "GetCropBySeason";
        private static string qn_GetDistinctCrop = "GetDistinctCrop";
        private static string qn_GetCropCategory = "GetCropCategory";
        private static string qn_GetRainfallDistricts = "GetAlldistricts";
        private static string sp_GetRainfallDistricts = "usp_getdata_rainfall_report_data";
        private static string qn_GetLGDirectory = "GetLGDirectory";
        private static string qn_GetLGDirectoryDistrict = "GetLGDirectoryDistrict";
        private static string qn_GetLGDirectoryBlock = "GetLGDirectoryBlock";
        private static string qn_GetLGDirectoryPancht = "GetLGDirectoryPancht";
        private static string qn_GetLGDirectoryVillage = "GetLGDirectoryVillage";
        private static string qn_GetSource = "GetSource";
        private static string sp_misc_cntrlr = "usp_misc_cntrlr";
        private static string qn_GetCropCoverageAimPancht = "GetCropCoverageAimPancht";
        private static string qn_GetCropCoverageAimVillage = "GetCropCoverageAimVillage";
        private static string sp_GetCropCoverageAimPancht = "usp_crop_cntrlr_tgt_get";
        private static string qn_GetCropCoverageAimPanchtDelta = "GetCropCoverageAimPanchtDelta";
        private static string qn_GetCropCoverageAimDistrict = "GetCropCoverageAimDistrict";
        private static string qn_GetCropCoverageAimBlock = "GetCropCoverageAimBlock";
        private static string qn_GetCropCoverageActualPancht = "GetCropCoverageActualPancht";
        private static string qn_GetCropCoverageActualVillage = "GetCropCoverageActualVillage";
        private static string qn_GetCropCoverageActualPanchtDelta = "GetCropCoverageActualPanchtDelta";
        private static string sp_GetCropCoverageActualPanchtDelta = "usp_crop_cntrlr_cvrg_get";
        private static string qn_GetCropCoverageActualPanchtCurrDate = "GetCropCoverageActualPanchtCurrDate";
        private static string qn_GetCropCoverageActualVillageCurrDate = "GetCropCoverageActualVillageCurrDate";
        private static string qn_GetCropCoverageActualBlock = "GetCropCoverageActualBlock";
        private static string qn_GetCropCoverageActualDistrict = "GetCropCoverageActualDistrict";
        private static string sp_GetCropDamagePancht = "usp_crop_cntrlr_dmg_get";
        private static string qn_GetCropDamagePancht = "GetCropDamagePancht";
        private static string qn_GetCropDamagePanchtDelta = "GetCropDamagePanchtDelta";
        private static string qn_GetCropDamageBlock = "GetCropDamageBlock";
        private static string qn_GetCropDamageDistrict = "GetCropDamageDistrict";
        private static string qn_GetCropCoverageTargetDAODist = "GetCropCoverageTargetDAODist";
        private static string qn_GetCropCoverageTargetDAOBlk = "GetCropCoverageTargetDAOBlk";
        private static string qn_GetCropCoverageTargetDAOPanchyt = "GetCropCoverageTargetDAOPanchyt";
        private static string qn_GetCropCoverageTargetDAOVillage = "GetCropCoverageTargetDAOVillage";
        private static string qn_GetCropCoverageTargetDAOOfflineDist = "GetCropCoverageTargetDAOOfflineDist";
        private static string qn_GetCropCoverageTargetDAOOfflineBlk = "GetCropCoverageTargetDAOOfflineBlk";
        private static string qn_GetCropCoverageTargetDAOOfflinePanchyt = "GetCropCoverageTargetDAOOfflinePanchyt";
        private static string qn_GetCropCoverageTargetDAOOfflineVillage = "GetCropCoverageTargetDAOOfflineVillage";
        private static string qn_GetCropCoverageTargetDAODeltaDist = "GetCropCoverageTargetDAODeltaDist";
        private static string qn_GetCropCoverageTargetDAODeltaBlk = "GetCropCoverageTargetDAODeltaBlk";
        private static string qn_GetCropCoverageTargetDAODeltaPanchyt = "GetCropCoverageTargetDAODeltaPanchyt";
        private static string qn_GetCropCoverageActualDAODeltaDist = "GetCropCoverageActualDAODeltaDist";
        private static string qn_GetCropCoverageActualDAODeltaBlk = "GetCropCoverageActualDAODeltaBlk";
        private static string qn_GetCropCoverageActualDAODeltaPanchyt = "GetCropCoverageActualDAODeltaPanchyt";
        private static string qn_GetCropCoverageActualDAOOfflineDist = "GetCropCoverageActualDAOOfflineDist";
        private static string qn_GetCropCoverageActualDAOOfflineBlk = "GetCropCoverageActualDAOOfflineBlk";
        private static string qn_GetCropCoverageActualDAOOfflinePanchyt = "GetCropCoverageActualDAOOfflinePanchyt";
        private static string qn_GetCropCoverageActualDAOOfflineVillage = "GetCropCoverageActualDAOOfflineVillage";
        private static string qn_GetCropCoverageActualDAODist = "GetCropCoverageActualDAODist";
        private static string qn_GetCropCoverageActualDAOBlk = "GetCropCoverageActualDAOBlk";
        private static string qn_GetCropCoverageActualDAOPanchyt = "GetCropCoverageActualDAOPanchyt";
        private static string qn_GetCropCoverageActualDAOVillage = "GetCropCoverageActualDAOVillage";
        private static string qn_GetCropDamageDAODist = "GetCropDamageDAODist";
        private static string qn_GetCropDamageDAOBlk = "GetCropDamageDAOBlk";
        private static string qn_GetCropDamageDAOPanchyt = "GetCropDamageDAOPanchyt";
        private static string qn_GetCropDamageDAOOfflineDist = "GetCropDamageDAOOfflineDist";
        private static string qn_GetCropDamageDAOOfflineBlk = "GetCropDamageDAOOfflineBlk";
        private static string qn_GetCropDamageDAOOfflinePanchyt = "GetCropDamageDAOOfflinePanchyt";
        private static string qn_GetCropDamageDAODeltaDist = "GetCropDamageDAODeltaDist";
        private static string qn_GetCropDamageDAODeltaBlk = "GetCropDamageDAODeltaBlk";
        private static string qn_GetCropDamageDAODeltaPanchyt = "GetCropDamageDAODeltaPanchyt";
        private static string qn_GetCropDamageBAODeltaDist = "GetCropDamageBAODeltaDist";
        private static string qn_GetCropDamageBAODeltaBlk = "GetCropDamageBAODeltaBlk";
        private static string qn_GetCropDamageBAODeltaPanchyt = "GetCropDamageBAODeltaPanchyt";
        private static string qn_GetCropCoverageTargetBAODist = "GetCropCoverageTargetBAODist";
        private static string qn_GetCropCoverageTargetBAOBlk = "GetCropCoverageTargetBAOBlk";
        private static string qn_GetCropCoverageTargetBAOPanchyt = "GetCropCoverageTargetBAOPanchyt";
        private static string qn_GetCropCoverageTargetBAOVillage = "GetCropCoverageTargetBAOVillage";
        private static string qn_GetCropCoverageTargetBAOOfflineDist = "GetCropCoverageTargetBAOOfflineDist";
        private static string qn_GetCropCoverageTargetBAOOfflineBlk = "GetCropCoverageTargetBAOOfflineBlk";
        private static string qn_GetCropCoverageTargetBAOOfflinePanchyt = "GetCropCoverageTargetBAOOfflinePanchyt";
        private static string qn_GetCropCoverageTargetBAOOfflineVillage = "GetCropCoverageTargetBAOOfflineVillage";
        private static string qn_GetCropCoverageTargetBAODeltaDist = "GetCropCoverageTargetBAODeltaDist";
        private static string qn_GetCropCoverageTargetBAODeltaBlk = "GetCropCoverageTargetBAODeltaBlk";
        private static string qn_GetCropCoverageTargetBAODeltaPanchyt = "GetCropCoverageTargetBAODeltaPanchyt";
        private static string qn_GetCropCoverageActualBAODeltaBlk = "GetCropCoverageActualBAODeltaBlk";
        private static string qn_GetCropCoverageActualBAODeltaPanchyt = "GetCropCoverageActualBAODeltaPanchyt";
        private static string qn_GetCropCoverageActualBAODeltaVillage = "GetCropCoverageActualBAODeltaVillage";
        private static string qn_GetCropCoverageActualBAOOfflineDist = "GetCropCoverageActualBAOOfflineDist";
        private static string qn_GetCropCoverageActualBAOOfflineBlk = "GetCropCoverageActualBAOOfflineBlk";
        private static string qn_GetCropCoverageActualBAOOfflinePanchyt = "GetCropCoverageActualBAOOfflinePanchyt";
        private static string qn_GetCropCoverageActualBAOOfflineVillage = "GetCropCoverageActualBAOOfflineVillage";
        private static string qn_GetCropCoverageActualBAODist = "GetCropCoverageActualBAODist";
        private static string qn_GetCropCoverageActualBAOBlk = "GetCropCoverageActualBAOBlk";
        private static string qn_GetCropCoverageActualBAOPanchyt = "GetCropCoverageActualBAOPanchyt";
        private static string qn_GetCropCoverageActualBAOVillage = "GetCropCoverageActualBAOVillage";
        private static string qn_GetCropDamageBAODist = "GetCropDamageBAODist";
        private static string qn_GetCropDamageBAOBlk = "GetCropDamageBAOBlk";
        private static string qn_GetCropDamageBAOPanchyt = "GetCropDamageBAOPanchyt";
        private static string qn_GetCropDamageBAOOfflineDist = "GetCropDamageBAOOfflineDist";
        private static string qn_GetCropDamageBAOOfflineBlk = "GetCropDamageBAOOfflineBlk";
        private static string qn_GetCropDamageBAOOfflinePanchyt = "GetCropDamageBAOOfflinePanchyt";
        private static string qn_PostCropDimIns = "PostCropDimIns";
        private static string qn_PostCropDimSel = "PostCropDimSel";
        private static string qn_PostCropDimMerge = "PostCropDimMerge";
        private static string strSuccess = "Success";
        private static string api_source = "PostCropDim";
        private static string activity_type = "Crop Addition";
        private static string sp_activity_audit = "usp_insert_activity_audit";
        private static string sp_cvrg_aim_panchayat = "usp_crop_cvrg_aim_pnchyt";
        private static string sp_cvrg_aim_village = "usp_crop_cvrg_aim_village";
        private static string sp_cvrg_actual_panchayat = "usp_crop_cvrg_actual_pnchyt";
        private static string sp_cvrg_actual_village = "usp_crop_cvrg_actual_village";
        private static string qn_aggr_crop_coverage = "aggregate_crop_coverage_actual";
        private static string sp_crp_dmg_panchayat = "usp_crop_damage_pnchyt";
        private static string qn_aggr_crp_dmg = "aggregate_crop_damage";
        private static string sp_actl_blk_appr = "usp_crop_cvrg_actual_block_approval";
        private static string sp_actl_panch_appr = "usp_crop_cvrg_actual_pnchyt_approval";
        private static string sp_tgt_blk_appr = "usp_crop_cvrg_target_block_approval";
        private static string sp_tgt_panch_appr = "usp_crop_cvrg_target_pnchyt_approval";
        private static string sp_crp_dmg_blk_appr = "usp_crop_damage_block_approval";
        private static string sp_crp_dmg_panch_appr = "usp_crop_damage_pnchyt_approval";
        private static string sp_seed_perf = "usp_seed_performance";
        private static string sp_get_seed_perf = "usp_getdata_seedperformance";
        private static string qn_get_seed_perf = "GetbrbnSeedApplnData";
        private static string qn_GetSeedPerformanceAgriculture = "GetAllSeedPerformanceAgriculture";
        private static string qn_GetAgricultureBlkPnchyt = "GetAgricultureBlkPnchyt";
        private static string qn_GetAllColdStorage = "GetAllColdStorage";
        private static string sp_GetAllColdStorage = "usp_horticulture_report_data";
        private static string qn_GetAllColdStorageByDistrict = "GetColdStoragesByDistrict";
        private static string sp_crp_dmg_ctlr_get = "usp_crop_Damage_cntrlr_cvrg_get";
        private static string qn_GetBAONotSubmittedByBlock = "GetBAONotSubmittedByBlock";
        private static string qn_GetDAONotSubmittedByDistrict = "GetDAONotSubmittedByDistrict";
        private static string qn_PostHorticultureSel = "PostHorticultureSel";
        private static string qn_GetSpecificDamageReason = "GetSpecificDamageReason";
        private static string qn_GetSpecificDamageDetailsByReasonID = "GetSpecificDamageDetailsByReasonID";
        private static string sp_dmg_list_ctrlr = "usp_crop_damage_list_cntrlr";
        private static string qn_GetDamageReasonNames = "GetDamageReasonNames";
        private static string qn_GetDamageCropList = "GetDamageCropList";
        private static string qn_GetDamageConstantCrops = "GetDamageConstantCrops";
        private static string qn_GetEstdCropDamagePercnt = "GetEstdCropDamagePercnt";
        private static string qn_GetAllListsOfDamageReasons = "GetAllListsOfDamageReasons";
        private static string qn_GetHorticultureCrop = "GetHorticultureCrop";
        private static string qn_GetBAOCropDamageDetailsBlock = "GetBAOOnlineCropDamageDetailsBlock";
        private static string qn_GetDamageReasons = "GetDamageReasons";
        private static string qn_GetDAOApprovedCropDamageDetailsDistrict = "GetDAOApprovedCropDamageDetailsDistrict";
        private static string qn_GetBAOApprovedCropDamageDetailsBlock = "GetBAOApprovedCropDamageDetailsBlock";
        private static string qn_GetDAOCropDamageDetailsBlock = "GetDAOCropDamageDetailsBlock";
        private static string qn_GetACCropCvrgCoveredAreaPancht = "GetOnlineACCropDamageDetailsPancht";
        private static string qn_GetACCropCvrgCoveredAreaPanchtOffline = "GetOfflineACCropDamageDetailsPancht";
        private static string qn_GetBAOCropDamageDetailsBlockOffline = "GetBAOOfflineCropDamageDetailsBlock";
        private static string qn_GetOfflineDAOCropDamageDetailsDistrict = "GetOfflineDAOCropDamageDetailsDistrict";
        private static string qn_GetOnlineACViewSubmissionPancht = "GetOnlineACViewSubmissionPancht";
        private static string sp_GetCropBySeasonID = "usp_crop_cvrg_Target_report_data";
        private static string qn_GetCropBySeasonID = "GetCropBySeasonID";
        private static string qn_GetSeasonByYear = "GetSeasonByYear";
        private static string sp_GetRainfallReportData = "usp_getdata_rainfall_report_data";
        private static string sp_GetCropDamageReportData = "usp_crop_damage_report_data";
        private static string qn_GetOfflineACViewSubmissionPancht = "GetOfflineACViewSubmissionPancht";
        private static string qn_GetAllDistrict = "GetLGDirectoryAllDistrict";
        private static string sp_POSTHorticultureproductivity = "horticulture_productivity";
        private static string qn_PostCropDamageReason = "PostCropDamageReason";
        private static string qn_PostCropDamageName = "PostCropDamageName";
        private static string qn_PostDelDamageReasonList = "PostDelDamageReasonList";
        private static string sp_PostCropCoverageDamageDetails = "usp_Crop_Cvrg_Damage_dtls";
        private static string qn_PostUpdateDamageReasonStatus = "PostUpdateDamageReasonStatus";
        private static string sp_Crop_Cvrg_Damage_dtls = "usp_Crop_Cvrg_Damage_dtls";
        private static string sp_PostCropCvgDamagePancytApproval = "usp_Crop_Cvrg_Damage_Pnchyt_Approval";
        private static string sp_PostDamageDetails = "usp_Insert_crop_damage_list";
        private static string sp_InsertSeedUsedInput = "USP_Insert_Seed_Used_Input";
        private static string spInsertPlantIndent = "USP_Insert_plant_Indent";
        private static string spInsertSeedIndentInput = "USP_Insert_Seed_Indent";
        private static string qn_GetSeedusedIputViewSubmission = "GetSeedusedIputViewSubmission";
        private static string sp_GetSeedusedIputViewSubmission = "USP_Getdata_Seed_Used_Input";
        private static string qnGetSeedIndentAcIputViewSubmission = "GetSeeddemandACViewSubmission";
        private static string qnGetSeedIndentAcIputViewSubmissionOffline = "GetSeedDemandACViewSubmissionOffline";
        private static string qnGetSeedUsedVarietynameAC = "GetSeedUsedVarietynameAC";
        private static string spGetSeedIndentIputViewSubmission = "USP_Getdata_seed_indent";
        private static string sp_usp_getdata_PAIS_local_preferences = "usp_getdata_PAIS_local_preferences";
        private static string qnGetAllMarketByDistrict = "GetallMarketbydistrict";
        private static string qn_GetSeedUsedVarietyname = "GetSeedUsedVarietyname";
        private static string qn_GetSeedusedinputViewSubmissionOffline = "GetSeedusedinputViewSubmissionOffline";
        private static string qn_GetCropCoverageActualBAODeltaDist = "GetCropCoverageActualBAODeltaDist";
        private static string spGetDataPlantIndent = "USP_Getdata_plant_indent";
        private static string qnGetSeedDemandBHOViewSubmission = "GetSeeddemandBHOViewSubmission";
        private static string qnGetSeedDemandBHOViewSubmissionOffline = "GetSeedDemandBHOViewSubmissionOffline";
        private static string qnGetSeedUsedVarietyNameBHO = "GetSeedUsedVarietynameBHO";
        private static string qnGetFutureSeason = "Getfutureseason";
        private static string qnGetCropName = "GetCropName";
        private static string qnGetPlantName = "GetPlantName";
        private static string qnGetCropSeedVariety = "GetCropSeedVariety";
        private static string qnGetPlantSeedVariety = "GetPlantSeedVariety";
        private static string qnGetCropCategoryBySeason = "GetCropCategoryBySeason";

        /// <summary>
        /// Initializes a new instance of the <see cref="CropRepository"/> class.
        /// CropRepository.
        /// </summary>
        /// <param name="config">config.</param>
        /// <param name="blobconfig">blobconfig.</param>
        /// <param name="options">options.</param>
        public CropRepository(IConfiguration config, IOptions<BlobConfig> blobconfig, IOptions<DBSettings> options)
            : base(config)
        {
            this.options = options;
            SqlHelper.SetConnectionString(this.options.Value.ConnectionString);
            this.blobconfig = blobconfig;
            this.istDate = this.GetDateFromServer();
        }

        /// <summary>
        /// GetDateFromServer.
        /// </summary>
        /// <returns>Datatable values.</returns>
        public string GetDateFromServer()
        {
            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(this.istStrDate, null, SqlHelper.ExecutionType.Query);
            return dt.Rows[0][0].ToString();
        }

        /// <summary>
        /// GetSeason.
        /// </summary>
        /// <returns>List.</returns>
        public List<SeasonDim> GetSeason()
        {
            List<SeasonDim> list = new List<SeasonDim>();
            List<DbParameter> dbparams = new List<DbParameter>();
            dbparams.Add(new SqlParameter { ParameterName = "@query_name", Value = qnGetSeason, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@Season_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@District_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@Block_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@Panchayat_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@Crop_name", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@Crop_Category", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });

            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(sp_dim_cntrlr, dbparams, SqlHelper.ExecutionType.Procedure);
            if (dt != null && dt.Rows.Count > 0)
            {
                list = SqlHelper.ConvertDataTableToList<SeasonDim>(dt);
            }

            return list;
        }

        /// <summary>
        /// GetCoverageSubmittedCropsBySeason.
        /// </summary>
        /// <param name="districtId">districtId.</param>
        /// <returns>List.</returns>
        public List<CoverageSubmittedCropsSeason> GetCoverageSubmittedCropsBySeason(int districtId)
        {
            List<CoverageSubmittedCropsSeason> list = new List<CoverageSubmittedCropsSeason>();
            List<DbParameter> dbparams = new List<DbParameter>();
            dbparams.Add(new SqlParameter { ParameterName = "@query_name", Value = qn_GetCoverageSubmittedCropsBySeason, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });

            dbparams.Add(new SqlParameter { ParameterName = "@District_Id", Value = districtId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });

            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(sp_dim_cntrlr, dbparams, SqlHelper.ExecutionType.Procedure);
            if (dt != null && dt.Rows.Count > 0)
            {
                list = SqlHelper.ConvertDataTableToList<CoverageSubmittedCropsSeason>(dt);
            }

            return list;
        }

        /// <summary>
        /// GetTargetSubmittedCropsBySeason.
        /// </summary>
        /// <param name="districtId">districtId.</param>
        /// <returns>List.</returns>
        public List<CoverageSubmittedCropsSeason> GetTargetSubmittedCropsBySeason(int districtId)
        {
            List<CoverageSubmittedCropsSeason> list = new List<CoverageSubmittedCropsSeason>();
            List<DbParameter> dbparams = new List<DbParameter>();
            dbparams.Add(new SqlParameter { ParameterName = "@query_name", Value = qn_GetTargetSubmittedCropsBySeason, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });

            dbparams.Add(new SqlParameter { ParameterName = "@District_Id", Value = districtId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });

            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(sp_dim_cntrlr, dbparams, SqlHelper.ExecutionType.Procedure);
            if (dt != null && dt.Rows.Count > 0)
            {
                list = SqlHelper.ConvertDataTableToList<CoverageSubmittedCropsSeason>(dt);
            }

            return list;
        }

        /// <summary>
        /// GetCoverageSubmittedCropsBySeasonBAO.
        /// </summary>
        /// <param name="districtId">districtId.</param>
        /// <returns>List.</returns>
        public List<CoverageSubmittedCropsSeason> GetCoverageSubmittedCropsBySeasonBAO(int districtId)
        {
            List<CoverageSubmittedCropsSeason> list = new List<CoverageSubmittedCropsSeason>();
            List<DbParameter> dbparams = new List<DbParameter>();
            dbparams.Add(new SqlParameter { ParameterName = "@query_name", Value = qn_GetCoverageSubmittedCropsBySeasonBAO, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });

            dbparams.Add(new SqlParameter { ParameterName = "@District_Id", Value = districtId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });

            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(sp_dim_cntrlr, dbparams, SqlHelper.ExecutionType.Procedure);
            if (dt != null && dt.Rows.Count > 0)
            {
                list = SqlHelper.ConvertDataTableToList<CoverageSubmittedCropsSeason>(dt);
            }

            return list;
        }

        /// <summary>
        /// GetCoverageSubmittedCropsBySeasonDAO.
        /// </summary>
        /// <param name="districtId">districtId.</param>
        /// <returns>List.</returns>
        public List<CoverageSubmittedCropsSeason> GetCoverageSubmittedCropsBySeasonDAO(int districtId)
        {
            List<CoverageSubmittedCropsSeason> list = new List<CoverageSubmittedCropsSeason>();
            List<DbParameter> dbparams = new List<DbParameter>();
            dbparams.Add(new SqlParameter { ParameterName = "@query_name", Value = qn_GetCoverageSubmittedCropsBySeasonDAO, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });

            dbparams.Add(new SqlParameter { ParameterName = "@District_Id", Value = districtId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });

            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(sp_dim_cntrlr, dbparams, SqlHelper.ExecutionType.Procedure);
            if (dt != null && dt.Rows.Count > 0)
            {
                list = SqlHelper.ConvertDataTableToList<CoverageSubmittedCropsSeason>(dt);
            }

            return list;
        }

        /// <summary>
        /// GetTargetSubmittedCropsBySeasonBAO.
        /// </summary>
        /// <param name="districtId">districtId.</param>
        /// <returns>List.</returns>
        public List<CoverageSubmittedCropsSeason> GetTargetSubmittedCropsBySeasonBAO(int districtId)
        {
            List<CoverageSubmittedCropsSeason> list = new List<CoverageSubmittedCropsSeason>();
            List<DbParameter> dbparams = new List<DbParameter>();
            dbparams.Add(new SqlParameter { ParameterName = "@query_name", Value = qn_GetTargetSubmittedCropsBySeasonBAO, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });

            dbparams.Add(new SqlParameter { ParameterName = "@District_Id", Value = districtId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });

            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(sp_dim_cntrlr, dbparams, SqlHelper.ExecutionType.Procedure);
            if (dt != null && dt.Rows.Count > 0)
            {
                list = SqlHelper.ConvertDataTableToList<CoverageSubmittedCropsSeason>(dt);
            }

            return list;
        }

        /// <summary>
        /// GetTargetSubmittedCropsBySeasonDAO.
        /// </summary>
        /// <param name="districtId">districtId.</param>
        /// <returns>List.</returns>
        public List<CoverageSubmittedCropsSeason> GetTargetSubmittedCropsBySeasonDAO(int districtId)
        {
            List<CoverageSubmittedCropsSeason> list = new List<CoverageSubmittedCropsSeason>();
            List<DbParameter> dbparams = new List<DbParameter>();
            dbparams.Add(new SqlParameter { ParameterName = "@query_name", Value = qn_GetTargetSubmittedCropsBySeasonDAO, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });

            dbparams.Add(new SqlParameter { ParameterName = "@District_Id", Value = districtId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });

            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(sp_dim_cntrlr, dbparams, SqlHelper.ExecutionType.Procedure);
            if (dt != null && dt.Rows.Count > 0)
            {
                list = SqlHelper.ConvertDataTableToList<CoverageSubmittedCropsSeason>(dt);
            }

            return list;
        }

        /// <summary>
        /// GetCrop.
        /// </summary>
        /// <param name="seasonName">seasonName.</param>
        /// <param name="districtId">districtId.</param>
        /// <returns>CropSeasonEntity List.</returns>
        public List<CropSeasonEntity> GetCrop(string seasonName, string districtId)
        {
            List<DbParameter> dbparams = new List<DbParameter>();

            List<CropSeasonEntity> list = new List<CropSeasonEntity>();
            DataTable dt;

            if (seasonName != string.Empty && seasonName != null && Convert.ToInt32(seasonName) != 0)
            {
                dbparams.Add(new SqlParameter { ParameterName = "@query_name", Value = qn_GetCropBySeason, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparams.Add(new SqlParameter { ParameterName = "@Season_Id", Value = seasonName, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparams.Add(new SqlParameter { ParameterName = "@District_Id", Value = districtId, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparams.Add(new SqlParameter { ParameterName = "@Block_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparams.Add(new SqlParameter { ParameterName = "@Panchayat_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparams.Add(new SqlParameter { ParameterName = "@Crop_name", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparams.Add(new SqlParameter { ParameterName = "@Crop_Category", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            }
            else
            {
                dbparams.Add(new SqlParameter { ParameterName = "@query_name", Value = qn_GetCrop, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparams.Add(new SqlParameter { ParameterName = "@Season_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparams.Add(new SqlParameter { ParameterName = "@District_Id", Value = districtId, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparams.Add(new SqlParameter { ParameterName = "@Block_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparams.Add(new SqlParameter { ParameterName = "@Panchayat_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparams.Add(new SqlParameter { ParameterName = "@Crop_name", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparams.Add(new SqlParameter { ParameterName = "@Crop_Category", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            }

            dt = SqlHelper.ExecuteSelect<SqlConnection>(sp_dim_cntrlr, dbparams, SqlHelper.ExecutionType.Procedure);

            if (dt != null && dt.Rows.Count > 0)
            {
                list = SqlHelper.ConvertDataTableToList<CropSeasonEntity>(dt);
            }

            return list;
        }

        /// <summary>
        /// GetDistinctCrop.
        /// </summary>
        /// <param name="districtId">districtId.</param>
        /// <returns>CropSeasonEntity List Values.</returns>
        public List<CropSeasonEntity> GetDistinctCrop(string districtId)
        {
            List<DbParameter> dbparams = new List<DbParameter>();

            List<CropSeasonEntity> list = new List<CropSeasonEntity>();

            dbparams.Add(new SqlParameter { ParameterName = "@query_name", Value = qn_GetDistinctCrop, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@Season_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@District_Id", Value = districtId, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@Block_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@Panchayat_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@Crop_name", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@Crop_Category", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });

            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(sp_dim_cntrlr, dbparams, SqlHelper.ExecutionType.Procedure);

            if (dt != null && dt.Rows.Count > 0)
            {
                list = SqlHelper.ConvertDataTableToList<CropSeasonEntity>(dt);
            }

            return list;
        }

        /// <summary>
        /// GetCropCategory.
        /// </summary>
        /// <param name="crop_Type">crop_Type.</param>
        /// <returns>CropCategoryEntity List.</returns>
        public List<CropCategoryEntity> GetCropCategory(string crop_Type)
        {
            List<CropCategoryEntity> list = new List<CropCategoryEntity>();
            List<DbParameter> dbparams = new List<DbParameter>();
            dbparams.Add(new SqlParameter { ParameterName = "@query_name", Value = qn_GetCropCategory, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@Season_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@District_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@Block_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@Panchayat_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@Crop_name", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@Crop_Category", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@Crop_Type", Value = string.IsNullOrEmpty(crop_Type) ? DBNull.Value : (object)crop_Type, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });

            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(sp_dim_cntrlr, dbparams, SqlHelper.ExecutionType.Procedure);

            if (dt != null && dt.Rows.Count > 0)
            {
                list = SqlHelper.ConvertDataTableToList<CropCategoryEntity>(dt);
            }

            return list;
        }

        /// <summary>
        /// GetRainfallDistricts.
        /// </summary>
        /// <returns>DistrictList List.</returns>
        public List<DistrictList> GetRainfallDistricts()
        {
            List<DistrictList> districtList = new List<DistrictList>();
            DataSet dataSet = new DataSet();
            List<DbParameter> parameters = new List<DbParameter>();
            parameters.Add(new SqlParameter { ParameterName = "@month_year", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@district_name", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@query_name", Value = qn_GetRainfallDistricts, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dataSet = SqlHelper.ExecuteDataSet<SqlConnection>(sp_GetRainfallDistricts, parameters, SqlHelper.ExecutionType.Procedure);

            if (dataSet != null && dataSet.Tables[0]?.Rows.Count > 0)
            {
                districtList = SqlHelper.ConvertDataTableToList<DistrictList>(dataSet.Tables[0]);
            }

            return districtList;
        }

        /// <summary>
        /// GetLGDirectory.
        /// </summary>
        /// <returns>LgDirectoryPanchayatDim List.</returns>
        public List<LgDirectoryPanchayatDim> GetLGDirectory()
        {
            List<LgDirectoryPanchayatDim> list = new List<LgDirectoryPanchayatDim>();
            List<DbParameter> dbparams = new List<DbParameter>();
            dbparams.Add(new SqlParameter { ParameterName = "@query_name", Value = qn_GetLGDirectory, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@Season_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@District_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@Block_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@Panchayat_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@Crop_name", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@Crop_Category", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });

            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(sp_dim_cntrlr, dbparams, SqlHelper.ExecutionType.Procedure);

            if (dt != null && dt.Rows.Count > 0)
            {
                list = SqlHelper.ConvertDataTableToList<LgDirectoryPanchayatDim>(dt);
            }

            return list;
        }

        /// <summary>
        /// GetLGDirectoryDistrict.
        /// </summary>
        /// <param name="districtId">districtId.</param>
        /// <returns>LgDirectoryPanchayatDim List.</returns>
        public List<LgDirectoryPanchayatDim> GetLGDirectoryDistrict(string districtId)
        {
            List<LgDirectoryPanchayatDim> list = new List<LgDirectoryPanchayatDim>();
            List<DbParameter> dbparams = new List<DbParameter>();
            dbparams.Add(new SqlParameter { ParameterName = "@query_name", Value = qn_GetLGDirectoryDistrict, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@Season_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });

            dbparams.Add(new SqlParameter { ParameterName = "@Block_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@Panchayat_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@Crop_name", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@Crop_Category", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });

            dbparams.Add(new SqlParameter { ParameterName = "@District_Id", Value = districtId, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });

            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(sp_dim_cntrlr, dbparams, SqlHelper.ExecutionType.Procedure);

            if (dt != null && dt.Rows.Count > 0)
            {
                list = SqlHelper.ConvertDataTableToList<LgDirectoryPanchayatDim>(dt);
            }

            return list;
        }

        /// <summary>
        /// GetLGDirectoryBlock.
        /// </summary>
        /// <param name="blockId">blockId.</param>
        /// <returns>LgDirectoryPanchayatDim List.</returns>
        public List<LgDirectoryPanchayatDim> GetLGDirectoryBlock(string blockId)
        {
            List<LgDirectoryPanchayatDim> list = new List<LgDirectoryPanchayatDim>();
            List<DbParameter> dbparams = new List<DbParameter>();
            dbparams.Add(new SqlParameter { ParameterName = "@query_name", Value = qn_GetLGDirectoryBlock, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@Season_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });

            dbparams.Add(new SqlParameter { ParameterName = "@Block_Id", Value = blockId, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@Panchayat_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@Crop_name", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@Crop_Category", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });

            dbparams.Add(new SqlParameter { ParameterName = "@District_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });

            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(sp_dim_cntrlr, dbparams, SqlHelper.ExecutionType.Procedure);

            if (dt != null && dt.Rows.Count > 0)
            {
                list = SqlHelper.ConvertDataTableToList<LgDirectoryPanchayatDim>(dt);
            }

            return list;
        }

        /// <summary>
        /// GetLGDirectoryPancht.
        /// </summary>
        /// <param name="panchayatId">panchayatId.</param>
        /// <returns>LgDirectoryPanchayatDim List.</returns>
        public List<LgDirectoryPanchayatDim> GetLGDirectoryPancht(string panchayatId)
        {
            List<LgDirectoryPanchayatDim> list = new List<LgDirectoryPanchayatDim>();
            List<DbParameter> dbparams = new List<DbParameter>();
            dbparams.Add(new SqlParameter { ParameterName = "@query_name", Value = qn_GetLGDirectoryPancht, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@Season_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });

            dbparams.Add(new SqlParameter { ParameterName = "@Block_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@Panchayat_Id", Value = panchayatId, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@Crop_name", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@Crop_Category", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });

            dbparams.Add(new SqlParameter { ParameterName = "@District_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });

            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(sp_dim_cntrlr, dbparams, SqlHelper.ExecutionType.Procedure);
            if (dt != null && dt.Rows.Count > 0)
            {
                list = SqlHelper.ConvertDataTableToList<LgDirectoryPanchayatDim>(dt);
            }

            return list;
        }

        /// <summary>
        /// GetLGDirectoryVillage.
        /// </summary>
        /// <param name="panchayatId">panchayatId.</param>
        /// <returns>LgDirectoryVillageDim List.</returns>
        public List<LgDirectoryVillageDim> GetLGDirectoryVillage(int panchayatId)
        {
            List<LgDirectoryVillageDim> list = new List<LgDirectoryVillageDim>();
            List<DbParameter> dbparams = new List<DbParameter>();
            dbparams.Add(new SqlParameter { ParameterName = "@query_name", Value = qn_GetLGDirectoryVillage, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@Panchayat_Id", Value = panchayatId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@user_id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@Season_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@Block_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@Crop_name", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@Crop_Category", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@District_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(sp_dim_cntrlr, dbparams, SqlHelper.ExecutionType.Procedure);
            if (dt != null && dt.Rows.Count > 0)
            {
                list = SqlHelper.ConvertDataTableToList<LgDirectoryVillageDim>(dt);
            }

            return list;
        }

        /// <summary>
        /// GetLGDirectoryVillageByUserId.
        /// </summary>
        /// <param name="userId">userId.</param>
        /// <param name="panchayatId">panchayatId.</param>
        /// <returns>LgDirectoryVillageDim List.</returns>
        public List<LgDirectoryVillageDim> GetLGDirectoryVillageByUserId(int userId, int panchayatId)
        {
            List<LgDirectoryVillageDim> list = new List<LgDirectoryVillageDim>();
            List<DbParameter> dbparams = new List<DbParameter>();
            dbparams.Add(new SqlParameter { ParameterName = "@query_name", Value = qn_GetLGDirectoryVillage, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@Panchayat_Id", Value = panchayatId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@user_id", Value = userId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@Season_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@Block_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@Crop_name", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@Crop_Category", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@District_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(sp_dim_cntrlr, dbparams, SqlHelper.ExecutionType.Procedure);
            if (dt != null && dt.Rows.Count > 0)
            {
                list = SqlHelper.ConvertDataTableToList<LgDirectoryVillageDim>(dt);
            }

            return list;
        }

        /// <summary>
        /// GetSource.
        /// </summary>
        /// <param name="attributeName">attributeName.</param>
        /// <returns>MobileAttributeConfig List.</returns>
        public List<MobileAttributeConfig> GetSource(string attributeName)
        {
            List<MobileAttributeConfig> list = new List<MobileAttributeConfig>();

            List<DbParameter> sqlParams = new List<DbParameter>();
            sqlParams.Add(new SqlParameter { ParameterName = "@query_name", Value = qn_GetSource, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });

            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(sp_misc_cntrlr, sqlParams, SqlHelper.ExecutionType.Procedure);

            if (dt != null && dt.Rows.Count > 0)
            {
                list = SqlHelper.ConvertDataTableToList<MobileAttributeConfig>(dt);
            }

            return list;
        }

        /// <summary>
        /// GetCropCoverageAimPancht.
        /// </summary>
        /// <param name="seasonId">seasonId.</param>
        /// <param name="panchayatId">panchayatId.</param>
        /// <returns>CropCoverageAimPancht List Values.</returns>
        public List<CropCoverageAimPancht> GetCropCoverageAimPancht(string seasonId, string panchayatId)
        {
            List<CropCoverageAimPancht> list = new List<CropCoverageAimPancht>();

            List<DbParameter> dbparamsCropInfo = new List<DbParameter>();
            dbparamsCropInfo.Add(new SqlParameter { ParameterName = "@query_name", Value = qn_GetCropCoverageAimPancht, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfo.Add(new SqlParameter { ParameterName = "@District_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfo.Add(new SqlParameter { ParameterName = "@Block_id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfo.Add(new SqlParameter { ParameterName = "@Panchayat_Id", Value = string.IsNullOrEmpty(panchayatId) ? DBNull.Value : (object)panchayatId, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfo.Add(new SqlParameter { ParameterName = "@Season_Id", Value = string.IsNullOrEmpty(seasonId) ? DBNull.Value : (object)seasonId, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfo.Add(new SqlParameter { ParameterName = "@Crop_id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfo.Add(new SqlParameter { ParameterName = "@Last_Refresh_Ts", Value = DateTime.Now, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });

            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(sp_GetCropCoverageAimPancht, dbparamsCropInfo, SqlHelper.ExecutionType.Procedure);
            if (dt != null && dt.Rows.Count > 0)
            {
                CropCoverageAimPancht cropCoverageAimPancht = SqlHelper.ConvertDataTableToList<CropCoverageAimPancht>(dt)[0];
                cropCoverageAimPancht.CropValues = new List<CropTgtEntity>();
                cropCoverageAimPancht.CropValues = SqlHelper.ConvertDataTableToList<CropTgtEntity>(dt);
                list.Add(cropCoverageAimPancht);
            }

            return list;
        }

        /// <summary>
        /// GetCropCoverageAimVillage.
        /// </summary>
        /// <param name="seasonId">seasonId.</param>
        /// <param name="panchayatId">panchayatId.</param>
        /// <param name="villageId">villageId.</param>
        /// <returns>CropCoverageAimVillage List Values.</returns>
        public List<CropCoverageAimVillage> GetCropCoverageAimVillage(string seasonId, string panchayatId, string villageId)
        {
            List<CropCoverageAimVillage> list = new List<CropCoverageAimVillage>();

            List<DbParameter> dbparamsCropInfo = new List<DbParameter>();
            dbparamsCropInfo.Add(new SqlParameter { ParameterName = "@query_name", Value = qn_GetCropCoverageAimVillage, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfo.Add(new SqlParameter { ParameterName = "@District_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfo.Add(new SqlParameter { ParameterName = "@Block_id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfo.Add(new SqlParameter { ParameterName = "@Panchayat_Id", Value = string.IsNullOrEmpty(panchayatId) ? DBNull.Value : (object)panchayatId, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfo.Add(new SqlParameter { ParameterName = "@village_id", Value = string.IsNullOrEmpty(villageId) ? DBNull.Value : (object)villageId, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfo.Add(new SqlParameter { ParameterName = "@Season_Id", Value = string.IsNullOrEmpty(seasonId) ? DBNull.Value : (object)seasonId, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfo.Add(new SqlParameter { ParameterName = "@Crop_id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfo.Add(new SqlParameter { ParameterName = "@Last_Refresh_Ts", Value = DateTime.Now, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });

            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(sp_GetCropCoverageAimPancht, dbparamsCropInfo, SqlHelper.ExecutionType.Procedure);
            if (dt != null && dt.Rows.Count > 0)
            {
                CropCoverageAimVillage cropCoverageAimVillage = SqlHelper.ConvertDataTableToList<CropCoverageAimVillage>(dt)[0];
                cropCoverageAimVillage.CropValues = new List<CropTgtEntity>();
                cropCoverageAimVillage.CropValues = SqlHelper.ConvertDataTableToList<CropTgtEntity>(dt);
                list.Add(cropCoverageAimVillage);
            }

            return list;
        }

        /// <summary>
        /// GetCropCoverageAimPanchtDelta.
        /// </summary>
        /// <param name="seasonId">seasonId.</param>
        /// <param name="panchayatId">panchayatId.</param>
        /// <param name="lastrefreshedTime">.lastrefreshedTime.</param>
        /// <returns>CropCoverageAimPancht List.</returns>
        public List<CropCoverageAimPancht> GetCropCoverageAimPanchtDelta(string seasonId, string panchayatId, DateTime lastrefreshedTime)
        {
            DateTime lastrefreshtime = Convert.ToDateTime(lastrefreshedTime);

            List<CropCoverageAimPancht> list = new List<CropCoverageAimPancht>();

            List<DbParameter> dbparamsCropInfo = new List<DbParameter>();
            dbparamsCropInfo.Add(new SqlParameter { ParameterName = "@query_name", Value = qn_GetCropCoverageAimPanchtDelta, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfo.Add(new SqlParameter { ParameterName = "@District_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfo.Add(new SqlParameter { ParameterName = "@Block_id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfo.Add(new SqlParameter { ParameterName = "@Panchayat_Id", Value = string.IsNullOrEmpty(panchayatId) ? DBNull.Value : (object)panchayatId, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfo.Add(new SqlParameter { ParameterName = "@Season_Id", Value = string.IsNullOrEmpty(seasonId) ? DBNull.Value : (object)seasonId, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfo.Add(new SqlParameter { ParameterName = "@Crop_id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfo.Add(new SqlParameter { ParameterName = "@Last_Refresh_Ts", Value = lastrefreshtime, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });

            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(sp_GetCropCoverageAimPancht, dbparamsCropInfo, SqlHelper.ExecutionType.Procedure);
            if (dt != null && dt.Rows.Count > 0)
            {
                CropCoverageAimPancht cropCoverageAimPancht = SqlHelper.ConvertDataTableToList<CropCoverageAimPancht>(dt)[0];
                cropCoverageAimPancht.CropValues = new List<CropTgtEntity>();
                cropCoverageAimPancht.CropValues = SqlHelper.ConvertDataTableToList<CropTgtEntity>(dt);
                list.Add(cropCoverageAimPancht);
            }

            return list;
        }

        /// <summary>
        /// GetCropCoverageAimBlock.
        /// </summary>
        /// <param name="blockId">blockId.</param>
        /// <returns>CropCoverageAimBlock.</returns>
        public List<CropCoverageAimBlock> GetCropCoverageAimBlock(string blockId)
        {
            List<CropCoverageAimBlock> list = new List<CropCoverageAimBlock>();

            List<DbParameter> dbparamsCropInfo = new List<DbParameter>();
            dbparamsCropInfo.Add(new SqlParameter { ParameterName = "@query_name", Value = qn_GetCropCoverageAimBlock, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfo.Add(new SqlParameter { ParameterName = "@District_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfo.Add(new SqlParameter { ParameterName = "@Block_id", Value = string.IsNullOrEmpty(blockId) ? DBNull.Value : (object)blockId, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfo.Add(new SqlParameter { ParameterName = "@Panchayat_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfo.Add(new SqlParameter { ParameterName = "@Season_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfo.Add(new SqlParameter { ParameterName = "@Crop_id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfo.Add(new SqlParameter { ParameterName = "@Last_Refresh_Ts", Value = DBNull.Value, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });

            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(sp_GetCropCoverageAimPancht, dbparamsCropInfo, SqlHelper.ExecutionType.Procedure);
            if (dt != null && dt.Rows.Count > 0)
            {
                CropCoverageAimBlock cropCoverageAimBlock = SqlHelper.ConvertDataTableToList<CropCoverageAimBlock>(dt)[0];
                cropCoverageAimBlock.CropValues = new List<CropTgtEntity>();
                cropCoverageAimBlock.CropValues = SqlHelper.ConvertDataTableToList<CropTgtEntity>(dt);
                list.Add(cropCoverageAimBlock);
            }

            return list;
        }

        /// <summary>
        /// GetCropCoverageAimDistrict.
        /// </summary>
        /// <param name="districtId">districtId.</param>
        /// <returns>CropCoverageAimDistrict List DT details.</returns>
        public List<CropCoverageAimDistrict> GetCropCoverageAimDistrict(string districtId)
        {
            List<CropCoverageAimDistrict> list = new List<CropCoverageAimDistrict>();

            List<DbParameter> dbparamsCropInfo = new List<DbParameter>();
            dbparamsCropInfo.Add(new SqlParameter { ParameterName = "@query_name", Value = qn_GetCropCoverageAimDistrict, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfo.Add(new SqlParameter { ParameterName = "@District_Id", Value = string.IsNullOrEmpty(districtId) ? DBNull.Value : (object)districtId, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfo.Add(new SqlParameter { ParameterName = "@Block_id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfo.Add(new SqlParameter { ParameterName = "@Panchayat_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfo.Add(new SqlParameter { ParameterName = "@Season_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfo.Add(new SqlParameter { ParameterName = "@Crop_id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfo.Add(new SqlParameter { ParameterName = "@Last_Refresh_Ts", Value = DBNull.Value, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });

            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(sp_GetCropCoverageAimPancht, dbparamsCropInfo, SqlHelper.ExecutionType.Procedure);
            if (dt != null && dt.Rows.Count > 0)
            {
                CropCoverageAimDistrict cropCoverageAimDistrict = SqlHelper.ConvertDataTableToList<CropCoverageAimDistrict>(dt)[0];
                cropCoverageAimDistrict.CropValues = new List<CropTgtEntity>();

                cropCoverageAimDistrict.CropValues = SqlHelper.ConvertDataTableToList<CropTgtEntity>(dt);
                list.Add(cropCoverageAimDistrict);
            }

            return list;
        }

        /// <summary>
        /// GetCropCoverageActualPancht.
        /// </summary>
        /// <param name="seasonId">seasonId.</param>
        /// <param name="panchayatId">panchayatId.</param>
        /// <returns>CropCoverageActualPancht List.</returns>
        public List<CropCoverageActualVillage> GetCropCoverageActualPancht(string seasonId, string panchayatId)
        {
            List<CropCoverageActualVillage> list = new List<CropCoverageActualVillage>();

            List<DbParameter> dbparamsCropInfo = new List<DbParameter>();
            dbparamsCropInfo.Add(new SqlParameter { ParameterName = "@query_name", Value = qn_GetCropCoverageActualPancht, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfo.Add(new SqlParameter { ParameterName = "@District_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfo.Add(new SqlParameter { ParameterName = "@Block_id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfo.Add(new SqlParameter { ParameterName = "@Panchayat_Id", Value = string.IsNullOrEmpty(panchayatId) ? DBNull.Value : (object)panchayatId, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfo.Add(new SqlParameter { ParameterName = "@Season_Id", Value = string.IsNullOrEmpty(seasonId) ? DBNull.Value : (object)seasonId, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfo.Add(new SqlParameter { ParameterName = "@Crop_id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfo.Add(new SqlParameter { ParameterName = "@Last_Refresh_Ts", Value = DBNull.Value, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
            dbparamsCropInfo.Add(new SqlParameter { ParameterName = "@Ist_Date", Value = DBNull.Value, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });

            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(sp_GetCropCoverageActualPanchtDelta, dbparamsCropInfo, SqlHelper.ExecutionType.Procedure);

            if (dt != null && dt.Rows.Count > 0)
            {
                CropCoverageActualVillage cropCoverageActualPancht = SqlHelper.ConvertDataTableToList<CropCoverageActualVillage>(dt)[0];
                cropCoverageActualPancht.CropList = new List<CropCvrgEntity>();
                cropCoverageActualPancht.CropList = SqlHelper.ConvertDataTableToList<CropCvrgEntity>(dt);
                list.Add(cropCoverageActualPancht);
            }

            return list;
        }

        /// <summary>
        /// GetCropCoverageActualVillage.
        /// </summary>
        /// <param name="seasonId">seasonId.</param>
        /// <param name="panchayatId">panchayatId.</param>
        /// <param name="villageId">villageId.</param>
        /// <returns>CropCoverageActualVillage List.</returns>
        public List<CropCoverageActualVillage> GetCropCoverageActualVillage(string seasonId, string panchayatId, string villageId)
        {
            List<CropCoverageActualVillage> list = new List<CropCoverageActualVillage>();

            List<DbParameter> dbparamsCropInfo = new List<DbParameter>();
            dbparamsCropInfo.Add(new SqlParameter { ParameterName = "@query_name", Value = qn_GetCropCoverageActualVillage, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfo.Add(new SqlParameter { ParameterName = "@District_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfo.Add(new SqlParameter { ParameterName = "@Block_id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfo.Add(new SqlParameter { ParameterName = "@Panchayat_Id", Value = string.IsNullOrEmpty(panchayatId) ? DBNull.Value : (object)panchayatId, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfo.Add(new SqlParameter { ParameterName = "@village_id", Value = string.IsNullOrEmpty(villageId) ? DBNull.Value : (object)villageId, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfo.Add(new SqlParameter { ParameterName = "@Season_Id", Value = string.IsNullOrEmpty(seasonId) ? DBNull.Value : (object)seasonId, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfo.Add(new SqlParameter { ParameterName = "@Crop_id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfo.Add(new SqlParameter { ParameterName = "@Last_Refresh_Ts", Value = DBNull.Value, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
            dbparamsCropInfo.Add(new SqlParameter { ParameterName = "@Ist_Date", Value = DBNull.Value, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });

            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(sp_GetCropCoverageActualPanchtDelta, dbparamsCropInfo, SqlHelper.ExecutionType.Procedure);

            if (dt != null && dt.Rows.Count > 0)
            {
                CropCoverageActualVillage cropCoverageActualVillage = SqlHelper.ConvertDataTableToList<CropCoverageActualVillage>(dt)[0];
                cropCoverageActualVillage.CropList = new List<CropCvrgEntity>();
                cropCoverageActualVillage.CropList = SqlHelper.ConvertDataTableToList<CropCvrgEntity>(dt);
                list.Add(cropCoverageActualVillage);
            }

            return list;
        }

        /// <summary>
        /// GetCropCoverageActualPanchtDelta.
        /// </summary>
        /// <param name="seasonId">seasonId.</param>
        /// <param name="panchayatId">panchayatId.</param>
        /// <param name="lastrefreshedTime">lastrefreshedTime.</param>
        /// <returns>CropCoverageActualPancht List.</returns>
        public List<CropCoverageActualPancht> GetCropCoverageActualPanchtDelta(string seasonId, string panchayatId, DateTime lastrefreshedTime)
        {
            DateTime lastrefreshtime = Convert.ToDateTime(lastrefreshedTime);

            List<CropCoverageActualPancht> list = new List<CropCoverageActualPancht>();

            List<DbParameter> dbparamsCropInfo = new List<DbParameter>();
            dbparamsCropInfo.Add(new SqlParameter { ParameterName = "@query_name", Value = qn_GetCropCoverageActualPanchtDelta, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfo.Add(new SqlParameter { ParameterName = "@District_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfo.Add(new SqlParameter { ParameterName = "@Block_id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfo.Add(new SqlParameter { ParameterName = "@Panchayat_Id", Value = string.IsNullOrEmpty(panchayatId) ? DBNull.Value : (object)panchayatId, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfo.Add(new SqlParameter { ParameterName = "@Season_Id", Value = string.IsNullOrEmpty(seasonId) ? DBNull.Value : (object)seasonId, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfo.Add(new SqlParameter { ParameterName = "@Crop_id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfo.Add(new SqlParameter { ParameterName = "@Last_Refresh_Ts", Value = lastrefreshtime, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
            dbparamsCropInfo.Add(new SqlParameter { ParameterName = "@Ist_Date", Value = DBNull.Value, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });

            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(sp_GetCropCoverageActualPanchtDelta, dbparamsCropInfo, SqlHelper.ExecutionType.Procedure);

            if (dt != null && dt.Rows.Count > 0)
            {
                CropCoverageActualPancht cropCoverageActualPancht = SqlHelper.ConvertDataTableToList<CropCoverageActualPancht>(dt)[0];
                cropCoverageActualPancht.CropList = new List<CropCvrgEntity>();
                cropCoverageActualPancht.CropList = SqlHelper.ConvertDataTableToList<CropCvrgEntity>(dt);
                list.Add(cropCoverageActualPancht);
            }

            return list;
        }

        /// <summary>
        /// GetCropCoverageActualPanchtCurrDate.
        /// </summary>
        /// <param name="seasonId">seasonId.</param>
        /// <param name="panchayatId">panchayatId.</param>
        /// <returns>CropCoverageActualPancht List with Datatable results.</returns>
        public List<CropCoverageActualVillage> GetCropCoverageActualPanchtCurrDate(string seasonId, string panchayatId)
        {
            List<CropCoverageActualVillage> list = new List<CropCoverageActualVillage>();

            List<DbParameter> dbparamsCropInfo = new List<DbParameter>();
            dbparamsCropInfo.Add(new SqlParameter { ParameterName = "@query_name", Value = qn_GetCropCoverageActualPanchtCurrDate, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfo.Add(new SqlParameter { ParameterName = "@District_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfo.Add(new SqlParameter { ParameterName = "@Block_id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfo.Add(new SqlParameter { ParameterName = "@Panchayat_Id", Value = string.IsNullOrEmpty(panchayatId) ? DBNull.Value : (object)panchayatId, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfo.Add(new SqlParameter { ParameterName = "@Season_Id", Value = string.IsNullOrEmpty(seasonId) ? DBNull.Value : (object)seasonId, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfo.Add(new SqlParameter { ParameterName = "@Crop_id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfo.Add(new SqlParameter { ParameterName = "@Last_Refresh_Ts", Value = DBNull.Value, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
            dbparamsCropInfo.Add(new SqlParameter { ParameterName = "@Ist_Date", Value = this.istDate, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });

            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(sp_GetCropCoverageActualPanchtDelta, dbparamsCropInfo, SqlHelper.ExecutionType.Procedure);

            if (dt != null && dt.Rows.Count > 0)
            {
                CropCoverageActualVillage cropCoverageActualPancht = SqlHelper.ConvertDataTableToList<CropCoverageActualVillage>(dt)[0];
                cropCoverageActualPancht.CropList = new List<CropCvrgEntity>();
                cropCoverageActualPancht.CropList = SqlHelper.ConvertDataTableToList<CropCvrgEntity>(dt);
                list.Add(cropCoverageActualPancht);
            }

            return list;
        }

        /// <summary>
        /// GetCropCoverageActualVillageCurrDate.
        /// </summary>
        /// <param name="seasonId">seasonId.</param>
        /// <param name="panchayatId">panchayatId.</param>
        /// <param name="villageId">villageId.</param>
        /// <returns>GetCropCoverageActualVillageCurrDate List.</returns>
        public List<CropCoverageActualVillage> GetCropCoverageActualVillageCurrDate(string seasonId, string panchayatId, string villageId)
        {
            List<CropCoverageActualVillage> list = new List<CropCoverageActualVillage>();

            List<DbParameter> dbparamsCropInfo = new List<DbParameter>();
            dbparamsCropInfo.Add(new SqlParameter { ParameterName = "@query_name", Value = qn_GetCropCoverageActualVillageCurrDate, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfo.Add(new SqlParameter { ParameterName = "@District_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfo.Add(new SqlParameter { ParameterName = "@Block_id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfo.Add(new SqlParameter { ParameterName = "@Panchayat_Id", Value = string.IsNullOrEmpty(panchayatId) ? DBNull.Value : (object)panchayatId, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfo.Add(new SqlParameter { ParameterName = "@village_id", Value = string.IsNullOrEmpty(villageId) ? DBNull.Value : (object)villageId, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfo.Add(new SqlParameter { ParameterName = "@Season_Id", Value = string.IsNullOrEmpty(seasonId) ? DBNull.Value : (object)seasonId, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfo.Add(new SqlParameter { ParameterName = "@Crop_id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfo.Add(new SqlParameter { ParameterName = "@Last_Refresh_Ts", Value = DBNull.Value, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
            dbparamsCropInfo.Add(new SqlParameter { ParameterName = "@Ist_Date", Value = this.istDate, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });

            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(sp_GetCropCoverageActualPanchtDelta, dbparamsCropInfo, SqlHelper.ExecutionType.Procedure);

            if (dt != null && dt.Rows.Count > 0)
            {
                CropCoverageActualVillage cropCoverageActualVillage = SqlHelper.ConvertDataTableToList<CropCoverageActualVillage>(dt)[0];
                cropCoverageActualVillage.CropList = new List<CropCvrgEntity>();
                cropCoverageActualVillage.CropList = SqlHelper.ConvertDataTableToList<CropCvrgEntity>(dt);
                list.Add(cropCoverageActualVillage);
            }

            return list;
        }

        /// <summary>
        /// GetCropCoverageActualBlock.
        /// </summary>
        /// <param name="blockId">blockId.</param>
        /// <returns>CropCoverageActualBlock List.</returns>
        public List<CropCoverageActualBlock> GetCropCoverageActualBlock(string blockId)
        {
            List<CropCoverageActualBlock> list = new List<CropCoverageActualBlock>();

            List<DbParameter> dbparamsCropInfo = new List<DbParameter>();
            dbparamsCropInfo.Add(new SqlParameter { ParameterName = "@query_name", Value = qn_GetCropCoverageActualBlock, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfo.Add(new SqlParameter { ParameterName = "@District_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfo.Add(new SqlParameter { ParameterName = "@Block_id", Value = string.IsNullOrEmpty(blockId) ? DBNull.Value : (object)blockId, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfo.Add(new SqlParameter { ParameterName = "@Panchayat_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfo.Add(new SqlParameter { ParameterName = "@Season_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfo.Add(new SqlParameter { ParameterName = "@Crop_id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfo.Add(new SqlParameter { ParameterName = "@Last_Refresh_Ts", Value = DBNull.Value, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
            dbparamsCropInfo.Add(new SqlParameter { ParameterName = "@Ist_Date", Value = DBNull.Value, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });

            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(sp_GetCropCoverageActualPanchtDelta, dbparamsCropInfo, SqlHelper.ExecutionType.Procedure);

            if (dt != null && dt.Rows.Count > 0)
            {
                CropCoverageActualBlock cropCoverageActualBlock = SqlHelper.ConvertDataTableToList<CropCoverageActualBlock>(dt)[0];
                cropCoverageActualBlock.CropList = new List<CropCvrgEntity>();
                cropCoverageActualBlock.CropList = SqlHelper.ConvertDataTableToList<CropCvrgEntity>(dt);
                list.Add(cropCoverageActualBlock);
            }

            return list;
        }

        /// <summary>
        /// GetCropCoverageActualDistrict.
        /// </summary>
        /// <param name="districtId">districtId.</param>
        /// <returns>CropCoverageActualDistrict List.</returns>
        public List<CropCoverageActualDistrict> GetCropCoverageActualDistrict(string districtId)
        {
            List<CropCoverageActualDistrict> list = new List<CropCoverageActualDistrict>();

            List<DbParameter> dbparamsCropInfo = new List<DbParameter>();
            dbparamsCropInfo.Add(new SqlParameter { ParameterName = "@query_name", Value = qn_GetCropCoverageActualDistrict, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfo.Add(new SqlParameter { ParameterName = "@District_Id", Value = string.IsNullOrEmpty(districtId) ? DBNull.Value : (object)districtId, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfo.Add(new SqlParameter { ParameterName = "@Block_id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfo.Add(new SqlParameter { ParameterName = "@Panchayat_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfo.Add(new SqlParameter { ParameterName = "@Season_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfo.Add(new SqlParameter { ParameterName = "@Crop_id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfo.Add(new SqlParameter { ParameterName = "@Last_Refresh_Ts", Value = DBNull.Value, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
            dbparamsCropInfo.Add(new SqlParameter { ParameterName = "@Ist_Date", Value = DBNull.Value, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });

            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(sp_GetCropCoverageActualPanchtDelta, dbparamsCropInfo, SqlHelper.ExecutionType.Procedure);
            if (dt != null && dt.Rows.Count > 0)
            {
                CropCoverageActualDistrict cropCoverageActualDistrict = SqlHelper.ConvertDataTableToList<CropCoverageActualDistrict>(dt)[0];
                cropCoverageActualDistrict.CropList = new List<CropCvrgEntity>();
                cropCoverageActualDistrict.CropList = SqlHelper.ConvertDataTableToList<CropCvrgEntity>(dt);
                list.Add(cropCoverageActualDistrict);
            }

            return list;
        }

        /// <summary>
        /// GetCropDamagePancht.
        /// </summary>
        /// <param name="seasonId">seasonId.</param>
        /// <param name="panchayatId">panchayatId.</param>
        /// <returns>DtoCropDamageDetails List.</returns>
        public List<CropDamagePancht> GetCropDamagePancht(string seasonId, string panchayatId)
        {
            List<DtoCropDamageDetails> dTOCropDamageDetails = new List<DtoCropDamageDetails>();
            List<CropDamagePancht> list = new List<CropDamagePancht>();
            CropDamagePancht cropDamagePancht = new CropDamagePancht();
            cropDamagePancht.DmgCropList = new List<CropDetailsPanchayat>();

            List<DbParameter> dbparamsCropInfo = new List<DbParameter>();
            dbparamsCropInfo.Add(new SqlParameter { ParameterName = "@query_name", Value = qn_GetCropDamagePancht, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfo.Add(new SqlParameter { ParameterName = "@District_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfo.Add(new SqlParameter { ParameterName = "@Block_id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfo.Add(new SqlParameter { ParameterName = "@Panchayat_Id", Value = string.IsNullOrEmpty(panchayatId) ? DBNull.Value : (object)panchayatId, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfo.Add(new SqlParameter { ParameterName = "@Season_Id", Value = string.IsNullOrEmpty(seasonId) ? DBNull.Value : (object)seasonId, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfo.Add(new SqlParameter { ParameterName = "@Crop_id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfo.Add(new SqlParameter { ParameterName = "@Last_Refresh_Ts", Value = DBNull.Value, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });

            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(sp_GetCropDamagePancht, dbparamsCropInfo, SqlHelper.ExecutionType.Procedure);

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
                            Ac_submit_flag = damage.Ac_submit_flag,
                            Dao_add_edit_flag = damage.Dao_add_edit_flag,
                            Rec_updated_userid = damage.Rec_updated_userid,
                            Bao_add_edit_flag = damage.Bao_add_edit_flag,
                            Rec_updated_date = damage.Rec_updated_date,
                            UpdatedBy = damage.UpdatedBy,
                        });
                    }
                }

                list.Add(cropDamagePancht);
            }

            return list;
        }

        /// <summary>
        /// GetCropDamagePanchtDelta.
        /// </summary>
        /// <param name="seasonId">seasonId.</param>
        /// <param name="panchayatId">panchayatId.</param>
        /// <param name="lastrefreshedTime">lastrefreshedTime.</param>
        /// <returns>CropDamagePancht List.</returns>
        public List<CropDamagePancht> GetCropDamagePanchtDelta(string seasonId, string panchayatId, DateTime lastrefreshedTime)
        {
            DateTime lastrefreshtime = Convert.ToDateTime(lastrefreshedTime);

            List<DtoCropDamageDetails> dTOCropDamageDetails = new List<DtoCropDamageDetails>();
            List<CropDamagePancht> list = new List<CropDamagePancht>();
            CropDamagePancht cropDamagePancht = new CropDamagePancht();
            cropDamagePancht.DmgCropList = new List<CropDetailsPanchayat>();

            List<DbParameter> dbparamsCropInfo = new List<DbParameter>();
            dbparamsCropInfo.Add(new SqlParameter { ParameterName = "@query_name", Value = qn_GetCropDamagePanchtDelta, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfo.Add(new SqlParameter { ParameterName = "@District_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfo.Add(new SqlParameter { ParameterName = "@Block_id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfo.Add(new SqlParameter { ParameterName = "@Panchayat_Id", Value = string.IsNullOrEmpty(panchayatId) ? DBNull.Value : (object)panchayatId, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfo.Add(new SqlParameter { ParameterName = "@Season_Id", Value = string.IsNullOrEmpty(seasonId) ? DBNull.Value : (object)seasonId, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfo.Add(new SqlParameter { ParameterName = "@Crop_id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfo.Add(new SqlParameter { ParameterName = "@Last_Refresh_Ts", Value = lastrefreshtime, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });

            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(sp_GetCropDamagePancht, dbparamsCropInfo, SqlHelper.ExecutionType.Procedure);

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
                            Ac_submit_flag = damage.Ac_submit_flag,
                            Dao_add_edit_flag = damage.Dao_add_edit_flag,
                            Rec_updated_userid = damage.Rec_updated_userid,
                            Bao_add_edit_flag = damage.Bao_add_edit_flag,
                            Rec_updated_date = damage.Rec_updated_date,
                            UpdatedBy = damage.UpdatedBy,
                        });
                    }
                }

                list.Add(cropDamagePancht);
            }

            return list;
        }

        /// <summary>
        /// GetCropDamageBlock.
        /// </summary>
        /// <param name="blockId">blockId.</param>
        /// <returns>CropDamageBlock List.</returns>
        public List<CropDamageBlock> GetCropDamageBlock(string blockId)
        {
            List<CropDamageBlock> list = new List<CropDamageBlock>();

            List<DbParameter> dbparamsCropInfo = new List<DbParameter>();
            dbparamsCropInfo.Add(new SqlParameter { ParameterName = "@query_name", Value = qn_GetCropDamageBlock, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfo.Add(new SqlParameter { ParameterName = "@District_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfo.Add(new SqlParameter { ParameterName = "@Block_id", Value = string.IsNullOrEmpty(blockId) ? DBNull.Value : (object)blockId, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfo.Add(new SqlParameter { ParameterName = "@Panchayat_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfo.Add(new SqlParameter { ParameterName = "@Season_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfo.Add(new SqlParameter { ParameterName = "@Crop_id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfo.Add(new SqlParameter { ParameterName = "@Last_Refresh_Ts", Value = DBNull.Value, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });

            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(sp_GetCropDamagePancht, dbparamsCropInfo, SqlHelper.ExecutionType.Procedure);
            if (dt != null && dt.Rows.Count > 0)
            {
                CropDamageBlock cropDamageBlock = SqlHelper.ConvertDataTableToList<CropDamageBlock>(dt)[0];
                cropDamageBlock.DmgCropList = new List<CropDmgEntity>();
                cropDamageBlock.DmgCropList = SqlHelper.ConvertDataTableToList<CropDmgEntity>(dt);
                list.Add(cropDamageBlock);
            }

            return list;
        }

        /// <summary>
        /// GetCropDamageDistrict.
        /// </summary>
        /// <param name="districtId">districtId.</param>
        /// <returns>CropDamageDistrict List.</returns>
        public List<CropDamageDistrict> GetCropDamageDistrict(string districtId)
        {
            List<CropDamageDistrict> list = new List<CropDamageDistrict>();

            List<DbParameter> dbparamsCropInfo = new List<DbParameter>();
            dbparamsCropInfo.Add(new SqlParameter { ParameterName = "@query_name", Value = qn_GetCropDamageDistrict, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfo.Add(new SqlParameter { ParameterName = "@District_Id", Value = string.IsNullOrEmpty(districtId) ? DBNull.Value : (object)districtId, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfo.Add(new SqlParameter { ParameterName = "@Block_id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfo.Add(new SqlParameter { ParameterName = "@Panchayat_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfo.Add(new SqlParameter { ParameterName = "@Season_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfo.Add(new SqlParameter { ParameterName = "@Crop_id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfo.Add(new SqlParameter { ParameterName = "@Last_Refresh_Ts", Value = DBNull.Value, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });

            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(sp_GetCropDamagePancht, dbparamsCropInfo, SqlHelper.ExecutionType.Procedure);
            if (dt != null && dt.Rows.Count > 0)
            {
                CropDamageDistrict cropDamageDistrict = SqlHelper.ConvertDataTableToList<CropDamageDistrict>(dt)[0];
                cropDamageDistrict.DmgCropList = new List<CropDmgEntity>();
                cropDamageDistrict.DmgCropList = SqlHelper.ConvertDataTableToList<CropDmgEntity>(dt);
                list.Add(cropDamageDistrict);
            }

            return list;
        }

        /// <summary>
        /// GetCropCoverageTargetDAO.
        /// </summary>
        /// <param name="district_id">district_id.</param>
        /// <param name="season_id">season_id.</param>
        /// <param name="crop_id">crop_id.</param>
        /// <returns>CropCoverageAim List.</returns>
        public CropCoverageAim GetCropCoverageTargetDAO(int district_id, int season_id, int crop_id)
        {
            List<DbParameter> dbparamsCropInfoDAODist = new List<DbParameter>();
            dbparamsCropInfoDAODist.Add(new SqlParameter { ParameterName = "@query_name", Value = qn_GetCropCoverageTargetDAODist, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODist.Add(new SqlParameter { ParameterName = "@District_Id", Value = string.IsNullOrEmpty(district_id.ToString()) ? DBNull.Value : (object)district_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODist.Add(new SqlParameter { ParameterName = "@Block_id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODist.Add(new SqlParameter { ParameterName = "@Panchayat_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODist.Add(new SqlParameter { ParameterName = "@Season_Id", Value = string.IsNullOrEmpty(season_id.ToString()) ? DBNull.Value : (object)season_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODist.Add(new SqlParameter { ParameterName = "@Crop_id", Value = string.IsNullOrEmpty(crop_id.ToString()) ? DBNull.Value : (object)crop_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODist.Add(new SqlParameter { ParameterName = "@Last_Refresh_Ts", Value = DBNull.Value, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });

            List<DbParameter> dbparamsCropInfoDAOBlk = new List<DbParameter>();
            dbparamsCropInfoDAOBlk.Add(new SqlParameter { ParameterName = "@query_name", Value = qn_GetCropCoverageTargetDAOBlk, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAOBlk.Add(new SqlParameter { ParameterName = "@District_Id", Value = string.IsNullOrEmpty(district_id.ToString()) ? DBNull.Value : (object)district_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAOBlk.Add(new SqlParameter { ParameterName = "@Block_id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAOBlk.Add(new SqlParameter { ParameterName = "@Panchayat_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAOBlk.Add(new SqlParameter { ParameterName = "@Season_Id", Value = string.IsNullOrEmpty(season_id.ToString()) ? DBNull.Value : (object)season_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAOBlk.Add(new SqlParameter { ParameterName = "@Crop_id", Value = string.IsNullOrEmpty(crop_id.ToString()) ? DBNull.Value : (object)crop_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAOBlk.Add(new SqlParameter { ParameterName = "@Last_Refresh_Ts", Value = DBNull.Value, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });

            List<DbParameter> dbparamsCropInfoDAOPanchyt = new List<DbParameter>();
            dbparamsCropInfoDAOPanchyt.Add(new SqlParameter { ParameterName = "@query_name", Value = qn_GetCropCoverageTargetDAOPanchyt, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAOPanchyt.Add(new SqlParameter { ParameterName = "@District_Id", Value = string.IsNullOrEmpty(district_id.ToString()) ? DBNull.Value : (object)district_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAOPanchyt.Add(new SqlParameter { ParameterName = "@Block_id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAOPanchyt.Add(new SqlParameter { ParameterName = "@Panchayat_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAOPanchyt.Add(new SqlParameter { ParameterName = "@Season_Id", Value = string.IsNullOrEmpty(season_id.ToString()) ? DBNull.Value : (object)season_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAOPanchyt.Add(new SqlParameter { ParameterName = "@Crop_id", Value = string.IsNullOrEmpty(crop_id.ToString()) ? DBNull.Value : (object)crop_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAOPanchyt.Add(new SqlParameter { ParameterName = "@Last_Refresh_Ts", Value = DBNull.Value, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });

            List<DbParameter> dbparamsCropInfoDAOVillage = new List<DbParameter>();
            dbparamsCropInfoDAOVillage.Add(new SqlParameter { ParameterName = "@query_name", Value = qn_GetCropCoverageTargetDAOVillage, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAOVillage.Add(new SqlParameter { ParameterName = "@District_Id", Value = string.IsNullOrEmpty(district_id.ToString()) ? DBNull.Value : (object)district_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAOVillage.Add(new SqlParameter { ParameterName = "@Block_id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAOVillage.Add(new SqlParameter { ParameterName = "@Panchayat_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAOVillage.Add(new SqlParameter { ParameterName = "@Season_Id", Value = string.IsNullOrEmpty(season_id.ToString()) ? DBNull.Value : (object)season_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAOVillage.Add(new SqlParameter { ParameterName = "@Crop_id", Value = string.IsNullOrEmpty(crop_id.ToString()) ? DBNull.Value : (object)crop_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAOVillage.Add(new SqlParameter { ParameterName = "@Last_Refresh_Ts", Value = DBNull.Value, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });

            DataTable dtDistricts = SqlHelper.ExecuteSelect<SqlConnection>(sp_GetCropCoverageAimPancht, dbparamsCropInfoDAODist, SqlHelper.ExecutionType.Procedure);
            DataTable dtBlocks = SqlHelper.ExecuteSelect<SqlConnection>(sp_GetCropCoverageAimPancht, dbparamsCropInfoDAOBlk, SqlHelper.ExecutionType.Procedure);
            DataTable dtPanchayats = SqlHelper.ExecuteSelect<SqlConnection>(sp_GetCropCoverageAimPancht, dbparamsCropInfoDAOPanchyt, SqlHelper.ExecutionType.Procedure);
            DataTable dtVillages = SqlHelper.ExecuteSelect<SqlConnection>(sp_GetCropCoverageAimPancht, dbparamsCropInfoDAOVillage, SqlHelper.ExecutionType.Procedure);

            var cropCoverageAim = dtDistricts.AsEnumerable().Any() ? SqlHelper.ConvertDataTableToList<CropCoverageAim>(dtDistricts)[0] : null;
            if (cropCoverageAim != null && dtPanchayats.Rows.Count > 0)
            {
                var aimBlocks = SqlHelper.ConvertDataTableToList<DtoBlock>(dtBlocks);
                var aimPanchayats = SqlHelper.ConvertDataTableToList<DtoPanchayat>(dtPanchayats);
                var aimVillages = SqlHelper.ConvertDataTableToList<DtoVillage>(dtVillages);

                foreach (var panchayat in aimPanchayats)
                {
                    panchayat.VillageList = aimVillages.Where(x => x.Panchayat_Id == panchayat.Panchayat_Id).ToList();
                }

                CropCoverageAim cropCoverageAimResponse = cropCoverageAim;

                if (aimBlocks.Any())
                {
                    cropCoverageAimResponse.BlockList = new List<AimBlock>();
                    foreach (var item in aimBlocks)
                    {
                        item.PanchayatList = aimPanchayats.Where(x => x.Block_Id == item.Block_Id).ToList();
                    }

                    cropCoverageAimResponse.BlockList = aimBlocks.Select(x =>
                     new AimBlock
                     {
                         Block_Id = x.Block_Id,
                         Block_Name = x.Block_Name,
                         Area_Target = x.Area_Target,
                         Refreshed_date = x.Refreshed_date,
                         Refreshed_userid = x.Refreshed_userid,
                         DAO_Approval_flag = x.DAO_Approval_flag,
                         DAO_Approval_Reason = x.DAO_Approval_Reason,
                         DAO_Approved_date = x.DAO_Approved_date,
                         DAO_Approved_userid = x.DAO_Approved_userid,
                         Refreshed_username = x.Refreshed_username,
                         Dao_approved_username = x.Dao_approved_username,
                         UpdatedBy = x.UpdatedBy,
                         Rec_updated_date = x.Rec_Updated_Date,
                         Rec_updated_userid = x.Rec_Updated_Userid,
                         PanchayatList = x.PanchayatList.Any() ? x.PanchayatList.Select(x =>
                           new AimPanchayat
                           {
                               Panchayat_Id = x.Panchayat_Id,
                               Panchayat_Name = x.Panchayat_Name,
                               Area_Target = x.Area_Target,
                               AC_Submitted_date = x.AC_Submitted_date,
                               AC_Submitted_userid = x.AC_Submitted_userid,
                               BAO_Approval_flag = x.BAO_Approval_flag,
                               BAO_Approval_Reason = x.BAO_Approval_Reason,
                               BAO_Approved_date = x.BAO_Approved_date,
                               BAO_Approved_userid = x.BAO_Approved_userid,
                               DAO_Approval_flag = x.DAO_Approval_flag,
                               DAO_Approval_Reason = x.DAO_Approval_Reason,
                               DAO_Approved_date = x.DAO_Approved_date,
                               DAO_Approved_userid = x.DAO_Approved_userid,
                               Ac_submitted_username = x.Ac_submitted_username,
                               Bao_approved_username = x.Bao_approved_username,
                               Dao_approved_username = x.Dao_approved_username,
                               UpdatedBy = x.UpdatedBy,
                               Rec_updated_date = x.Rec_Updated_Date,
                               Rec_updated_userid = x.Rec_Updated_Userid,
                               Ac_submit_flag = x.Ac_submit_flag,
                               Bao_add_edit_flag = x.Bao_add_edit_flag,
                               Dao_add_edit_flag = x.Dao_add_edit_flag,
                               VillageList = x.VillageList.Any() ? x.VillageList.Select(x =>
                               new AimVillage
                               {
                                   Reported_date = x.Reported_date,
                                   Village_Id = x.Village_Id,
                                   Village_Name = x.Village_Name,
                                   Area_Target = x.Area_Target,
                                   CreatedBy = x.CreatedBy,
                                   UpdatedBy = x.UpdatedBy,
                                   Rec_updated_userid = x.Rec_updated_userid,
                                   Rec_updated_date = x.Rec_updated_date,
                                   Submission_source = x.Submission_source,
                                   AC_Submitted_date = x.AC_Submitted_date,
                                   AC_Submitted_userid = x.AC_Submitted_userid,
                                   BAO_Approval_flag = x.BAO_Approval_flag,
                                   BAO_Approval_Reason = x.BAO_Approval_Reason,
                                   BAO_Approved_date = x.BAO_Approved_date,
                                   BAO_Approved_userid = x.BAO_Approved_userid,
                                   DAO_Approval_flag = x.DAO_Approval_flag,
                                   DAO_Approval_Reason = x.DAO_Approval_Reason,
                                   DAO_Approved_date = x.DAO_Approved_date,
                                   DAO_Approved_userid = x.DAO_Approved_userid,
                                   Ac_submitted_username = x.Ac_submitted_username,
                                   Bao_approved_username = x.Bao_approved_username,
                                   Dao_approved_username = x.Dao_approved_username,
                                   Ac_submit_flag = x.Ac_submit_flag,
                                   Bao_add_edit_flag = x.Bao_add_edit_flag,
                                   Dao_add_edit_flag = x.Dao_add_edit_flag,
                               }).ToList() : null,
                           })
                     .ToList() : null,
                     }).ToList();
                }

                return cropCoverageAimResponse;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// GetCropCoverageTargetDAOOffline.
        /// </summary>
        /// <param name="district_id">district_id.</param>
        /// <param name="season_id">season_id.</param>
        /// <param name="crop_ids">crop_ids.</param>
        /// <returns>CropCoverageAim List.</returns>
        public List<CropCoverageAim> GetCropCoverageTargetDAOOffline(int district_id, int season_id, string crop_ids)
        {
            List<DbParameter> dbparamsCropInfoDAOOfflineDist = new List<DbParameter>();
            dbparamsCropInfoDAOOfflineDist.Add(new SqlParameter { ParameterName = "@query_name", Value = qn_GetCropCoverageTargetDAOOfflineDist, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAOOfflineDist.Add(new SqlParameter { ParameterName = "@District_Id", Value = string.IsNullOrEmpty(district_id.ToString()) ? DBNull.Value : (object)district_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAOOfflineDist.Add(new SqlParameter { ParameterName = "@Block_id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAOOfflineDist.Add(new SqlParameter { ParameterName = "@Panchayat_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAOOfflineDist.Add(new SqlParameter { ParameterName = "@Season_Id", Value = string.IsNullOrEmpty(season_id.ToString()) ? DBNull.Value : (object)season_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAOOfflineDist.Add(new SqlParameter { ParameterName = "@Crop_id", Value = string.IsNullOrEmpty(crop_ids.ToString()) ? DBNull.Value : (object)crop_ids.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAOOfflineDist.Add(new SqlParameter { ParameterName = "@Last_Refresh_Ts", Value = DBNull.Value, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });

            List<DbParameter> dbparamsCropInfoDAOOfflineBlk = new List<DbParameter>();
            dbparamsCropInfoDAOOfflineBlk.Add(new SqlParameter { ParameterName = "@query_name", Value = qn_GetCropCoverageTargetDAOOfflineBlk, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAOOfflineBlk.Add(new SqlParameter { ParameterName = "@District_Id", Value = string.IsNullOrEmpty(district_id.ToString()) ? DBNull.Value : (object)district_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAOOfflineBlk.Add(new SqlParameter { ParameterName = "@Block_id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAOOfflineBlk.Add(new SqlParameter { ParameterName = "@Panchayat_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAOOfflineBlk.Add(new SqlParameter { ParameterName = "@Season_Id", Value = string.IsNullOrEmpty(season_id.ToString()) ? DBNull.Value : (object)season_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAOOfflineBlk.Add(new SqlParameter { ParameterName = "@Crop_id", Value = string.IsNullOrEmpty(crop_ids.ToString()) ? DBNull.Value : (object)crop_ids.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAOOfflineBlk.Add(new SqlParameter { ParameterName = "@Last_Refresh_Ts", Value = DBNull.Value, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });

            List<DbParameter> dbparamsCropInfoDAOOfflinePanchyt = new List<DbParameter>();
            dbparamsCropInfoDAOOfflinePanchyt.Add(new SqlParameter { ParameterName = "@query_name", Value = qn_GetCropCoverageTargetDAOOfflinePanchyt, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAOOfflinePanchyt.Add(new SqlParameter { ParameterName = "@District_Id", Value = string.IsNullOrEmpty(district_id.ToString()) ? DBNull.Value : (object)district_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAOOfflinePanchyt.Add(new SqlParameter { ParameterName = "@Block_id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAOOfflinePanchyt.Add(new SqlParameter { ParameterName = "@Panchayat_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAOOfflinePanchyt.Add(new SqlParameter { ParameterName = "@Season_Id", Value = string.IsNullOrEmpty(season_id.ToString()) ? DBNull.Value : (object)season_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAOOfflinePanchyt.Add(new SqlParameter { ParameterName = "@Crop_id", Value = string.IsNullOrEmpty(crop_ids.ToString()) ? DBNull.Value : (object)crop_ids.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAOOfflinePanchyt.Add(new SqlParameter { ParameterName = "@Last_Refresh_Ts", Value = DBNull.Value, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });

            List<DbParameter> dbparamsCropInfoDAOVillage = new List<DbParameter>();
            dbparamsCropInfoDAOVillage.Add(new SqlParameter { ParameterName = "@query_name", Value = qn_GetCropCoverageTargetDAOVillage, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAOVillage.Add(new SqlParameter { ParameterName = "@District_Id", Value = string.IsNullOrEmpty(district_id.ToString()) ? DBNull.Value : (object)district_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAOVillage.Add(new SqlParameter { ParameterName = "@Block_id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAOVillage.Add(new SqlParameter { ParameterName = "@Panchayat_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAOVillage.Add(new SqlParameter { ParameterName = "@Season_Id", Value = string.IsNullOrEmpty(season_id.ToString()) ? DBNull.Value : (object)season_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAOVillage.Add(new SqlParameter { ParameterName = "@Crop_id", Value = string.IsNullOrEmpty(crop_ids.ToString()) ? DBNull.Value : (object)crop_ids.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAOVillage.Add(new SqlParameter { ParameterName = "@Last_Refresh_Ts", Value = DBNull.Value, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });

            DataTable dtDistricts = SqlHelper.ExecuteSelect<SqlConnection>(sp_GetCropCoverageAimPancht, dbparamsCropInfoDAOOfflineDist, SqlHelper.ExecutionType.Procedure);
            DataTable dtBlocks = SqlHelper.ExecuteSelect<SqlConnection>(sp_GetCropCoverageAimPancht, dbparamsCropInfoDAOOfflineBlk, SqlHelper.ExecutionType.Procedure);
            DataTable dtPanchayats = SqlHelper.ExecuteSelect<SqlConnection>(sp_GetCropCoverageAimPancht, dbparamsCropInfoDAOOfflinePanchyt, SqlHelper.ExecutionType.Procedure);
            DataTable dtVillages = SqlHelper.ExecuteSelect<SqlConnection>(sp_GetCropCoverageAimPancht, dbparamsCropInfoDAOVillage, SqlHelper.ExecutionType.Procedure);

            var cropCoverageAim = dtDistricts.AsEnumerable().Any() ? SqlHelper.ConvertDataTableToList<CropCoverageAim>(dtDistricts) : null;
            if (cropCoverageAim != null && dtPanchayats.Rows.Count > 0)
            {
                var aimBlocks = SqlHelper.ConvertDataTableToList<DtoBlock>(dtBlocks);
                var aimPanchayats = SqlHelper.ConvertDataTableToList<DtoPanchayat>(dtPanchayats);
                var aimVillages = SqlHelper.ConvertDataTableToList<DtoVillage>(dtVillages);

                foreach (var panchayat in aimPanchayats)
                {
                    panchayat.VillageList = aimVillages.Where(x => x.Panchayat_Id == panchayat.Panchayat_Id).ToList();
                }

                List<CropCoverageAim> cropCoverageAimResponse = cropCoverageAim;

                if (aimBlocks.Any())
                {
                    foreach (var item in aimBlocks)
                    {
                        item.PanchayatList = aimPanchayats.Where(x => x.Block_Id == item.Block_Id && x.Crop_id == item.Crop_id).ToList();
                    }

                    foreach (var item in cropCoverageAimResponse)
                    {
                        item.BlockList = aimBlocks.Where(x => x.Crop_id == item.Crop_Id).Select(x =>
                         new AimBlock
                         {
                             Block_Id = x.Block_Id,
                             Block_Name = x.Block_Name,
                             Area_Target = x.Area_Target,
                             Refreshed_date = x.Refreshed_date,
                             Refreshed_userid = x.Refreshed_userid,
                             DAO_Approval_flag = x.DAO_Approval_flag,
                             DAO_Approval_Reason = x.DAO_Approval_Reason,
                             DAO_Approved_date = x.DAO_Approved_date,
                             DAO_Approved_userid = x.DAO_Approved_userid,
                             Refreshed_username = x.Refreshed_username,
                             Dao_approved_username = x.Dao_approved_username,
                             UpdatedBy = x.UpdatedBy,
                             Rec_updated_date = x.Rec_Updated_Date,
                             Rec_updated_userid = x.Rec_Updated_Userid,
                             PanchayatList = x.PanchayatList.Any() ? x.PanchayatList.Select(x =>
                               new AimPanchayat
                               {
                                   Panchayat_Id = x.Panchayat_Id,
                                   Panchayat_Name = x.Panchayat_Name,
                                   Area_Target = x.Area_Target,
                                   AC_Submitted_date = x.AC_Submitted_date,
                                   AC_Submitted_userid = x.AC_Submitted_userid,
                                   BAO_Approval_flag = x.BAO_Approval_flag,
                                   BAO_Approval_Reason = x.BAO_Approval_Reason,
                                   BAO_Approved_date = x.BAO_Approved_date,
                                   BAO_Approved_userid = x.BAO_Approved_userid,
                                   DAO_Approval_flag = x.DAO_Approval_flag,
                                   DAO_Approval_Reason = x.DAO_Approval_Reason,
                                   DAO_Approved_date = x.DAO_Approved_date,
                                   DAO_Approved_userid = x.DAO_Approved_userid,
                                   Ac_submitted_username = x.Ac_submitted_username,
                                   Bao_approved_username = x.Bao_approved_username,
                                   Dao_approved_username = x.Dao_approved_username,
                                   UpdatedBy = x.UpdatedBy,
                                   Rec_updated_date = x.Rec_Updated_Date,
                                   Rec_updated_userid = x.Rec_Updated_Userid,
                                   Ac_submit_flag = x.Ac_submit_flag,
                                   Bao_add_edit_flag = x.Bao_add_edit_flag,
                                   Dao_add_edit_flag = x.Dao_add_edit_flag,
                                   VillageList = x.VillageList.Any() ? x.VillageList.Select(x =>
                                    new AimVillage
                                    {
                                        Reported_date = x.Reported_date,
                                        Village_Id = x.Village_Id,
                                        Village_Name = x.Village_Name,
                                        Area_Target = x.Area_Target,
                                        CreatedBy = x.CreatedBy,
                                        UpdatedBy = x.UpdatedBy,
                                        Rec_updated_userid = x.Rec_updated_userid,
                                        Rec_updated_date = x.Rec_updated_date,
                                        Submission_source = x.Submission_source,
                                        AC_Submitted_date = x.AC_Submitted_date,
                                        AC_Submitted_userid = x.AC_Submitted_userid,
                                        BAO_Approval_flag = x.BAO_Approval_flag,
                                        BAO_Approval_Reason = x.BAO_Approval_Reason,
                                        BAO_Approved_date = x.BAO_Approved_date,
                                        BAO_Approved_userid = x.BAO_Approved_userid,
                                        DAO_Approval_flag = x.DAO_Approval_flag,
                                        DAO_Approval_Reason = x.DAO_Approval_Reason,
                                        DAO_Approved_date = x.DAO_Approved_date,
                                        DAO_Approved_userid = x.DAO_Approved_userid,
                                        Ac_submitted_username = x.Ac_submitted_username,
                                        Bao_approved_username = x.Bao_approved_username,
                                        Dao_approved_username = x.Dao_approved_username,
                                        Ac_submit_flag = x.Ac_submit_flag,
                                        Bao_add_edit_flag = x.Bao_add_edit_flag,
                                        Dao_add_edit_flag = x.Dao_add_edit_flag,
                                    }).ToList() : null,
                               })
                         .ToList() : null,
                         }).ToList();
                    }
                }

                return cropCoverageAimResponse;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// GetCropCoverageTargetDAODelta.
        /// </summary>
        /// <param name="district_id">district_id.</param>
        /// <param name="season_id">season_id.</param>
        /// <param name="last_refresh_time">last_refresh_time.</param>
        /// <returns>CropCoverageAim List.</returns>
        public List<CropCoverageAim> GetCropCoverageTargetDAODelta(int district_id, int season_id, DateTime last_refresh_time)
        {
            DateTime lastrefreshtime = Convert.ToDateTime(last_refresh_time);

            List<DbParameter> dbparamsCropInfoDAODeltaDist = new List<DbParameter>();
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@query_name", Value = qn_GetCropCoverageTargetDAODeltaDist, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@District_Id", Value = string.IsNullOrEmpty(district_id.ToString()) ? DBNull.Value : (object)district_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@Block_id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@Panchayat_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@Season_Id", Value = string.IsNullOrEmpty(season_id.ToString()) ? DBNull.Value : (object)season_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@Crop_id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@Last_Refresh_Ts", Value = lastrefreshtime, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });

            List<DbParameter> dbparamsCropInfoDAODeltaBlk = new List<DbParameter>();
            dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@query_name", Value = qn_GetCropCoverageTargetDAODeltaBlk, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@District_Id", Value = string.IsNullOrEmpty(district_id.ToString()) ? DBNull.Value : (object)district_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@Block_id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@Panchayat_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@Season_Id", Value = string.IsNullOrEmpty(season_id.ToString()) ? DBNull.Value : (object)season_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@Crop_id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@Last_Refresh_Ts", Value = lastrefreshtime, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });

            List<DbParameter> dbparamsCropInfoDAODeltaPanchyt = new List<DbParameter>();
            dbparamsCropInfoDAODeltaPanchyt.Add(new SqlParameter { ParameterName = "@query_name", Value = qn_GetCropCoverageTargetDAODeltaPanchyt, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaPanchyt.Add(new SqlParameter { ParameterName = "@District_Id", Value = string.IsNullOrEmpty(district_id.ToString()) ? DBNull.Value : (object)district_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaPanchyt.Add(new SqlParameter { ParameterName = "@Block_id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaPanchyt.Add(new SqlParameter { ParameterName = "@Panchayat_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaPanchyt.Add(new SqlParameter { ParameterName = "@Season_Id", Value = string.IsNullOrEmpty(season_id.ToString()) ? DBNull.Value : (object)season_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaPanchyt.Add(new SqlParameter { ParameterName = "@Crop_id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaPanchyt.Add(new SqlParameter { ParameterName = "@Last_Refresh_Ts", Value = lastrefreshtime, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });

            DataTable dtDistricts = SqlHelper.ExecuteSelect<SqlConnection>(sp_GetCropCoverageAimPancht, dbparamsCropInfoDAODeltaDist, SqlHelper.ExecutionType.Procedure);
            DataTable dtBlocks = SqlHelper.ExecuteSelect<SqlConnection>(sp_GetCropCoverageAimPancht, dbparamsCropInfoDAODeltaBlk, SqlHelper.ExecutionType.Procedure);
            DataTable dtPanchayats = SqlHelper.ExecuteSelect<SqlConnection>(sp_GetCropCoverageAimPancht, dbparamsCropInfoDAODeltaPanchyt, SqlHelper.ExecutionType.Procedure);

            var cropCoverageAim = dtDistricts.AsEnumerable().Any() ? SqlHelper.ConvertDataTableToList<CropCoverageAim>(dtDistricts) : null;
            if (cropCoverageAim != null && dtPanchayats.Rows.Count > 0)
            {
                var aimBlocks = SqlHelper.ConvertDataTableToList<DtoBlock>(dtBlocks);
                var aimPanchayats = SqlHelper.ConvertDataTableToList<DtoPanchayat>(dtPanchayats);
                List<CropCoverageAim> cropCoverageAimResponse = cropCoverageAim;

                if (aimBlocks.Any())
                {
                    foreach (var item in aimBlocks)
                    {
                        item.PanchayatList = aimPanchayats.Where(x => x.Block_Id == item.Block_Id && x.Crop_id == item.Crop_id).ToList();
                    }

                    foreach (var item in cropCoverageAimResponse)
                    {
                        item.BlockList = aimBlocks.Where(x => x.Crop_id == item.Crop_Id).Select(x =>
                             new AimBlock
                             {
                                 Block_Id = x.Block_Id,
                                 Block_Name = x.Block_Name,
                                 Area_Target = x.Area_Target,
                                 Refreshed_date = x.Refreshed_date,
                                 Refreshed_userid = x.Refreshed_userid,
                                 DAO_Approval_flag = x.DAO_Approval_flag,
                                 DAO_Approval_Reason = x.DAO_Approval_Reason,
                                 DAO_Approved_date = x.DAO_Approved_date,
                                 DAO_Approved_userid = x.DAO_Approved_userid,
                                 Refreshed_username = x.Refreshed_username,
                                 Dao_approved_username = x.Dao_approved_username,
                                 UpdatedBy = x.UpdatedBy,
                                 Rec_updated_date = x.Rec_Updated_Date,
                                 Rec_updated_userid = x.Rec_Updated_Userid,
                                 PanchayatList = x.PanchayatList.Any() ? x.PanchayatList.Select(x =>
                                   new AimPanchayat
                                   {
                                       Panchayat_Id = x.Panchayat_Id,
                                       Panchayat_Name = x.Panchayat_Name,
                                       Area_Target = x.Area_Target,
                                       AC_Submitted_date = x.AC_Submitted_date,
                                       AC_Submitted_userid = x.AC_Submitted_userid,
                                       BAO_Approval_flag = x.BAO_Approval_flag,
                                       BAO_Approval_Reason = x.BAO_Approval_Reason,
                                       BAO_Approved_date = x.BAO_Approved_date,
                                       BAO_Approved_userid = x.BAO_Approved_userid,
                                       DAO_Approval_flag = x.DAO_Approval_flag,
                                       DAO_Approval_Reason = x.DAO_Approval_Reason,
                                       DAO_Approved_date = x.DAO_Approved_date,
                                       DAO_Approved_userid = x.DAO_Approved_userid,
                                       Ac_submitted_username = x.Ac_submitted_username,
                                       Bao_approved_username = x.Bao_approved_username,
                                       Dao_approved_username = x.Dao_approved_username,
                                       UpdatedBy = x.UpdatedBy,
                                       Rec_updated_date = x.Rec_Updated_Date,
                                       Rec_updated_userid = x.Rec_Updated_Userid,
                                       Ac_submit_flag = x.Ac_submit_flag,
                                       Bao_add_edit_flag = x.Bao_add_edit_flag,
                                       Dao_add_edit_flag = x.Dao_add_edit_flag,
                                   })
                             .ToList() : null,
                             }).ToList();
                    }
                }

                return cropCoverageAimResponse;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// GetCropCoverageActualDAODelta.
        /// </summary>
        /// <param name="district_id">district_id.</param>
        /// <param name="season_id">season_id.</param>
        /// <param name="last_refresh_time">last_refresh_time.</param>
        /// <returns>CropCoverageActual List DB details.</returns>
        public List<CropCoverageActual> GetCropCoverageActualDAODelta(int district_id, int season_id, DateTime last_refresh_time)
        {
            DateTime lastrefreshtime = Convert.ToDateTime(last_refresh_time);

            List<DbParameter> dbparamsCropInfoDAODeltaDist = new List<DbParameter>();
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@query_name", Value = qn_GetCropCoverageActualDAODeltaDist, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@District_Id", Value = string.IsNullOrEmpty(district_id.ToString()) ? DBNull.Value : (object)district_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@Block_id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@Panchayat_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@Season_Id", Value = string.IsNullOrEmpty(season_id.ToString()) ? DBNull.Value : (object)season_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@Crop_id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@Last_Refresh_Ts", Value = lastrefreshtime, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@Ist_Date", Value = this.istDate, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });

            DataTable dtDistricts = SqlHelper.ExecuteSelect<SqlConnection>(sp_GetCropCoverageActualPanchtDelta, dbparamsCropInfoDAODeltaDist, SqlHelper.ExecutionType.Procedure);

            if (dtDistricts.Rows.Count > 0)
            {
                var cropCoverageAct = dtDistricts.AsEnumerable().Any() ? SqlHelper.ConvertDataTableToList<CropCoverageActual>(dtDistricts) : null;

                List<DbParameter> dbparamsCropInfoDAODeltaBlk = new List<DbParameter>();
                dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@query_name", Value = qn_GetCropCoverageActualDAODeltaBlk, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@District_Id", Value = string.IsNullOrEmpty(district_id.ToString()) ? DBNull.Value : (object)district_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@Block_id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@Panchayat_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@Season_Id", Value = string.IsNullOrEmpty(season_id.ToString()) ? DBNull.Value : (object)season_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@Crop_id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@Last_Refresh_Ts", Value = lastrefreshtime, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@Ist_Date", Value = this.istDate, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
                List<DbParameter> dbparamsCropInfoDAODeltaPanchyt = new List<DbParameter>();
                dbparamsCropInfoDAODeltaPanchyt.Add(new SqlParameter { ParameterName = "@query_name", Value = qn_GetCropCoverageActualDAODeltaPanchyt, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaPanchyt.Add(new SqlParameter { ParameterName = "@District_Id", Value = string.IsNullOrEmpty(district_id.ToString()) ? DBNull.Value : (object)district_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaPanchyt.Add(new SqlParameter { ParameterName = "@Block_id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaPanchyt.Add(new SqlParameter { ParameterName = "@Panchayat_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaPanchyt.Add(new SqlParameter { ParameterName = "@Season_Id", Value = string.IsNullOrEmpty(season_id.ToString()) ? DBNull.Value : (object)season_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaPanchyt.Add(new SqlParameter { ParameterName = "@Crop_id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaPanchyt.Add(new SqlParameter { ParameterName = "@Last_Refresh_Ts", Value = lastrefreshtime, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaPanchyt.Add(new SqlParameter { ParameterName = "@Ist_Date", Value = this.istDate, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });

                DataTable dtBlocks = SqlHelper.ExecuteSelect<SqlConnection>(sp_GetCropCoverageActualPanchtDelta, dbparamsCropInfoDAODeltaBlk, SqlHelper.ExecutionType.Procedure);

                DataTable dtPanchayats = SqlHelper.ExecuteSelect<SqlConnection>(sp_GetCropCoverageActualPanchtDelta, dbparamsCropInfoDAODeltaPanchyt, SqlHelper.ExecutionType.Procedure);

                if (cropCoverageAct != null && dtPanchayats.Rows.Count > 0)
                {
                    var actBlocks = SqlHelper.ConvertDataTableToList<DtoCoverageActualBlock>(dtBlocks);
                    var actPanchayats = SqlHelper.ConvertDataTableToList<DtoCoverageActualPanchayat>(dtPanchayats);
                    List<CropCoverageActual> cropCoverageActualResponse = cropCoverageAct;

                    if (actBlocks.Any())
                    {
                        foreach (var item in actBlocks)
                        {
                            item.PanchayatList = actPanchayats.Where(x => x.Block_Id == item.Block_Id && x.Crop_id == item.Crop_id).ToList();
                        }

                        foreach (var item in cropCoverageActualResponse)
                        {
                            item.BlockList = actBlocks.Where(x => x.Crop_id == item.Crop_Id).Select(x =>
                         new CoverageBlock
                         {
                             Reported_date = x.Reported_date,
                             Block_Id = x.Block_Id,
                             Block_Name = x.Block_Name,
                             Area_Target = x.Area_Target,
                             Cumm_Area_Prev = x.Cumm_Area_Prev,
                             Cumm_Area_Curr = x.Cumm_Area_Curr,
                             Refreshed_date = x.Refreshed_date,
                             Refreshed_userid = x.Refreshed_userid,
                             DAO_Approval_flag = x.DAO_Approval_flag,
                             DAO_Approval_Reason = x.DAO_Approval_Reason,
                             DAO_Approved_date = x.DAO_Approved_date,
                             DAO_Approved_userid = x.DAO_Approved_userid,
                             Refreshed_username = x.Refreshed_username,
                             Dao_approved_username = x.Dao_approved_username,

                             PanchayatList = x.PanchayatList.Any() ? x.PanchayatList.Select(x =>
                               new CoveragePanchayat
                               {
                                   Reported_date = x.Reported_date,
                                   Panchayat_Id = x.Panchayat_Id,
                                   Panchayat_Name = x.Panchayat_Name,
                                   Area_Target = x.Area_Target,
                                   Cumm_Area_Prev = x.Cumm_Area_Prev,
                                   Cumm_Area_Curr = x.Cumm_Area_Curr,
                                   AC_Submitted_date = x.AC_Submitted_date,
                                   AC_Submitted_userid = x.AC_Submitted_userid,
                                   BAO_Approval_flag = x.BAO_Approval_flag,
                                   BAO_Approval_Reason = x.BAO_Approval_Reason,
                                   BAO_Approved_date = x.BAO_Approved_date,
                                   BAO_Approved_userid = x.BAO_Approved_userid,
                                   DAO_Approval_flag = x.DAO_Approval_flag,
                                   DAO_Approval_Reason = x.DAO_Approval_Reason,
                                   DAO_Approved_date = x.DAO_Approved_date,
                                   DAO_Approved_userid = x.DAO_Approved_userid,
                                   Ac_submitted_username = x.Ac_submitted_username,
                                   Bao_approved_username = x.Bao_approved_username,
                                   Dao_approved_username = x.Dao_approved_username,
                                   Ac_submit_flag = x.Ac_submit_flag,
                                   Bao_add_edit_flag = x.Bao_add_edit_flag,
                                   Dao_add_edit_flag = x.Dao_add_edit_flag,
                                   Final_cvrg_flg = x.Final_cvrg_flg,
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
        /// GetCropCoverageActualDAOOffline.
        /// </summary>
        /// <param name="district_id">district_id.</param>
        /// <param name="season_id">season_id.</param>
        /// <param name="crop_ids">crop_ids.</param>
        /// <returns>List.</returns>
        public List<CropCoverageActual> GetCropCoverageActualDAOOffline(int district_id, int season_id, string crop_ids)
        {
            List<DbParameter> dbparamsCropInfoDAODeltaDist = new List<DbParameter>();
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@query_name", Value = qn_GetCropCoverageActualDAOOfflineDist, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@District_Id", Value = string.IsNullOrEmpty(district_id.ToString()) ? DBNull.Value : (object)district_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@Block_id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@Panchayat_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@village_id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@Season_Id", Value = string.IsNullOrEmpty(season_id.ToString()) ? DBNull.Value : (object)season_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@Crop_id", Value = string.IsNullOrEmpty(crop_ids.ToString()) ? DBNull.Value : (object)crop_ids.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@Last_Refresh_Ts", Value = DBNull.Value, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@Ist_Date", Value = this.istDate, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
            DataTable dtDistricts = SqlHelper.ExecuteSelect<SqlConnection>(sp_GetCropCoverageActualPanchtDelta, dbparamsCropInfoDAODeltaDist, SqlHelper.ExecutionType.Procedure);

            if (dtDistricts.Rows.Count > 0)
            {
                var cropCoverageAct = dtDistricts.AsEnumerable().Any() ? SqlHelper.ConvertDataTableToList<CropCoverageActual>(dtDistricts) : null;

                List<DbParameter> dbparamsCropInfoDAODeltaBlk = new List<DbParameter>();
                dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@query_name", Value = qn_GetCropCoverageActualDAOOfflineBlk, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@District_Id", Value = string.IsNullOrEmpty(district_id.ToString()) ? DBNull.Value : (object)district_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@Block_id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@Panchayat_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@village_id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@Season_Id", Value = string.IsNullOrEmpty(season_id.ToString()) ? DBNull.Value : (object)season_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@Crop_id", Value = string.IsNullOrEmpty(crop_ids.ToString()) ? DBNull.Value : (object)crop_ids.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@Last_Refresh_Ts", Value = DBNull.Value, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@Ist_Date", Value = this.istDate, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });

                List<DbParameter> dbparamsCropInfoDAODeltaPanchyt = new List<DbParameter>();
                dbparamsCropInfoDAODeltaPanchyt.Add(new SqlParameter { ParameterName = "@query_name", Value = qn_GetCropCoverageActualDAOOfflinePanchyt, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaPanchyt.Add(new SqlParameter { ParameterName = "@District_Id", Value = string.IsNullOrEmpty(district_id.ToString()) ? DBNull.Value : (object)district_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaPanchyt.Add(new SqlParameter { ParameterName = "@Block_id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaPanchyt.Add(new SqlParameter { ParameterName = "@Panchayat_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaPanchyt.Add(new SqlParameter { ParameterName = "@village_id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaPanchyt.Add(new SqlParameter { ParameterName = "@Season_Id", Value = string.IsNullOrEmpty(season_id.ToString()) ? DBNull.Value : (object)season_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaPanchyt.Add(new SqlParameter { ParameterName = "@Crop_id", Value = string.IsNullOrEmpty(crop_ids.ToString()) ? DBNull.Value : (object)crop_ids.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaPanchyt.Add(new SqlParameter { ParameterName = "@Last_Refresh_Ts", Value = DBNull.Value, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaPanchyt.Add(new SqlParameter { ParameterName = "@Ist_Date", Value = this.istDate, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });

                List<DbParameter> dbparamsCropInfoDAODeltaVillage = new List<DbParameter>();
                dbparamsCropInfoDAODeltaVillage.Add(new SqlParameter { ParameterName = "@query_name", Value = qn_GetCropCoverageActualDAOOfflineVillage, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaVillage.Add(new SqlParameter { ParameterName = "@District_Id", Value = string.IsNullOrEmpty(district_id.ToString()) ? DBNull.Value : (object)district_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaVillage.Add(new SqlParameter { ParameterName = "@Block_id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaVillage.Add(new SqlParameter { ParameterName = "@Panchayat_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaVillage.Add(new SqlParameter { ParameterName = "@village_id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaVillage.Add(new SqlParameter { ParameterName = "@Season_Id", Value = string.IsNullOrEmpty(season_id.ToString()) ? DBNull.Value : (object)season_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaVillage.Add(new SqlParameter { ParameterName = "@Crop_id", Value = string.IsNullOrEmpty(crop_ids.ToString()) ? DBNull.Value : (object)crop_ids.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaVillage.Add(new SqlParameter { ParameterName = "@Last_Refresh_Ts", Value = DBNull.Value, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaVillage.Add(new SqlParameter { ParameterName = "@Ist_Date", Value = this.istDate, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });

                DataTable dtBlocks = SqlHelper.ExecuteSelect<SqlConnection>(sp_GetCropCoverageActualPanchtDelta, dbparamsCropInfoDAODeltaBlk, SqlHelper.ExecutionType.Procedure);
                DataTable dtPanchayats = SqlHelper.ExecuteSelect<SqlConnection>(sp_GetCropCoverageActualPanchtDelta, dbparamsCropInfoDAODeltaPanchyt, SqlHelper.ExecutionType.Procedure);
                DataTable dtVillages = SqlHelper.ExecuteSelect<SqlConnection>(sp_GetCropCoverageActualPanchtDelta, dbparamsCropInfoDAODeltaVillage, SqlHelper.ExecutionType.Procedure);

                if (cropCoverageAct != null && dtPanchayats.Rows.Count > 0)
                {
                    var actBlocks = SqlHelper.ConvertDataTableToList<DtoCoverageActualBlock>(dtBlocks);
                    var actPanchayats = SqlHelper.ConvertDataTableToList<DtoCoverageActualPanchayat>(dtPanchayats);
                    var actVillages = SqlHelper.ConvertDataTableToList<DtoCoverageActualVillage>(dtVillages);

                    foreach (var panchayat in actPanchayats)
                    {
                        panchayat.VillageList = actVillages.Where(x => x.Panchayat_Id == panchayat.Panchayat_Id).ToList();
                    }

                    List<CropCoverageActual> cropCoverageActualResponse = cropCoverageAct;

                    if (actBlocks.Any())
                    {
                        foreach (var item in actBlocks)
                        {
                            item.PanchayatList = actPanchayats.Where(x => x.Block_Id == item.Block_Id && x.Crop_id == item.Crop_id).ToList();
                        }

                        foreach (var item in cropCoverageActualResponse)
                        {
                            item.BlockList = actBlocks.Where(x => x.Crop_id == item.Crop_Id).Select(x =>
                         new CoverageBlock
                         {
                             Reported_date = x.Reported_date,
                             Block_Id = x.Block_Id,
                             Block_Name = x.Block_Name,
                             Area_Target = x.Area_Target,
                             Cumm_Area_Prev = x.Cumm_Area_Prev,
                             Cumm_Area_Curr = x.Cumm_Area_Curr,
                             Refreshed_date = x.Refreshed_date,
                             Refreshed_userid = x.Refreshed_userid,
                             DAO_Approval_flag = x.DAO_Approval_flag,
                             DAO_Approval_Reason = x.DAO_Approval_Reason,
                             DAO_Approved_date = x.DAO_Approved_date,
                             DAO_Approved_userid = x.DAO_Approved_userid,
                             Refreshed_username = x.Refreshed_username,
                             Dao_approved_username = x.Dao_approved_username,
                             PanchayatList = x.PanchayatList.Any() ? x.PanchayatList.Select(x =>
                               new CoveragePanchayat
                               {
                                   Reported_date = x.Reported_date,
                                   Panchayat_Id = x.Panchayat_Id,
                                   Panchayat_Name = x.Panchayat_Name,
                                   Area_Target = x.Area_Target,
                                   Cumm_Area_Prev = x.Cumm_Area_Prev,
                                   Cumm_Area_Curr = x.Cumm_Area_Curr,
                                   Existing_area = x.Existing_area,
                                   AC_Submitted_date = x.AC_Submitted_date,
                                   AC_Submitted_userid = x.AC_Submitted_userid,
                                   BAO_Approval_flag = x.BAO_Approval_flag,
                                   BAO_Approval_Reason = x.BAO_Approval_Reason,
                                   BAO_Approved_date = x.BAO_Approved_date,
                                   BAO_Approved_userid = x.BAO_Approved_userid,
                                   DAO_Approval_flag = x.DAO_Approval_flag,
                                   DAO_Approval_Reason = x.DAO_Approval_Reason,
                                   DAO_Approved_date = x.DAO_Approved_date,
                                   DAO_Approved_userid = x.DAO_Approved_userid,
                                   Ac_submitted_username = x.Ac_submitted_username,
                                   Bao_approved_username = x.Bao_approved_username,
                                   Dao_approved_username = x.Dao_approved_username,
                                   Ac_submit_flag = x.Ac_submit_flag,
                                   Bao_add_edit_flag = x.Bao_add_edit_flag,
                                   Dao_add_edit_flag = x.Dao_add_edit_flag,
                                   Final_cvrg_flg = x.Final_cvrg_flg,
                                   Rec_updated_date = x.Rec_Updated_Date,
                                   Rec_updated_userid = x.Rec_Updated_Userid,
                                   UpdatedBy = x.UpdatedBy,
                                   VillageList = x.VillageList.Any() ? x.VillageList.Select(x =>
                                   new CoverageVillage
                                   {
                                       Reported_date = x.Reported_date,
                                       Village_Id = x.Village_Id,
                                       Village_Name = x.Village_Name,
                                       Area_Target = x.Area_Target,
                                       Cumm_Area_Prev = x.Cumm_Area_Prev,
                                       Cumm_Area_Curr = x.Cumm_Area_Curr,
                                       Existing_area = x.Existing_area,
                                       AC_Submitted_date = x.AC_Submitted_date,
                                       AC_Submitted_userid = x.AC_Submitted_userid,
                                       BAO_Approval_flag = x.BAO_Approval_flag,
                                       BAO_Approval_Reason = x.BAO_Approval_Reason,
                                       BAO_Approved_date = x.BAO_Approved_date,
                                       BAO_Approved_userid = x.BAO_Approved_userid,
                                       DAO_Approval_flag = x.DAO_Approval_flag,
                                       DAO_Approval_Reason = x.DAO_Approval_Reason,
                                       DAO_Approved_date = x.DAO_Approved_date,
                                       DAO_Approved_userid = x.DAO_Approved_userid,
                                       Ac_submitted_username = x.Ac_submitted_username,
                                       Bao_approved_username = x.Bao_approved_username,
                                       Dao_approved_username = x.Dao_approved_username,
                                       Ac_submit_flag = x.Ac_submit_flag,
                                       Bao_add_edit_flag = x.Bao_add_edit_flag,
                                       Dao_add_edit_flag = x.Dao_add_edit_flag,
                                       Final_cvrg_flg = x.Final_cvrg_flg,
                                       Rec_updated_userid = x.Rec_Updated_Userid,
                                       Rec_updated_date = x.Rec_Updated_Date,
                                       UpdatedBy = x.UpdatedBy,
                                   }).ToList() : null,
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
        /// GetCropCoverageActualDAO.
        /// </summary>
        /// <param name="district_id">district_id.</param>
        /// <param name="season_id">season_id.</param>
        /// <param name="crop_id">crop_id.</param>
        /// <returns>CropCoverageActual List.</returns>
        public CropCoverageActual GetCropCoverageActualDAO(int district_id, int season_id, int crop_id)
        {
            List<DbParameter> dbparamsCropInfoDAODeltaDist = new List<DbParameter>();
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@query_name", Value = qn_GetCropCoverageActualDAODist, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@District_Id", Value = string.IsNullOrEmpty(district_id.ToString()) ? DBNull.Value : (object)district_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@Block_id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@Panchayat_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@village_id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@Season_Id", Value = string.IsNullOrEmpty(season_id.ToString()) ? DBNull.Value : (object)season_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@Crop_id", Value = string.IsNullOrEmpty(crop_id.ToString()) ? DBNull.Value : (object)crop_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@Last_Refresh_Ts", Value = DBNull.Value, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@Ist_Date", Value = this.istDate, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });

            DataTable dtDistricts = SqlHelper.ExecuteSelect<SqlConnection>(sp_GetCropCoverageActualPanchtDelta, dbparamsCropInfoDAODeltaDist, SqlHelper.ExecutionType.Procedure);

            if (dtDistricts.Rows.Count > 0)
            {
                var cropCoverageAct = dtDistricts.AsEnumerable().Any() ? SqlHelper.ConvertDataTableToList<CropCoverageActual>(dtDistricts)[0] : null;

                List<DbParameter> dbparamsCropInfoDAODeltaBlk = new List<DbParameter>();
                dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@query_name", Value = qn_GetCropCoverageActualDAOBlk, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@District_Id", Value = string.IsNullOrEmpty(district_id.ToString()) ? DBNull.Value : (object)district_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@Block_id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@Panchayat_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@village_id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@Season_Id", Value = string.IsNullOrEmpty(season_id.ToString()) ? DBNull.Value : (object)season_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@Crop_id", Value = string.IsNullOrEmpty(crop_id.ToString()) ? DBNull.Value : (object)crop_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@Last_Refresh_Ts", Value = DBNull.Value, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@Ist_Date", Value = this.istDate, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });

                List<DbParameter> dbparamsCropInfoDAODeltaPanchayat = new List<DbParameter>();
                dbparamsCropInfoDAODeltaPanchayat.Add(new SqlParameter { ParameterName = "@query_name", Value = qn_GetCropCoverageActualDAOPanchyt, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaPanchayat.Add(new SqlParameter { ParameterName = "@District_Id", Value = string.IsNullOrEmpty(district_id.ToString()) ? DBNull.Value : (object)district_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaPanchayat.Add(new SqlParameter { ParameterName = "@Block_id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaPanchayat.Add(new SqlParameter { ParameterName = "@Panchayat_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaPanchayat.Add(new SqlParameter { ParameterName = "@village_id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaPanchayat.Add(new SqlParameter { ParameterName = "@Season_Id", Value = string.IsNullOrEmpty(season_id.ToString()) ? DBNull.Value : (object)season_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaPanchayat.Add(new SqlParameter { ParameterName = "@Crop_id", Value = string.IsNullOrEmpty(crop_id.ToString()) ? DBNull.Value : (object)crop_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaPanchayat.Add(new SqlParameter { ParameterName = "@Last_Refresh_Ts", Value = DBNull.Value, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaPanchayat.Add(new SqlParameter { ParameterName = "@Ist_Date", Value = this.istDate, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });

                List<DbParameter> dbparamsCropInfoDAODeltaVillage = new List<DbParameter>();
                dbparamsCropInfoDAODeltaVillage.Add(new SqlParameter { ParameterName = "@query_name", Value = qn_GetCropCoverageActualDAOVillage, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaVillage.Add(new SqlParameter { ParameterName = "@District_Id", Value = string.IsNullOrEmpty(district_id.ToString()) ? DBNull.Value : (object)district_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaVillage.Add(new SqlParameter { ParameterName = "@Block_id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaVillage.Add(new SqlParameter { ParameterName = "@Panchayat_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaVillage.Add(new SqlParameter { ParameterName = "@village_id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaVillage.Add(new SqlParameter { ParameterName = "@Season_Id", Value = string.IsNullOrEmpty(season_id.ToString()) ? DBNull.Value : (object)season_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaVillage.Add(new SqlParameter { ParameterName = "@Crop_id", Value = string.IsNullOrEmpty(crop_id.ToString()) ? DBNull.Value : (object)crop_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaVillage.Add(new SqlParameter { ParameterName = "@Last_Refresh_Ts", Value = DBNull.Value, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaVillage.Add(new SqlParameter { ParameterName = "@Ist_Date", Value = this.istDate, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });

                DataTable dtBlocks = SqlHelper.ExecuteSelect<SqlConnection>(sp_GetCropCoverageActualPanchtDelta, dbparamsCropInfoDAODeltaBlk, SqlHelper.ExecutionType.Procedure);
                DataTable dtPanchayats = SqlHelper.ExecuteSelect<SqlConnection>(sp_GetCropCoverageActualPanchtDelta, dbparamsCropInfoDAODeltaPanchayat, SqlHelper.ExecutionType.Procedure);
                DataTable dtVillages = SqlHelper.ExecuteSelect<SqlConnection>(sp_GetCropCoverageActualPanchtDelta, dbparamsCropInfoDAODeltaVillage, SqlHelper.ExecutionType.Procedure);

                if (cropCoverageAct != null && dtPanchayats.Rows.Count > 0)
                {
                    var actBlocks = SqlHelper.ConvertDataTableToList<DtoCoverageActualBlock>(dtBlocks);
                    var actPanchayats = SqlHelper.ConvertDataTableToList<DtoCoverageActualPanchayat>(dtPanchayats);
                    var actVillages = SqlHelper.ConvertDataTableToList<DtoCoverageActualVillage>(dtVillages);

                    foreach (var panchayat in actPanchayats)
                    {
                        panchayat.VillageList = actVillages.Where(x => x.Panchayat_Id == panchayat.Panchayat_Id).ToList();
                    }

                    CropCoverageActual cropCoverageActualResponse = cropCoverageAct;

                    if (actBlocks.Any())
                    {
                        cropCoverageActualResponse.BlockList = new List<CoverageBlock>();

                        foreach (var item in actBlocks)
                        {
                            item.PanchayatList = actPanchayats.Where(x => x.Block_Id == item.Block_Id).ToList();
                        }

                        cropCoverageActualResponse.BlockList = actBlocks.Select(x =>
                         new CoverageBlock
                         {
                             Reported_date = x.Reported_date,
                             Block_Id = x.Block_Id,
                             Block_Name = x.Block_Name,
                             Area_Target = x.Area_Target,
                             Cumm_Area_Prev = x.Cumm_Area_Prev,
                             Cumm_Area_Curr = x.Cumm_Area_Curr,
                             Refreshed_date = x.Refreshed_date,
                             Refreshed_userid = x.Refreshed_userid,
                             DAO_Approval_flag = x.DAO_Approval_flag,
                             DAO_Approval_Reason = x.DAO_Approval_Reason,
                             DAO_Approved_date = x.DAO_Approved_date,
                             DAO_Approved_userid = x.DAO_Approved_userid,
                             Refreshed_username = x.Refreshed_username,
                             Dao_approved_username = x.Dao_approved_username,

                             PanchayatList = x.PanchayatList.Any() ? x.PanchayatList.Select(x =>
                               new CoveragePanchayat
                               {
                                   Reported_date = x.Reported_date,
                                   Panchayat_Id = x.Panchayat_Id,
                                   Panchayat_Name = x.Panchayat_Name,
                                   Area_Target = x.Area_Target,
                                   Cumm_Area_Prev = x.Cumm_Area_Prev,
                                   Cumm_Area_Curr = x.Cumm_Area_Curr,
                                   Existing_area = x.Existing_area,
                                   AC_Submitted_date = x.AC_Submitted_date,
                                   AC_Submitted_userid = x.AC_Submitted_userid,
                                   BAO_Approval_flag = x.BAO_Approval_flag,
                                   BAO_Approval_Reason = x.BAO_Approval_Reason,
                                   BAO_Approved_date = x.BAO_Approved_date,
                                   BAO_Approved_userid = x.BAO_Approved_userid,
                                   DAO_Approval_flag = x.DAO_Approval_flag,
                                   DAO_Approval_Reason = x.DAO_Approval_Reason,
                                   DAO_Approved_date = x.DAO_Approved_date,
                                   DAO_Approved_userid = x.DAO_Approved_userid,
                                   Ac_submitted_username = x.Ac_submitted_username,
                                   Bao_approved_username = x.Bao_approved_username,
                                   Dao_approved_username = x.Dao_approved_username,
                                   Ac_submit_flag = x.Ac_submit_flag,
                                   Bao_add_edit_flag = x.Bao_add_edit_flag,
                                   Dao_add_edit_flag = x.Dao_add_edit_flag,
                                   Final_cvrg_flg = x.Final_cvrg_flg,
                                   Rec_updated_userid = x.Rec_Updated_Userid,
                                   Rec_updated_date = x.Rec_Updated_Date,
                                   UpdatedBy = x.UpdatedBy,
                                   VillageList = x.VillageList.Any() ? x.VillageList.Select(x =>
                                   new CoverageVillage
                                   {
                                       Reported_date = x.Reported_date,
                                       Village_Id = x.Village_Id,
                                       Village_Name = x.Village_Name,
                                       Area_Target = x.Area_Target,
                                       Cumm_Area_Prev = x.Cumm_Area_Prev,
                                       Cumm_Area_Curr = x.Cumm_Area_Curr,
                                       Existing_area = x.Existing_area,
                                       AC_Submitted_date = x.AC_Submitted_date,
                                       AC_Submitted_userid = x.AC_Submitted_userid,
                                       BAO_Approval_flag = x.BAO_Approval_flag,
                                       BAO_Approval_Reason = x.BAO_Approval_Reason,
                                       BAO_Approved_date = x.BAO_Approved_date,
                                       BAO_Approved_userid = x.BAO_Approved_userid,
                                       DAO_Approval_flag = x.DAO_Approval_flag,
                                       DAO_Approval_Reason = x.DAO_Approval_Reason,
                                       DAO_Approved_date = x.DAO_Approved_date,
                                       DAO_Approved_userid = x.DAO_Approved_userid,
                                       Ac_submitted_username = x.Ac_submitted_username,
                                       Bao_approved_username = x.Bao_approved_username,
                                       Dao_approved_username = x.Dao_approved_username,
                                       Ac_submit_flag = x.Ac_submit_flag,
                                       Bao_add_edit_flag = x.Bao_add_edit_flag,
                                       Dao_add_edit_flag = x.Dao_add_edit_flag,
                                       Final_cvrg_flg = x.Final_cvrg_flg,
                                       Rec_updated_userid = x.Rec_Updated_Userid,
                                       Rec_updated_date = x.Rec_Updated_Date,
                                       UpdatedBy = x.UpdatedBy,
                                   }).ToList() : null,
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
        /// GetCropDamageDAO.
        /// </summary>
        /// <param name="district_id">district_id.</param>
        /// <param name="season_id">season_id.</param>
        /// <param name="crop_id">crop_id.</param>
        /// <returns>CropDamage List.</returns>
        public CropDamage GetCropDamageDAO(int district_id, int season_id, int crop_id)
        {
            List<DbParameter> dbparamsCropInfoDAODeltaDist = new List<DbParameter>();
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@query_name", Value = qn_GetCropDamageDAODist, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@District_Id", Value = string.IsNullOrEmpty(district_id.ToString()) ? DBNull.Value : (object)district_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@Block_id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@Panchayat_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@Season_Id", Value = string.IsNullOrEmpty(season_id.ToString()) ? DBNull.Value : (object)season_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@Crop_id", Value = string.IsNullOrEmpty(crop_id.ToString()) ? DBNull.Value : (object)crop_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@Last_Refresh_Ts", Value = DBNull.Value, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });

            List<DbParameter> dbparamsCropInfoDAODeltaBlk = new List<DbParameter>();
            dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@query_name", Value = qn_GetCropDamageDAOBlk, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@District_Id", Value = string.IsNullOrEmpty(district_id.ToString()) ? DBNull.Value : (object)district_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@Block_id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@Panchayat_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@Season_Id", Value = string.IsNullOrEmpty(season_id.ToString()) ? DBNull.Value : (object)season_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@Crop_id", Value = string.IsNullOrEmpty(crop_id.ToString()) ? DBNull.Value : (object)crop_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@Last_Refresh_Ts", Value = DBNull.Value, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });

            List<DbParameter> dbparamsCropInfoDAODeltaPanchyt = new List<DbParameter>();
            dbparamsCropInfoDAODeltaPanchyt.Add(new SqlParameter { ParameterName = "@query_name", Value = qn_GetCropDamageDAOPanchyt, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaPanchyt.Add(new SqlParameter { ParameterName = "@District_Id", Value = string.IsNullOrEmpty(district_id.ToString()) ? DBNull.Value : (object)district_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaPanchyt.Add(new SqlParameter { ParameterName = "@Block_id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaPanchyt.Add(new SqlParameter { ParameterName = "@Panchayat_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaPanchyt.Add(new SqlParameter { ParameterName = "@Season_Id", Value = string.IsNullOrEmpty(season_id.ToString()) ? DBNull.Value : (object)season_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaPanchyt.Add(new SqlParameter { ParameterName = "@Crop_id", Value = string.IsNullOrEmpty(crop_id.ToString()) ? DBNull.Value : (object)crop_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaPanchyt.Add(new SqlParameter { ParameterName = "@Last_Refresh_Ts", Value = DBNull.Value, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });

            DataTable dtDistricts = SqlHelper.ExecuteSelect<SqlConnection>(sp_GetCropDamagePancht, dbparamsCropInfoDAODeltaDist, SqlHelper.ExecutionType.Procedure);
            DataTable dtBlocks = SqlHelper.ExecuteSelect<SqlConnection>(sp_GetCropDamagePancht, dbparamsCropInfoDAODeltaBlk, SqlHelper.ExecutionType.Procedure);
            DataTable dtPanchayats = SqlHelper.ExecuteSelect<SqlConnection>(sp_GetCropDamagePancht, dbparamsCropInfoDAODeltaPanchyt, SqlHelper.ExecutionType.Procedure);

            var cropDamage = dtDistricts.AsEnumerable().Any() ? SqlHelper.ConvertDataTableToList<CropDamage>(dtDistricts)[0] : null;

            if (cropDamage != null && dtPanchayats.Rows.Count > 0)
            {
                var cropDamageBlocks = SqlHelper.ConvertDataTableToList<DtoCropDamageBlock>(dtBlocks);
                var cropDamagePanchayats = SqlHelper.ConvertDataTableToList<DtoCropDamagePanchayat>(dtPanchayats);
                CropDamage cropDamageResponse = cropDamage;

                if (cropDamageBlocks.Any())
                {
                    cropDamageResponse.BlockList = new List<CropDmgBlock>();

                    foreach (var item in cropDamageBlocks)
                    {
                        item.PanchayatList = cropDamagePanchayats.Where(x => x.Block_Id == item.Block_Id).ToList();
                    }

                    cropDamageResponse.BlockList = cropDamageBlocks.Select(x =>
                     new CropDmgBlock
                     {
                         Block_Id = x.Block_Id,
                         Block_Name = x.Block_Name,
                         Irrigated_Dmg_Area = x.Irrigated_Dmg_Area,
                         Damage_Reason = x.Damage_Reason,
                         Refreshed_date = x.Refreshed_date,
                         Refreshed_userid = x.Refreshed_userid,
                         DAO_Approval_flag = x.DAO_Approval_flag,
                         DAO_Approval_Reason = x.DAO_Approval_Reason,
                         DAO_Approved_date = x.DAO_Approved_date,
                         DAO_Approved_userid = x.DAO_Approved_userid,
                         Refreshed_username = x.Refreshed_username,
                         Dao_approved_username = x.Dao_approved_username,

                         PanchayatList = x.PanchayatList.Any() ? x.PanchayatList.Select(x =>
                           new CropDmgPanchayat
                           {
                               Panchayat_Id = x.Panchayat_Id,
                               Panchayat_Name = x.Panchayat_Name,
                               Area_covered = x.Area_covered,
                               Dmg_Till_Date = x.Dmg_Till_Date,
                           }).GroupBy(x => x.Panchayat_Id).Select(x => x.First())
                     .ToList() : null,
                     }).ToList();

                    foreach (var item in cropDamageResponse.BlockList)
                    {
                        foreach (var panchayt in item.PanchayatList)
                        {
                            panchayt.DamageList = cropDamagePanchayats.Where(x => x.Panchayat_Id == panchayt.Panchayat_Id).Select(x => new DamageEntity
                            {
                                Reported_Date = x.Reported_date,
                                Damage_Reason = x.Damage_Reason,
                                Irrigated_Dmg_Area = x.Irrigated_Dmg_Area,
                                AC_Submitted_date = x.AC_Submitted_date,
                                AC_Submitted_userid = x.AC_Submitted_userid,
                                BAO_Approval_flag = x.BAO_Approval_flag,
                                BAO_Approval_Reason = x.BAO_Approval_Reason,
                                BAO_Approved_date = x.BAO_Approved_date,
                                BAO_Approved_userid = x.BAO_Approved_userid,
                                DAO_Approval_flag = x.DAO_Approval_flag,
                                DAO_Approval_Reason = x.DAO_Approval_Reason,
                                DAO_Approved_date = x.DAO_Approved_date,
                                DAO_Approved_userid = x.DAO_Approved_userid,
                                Ac_submitted_username = x.Ac_submitted_username,
                                Bao_approved_username = x.Bao_approved_username,
                                Dao_approved_username = x.Dao_approved_username,
                                Ac_submit_flag = x.Ac_submit_flag,
                                Dao_add_edit_flag = x.Dao_add_edit_flag,
                                Rec_updated_userid = x.Rec_updated_userid,
                                Bao_add_edit_flag = x.Bao_add_edit_flag,
                                Rec_updated_date = x.Rec_updated_date,
                                UpdatedBy = x.UpdatedBy,
                            }).ToList();
                        }
                    }
                }

                return cropDamageResponse;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// GetCropDamageDAOOffline.
        /// </summary>
        /// <param name="district_id">district_id.</param>
        /// <param name="season_id">season_id.</param>
        /// <param name="crop_ids">crop_ids.</param>
        /// <returns>CropDamage List.</returns>
        public List<CropDamage> GetCropDamageDAOOffline(int district_id, int season_id, string crop_ids)
        {
            List<DbParameter> dbparamsCropInfoDAODeltaDist = new List<DbParameter>();
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@query_name", Value = qn_GetCropDamageDAOOfflineDist, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@District_Id", Value = string.IsNullOrEmpty(district_id.ToString()) ? DBNull.Value : (object)district_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@Block_id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@Panchayat_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@Season_Id", Value = string.IsNullOrEmpty(season_id.ToString()) ? DBNull.Value : (object)season_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@Crop_id", Value = string.IsNullOrEmpty(crop_ids.ToString()) ? DBNull.Value : (object)crop_ids.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@Last_Refresh_Ts", Value = DBNull.Value, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });

            List<DbParameter> dbparamsCropInfoDAODeltaBlk = new List<DbParameter>();
            dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@query_name", Value = qn_GetCropDamageDAOOfflineBlk, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@District_Id", Value = string.IsNullOrEmpty(district_id.ToString()) ? DBNull.Value : (object)district_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@Block_id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@Panchayat_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@Season_Id", Value = string.IsNullOrEmpty(season_id.ToString()) ? DBNull.Value : (object)season_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@Crop_id", Value = string.IsNullOrEmpty(crop_ids.ToString()) ? DBNull.Value : (object)crop_ids.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@Last_Refresh_Ts", Value = DBNull.Value, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });

            List<DbParameter> dbparamsCropInfoDAODeltaPanchyt = new List<DbParameter>();
            dbparamsCropInfoDAODeltaPanchyt.Add(new SqlParameter { ParameterName = "@query_name", Value = qn_GetCropDamageDAOOfflinePanchyt, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaPanchyt.Add(new SqlParameter { ParameterName = "@District_Id", Value = string.IsNullOrEmpty(district_id.ToString()) ? DBNull.Value : (object)district_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaPanchyt.Add(new SqlParameter { ParameterName = "@Block_id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaPanchyt.Add(new SqlParameter { ParameterName = "@Panchayat_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaPanchyt.Add(new SqlParameter { ParameterName = "@Season_Id", Value = string.IsNullOrEmpty(season_id.ToString()) ? DBNull.Value : (object)season_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaPanchyt.Add(new SqlParameter { ParameterName = "@Crop_id", Value = string.IsNullOrEmpty(crop_ids.ToString()) ? DBNull.Value : (object)crop_ids.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaPanchyt.Add(new SqlParameter { ParameterName = "@Last_Refresh_Ts", Value = DBNull.Value, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });

            DataTable dtDistricts = SqlHelper.ExecuteSelect<SqlConnection>(sp_GetCropDamagePancht, dbparamsCropInfoDAODeltaDist, SqlHelper.ExecutionType.Procedure);
            DataTable dtBlocks = SqlHelper.ExecuteSelect<SqlConnection>(sp_GetCropDamagePancht, dbparamsCropInfoDAODeltaBlk, SqlHelper.ExecutionType.Procedure);
            DataTable dtPanchayats = SqlHelper.ExecuteSelect<SqlConnection>(sp_GetCropDamagePancht, dbparamsCropInfoDAODeltaPanchyt, SqlHelper.ExecutionType.Procedure);

            var cropDamage = dtDistricts.AsEnumerable().Any() ? SqlHelper.ConvertDataTableToList<CropDamage>(dtDistricts) : null;

            if (cropDamage != null && dtPanchayats.Rows.Count > 0)
            {
                var cropDamageBlocks = SqlHelper.ConvertDataTableToList<DtoCropDamageBlock>(dtBlocks);
                var cropDamagePanchayats = SqlHelper.ConvertDataTableToList<DtoCropDamagePanchayat>(dtPanchayats);
                List<CropDamage> cropDamageResponse = cropDamage;

                if (cropDamageBlocks.Any())
                {
                    foreach (var item in cropDamageBlocks)
                    {
                        item.PanchayatList = cropDamagePanchayats.Where(x => x.Block_Id == item.Block_Id && x.Crop_id == item.Crop_id).ToList();
                    }

                    foreach (var item in cropDamageResponse)
                    {
                        item.BlockList = cropDamageBlocks.Where(x => x.Crop_id == item.Crop_Id).Select(x =>
                         new CropDmgBlock
                         {
                             Block_Id = x.Block_Id,
                             Block_Name = x.Block_Name,
                             Irrigated_Dmg_Area = x.Irrigated_Dmg_Area,
                             Damage_Reason = x.Damage_Reason,
                             Refreshed_date = x.Refreshed_date,
                             Refreshed_userid = x.Refreshed_userid,
                             DAO_Approval_flag = x.DAO_Approval_flag,
                             DAO_Approval_Reason = x.DAO_Approval_Reason,
                             DAO_Approved_date = x.DAO_Approved_date,
                             DAO_Approved_userid = x.DAO_Approved_userid,
                             Refreshed_username = x.Refreshed_username,
                             Dao_approved_username = x.Dao_approved_username,

                             PanchayatList = x.PanchayatList.Any() ? x.PanchayatList.Select(x =>
                               new CropDmgPanchayat
                               {
                                   Reported_date = x.Reported_date,
                                   Panchayat_Id = x.Panchayat_Id,
                                   Panchayat_Name = x.Panchayat_Name,
                                   Area_covered = x.Area_covered,
                                   Dmg_Till_Date = x.Dmg_Till_Date,
                               }).GroupBy(x => x.Panchayat_Id).Select(x => x.First())
                         .ToList() : null,
                         }).ToList();
                    }

                    foreach (var item in cropDamageResponse)
                    {
                        foreach (var blkiten in item.BlockList)
                        {
                            foreach (var panchayt in blkiten.PanchayatList)
                            {
                                panchayt.DamageList = cropDamagePanchayats.Where(x => x.Panchayat_Id == panchayt.Panchayat_Id && x.Crop_id == item.Crop_Id).Select(x => new DamageEntity
                                {
                                    Reported_Date = x.Reported_date,
                                    Damage_Reason = x.Damage_Reason,
                                    Irrigated_Dmg_Area = x.Irrigated_Dmg_Area,
                                    AC_Submitted_date = x.AC_Submitted_date,
                                    AC_Submitted_userid = x.AC_Submitted_userid,
                                    BAO_Approval_flag = x.BAO_Approval_flag,
                                    BAO_Approval_Reason = x.BAO_Approval_Reason,
                                    BAO_Approved_date = x.BAO_Approved_date,
                                    BAO_Approved_userid = x.BAO_Approved_userid,
                                    DAO_Approval_flag = x.DAO_Approval_flag,
                                    DAO_Approval_Reason = x.DAO_Approval_Reason,
                                    DAO_Approved_date = x.DAO_Approved_date,
                                    DAO_Approved_userid = x.DAO_Approved_userid,
                                    Ac_submitted_username = x.Ac_submitted_username,
                                    Bao_approved_username = x.Bao_approved_username,
                                    Dao_approved_username = x.Dao_approved_username,
                                    Ac_submit_flag = x.Ac_submit_flag,
                                    Dao_add_edit_flag = x.Dao_add_edit_flag,
                                    Rec_updated_userid = x.Rec_updated_userid,
                                    Bao_add_edit_flag = x.Bao_add_edit_flag,
                                    Rec_updated_date = x.Rec_updated_date,
                                    UpdatedBy = x.UpdatedBy,
                                }).ToList();
                            }
                        }
                    }
                }

                return cropDamageResponse;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// GetCropDamageDAOOfflineDelta.
        /// </summary>
        /// <param name="district_id">district_id.</param>
        /// <param name="season_id">season_id.</param>
        /// <param name="last_refresh_time">last_refresh_time.</param>
        /// <returns>CropDamage List.</returns>
        public List<CropDamage> GetCropDamageDAOOfflineDelta(int district_id, int season_id, DateTime last_refresh_time)
        {
            DateTime lastrefreshtime = Convert.ToDateTime(last_refresh_time);

            List<DbParameter> dbparamsCropInfoDAODeltaDist = new List<DbParameter>();
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@query_name", Value = qn_GetCropDamageDAODeltaDist, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@District_Id", Value = string.IsNullOrEmpty(district_id.ToString()) ? DBNull.Value : (object)district_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@Block_id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@Panchayat_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@Season_Id", Value = string.IsNullOrEmpty(season_id.ToString()) ? DBNull.Value : (object)season_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@Crop_id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@Last_Refresh_Ts", Value = lastrefreshtime, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });

            List<DbParameter> dbparamsCropInfoDAODeltaBlk = new List<DbParameter>();
            dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@query_name", Value = qn_GetCropDamageDAODeltaBlk, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@District_Id", Value = string.IsNullOrEmpty(district_id.ToString()) ? DBNull.Value : (object)district_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@Block_id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@Panchayat_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@Season_Id", Value = string.IsNullOrEmpty(season_id.ToString()) ? DBNull.Value : (object)season_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@Crop_id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@Last_Refresh_Ts", Value = lastrefreshtime, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });

            List<DbParameter> dbparamsCropInfoDAODeltaPanchyt = new List<DbParameter>();
            dbparamsCropInfoDAODeltaPanchyt.Add(new SqlParameter { ParameterName = "@query_name", Value = qn_GetCropDamageDAODeltaPanchyt, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaPanchyt.Add(new SqlParameter { ParameterName = "@District_Id", Value = string.IsNullOrEmpty(district_id.ToString()) ? DBNull.Value : (object)district_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaPanchyt.Add(new SqlParameter { ParameterName = "@Block_id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaPanchyt.Add(new SqlParameter { ParameterName = "@Panchayat_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaPanchyt.Add(new SqlParameter { ParameterName = "@Season_Id", Value = string.IsNullOrEmpty(season_id.ToString()) ? DBNull.Value : (object)season_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaPanchyt.Add(new SqlParameter { ParameterName = "@Crop_id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaPanchyt.Add(new SqlParameter { ParameterName = "@Last_Refresh_Ts", Value = lastrefreshtime, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });

            DataTable dtDistricts = SqlHelper.ExecuteSelect<SqlConnection>(sp_GetCropDamagePancht, dbparamsCropInfoDAODeltaDist, SqlHelper.ExecutionType.Procedure);
            DataTable dtBlocks = SqlHelper.ExecuteSelect<SqlConnection>(sp_GetCropDamagePancht, dbparamsCropInfoDAODeltaBlk, SqlHelper.ExecutionType.Procedure);
            DataTable dtPanchayats = SqlHelper.ExecuteSelect<SqlConnection>(sp_GetCropDamagePancht, dbparamsCropInfoDAODeltaPanchyt, SqlHelper.ExecutionType.Procedure);

            var cropDamage = dtDistricts.AsEnumerable().Any() ? SqlHelper.ConvertDataTableToList<CropDamage>(dtDistricts) : null;

            if (cropDamage != null && dtPanchayats.Rows.Count > 0)
            {
                var cropDamageBlocks = SqlHelper.ConvertDataTableToList<DtoCropDamageBlock>(dtBlocks);
                var cropDamagePanchayats = SqlHelper.ConvertDataTableToList<DtoCropDamagePanchayat>(dtPanchayats);
                List<CropDamage> cropDamageResponse = cropDamage;

                if (cropDamageBlocks.Any())
                {
                    foreach (var item in cropDamageBlocks)
                    {
                        item.PanchayatList = cropDamagePanchayats.Where(x => x.Block_Id == item.Block_Id && x.Crop_id == item.Crop_id).ToList();
                    }

                    foreach (var item in cropDamageResponse)
                    {
                        item.BlockList = cropDamageBlocks.Where(x => x.Crop_id == item.Crop_Id).Select(x =>
                         new CropDmgBlock
                         {
                             Block_Id = x.Block_Id,
                             Block_Name = x.Block_Name,
                             Irrigated_Dmg_Area = x.Irrigated_Dmg_Area,
                             Damage_Reason = x.Damage_Reason,
                             Refreshed_date = x.Refreshed_date,
                             Refreshed_userid = x.Refreshed_userid,
                             DAO_Approval_flag = x.DAO_Approval_flag,
                             DAO_Approval_Reason = x.DAO_Approval_Reason,
                             DAO_Approved_date = x.DAO_Approved_date,
                             DAO_Approved_userid = x.DAO_Approved_userid,
                             Refreshed_username = x.Refreshed_username,
                             Dao_approved_username = x.Dao_approved_username,

                             PanchayatList = x.PanchayatList.Any() ? x.PanchayatList.Select(x =>
                               new CropDmgPanchayat
                               {
                                   Reported_date = x.Reported_date,
                                   Panchayat_Id = x.Panchayat_Id,
                                   Panchayat_Name = x.Panchayat_Name,
                                   Area_covered = x.Area_covered,
                                   Dmg_Till_Date = x.Dmg_Till_Date,
                               }).GroupBy(x => x.Panchayat_Id).Select(x => x.First())
                         .ToList() : null,
                         }).ToList();
                    }

                    foreach (var item in cropDamageResponse)
                    {
                        foreach (var blkiten in item.BlockList)
                        {
                            foreach (var panchayt in blkiten.PanchayatList)
                            {
                                panchayt.DamageList = cropDamagePanchayats.Where(x => x.Panchayat_Id == panchayt.Panchayat_Id).Select(x => new DamageEntity
                                {
                                    Reported_Date = x.Reported_date,
                                    Damage_Reason = x.Damage_Reason,
                                    Irrigated_Dmg_Area = x.Irrigated_Dmg_Area,
                                    AC_Submitted_date = x.AC_Submitted_date,
                                    AC_Submitted_userid = x.AC_Submitted_userid,
                                    BAO_Approval_flag = x.BAO_Approval_flag,
                                    BAO_Approval_Reason = x.BAO_Approval_Reason,
                                    BAO_Approved_date = x.BAO_Approved_date,
                                    BAO_Approved_userid = x.BAO_Approved_userid,
                                    DAO_Approval_flag = x.DAO_Approval_flag,
                                    DAO_Approval_Reason = x.DAO_Approval_Reason,
                                    DAO_Approved_date = x.DAO_Approved_date,
                                    DAO_Approved_userid = x.DAO_Approved_userid,
                                    Ac_submitted_username = x.Ac_submitted_username,
                                    Bao_approved_username = x.Bao_approved_username,
                                    Dao_approved_username = x.Dao_approved_username,
                                    Ac_submit_flag = x.Ac_submit_flag,
                                    Dao_add_edit_flag = x.Dao_add_edit_flag,
                                    Rec_updated_userid = x.Rec_updated_userid,
                                    Bao_add_edit_flag = x.Bao_add_edit_flag,
                                    Rec_updated_date = x.Rec_updated_date,
                                    UpdatedBy = x.UpdatedBy,
                                }).ToList();
                            }
                        }
                    }
                }

                return cropDamageResponse;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// GetCropDamageBAOOfflineDelta.
        /// </summary>
        /// <param name="district_id">district_id.</param>
        /// <param name="block_id">block_id.</param>
        /// <param name="season_id">season_id.</param>
        /// <param name="last_refresh_time">last_refresh_time.</param>
        /// <returns>GetCropDamageBAOOfflineDelta List.</returns>
        public List<CropDamage> GetCropDamageBAOOfflineDelta(int district_id, int block_id, int season_id, DateTime last_refresh_time)
        {
            DateTime lastrefreshtime = Convert.ToDateTime(last_refresh_time);

            List<DbParameter> dbparamsCropInfoDAODeltaDist = new List<DbParameter>();
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@query_name", Value = qn_GetCropDamageBAODeltaDist, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@District_Id", Value = string.IsNullOrEmpty(district_id.ToString()) ? DBNull.Value : (object)district_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@Block_id", Value = string.IsNullOrEmpty(block_id.ToString()) ? DBNull.Value : (object)block_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@Panchayat_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@Season_Id", Value = string.IsNullOrEmpty(season_id.ToString()) ? DBNull.Value : (object)season_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@Crop_id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@Last_Refresh_Ts", Value = lastrefreshtime, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });

            List<DbParameter> dbparamsCropInfoDAODeltaBlk = new List<DbParameter>();
            dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@query_name", Value = qn_GetCropDamageBAODeltaBlk, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@District_Id", Value = string.IsNullOrEmpty(district_id.ToString()) ? DBNull.Value : (object)district_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@Block_id", Value = string.IsNullOrEmpty(block_id.ToString()) ? DBNull.Value : (object)block_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@Panchayat_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@Season_Id", Value = string.IsNullOrEmpty(season_id.ToString()) ? DBNull.Value : (object)season_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@Crop_id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@Last_Refresh_Ts", Value = lastrefreshtime, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });

            List<DbParameter> dbparamsCropInfoDAODeltaPanchyt = new List<DbParameter>();
            dbparamsCropInfoDAODeltaPanchyt.Add(new SqlParameter { ParameterName = "@query_name", Value = qn_GetCropDamageBAODeltaPanchyt, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaPanchyt.Add(new SqlParameter { ParameterName = "@District_Id", Value = string.IsNullOrEmpty(district_id.ToString()) ? DBNull.Value : (object)district_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaPanchyt.Add(new SqlParameter { ParameterName = "@Block_id", Value = string.IsNullOrEmpty(block_id.ToString()) ? DBNull.Value : (object)block_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaPanchyt.Add(new SqlParameter { ParameterName = "@Panchayat_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaPanchyt.Add(new SqlParameter { ParameterName = "@Season_Id", Value = string.IsNullOrEmpty(season_id.ToString()) ? DBNull.Value : (object)season_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaPanchyt.Add(new SqlParameter { ParameterName = "@Crop_id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaPanchyt.Add(new SqlParameter { ParameterName = "@Last_Refresh_Ts", Value = lastrefreshtime, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });

            DataTable dtDistricts = SqlHelper.ExecuteSelect<SqlConnection>(sp_GetCropDamagePancht, dbparamsCropInfoDAODeltaDist, SqlHelper.ExecutionType.Procedure);
            DataTable dtBlocks = SqlHelper.ExecuteSelect<SqlConnection>(sp_GetCropDamagePancht, dbparamsCropInfoDAODeltaBlk, SqlHelper.ExecutionType.Procedure);
            DataTable dtPanchayats = SqlHelper.ExecuteSelect<SqlConnection>(sp_GetCropDamagePancht, dbparamsCropInfoDAODeltaPanchyt, SqlHelper.ExecutionType.Procedure);

            var cropDamage = dtDistricts.AsEnumerable().Any() ? SqlHelper.ConvertDataTableToList<CropDamage>(dtDistricts) : null;

            if (cropDamage != null && dtPanchayats.Rows.Count > 0)
            {
                var cropDamageBlocks = SqlHelper.ConvertDataTableToList<DtoCropDamageBlock>(dtBlocks);
                var cropDamagePanchayats = SqlHelper.ConvertDataTableToList<DtoCropDamagePanchayat>(dtPanchayats);
                List<CropDamage> cropDamageResponse = cropDamage;

                if (cropDamageBlocks.Any())
                {
                    foreach (var item in cropDamageBlocks)
                    {
                        item.PanchayatList = cropDamagePanchayats.Where(x => x.Block_Id == item.Block_Id && x.Crop_id == item.Crop_id).ToList();
                    }

                    foreach (var item in cropDamageResponse)
                    {
                        item.BlockList = cropDamageBlocks.Where(x => x.Crop_id == item.Crop_Id).Select(x =>
                         new CropDmgBlock
                         {
                             Block_Id = x.Block_Id,
                             Block_Name = x.Block_Name,
                             Irrigated_Dmg_Area = x.Irrigated_Dmg_Area,
                             Damage_Reason = x.Damage_Reason,
                             Refreshed_date = x.Refreshed_date,
                             Refreshed_userid = x.Refreshed_userid,
                             DAO_Approval_flag = x.DAO_Approval_flag,
                             DAO_Approval_Reason = x.DAO_Approval_Reason,
                             DAO_Approved_date = x.DAO_Approved_date,
                             DAO_Approved_userid = x.DAO_Approved_userid,
                             Refreshed_username = x.Refreshed_username,
                             Dao_approved_username = x.Dao_approved_username,
                             PanchayatList = x.PanchayatList.Any() ? x.PanchayatList.Select(x =>
                               new CropDmgPanchayat
                               {
                                   Reported_date = x.Reported_date,
                                   Panchayat_Id = x.Panchayat_Id,
                                   Panchayat_Name = x.Panchayat_Name,
                                   Area_covered = x.Area_covered,
                                   Dmg_Till_Date = x.Dmg_Till_Date,
                               }).GroupBy(x => x.Panchayat_Id).Select(x => x.First())
                         .ToList() : null,
                         }).ToList();
                    }

                    foreach (var item in cropDamageResponse)
                    {
                        foreach (var blkiten in item.BlockList)
                        {
                            foreach (var panchayt in blkiten.PanchayatList)
                            {
                                panchayt.DamageList = cropDamagePanchayats.Where(x => x.Panchayat_Id == panchayt.Panchayat_Id).Select(x => new DamageEntity
                                {
                                    Reported_Date = x.Reported_date,
                                    Damage_Reason = x.Damage_Reason,
                                    Irrigated_Dmg_Area = x.Irrigated_Dmg_Area,
                                    AC_Submitted_date = x.AC_Submitted_date,
                                    AC_Submitted_userid = x.AC_Submitted_userid,
                                    BAO_Approval_flag = x.BAO_Approval_flag,
                                    BAO_Approval_Reason = x.BAO_Approval_Reason,
                                    BAO_Approved_date = x.BAO_Approved_date,
                                    BAO_Approved_userid = x.BAO_Approved_userid,
                                    DAO_Approval_flag = x.DAO_Approval_flag,
                                    DAO_Approval_Reason = x.DAO_Approval_Reason,
                                    DAO_Approved_date = x.DAO_Approved_date,
                                    DAO_Approved_userid = x.DAO_Approved_userid,
                                    Ac_submitted_username = x.Ac_submitted_username,
                                    Bao_approved_username = x.Bao_approved_username,
                                    Dao_approved_username = x.Dao_approved_username,
                                    Ac_submit_flag = x.Ac_submit_flag,
                                    Dao_add_edit_flag = x.Dao_add_edit_flag,
                                    Rec_updated_userid = x.Rec_updated_userid,
                                    Bao_add_edit_flag = x.Bao_add_edit_flag,
                                    Rec_updated_date = x.Rec_updated_date,
                                    UpdatedBy = x.UpdatedBy,
                                }).ToList();
                            }
                        }
                    }
                }

                return cropDamageResponse;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// GetCropCoverageTargetBAO.
        /// </summary>
        /// <param name="district_id">district_id.</param>
        /// <param name="block_id">block_id.</param>
        /// <param name="season_id">season_id.</param>
        /// <param name="crop_id">crop_id.</param>
        /// <returns>List Values.</returns>
        public CropCoverageAim GetCropCoverageTargetBAO(int district_id, int block_id, int season_id, int crop_id)
        {
            List<DbParameter> dbparamsCropInfoDAODeltaDist = new List<DbParameter>();
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@query_name", Value = qn_GetCropCoverageTargetBAODist, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@District_Id", Value = string.IsNullOrEmpty(district_id.ToString()) ? DBNull.Value : (object)district_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@Block_id", Value = string.IsNullOrEmpty(block_id.ToString()) ? DBNull.Value : (object)block_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@Panchayat_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@Season_Id", Value = string.IsNullOrEmpty(season_id.ToString()) ? DBNull.Value : (object)season_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@Crop_id", Value = string.IsNullOrEmpty(crop_id.ToString()) ? DBNull.Value : (object)crop_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@Last_Refresh_Ts", Value = DBNull.Value, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });

            List<DbParameter> dbparamsCropInfoDAODeltaBlk = new List<DbParameter>();
            dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@query_name", Value = qn_GetCropCoverageTargetBAOBlk, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@District_Id", Value = string.IsNullOrEmpty(district_id.ToString()) ? DBNull.Value : (object)district_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@Block_id", Value = string.IsNullOrEmpty(block_id.ToString()) ? DBNull.Value : (object)block_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@Panchayat_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@Season_Id", Value = string.IsNullOrEmpty(season_id.ToString()) ? DBNull.Value : (object)season_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@Crop_id", Value = string.IsNullOrEmpty(crop_id.ToString()) ? DBNull.Value : (object)crop_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@Last_Refresh_Ts", Value = DBNull.Value, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });

            List<DbParameter> dbparamsCropInfoDAODeltaPanchayat = new List<DbParameter>();
            dbparamsCropInfoDAODeltaPanchayat.Add(new SqlParameter { ParameterName = "@query_name", Value = qn_GetCropCoverageTargetBAOPanchyt, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaPanchayat.Add(new SqlParameter { ParameterName = "@District_Id", Value = string.IsNullOrEmpty(district_id.ToString()) ? DBNull.Value : (object)district_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaPanchayat.Add(new SqlParameter { ParameterName = "@Block_id", Value = string.IsNullOrEmpty(block_id.ToString()) ? DBNull.Value : (object)block_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaPanchayat.Add(new SqlParameter { ParameterName = "@Panchayat_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaPanchayat.Add(new SqlParameter { ParameterName = "@Season_Id", Value = string.IsNullOrEmpty(season_id.ToString()) ? DBNull.Value : (object)season_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaPanchayat.Add(new SqlParameter { ParameterName = "@Crop_id", Value = string.IsNullOrEmpty(crop_id.ToString()) ? DBNull.Value : (object)crop_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaPanchayat.Add(new SqlParameter { ParameterName = "@Last_Refresh_Ts", Value = DBNull.Value, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });

            List<DbParameter> dbparamsCropInfoDAODeltaVillage = new List<DbParameter>();
            dbparamsCropInfoDAODeltaVillage.Add(new SqlParameter { ParameterName = "@query_name", Value = qn_GetCropCoverageTargetBAOVillage, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaVillage.Add(new SqlParameter { ParameterName = "@District_Id", Value = string.IsNullOrEmpty(district_id.ToString()) ? DBNull.Value : (object)district_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaVillage.Add(new SqlParameter { ParameterName = "@Block_id", Value = string.IsNullOrEmpty(block_id.ToString()) ? DBNull.Value : (object)block_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaVillage.Add(new SqlParameter { ParameterName = "@Panchayat_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaVillage.Add(new SqlParameter { ParameterName = "@Season_Id", Value = string.IsNullOrEmpty(season_id.ToString()) ? DBNull.Value : (object)season_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaVillage.Add(new SqlParameter { ParameterName = "@Crop_id", Value = string.IsNullOrEmpty(crop_id.ToString()) ? DBNull.Value : (object)crop_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaVillage.Add(new SqlParameter { ParameterName = "@Last_Refresh_Ts", Value = DBNull.Value, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });

            DataTable dtDistricts = SqlHelper.ExecuteSelect<SqlConnection>(sp_GetCropCoverageAimPancht, dbparamsCropInfoDAODeltaDist, SqlHelper.ExecutionType.Procedure);
            DataTable dtBlocks = SqlHelper.ExecuteSelect<SqlConnection>(sp_GetCropCoverageAimPancht, dbparamsCropInfoDAODeltaBlk, SqlHelper.ExecutionType.Procedure);
            DataTable dtPanchayats = SqlHelper.ExecuteSelect<SqlConnection>(sp_GetCropCoverageAimPancht, dbparamsCropInfoDAODeltaPanchayat, SqlHelper.ExecutionType.Procedure);
            DataTable dtVillages = SqlHelper.ExecuteSelect<SqlConnection>(sp_GetCropCoverageAimPancht, dbparamsCropInfoDAODeltaVillage, SqlHelper.ExecutionType.Procedure);
            var cropCoverageAim = dtDistricts.AsEnumerable().Any() ? SqlHelper.ConvertDataTableToList<CropCoverageAim>(dtDistricts)[0] : null;

            if (cropCoverageAim != null && dtPanchayats.Rows.Count > 0)
            {
                var aimBlocks = SqlHelper.ConvertDataTableToList<DtoBlock>(dtBlocks);
                var aimPanchayats = SqlHelper.ConvertDataTableToList<DtoPanchayat>(dtPanchayats);
                var aimVillages = SqlHelper.ConvertDataTableToList<DtoVillage>(dtVillages);

                foreach (var panchayat in aimPanchayats)
                {
                    panchayat.VillageList = aimVillages.Where(x => x.Panchayat_Id == panchayat.Panchayat_Id).ToList();
                }

                CropCoverageAim cropCoverageAimResponse = cropCoverageAim;

                if (aimBlocks.Any())
                {
                    cropCoverageAimResponse.BlockList = new List<AimBlock>();

                    foreach (var item in aimBlocks)
                    {
                        item.PanchayatList = aimPanchayats.Where(x => x.Block_Id == item.Block_Id).ToList();
                    }

                    cropCoverageAimResponse.BlockList = aimBlocks.Select(x =>
                     new AimBlock
                     {
                         Block_Id = x.Block_Id,
                         Block_Name = x.Block_Name,
                         Area_Target = x.Area_Target,
                         Refreshed_userid = x.Refreshed_userid,
                         DAO_Approval_flag = x.DAO_Approval_flag,
                         DAO_Approval_Reason = x.DAO_Approval_Reason,
                         DAO_Approved_date = x.DAO_Approved_date,
                         DAO_Approved_userid = x.DAO_Approved_userid,
                         Refreshed_username = x.Refreshed_username,
                         Dao_approved_username = x.Dao_approved_username,
                         UpdatedBy = x.UpdatedBy,
                         Rec_updated_date = x.Rec_Updated_Date,
                         Rec_updated_userid = x.Rec_Updated_Userid,
                         PanchayatList = x.PanchayatList.Any() ? x.PanchayatList.Select(x =>
                           new AimPanchayat
                           {
                               Panchayat_Id = x.Panchayat_Id,
                               Panchayat_Name = x.Panchayat_Name,
                               Area_Target = x.Area_Target,
                               AC_Submitted_date = x.AC_Submitted_date,
                               AC_Submitted_userid = x.AC_Submitted_userid,
                               BAO_Approval_flag = x.BAO_Approval_flag,
                               BAO_Approval_Reason = x.BAO_Approval_Reason,
                               BAO_Approved_date = x.BAO_Approved_date,
                               BAO_Approved_userid = x.BAO_Approved_userid,
                               DAO_Approval_flag = x.DAO_Approval_flag,
                               DAO_Approval_Reason = x.DAO_Approval_Reason,
                               DAO_Approved_date = x.DAO_Approved_date,
                               DAO_Approved_userid = x.DAO_Approved_userid,
                               Ac_submitted_username = x.Ac_submitted_username,
                               Bao_approved_username = x.Bao_approved_username,
                               Dao_approved_username = x.Dao_approved_username,
                               UpdatedBy = x.UpdatedBy,
                               Rec_updated_date = x.Rec_Updated_Date,
                               Rec_updated_userid = x.Rec_Updated_Userid,
                               Ac_submit_flag = x.Ac_submit_flag,
                               Bao_add_edit_flag = x.Bao_add_edit_flag,
                               Dao_add_edit_flag = x.Dao_add_edit_flag,
                               VillageList = x.VillageList.Any() ? x.VillageList.Select(x =>
                               new AimVillage
                               {
                                   Reported_date = x.Reported_date,
                                   Village_Id = x.Village_Id,
                                   Village_Name = x.Village_Name,
                                   Area_Target = x.Area_Target,
                                   CreatedBy = x.CreatedBy,
                                   UpdatedBy = x.UpdatedBy,
                                   Rec_updated_userid = x.Rec_updated_userid,
                                   Rec_updated_date = x.Rec_updated_date,
                                   Submission_source = x.Submission_source,
                                   AC_Submitted_date = x.AC_Submitted_date,
                                   AC_Submitted_userid = x.AC_Submitted_userid,
                                   BAO_Approval_flag = x.BAO_Approval_flag,
                                   BAO_Approval_Reason = x.BAO_Approval_Reason,
                                   BAO_Approved_date = x.BAO_Approved_date,
                                   BAO_Approved_userid = x.BAO_Approved_userid,
                                   DAO_Approval_flag = x.DAO_Approval_flag,
                                   DAO_Approval_Reason = x.DAO_Approval_Reason,
                                   DAO_Approved_date = x.DAO_Approved_date,
                                   DAO_Approved_userid = x.DAO_Approved_userid,
                                   Ac_submitted_username = x.Ac_submitted_username,
                                   Bao_approved_username = x.Bao_approved_username,
                                   Dao_approved_username = x.Dao_approved_username,
                                   Ac_submit_flag = x.Ac_submit_flag,
                                   Bao_add_edit_flag = x.Bao_add_edit_flag,
                                   Dao_add_edit_flag = x.Dao_add_edit_flag,
                               }).ToList() : null,
                           })
                     .ToList() : null,
                     }).ToList();
                }

                return cropCoverageAimResponse;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// GetCropCoverageTargetBAOOffline.
        /// </summary>
        /// <param name="district_id">district_id.</param>
        /// <param name="block_id">block_id.</param>
        /// <param name="season_id">season_id.</param>
        /// <param name="crop_ids">crop_ids.</param>
        /// <returns>CropCoverageAim List.</returns>
        public List<CropCoverageAim> GetCropCoverageTargetBAOOffline(int district_id, int block_id, int season_id, string crop_ids)
        {
            List<DbParameter> dbparamsCropInfoDAODeltaDist = new List<DbParameter>();
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@query_name", Value = qn_GetCropCoverageTargetBAOOfflineDist, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@District_Id", Value = string.IsNullOrEmpty(district_id.ToString()) ? DBNull.Value : (object)district_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@Block_id", Value = string.IsNullOrEmpty(block_id.ToString()) ? DBNull.Value : (object)block_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@Panchayat_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@Season_Id", Value = string.IsNullOrEmpty(season_id.ToString()) ? DBNull.Value : (object)season_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@Crop_id", Value = string.IsNullOrEmpty(crop_ids.ToString()) ? DBNull.Value : (object)crop_ids.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@Last_Refresh_Ts", Value = DBNull.Value, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });

            List<DbParameter> dbparamsCropInfoDAODeltaBlk = new List<DbParameter>();
            dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@query_name", Value = qn_GetCropCoverageTargetBAOOfflineBlk, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@District_Id", Value = string.IsNullOrEmpty(district_id.ToString()) ? DBNull.Value : (object)district_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@Block_id", Value = string.IsNullOrEmpty(block_id.ToString()) ? DBNull.Value : (object)block_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@Panchayat_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@Season_Id", Value = string.IsNullOrEmpty(season_id.ToString()) ? DBNull.Value : (object)season_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@Crop_id", Value = string.IsNullOrEmpty(crop_ids.ToString()) ? DBNull.Value : (object)crop_ids.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@Last_Refresh_Ts", Value = DBNull.Value, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });

            List<DbParameter> dbparamsCropInfoDAODeltaPanchayat = new List<DbParameter>();
            dbparamsCropInfoDAODeltaPanchayat.Add(new SqlParameter { ParameterName = "@query_name", Value = qn_GetCropCoverageTargetBAOOfflinePanchyt, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaPanchayat.Add(new SqlParameter { ParameterName = "@District_Id", Value = string.IsNullOrEmpty(district_id.ToString()) ? DBNull.Value : (object)district_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaPanchayat.Add(new SqlParameter { ParameterName = "@Block_id", Value = string.IsNullOrEmpty(block_id.ToString()) ? DBNull.Value : (object)block_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaPanchayat.Add(new SqlParameter { ParameterName = "@Panchayat_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaPanchayat.Add(new SqlParameter { ParameterName = "@Season_Id", Value = string.IsNullOrEmpty(season_id.ToString()) ? DBNull.Value : (object)season_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaPanchayat.Add(new SqlParameter { ParameterName = "@Crop_id", Value = string.IsNullOrEmpty(crop_ids.ToString()) ? DBNull.Value : (object)crop_ids.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaPanchayat.Add(new SqlParameter { ParameterName = "@Last_Refresh_Ts", Value = DBNull.Value, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });

            List<DbParameter> dbparamsCropInfoDAODeltaVillage = new List<DbParameter>();
            dbparamsCropInfoDAODeltaVillage.Add(new SqlParameter { ParameterName = "@query_name", Value = qn_GetCropCoverageTargetBAOOfflineVillage, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaVillage.Add(new SqlParameter { ParameterName = "@District_Id", Value = string.IsNullOrEmpty(district_id.ToString()) ? DBNull.Value : (object)district_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaVillage.Add(new SqlParameter { ParameterName = "@Block_id", Value = string.IsNullOrEmpty(block_id.ToString()) ? DBNull.Value : (object)block_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaVillage.Add(new SqlParameter { ParameterName = "@Panchayat_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaVillage.Add(new SqlParameter { ParameterName = "@Season_Id", Value = string.IsNullOrEmpty(season_id.ToString()) ? DBNull.Value : (object)season_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaVillage.Add(new SqlParameter { ParameterName = "@Crop_id", Value = string.IsNullOrEmpty(crop_ids.ToString()) ? DBNull.Value : (object)crop_ids.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaVillage.Add(new SqlParameter { ParameterName = "@Last_Refresh_Ts", Value = DBNull.Value, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });

            DataTable dtDistricts = SqlHelper.ExecuteSelect<SqlConnection>(sp_GetCropCoverageAimPancht, dbparamsCropInfoDAODeltaDist, SqlHelper.ExecutionType.Procedure);
            DataTable dtBlocks = SqlHelper.ExecuteSelect<SqlConnection>(sp_GetCropCoverageAimPancht, dbparamsCropInfoDAODeltaBlk, SqlHelper.ExecutionType.Procedure);
            DataTable dtPanchayats = SqlHelper.ExecuteSelect<SqlConnection>(sp_GetCropCoverageAimPancht, dbparamsCropInfoDAODeltaPanchayat, SqlHelper.ExecutionType.Procedure);
            DataTable dtVillages = SqlHelper.ExecuteSelect<SqlConnection>(sp_GetCropCoverageAimPancht, dbparamsCropInfoDAODeltaVillage, SqlHelper.ExecutionType.Procedure);

            var cropCoverageAim = dtDistricts.AsEnumerable().Any() ? SqlHelper.ConvertDataTableToList<CropCoverageAim>(dtDistricts) : null;
            if (cropCoverageAim != null && dtPanchayats.Rows.Count > 0)
            {
                var aimBlocks = SqlHelper.ConvertDataTableToList<DtoBlock>(dtBlocks);
                var aimPanchayats = SqlHelper.ConvertDataTableToList<DtoPanchayat>(dtPanchayats);
                var aimVillages = SqlHelper.ConvertDataTableToList<DtoVillage>(dtVillages);

                foreach (var panchayat in aimPanchayats)
                {
                    panchayat.VillageList = aimVillages.Where(x => x.Panchayat_Id == panchayat.Panchayat_Id).ToList();
                }

                List<CropCoverageAim> cropCoverageAimResponse = cropCoverageAim;

                if (aimBlocks.Any())
                {
                    foreach (var item in aimBlocks)
                    {
                        item.PanchayatList = aimPanchayats.Where(x => x.Block_Id == item.Block_Id && x.Crop_id == item.Crop_id).ToList();
                    }

                    foreach (var item in cropCoverageAimResponse)
                    {
                        item.BlockList = aimBlocks.Where(x => x.Crop_id == item.Crop_Id).Select(x =>
                             new AimBlock
                             {
                                 Block_Id = x.Block_Id,
                                 Block_Name = x.Block_Name,
                                 Area_Target = x.Area_Target,
                                 Refreshed_date = x.Refreshed_date,
                                 Refreshed_userid = x.Refreshed_userid,
                                 DAO_Approval_flag = x.DAO_Approval_flag,
                                 DAO_Approval_Reason = x.DAO_Approval_Reason,
                                 DAO_Approved_date = x.DAO_Approved_date,
                                 DAO_Approved_userid = x.DAO_Approved_userid,
                                 Refreshed_username = x.Refreshed_username,
                                 Dao_approved_username = x.Dao_approved_username,
                                 UpdatedBy = x.UpdatedBy,
                                 Rec_updated_date = x.Rec_Updated_Date,
                                 Rec_updated_userid = x.Rec_Updated_Userid,
                                 PanchayatList = x.PanchayatList.Any() ? x.PanchayatList.Select(x =>
                                   new AimPanchayat
                                   {
                                       Panchayat_Id = x.Panchayat_Id,
                                       Panchayat_Name = x.Panchayat_Name,
                                       Area_Target = x.Area_Target,
                                       AC_Submitted_date = x.AC_Submitted_date,
                                       AC_Submitted_userid = x.AC_Submitted_userid,
                                       BAO_Approval_flag = x.BAO_Approval_flag,
                                       BAO_Approval_Reason = x.BAO_Approval_Reason,
                                       BAO_Approved_date = x.BAO_Approved_date,
                                       BAO_Approved_userid = x.BAO_Approved_userid,
                                       DAO_Approval_flag = x.DAO_Approval_flag,
                                       DAO_Approval_Reason = x.DAO_Approval_Reason,
                                       DAO_Approved_date = x.DAO_Approved_date,
                                       DAO_Approved_userid = x.DAO_Approved_userid,
                                       Ac_submitted_username = x.Ac_submitted_username,
                                       Bao_approved_username = x.Bao_approved_username,
                                       Dao_approved_username = x.Dao_approved_username,
                                       UpdatedBy = x.UpdatedBy,
                                       Rec_updated_date = x.Rec_Updated_Date,
                                       Rec_updated_userid = x.Rec_Updated_Userid,
                                       Ac_submit_flag = x.Ac_submit_flag,
                                       Bao_add_edit_flag = x.Bao_add_edit_flag,
                                       Dao_add_edit_flag = x.Dao_add_edit_flag,
                                       VillageList = x.VillageList.Any() ? x.VillageList.Select(x =>
                                        new AimVillage
                                        {
                                            Reported_date = x.Reported_date,
                                            Village_Id = x.Village_Id,
                                            Village_Name = x.Village_Name,
                                            Area_Target = x.Area_Target,
                                            CreatedBy = x.CreatedBy,
                                            UpdatedBy = x.UpdatedBy,
                                            Rec_updated_userid = x.Rec_updated_userid,
                                            Rec_updated_date = x.Rec_updated_date,
                                            Submission_source = x.Submission_source,
                                            AC_Submitted_date = x.AC_Submitted_date,
                                            AC_Submitted_userid = x.AC_Submitted_userid,
                                            BAO_Approval_flag = x.BAO_Approval_flag,
                                            BAO_Approval_Reason = x.BAO_Approval_Reason,
                                            BAO_Approved_date = x.BAO_Approved_date,
                                            BAO_Approved_userid = x.BAO_Approved_userid,
                                            DAO_Approval_flag = x.DAO_Approval_flag,
                                            DAO_Approval_Reason = x.DAO_Approval_Reason,
                                            DAO_Approved_date = x.DAO_Approved_date,
                                            DAO_Approved_userid = x.DAO_Approved_userid,
                                            Ac_submitted_username = x.Ac_submitted_username,
                                            Bao_approved_username = x.Bao_approved_username,
                                            Dao_approved_username = x.Dao_approved_username,
                                            Ac_submit_flag = x.Ac_submit_flag,
                                            Bao_add_edit_flag = x.Bao_add_edit_flag,
                                            Dao_add_edit_flag = x.Dao_add_edit_flag,
                                        }).ToList() : null,
                                   })
                             .ToList() : null,
                             }).ToList();
                    }
                }

                return cropCoverageAimResponse;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// GetCropCoverageTargetBAODelta.
        /// </summary>
        /// <param name="district_id">district_id.</param>
        /// <param name="block_id">block_id.</param>
        /// <param name="season_id">season_id.</param>
        /// <param name="last_refresh_time">last_refresh_time.</param>
        /// <returns>GetCropCoverageTargetBAODelta List.</returns>
        public List<CropCoverageAim> GetCropCoverageTargetBAODelta(int district_id, int block_id, int season_id, DateTime last_refresh_time)
        {
            DateTime lastrefreshtime = Convert.ToDateTime(last_refresh_time);

            List<DbParameter> dbparamsCropInfoDAODeltaDist = new List<DbParameter>();
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@query_name", Value = qn_GetCropCoverageTargetBAODeltaDist, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@District_Id", Value = string.IsNullOrEmpty(district_id.ToString()) ? DBNull.Value : (object)district_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@Block_id", Value = string.IsNullOrEmpty(block_id.ToString()) ? DBNull.Value : (object)block_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@Panchayat_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@Season_Id", Value = string.IsNullOrEmpty(season_id.ToString()) ? DBNull.Value : (object)season_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@Crop_id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@Last_Refresh_Ts", Value = lastrefreshtime, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });

            List<DbParameter> dbparamsCropInfoDAODeltaBlk = new List<DbParameter>();
            dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@query_name", Value = qn_GetCropCoverageTargetBAODeltaBlk, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@District_Id", Value = string.IsNullOrEmpty(district_id.ToString()) ? DBNull.Value : (object)district_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@Block_id", Value = string.IsNullOrEmpty(block_id.ToString()) ? DBNull.Value : (object)block_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@Panchayat_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@Season_Id", Value = string.IsNullOrEmpty(season_id.ToString()) ? DBNull.Value : (object)season_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@Crop_id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@Last_Refresh_Ts", Value = lastrefreshtime, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
            List<DbParameter> dbparamsCropInfoDAODeltaPanchayat = new List<DbParameter>();
            dbparamsCropInfoDAODeltaPanchayat.Add(new SqlParameter { ParameterName = "@query_name", Value = qn_GetCropCoverageTargetBAODeltaPanchyt, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaPanchayat.Add(new SqlParameter { ParameterName = "@District_Id", Value = string.IsNullOrEmpty(district_id.ToString()) ? DBNull.Value : (object)district_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaPanchayat.Add(new SqlParameter { ParameterName = "@Block_id", Value = string.IsNullOrEmpty(block_id.ToString()) ? DBNull.Value : (object)block_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaPanchayat.Add(new SqlParameter { ParameterName = "@Panchayat_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaPanchayat.Add(new SqlParameter { ParameterName = "@Season_Id", Value = string.IsNullOrEmpty(season_id.ToString()) ? DBNull.Value : (object)season_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaPanchayat.Add(new SqlParameter { ParameterName = "@Crop_id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaPanchayat.Add(new SqlParameter { ParameterName = "@Last_Refresh_Ts", Value = lastrefreshtime, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });

            DataTable dtDistricts = SqlHelper.ExecuteSelect<SqlConnection>(sp_GetCropCoverageAimPancht, dbparamsCropInfoDAODeltaDist, SqlHelper.ExecutionType.Procedure);
            DataTable dtBlocks = SqlHelper.ExecuteSelect<SqlConnection>(sp_GetCropCoverageAimPancht, dbparamsCropInfoDAODeltaBlk, SqlHelper.ExecutionType.Procedure);
            DataTable dtPanchayats = SqlHelper.ExecuteSelect<SqlConnection>(sp_GetCropCoverageAimPancht, dbparamsCropInfoDAODeltaPanchayat, SqlHelper.ExecutionType.Procedure);

            var cropCoverageAim = dtDistricts.AsEnumerable().Any() ? SqlHelper.ConvertDataTableToList<CropCoverageAim>(dtDistricts) : null;
            if (cropCoverageAim != null && dtPanchayats.Rows.Count > 0)
            {
                var aimBlocks = SqlHelper.ConvertDataTableToList<DtoBlock>(dtBlocks);
                var aimPanchayats = SqlHelper.ConvertDataTableToList<DtoPanchayat>(dtPanchayats);
                List<CropCoverageAim> cropCoverageAimResponse = cropCoverageAim;

                if (aimBlocks.Any())
                {
                    foreach (var item in aimBlocks)
                    {
                        item.PanchayatList = aimPanchayats.Where(x => x.Block_Id == item.Block_Id && x.Crop_id == item.Crop_id).ToList();
                    }

                    foreach (var item in cropCoverageAimResponse)
                    {
                        item.BlockList = aimBlocks.Where(x => x.Crop_id == item.Crop_Id).Select(x =>
                             new AimBlock
                             {
                                 Block_Id = x.Block_Id,
                                 Block_Name = x.Block_Name,
                                 Area_Target = x.Area_Target,
                                 Refreshed_date = x.Refreshed_date,
                                 Refreshed_userid = x.Refreshed_userid,
                                 DAO_Approval_flag = x.DAO_Approval_flag,
                                 DAO_Approval_Reason = x.DAO_Approval_Reason,
                                 DAO_Approved_date = x.DAO_Approved_date,
                                 DAO_Approved_userid = x.DAO_Approved_userid,
                                 Refreshed_username = x.Refreshed_username,
                                 Dao_approved_username = x.Dao_approved_username,
                                 UpdatedBy = x.UpdatedBy,
                                 Rec_updated_date = x.Rec_Updated_Date,
                                 Rec_updated_userid = x.Rec_Updated_Userid,
                                 PanchayatList = x.PanchayatList.Any() ? x.PanchayatList.Select(x =>
                                   new AimPanchayat
                                   {
                                       Panchayat_Id = x.Panchayat_Id,
                                       Panchayat_Name = x.Panchayat_Name,
                                       Area_Target = x.Area_Target,
                                       AC_Submitted_date = x.AC_Submitted_date,
                                       AC_Submitted_userid = x.AC_Submitted_userid,
                                       BAO_Approval_flag = x.BAO_Approval_flag,
                                       BAO_Approval_Reason = x.BAO_Approval_Reason,
                                       BAO_Approved_date = x.BAO_Approved_date,
                                       BAO_Approved_userid = x.BAO_Approved_userid,
                                       DAO_Approval_flag = x.DAO_Approval_flag,
                                       DAO_Approval_Reason = x.DAO_Approval_Reason,
                                       DAO_Approved_date = x.DAO_Approved_date,
                                       DAO_Approved_userid = x.DAO_Approved_userid,
                                       Ac_submitted_username = x.Ac_submitted_username,
                                       Bao_approved_username = x.Bao_approved_username,
                                       Dao_approved_username = x.Dao_approved_username,
                                       UpdatedBy = x.UpdatedBy,
                                       Rec_updated_date = x.Rec_Updated_Date,
                                       Rec_updated_userid = x.Rec_Updated_Userid,
                                       Ac_submit_flag = x.Ac_submit_flag,
                                       Bao_add_edit_flag = x.Bao_add_edit_flag,
                                       Dao_add_edit_flag = x.Dao_add_edit_flag,
                                   })
                             .ToList() : null,
                             }).ToList();
                    }
                }

                return cropCoverageAimResponse;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// GetCropCoverageActualBAODelta.
        /// </summary>
        /// <param name="district_id">district_id.</param>
        /// <param name="block_id">block_id.</param>
        /// <param name="season_id">season_id.</param>
        /// <param name="last_refresh_time">last_refresh_time.</param>
        /// <returns>CropCoverageActual list.</returns>
        public List<CropCoverageActual> GetCropCoverageActualBAODelta(int district_id, int block_id, int season_id, DateTime last_refresh_time)
        {
            DateTime lastrefreshtime = Convert.ToDateTime(last_refresh_time);

            List<DbParameter> dbparamsCropInfoDAODeltaDist = new List<DbParameter>();
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@query_name", Value = qn_GetCropCoverageActualBAODeltaDist, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@District_Id", Value = string.IsNullOrEmpty(district_id.ToString()) ? DBNull.Value : (object)district_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@Block_id", Value = string.IsNullOrEmpty(block_id.ToString()) ? DBNull.Value : (object)block_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@Panchayat_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@village_id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@Season_Id", Value = string.IsNullOrEmpty(season_id.ToString()) ? DBNull.Value : (object)season_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@Crop_id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@Last_Refresh_Ts", Value = lastrefreshtime, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@Ist_Date", Value = this.istDate, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });

            DataTable dtDistricts = SqlHelper.ExecuteSelect<SqlConnection>(sp_GetCropCoverageActualPanchtDelta, dbparamsCropInfoDAODeltaDist, SqlHelper.ExecutionType.Procedure);

            if (dtDistricts.Rows.Count > 0)
            {
                var cropCoverageAct = dtDistricts.AsEnumerable().Any() ? SqlHelper.ConvertDataTableToList<CropCoverageActual>(dtDistricts) : null;

                List<DbParameter> dbparamsCropInfoDAODeltaBlk = new List<DbParameter>();
                dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@query_name", Value = qn_GetCropCoverageActualBAODeltaBlk, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@District_Id", Value = string.IsNullOrEmpty(district_id.ToString()) ? DBNull.Value : (object)district_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@Block_id", Value = string.IsNullOrEmpty(block_id.ToString()) ? DBNull.Value : (object)block_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@Panchayat_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@village_id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@Season_Id", Value = string.IsNullOrEmpty(season_id.ToString()) ? DBNull.Value : (object)season_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@Crop_id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@Last_Refresh_Ts", Value = lastrefreshtime, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@Ist_Date", Value = this.istDate, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });

                List<DbParameter> dbparamsCropInfoDAODeltaPanchyt = new List<DbParameter>();
                dbparamsCropInfoDAODeltaPanchyt.Add(new SqlParameter { ParameterName = "@query_name", Value = qn_GetCropCoverageActualBAODeltaPanchyt, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaPanchyt.Add(new SqlParameter { ParameterName = "@District_Id", Value = string.IsNullOrEmpty(district_id.ToString()) ? DBNull.Value : (object)district_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaPanchyt.Add(new SqlParameter { ParameterName = "@Block_id", Value = string.IsNullOrEmpty(block_id.ToString()) ? DBNull.Value : (object)block_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaPanchyt.Add(new SqlParameter { ParameterName = "@Panchayat_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaPanchyt.Add(new SqlParameter { ParameterName = "@village_id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaPanchyt.Add(new SqlParameter { ParameterName = "@Season_Id", Value = string.IsNullOrEmpty(season_id.ToString()) ? DBNull.Value : (object)season_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaPanchyt.Add(new SqlParameter { ParameterName = "@Crop_id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaPanchyt.Add(new SqlParameter { ParameterName = "@Last_Refresh_Ts", Value = lastrefreshtime, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaPanchyt.Add(new SqlParameter { ParameterName = "@Ist_Date", Value = this.istDate, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });

                List<DbParameter> dbparamsCropInfoDAODeltaVillage = new List<DbParameter>();
                dbparamsCropInfoDAODeltaVillage.Add(new SqlParameter { ParameterName = "@query_name", Value = qn_GetCropCoverageActualBAODeltaVillage, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaVillage.Add(new SqlParameter { ParameterName = "@District_Id", Value = string.IsNullOrEmpty(district_id.ToString()) ? DBNull.Value : (object)district_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaVillage.Add(new SqlParameter { ParameterName = "@Block_id", Value = string.IsNullOrEmpty(block_id.ToString()) ? DBNull.Value : (object)block_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaVillage.Add(new SqlParameter { ParameterName = "@Panchayat_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaVillage.Add(new SqlParameter { ParameterName = "@village_id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaVillage.Add(new SqlParameter { ParameterName = "@Season_Id", Value = string.IsNullOrEmpty(season_id.ToString()) ? DBNull.Value : (object)season_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaVillage.Add(new SqlParameter { ParameterName = "@Crop_id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaVillage.Add(new SqlParameter { ParameterName = "@Last_Refresh_Ts", Value = lastrefreshtime, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaVillage.Add(new SqlParameter { ParameterName = "@Ist_Date", Value = this.istDate, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });

                DataTable dtBlocks = SqlHelper.ExecuteSelect<SqlConnection>(sp_GetCropCoverageActualPanchtDelta, dbparamsCropInfoDAODeltaBlk, SqlHelper.ExecutionType.Procedure);
                DataTable dtPanchayats = SqlHelper.ExecuteSelect<SqlConnection>(sp_GetCropCoverageActualPanchtDelta, dbparamsCropInfoDAODeltaPanchyt, SqlHelper.ExecutionType.Procedure);
                DataTable dtVillages = SqlHelper.ExecuteSelect<SqlConnection>(sp_GetCropCoverageActualPanchtDelta, dbparamsCropInfoDAODeltaVillage, SqlHelper.ExecutionType.Procedure);

                if (cropCoverageAct != null && dtPanchayats.Rows.Count > 0)
                {
                    var actBlocks = SqlHelper.ConvertDataTableToList<DtoCoverageActualBlock>(dtBlocks);
                    var actPanchayats = SqlHelper.ConvertDataTableToList<DtoCoverageActualPanchayat>(dtPanchayats);
                    var actVillages = SqlHelper.ConvertDataTableToList<DtoCoverageActualVillage>(dtVillages);

                    foreach (var panchayat in actPanchayats)
                    {
                        panchayat.VillageList = actVillages.Where(x => x.Panchayat_Id == panchayat.Panchayat_Id).ToList();
                    }

                    List<CropCoverageActual> cropCoverageActualResponse = cropCoverageAct;

                    if (actBlocks.Any())
                    {
                        foreach (var item in actBlocks)
                        {
                            item.PanchayatList = actPanchayats.Where(x => x.Block_Id == item.Block_Id && x.Crop_id == item.Crop_id).ToList();
                        }

                        foreach (var item in cropCoverageActualResponse)
                        {
                            item.BlockList = actBlocks.Where(x => x.Crop_id == item.Crop_Id).Select(x =>
                             new CoverageBlock
                             {
                                 Reported_date = x.Reported_date,
                                 Block_Id = x.Block_Id,
                                 Block_Name = x.Block_Name,
                                 Area_Target = x.Area_Target,
                                 Cumm_Area_Prev = x.Cumm_Area_Prev,
                                 Cumm_Area_Curr = x.Cumm_Area_Curr,
                                 Refreshed_date = x.Refreshed_date,
                                 Refreshed_userid = x.Refreshed_userid,
                                 DAO_Approval_flag = x.DAO_Approval_flag,
                                 DAO_Approval_Reason = x.DAO_Approval_Reason,
                                 DAO_Approved_date = x.DAO_Approved_date,
                                 DAO_Approved_userid = x.DAO_Approved_userid,
                                 Refreshed_username = x.Refreshed_username,
                                 Dao_approved_username = x.Dao_approved_username,

                                 PanchayatList = x.PanchayatList.Any() ? x.PanchayatList.Select(x =>
                                   new CoveragePanchayat
                                   {
                                       Reported_date = x.Reported_date,
                                       Panchayat_Id = x.Panchayat_Id,
                                       Panchayat_Name = x.Panchayat_Name,
                                       Area_Target = x.Area_Target,
                                       Cumm_Area_Prev = x.Cumm_Area_Prev,
                                       Cumm_Area_Curr = x.Cumm_Area_Curr,
                                       Existing_area = x.Existing_area,
                                       AC_Submitted_date = x.AC_Submitted_date,
                                       AC_Submitted_userid = x.AC_Submitted_userid,
                                       BAO_Approval_flag = x.BAO_Approval_flag,
                                       BAO_Approval_Reason = x.BAO_Approval_Reason,
                                       BAO_Approved_date = x.BAO_Approved_date,
                                       BAO_Approved_userid = x.BAO_Approved_userid,
                                       DAO_Approval_flag = x.DAO_Approval_flag,
                                       DAO_Approval_Reason = x.DAO_Approval_Reason,
                                       DAO_Approved_date = x.DAO_Approved_date,
                                       DAO_Approved_userid = x.DAO_Approved_userid,
                                       Ac_submitted_username = x.Ac_submitted_username,
                                       Bao_approved_username = x.Bao_approved_username,
                                       Dao_approved_username = x.Dao_approved_username,
                                       Ac_submit_flag = x.Ac_submit_flag,
                                       Bao_add_edit_flag = x.Bao_add_edit_flag,
                                       Dao_add_edit_flag = x.Dao_add_edit_flag,
                                       Final_cvrg_flg = x.Final_cvrg_flg,
                                       Rec_updated_date = x.Rec_Updated_Date,
                                       Rec_updated_userid = x.Rec_Updated_Userid,
                                       UpdatedBy = x.UpdatedBy,
                                       VillageList = x.VillageList.Any() ? x.VillageList.Select(x =>
                                   new CoverageVillage
                                   {
                                       Reported_date = x.Reported_date,
                                       Village_Id = x.Village_Id,
                                       Village_Name = x.Village_Name,
                                       Area_Target = x.Area_Target,
                                       Cumm_Area_Prev = x.Cumm_Area_Prev,
                                       Cumm_Area_Curr = x.Cumm_Area_Curr,
                                       Existing_area = x.Existing_area,
                                       AC_Submitted_date = x.AC_Submitted_date,
                                       AC_Submitted_userid = x.AC_Submitted_userid,
                                       BAO_Approval_flag = x.BAO_Approval_flag,
                                       BAO_Approval_Reason = x.BAO_Approval_Reason,
                                       BAO_Approved_date = x.BAO_Approved_date,
                                       BAO_Approved_userid = x.BAO_Approved_userid,
                                       DAO_Approval_flag = x.DAO_Approval_flag,
                                       DAO_Approval_Reason = x.DAO_Approval_Reason,
                                       DAO_Approved_date = x.DAO_Approved_date,
                                       DAO_Approved_userid = x.DAO_Approved_userid,
                                       Ac_submitted_username = x.Ac_submitted_username,
                                       Bao_approved_username = x.Bao_approved_username,
                                       Dao_approved_username = x.Dao_approved_username,
                                       Ac_submit_flag = x.Ac_submit_flag,
                                       Bao_add_edit_flag = x.Bao_add_edit_flag,
                                       Dao_add_edit_flag = x.Dao_add_edit_flag,
                                       Final_cvrg_flg = x.Final_cvrg_flg,
                                       Rec_updated_userid = x.Rec_Updated_Userid,
                                       Rec_updated_date = x.Rec_Updated_Date,
                                       UpdatedBy = x.UpdatedBy,
                                   }).ToList() : null,
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
        /// GetCropCoverageActualBAOOffline.
        /// </summary>
        /// <param name="district_id">district_id.</param>
        /// <param name="block_id">block_id.</param>
        /// <param name="season_id">season_id.</param>
        /// <param name="crop_ids">crop_ids.</param>
        /// <returns>CropCoverageActual List.</returns>
        public List<CropCoverageActual> GetCropCoverageActualBAOOffline(int district_id, int block_id, int season_id, string crop_ids)
        {
            List<DbParameter> dbparamsCropInfoDAODeltaDist = new List<DbParameter>();
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@query_name", Value = qn_GetCropCoverageActualBAOOfflineDist, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@District_Id", Value = string.IsNullOrEmpty(district_id.ToString()) ? DBNull.Value : (object)district_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@Block_id", Value = string.IsNullOrEmpty(block_id.ToString()) ? DBNull.Value : (object)block_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@Panchayat_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@village_id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@Season_Id", Value = string.IsNullOrEmpty(season_id.ToString()) ? DBNull.Value : (object)season_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@Crop_id", Value = string.IsNullOrEmpty(crop_ids.ToString()) ? DBNull.Value : (object)crop_ids.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@Last_Refresh_Ts", Value = DBNull.Value, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@Ist_Date", Value = this.istDate, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
            DataTable dtDistricts = SqlHelper.ExecuteSelect<SqlConnection>("usp_crop_cntrlr_cvrg_get", dbparamsCropInfoDAODeltaDist, SqlHelper.ExecutionType.Procedure);

            if (dtDistricts.Rows.Count > 0)
            {
                var cropCoverageAct = dtDistricts.AsEnumerable().Any() ? SqlHelper.ConvertDataTableToList<CropCoverageActual>(dtDistricts) : null;
                List<DbParameter> dbparamsCropInfoDAODeltaBlk = new List<DbParameter>();
                dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@query_name", Value = qn_GetCropCoverageActualBAOOfflineBlk, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@District_Id", Value = string.IsNullOrEmpty(district_id.ToString()) ? DBNull.Value : (object)district_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@Block_id", Value = string.IsNullOrEmpty(block_id.ToString()) ? DBNull.Value : (object)block_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@Panchayat_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@village_id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@Season_Id", Value = string.IsNullOrEmpty(season_id.ToString()) ? DBNull.Value : (object)season_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@Crop_id", Value = string.IsNullOrEmpty(crop_ids.ToString()) ? DBNull.Value : (object)crop_ids.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@Last_Refresh_Ts", Value = DBNull.Value, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@Ist_Date", Value = this.istDate, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });

                List<DbParameter> dbparamsCropInfoDAODeltaPanchyt = new List<DbParameter>();
                dbparamsCropInfoDAODeltaPanchyt.Add(new SqlParameter { ParameterName = "@query_name", Value = qn_GetCropCoverageActualBAOOfflinePanchyt, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaPanchyt.Add(new SqlParameter { ParameterName = "@District_Id", Value = string.IsNullOrEmpty(district_id.ToString()) ? DBNull.Value : (object)district_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaPanchyt.Add(new SqlParameter { ParameterName = "@Block_id", Value = string.IsNullOrEmpty(block_id.ToString()) ? DBNull.Value : (object)block_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaPanchyt.Add(new SqlParameter { ParameterName = "@Panchayat_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaPanchyt.Add(new SqlParameter { ParameterName = "@village_id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaPanchyt.Add(new SqlParameter { ParameterName = "@Season_Id", Value = string.IsNullOrEmpty(season_id.ToString()) ? DBNull.Value : (object)season_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaPanchyt.Add(new SqlParameter { ParameterName = "@Crop_id", Value = string.IsNullOrEmpty(crop_ids.ToString()) ? DBNull.Value : (object)crop_ids.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaPanchyt.Add(new SqlParameter { ParameterName = "@Last_Refresh_Ts", Value = DBNull.Value, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaPanchyt.Add(new SqlParameter { ParameterName = "@Ist_Date", Value = this.istDate, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });

                List<DbParameter> dbparamsCropInfoDAODeltaVillage = new List<DbParameter>();
                dbparamsCropInfoDAODeltaVillage.Add(new SqlParameter { ParameterName = "@query_name", Value = qn_GetCropCoverageActualBAOOfflineVillage, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaVillage.Add(new SqlParameter { ParameterName = "@District_Id", Value = string.IsNullOrEmpty(district_id.ToString()) ? DBNull.Value : (object)district_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaVillage.Add(new SqlParameter { ParameterName = "@Block_id", Value = string.IsNullOrEmpty(block_id.ToString()) ? DBNull.Value : (object)block_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaVillage.Add(new SqlParameter { ParameterName = "@Panchayat_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaVillage.Add(new SqlParameter { ParameterName = "@village_id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaVillage.Add(new SqlParameter { ParameterName = "@Season_Id", Value = string.IsNullOrEmpty(season_id.ToString()) ? DBNull.Value : (object)season_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaVillage.Add(new SqlParameter { ParameterName = "@Crop_id", Value = string.IsNullOrEmpty(crop_ids.ToString()) ? DBNull.Value : (object)crop_ids.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaVillage.Add(new SqlParameter { ParameterName = "@Last_Refresh_Ts", Value = DBNull.Value, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaVillage.Add(new SqlParameter { ParameterName = "@Ist_Date", Value = this.istDate, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });

                DataTable dtBlocks = SqlHelper.ExecuteSelect<SqlConnection>(sp_GetCropCoverageActualPanchtDelta, dbparamsCropInfoDAODeltaBlk, SqlHelper.ExecutionType.Procedure);
                DataTable dtPanchayats = SqlHelper.ExecuteSelect<SqlConnection>(sp_GetCropCoverageActualPanchtDelta, dbparamsCropInfoDAODeltaPanchyt, SqlHelper.ExecutionType.Procedure);
                DataTable dtVillages = SqlHelper.ExecuteSelect<SqlConnection>(sp_GetCropCoverageActualPanchtDelta, dbparamsCropInfoDAODeltaVillage, SqlHelper.ExecutionType.Procedure);

                if (cropCoverageAct != null && dtPanchayats.Rows.Count > 0)
                {
                    var actBlocks = SqlHelper.ConvertDataTableToList<DtoCoverageActualBlock>(dtBlocks);
                    var actPanchayats = SqlHelper.ConvertDataTableToList<DtoCoverageActualPanchayat>(dtPanchayats);
                    var actVillages = SqlHelper.ConvertDataTableToList<DtoCoverageActualVillage>(dtVillages);

                    foreach (var panchayat in actPanchayats)
                    {
                        panchayat.VillageList = actVillages.Where(x => x.Panchayat_Id == panchayat.Panchayat_Id).ToList();
                    }

                    List<CropCoverageActual> cropCoverageActualResponse = cropCoverageAct;

                    if (actBlocks.Any())
                    {
                        foreach (var item in actBlocks)
                        {
                            item.PanchayatList = actPanchayats.Where(x => x.Block_Id == item.Block_Id && x.Crop_id == item.Crop_id).ToList();
                        }

                        foreach (var item in cropCoverageActualResponse)
                        {
                            item.BlockList = actBlocks.Where(x => x.Crop_id == item.Crop_Id).Select(x =>
                             new CoverageBlock
                             {
                                 Reported_date = x.Reported_date,
                                 Block_Id = x.Block_Id,
                                 Block_Name = x.Block_Name,
                                 Area_Target = x.Area_Target,
                                 Cumm_Area_Prev = x.Cumm_Area_Prev,
                                 Cumm_Area_Curr = x.Cumm_Area_Curr,
                                 Refreshed_date = x.Refreshed_date,
                                 Refreshed_userid = x.Refreshed_userid,
                                 DAO_Approval_flag = x.DAO_Approval_flag,
                                 DAO_Approval_Reason = x.DAO_Approval_Reason,
                                 DAO_Approved_date = x.DAO_Approved_date,
                                 DAO_Approved_userid = x.DAO_Approved_userid,
                                 Refreshed_username = x.Refreshed_username,
                                 Dao_approved_username = x.Dao_approved_username,

                                 PanchayatList = x.PanchayatList.Any() ? x.PanchayatList.Select(x =>
                                   new CoveragePanchayat
                                   {
                                       Reported_date = x.Reported_date,
                                       Panchayat_Id = x.Panchayat_Id,
                                       Panchayat_Name = x.Panchayat_Name,
                                       Area_Target = x.Area_Target,
                                       Existing_area = x.Existing_area,
                                       Cumm_Area_Prev = x.Cumm_Area_Prev,
                                       Cumm_Area_Curr = x.Cumm_Area_Curr,
                                       AC_Submitted_date = x.AC_Submitted_date,
                                       AC_Submitted_userid = x.AC_Submitted_userid,
                                       BAO_Approval_flag = x.BAO_Approval_flag,
                                       BAO_Approval_Reason = x.BAO_Approval_Reason,
                                       BAO_Approved_date = x.BAO_Approved_date,
                                       BAO_Approved_userid = x.BAO_Approved_userid,
                                       DAO_Approval_flag = x.DAO_Approval_flag,
                                       DAO_Approval_Reason = x.DAO_Approval_Reason,
                                       DAO_Approved_date = x.DAO_Approved_date,
                                       DAO_Approved_userid = x.DAO_Approved_userid,
                                       Ac_submitted_username = x.Ac_submitted_username,
                                       Bao_approved_username = x.Bao_approved_username,
                                       Dao_approved_username = x.Dao_approved_username,
                                       Ac_submit_flag = x.Ac_submit_flag,
                                       Bao_add_edit_flag = x.Bao_add_edit_flag,
                                       Dao_add_edit_flag = x.Dao_add_edit_flag,
                                       Final_cvrg_flg = x.Final_cvrg_flg,
                                       Rec_updated_date = x.Rec_Updated_Date,
                                       Rec_updated_userid = x.Rec_Updated_Userid,
                                       UpdatedBy = x.UpdatedBy,
                                       VillageList = x.VillageList.Any() ? x.VillageList.Select(x =>
                               new CoverageVillage
                               {
                                   Reported_date = x.Reported_date,
                                   Village_Id = x.Village_Id,
                                   Village_Name = x.Village_Name,
                                   Area_Target = x.Area_Target,
                                   Existing_area = x.Existing_area,
                                   Cumm_Area_Prev = x.Cumm_Area_Prev,
                                   Cumm_Area_Curr = x.Cumm_Area_Curr,
                                   AC_Submitted_date = x.AC_Submitted_date,
                                   AC_Submitted_userid = x.AC_Submitted_userid,
                                   BAO_Approval_flag = x.BAO_Approval_flag,
                                   BAO_Approval_Reason = x.BAO_Approval_Reason,
                                   BAO_Approved_date = x.BAO_Approved_date,
                                   BAO_Approved_userid = x.BAO_Approved_userid,
                                   DAO_Approval_flag = x.DAO_Approval_flag,
                                   DAO_Approval_Reason = x.DAO_Approval_Reason,
                                   DAO_Approved_date = x.DAO_Approved_date,
                                   DAO_Approved_userid = x.DAO_Approved_userid,
                                   Ac_submitted_username = x.Ac_submitted_username,
                                   Bao_approved_username = x.Bao_approved_username,
                                   Dao_approved_username = x.Dao_approved_username,
                                   Ac_submit_flag = x.Ac_submit_flag,
                                   Bao_add_edit_flag = x.Bao_add_edit_flag,
                                   Dao_add_edit_flag = x.Dao_add_edit_flag,
                                   Final_cvrg_flg = x.Final_cvrg_flg,
                                   Rec_updated_date = x.Rec_Updated_Date,
                                   Rec_updated_userid = x.Rec_Updated_Userid,
                                   UpdatedBy = x.UpdatedBy,
                               }).ToList() : null,
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
        /// GetCropCoverageActualBAO.
        /// </summary>
        /// <param name="district_id">district_id.</param>
        /// <param name="block_id">block_id.</param>
        /// <param name="season_id">season_id.</param>
        /// <param name="crop_id">crop_id.</param>
        /// <returns>CropCoverageActual List.</returns>
        public CropCoverageActual GetCropCoverageActualBAO(int district_id, int block_id, int season_id, int crop_id)
        {
            List<DbParameter> dbparamsCropInfoDAODeltaDist = new List<DbParameter>();
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@query_name", Value = qn_GetCropCoverageActualBAODist, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@District_Id", Value = string.IsNullOrEmpty(district_id.ToString()) ? DBNull.Value : (object)district_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@Block_id", Value = string.IsNullOrEmpty(block_id.ToString()) ? DBNull.Value : (object)block_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@Panchayat_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@village_id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@Season_Id", Value = string.IsNullOrEmpty(season_id.ToString()) ? DBNull.Value : (object)season_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@Crop_id", Value = string.IsNullOrEmpty(crop_id.ToString()) ? DBNull.Value : (object)crop_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@Last_Refresh_Ts", Value = DBNull.Value, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@Ist_Date", Value = this.istDate, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });

            DataTable dtDistricts = SqlHelper.ExecuteSelect<SqlConnection>(sp_GetCropCoverageActualPanchtDelta, dbparamsCropInfoDAODeltaDist, SqlHelper.ExecutionType.Procedure);

            if (dtDistricts.Rows.Count > 0)
            {
                var cropCoverageAct = dtDistricts.AsEnumerable().Any() ? SqlHelper.ConvertDataTableToList<CropCoverageActual>(dtDistricts)[0] : null;

                List<DbParameter> dbparamsCropInfoDAODeltaBlk = new List<DbParameter>();
                dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@query_name", Value = qn_GetCropCoverageActualBAOBlk, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@District_Id", Value = string.IsNullOrEmpty(district_id.ToString()) ? DBNull.Value : (object)district_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@Block_id", Value = string.IsNullOrEmpty(block_id.ToString()) ? DBNull.Value : (object)block_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@Panchayat_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@village_id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@Season_Id", Value = string.IsNullOrEmpty(season_id.ToString()) ? DBNull.Value : (object)season_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@Crop_id", Value = string.IsNullOrEmpty(crop_id.ToString()) ? DBNull.Value : (object)crop_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@Last_Refresh_Ts", Value = DBNull.Value, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@Ist_Date", Value = this.istDate, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });

                List<DbParameter> dbparamsCropInfoDAODeltaPanchyt = new List<DbParameter>();
                dbparamsCropInfoDAODeltaPanchyt.Add(new SqlParameter { ParameterName = "@query_name", Value = qn_GetCropCoverageActualBAOPanchyt, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaPanchyt.Add(new SqlParameter { ParameterName = "@District_Id", Value = string.IsNullOrEmpty(district_id.ToString()) ? DBNull.Value : (object)district_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaPanchyt.Add(new SqlParameter { ParameterName = "@Block_id", Value = string.IsNullOrEmpty(block_id.ToString()) ? DBNull.Value : (object)block_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaPanchyt.Add(new SqlParameter { ParameterName = "@Panchayat_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaPanchyt.Add(new SqlParameter { ParameterName = "@village_id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaPanchyt.Add(new SqlParameter { ParameterName = "@Season_Id", Value = string.IsNullOrEmpty(season_id.ToString()) ? DBNull.Value : (object)season_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaPanchyt.Add(new SqlParameter { ParameterName = "@Crop_id", Value = string.IsNullOrEmpty(crop_id.ToString()) ? DBNull.Value : (object)crop_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaPanchyt.Add(new SqlParameter { ParameterName = "@Last_Refresh_Ts", Value = DBNull.Value, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaPanchyt.Add(new SqlParameter { ParameterName = "@Ist_Date", Value = this.istDate, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });

                List<DbParameter> dbparamsCropInfoDAODeltaVillage = new List<DbParameter>();
                dbparamsCropInfoDAODeltaVillage.Add(new SqlParameter { ParameterName = "@query_name", Value = qn_GetCropCoverageActualBAOVillage, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaVillage.Add(new SqlParameter { ParameterName = "@District_Id", Value = string.IsNullOrEmpty(district_id.ToString()) ? DBNull.Value : (object)district_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaVillage.Add(new SqlParameter { ParameterName = "@Block_id", Value = string.IsNullOrEmpty(block_id.ToString()) ? DBNull.Value : (object)block_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaVillage.Add(new SqlParameter { ParameterName = "@Panchayat_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaVillage.Add(new SqlParameter { ParameterName = "@village_id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaVillage.Add(new SqlParameter { ParameterName = "@Season_Id", Value = string.IsNullOrEmpty(season_id.ToString()) ? DBNull.Value : (object)season_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaVillage.Add(new SqlParameter { ParameterName = "@Crop_id", Value = string.IsNullOrEmpty(crop_id.ToString()) ? DBNull.Value : (object)crop_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaVillage.Add(new SqlParameter { ParameterName = "@Last_Refresh_Ts", Value = DBNull.Value, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
                dbparamsCropInfoDAODeltaVillage.Add(new SqlParameter { ParameterName = "@Ist_Date", Value = this.istDate, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });

                DataTable dtBlocks = SqlHelper.ExecuteSelect<SqlConnection>(sp_GetCropCoverageActualPanchtDelta, dbparamsCropInfoDAODeltaBlk, SqlHelper.ExecutionType.Procedure);
                DataTable dtPanchayats = SqlHelper.ExecuteSelect<SqlConnection>(sp_GetCropCoverageActualPanchtDelta, dbparamsCropInfoDAODeltaPanchyt, SqlHelper.ExecutionType.Procedure);
                DataTable dtVillages = SqlHelper.ExecuteSelect<SqlConnection>(sp_GetCropCoverageActualPanchtDelta, dbparamsCropInfoDAODeltaVillage, SqlHelper.ExecutionType.Procedure);

                if (cropCoverageAct != null && dtPanchayats.Rows.Count > 0)
                {
                    var actBlocks = SqlHelper.ConvertDataTableToList<DtoCoverageActualBlock>(dtBlocks);
                    var actPanchayats = SqlHelper.ConvertDataTableToList<DtoCoverageActualPanchayat>(dtPanchayats);
                    var actVillages = SqlHelper.ConvertDataTableToList<DtoCoverageActualVillage>(dtVillages);

                    foreach (var panchayat in actPanchayats)
                    {
                        panchayat.VillageList = actVillages.Where(x => x.Panchayat_Id == panchayat.Panchayat_Id).ToList();
                    }

                    CropCoverageActual cropCoverageActualResponse = cropCoverageAct;

                    if (actBlocks.Any())
                    {
                        cropCoverageActualResponse.BlockList = new List<CoverageBlock>();

                        foreach (var item in actBlocks)
                        {
                            item.PanchayatList = actPanchayats.Where(x => x.Block_Id == item.Block_Id).ToList();
                        }

                        cropCoverageActualResponse.BlockList = actBlocks.Select(x =>
                         new CoverageBlock
                         {
                             Reported_date = x.Reported_date,
                             Block_Id = x.Block_Id,
                             Block_Name = x.Block_Name,
                             Area_Target = x.Area_Target,
                             Cumm_Area_Prev = x.Cumm_Area_Prev,
                             Cumm_Area_Curr = x.Cumm_Area_Curr,
                             Refreshed_date = x.Refreshed_date,
                             Refreshed_userid = x.Refreshed_userid,
                             DAO_Approval_flag = x.DAO_Approval_flag,
                             DAO_Approval_Reason = x.DAO_Approval_Reason,
                             DAO_Approved_date = x.DAO_Approved_date,
                             DAO_Approved_userid = x.DAO_Approved_userid,
                             Refreshed_username = x.Refreshed_username,
                             Dao_approved_username = x.Dao_approved_username,

                             PanchayatList = x.PanchayatList.Any() ? x.PanchayatList.Select(x =>
                               new CoveragePanchayat
                               {
                                   Reported_date = x.Reported_date,
                                   Panchayat_Id = x.Panchayat_Id,
                                   Panchayat_Name = x.Panchayat_Name,
                                   Area_Target = x.Area_Target,
                                   Existing_area = x.Existing_area,
                                   Cumm_Area_Prev = x.Cumm_Area_Prev,
                                   Cumm_Area_Curr = x.Cumm_Area_Curr,
                                   AC_Submitted_date = x.AC_Submitted_date,
                                   AC_Submitted_userid = x.AC_Submitted_userid,
                                   BAO_Approval_flag = x.BAO_Approval_flag,
                                   BAO_Approval_Reason = x.BAO_Approval_Reason,
                                   BAO_Approved_date = x.BAO_Approved_date,
                                   BAO_Approved_userid = x.BAO_Approved_userid,
                                   DAO_Approval_flag = x.DAO_Approval_flag,
                                   DAO_Approval_Reason = x.DAO_Approval_Reason,
                                   DAO_Approved_date = x.DAO_Approved_date,
                                   DAO_Approved_userid = x.DAO_Approved_userid,
                                   Ac_submitted_username = x.Ac_submitted_username,
                                   Bao_approved_username = x.Bao_approved_username,
                                   Dao_approved_username = x.Dao_approved_username,
                                   Ac_submit_flag = x.Ac_submit_flag,
                                   Bao_add_edit_flag = x.Bao_add_edit_flag,
                                   Dao_add_edit_flag = x.Dao_add_edit_flag,
                                   Final_cvrg_flg = x.Final_cvrg_flg,
                                   Rec_updated_date = x.Rec_Updated_Date,
                                   Rec_updated_userid = x.Rec_Updated_Userid,
                                   UpdatedBy = x.UpdatedBy,
                                   VillageList = x.VillageList.Any() ? x.VillageList.Select(x =>
                               new CoverageVillage
                               {
                                   Reported_date = x.Reported_date,
                                   Village_Id = x.Village_Id,
                                   Village_Name = x.Village_Name,
                                   Area_Target = x.Area_Target,
                                   Existing_area = x.Existing_area,
                                   Cumm_Area_Prev = x.Cumm_Area_Prev,
                                   Cumm_Area_Curr = x.Cumm_Area_Curr,
                                   AC_Submitted_date = x.AC_Submitted_date,
                                   AC_Submitted_userid = x.AC_Submitted_userid,
                                   BAO_Approval_flag = x.BAO_Approval_flag,
                                   BAO_Approval_Reason = x.BAO_Approval_Reason,
                                   BAO_Approved_date = x.BAO_Approved_date,
                                   BAO_Approved_userid = x.BAO_Approved_userid,
                                   DAO_Approval_flag = x.DAO_Approval_flag,
                                   DAO_Approval_Reason = x.DAO_Approval_Reason,
                                   DAO_Approved_date = x.DAO_Approved_date,
                                   DAO_Approved_userid = x.DAO_Approved_userid,
                                   Ac_submitted_username = x.Ac_submitted_username,
                                   Bao_approved_username = x.Bao_approved_username,
                                   Dao_approved_username = x.Dao_approved_username,
                                   Ac_submit_flag = x.Ac_submit_flag,
                                   Bao_add_edit_flag = x.Bao_add_edit_flag,
                                   Dao_add_edit_flag = x.Dao_add_edit_flag,
                                   Final_cvrg_flg = x.Final_cvrg_flg,
                                   Rec_updated_date = x.Rec_Updated_Date,
                                   Rec_updated_userid = x.Rec_Updated_Userid,
                                   UpdatedBy = x.UpdatedBy,
                               }).ToList() : null,
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
        /// GetCropDamageBAO.
        /// </summary>
        /// <param name="district_id">district_id.</param>
        /// <param name="block_id">block_id.</param>
        /// <param name="season_id">season_id.</param>
        /// <param name="crop_id">crop_id.</param>
        /// <returns>CropDamage List.</returns>
        public CropDamage GetCropDamageBAO(int district_id, int block_id, int season_id, int crop_id)
        {
            List<DbParameter> dbparamsCropInfoDAODeltaDist = new List<DbParameter>();
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@query_name", Value = qn_GetCropDamageBAODist, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@District_Id", Value = string.IsNullOrEmpty(district_id.ToString()) ? DBNull.Value : (object)district_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@Block_id", Value = string.IsNullOrEmpty(block_id.ToString()) ? DBNull.Value : (object)block_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@Panchayat_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@Season_Id", Value = string.IsNullOrEmpty(season_id.ToString()) ? DBNull.Value : (object)season_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@Crop_id", Value = string.IsNullOrEmpty(crop_id.ToString()) ? DBNull.Value : (object)crop_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@Last_Refresh_Ts", Value = DBNull.Value, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });

            List<DbParameter> dbparamsCropInfoDAODeltaBlk = new List<DbParameter>();
            dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@query_name", Value = qn_GetCropDamageBAOBlk, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@District_Id", Value = string.IsNullOrEmpty(district_id.ToString()) ? DBNull.Value : (object)district_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@Block_id", Value = string.IsNullOrEmpty(block_id.ToString()) ? DBNull.Value : (object)block_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@Panchayat_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@Season_Id", Value = string.IsNullOrEmpty(season_id.ToString()) ? DBNull.Value : (object)season_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@Crop_id", Value = string.IsNullOrEmpty(crop_id.ToString()) ? DBNull.Value : (object)crop_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@Last_Refresh_Ts", Value = DBNull.Value, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });

            List<DbParameter> dbparamsCropInfoDAODeltaPanchayat = new List<DbParameter>();
            dbparamsCropInfoDAODeltaPanchayat.Add(new SqlParameter { ParameterName = "@query_name", Value = qn_GetCropDamageBAOPanchyt, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaPanchayat.Add(new SqlParameter { ParameterName = "@District_Id", Value = string.IsNullOrEmpty(district_id.ToString()) ? DBNull.Value : (object)district_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaPanchayat.Add(new SqlParameter { ParameterName = "@Block_id", Value = string.IsNullOrEmpty(block_id.ToString()) ? DBNull.Value : (object)block_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaPanchayat.Add(new SqlParameter { ParameterName = "@Panchayat_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaPanchayat.Add(new SqlParameter { ParameterName = "@Season_Id", Value = string.IsNullOrEmpty(season_id.ToString()) ? DBNull.Value : (object)season_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaPanchayat.Add(new SqlParameter { ParameterName = "@Crop_id", Value = string.IsNullOrEmpty(crop_id.ToString()) ? DBNull.Value : (object)crop_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaPanchayat.Add(new SqlParameter { ParameterName = "@Last_Refresh_Ts", Value = DBNull.Value, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });

            DataTable dtDistricts = SqlHelper.ExecuteSelect<SqlConnection>(sp_GetCropDamagePancht, dbparamsCropInfoDAODeltaDist, SqlHelper.ExecutionType.Procedure);
            DataTable dtBlocks = SqlHelper.ExecuteSelect<SqlConnection>(sp_GetCropDamagePancht, dbparamsCropInfoDAODeltaBlk, SqlHelper.ExecutionType.Procedure);
            DataTable dtPanchayats = SqlHelper.ExecuteSelect<SqlConnection>(sp_GetCropDamagePancht, dbparamsCropInfoDAODeltaPanchayat, SqlHelper.ExecutionType.Procedure);
            var cropDamage = dtDistricts.AsEnumerable().Any() ? SqlHelper.ConvertDataTableToList<CropDamage>(dtDistricts)[0] : null;

            if (cropDamage != null && dtPanchayats.Rows.Count > 0)
            {
                var cropDamageBlocks = SqlHelper.ConvertDataTableToList<DtoCropDamageBlock>(dtBlocks);
                var cropDamagePanchayats = SqlHelper.ConvertDataTableToList<DtoCropDamagePanchayat>(dtPanchayats);
                CropDamage cropDamageResponse = cropDamage;

                if (cropDamageBlocks.Any())
                {
                    cropDamageResponse.BlockList = new List<CropDmgBlock>();

                    foreach (var item in cropDamageBlocks)
                    {
                        item.PanchayatList = cropDamagePanchayats.Where(x => x.Block_Id == item.Block_Id).ToList();
                    }

                    cropDamageResponse.BlockList = cropDamageBlocks.Select(x =>
                     new CropDmgBlock
                     {
                         Block_Id = x.Block_Id,
                         Block_Name = x.Block_Name,
                         Irrigated_Dmg_Area = x.Irrigated_Dmg_Area,
                         Damage_Reason = x.Damage_Reason,
                         Refreshed_date = x.Refreshed_date,
                         Refreshed_userid = x.Refreshed_userid,
                         DAO_Approval_flag = x.DAO_Approval_flag,
                         DAO_Approval_Reason = x.DAO_Approval_Reason,
                         DAO_Approved_date = x.DAO_Approved_date,
                         DAO_Approved_userid = x.DAO_Approved_userid,
                         Refreshed_username = x.Refreshed_username,
                         Dao_approved_username = x.Dao_approved_username,

                         PanchayatList = x.PanchayatList.Any() ? x.PanchayatList.Select(x =>
                           new CropDmgPanchayat
                           {
                               Panchayat_Id = x.Panchayat_Id,
                               Panchayat_Name = x.Panchayat_Name,
                               Area_covered = x.Area_covered,
                               Dmg_Till_Date = x.Dmg_Till_Date,
                           }).GroupBy(x => x.Panchayat_Id).Select(x => x.First())
                     .ToList() : null,
                     }).ToList();
                    foreach (var item in cropDamageResponse.BlockList)
                    {
                        foreach (var panchayt in item.PanchayatList)
                        {
                            panchayt.DamageList = cropDamagePanchayats.Where(x => x.Panchayat_Id == panchayt.Panchayat_Id).Select(
                                x => new DamageEntity
                                {
                                    Damage_Reason = x.Damage_Reason,
                                    Irrigated_Dmg_Area = x.Irrigated_Dmg_Area,
                                    AC_Submitted_date = x.AC_Submitted_date,
                                    AC_Submitted_userid = x.AC_Submitted_userid,
                                    BAO_Approval_flag = x.BAO_Approval_flag,
                                    BAO_Approval_Reason = x.BAO_Approval_Reason,
                                    BAO_Approved_date = x.BAO_Approved_date,
                                    BAO_Approved_userid = x.BAO_Approved_userid,
                                    DAO_Approval_flag = x.DAO_Approval_flag,
                                    DAO_Approval_Reason = x.DAO_Approval_Reason,
                                    DAO_Approved_date = x.DAO_Approved_date,
                                    DAO_Approved_userid = x.DAO_Approved_userid,
                                    Ac_submitted_username = x.Ac_submitted_username,
                                    Bao_approved_username = x.Bao_approved_username,
                                    Dao_approved_username = x.Dao_approved_username,
                                    Ac_submit_flag = x.Ac_submit_flag,
                                    Dao_add_edit_flag = x.Dao_add_edit_flag,
                                    Rec_updated_userid = x.Rec_updated_userid,
                                    Bao_add_edit_flag = x.Bao_add_edit_flag,
                                    Rec_updated_date = x.Rec_updated_date,
                                    UpdatedBy = x.UpdatedBy,
                                }).ToList();
                        }
                    }
                }

                return cropDamageResponse;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// GetCropDamageBAOOffline.
        /// </summary>
        /// <param name="district_id">district_id.</param>
        /// <param name="block_id">block_id.</param>
        /// <param name="season_id">season_id.</param>
        /// <param name="crop_ids">crop_ids.</param>
        /// <returns>CropDamage List.</returns>
        public List<CropDamage> GetCropDamageBAOOffline(int district_id, int block_id, int season_id, string crop_ids)
        {
            List<DbParameter> dbparamsCropInfoDAODeltaDist = new List<DbParameter>();
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@query_name", Value = qn_GetCropDamageBAOOfflineDist, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@District_Id", Value = string.IsNullOrEmpty(district_id.ToString()) ? DBNull.Value : (object)district_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@Block_id", Value = string.IsNullOrEmpty(block_id.ToString()) ? DBNull.Value : (object)block_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@Panchayat_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@Season_Id", Value = string.IsNullOrEmpty(season_id.ToString()) ? DBNull.Value : (object)season_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@Crop_id", Value = string.IsNullOrEmpty(crop_ids.ToString()) ? DBNull.Value : (object)crop_ids.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaDist.Add(new SqlParameter { ParameterName = "@Last_Refresh_Ts", Value = DBNull.Value, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });

            List<DbParameter> dbparamsCropInfoDAODeltaBlk = new List<DbParameter>();
            dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@query_name", Value = qn_GetCropDamageBAOOfflineBlk, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@District_Id", Value = string.IsNullOrEmpty(district_id.ToString()) ? DBNull.Value : (object)district_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@Block_id", Value = string.IsNullOrEmpty(block_id.ToString()) ? DBNull.Value : (object)block_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@Panchayat_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@Season_Id", Value = string.IsNullOrEmpty(season_id.ToString()) ? DBNull.Value : (object)season_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@Crop_id", Value = string.IsNullOrEmpty(crop_ids.ToString()) ? DBNull.Value : (object)crop_ids.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaBlk.Add(new SqlParameter { ParameterName = "@Last_Refresh_Ts", Value = DBNull.Value, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });

            List<DbParameter> dbparamsCropInfoDAODeltaPanchayat = new List<DbParameter>();
            dbparamsCropInfoDAODeltaPanchayat.Add(new SqlParameter { ParameterName = "@query_name", Value = qn_GetCropDamageBAOOfflinePanchyt, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaPanchayat.Add(new SqlParameter { ParameterName = "@District_Id", Value = string.IsNullOrEmpty(district_id.ToString()) ? DBNull.Value : (object)district_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaPanchayat.Add(new SqlParameter { ParameterName = "@Block_id", Value = string.IsNullOrEmpty(block_id.ToString()) ? DBNull.Value : (object)block_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaPanchayat.Add(new SqlParameter { ParameterName = "@Panchayat_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaPanchayat.Add(new SqlParameter { ParameterName = "@Season_Id", Value = string.IsNullOrEmpty(season_id.ToString()) ? DBNull.Value : (object)season_id.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaPanchayat.Add(new SqlParameter { ParameterName = "@Crop_id", Value = string.IsNullOrEmpty(crop_ids.ToString()) ? DBNull.Value : (object)crop_ids.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoDAODeltaPanchayat.Add(new SqlParameter { ParameterName = "@Last_Refresh_Ts", Value = DBNull.Value, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });

            DataTable dtDistricts = SqlHelper.ExecuteSelect<SqlConnection>(sp_GetCropDamagePancht, dbparamsCropInfoDAODeltaDist, SqlHelper.ExecutionType.Procedure);
            DataTable dtBlocks = SqlHelper.ExecuteSelect<SqlConnection>(sp_GetCropDamagePancht, dbparamsCropInfoDAODeltaBlk, SqlHelper.ExecutionType.Procedure);
            DataTable dtPanchayats = SqlHelper.ExecuteSelect<SqlConnection>(sp_GetCropDamagePancht, dbparamsCropInfoDAODeltaPanchayat, SqlHelper.ExecutionType.Procedure);
            var cropDamage = dtDistricts.AsEnumerable().Any() ? SqlHelper.ConvertDataTableToList<CropDamage>(dtDistricts) : null;

            if (cropDamage != null && dtPanchayats.Rows.Count > 0)
            {
                var cropDamageBlocks = SqlHelper.ConvertDataTableToList<DtoCropDamageBlock>(dtBlocks);
                var cropDamagePanchayats = SqlHelper.ConvertDataTableToList<DtoCropDamagePanchayat>(dtPanchayats);
                List<CropDamage> cropDamageResponse = cropDamage;

                if (cropDamageBlocks.Any())
                {
                    foreach (var item in cropDamageBlocks)
                    {
                        item.PanchayatList = cropDamagePanchayats.Where(x => x.Block_Id == item.Block_Id && x.Crop_id == item.Crop_id).ToList();
                    }

                    foreach (var item in cropDamageResponse)
                    {
                        item.BlockList = cropDamageBlocks.Where(x => x.Crop_id == item.Crop_Id).Select(x =>
                         new CropDmgBlock
                         {
                             Block_Id = x.Block_Id,
                             Block_Name = x.Block_Name,
                             Irrigated_Dmg_Area = x.Irrigated_Dmg_Area,
                             Damage_Reason = x.Damage_Reason,
                             Refreshed_date = x.Refreshed_date,
                             Refreshed_userid = x.Refreshed_userid,
                             DAO_Approval_flag = x.DAO_Approval_flag,
                             DAO_Approval_Reason = x.DAO_Approval_Reason,
                             DAO_Approved_date = x.DAO_Approved_date,
                             DAO_Approved_userid = x.DAO_Approved_userid,
                             Refreshed_username = x.Refreshed_username,
                             Dao_approved_username = x.Dao_approved_username,

                             PanchayatList = x.PanchayatList.Any() ? x.PanchayatList.Select(x =>
                               new CropDmgPanchayat
                               {
                                   Reported_date = x.Reported_date,
                                   Panchayat_Id = x.Panchayat_Id,
                                   Panchayat_Name = x.Panchayat_Name,
                                   Area_covered = x.Area_covered,
                                   Dmg_Till_Date = x.Dmg_Till_Date,
                               }).GroupBy(x => x.Panchayat_Id).Select(x => x.First())
                         .ToList() : null,
                         }).ToList();
                    }

                    foreach (var item in cropDamageResponse)
                    {
                        foreach (var blkiten in item.BlockList)
                        {
                            foreach (var panchayt in blkiten.PanchayatList)
                            {
                                panchayt.DamageList = cropDamagePanchayats.Where(x => x.Panchayat_Id == panchayt.Panchayat_Id && x.Crop_id == item.Crop_Id).Select(x => new DamageEntity
                                {
                                    Reported_Date = x.Reported_date,
                                    Damage_Reason = x.Damage_Reason,
                                    Irrigated_Dmg_Area = x.Irrigated_Dmg_Area,
                                    AC_Submitted_date = x.AC_Submitted_date,
                                    AC_Submitted_userid = x.AC_Submitted_userid,
                                    BAO_Approval_flag = x.BAO_Approval_flag,
                                    BAO_Approval_Reason = x.BAO_Approval_Reason,
                                    BAO_Approved_date = x.BAO_Approved_date,
                                    BAO_Approved_userid = x.BAO_Approved_userid,
                                    DAO_Approval_flag = x.DAO_Approval_flag,
                                    DAO_Approval_Reason = x.DAO_Approval_Reason,
                                    DAO_Approved_date = x.DAO_Approved_date,
                                    DAO_Approved_userid = x.DAO_Approved_userid,
                                    Ac_submitted_username = x.Ac_submitted_username,
                                    Bao_approved_username = x.Bao_approved_username,
                                    Dao_approved_username = x.Dao_approved_username,
                                    Ac_submit_flag = x.Ac_submit_flag,
                                    Bao_add_edit_flag = x.Bao_add_edit_flag,
                                    Dao_add_edit_flag = x.Dao_add_edit_flag,
                                    Rec_updated_date = x.Rec_updated_date,
                                    Rec_updated_userid = x.Rec_updated_userid,
                                    Submission_source = x.Submission_source,
                                    UpdatedBy = x.UpdatedBy,
                                }).ToList();
                            }
                        }
                    }
                }

                return cropDamageResponse;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// InsertCropDim.
        /// </summary>
        /// <param name="crop">crop.</param>
        /// <returns>Success values.</returns>
        public int InsertCropDim(CropSeasonEntity crop)
        {
            string appRegId = "Bihan";

            List<DbParameter> dbparams = new List<DbParameter>();
            dbparams.Add(new SqlParameter { ParameterName = "@query_name", Value = qn_PostCropDimIns, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@Season_Id", Value = crop.Season_Id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@Crop_Category", Value = crop.Crop_Category, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@Crop_name", Value = crop.Crop_Name, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@District_Id", Value = DBNull.Value, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });

            dbparams.Add(new SqlParameter { ParameterName = "@Block_Id", Value = DBNull.Value, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@Panchayat_Id", Value = DBNull.Value, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            int insertRowsCount = 0;

            List<DbParameter> dimselparams = new List<DbParameter>();

            dimselparams.Add(new SqlParameter { ParameterName = "@query_name", Value = qn_PostCropDimSel, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dimselparams.Add(new SqlParameter { ParameterName = "@Crop_name", Value = crop.Crop_Name, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dimselparams.Add(new SqlParameter { ParameterName = "@Crop_Category", Value = crop.Crop_Category, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dimselparams.Add(new SqlParameter { ParameterName = "@Season_Id", Value = crop.Season_Id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });

            dimselparams.Add(new SqlParameter { ParameterName = "@District_Id", Value = DBNull.Value, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });

            dimselparams.Add(new SqlParameter { ParameterName = "@Block_Id", Value = DBNull.Value, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            dimselparams.Add(new SqlParameter { ParameterName = "@Panchayat_Id", Value = DBNull.Value, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });

            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>("usp_dim_cntrlr", dimselparams, SqlHelper.ExecutionType.Procedure);

            if (dt.Rows.Count == 0)
            {
                List<DbParameter> mergeInsparams = new List<DbParameter>();
                mergeInsparams.Add(new SqlParameter { ParameterName = "@query_name", Value = qn_PostCropDimMerge, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                mergeInsparams.Add(new SqlParameter { ParameterName = "@Crop_name", Value = crop.Crop_Name, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                mergeInsparams.Add(new SqlParameter { ParameterName = "@Crop_Category", Value = crop.Crop_Category, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                mergeInsparams.Add(new SqlParameter { ParameterName = "@Season_Id", Value = DBNull.Value, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });

                mergeInsparams.Add(new SqlParameter { ParameterName = "@District_Id", Value = crop.District_Id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });

                mergeInsparams.Add(new SqlParameter { ParameterName = "@Block_Id", Value = DBNull.Value, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                mergeInsparams.Add(new SqlParameter { ParameterName = "@Panchayat_Id", Value = DBNull.Value, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });

                Dictionary<string, dynamic> result = SqlHelper.ExecuteNonQuery<SqlConnection>(sp_dim_cntrlr, mergeInsparams, SqlHelper.ExecutionType.Procedure);
                insertRowsCount += result["RowsAffected"];

                if (result["RowsAffected"] != 0)
                {
                    List<DbParameter> auditInsCropparams = new List<DbParameter>();
                    auditInsCropparams.Add(new SqlParameter { ParameterName = "@user_id", Value = 0, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                    auditInsCropparams.Add(new SqlParameter { ParameterName = "@activity_desc", Value = ("Insert into crop_dim For :District-Id:" + crop.District_Id + "Crop-Category:" + (string.IsNullOrEmpty(crop.Crop_Category) ? "Crop Category is not Provided" : crop.Crop_Category) + "Crop-Name:" + (string.IsNullOrEmpty(crop.Crop_Name) ? "Crop Name is not Provided" : crop.Crop_Name)).ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                    auditInsCropparams.Add(new SqlParameter { ParameterName = "@activity_status", Value = strSuccess, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                    auditInsCropparams.Add(new SqlParameter { ParameterName = "@activity_ts", Value = this.istDate, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                    auditInsCropparams.Add(new SqlParameter { ParameterName = "@activity_source", Value = appRegId, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                    auditInsCropparams.Add(new SqlParameter { ParameterName = "@api_source", Value = api_source, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                    auditInsCropparams.Add(new SqlParameter { ParameterName = "@activity_type", Value = activity_type, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                    auditInsCropparams.Add(new SqlParameter { ParameterName = "@retval", Value = 0, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Output });
                    Dictionary<string, dynamic> auditresult = SqlHelper.ExecuteNonQuery<SqlConnection>(sp_activity_audit, auditInsCropparams, SqlHelper.ExecutionType.Procedure);
                }

                result = SqlHelper.ExecuteNonQuery<SqlConnection>(sp_dim_cntrlr, dbparams, SqlHelper.ExecutionType.Procedure);
                insertRowsCount += result["RowsAffected"];
                if (result["RowsAffected"] != 0)
                {
                    List<DbParameter> auditInsCropparams = new List<DbParameter>();
                    auditInsCropparams.Add(new SqlParameter { ParameterName = "@user_id", Value = 0, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                    auditInsCropparams.Add(new SqlParameter { ParameterName = "@activity_desc", Value = ("Insert into season_crop_xref For :SeasonId:" + crop.Season_Id + "Crop - Category:" + (string.IsNullOrEmpty(crop.Crop_Category) ? "Crop Category is not Provided" : crop.Crop_Category) + "Crop - Name:" + (string.IsNullOrEmpty(crop.Crop_Name) ? "Crop Name is not Provided" : crop.Crop_Name)).ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                    auditInsCropparams.Add(new SqlParameter { ParameterName = "@activity_status", Value = strSuccess, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                    auditInsCropparams.Add(new SqlParameter { ParameterName = "@activity_ts", Value = this.istDate, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                    auditInsCropparams.Add(new SqlParameter { ParameterName = "@activity_source", Value = appRegId, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                    auditInsCropparams.Add(new SqlParameter { ParameterName = "@api_source", Value = api_source, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                    auditInsCropparams.Add(new SqlParameter { ParameterName = "@activity_type", Value = activity_type, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                    auditInsCropparams.Add(new SqlParameter { ParameterName = "@retval", Value = 0, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Output });
                    Dictionary<string, dynamic> auditresult = SqlHelper.ExecuteNonQuery<SqlConnection>(sp_activity_audit, auditInsCropparams, SqlHelper.ExecutionType.Procedure);
                }
            }

            return insertRowsCount;
        }

        /// <summary>
        /// InsertCropCoverageAimPancht.
        /// </summary>
        /// <param name="crop">crop.</param>
        /// <returns>Success Status.</returns>
        public List<CropCoverageTargetPanchytApprovalResponse> InsertCropCoverageAimPancht(List<CropCoverageAimPancht> crop)
        {
            int insertRowsCount = 0;

            List<CropCoverageTargetPanchytApprovalResponse> response = new List<CropCoverageTargetPanchytApprovalResponse>();
            foreach (CropCoverageAimPancht cclist in crop)
            {
                foreach (CropTgtEntity cpvalue in cclist.CropValues)
                {
                    List<DbParameter> dbparams = new List<DbParameter>();

                    dbparams.Add(new SqlParameter { ParameterName = "@season_id", Value = cclist.Season_Id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@panchayat_id", Value = cclist.Panchayat_Id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@crop_id", Value = cpvalue.Crop_Id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@area_target", Value = cpvalue.Area_Target, SqlDbType = SqlDbType.Decimal, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@dao_approval_flag", Value = string.IsNullOrEmpty(cpvalue.DAO_Approval_flag) ? DBNull.Value : (object)cpvalue.DAO_Approval_flag, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@dao_approval_reason", Value = string.IsNullOrEmpty(cpvalue.DAO_Approval_Reason) ? DBNull.Value : (object)cpvalue.DAO_Approval_Reason, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@dao_approved_userid", Value = cpvalue.DAO_Approved_userid == 0 ? DBNull.Value : (object)cpvalue.DAO_Approved_userid, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@dao_approved_date", Value = cpvalue.DAO_Approved_date == DateTime.MinValue ? DBNull.Value : (object)cpvalue.DAO_Approved_date, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@bao_approval_flag", Value = string.IsNullOrEmpty(cpvalue.BAO_Approval_flag) ? DBNull.Value : (object)cpvalue.BAO_Approval_flag, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@bao_approval_reason", Value = string.IsNullOrEmpty(cpvalue.BAO_Approval_Reason) ? DBNull.Value : (object)cpvalue.BAO_Approval_Reason, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@bao_approved_userid", Value = cpvalue.BAO_Approved_userid == 0 ? DBNull.Value : (object)cpvalue.BAO_Approved_userid, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@bao_approved_date", Value = cpvalue.BAO_Approved_date == DateTime.MinValue ? DBNull.Value : (object)cpvalue.BAO_Approved_date, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@ac_submitted_userid", Value = cpvalue.AC_Submitted_userid == 0 ? DBNull.Value : (object)cpvalue.AC_Submitted_userid, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@ac_submitted_date", Value = cpvalue.AC_Submitted_date == DateTime.MinValue ? DBNull.Value : (object)cpvalue.AC_Submitted_date, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@response_status", SqlDbType = SqlDbType.NVarChar, Size = 1000, Direction = ParameterDirection.Output });
                    dbparams.Add(new SqlParameter { ParameterName = "@submission_source", Value = string.IsNullOrEmpty(cpvalue.Submission_source) ? DBNull.Value : (object)cpvalue.Submission_source, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@rec_updated_userid", Value = cpvalue.Rec_updated_userid == 0 ? DBNull.Value : (object)cpvalue.Rec_updated_userid, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@rec_updated_date", Value = cpvalue.Rec_updated_date == DateTime.MinValue ? DBNull.Value : (object)cpvalue.Rec_updated_date, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@ac_submit_flag", Value = string.IsNullOrEmpty(cpvalue.Ac_submit_flag) ? DBNull.Value : (object)cpvalue.Ac_submit_flag, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@bao_add_edit_flag", Value = string.IsNullOrEmpty(cpvalue.Bao_add_edit_flag) ? DBNull.Value : (object)cpvalue.Bao_add_edit_flag, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@dao_add_edit_flag", Value = string.IsNullOrEmpty(cpvalue.Dao_add_edit_flag) ? DBNull.Value : (object)cpvalue.Dao_add_edit_flag, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });

                    Dictionary<string, dynamic> result = SqlHelper.ExecuteNonQuery<SqlConnection>(sp_cvrg_aim_panchayat, dbparams, SqlHelper.ExecutionType.Procedure);
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
            }

            return response;
        }

        /// <summary>
        /// InsertCropCoverageAimVillage.
        /// </summary>
        /// <param name="crop">crop.</param>
        /// <returns>Success Status.</returns>
        public List<CropCoverageTargetVillageApprovalResponse> InsertCropCoverageAimVillage(List<CropCoverageAimVillage> crop)
        {
            int insertRowsCount = 0;

            List<CropCoverageTargetVillageApprovalResponse> response = new List<CropCoverageTargetVillageApprovalResponse>();
            foreach (CropCoverageAimVillage cclist in crop)
            {
                foreach (CropTgtEntity cpvalue in cclist.CropValues)
                {
                    List<DbParameter> dbparams = new List<DbParameter>();

                    dbparams.Add(new SqlParameter { ParameterName = "@season_id", Value = cclist.Season_Id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@village_id", Value = cclist.Village_Id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@crop_id", Value = cpvalue.Crop_Id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@area_target", Value = cpvalue.Area_Target, SqlDbType = SqlDbType.Decimal, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@dao_approval_flag", Value = string.IsNullOrEmpty(cpvalue.DAO_Approval_flag) ? DBNull.Value : (object)cpvalue.DAO_Approval_flag, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@dao_approval_reason", Value = string.IsNullOrEmpty(cpvalue.DAO_Approval_Reason) ? DBNull.Value : (object)cpvalue.DAO_Approval_Reason, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@dao_approved_userid", Value = cpvalue.DAO_Approved_userid == 0 ? DBNull.Value : (object)cpvalue.DAO_Approved_userid, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@dao_approved_date", Value = cpvalue.DAO_Approved_date == DateTime.MinValue ? DBNull.Value : (object)cpvalue.DAO_Approved_date, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@bao_approval_flag", Value = string.IsNullOrEmpty(cpvalue.BAO_Approval_flag) ? DBNull.Value : (object)cpvalue.BAO_Approval_flag, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@bao_approval_reason", Value = string.IsNullOrEmpty(cpvalue.BAO_Approval_Reason) ? DBNull.Value : (object)cpvalue.BAO_Approval_Reason, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@bao_approved_userid", Value = cpvalue.BAO_Approved_userid == 0 ? DBNull.Value : (object)cpvalue.BAO_Approved_userid, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@bao_approved_date", Value = cpvalue.BAO_Approved_date == DateTime.MinValue ? DBNull.Value : (object)cpvalue.BAO_Approved_date, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@ac_submitted_userid", Value = cpvalue.AC_Submitted_userid == 0 ? DBNull.Value : (object)cpvalue.AC_Submitted_userid, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@ac_submitted_date", Value = cpvalue.AC_Submitted_date == DateTime.MinValue ? DBNull.Value : (object)cpvalue.AC_Submitted_date, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@response_status", SqlDbType = SqlDbType.NVarChar, Size = 1000, Direction = ParameterDirection.Output });
                    dbparams.Add(new SqlParameter { ParameterName = "@submission_source", Value = string.IsNullOrEmpty(cpvalue.Submission_source) ? DBNull.Value : (object)cpvalue.Submission_source, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@rec_updated_userid", Value = cpvalue.Rec_updated_userid == 0 ? DBNull.Value : (object)cpvalue.Rec_updated_userid, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@rec_updated_date", Value = cpvalue.Rec_updated_date == DateTime.MinValue ? DBNull.Value : (object)cpvalue.Rec_updated_date, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@ac_submit_flag", Value = string.IsNullOrEmpty(cpvalue.Ac_submit_flag) ? DBNull.Value : (object)cpvalue.Ac_submit_flag, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@bao_add_edit_flag", Value = string.IsNullOrEmpty(cpvalue.Bao_add_edit_flag) ? DBNull.Value : (object)cpvalue.Bao_add_edit_flag, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@dao_add_edit_flag", Value = string.IsNullOrEmpty(cpvalue.Dao_add_edit_flag) ? DBNull.Value : (object)cpvalue.Dao_add_edit_flag, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });

                    Dictionary<string, dynamic> result = SqlHelper.ExecuteNonQuery<SqlConnection>(sp_cvrg_aim_village, dbparams, SqlHelper.ExecutionType.Procedure);
                    insertRowsCount += result["RowsAffected"];
                    string spOut = DBNull.Value.Equals(result["@response_status"]) ? string.Empty : result["@response_status"];

                    if (!string.IsNullOrEmpty(spOut))
                    {
                        CropCoverageTargetVillageApprovalResponse respobj = new CropCoverageTargetVillageApprovalResponse();

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
                            else if (splitteddata[0].Trim().Equals("VillageId"))
                            {
                                respobj.VillageId = splitteddata[1].ToString();
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
        /// InsertCropCoverageActualPancht.
        /// </summary>
        /// <param name="crop">crop.</param>
        /// <returns>Response value.</returns>
        public List<CropCoverageActualPanchayatApprovalResponse> InsertCropCoverageActualPancht(List<CropCoverageActualVillage> crop)
        {
            int insertRowsCount = 0;
            List<CropCoverageActualPanchayatApprovalResponse> response = new List<CropCoverageActualPanchayatApprovalResponse>();

            foreach (CropCoverageActualVillage cclist in crop)
            {
                foreach (CropCvrgEntity cpvalue in cclist.CropList)
                {
                    List<DbParameter> dbparams = new List<DbParameter>();

                    dbparams.Add(new SqlParameter { ParameterName = "@reported_date", Value = cclist.Reported_Date, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@season_id", Value = cclist.Season_Id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@panchayat_id", Value = cclist.Panchayat_Id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@crop_id", Value = cpvalue.Crop_Id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@cumm_area_prev", Value = cpvalue.Cumm_Area_Prev, SqlDbType = SqlDbType.Decimal, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@cumm_area_curr", Value = cpvalue.Cumm_Area_Curr, SqlDbType = SqlDbType.Decimal, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@dao_approval_flag", Value = string.IsNullOrEmpty(cpvalue.DAO_Approval_flag) ? DBNull.Value : (object)cpvalue.DAO_Approval_flag, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@dao_approval_reason", Value = string.IsNullOrEmpty(cpvalue.DAO_Approval_Reason) ? DBNull.Value : (object)cpvalue.DAO_Approval_Reason, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@dao_approved_userid", Value = cpvalue.DAO_Approved_userid == 0 ? DBNull.Value : (object)cpvalue.DAO_Approved_userid, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@dao_approved_date", Value = cpvalue.DAO_Approved_date == DateTime.MinValue ? DBNull.Value : (object)cpvalue.DAO_Approved_date, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@bao_approval_flag", Value = string.IsNullOrEmpty(cpvalue.BAO_Approval_flag) ? DBNull.Value : (object)cpvalue.BAO_Approval_flag, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@bao_approval_reason", Value = string.IsNullOrEmpty(cpvalue.BAO_Approval_Reason) ? DBNull.Value : (object)cpvalue.BAO_Approval_Reason, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@bao_approved_userid", Value = cpvalue.BAO_Approved_userid == 0 ? DBNull.Value : (object)cpvalue.BAO_Approved_userid, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@bao_approved_date", Value = cpvalue.BAO_Approved_date == DateTime.MinValue ? DBNull.Value : (object)cpvalue.BAO_Approved_date, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@ac_submitted_userid", Value = cpvalue.AC_Submitted_userid == 0 ? DBNull.Value : (object)cpvalue.AC_Submitted_userid, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@ac_submitted_date", Value = cpvalue.AC_Submitted_date == DateTime.MinValue ? DBNull.Value : (object)cpvalue.AC_Submitted_date, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@submission_source", Value = string.IsNullOrEmpty(cpvalue.Submission_source) ? DBNull.Value : (object)cpvalue.Submission_source, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@ac_submit_flag", Value = string.IsNullOrEmpty(cpvalue.Ac_submit_flag) ? DBNull.Value : (object)cpvalue.Ac_submit_flag, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@bao_add_edit_flag", Value = string.IsNullOrEmpty(cpvalue.Bao_add_edit_flag) ? DBNull.Value : (object)cpvalue.Bao_add_edit_flag, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@dao_add_edit_flag", Value = string.IsNullOrEmpty(cpvalue.Dao_add_edit_flag) ? DBNull.Value : (object)cpvalue.Dao_add_edit_flag, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@final_cvrg_flg", Value = string.IsNullOrEmpty(cpvalue.Final_cvrg_flg) ? DBNull.Value : (object)cpvalue.Final_cvrg_flg, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@rec_updated_userid", Value = cpvalue.Rec_updated_userid == 0 ? DBNull.Value : (object)cpvalue.Rec_updated_userid, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@rec_updated_date", Value = cpvalue.Rec_updated_date == DateTime.MinValue ? DBNull.Value : (object)cpvalue.Rec_updated_date, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@response_status", SqlDbType = SqlDbType.NVarChar, Size = 1000, Direction = ParameterDirection.Output });
                    Dictionary<string, dynamic> result = SqlHelper.ExecuteNonQuery<SqlConnection>(sp_cvrg_actual_panchayat, dbparams, SqlHelper.ExecutionType.Procedure);

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

                        string sp_query = qn_aggr_crop_coverage;
                        result = SqlHelper.ExecuteNonQuery<SqlConnection>(sp_query, spparams, SqlHelper.ExecutionType.Procedure);
                    }
                }
            }

            return response;
        }

        /// <summary>
        /// InsertCropCoverageActualVillage.
        /// </summary>
        /// <param name="crop">crop.</param>
        /// <returns>Response value.</returns>
        public List<CropCoverageActualVillageApprovalResponse> InsertCropCoverageActualVillage(List<CropCoverageActualVillage> crop)
        {
            int insertRowsCount = 0;
            List<CropCoverageActualVillageApprovalResponse> response = new List<CropCoverageActualVillageApprovalResponse>();

            foreach (CropCoverageActualVillage cclist in crop)
            {
                foreach (CropCvrgEntity cpvalue in cclist.CropList)
                {
                    List<DbParameter> dbparams = new List<DbParameter>();

                    dbparams.Add(new SqlParameter { ParameterName = "@reported_date", Value = cclist.Reported_Date, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@season_id", Value = cclist.Season_Id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@village_id", Value = cclist.Village_Id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@crop_id", Value = cpvalue.Crop_Id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@cumm_area_prev", Value = cpvalue.Cumm_Area_Prev, SqlDbType = SqlDbType.Decimal, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@cumm_area_curr", Value = cpvalue.Cumm_Area_Curr, SqlDbType = SqlDbType.Decimal, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@dao_approval_flag", Value = string.IsNullOrEmpty(cpvalue.DAO_Approval_flag) ? DBNull.Value : (object)cpvalue.DAO_Approval_flag, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@dao_approval_reason", Value = string.IsNullOrEmpty(cpvalue.DAO_Approval_Reason) ? DBNull.Value : (object)cpvalue.DAO_Approval_Reason, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@dao_approved_userid", Value = cpvalue.DAO_Approved_userid == 0 ? DBNull.Value : (object)cpvalue.DAO_Approved_userid, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@dao_approved_date", Value = cpvalue.DAO_Approved_date == DateTime.MinValue ? DBNull.Value : (object)cpvalue.DAO_Approved_date, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@bao_approval_flag", Value = string.IsNullOrEmpty(cpvalue.BAO_Approval_flag) ? DBNull.Value : (object)cpvalue.BAO_Approval_flag, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@bao_approval_reason", Value = string.IsNullOrEmpty(cpvalue.BAO_Approval_Reason) ? DBNull.Value : (object)cpvalue.BAO_Approval_Reason, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@bao_approved_userid", Value = cpvalue.BAO_Approved_userid == 0 ? DBNull.Value : (object)cpvalue.BAO_Approved_userid, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@bao_approved_date", Value = cpvalue.BAO_Approved_date == DateTime.MinValue ? DBNull.Value : (object)cpvalue.BAO_Approved_date, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@ac_submitted_userid", Value = cpvalue.AC_Submitted_userid == 0 ? DBNull.Value : (object)cpvalue.AC_Submitted_userid, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@ac_submitted_date", Value = cpvalue.AC_Submitted_date == DateTime.MinValue ? DBNull.Value : (object)cpvalue.AC_Submitted_date, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@submission_source", Value = string.IsNullOrEmpty(cpvalue.Submission_source) ? DBNull.Value : (object)cpvalue.Submission_source, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@ac_submit_flag", Value = string.IsNullOrEmpty(cpvalue.Ac_submit_flag) ? DBNull.Value : (object)cpvalue.Ac_submit_flag, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@bao_add_edit_flag", Value = string.IsNullOrEmpty(cpvalue.Bao_add_edit_flag) ? DBNull.Value : (object)cpvalue.Bao_add_edit_flag, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@dao_add_edit_flag", Value = string.IsNullOrEmpty(cpvalue.Dao_add_edit_flag) ? DBNull.Value : (object)cpvalue.Dao_add_edit_flag, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@final_cvrg_flg", Value = string.IsNullOrEmpty(cpvalue.Final_cvrg_flg) ? DBNull.Value : (object)cpvalue.Final_cvrg_flg, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@rec_updated_userid", Value = cpvalue.Rec_updated_userid == 0 ? DBNull.Value : (object)cpvalue.Rec_updated_userid, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@rec_updated_date", Value = cpvalue.Rec_updated_date == DateTime.MinValue ? DBNull.Value : (object)cpvalue.Rec_updated_date, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@response_status", SqlDbType = SqlDbType.NVarChar, Size = 1000, Direction = ParameterDirection.Output });
                    Dictionary<string, dynamic> result = SqlHelper.ExecuteNonQuery<SqlConnection>(sp_cvrg_actual_village, dbparams, SqlHelper.ExecutionType.Procedure);

                    insertRowsCount += result["RowsAffected"];
                    string spOut = DBNull.Value.Equals(result["@response_status"]) ? string.Empty : result["@response_status"];
                    if (!string.IsNullOrEmpty(spOut))
                    {
                        CropCoverageActualVillageApprovalResponse respobj = new CropCoverageActualVillageApprovalResponse();
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
                            else if (splitteddata[0].Trim().Equals("VillageId"))
                            {
                                respobj.VillageId = splitteddata[1].ToString();
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
                        spparams.Add(new SqlParameter { ParameterName = "@village_id", Value = cclist.Village_Id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                        spparams.Add(new SqlParameter { ParameterName = "@crop_id", Value = cpvalue.Crop_Id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });

                        string sp_query = qn_aggr_crop_coverage;
                        result = SqlHelper.ExecuteNonQuery<SqlConnection>(sp_query, spparams, SqlHelper.ExecutionType.Procedure);
                    }
                }
            }

            return response;
        }

        /// <summary>
        /// InsertCropDamagePancht.
        /// </summary>
        /// <param name="crop">crop.</param>
        /// <returns>Response message.</returns>
        public List<CropDamagePanchayatResponse> InsertCropDamagePancht(List<CropDamagePancht> crop)
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

                        dbparams.Add(new SqlParameter { ParameterName = "@ac_submit_flag", Value = string.IsNullOrEmpty(damagedetail.Ac_submit_flag) ? DBNull.Value : (object)damagedetail.Ac_submit_flag, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                        dbparams.Add(new SqlParameter { ParameterName = "@bao_add_edit_flag", Value = string.IsNullOrEmpty(damagedetail.Bao_add_edit_flag) ? DBNull.Value : (object)damagedetail.Bao_add_edit_flag, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                        dbparams.Add(new SqlParameter { ParameterName = "@dao_add_edit_flag", Value = string.IsNullOrEmpty(damagedetail.Dao_add_edit_flag) ? DBNull.Value : (object)damagedetail.Dao_add_edit_flag, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                        dbparams.Add(new SqlParameter { ParameterName = "@rec_updated_userid", Value = Convert.ToInt32(damagedetail.Rec_updated_userid) == 0 ? DBNull.Value : (object)damagedetail.Rec_updated_userid, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                        dbparams.Add(new SqlParameter { ParameterName = "@rec_updated_date", Value = damagedetail.Rec_updated_date == DateTime.MinValue ? DBNull.Value : (object)damagedetail.Rec_updated_date, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });

                        dbparams.Add(new SqlParameter { ParameterName = "@response_status", SqlDbType = SqlDbType.NVarChar, Size = 200, Direction = ParameterDirection.Output });

                        Dictionary<string, dynamic> result = SqlHelper.ExecuteNonQuery<SqlConnection>(sp_crp_dmg_panchayat, dbparams, SqlHelper.ExecutionType.Procedure);
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
                            spparams.Add(new SqlParameter { ParameterName = "@season_id", Value = cdlist.Season_Id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                            spparams.Add(new SqlParameter { ParameterName = "@panchayat_id", Value = cdlist.Panchayat_Id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                            spparams.Add(new SqlParameter { ParameterName = "@crop_id", Value = cpvalue.Crop_Id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                            spparams.Add(new SqlParameter { ParameterName = "@damage_reason", Value = string.IsNullOrEmpty(damagedetail.Damage_Reason) ? DBNull.Value : (object)damagedetail.Damage_Reason, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });

                            string sp_query = qn_aggr_crp_dmg;
                            result = SqlHelper.ExecuteNonQuery<SqlConnection>(sp_query, spparams, SqlHelper.ExecutionType.Procedure);
                        }
                    }
                }
            }

            return response;
        }

        /// <summary>
        /// InsertCropCoverageActualApproval.
        /// </summary>
        /// <param name="cropCoverageActual">cropCoverageActual.</param>
        /// <returns>Response Value.</returns>
        public List<CropCoverageActualBlockApprovalResponse> InsertCropCoverageActualApproval(CropCoverageActual cropCoverageActual)
        {
            List<CropCoverageActualBlockApprovalResponse> response = new List<CropCoverageActualBlockApprovalResponse>();

            int insertRowsCount = 0;

            if (cropCoverageActual != null)
            {
                List<DbParameter> dbparamsBlocks = new List<DbParameter>();

                List<DbParameter> dbparamsPanchayat = new List<DbParameter>();
                if (cropCoverageActual.BlockList != null)
                {
                    foreach (var block in cropCoverageActual.BlockList)
                    {
                        CropCoverageActualBlockApprovalResponse respblkobj = new CropCoverageActualBlockApprovalResponse();
                        if (block.DAO_Approved_userid != 0 && block.DAO_Approval_flag != "N")
                        {
                            dbparamsBlocks.Add(new SqlParameter { ParameterName = "@Season_Id", Value = cropCoverageActual.Season_Id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                            dbparamsBlocks.Add(new SqlParameter { ParameterName = "@Block_Id", Value = block.Block_Id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                            dbparamsBlocks.Add(new SqlParameter { ParameterName = "@Crop_Id", Value = cropCoverageActual.Crop_Id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                            dbparamsBlocks.Add(new SqlParameter { ParameterName = "@dao_approval_flag", Value = string.IsNullOrEmpty(block.DAO_Approval_flag) ? DBNull.Value : (object)block.DAO_Approval_flag, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                            dbparamsBlocks.Add(new SqlParameter { ParameterName = "@dao_approval_reason", Value = string.IsNullOrEmpty(block.DAO_Approval_Reason) ? DBNull.Value : (object)block.DAO_Approval_Reason, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                            dbparamsBlocks.Add(new SqlParameter { ParameterName = "@dao_approved_userid", Value = block.DAO_Approved_userid == 0 ? DBNull.Value : (object)block.DAO_Approved_userid, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                            dbparamsBlocks.Add(new SqlParameter { ParameterName = "@dao_approved_date", Value = block.DAO_Approved_date == DateTime.MinValue ? DBNull.Value : (object)block.DAO_Approved_date, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
                            dbparamsBlocks.Add(new SqlParameter { ParameterName = "@reported_date", Value = block.Reported_date, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
                            dbparamsBlocks.Add(new SqlParameter { ParameterName = "@response_status", SqlDbType = SqlDbType.NVarChar, Size = 1000, Direction = ParameterDirection.Output });

                            Dictionary<string, dynamic> result = SqlHelper.ExecuteNonQuery<SqlConnection>(sp_actl_blk_appr, dbparamsBlocks, SqlHelper.ExecutionType.Procedure);

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
                                dbparamsPanchayat.Add(new SqlParameter { ParameterName = "@dao_approval_flag", Value = string.IsNullOrEmpty(panchayat.DAO_Approval_flag) ? DBNull.Value : (object)panchayat.DAO_Approval_flag, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                                dbparamsPanchayat.Add(new SqlParameter { ParameterName = "@dao_approval_reason", Value = string.IsNullOrEmpty(panchayat.DAO_Approval_Reason) ? DBNull.Value : (object)panchayat.DAO_Approval_Reason, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                                dbparamsPanchayat.Add(new SqlParameter { ParameterName = "@dao_approved_userid", Value = panchayat.DAO_Approved_userid == 0 ? DBNull.Value : (object)panchayat.DAO_Approved_userid, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                                dbparamsPanchayat.Add(new SqlParameter { ParameterName = "@dao_approved_date", Value = panchayat.DAO_Approved_date == DateTime.MinValue ? DBNull.Value : (object)panchayat.DAO_Approved_date, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
                                dbparamsPanchayat.Add(new SqlParameter { ParameterName = "@bao_approval_flag", Value = string.IsNullOrEmpty(panchayat.BAO_Approval_flag) ? DBNull.Value : (object)panchayat.BAO_Approval_flag, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                                dbparamsPanchayat.Add(new SqlParameter { ParameterName = "@bao_approval_reason", Value = string.IsNullOrEmpty(panchayat.BAO_Approval_Reason) ? DBNull.Value : (object)panchayat.BAO_Approval_Reason, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                                dbparamsPanchayat.Add(new SqlParameter { ParameterName = "@bao_approved_userid", Value = panchayat.BAO_Approved_userid == 0 ? DBNull.Value : (object)panchayat.BAO_Approved_userid, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                                dbparamsPanchayat.Add(new SqlParameter { ParameterName = "@bao_approved_date", Value = panchayat.BAO_Approved_date == DateTime.MinValue ? DBNull.Value : (object)panchayat.BAO_Approved_date, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
                                dbparamsPanchayat.Add(new SqlParameter { ParameterName = "@ac_submitted_userid", Value = panchayat.AC_Submitted_userid == 0 ? DBNull.Value : (object)panchayat.AC_Submitted_userid, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                                dbparamsPanchayat.Add(new SqlParameter { ParameterName = "@ac_submitted_date", Value = panchayat.AC_Submitted_date == DateTime.MinValue ? DBNull.Value : (object)panchayat.AC_Submitted_date, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
                                dbparamsPanchayat.Add(new SqlParameter { ParameterName = "@submission_source", Value = string.IsNullOrEmpty(panchayat.Submission_source) ? DBNull.Value : (object)panchayat.Submission_source, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                                dbparamsPanchayat.Add(new SqlParameter { ParameterName = "@response_status", SqlDbType = SqlDbType.NVarChar, Size = 1000, Direction = ParameterDirection.Output });

                                Dictionary<string, dynamic> result = SqlHelper.ExecuteNonQuery<SqlConnection>(sp_actl_panch_appr, dbparamsPanchayat, SqlHelper.ExecutionType.Procedure);

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
        /// InsertCropCoverageTargetApproval.
        /// </summary>
        /// <param name="cropCoverageTarget">cropCoverageTarget.</param>
        /// <returns>Response Values.</returns>
        public List<CropCoverageTargetBlockApproval> InsertCropCoverageTargetApproval(CropCoverageAim cropCoverageTarget)
        {
            int insertRowsCount = 0;

            List<CropCoverageTargetBlockApproval> response = new List<CropCoverageTargetBlockApproval>();

            if (cropCoverageTarget != null)
            {
                List<DbParameter> dbparamsBlocks = new List<DbParameter>();
                List<DbParameter> dbparamsPanchayat = new List<DbParameter>();
                if (cropCoverageTarget.BlockList != null)
                {
                    foreach (var block in cropCoverageTarget.BlockList)
                    {
                        CropCoverageTargetBlockApproval respblkobj = new CropCoverageTargetBlockApproval();
                        if (block.DAO_Approved_userid != 0 && block.DAO_Approval_flag != "N")
                        {
                            dbparamsBlocks.Add(new SqlParameter { ParameterName = "@Season_Id", Value = cropCoverageTarget.Season_Id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                            dbparamsBlocks.Add(new SqlParameter { ParameterName = "@Block_Id", Value = block.Block_Id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                            dbparamsBlocks.Add(new SqlParameter { ParameterName = "@Crop_Id", Value = cropCoverageTarget.Crop_Id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                            dbparamsBlocks.Add(new SqlParameter { ParameterName = "@dao_approval_flag", Value = string.IsNullOrEmpty(block.DAO_Approval_flag) ? DBNull.Value : (object)block.DAO_Approval_flag, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                            dbparamsBlocks.Add(new SqlParameter { ParameterName = "@dao_approval_reason", Value = string.IsNullOrEmpty(block.DAO_Approval_Reason) ? DBNull.Value : (object)block.DAO_Approval_Reason, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                            dbparamsBlocks.Add(new SqlParameter { ParameterName = "@dao_approved_userid", Value = block.DAO_Approved_userid == 0 ? DBNull.Value : (object)block.DAO_Approved_userid, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                            dbparamsBlocks.Add(new SqlParameter { ParameterName = "@dao_approved_date", Value = block.DAO_Approved_date == DateTime.MinValue ? DBNull.Value : (object)block.DAO_Approved_date, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
                            dbparamsBlocks.Add(new SqlParameter { ParameterName = "@response_status", SqlDbType = SqlDbType.NVarChar, Size = 1000, Direction = ParameterDirection.Output });

                            Dictionary<string, dynamic> result = SqlHelper.ExecuteNonQuery<SqlConnection>(sp_tgt_blk_appr, dbparamsBlocks, SqlHelper.ExecutionType.Procedure);
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
                                dbparamsPanchayat.Add(new SqlParameter { ParameterName = "@dao_approval_flag", Value = string.IsNullOrEmpty(panchayat.DAO_Approval_flag) ? DBNull.Value : (object)panchayat.DAO_Approval_flag, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                                dbparamsPanchayat.Add(new SqlParameter { ParameterName = "@dao_approval_reason", Value = string.IsNullOrEmpty(panchayat.DAO_Approval_Reason) ? DBNull.Value : (object)panchayat.DAO_Approval_Reason, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                                dbparamsPanchayat.Add(new SqlParameter { ParameterName = "@dao_approved_userid", Value = panchayat.DAO_Approved_userid == 0 ? DBNull.Value : (object)panchayat.DAO_Approved_userid, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                                dbparamsPanchayat.Add(new SqlParameter { ParameterName = "@dao_approved_date", Value = panchayat.DAO_Approved_date == DateTime.MinValue ? DBNull.Value : (object)panchayat.DAO_Approved_date, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
                                dbparamsPanchayat.Add(new SqlParameter { ParameterName = "@bao_approval_flag", Value = string.IsNullOrEmpty(panchayat.BAO_Approval_flag) ? DBNull.Value : (object)panchayat.BAO_Approval_flag, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                                dbparamsPanchayat.Add(new SqlParameter { ParameterName = "@bao_approval_reason", Value = string.IsNullOrEmpty(panchayat.BAO_Approval_Reason) ? DBNull.Value : (object)panchayat.BAO_Approval_Reason, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                                dbparamsPanchayat.Add(new SqlParameter { ParameterName = "@bao_approved_userid", Value = panchayat.BAO_Approved_userid == 0 ? DBNull.Value : (object)panchayat.BAO_Approved_userid, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                                dbparamsPanchayat.Add(new SqlParameter { ParameterName = "@bao_approved_date", Value = panchayat.BAO_Approved_date == DateTime.MinValue ? DBNull.Value : (object)panchayat.BAO_Approved_date, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
                                dbparamsPanchayat.Add(new SqlParameter { ParameterName = "@ac_submitted_userid", Value = panchayat.AC_Submitted_userid == 0 ? DBNull.Value : (object)panchayat.AC_Submitted_userid, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                                dbparamsPanchayat.Add(new SqlParameter { ParameterName = "@ac_submitted_date", Value = panchayat.AC_Submitted_date == DateTime.MinValue ? DBNull.Value : (object)panchayat.AC_Submitted_date, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
                                dbparamsPanchayat.Add(new SqlParameter { ParameterName = "@response_status", SqlDbType = SqlDbType.NVarChar, Size = 1000, Direction = ParameterDirection.Output });
                                dbparamsPanchayat.Add(new SqlParameter { ParameterName = "@submission_source", Value = string.IsNullOrEmpty(panchayat.Submission_source) ? DBNull.Value : (object)panchayat.Submission_source, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                                dbparamsPanchayat.Add(new SqlParameter { ParameterName = "@ac_submit_flag", Value = string.IsNullOrEmpty(panchayat.Ac_submit_flag) ? DBNull.Value : (object)panchayat.Ac_submit_flag, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                                dbparamsPanchayat.Add(new SqlParameter { ParameterName = "@bao_add_edit_flag", Value = string.IsNullOrEmpty(panchayat.Bao_add_edit_flag) ? DBNull.Value : (object)panchayat.Bao_add_edit_flag, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                                dbparamsPanchayat.Add(new SqlParameter { ParameterName = "@dao_add_edit_flag", Value = string.IsNullOrEmpty(panchayat.Dao_add_edit_flag) ? DBNull.Value : (object)panchayat.Dao_add_edit_flag, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                                Dictionary<string, dynamic> result = SqlHelper.ExecuteNonQuery<SqlConnection>(sp_tgt_panch_appr, dbparamsPanchayat, SqlHelper.ExecutionType.Procedure);

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
        /// InsertCropDamageApproval.
        /// </summary>
        /// <param name="cropDamage">cropDamage.</param>
        /// <returns>Response Values.</returns>
        public List<CropDamageBlockApprovalResponse> InsertCropDamageApproval(CropDamage cropDamage)
        {
            int insertRowsCount = 0;

            List<CropDamageBlockApprovalResponse> response = new List<CropDamageBlockApprovalResponse>();
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
                            dbparamsBlocks.Add(new SqlParameter { ParameterName = "@dao_approval_flag", Value = string.IsNullOrEmpty(block.DAO_Approval_flag) ? DBNull.Value : (object)block.DAO_Approval_flag, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                            dbparamsBlocks.Add(new SqlParameter { ParameterName = "@dao_approval_reason", Value = string.IsNullOrEmpty(block.DAO_Approval_Reason) ? DBNull.Value : (object)block.DAO_Approval_Reason, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                            dbparamsBlocks.Add(new SqlParameter { ParameterName = "@dao_approved_userid", Value = block.DAO_Approved_userid == 0 ? DBNull.Value : (object)block.DAO_Approved_userid, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                            dbparamsBlocks.Add(new SqlParameter { ParameterName = "@dao_approved_date", Value = block.DAO_Approved_date == DateTime.MinValue ? DBNull.Value : (object)block.DAO_Approved_date, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
                            dbparamsBlocks.Add(new SqlParameter { ParameterName = "@response_status", SqlDbType = SqlDbType.NVarChar, Size = 200, Direction = ParameterDirection.Output });

                            Dictionary<string, dynamic> result = SqlHelper.ExecuteNonQuery<SqlConnection>(sp_crp_dmg_blk_appr, dbparamsBlocks, SqlHelper.ExecutionType.Procedure);

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

                                    dbparamsPanchayat.Add(new SqlParameter { ParameterName = "@ac_submit_flag", Value = string.IsNullOrEmpty(damage.Ac_submit_flag) ? DBNull.Value : (object)damage.Ac_submit_flag, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                                    dbparamsPanchayat.Add(new SqlParameter { ParameterName = "@bao_add_edit_flag", Value = string.IsNullOrEmpty(damage.Bao_add_edit_flag) ? DBNull.Value : (object)damage.Bao_add_edit_flag, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                                    dbparamsPanchayat.Add(new SqlParameter { ParameterName = "@dao_add_edit_flag", Value = string.IsNullOrEmpty(damage.Dao_add_edit_flag) ? DBNull.Value : (object)damage.Dao_add_edit_flag, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                                    dbparamsPanchayat.Add(new SqlParameter { ParameterName = "@rec_updated_userid", Value = damage.Rec_updated_userid == 0 ? DBNull.Value : (object)damage.Rec_updated_userid, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                                    dbparamsPanchayat.Add(new SqlParameter { ParameterName = "@rec_updated_date", Value = damage.Rec_updated_date == DateTime.MinValue ? DBNull.Value : (object)damage.Rec_updated_date, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
                                    dbparamsPanchayat.Add(new SqlParameter { ParameterName = "@response_status", SqlDbType = SqlDbType.NVarChar, Size = 200, Direction = ParameterDirection.Output });
                                    Dictionary<string, dynamic> result = SqlHelper.ExecuteNonQuery<SqlConnection>(sp_crp_dmg_panch_appr, dbparamsPanchayat, SqlHelper.ExecutionType.Procedure);
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
        /// InsertSeedPerformance.
        /// </summary>
        /// <param name="seedPerformance">seedPerformance.</param>
        /// <returns>Response Values.</returns>
        public async Task<InsertSeedPerformanceResponse> InsertSeedPerformance(SeedPerformance seedPerformance)
        {
            int insertRowsCount = 0;
            BlobServiceClient blobServiceClient = new BlobServiceClient(this.blobconfig.Value.BlobConnection);

            InsertSeedPerformanceResponse respobj = new InsertSeedPerformanceResponse();

            List<DbParameter> dbparamsUserInfo = new List<DbParameter>();

            if (seedPerformance.Application_no != 0)
            {
                if (!string.IsNullOrEmpty(seedPerformance.SownImageData))
                {
                    if (seedPerformance.SownImageData.ToString() != "0")
                    {
                        BlobEntity blobEntity = new BlobEntity();
                        blobEntity.DirectoryName = "Seed_Performance_Photos";
                        blobEntity.FolderName = seedPerformance.Application_no.ToString() + "-" + "SOWNIMAGE" + ".jpg";
                        blobEntity.ByteArray = seedPerformance.SownImageData;

                        BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient("mobileapp");

                        string blobPath = blobEntity.DirectoryName + Path.AltDirectorySeparatorChar + blobEntity.FolderName;

                        BlobClient blobClient = containerClient.GetBlobClient(blobPath);

                        if (seedPerformance.Area_sown.ToString() != "0" || (seedPerformance.Area_sown.ToString() == "0" && (!string.IsNullOrEmpty(seedPerformance.SownImageData))))
                        {
                            byte[] bytes1 = Convert.FromBase64String(blobEntity.ByteArray);
                            Stream stream = new MemoryStream(bytes1);

                            await blobClient.UploadAsync(stream, true);

                            seedPerformance.Sown_img_file_location = this.blobconfig.Value.BlobSeedPhoto;
                            seedPerformance.Sown_img_file_name = blobEntity.FolderName;
                        }
                        else
                        {
                            seedPerformance.Sown_img_file_location = "0";
                            seedPerformance.Sown_img_file_name = "0";
                        }
                    }
                    else
                    {
                        seedPerformance.Sown_img_file_location = "0";
                        seedPerformance.Sown_img_file_name = "0";
                    }
                }

                if (!string.IsNullOrEmpty(seedPerformance.HarvestImageData))
                {
                    BlobEntity blobEntity = new BlobEntity();
                    blobEntity.DirectoryName = "Seed_Performance_Photos";
                    blobEntity.FolderName = seedPerformance.Application_no.ToString() + "-" + "HARVESTIMAGE" + ".jpg";
                    blobEntity.ByteArray = seedPerformance.HarvestImageData;

                    BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient("mobileapp");

                    string blobPath = blobEntity.DirectoryName + Path.AltDirectorySeparatorChar + blobEntity.FolderName;

                    BlobClient blobClient = containerClient.GetBlobClient(blobPath);

                    byte[] bytes1 = Convert.FromBase64String(blobEntity.ByteArray);
                    Stream stream = new MemoryStream(bytes1);
                    await blobClient.UploadAsync(stream, true);

                    seedPerformance.Harvest_img_file_location = this.blobconfig.Value.BlobSeedPhoto;
                    seedPerformance.Harvest_img_file_name = blobEntity.FolderName;
                }

                dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@application_no", Value = seedPerformance.Application_no, SqlDbType = SqlDbType.BigInt, Direction = ParameterDirection.Input });

                dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@area_sown", Value = seedPerformance.Area_sown == null ? DBNull.Value : (object)seedPerformance.Area_sown, SqlDbType = SqlDbType.Decimal, Direction = ParameterDirection.Input });

                dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@production", Value = seedPerformance.Production == null ? DBNull.Value : (object)seedPerformance.Production, SqlDbType = SqlDbType.Decimal, Direction = ParameterDirection.Input });

                dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@productivity", Value = seedPerformance.Productivity == null ? DBNull.Value : (object)seedPerformance.Productivity, SqlDbType = SqlDbType.Decimal, Direction = ParameterDirection.Input });

                dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@sown_reported_dt", Value = seedPerformance.Sown_reported_dt == null ? DBNull.Value : (object)seedPerformance.Sown_reported_dt, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });

                dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@sown_latitude", Value = string.IsNullOrEmpty(seedPerformance.Sown_latitude) ? DBNull.Value : (object)seedPerformance.Sown_latitude, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });

                dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@sown_longitude", Value = string.IsNullOrEmpty(seedPerformance.Sown_longitude) ? DBNull.Value : (object)seedPerformance.Sown_longitude, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });

                dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@sown_img_file_location", Value = string.IsNullOrEmpty(seedPerformance.Sown_img_file_location) ? DBNull.Value : (object)seedPerformance.Sown_img_file_location, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });

                dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@sown_img_file_name", Value = string.IsNullOrEmpty(seedPerformance.Sown_img_file_name) ? DBNull.Value : (object)seedPerformance.Sown_img_file_name, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });

                dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@sown_submitted_userid", Value = seedPerformance.Sown_submitted_userid == null ? DBNull.Value : (object)seedPerformance.Sown_submitted_userid, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });

                dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@harvest_reported_dt", Value = seedPerformance.Harvest_reported_dt == null ? DBNull.Value : (object)seedPerformance.Harvest_reported_dt, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });

                dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@harvest_latitude", Value = string.IsNullOrEmpty(seedPerformance.Harvest_latitude) ? DBNull.Value : (object)seedPerformance.Harvest_latitude, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });

                dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@harvest_longitude", Value = string.IsNullOrEmpty(seedPerformance.Harvest_longitude) ? DBNull.Value : (object)seedPerformance.Harvest_longitude, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });

                dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@harvest_img_file_location", Value = string.IsNullOrEmpty(seedPerformance.Harvest_img_file_location) ? DBNull.Value : (object)seedPerformance.Harvest_img_file_location, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });

                dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@harvest_img_file_name", Value = string.IsNullOrEmpty(seedPerformance.Harvest_img_file_name) ? DBNull.Value : (object)seedPerformance.Harvest_img_file_name, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });

                dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@harvest_submitted_userid", Value = seedPerformance.Harvest_submitted_userid == null ? DBNull.Value : (object)seedPerformance.Harvest_submitted_userid, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });

                dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@response_status", SqlDbType = SqlDbType.VarChar, Size = 500, Direction = ParameterDirection.Output });

                Dictionary<string, dynamic> result = SqlHelper.ExecuteNonQuery<SqlConnection>(sp_seed_perf, dbparamsUserInfo, SqlHelper.ExecutionType.Procedure);

                insertRowsCount += result["RowsAffected"];

                string spOut = DBNull.Value.Equals(result["@response_status"]) ? string.Empty : result["@response_status"];

                if (!string.IsNullOrEmpty(spOut))
                {
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
                        else if (splitteddata[0].Trim().Equals("ApplicationNo"))
                        {
                            respobj.ApplicationNo = splitteddata[1].ToString();
                        }
                    }
                }
                else
                {
                    respobj.Status = "Failed";
                    respobj.Reason = "SomeErrorOccured";
                    respobj.ApplicationNo = string.Empty;
                }
            }

            return respobj;
        }

        /// <summary>
        /// GetSeedPerformance.
        /// </summary>
        /// <param name="panchayatId">panchayatId.</param>
        /// <param name="seasonId">seasonId.</param>
        /// <returns>Reponse Values.</returns>
        public List<DtoSeedPerformance> GetSeedPerformance(int panchayatId, int seasonId)
        {
            List<DtoSeedPerformance> list = new List<DtoSeedPerformance>();

            List<DbParameter> parameters = new List<DbParameter>();

            parameters.Add(new SqlParameter { ParameterName = "@panchayat_id", Value = panchayatId.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@season_id", Value = seasonId.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@district_id", Value = 0, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@block_id", Value = 0, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@query_name", Value = qn_get_seed_perf, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(sp_get_seed_perf, parameters, SqlHelper.ExecutionType.Procedure);
            if (dt != null && dt.Rows.Count > 0)
            {
                list = SqlHelper.ConvertDataTableToList<DtoSeedPerformance>(dt);
            }

            return list;
        }

        /// <summary>
        /// GetSeedPerformanceAgriculture.
        /// </summary>
        /// <param name="districtId">districtId.</param>
        /// <param name="blockId">blockId.</param>
        /// <param name="panchayatId">panchayatId.</param>
        /// <param name="seasonId">seasonId.</param>
        /// <returns>Values.</returns>
        public List<DtoSeedPerformanceAgriculture> GetSeedPerformanceAgriculture(string districtId, string blockId, string panchayatId, string seasonId)
        {
            List<DtoSeedPerformanceAgriculture> list = new List<DtoSeedPerformanceAgriculture>();

            List<DbParameter> parameters = new List<DbParameter>();

            parameters.Add(new SqlParameter { ParameterName = "@panchayat_id", Value = panchayatId.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@season_id", Value = seasonId.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@district_id", Value = Convert.ToInt32(districtId), SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@block_id", Value = Convert.ToInt32(blockId), SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@query_name", Value = qn_GetSeedPerformanceAgriculture, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(sp_get_seed_perf, parameters, SqlHelper.ExecutionType.Procedure);
            if (dt != null && dt.Rows.Count > 0)
            {
                list = SqlHelper.ConvertDataTableToList<DtoSeedPerformanceAgriculture>(dt);
            }

            return list;
        }

        /// <summary>
        /// GetAgricultureBlockkPanchayatByDistrict.
        /// </summary>
        /// <param name="districtId">districtId.</param>
        /// <returns>Values.</returns>
        public BlockPanchayatByDistrict GetAgricultureBlockkPanchayatByDistrict(string districtId)
        {
            BlockPanchayatByDistrict response = new BlockPanchayatByDistrict();

            List<DbParameter> parameters = new List<DbParameter>();

            parameters.Add(new SqlParameter { ParameterName = "@panchayat_id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@season_id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@district_id", Value = Convert.ToInt32(districtId), SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@block_id", Value = 0, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@query_name", Value = qn_GetAgricultureBlkPnchyt, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(sp_get_seed_perf, parameters, SqlHelper.ExecutionType.Procedure);
            if (dt != null && dt.Rows.Count > 0)
            {
                List<BlockPanchayatByDistricts> list = SqlHelper.ConvertDataTableToList<BlockPanchayatByDistricts>(dt);
                int[] distId = list.Select(x => x.District_id).Distinct().ToArray();

                foreach (int item in distId)
                {
                    int[] blockId = list.Where(x => x.District_id == item).Select(x => x.Block_id).Distinct().ToArray();
                    List<Blocks> blks = new List<Blocks>();
                    foreach (int blkId in blockId)
                    {
                        Blocks blk = new Blocks();
                        List<Panchayats> panchs = new List<Panchayats>();
                        List<BlockPanchayatByDistricts> panch = list.Where(x => x.District_id == item && x.Block_id == blkId).ToList();
                        foreach (var data in panch)
                        {
                            Panchayats panchayat = new Panchayats();
                            panchayat.Panchayat_lg_code = data.Panchayat_id;
                            panchayat.Panchayat_name = data.Panchayat_name;
                            panchs.Add(panchayat);
                        }

                        blk.Block_lg_code = blkId;
                        blk.Block_name = list.Where(x => x.Block_id == blkId).Select(x => x.Block_name).FirstOrDefault();
                        blk.Panchayat = panchs;
                        blks.Add(blk);
                    }

                    response.District_lg_code = item;
                    response.District_name = list.Where(x => x.District_id == item).Select(x => x.District_name).FirstOrDefault();
                    response.Blocks = blks;
                }
            }

            return response;
        }

        /// <summary>
        /// GetSchemesByPanchayat.
        /// </summary>
        /// <param name="panchayatId">panchayatId.</param>
        /// <returns>Values.</returns>
        public List<SchemeModel> GetSchemesByPanchayat(int panchayatId)
        {
            List<SchemeModel> list = new List<SchemeModel>();

            List<DbParameter> parameters = new List<DbParameter>();

            string query = " SELECT xref.scheme_id,scheme_code,session_name,scheme_type,scheme_name " +
                           " FROM BRBN_SCHEME_DIM dim, BRBN_SCHEME_LGDIR_XREF xref " +
                           " where xref.scheme_id = dim.scheme_id and xref.panchayat_id = @panchayatId;";

            parameters.Add(new SqlParameter { ParameterName = "@panchayatId", Value = panchayatId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });

            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(query, parameters, SqlHelper.ExecutionType.Query);
            if (dt != null && dt.Rows.Count > 0)
            {
                list = SqlHelper.ConvertDataTableToList<SchemeModel>(dt);
            }

            return list;
        }

        /// <summary>
        /// GetAllColdStorage.
        /// </summary>
        /// <param name="getHortiReportPHMS">getHortiReportPHMS.</param>
        /// <returns>Response Modle Values.</returns>
        public List<GetGrpdwnColdStorageModel> GetAllColdStorage(GetHortiReportPhmsModel getHortiReportPHMS)
        {
            List<GetGrpdwnColdStorageModel> response = new List<GetGrpdwnColdStorageModel>();
            DataSet dataSet = new DataSet();
            List<DbParameter> parameters = new List<DbParameter>();
            parameters.Add(new SqlParameter { ParameterName = "@User_Id", Value = getHortiReportPHMS.User_Id, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@query_name", Value = qn_GetAllColdStorage, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@Year", Value = getHortiReportPHMS.Year, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@Month", Value = getHortiReportPHMS.Month, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@Crop_activity", Value = getHortiReportPHMS.Crop_activity, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@struct_type", Value = getHortiReportPHMS.Struct_type, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@date", Value = DBNull.Value, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@Approval_Status", Value = getHortiReportPHMS.Approval_Status, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@District_ID", Value = getHortiReportPHMS.District_ID, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@block_ID", Value = getHortiReportPHMS.Block_ID, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@Panchayat_ID", Value = getHortiReportPHMS.Panchayat_ID, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@Crop_id", Value = getHortiReportPHMS.Crop_id, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@Stor_Name_Address", Value = "0", SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dataSet = SqlHelper.ExecuteDataSet<SqlConnection>(sp_GetAllColdStorage, parameters, SqlHelper.ExecutionType.Procedure);

            if (dataSet != null && dataSet.Tables.Count > 0)
            {
                if (dataSet.Tables[0].Rows.Count > 0)
                {
                    response = SqlHelper.ConvertDataTableToList<GetGrpdwnColdStorageModel>(dataSet.Tables[0]);
                }

                return response;
            }

            return null;
        }

        /// <summary>
        /// GetAllColdStorageByDistrict.
        /// </summary>
        /// <param name="getHortiReportPHMS">getHortiReportPHMS.</param>
        /// <returns>GetGrpdwnColdStorageModel List.</returns>
        public List<GetGrpdwnColdStorageModel> GetAllColdStorageByDistrict(GetHortiReportPhmsModel getHortiReportPHMS)
        {
            List<GetGrpdwnColdStorageModel> response = new List<GetGrpdwnColdStorageModel>();
            DataSet dataSet = new DataSet();
            List<DbParameter> parameters = new List<DbParameter>();
            parameters.Add(new SqlParameter { ParameterName = "@User_Id", Value = getHortiReportPHMS.User_Id, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@query_name", Value = qn_GetAllColdStorageByDistrict, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@Year", Value = getHortiReportPHMS.Year, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@Month", Value = getHortiReportPHMS.Month, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@Crop_activity", Value = getHortiReportPHMS.Crop_activity, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@struct_type", Value = getHortiReportPHMS.Struct_type, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@date", Value = DBNull.Value, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@Approval_Status", Value = (getHortiReportPHMS.Approval_Status == null) ? string.Empty : getHortiReportPHMS.Approval_Status, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@District_ID", Value = getHortiReportPHMS.District_ID, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@block_ID", Value = getHortiReportPHMS.Block_ID, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@Panchayat_ID", Value = getHortiReportPHMS.Panchayat_ID, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@Crop_id", Value = getHortiReportPHMS.Crop_id, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@Stor_Name_Address", Value = "0", SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@strg_id", Value = "0", SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dataSet = SqlHelper.ExecuteDataSet<SqlConnection>(sp_GetAllColdStorage, parameters, SqlHelper.ExecutionType.Procedure);

            if (dataSet != null && dataSet.Tables.Count > 0)
            {
                if (dataSet.Tables[0].Rows.Count > 0)
                {
                    response = SqlHelper.ConvertDataTableToList<GetGrpdwnColdStorageModel>(dataSet.Tables[0]);
                }

                return response;
            }

            return null;
        }

        /// <summary>
        /// GetBAONotSubmittedByBlock.
        /// </summary>
        /// <param name="block_id">block_id.</param>
        /// <returns>List Values.</returns>
        public List<CropDamageDetailsGet> GetBAONotSubmittedByBlock(int block_id)
        {
            CropDamageAll cropDamageAll = new CropDamageAll();
            List<GetNotSubittedModel> getNotSubittedModel = new List<GetNotSubittedModel>();
            DataSet dataSet = new DataSet();
            List<DbParameter> parameters = new List<DbParameter>();
            parameters.Add(new SqlParameter { ParameterName = "@query_name", Value = qn_GetBAONotSubmittedByBlock, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@District_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@Block_id", Value = block_id, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@Panchayat_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@damage_reason_id", Value = DBNull.Value, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@damage_id", Value = DBNull.Value, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            dataSet = SqlHelper.ExecuteDataSet<SqlConnection>(sp_crp_dmg_ctlr_get, parameters, SqlHelper.ExecutionType.Procedure);

            if (dataSet != null && dataSet.Tables.Count > 0)
            {
                getNotSubittedModel = SqlHelper.ConvertDataTableToList<GetNotSubittedModel>(dataSet.Tables[0]);
            }

            List<CropDamageDetailsGet> crodmglist = getNotSubittedModel.Select(x => new CropDamageDetailsGet
            {
                PanchayatId = x.Panchayat_ID,
                Panchayat_Name = getNotSubittedModel.Count > 0 ? getNotSubittedModel.Where(a => a.Panchayat_ID == x.Panchayat_ID).Select(x => x.Panchayat_Name).First().ToString() : string.Empty,
                Block_Id = x.Block_id.ToString(),
                Block_Name = x.Block_name,
                District_ID = x.District_ID.ToString(),
                District_name = x.District_name,
            }).ToList();
            foreach (var item in crodmglist)
            {
                item.CoveredArea = cropDamageAll.CropdamageCoverages.Count == 0 ? new CoveredAreaGet() : cropDamageAll.CropdamageCoverages.Where(x => x.Damage_reason_id == item.DamageReasonId)?.Select(x => new CoveredAreaGet
                {
                    Irrigated_Dmg_Area = x.Irrigated_area,
                    Non_irrigated_Dmg_Area = x.Nonirrigated_area,
                    Perennial_horticulture = x.Perennial_horti,
                    Perennial_sugarcane = x.Perennial_sugarcane,
                    Total = x.Total_area,
                    GrandTotal = x.Grand_total_area,
                }).FirstOrDefault();
                item.AffectedArea = cropDamageAll.CropdamageImpactModels.Count == 0 ? new AffectedAreaGet() : cropDamageAll.CropdamageImpactModels.Where(x => x.Damage_reason_id == item.DamageReasonId)?.Select(x => new AffectedAreaGet
                {
                    Perennial_horti_impact = x.Perennial_horti_impact,
                    Perennial_sugarcane_impact = x.Perennial_sugarcane_impact,
                    Total_impact_area = x.Total_impact_area,
                    Grand_total_impact = x.Grand_total_impact,
                    CropName = cropDamageAll.CropdamageImpactModels.Count == 0 ? new List<CropNameGet>() : cropDamageAll.CropdamageImpactModels.Where(x => x.Damage_reason_id == item.DamageReasonId)?.Select(x => new CropNameGet
                    {
                        Crop_id = x.Crop_id,
                        Crop_name = x.Crop_name,
                        Crop_value = x.Damage_area,
                    }).ToList(),
                }).FirstOrDefault();

                if (item.AffectedArea.CropName.Count == 0)
                {
                    CropNameGet cropNameGet = new CropNameGet();
                    cropNameGet.Crop_id = null;
                    cropNameGet.Crop_name = null;
                    cropNameGet.Crop_value = null;
                    item.AffectedArea.CropName.Add(cropNameGet);
                }

                var damageOutput = cropDamageAll.CropdamageDetailsModels.Where(x => x.Damage_reason_id == item.DamageReasonId)?.Select(x => new DamageAreaGet
                {
                    Irrigated_Dmg_Area = x.Irrigated_area_dmg,
                    Irrigated_Dmg_Area_Estimated = x.Irrigated_cost_dmg,
                    Non_irrigated_Dmg_Area = x.Nonirrigated_area_dmg,
                    Non_irrigated_Dmg_Area_Estimated = x.Nonirrigated_cost_dmg,
                    Perennial_horticulture = x.Perennial_horti_dmg,
                    Perennial_horticulture_Estimated = x.Perennial_horti_cost_dmg,
                    Perennial_sugarcane = x.Perennial_sugarcane_dmg,
                    Perennial_sugarcane_Estimated = x.Perennial_sugarcane_cost_dmg,
                    TotalArea = x.Total_area_dmg,
                    GrandTotalArea = x.Total_cost_dmg,
                    GrandTotalAmount = x.Grand_total_area_dmg,
                    TotalAmount = x.Grand_total_cost_dmg,
                }).FirstOrDefault();

                item.DamageArea = damageOutput == null ? new DamageAreaGet() : damageOutput;
            }

            return crodmglist;
        }

        /// <summary>
        /// GetDAONotSubmittedByDistrict.
        /// </summary>
        /// <param name="district_Id">district_Id.</param>
        /// <returns>List Values.</returns>
        public List<CropDamageDetailsGet> GetDAONotSubmittedByDistrict(int district_Id)
        {
            CropDamageAll cropDamageAll = new CropDamageAll();
            List<GetNotSubittedModel> getNotSubittedModel = new List<GetNotSubittedModel>();
            DataSet dataSet = new DataSet();
            List<DbParameter> parameters = new List<DbParameter>();
            parameters.Add(new SqlParameter { ParameterName = "@query_name", Value = qn_GetDAONotSubmittedByDistrict, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@District_Id", Value = district_Id, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@Block_id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@Panchayat_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@damage_reason_id", Value = DBNull.Value, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@damage_id", Value = DBNull.Value, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            dataSet = SqlHelper.ExecuteDataSet<SqlConnection>(sp_crp_dmg_ctlr_get, parameters, SqlHelper.ExecutionType.Procedure);

            if (dataSet != null && dataSet.Tables.Count > 0)
            {
                getNotSubittedModel = SqlHelper.ConvertDataTableToList<GetNotSubittedModel>(dataSet.Tables[0]);
            }

            List<CropDamageDetailsGet> crodmglist = getNotSubittedModel.Select(x => new CropDamageDetailsGet
            {
                PanchayatId = x.Panchayat_ID,
                Panchayat_Name = getNotSubittedModel.Count > 0 ? getNotSubittedModel.Where(a => a.Panchayat_ID == x.Panchayat_ID).Select(x => x.Panchayat_Name).First().ToString() : string.Empty,
                Block_Id = x.Block_id.ToString(),
                Block_Name = x.Block_name,
                District_ID = x.District_ID.ToString(),
                District_name = x.District_name,
            }).ToList();

            foreach (var item in crodmglist)
            {
                item.CoveredArea = cropDamageAll.CropdamageCoverages.Count == 0 ? new CoveredAreaGet() : cropDamageAll.CropdamageCoverages.Where(x => x.Damage_reason_id == item.DamageReasonId)?.Select(x => new CoveredAreaGet
                {
                    Irrigated_Dmg_Area = x.Irrigated_area,
                    Non_irrigated_Dmg_Area = x.Nonirrigated_area,
                    Perennial_horticulture = x.Perennial_horti,
                    Perennial_sugarcane = x.Perennial_sugarcane,
                    Total = x.Total_area,
                    GrandTotal = x.Grand_total_area,
                }).FirstOrDefault();

                item.AffectedArea = cropDamageAll.CropdamageImpactModels.Count == 0 ? new AffectedAreaGet() : cropDamageAll.CropdamageImpactModels.Where(x => x.Damage_reason_id == item.DamageReasonId)?.Select(x => new AffectedAreaGet
                {
                    Perennial_horti_impact = x.Perennial_horti_impact,
                    Perennial_sugarcane_impact = x.Perennial_sugarcane_impact,
                    Total_impact_area = x.Total_impact_area,
                    Grand_total_impact = x.Grand_total_impact,
                    CropName = cropDamageAll.CropdamageImpactModels.Count == 0 ? new List<CropNameGet>() : cropDamageAll.CropdamageImpactModels.Where(x => x.Damage_reason_id == item.DamageReasonId)?.Select(x => new CropNameGet
                    {
                        Crop_id = x.Crop_id,
                        Crop_name = x.Crop_name,
                        Crop_value = x.Damage_area,
                    }).ToList(),
                }).FirstOrDefault();
                if (item.AffectedArea.CropName.Count == 0)
                {
                    CropNameGet cropNameGet = new CropNameGet();
                    cropNameGet.Crop_id = null;
                    cropNameGet.Crop_name = null;
                    cropNameGet.Crop_value = null;
                    item.AffectedArea.CropName.Add(cropNameGet);
                }

                var damageOutput = cropDamageAll.CropdamageDetailsModels.Where(x => x.Damage_reason_id == item.DamageReasonId)?.Select(x => new DamageAreaGet
                {
                    Irrigated_Dmg_Area = x.Irrigated_area_dmg,
                    Irrigated_Dmg_Area_Estimated = x.Irrigated_cost_dmg,
                    Non_irrigated_Dmg_Area = x.Nonirrigated_area_dmg,
                    Non_irrigated_Dmg_Area_Estimated = x.Nonirrigated_cost_dmg,
                    Perennial_horticulture = x.Perennial_horti_dmg,
                    Perennial_horticulture_Estimated = x.Perennial_horti_cost_dmg,
                    Perennial_sugarcane = x.Perennial_sugarcane_dmg,
                    Perennial_sugarcane_Estimated = x.Perennial_sugarcane_cost_dmg,
                    TotalArea = x.Total_area_dmg,
                    GrandTotalArea = x.Total_cost_dmg,
                    GrandTotalAmount = x.Grand_total_area_dmg,
                    TotalAmount = x.Grand_total_cost_dmg,
                }).FirstOrDefault();
                item.DamageArea = damageOutput == null ? new DamageAreaGet() : damageOutput;
            }

            return crodmglist;
        }

        /// <summary>
        /// GetHorticultureSubmittedDetails.
        /// </summary>
        /// <param name="seasonId">seasonId.</param>
        /// <param name="districtId">districtId.</param>
        /// <returns>List.</returns>
        public List<HorticultureDetails> GetHorticultureSubmittedDetails(int seasonId, int districtId)
        {
            List<HorticultureDetails> list = new List<HorticultureDetails>();
            List<DbParameter> dbparams = new List<DbParameter>();
            dbparams.Add(new SqlParameter { ParameterName = "@query_name", Value = qn_PostHorticultureSel, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@Season_Id", Value = seasonId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@District_Id", Value = districtId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(sp_dim_cntrlr, dbparams, SqlHelper.ExecutionType.Procedure);
            if (dt != null && dt.Rows.Count > 0)
            {
                list = SqlHelper.ConvertDataTableToList<HorticultureDetails>(dt);
            }

            return list;
        }

        /// <summary>
        /// GetSpecificDamageReason.
        /// </summary>
        /// <param name="damageDetails">damageDetails.</param>
        /// <returns>List Values.</returns>
        public DtoEditDamage GetSpecificDamageReason(DtoEditDamageRequest damageDetails)
        {
            DtoEditDamage damage = null;
            List<DbParameter> dbparams = new List<DbParameter>();

            if (!string.IsNullOrEmpty(damageDetails.Assigned_Crop_Id) && !string.IsNullOrEmpty(damageDetails.Assigned_District_Ids))
            {
                dbparams.Add(new SqlParameter { ParameterName = "@query_name", Value = qn_GetSpecificDamageReason, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            }
            else
            {
                dbparams.Add(new SqlParameter { ParameterName = "@query_name", Value = qn_GetSpecificDamageDetailsByReasonID, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            }

            dbparams.Add(new SqlParameter { ParameterName = "@damage_reason_id", Value = damageDetails.Damage_reason_id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@Assigned_Crop_Id", Value = string.IsNullOrEmpty(damageDetails.Assigned_Crop_Id) ? DBNull.Value : (object)damageDetails.Assigned_Crop_Id, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@Assigned_District_Ids", Value = string.IsNullOrEmpty(damageDetails.Assigned_District_Ids) ? DBNull.Value : (object)damageDetails.Assigned_District_Ids, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@Year", Value = damageDetails.Year, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });

            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(sp_dmg_list_ctrlr, dbparams, SqlHelper.ExecutionType.Procedure);
            if (dt != null && dt.Rows.Count > 0)
            {
                damage = SqlHelper.ConvertDataTableToList<DtoEditDamage>(dt)[0];
            }

            return damage;
        }

        /// <summary>
        /// GetDamageReasonNames.
        /// </summary>
        /// <returns>List.</returns>
        public List<CropDamageReasonNames> GetDamageReasonNames()
        {
            List<CropDamageReasonNames> list = new List<CropDamageReasonNames>();
            List<DbParameter> dbparams = new List<DbParameter>();
            dbparams.Add(new SqlParameter { ParameterName = "@query_name", Value = qn_GetDamageReasonNames, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });

            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(sp_dmg_list_ctrlr, dbparams, SqlHelper.ExecutionType.Procedure);
            if (dt != null && dt.Rows.Count > 0)
            {
                list = SqlHelper.ConvertDataTableToList<CropDamageReasonNames>(dt);
            }

            return list;
        }

        /// <summary>
        /// GetDamageCropList.
        /// </summary>
        /// <returns>List Values.</returns>
        public List<CropNames> GetDamageCropList()
        {
            List<CropNames> list = new List<CropNames>();
            List<DbParameter> dbparams = new List<DbParameter>();
            dbparams.Add(new SqlParameter { ParameterName = "@query_name", Value = qn_GetDamageCropList, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });

            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(sp_dmg_list_ctrlr, dbparams, SqlHelper.ExecutionType.Procedure);
            if (dt != null && dt.Rows.Count > 0)
            {
                list = SqlHelper.ConvertDataTableToList<CropNames>(dt);
            }

            return list;
        }

        /// <summary>
        /// GetDamageConstantCropList.
        /// </summary>
        /// <returns>Liost Values.</returns>
        public List<CropNames> GetDamageConstantCropList()
        {
            List<CropNames> list = new List<CropNames>();
            List<DbParameter> dbparams = new List<DbParameter>();
            dbparams.Add(new SqlParameter { ParameterName = "@query_name", Value = qn_GetDamageConstantCrops, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });

            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(sp_dmg_list_ctrlr, dbparams, SqlHelper.ExecutionType.Procedure);
            if (dt != null && dt.Rows.Count > 0)
            {
                list = SqlHelper.ConvertDataTableToList<CropNames>(dt);
            }

            return list;
        }

        /// <summary>
        /// GetEstdCropDamagePercnt.
        /// </summary>
        /// <param name="cropDamageReasonModel">cropDamageReasonModel.</param>
        /// <returns>List.</returns>
        public List<CropDamageGetModel> GetEstdCropDamagePercnt(CropDamageGetModel cropDamageReasonModel)
        {
            List<CropDamageGetModel> list = new List<CropDamageGetModel>();
            List<DbParameter> dbparams = new List<DbParameter>();
            dbparams.Add(new SqlParameter { ParameterName = "@query_name", Value = qn_GetEstdCropDamagePercnt, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@Year", Value = cropDamageReasonModel.Year, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@Adv_Search_Str", Value = cropDamageReasonModel.Adv_Search_Str, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@damage_reason_id", Value = cropDamageReasonModel.Damage_reason_id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@damage_reason_name", Value = cropDamageReasonModel.Damage_reason_name, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@Assigned_Crop_Id", Value = cropDamageReasonModel.Assigned_Crop_Id, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@Assigned_District_Id", Value = cropDamageReasonModel.Assigned_District_Id, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@Estd_Crop_Damage", Value = cropDamageReasonModel.Estd_Crop_Damage, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@Status_Flg", Value = cropDamageReasonModel.Status_Flg, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@Crop_name", Value = cropDamageReasonModel.Crop_name, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });

            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(sp_dmg_list_ctrlr, dbparams, SqlHelper.ExecutionType.Procedure);
            if (dt != null && dt.Rows.Count > 0)
            {
                list = SqlHelper.ConvertDataTableToList<CropDamageGetModel>(dt);
            }

            return list;
        }

        /// <summary>
        /// GetAllListsOfDamageReasons.
        /// </summary>
        /// <param name="year">year.</param>
        /// <returns>List.</returns>
        public List<CropDamageEntity> GetAllListsOfDamageReasons(int year)
        {
            List<CropDamageEntity> list = new List<CropDamageEntity>();
            List<DbParameter> dbparams = new List<DbParameter>();
            dbparams.Add(new SqlParameter { ParameterName = "@query_name", Value = qn_GetAllListsOfDamageReasons, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@Year", Value = year, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });

            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(sp_dmg_list_ctrlr, dbparams, SqlHelper.ExecutionType.Procedure);
            if (dt != null && dt.Rows.Count > 0)
            {
                list = SqlHelper.ConvertDataTableToList<CropDamageEntity>(dt);
            }

            return list;
        }

        /// <summary>
        /// GetHorticultureCrop.
        /// </summary>
        /// <param name="districtId">districtId.</param>
        /// <param name="seasonId">seasonId.</param>
        /// <returns>List.</returns>
        public List<HorticultureCrop> GetHorticultureCrop(int districtId, int seasonId)
        {
            List<HorticultureCrop> list = new List<HorticultureCrop>();
            List<DbParameter> dbparams = new List<DbParameter>();
            dbparams.Add(new SqlParameter { ParameterName = "@query_name", Value = qn_GetHorticultureCrop, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@District_Id", Value = districtId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@Season_Id", Value = seasonId, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(sp_dim_cntrlr, dbparams, SqlHelper.ExecutionType.Procedure);
            if (dt != null && dt.Rows.Count > 0)
            {
                list = SqlHelper.ConvertDataTableToList<HorticultureCrop>(dt);
            }

            return list;
        }

        /// <summary>
        /// GetBAOCropDamageDetailsBlock.
        /// </summary>
        /// <param name="districtId">districtId.</param>
        /// <param name="blockid">blockid.</param>
        /// <param name="damage_id">damage_id.</param>
        /// <returns>List.</returns>
        public InsBaoCropdmgModel GetBAOCropDamageDetailsBlock(int districtId, int blockid, int damage_id)
        {
            CropDamageAll cropDamageAll = new CropDamageAll();
            DataSet dataSet = new DataSet();
            List<DbParameter> parameters = new List<DbParameter>();
            parameters.Add(new SqlParameter { ParameterName = "@query_name", Value = qn_GetBAOCropDamageDetailsBlock, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@District_Id", Value = districtId, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@Block_id", Value = blockid, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@Panchayat_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@damage_reason_id", Value = damage_id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@damage_id", Value = damage_id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            dataSet = SqlHelper.ExecuteDataSet<SqlConnection>(sp_crp_dmg_ctlr_get, parameters, SqlHelper.ExecutionType.Procedure);

            if (dataSet != null && dataSet.Tables.Count > 0)
            {
                cropDamageAll.CropdamageCoverages = SqlHelper.ConvertDataTableToList<CropDamageCoverage>(dataSet.Tables[0]);
                cropDamageAll.CropdamageImpactModels = SqlHelper.ConvertDataTableToList<CropDamageImpactModel>(dataSet.Tables[1]);

                cropDamageAll.CropdamageDetailsModels = SqlHelper.ConvertDataTableToList<CropDamageDetailsModel>(dataSet.Tables[2]);

                if (cropDamageAll.CropdamageCoverages.Any())
                {
                    InsBaoCropdmgModel cropDamageGetAll = cropDamageAll.CropdamageCoverages.Select(x => new InsBaoCropdmgModel
                    {
                        Reported_date = x.Rec_created_date.ToString(),
                        Damage_Reason = x.Damage_reason_name,
                        Damage_Reason_id = damage_id.ToString(),
                        District_Id = x.District_id.ToString(),
                        District_Name = x.District_name,
                        CreatedBy = x.Rec_created_username,
                        UpdatedBy = x.Rec_updated_username,
                        Refreshed_date = null,
                        Refreshed_userid = null,
                        Dm_approval_flag = x.DM_Final_Approval_Flag,
                    }).FirstOrDefault();

                    cropDamageGetAll.BlockList = cropDamageAll.CropdamageCoverages.GroupBy(x => x.Block_id).Select(x => x.First()).
                        AsEnumerable().Select(x => new BlockList
                        {
                            Reported_date = x.Rec_created_date.ToString(),
                            Block_Id = x.Block_id.ToString(),
                            DamageReasonId = x.Damage_reason_id,
                            Damage_Reason = x.Damage_reason_name,
                            Block_Name = x.Block_name,
                            Net_area_sown = x.Net_sown_area.ToString(),
                            CreatedBy = x.Rec_created_username,
                            UpdatedBy = x.Rec_updated_username,
                            Refreshed_date = null,
                            Refreshed_userid = null,
                            Refreshed_username = null,
                            PanchayatList = cropDamageAll.CropdamageCoverages.GroupBy(x => new { x.Panchayat_id, x.Damage_reason_id }).Select(x => x.First()).AsEnumerable().Select(x => new CropDamageDetailsGet
                            {
                                Reported_date = x.Rec_created_date.ToString(),
                                DamageReasonId = x.Damage_reason_id,
                                PanchayatId = x.Panchayat_id,
                                Panchayat_Name = x.Panchayat_name,
                                Block_Id = x.Block_id.ToString(),
                                Block_Name = x.Block_name,
                                District_ID = x.District_id.ToString(),
                                District_name = x.District_name,
                                Ac_Submitted_date = x.AC_Submitted_date,
                                Ac_Submitted_userid = x.AC_Submitted_userid,
                                DamageReasonCreatedDate = x.Rec_created_date,
                                Damage_Reason = x.Damage_reason_name,
                                CropDamagePercentage = x.Estd_Crop_Damage,
                                NetSownArea = x.Net_sown_area,
                                Bao_Approval_flag = x.BAO_Approval_flag,
                                Bao_Approval_Reason = x.Bao_comments,
                                Bao_Approved_date = x.BAO_Approved_date,
                                Bao_Approved_userid = x.BAO_Approved_userid,
                                Dm_finalapprovalFlag = x.DM_Final_Approval_Flag,
                                Dao_Approval_flag = x.DAO_Approval_flag,
                                Dao_Approval_Reason = x.Dao_comments,
                                Dao_Approved_date = x.DAO_Approved_date,
                                Dao_Approved_userid = x.DAO_Approved_userid,
                                Ac_submitted_username = x.Ac_submitted_username,
                                Bao_approved_username = x.Bao_submitted_username,
                                Dao_approved_username = x.Dao_submitted_username,
                                Submission_source = null,
                                Ac_submit_flag = x.Ac_submit_flag,
                                Bao_add_edit_flag = x.Bao_add_edit_flag,
                                Dao_add_edit_flag = x.Dao_add_edit_flag,
                                Rec_updated_userid = x.Rec_updated_userid,
                                Rec_updated_date = x.Rec_updated_date,
                                UpdatedBy = x.Rec_updated_username,
                            }).ToList(),
                        }).Distinct().ToList();

                    foreach (var item in cropDamageGetAll.BlockList)
                    {
                        foreach (var panch in item.PanchayatList)
                        {
                            panch.CoveredArea = cropDamageAll.CropdamageCoverages.Count == 0 ? new CoveredAreaGet() : cropDamageAll.CropdamageCoverages.Where(x => x.Panchayat_id == panch.PanchayatId && x.Damage_reason_id == panch.DamageReasonId).Select(x => new CoveredAreaGet
                            {
                                Irrigated_Dmg_Area = x.Irrigated_area,
                                Non_irrigated_Dmg_Area = x.Nonirrigated_area,
                                Perennial_horticulture = x.Perennial_horti,
                                Perennial_sugarcane = x.Perennial_sugarcane,
                                Total = x.Total_area,
                                GrandTotal = x.Grand_total_area,
                            }).FirstOrDefault();

                            panch.AffectedArea = cropDamageAll.CropdamageImpactModels.Where(x => x.Panchayat_id == panch.PanchayatId && x.Damage_reason_id == panch.DamageReasonId).Select(x => new AffectedAreaGet
                            {
                                Perennial_horti_impact = x.Perennial_horti_impact,
                                Perennial_sugarcane_impact = x.Perennial_sugarcane_impact,
                                Total_impact_area = x.Total_impact_area,
                                Grand_total_impact = x.Grand_total_impact,
                                CropName = cropDamageAll.CropdamageImpactModels.Count == 0 ? new List<CropNameGet>() : cropDamageAll.CropdamageImpactModels.Where(x => x.Damage_reason_id == panch.DamageReasonId && x.Panchayat_id == panch.PanchayatId).Select(x => new CropNameGet
                                {
                                    Crop_id = x.Crop_id,
                                    Crop_value = x.Damage_area,
                                    Crop_name = x.Crop_name,
                                }).ToList(),
                            }).FirstOrDefault();
                            panch.AffectedArea = panch.AffectedArea == null ? new AffectedAreaGet() : panch.AffectedArea;
                            panch.AffectedArea.CropName = cropDamageAll.CropdamageImpactModels.Where(x => x.Damage_reason_id == damage_id && x.Panchayat_id == panch.PanchayatId).Select(x => new CropNameGet
                            {
                                Crop_id = x.Crop_id,
                                Crop_value = x.Damage_area,
                                Crop_name = x.Crop_name,
                            }).ToList();
                            if (panch.AffectedArea.CropName.Count == 0)
                            {
                                CropNameGet cropNameGet = new CropNameGet();
                                cropNameGet.Crop_id = null;
                                cropNameGet.Crop_name = null;
                                cropNameGet.Crop_value = null;
                                panch.AffectedArea.CropName.Add(cropNameGet);
                            }

                            panch.DamageArea = cropDamageAll.CropdamageDetailsModels.Where(x => x.Panchayat_id == panch.PanchayatId && x.Damage_reason_id == panch.DamageReasonId).Select(x => new DamageAreaGet
                            {
                                Irrigated_Dmg_Area = x.Irrigated_area_dmg,
                                Irrigated_Dmg_Area_Estimated = x.Irrigated_cost_dmg,
                                Non_irrigated_Dmg_Area = x.Nonirrigated_area_dmg,
                                Non_irrigated_Dmg_Area_Estimated = x.Nonirrigated_cost_dmg,
                                Perennial_horticulture = x.Perennial_horti_dmg,
                                Perennial_horticulture_Estimated = x.Perennial_horti_cost_dmg,
                                Perennial_sugarcane = x.Perennial_sugarcane_dmg,
                                Perennial_sugarcane_Estimated = x.Perennial_sugarcane_cost_dmg,
                                TotalArea = x.Total_area_dmg,
                                GrandTotalArea = x.Grand_total_area_dmg,
                                TotalAmount = x.Total_cost_dmg,
                                GrandTotalAmount = x.Grand_total_cost_dmg,
                            }).FirstOrDefault();

                            panch.DamageArea = panch.DamageArea == null ? new DamageAreaGet() : panch.DamageArea;
                        }
                    }

                    return cropDamageGetAll;
                }
            }

            return null;
        }

        /// <summary>
        /// GetCropDamageReasonDrpdwn.
        /// </summary>
        /// <param name="district_Id">district_Id.</param>
        /// <returns>List.</returns>
        public List<GetCropDamageReasonDrpDwn> GetCropDamageReasonDrpdwn(string district_Id)
        {
            List<GetCropDamageReasonDrpDwn> list = new List<GetCropDamageReasonDrpDwn>();
            List<DbParameter> dbparamsCropInfo = new List<DbParameter>();
            dbparamsCropInfo.Add(new SqlParameter { ParameterName = "@query_name", Value = qn_GetDamageReasons, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfo.Add(new SqlParameter { ParameterName = "@District_Id", Value = district_Id, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfo.Add(new SqlParameter { ParameterName = "@Block_id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfo.Add(new SqlParameter { ParameterName = "@Panchayat_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfo.Add(new SqlParameter { ParameterName = "@damage_reason_id", Value = DBNull.Value, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            dbparamsCropInfo.Add(new SqlParameter { ParameterName = "@damage_id", Value = DBNull.Value, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });

            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(sp_crp_dmg_ctlr_get, dbparamsCropInfo, SqlHelper.ExecutionType.Procedure);
            GetCropDamageReasonDrpDwn getCropDamageReason = new GetCropDamageReasonDrpDwn();
            if (dt != null && dt.Rows.Count > 0)
            {
                for (var i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    getCropDamageReason = SqlHelper.ConvertDataTableToList<GetCropDamageReasonDrpDwn>(dt)[i];
                    list.Add(getCropDamageReason);
                }
            }

            return list;
        }

        /// <summary>
        /// GetDAOApprovedCropDamageDetailsDistrict.
        /// </summary>
        /// <param name="district_Id">district_Id.</param>
        /// <param name="block_id">block_id.</param>
        /// <param name="panchayat_Id">panchayat_Id.</param>
        /// <param name="damage_id">damage_id.</param>
        /// <returns>List.</returns>
        public CropDamageAll GetDAOApprovedCropDamageDetailsDistrict(string district_Id, string block_id, string panchayat_Id, int damage_id)
        {
            CropDamageAll cropDamageAll = new CropDamageAll();
            DataSet dataSet = new DataSet();
            List<DbParameter> parameters = new List<DbParameter>();
            parameters.Add(new SqlParameter { ParameterName = "@query_name", Value = qn_GetDAOApprovedCropDamageDetailsDistrict, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@District_Id", Value = district_Id, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@Block_id", Value = block_id, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@Panchayat_Id", Value = panchayat_Id, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@damage_id", Value = damage_id, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dataSet = SqlHelper.ExecuteDataSet<SqlConnection>(sp_crp_dmg_ctlr_get, parameters, SqlHelper.ExecutionType.Procedure);

            if (dataSet != null && dataSet.Tables.Count > 0)
            {
                cropDamageAll.CropdamageCoverages = SqlHelper.ConvertDataTableToList<CropDamageCoverage>(dataSet.Tables[0]);
                cropDamageAll.CropdamageImpactModels = SqlHelper.ConvertDataTableToList<CropDamageImpactModel>(dataSet.Tables[1]);

                cropDamageAll.CropdamageDetailsModels = SqlHelper.ConvertDataTableToList<CropDamageDetailsModel>(dataSet.Tables[2]);
            }

            return cropDamageAll;
        }

        /// <summary>
        /// GetBAOApprovedCropDamageDetailsBlock.
        /// </summary>
        /// <param name="district_Id">district_Id.</param>
        /// <param name="block_id">block_id.</param>
        /// <param name="panchayat_Id">panchayat_Id.</param>
        /// <param name="damage_id">damage_id.</param>
        /// <returns>List.</returns>
        public CropDamageAll GetBAOApprovedCropDamageDetailsBlock(string district_Id, string block_id, string panchayat_Id, int damage_id)
        {
            CropDamageAll cropDamageAll = new CropDamageAll();
            DataSet dataSet = new DataSet();
            List<DbParameter> parameters = new List<DbParameter>();
            parameters.Add(new SqlParameter { ParameterName = "@query_name", Value = qn_GetBAOApprovedCropDamageDetailsBlock, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@District_Id", Value = district_Id, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@Block_id", Value = block_id, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@Panchayat_Id", Value = panchayat_Id, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@damage_id", Value = damage_id, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dataSet = SqlHelper.ExecuteDataSet<SqlConnection>(sp_crp_dmg_ctlr_get, parameters, SqlHelper.ExecutionType.Procedure);

            if (dataSet != null && dataSet.Tables.Count > 0)
            {
                cropDamageAll.CropdamageCoverages = SqlHelper.ConvertDataTableToList<CropDamageCoverage>(dataSet.Tables[0]);
                cropDamageAll.CropdamageImpactModels = SqlHelper.ConvertDataTableToList<CropDamageImpactModel>(dataSet.Tables[1]);

                cropDamageAll.CropdamageDetailsModels = SqlHelper.ConvertDataTableToList<CropDamageDetailsModel>(dataSet.Tables[2]);
            }

            return cropDamageAll;
        }

        /// <summary>
        /// GetDAOCropDamageDetailsBlock.
        /// </summary>
        /// <param name="district_Id">district_Id.</param>
        /// <param name="damage_id">damage_id.</param>
        /// <returns>List.</returns>
        public InsBaoCropdmgModel GetDAOCropDamageDetailsBlock(string district_Id, int damage_id)
        {
            CropDamageAll cropDamageAll = new CropDamageAll();
            DataSet dataSet = new DataSet();
            List<DbParameter> parameters = new List<DbParameter>();
            parameters.Add(new SqlParameter { ParameterName = "@query_name", Value = qn_GetDAOCropDamageDetailsBlock, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@District_Id", Value = district_Id, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@Block_id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@Panchayat_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@damage_reason_id", Value = damage_id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@damage_id", Value = damage_id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            dataSet = SqlHelper.ExecuteDataSet<SqlConnection>(sp_crp_dmg_ctlr_get, parameters, SqlHelper.ExecutionType.Procedure);

            if (dataSet != null && dataSet.Tables.Count > 0)
            {
                cropDamageAll.CropdamageCoverages = SqlHelper.ConvertDataTableToList<CropDamageCoverage>(dataSet.Tables[0]);
                cropDamageAll.CropdamageImpactModels = SqlHelper.ConvertDataTableToList<CropDamageImpactModel>(dataSet.Tables[1]);

                cropDamageAll.CropdamageDetailsModels = SqlHelper.ConvertDataTableToList<CropDamageDetailsModel>(dataSet.Tables[2]);

                if (cropDamageAll.CropdamageCoverages.Any())
                {
                    var netshownArea = cropDamageAll.CropdamageCoverages.Where(x => x.Net_sown_area != null).Select(a => a.Net_sown_area).ToList();
                    var dm_ApprovalFlag = cropDamageAll.CropdamageCoverages.Where(x => x.DM_Final_Approval_Flag == "Y").Select(a => a.DM_Final_Approval_Flag).ToList();

                    if (netshownArea == null)
                    {
                        netshownArea = new List<decimal?>();
                    }

                    InsBaoCropdmgModel cropDamageGetAll = cropDamageAll.CropdamageCoverages.Where(x => x.Damage_reason_id != null).Select(x => new InsBaoCropdmgModel
                    {
                        Reported_date = x.Rec_created_date.ToString(),
                        Damage_Reason = x.Damage_reason_name,
                        Damage_Reason_id = x.Damage_reason_id.ToString(),
                        District_Id = x.District_id.ToString(),
                        District_Name = x.District_name,
                        CreatedBy = x.Rec_created_username,
                        UpdatedBy = x.Rec_updated_username,
                        Refreshed_date = null,
                        Refreshed_userid = null,
                        Dm_approval_flag = ((netshownArea.Count == dm_ApprovalFlag.Count) && (dm_ApprovalFlag.Count != 0)) ? "Y" : "N",
                    }).FirstOrDefault();

                    cropDamageGetAll.BlockList = cropDamageAll.CropdamageCoverages.GroupBy(x => x.Block_id).Select(x => x.First()).AsEnumerable().Select(x => new BlockList
                    {
                        Reported_date = x.Rec_created_date.ToString(),
                        Block_Id = x.Block_id.ToString(),
                        Block_Name = x.Block_name,
                        DamageReasonId = x.Damage_reason_id,
                        Damage_Reason = x.Damage_reason_name,
                        Net_area_sown = x.Net_sown_area.ToString(),
                        CreatedBy = x.Rec_created_username,
                        UpdatedBy = x.Rec_updated_username,
                        Refreshed_date = null,
                        Refreshed_userid = null,
                        Refreshed_username = null,
                    }).Distinct().ToList();

                    foreach (var item in cropDamageGetAll.BlockList)
                    {
                        item.PanchayatList = cropDamageAll.CropdamageCoverages.Where(a => a.Block_id == int.Parse(item.Block_Id)).Select(x => new CropDamageDetailsGet
                        {
                            Reported_date = x.Rec_created_date.ToString(),
                            PanchayatId = x.Panchayat_id,
                            CropDamagePercentage = x.Estd_Crop_Damage,
                            Block_Id = x.Block_id.ToString(),
                            Block_Name = x.Block_name,
                            District_ID = x.District_id.ToString(),
                            District_name = x.District_name,
                            NetSownArea = x.Net_sown_area,
                            Damage_Reason = x.Damage_reason_name,
                            DamageReasonCreatedDate = x.Rec_created_date,
                            Dm_finalapprovalFlag = x.DM_Final_Approval_Flag,
                            DamageReasonId = x.Damage_reason_id,
                            Panchayat_Name = x.Panchayat_name,
                            Ac_Submitted_date = x.AC_Submitted_date,
                            Ac_Submitted_userid = x.AC_Submitted_userid,
                            Bao_Approval_flag = x.BAO_Approval_flag,
                            Bao_Approval_Reason = x.Bao_comments,
                            Bao_Approved_date = x.BAO_Approved_date,
                            Bao_Approved_userid = x.BAO_Approved_userid,
                            Dao_Approval_flag = x.DAO_Approval_flag,
                            Dao_Approval_Reason = x.Dao_comments,
                            Dao_Approved_date = x.DAO_Approved_date,
                            Dao_Approved_userid = x.DAO_Approved_userid,
                            Ac_submitted_username = x.Ac_submitted_username,
                            Bao_approved_username = x.Bao_submitted_username,
                            Dao_approved_username = x.Dao_submitted_username,
                            Submission_source = null,
                            Ac_submit_flag = x.Ac_submit_flag,
                            Bao_add_edit_flag = x.Bao_add_edit_flag,
                            Dao_add_edit_flag = x.Dao_add_edit_flag,
                            Rec_updated_userid = x.Rec_updated_userid,
                            Rec_updated_date = x.Rec_updated_date,
                            UpdatedBy = x.Rec_updated_username,
                        }).ToList();
                    }

                    foreach (var item in cropDamageGetAll.BlockList)
                    {
                        foreach (var panch in item.PanchayatList)
                        {
                            panch.CoveredArea = cropDamageAll.CropdamageCoverages.Where(x => x.Panchayat_id == panch.PanchayatId && x.Damage_reason_id == panch.DamageReasonId).Select(x => new CoveredAreaGet
                            {
                                Irrigated_Dmg_Area = x.Irrigated_area,
                                Non_irrigated_Dmg_Area = x.Nonirrigated_area,
                                Perennial_horticulture = x.Perennial_horti,
                                Perennial_sugarcane = x.Perennial_sugarcane,
                                Total = x.Total_area,
                                GrandTotal = x.Grand_total_area,
                            }).FirstOrDefault();
                            panch.CoveredArea = panch.CoveredArea == null ? new CoveredAreaGet() : panch.CoveredArea;

                            panch.AffectedArea = cropDamageAll.CropdamageImpactModels.Where(x => x.Panchayat_id == panch.PanchayatId && x.Damage_reason_id == panch.DamageReasonId).Select(x => new AffectedAreaGet
                            {
                                Perennial_horti_impact = x.Perennial_horti_impact,
                                Perennial_sugarcane_impact = x.Perennial_sugarcane_impact,
                                Total_impact_area = x.Total_impact_area,
                                Grand_total_impact = x.Grand_total_impact,
                                CropName = cropDamageAll.CropdamageImpactModels.Where(x => x.Panchayat_id == panch.PanchayatId && x.Damage_reason_id == panch.DamageReasonId).Select(x => new CropNameGet
                                {
                                    Crop_id = x.Crop_id,
                                    Crop_value = x.Damage_area,
                                    Crop_name = x.Crop_name,
                                }).GroupBy(x => x.Crop_id).Select(x => x.First()).ToList(),
                            }).FirstOrDefault();

                            panch.AffectedArea = panch.AffectedArea == null ? new AffectedAreaGet() : panch.AffectedArea;
                            panch.AffectedArea.CropName = cropDamageAll.CropdamageImpactModels.Where(x => x.Damage_reason_id == damage_id && x.Panchayat_id == panch.PanchayatId).Select(x => new CropNameGet
                            {
                                Crop_id = x.Crop_id,
                                Crop_value = x.Damage_area,
                                Crop_name = x.Crop_name,
                            }).ToList();
                            if (panch.AffectedArea.CropName.Count == 0)
                            {
                                CropNameGet cropNameGet = new CropNameGet();
                                cropNameGet.Crop_id = null;
                                cropNameGet.Crop_name = null;
                                cropNameGet.Crop_value = null;
                                panch.AffectedArea.CropName.Add(cropNameGet);
                            }

                            panch.DamageArea = cropDamageAll.CropdamageDetailsModels.Where(x => x.Panchayat_id == panch.PanchayatId && x.Damage_reason_id == panch.DamageReasonId).Select(x => new DamageAreaGet
                            {
                                Irrigated_Dmg_Area = x.Irrigated_area_dmg,
                                Irrigated_Dmg_Area_Estimated = x.Irrigated_cost_dmg,
                                Non_irrigated_Dmg_Area = x.Nonirrigated_area_dmg,
                                Non_irrigated_Dmg_Area_Estimated = x.Nonirrigated_cost_dmg,
                                Perennial_horticulture = x.Perennial_horti_dmg,
                                Perennial_horticulture_Estimated = x.Perennial_horti_cost_dmg,
                                Perennial_sugarcane = x.Perennial_sugarcane_dmg,
                                Perennial_sugarcane_Estimated = x.Perennial_sugarcane_cost_dmg,
                                TotalArea = x.Total_area_dmg,
                                GrandTotalArea = x.Grand_total_area_dmg,
                                TotalAmount = x.Total_cost_dmg,
                                GrandTotalAmount = x.Grand_total_cost_dmg,
                            }).FirstOrDefault();

                            panch.DamageArea = panch.DamageArea == null ? new DamageAreaGet() : panch.DamageArea;
                        }
                    }

                    return cropDamageGetAll;
                }
            }

            return null;
        }

        /// <summary>
        /// GetACCropCvrgCoveredAreaPancht.
        /// </summary>
        /// <param name="panchayat_Id">panchayat_Id.</param>
        /// <param name="damage_id">damage_id.</param>
        /// <returns>List.</returns>
        public CropDamageDetailsGet GetACCropCvrgCoveredAreaPancht(string panchayat_Id, int damage_id)
        {
            CropDamageAll cropDamageAll = new CropDamageAll();
            CropDamageDetailsGet cropDamageGetAll = new CropDamageDetailsGet();
            List<CropNameGet> getCrop = new List<CropNameGet>();
            DataSet dataSet = new DataSet();
            List<DbParameter> parameters = new List<DbParameter>();
            parameters.Add(new SqlParameter { ParameterName = "@query_name", Value = qn_GetACCropCvrgCoveredAreaPancht, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@District_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@Block_id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@Panchayat_Id", Value = panchayat_Id, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@damage_reason_id", Value = damage_id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@damage_id", Value = DBNull.Value, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            dataSet = SqlHelper.ExecuteDataSet<SqlConnection>(sp_crp_dmg_ctlr_get, parameters, SqlHelper.ExecutionType.Procedure);

            if (dataSet != null && dataSet.Tables.Count > 0)
            {
                cropDamageAll.CropdamageCoverages = SqlHelper.ConvertDataTableToList<CropDamageCoverage>(dataSet.Tables[0]);
                cropDamageAll.CropdamageImpactModels = SqlHelper.ConvertDataTableToList<CropDamageImpactModel>(dataSet.Tables[1]);

                cropDamageAll.CropdamageDetailsModels = SqlHelper.ConvertDataTableToList<CropDamageDetailsModel>(dataSet.Tables[2]);
            }

            string panchaytName = string.Empty;

            if (cropDamageAll.CropdamageImpactModels.Count > 0)
            {
                panchaytName = cropDamageAll.CropdamageImpactModels.Where(a => a.Panchayat_id == Convert.ToInt32(panchayat_Id)).Select(x => x.Panchayat_name).First().ToString();
            }
            else
            {
                panchaytName = string.Empty;
            }

            cropDamageGetAll.PanchayatId = Convert.ToInt32(panchayat_Id);
            cropDamageGetAll.DamageReasonId = cropDamageAll.CropdamageCoverages.Count > 0 ? cropDamageAll.CropdamageCoverages[0].Damage_reason_id : 0;
            cropDamageGetAll.Panchayat_Name = panchaytName;
            cropDamageGetAll.Block_Id = cropDamageAll.CropdamageCoverages.Count > 0 ? cropDamageAll.CropdamageCoverages[0].Block_id.ToString() : string.Empty;
            cropDamageGetAll.Block_Name = cropDamageAll.CropdamageCoverages.Count > 0 ? cropDamageAll.CropdamageCoverages[0].Block_name : string.Empty;
            cropDamageGetAll.Damage_Reason = cropDamageAll.CropdamageCoverages.Count > 0 ? cropDamageAll.CropdamageCoverages[0].Damage_reason_name : string.Empty;
            cropDamageGetAll.DamageReasonCreatedDate = cropDamageAll.CropdamageCoverages.Count > 0 ? cropDamageAll.CropdamageCoverages[0].Rec_created_date : null;
            cropDamageGetAll.Dm_finalapprovalFlag = cropDamageAll.CropdamageCoverages.Count > 0 ? cropDamageAll.CropdamageCoverages[0].DM_Final_Approval_Flag : null;
            cropDamageGetAll.CropDamagePercentage = cropDamageAll.CropdamageCoverages.Count > 0 ? cropDamageAll.CropdamageCoverages[0].Estd_Crop_Damage : string.Empty;
            cropDamageGetAll.NetSownArea = cropDamageAll.CropdamageCoverages.Count > 0 ? cropDamageAll.CropdamageCoverages[0].Net_sown_area : 0;
            cropDamageGetAll.Ac_Submitted_date = cropDamageAll.CropdamageCoverages.Count > 0 ? cropDamageAll.CropdamageCoverages[0].AC_Submitted_date : null;
            cropDamageGetAll.Ac_Submitted_userid = cropDamageAll.CropdamageCoverages.Count > 0 ? cropDamageAll.CropdamageCoverages[0].AC_Submitted_userid : 0;
            cropDamageGetAll.Bao_Approval_flag = cropDamageAll.CropdamageCoverages.Count > 0 ? cropDamageAll.CropdamageCoverages[0].BAO_Approval_flag : string.Empty;
            cropDamageGetAll.Bao_Approval_Reason = cropDamageAll.CropdamageCoverages.Count > 0 ? cropDamageAll.CropdamageCoverages[0].Bao_comments : string.Empty;
            cropDamageGetAll.Bao_Approved_date = cropDamageAll.CropdamageCoverages.Count > 0 ? cropDamageAll.CropdamageCoverages[0].BAO_Approved_date : null;
            cropDamageGetAll.Bao_Approved_userid = cropDamageAll.CropdamageCoverages.Count > 0 ? cropDamageAll.CropdamageCoverages[0].BAO_Approved_userid : 0;
            cropDamageGetAll.Dao_Approval_flag = cropDamageAll.CropdamageCoverages.Count > 0 ? cropDamageAll.CropdamageCoverages[0].DAO_Approval_flag : string.Empty;
            cropDamageGetAll.Dao_Approval_Reason = cropDamageAll.CropdamageCoverages.Count > 0 ? cropDamageAll.CropdamageCoverages[0].Dao_comments : string.Empty;
            cropDamageGetAll.Dao_Approved_date = cropDamageAll.CropdamageCoverages.Count > 0 ? cropDamageAll.CropdamageCoverages[0].DAO_Approved_date : null;
            cropDamageGetAll.Dao_Approved_userid = cropDamageAll.CropdamageCoverages.Count > 0 ? cropDamageAll.CropdamageCoverages[0].DAO_Approved_userid : 0;
            cropDamageGetAll.Ac_submitted_username = cropDamageAll.CropdamageCoverages.Count > 0 ? cropDamageAll.CropdamageCoverages[0].Ac_submitted_username : string.Empty;
            cropDamageGetAll.Bao_approved_username = cropDamageAll.CropdamageCoverages.Count > 0 ? cropDamageAll.CropdamageCoverages[0].Bao_submitted_username : string.Empty;
            cropDamageGetAll.Dao_approved_username = cropDamageAll.CropdamageCoverages.Count > 0 ? cropDamageAll.CropdamageCoverages[0].Dao_submitted_username : string.Empty;
            cropDamageGetAll.Submission_source = cropDamageAll.CropdamageCoverages.Count > 0 ? string.Empty : string.Empty;
            cropDamageGetAll.Ac_submit_flag = cropDamageAll.CropdamageCoverages.Count > 0 ? cropDamageAll.CropdamageCoverages[0].Ac_submit_flag : string.Empty;
            cropDamageGetAll.Bao_add_edit_flag = cropDamageAll.CropdamageCoverages.Count > 0 ? cropDamageAll.CropdamageCoverages[0].Bao_add_edit_flag : string.Empty;
            cropDamageGetAll.Dao_add_edit_flag = cropDamageAll.CropdamageCoverages.Count > 0 ? cropDamageAll.CropdamageCoverages[0].Dao_add_edit_flag : string.Empty;
            cropDamageGetAll.Rec_updated_userid = cropDamageAll.CropdamageCoverages.Count > 0 ? cropDamageAll.CropdamageCoverages[0].Rec_updated_userid : 0;
            cropDamageGetAll.Rec_updated_date = cropDamageAll.CropdamageCoverages.Count > 0 ? cropDamageAll.CropdamageCoverages[0].Rec_updated_date : null;
            cropDamageGetAll.UpdatedBy = cropDamageAll.CropdamageCoverages.Count > 0 ? cropDamageAll.CropdamageCoverages[0].Rec_updated_username : string.Empty;
            cropDamageGetAll.CoveredArea = cropDamageAll.CropdamageCoverages.Count == 0 ?
                new CoveredAreaGet()
                {
                    Irrigated_Dmg_Area = 0,
                    Non_irrigated_Dmg_Area = 0,
                    Perennial_horticulture = 0,
                    Perennial_sugarcane = 0,
                    Total = 0,
                    GrandTotal = 0,
                }
                : new CoveredAreaGet()
                {
                    Irrigated_Dmg_Area = cropDamageAll.CropdamageCoverages[0].Irrigated_area,
                    Non_irrigated_Dmg_Area = cropDamageAll.CropdamageCoverages[0].Nonirrigated_area,
                    Perennial_horticulture = cropDamageAll.CropdamageCoverages[0].Perennial_horti,
                    Perennial_sugarcane = cropDamageAll.CropdamageCoverages[0].Perennial_sugarcane,
                    Total = cropDamageAll.CropdamageCoverages[0].Total_area,
                    GrandTotal = cropDamageAll.CropdamageCoverages[0].Grand_total_area,
                };
            if (cropDamageAll.CropdamageImpactModels.Count > 0)
            {
                for (var i = 0; i < cropDamageAll.CropdamageImpactModels.Count; i++)
                {
                    CropNameGet cropNameGet = new CropNameGet()
                    {
                        Crop_id = cropDamageAll.CropdamageImpactModels[i].Crop_id,
                        Crop_name = cropDamageAll.CropdamageImpactModels[i].Crop_name,
                        Crop_value = cropDamageAll.CropdamageImpactModels[i].Damage_area,
                    };
                    getCrop.Add(cropNameGet);
                }
            }

            cropDamageGetAll.AffectedArea = cropDamageAll.CropdamageImpactModels.Count == 0 ?
                new AffectedAreaGet()
                {
                    Perennial_horti_impact = 0,
                    Perennial_sugarcane_impact = 0,
                    CropName = new List<CropNameGet>()
                {
                           new CropNameGet { Crop_id = 0, Crop_name = string.Empty, Crop_value = 0 },
                },
                    Grand_total_impact = 0,
                    Total_impact_area = 0,
                }

                : new AffectedAreaGet()
                {
                    Perennial_horti_impact = cropDamageAll.CropdamageImpactModels[0].Perennial_horti_impact,
                    Perennial_sugarcane_impact = cropDamageAll.CropdamageImpactModels[0].Perennial_sugarcane_impact,
                    CropName = getCrop,
                    Grand_total_impact = cropDamageAll.CropdamageImpactModels[0].Grand_total_impact,
                    Total_impact_area = cropDamageAll.CropdamageImpactModels[0].Total_impact_area,
                };
            cropDamageGetAll.DamageArea = cropDamageAll.CropdamageDetailsModels.Count == 0 ?
                new DamageAreaGet()
                {
                    Irrigated_Dmg_Area = 0,
                    Irrigated_Dmg_Area_Estimated = 0,
                    Non_irrigated_Dmg_Area = 0,
                    Non_irrigated_Dmg_Area_Estimated = 0,

                    Perennial_horticulture = 0,
                    Perennial_horticulture_Estimated = 0,
                    Perennial_sugarcane = 0,
                    Perennial_sugarcane_Estimated = 0,
                    GrandTotalAmount = 0,
                    GrandTotalArea = 0,
                    TotalAmount = 0,
                    TotalArea = 0,
                }
                : new DamageAreaGet()
                {
                    Irrigated_Dmg_Area = cropDamageAll.CropdamageDetailsModels[0].Irrigated_area_dmg,
                    Irrigated_Dmg_Area_Estimated = cropDamageAll.CropdamageDetailsModels[0].Irrigated_cost_dmg,
                    Non_irrigated_Dmg_Area = cropDamageAll.CropdamageDetailsModels[0].Nonirrigated_area_dmg,
                    Non_irrigated_Dmg_Area_Estimated = cropDamageAll.CropdamageDetailsModels[0].Nonirrigated_cost_dmg,

                    Perennial_horticulture = cropDamageAll.CropdamageDetailsModels[0].Perennial_horti_dmg,
                    Perennial_horticulture_Estimated = cropDamageAll.CropdamageDetailsModels[0].Perennial_horti_cost_dmg,
                    Perennial_sugarcane = cropDamageAll.CropdamageDetailsModels[0].Perennial_sugarcane_dmg,
                    Perennial_sugarcane_Estimated = cropDamageAll.CropdamageDetailsModels[0].Perennial_sugarcane_cost_dmg,
                    GrandTotalAmount = cropDamageAll.CropdamageDetailsModels[0].Grand_total_cost_dmg,
                    GrandTotalArea = cropDamageAll.CropdamageDetailsModels[0].Grand_total_area_dmg,
                    TotalAmount = cropDamageAll.CropdamageDetailsModels[0].Total_cost_dmg,
                    TotalArea = cropDamageAll.CropdamageDetailsModels[0].Total_area_dmg,
                };
            return cropDamageGetAll;
        }

        /// <summary>
        /// GetACCropCvrgCoveredAreaPanchtOffline.
        /// </summary>
        /// <param name="panchayat_Id">panchayat_Id.</param>
        /// <returns>List.</returns>
        public List<CropDamageDetailsGet> GetACCropCvrgCoveredAreaPanchtOffline(string panchayat_Id)
        {
            CropDamageAll cropDamageAll = new CropDamageAll();
            DataSet dataSet = new DataSet();
            List<DbParameter> parameters = new List<DbParameter>();
            parameters.Add(new SqlParameter { ParameterName = "@query_name", Value = qn_GetACCropCvrgCoveredAreaPanchtOffline, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@District_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@Block_id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@Panchayat_Id", Value = panchayat_Id, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@damage_reason_id", Value = DBNull.Value, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@damage_id", Value = DBNull.Value, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            dataSet = SqlHelper.ExecuteDataSet<SqlConnection>(sp_crp_dmg_ctlr_get, parameters, SqlHelper.ExecutionType.Procedure);

            if (dataSet != null && dataSet.Tables.Count > 0)
            {
                cropDamageAll.CropdamageCoverages = SqlHelper.ConvertDataTableToList<CropDamageCoverage>(dataSet.Tables[0]);
                cropDamageAll.CropdamageImpactModels = SqlHelper.ConvertDataTableToList<CropDamageImpactModel>(dataSet.Tables[1]);

                cropDamageAll.CropdamageDetailsModels = SqlHelper.ConvertDataTableToList<CropDamageDetailsModel>(dataSet.Tables[2]);
            }

            List<CropDamageDetailsGet> crodmglist = cropDamageAll.CropdamageCoverages.Select(x => new CropDamageDetailsGet
            {
                PanchayatId = x.Panchayat_id,
                Damage_Reason = x.Damage_reason_name,
                Panchayat_Name = cropDamageAll.CropdamageImpactModels.Count > 0 ? cropDamageAll.CropdamageImpactModels.Where(a => a.Panchayat_id == x.Panchayat_id).Select(x => x.Panchayat_name).First().ToString() : string.Empty,
                DamageReasonId = x.Damage_reason_id,
                Block_Id = x.Block_id.ToString(),
                Block_Name = x.Block_name,
                DamageReasonCreatedDate = x.Rec_created_date,
                CropDamagePercentage = x.Estd_Crop_Damage,
                NetSownArea = x.Net_sown_area,
                Ac_Submitted_date = x.AC_Submitted_date,
                Ac_Submitted_userid = x.AC_Submitted_userid,
                Bao_Approval_flag = x.BAO_Approval_flag,
                Bao_Approval_Reason = x.Bao_comments,
                Bao_Approved_date = x.BAO_Approved_date,
                Bao_Approved_userid = x.BAO_Approved_userid,
                Dao_Approval_flag = x.DAO_Approval_flag,
                Dm_finalapprovalFlag = x.DM_Final_Approval_Flag,
                Dao_Approval_Reason = x.Dao_comments,
                Dao_Approved_date = x.DAO_Approved_date,
                Dao_Approved_userid = x.DAO_Approved_userid,
                Ac_submitted_username = x.Ac_submitted_username,
                Bao_approved_username = x.Bao_submitted_username,
                Dao_approved_username = x.Dao_submitted_username,
                Submission_source = string.Empty,
                Ac_submit_flag = x.Ac_submit_flag,
                Bao_add_edit_flag = x.Bao_add_edit_flag,
                Dao_add_edit_flag = x.Dao_add_edit_flag,
                Rec_updated_userid = x.Rec_updated_userid,
                Rec_updated_date = x.Rec_updated_date,
                UpdatedBy = x.Rec_updated_username,
            }).ToList();

            foreach (var item in crodmglist)
            {
                item.CoveredArea = cropDamageAll.CropdamageCoverages.Where(x => x.Damage_reason_id == item.DamageReasonId)?.Select(x => new CoveredAreaGet
                {
                    Irrigated_Dmg_Area = x.Irrigated_area,
                    Non_irrigated_Dmg_Area = x.Nonirrigated_area,
                    Perennial_horticulture = x.Perennial_horti,
                    Perennial_sugarcane = x.Perennial_sugarcane,
                    Total = x.Total_area,
                    GrandTotal = x.Grand_total_area,
                }).FirstOrDefault();

                item.AffectedArea = cropDamageAll.CropdamageImpactModels.Where(x => x.Damage_reason_id == item.DamageReasonId)?.Select(x => new AffectedAreaGet
                {
                    Perennial_horti_impact = x.Perennial_horti_impact,
                    Perennial_sugarcane_impact = x.Perennial_sugarcane_impact,
                    Total_impact_area = x.Total_impact_area,
                    Grand_total_impact = x.Grand_total_impact,
                    CropName = cropDamageAll.CropdamageImpactModels.Where(x => x.Damage_reason_id == item.DamageReasonId)?.Select(x => new CropNameGet
                    {
                        Crop_id = x.Crop_id,
                        Crop_name = x.Crop_name,
                        Crop_value = x.Damage_area,
                    }).ToList(),
                }).FirstOrDefault();

                var damageOutput = cropDamageAll.CropdamageDetailsModels.Where(x => x.Damage_reason_id == item.DamageReasonId)?.Select(x => new DamageAreaGet
                {
                    Irrigated_Dmg_Area = x.Irrigated_area_dmg,
                    Irrigated_Dmg_Area_Estimated = x.Irrigated_cost_dmg,
                    Non_irrigated_Dmg_Area = x.Nonirrigated_area_dmg,
                    Non_irrigated_Dmg_Area_Estimated = x.Nonirrigated_cost_dmg,
                    Perennial_horticulture = x.Perennial_horti_dmg,
                    Perennial_horticulture_Estimated = x.Perennial_horti_cost_dmg,
                    Perennial_sugarcane = x.Perennial_sugarcane_dmg,
                    Perennial_sugarcane_Estimated = x.Perennial_sugarcane_cost_dmg,
                    TotalArea = x.Total_area_dmg,
                    GrandTotalArea = x.Total_cost_dmg,
                    GrandTotalAmount = x.Grand_total_area_dmg,
                    TotalAmount = x.Grand_total_cost_dmg,
                }).FirstOrDefault();

                item.DamageArea = damageOutput == null ? new DamageAreaGet() : damageOutput;
            }

            return crodmglist;
        }

        /// <summary>
        /// GetBAOCropDamageDetailsBlockOffline.
        /// </summary>
        /// <param name="districtId">districtId.</param>
        /// <param name="blockId">blockId.</param>
        /// <returns>List.</returns>
        public List<InsBaoCropdmgModel> GetBAOCropDamageDetailsBlockOffline(int districtId, int blockId)
        {
            CropDamageAll cropDamageAll = new CropDamageAll();
            DataSet dataSet = new DataSet();
            List<DbParameter> parameters = new List<DbParameter>();
            parameters.Add(new SqlParameter { ParameterName = "@query_name", Value = qn_GetBAOCropDamageDetailsBlockOffline, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@District_Id", Value = districtId, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@Block_id", Value = blockId.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@Panchayat_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@damage_reason_id", Value = DBNull.Value, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@damage_id", Value = DBNull.Value, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            dataSet = SqlHelper.ExecuteDataSet<SqlConnection>(sp_crp_dmg_ctlr_get, parameters, SqlHelper.ExecutionType.Procedure);

            if (dataSet != null && dataSet.Tables.Count > 0)
            {
                cropDamageAll.CropdamageCoverages = SqlHelper.ConvertDataTableToList<CropDamageCoverage>(dataSet.Tables[0]);
                cropDamageAll.CropdamageImpactModels = SqlHelper.ConvertDataTableToList<CropDamageImpactModel>(dataSet.Tables[1]);

                cropDamageAll.CropdamageDetailsModels = SqlHelper.ConvertDataTableToList<CropDamageDetailsModel>(dataSet.Tables[2]);

                if (cropDamageAll.CropdamageCoverages.Any())
                {
                    List<InsBaoCropdmgModel> cropDamageGetAll = cropDamageAll.CropdamageCoverages.Where(x => x.Damage_reason_id != null).GroupBy(x => x.Damage_reason_id).Select(x => x.First()).AsEnumerable().Select(x => new InsBaoCropdmgModel
                    {
                        Reported_date = x.Rec_created_date.ToString(),
                        Damage_Reason = x.Damage_reason_name,
                        Damage_Reason_id = x.Damage_reason_id.ToString(),
                        District_Id = x.District_id.ToString(),
                        District_Name = x.District_name,
                        CreatedBy = x.Rec_created_username,
                        UpdatedBy = x.Rec_updated_username,
                        Refreshed_date = null,
                        Refreshed_userid = null,
                        Dm_approval_flag = x.DM_Final_Approval_Flag,
                    }).ToList();

                    foreach (var item in cropDamageGetAll)
                    {
                        item.BlockList = cropDamageAll.CropdamageCoverages.Where(x => x.Damage_reason_id.ToString() == item.Damage_Reason_id).GroupBy(x => x.Damage_reason_id).Select(x => x.First()).AsEnumerable().Select(x => new BlockList
                        {
                            Reported_date = x.Rec_created_date.ToString(),
                            Block_Id = x.Block_id.ToString(),
                            DamageReasonId = x.Damage_reason_id,
                            Damage_Reason = x.Damage_reason_name,
                            Block_Name = x.Block_name,
                            Net_area_sown = x.Net_sown_area.ToString(),
                            CreatedBy = x.Rec_created_username,
                            UpdatedBy = x.Rec_updated_username,
                            Refreshed_date = null,
                            Refreshed_userid = null,
                            Refreshed_username = null,
                        }).ToList();
                    }

                    foreach (var items in cropDamageGetAll)
                    {
                        foreach (var item in items.BlockList)
                        {
                            item.PanchayatList = cropDamageAll.CropdamageCoverages.Where(x => x.Damage_reason_id == item.DamageReasonId || x.Damage_reason_id == null).GroupBy(x => new { x.Panchayat_id, x.Damage_reason_id }).Select(x => x.First()).AsEnumerable().Select(x => new CropDamageDetailsGet
                            {
                                Reported_date = x.Rec_created_date.ToString(),
                                PanchayatId = x.Panchayat_id,
                                DamageReasonId = x.Damage_reason_id,
                                Panchayat_Name = x.Panchayat_name,
                                Block_Id = x.Block_id.ToString(),
                                Block_Name = x.Block_name,
                                District_ID = x.District_id.ToString(),
                                District_name = x.District_name,
                                DamageReasonCreatedDate = x.Rec_created_date,
                                Damage_Reason = x.Damage_reason_name,
                                CropDamagePercentage = x.Estd_Crop_Damage,
                                NetSownArea = x.Net_sown_area,
                                Ac_Submitted_date = x.AC_Submitted_date,
                                Ac_Submitted_userid = x.AC_Submitted_userid,
                                Bao_Approval_flag = x.BAO_Approval_flag,
                                Dm_finalapprovalFlag = x.DM_Final_Approval_Flag,
                                Bao_Approval_Reason = x.Bao_comments,
                                Bao_Approved_date = x.BAO_Approved_date,
                                Bao_Approved_userid = x.BAO_Approved_userid,
                                Dao_Approval_flag = x.DAO_Approval_flag,
                                Dao_Approval_Reason = x.Dao_comments,
                                Dao_Approved_date = x.DAO_Approved_date,
                                Dao_Approved_userid = x.DAO_Approved_userid,
                                Ac_submitted_username = x.Ac_submitted_username,
                                Bao_approved_username = x.Bao_submitted_username,
                                Dao_approved_username = x.Dao_submitted_username,
                                Submission_source = null,
                                Ac_submit_flag = x.Ac_submit_flag,
                                Bao_add_edit_flag = x.Bao_add_edit_flag,
                                Dao_add_edit_flag = x.Dao_add_edit_flag,
                                Rec_updated_userid = x.Rec_updated_userid,
                                Rec_updated_date = x.Rec_updated_date,
                                UpdatedBy = x.Rec_updated_username,
                            }).ToList();
                        }
                    }

                    foreach (var lst in cropDamageGetAll)
                    {
                        foreach (var item in lst.BlockList)
                        {
                            foreach (var panch in item.PanchayatList)
                            {
                                panch.CoveredArea = cropDamageAll.CropdamageCoverages.Count == 0 ? new CoveredAreaGet() : cropDamageAll.CropdamageCoverages.Where(x => x.Panchayat_id == panch.PanchayatId && x.Damage_reason_id == panch.DamageReasonId).Select(x => new CoveredAreaGet
                                {
                                    Irrigated_Dmg_Area = x.Irrigated_area,
                                    Non_irrigated_Dmg_Area = x.Nonirrigated_area,
                                    Perennial_horticulture = x.Perennial_horti,
                                    Perennial_sugarcane = x.Perennial_sugarcane,
                                    Total = x.Total_area,
                                    GrandTotal = x.Grand_total_area,
                                }).FirstOrDefault();

                                panch.AffectedArea = cropDamageAll.CropdamageImpactModels.Count == 0 ? new AffectedAreaGet() : cropDamageAll.CropdamageImpactModels.Where(x => x.Panchayat_id == panch.PanchayatId && x.Damage_reason_id == panch.DamageReasonId).Select(x => new AffectedAreaGet
                                {
                                    Perennial_horti_impact = x.Perennial_horti_impact,
                                    Perennial_sugarcane_impact = x.Perennial_sugarcane_impact,
                                    Total_impact_area = x.Total_impact_area,
                                    Grand_total_impact = x.Grand_total_impact,
                                    CropName = cropDamageAll.CropdamageImpactModels.Where(x => x.Panchayat_id == panch.PanchayatId && x.Damage_reason_id == panch.DamageReasonId).Select(x => new CropNameGet
                                    {
                                        Crop_id = x.Crop_id,
                                        Crop_value = x.Damage_area,
                                        Crop_name = x.Crop_name,
                                    }).GroupBy(x => x.Crop_id).Select(x => x.First()).ToList(),
                                }).FirstOrDefault();
                                panch.AffectedArea = panch.AffectedArea == null ? new AffectedAreaGet() : panch.AffectedArea;
                                panch.AffectedArea.CropName = cropDamageAll.CropdamageImpactModels.Where(x => x.Damage_reason_id == item.DamageReasonId && x.Panchayat_id == panch.PanchayatId).Select(x => new CropNameGet
                                {
                                    Crop_id = x.Crop_id,
                                    Crop_value = x.Damage_area,
                                    Crop_name = x.Crop_name,
                                }).ToList();
                                if (panch.AffectedArea.CropName.Count == 0)
                                {
                                    CropNameGet cropNameGet = new CropNameGet();
                                    cropNameGet.Crop_id = null;
                                    cropNameGet.Crop_name = null;
                                    cropNameGet.Crop_value = null;
                                    panch.AffectedArea.CropName.Add(cropNameGet);
                                }

                                panch.DamageArea = cropDamageAll.CropdamageDetailsModels.Where(x => x.Panchayat_id == panch.PanchayatId && x.Damage_reason_id == panch.DamageReasonId).Select(x => new DamageAreaGet
                                {
                                    Irrigated_Dmg_Area = x.Irrigated_area_dmg,
                                    Irrigated_Dmg_Area_Estimated = x.Irrigated_cost_dmg,
                                    Non_irrigated_Dmg_Area = x.Nonirrigated_area_dmg,
                                    Non_irrigated_Dmg_Area_Estimated = x.Nonirrigated_cost_dmg,
                                    Perennial_horticulture = x.Perennial_horti_dmg,
                                    Perennial_horticulture_Estimated = x.Perennial_horti_cost_dmg,
                                    Perennial_sugarcane = x.Perennial_sugarcane_dmg,
                                    Perennial_sugarcane_Estimated = x.Perennial_sugarcane_cost_dmg,
                                    TotalArea = x.Total_area_dmg,
                                    GrandTotalArea = x.Grand_total_area_dmg,
                                    TotalAmount = x.Total_cost_dmg,
                                    GrandTotalAmount = x.Grand_total_cost_dmg,
                                }).FirstOrDefault();

                                panch.DamageArea = panch.DamageArea == null ? new DamageAreaGet() : panch.DamageArea;
                            }
                        }
                    }

                    return cropDamageGetAll;
                }
            }

            return null;
        }

        /// <summary>
        /// GetDAOCropDamageDetailsBlockOffline.
        /// </summary>
        /// <param name="districtId">districtId.</param>
        /// <returns>List.</returns>
        public List<InsBaoCropdmgModel> GetDAOCropDamageDetailsBlockOffline(string districtId)
        {
            CropDamageAll cropDamageAll = new CropDamageAll();
            DataSet dataSet = new DataSet();
            List<DbParameter> parameters = new List<DbParameter>();
            parameters.Add(new SqlParameter { ParameterName = "@query_name", Value = qn_GetOfflineDAOCropDamageDetailsDistrict, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@District_Id", Value = districtId, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@Block_id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@Panchayat_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@damage_reason_id", Value = DBNull.Value, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@damage_id", Value = DBNull.Value, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });

            dataSet = SqlHelper.ExecuteDataSet<SqlConnection>(sp_crp_dmg_ctlr_get, parameters, SqlHelper.ExecutionType.Procedure);

            if (dataSet != null && dataSet.Tables.Count > 0)
            {
                cropDamageAll.CropdamageCoverages = SqlHelper.ConvertDataTableToList<CropDamageCoverage>(dataSet.Tables[0]);
                cropDamageAll.CropdamageImpactModels = SqlHelper.ConvertDataTableToList<CropDamageImpactModel>(dataSet.Tables[1]);

                cropDamageAll.CropdamageDetailsModels = SqlHelper.ConvertDataTableToList<CropDamageDetailsModel>(dataSet.Tables[2]);

                if (cropDamageAll.CropdamageCoverages.Any())
                {
                    var netshownArea = cropDamageAll.CropdamageCoverages.Where(x => x.Net_sown_area != null).Select(a => a.Net_sown_area).ToList();
                    var dm_ApprovalFlag = cropDamageAll.CropdamageCoverages.Where(x => x.DM_Final_Approval_Flag == "Y").Select(a => a.DM_Final_Approval_Flag).ToList();

                    if (netshownArea == null)
                    {
                        netshownArea = new List<decimal?>();
                    }

                    List<InsBaoCropdmgModel> cropDamageGetAll = cropDamageAll.CropdamageCoverages.Where(x => x.Damage_reason_id != null).GroupBy(x => new { x.District_id, x.Damage_reason_id }).Select(x => x.First()).AsEnumerable().Select(x => new InsBaoCropdmgModel
                    {
                        Reported_date = x.Rec_created_date.ToString(),
                        Damage_Reason = x.Damage_reason_name,
                        Damage_Reason_id = x.Damage_reason_id.ToString(),
                        District_Id = x.District_id.ToString(),
                        District_Name = x.District_name,
                        CreatedBy = x.Rec_created_username,
                        UpdatedBy = x.Rec_updated_username,
                        Refreshed_date = null,
                        Refreshed_userid = null,
                        Dm_approval_flag = (netshownArea.Count == dm_ApprovalFlag.Count && dm_ApprovalFlag.Count != 0) ? "Y" : "N",
                    }).ToList();

                    foreach (var item in cropDamageGetAll)
                    {
                        item.BlockList = cropDamageAll.CropdamageCoverages.Where(x => x.Damage_reason_id.ToString() == item.Damage_Reason_id).GroupBy(x => x.Block_id).Select(x => x.First()).AsEnumerable().Select(x => new BlockList
                        {
                            Reported_date = x.Rec_created_date.ToString(),
                            Block_Id = x.Block_id.ToString(),
                            Block_Name = x.Block_name,
                            DamageReasonId = x.Damage_reason_id,
                            Damage_Reason = x.Damage_reason_name,
                            Net_area_sown = x.Net_sown_area.ToString(),
                            CreatedBy = x.Rec_created_username,
                            UpdatedBy = x.Rec_updated_username,
                            Refreshed_date = null,
                            Refreshed_userid = null,
                            Refreshed_username = null,
                        }).Distinct().ToList();
                    }

                    foreach (var dmg in cropDamageGetAll)
                    {
                        foreach (var item in dmg.BlockList)
                        {
                            item.PanchayatList = cropDamageAll.CropdamageCoverages.Where(a => (a.Block_id == int.Parse(item.Block_Id) && a.Damage_reason_id.ToString() == dmg.Damage_Reason_id) || (a.Damage_reason_id == null)).Select(x => new CropDamageDetailsGet
                            {
                                Reported_date = x.Rec_created_date.ToString(),
                                PanchayatId = x.Panchayat_id,
                                DamageReasonId = x.Damage_reason_id,
                                Panchayat_Name = x.Panchayat_name,
                                Block_Id = x.Block_id.ToString(),
                                Block_Name = x.Block_name,
                                District_ID = x.District_id.ToString(),
                                Damage_Reason = x.Damage_reason_name,
                                District_name = x.District_name,
                                NetSownArea = x.Net_sown_area,
                                Ac_Submitted_date = x.AC_Submitted_date,
                                Ac_Submitted_userid = x.AC_Submitted_userid,
                                Bao_Approval_flag = x.BAO_Approval_flag,
                                Bao_Approval_Reason = x.Bao_comments,
                                Bao_Approved_date = x.BAO_Approved_date,
                                Bao_Approved_userid = x.BAO_Approved_userid,
                                Dm_finalapprovalFlag = x.DM_Final_Approval_Flag,
                                Dao_Approval_flag = x.DAO_Approval_flag,
                                Dao_Approval_Reason = x.Dao_comments,
                                Dao_Approved_date = x.DAO_Approved_date,
                                Dao_Approved_userid = x.DAO_Approved_userid,
                                Ac_submitted_username = x.Ac_submitted_username,
                                Bao_approved_username = x.Bao_submitted_username,
                                Dao_approved_username = x.Dao_submitted_username,
                                Submission_source = null,
                                Ac_submit_flag = x.Ac_submit_flag,
                                DamageReasonCreatedDate = x.Rec_created_date,
                                CropDamagePercentage = x.Estd_Crop_Damage,
                                Bao_add_edit_flag = x.Bao_add_edit_flag,
                                Dao_add_edit_flag = x.Dao_add_edit_flag,
                                Rec_updated_userid = x.Rec_updated_userid,
                                Rec_updated_date = x.Rec_updated_date,
                                UpdatedBy = x.Rec_updated_username,
                            }).ToList();
                        }
                    }

                    foreach (var dmgg in cropDamageGetAll)
                    {
                        foreach (var item in dmgg.BlockList)
                        {
                            foreach (var panch in item.PanchayatList)
                            {
                                panch.CoveredArea = cropDamageAll.CropdamageCoverages.Where(x => x.Panchayat_id == panch.PanchayatId && x.Damage_reason_id == panch.DamageReasonId).Select(x => new CoveredAreaGet
                                {
                                    Irrigated_Dmg_Area = x.Irrigated_area,
                                    Non_irrigated_Dmg_Area = x.Nonirrigated_area,
                                    Perennial_horticulture = x.Perennial_horti,
                                    Perennial_sugarcane = x.Perennial_sugarcane,
                                    Total = x.Total_area,
                                    GrandTotal = x.Grand_total_area,
                                }).FirstOrDefault();
                                panch.CoveredArea = panch.CoveredArea == null ? new CoveredAreaGet() : panch.CoveredArea;

                                panch.AffectedArea = cropDamageAll.CropdamageImpactModels.Where(x => x.Panchayat_id == panch.PanchayatId && x.Damage_reason_id == panch.DamageReasonId).Select(x => new AffectedAreaGet
                                {
                                    Perennial_horti_impact = x.Perennial_horti_impact,
                                    Perennial_sugarcane_impact = x.Perennial_sugarcane_impact,
                                    Total_impact_area = x.Total_impact_area,
                                    Grand_total_impact = x.Grand_total_impact,
                                    CropName = cropDamageAll.CropdamageImpactModels.Where(x => x.Panchayat_id == panch.PanchayatId && x.Damage_reason_id == panch.DamageReasonId).Select(x => new CropNameGet
                                    {
                                        Crop_id = x.Crop_id,
                                        Crop_value = x.Damage_area,
                                        Crop_name = x.Crop_name,
                                    }).GroupBy(x => x.Crop_id).Select(x => x.First()).ToList(),
                                }).FirstOrDefault();
                                panch.AffectedArea = panch.AffectedArea == null ? new AffectedAreaGet() : panch.AffectedArea;
                                panch.AffectedArea.CropName = cropDamageAll.CropdamageImpactModels.Where(x => x.Damage_reason_id == item.DamageReasonId && x.Panchayat_id == panch.PanchayatId).Select(x => new CropNameGet
                                {
                                    Crop_id = x.Crop_id,
                                    Crop_value = x.Damage_area,
                                    Crop_name = x.Crop_name,
                                }).ToList();
                                if (panch.AffectedArea.CropName.Count == 0)
                                {
                                    CropNameGet cropNameGet = new CropNameGet();
                                    cropNameGet.Crop_id = null;
                                    cropNameGet.Crop_name = null;
                                    cropNameGet.Crop_value = null;
                                    panch.AffectedArea.CropName.Add(cropNameGet);
                                }

                                panch.DamageArea = cropDamageAll.CropdamageDetailsModels.Where(x => x.Panchayat_id == panch.PanchayatId && x.Damage_reason_id == panch.DamageReasonId).Select(x => new DamageAreaGet
                                {
                                    Irrigated_Dmg_Area = x.Irrigated_area_dmg,
                                    Irrigated_Dmg_Area_Estimated = x.Irrigated_cost_dmg,
                                    Non_irrigated_Dmg_Area = x.Nonirrigated_area_dmg,
                                    Non_irrigated_Dmg_Area_Estimated = x.Nonirrigated_cost_dmg,
                                    Perennial_horticulture = x.Perennial_horti_dmg,
                                    Perennial_horticulture_Estimated = x.Perennial_horti_cost_dmg,
                                    Perennial_sugarcane = x.Perennial_sugarcane_dmg,
                                    Perennial_sugarcane_Estimated = x.Perennial_sugarcane_cost_dmg,
                                    TotalArea = x.Total_area_dmg,
                                    GrandTotalArea = x.Grand_total_area_dmg,
                                    TotalAmount = x.Total_cost_dmg,
                                    GrandTotalAmount = x.Grand_total_cost_dmg,
                                }).FirstOrDefault();
                                panch.DamageArea = panch.DamageArea == null ? new DamageAreaGet() : panch.DamageArea;
                            }
                        }
                    }

                    return cropDamageGetAll;
                }
            }

            return null;
        }

        /// <summary>
        /// GetOnlineACViewSubmissionPancht.
        /// </summary>
        /// <param name="panchayat_Id">panchayat_Id.</param>
        /// <param name="damage_reason_id">damage_reason_id.</param>
        /// <returns>List.</returns>
        public ViewSubmissionAcPanchayat GetOnlineACViewSubmissionPancht(string panchayat_Id, int damage_reason_id)
        {
            ViewSubmissionAcPanchayat response = new ViewSubmissionAcPanchayat();
            List<DtoViewSubmissionAcPanchayat> dtocaptureDT = new List<DtoViewSubmissionAcPanchayat>();

            List<DtoViewSubmissionAffectedArea> dtoAffectedArea = new List<DtoViewSubmissionAffectedArea>();

            List<DtoViewSubmissionDamageArea> dtodamageArea = new List<DtoViewSubmissionDamageArea>();
            DataSet dataSet = new DataSet();
            List<DbParameter> parameters = new List<DbParameter>();
            parameters.Add(new SqlParameter { ParameterName = "@query_name", Value = qn_GetOnlineACViewSubmissionPancht, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@District_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@Block_id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@Panchayat_Id", Value = panchayat_Id, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@damage_reason_id", Value = damage_reason_id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@damage_id", Value = DBNull.Value, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            dataSet = SqlHelper.ExecuteDataSet<SqlConnection>(sp_crp_dmg_ctlr_get, parameters, SqlHelper.ExecutionType.Procedure);

            if (dataSet != null && dataSet.Tables.Count > 0)
            {
                dtocaptureDT = SqlHelper.ConvertDataTableToList<DtoViewSubmissionAcPanchayat>(dataSet.Tables[0]);
                dtoAffectedArea = SqlHelper.ConvertDataTableToList<DtoViewSubmissionAffectedArea>(dataSet.Tables[1]);

                dtodamageArea = SqlHelper.ConvertDataTableToList<DtoViewSubmissionDamageArea>(dataSet.Tables[2]);
                if (dtocaptureDT != null && dtocaptureDT.Any())
                {
                    response = dtocaptureDT.Select(x =>
                      new ViewSubmissionAcPanchayat
                      {
                          Damage_reason_id = x.Damage_reason_id,
                          Damage_reason_name = x.Damage_reason_name,
                          Panchayat_id = x.Panchayat_id,
                          Panchayat_name = x.Panchayat_name,
                          AC_Submitted_date = x.AC_Submitted_date,
                          Dm_finalapprovalFlag = x.DM_Final_Approval_Flag,
                          BAO_Approval_flag = x.BAO_Approval_flag,
                          BAO_Approved_date = x.BAO_Approved_date,
                          BAO_Approved_userid = x.BAO_Approved_userid,
                          DAO_Approval_flag = x.DAO_Approval_flag,
                          DAO_Approved_date = x.DAO_Approved_date,
                          Ac_submitted_username = x.Ac_submitted_username,
                          AC_Submitted_userid = x.AC_Submitted_userid,
                          DAO_Approved_userid = x.DAO_Approved_userid,
                          Bao_approved_username = x.Bao_submitted_username,
                          Dao_approved_username = x.Dao_submitted_username,
                          Ac_submit_flag = x.Ac_submit_flag,
                          Bao_add_edit_flag = x.Bao_add_edit_flag,
                          Dao_add_edit_flag = x.Dao_add_edit_flag,
                          Rec_updated_userid = x.Rec_updated_userid,
                          Rec_updated_date = x.Rec_updated_date,
                          BaO_Approval_Reason = x.Bao_comments,
                          DaO_Approval_Reason = x.Dao_comments,
                          UpdatedBy = x.Rec_updated_username,
                          CoveredAreaobj = dtocaptureDT.Select(x => new ViewSubmissionCoveredArea
                          {
                              Irrigated_area = x.Irrigated_area,
                              Nonirrigated_area = x.Nonirrigated_area,
                              Total_area = x.Total_area,
                              Grand_total_area = x.Grand_total_area,
                              Perennial_horti = x.Perennial_horti,
                              Perennial_sugarcane = x.Perennial_sugarcane,
                          }).FirstOrDefault(),

                          AffectedAreaobj = dtoAffectedArea.Select(x => new ViewSubmissionAffectedArea
                          {
                              Perennial_horti_impact = x.Perennial_horti_impact,
                              Perennial_sugarcane_impact = x.Perennial_sugarcane_impact,
                              Total_impact_area = x.Total_impact_area,
                              Grand_total_impact = x.Grand_total_impact,
                              Crops = dtoAffectedArea.Select(x => new ViewSubmissionCrops
                              {
                                  Crop_id = x.Crop_id,
                                  Crop_name = x.Crop_name,
                                  Damage_area = x.Damage_area,
                              }).GroupBy(x => x.Crop_id).Select(x => x.First()).ToList(),
                          }).FirstOrDefault(),

                          DamageAreaobj = dtodamageArea.Select(x => new ViewSubmissionDamageArea
                          {
                              Irrigated_area_dmg = x.Irrigated_area_dmg,
                              Nonirrigated_area_dmg = x.Nonirrigated_area_dmg,
                              Total_area_dmg = x.Total_area_dmg,
                              Grand_total_area_dmg = x.Grand_total_area_dmg,
                              Perennial_horti_dmg = x.Perennial_horti_dmg,
                              Perennial_sugarcane_dmg = x.Perennial_sugarcane_dmg,
                          }).FirstOrDefault(),

                          DamageValueobj = dtodamageArea.Select(x => new ViewSubmissionDamageValue
                          {
                              Irrigated_cost_dmg = x.Irrigated_cost_dmg,
                              Nonirrigated_cost_dmg = x.Nonirrigated_cost_dmg,
                              Total_cost_dmg = x.Total_cost_dmg,
                              Grand_total_cost_dmg = x.Grand_total_cost_dmg,
                              Perennial_horti_cost_dmg = x.Perennial_horti_cost_dmg,
                              Perennial_sugarcane_cost_dmg = x.Perennial_sugarcane_cost_dmg,
                          }).FirstOrDefault(),
                      }).FirstOrDefault();
                }

                return response;
            }

            return null;
        }

        /// <summary>
        /// GetCropBySeasonID.
        /// </summary>
        /// <param name="season_id">season_id.</param>
        /// <param name="crop_type">crop_type.</param>
        /// <returns>List.</returns>
        public List<GetCropBySeasonIdModel> GetCropBySeasonID(string season_id, string crop_type)
        {
            DataSet dataSet = new DataSet();
            List<DbParameter> parameters = new List<DbParameter>();
            parameters.Add(new SqlParameter { ParameterName = "@query_name", Value = qn_GetCropBySeasonID, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@User_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@Year", Value = DBNull.Value, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@season_id", Value = season_id, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@Crop_activity", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@Crop_type", Value = crop_type, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@Crop_Name_Like", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@date", Value = DBNull.Value, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@Approval_Status", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@Crop_ID", Value = DBNull.Value, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@District_ID", Value = DBNull.Value, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@block_ID", Value = DBNull.Value, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@Panchayat_ID", Value = DBNull.Value, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            dataSet = SqlHelper.ExecuteDataSet<SqlConnection>(sp_GetCropBySeasonID, parameters, SqlHelper.ExecutionType.Procedure);

            if (dataSet != null && dataSet.Tables.Count > 0)
            {
                List<GetCropBySeasonIdModel> response = SqlHelper.ConvertDataTableToList<GetCropBySeasonIdModel>(dataSet.Tables[0]);
                return response;
            }

            return null;
        }

        /// <summary>
        /// GetSeasonByYear.
        /// </summary>
        /// <param name="year">year.</param>
        /// <param name="crop_type">crop_type.</param>
        /// <returns>List.</returns>
        public List<GetSeasonByYearModel> GetSeasonByYear(int year, string crop_type)
        {
            DataSet dataSet = new DataSet();
            List<DbParameter> parameters = new List<DbParameter>();
            parameters.Add(new SqlParameter { ParameterName = "@query_name", Value = qn_GetSeasonByYear, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@User_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@Year", Value = year, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@season_id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@Crop_activity", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@Crop_type", Value = crop_type, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@Crop_Name_Like", Value = crop_type, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@date", Value = DBNull.Value, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@Approval_Status", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@Crop_ID", Value = DBNull.Value, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@District_ID", Value = DBNull.Value, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@block_ID", Value = DBNull.Value, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@Panchayat_ID", Value = DBNull.Value, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });

            dataSet = SqlHelper.ExecuteDataSet<SqlConnection>(sp_GetCropBySeasonID, parameters, SqlHelper.ExecutionType.Procedure);

            if (dataSet != null && dataSet.Tables.Count > 0)
            {
                List<GetSeasonByYearModel> response = SqlHelper.ConvertDataTableToList<GetSeasonByYearModel>(dataSet.Tables[0]);
                return response;
            }

            return null;
        }

        /// <summary>
        /// GetHortiProduceDataReport.
        /// </summary>
        /// <param name="cropCvrgTargetDataReportModel">cropCvrgTargetDataReportModel.</param>
        /// <returns>List.</returns>
        public List<string> GetHortiProduceDataReport(GetCropCvrgTargetDataReportModel cropCvrgTargetDataReportModel)
        {
            List<string> response = new List<string>();

            DataSet dataSet = new DataSet();
            List<DbParameter> parameters = new List<DbParameter>();
            parameters.Add(new SqlParameter { ParameterName = "@User_Id", Value = cropCvrgTargetDataReportModel.User_Id, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@query_name", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@Year", Value = cropCvrgTargetDataReportModel.Year, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@season_id", Value = cropCvrgTargetDataReportModel.Season_id, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@Crop_activity", Value = cropCvrgTargetDataReportModel.Crop_activity, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@Crop_type", Value = cropCvrgTargetDataReportModel.Crop_type, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@Crop_Name_Like", Value = cropCvrgTargetDataReportModel.Crop_Name_Like, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@date", Value = DBNull.Value, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@Approval_Status", Value = cropCvrgTargetDataReportModel.Approval_Status, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@Crop_ID", Value = cropCvrgTargetDataReportModel.Crop_ID, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@District_ID", Value = cropCvrgTargetDataReportModel.District_ID, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@block_ID", Value = cropCvrgTargetDataReportModel.Block_ID, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@Panchayat_ID", Value = cropCvrgTargetDataReportModel.Panchayat_ID, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            dataSet = SqlHelper.ExecuteDataSet<SqlConnection>(sp_GetCropBySeasonID, parameters, SqlHelper.ExecutionType.Procedure);

            if (dataSet != null && dataSet.Tables.Count > 0)
            {
                if (dataSet.Tables[0].Rows.Count > 0)
                {
                    for (var i = 0; i < dataSet.Tables.Count; i++)
                    {
                        string crpdmgmdl = this.DataTableToJsonObj(dataSet.Tables[i]);
                        response.Add(crpdmgmdl);
                    }
                }

                return response;
            }

            return null;
        }

        /// <summary>
        /// GetCropCvrgTargetProductivityDataReport.
        /// </summary>
        /// <param name="cropCvrgTargetDataReportModel">cropCvrgTargetDataReportModel.</param>
        /// <returns>List Values.</returns>
        public List<string> GetCropCvrgTargetProductivityDataReport(GetCropCvrgTargetDataReportModel cropCvrgTargetDataReportModel)
        {
            List<string> response = new List<string>();

            DataSet dataSet = new DataSet();
            List<DbParameter> parameters = new List<DbParameter>();
            parameters.Add(new SqlParameter { ParameterName = "@User_Id", Value = cropCvrgTargetDataReportModel.User_Id, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@query_name", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@Year", Value = cropCvrgTargetDataReportModel.Year, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@season_id", Value = cropCvrgTargetDataReportModel.Season_id, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@Crop_activity", Value = cropCvrgTargetDataReportModel.Crop_activity, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@Crop_type", Value = cropCvrgTargetDataReportModel.Crop_type, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@Crop_Name_Like", Value = cropCvrgTargetDataReportModel.Crop_Name_Like, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@date", Value = DBNull.Value, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@Approval_Status", Value = cropCvrgTargetDataReportModel.Approval_Status, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@Crop_ID", Value = cropCvrgTargetDataReportModel.Crop_ID, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@District_ID", Value = cropCvrgTargetDataReportModel.District_ID, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@block_ID", Value = cropCvrgTargetDataReportModel.Block_ID, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@Panchayat_ID", Value = cropCvrgTargetDataReportModel.Panchayat_ID, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });

            dataSet = SqlHelper.ExecuteDataSet<SqlConnection>(sp_GetCropBySeasonID, parameters, SqlHelper.ExecutionType.Procedure);

            if (dataSet != null && dataSet.Tables.Count > 0)
            {
                if (dataSet.Tables[0].Rows.Count > 0)
                {
                    string crpdmgmdl = this.DataTableToJsonObj(dataSet.Tables[0]);
                    response.Add(crpdmgmdl);
                }

                return response;
            }

            return null;
        }

        /// <summary>
        /// GetCropCvrgTargetDataReport.
        /// </summary>
        /// <param name="cropCvrgTargetDataReportModel">cropCvrgTargetDataReportModel.</param>
        /// <returns>Values.</returns>
        public List<string> GetCropCvrgTargetDataReport(GetCropCvrgTargetDataReportModel cropCvrgTargetDataReportModel)
        {
            List<string> response = new List<string>();

            DataSet dataSet = new DataSet();
            List<DbParameter> parameters = new List<DbParameter>();

            DateTime dateval = default(DateTime);
            if (cropCvrgTargetDataReportModel.Date != null)
            {
                dateval = DateTime.ParseExact(
                    cropCvrgTargetDataReportModel.Date,
                    "yyyy-MM-dd",
                    System.Globalization.CultureInfo.InvariantCulture);
            }

            parameters.Add(new SqlParameter { ParameterName = "@User_Id", Value = cropCvrgTargetDataReportModel.User_Id, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@query_name", Value = string.Empty, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@Year", Value = cropCvrgTargetDataReportModel.Year, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@season_id", Value = cropCvrgTargetDataReportModel.Season_id, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@Crop_activity", Value = cropCvrgTargetDataReportModel.Crop_activity, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@Crop_type", Value = cropCvrgTargetDataReportModel.Crop_type, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@Crop_Name_Like", Value = cropCvrgTargetDataReportModel.Crop_Name_Like, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@date", Value = string.IsNullOrEmpty(cropCvrgTargetDataReportModel.Date) ? DBNull.Value : (object)dateval, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@Approval_Status", Value = cropCvrgTargetDataReportModel.Approval_Status, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@Crop_ID", Value = cropCvrgTargetDataReportModel.Crop_ID, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@District_ID", Value = cropCvrgTargetDataReportModel.District_ID, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@block_ID", Value = cropCvrgTargetDataReportModel.Block_ID, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@Panchayat_ID", Value = cropCvrgTargetDataReportModel.Panchayat_ID, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@fruits_Perennial", Value = cropCvrgTargetDataReportModel.FruitPerennial, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@crop_category", Value = cropCvrgTargetDataReportModel.CropCategory, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });

            dataSet = SqlHelper.ExecuteDataSet<SqlConnection>(sp_GetCropBySeasonID, parameters, SqlHelper.ExecutionType.Procedure);

            if (dataSet != null && dataSet.Tables.Count > 0 && cropCvrgTargetDataReportModel.Crop_Name_Like == "Both" && cropCvrgTargetDataReportModel.Crop_activity == "Horticulture Produce")
            {
                string crpdmgmdl1 = this.DataTableToJsonObj(dataSet.Tables[0]);
                string crpdmgmdl2 = this.DataTableToJsonObj(dataSet.Tables[1]);
                response.Add(crpdmgmdl1);
                response.Add(crpdmgmdl2);
                return response;
            }

            if (dataSet.Tables.Count > 1)
            {
                if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Columns.Count > 2 && dataSet.Tables[1].Columns.Count > 2)
                {
                    string crpdmgmdl1 = this.DataTableToJsonObj(dataSet.Tables[0]);
                    string crpdmgmdl2 = this.DataTableToJsonObj(dataSet.Tables[1]);

                    response.Add(crpdmgmdl1);
                    response.Add(crpdmgmdl2);
                    if (dataSet.Tables.Count > 2)
                    {
                        string crpdmgmdl3 = this.DataTableToJsonObj(dataSet.Tables[2]);
                        response.Add(crpdmgmdl3);
                    }

                    return response;
                }
            }

            if (dataSet != null && dataSet.Tables.Count > 0)
            {
                if (dataSet.Tables[0].Rows.Count > 0)
                {
                    string crpdmgmdl = this.DataTableToJsonObj(dataSet.Tables[0]);
                    response.Add(crpdmgmdl);
                }

                return response;
            }

            return null;
        }

        /// <summary>
        /// Implemented for Target Vs coverage.
        /// </summary>
        /// <param name="targetVsCoverageModel">Collection of input parameters.</param>
        /// <returns>output received in Json format.</returns>
        public List<string> GetCropCvrgTargetVsCoverageDataReport(TargetVsCoverageModel targetVsCoverageModel)
        {
            List<string> response = new List<string>();
            DataSet dataSet = new DataSet();
            List<DbParameter> parameters = new List<DbParameter>();
            parameters.Add(new SqlParameter { ParameterName = "@User_Id", Value = targetVsCoverageModel.User_Id, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@Year", Value = targetVsCoverageModel.Year, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@season_id", Value = targetVsCoverageModel.Season_id, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@Crop_type", Value = targetVsCoverageModel.Crop_type, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@Approval_Status", Value = targetVsCoverageModel.Approval_Status, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@Crop_ID", Value = targetVsCoverageModel.Crop_ID, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@District_ID", Value = targetVsCoverageModel.District_ID, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@block_ID", Value = targetVsCoverageModel.Block_ID, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@Panchayat_ID", Value = targetVsCoverageModel.Panchayat_ID, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@template", Value = targetVsCoverageModel.Template, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dataSet = SqlHelper.ExecuteDataSet<SqlConnection>("usp_crop_tgt_vs_cvrg_report_data", parameters, SqlHelper.ExecutionType.Procedure);

            if (dataSet != null && dataSet.Tables.Count > 0 && targetVsCoverageModel.Crop_Name_Like == "Both" && targetVsCoverageModel.Crop_activity == "Horticulture Produce")
            {
                string crpdmgmdl1 = this.DataTableToJsonObj(dataSet.Tables[0]);
                string crpdmgmdl2 = this.DataTableToJsonObj(dataSet.Tables[1]);
                response.Add(crpdmgmdl1);
                response.Add(crpdmgmdl2);

                return response;
            }

            if (dataSet.Tables.Count > 1)
            {
                if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Columns.Count > 2 && dataSet.Tables[1].Columns.Count > 2)
                {
                    string crpdmgmdl1 = this.DataTableToJsonObj(dataSet.Tables[0]);
                    string crpdmgmdl2 = this.DataTableToJsonObj(dataSet.Tables[1]);
                    response.Add(crpdmgmdl1);
                    response.Add(crpdmgmdl2);

                    return response;
                }
            }

            if (dataSet != null && dataSet.Tables.Count > 0)
            {
                if (dataSet.Tables[0].Rows.Count > 0)
                {
                    string crpdmgmdl = this.DataTableToJsonObj(dataSet.Tables[0]);
                    response.Add(crpdmgmdl);
                }

                return response;
            }

            return null;
        }

        /// <summary>
        /// GetRainfallReportData.
        /// </summary>
        /// <param name="month_year">month_year.</param>
        /// <param name="district_name">district_name.</param>
        /// <returns>Values List.</returns>
        public List<string> GetRainfallReportData(string month_year, string district_name)
        {
            List<string> response = new List<string>();
            DataSet dataSet = new DataSet();
            List<DbParameter> parameters = new List<DbParameter>();
            parameters.Add(new SqlParameter { ParameterName = "@month_year", Value = month_year, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@district_name", Value = district_name, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@query_name", Value = string.Empty, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dataSet = SqlHelper.ExecuteDataSet<SqlConnection>(sp_GetRainfallReportData, parameters, SqlHelper.ExecutionType.Procedure);

            if (dataSet != null && dataSet.Tables.Count > 0)
            {
                string crpdmgmdl = this.DataTableToJsonObj(dataSet.Tables[0]);
                response.Add(crpdmgmdl);

                return response;
            }

            return null;
        }

        /// <summary>
        /// GetCropDamageReportData.
        /// </summary>
        /// <param name="year">year.</param>
        /// <param name="district_ID">district_ID.</param>
        /// <param name="user_id">user_id.</param>
        /// <param name="damageReason_Id">damageReason_Id.</param>
        /// <returns>Values.</returns>
        public List<string> GetCropDamageReportData(int year, string district_ID, int user_id, string damageReason_Id)
        {
            List<string> response = new List<string>();
            DataSet dataSet = new DataSet();
            List<DbParameter> parameters = new List<DbParameter>();
            parameters.Add(new SqlParameter { ParameterName = "@User_Id", Value = user_id, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@year", Value = year, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@from_date", Value = DBNull.Value, SqlDbType = SqlDbType.Date, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@end_date", Value = DBNull.Value, SqlDbType = SqlDbType.Date, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@district_ID", Value = district_ID, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@block_ID", Value = DBNull.Value, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@damage_reason_id", Value = Convert.ToInt32(damageReason_Id), SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            dataSet = SqlHelper.ExecuteDataSet<SqlConnection>(sp_GetCropDamageReportData, parameters, SqlHelper.ExecutionType.Procedure);

            if (dataSet != null && dataSet.Tables.Count > 0)
            {
                string crpdmgmdl = this.DataTableToJsonObj(dataSet.Tables[1]);
                response.Add(crpdmgmdl);
                string cropDamageReportSums = this.DataTableToJsonObj(dataSet.Tables[3]);
                response.Add(cropDamageReportSums);
                string sumvalue = this.DataTableToJsonObj(dataSet.Tables[2]);
                response.Add(sumvalue);
                return response;
            }

            return null;
        }

        /// <summary>
        /// DataTableToJsonObj_Pivot.
        /// </summary>
        /// <param name="dt">dt.</param>
        /// <param name="templateColumns">templateColumns.</param>
        /// <param name="cropColumn">cropColumn.</param>
        /// <returns>string.</returns>
        public string DataTableToJsonObj_Pivot(DataTable dt, string templateColumns, string cropColumn)
        {
            DataSet ds = new DataSet();
            ds.Merge(dt);
            StringBuilder jsonString = new StringBuilder();
            Dictionary<string, string> dicTemplateColumnsValues = new Dictionary<string, string>();
            string cropWithPivotColumns = string.Empty;
            bool isSkipIteration = false;
            if (templateColumns.Split(',').Length > 0)
            {
                foreach (string templateColumn in templateColumns.Split(','))
                {
                    if (!string.IsNullOrEmpty(templateColumn))
                    {
                        dicTemplateColumnsValues.Add(templateColumn, string.Empty);
                    }
                }
            }
            else
            {
                dicTemplateColumnsValues.Add(templateColumns, string.Empty);

                dicTemplateColumnsValues.Add(cropColumn, string.Empty);
            }

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                jsonString.Append("[");
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (!isSkipIteration) jsonString.Append("{");
                    {
                        cropWithPivotColumns = string.Empty;
                        dicTemplateColumnsValues[cropColumn] = string.Empty;
                    }

                    for (int j = 0; j < ds.Tables[0].Columns.Count; j++)
                    {
                        if (dicTemplateColumnsValues.ContainsKey(ds.Tables[0].Columns[j].ColumnName))
                        {
                            dicTemplateColumnsValues[ds.Tables[0].Columns[j].ColumnName] = ds.Tables[0].Rows[i][j].ToString();
                            if (isSkipIteration && ds.Tables[0].Columns[j].ColumnName != cropColumn) continue;
                        }

                        if (!string.IsNullOrEmpty(dicTemplateColumnsValues[cropColumn]))
                        {
                            cropWithPivotColumns = dicTemplateColumnsValues[cropColumn] + "_";
                        }

                        if (ds.Tables[0].Columns[j].ColumnName != cropColumn)
                        {
                            if (j < ds.Tables[0].Columns.Count - 1)
                            {
                                jsonString.Append("\"" + cropWithPivotColumns + ds.Tables[0].Columns[j].ColumnName.ToString() + "\":" + "\"" + ds.Tables[0].Rows[i][j].ToString() + "\",");
                            }
                            else if (j == ds.Tables[0].Columns.Count - 1)
                            {
                                jsonString.Append("\"" + cropWithPivotColumns + ds.Tables[0].Columns[j].ColumnName.ToString() + "\":" + "\"" + ds.Tables[0].Rows[i][j].ToString() + "\"");
                            }
                        }
                    }

                    dicTemplateColumnsValues.All(k => k.Key == cropColumn || (i + 1) > ds.Tables[0].Rows.Count - 1 ? true : isSkipIteration = ds.Tables[0].Rows[i + 1][k.Key].ToString() == k.Value);
                    if (i == ds.Tables[0].Rows.Count - 1) isSkipIteration = false;
                    if (isSkipIteration) jsonString.Append(",");
                    if (!isSkipIteration)
                    {
                        if (i == ds.Tables[0].Rows.Count - 1)
                        {
                            jsonString.Append("}");
                        }
                        else
                        {
                            jsonString.Append("},");
                        }
                    }
                    else
                    {
                        if (i == ds.Tables[0].Rows.Count - 1)
                        {
                            jsonString.Append("}");
                        }
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
        /// DataTableToJsonObj.
        /// </summary>
        /// <param name="dt">dt.</param>
        /// <returns>Datatable values.</returns>
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
        /// GetOfflineACViewSubmissionPancht.
        /// </summary>
        /// <param name="panchayat_Id">panchayat_Id.</param>
        /// <returns>Values.</returns>
        public List<ViewSubmissionAcPanchayat> GetOfflineACViewSubmissionPancht(string panchayat_Id)
        {
            List<ViewSubmissionAcPanchayat> response = new List<ViewSubmissionAcPanchayat>();

            List<DtoViewSubmissionAffectedArea> dtoAffectedArea = new List<DtoViewSubmissionAffectedArea>();

            DataSet dataSet = new DataSet();
            List<DbParameter> parameters = new List<DbParameter>();
            parameters.Add(new SqlParameter { ParameterName = "@query_name", Value = qn_GetOfflineACViewSubmissionPancht, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@District_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@Block_id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@Panchayat_Id", Value = panchayat_Id, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@damage_reason_id", Value = DBNull.Value, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@damage_id", Value = DBNull.Value, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            dataSet = SqlHelper.ExecuteDataSet<SqlConnection>(sp_crp_dmg_ctlr_get, parameters, SqlHelper.ExecutionType.Procedure);

            if (dataSet != null && dataSet.Tables.Count > 0)
            {
                List<DtoViewSubmissionAcPanchayat> dtocaptureDT = SqlHelper.ConvertDataTableToList<DtoViewSubmissionAcPanchayat>(dataSet.Tables[0]);
                dtoAffectedArea = SqlHelper.ConvertDataTableToList<DtoViewSubmissionAffectedArea>(dataSet.Tables[1]);

                List<DtoViewSubmissionDamageArea> dtodamageArea = SqlHelper.ConvertDataTableToList<DtoViewSubmissionDamageArea>(dataSet.Tables[2]);
                if (dtocaptureDT != null && dtocaptureDT.Any())
                {
                    response.AddRange(dtocaptureDT.Select(x =>
                     new ViewSubmissionAcPanchayat
                     {
                         Damage_reason_id = x.Damage_reason_id,
                         Damage_reason_name = x.Damage_reason_name,
                         Panchayat_id = x.Panchayat_id,
                         Panchayat_name = x.Panchayat_name,
                         AC_Submitted_date = x.AC_Submitted_date,
                         BAO_Approval_flag = x.BAO_Approval_flag,
                         BAO_Approved_date = x.BAO_Approved_date,
                         BAO_Approved_userid = x.BAO_Approved_userid,
                         Dm_finalapprovalFlag = x.DM_Final_Approval_Flag,
                         DAO_Approval_flag = x.DAO_Approval_flag,
                         DAO_Approved_date = x.DAO_Approved_date,
                         Ac_submitted_username = x.Ac_submitted_username,
                         Bao_approved_username = x.Bao_submitted_username,
                         Dao_approved_username = x.Dao_submitted_username,
                         Ac_submit_flag = x.Ac_submit_flag,
                         Bao_add_edit_flag = x.Bao_add_edit_flag,
                         Dao_add_edit_flag = x.Dao_add_edit_flag,
                         Rec_updated_userid = x.Rec_updated_userid,
                         DaO_Approval_Reason = x.Dao_comments,
                         BaO_Approval_Reason = x.Bao_comments,
                         Rec_updated_date = x.Rec_updated_date,
                         UpdatedBy = x.Rec_updated_username,
                         AC_Submitted_userid = x.AC_Submitted_userid,
                         DAO_Approved_userid = x.DAO_Approved_userid,
                     }).GroupBy(x => new { x.Panchayat_id, x.Damage_reason_id }).Select(x => x.First()).ToList());

                    foreach (var item in response)
                    {
                        item.CoveredAreaobj = dtocaptureDT.Where(x => x.Damage_reason_id == item.Damage_reason_id).Select(x => new ViewSubmissionCoveredArea
                        {
                            Irrigated_area = x.Irrigated_area,
                            Nonirrigated_area = x.Nonirrigated_area,
                            Total_area = x.Total_area,
                            Grand_total_area = x.Grand_total_area,
                            Perennial_horti = x.Perennial_horti,
                            Perennial_sugarcane = x.Perennial_sugarcane,
                        }).FirstOrDefault();

                        item.AffectedAreaobj = dtoAffectedArea.Where(x => x.Damage_reason_id == item.Damage_reason_id).Select(x => new ViewSubmissionAffectedArea
                        {
                            Perennial_horti_impact = x.Perennial_horti_impact,
                            Perennial_sugarcane_impact = x.Perennial_sugarcane_impact,
                            Total_impact_area = x.Total_impact_area,
                            Grand_total_impact = x.Grand_total_impact,
                            Crops = dtoAffectedArea.Where(x => x.Damage_reason_id == x.Damage_reason_id).Select(x => new ViewSubmissionCrops
                            {
                                Crop_id = x.Crop_id,
                                Crop_name = x.Crop_name,
                                Damage_area = x.Damage_area,
                            }).GroupBy(x => x.Crop_id).Select(x => x.First()).ToList(),
                        }).FirstOrDefault();

                        item.DamageAreaobj = dtodamageArea.Where(x => x.Damage_reason_id == item.Damage_reason_id).Select(x => new ViewSubmissionDamageArea
                        {
                            Irrigated_area_dmg = x.Irrigated_area_dmg,
                            Nonirrigated_area_dmg = x.Nonirrigated_area_dmg,
                            Total_area_dmg = x.Total_area_dmg,
                            Grand_total_area_dmg = x.Grand_total_area_dmg,
                            Perennial_horti_dmg = x.Perennial_horti_dmg,
                            Perennial_sugarcane_dmg = x.Perennial_sugarcane_dmg,
                        }).FirstOrDefault();

                        item.DamageValueobj = dtodamageArea.Where(x => x.Damage_reason_id == item.Damage_reason_id).Select(x => new ViewSubmissionDamageValue
                        {
                            Irrigated_cost_dmg = x.Irrigated_cost_dmg,
                            Nonirrigated_cost_dmg = x.Nonirrigated_cost_dmg,
                            Total_cost_dmg = x.Total_cost_dmg,
                            Grand_total_cost_dmg = x.Grand_total_cost_dmg,
                            Perennial_horti_cost_dmg = x.Perennial_horti_cost_dmg,
                            Perennial_sugarcane_cost_dmg = x.Perennial_sugarcane_cost_dmg,
                        }).FirstOrDefault();
                    }
                }

                return response;
            }

            return null;
        }

        /// <summary>
        /// GetAllDistrict.
        /// </summary>
        /// <returns>List Values.</returns>
        public List<DistrictList> GetAllDistrict()
        {
            List<DistrictList> list = new List<DistrictList>();
            List<DbParameter> dbparams = new List<DbParameter>();
            dbparams.Add(new SqlParameter { ParameterName = "@query_name", Value = qn_GetAllDistrict, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(sp_dim_cntrlr, dbparams, SqlHelper.ExecutionType.Procedure);
            if (dt != null && dt.Rows.Count > 0)
            {
                list = SqlHelper.ConvertDataTableToList<DistrictList>(dt);
            }

            return list;
        }

        /// <summary>
        /// POSTHorticultureproductivity.
        /// </summary>
        /// <param name="horticultureproductivity">horticultureproductivity.</param>
        /// <returns>Status Response.</returns>
        public int POSTHorticultureproductivity(List<Horticultureproductivity> horticultureproductivity)
        {
            int insertRowsCount = 0;
            if (horticultureproductivity.Any())
            {
                foreach (var cclist in horticultureproductivity)
                {
                    foreach (var cpvalue in cclist.CropValues)
                    {
                        List<DbParameter> dbparams = new List<DbParameter>();
                        dbparams.Add(new SqlParameter { ParameterName = "@District_Id", Value = cclist.District_Id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                        dbparams.Add(new SqlParameter { ParameterName = "@Season_Id", Value = cclist.Season_Id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });

                        dbparams.Add(new SqlParameter { ParameterName = "@Crop_name", Value = cpvalue.Crop_name, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                        dbparams.Add(new SqlParameter { ParameterName = "@productivity", Value = cpvalue.Productivity, SqlDbType = SqlDbType.Decimal, Direction = ParameterDirection.Input });

                        dbparams.Add(new SqlParameter { ParameterName = "@Submitted_date", Value = cpvalue.Submitted_date, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
                        dbparams.Add(new SqlParameter { ParameterName = "@Submitted_userid", Value = cpvalue.Submitted_userid, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                        dbparams.Add(new SqlParameter { ParameterName = "@crop_id", Value = cpvalue.CropID, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                        Dictionary<string, dynamic> result = SqlHelper.ExecuteNonQuery<SqlConnection>(sp_POSTHorticultureproductivity, dbparams, SqlHelper.ExecutionType.Procedure);
                        insertRowsCount += result["RowsAffected"];
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
        /// PostCropDamageReason.
        /// </summary>
        /// <param name="damageReason">damageReason.</param>
        /// <returns>Status Response.</returns>
        public int PostCropDamageReason(DamageReasonPost damageReason)
        {
            List<DbParameter> dbparams = new List<DbParameter>();
            dbparams.Add(new SqlParameter { ParameterName = "@query_name", Value = qn_PostCropDamageReason, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@damage_reason_name", Value = damageReason.DamageReasonName, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@User_id", Value = damageReason.UserId, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(sp_dmg_list_ctrlr, dbparams, SqlHelper.ExecutionType.Procedure);

            if (dt != null && dt.Rows.Count > 0)
            {
                string gdata = dt.Rows[0]["Column1"].ToString();

                if (gdata == "I")
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// PostCropDamageName.
        /// </summary>
        /// <param name="cropPost">cropPost.</param>
        /// <returns>Status Response.</returns>
        public int PostCropDamageName(CropPost cropPost)
        {
            List<DbParameter> dbparams = new List<DbParameter>();
            dbparams.Add(new SqlParameter { ParameterName = "@query_name", Value = qn_PostCropDamageName, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });

            dbparams.Add(new SqlParameter { ParameterName = "@Crop_name", Value = cropPost.CropName, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });

            dbparams.Add(new SqlParameter { ParameterName = "@User_id", Value = cropPost.UserId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });

            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(sp_dmg_list_ctrlr, dbparams, SqlHelper.ExecutionType.Procedure);

            if (dt != null && dt.Rows.Count > 0)
            {
                string gdata = dt.Rows[0]["Column1"].ToString();

                if (gdata == "I")
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }

            return 0;
        }

        /// <summary>
        /// PostDelDamageReasonList.
        /// </summary>
        /// <param name="damage">damage.</param>
        /// <returns>Response Status.</returns>
        public int PostDelDamageReasonList(DtoEditDamageRequest damage)
        {
            List<DbParameter> dbparams = new List<DbParameter>();
            dbparams.Add(new SqlParameter { ParameterName = "@query_name", Value = qn_PostDelDamageReasonList, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@damage_reason_id", Value = damage.Damage_reason_id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@Assigned_Crop_Id", Value = damage.Assigned_Crop_Id, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@Assigned_District_Ids", Value = damage.Assigned_District_Ids, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@Year", Value = damage.Year, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(sp_dmg_list_ctrlr, dbparams, SqlHelper.ExecutionType.Procedure);
            if (dt != null && dt.Rows.Count > 0)
            {
                string gdata = dt.Rows[0]["Column1"].ToString();

                if (gdata == "D")
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }

            return 0;
        }

        /// <summary>
        /// PostCropCoverageDamageDetails.
        /// </summary>
        /// <param name="postCropCoverageDamageDetails">postCropCoverageDamageDetails.</param>
        /// <returns>Status response.</returns>
        public int PostCropCoverageDamageDetails(InsCropCoverageDamageDetails postCropCoverageDamageDetails)
        {
            List<DbParameter> dbparams = new List<DbParameter>();
            dbparams.Add(new SqlParameter { ParameterName = "@User_Id", Value = postCropCoverageDamageDetails.User_Id, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@panchayat_id", Value = postCropCoverageDamageDetails.Panchayat_id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@damage_id", Value = postCropCoverageDamageDetails.Damage_id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@damage_reason_id", Value = postCropCoverageDamageDetails.Damage_reason_id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@crop_id", Value = postCropCoverageDamageDetails.Crop_id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@damage_area", Value = postCropCoverageDamageDetails.Damage_area, SqlDbType = SqlDbType.Decimal, Direction = ParameterDirection.Input });

            Dictionary<string, dynamic> result = SqlHelper.ExecuteNonQuery<SqlConnection>(sp_PostCropCoverageDamageDetails, dbparams, SqlHelper.ExecutionType.Procedure);


            return 1;
        }

        /// <summary>
        /// PostUpdateDamageReasonStatus.
        /// </summary>
        /// <param name="cropDamageGetModel">cropDamageGetModel.</param>
        /// <returns>Response Status.</returns>
        public int PostUpdateDamageReasonStatus(DtoDamageStatusDetails cropDamageGetModel)
        {
            List<DbParameter> dbparams = new List<DbParameter>();
            dbparams.Add(new SqlParameter { ParameterName = "@query_name", Value = qn_PostUpdateDamageReasonStatus, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@Status_Flg", Value = cropDamageGetModel.Status, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@damage_reason_id", Value = cropDamageGetModel.Damage_reason_id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            Dictionary<string, dynamic> result = SqlHelper.ExecuteNonQuery<SqlConnection>(sp_dmg_list_ctrlr, dbparams, SqlHelper.ExecutionType.Procedure);



            return 1;
        }

        /// <summary>
        /// PostCropCvgDamagePancytApproval.
        /// </summary>
        /// <param name="insCropDamagePancytApproval">insCropDamagePancytApproval.</param>
        /// <returns>Status Response.</returns>
        public CropResponce PostCropCvgDamagePancytApproval(CropDamageGetAll insCropDamagePancytApproval)
        {
            int insertRowsCount = 0;
            int insertRowsCount1 = 0;
            CropResponce responce = new CropResponce();
            List<DbParameter> dbparams = new List<DbParameter>();
            dbparams.Add(new SqlParameter { ParameterName = "@User_Id", Value = 0, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@submission_source", Value = insCropDamagePancytApproval.Submission_source, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@panchayat_id", Value = insCropDamagePancytApproval.PanchayatId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@damage_id", Value = DBNull.Value, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@damage_reason_id", Value = insCropDamagePancytApproval.DamageReasonId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@net_sown_area", Value = insCropDamagePancytApproval.NetSownArea, SqlDbType = SqlDbType.Decimal, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@irrigated_area", Value = insCropDamagePancytApproval.CoveredArea.Irrigated_Dmg_Area, SqlDbType = SqlDbType.Decimal, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@nonirrigated_area", Value = insCropDamagePancytApproval.CoveredArea.Non_irrigated_Dmg_Area, SqlDbType = SqlDbType.Decimal, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@total_area", Value = insCropDamagePancytApproval.CoveredArea.Total, SqlDbType = SqlDbType.Decimal, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@perennial_horti", Value = insCropDamagePancytApproval.CoveredArea.Perennial_horticulture, SqlDbType = SqlDbType.Decimal, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@perennial_sugarcane", Value = insCropDamagePancytApproval.CoveredArea.Perennial_sugarcane, SqlDbType = SqlDbType.Decimal, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@grand_total_area", Value = insCropDamagePancytApproval.CoveredArea.GrandTotal, SqlDbType = SqlDbType.Decimal, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@irrigated_area_dmg", Value = insCropDamagePancytApproval.DamageArea.Irrigated_Dmg_Area, SqlDbType = SqlDbType.Decimal, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@irrigated_cost_dmg", Value = insCropDamagePancytApproval.DamageArea.Irrigated_Dmg_Area_Estimated, SqlDbType = SqlDbType.Decimal, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@nonirrigated_area_dmg", Value = insCropDamagePancytApproval.DamageArea.Non_irrigated_Dmg_Area, SqlDbType = SqlDbType.Decimal, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@nonirrigated_cost_dmg", Value = insCropDamagePancytApproval.DamageArea.Non_irrigated_Dmg_Area_Estimated, SqlDbType = SqlDbType.Decimal, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@total_area_dmg", Value = insCropDamagePancytApproval.DamageArea.TotalArea, SqlDbType = SqlDbType.Decimal, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@total_cost_dmg", Value = insCropDamagePancytApproval.DamageArea.TotalAmount, SqlDbType = SqlDbType.Decimal, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@perennial_horti_dmg", Value = insCropDamagePancytApproval.DamageArea.Perennial_horticulture, SqlDbType = SqlDbType.Decimal, Direction = ParameterDirection.Input });

            dbparams.Add(new SqlParameter { ParameterName = "@perennial_sugarcane_dmg", Value = insCropDamagePancytApproval.DamageArea.Perennial_sugarcane, SqlDbType = SqlDbType.Decimal, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@perennial_horti_cost_dmg", Value = insCropDamagePancytApproval.DamageArea.Perennial_horticulture_Estimated, SqlDbType = SqlDbType.Decimal, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@perennial_sugarcane_cost_dmg", Value = insCropDamagePancytApproval.DamageArea.Perennial_sugarcane_Estimated, SqlDbType = SqlDbType.Decimal, Direction = ParameterDirection.Input });

            dbparams.Add(new SqlParameter { ParameterName = "@grand_total_area_dmg", Value = insCropDamagePancytApproval.DamageArea.GrandTotalArea, SqlDbType = SqlDbType.Decimal, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@grand_total_cost_dmg", Value = insCropDamagePancytApproval.DamageArea.GrandTotalAmount, SqlDbType = SqlDbType.Decimal, Direction = ParameterDirection.Input });

            dbparams.Add(new SqlParameter { ParameterName = "@perennial_horti_impact", Value = insCropDamagePancytApproval.AffectedArea.Perennial_horti_impact, SqlDbType = SqlDbType.Decimal, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@perennial_sugarcane_impact", Value = insCropDamagePancytApproval.AffectedArea.Perennial_sugarcane_impact, SqlDbType = SqlDbType.Decimal, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@total_impact_area", Value = insCropDamagePancytApproval.AffectedArea.Total_impact_area, SqlDbType = SqlDbType.Decimal, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@grand_total_impact", Value = insCropDamagePancytApproval.AffectedArea.Grand_total_impact, SqlDbType = SqlDbType.Decimal, Direction = ParameterDirection.Input });

            dbparams.Add(new SqlParameter { ParameterName = "@ac_submitted_userid", Value = insCropDamagePancytApproval.Ac_Submitted_userid == 0 ? DBNull.Value : (object)insCropDamagePancytApproval.Ac_Submitted_userid, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@bao_approved_userid", Value = insCropDamagePancytApproval.Bao_Approved_userid == 0 ? DBNull.Value : (object)insCropDamagePancytApproval.Bao_Approved_userid, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@dao_approved_userid", Value = insCropDamagePancytApproval.Dao_Approved_userid == 0 ? DBNull.Value : (object)insCropDamagePancytApproval.Dao_Approved_userid, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@rec_created_userid", Value = insCropDamagePancytApproval.Rec_updated_userid == 0 ? DBNull.Value : (object)insCropDamagePancytApproval.Rec_updated_userid, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@rec_created_date", Value = DBNull.Value, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@rec_updated_userid", Value = insCropDamagePancytApproval.Rec_updated_userid == 0 ? DBNull.Value : (object)insCropDamagePancytApproval.Rec_updated_userid, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@rec_updated_date", Value = insCropDamagePancytApproval.Rec_updated_date == DateTime.MinValue ? DBNull.Value : (object)insCropDamagePancytApproval.Rec_updated_date, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });

            dbparams.Add(new SqlParameter { ParameterName = "@AC_Submitted_date", Value = insCropDamagePancytApproval.Ac_Submitted_date == DateTime.MinValue ? DBNull.Value : (object)insCropDamagePancytApproval.Ac_Submitted_date, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@BAO_Approved_date", Value = insCropDamagePancytApproval.Bao_Approved_date == DateTime.MinValue ? DBNull.Value : (object)insCropDamagePancytApproval.Bao_Approved_date, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@baO_Approval_Reason", Value = insCropDamagePancytApproval.Bao_Approval_Reason, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });

            dbparams.Add(new SqlParameter { ParameterName = "@DAO_Approved_date", Value = insCropDamagePancytApproval.Dao_Approved_date == DateTime.MinValue ? DBNull.Value : (object)insCropDamagePancytApproval.Dao_Approved_date, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@DaO_Approval_Reason", Value = insCropDamagePancytApproval.Dao_Approval_Reason, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });

            dbparams.Add(new SqlParameter { ParameterName = "@BAO_Approval_flag", Value = insCropDamagePancytApproval.Bao_Approval_flag, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@ac_submit_flag", Value = insCropDamagePancytApproval.Ac_submit_flag, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@bao_add_edit_flag", Value = insCropDamagePancytApproval.Bao_add_edit_flag, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@dao_add_edit_flag", Value = insCropDamagePancytApproval.Dao_add_edit_flag, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });

            dbparams.Add(new SqlParameter { ParameterName = "@dao_comments", Value = insCropDamagePancytApproval.Dao_Approval_Reason, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@bao_comments", Value = insCropDamagePancytApproval.Bao_Approval_Reason, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@DAO_Approval_flag", Value = insCropDamagePancytApproval.Dao_Approval_flag, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@DM_Final_Approval_Flag", Value = insCropDamagePancytApproval.Dm_final_approve, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@response_status", SqlDbType = SqlDbType.NVarChar, Size = 1000, Direction = ParameterDirection.Output });
            dbparams.Add(new SqlParameter { ParameterName = "@last_identity", SqlDbType = SqlDbType.NVarChar, Size = 1000, Direction = ParameterDirection.Output });

            Dictionary<string, dynamic> result = SqlHelper.ExecuteNonQuery<SqlConnection>(sp_PostCropCvgDamagePancytApproval, dbparams, SqlHelper.ExecutionType.Procedure);

            insertRowsCount += result["RowsAffected"];

            string insertspOut = DBNull.Value.Equals(result["@response_status"]) ? string.Empty : result["@response_status"];
            string last_identity = DBNull.Value.Equals(result["@last_identity"]) ? string.Empty : result["@last_identity"];
            responce.Status = insertspOut == string.Empty ? "Insert Failed" : insertspOut;

            foreach (var crp in insCropDamagePancytApproval.AffectedArea.CropName)
            {
                Dictionary<string, dynamic> result1 = new Dictionary<string, dynamic>();

                List<DbParameter> dbparamsDng = new List<DbParameter>();
                dbparamsDng.Add(new SqlParameter { ParameterName = "@User_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsDng.Add(new SqlParameter { ParameterName = "@panchayat_id", Value = insCropDamagePancytApproval.PanchayatId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                dbparamsDng.Add(new SqlParameter { ParameterName = "@damage_id", Value = Convert.ToInt32(last_identity), SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                dbparamsDng.Add(new SqlParameter { ParameterName = "@damage_reason_id", Value = insCropDamagePancytApproval.DamageReasonId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                dbparamsDng.Add(new SqlParameter { ParameterName = "@crop_id", Value = crp.Crop_id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                dbparamsDng.Add(new SqlParameter { ParameterName = "@damage_area", Value = crp.Crop_value, SqlDbType = SqlDbType.Decimal, Direction = ParameterDirection.Input });

                result1 = SqlHelper.ExecuteNonQuery<SqlConnection>(sp_Crop_Cvrg_Damage_dtls, dbparamsDng, SqlHelper.ExecutionType.Procedure);
                insertRowsCount1 += result["RowsAffected"];
            }

            return responce;
        }

        /// <summary>
        /// PostDAOCropDamageApproval.
        /// </summary>
        /// <param name="insBAOCropdmgModel">insBAOCropdmgModel.</param>
        /// <returns>Status Response.</returns>
        public CropResponce PostDAOCropDamageApproval(InsBaoCropdmgModel insBAOCropdmgModel)
        {
            int insertRowsCount = 0;
            int insertRowsCount1 = 0;
            CropResponce responce = new CropResponce();
            foreach (var item in insBAOCropdmgModel.BlockList)
            {
                foreach (var item1 in item.PanchayatList)
                {
                    List<DbParameter> dbparams = new List<DbParameter>();
                    dbparams.Add(new SqlParameter { ParameterName = "@User_Id", Value = 0, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@submission_source", Value = item1.Submission_source, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@panchayat_id", Value = item1.PanchayatId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@damage_id", Value = DBNull.Value, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@damage_reason_id", Value = item1.DamageReasonId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@net_sown_area", Value = item1.NetSownArea, SqlDbType = SqlDbType.Decimal, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@irrigated_area", Value = item1.CoveredArea.Irrigated_Dmg_Area, SqlDbType = SqlDbType.Decimal, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@nonirrigated_area", Value = item1.CoveredArea.Non_irrigated_Dmg_Area, SqlDbType = SqlDbType.Decimal, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@total_area", Value = item1.CoveredArea.Total, SqlDbType = SqlDbType.Decimal, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@perennial_horti", Value = item1.CoveredArea.Perennial_horticulture, SqlDbType = SqlDbType.Decimal, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@perennial_sugarcane", Value = item1.CoveredArea.Perennial_sugarcane, SqlDbType = SqlDbType.Decimal, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@grand_total_area", Value = item1.CoveredArea.GrandTotal, SqlDbType = SqlDbType.Decimal, Direction = ParameterDirection.Input });

                    dbparams.Add(new SqlParameter { ParameterName = "@irrigated_area_dmg", Value = item1.DamageArea.Irrigated_Dmg_Area, SqlDbType = SqlDbType.Decimal, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@irrigated_cost_dmg", Value = item1.DamageArea.Irrigated_Dmg_Area_Estimated, SqlDbType = SqlDbType.Decimal, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@nonirrigated_area_dmg", Value = item1.DamageArea.Non_irrigated_Dmg_Area, SqlDbType = SqlDbType.Decimal, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@nonirrigated_cost_dmg", Value = item1.DamageArea.Non_irrigated_Dmg_Area_Estimated, SqlDbType = SqlDbType.Decimal, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@total_area_dmg", Value = item1.DamageArea.TotalArea, SqlDbType = SqlDbType.Decimal, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@total_cost_dmg", Value = item1.DamageArea.TotalAmount, SqlDbType = SqlDbType.Decimal, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@perennial_horti_dmg", Value = item1.DamageArea.Perennial_horticulture, SqlDbType = SqlDbType.Decimal, Direction = ParameterDirection.Input });

                    dbparams.Add(new SqlParameter { ParameterName = "@perennial_sugarcane_dmg", Value = item1.DamageArea.Perennial_sugarcane, SqlDbType = SqlDbType.Decimal, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@perennial_horti_cost_dmg", Value = item1.DamageArea.Perennial_horticulture_Estimated, SqlDbType = SqlDbType.Decimal, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@perennial_sugarcane_cost_dmg", Value = item1.DamageArea.Perennial_sugarcane_Estimated, SqlDbType = SqlDbType.Decimal, Direction = ParameterDirection.Input });

                    dbparams.Add(new SqlParameter { ParameterName = "@grand_total_area_dmg", Value = item1.DamageArea.GrandTotalArea, SqlDbType = SqlDbType.Decimal, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@grand_total_cost_dmg", Value = item1.DamageArea.GrandTotalAmount, SqlDbType = SqlDbType.Decimal, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@ac_submitted_userid", Value = item1.Ac_Submitted_userid, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@bao_approved_userid", Value = item1.Bao_Approved_userid, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@dao_approved_userid", Value = item1.Dao_Approved_userid, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@rec_created_userid", Value = item1.Rec_updated_userid, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@rec_created_date", Value = DBNull.Value, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@rec_updated_userid", Value = item1.Rec_updated_userid, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@rec_updated_date", Value = item1.Rec_updated_date == DateTime.MinValue ? DBNull.Value : (object)item1.Rec_updated_date, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });

                    dbparams.Add(new SqlParameter { ParameterName = "@AC_Submitted_date", Value = item1.Ac_Submitted_date == DateTime.MinValue ? DBNull.Value : (object)item1.Ac_Submitted_date, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@BAO_Approved_date", Value = item1.Bao_Approved_date == DateTime.MinValue ? DBNull.Value : (object)item1.Bao_Approved_date, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@baO_Approval_Reason", Value = item1.Bao_Approval_Reason, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });

                    dbparams.Add(new SqlParameter { ParameterName = "@DAO_Approved_date", Value = item1.Dao_Approved_date == DateTime.MinValue ? DBNull.Value : (object)item1.Dao_Approved_date, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@DaO_Approval_Reason", Value = item1.Dao_Approval_Reason, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });

                    dbparams.Add(new SqlParameter { ParameterName = "@BAO_Approval_flag", Value = item1.Bao_Approval_flag, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@ac_submit_flag", Value = item1.Ac_submit_flag, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@bao_add_edit_flag", Value = item1.Bao_add_edit_flag, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@dao_add_edit_flag", Value = item1.Dao_add_edit_flag, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });

                    dbparams.Add(new SqlParameter { ParameterName = "@dao_comments", Value = item1.Dao_Approval_Reason, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@bao_comments", Value = item1.Bao_Approval_Reason, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@DAO_Approval_flag", Value = item1.Dao_Approval_flag, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@DM_Final_Approval_Flag", Value = item1.Dm_finalapprovalFlag, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@response_status", SqlDbType = SqlDbType.NVarChar, Size = 1000, Direction = ParameterDirection.Output });
                    dbparams.Add(new SqlParameter { ParameterName = "@last_identity", SqlDbType = SqlDbType.NVarChar, Size = 1000, Direction = ParameterDirection.Output });

                    Dictionary<string, dynamic> result = SqlHelper.ExecuteNonQuery<SqlConnection>(sp_PostCropCvgDamagePancytApproval, dbparams, SqlHelper.ExecutionType.Procedure);

                    insertRowsCount += result["RowsAffected"];

                    string insertspOut = DBNull.Value.Equals(result["@response_status"]) ? string.Empty : result["@response_status"];
                    string last_identity = DBNull.Value.Equals(result["@last_identity"]) ? string.Empty : result["@last_identity"];
                    responce.Status = insertspOut == string.Empty ? "Insert Failed" : insertspOut;

                    if (!string.IsNullOrEmpty(last_identity))
                    {
                        foreach (var crp in item1.AffectedArea.CropName)
                        {
                            Dictionary<string, dynamic> result1 = new Dictionary<string, dynamic>();

                            List<DbParameter> dbparamsDng = new List<DbParameter>();
                            dbparamsDng.Add(new SqlParameter { ParameterName = "@User_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                            dbparamsDng.Add(new SqlParameter { ParameterName = "@panchayat_id", Value = item1.PanchayatId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                            dbparamsDng.Add(new SqlParameter { ParameterName = "@damage_id", Value = Convert.ToInt32(last_identity), SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                            dbparamsDng.Add(new SqlParameter { ParameterName = "@damage_reason_id", Value = item1.DamageReasonId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                            dbparamsDng.Add(new SqlParameter { ParameterName = "@crop_id", Value = crp.Crop_id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                            dbparamsDng.Add(new SqlParameter { ParameterName = "@damage_area", Value = crp.Crop_value, SqlDbType = SqlDbType.Decimal, Direction = ParameterDirection.Input });

                            result1 = SqlHelper.ExecuteNonQuery<SqlConnection>(sp_Crop_Cvrg_Damage_dtls, dbparamsDng, SqlHelper.ExecutionType.Procedure);
                            insertRowsCount1 += result["RowsAffected"];
                        }
                    }
                }
            }

            return responce;
        }

        /// <summary>
        /// PostBAOCropDamageApproval.
        /// </summary>
        /// <param name="insBAOCropdmgModel">insBAOCropdmgModel.</param>
        /// <returns>Status Response.</returns>
        public CropResponce PostBAOCropDamageApproval(InsBaoCropdmgModel insBAOCropdmgModel)
        {
            int insertRowsCount = 0;
            int insertRowsCount1 = 0;
            CropResponce responce = new CropResponce();
            foreach (var item in insBAOCropdmgModel.BlockList)
            {
                foreach (var item1 in item.PanchayatList)
                {
                    List<DbParameter> dbparams = new List<DbParameter>();
                    dbparams.Add(new SqlParameter { ParameterName = "@User_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@submission_source", Value = item1.Submission_source, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@panchayat_id", Value = item1.PanchayatId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@damage_id", Value = DBNull.Value, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@damage_reason_id", Value = item1.DamageReasonId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@net_sown_area", Value = item1.NetSownArea, SqlDbType = SqlDbType.Decimal, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@irrigated_area", Value = item1.CoveredArea.Irrigated_Dmg_Area, SqlDbType = SqlDbType.Decimal, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@nonirrigated_area", Value = item1.CoveredArea.Non_irrigated_Dmg_Area, SqlDbType = SqlDbType.Decimal, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@total_area", Value = item1.CoveredArea.Total, SqlDbType = SqlDbType.Decimal, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@perennial_horti", Value = item1.CoveredArea.Perennial_horticulture, SqlDbType = SqlDbType.Decimal, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@perennial_sugarcane", Value = item1.CoveredArea.Perennial_sugarcane, SqlDbType = SqlDbType.Decimal, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@grand_total_area", Value = item1.CoveredArea.GrandTotal, SqlDbType = SqlDbType.Decimal, Direction = ParameterDirection.Input });

                    dbparams.Add(new SqlParameter { ParameterName = "@irrigated_area_dmg", Value = item1.DamageArea.Irrigated_Dmg_Area, SqlDbType = SqlDbType.Decimal, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@irrigated_cost_dmg", Value = item1.DamageArea.Irrigated_Dmg_Area_Estimated, SqlDbType = SqlDbType.Decimal, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@nonirrigated_area_dmg", Value = item1.DamageArea.Non_irrigated_Dmg_Area, SqlDbType = SqlDbType.Decimal, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@nonirrigated_cost_dmg", Value = item1.DamageArea.Non_irrigated_Dmg_Area_Estimated, SqlDbType = SqlDbType.Decimal, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@total_area_dmg", Value = item1.DamageArea.TotalArea, SqlDbType = SqlDbType.Decimal, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@total_cost_dmg", Value = item1.DamageArea.TotalAmount, SqlDbType = SqlDbType.Decimal, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@perennial_horti_dmg", Value = item1.DamageArea.Perennial_horticulture, SqlDbType = SqlDbType.Decimal, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@perennial_sugarcane_dmg", Value = item1.DamageArea.Perennial_sugarcane, SqlDbType = SqlDbType.Decimal, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@perennial_horti_cost_dmg", Value = item1.DamageArea.Perennial_horticulture_Estimated, SqlDbType = SqlDbType.Decimal, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@perennial_sugarcane_cost_dmg", Value = item1.DamageArea.Perennial_sugarcane_Estimated, SqlDbType = SqlDbType.Decimal, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@grand_total_area_dmg", Value = item1.DamageArea.GrandTotalArea, SqlDbType = SqlDbType.Decimal, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@grand_total_cost_dmg", Value = item1.DamageArea.GrandTotalAmount, SqlDbType = SqlDbType.Decimal, Direction = ParameterDirection.Input });

                    dbparams.Add(new SqlParameter { ParameterName = "@perennial_horti_impact", Value = item1.AffectedArea.Perennial_horti_impact, SqlDbType = SqlDbType.Decimal, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@perennial_sugarcane_impact", Value = item1.AffectedArea.Perennial_sugarcane_impact, SqlDbType = SqlDbType.Decimal, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@total_impact_area", Value = item1.AffectedArea.Total_impact_area, SqlDbType = SqlDbType.Decimal, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@grand_total_impact", Value = item1.AffectedArea.Grand_total_impact, SqlDbType = SqlDbType.Decimal, Direction = ParameterDirection.Input });

                    dbparams.Add(new SqlParameter { ParameterName = "@ac_submitted_userid", Value = item1.Ac_Submitted_userid == null ? 0 : (object)item1.Ac_Submitted_userid, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@bao_approved_userid", Value = item1.Bao_Approved_userid == null ? 0 : (object)item1.Bao_Approved_userid, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@dao_approved_userid", Value = item1.Dao_Approved_userid == null ? 0 : (object)item1.Dao_Approved_userid, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@rec_created_userid", Value = item1.Rec_updated_userid == null ? 0 : (object)item1.Rec_updated_userid, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@rec_created_date", Value = DBNull.Value, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@rec_updated_userid", Value = item1.Rec_updated_userid == 0 ? 0 : (object)item1.Rec_updated_userid, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@rec_updated_date", Value = item1.Rec_updated_date == DateTime.MinValue ? DBNull.Value : (object)item1.Rec_updated_date, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });

                    dbparams.Add(new SqlParameter { ParameterName = "@AC_Submitted_date", Value = item1.Ac_Submitted_date == DateTime.MinValue ? DBNull.Value : (object)item1.Ac_Submitted_date, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });

                    dbparams.Add(new SqlParameter { ParameterName = "@BAO_Approved_date", Value = item1.Bao_Approved_date == DateTime.MinValue ? DBNull.Value : (object)item1.Bao_Approved_date, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@baO_Approval_Reason", Value = item1.Bao_Approval_Reason, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });

                    dbparams.Add(new SqlParameter { ParameterName = "@DAO_Approved_date", Value = item1.Dao_Approved_date == DateTime.MinValue ? DBNull.Value : (object)item1.Dao_Approved_date, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@DaO_Approval_Reason", Value = item1.Dao_Approval_Reason, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });

                    dbparams.Add(new SqlParameter { ParameterName = "@BAO_Approval_flag", Value = item1.Bao_Approval_flag, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });

                    dbparams.Add(new SqlParameter { ParameterName = "@ac_submit_flag", Value = item1.Ac_submit_flag, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });

                    dbparams.Add(new SqlParameter { ParameterName = "@bao_add_edit_flag", Value = item1.Bao_add_edit_flag, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@dao_add_edit_flag", Value = item1.Dao_add_edit_flag, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });

                    dbparams.Add(new SqlParameter { ParameterName = "@dao_comments", Value = item1.Dao_Approval_Reason, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@bao_comments", Value = item1.Bao_Approval_Reason, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });

                    dbparams.Add(new SqlParameter { ParameterName = "@DAO_Approval_flag", Value = item1.Dao_Approval_flag, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });

                    dbparams.Add(new SqlParameter { ParameterName = "@DM_Final_Approval_Flag", Value = item1.Dm_finalapprovalFlag, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@response_status", SqlDbType = SqlDbType.NVarChar, Size = 1000, Direction = ParameterDirection.Output });
                    dbparams.Add(new SqlParameter { ParameterName = "@last_identity", SqlDbType = SqlDbType.NVarChar, Size = 1000, Direction = ParameterDirection.Output });

                    Dictionary<string, dynamic> result = SqlHelper.ExecuteNonQuery<SqlConnection>(sp_PostCropCvgDamagePancytApproval, dbparams, SqlHelper.ExecutionType.Procedure);

                    insertRowsCount += result["RowsAffected"];

                    string insertspOut = DBNull.Value.Equals(result["@response_status"]) ? string.Empty : result["@response_status"];
                    string last_identity = DBNull.Value.Equals(result["@last_identity"]) ? string.Empty : result["@last_identity"];
                    responce.Status = insertspOut == string.Empty ? "Insert Failed" : insertspOut;

                    if (!string.IsNullOrEmpty(last_identity))
                    {
                        foreach (var crp in item1.AffectedArea.CropName)
                        {
                            Dictionary<string, dynamic> result1 = new Dictionary<string, dynamic>();

                            List<DbParameter> dbparamsDng = new List<DbParameter>();
                            dbparamsDng.Add(new SqlParameter { ParameterName = "@User_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                            dbparamsDng.Add(new SqlParameter { ParameterName = "@panchayat_id", Value = item1.PanchayatId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                            dbparamsDng.Add(new SqlParameter { ParameterName = "@damage_id", Value = Convert.ToInt32(last_identity), SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                            dbparamsDng.Add(new SqlParameter { ParameterName = "@damage_reason_id", Value = item1.DamageReasonId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                            dbparamsDng.Add(new SqlParameter { ParameterName = "@crop_id", Value = crp.Crop_id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                            dbparamsDng.Add(new SqlParameter { ParameterName = "@damage_area", Value = crp.Crop_value, SqlDbType = SqlDbType.Decimal, Direction = ParameterDirection.Input });

                            result1 = SqlHelper.ExecuteNonQuery<SqlConnection>(sp_Crop_Cvrg_Damage_dtls, dbparamsDng, SqlHelper.ExecutionType.Procedure);
                            insertRowsCount1 += result["RowsAffected"];
                        }
                    }
                }
            }

            return responce;
        }

        /// <summary>
        /// PostDamageDetails.
        /// </summary>
        /// <param name="damageDetails">damageDetails.</param>
        /// <returns>Status Response.</returns>
        public List<DtoPostDamageResponse> PostDamageDetails(DamageDetails damageDetails)
        {
            List<DbParameter> dbparams = new List<DbParameter>();
            dbparams.Add(new SqlParameter { ParameterName = "@User_Id", Value = damageDetails.UserId.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@damage_reason_id", Value = damageDetails.DamageReasonId, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@Year", Value = damageDetails.Year, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@Assigned_Crop_Id", Value = damageDetails.AssignedCropId, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@Assigned_District_Id", Value = damageDetails.AssigneddistrictId, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@Estd_Crop_Damage", Value = damageDetails.EstdCropDamage, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@Status_Flg", Value = damageDetails.StatusFlag, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });

            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(sp_PostDamageDetails, dbparams, SqlHelper.ExecutionType.Procedure);

            List<DtoPostDamageResponse> list = new List<DtoPostDamageResponse>();
            if (dt != null && dt.Rows.Count > 0)
            {
                list = SqlHelper.ConvertDataTableToList<DtoPostDamageResponse>(dt).GroupBy(x => x.District_name).Select(x => x.First()).ToList();
            }

            return list;
        }

        /// <summary>
        /// PostEditDamageDetails.
        /// </summary>
        /// <param name="damageDetails">damageDetails.</param>
        /// <returns>Status Response.</returns>
        public List<DtoPostDamageResponse> PostEditDamageDetails(EditDamageDetails damageDetails)
        {
            List<DbParameter> dbparams = new List<DbParameter>();
            dbparams.Add(new SqlParameter { ParameterName = "@User_Id", Value = damageDetails.UserId.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@damage_reason_id", Value = damageDetails.DamageReasonId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@Year", Value = damageDetails.Year, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@Assigned_Crop_Id", Value = damageDetails.AssignedCropId, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@Assigned_District_Id", Value = damageDetails.AssigneddistrictId, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@Estd_Crop_Damage", Value = damageDetails.EstdCropDamage, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@Status_Flg", Value = damageDetails.StatusFlag, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });

            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(sp_PostDamageDetails, dbparams, SqlHelper.ExecutionType.Procedure);

            List<DtoPostDamageResponse> list = new List<DtoPostDamageResponse>();
            if (dt != null && dt.Rows.Count > 0)
            {
                list = SqlHelper.ConvertDataTableToList<DtoPostDamageResponse>(dt).GroupBy(x => x.District_name).Select(x => x.First()).ToList();
            }

            return list;
        }

        /// <summary>
        /// InsertSeedUsedInput.
        /// </summary>
        /// <param name="input">input.</param>
        /// <returns>Success or Failure Status.</returns>
        public int InsertSeedUsedInput(SeedUsedInput input)
        {
            int insertRowsCount = 0;

            if (input != null && input.Crop_id != 0 && input.Season_id != 0 && input.UserId != 0 && input.SeedVariety != null && input.SeedVariety.Count > 0 && input.Crop_category != string.Empty)
            {
                foreach (var seedinput in input.SeedVariety)
                {
                    List<DbParameter> dbParams = new List<DbParameter>();

                    dbParams.Add(new SqlParameter { ParameterName = "@season_id", Value = input.Season_id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                    dbParams.Add(new SqlParameter { ParameterName = "@crop_id", Value = input.Crop_id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                    dbParams.Add(new SqlParameter { ParameterName = "@Crop_Variety_ID", Value = seedinput.Crop_variety_id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });

                    dbParams.Add(new SqlParameter { ParameterName = "@seed_variety", Value = seedinput.Seed_variety, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                    dbParams.Add(new SqlParameter { ParameterName = "@crop_category", Value = input.Crop_category, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                    dbParams.Add(new SqlParameter { ParameterName = "@seed_used_qty", Value = seedinput.Seed_used_qty, SqlDbType = SqlDbType.Decimal, Direction = ParameterDirection.Input });
                    dbParams.Add(new SqlParameter { ParameterName = "@user_id", Value = input.UserId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                    dbParams.Add(new SqlParameter { ParameterName = "@response_status", Value = input.Response_status, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });

                    Dictionary<string, dynamic> result = SqlHelper.ExecuteNonQuery<SqlConnection>(sp_InsertSeedUsedInput, dbParams, SqlHelper.ExecutionType.Procedure);

                    insertRowsCount += result["RowsAffected"];
                }

                return insertRowsCount;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// GetSeedusedIputViewSubmission.
        /// </summary>
        /// <param name="seasonId">seasonId.</param>
        /// <param name="cropId">cropId.</param>
        /// <param name="district_id">district_id.</param>
        /// <returns>List Values.</returns>
        public DtoSeedUserInput GetSeedusedIputViewSubmission(int seasonId, int cropId, int district_id)
        {
            DtoSeedUserInput list = null;

            List<DbParameter> parameters = new List<DbParameter>();

            parameters.Add(new SqlParameter { ParameterName = "@query", Value = qn_GetSeedusedIputViewSubmission, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@season_id", Value = seasonId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@crop_id", Value = cropId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@District_id", Value = district_id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });

            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(sp_GetSeedusedIputViewSubmission, parameters, SqlHelper.ExecutionType.Procedure);
            if (dt != null && dt.Rows.Count > 0)
            {
                list = SqlHelper.ConvertDataTableToList<DtoSeedUserInput>(dt)[0];

                list.Varieties = SqlHelper.ConvertDataTableToList<DtoSeedVariety>(dt).GroupBy(x => new { x.Seed_Id, x.Seed_variety, x.Crop_variety_id }).Select(x => x.First()).ToList();
            }

            return list;
        }

        /// <summary>
        /// GetSeedUsedVarietyname.
        /// </summary>
        /// <param name="seasonId">seasonId.</param>
        /// <param name="cropId">cropId.</param>
        /// <param name="district_id">district_id.</param>
        /// <returns>Values.</returns>
        public DtoSeedUserInput GetSeedUsedVarietyname(int seasonId, int cropId, int district_id)
        {
            DtoSeedUserInput list = null;

            List<DbParameter> parameters = new List<DbParameter>();

            parameters.Add(new SqlParameter { ParameterName = "@query", Value = qn_GetSeedUsedVarietyname, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@season_id", Value = seasonId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@crop_id", Value = cropId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@District_id", Value = district_id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });

            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(sp_GetSeedusedIputViewSubmission, parameters, SqlHelper.ExecutionType.Procedure);
            if (dt != null && dt.Rows.Count > 0)
            {
                list = SqlHelper.ConvertDataTableToList<DtoSeedUserInput>(dt)[0];

                list.Varieties = SqlHelper.ConvertDataTableToList<DtoSeedVariety>(dt).GroupBy(x => new { x.Seed_Id, x.Seed_variety, x.Crop_variety_id, x.Crop_category }).Select(x => x.First()).ToList();
            }

            return list;
        }

        /// <summary>
        /// GetSeedusedinputViewSubmissionOffline.
        /// </summary>
        /// <param name="district_id">district_id.</param>
        /// <returns>Values.</returns>
        public List<DtoSeedUserInput> GetSeedusedinputViewSubmissionOffline(int district_id)
        {
            List<DtoSeedUserInput> list = new List<DtoSeedUserInput>();

            List<DbParameter> parameters = new List<DbParameter>();

            parameters.Add(new SqlParameter { ParameterName = "@query", Value = qn_GetSeedusedinputViewSubmissionOffline, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@District_id", Value = district_id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });

            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(sp_GetSeedusedIputViewSubmission, parameters, SqlHelper.ExecutionType.Procedure);
            if (dt != null && dt.Rows.Count > 0)
            {
                list = SqlHelper.ConvertDataTableToList<DtoSeedUserInput>(dt).GroupBy(x => new { x.Season_id, x.Crop_id }).Select(x => x.First()).ToList();

                List<DtoSeedVarietyInput> listDTO = SqlHelper.ConvertDataTableToList<DtoSeedVarietyInput>(dt).GroupBy(x => new { x.Seed_Id, x.Seed_variety, x.Crop_Variety_ID, x.Crop_id, x.Season_id }).Select(x => x.First()).ToList();

                foreach (var item in list)
                {
                    item.Varieties = listDTO.Where(x => x.Season_id == item.Season_id && x.Crop_id == item.Crop_id).GroupBy(x => new { x.Seed_Id, x.Seed_variety, x.Crop_Variety_ID, x.Crop_id, x.Season_id }).Select(x => x.First()).Select(x => new DtoSeedVariety { Seed_Id = x.Seed_Id, Crop_variety_id = x.Crop_Variety_ID, Seed_used_qty = x.Seed_used_qty, Seed_variety = x.Seed_variety, Rec_created_userid = x.Rec_created_userid, Rec_created_date = x.Rec_created_date, Rec_updated_userid = x.Rec_updated_userid, Rec_updated_date = x.Rec_updated_date }).ToList();
                }
            }

            return list;
        }

        /// <summary>
        /// GetSeedPerformanceDataReport.
        /// </summary>
        /// <param name="year">year.</param>
        /// <param name="season">season.</param>
        /// <param name="scheme">scheme.</param>
        /// <param name="activity">activity.</param>
        /// <param name="status">status.</param>
        /// <param name="district">district.</param>
        /// <param name="block">block.</param>
        /// <param name="panchayat">panchayat.</param>
        /// <param name="crop_type">crop_type.</param>
        /// <returns>list.</returns>
        public List<string> GetSeedPerformanceDataReport(int year, int season, string scheme, string activity, string status, string district, string block, string panchayat, string crop_type)
        {
            List<string> response = new List<string>();

            DataSet dataSet = new DataSet();
            List<DbParameter> parameters = new List<DbParameter>();
            parameters.Add(new SqlParameter { ParameterName = "@year", Value = year, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@season_id", Value = season.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@Crop_activity", Value = activity, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@scheme_name", Value = scheme, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@approval_status", Value = status, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@District_id", Value = district, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@block_id", Value = block, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@panchayat_id", Value = panchayat, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@crop_type", Value = crop_type, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dataSet = SqlHelper.ExecuteDataSet<SqlConnection>("usp_seed_performance_report_data", parameters, SqlHelper.ExecutionType.Procedure);
            if (dataSet != null && dataSet.Tables.Count > 0)
            {
                for (int i = 0; i < dataSet.Tables.Count; i++)
                {
                    string seedPerTbl = this.DataTableToJsonObj(dataSet.Tables[i]);
                    response.Add(seedPerTbl);
                }

                return response;
            }

            return null;
        }

        /// <summary>
        /// To Get Combine Pass Data Report.
        /// </summary>
        /// <param name="combinePassModel">combinePassModel.</param>
        /// <returns>list of string.</returns>
        public List<string> GetCombinePassDataReport(CombinePassInputModel combinePassModel)
        {
            List<string> response = new List<string>();
            List<DbParameter> parameters = new List<DbParameter>();
            parameters.Add(new SqlParameter { ParameterName = "@season_id", Value = combinePassModel.Season, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@district_id", Value = combinePassModel.District, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@block_id", Value = combinePassModel.Block, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@panchayat_id", Value = combinePassModel.Panchayat, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@status", Value = combinePassModel.Template, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            DataSet dataSet = SqlHelper.ExecuteDataSet<SqlConnection>("usp_Combine_Pass_data_report", parameters, SqlHelper.ExecutionType.Procedure);
            if (dataSet != null && dataSet.Tables.Count > 0)
            {
                string crpdmgmdl = string.Empty;
                bool isMastertableloaded = false;
                if (dataSet.Tables[0] != null && dataSet.Tables[0].Rows.Count > 0)
                {
                    crpdmgmdl = this.DataTableToJsonObj(dataSet.Tables[0]);
                    response.Add(crpdmgmdl);
                    isMastertableloaded = true;
                }

                if (dataSet.Tables[1] != null && dataSet.Tables[1].Rows.Count > 0)
                {
                    crpdmgmdl = this.DataTableToJsonObj(dataSet.Tables[1]);
                    response.Add(crpdmgmdl);
                }

                if (dataSet.Tables[2] != null && dataSet.Tables[2].Rows.Count > 0 && isMastertableloaded)
                {
                    crpdmgmdl = this.DataTableToJsonObj(dataSet.Tables[2]);
                    response.Add(crpdmgmdl);
                }

                return response;
            }

            return null;
        }

        /// <summary>
        /// Get AgriAsst Data Report.
        /// </summary>
        /// <param name="agriasstmodel">agriasstmodel.</param>
        /// <returns>list of string.</returns>
        public List<string> GetAgriAsstDataReport(AgricultureAssetManagement agriasstmodel)
        {
            List<string> response = new List<string>();

            DataSet dataSet = new DataSet();
            List<DbParameter> parameters = new List<DbParameter>();
            parameters.Add(new SqlParameter { ParameterName = "@crop_activity", Value = agriasstmodel.SelectedActivity, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@machinery_id", Value = agriasstmodel.SelectedMachines, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@Structure_id", Value = agriasstmodel.SelectedStructures, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@district_id", Value = agriasstmodel.SelectedDistrict, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@block_id", Value = agriasstmodel.SelectedBlocks, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@panchayat_id", Value = agriasstmodel.SelectedPanchayats, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@approval_status", Value = agriasstmodel.SelectedStatus, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@Template", Value = agriasstmodel.SelectedTemplate, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dataSet = SqlHelper.ExecuteDataSet<SqlConnection>("usp_agri_asset_report_data", parameters, SqlHelper.ExecutionType.Procedure);
            if (dataSet != null && dataSet.Tables.Count > 0)
            {
                string crpdmgmdl = this.DataTableToJsonObj(dataSet.Tables[0]);
                response.Add(crpdmgmdl);

                return response;
            }

            return null;
        }

        /// <summary>
        /// GetPiasDataReport.
        /// </summary>
        /// <param name="piasDataReportInputModel"></param>
        /// <returns>list of string.</returns>
        public List<string> GetPiasDataReport(GetPiasDataReportInputModel piasDataReportInputModel)
        {
            List<string> response = new List<string>();
            DataSet dataSet = new DataSet();
            using (SqlConnection sqlcon = new SqlConnection(this.options.Value.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("usp_PAIS_report_data", sqlcon))
                {
                    cmd.Parameters.Add(new SqlParameter { ParameterName = "@report_type", Value = piasDataReportInputModel.Report_Type, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                    cmd.Parameters.Add(new SqlParameter { ParameterName = "@district_id", Value = piasDataReportInputModel.District_ID, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                    cmd.Parameters.Add(new SqlParameter { ParameterName = "@mkt_id", Value = piasDataReportInputModel.Market_Id, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                    cmd.Parameters.Add(new SqlParameter { ParameterName = "@cmdt_grp_id", Value = piasDataReportInputModel.Comodity_Group_Id, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                    cmd.Parameters.Add(new SqlParameter { ParameterName = "@cmdt_id", Value = piasDataReportInputModel.Comodity_Id, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                    cmd.Parameters.Add(new SqlParameter { ParameterName = "@Variety_id", Value = piasDataReportInputModel.Variety_Id, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                    cmd.Parameters.Add(new SqlParameter { ParameterName = "@Date", Value = piasDataReportInputModel.Date, SqlDbType = SqlDbType.Date, Direction = ParameterDirection.Input });
                    cmd.Parameters.Add(new SqlParameter { ParameterName = "@From_date", Value = piasDataReportInputModel.From_Date, SqlDbType = SqlDbType.Date, Direction = ParameterDirection.Input });
                    cmd.Parameters.Add(new SqlParameter { ParameterName = "@To_date", Value = piasDataReportInputModel.To_Date, SqlDbType = SqlDbType.Date, Direction = ParameterDirection.Input });
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 300;
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dataSet);
                    }
                }
            }
            if (dataSet != null && dataSet.Tables.Count > 0)
            {
                string piasTbl = DataTableToJsonObj(dataSet.Tables[0]);
                response.Add(piasTbl);

                return response;
            }
            return null;
        }

        /// <summary>
        /// PostSeedDemandAc.
        /// </summary>
        /// <param name="input">input.</param>
        /// <returns>Success or Failure Status.</returns>
        public int PostSeedDemandAc(SeedIndentInput input)
        {
            int insertRowsCount = 0;

            if (input != null && input.Crop_id != 0 && input.Panchayat_id != 0 && input.Season_id != 0 && input.UserId != 0 && input.SeedVariety != null && input.SeedVariety.Count > 0)
            {
                foreach (var seedIndentInput in input.SeedVariety)
                {
                    List<DbParameter> dbParams = new List<DbParameter>();

                    dbParams.Add(new SqlParameter { ParameterName = "@season_id", Value = input.Season_id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                    dbParams.Add(new SqlParameter { ParameterName = "@crop_id", Value = input.Crop_id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                    dbParams.Add(new SqlParameter { ParameterName = "@panchayat_id", Value = input.Panchayat_id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                    dbParams.Add(new SqlParameter { ParameterName = "@Crop_Variety_ID", Value = seedIndentInput.Crop_variety_id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                    dbParams.Add(new SqlParameter { ParameterName = "@seed_variety", Value = seedIndentInput.Seed_variety, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                    dbParams.Add(new SqlParameter { ParameterName = "@seed_demand", Value = seedIndentInput.Seed_used_qty, SqlDbType = SqlDbType.Decimal, Direction = ParameterDirection.Input });
                    dbParams.Add(new SqlParameter { ParameterName = "@user_id", Value = input.UserId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                    dbParams.Add(new SqlParameter { ParameterName = "@district_id", Value = input.District_id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });

                    Dictionary<string, dynamic> result = SqlHelper.ExecuteNonQuery<SqlConnection>(spInsertSeedIndentInput, dbParams, SqlHelper.ExecutionType.Procedure);

                    insertRowsCount += result["RowsAffected"];
                }

                return insertRowsCount;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// GetSeedDemandACViewSubmission.
        /// </summary>
        /// <param name="seasonId">seasonId.</param>
        /// <param name="cropId">cropId.</param>
        /// <param name="districtId">district_id.</param>
        /// <param name="panchayatId">panchayatId.</param>
        /// <returns>List Values.</returns>
        public DtoSeedIndentInput GetSeedDemandACViewSubmission(int seasonId, int cropId, int districtId, int panchayatId)
        {
            DtoSeedIndentInput list = null;

            List<DbParameter> parameters = new List<DbParameter>();

            parameters.Add(new SqlParameter { ParameterName = "@query", Value = qnGetSeedIndentAcIputViewSubmission, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@season_id", Value = seasonId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@crop_id", Value = cropId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@District_id", Value = districtId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@panchayat_id", Value = panchayatId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });

            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(spGetSeedIndentIputViewSubmission, parameters, SqlHelper.ExecutionType.Procedure);
            if (dt != null && dt.Rows.Count > 0)
            {
                list = SqlHelper.ConvertDataTableToList<DtoSeedIndentInput>(dt)[0];

                list.Varieties = SqlHelper.ConvertDataTableToList<DtoSeedVarietyIndent>(dt).GroupBy(x => new { x.Seed_variety, x.Crop_variety_id, x.Seed_indent_id, x.Seed_demand, x.Rec_created_date, x.Rec_created_userid, x.Rec_updated_date, x.Rec_updated_userid }).Select(x => x.First()).ToList();
            }

            return list;
        }

        /// <summary>
        /// GetSeedDemandAcViewSubmissionOffline.
        /// </summary>
        /// <param name="districtId">district_id.</param>
        /// <param name="panchayatId">panchayatId.</param>
        /// <returns>List Values.</returns>
        public List<DtoSeedIndentInput> GetSeedDemandAcViewSubmissionOffline(int districtId, int panchayatId)
        {
            List<DtoSeedIndentInput> list = new List<DtoSeedIndentInput>();

            List<DbParameter> parameters = new List<DbParameter>();

            parameters.Add(new SqlParameter { ParameterName = "@query", Value = qnGetSeedIndentAcIputViewSubmissionOffline, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@District_id", Value = districtId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@panchayat_id", Value = panchayatId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });

            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(spGetSeedIndentIputViewSubmission, parameters, SqlHelper.ExecutionType.Procedure);
            if (dt != null && dt.Rows.Count > 0)
            {
                list = SqlHelper.ConvertDataTableToList<DtoSeedIndentInput>(dt).GroupBy(x => new { x.Season_id, x.Season_name, x.Crop_id, x.Crop_name, x.Panchayat_id, x.Panchayat_name }).Select(x => x.First()).ToList();

                List<DtoSeedVarietyIndentInput> listDTO = SqlHelper.ConvertDataTableToList<DtoSeedVarietyIndentInput>(dt).GroupBy(x => new { x.Season_id, x.Crop_id, x.Seed_variety, x.Crop_variety_id, x.Seed_indent_id, x.Seed_demand, x.Rec_created_date, x.Rec_created_userid, x.Rec_updated_date, x.Rec_updated_userid }).Select(x => x.First()).ToList();

                foreach (var item in list)
                {
                    item.Varieties = listDTO.Where(x => x.Season_id == item.Season_id && x.Crop_id == item.Crop_id).GroupBy(x => new { x.Seed_variety, x.Crop_variety_id, x.Seed_indent_id, x.Seed_demand, x.Rec_created_date, x.Rec_created_userid, x.Rec_updated_date, x.Rec_updated_userid }).Select(x => x.First()).Select(x => new DtoSeedVarietyIndent { Seed_variety = x.Seed_variety, Crop_variety_id = x.Crop_variety_id, Seed_indent_id = x.Seed_indent_id, Seed_demand = x.Seed_demand, Rec_created_date = x.Rec_created_date, Rec_created_userid = x.Rec_created_userid, Rec_updated_date = x.Rec_updated_date, Rec_updated_userid = x.Rec_updated_userid }).ToList();
                }
            }

            return list;
        }

        /// <summary>
        /// GetSeedUsedVarietynameAC.
        /// </summary>
        /// <param name="seasonId">seasonId.</param>
        /// <param name="cropId">cropId.</param>
        /// <param name="districtId">districtId.</param>
        /// <param name="panchayatId">panchayatId.</param>
        /// <returns>Values.</returns>
        public DtoSeedIndentInput GetSeedUsedVarietynameAC(int seasonId, int cropId, int districtId, int panchayatId)
        {
            DtoSeedIndentInput list = null;

            List<DbParameter> parameters = new List<DbParameter>();

            parameters.Add(new SqlParameter { ParameterName = "@query", Value = qnGetSeedUsedVarietynameAC, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@season_id", Value = seasonId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@crop_id", Value = cropId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@District_id", Value = districtId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@panchayat_id", Value = panchayatId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });

            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(spGetSeedIndentIputViewSubmission, parameters, SqlHelper.ExecutionType.Procedure);
            if (dt != null && dt.Rows.Count > 0)
            {
                list = SqlHelper.ConvertDataTableToList<DtoSeedIndentInput>(dt)[0];

                list.Varieties = SqlHelper.ConvertDataTableToList<DtoSeedVarietyIndent>(dt).GroupBy(x => new { x.Seed_variety, x.Crop_variety_id, x.Seed_indent_id, x.Seed_demand, x.Rec_created_date, x.Rec_created_userid, x.Rec_updated_date, x.Rec_updated_userid }).Select(x => x.First()).ToList();
            }

            return list;
        }

        /// <summary>
        /// GetAllMarketByDistrict.
        /// </summary>
        /// <param name="districtId">districtId.</param>
        /// <returns>Values.</returns>
        public List<MarketData> GetAllMarketByDistrict(string districtId)
        {
            List<MarketData> list = new List<MarketData>();

            List<DbParameter> parameters = new List<DbParameter>();

            parameters.Add(new SqlParameter { ParameterName = "@query_name", Value = qnGetAllMarketByDistrict, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@district_id", Value = districtId, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });

            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(sp_usp_getdata_PAIS_local_preferences, parameters, SqlHelper.ExecutionType.Procedure);
            if (dt != null && dt.Rows.Count > 0)
            {
                list = SqlHelper.ConvertDataTableToList<MarketData>(dt);
            }

            return list;
        }

        /// <summary>
        /// PostPlantIndent.
        /// </summary>
        /// <param name="input">input.</param>
        /// <returns>Success or Failure Status.</returns>
        public int PostPlantIndent(PlantIndentInput input)
        {
            int insertRowsCount = 0;

            if (input != null && input.Crop_id != 0 && input.Panchayat_id != 0 && input.Block_id != 0 && input.Season_id != 0 && input.UserId != 0 && input.SeedVariety != null && input.SeedVariety.Count > 0)
            {
                foreach (var plantIndentInput in input.SeedVariety)
                {
                    List<DbParameter> dbParams = new List<DbParameter>();

                    dbParams.Add(new SqlParameter { ParameterName = "@season_id", Value = input.Season_id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                    dbParams.Add(new SqlParameter { ParameterName = "@crop_id", Value = input.Crop_id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                    dbParams.Add(new SqlParameter { ParameterName = "@block_id", Value = input.Block_id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                    dbParams.Add(new SqlParameter { ParameterName = "@panchayat_id", Value = input.Panchayat_id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                    dbParams.Add(new SqlParameter { ParameterName = "@Crop_Variety_ID", Value = plantIndentInput.Crop_variety_id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                    dbParams.Add(new SqlParameter { ParameterName = "@seed_variety", Value = plantIndentInput.Seed_variety, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                    dbParams.Add(new SqlParameter { ParameterName = "@crop_category", Value = plantIndentInput.Crop_category, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                    dbParams.Add(new SqlParameter { ParameterName = "@Plant_demand", Value = plantIndentInput.Plant_demand, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                    dbParams.Add(new SqlParameter { ParameterName = "@user_id", Value = input.UserId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                    dbParams.Add(new SqlParameter { ParameterName = "@district_id", Value = input.District_id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });

                    Dictionary<string, dynamic> result = SqlHelper.ExecuteNonQuery<SqlConnection>(spInsertPlantIndent, dbParams, SqlHelper.ExecutionType.Procedure);

                    insertRowsCount += result["RowsAffected"];
                }

                return insertRowsCount;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// GetSeedDemandBHOViewSubmission.
        /// </summary>
        /// <param name="seasonId">seasonId.</param>
        /// <param name="cropId">cropId.</param>
        /// <param name="blockId">blockId.</param>
        /// <param name="panchayatId">panchayatId.</param>
        /// <returns>List Values.</returns>
        public DtoPlantIndentInput GetSeedDemandBHOViewSubmission(int seasonId, int cropId, string blockId, int panchayatId)
        {
            DtoPlantIndentInput list = null;

            List<DbParameter> parameters = new List<DbParameter>();

            parameters.Add(new SqlParameter { ParameterName = "@query", Value = qnGetSeedDemandBHOViewSubmission, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@season_id", Value = seasonId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@crop_id", Value = cropId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@Block_id", Value = blockId, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@panchayat_id", Value = panchayatId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });

            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(spGetDataPlantIndent, parameters, SqlHelper.ExecutionType.Procedure);
            if (dt != null && dt.Rows.Count > 0)
            {
                list = SqlHelper.ConvertDataTableToList<DtoPlantIndentInput>(dt)[0];

                list.Varieties = SqlHelper.ConvertDataTableToList<DtoSeedVarietyPlantIndent>(dt).GroupBy(x => new { x.Seed_variety, x.Crop_variety_id, x.Plant_indent_id, x.Plant_demand, x.Crop_category, x.Rec_created_date, x.Rec_created_userid, x.Rec_updated_date, x.Rec_updated_userid }).Select(x => x.First()).ToList();
            }

            return list;
        }

        /// <summary>
        /// GetSeedDemandBHOViewSubmissionOffline.
        /// </summary>
        /// <param name="districtId">district_id.</param>
        /// <param name="seasonId">seasonId.</param>
        /// <param name="blockId">blockId.</param>
        /// <returns>List Values.</returns>
        public List<DtoPlantIndentInput> GetSeedDemandBHOViewSubmissionOffline(int districtId, int seasonId, string blockId)
        {
            List<DtoPlantIndentInput> list = new List<DtoPlantIndentInput>();

            List<DbParameter> parameters = new List<DbParameter>();

            parameters.Add(new SqlParameter { ParameterName = "@query", Value = qnGetSeedDemandBHOViewSubmissionOffline, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@district_id", Value = districtId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@season_id", Value = seasonId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@Block_id", Value = blockId, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });

            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(spGetDataPlantIndent, parameters, SqlHelper.ExecutionType.Procedure);
            if (dt != null && dt.Rows.Count > 0)
            {
                list = SqlHelper.ConvertDataTableToList<DtoPlantIndentInput>(dt).GroupBy(x => new { x.Season_id, x.Season_name, x.Crop_id, x.Crop_name, x.Panchayat_id, x.Panchayat_name, x.Block_id, x.Block_name, x.District_id }).Select(x => x.First()).ToList();

                List<DtoSeedVarietyPlantIndentInput> listDTO = SqlHelper.ConvertDataTableToList<DtoSeedVarietyPlantIndentInput>(dt).GroupBy(x => new { x.Season_id, x.Crop_id, x.Seed_variety, x.Crop_variety_id, x.Plant_indent_id, x.Plant_demand, x.Crop_category, x.Rec_created_date, x.Rec_created_userid, x.Rec_updated_date, x.Rec_updated_userid }).Select(x => x.First()).ToList();

                foreach (var item in list)
                {
                    item.Varieties = listDTO.Where(x => x.Season_id == item.Season_id && x.Crop_id == item.Crop_id).GroupBy(x => new { x.Seed_variety, x.Crop_variety_id, x.Plant_indent_id, x.Plant_demand, x.Crop_category, x.Rec_created_date, x.Rec_created_userid, x.Rec_updated_date, x.Rec_updated_userid }).Select(x => x.First()).Select(x => new DtoSeedVarietyPlantIndent { Seed_variety = x.Seed_variety, Crop_variety_id = x.Crop_variety_id, Plant_indent_id = x.Plant_indent_id, Plant_demand = x.Plant_demand, Crop_category = x.Crop_category, Rec_created_date = x.Rec_created_date, Rec_created_userid = x.Rec_created_userid, Rec_updated_date = x.Rec_updated_date, Rec_updated_userid = x.Rec_updated_userid }).ToList();
                }
            }

            return list;
        }

        /// <summary>
        /// GetSeedUsedVarietyNameBHO.
        /// </summary>
        /// <param name="seasonId">seasonId.</param>
        /// <param name="cropId">cropId.</param>
        /// <param name="districtId">districtId.</param>
        /// <param name="blockId">blockId.</param>
        /// <param name="panchayatId">panchayatId.</param>
        /// <returns>Values.</returns>
        public DtoPlantIndentInput GetSeedUsedVarietyNameBHO(int seasonId, int cropId, int districtId, string blockId, int panchayatId)
        {
            DtoPlantIndentInput list = null;

            List<DbParameter> parameters = new List<DbParameter>();

            parameters.Add(new SqlParameter { ParameterName = "@query", Value = qnGetSeedUsedVarietyNameBHO, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@season_id", Value = seasonId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@crop_id", Value = cropId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@district_id", Value = districtId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@Block_id", Value = blockId, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@panchayat_id", Value = panchayatId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });

            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(spGetDataPlantIndent, parameters, SqlHelper.ExecutionType.Procedure);
            if (dt != null && dt.Rows.Count > 0)
            {
                list = SqlHelper.ConvertDataTableToList<DtoPlantIndentInput>(dt)[0];

                list.Varieties = SqlHelper.ConvertDataTableToList<DtoSeedVarietyPlantIndent>(dt).GroupBy(x => new { x.Seed_variety, x.Crop_variety_id, x.Plant_indent_id, x.Plant_demand, x.Crop_category, x.Rec_created_date, x.Rec_created_userid, x.Rec_updated_date, x.Rec_updated_userid }).Select(x => x.First()).ToList();
            }

            return list;
        }

        /// <summary>
        /// GetFutureSeason.
        /// </summary>
        /// <returns>FutureSeason List.</returns>
        public List<FutureSeason> GetFutureSeason()
        {
            List<DbParameter> parameters = new List<DbParameter>();

            parameters.Add(new SqlParameter { ParameterName = "@query", Value = qnGetFutureSeason, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });

            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(spGetDataPlantIndent, parameters, SqlHelper.ExecutionType.Procedure);
            List<FutureSeason> list = SqlHelper.ConvertDataTableToList<FutureSeason>(dt);

            return list;
        }

        /// <summary>
        /// GetCropName.
        /// </summary>
        /// <param name="seasonId">seasonId.</param>
        /// <returns>Crop Name List.</returns>
        public List<CropNames> GetCropName(int seasonId)
        {
            List<DbParameter> parameters = new List<DbParameter>();

            parameters.Add(new SqlParameter { ParameterName = "@query", Value = qnGetCropName, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@season_id", Value = seasonId, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });

            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(spGetSeedIndentIputViewSubmission, parameters, SqlHelper.ExecutionType.Procedure);
            List<CropNames> list = SqlHelper.ConvertDataTableToList<CropNames>(dt);

            return list;
        }

        /// <summary>
        /// GetPlantName.
        /// </summary>
        /// <param name="plantCategory">plantCategory.</param>
        /// <param name="seasonId">seasonId.</param>
        /// <returns>Plant Name List.</returns>
        public List<PlantNames> GetPlantName(string plantCategory, int seasonId)
        {
            List<DbParameter> parameters = new List<DbParameter>();

            parameters.Add(new SqlParameter { ParameterName = "@query", Value = qnGetPlantName, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@crop_category", Value = plantCategory, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@season_id", Value = seasonId, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });

            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(spGetDataPlantIndent, parameters, SqlHelper.ExecutionType.Procedure);
            List<PlantNames> list = SqlHelper.ConvertDataTableToList<PlantNames>(dt);

            return list;
        }

        /// <summary>
        /// GetCropSeedVariety.
        /// </summary>
        /// <param name="cropId">cropId.</param>
        /// <param name="seasonId">seasonId.</param>
        /// <returns>Seed Variety List.</returns>
        public List<SeedVarietyList> GetCropSeedVariety(int cropId, int seasonId)
        {
            List<DbParameter> parameters = new List<DbParameter>();

            parameters.Add(new SqlParameter { ParameterName = "@query", Value = qnGetCropSeedVariety, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@crop_id", Value = cropId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@season_id", Value = seasonId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });

            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(spGetSeedIndentIputViewSubmission, parameters, SqlHelper.ExecutionType.Procedure);
            List<SeedVarietyList> list = SqlHelper.ConvertDataTableToList<SeedVarietyList>(dt);

            return list;
        }

        /// <summary>
        /// GetPlantSeedVariety.
        /// </summary>
        /// <param name="cropId">cropId.</param>
        /// <param name="seasonId">seasonId.</param>
        /// <returns>Seed Variety List.</returns>
        public List<SeedVarietyList> GetPlantSeedVariety(int cropId, int seasonId)
        {
            List<DbParameter> parameters = new List<DbParameter>();

            parameters.Add(new SqlParameter { ParameterName = "@query", Value = qnGetPlantSeedVariety, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@crop_id", Value = cropId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@season_id", Value = seasonId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });

            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(spGetDataPlantIndent, parameters, SqlHelper.ExecutionType.Procedure);
            List<SeedVarietyList> list = SqlHelper.ConvertDataTableToList<SeedVarietyList>(dt);

            return list;
        }

        /// <summary>
        /// GetCropCategoryBySeason.
        /// </summary>
        /// <param name="seasonId">seasonId.</param>
        /// <returns>Crop Category List.</returns>
        public List<CropCategoryEntity> GetCropCategoryBySeason(int seasonId)
        {
            List<DbParameter> parameters = new List<DbParameter>();

            parameters.Add(new SqlParameter { ParameterName = "@query", Value = qnGetCropCategoryBySeason, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@season_id", Value = seasonId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });

            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(spGetDataPlantIndent, parameters, SqlHelper.ExecutionType.Procedure);
            List<CropCategoryEntity> list = SqlHelper.ConvertDataTableToList<CropCategoryEntity>(dt);

            return list;
        }

        /// <summary>
        /// GetInputIndentDataReport.
        /// </summary>
        /// <param name="year">year.</param>
        /// <param name="seasonId">season.</param>
        /// <param name="activity">activity.</param>
        /// <param name="cropVarietyId">cropVarietyId.</param>
        /// <param name="plantCategory">plantCategory.</param>
        /// <param name="cropId">cropId.</param>
        /// <param name="district">district.</param>
        /// <param name="block">block.</param>
        /// <param name="panchayat">panchayat.</param>
        /// <returns>list.</returns>
        public List<string> GetInputIndentDataReport(int year, string seasonId, string activity, string cropVarietyId, string plantCategory, string cropId, string district, string block, string panchayat)
        {
            List<string> response = new List<string>();
            List<DbParameter> parameters = new List<DbParameter>();
            parameters.Add(new SqlParameter { ParameterName = "@Year", Value = year, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@season_id", Value = seasonId, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@activity", Value = activity, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@crop_variety_id", Value = cropVarietyId, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@plant_category", Value = activity.Equals("Seed Demand") ? DBNull.Value : plantCategory, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@Crop_ID", Value = cropId, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@District_ID", Value = district, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@Block_ID", Value = block, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@Panchayat_ID", Value = panchayat, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            DataSet dataSet = SqlHelper.ExecuteDataSet<SqlConnection>("usp_seed_indent_plant_material_report_data", parameters, SqlHelper.ExecutionType.Procedure);
            if (dataSet != null && dataSet.Tables.Count > 0)
            {
                for (int i = 0; i < dataSet.Tables.Count; i++)
                {
                    string seedPerTbl = this.DataTableToJsonObj(dataSet.Tables[i]);
                    response.Add(seedPerTbl);
                }

                return response;
            }

            return null;
        }
    }
}
