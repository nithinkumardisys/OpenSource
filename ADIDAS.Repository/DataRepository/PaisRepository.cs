//------------------------------------------------------------------------------
// <copyright file="PaisRepository.cs" company="Government of Bihar">
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
    using System.Text;
    using ADIDAS.Model.DTO;
    using ADIDAS.Repository.Helper;
    using ADIDAS.Repository.Interfaces;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Options;

    /// <summary>
    /// PaisRepository.
    /// </summary>
    public class PaisRepository : BaseRepository, IPaisRepository
    {
        private readonly IOptions<DBSettings> options;

        /// <summary>
        /// Initializes a new instance of the <see cref="PaisRepository"/> class.
        /// </summary>
        /// <param name="config">config</param>
        /// <param name="options">options</param>
        public PaisRepository(IConfiguration config, IOptions<DBSettings> options)
            : base(config)
        {
            this.options = options;
            SqlHelper.SetConnectionString(this.options.Value.ConnectionString);
        }

        /// <summary>
        /// Populate all markets based on the user id.
        /// </summary>
        /// <param name="userId">userId.</param>
        /// <returns>List.</returns>
        public List<PaisMarketsEntity> GetMarket(string userId)
        {
            List<PaisMarketsEntity> paisMarketsEntity = new List<PaisMarketsEntity>();
            List<DbParameter> dbparams = new List<DbParameter>();
            dbparams.Add(new SqlParameter { ParameterName = "@query_name", Value = "GetMarket", SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@User_Id", Value = userId, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>("usp_getdata_PAIS_local_preferences", dbparams, SqlHelper.ExecutionType.Procedure);
            if (dt != null && dt.Rows.Count > 0)
            {
                paisMarketsEntity = SqlHelper.ConvertDataTableToList<PaisMarketsEntity>(dt);
            }

            return paisMarketsEntity;
        }

        /// <summary>
        /// Populate all commodity Group.
        /// </summary>
        /// <param name="userId">userId.</param>
        /// <returns>List</returns
        public List<PaisCommodityGroupEntity> GetCommodityGroup()
        {
            List<PaisCommodityGroupEntity> paisCommodityGroupEntity = new List<PaisCommodityGroupEntity>();
            List<DbParameter> dbparams = new List<DbParameter>();
            dbparams.Add(new SqlParameter { ParameterName = "@query_name", Value = "GetCommodityGroup", SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>("usp_getdata_PAIS_local_preferences", dbparams, SqlHelper.ExecutionType.Procedure);
            if (dt != null && dt.Rows.Count > 0)
            {
                paisCommodityGroupEntity = SqlHelper.ConvertDataTableToList<PaisCommodityGroupEntity>(dt);
            }

            return paisCommodityGroupEntity;
        }

        /// <summary>
        /// Populate all commodity details filtered with commodityGroupId.
        /// </summary>
        /// <param name="commodityGroupId">commodityGroupId.</param>
        /// <returns>List</returns>
        public List<PaisCommodityEntity> GetCommodity(string commodityGroupId)
        {
            List<PaisCommodityEntity> paisCommodityEntity = new List<PaisCommodityEntity>();
            List<DbParameter> dbparams = new List<DbParameter>();
            dbparams.Add(new SqlParameter { ParameterName = "@query_name", Value = "GetCommodity", SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@Cmdt_Grp_Id", Value = commodityGroupId, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });

            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>("usp_getdata_PAIS_local_preferences", dbparams, SqlHelper.ExecutionType.Procedure);
            if (dt != null && dt.Rows.Count > 0)
            {
                paisCommodityEntity = SqlHelper.ConvertDataTableToList<PaisCommodityEntity>(dt);
            }

            return paisCommodityEntity;
        }

        /// <summary>
        /// Populate all Units.
        /// </summary>
        /// <returns>List</returns>
        public List<PaisUnit> GetUnits()
        {
            List<PaisUnit> paisUnits = new List<PaisUnit>();
            List<DbParameter> dbparams = new List<DbParameter>();
            dbparams.Add(new SqlParameter { ParameterName = "@query_name", Value = "GetUnit", SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>("usp_getdata_PAIS_local_preferences", dbparams, SqlHelper.ExecutionType.Procedure);
            if (dt != null && dt.Rows.Count > 0)
            {
                paisUnits = SqlHelper.ConvertDataTableToList<PaisUnit>(dt);
            }

            return paisUnits;
        }
        /// <summary>
        /// Populate all variety based on Commidity Group and commodity id.
        /// </summary>
        /// <param name="commodityGroupId">commodityGroupId.</param>
        /// <param name="commodityId">commodityId.</param>
        /// <returns>List</returns>
        public List<PaisVariety> GetVariety(string commodityGroupId, string commodityId)
        {
            List<PaisVariety> paisVarieties = new List<PaisVariety>();
            List<DbParameter> dbparams = new List<DbParameter>();
            dbparams.Add(new SqlParameter { ParameterName = "@query_name", Value = "GetVariety", SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@Cmdt_Grp_Id", Value = commodityGroupId, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@Cmdt_Id", Value = commodityId, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });

            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>("usp_getdata_PAIS_local_preferences", dbparams, SqlHelper.ExecutionType.Procedure);
            if (dt != null && dt.Rows.Count > 0)
            {
                paisVarieties = SqlHelper.ConvertDataTableToList<PaisVariety>(dt);
            }

            return paisVarieties;
        }

        /// <summary>
        /// Get inserted data from database by using market id and commodity group id.
        /// </summary>
        /// <param name="marketId">marketId.</param>
        /// <param name="commodityGroupId">commodityGroupId.</param>
        /// <returns>List</returns>
        public PaisLocalPrefencesInfo GetSubmittedData(string marketId, string commodityGroupId)
        {
            PaisLocalPrefencesInfo paisLocalPrefencesInfo = new PaisLocalPrefencesInfo();
            List<DbParameter> dbparams = new List<DbParameter>();
            dbparams.Add(new SqlParameter { ParameterName = "@query_name", Value = "GetSubmittedData", SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@Market_Id", Value = marketId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@Cmdt_Grp_Id", Value = commodityGroupId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });

            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>("usp_getdata_PAIS_local_preferences", dbparams, SqlHelper.ExecutionType.Procedure);

            if (dt != null && dt.Rows.Count > 0)
            {
                // Collecting distinct value of Market information from dataset and loading the same into the below class object
                 paisLocalPrefencesInfo = (from DataRow dRow in dt.Rows
                                          select new
                                          {
                                              marketId = dRow["mkt_id"] == null ? 0 : dRow["mkt_id"],
                                              marketName = dRow["mkt_name"] == null ? string.Empty : dRow["mkt_name"],
                                              commodityGroupId = dRow["cmdt_grp_id"] == null ? 0 : dRow["cmdt_grp_id"],
                                              commodityGroupName = dRow["cmdt_grp_name"] == null ? string.Empty : dRow["cmdt_grp_name"],
                                              userId = dRow["userid"] == null ? 0 : dRow["userid"]
                                          }).Distinct().
                                    Select(x => new PaisLocalPrefencesInfo()
                                    {
                                        MarketId = Convert.ToInt16(x.marketId),
                                        MarketName = x.marketName.ToString(),
                                        CommodityGroupId = Convert.ToInt16(x.commodityGroupId),
                                        CommodityGroupName = x.commodityGroupName.ToString(),
                                        UserId = Convert.ToInt16(x.userId),
                                    }).FirstOrDefault();

                // Collecting distinct values of commodity details from dataset and loading same into below object.
                paisLocalPrefencesInfo.CommodityDetails = (from DataRow dRow in dt.Rows
                                                           select new
                                                           {
                                                               commodityId = dRow["cmdt_id"] == null ? 0 : dRow["cmdt_id"],
                                                               commodityName = dRow["cmdt_name"] == null ? string.Empty : dRow["cmdt_name"],
                                                               arrivalUnitId = dRow["Arrival_unit_id"] == null ? 0 : dRow["Arrival_unit_id"],
                                                               arrivalCompValue = dRow["Arrival_comp_value"] == null ? 0 : dRow["Arrival_comp_value"],
                                                               arrivalUnitName = dRow["Arrival_unit_name"] == null ? string.Empty : dRow["Arrival_unit_name"],
                                                           }).Distinct().
                                    Select(x => new CommodityDetail
                                    {
                                        CommodityId = Convert.ToInt16(x.commodityId),
                                        CommodityName = x.commodityName.ToString(),
                                        ArrivalUnitId = Convert.ToInt16(x.arrivalUnitId),
                                        ArrivalCompValue = Convert.ToDecimal(x.arrivalCompValue),
                                        ArrivalUnitName = x.arrivalUnitName?.ToString(),
                                    }).ToList();

                // Now, loading related variety details based on already collected commodity details by using the concept of Foreach,
                // this Foreach iterating commodity details one by one and get the varietry details.
                paisLocalPrefencesInfo.CommodityDetails.ForEach(x =>
                {
                    x.Varieties = dt.AsEnumerable().Where(v => v.Field<int>("mkt_id").ToString() == marketId &&
                    v.Field<int>("cmdt_grp_id").ToString() == commodityGroupId &&
                    v.Field<int>("cmdt_id") == x.CommodityId &&
                    v.Field<int>("Arrival_unit_id") == x.ArrivalUnitId).Select(x => new Variety
                    {
                        VarietyId = x.Field<int>("Variety_id"),
                        VarietyName = x.Field<string>("Variety_name"),
                        PriceCompValue = x.Field<decimal>("price_comp_value"),
                        PriceUnitId = x.Field<int>("price_unit_id"),
                        PriceUnitName = x.Field<string>("price_unit_name") == null ? string.Empty : x.Field<string>("price_unit_name"),
                    }).ToList();
                });
            }
            else
            {
                return null;
            }

            return paisLocalPrefencesInfo;
        }

        /// <summary>
        /// Populate the market related information for offline use.
        /// </summary>
        /// <param name="marketId">marketId</param>
        /// <returns>List</returns>
        public List<PaisLocalPrefencesInfo> GetSubmittedDataOffline(string marketId)
        {
            List<PaisLocalPrefencesInfo> paisLocalPrefencesInfo = new List<PaisLocalPrefencesInfo>();
            List<DbParameter> dbparams = new List<DbParameter>();
            dbparams.Add(new SqlParameter { ParameterName = "@query_name", Value = "GetSubmittedDataOffline", SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@Market_Id", Value = marketId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });

            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>("usp_getdata_PAIS_local_preferences", dbparams, SqlHelper.ExecutionType.Procedure);
            if (dt != null && dt.Rows.Count > 0)
            {
                // Collecting all markets details with different commodity groups if anything in dataset.
                paisLocalPrefencesInfo = (from DataRow dRow in dt.Rows
                                          select new
                                          {
                                              marketId = dRow["mkt_id"] == null ? "0" : dRow["mkt_id"],
                                              marketName = dRow["mkt_name"] == null ? string.Empty : dRow["mkt_name"],
                                              commodityGroupId = dRow["cmdt_grp_id"] == null ? "0" : dRow["cmdt_grp_id"],
                                              commodityGroupName = dRow["cmdt_grp_name"] == null ? string.Empty : dRow["cmdt_grp_name"],
                                              userId = dRow["userid"] == null ? 0 : dRow["userid"],
                                          }).Distinct().Select(x => new PaisLocalPrefencesInfo()
                                          {
                                              MarketId = Convert.ToInt16(x.marketId == null ? 0 : x.marketId),
                                              MarketName = Convert.ToString(x.marketName == null ? string.Empty : x.marketName),
                                              CommodityGroupId = Convert.ToInt16(x.commodityGroupId == null ? 0 : x.commodityGroupId),
                                              CommodityGroupName = Convert.ToString(x.commodityGroupName == null ? string.Empty : x.commodityGroupName),
                                              UserId = Convert.ToInt16(x.userId),
                                          }).ToList();

                // Iterate all commodity groups from the single market and populate related commodity details based on group into below object.
                paisLocalPrefencesInfo.ForEach(p =>
                {
                    p.CommodityDetails = (from DataRow dRow in dt.Rows
                                          where dRow["mkt_id"].ToString() == p.MarketId.ToString()
                                          & dRow["cmdt_grp_id"].ToString() == p.CommodityGroupId.ToString()
                                          select new
                                          {
                                              commodityId = dRow["cmdt_id"] == null ? "0" : dRow["cmdt_id"],
                                              commodityName = dRow["cmdt_name"] == null ? string.Empty : dRow["cmdt_name"],
                                              arrivalUnitId = dRow["Arrival_unit_id"] == null ? "0" : dRow["Arrival_unit_id"],
                                              arrivalCompValue = dRow["Arrival_comp_value"] == null ? "0" : dRow["Arrival_comp_value"],
                                              arrivalUnitName = dRow["Arrival_unit_name"] == null ? string.Empty : dRow["Arrival_unit_name"],
                                          }).Distinct().
                                    Select(x => new CommodityDetail
                                    {
                                        CommodityId = Convert.ToInt16(x.commodityId),
                                        CommodityName = Convert.ToString(x.commodityName),
                                        ArrivalUnitId = Convert.ToInt16(x.arrivalUnitId),
                                        ArrivalCompValue = Convert.ToDecimal(x.arrivalCompValue),
                                    }).ToList();

                    // Now, loading related variety details based on already collected commodity details by using the concept of Foreach,

                    // this Foreach iterating commodity details one by one and get the varietry details.
                    p.CommodityDetails.ForEach(x =>
                    {
                        x.Varieties = dt.AsEnumerable().Where(v => v.Field<int>("mkt_id").ToString() == marketId &&
                        v.Field<int>("cmdt_id") == x.CommodityId && v.Field<int>("cmdt_grp_id") == p.CommodityGroupId &&
                        v.Field<int>("Arrival_unit_id") == x.ArrivalUnitId).Select(x => new Variety
                        {
                            VarietyId = x.Field<int>("Variety_id"),
                            VarietyName = x.Field<string>("Variety_name") == null ? string.Empty : x.Field<string>("Variety_name"),
                            PriceCompValue = x.Field<decimal>("price_comp_value"),
                            PriceUnitId = x.Field<int>("price_unit_id"),
                            PriceUnitName = x.Field<string>("price_unit_name") == null ? string.Empty : x.Field<string>("price_unit_name"),
                        }).ToList();
                    });
                });
            }

            return paisLocalPrefencesInfo;
        }

        /// <summary>
        /// GetArrivalDetails.
        /// </summary>
        /// <param name="marketId">marketId.</param>
        /// <param name="selectedDate">selectedDate.</param>
        /// <param name="userId">userId.</param>
        /// <returns>List.</returns>
        public List<ArrivalDetails> GetArrivalDetails(string marketId, DateTime selectedDate, string userId)
        {
            List<ArrivalDetails> arrivalDetails = new List<ArrivalDetails>();
            List<DbParameter> dbparams = new List<DbParameter>();
            dbparams.Add(new SqlParameter { ParameterName = "@query_name", Value = "GetArrivalDetails", SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@mkt_id", Value = marketId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@reported_date", Value = selectedDate, SqlDbType = SqlDbType.Date, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@User_Id", Value = userId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>("usp_get_PAIS_dailly_arivals", dbparams, SqlHelper.ExecutionType.Procedure);
            if (dt != null && dt.Rows.Count > 0)
            {
                arrivalDetails = this.PopulateArrivalDetails(marketId, dt);
            }

            return arrivalDetails;
        }

        private List<ArrivalDetails> PopulateArrivalDetails(string marketId, DataTable dt)
        {
            List<ArrivalDetails> arrivalDetails = (from DataRow dRow in dt.Rows
                                                   group dRow by new
                                                   {
                                                       UserId = dRow["UserId"] == DBNull.Value ? 0 : (int)dRow["UserId"],
                                                       MarketId = dRow["MarketId"] == DBNull.Value ? 0 : (int)dRow["MarketId"],
                                                       MarketName = dRow["MarketName"] == DBNull.Value ? string.Empty : dRow["MarketName"].ToString(),
                                                       DistrictName = dRow["DistrictName"] == DBNull.Value ? string.Empty : dRow["DistrictName"].ToString(),
                                                       Districtid = dRow["DistrictId"] == DBNull.Value ? 0 : (int)dRow["DistrictId"],
                                                   }
                                                   into groupby
                                                   select new ArrivalDetails
                                                   {
                                                       UserId = groupby.Key.UserId,
                                                       MarketId = groupby.Key.MarketId,
                                                       MarketName = groupby.Key.MarketName,
                                                       DistrictName = groupby.Key.DistrictName,
                                                       Districtid = groupby.Key.Districtid,
                                                   }).ToList();
            arrivalDetails.ForEach(a =>
            {
                a.SubmittedDateList = (from DataRow dRow in dt.Rows
                                       where dRow["MarketId"].ToString() == a.MarketId.ToString()
                                       && dRow["DistrictId"].ToString() == a.Districtid.ToString()
                                       group dRow by new
                                       {
                                           ReportedDate = dRow["ReportedDate"] == DBNull.Value ? DateTime.MinValue : (DateTime)dRow["ReportedDate"],
                                       }
                                       into groupbySubmittedList
                                       select new SubmittedDate
                                       {
                                           ReportedDate = groupbySubmittedList.Key.ReportedDate,
                                       }).ToList();

                a.SubmittedDateList.ForEach(s =>
                {
                    s.ArrivalCommodityGroupDetail = (from DataRow dRow in dt.Rows
                                                     where dRow["MarketId"].ToString() == a.MarketId.ToString()
                                                            && dRow["DistrictId"].ToString() == a.Districtid.ToString()
                                                            && dRow["ReportedDate"].ToString() == s.ReportedDate.ToString()
                                                     group dRow by new
                                                     {
                                                         CommodityGroupId = dRow["CommodityGroupId"] == DBNull.Value ? 0 : (int)dRow["CommodityGroupId"],
                                                         CommodityGroupName = dRow["CommodityGroupName"] == DBNull.Value ? string.Empty : dRow["CommodityGroupName"].ToString(),
                                                     }
                                                     into groupbyArrivalCommodityGroup
                                                     select new ArrivalCommodityGroupDetail
                                                     {
                                                         CommodityGroupId = groupbyArrivalCommodityGroup.Key.CommodityGroupId,
                                                         CommodityGroupName = groupbyArrivalCommodityGroup.Key.CommodityGroupName,
                                                     }).ToList();

                    s.ArrivalCommodityGroupDetail.ForEach(cg =>
                    {
                        cg.ArrivalCommodityDetail = (from DataRow dRow in dt.Rows
                                                     where dRow["MarketId"].ToString() == a.MarketId.ToString()
                                                            && dRow["DistrictId"].ToString() == a.Districtid.ToString()
                                                            && dRow["ReportedDate"].ToString() == s.ReportedDate.ToString() 
                                                            && dRow["CommodityGroupId"].ToString() == cg.CommodityGroupId.ToString()
                                                     group dRow by new
                                                     {
                                                         CommodityId = dRow["CommodityId"] == DBNull.Value ? 0 : (int)dRow["CommodityId"],
                                                         CommodityName = dRow["CommodityName"] == DBNull.Value ? string.Empty : dRow["CommodityName"].ToString(),
                                                         ArrivalUnitId = dRow["ArrivalUnitId"] == DBNull.Value ? 0 : (int)dRow["ArrivalUnitId"],

                                                         // ArrivalStdUnit = dRow["ArrivalStdUnit"] == null ? string.Empty : dRow["ArrivalStdUnit"].ToString(),
                                                         ArrivalCompValue = dRow["ArrivalCompValue"] == DBNull.Value ? 0 : (decimal)dRow["ArrivalCompValue"],
                                                         ArrivalUnitName = dRow["ArrivalUnitName"] == DBNull.Value ? string.Empty : dRow["ArrivalUnitName"].ToString(),
                                                         LocArrivalQty = dRow["LocArrivalQty"] == DBNull.Value ? 0 : (decimal)dRow["LocArrivalQty"],
                                                         ArrivalQty = dRow["ArrivalQty"] == DBNull.Value ? 0 : (decimal)dRow["ArrivalQty"],
                                                         Trend = dRow["Trend"] == DBNull.Value ? string.Empty : dRow["Trend"].ToString(),
                                                     }
                                                     into groupbyArrivalCommodity
                                                     select new ArrivalCommodityDetail
                                                     {
                                                         CommodityId = groupbyArrivalCommodity.Key.CommodityId,
                                                         CommodityName = groupbyArrivalCommodity.Key.CommodityName,
                                                         ArrivalUnitId = groupbyArrivalCommodity.Key.ArrivalUnitId,
                                                         ArrivalCompValue = groupbyArrivalCommodity.Key.ArrivalCompValue,

                                                         // ArrivalStdUnit = groupbyArrivalCommodity.Key.ArrivalStdUnit,
                                                         ArrivalUnitName = groupbyArrivalCommodity.Key.ArrivalUnitName,
                                                         LocArrivalQty = groupbyArrivalCommodity.Key.LocArrivalQty,
                                                         ArrivalQty = groupbyArrivalCommodity.Key.ArrivalQty,
                                                         Trend = groupbyArrivalCommodity.Key.Trend,
                                                     }).ToList();

                        // Now, loading related variety details based on already collected commodity details by using the concept of Foreach,
                        // this Foreach iterating commodity details one by one and get the varietry details.
                        cg.ArrivalCommodityDetail.ForEach(ac =>
                        {
                            ac.Varietylist = (from DataRow dRow in dt.Rows
                                              where dRow["MarketId"].ToString() == a.MarketId.ToString()
                                                     && dRow["DistrictId"].ToString() == a.Districtid.ToString()
                                                     && dRow["ReportedDate"].ToString() == s.ReportedDate.ToString() 
                                                     && dRow["CommodityGroupId"].ToString() == cg.CommodityGroupId.ToString() 
                                                      && dRow["CommodityId"].ToString() == ac.CommodityId.ToString() 
                                                      && dRow["ArrivalUnitId"].ToString() == ac.ArrivalUnitId.ToString()
                                              select new ArrivalVariety
                                              {
                                                  VarietyId = dRow["VarietyId"] == DBNull.Value ? 0 : (int)dRow["VarietyId"],
                                                  VarietyName = dRow["VarietyName"] == DBNull.Value ? string.Empty : dRow["VarietyName"].ToString(),
                                                  MaxPrice = dRow["MaxPrice"] == DBNull.Value ? 0 : (decimal)dRow["MaxPrice"],
                                                  PriceCompValue = dRow["PriceCompValue"] == DBNull.Value ? 0 : (decimal)dRow["PriceCompValue"],
                                                  MinPrice = dRow["MinPrice"] == DBNull.Value ? 0 : (decimal)dRow["MinPrice"],
                                                  ModelPrice = dRow["ModelPrice"] == DBNull.Value ? 0 : (decimal)dRow["ModelPrice"],
                                                  VarietyUnit = dRow["PriceUnitId"] == DBNull.Value ? 0 : (int)dRow["PriceUnitId"],
                                                  VarietyUnitName = dRow["PriceUnitName"] == DBNull.Value ? string.Empty : dRow["PriceUnitName"].ToString(),
                                                  Grade = dRow["Grade"] == DBNull.Value ? string.Empty : dRow["Grade"].ToString(),
                                              }).ToList();
                        });
                    });
                });
            });

            return arrivalDetails;
        }

        /// <summary>
        /// GetArrivalDetailsOffline.
        /// </summary>
        /// <param name="userId">userId.</param>
        /// <param name="marketId">marketId.</param>
        /// <returns>marketId</returns>
        public List<ArrivalDetails> GetArrivalDetailsOffline(string marketId, string userId)
        {
            List<ArrivalDetails> arrivalDetails = new List<ArrivalDetails>();
            List<DbParameter> dbparams = new List<DbParameter>();
            dbparams.Add(new SqlParameter { ParameterName = "@query_name", Value = "GetArrivalDetailsOffline", SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@mkt_id", Value = marketId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@reported_date", Value = DBNull.Value, SqlDbType = SqlDbType.Date, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@User_Id", Value = userId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>("usp_get_PAIS_dailly_arivals", dbparams, SqlHelper.ExecutionType.Procedure);
            if (dt != null && dt.Rows.Count > 0)
            {
                arrivalDetails = this.PopulateArrivalDetails(marketId, dt);
            }

            return arrivalDetails;
        }

        /// <summary>
        /// GetAnamolusDate.
        /// </summary>
        /// <param name="marketId">marketId.</param>
        /// <param name="currentYear">currentYear.</param>
        /// <param name="userId">userId.</param>
        /// <returns>AnamolusDates.</returns>
        public List<AnamolusDate> GetAnamolusDate(string marketId, string currentYear, string userId)
        {
            List<AnamolusDate> AnamolusDates = new List<AnamolusDate>();
            List<DbParameter> dbparams = new List<DbParameter>();
            dbparams.Add(new SqlParameter { ParameterName = "@query_name", Value = "GetAnamolusDate", SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@mkt_id", Value = marketId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@year", Value = currentYear, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@User_Id", Value = userId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });

            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>("usp_get_PAIS_dailly_arivals", dbparams, SqlHelper.ExecutionType.Procedure);
            if (dt != null && dt.Rows.Count > 0)
            {
                AnamolusDates = SqlHelper.ConvertDataTableToList<AnamolusDate>(dt);
            }

            return AnamolusDates;
        }


        /// <summary>
        /// GetAnamolusDateOffline.
        /// </summary>
        /// <param name="marketId">marketId.</param>
        /// <param name="userId">userId.</param>
        /// <returns>anamolusDates.</returns>
        public List<AnamolusDate> GetAnamolusDateOffline(string marketId, string userId)
        {
            List<AnamolusDate> anamolusDates = new List<AnamolusDate>();
            List<DbParameter> dbparams = new List<DbParameter>();
            dbparams.Add(new SqlParameter { ParameterName = "@query_name", Value = "GetAnamolusDateOffline", SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@mkt_id", Value = marketId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@User_Id", Value = userId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });

            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>("usp_get_PAIS_dailly_arivals", dbparams, SqlHelper.ExecutionType.Procedure);
            if (dt != null && dt.Rows.Count > 0)
            {
                anamolusDates = SqlHelper.ConvertDataTableToList<AnamolusDate>(dt);
            }

            return anamolusDates;
        }

        /// <summary>
        /// GetAnamolusDate.
        /// </summary>
        /// <param name="marketId">marketId.</param>
        /// <param name="selectedDate">selectedDate.</param>
        /// <param name="userId">userId.</param>
        /// <returns>AnamolusDates.</returns>
        public List<ArrivalDetails> GetEditPriceDataAnamolus(string marketId, string selectedDate, string userId)
        {
            List<ArrivalDetails> arrivalDetails = new List<ArrivalDetails>();
            List<DbParameter> dbparams = new List<DbParameter>();
            dbparams.Add(new SqlParameter { ParameterName = "@query_name", Value = "GetEditPriceDatainAnamolus", SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@mkt_id", Value = marketId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@reported_date", Value = selectedDate, SqlDbType = SqlDbType.Date, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@User_Id", Value = userId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>("usp_get_PAIS_dailly_arivals", dbparams, SqlHelper.ExecutionType.Procedure);
            if (dt != null && dt.Rows.Count > 0)
            {
                arrivalDetails = this.PopulateArrivalDetails(marketId, dt);
            }

            return arrivalDetails;
        }

        /// <summary>
        /// GetEditPriceDataAnamolusOffline.
        /// </summary>
        ///  <param name="marketId">marketId.</param>
        /// <param name="userId">userId.</param>
        public List<ArrivalDetails> GetEditPriceDataAnamolusOffline(string marketId, string userId)
        {
            List<ArrivalDetails> arrivalDetails = new List<ArrivalDetails>();
            List<DbParameter> dbparams = new List<DbParameter>();
            dbparams.Add(new SqlParameter { ParameterName = "@query_name", Value = "GetEditPriceDatainAnamolusOffline", SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@mkt_id", Value = marketId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@User_Id", Value = userId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>("usp_get_PAIS_dailly_arivals", dbparams, SqlHelper.ExecutionType.Procedure);
            if (dt != null && dt.Rows.Count > 0)
            {
                arrivalDetails = this.PopulateArrivalDetails(marketId, dt);
            }

            return arrivalDetails;
        }

        /// <summary>
        /// GetViewSubmissionData.
        /// </summary>
        /// <param name="marketId">marketId.</param>
        /// <param name="selectedDate">selectedDate.</param>
        /// <param name="userId">userId.</param>
        /// <returns>arrivalDetails.</returns>
        public List<ArrivalDetails> GetViewSubmissionData(string marketId, DateTime selectedDate, string userId)
        {
            List<ArrivalDetails> arrivalDetails = new List<ArrivalDetails>();
            List<DbParameter> dbparams = new List<DbParameter>();
            dbparams.Add(new SqlParameter { ParameterName = "@query_name", Value = "GetViewSubmissionData", SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@mkt_id", Value = marketId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@reported_date", Value = selectedDate, SqlDbType = SqlDbType.Date, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@User_Id", Value = userId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>("usp_get_PAIS_dailly_arivals", dbparams, SqlHelper.ExecutionType.Procedure);
            if (dt != null && dt.Rows.Count > 0)
            {
                arrivalDetails = PopulateArrivalDetails(marketId, dt);
            }

            return arrivalDetails;
        }

        /// <summary>
        /// Add commedity details for markets by using this method.
        /// </summary>
        /// <param name="paisLocalPrefencesInfos">paisLocalPrefencesInfos.</param>
        /// <returns>InsertCommodityVariety.</returns>
        public bool InsertCommodityVariety(List<PaisLocalPrefencesInfo> paisLocalPrefencesInfos)
        {
            string response = string.Empty;
            Dictionary<string, dynamic> result = new Dictionary<string, dynamic>();
            foreach (PaisLocalPrefencesInfo paisLocalPrefencesInfo in paisLocalPrefencesInfos)
            {
                foreach (CommodityDetail commodityDetail in paisLocalPrefencesInfo.CommodityDetails)
                {
                    foreach (Variety variety in commodityDetail.Varieties)
                    {
                        List<DbParameter> dbparamsUserInfo = new List<DbParameter>();

                        dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@query_name", Value = "AddCommodityVariety", SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                        dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@mkt_id", Value = paisLocalPrefencesInfo.MarketId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                        dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@Arrival_unit_id", Value = commodityDetail.ArrivalUnitId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                        dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@Arrival_comp_value", Value = commodityDetail.ArrivalCompValue, SqlDbType = SqlDbType.Decimal, Direction = ParameterDirection.Input });
                        dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@var_price_unit_id", Value = variety.PriceUnitId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                        dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@var_price_comp_value", Value = variety.PriceCompValue, SqlDbType = SqlDbType.Decimal, Direction = ParameterDirection.Input });
                        dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@User_Id", Value = paisLocalPrefencesInfo.UserId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                        dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@cmdt_grp_id", Value = paisLocalPrefencesInfo.CommodityGroupId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                        dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@cmdt_id", Value = commodityDetail.CommodityId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                        dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@Variety_id", Value = variety.VarietyId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                        dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@response_status", Value = DBNull.Value, SqlDbType = SqlDbType.NVarChar, Size = 1, Direction = ParameterDirection.Output });
                        result = SqlHelper.ExecuteNonQuery<SqlConnection>("USP_INSERT_PAIS_local_preferences", dbparamsUserInfo, SqlHelper.ExecutionType.Procedure);
                        response = Convert.ToString(result["@response_status"]);
                    }
                }
            }

            if (response == "Y")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Able to delete the record based on Market, commodity group and commodity.
        /// </summary>
        /// <param name="deleteCommodities">deleteCommodities.</param>
        /// <returns>DeleteCommodity.</returns>
        public bool DeleteCommodity(List<DeleteCommodityOrVariety> deleteCommodities)
        {
            string response = string.Empty;
            Dictionary<string, dynamic> result = new Dictionary<string, dynamic>();
            foreach (DeleteCommodityOrVariety commodity in deleteCommodities)
            {
                List<DbParameter> dbparamsUserInfo = new List<DbParameter>();

                dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@query_name", Value = "DeleteCommodityVariety", SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@mkt_id", Value = commodity.MarketId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@cmdt_grp_id", Value = commodity.CmdtGrpId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@cmdt_id", Value = commodity.CmdtId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@Variety_id", Value = DBNull.Value, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@response_status", Value = DBNull.Value, SqlDbType = SqlDbType.NVarChar, Size = 1, Direction = ParameterDirection.Output });

                result = SqlHelper.ExecuteNonQuery<SqlConnection>("USP_INSERT_PAIS_local_preferences", dbparamsUserInfo, SqlHelper.ExecutionType.Procedure);
                response = Convert.ToString(result["@response_status"]);
            }
            if (response == "Y")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Delete only Variety record from database.
        /// </summary>
        /// <param name="deleteCommodityOrVarieties">deleteCommodityOrVarieties.</param>
        /// <returns>DeleteVariety.</returns>
        public bool DeleteVariety(List<DeleteCommodityOrVariety> deleteCommodityOrVarieties)
        {
            string response = string.Empty;
            Dictionary<string, dynamic> result = new Dictionary<string, dynamic>();
            foreach (var deleteCommodityOrVariety in deleteCommodityOrVarieties)
            {
                List<DbParameter> dbparamsUserInfo = new List<DbParameter>();

                dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@query_name", Value = "DeleteCommodityVariety", SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@mkt_id", Value = deleteCommodityOrVariety.MarketId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@cmdt_grp_id", Value = deleteCommodityOrVariety.CmdtGrpId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@cmdt_id", Value = deleteCommodityOrVariety.CmdtId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@Variety_id", Value = deleteCommodityOrVariety.VarietyId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@response_status", Value = DBNull.Value, SqlDbType = SqlDbType.NVarChar, Size = 1, Direction = ParameterDirection.Output });

                result = SqlHelper.ExecuteNonQuery<SqlConnection>("USP_INSERT_PAIS_local_preferences", dbparamsUserInfo, SqlHelper.ExecutionType.Procedure);
                response = Convert.ToString(result["@response_status"]);
            }

            if (response == "Y")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// InsertArrivalData.
        /// </summary>
        /// <param name="arrivalDetails">arrivalDetails.</param>
        /// <returns></returns>
        public bool InsertArrivalData(List<ArrivalDetails> arrivalDetails)
        {
            string response = string.Empty;
            Dictionary<string, dynamic> result = new Dictionary<string, dynamic>();

            foreach (ArrivalDetails arrivaldetails in arrivalDetails)
            {
                foreach (SubmittedDate submittedDate in arrivaldetails.SubmittedDateList)
                {
                    foreach (ArrivalCommodityGroupDetail arrivalCommodityGroupDetail in submittedDate.ArrivalCommodityGroupDetail)
                    {
                        foreach (ArrivalCommodityDetail arrivalCommodityDetail in arrivalCommodityGroupDetail.ArrivalCommodityDetail)
                        {
                            foreach (ArrivalVariety arrivalVariety in arrivalCommodityDetail.Varietylist)
                            {
                                List<DbParameter> dbparamsUserInfo = new List<DbParameter>();

                                dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@reported_date", Value = submittedDate.ReportedDate, SqlDbType = SqlDbType.Date, Direction = ParameterDirection.Input });
                                dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@mkt_id", Value = arrivaldetails.MarketId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                                dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@district_id", Value = arrivaldetails.Districtid, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                                dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@cmdt_grp_id", Value = arrivalCommodityGroupDetail.CommodityGroupId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                                dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@cmdt_id", Value = arrivalCommodityDetail.CommodityId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                                dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@Variety_id", Value = arrivalVariety.VarietyId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                                dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@Arrival_qty", Value = arrivalCommodityDetail.ArrivalQty, SqlDbType = SqlDbType.Decimal, Direction = ParameterDirection.Input });
                                dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@Trend", Value = arrivalCommodityDetail.Trend, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                                dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@Grade", Value = arrivalVariety.Grade, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                                dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@min_price", Value = arrivalVariety.MinPrice, SqlDbType = SqlDbType.Decimal, Direction = ParameterDirection.Input });
                                dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@model_price", Value = arrivalVariety.ModelPrice, SqlDbType = SqlDbType.Decimal, Direction = ParameterDirection.Input });
                                dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@max_price", Value = arrivalVariety.MaxPrice, SqlDbType = SqlDbType.Decimal, Direction = ParameterDirection.Input });
                                dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@user_id", Value = arrivaldetails.UserId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });

                                dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@response_status", Value = DBNull.Value, SqlDbType = SqlDbType.NVarChar, Size = 1, Direction = ParameterDirection.Output });
                                result = SqlHelper.ExecuteNonQuery<SqlConnection>("USP_insert_PAIS_dailly_mkt", dbparamsUserInfo, SqlHelper.ExecutionType.Procedure);
                                response = Convert.ToString(result["@response_status"]);
                            }
                        }
                    }
                }
            }

            if (response == "Y")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// InsertNilTransaction.
        /// </summary>
        /// <param name="NilTransactions">NilTransactions.</param>
        /// <returns>bool.</returns>
        public bool InsertNilTransaction(List<NilTransaction> NilTransactions)
        {
            string response = string.Empty;
            Dictionary<string, dynamic> result = new Dictionary<string, dynamic>();

            foreach (NilTransaction nilTransaction in NilTransactions)
            {
                List<DbParameter> dbparamsUserInfo = new List<DbParameter>();

                dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@reported_date", Value = nilTransaction.ReportedDate, SqlDbType = SqlDbType.Date, Direction = ParameterDirection.Input });
                dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@mkt_id", Value = nilTransaction.MarketId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@district_id", Value = nilTransaction.Districtid, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@user_id", Value = nilTransaction.UserId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });

                dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@response_status", Value = DBNull.Value, SqlDbType = SqlDbType.NVarChar, Size = 1, Direction = ParameterDirection.Output });
                result = SqlHelper.ExecuteNonQuery<SqlConnection>("USP_insert_PAIS_dailly_mkt", dbparamsUserInfo, SqlHelper.ExecutionType.Procedure);
                response = Convert.ToString(result["@response_status"]);
            }

            if (response == "Y")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// EditPriceData.
        /// </summary>
        /// <returns></returns>
        public bool EditPriceData(List<ArrivalDetails> arrivalDetails)
        {
            string response = string.Empty;
            Dictionary<string, dynamic> result = new Dictionary<string, dynamic>();


            foreach (ArrivalDetails arrivaldetails in arrivalDetails)
            {
                foreach (SubmittedDate submittedDate in arrivaldetails.SubmittedDateList)
                {
                    foreach (ArrivalCommodityGroupDetail arrivalCommodityGroupDetail in submittedDate.ArrivalCommodityGroupDetail)
                    {
                        foreach (ArrivalCommodityDetail arrivalCommodityDetail in arrivalCommodityGroupDetail.ArrivalCommodityDetail)
                        {
                            foreach (ArrivalVariety arrivalVariety in arrivalCommodityDetail.Varietylist)
                            {
                                List<DbParameter> dbparamsUserInfo = new List<DbParameter>();

                                dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@reported_date", Value = submittedDate.ReportedDate, SqlDbType = SqlDbType.Date, Direction = ParameterDirection.Input });
                                dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@mkt_id", Value = arrivaldetails.MarketId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                                dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@district_id", Value = arrivaldetails.Districtid, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                                dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@cmdt_grp_id", Value = arrivalCommodityGroupDetail.CommodityGroupId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                                dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@cmdt_id", Value = arrivalCommodityDetail.CommodityId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                                dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@Variety_id", Value = arrivalVariety.VarietyId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                                dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@Arrival_qty", Value = arrivalCommodityDetail.ArrivalQty, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                                dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@Trend", Value = arrivalCommodityDetail.Trend, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                                dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@Grade", Value = arrivalVariety.Grade, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                                dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@min_price", Value = arrivalVariety.MinPrice, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                                dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@model_price", Value = arrivalVariety.ModelPrice, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                                dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@max_price", Value = arrivalVariety.MaxPrice, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                                dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@user_id", Value = arrivaldetails.UserId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });

                                dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@response_status", Value = DBNull.Value, SqlDbType = SqlDbType.NVarChar, Size = 1, Direction = ParameterDirection.Output });
                                result = SqlHelper.ExecuteNonQuery<SqlConnection>("USP_insert_PAIS_dailly_mkt", dbparamsUserInfo, SqlHelper.ExecutionType.Procedure);
                                response = Convert.ToString(result["@response_status"]);
                            }
                        }
                    }
                }
            }

            if (response == "Y")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}