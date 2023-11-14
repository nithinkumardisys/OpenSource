//------------------------------------------------------------------------------
// <copyright file="AgmarknetRepository.cs" company="Government of Bihar">
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
    using ADIDAS.Model.DTO;
    using ADIDAS.Repository.Helper;
    using ADIDAS.Repository.Interfaces;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Options;

    /// <summary>
    /// Agmarknet Repository.
    /// </summary>
    public class AgmarknetRepository : BaseRepository, IAgmarknetRepository
    {
        private readonly IOptions<DBSettings> options;

        /// <summary>
        /// Initializes a new instance of the <see cref="AgmarknetRepository"/> class.
        /// </summary>
        public AgmarknetRepository(IConfiguration config, IOptions<DBSettings> options)
            : base(config)
        {
            this.options = options;
            SqlHelper.SetConnectionString(this.options.Value.ConnectionString);
        }

        /// <summary>
        /// Get Agmarknet Arrival Data.
        /// </summary>
        /// <param name="reportedDate">Reporting Date</param>
        /// <returns>List Of Arrival Data</returns>
        public AgmarknetArrivalData GetAgmarknetArrivalData(DateTime reportedDate)
        {
            AgmarknetArrivalData agmarknetArrivalData = new AgmarknetArrivalData();

            List<DbParameter> parameters = new List<DbParameter>();

            parameters.Add(new SqlParameter { ParameterName = "@query_name", Value = "GetArrivalData", SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@reported_date", Value = reportedDate, SqlDbType = SqlDbType.Date, Direction = ParameterDirection.Input });

            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>("usp_getdata_pais_agrimarket", parameters, SqlHelper.ExecutionType.Procedure);
            if (dt != null && dt.Rows.Count > 0)
            {
                var arrivalData = SqlHelper.ConvertDataTableToList<ArrivalData>(dt);
                agmarknetArrivalData.Arrival_data = arrivalData;
            }

            return agmarknetArrivalData;
        }

        /// <summary>
        /// Get Agmarknet Price Data.
        /// </summary>
        /// <param name="reportedDate">Reporting Date</param>
        /// <returns>List Of Price Data</returns>
        public AgmarknetPriceData GetAgmarknetPriceData(DateTime reportedDate)
        {
            AgmarknetPriceData agmarknetPriceData = new AgmarknetPriceData();

            List<DbParameter> parameters = new List<DbParameter>();

            parameters.Add(new SqlParameter { ParameterName = "@query_name", Value = "GetPriceData", SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@reported_date", Value = reportedDate, SqlDbType = SqlDbType.Date, Direction = ParameterDirection.Input });

            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>("usp_getdata_pais_agrimarket", parameters, SqlHelper.ExecutionType.Procedure);
            if (dt != null && dt.Rows.Count > 0)
            {
                var priceData = SqlHelper.ConvertDataTableToList<PriceData>(dt);
                agmarknetPriceData.Price_data = priceData;
            }

            return agmarknetPriceData;
        }
    }
}
