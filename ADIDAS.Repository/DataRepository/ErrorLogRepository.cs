//------------------------------------------------------------------------------
// <copyright file="ErrorLogRepository.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------
namespace ADIDAS.Repository.DataRepository
{
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Data.SqlClient;
    using ADIDAS.Model.DTO;
    using ADIDAS.Repository.Helper;
    using ADIDAS.Repository.Interfaces;
    using Microsoft.Extensions.Options;

    /// <summary>
    /// ErrorLogRepository.
    /// </summary>
    public class ErrorLogRepository : IErrorLogRepository
    {
        private readonly IOptions<DBSettings> options;

        private static string spAuditErrorLog = "usp_insert_activity_audit";

        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorLogRepository"/> class.
        /// ErrorLogRepository.
        /// </summary>
        /// <param name="options">options.</param>
        public ErrorLogRepository(IOptions<DBSettings> options)
        {
            this.options = options;
            SqlHelper.SetConnectionString(this.options.Value.ConnectionString);
        }

        /// <summary>
        /// oject ErrorModel need to send.
        /// </summary>
        /// <param name="errorModel">errorModel.</param>
        /// <returns>Saving Log.</returns>
        public bool AuditErrorLog(ErrorModel errorModel)
        {
            List<DbParameter> dbparamsCropInfoError = new List<DbParameter>();
            dbparamsCropInfoError.Add(new SqlParameter { ParameterName = "@user_id", Value = errorModel.UserId, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoError.Add(new SqlParameter { ParameterName = "@activity_desc", Value = errorModel.ActivityDesription, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoError.Add(new SqlParameter { ParameterName = "@activity_status", Value = errorModel.ActivityStatus, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoError.Add(new SqlParameter { ParameterName = "@activity_ts", Value = errorModel.ActivityTF, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
            dbparamsCropInfoError.Add(new SqlParameter { ParameterName = "@activity_source", Value = errorModel.ActivitySource, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoError.Add(new SqlParameter { ParameterName = "@api_source", Value = errorModel.ApiSource, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoError.Add(new SqlParameter { ParameterName = "@activity_type", Value = errorModel.ActivityType, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsCropInfoError.Add(new SqlParameter { ParameterName = "@retval", Value = errorModel.RetValue, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Output });
            var result = SqlHelper.ExecuteNonQuery<SqlConnection>(spAuditErrorLog, dbparamsCropInfoError, SqlHelper.ExecutionType.Procedure);

            return true;
        }
    }
}
