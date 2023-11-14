//------------------------------------------------------------------------------
// <copyright file="OrganicFarmingRepository.cs" company="Government of Bihar">
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
    /// Organic Farming Repository.
    /// </summary>
    public class OrganicFarmingRepository : BaseRepository, IOrganicFarmingRepository
    {
        /// <summary>
        /// Usp Name GetFPODetails.
        /// </summary>
        public const string UspNameGetFPODetails = "usp_getdata_NPOP";

        /// <summary>
        /// IOptions.
        /// </summary>
        private readonly IOptions<DBSettings> options;
        string istStrDate = "select CAST(DATEADD(HOUR, 5, DATEADD(MINUTE, 30, GETUTCDATE())) as DATE)";
        private string istDate = string.Empty;

        /// <summary>
        /// Organic Farming Repository.
        /// </summary>
        /// <param name="config">config</param>
        /// <param name="options">options</param>
        public OrganicFarmingRepository(IConfiguration config, IOptions<DBSettings> options)
            : base(config)
        {
            this.options = options;
            SqlHelper.SetConnectionString(this.options.Value.ConnectionString);
            this.istDate = this.GetDateFromServer();
        }

        /// <summary>
        /// GetDateFromServer.
        /// </summary>
        /// <returns>GetDateFromServer</returns>
        public string GetDateFromServer()
        {
            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(this.istStrDate, null, SqlHelper.ExecutionType.Query);
            return dt.Rows[0][0].ToString();
        }

        /// <summary>
        /// Get FPO Details.
        /// </summary>
        /// <param name="fpoId">fpo Id.</param>
        /// <returns>FPO Details.</returns>
        public List<FPODetails> GetFPODetails(int fpoId)
        {
            List<FPODetails> result = new List<FPODetails>();
            List<DbParameter> parameters = new List<DbParameter>();
            parameters.Add(new SqlParameter { ParameterName = "@query_name", Value = "GetFPODetails", SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@fpo_id", Value = fpoId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(UspNameGetFPODetails, parameters, SqlHelper.ExecutionType.Procedure);
            if (dt != null && dt.Rows.Count > 0)
            {
                result = (from DataRow row in dt.Rows
                          select new FPODetails()
                          {
                              FpoId = Convert.ToInt32(row["fpo_id"]),
                              FpoName = row["fpo_name"].ToString(),
                              FpoAddress = row["fpo_address"].ToString(),
                              FpoContactPerson = row["fpo_contact_person"].ToString(),
                              FpoPhoneNum = row["fpo_phone_num"].ToString(),
                          }).ToList();
            }

            return result;
        }

        /// <summary>
        /// Get NPOP Farmer Details.
        /// </summary>
        /// <returns>NPOPFarmerInfo.</returns>
        public NpopFarmerDetailsModel GetNPOPFarmerDetails(long farmer_dbt_reg_no)
        {
            NpopFarmerDetailsModel result = null;
            List<DbParameter> parameters = new List<DbParameter>();
            parameters.Add(new SqlParameter { ParameterName = "@query_name", Value = "GetFarmerdetails", SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@farmer_dbt_reg_no", Value = farmer_dbt_reg_no, SqlDbType = SqlDbType.BigInt, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@phone_num", Value = 0, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(UspNameGetFPODetails, parameters, SqlHelper.ExecutionType.Procedure);
            if (dt != null && dt.Rows.Count > 0)
            {
                result = SqlHelper.ConvertDataTableToList<NpopFarmerDetailsModel>(dt)[0];
            }

            return result;
        }

        /// <summary>
        /// Get Scheme Details.
        /// </summary>
        /// <returns>NPOPScheme.</returns>
        public List<NpopSchemeModel> GetSchemeDetails()
        {
            List<NpopSchemeModel> result = null;
            List<DbParameter> parameters = new List<DbParameter>();
            parameters.Add(new SqlParameter { ParameterName = "@query_name", Value = "GETSchemeDetails", SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(UspNameGetFPODetails, parameters, SqlHelper.ExecutionType.Procedure);
            if (dt != null && dt.Rows.Count > 0)
            {
                result = (from DataRow row in dt.Rows
                          select new NpopSchemeModel()
                          {
                              SchemeId = Convert.ToInt32(row["scheme_id"]),
                              SchemeName = row["scheme_name"].ToString(),
                          }).ToList();
            }

            return result;
        }

        /// <summary>
        /// Get NPOP Farm Details.
        /// </summary>
        /// <returns>NPOPFarmInfo.</returns>
        public List<NpopFarmDetailsModel> GetNPOPFarmDetails(long farmer_dbt_reg_no)
        {
            List<NpopFarmDetailsModel> result = null;
            List<DbParameter> parameters = new List<DbParameter>();
            parameters.Add(new SqlParameter { ParameterName = "@query_name", Value = "GetFarmDetails", SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@farmer_dbt_reg_no", Value = farmer_dbt_reg_no, SqlDbType = SqlDbType.BigInt, Direction = ParameterDirection.Input });
            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(UspNameGetFPODetails, parameters, SqlHelper.ExecutionType.Procedure);
            if (dt != null && dt.Rows.Count > 0)
            {
                result = SqlHelper.ConvertDataTableToList<NpopFarmDetailsModel>(dt);
            }

            return result;
        }

        /// <summary>
        /// Get NPOP Crop Details.
        /// </summary>
        /// <returns>NPOPCropInfo.</returns>
        public List<NpopCropDetailsModel> GetNPOPCropDetails(long farmerDbtRegNo, int farmId, int seasonId)
        {
            List<NpopCropDetailsModel> result = new List<NpopCropDetailsModel>();
            List<DbParameter> parameters = new List<DbParameter>();
            parameters.Add(new SqlParameter { ParameterName = "@query_name", Value = "GetCropDetails", SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@farmer_dbt_reg_no", Value = farmerDbtRegNo, SqlDbType = SqlDbType.BigInt, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@farm_id", Value = farmId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@season_id", Value = seasonId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });

            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(UspNameGetFPODetails, parameters, SqlHelper.ExecutionType.Procedure);
            if (dt != null && dt.Rows.Count > 0)
            {
                result = SqlHelper.ConvertDataTableToList<NpopCropDetailsModel>(dt);
            }

            return result;
        }

        /// <summary>
        /// Get NPOP Details.
        /// </summary>
        /// <returns>List NPOPDetails.</returns>
        public List<NpopDetailsListModel> GetNPOPDetails(int districtId, string blockId, string panchayatId, int seasonId, string status)
        {
            List<NpopDetailsListModel> list = new List<NpopDetailsListModel>();
            List<DbParameter> parameters = new List<DbParameter>();
            parameters.Add(new SqlParameter { ParameterName = "@query_name", Value = "GetDetails", SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@district_id", Value = districtId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@block_id", Value = blockId, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@panchayth_id", Value = panchayatId, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@season_id", Value = seasonId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@Status", Value = status, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(UspNameGetFPODetails, parameters, SqlHelper.ExecutionType.Procedure);
            if (dt != null && dt.Rows.Count > 0)
            {
                list = (from DataRow dr in dt.Rows
                        select new NpopDetailsListModel()
                        {
                            FarmerName = dr["farmer_name"].ToString(),
                            FarmerDbtRegNo = dr["farmer_dbt_reg_no"].ToString(),
                            PhoneNum = dr["phone_num"].ToString(),
                            BlockName = dr["block_name"].ToString(),
                            PanchayatName = dr["panchayat_name"].ToString(),
                            NoOfFarms = Convert.ToInt32(dr["no_of_farms"]),
                            NoOfCrops = Convert.ToInt32(dr["no_of_crops"]),
                            StatusF = dr["Status_f"].ToString(),
                        }).ToList();
            }

            return list;
        }

        /// <summary>
        /// Post NPOP Details.
        /// </summary>
        /// <param name="createNpopDetails">createNpopDetails.</param>
        /// <returns>int</returns>
        public int PostNPOPDetails(NpopDetailsCreateModel createNpopDetails)
        {
            int insertRowsCount = 0;
            string activity_type = string.Empty;
            Dictionary<string, dynamic> result = new Dictionary<string, dynamic>();
            Dictionary<string, dynamic> farmresult = new Dictionary<string, dynamic>();
            Dictionary<string, dynamic> cropresult = new Dictionary<string, dynamic>();
            if (createNpopDetails != null && createNpopDetails.FarmerDetails != null)
            {
                activity_type = "FarmerDetails";
                List<DbParameter> farmerInsparams = new List<DbParameter>();
                farmerInsparams.Add(new SqlParameter { ParameterName = "@farmer_dbt_reg_no", Value = createNpopDetails.FarmerDetails.Farmer_dbt_reg_no, SqlDbType = SqlDbType.BigInt, Direction = ParameterDirection.Input });
                farmerInsparams.Add(new SqlParameter { ParameterName = "@farmer_name", Value = createNpopDetails.FarmerDetails.Farmer_name, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                farmerInsparams.Add(new SqlParameter { ParameterName = "@father_hus_name", Value = createNpopDetails.FarmerDetails.Father_hus_name, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                farmerInsparams.Add(new SqlParameter { ParameterName = "@phone_num", Value = createNpopDetails.FarmerDetails.Phone_num, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                farmerInsparams.Add(new SqlParameter { ParameterName = "@district_lg_code", Value = createNpopDetails.FarmerDetails.District_lg_code, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                farmerInsparams.Add(new SqlParameter { ParameterName = "@block_lg_code", Value = createNpopDetails.FarmerDetails.Block_lg_code, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                farmerInsparams.Add(new SqlParameter { ParameterName = "@panchayat_lg_code", Value = createNpopDetails.FarmerDetails.Panchayat_lg_code, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                farmerInsparams.Add(new SqlParameter { ParameterName = "@village_name", Value = createNpopDetails.FarmerDetails.Village_name, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                farmerInsparams.Add(new SqlParameter { ParameterName = "@is_belongs_to_FPO", Value = createNpopDetails.FarmerDetails.Is_belongs_to_FPO, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                farmerInsparams.Add(new SqlParameter { ParameterName = "@fpo_id", Value = createNpopDetails.FarmerDetails.Fpo_id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                farmerInsparams.Add(new SqlParameter { ParameterName = "@scheme_id", Value = createNpopDetails.FarmerDetails.Scheme_id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                farmerInsparams.Add(new SqlParameter { ParameterName = "@is_facility_added", Value = createNpopDetails.FarmerDetails.Is_facility_added, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                farmerInsparams.Add(new SqlParameter { ParameterName = "@major_town_name", Value = createNpopDetails.FarmerDetails.Major_town_name, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                farmerInsparams.Add(new SqlParameter { ParameterName = "@major_crop_name", Value = createNpopDetails.FarmerDetails.Major_crop_name, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                farmerInsparams.Add(new SqlParameter { ParameterName = "@subsidy_date_1", Value = createNpopDetails.FarmerDetails.Subsidy_date_1, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
                farmerInsparams.Add(new SqlParameter { ParameterName = "@subsidy_amount_1", Value = createNpopDetails.FarmerDetails.Subsidy_amount_1, SqlDbType = SqlDbType.Decimal, Direction = ParameterDirection.Input });
                farmerInsparams.Add(new SqlParameter { ParameterName = "@subsidy_date_2", Value = createNpopDetails.FarmerDetails.Subsidy_date_2, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
                farmerInsparams.Add(new SqlParameter { ParameterName = "@subsidy_amount_2", Value = createNpopDetails.FarmerDetails.Subsidy_amount_2, SqlDbType = SqlDbType.Decimal, Direction = ParameterDirection.Input });
                farmerInsparams.Add(new SqlParameter { ParameterName = "@subsidy_date_3", Value = createNpopDetails.FarmerDetails.Subsidy_date_3, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
                farmerInsparams.Add(new SqlParameter { ParameterName = "@subsidy_amount_3", Value = createNpopDetails.FarmerDetails.Subsidy_amount_3, SqlDbType = SqlDbType.Decimal, Direction = ParameterDirection.Input });
                farmerInsparams.Add(new SqlParameter { ParameterName = "@userid", Value = createNpopDetails.FarmerDetails.Rec_created_userid, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                farmerInsparams.Add(new SqlParameter { ParameterName = "@certification", Value = createNpopDetails.FarmerDetails.Certification, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });

                result = SqlHelper.ExecuteNonQuery<SqlConnection>("USP_insert_NPOP_organic_farming", farmerInsparams, SqlHelper.ExecutionType.Procedure);
                insertRowsCount = insertRowsCount + result["RowsAffected"];
            }

            if (result["RowsAffected"] != 0 && createNpopDetails.FarmDetails != null)
            {
                activity_type = "FarmDetails";
                for (int i = 0; i < createNpopDetails.FarmDetails.Count; i++)
                {
                    List<DbParameter> farmInsCropparams = new List<DbParameter>();
                    farmInsCropparams.Add(new SqlParameter { ParameterName = "@farmer_dbt_reg_no", Value = createNpopDetails.FarmDetails[i].Farmer_dbt_reg_no, SqlDbType = SqlDbType.BigInt, Direction = ParameterDirection.Input });
                    farmInsCropparams.Add(new SqlParameter { ParameterName = "@farm_id", Value = createNpopDetails.FarmDetails[i].Farm_id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                    farmInsCropparams.Add(new SqlParameter { ParameterName = "@farm_area", Value = createNpopDetails.FarmDetails[i].Farm_area, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                    farmInsCropparams.Add(new SqlParameter { ParameterName = "@latitude", Value = createNpopDetails.FarmDetails[i].Latitude, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                    farmInsCropparams.Add(new SqlParameter { ParameterName = "@langitude", Value = createNpopDetails.FarmDetails[i].Langitude, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                    farmInsCropparams.Add(new SqlParameter { ParameterName = "@userid", Value = createNpopDetails.FarmDetails[i].Rec_created_userid, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });

                    farmresult = SqlHelper.ExecuteNonQuery<SqlConnection>("USP_insert_NPOP_farm_details", farmInsCropparams, SqlHelper.ExecutionType.Procedure);
                }
            }

            if (result["RowsAffected"] != 0 && createNpopDetails.CropDetails != null)
            {
                for (int i = 0; i < createNpopDetails?.CropDetails.Count; i++)
                {
                    activity_type = "CropDetails";
                    List<DbParameter> farmInsCropparams = new List<DbParameter>();
                    farmInsCropparams.Add(new SqlParameter { ParameterName = "@query_name", Value = createNpopDetails.CropDetails[i].Query_name, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                    farmInsCropparams.Add(new SqlParameter { ParameterName = "@farmer_dbt_reg_no", Value = createNpopDetails.CropDetails[i].Farmer_dbt_reg_no, SqlDbType = SqlDbType.BigInt, Direction = ParameterDirection.Input });
                    farmInsCropparams.Add(new SqlParameter { ParameterName = "@farm_id", Value = createNpopDetails.CropDetails[i].Farm_id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                    farmInsCropparams.Add(new SqlParameter { ParameterName = "@Season_id", Value = createNpopDetails.CropDetails[i].Season_id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                    farmInsCropparams.Add(new SqlParameter { ParameterName = "@crop_id", Value = createNpopDetails.CropDetails[i].Crop_id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                    farmInsCropparams.Add(new SqlParameter { ParameterName = "@Crop_cvrg", Value = createNpopDetails.CropDetails[i].Crop_cvrg, SqlDbType = SqlDbType.Decimal, Direction = ParameterDirection.Input });
                    farmInsCropparams.Add(new SqlParameter { ParameterName = "@harvest", Value = createNpopDetails.CropDetails[i].Harvest, SqlDbType = SqlDbType.Decimal, Direction = ParameterDirection.Input });
                    farmInsCropparams.Add(new SqlParameter { ParameterName = "@productivity", Value = createNpopDetails.CropDetails[i].Productivity, SqlDbType = SqlDbType.Decimal, Direction = ParameterDirection.Input });
                    farmInsCropparams.Add(new SqlParameter { ParameterName = "@final_submission_flg", Value = createNpopDetails.CropDetails[i].Final_submission_flg, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                    farmInsCropparams.Add(new SqlParameter { ParameterName = "@is_inspection_happened", Value = createNpopDetails.CropDetails[i].Is_inspection_happened, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                    farmInsCropparams.Add(new SqlParameter { ParameterName = "@inspection_date", Value = createNpopDetails.CropDetails[i].Inspection_date, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
                    farmInsCropparams.Add(new SqlParameter { ParameterName = "@userid", Value = createNpopDetails.CropDetails[i].Rec_created_userid, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });

                    cropresult = SqlHelper.ExecuteNonQuery<SqlConnection>("USP_insert_NPOP_crop_details", farmInsCropparams, SqlHelper.ExecutionType.Procedure);
                }
            }

            return insertRowsCount;
        }

        /// <summary>
        /// GetFarmerGroupTable.
        /// </summary>
        /// <param name="districtId">districtId.</param>
        /// <returns>PGSFarmerGroupTable list.</returns>
        public List<PGSFarmerGroupTable> GetFarmerGroupTable(int districtId)
        {
            List<PGSFarmerGroupTable> farmerGroupTable = new List<PGSFarmerGroupTable>();
            List<DbParameter> dbparams = new List<DbParameter>();
            dbparams.Add(new SqlParameter { ParameterName = "@query_name", Value = "farmergrouplist", SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@district_ID", Value = districtId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>("usp_getdata_PGS", dbparams, SqlHelper.ExecutionType.Procedure);
            if (dt != null && dt.Rows.Count > 0)
            {
                farmerGroupTable = (from DataRow dr in dt.Rows
                                    select new PGSFarmerGroupTable()
                                    {
                                        GroupName = dr["group_name"].ToString(),
                                        GroupRegNo = dr["group_reg_no"].ToString(),
                                        ServiceProviderName = dr["service_provider_name"].ToString(),
                                        AssociatedFarmers = Convert.ToInt32(dr["associated_farmers"]),
                                    }).ToList();
            }

            return farmerGroupTable;
        }

        /// <summary>
        /// GET PGS Scheme Names.
        /// </summary>
        public List<PGSSchemeNames> GetPGSSchemeNames()
        {
            List<PGSSchemeNames> schemeNames = new List<PGSSchemeNames>();
            List<DbParameter> dbparams = new List<DbParameter>();
            dbparams.Add(new SqlParameter { ParameterName = "@query_name", Value = "scheme", SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>("usp_getdata_PGS", dbparams, SqlHelper.ExecutionType.Procedure);
            if (dt != null && dt.Rows.Count > 0)
            {
                schemeNames = (from DataRow dr in dt.Rows
                               select new PGSSchemeNames()
                               {
                                   SchemeName = dr["scheme_name"].ToString(),
                                   SchemeId = dr["scheme_id"].ToString(),
                               }).ToList();
            }

            return schemeNames;
        }

        /// <summary>
        /// Post Farmer Group Details.
        /// </summary>
        public int InsertFarmerGroupDetails(PGSFarmerGroupDetails pgsFarmerGroupDtls)
        {
            int insertRowsCount = 0;
            if (pgsFarmerGroupDtls != null)
            {
                Dictionary<string, dynamic> result = new Dictionary<string, dynamic>();

                List<DbParameter> dbparams = new List<DbParameter>();
                dbparams.Add(new SqlParameter { ParameterName = "@group_name", Value = pgsFarmerGroupDtls.GroupName, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparams.Add(new SqlParameter { ParameterName = "@group_reg_no", Value = pgsFarmerGroupDtls.GroupRegNo, SqlDbType = SqlDbType.NVarChar, Direction = ParameterDirection.Input });
                dbparams.Add(new SqlParameter { ParameterName = "@group_address", Value = pgsFarmerGroupDtls.GroupAdress, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparams.Add(new SqlParameter { ParameterName = "@lrp_name", Value = pgsFarmerGroupDtls.LRPName, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparams.Add(new SqlParameter { ParameterName = "@lrp_phone_num", Value = pgsFarmerGroupDtls.LRPPhoneNo, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparams.Add(new SqlParameter { ParameterName = "@scheme_id", Value = pgsFarmerGroupDtls.SchemeId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                dbparams.Add(new SqlParameter { ParameterName = "@service_provider_name", Value = pgsFarmerGroupDtls.ServiceProviderName, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparams.Add(new SqlParameter { ParameterName = "@certification", Value = pgsFarmerGroupDtls.Certification, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparams.Add(new SqlParameter { ParameterName = "@no_of_farmers_trained", Value = pgsFarmerGroupDtls.NoOfFarmersTrained, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                dbparams.Add(new SqlParameter { ParameterName = "@no_of_farmers_exposure_visit", Value = pgsFarmerGroupDtls.NoOfFarmersExpoVisit, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                dbparams.Add(new SqlParameter { ParameterName = "@is_belongs_to_FPO", Value = pgsFarmerGroupDtls.PartOfFPO, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparams.Add(new SqlParameter { ParameterName = "@is_mkt_facility_added", Value = pgsFarmerGroupDtls.AvailOfMktFacility, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparams.Add(new SqlParameter { ParameterName = "@major_town_name", Value = pgsFarmerGroupDtls.MajorTownName, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparams.Add(new SqlParameter { ParameterName = "@major_crop_name", Value = pgsFarmerGroupDtls.MajorCropName, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparams.Add(new SqlParameter { ParameterName = "@fpo_name", Value = pgsFarmerGroupDtls.FPOName, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparams.Add(new SqlParameter { ParameterName = "@fpo_incharge", Value = pgsFarmerGroupDtls.FPOIncharge, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparams.Add(new SqlParameter { ParameterName = "@fpo_incharge_num", Value = pgsFarmerGroupDtls.FPOInchargeNo, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparams.Add(new SqlParameter { ParameterName = "@fpo_address", Value = pgsFarmerGroupDtls.FPOAddress, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparams.Add(new SqlParameter { ParameterName = "@userid", Value = pgsFarmerGroupDtls.RecCreatedUserid, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                dbparams.Add(new SqlParameter { ParameterName = "@district_id", Value = pgsFarmerGroupDtls.DistrictId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });

                result = SqlHelper.ExecuteNonQuery<SqlConnection>("USP_insert_PGS_farmer_group_dtls", dbparams, SqlHelper.ExecutionType.Procedure);

                insertRowsCount = insertRowsCount + result["RowsAffected"];
            }

            if (insertRowsCount != 0)
                return 1;
            else
                return 0;
        }

        /// <summary>
        /// Get Associated Farmer Details.
        /// </summary>
        /// <returns>List Associated Farmer Details.</returns>
        public List<AssociatedFarmerDetailsListModel> GetAssociatedFarmerDetails(int districtId, int blockId, int panchayatId, int seasonId, int groupId, string status)
        {
            List<AssociatedFarmerDetailsListModel> list = new List<AssociatedFarmerDetailsListModel>();
            List<DbParameter> parameters = new List<DbParameter>();
            parameters.Add(new SqlParameter { ParameterName = "@query_name", Value = "AssociateFarmerDetails", SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@district_id", Value = districtId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@block_id", Value = blockId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@panchayth_id", Value = panchayatId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@season_id", Value = seasonId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@group_id", Value = groupId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@Status", Value = status, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>("usp_getdata_PGS", parameters, SqlHelper.ExecutionType.Procedure);
            if (dt != null && dt.Rows.Count > 0)
            {
                list = (from DataRow dr in dt.Rows
                        select new AssociatedFarmerDetailsListModel()
                        {
                            FarmerName = dr["farmer_name"].ToString(),
                            FarmerDbtRegNo = dr["farmer_dbt_reg_no"].ToString(),
                            BlockName = dr["block_name"].ToString(),
                            PanchayatName = dr["panchayat_name"].ToString(),
                            GroupName = dr["group_name"].ToString(),
                            NoOfFarms = Convert.ToInt32(dr["no_of_farms"]),
                            StatusF = dr["Status_f"].ToString(),
                        }).ToList();
            }

            return list;
        }

        /// <summary>
        /// GET PGS Group Names.
        /// </summary>
        public List<PGSGroupNames> GetPGSGroupNames(int districtId)
        {
            List<PGSGroupNames> groupNames = new List<PGSGroupNames>();
            List<DbParameter> dbparams = new List<DbParameter>();
            dbparams.Add(new SqlParameter { ParameterName = "@query_name", Value = "GetgrpDetails", SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@district_id ", Value = districtId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>("usp_getdata_PGS", dbparams, SqlHelper.ExecutionType.Procedure);
            if (dt != null && dt.Rows.Count > 0)
            {
                groupNames = (from DataRow dr in dt.Rows
                              select new PGSGroupNames()
                              {
                                  GroupName = dr["group_name"].ToString(),
                                  GroupId = dr["ID"].ToString(),
                                  GroupRegNo = dr["group_reg_no"].ToString(),
                              }).ToList();
            }

            return groupNames;
        }

        /// <summary>
        /// Get Farmer GroupDetails.
        /// </summary>
        /// <returns>List Associated Farmer Details.</returns>
        public List<PGSFarmerGroupDetailsId> GetFarmerGroupDetails(string Regno)
        {
            List<PGSFarmerGroupDetailsId> list = new List<PGSFarmerGroupDetailsId>();
            List<DbParameter> parameters = new List<DbParameter>();
            parameters.Add(new SqlParameter { ParameterName = "@query_name", Value = "GroupDetails", SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@group_reg_no ", Value = Regno, SqlDbType = SqlDbType.NVarChar, Direction = ParameterDirection.Input });
            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>("usp_getdata_PGS", parameters, SqlHelper.ExecutionType.Procedure);
            if (dt != null && dt.Rows.Count > 0)
            {
                list = (from DataRow dr in dt.Rows
                        select new PGSFarmerGroupDetailsId()
                        {
                            GroupName = dr["group_name"].ToString(),
                            GroupRegNo = dr["group_reg_no"].ToString(),
                            GroupAdress = dr["group_address"].ToString(),
                            LRPName = dr["lrp_name"].ToString(),
                            LRPPhoneNo = dr["lrp_phone_num"].ToString(),
                            SchemeId = Convert.ToInt32(dr["scheme_id"]),
                            ServiceProviderName = dr["service_provider_name"].ToString(),
                            NoOfFarmersTrained = Convert.ToInt32(dr["no_of_farmers_trained"]),
                            NoOfFarmersExpoVisit = Convert.ToInt32(dr["no_of_farmers_exposure_visit"]),
                            AvailOfMktFacility = dr["is_mkt_facility_added"].ToString(),
                            MajorTownName = dr["major_town_name"].ToString(),
                            MajorCropName = dr["major_crop_name"].ToString(),
                            Certification = dr["certification"].ToString(),
                            PartOfFPO = dr["is_belongs_to_FPO"].ToString(),
                            FPOName = dr["fpo_name"].ToString(),
                            FPOIncharge = dr["fpo_incharge"].ToString(),
                            FPOInchargeNo = dr["fpo_incharge_num"].ToString(),
                            FPOAddress = dr["fpo_address"].ToString(),
                        }).ToList();
            }

            return list;
        }

        /// <summary>
        /// GET PGS Major Town Names.
        /// </summary>
        public List<PGSMajorTownNames> GetPGSMajorTownNames()
        {
            List<PGSMajorTownNames> majorTownNames = new List<PGSMajorTownNames>();
            List<DbParameter> dbparams = new List<DbParameter>();
            dbparams.Add(new SqlParameter { ParameterName = "@query_name", Value = "Getmajortowndetails", SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>("usp_getdata_PGS", dbparams, SqlHelper.ExecutionType.Procedure);
            if (dt != null && dt.Rows.Count > 0)
            {
                majorTownNames = (from DataRow dr in dt.Rows
                                  select new PGSMajorTownNames()
                                  {
                                      MajorTownName = dr["MajorTownName"].ToString(),
                                  }).ToList();
            }

            return majorTownNames;
        }

        /// <summary>
        /// GET PGS Major Crop Names.
        /// </summary>
        public List<PGSMajorCropNames> GetPGSMajorCropNames()
        {
            List<PGSMajorCropNames> majorCropNames = new List<PGSMajorCropNames>();
            List<DbParameter> dbparams = new List<DbParameter>();
            dbparams.Add(new SqlParameter { ParameterName = "@query_name", Value = "Getmajorcropdetails", SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>("usp_getdata_PGS", dbparams, SqlHelper.ExecutionType.Procedure);
            if (dt != null && dt.Rows.Count > 0)
            {
                majorCropNames = (from DataRow dr in dt.Rows
                                  select new PGSMajorCropNames()
                                  {
                                      MajorCropName = dr["MajorCropName"].ToString(),
                                  }).ToList();
            }

            return majorCropNames;
        }

        /// <summary>
        /// Get PGS Farmer Details.
        /// </summary>
        /// <returns>PGSFarmerInfo.</returns>
        public PgsFarmerDetailsModel GetPGSFarmerDetails(long farmer_dbt_reg_no)
        {
            PgsFarmerDetailsModel result = null;
            List<DbParameter> parameters = new List<DbParameter>();
            parameters.Add(new SqlParameter { ParameterName = "@query_name", Value = "GetFarmerdetails", SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@farmer_dbt_reg_no", Value = farmer_dbt_reg_no, SqlDbType = SqlDbType.BigInt, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@phone_num", Value = 0, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>("usp_getdata_PGS", parameters, SqlHelper.ExecutionType.Procedure);
            if (dt != null && dt.Rows.Count > 0)
            {
                result = SqlHelper.ConvertDataTableToList<PgsFarmerDetailsModel>(dt)[0];
            }

            return result;
        }

        /// <summary>
        /// Get PGS Farm Details.
        /// </summary>
        /// <returns>PGSFarmInfo.</returns>
        public List<PgsFarmDetailsModel> GetPGSFarmDetails(long farmer_dbt_reg_no)
        {
            List<PgsFarmDetailsModel> result = null;
            List<DbParameter> parameters = new List<DbParameter>();
            parameters.Add(new SqlParameter { ParameterName = "@query_name", Value = "GetFarmDetails", SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@farmer_dbt_reg_no", Value = farmer_dbt_reg_no, SqlDbType = SqlDbType.BigInt, Direction = ParameterDirection.Input });
            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>("usp_getdata_PGS", parameters, SqlHelper.ExecutionType.Procedure);
            if (dt != null && dt.Rows.Count > 0)
            {
                result = SqlHelper.ConvertDataTableToList<PgsFarmDetailsModel>(dt);
            }

            return result;
        }

        /// <summary>
        /// Get PGS Crop Details.
        /// </summary>
        /// <returns>PGSCropInfo.</returns>
        public List<PgsCropDetailsModel> GetPGSCropDetails(long farmerDbtRegNo, int farmId, int seasonId)
        {
            List<PgsCropDetailsModel> result = new List<PgsCropDetailsModel>();
            List<DbParameter> parameters = new List<DbParameter>();
            parameters.Add(new SqlParameter { ParameterName = "@query_name", Value = "GetCropDetails", SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@farmer_dbt_reg_no", Value = farmerDbtRegNo, SqlDbType = SqlDbType.BigInt, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@farm_id", Value = farmId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@season_id", Value = seasonId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });

            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>("usp_getdata_PGS", parameters, SqlHelper.ExecutionType.Procedure);
            if (dt != null && dt.Rows.Count > 0)
            {
                result = SqlHelper.ConvertDataTableToList<PgsCropDetailsModel>(dt);
            }

            return result;
        }

        /// <summary>
        /// Post PGS Details
        /// </summary>
        /// <param name="createPgsDetails">createPgsDetails.</param>
        /// <returns>int</returns>
        public int PostPGSDetails(PgsDetailsCreateModel createPgsDetails)
        {
            int insertRowsCount = 0;
            string activity_type = string.Empty;
            Dictionary<string, dynamic> result = new Dictionary<string, dynamic>();
            Dictionary<string, dynamic> farmresult = new Dictionary<string, dynamic>();
            List<PgsCropDetailsFarmXrefId> resultt = new List<PgsCropDetailsFarmXrefId>();
            Dictionary<string, dynamic> cropresult = new Dictionary<string, dynamic>();
            if (createPgsDetails != null && createPgsDetails.FarmerDetails != null)
            {
                activity_type = "FarmerDetails";
                List<DbParameter> farmerInsparams = new List<DbParameter>();
                farmerInsparams.Add(new SqlParameter { ParameterName = "@group_id", Value = createPgsDetails.FarmerDetails.Group_id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                farmerInsparams.Add(new SqlParameter { ParameterName = "@farmer_dbt_reg_no", Value = createPgsDetails.FarmerDetails.Farmer_dbt_reg_no, SqlDbType = SqlDbType.BigInt, Direction = ParameterDirection.Input });
                farmerInsparams.Add(new SqlParameter { ParameterName = "@farmer_name", Value = createPgsDetails.FarmerDetails.Farmer_name, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                farmerInsparams.Add(new SqlParameter { ParameterName = "@father_hus_name", Value = createPgsDetails.FarmerDetails.Father_hus_name, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                farmerInsparams.Add(new SqlParameter { ParameterName = "@phone_num", Value = createPgsDetails.FarmerDetails.Phone_num, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                farmerInsparams.Add(new SqlParameter { ParameterName = "@district_lg_code", Value = createPgsDetails.FarmerDetails.District_lg_code, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                farmerInsparams.Add(new SqlParameter { ParameterName = "@block_lg_code", Value = createPgsDetails.FarmerDetails.Block_lg_code, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                farmerInsparams.Add(new SqlParameter { ParameterName = "@panchayat_lg_code", Value = createPgsDetails.FarmerDetails.Panchayat_lg_code, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                farmerInsparams.Add(new SqlParameter { ParameterName = "@village_name", Value = createPgsDetails.FarmerDetails.Village_name, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                farmerInsparams.Add(new SqlParameter { ParameterName = "@subsidy_date_1", Value = createPgsDetails.FarmerDetails.Subsidy_date_1, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
                farmerInsparams.Add(new SqlParameter { ParameterName = "@subsidy_amount_1", Value = createPgsDetails.FarmerDetails.Subsidy_amount_1, SqlDbType = SqlDbType.Decimal, Direction = ParameterDirection.Input });
                farmerInsparams.Add(new SqlParameter { ParameterName = "@subsidy_date_2", Value = createPgsDetails.FarmerDetails.Subsidy_date_2, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
                farmerInsparams.Add(new SqlParameter { ParameterName = "@subsidy_amount_2", Value = createPgsDetails.FarmerDetails.Subsidy_amount_2, SqlDbType = SqlDbType.Decimal, Direction = ParameterDirection.Input });
                farmerInsparams.Add(new SqlParameter { ParameterName = "@subsidy_date_3", Value = createPgsDetails.FarmerDetails.Subsidy_date_3, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
                farmerInsparams.Add(new SqlParameter { ParameterName = "@subsidy_amount_3", Value = createPgsDetails.FarmerDetails.Subsidy_amount_3, SqlDbType = SqlDbType.Decimal, Direction = ParameterDirection.Input });
                farmerInsparams.Add(new SqlParameter { ParameterName = "@userid", Value = createPgsDetails.FarmerDetails.Rec_created_userid, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                result = SqlHelper.ExecuteNonQuery<SqlConnection>("USP_insert_PGS_farmer_dtls", farmerInsparams, SqlHelper.ExecutionType.Procedure);
                insertRowsCount = insertRowsCount + result["RowsAffected"];
            }

            if (result["RowsAffected"] != 0 && createPgsDetails?.FarmDetails != null)
            {
                activity_type = "FarmDetails";
                for (int i = 0; i < createPgsDetails?.FarmDetails.Count; i++)
                {
                    List<DbParameter> farmInsCropparams = new List<DbParameter>();
                    farmInsCropparams.Add(new SqlParameter { ParameterName = "@farmer_dbt_reg_no", Value = createPgsDetails.FarmDetails[i].Farmer_dbt_reg_no, SqlDbType = SqlDbType.BigInt, Direction = ParameterDirection.Input });
                    farmInsCropparams.Add(new SqlParameter { ParameterName = "@farm_id", Value = createPgsDetails.FarmDetails[i].Farm_id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                    farmInsCropparams.Add(new SqlParameter { ParameterName = "@farm_area", Value = createPgsDetails.FarmDetails[i].Farm_area, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                    farmInsCropparams.Add(new SqlParameter { ParameterName = "@latitude", Value = createPgsDetails.FarmDetails[i].Latitude, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                    farmInsCropparams.Add(new SqlParameter { ParameterName = "@longitude", Value = createPgsDetails.FarmDetails[i].Langitude, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                    farmInsCropparams.Add(new SqlParameter { ParameterName = "@userid", Value = createPgsDetails.FarmDetails[i].Rec_created_userid, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });

                    farmresult = SqlHelper.ExecuteNonQuery<SqlConnection>("USP_insert_PGS_farm_dtls", farmInsCropparams, SqlHelper.ExecutionType.Procedure);
                    DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>("USP_insert_PGS_farm_dtls", farmInsCropparams, SqlHelper.ExecutionType.Procedure);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        resultt = SqlHelper.ConvertDataTableToList<PgsCropDetailsFarmXrefId>(dt);
                    }

                    for (int j = 0; j < createPgsDetails.CropDetails.Count; j++)
                    {
                        if ((createPgsDetails.CropDetails[j].Farm_id) == (createPgsDetails.FarmDetails[i].Farm_id))
                        {
                            activity_type = "CropDetails";
                            List<DbParameter> InsCropparams = new List<DbParameter>();
                            InsCropparams.Add(new SqlParameter { ParameterName = "@query_name", Value = "Insert", SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                            InsCropparams.Add(new SqlParameter { ParameterName = "@farm_xref_id", Value = resultt[0].Farm_xref_id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                            InsCropparams.Add(new SqlParameter { ParameterName = "@Season_id", Value = createPgsDetails.CropDetails[j].Season_id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                            InsCropparams.Add(new SqlParameter { ParameterName = "@crop_id", Value = createPgsDetails.CropDetails[j].Crop_id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                            InsCropparams.Add(new SqlParameter { ParameterName = "@cvrg_area", Value = createPgsDetails.CropDetails[j].Cvrg_area, SqlDbType = SqlDbType.Decimal, Direction = ParameterDirection.Input });
                            InsCropparams.Add(new SqlParameter { ParameterName = "@harvest", Value = createPgsDetails.CropDetails[j].Harvest, SqlDbType = SqlDbType.Decimal, Direction = ParameterDirection.Input });
                            InsCropparams.Add(new SqlParameter { ParameterName = "@productivity", Value = createPgsDetails.CropDetails[j].Productivity, SqlDbType = SqlDbType.Decimal, Direction = ParameterDirection.Input });
                            InsCropparams.Add(new SqlParameter { ParameterName = "@final_submission_flg", Value = createPgsDetails.CropDetails[j].Final_submission_flg, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                            InsCropparams.Add(new SqlParameter { ParameterName = "@Is_inspection_happened", Value = createPgsDetails.CropDetails[j].Is_inspection_happened, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                            InsCropparams.Add(new SqlParameter { ParameterName = "@Inspection_date", Value = createPgsDetails.CropDetails[j].Inspection_date, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
                            InsCropparams.Add(new SqlParameter { ParameterName = "@userid", Value = createPgsDetails.CropDetails[j].Rec_created_userid, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });

                            cropresult = SqlHelper.ExecuteNonQuery<SqlConnection>("USP_insert_PGS_crop_details", InsCropparams, SqlHelper.ExecutionType.Procedure);
                        }
                    }
                }
            }

            return insertRowsCount;
        }

        /// <summary>
        /// Get Group Names And DbtNo.
        /// </summary>
        public List<PGSGroupNamesAndDbtNo> GetGroupNamesAndDbtNo(int districtId)
        {
            List<PGSGroupNamesAndDbtNo> groupNames = new List<PGSGroupNamesAndDbtNo>();
            List<DbParameter> dbparams = new List<DbParameter>();
            dbparams.Add(new SqlParameter { ParameterName = "@query_name", Value = "GetdbtDetails", SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@district_id ", Value = districtId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>("usp_getdata_PGS", dbparams, SqlHelper.ExecutionType.Procedure);
            if (dt != null && dt.Rows.Count > 0)
            {
                groupNames = (from DataRow dr in dt.Rows
                              select new PGSGroupNamesAndDbtNo()
                              {
                                  GroupName = dr["group_name"].ToString(),
                                  Id = dr["ID"].ToString(),
                                  GroupDbtNo = dr["farmer_dbt_reg_no"].ToString(),
                              }).ToList();
            }

            return groupNames;
        }

        /// <summary>
        /// Getting PGS User Details.
        /// </summary>
        /// <param name="userid">userid.</param>
        /// <returns>User Details.</returns>
        public List<DisburseEntity> IsPgsUser(int userid)
        {
            var enitity = new List<DisburseEntity>();
            var parameters = new List<DbParameter>();
            parameters.Add(new SqlParameter { ParameterName = "@user_id", Value = userid, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@query_name", Value = "GetpgsuserDetails", SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>("usp_getdata_PGS", parameters, SqlHelper.ExecutionType.Procedure);
            if (dt != null && dt.Rows.Count > 0)
            {
                enitity = SqlHelper.ConvertDataTableToList<DisburseEntity>(dt);
            }

            return enitity;
        }

        /// <summary>
        /// Getting NPOP User Details.
        /// </summary>
        /// <param name="userid">userid.</param>
        /// <param name="designation">designation.</param>
        /// <returns>User Details.</returns>
        public List<DisburseEntity> IsNpopUser(int userid, string designation)
        {
            var enitity = new List<DisburseEntity>();
            var parameters = new List<DbParameter>();
            parameters.Add(new SqlParameter { ParameterName = "@user_id", Value = userid, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@Designation", Value = designation, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@query_name", Value = "GetNPOPUserDetails", SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>("usp_getdata_NPOP", parameters, SqlHelper.ExecutionType.Procedure);
            if (dt != null && dt.Rows.Count > 0)
            {
                enitity = SqlHelper.ConvertDataTableToList<DisburseEntity>(dt);
            }

            return enitity;
        }

        /// <summary>
        /// GET Npop Major Town Names.
        /// </summary>
        public List<NpopMajorTownNames> GetNpopMajorTownNames()
        {
            List<NpopMajorTownNames> majorTownNames = new List<NpopMajorTownNames>();
            List<DbParameter> dbparams = new List<DbParameter>();
            dbparams.Add(new SqlParameter { ParameterName = "@query_name", Value = "Getmajortowndetails", SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>("usp_getdata_NPOP", dbparams, SqlHelper.ExecutionType.Procedure);
            if (dt != null && dt.Rows.Count > 0)
            {
                majorTownNames = (from DataRow dr in dt.Rows
                                  select new NpopMajorTownNames()
                                  {
                                      MajorTownName = dr["MajorTownName"].ToString(),
                                  }).ToList();
            }

            return majorTownNames;
        }

        /// <summary>
        /// GET Npop Major Crop Names.
        /// </summary>
        public List<NpopMajorCropNames> GetNpopMajorCropNames()
        {
            List<NpopMajorCropNames> majorCropNames = new List<NpopMajorCropNames>();
            List<DbParameter> dbparams = new List<DbParameter>();
            dbparams.Add(new SqlParameter { ParameterName = "@query_name", Value = "Getmajorcropdetails", SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>("usp_getdata_NPOP", dbparams, SqlHelper.ExecutionType.Procedure);
            if (dt != null && dt.Rows.Count > 0)
            {
                majorCropNames = (from DataRow dr in dt.Rows
                                  select new NpopMajorCropNames()
                                  {
                                      MajorCropName = dr["MajorCropName"].ToString(),
                                  }).ToList();
            }

            return majorCropNames;
        }

        /// <summary>
        /// InsertAgriFarmerDetails.
        /// </summary>
        /// <param name="orgFarmAgriFarmerDetails">orgFarmAgriFarmerDetails.</param>
        /// <returns>orgFarmAgriFarmerDetails Respinse.</returns>
        public int InsertAgriFarmerDetails(OrgFarmAgriFarmerDetails orgFarmAgriFarmerDetails)
        {
            var insertRowsCount = 0;
            if (orgFarmAgriFarmerDetails != null)
            {
                var parameters = new List<DbParameter>
                    {
                        new SqlParameter { ParameterName = "@farmer_dbt_reg_no", Value = orgFarmAgriFarmerDetails.RegistrationID, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input },
                        new SqlParameter { ParameterName = "@phone_num", Value = orgFarmAgriFarmerDetails.MobileNumber, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input },
                        new SqlParameter { ParameterName = "@gender_name", Value = orgFarmAgriFarmerDetails.Gender, SqlDbType = SqlDbType.NVarChar, Direction = ParameterDirection.Input },
                        new SqlParameter { ParameterName = "@type_name", Value = orgFarmAgriFarmerDetails.Farmertype, SqlDbType = SqlDbType.NVarChar, Direction = ParameterDirection.Input },
                        new SqlParameter { ParameterName = "@caste_category", Value = orgFarmAgriFarmerDetails.CastCateogary, SqlDbType = SqlDbType.NVarChar, Direction = ParameterDirection.Input },
                    };
                var result = new Dictionary<string, dynamic>();
                result = SqlHelper.ExecuteNonQuery<SqlConnection>("usp_insert_NPOP_dbt_dtls", parameters, SqlHelper.ExecutionType.Procedure);
                insertRowsCount += result["RowsAffected"];
            }

            return insertRowsCount != 0 ? 1 : 0;
        }
    }
}