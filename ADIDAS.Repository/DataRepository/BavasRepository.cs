//------------------------------------------------------------------------------
// <copyright file="BavasRepository.cs" company="Government of Bihar">
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
    /// BavasRepository.
    /// </summary>
    public class BavasRepository : BaseRepository, IBavasRepository
    {
        private static string spInsertNewNode = "USP_Insert_bavas_nodes";
        private static string spInsertBavasStructure = "USP_Insert_bavas_structure";
        private static string spInsertBavasCropsIntelligence = "USP_Insert_bavas_price_intelligence_crops";
        private static string spInsertBavasPriceIntelligence = "USP_Insert_bavas_price_intelligence_map";
        private static string qnGetUnitofMeasure = "GetDistinctunitofmeasure";
        private static string spGetUnitofMeasure = "usp_getdata_Crop_structure_nodes";
        private static string qnGetAllPriceIntelligenceCrops = "GetAllPriceIntelligenceCrops";
        private static string qnGetPriceIntelligenceDetailsforPastSevenDays = "GetLast_7DaysPriceIntelligenceDetails";
        private static string spPostBavasContractForming = "USP_Insert_bavas_contract_farming";
        private static string qnGetAllNodesByDistrictId = "GetAllNodesByDistrictID";
        private static string qnGetAllStructureByDistrictId = "GetAllStructuresByDistrictID";
        private static string qnGetAgriHortiCrops = "GETAGRIHORTICrops";
        private static string qnGetAllFarmerProducerOrgByPanchayat = "GetAllFarmerProducerOrgByPanchayat";
        private static string qnGetSpecificFarmerProducerOrg = "GetSpecificFarmerProducerOrg";
        private static string qnGetAllContractFarmingDetails = "GetAllContractFarmingDetails";
        private static string qnGetSpecificContractFarmingDetail = "GetSpecificContractFarmingDetail";
        private static string qnGETSEASONALCropsByDistrictId = "GETSEASONALCrops";
        private static string spInsertBavasfarmerproducerOrg = "USP_Insert_bavas_farmer_producer_org";
        private static string qnGetAllFacilityDetails = "GetAllFacilityDetails";
        private static string qnGetDirectorMarketingInfraStructure = "GetDirectorMarketingInfraStructure";
        private static string spInsertBavasMarkettingInfra = "USP_Insert_bavas_marketing_infrastructure";
        private static string qnGetDirectorMarketingInfraStructureByDistrict = "GetDirectorMarketingInfraStructureByDistrict";
        private static string qnGetMarketingInfoNoFacilityData = "GetMarketingInfoNoFacilityData";

        private readonly IOptions<DBSettings> options;

        /// <summary>
        /// Initializes a new instance of the <see cref="BavasRepository"/> class.
        /// BavasRepository.
        /// </summary>
        /// <param name="config">config.</param>
        /// <param name="options">options.</param>
        public BavasRepository(IConfiguration config, IOptions<DBSettings> options)
            : base(config)
        {
            this.options = options;
            SqlHelper.SetConnectionString(this.options.Value.ConnectionString);
        }

        /// <summary>
        /// InsertNewNode.
        /// </summary>
        /// <param name="node">node.</param>
        /// <returns>Value.</returns>
        public int InsertNewNode(Node node)
        {
            if (node.District_id != 0 && node.Block_id != 0 && node.Panchayat_id != 0 && !string.IsNullOrEmpty(node.Node_name) && node.Rec_created_userid != 0)
            {
                List<DbParameter> dbParams = new List<DbParameter>();
                dbParams.Add(new SqlParameter { ParameterName = "@district_id", Value = node.District_id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                dbParams.Add(new SqlParameter { ParameterName = "@block_id", Value = node.Block_id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                dbParams.Add(new SqlParameter { ParameterName = "@panchayat_id", Value = node.Panchayat_id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                dbParams.Add(new SqlParameter { ParameterName = "@node_name", Value = node.Node_name, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbParams.Add(new SqlParameter { ParameterName = "@rec_created_userid", Value = node.Rec_created_userid, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                dbParams.Add(new SqlParameter { ParameterName = "@node_id", Value = node.Node_id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });

                Dictionary<string, dynamic> result = SqlHelper.ExecuteNonQuery<SqlConnection>(spInsertNewNode, dbParams, SqlHelper.ExecutionType.Procedure);

                int insertRowsCount = 0;

                insertRowsCount += result["RowsAffected"];

                return insertRowsCount;
            }
            else
            {
                return -1;
            }
        }

        /// <summary>
        /// InsertBavasStructure.
        /// </summary>
        /// <param name="structureBavas">structureBavas.</param>
        /// <returns>Value.</returns>
        public int InsertBavasStructure(List<StructureBavas> structureBavas)
        {
            int insertRowsCount = 0;
            if (structureBavas != null && structureBavas.Any())
            {
                foreach (var structure in structureBavas)
                {
                    List<DbParameter> dbParams = new List<DbParameter>();
                    dbParams.Add(new SqlParameter { ParameterName = "@district_id", Value = structure.District_id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                    dbParams.Add(new SqlParameter { ParameterName = "@Structure_name", Value = structure.Structure_name, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                    dbParams.Add(new SqlParameter { ParameterName = "@structure_desc", Value = structure.Structure_description, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                    dbParams.Add(new SqlParameter { ParameterName = "@Is_Name_Mandatory_flg", Value = structure.Namemandatoryflag, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                    dbParams.Add(new SqlParameter { ParameterName = "@Is_Addr_Mandatory_flg", Value = structure.Addressmandatoryflag, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                    dbParams.Add(new SqlParameter { ParameterName = "@Is_Capacity_Mandatory_flg", Value = structure.Capacitymandatoryflag, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                    dbParams.Add(new SqlParameter { ParameterName = "@unit_Of_measure", Value = structure.UnitofMeasure, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                    dbParams.Add(new SqlParameter { ParameterName = "@rec_created_userid", Value = structure.ReccreateduserId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });

                    Dictionary<string, dynamic> result = SqlHelper.ExecuteNonQuery<SqlConnection>(spInsertBavasStructure, dbParams, SqlHelper.ExecutionType.Procedure);

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
        /// InsertBavasCropsIntelligence.
        /// </summary>
        /// <param name="intelligenceCrops">intelligenceCrops.</param>
        /// <returns>Value.</returns>
        public int InsertBavasCropsIntelligence(List<BavasIntelligenceCrops> intelligenceCrops)
        {
            int insertRowsCount = 0;

            if (intelligenceCrops != null && intelligenceCrops.Any())
            {
                foreach (var intelligenceCrop in intelligenceCrops)
                {
                    if (intelligenceCrop.District_id != 0 && !string.IsNullOrEmpty(intelligenceCrop.Crop_name) && intelligenceCrop.Reccreateduserid != 0)
                    {
                        List<DbParameter> dbParams = new List<DbParameter>();
                        dbParams.Add(new SqlParameter { ParameterName = "@district_id", Value = intelligenceCrop.District_id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                        dbParams.Add(new SqlParameter { ParameterName = "@Crop_ID", Value = intelligenceCrop.Crop_id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                        dbParams.Add(new SqlParameter { ParameterName = "@crop_category", Value = intelligenceCrop.Crop_category, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                        dbParams.Add(new SqlParameter { ParameterName = "@Crop_name", Value = intelligenceCrop.Crop_name, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                        dbParams.Add(new SqlParameter { ParameterName = "@rec_created_userid", Value = intelligenceCrop.Reccreateduserid, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                        Dictionary<string, dynamic> result = SqlHelper.ExecuteNonQuery<SqlConnection>(spInsertBavasCropsIntelligence, dbParams, SqlHelper.ExecutionType.Procedure);

                        insertRowsCount = insertRowsCount + result["RowsAffected"];
                    }
                }

                return insertRowsCount;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// InsertBavasPriceIntelligence.
        /// </summary>
        /// <param name="intelligencePrice">intelligencePrice.</param>
        /// <returns>Value.</returns>
        public int InsertBavasPriceIntelligence(List<DtoPriceIntelligenceInsert> intelligencePrice)
        {
            int insertRowsCount = 0;

            if (intelligencePrice != null && intelligencePrice.Any())
            {
                foreach (var intelligence in intelligencePrice)
                {
                    if (intelligence.PanchayatId != 0 && intelligence.NodeId != 0 && !string.IsNullOrEmpty(intelligence.NodeName) && intelligence.CropId != 0 && !string.IsNullOrEmpty(intelligence.CropName) && intelligence.ReccreatedUserId != 0)
                    {
                        List<DbParameter> dbParams = new List<DbParameter>();
                        dbParams.Add(new SqlParameter { ParameterName = "@panchayat_id", Value = intelligence.PanchayatId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                        dbParams.Add(new SqlParameter { ParameterName = "@reported_date", Value = intelligence.ReportedDate, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
                        dbParams.Add(new SqlParameter { ParameterName = "@node_id", Value = intelligence.NodeId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                        dbParams.Add(new SqlParameter { ParameterName = "@node_name", Value = intelligence.NodeName, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                        dbParams.Add(new SqlParameter { ParameterName = "@Crop_ID", Value = intelligence.CropId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                        dbParams.Add(new SqlParameter { ParameterName = "@Crop_name", Value = intelligence.CropName, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                        dbParams.Add(new SqlParameter { ParameterName = "@rate_in_quintal", Value = intelligence.RateInQuintal, SqlDbType = SqlDbType.Decimal, Direction = ParameterDirection.Input });
                        dbParams.Add(new SqlParameter { ParameterName = "@rec_created_userid", Value = intelligence.ReccreatedUserId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                        dbParams.Add(new SqlParameter { ParameterName = "@rec_created_date", Value = intelligence.RecCreatedDate, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
                        Dictionary<string, dynamic> result = SqlHelper.ExecuteNonQuery<SqlConnection>(spInsertBavasPriceIntelligence, dbParams, SqlHelper.ExecutionType.Procedure);

                        insertRowsCount += result["RowsAffected"];
                    }
                }
            }

            return insertRowsCount;
        }

        /// <summary>
        /// GetUnitofMeasure.
        /// </summary>
        /// <returns>List.</returns>
        public List<MeasureBavas> GetUnitofMeasure()
        {
            List<MeasureBavas> list = new List<MeasureBavas>();

            List<DbParameter> parameters = new List<DbParameter>();
            parameters.Add(new SqlParameter { ParameterName = "@query_name", Value = qnGetUnitofMeasure, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(spGetUnitofMeasure, parameters, SqlHelper.ExecutionType.Procedure);
            if (dt != null && dt.Rows.Count > 0)
            {
                list = SqlHelper.ConvertDataTableToList<MeasureBavas>(dt);
            }

            return list;
        }

        /// <summary>
        /// GetAllPriceIntelligenceCrops.
        /// </summary>
        /// <param name="districtId">districtId.</param>
        /// <returns>List.</returns>
        public List<DtoPriceIntelligenceCrops> GetAllPriceIntelligenceCrops(int districtId)
        {
            List<DtoPriceIntelligenceCrops> list = new List<DtoPriceIntelligenceCrops>();

            List<DbParameter> parameters = new List<DbParameter>();

            parameters.Add(new SqlParameter { ParameterName = "@query_name", Value = qnGetAllPriceIntelligenceCrops, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@district_id", Value = districtId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(spGetUnitofMeasure, parameters, SqlHelper.ExecutionType.Procedure);
            if (dt != null && dt.Rows.Count > 0)
            {
                list = SqlHelper.ConvertDataTableToList<DtoPriceIntelligenceCrops>(dt).GroupBy(x => new { x.Crop_id, x.District_id }).Select(x => x.First()).ToList();
            }

            return list;
        }

        /// <summary>
        /// GetPriceIntelligenceDetailsforPastSevenDays.
        /// </summary>
        /// <param name="panchayatId">panchayatId.</param>
        /// <returns>List.</returns>
        public List<PriceIntelligenceDetails> GetPriceIntelligenceDetailsforPastSevenDays(int panchayatId)
        {
            List<PriceIntelligenceDetails> list = new List<PriceIntelligenceDetails>();

            List<DbParameter> parameters = new List<DbParameter>();

            parameters.Add(new SqlParameter { ParameterName = "@query_name", Value = qnGetPriceIntelligenceDetailsforPastSevenDays, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@panchayat_id", Value = panchayatId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(spGetUnitofMeasure, parameters, SqlHelper.ExecutionType.Procedure);
            if (dt != null && dt.Rows.Count > 0)
            {
                list = SqlHelper.ConvertDataTableToList<PriceIntelligenceDetails>(dt);
            }

            return list;
        }

        /// <summary>
        /// PostBavasContractForming.
        /// </summary>
        /// <param name="insBavasContractfarmings">insBavasContractfarmings.</param>
        /// <returns>Value.</returns>
        public int PostBavasContractForming(List<InsBavasContractfarming> insBavasContractfarmings)
        {
            int insertRowsCount = 0;
            foreach (var insBavasContractfarming in insBavasContractfarmings)
            {
                List<DbParameter> dbparams = new List<DbParameter>();
                dbparams.Add(new SqlParameter { ParameterName = "@season_id", Value = insBavasContractfarming.Season_id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                dbparams.Add(new SqlParameter { ParameterName = "@panchayat_id", Value = insBavasContractfarming.Panchayat_id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                dbparams.Add(new SqlParameter { ParameterName = "@farmer_Name", Value = insBavasContractfarming.Farmer_Name, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparams.Add(new SqlParameter { ParameterName = "@farmer_dbt_reg_no", Value = insBavasContractfarming.Farmer_dbt_reg_no, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparams.Add(new SqlParameter { ParameterName = "@old_farmer_dbt_reg_no", Value = insBavasContractfarming.Old_farmer_dbt_reg_no, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparams.Add(new SqlParameter { ParameterName = "@farmer_phone_num", Value = insBavasContractfarming.Farmer_phone_num, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparams.Add(new SqlParameter { ParameterName = "@Interested_In_Contract_flg", Value = insBavasContractfarming.Interested_In_Contract_flg, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparams.Add(new SqlParameter { ParameterName = "@Crop_ID", Value = insBavasContractfarming.Crop_ID, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparams.Add(new SqlParameter { ParameterName = "@rec_created_userid", Value = insBavasContractfarming.Rec_created_userid, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                dbparams.Add(new SqlParameter { ParameterName = "@rec_updated_userid", Value = insBavasContractfarming.Rec_updated_userid, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                Dictionary<string, dynamic> result = SqlHelper.ExecuteNonQuery<SqlConnection>(spPostBavasContractForming, dbparams, SqlHelper.ExecutionType.Procedure);

                insertRowsCount += result["RowsAffected"];
            }

            return insertRowsCount;
        }

        /// <summary>
        /// GetAllNodesByDistrictId.
        /// </summary>
        /// <param name="districtId">districtId.</param>
        /// <returns>List.</returns>
        public List<DtoNodes> GetAllNodesByDistrictId(int districtId)
        {
            List<DtoNodes> list = new List<DtoNodes>();

            List<DbParameter> parameters = new List<DbParameter>();
            parameters.Add(new SqlParameter { ParameterName = "@query_name", Value = qnGetAllNodesByDistrictId, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@district_id", Value = districtId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });

            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(spGetUnitofMeasure, parameters, SqlHelper.ExecutionType.Procedure);
            if (dt != null && dt.Rows.Count > 0)
            {
                list = SqlHelper.ConvertDataTableToList<DtoNodes>(dt).GroupBy(x => x.Node_id).Select(x => x.First()).ToList();
            }

            return list;
        }

        /// <summary>
        /// GetAllStructureByDistrictId.
        /// </summary>
        /// <param name="districtId">districtId.</param>
        /// <returns>List.</returns>
        public List<DtoStructure> GetAllStructureByDistrictId(int districtId)
        {
            List<DtoStructure> list = new List<DtoStructure>();

            List<DbParameter> parameters = new List<DbParameter>();

            parameters.Add(new SqlParameter { ParameterName = "@query_name", Value = qnGetAllStructureByDistrictId, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@district_id", Value = districtId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });

            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(spGetUnitofMeasure, parameters, SqlHelper.ExecutionType.Procedure);
            if (dt != null && dt.Rows.Count > 0)
            {
                list = SqlHelper.ConvertDataTableToList<DtoStructure>(dt);
            }

            return list;
        }

        /// <summary>
        /// GetAgriHortiCrops.
        /// </summary>
        /// <returns>list.</returns>
        public List<AgriHortiCrops> GetAgriHortiCrops()
        {
            List<AgriHortiCrops> list = new List<AgriHortiCrops>();

            List<DbParameter> parameters = new List<DbParameter>();

            parameters.Add(new SqlParameter { ParameterName = "@query_name", Value = qnGetAgriHortiCrops, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });

            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(spGetUnitofMeasure, parameters, SqlHelper.ExecutionType.Procedure);
            if (dt != null && dt.Rows.Count > 0)
            {
                list = SqlHelper.ConvertDataTableToList<AgriHortiCrops>(dt);
            }

            return list;
        }

        /// <summary>
        /// GetAllFarmerProducerOrgByPanchayat.
        /// </summary>
        /// <param name="panchayatid">panchayatid.</param>
        /// <returns>List.</returns>
        public List<DtoFpo> GetAllFarmerProducerOrgByPanchayat(int panchayatid)
        {
            List<DtoFpo> list = new List<DtoFpo>();

            List<DbParameter> parameters = new List<DbParameter>();

            parameters.Add(new SqlParameter { ParameterName = "@query_name", Value = qnGetAllFarmerProducerOrgByPanchayat, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@panchayat_id", Value = panchayatid, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(spGetUnitofMeasure, parameters, SqlHelper.ExecutionType.Procedure);

            if (dt != null && dt.Rows.Count > 0)
            {
                list = SqlHelper.ConvertDataTableToList<DtoFpo>(dt).GroupBy(x => new { x.Fpo_id, x.Panchayat_id }).Select(x => x.First()).ToList();
                List<DtoCropsFpo> crops = SqlHelper.ConvertDataTableToList<DtoCropsFpo>(dt);

                foreach (var item in list)
                {
                    var crop = crops.Where(x => x.Fpo_id == item.Fpo_id && x.Panchayat_id == item.Panchayat_id).Select(x => x.Crop_Id).ToList();
                    item.Crops = string.Join(",", crop);
                }
            }

            return list;
        }

        /// <summary>
        /// GetSpecificFarmerProducerOrg.
        /// </summary>
        /// <param name="fpoId">fpoId.</param>
        /// <returns>DtoFpoCrops Model Value.</returns>
        public DtoFpoCrops GetSpecificFarmerProducerOrg(int fpoId)
        {
            DtoFpoCrops fpoCrops = new DtoFpoCrops();

            List<DbParameter> parameters = new List<DbParameter>();

            parameters.Add(new SqlParameter { ParameterName = "@query_name", Value = qnGetSpecificFarmerProducerOrg, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@fpo_id", Value = fpoId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });

            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(spGetUnitofMeasure, parameters, SqlHelper.ExecutionType.Procedure);
            if (dt != null && dt.Rows.Count > 0)
            {
                fpoCrops = SqlHelper.ConvertDataTableToList<DtoFpoCrops>(dt)[0];

                fpoCrops.Crops = SqlHelper.ConvertDataTableToList<CropsDtofpo>(dt).GroupBy(x => x.Crop_Id).Select(x => x.First()).ToList();
            }

            return fpoCrops;
        }

        /// <summary>
        /// GetAllContractFarmingDetails.
        /// </summary>
        /// <param name="panchaytId">panchaytId.</param>
        /// <param name="seasonId">seasonId.</param>
        /// <returns>List.</returns>
        public List<DtoFarmerContractingDetails> GetAllContractFarmingDetails(int panchaytId, int seasonId)
        {
            List<DtoFarmerContractingDetails> farmercontractCrops = new List<DtoFarmerContractingDetails>();

            List<DbParameter> parameters = new List<DbParameter>();

            parameters.Add(new SqlParameter { ParameterName = "@query_name", Value = qnGetAllContractFarmingDetails, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@panchayat_id", Value = panchaytId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@season_id", Value = seasonId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(spGetUnitofMeasure, parameters, SqlHelper.ExecutionType.Procedure);
            if (dt != null && dt.Rows.Count > 0)
            {
                farmercontractCrops = SqlHelper.ConvertDataTableToList<DtoFarmerContractingDetails>(dt).GroupBy(x => x.Farmer_dbt_reg_no).Select(x => x.First()).ToList();

                List<DtoFarmerContractCropsreg> dTOCropsTemp = SqlHelper.ConvertDataTableToList<DtoFarmerContractCropsreg>(dt);

                foreach (var item in farmercontractCrops)
                {
                    item.Crops = dTOCropsTemp.Where(x => x.Farmer_dbt_reg_no == item.Farmer_dbt_reg_no).Select(x => new DtoFarmerContractCrops { Crop_id = x.Crop_id, Crop_category = x.Crop_category, Crop_name = x.Crop_name, Crop_type = x.Crop_type }).ToList();
                }
            }

            return farmercontractCrops;
        }

        /// <summary>
        /// GetSpecificContractFarmingDetail.
        /// </summary>
        /// <param name="regno">regno.</param>
        /// <returns>DtoFarmerContractingDetails.</returns>
        public DtoFarmerContractingDetails GetSpecificContractFarmingDetail(string regno)
        {
            DtoFarmerContractingDetails farmercontractCrops = new DtoFarmerContractingDetails();
            List<DbParameter> parameters = new List<DbParameter>();

            parameters.Add(new SqlParameter { ParameterName = "@query_name", Value = qnGetSpecificContractFarmingDetail, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@farmer_dbt_reg_no", Value = regno, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(spGetUnitofMeasure, parameters, SqlHelper.ExecutionType.Procedure);

            if (dt != null && dt.Rows.Count > 0)
            {
                farmercontractCrops = SqlHelper.ConvertDataTableToList<DtoFarmerContractingDetails>(dt)[0];
                List<DtoFarmerContractCropsreg> DTOCropsTemp = SqlHelper.ConvertDataTableToList<DtoFarmerContractCropsreg>(dt);
                farmercontractCrops.Crops = DTOCropsTemp.Where(x => x.Farmer_dbt_reg_no == regno).Select(x => new DtoFarmerContractCrops { Crop_id = x.Crop_id, Crop_category = x.Crop_category, Crop_name = x.Crop_name, Crop_type = x.Crop_type }).ToList();
            }

            return farmercontractCrops;
        }

        /// <summary>
        /// GETSEASONALCropsByDistrictId.
        /// </summary>
        /// <param name="districtId">districtId.</param>
        /// <returns>List.</returns>
        public List<DtoSeasonalCrops> GETSEASONALCropsByDistrictId(int districtId)
        {
            List<DtoSeasonalCrops> list = new List<DtoSeasonalCrops>();

            List<DbParameter> parameters = new List<DbParameter>();

            parameters.Add(new SqlParameter { ParameterName = "@query_name", Value = qnGETSEASONALCropsByDistrictId, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@district_id", Value = districtId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(spGetUnitofMeasure, parameters, SqlHelper.ExecutionType.Procedure);

            if (dt != null && dt.Rows.Count > 0)
            {
                list = SqlHelper.ConvertDataTableToList<DtoSeasonalCrops>(dt);
            }

            return list;
        }

        /// <summary>
        /// InsertBavasfarmerproducerOrg.
        /// </summary>
        /// <param name="farmerProduceOrgs">farmerProduceOrgs.</param>
        /// <returns>FpoResponse List Values.</returns>
        public List<FpoResponse> InsertBavasfarmerproducerOrg(List<FarmerProduceOrg> farmerProduceOrgs)
        {
            List<FpoResponse> response = new List<FpoResponse>();
            if (farmerProduceOrgs != null && farmerProduceOrgs.Any())
            {
                foreach (var fpo in farmerProduceOrgs)
                {
                    List<DbParameter> dbParams = new List<DbParameter>();

                    dbParams.Add(new SqlParameter { ParameterName = "@district_id", Value = fpo.District_id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                    dbParams.Add(new SqlParameter { ParameterName = "@panchayat_id", Value = fpo.Panchayat_id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                    dbParams.Add(new SqlParameter { ParameterName = "@fpo_id", Value = fpo.Fpo_id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                    dbParams.Add(new SqlParameter { ParameterName = "@fpo_name", Value = fpo.Fpo_name, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                    dbParams.Add(new SqlParameter { ParameterName = "@fpo_contact_person", Value = fpo.Fpo_contact_person, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                    dbParams.Add(new SqlParameter { ParameterName = "@fpo_phone_num", Value = fpo.Fpo_phone_num, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                    dbParams.Add(new SqlParameter { ParameterName = "@type", Value = fpo.Type, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                    dbParams.Add(new SqlParameter { ParameterName = "@Crop_ID", Value = fpo.Crop_ID, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                    dbParams.Add(new SqlParameter { ParameterName = "@Interested_In_Contract_flg", Value = fpo.Interested_In_Contract_flg, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                    dbParams.Add(new SqlParameter { ParameterName = "@rec_created_userid", Value = fpo.Rec_created_userid == 0 ? DBNull.Value : (object)fpo.Rec_created_userid, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                    dbParams.Add(new SqlParameter { ParameterName = "@rec_created_date", Value = fpo.Rec_created_date == DateTime.MinValue ? DBNull.Value : (object)fpo.Rec_created_date, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
                    dbParams.Add(new SqlParameter { ParameterName = "@rec_updated_userid", Value = fpo.Rec_updated_userid == 0 ? DBNull.Value : (object)fpo.Rec_updated_userid, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                    dbParams.Add(new SqlParameter { ParameterName = "@rec_updated_date", Value = fpo.Rec_updated_date == DateTime.MinValue ? DBNull.Value : (object)fpo.Rec_updated_date, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
                    DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(spInsertBavasfarmerproducerOrg, dbParams, SqlHelper.ExecutionType.Procedure);

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        string gdata = dt.Rows[0]["Column1"].ToString();

                        if (gdata == "I")
                        {
                            response.Add(new FpoResponse { Fponame = fpo.Fpo_name, Status = "I" });
                        }
                        else if (gdata == "U")
                        {
                            response.Add(new FpoResponse { Fponame = fpo.Fpo_name, Status = "U" });
                        }
                        else if (gdata == "S")
                        {
                            response.Add(new FpoResponse { Fponame = fpo.Fpo_name, Status = "S" });
                        }
                        else
                        {
                            response.Add(new FpoResponse { Fponame = fpo.Fpo_name, Status = "NR" });
                        }
                    }
                }

                return response;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// GetAllFacilityDetails.
        /// </summary>
        /// <param name="structureId">structureId.</param>
        /// <param name="panchayatId">panchayatId.</param>
        /// <returns>List.</returns>
        public List<DtoFacilityDetails> GetAllFacilityDetails(int structureId, int panchayatId)
        {
            List<DtoFacilityDetails> list = new List<DtoFacilityDetails>();

            List<DbParameter> parameters = new List<DbParameter>();

            parameters.Add(new SqlParameter { ParameterName = "@query_name", Value = qnGetAllFacilityDetails, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@structure_id", Value = structureId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@panchayat_id", Value = panchayatId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(spGetUnitofMeasure, parameters, SqlHelper.ExecutionType.Procedure);

            if (dt != null && dt.Rows.Count > 0)
            {
                list = SqlHelper.ConvertDataTableToList<DtoFacilityDetails>(dt);
            }

            return list;
        }

        /// <summary>
        /// GetDirectorMarketingInfraStructure.
        /// </summary>
        /// <param name="structureId">structureId.</param>
        /// <param name="panchayatId">panchayatId.</param>
        /// <returns>List.</returns>
        public List<MarkettingInfrastructure> GetDirectorMarketingInfraStructure(int structureId, int panchayatId)
        {
            List<MarkettingInfrastructure> list = new List<MarkettingInfrastructure>();

            List<DbParameter> parameters = new List<DbParameter>();
            parameters.Add(new SqlParameter { ParameterName = "@query_name", Value = qnGetDirectorMarketingInfraStructure, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@structure_id", Value = structureId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@panchayat_id", Value = panchayatId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(spGetUnitofMeasure, parameters, SqlHelper.ExecutionType.Procedure);

            if (dt != null && dt.Rows.Count > 0)
            {
                list = SqlHelper.ConvertDataTableToList<MarkettingInfrastructure>(dt);
            }

            return list;
        }

        /// <summary>
        /// InsertBavasMarkettingInfra.
        /// </summary>
        /// <param name="markettingInfra">markettingInfra.</param>
        /// <returns>Values.</returns>
        public int InsertBavasMarkettingInfra(List<DtoMarketingInfra> markettingInfra)
        {
            int insertRowsCount = 0;
            if (markettingInfra != null && markettingInfra.Any())
            {
                foreach (var markInfra in markettingInfra)
                {
                    List<DbParameter> dbParams = new List<DbParameter>();
                    dbParams.Add(new SqlParameter { ParameterName = "@facility_ID", Value = markInfra.Facility_ID, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                    dbParams.Add(new SqlParameter { ParameterName = "@Structure_Id", Value = markInfra.Structure_Id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                    dbParams.Add(new SqlParameter { ParameterName = "@panchayat_id", Value = markInfra.Panchayat_id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                    dbParams.Add(new SqlParameter { ParameterName = "@old_facility_name", Value = markInfra.Old_facility_name, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                    dbParams.Add(new SqlParameter { ParameterName = "@old_address", Value = markInfra.Old_address, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                    dbParams.Add(new SqlParameter { ParameterName = "@old_capacity", Value = markInfra.Old_capacity, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                    dbParams.Add(new SqlParameter { ParameterName = "@facility_name", Value = markInfra.Facility_name, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                    dbParams.Add(new SqlParameter { ParameterName = "@facility_add", Value = markInfra.Facility_add, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                    dbParams.Add(new SqlParameter { ParameterName = "@capacity", Value = markInfra.Capacity, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                    dbParams.Add(new SqlParameter { ParameterName = "@Approval_status", Value = markInfra.Approval_status, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                    dbParams.Add(new SqlParameter { ParameterName = "@Is_modified", Value = markInfra.Is_modified, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                    dbParams.Add(new SqlParameter { ParameterName = "@is_facility_added", Value = markInfra.Is_Facility_Added, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                    dbParams.Add(new SqlParameter { ParameterName = "@period", Value = markInfra.Period, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                    dbParams.Add(new SqlParameter { ParameterName = "@rec_created_userid", Value = markInfra.Rec_created_userid == 0 ? DBNull.Value : (object)markInfra.Rec_created_userid, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                    dbParams.Add(new SqlParameter { ParameterName = "@rec_updated_userid", Value = markInfra.Rec_updated_userid == 0 ? DBNull.Value : (object)markInfra.Rec_updated_userid, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                    dbParams.Add(new SqlParameter { ParameterName = "@response_status", SqlDbType = SqlDbType.VarChar, Size = 50, Direction = ParameterDirection.Output });
                    Dictionary<string, dynamic> result = SqlHelper.ExecuteNonQuery<SqlConnection>(spInsertBavasMarkettingInfra, dbParams, SqlHelper.ExecutionType.Procedure);
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
        /// GetDirectorMarketingInfraStructureByDistrict.
        /// </summary>
        /// <param name="structureId">structureId.</param>
        /// <param name="districtId">districtId.</param>
        /// <returns>List.</returns>
        public List<MarkettingInfrastructure> GetDirectorMarketingInfraStructureByDistrict(int structureId, int districtId)
        {
            List<MarkettingInfrastructure> list = new List<MarkettingInfrastructure>();

            List<DbParameter> parameters = new List<DbParameter>();

            parameters.Add(new SqlParameter { ParameterName = "@query_name", Value = qnGetDirectorMarketingInfraStructureByDistrict, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@structure_id", Value = structureId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@district_id", Value = districtId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(spGetUnitofMeasure, parameters, SqlHelper.ExecutionType.Procedure);

            if (dt != null && dt.Rows.Count > 0)
            {
                list = SqlHelper.ConvertDataTableToList<MarkettingInfrastructure>(dt);
            }

            return list;
        }

        /// <summary>
        /// GetMarketingInfoNoFacilityData.
        /// </summary>
        /// <param name="blockid">blockid.</param>
        /// <returns>List.</returns>
        public List<MarketInfoData> GetMarketingInfoNoFacilityData(int blockid)
        {
            List<MarketInfoData> list = new List<MarketInfoData>();

            List<DbParameter> parameters = new List<DbParameter>();

            parameters.Add(new SqlParameter { ParameterName = "@query_name", Value = qnGetMarketingInfoNoFacilityData, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@block_id", Value = blockid, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });

            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(spGetUnitofMeasure, parameters, SqlHelper.ExecutionType.Procedure);
            if (dt != null && dt.Rows.Count > 0)
            {
                list = SqlHelper.ConvertDataTableToList<MarketInfoData>(dt);
            }

            return list;
        }
    }
}
