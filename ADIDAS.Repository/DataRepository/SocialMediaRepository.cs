//------------------------------------------------------------------------------
// <copyright file="SocialMediaRepository.cs" company="Government of Bihar">
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
    using Microsoft.Extensions.Options;

    /// <summary>
    /// SocialMediaRepository.
    /// </summary>
    public class SocialMediaRepository : ISocialMediaRepository
    {
        private static string spInsertYoutubeVideosData = "usp_ins_youtube_blob_master";

        private readonly IOptions<DBSettings> options;

        /// <summary>
        /// Initializes a new instance of the <see cref="SocialMediaRepository"/> class.
        /// To initialize configuration options.
        /// </summary>
        /// <param name="options">options.</param>
        public SocialMediaRepository(IOptions<DBSettings> options)
        {
            this.options = options;
            SqlHelper.SetConnectionString(this.options.Value.ConnectionString);
        }

        /// <summary>
        /// Insert Youtube Videos Data.
        /// </summary>
        /// <param name="title">title.</param>
        /// <param name="description">description.</param>
        /// <param name="fileType">fileType.</param>
        /// <param name="fileName">fileName.</param>
        /// <returns>no. of rows affected in integer.</returns>
        public int InsertYoutubeVideosData(string title, string description, string fileType, string fileName)
        {
            List<DbParameter> dbparamsVideoInfo = new List<DbParameter>();

            dbparamsVideoInfo.Add(new SqlParameter { ParameterName = "@title", Value = (object)title, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsVideoInfo.Add(new SqlParameter { ParameterName = "@description", Value = (object)description, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsVideoInfo.Add(new SqlParameter { ParameterName = "@file_type", Value = (object)fileType, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsVideoInfo.Add(new SqlParameter { ParameterName = "@file_name", Value = (object)fileName, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsVideoInfo.Add(new SqlParameter { ParameterName = "@response_status", SqlDbType = SqlDbType.VarChar, Size = 200, Direction = ParameterDirection.Output });
            Dictionary<string, dynamic> result = SqlHelper.ExecuteNonQuery<SqlConnection>(spInsertYoutubeVideosData, dbparamsVideoInfo, SqlHelper.ExecutionType.Procedure);

            return 1;
        }
    }
}
