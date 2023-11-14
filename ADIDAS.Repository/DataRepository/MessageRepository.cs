//------------------------------------------------------------------------------
// <copyright file="MessageRepository.cs" company="Government of Bihar">
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
    using ADIDAS.Model.Entities;
    using ADIDAS.Repository.Helper;
    using ADIDAS.Repository.Interfaces;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Options;

    /// <summary>
    /// MessageRepository.
    /// </summary>
    public class MessageRepository : BaseRepository, IMessageRepository
    {
        private static string qnGetMessage = "GetMessage";
        private static string spGetMessage = "usp_msg_cntrlr";
        private static string qnPostMessage = "PostMessage";
        private static string qnDeleteMessage = "DeleteMessage";
        private readonly IOptions<DBSettings> options;

        /// <summary>
        /// Initializes a new instance of the <see cref="MessageRepository"/> class.
        /// MessageRepository.
        /// </summary>
        /// <param name="config">config.</param>
        public MessageRepository(IConfiguration config, IOptions<DBSettings> options)
            : base(config)
        {
            this.options = options;
            SqlHelper.SetConnectionString(this.options.Value.ConnectionString);
        }

        /// <summary>
        /// GetMessage.
        /// </summary>
        /// <returns>List Values.</returns>
        public List<MessageEntity> GetMessage()
        {
            List<MessageEntity> appMessage = new List<MessageEntity>();

            List<DbParameter> dbparamsMessageInfo = new List<DbParameter>();
            dbparamsMessageInfo.Add(new SqlParameter { ParameterName = "@query_name", Value = qnGetMessage, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsMessageInfo.Add(new SqlParameter { ParameterName = "@User_Name", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsMessageInfo.Add(new SqlParameter { ParameterName = "@recp_group_id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsMessageInfo.Add(new SqlParameter { ParameterName = "@Msg_Subject", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsMessageInfo.Add(new SqlParameter { ParameterName = "@Msg_Desc", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsMessageInfo.Add(new SqlParameter { ParameterName = "@Msg_Date", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });

            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(spGetMessage, dbparamsMessageInfo, SqlHelper.ExecutionType.Procedure);
            if (dt != null && dt.Rows.Count > 0)
            {
                appMessage = SqlHelper.ConvertDataTableToList<MessageEntity>(dt);
            }

            return appMessage;
        }

        /// <summary>
        /// PostMessage.
        /// </summary>
        /// <param name="message">message.</param>
        /// <returns>DB insertUpdate Stauts.</returns>
        public int PostMessage(MessageEntity message)
        {
            int insertRowsCount = 0;

            DateTime messageDate = Convert.ToDateTime(message.Msg_Date);

            List<DbParameter> dbparamsMessageInfo = new List<DbParameter>();
            dbparamsMessageInfo.Add(new SqlParameter { ParameterName = "@query_name", Value = qnPostMessage, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsMessageInfo.Add(new SqlParameter { ParameterName = "@User_Name", Value = string.IsNullOrEmpty(message.User_Name) ? DBNull.Value : (object)message.User_Name, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsMessageInfo.Add(new SqlParameter { ParameterName = "@recp_group_id", Value = string.IsNullOrEmpty(message.Recp_group_id) ? DBNull.Value : (object)message.Recp_group_id, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsMessageInfo.Add(new SqlParameter { ParameterName = "@Msg_Subject", Value = string.IsNullOrEmpty(message.Msg_Subject) ? DBNull.Value : (object)message.Msg_Subject, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsMessageInfo.Add(new SqlParameter { ParameterName = "@Msg_Desc", Value = string.IsNullOrEmpty(message.Msg_Desc) ? DBNull.Value : (object)message.Msg_Desc, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsMessageInfo.Add(new SqlParameter { ParameterName = "@Msg_Date", Value = messageDate, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });

            Dictionary<string, dynamic> result = SqlHelper.ExecuteNonQuery<SqlConnection>(spGetMessage, dbparamsMessageInfo, SqlHelper.ExecutionType.Procedure);
            insertRowsCount += result["RowsAffected"];
            return insertRowsCount;
        }

        /// <summary>
        /// DeleteMessage.
        /// </summary>
        /// <param name="message">message.</param>
        /// <returns>List Values.</returns>
        public int DeleteMessage(MessageEntity message)
        {
            DateTime messageDate = Convert.ToDateTime(message.Msg_Date);

            List<DbParameter> dbparamsMessageInfo = new List<DbParameter>();
            dbparamsMessageInfo.Add(new SqlParameter { ParameterName = "@query_name", Value = qnDeleteMessage, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsMessageInfo.Add(new SqlParameter { ParameterName = "@User_Name", Value = string.IsNullOrEmpty(message.User_Name) ? DBNull.Value : (object)message.User_Name, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsMessageInfo.Add(new SqlParameter { ParameterName = "@recp_group_id", Value = string.IsNullOrEmpty(message.Recp_group_id) ? DBNull.Value : (object)message.Recp_group_id, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsMessageInfo.Add(new SqlParameter { ParameterName = "@Msg_Subject", Value = string.IsNullOrEmpty(message.Msg_Subject) ? DBNull.Value : (object)message.Msg_Subject, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsMessageInfo.Add(new SqlParameter { ParameterName = "@Msg_Desc", Value = string.IsNullOrEmpty(message.Msg_Desc) ? DBNull.Value : (object)message.Msg_Desc, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsMessageInfo.Add(new SqlParameter { ParameterName = "@Msg_Date", Value = messageDate, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });

            Dictionary<string, dynamic> result = SqlHelper.ExecuteNonQuery<SqlConnection>(spGetMessage, dbparamsMessageInfo, SqlHelper.ExecutionType.Procedure);
            int insertRowsCount = result["RowsAffected"];

            return insertRowsCount;
        }
    }
}
