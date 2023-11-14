//------------------------------------------------------------------------------
// <copyright file="PlantProtectionRepository.cs" company="Government of Bihar">
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
    /// PlantProtectionRepository.
    /// </summary>
    public class PlantProtectionRepository : BaseRepository, IPlantProtectionRepository
    {
        private readonly IOptions<BlobConfig> blobconfig;

        private readonly IOptions<DBSettings> options;

        private static string spGetPesticideData = "usp_getdata_pesticide";
        private static string spGetPesticidePerfData = "usp_getdata_pesticide_perf";
        private static string spGetPesticidePerfDataMonthly = "usp_getdata_pesticide_perf_monthly";
        private static string spGetPesticideSurveillanceMonthly = "usp_getdata_pest_surveillance_monthly";
        private static string spGetCombPesticideData = "usp_getdata_comb_pesticide";
        private static string spGetPesticideCombPerfData = "usp_getdata_comb_pesticide_perf";
        private static string spGetPesticideCombPerfDataMonthly = "usp_getdata_comb_pesticide_perf_monthly";
        private static string spInsertPesticidePerf = "usp_merge_pesticide_perf_mm";
        private static string spInsertPesticidePerfComb = "usp_merge_comb_pesticide_perf_mm";
        private static string spInsertPestSurveillance = "usp_merge_pest_surveillance_mm";
        private static string spGetPesticideSurveillanceDisease = "usp_getdata_pest_surveillance_Perf";
        private static string qngetPestSurveillanceDisease = "getPestSurveillanceDisease";
        private static string qnGetApprovedAreaCoverage = "GetApprovedAreaCoverage";
        private static string qnGetStageOfCrop = "GetStageOfCrop";
        private static string qngetdataPestSurveillancePerf = "getdataPestSurveillancePerf";
        private static string spGetPesticidePerfOffline = "usp_getdata_pesticide_Perf_Offline";
        private static string spGetPesticidesurveillanceOffline = "usp_getdata_pest_surveillance_Offline";
        private static string spGetCombPesticidePerfOffline = "usp_getdata_comb_pesticide_perf_Offline";

        /// <summary>
        /// Initializes a new instance of the <see cref="PlantProtectionRepository"/> class.
        /// PlantProtectionRepository.
        /// </summary>
        /// <param name="config">config.</param>
        /// <param name="blobconfig">blobconfig.</param>
        /// <param name="options">options.</param>
        public PlantProtectionRepository(IConfiguration config, IOptions<BlobConfig> blobconfig, IOptions<DBSettings> options)
            : base(config)
        {
            this.options = options;
            SqlHelper.SetConnectionString(this.options.Value.ConnectionString);

            this.blobconfig = blobconfig;
        }

        /// <summary>
        /// GetPesticideData.
        /// </summary>
        /// <returns>List vsalues.</returns>
        public List<PesticideModel> GetPesticideData()
        {
            List<PesticideModel> resultset = new List<PesticideModel>();
            List<DbParameter> dbparams = new List<DbParameter>();

            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(spGetPesticideData, dbparams, SqlHelper.ExecutionType.Procedure);

            if (dt != null && dt.Rows.Count > 0)
            {
                resultset = SqlHelper.ConvertDataTableToList<PesticideModel>(dt);
            }

            return resultset;
        }

        /// <summary>
        /// GetPesticidePerfData.
        /// </summary>
        /// <param name="district_Id">district_Id.</param>
        /// <param name="pesticide_Id">pesticide_Id.</param>
        /// <param name="formulation_Id">formulation_Id.</param>
        /// <returns>List Values.</returns>
        public List<PesticidePerf> GetPesticidePerfData(string district_Id, string pesticide_Id, string formulation_Id)
        {
            List<PesticidePerf> resultset = new List<PesticidePerf>();
            List<DbParameter> dbparams = new List<DbParameter>();

            dbparams.Add(new SqlParameter { ParameterName = "@District_Id", Value = district_Id, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@Pesticide_Id", Value = pesticide_Id, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@Formulation_Id", Value = formulation_Id, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });

            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(spGetPesticidePerfData, dbparams, SqlHelper.ExecutionType.Procedure);

            if (dt != null && dt.Rows.Count > 0)
            {
                resultset = SqlHelper.ConvertDataTableToList<PesticidePerf>(dt);
            }

            return resultset;
        }

        /// <summary>
        /// GetPesticidePerfDataMonthly.
        /// </summary>
        /// <param name="district_Id">district_Id.</param>
        /// <param name="mm_year">mm_year.</param>
        /// <returns>List Values.</returns>
        public List<PesticidePerf> GetPesticidePerfDataMonthly(string district_Id, string mm_year)
        {
            List<PesticidePerf> resultset = new List<PesticidePerf>();
            List<DbParameter> dbparams = new List<DbParameter>();

            dbparams.Add(new SqlParameter { ParameterName = "@district_id", Value = district_Id, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@mm_year", Value = mm_year, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(spGetPesticidePerfDataMonthly, dbparams, SqlHelper.ExecutionType.Procedure);

            if (dt != null && dt.Rows.Count > 0)
            {
                resultset = SqlHelper.ConvertDataTableToList<PesticidePerf>(dt);
            }

            return resultset;
        }

        /// <summary>
        /// GetPesticideSurveillanceMonthly.
        /// </summary>
        /// <param name="district_Id">district_Id.</param>
        /// <param name="mm_year">mm_year.</param>
        /// <returns>List Values.</returns>
        public PestSurviellanceDisease GetPesticideSurveillanceMonthly(string district_Id, string mm_year)
        {
            PestSurviellanceDisease pestSurviellanceDisease = new PestSurviellanceDisease();
            List<DbParameter> dbparams = new List<DbParameter>();

            dbparams.Add(new SqlParameter { ParameterName = "@district_id", Value = district_Id, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@mm_year", Value = mm_year, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(spGetPesticideSurveillanceMonthly, dbparams, SqlHelper.ExecutionType.Procedure);

            if (dt != null && dt.Rows.Count > 0)
            {
                List<PestSurviellance> resultset = SqlHelper.ConvertDataTableToList<PestSurviellance>(dt);

                pestSurviellanceDisease.Mm_year = resultset[0].Mm_year;
                pestSurviellanceDisease.District_id = resultset[0].District_id;

                int[] cropId = resultset.Select(x => x.Crop_id).Distinct().ToArray();

                List<CropList> cropLists = new List<CropList>();
                for (int i = 0; i <= cropId.Length - 1; i++)
                {
                    List<DiseaseList> diseaseList = new List<DiseaseList>();
                    int[] cropIds = resultset.Select(x => x.Crop_id).Distinct().ToArray();
                    int rowIndexNumber = 0;
                    List<PestSurviellance> pestSurviellances = resultset.Where(x => x.Crop_id == cropIds[i]).ToList();
                    pestSurviellances.ToList().ForEach(x =>
                    {
                        rowIndexNumber++;
                        x.SlNo = rowIndexNumber;
                    });

                    CropList cropList = new CropList();
                    cropList.Crop_id = pestSurviellances[0].Crop_id;
                    cropList.Crop_name = pestSurviellances[0].Crop_name;
                    cropList.Crop_category = pestSurviellances[0].Crop_category;

                    for (int j = 0; j <= pestSurviellances.Count - 1; j++)
                    {
                        DiseaseList disease = new DiseaseList();
                        PestSurviellance pestSurv = pestSurviellances.Where(x => x.SlNo == j + 1).SingleOrDefault();
                        disease.Season_id = pestSurv.Season_id;
                        disease.Crop_state = pestSurv.Crop_state;
                        disease.Covered_area = pestSurv.Covered_area;
                        disease.Disease_name = pestSurv.Disease_name;
                        disease.Affected_area = pestSurv.Affected_area;
                        disease.Treated_area = pestSurv.Treated_area;
                        disease.Incedence_percent = pestSurv.Incedence_percent;
                        disease.Insect_intensity = pestSurv.Insect_intensity;
                        disease.Submitted_date = pestSurv.Submitted_date;
                        disease.Submitted_by = pestSurv.Submitted_by;
                        disease.Technique_used = pestSurv.Technique_used;
                        diseaseList.Add(disease);
                    }

                    cropList.DiseaseList = diseaseList;
                    cropLists.Add(cropList);
                }

                pestSurviellanceDisease.CropList = cropLists;
            }

            return pestSurviellanceDisease;
        }

        /// <summary>
        /// GetCombPesticideData.
        /// </summary>
        /// <returns>List Values.</returns>
        public List<CombPesticide> GetCombPesticideData()
        {
            List<CombPesticide> resultset = new List<CombPesticide>();
            List<DbParameter> dbparams = new List<DbParameter>();

            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(spGetCombPesticideData, dbparams, SqlHelper.ExecutionType.Procedure);

            if (dt != null && dt.Rows.Count > 0)
            {
                resultset = SqlHelper.ConvertDataTableToList<CombPesticide>(dt);
            }

            return resultset;
        }

        /// <summary>
        /// GetPesticideCombPerfData.
        /// </summary>
        /// <param name="district_Id">district_Id.</param>
        /// <param name="comb_Pesticide_Id">comb_Pesticide_Id.</param>
        /// <returns>List Values.</returns>
        public List<PesticideCombPerf> GetPesticideCombPerfData(string district_Id, string comb_Pesticide_Id)
        {
            List<PesticideCombPerf> resultset = new List<PesticideCombPerf>();
            List<DbParameter> dbparams = new List<DbParameter>();

            dbparams.Add(new SqlParameter { ParameterName = "@district_id", Value = district_Id, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@Comb_Pesticide_Id", Value = comb_Pesticide_Id, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(spGetPesticideCombPerfData, dbparams, SqlHelper.ExecutionType.Procedure);

            if (dt != null && dt.Rows.Count > 0)
            {
                resultset = SqlHelper.ConvertDataTableToList<PesticideCombPerf>(dt);
            }

            return resultset;
        }

        /// <summary>
        /// GetPesticideCombPerfDataMonthly.
        /// </summary>
        /// <param name="district_Id">district_Id.</param>
        /// <param name="mm_year">mm_year.</param>
        /// <returns>List Values.</returns>
        public List<PesticideCombPerf> GetPesticideCombPerfDataMonthly(string district_Id, string mm_year)
        {
            List<PesticideCombPerf> resultset = new List<PesticideCombPerf>();
            List<DbParameter> dbparams = new List<DbParameter>();

            dbparams.Add(new SqlParameter { ParameterName = "@district_id", Value = district_Id, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@mm_year", Value = mm_year, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(spGetPesticideCombPerfDataMonthly, dbparams, SqlHelper.ExecutionType.Procedure);

            if (dt != null && dt.Rows.Count > 0)
            {
                resultset = SqlHelper.ConvertDataTableToList<PesticideCombPerf>(dt);
            }

            return resultset;
        }

        /// <summary>
        /// InsertPesticidePerf.
        /// </summary>
        /// <param name="pesticidePerf">pesticidePerf.</param>
        /// <returns>Response and Status.</returns>
        public int InsertPesticidePerf(DtoPesticideperf pesticidePerf)
        {
            if (pesticidePerf != null && pesticidePerf.Pesticide_id != 0 && pesticidePerf.Formulation_id != 0 && pesticidePerf.District_id != 0 && pesticidePerf.Submitted_userid != 0 && !string.IsNullOrEmpty(pesticidePerf.Mm_year))
            {
                List<DbParameter> dbparams = new List<DbParameter>();

                dbparams.Add(new SqlParameter { ParameterName = "@submitted_date", Value = (object)pesticidePerf.Submitted_date, SqlDbType = SqlDbType.Date, Direction = ParameterDirection.Input });
                dbparams.Add(new SqlParameter { ParameterName = "@pesticide_id", Value = (object)pesticidePerf.Pesticide_id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                dbparams.Add(new SqlParameter { ParameterName = "@formulation_id", Value = (object)pesticidePerf.Formulation_id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                dbparams.Add(new SqlParameter { ParameterName = "@district_id", Value = (object)pesticidePerf.District_id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                dbparams.Add(new SqlParameter { ParameterName = "@prev_balance", Value = (object)pesticidePerf.Prev_balance, SqlDbType = SqlDbType.Decimal, Direction = ParameterDirection.Input });
                dbparams.Add(new SqlParameter { ParameterName = "@supply", Value = (object)pesticidePerf.Supply, SqlDbType = SqlDbType.Decimal, Direction = ParameterDirection.Input });
                dbparams.Add(new SqlParameter { ParameterName = "@consumption", Value = (object)pesticidePerf.Consumption, SqlDbType = SqlDbType.Decimal, Direction = ParameterDirection.Input });
                dbparams.Add(new SqlParameter { ParameterName = "@balance", Value = (object)pesticidePerf.Balance, SqlDbType = SqlDbType.Decimal, Direction = ParameterDirection.Input });
                dbparams.Add(new SqlParameter { ParameterName = "@submitted_userid", Value = (object)pesticidePerf.Submitted_userid, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                dbparams.Add(new SqlParameter { ParameterName = "@mm_year", Value = (object)pesticidePerf.Mm_year, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                Dictionary<string, dynamic> result = SqlHelper.ExecuteNonQuery<SqlConnection>(spInsertPesticidePerf, dbparams, SqlHelper.ExecutionType.Procedure);

                return 1;
                }

            return 0;
        }

        /// <summary>
        /// InsertPesticidePerfComb.
        /// </summary>
        /// <param name="pesticidePerf">pesticidePerf.</param>
        /// <returns>Insert responce and Status.</returns>
        public int InsertPesticidePerfComb(DtoPesticidePerfComb pesticidePerf)
        {
            if (pesticidePerf != null && pesticidePerf.Comb_pesticide_id != 0 && pesticidePerf.District_id != 0 && pesticidePerf.Submitted_userid != 0 && !string.IsNullOrEmpty(pesticidePerf.Mm_year))
            {
                List<DbParameter> dbparams = new List<DbParameter>();

                dbparams.Add(new SqlParameter { ParameterName = "@submitted_date", Value = (object)pesticidePerf.Submitted_date, SqlDbType = SqlDbType.Date, Direction = ParameterDirection.Input });
                dbparams.Add(new SqlParameter { ParameterName = "@comb_pesticide_id", Value = (object)pesticidePerf.Comb_pesticide_id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                dbparams.Add(new SqlParameter { ParameterName = "@district_id", Value = (object)pesticidePerf.District_id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                dbparams.Add(new SqlParameter { ParameterName = "@prev_balance", Value = (object)pesticidePerf.Prev_balance, SqlDbType = SqlDbType.Decimal, Direction = ParameterDirection.Input });
                dbparams.Add(new SqlParameter { ParameterName = "@supply", Value = (object)pesticidePerf.Supply, SqlDbType = SqlDbType.Decimal, Direction = ParameterDirection.Input });
                dbparams.Add(new SqlParameter { ParameterName = "@consumption", Value = (object)pesticidePerf.Consumption, SqlDbType = SqlDbType.Decimal, Direction = ParameterDirection.Input });
                dbparams.Add(new SqlParameter { ParameterName = "@balance", Value = (object)pesticidePerf.Balance, SqlDbType = SqlDbType.Decimal, Direction = ParameterDirection.Input });
                dbparams.Add(new SqlParameter { ParameterName = "@submitted_userid", Value = (object)pesticidePerf.Submitted_userid, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                dbparams.Add(new SqlParameter { ParameterName = "@mm_year", Value = (object)pesticidePerf.Mm_year, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                Dictionary<string, dynamic> result = SqlHelper.ExecuteNonQuery<SqlConnection>(spInsertPesticidePerfComb, dbparams, SqlHelper.ExecutionType.Procedure);

                return 1;
            }

            return 0;
        }

        /// <summary>
        /// InsertPestSurveillance.
        /// </summary>
        /// <param name="surviellance">surviellance.</param>
        /// <returns>Insert Status and Response.</returns>
        public int InsertPestSurveillance(PestSurviellance surviellance)
        {
            List<DbParameter> dbparams = new List<DbParameter>();

            dbparams.Add(new SqlParameter { ParameterName = "@submitted_date", Value = (object)surviellance.Submitted_date, SqlDbType = SqlDbType.Date, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@crop_id", Value = (object)surviellance.Crop_id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@district_id", Value = (object)surviellance.District_id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@covered_area", Value = (object)surviellance.Covered_area, SqlDbType = SqlDbType.Decimal, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@crop_state", Value = (object)surviellance.Crop_state, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@crop_state_id", Value = (object)surviellance.Crop_state_id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@disease_id", Value = surviellance.Disease_id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@affected_area", Value = (object)surviellance.Affected_area, SqlDbType = SqlDbType.Decimal, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@treated_area", Value = (object)surviellance.Treated_area, SqlDbType = SqlDbType.Decimal, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@incedence_percent", Value = (object)surviellance.Incedence_percent, SqlDbType = SqlDbType.Decimal, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@insect_intensity", Value = (object)surviellance.Insect_intensity, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@season_id", Value = (object)surviellance.Season_id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@technique_used", Value = (object)surviellance.Technique_used, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@submitted_by", Value = (object)surviellance.Submitted_by, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@mm_year", Value = (object)surviellance.Mm_year, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            Dictionary<string, dynamic> result = SqlHelper.ExecuteNonQuery<SqlConnection>(spInsertPestSurveillance, dbparams, SqlHelper.ExecutionType.Procedure);

            return 1;
        }

        /// <summary>
        /// GetPesticideSurveillanceDisease.
        /// </summary>
        /// <returns>List Values.</returns>
        public List<PestSurveillanceDisease> GetPesticideSurveillanceDisease()
        {
            List<PestSurveillanceDisease> result = new List<PestSurveillanceDisease>();
            List<DbParameter> dbparams = new List<DbParameter>();
            dbparams.Add(new SqlParameter { ParameterName = "@query_name", Value = qngetPestSurveillanceDisease, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@District_Id", Value = String.Empty, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@Crop_id", Value = String.Empty, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@season_id", Value = String.Empty, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });

            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(spGetPesticideSurveillanceDisease, dbparams, SqlHelper.ExecutionType.Procedure);
            if (dt != null && dt.Rows.Count > 0)
            {
                result = SqlHelper.ConvertDataTableToList<PestSurveillanceDisease>(dt);
            }

            return result;
        }

        /// <summary>
        /// GetApprovedAreaCoverage.
        /// </summary>
        /// <param name="district_Id">district_Id.</param>
        /// <param name="crop_Id">crop_Id.</param>
        /// <param name="season_Id">season_Id.</param>
        /// <returns>List Details.</returns>
        public List<ApprovedAreaCoverageRes> GetApprovedAreaCoverage(string district_Id, string crop_Id, string season_Id)
        {
            List<ApprovedAreaCoverageRes> result = new List<ApprovedAreaCoverageRes>();
            List<DbParameter> dbparams = new List<DbParameter>();
            dbparams.Add(new SqlParameter { ParameterName = "@query_name", Value = qnGetApprovedAreaCoverage, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@District_Id", Value = district_Id, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@Crop_id", Value = crop_Id, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@season_id", Value = season_Id, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });

            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(spGetPesticideSurveillanceDisease, dbparams, SqlHelper.ExecutionType.Procedure);
            if (dt != null && dt.Rows.Count > 0)
            {
                result = SqlHelper.ConvertDataTableToList<ApprovedAreaCoverageRes>(dt);
            }

            return result;
        }

        /// <summary>
        /// GetCropStage.
        /// </summary>
        /// <returns>List Details.</returns>
        public List<CropStageName> GetCropStage()
        {
            List<CropStage> result = new List<CropStage>();
            List<CropStageName> res = new List<CropStageName>();
            List<DbParameter> dbparams = new List<DbParameter>();
            dbparams.Add(new SqlParameter { ParameterName = "@query_name", Value = qnGetStageOfCrop, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@District_Id", Value = String.Empty, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@Crop_id", Value = String.Empty, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@season_id", Value = String.Empty, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });

            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(spGetPesticideSurveillanceDisease, dbparams, SqlHelper.ExecutionType.Procedure);
            if (dt != null && dt.Rows.Count > 0)
            {
                result = SqlHelper.ConvertDataTableToList<CropStage>(dt);
            }

            foreach (var item in result)
            {
                CropStageName cropStageName = new CropStageName();
                cropStageName.Stage_Name = item.Crop_state;
                int crop_state_id = item.Crop_state_id;
                cropStageName.State_id = crop_state_id;
                res.Add(cropStageName);
            }

            return res;
        }

        /// <summary>
        /// GetPesticideSurveillancePerfData.
        /// </summary>
        /// <param name="district_Id">district_Id.</param>
        /// <param name="crop_Id">crop_Id.</param>
        /// <returns>List Values.</returns>
        public List<PestSurveillancePerf> GetPesticideSurveillancePerfData(string district_Id, string crop_Id)
        {
            List<PestSurveillancePerf> result = new List<PestSurveillancePerf>();
            List<DbParameter> dbparams = new List<DbParameter>();
            dbparams.Add(new SqlParameter { ParameterName = "@query_name", Value = qngetdataPestSurveillancePerf, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@District_Id", Value = district_Id, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@Crop_id", Value = crop_Id, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@season_id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });

            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(spGetPesticideSurveillanceDisease, dbparams, SqlHelper.ExecutionType.Procedure);
            if (dt != null && dt.Rows.Count > 0)
            {
                result = SqlHelper.ConvertDataTableToList<PestSurveillancePerf>(dt);
            }

            return result;
        }

        /// <summary>
        /// GetPesticidePerfOffline.
        /// </summary>
        /// <param name="district_Id">district_Id.</param>
        /// <returns>List Values.</returns>
        public List<PesticidePerfOffline> GetPesticidePerfOffline(string district_Id)
        {
            List<PesticidePerfOffline> result = new List<PesticidePerfOffline>();
            List<DbParameter> dbparams = new List<DbParameter>();
            dbparams.Add(new SqlParameter { ParameterName = "@District_Id", Value = district_Id, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });

            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(spGetPesticidePerfOffline, dbparams, SqlHelper.ExecutionType.Procedure);
            if (dt != null && dt.Rows.Count > 0)
            {
                result = SqlHelper.ConvertDataTableToList<PesticidePerfOffline>(dt);
            }

            return result;
        }

        /// <summary>
        /// GetPesticidesurveillanceOffline.
        /// </summary>
        /// <param name="district_Id">district_Id.</param>
        /// <returns>List Results.</returns>
        public List<PestSurviellanceDisease> GetPesticidesurveillanceOffline(string district_Id)
        {
            List<PestSurviellanceDisease> pestSurviellanceDiseaseList = new List<PestSurviellanceDisease>();
            List<DbParameter> dbparams = new List<DbParameter>();
            dbparams.Add(new SqlParameter { ParameterName = "@District_Id", Value = district_Id, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });

            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(spGetPesticidesurveillanceOffline, dbparams, SqlHelper.ExecutionType.Procedure);
            if (dt != null && dt.Rows.Count > 0)
            {
                List<PestSurviellance> resultset = SqlHelper.ConvertDataTableToList<PestSurviellance>(dt);
                string[] yearList = resultset.Select(x => x.Mm_year).Distinct().ToArray();

                for (int year = 0; year <= yearList.Length - 1; year++)
                {
                    PestSurviellanceDisease pestSurviellanceDisease = new PestSurviellanceDisease();
                    pestSurviellanceDisease.Mm_year = yearList[year];
                    pestSurviellanceDisease.District_id = resultset[0].District_id;

                    int[] cropId = resultset.Where(x => x.Mm_year == yearList[year]).Select(x => x.Crop_id).Distinct().ToArray();

                    List<CropList> cropLists = new List<CropList>();
                    for (int i = 0; i <= cropId.Length - 1; i++)
                    {
                        List<DiseaseList> diseaseList = new List<DiseaseList>();
                        int[] cropIds = resultset.Where(x => x.Mm_year == yearList[year]).Select(x => x.Crop_id).Distinct().ToArray();
                        int rowIndexNumber = 0;
                        List<PestSurviellance> pestSurviellances = resultset.Where(x => x.Crop_id == cropIds[i] && x.Mm_year == yearList[year]).ToList();
                        pestSurviellances.ToList().ForEach(x =>
                        {
                            rowIndexNumber++;
                            x.SlNo = rowIndexNumber;
                        });

                        CropList cropList = new CropList();
                        cropList.Crop_id = pestSurviellances[0].Crop_id;
                        cropList.Crop_name = pestSurviellances[0].Crop_name;
                        cropList.Crop_category = pestSurviellances[0].Crop_category;

                        for (int j = 0; j <= pestSurviellances.Count - 1; j++)
                        {
                            DiseaseList disease = new DiseaseList();
                            PestSurviellance pestSurv = pestSurviellances.Where(x => x.SlNo == j + 1).SingleOrDefault();
                            disease.Season_id = pestSurv.Season_id;
                            disease.Crop_state = pestSurv.Crop_state;
                            disease.Covered_area = pestSurv.Covered_area;
                            disease.Disease_name = pestSurv.Disease_name;
                            disease.Affected_area = pestSurv.Affected_area;
                            disease.Treated_area = pestSurv.Treated_area;
                            disease.Incedence_percent = pestSurv.Incedence_percent;
                            disease.Insect_intensity = pestSurv.Insect_intensity;
                            disease.Submitted_date = pestSurv.Submitted_date;
                            disease.Submitted_by = pestSurv.Submitted_by;
                            disease.Technique_used = pestSurv.Technique_used;
                            diseaseList.Add(disease);
                        }

                        cropList.DiseaseList = diseaseList;
                        cropLists.Add(cropList);

                        pestSurviellanceDisease.CropList = cropLists;
                    }

                    pestSurviellanceDiseaseList.Add(pestSurviellanceDisease);
                }
            }

            return pestSurviellanceDiseaseList;
        }

        /// <summary>
        /// GetCombPesticidePerfOffline.
        /// </summary>
        /// <param name="district_Id">district_Id.</param>
        /// <returns>List Details.</returns>
        public List<CombPesticidePerfOffline> GetCombPesticidePerfOffline(string district_Id)
        {
            List<CombPesticidePerfOffline> result = new List<CombPesticidePerfOffline>();
            List<DbParameter> dbparams = new List<DbParameter>();
            dbparams.Add(new SqlParameter { ParameterName = "@District_Id", Value = district_Id, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });

            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(spGetCombPesticidePerfOffline, dbparams, SqlHelper.ExecutionType.Procedure);
            if (dt != null && dt.Rows.Count > 0)
            {
                result = SqlHelper.ConvertDataTableToList<CombPesticidePerfOffline>(dt);
            }

            return result;
        }
    }
}