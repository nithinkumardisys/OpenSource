//------------------------------------------------------------------------------
// <copyright file="CombinePassHarvesterRepository.cs" company="Government of Bihar">
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
    using ADIDAS.Repository.Helper;
    using ADIDAS.Repository.Interfaces;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Options;

    /// <summary>
    /// CombinePassHarvesterRepository.
    /// </summary>
    public class CombinePassHarvesterRepository : BaseRepository, ICombinePassHarvesterRepository
    {
        /// <summary>
        /// IOptions.
        /// </summary>
        private readonly IOptions<DBSettings> options;

        /// <summary>
        /// CombinePassHarvesterRepository.
        /// </summary>
        /// <param name="config">config.</param>
        /// <param name="options">options.</param>
        public CombinePassHarvesterRepository(IConfiguration config, IOptions<DBSettings> options) : base(config)
        {
            this.options = options;
            SqlHelper.SetConnectionString(this.options.Value.ConnectionString);
        }

        /// <summary>
        /// GetCombinePassHarvester.
        /// </summary>
        /// <param name="seasonId">seasonId.</param>
        /// <param name="districtId">districtId.</param>
        /// <param name="machineTypeId">machineTypeId.</param>
        /// <returns>CombinePassHarvesterModelList.</returns>
        public List<CombinePassHarvesterModel> GetCombinePassHarvester(string seasonId, int districtId, int machineTypeId)
        {
            List<CombinePassHarvesterModel> result = new List<CombinePassHarvesterModel>();
            List<DbParameter> parameters = new List<DbParameter>();
            DataTable dt = new DataTable();
            parameters.Add(new SqlParameter { ParameterName = "@query_name", Value = "GetCombinePassHarvester", SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@season_id", Value = seasonId, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@district_id", Value = districtId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@machine_type_id", Value = machineTypeId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            dt = SqlHelper.ExecuteSelect<SqlConnection>("usp_getdata_Combine_Pass", parameters, SqlHelper.ExecutionType.Procedure);
            if (dt != null && dt.Rows.Count > 0)
            {
                result = (from DataRow row in dt.Rows
                          select new CombinePassHarvesterModel()
                          {
                              Season_id = Convert.ToInt32(row["season_id"]),
                              Season_name = row["season_name"].ToString(),
                              District_id = Convert.ToInt32(row["district_id"]),
                              District_name = row["district_name"].ToString(),
                              Machine_type_id = Convert.ToInt32(row["machine_type_id"]),
                              Machine_type_name = row["machine_type_name"].ToString(),
                              Applicant_name = row["applicant_name"].ToString(),
                              Mobile_number = row["mobile_number"].ToString(),
                              Block_id = Convert.ToInt32(row["block_id"]),
                              Block_name = row["block_name"].ToString(),
                              Panchayat_id = Convert.ToInt32(row["panchayat_id"]),
                              Panchayat_name = row["panchayat_name"].ToString(),
                              Village_name = row["village_name"].ToString(),
                              Combine_harvester_number = row["machine_reg_no"].ToString(),
                              Issue_date = row["issue_date"] == null ? null : Convert.ToDateTime(row["issue_date"]),
                              Rec_created_userid = Convert.ToInt32(row["rec_created_userid"]),
                              Rec_created_date = row["rec_created_date"] == null ? null : Convert.ToDateTime(row["rec_created_date"]),
                          }).ToList();
            }

            return result;
        }

        /// <summary>
        /// GetMachineType.
        /// </summary>
        /// <returns>CombinePassMachinery.</returns>
        public List<CombinePassMachinery> GetMachineType()
        {
            List<CombinePassMachinery> result = new List<CombinePassMachinery>();
            List<DbParameter> parameters = new List<DbParameter>();
            DataTable dt = new DataTable();
            parameters.Add(new SqlParameter { ParameterName = "@query_name", Value = "GetMachineType", SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dt = SqlHelper.ExecuteSelect<SqlConnection>("usp_getdata_Combine_Pass", parameters, SqlHelper.ExecutionType.Procedure);
            if (dt != null && dt.Rows.Count > 0)
            {
                result = (from DataRow row in dt.Rows
                          select new CombinePassMachinery()
                          {
                              Machine_type_id = Convert.ToInt32(row["machine_type_id"]),
                              Machine_type_name = row["machine_type_name"].ToString(),
                          }).ToList();
            }

            return result;
        }

        /// <summary>
        /// PostCombinePassHarvester.
        /// </summary>
        /// <param name="combinePassHarvesterModel">combinePassHarvesterModel.</param>
        /// <returns>int.</returns>
        public int PostCombinePassHarvester(List<CombinePassHarvesterModel> combinePassHarvesterModel)
        {
            int insertRowsCount = 0;
            if (combinePassHarvesterModel != null)
            {
                Dictionary<string, dynamic> result = new Dictionary<string, dynamic>();
                DataTable dt = new DataTable();

                foreach(CombinePassHarvesterModel combinePassHarvester in combinePassHarvesterModel) {
                    List<DbParameter> dbparams = new List<DbParameter>();

                    dbparams.Add(new SqlParameter { ParameterName = "@machine_reg_no", Value = combinePassHarvester.Combine_harvester_number, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@season_id", Value = combinePassHarvester.Season_id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@district_id", Value = combinePassHarvester.District_id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@block_id", Value = combinePassHarvester.Block_id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@panchayat_id", Value = combinePassHarvester.Panchayat_id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@machine_type_id", Value = combinePassHarvester.Machine_type_id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@applicant_name", Value = combinePassHarvester.Applicant_name, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@mobile_number", Value = combinePassHarvester.Mobile_number, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@village_name", Value = combinePassHarvester.Village_name, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@issue_date", Value = combinePassHarvester.Issue_date, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@rec_created_date", Value = combinePassHarvester.Rec_created_date, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
                    dbparams.Add(new SqlParameter { ParameterName = "@rec_created_userid", Value = combinePassHarvester.Rec_created_userid, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });

                    result = SqlHelper.ExecuteNonQuery<SqlConnection>("usp_insert_Combine_Pass", dbparams, SqlHelper.ExecutionType.Procedure);
                }
                insertRowsCount = insertRowsCount + result["RowsAffected"];
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
    }
}
