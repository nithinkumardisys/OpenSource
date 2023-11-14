//------------------------------------------------------------------------------
// <copyright file="UserRepository.cs" company="Government of Bihar">
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
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Net.Security;
    using System.Security.Cryptography;
    using System.Security.Cryptography.X509Certificates;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web;
    using ADIDAS.Model.DTO;
    using ADIDAS.Model.Entities;
    using ADIDAS.Repository.Helper;
    using ADIDAS.Repository.Interfaces;
    using Azure.Storage.Blobs;
    using DepartmentOfAgriculture.Admin.Models.DTO;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Options;
    using Newtonsoft.Json;
    using ShareCare.Models.Models;
    using static System.Net.WebRequestMethods;

    /// <summary>
    /// User Repository.
    /// </summary>
    public class UserRepository : BaseRepository, IUserRepository
    {
        private readonly IOptions<BlobConfig> blobconfig;
        private readonly IOptions<Notifications> notification;
        private readonly IOptions<DBSettings> options;
        private readonly string istStrDate = "select CAST(DATEADD(HOUR, 5, DATEADD(MINUTE, 30, GETUTCDATE())) as DATE)";
        private readonly string istDate;
        private readonly List<NotificationResponse> notificationResponses = new List<NotificationResponse>();
        private static string qnGetUserInformation = "GetUserDetails";
        private static string spGetUserInformation = "usp_user_cntrlr_get";
        private static string qnGetUserPrivilage = "GetUserPrivilage";
        private static string qnPostUserDetails = "PostUserDetails";
        private static string activity_desc = "User Login";
        private static string strSuccess = "Success";
        private static string apisource = "PostUserDetails";
        private static string spactivitylog = "usp_insert_activity_audit";
        private static string qnGetUserStatus = "GetUserStatus";
        private static string qnGetUserStatusbyPhoneNumber = "GetUserStatusbyPhoneNumber";
        private static string qnGetAllUsers = "GetAllUsers";
        private static string strSendMessage = "SendMessage";
        private static string spInsertUserInfo = "usp_insert_user_details";
        private static string qnGetUsersDAODetails = "GetUsersDao";
        private static string spUpdateUserStaus = "usp_update_status";
        private static string spCheckConflictonStatusUpdate = "Usp_edit_user_status";
        private static string qnGetUserInfo = "GetUserInfo";
        private static string spApproveUsers = "usp_edit_user_details";
        private static string spSaveUserProfile = "usp_update_user_details";
        private static string qnSaveUserPersonalInformation = "GetUserStatusbyPhoneNumber";
        private static string spGetUsersUnderDAO = "usp_getusersunderdao";
        private static string qnGetUserDetailsById = "GetUserDetailsById";
        private static string qnGetPermissions = "GetPermissions";
        private static string qnGetUserDetailsByIdorName = "GetUserDetailsByIdorName";
        private static string qnGetUserDetailsByName = "GetUserDetailsByName";
        private static string qnGetUserDetailsByIdorNameForAllDesignation = "GetUserDetailsByIdorNameForAllDesignation";
        private static string qnGetUserDetailsByNameForAllDesignation = "GetUserDetailsByNameForAllDesignation";
        private static string spInsertRoleDetails = "usp_insert_role_permission";
        private static string qnGetRoleDetails = "GetRoleDetails";
        private static string qnGetRoleList = "GetRoleList";
        private static string spDeleteRoles = "usp_delete_role_permission";
        private static string spDeleteGroup = "usp_delete_recp_sender_group";
        private static string qnGetUserRoleList = "GetUserRoleList";
        private static string qnInsertGroupDetails = "usp_insert_recp_sender_group";
        private static string qnGetGroupList = "GetGroupList";
        private static string qnGetSenderUserList = "GetSenderUserList";
        private static string qnGetRecipientsUserList = "GetRecipientUserList";
        private static string qnGetGroupDetails = "GetGroupDetails";
        private static string spInsertTransfer = "usp_insert_user_transfer";
        private static string spGetTransferHistory = "usp_getusers_transferred";
        private static string spGetConflictUsers = "usp_getconflictusers";
        private static string spResetUserPassword = "usp_reset_user_password";
        private static string qnSaveRefreshToken = "UpdateTokenDetails";
        private static string qnGetToken = "GetTokenDetails";
        private static string qnSaveNotificationToken = "UpdateNotificationToken";
        private static string qnMarqueMesssage = "MarqueeMessageDtls";
        private static string spMarqueMesssage = "usp_msg_cntrlr";
        private static string qnGetUserNotification = "GetUserNotification";
        private static string qnSendNotification = "GetNoticationToken";

        /// <summary>
        /// Initializes a new instance of the <see cref="UserRepository"/> class.
        /// To initialize configuration options.
        /// </summary>
        /// <param name="config">config.</param>
        /// <param name="blobconfig">blobconfig.</param>
        /// <param name="options">options.</param>
        /// <param name="notification">notification.</param>
        public UserRepository(IConfiguration config, IOptions<BlobConfig> blobconfig, IOptions<DBSettings> options, IOptions<Notifications> notification)
            : base(config)
        {
            this.blobconfig = blobconfig;
            this.options = options;
            SqlHelper.SetConnectionString(this.options.Value.ConnectionString);
            this.istDate = this.GetDateFromServer();
            this.notification = notification;
        }

        /// <summary>
        /// Get Date From Server.
        /// </summary>
        /// <returns>date string.</returns>
        public string GetDateFromServer()
        {
            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(this.istStrDate, null, SqlHelper.ExecutionType.Query);
            return dt.Rows[0][0].ToString();
        }

        /// <summary>
        /// Get User Information.
        /// </summary>
        /// <param name="userName">userName.</param>
        /// <param name="password">password.</param>
        /// <returns>UserEntity.</returns>
        public UserEntity GetUserInformation(string userName, string password)
        {
            List<DbParameter> dbparamsUserInfo = new List<DbParameter>();

            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@query_name", Value = qnGetUserInformation, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@User_Name", Value = userName, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@user_password", Value = password, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@district_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@User_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@Role_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@Recp_Group_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@Phone_num", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(spGetUserInformation, dbparamsUserInfo, SqlHelper.ExecutionType.Procedure);

            UserEntity userResponse = new UserEntity();
            if (dt != null && dt.Rows.Count > 0)
            {
                userResponse = SqlHelper.ConvertDataTableToList<UserEntity>(dt)[0];
                userResponse.LGDirLst = new List<LgDir>();
                userResponse.LGDirLst = SqlHelper.ConvertDataTableToList<LgDir>(dt);
            }

            return userResponse;
        }

        /// <summary>
        /// Get User Privilage.
        /// </summary>
        /// <param name="userName">userName.</param>
        /// <returns>UserPrivilege list.</returns>
        public List<UserPrivilege> GetUserPrivilage(string userName)
        {
            List<UserPrivilege> list = new List<UserPrivilege>();

            List<DbParameter> dbparamsUserInfo = new List<DbParameter>();
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@query_name", Value = qnGetUserPrivilage, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@User_Name", Value = userName, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@user_password", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@district_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@User_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@Role_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@Recp_Group_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@Phone_num", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });

            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(spGetUserInformation, dbparamsUserInfo, SqlHelper.ExecutionType.Procedure);
            if (dt != null && dt.Rows.Count > 0)
            {
                list = SqlHelper.ConvertDataTableToList<UserPrivilege>(dt);
            }

            return list;
        }

        /// <summary>
        /// Post User Details.
        /// </summary>
        /// <param name="user">user.</param>
        /// <returns>UserEntity.</returns>
        public UserEntity PostUserDetails(UserEntity user)
        {
            DateTime ist_Date = DateTime.Now.AddHours(5).AddMinutes(30);

            UserEntity userResponse = new UserEntity();
            List<RecipientGroupDetails> listRecipientGroupDetails = new List<RecipientGroupDetails>();

            List<SenderGroupDetails> listSenderGroupDetails = new List<SenderGroupDetails>();
            List<DbParameter> dbparamsUserInfo = new List<DbParameter>();
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@query_name", Value = qnPostUserDetails, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@User_Name", Value = user.User_Name, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@user_password", Value = user.User_Password, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@district_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@User_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@Role_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@Recp_Group_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@Phone_num", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });

            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(spGetUserInformation, dbparamsUserInfo, SqlHelper.ExecutionType.Procedure);

            if (dt != null && dt.Rows.Count > 0)
            {
                userResponse = SqlHelper.ConvertDataTableToList<UserEntity>(dt)[0];
                userResponse.LGDirLst = new List<LgDir>();
                userResponse.LGDirLst = SqlHelper.ConvertDataTableToList<LgDir>(dt).GroupBy(x => new { x.District_id, x.Block_id, x.Panchayat_id }).Select(x => x.First()).ToList();

                var temp = SqlHelper.ConvertDataTableToList<DtoPermissionType>(dt);

                var tempGroupRecipeint = SqlHelper.ConvertDataTableToList<RecipientGroupDetails>(dt);

                var tempGroupSender = SqlHelper.ConvertDataTableToList<SenderGroupDetails>(dt);

                if (temp.Any(x => x.Permission_id != 0))
                {
                    userResponse.Permissions = SqlHelper.ConvertDataTableToList<PermissionName>(dt).GroupBy(x => x.Permission_name).Select(x => x.First()).ToList();

                    foreach (var item in userResponse.Permissions)
                    {
                        item.PermissionTypes.AddRange(temp.Where(x => x.Permission_name == item.Permission_name).Select(x => new PermissionType { Permission_id = x.Permission_id, Permission_type = x.Permission_type }).GroupBy(x => x.Permission_id).Select(x => x.First()).ToList());
                    }
                }

                if (tempGroupRecipeint.Any(x => x.Recp_group_id != 0))
                {
                    foreach (var item in tempGroupRecipeint)
                    {
                        if (item.Recp_group_id != 0)
                        {
                            listRecipientGroupDetails.Add(item);
                        }
                    }

                    userResponse.Groups.ReceiverGroup = listRecipientGroupDetails.GroupBy(x => x.Recp_group_id).Select(x => x.First()).ToList();
                }

                if (tempGroupSender.Any(x => x.My_grp_id != 0))
                {
                    foreach (var item in tempGroupSender)
                    {
                        if (item.My_grp_id != 0)
                        {
                            listSenderGroupDetails.Add(item);
                        }
                    }

                    userResponse.Groups.MyGroup = listSenderGroupDetails.GroupBy(x => x.My_grp_id).Select(x => x.First()).ToList();
                }

                List<DbParameter> dbparamsCropInfoError = new List<DbParameter>();
                dbparamsCropInfoError.Add(new SqlParameter { ParameterName = "@user_id", Value = userResponse.User_Id, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoError.Add(new SqlParameter { ParameterName = "@activity_desc", Value = activity_desc, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoError.Add(new SqlParameter { ParameterName = "@activity_status", Value = strSuccess, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoError.Add(new SqlParameter { ParameterName = "@activity_ts", Value = ist_Date, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoError.Add(new SqlParameter { ParameterName = "@activity_source", Value = user.App_Reg_Id, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoError.Add(new SqlParameter { ParameterName = "@api_source", Value = apisource, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoError.Add(new SqlParameter { ParameterName = "@activity_type", Value = activity_desc, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsCropInfoError.Add(new SqlParameter { ParameterName = "@retval", Value = 0, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Output });

                Dictionary<string, dynamic> result = SqlHelper.ExecuteNonQuery<SqlConnection>(spactivitylog, dbparamsCropInfoError, SqlHelper.ExecutionType.Procedure);
            }

            return userResponse;
        }

        /// <summary>
        /// Get User Status.
        /// </summary>
        /// <param name="username">username.</param>
        /// <returns>result in integer.</returns>
        public int GetUserStatus(string username)
        {
            List<UserStatus> status = new List<UserStatus>();
            int result = 0;

            List<DbParameter> dbparamsUserInfo = new List<DbParameter>();
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@query_name", Value = qnGetUserStatus, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@User_Name", Value = username, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@user_password", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@district_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@User_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@Role_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@Recp_Group_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@Phone_num", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });

            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(spGetUserInformation, dbparamsUserInfo, SqlHelper.ExecutionType.Procedure);

            if (dt != null && dt.Rows.Count > 0)
            {
                status = SqlHelper.ConvertDataTableToList<UserStatus>(dt);
            }

            if (!status.Any())
            {
                result = 5;
            }
            else if (status.Any(x => x.Approval_status == "R"))
            {
                result = -2;
            }
            else if (status.Any(x => x.Approval_status == "A"))
            {
                result = -2;
            }
            else if (status.Any(x => x.Approval_status == "T"))
            {
                result = -2;
            }
            else if (status.Any(x => x.Approval_status == "N"))
            {
                result = -2;
            }
            else if (status.Any(x => x.Approval_status == "D"))
            {
                if (!status.Any(x => x.Approval_status == "N" || x.Approval_status == "A" || x.Approval_status == "T"))
                {
                    result = 5;
                }
            }

            return result;
        }

        /// <summary>
        /// Get User Status by Phone Number.
        /// </summary>
        /// <param name="phoneNumber">PhoneNumber.</param>
        /// <returns>result in integer.</returns>
        public int GetUserStatusbyPhoneNumber(string phoneNumber)
        {
            int result = 0;
            List<UserStatus> status = new List<UserStatus>();

            List<DbParameter> dbparamsUserInfo = new List<DbParameter>();
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@query_name", Value = qnGetUserStatusbyPhoneNumber, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@User_Name", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@user_password", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@district_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@User_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@Role_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@Recp_Group_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@Phone_num", Value = phoneNumber, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });

            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(spGetUserInformation, dbparamsUserInfo, SqlHelper.ExecutionType.Procedure);

            if (dt != null && dt.Rows.Count > 0)
            {
                status = SqlHelper.ConvertDataTableToList<UserStatus>(dt);
            }

            if (!status.Any())
            {
                result = 5;
            }
            else if (status.Any(x => x.Approval_status == "R"))
            {
                result = -1;
            }
            else if (status.Any(x => x.Approval_status == "A"))
            {
                result = -1;
            }
            else if (status.Any(x => x.Approval_status == "T"))
            {
                result = -1;
            }
            else if (status.Any(x => x.Approval_status == "N"))
            {
                result = -1;
            }
            else if (status.Any(x => x.Approval_status == "D"))
            {
                if (!status.Any(x => x.Approval_status == "N" || x.Approval_status == "A" || x.Approval_status == "T"))
                {
                    result = 5;
                }
            }

            return result;
        }

        /// <summary>
        /// Send Message.
        /// </summary>
        /// <returns>result in integer.</returns>
        public int SendMessage()
        {
            List<SendMessage> details = null;

            List<DbParameter> dbparamsUserInfo = new List<DbParameter>();

            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@query_name", Value = qnGetAllUsers, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });

            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(spGetUserInformation, dbparamsUserInfo, SqlHelper.ExecutionType.Procedure);

            if (dt != null && dt.Rows.Count > 0)
            {
                details = SqlHelper.ConvertDataTableToList<SendMessage>(dt);
            }

            if (details.Any())
            {
                foreach (var userInfo in details)
                {
                    string template = "A new version of Bihan App 1.5 is available on the Google Play App Store, please upgrade for hassle free use of app.";

                    List<DbParameter> jobSchedularError = new List<DbParameter>();
                    jobSchedularError.Add(new SqlParameter { ParameterName = "@user_id", Value = userInfo.USER_ID.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                    jobSchedularError.Add(new SqlParameter { ParameterName = "@activity_desc", Value = "Bihan app Release 1.5 sms SentMessageSuccessfully", SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                    jobSchedularError.Add(new SqlParameter { ParameterName = "@activity_status", Value = strSuccess, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                    jobSchedularError.Add(new SqlParameter { ParameterName = "@activity_ts", Value = this.istDate, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                    jobSchedularError.Add(new SqlParameter { ParameterName = "@activity_source", Value = strSendMessage, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                    jobSchedularError.Add(new SqlParameter { ParameterName = "@api_source", Value = strSendMessage, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                    jobSchedularError.Add(new SqlParameter { ParameterName = "@activity_type", Value = strSendMessage, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                    jobSchedularError.Add(new SqlParameter { ParameterName = "@retval", Value = 0, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Output });

                    SqlHelper.ExecuteNonQuery<SqlConnection>(spactivitylog, jobSchedularError, SqlHelper.ExecutionType.Procedure);
                }

                return 1;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// Self Signed For Localhost.
        /// </summary>
        /// <param name="sender">sender.</param>
        /// <param name="certificate">certificate.</param>
        /// <param name="chain">chain.</param>
        /// <param name="sslPolicyErrors">sslPolicyErrors.</param>
        /// <returns>status is true/false based on result.</returns>
        private static bool SelfSignedForLocalhost(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            if (sslPolicyErrors == SslPolicyErrors.None)
            {
                return true;
            }

            return sender is HttpWebRequest httpWebRequest
                    && httpWebRequest.RequestUri.Host == "localhost"
                    && certificate is X509Certificate2 x509Certificate2
                    && x509Certificate2.Thumbprint == "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA"
                    && sslPolicyErrors == SslPolicyErrors.RemoteCertificateChainErrors;
        }

        /// <summary>
        /// send Single SMS.
        /// </summary>
        /// <param name="username">username.</param>
        /// <param name="password">password.</param>
        /// <param name="senderid">senderid.</param>
        /// <param name="mobileNo">mobileNo.</param>
        /// <param name="message">message.</param>
        /// <param name="secureKey">secureKey.</param>
        /// <param name="templateid">templateid.</param>
        /// <returns>response from server in string.</returns>
        public string SendSingleSMS(string username, string password, string senderid, string mobileNo, string message, string secureKey, string templateid)
        {
            // Latest Generated Secure Key
            Stream dataStream;

            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12; // forcing .Net framework to use TLSv1.2
            string url = "https://msdgweb.mgov.gov.in/esms/sendsmsrequestDLT";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.ProtocolVersion = HttpVersion.Version10;
            request.KeepAlive = false;
            request.ServicePoint.ConnectionLimit = 1;

            request.UserAgent = "Mozilla/4.0 (compatible; MSIE 5.0; Windows 98; DigExt)";

            request.Method = "POST";

            ServicePointManager.ServerCertificateValidationCallback += SelfSignedForLocalhost;

            string encryptedPassword = this.EncryptedPasswod(password);
            string newsecureKey = this.HashGenerator(username.Trim(), senderid.Trim(), message.Trim(), secureKey.Trim());
            string smsservicetype = "singlemsg"; // For single message.

            string query = "username=" + HttpUtility.UrlEncode(username.Trim()) +
                "&password=" + HttpUtility.UrlEncode(encryptedPassword) +

                "&smsservicetype=" + HttpUtility.UrlEncode(smsservicetype) +

                "&content=" + HttpUtility.UrlEncode(message.Trim()) +

                "&mobileno=" + HttpUtility.UrlEncode(mobileNo) +

                "&senderid=" + HttpUtility.UrlEncode(senderid.Trim()) +
              "&key=" + HttpUtility.UrlEncode(newsecureKey.Trim()) +
              "&templateid=" + HttpUtility.UrlEncode(templateid.Trim());

            byte[] byteArray = Encoding.ASCII.GetBytes(query);

            request.ContentType = "application/x-www-form-urlencoded";

            request.ContentLength = byteArray.Length;

            dataStream = request.GetRequestStream();

            dataStream.Write(byteArray, 0, byteArray.Length);

            dataStream.Close();

            WebResponse response = request.GetResponse();

            dataStream = response.GetResponseStream();

            StreamReader reader = new StreamReader(dataStream);

            string responseFromServer = reader.ReadToEnd();

            reader.Close();

            dataStream.Close();

            response.Close();
            return responseFromServer;
        }

        /// <summary>
        /// Encrypted Passwod.
        /// </summary>
        /// <param name="password">password.</param>
        /// <returns>Encrypted Passwod in string.</returns>
        public string EncryptedPasswod(string password)
        {
            byte[] encPwd = Encoding.UTF8.GetBytes(password);
            SHA256 mySHA256 = SHA256.Create();
            byte[] pp = mySHA256.ComputeHash(encPwd);
            StringBuilder sb = new StringBuilder();
            foreach (byte b in pp)
            {
                sb.Append(b.ToString("x2"));
            }

            return sb.ToString();
        }

        /// <summary>
        /// hashGenerator.
        /// </summary>
        /// <param name="username">Username.</param>
        /// <param name="sender_id">sender_id.</param>
        /// <param name="message">message.</param>
        /// <param name="secure_key">secure_key.</param>
        /// <returns>hashGenerator in string.</returns>
        public string HashGenerator(string username, string sender_id, string message, string secure_key)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(username).Append(sender_id).Append(message).Append(secure_key);

            byte[] genkey = Encoding.UTF8.GetBytes(sb.ToString());

            HashAlgorithm sha1 = HashAlgorithm.Create("SHA512");

            byte[] sec_key = sha1.ComputeHash(genkey);

            StringBuilder sb1 = new StringBuilder();
            for (int i = 0; i < sec_key.Length; i++)
            {
                sb1.Append(sec_key[i].ToString("x2"));
            }

            return sb1.ToString();
        }

        /// <summary>
        /// Insert User Info.
        /// </summary>
        /// <param name="userDetails">userDetails.</param>
        /// <returns>user info in string.</returns>
        public string InsertUserInfo(UserDetails userDetails)
        {
            string userId = string.Empty;

            List<DbParameter> dbparamsUserInfo = new List<DbParameter>();

            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@user_name", Value = string.IsNullOrEmpty(userDetails.Email) ? DBNull.Value : (object)userDetails.Email, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@user_password", Value = string.IsNullOrEmpty(userDetails.Password) ? DBNull.Value : (object)userDetails.Password, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@first_name", Value = string.IsNullOrEmpty(userDetails.FirstName) ? DBNull.Value : (object)userDetails.FirstName, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@last_name", Value = string.IsNullOrEmpty(userDetails.LastName) ? DBNull.Value : (object)userDetails.LastName, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@email_id", Value = string.IsNullOrEmpty(userDetails.Email) ? DBNull.Value : (object)userDetails.Email, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@designation", Value = string.IsNullOrEmpty(userDetails.Designation) ? DBNull.Value : (object)userDetails.Designation, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@department", Value = string.IsNullOrEmpty(userDetails.Department) ? DBNull.Value : (object)userDetails.Department, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@phone_num", Value = string.IsNullOrEmpty(userDetails.PhoneNumer) ? DBNull.Value : (object)userDetails.PhoneNumer, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@preferred_lang", Value = string.IsNullOrEmpty(userDetails.PreferedLanguage) ? DBNull.Value : (object)userDetails.PreferedLanguage, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@gender", Value = string.IsNullOrEmpty(userDetails.Gender) ? DBNull.Value : (object)userDetails.Gender, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@division_id", Value = string.IsNullOrEmpty(userDetails.Divisions) ? DBNull.Value : (object)userDetails.Divisions, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@district_id", Value = string.IsNullOrEmpty(userDetails.Districts) ? DBNull.Value : (object)userDetails.Districts, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@block_id", Value = string.IsNullOrEmpty(userDetails.Blocks) ? DBNull.Value : (object)userDetails.Blocks, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@sub_division_id", Value = string.IsNullOrEmpty(userDetails.SubDivisions) ? DBNull.Value : (object)userDetails.SubDivisions, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@panchayat_id", Value = string.IsNullOrEmpty(userDetails.Panchayats) ? DBNull.Value : (object)userDetails.Panchayats, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@Inserted_User_id", SqlDbType = SqlDbType.VarChar, Size = 20, Direction = ParameterDirection.Output });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@email_verified", Value = string.IsNullOrEmpty(userDetails.Emailverified) ? DBNull.Value : (object)userDetails.Emailverified, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@data_entry_flg", Value = string.IsNullOrEmpty(userDetails.DataFlag) ? DBNull.Value : (object)userDetails.DataFlag, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@approval_flg", Value = string.IsNullOrEmpty(userDetails.ApprovalFlg) ? DBNull.Value : (object)userDetails.ApprovalFlg, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@hq_flg", Value = string.IsNullOrEmpty(userDetails.HqFlg) ? DBNull.Value : (object)userDetails.HqFlg, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            Dictionary<string, dynamic> result = SqlHelper.ExecuteNonQuery<SqlConnection>(spInsertUserInfo, dbparamsUserInfo, SqlHelper.ExecutionType.Procedure);

            userId = result["@Inserted_User_id"];

            return userId;
        }

        /// <summary>
        /// Update Image Details.
        /// </summary>
        /// <param name="filename">filename.</param>
        /// <param name="filelocation">filelocation.</param>
        /// <param name="userid">userid.</param>
        /// <returns>result status in integer.</returns>
        public int UpdateImageDetails(string filename, string filelocation, string userid)
        {
            List<DbParameter> dbparams = new List<DbParameter>();

            string updateQuery = "UPDATE app_user SET image_file_name=@filename ,image_file_location=@filelocation where user_id=@userid;";
            dbparams.Add(new SqlParameter { ParameterName = "@filename", Value = filename, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@filelocation", Value = filelocation, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@userid", Value = Convert.ToInt32(userid), SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });

            Dictionary<string, dynamic> result = SqlHelper.ExecuteNonQuery<SqlConnection>(updateQuery, dbparams, SqlHelper.ExecutionType.Query);
            return result["RowsAffected"];
        }

        /// <summary>
        /// Get Users DAO Details.
        /// </summary>
        /// <param name="districtid">districtid.</param>
        /// <returns>DaoDetails as entity.</returns>
        public DaoDetails GetUsersDAODetails(int districtid)
        {
            DaoDetails details = null;

            List<DbParameter> dbparamsUserInfo = new List<DbParameter>();
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@query_name", Value = qnGetUsersDAODetails, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@User_Name", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@user_password", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@district_Id", Value = districtid.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@User_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@Role_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@Recp_Group_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@Phone_num", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(spGetUserInformation, dbparamsUserInfo, SqlHelper.ExecutionType.Procedure);

            if (dt != null && dt.Rows.Count > 0)
            {
                details = SqlHelper.ConvertDataTableToList<DaoDetails>(dt)[0];
            }

            return details;
        }

        /// <summary>
        /// Check Conflicton Status Update.
        /// </summary>
        /// <param name="userStatus">userStatus.</param>
        /// <returns>string.</returns>
        public string CheckConflictonStatusUpdate(DtoUserStatus userStatus)
        {
            string conflict = string.Empty;

            List<DbParameter> dbparamsUserInfo = new List<DbParameter>();
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@user_id", Value = userStatus.UserID.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@userstatus", Value = userStatus.Status, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@conflict_flag", Size = 10, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Output });
            Dictionary<string, dynamic> result = SqlHelper.ExecuteNonQuery<SqlConnection>(spCheckConflictonStatusUpdate, dbparamsUserInfo, SqlHelper.ExecutionType.Procedure);
            conflict = result["@conflict_flag"];

            return conflict;
        }

        /// <summary>
        /// Update User Staus.
        /// </summary>
        /// <param name="userStatus">userStatus.</param>
        /// <returns>AppUserName list.</returns>
        public List<AppUserName> UpdateUserStaus(DtoUserStatusRequest userStatus)
        {
            int rowsAffected = 0;
            List<AppUserName> appusers = new List<AppUserName>();

            foreach (var userstat in userStatus.UserList)
            {
                List<DbParameter> dbparamsUserInfo = new List<DbParameter>();
                dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@User_Id", Value = userstat.UserID, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@Role_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@Recp_Group_ID", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@status", Value = userstat.Status, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@comments", Value = userStatus.Comments, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@retval", Value = DBNull.Value, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@Approved_User_Id", Value = string.IsNullOrEmpty(userStatus.DaoId) ? DBNull.Value : (object)userStatus.DaoId, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });

                Dictionary<string, dynamic> result = SqlHelper.ExecuteNonQuery<SqlConnection>(spUpdateUserStaus, dbparamsUserInfo, SqlHelper.ExecutionType.Procedure);
                rowsAffected += result["RowsAffected"];

                if (result["RowsAffected"] != 0)
                {
                    List<DbParameter> dbparamsUserInfos = new List<DbParameter>();
                    dbparamsUserInfos.Add(new SqlParameter { ParameterName = "@query_name", Value = qnGetUserInfo, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                    dbparamsUserInfos.Add(new SqlParameter { ParameterName = "@User_Name", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                    dbparamsUserInfos.Add(new SqlParameter { ParameterName = "@user_password", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                    dbparamsUserInfos.Add(new SqlParameter { ParameterName = "@district_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                    dbparamsUserInfos.Add(new SqlParameter { ParameterName = "@User_Id", Value = userstat.UserID, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                    dbparamsUserInfos.Add(new SqlParameter { ParameterName = "@Role_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                    dbparamsUserInfos.Add(new SqlParameter { ParameterName = "@Phone_num", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                    dbparamsUserInfos.Add(new SqlParameter { ParameterName = "@Recp_Group_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });

                    DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(spGetUserInformation, dbparamsUserInfos, SqlHelper.ExecutionType.Procedure);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        appusers.Add(SqlHelper.ConvertDataTableToList<AppUserName>(dt)[0]);
                    }

                    dt?.Rows.Clear();
                }
            }

            return appusers;
        }

        /// <summary>
        /// Approve Users.
        /// </summary>
        /// <param name="userStatus">userStatus.</param>
        /// <returns>AppUserName list.</returns>
        public List<AppUserName> ApproveUsers(ApproveUserStatusRequest userStatus)
        {
            int rowsAffected = 0;
            List<DbParameter> dbparams = new List<DbParameter>();
            List<AppUserName> appusers = new List<AppUserName>();
            List<DbParameter> dbparamsselectquery = new List<DbParameter>();
            string conflictFlag;
            foreach (var userstat in userStatus.UserList)
            {
                List<DbParameter> dbparamsUserInfo = new List<DbParameter>();
                dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@user_id", Value = userstat.UserID.ToString(), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@userstatus", Value = userstat.Status, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@firstname", Value = userstat.FirstName, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@lastname", Value = userstat.LastName, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@designation", Value = userstat.Designation, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@department", Value = userstat.Department, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@division", Value = userstat.Divisions, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@district", Value = userstat.Districts, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@sub_division", Value = userstat.SubDivisions, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@block", Value = string.IsNullOrEmpty(userstat.Blocks) ? DBNull.Value : (object)userstat.Blocks, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@panchayat", Value = string.IsNullOrEmpty(userstat.Panchayats) ? DBNull.Value : (object)userstat.Panchayats, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@phonenumber", Value = userstat.PhoneNumber, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@comments", Value = string.IsNullOrEmpty(userStatus.Comments) ? DBNull.Value : (object)userStatus.Comments, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@data_entry_flg", Value = userstat.PanchayatLevel, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });

                dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@conflict_flag", SqlDbType = SqlDbType.VarChar, Size = 20, Direction = ParameterDirection.Output });

                Dictionary<string, dynamic> result = SqlHelper.ExecuteNonQuery<SqlConnection>(spApproveUsers, dbparamsUserInfo, SqlHelper.ExecutionType.Procedure);
                rowsAffected += result["RowsAffected"];

                conflictFlag = result["@conflict_flag"];

                dbparams.Clear();
                if (conflictFlag == "N")
                {
                    List<DbParameter> dbparamsUserInfos = new List<DbParameter>();
                    dbparamsUserInfos.Add(new SqlParameter { ParameterName = "@query_name", Value = qnGetUserInfo, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                    dbparamsUserInfos.Add(new SqlParameter { ParameterName = "@User_Name", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                    dbparamsUserInfos.Add(new SqlParameter { ParameterName = "@user_password", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                    dbparamsUserInfos.Add(new SqlParameter { ParameterName = "@district_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                    dbparamsUserInfos.Add(new SqlParameter { ParameterName = "@User_Id", Value = userstat.UserID, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                    dbparamsUserInfos.Add(new SqlParameter { ParameterName = "@Role_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                    dbparamsUserInfos.Add(new SqlParameter { ParameterName = "@Phone_num", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                    dbparamsUserInfos.Add(new SqlParameter { ParameterName = "@Recp_Group_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });

                    DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(spGetUserInformation, dbparamsUserInfos, SqlHelper.ExecutionType.Procedure);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        appusers.Add(SqlHelper.ConvertDataTableToList<AppUserName>(dt)[0]);
                    }

                    dt?.Rows.Clear();
                    dbparamsselectquery.Clear();
                }
            }

            return appusers;
        }

        /// <summary>
        /// Save User Profile.
        /// </summary>
        /// <param name="userProfile">userProfile.</param>
        /// <returns>DtoUserProfile.</returns>
        public async Task<DtoUserProfile> SaveUserProfile(DtoUserProfile userProfile)
        {
            BlobServiceClient blobServiceClient = new BlobServiceClient(this.blobconfig.Value.BlobConnection);
            if (userProfile.UserId != 0)
            {
                if (!string.IsNullOrEmpty(userProfile.ImageData))
                {
                    string imagedata = UserRepository.ScaleImage(userProfile.ImageData, 140, 140);

                    userProfile.ImageData = string.Empty;
                    userProfile.ImageData = imagedata;

                    BlobEntity blobEntity = new BlobEntity();
                    blobEntity.DirectoryName = "Profile";
                    blobEntity.FolderName = userProfile.FirstName + "-" + userProfile.UserId + "-" + DateTime.Now.ToString("dd-MM-yyyy") + ".jpg";
                    blobEntity.ByteArray = userProfile.ImageData;

                    BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient("mobileapp");

                    string blobPath = blobEntity.DirectoryName + Path.AltDirectorySeparatorChar + blobEntity.FolderName;

                    BlobClient blobClient = containerClient.GetBlobClient(blobPath);

                    byte[] bytes1 = Convert.FromBase64String(blobEntity.ByteArray);
                    Stream stream = new MemoryStream(bytes1);

                    await blobClient.UploadAsync(stream, true);
                    userProfile.ImageFileLocation = this.blobconfig.Value.UserProfilePhoto;
                    userProfile.ImageFileName = blobEntity.FolderName;
                }

                List<DbParameter> dbparamsUserInfo = new List<DbParameter>();
                dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@user_id", Value = (object)userProfile.UserId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@first_name", Value = string.IsNullOrEmpty(userProfile.FirstName) ? DBNull.Value : (object)userProfile.FirstName, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@last_name", Value = string.IsNullOrEmpty(userProfile.LastName) ? DBNull.Value : (object)userProfile.LastName, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@preferred_lang", Value = string.IsNullOrEmpty(userProfile.PreferedLanguage) ? DBNull.Value : (object)userProfile.PreferedLanguage, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@phone_num", Value = string.IsNullOrEmpty(userProfile.PhoneNumber) ? DBNull.Value : (object)userProfile.PhoneNumber, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@gender", Value = string.IsNullOrEmpty(userProfile.Gender) ? DBNull.Value : (object)userProfile.Gender, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@image_file_name", Value = string.IsNullOrEmpty(userProfile.ImageFileName) ? DBNull.Value : (object)userProfile.ImageFileName, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@image_file_location", Value = string.IsNullOrEmpty(userProfile.ImageFileLocation) ? DBNull.Value : (object)userProfile.ImageFileLocation, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@user_password", Value = string.IsNullOrEmpty(userProfile.UserPassword) ? DBNull.Value : (object)userProfile.UserPassword, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                Dictionary<string, dynamic> result = SqlHelper.ExecuteNonQuery<SqlConnection>(spSaveUserProfile, dbparamsUserInfo, SqlHelper.ExecutionType.Procedure);

                return userProfile;
            }

            return null;
        }

        /// <summary>
        /// Save User Personal Information.
        /// </summary>
        /// <param name="userProfile">userProfile.</param>
        /// <returns>no. of rows affected in integer.</returns>
        public int SaveUserPersonalInformation(DtoUserProfile userProfile)
        {
            int results = 0;
            List<UserStatus> status = new List<UserStatus>();

            List<DbParameter> dbparamsUserInfos = new List<DbParameter>();
            dbparamsUserInfos.Add(new SqlParameter { ParameterName = "@query_name", Value = qnSaveUserPersonalInformation, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfos.Add(new SqlParameter { ParameterName = "@User_Name", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfos.Add(new SqlParameter { ParameterName = "@user_password", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfos.Add(new SqlParameter { ParameterName = "@district_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfos.Add(new SqlParameter { ParameterName = "@User_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfos.Add(new SqlParameter { ParameterName = "@Role_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfos.Add(new SqlParameter { ParameterName = "@Recp_Group_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfos.Add(new SqlParameter { ParameterName = "@Phone_num", Value = userProfile.PhoneNumber, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });

            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(spGetUserInformation, dbparamsUserInfos, SqlHelper.ExecutionType.Procedure);

            if (dt != null && dt.Rows.Count > 0)
            {
                status = SqlHelper.ConvertDataTableToList<UserStatus>(dt);
            }

            if (results != -1)
            {
                List<DbParameter> dbparamsUserInfo = new List<DbParameter>();

                dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@user_id", Value = (object)userProfile.UserId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@first_name", Value = string.IsNullOrEmpty(userProfile.FirstName) ? DBNull.Value : (object)userProfile.FirstName, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@last_name", Value = string.IsNullOrEmpty(userProfile.LastName) ? DBNull.Value : (object)userProfile.LastName, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@preferred_lang", Value = string.IsNullOrEmpty(userProfile.PreferedLanguage) ? DBNull.Value : (object)userProfile.PreferedLanguage, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@phone_num", Value = string.IsNullOrEmpty(userProfile.PhoneNumber) ? DBNull.Value : (object)userProfile.PhoneNumber, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@gender", Value = string.IsNullOrEmpty(userProfile.Gender) ? DBNull.Value : (object)userProfile.Gender, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@image_file_name", Value = string.IsNullOrEmpty(userProfile.ImageFileName) ? DBNull.Value : (object)userProfile.ImageFileName, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@image_file_location", Value = string.IsNullOrEmpty(userProfile.ImageFileLocation) ? DBNull.Value : (object)userProfile.ImageFileLocation, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@user_password", Value = string.IsNullOrEmpty(userProfile.UserPassword) ? DBNull.Value : (object)userProfile.UserPassword, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                Dictionary<string, dynamic> result = SqlHelper.ExecuteNonQuery<SqlConnection>(spSaveUserProfile, dbparamsUserInfo, SqlHelper.ExecutionType.Procedure);

                return 1;
            }
            else
            {
                return -1;
            }
        }

        /// <summary>
        /// Scale Image.
        /// </summary>
        /// <param name="imageData">ImageData.</param>
        /// <param name="maxWidth">maxWidth.</param>
        /// <param name="maxHeight">maxHeight.</param>
        /// <returns>scaled image in string.</returns>
        public static string ScaleImage(string imageData, int maxWidth, int maxHeight)
        {
            System.Drawing.Image image;
            byte[] bytes = System.Convert.FromBase64String(imageData);
            string base64;

            using (MemoryStream ms = new MemoryStream(bytes))
            {
                image = System.Drawing.Image.FromStream(ms);
            }

            var ratioX = (double)maxWidth / image.Width;
            var ratioY = (double)maxHeight / image.Height;
            var ratio = Math.Min(ratioX, ratioY);

            var newWidth = (int)(image.Width * ratio);
            var newHeight = (int)(image.Height * ratio);

            var newImage = new Bitmap(newWidth, newHeight);
            using (var graphics = Graphics.FromImage(newImage))
            {
                graphics.DrawImage(image, 0, 0, newWidth, newHeight);
            }

            using (MemoryStream m = new MemoryStream())
            {
                newImage.Save(m, System.Drawing.Imaging.ImageFormat.Jpeg);
                byte[] imageBytes = m.ToArray();
                base64 = Convert.ToBase64String(imageBytes);
            }

            return base64;
        }

        /// <summary>
        /// Update UserStaus For Active Deactive.
        /// </summary>
        /// <param name="userStatus">userStatus.</param>
        /// <returns>no. of rows affected in integer.</returns>
        public int UpdateUserStausForActiveDeactive(DtoUserStatusRequest userStatus)
        {
            int rowsAffected = 0;

            foreach (var userstat in userStatus.UserList)
            {
                List<DbParameter> dbparamsUserInfo = new List<DbParameter>();
                dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@User_Id", Value = userstat.UserID, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@Role_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@Recp_Group_ID", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@status", Value = userstat.Status, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@comments", Value = userStatus.Comments, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@retval", Value = DBNull.Value, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@Approved_User_Id", Value = string.IsNullOrEmpty(userStatus.DaoId) ? DBNull.Value : (object)userStatus.DaoId, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });

                Dictionary<string, dynamic> result = SqlHelper.ExecuteNonQuery<SqlConnection>(spUpdateUserStaus, dbparamsUserInfo, SqlHelper.ExecutionType.Procedure);
                rowsAffected += result["RowsAffected"];
            }

            return rowsAffected;
        }

        /// <summary>
        /// Get Users Under DAO.
        /// </summary>
        /// <param name="userStatus">userStatus.</param>
        /// <param name="userID">userID.</param>
        /// <returns>DaoUsersDetails list.</returns>
        public List<DaoUsersDetails> GetUsersUnderDAO(string userStatus, int userID)
        {
            List<DaoUsersDetails> usersDetails = new List<DaoUsersDetails>();
            List<DbParameter> dbparamsUserInfo = new List<DbParameter>();
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@user_id", Value = userID, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@userstatus", Value = userStatus, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(spGetUsersUnderDAO, dbparamsUserInfo, SqlHelper.ExecutionType.Procedure);

            if (dt != null && dt.Rows.Count > 0)
            {
                List<DtodaoUsersModel> list = SqlHelper.ConvertDataTableToList<DtodaoUsersModel>(dt);

                usersDetails = list.Select(x => new DaoUsersDetails
                {
                    User_id = x.User_id,
                    Email_id = x.Email_id,
                    User_name = x.User_name,
                    First_name = x.First_name,
                    Last_name = x.Last_name,
                    Phone_num = x.Phone_num,
                    Designation = x.Designation,
                    Department = x.Department,
                    Division_id = x.Division_id,
                    Division_name = x.Division_name,
                    Sub_Division_id = x.Sub_Division_id,
                    Sub_Division_name = x.Sub_Division_name,
                    District_id = x.District_id,
                    District_name = x.District_name,
                    Block_id = x.Block_id,
                    Block_name = x.Block_name,
                    Approval_status = x.Approval_status,
                    Email_verified = x.Email_verified,
                    Data_entry_flg =
                    x.Data_entry_flg == "Y" ? "YES" : (x.Designation == "Assistant Agriculture Officer" || x.Designation == "Assistant Director" || x.Designation == "Assistant Statistical Officer" || x.Designation == "Block Agriculture Officer" || x.Designation == "Block Horticulture Officer" || x.Designation == "Assistant Director Agriculture Chemistry" || x.Designation == "Assistant Director Agriculture Engineering" || x.Designation == "Assistant Director Agronomy" || x.Designation == "Assistant Director Horticulture" || x.Designation == "Assistant Director Plant Protection" || x.Designation == "Assistant Director Soil Conservation") ? "NA" : "NO",
                    Date_applied = x.Date_applied.ToString("dd/MM/yyyy"),
                    Date_approved = x.Date_approved.ToString("dd/MM/yyyy"),
                    DbEditFlag = x.Data_entry_flg,
                }).GroupBy(x => x.User_id).Select(x => x.First()).ToList();

                foreach (var userdetail in usersDetails)
                {
                    userdetail.Conflict_flag = list.Where(x => x.User_id == userdetail.User_id).Any(x => x.Conflict_flag == "Y") ? "Y" : "N";
                    userdetail.Panchayats = list.Where(x => x.User_id == userdetail.User_id && x.Panchayat_id != 0 && x.Panchayat_id != null).Select(x => new PanchayatModel { Panchayat_id = x.Panchayat_id, Panchayat_name = x.Panchayat_name }).GroupBy(x => x.Panchayat_id).Select(x => x.First()).ToList();
                    userdetail.Blocks = list.Where(x => x.User_id == userdetail.User_id && x.Block_id != 0 && x.Block_id != null).Select(x => new BlockLst { BlockId = x.Block_id, BlockName = x.Block_name }).GroupBy(x => x.BlockId).Select(x => x.First()).ToList();
                    userdetail.PanchayatInserted = string.Join(",", list.Where(x => x.User_id == userdetail.User_id && x.Panchayat_id != 0 && x.Panchayat_id != null).Select(x => x.Panchayat_id).Distinct().ToList());
                    userdetail.Blockinserted = string.Join(",", list.Where(x => x.User_id == userdetail.User_id && x.Block_id != 0 && x.Block_id != null).Select(x => x.Block_id).Distinct().ToList());
                    userdetail.SubdivInserted = string.Join(",", list.Where(x => x.User_id == userdetail.User_id && x.Sub_Division_id != 0 && x.Sub_Division_id != null).Select(x => x.Sub_Division_id).Distinct().ToList());
                    userdetail.Subdivs = list.Where(x => x.User_id == userdetail.User_id && x.Sub_Division_id != 0 && x.Sub_Division_id != null).Select(x => new SubDivision { SubDivisionId = x.Sub_Division_id, SubDivisionName = x.Sub_Division_name }).GroupBy(x => x.SubDivisionId).Select(x => x.First()).ToList();
                }
            }

            return usersDetails;
        }

        /// <summary>
        /// Get Advanced Search Data.
        /// </summary>
        /// <param name="advancedSearchModel">advancedSearchModel.</param>
        /// <returns>DaoUsersDetails list.</returns>
        public List<DaoUsersDetails> GetAdvancedSearchData(AdvancedSearchModel advancedSearchModel)
        {
            List<DaoUsersDetails> usersDetails = new List<DaoUsersDetails>();
            List<DbParameter> dbparamsUserInfo = new List<DbParameter>();
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@user_id", Value = advancedSearchModel.User_id, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@userstatus", Value = string.IsNullOrEmpty(advancedSearchModel.Approval_status) ? null : advancedSearchModel.Approval_status, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@first_name", Value = advancedSearchModel.First_name != string.Empty ? advancedSearchModel.First_name : null, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@last_name", Value = advancedSearchModel.Last_name != string.Empty ? advancedSearchModel.Last_name : null, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@designation", Value = advancedSearchModel.Designation != string.Empty ? advancedSearchModel.Designation : null, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@department", Value = advancedSearchModel.Department != string.Empty ? advancedSearchModel.Department : null, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@gender", Value = advancedSearchModel.Gender != string.Empty ? advancedSearchModel.Gender : null, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@district_name", Value = advancedSearchModel.District_name != string.Empty ? advancedSearchModel.District_name : null, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@block_name", Value = advancedSearchModel.Block_name != string.Empty ? advancedSearchModel.Block_name : null, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@panchayat_name", Value = advancedSearchModel.Panchayat_name != string.Empty ? advancedSearchModel.Panchayat_name : null, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@Date_applied_from", Value = advancedSearchModel.Date_appliedFrom != string.Empty ? advancedSearchModel.Date_appliedFrom : null, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@Date_applied_to", Value = advancedSearchModel.Date_appliedTo != string.Empty ? advancedSearchModel.Date_appliedTo : null, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });

            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(spGetUsersUnderDAO, dbparamsUserInfo, SqlHelper.ExecutionType.Procedure);

            if (dt != null && dt.Rows.Count > 0)
            {
                List<DtodaoUsersModel> list = SqlHelper.ConvertDataTableToList<DtodaoUsersModel>(dt);

                usersDetails = list.Select(x => new DaoUsersDetails
                {
                    User_id = x.User_id,
                    Email_id = x.Email_id,
                    User_name = x.User_name,
                    First_name = x.First_name,
                    Last_name = x.Last_name,
                    Phone_num = x.Phone_num,
                    Designation = x.Designation,
                    Department = x.Department,
                    Division_id = x.Division_id,
                    Division_name = x.Division_name,
                    Sub_Division_id = x.Sub_Division_id,
                    Sub_Division_name = x.Sub_Division_name,
                    District_id = x.District_id,
                    District_name = x.District_name,
                    Block_id = x.Block_id,
                    Block_name = x.Block_name,
                    Approval_status = x.Approval_status,
                    Email_verified = x.Email_verified,
                    Conflict_flag = x.Conflict_flag,
                    Data_entry_flg =
                    x.Data_entry_flg == "Y" ? "YES" : (x.Designation == "Assistant Agriculture Officer" || x.Designation == "Assistant Director" || x.Designation == "Assistant Statistical Officer" || x.Designation == "Block Agriculture Officer" || x.Designation == "Block Horticulture Officer" || x.Designation == "Assistant Director Agriculture Chemistry" || x.Designation == "Assistant Director Agriculture Engineering" || x.Designation == "Assistant Director Agronomy" || x.Designation == "Assistant Director Horticulture" || x.Designation == "Assistant Director Plant Protection" || x.Designation == "Assistant Director Soil Conservation") ? "NA" : "NO",

                    Date_applied = x.Date_applied.ToString("dd/MM/yyyy"),
                    Date_approved = x.Date_approved.ToString("dd/MM/yyyy"),
                }).GroupBy(x => x.User_id).Select(x => x.First()).ToList();

                foreach (var userdetail in usersDetails)
                {
                    userdetail.Panchayats = list.Where(x => x.User_id == userdetail.User_id).Select(x => new PanchayatModel { Panchayat_id = x.Panchayat_id, Panchayat_name = x.Panchayat_name }).Distinct().ToList();

                    userdetail.Blocks = list.Where(x => x.User_id == userdetail.User_id && x.Block_id != 0 && x.Block_id != null).Select(x => new BlockLst { BlockId = x.Block_id, BlockName = x.Block_name }).GroupBy(x => x.BlockId).Select(x => x.First()).ToList();
                }
            }

            return usersDetails;
        }

        /// <summary>
        /// Get UserDetails By Id.
        /// </summary>
        /// <param name="userId">userId.</param>
        /// <param name="queryname">queryname.</param>
        /// <returns>DaoUsersDetails entity.</returns>
        public DaoUsersDetails GetUserDetailsById(string userId, string queryname)
        {
            DaoUsersDetails details = null;

            List<DbParameter> dbparamsUserInfo = new List<DbParameter>();
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@query_name", Value = queryname, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@User_Name", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@user_password", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@district_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@User_Id", Value = userId, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@Role_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@Recp_Group_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@Phone_num", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });

            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(spGetUserInformation, dbparamsUserInfo, SqlHelper.ExecutionType.Procedure);

            if (dt != null && dt.Rows.Count > 0)
            {
                details = new DaoUsersDetails();

                List<DtodaoUsersModel> list = SqlHelper.ConvertDataTableToList<DtodaoUsersModel>(dt);

                details = list.Select(x => new DaoUsersDetails
                {
                    User_id = x.User_id,
                    User_name = x.User_name,
                    First_name = x.First_name,
                    Last_name = x.Last_name,
                    Phone_num = x.Phone_num,
                    Designation = x.Designation,
                    Department = x.Department,
                    Division_id = x.Division_id,
                    Division_name = x.Division_name,
                    Sub_Division_id = x.Sub_Division_id,
                    Sub_Division_name = x.Sub_Division_name,
                    District_id = x.District_id,
                    District_name = x.District_name,
                    Block_id = x.Block_id,
                    Block_name = x.Block_name,
                    COMMENTS = x.COMMENTS,
                    Email_verified = x.Email_verified,
                    Date_applied = x.Date_applied.ToString("MM/dd/yyyy"),
                    Approval_status = x.Approval_status,
                    PanchayatLevel = x.Data_entry_flg,
                    New_district_id = x.New_district_id,
                    New_block_id = x.New_block_id,
                    New_Division_Id = x.New_division_id,
                    New_Subdivision_Id = x.New_sub_division_id,
                }).GroupBy(x => x.User_id).Select(x => x.First()).FirstOrDefault();
                details.Blocks = list.Where(x => x.User_id == details.User_id && x.Block_id != 0 && x.Block_id != null).Select(x => new BlockLst { BlockId = x.Block_id, BlockName = x.Block_name }).GroupBy(x => x.BlockId).Select(x => x.First()).ToList();
                details.Panchayats = list.Where(x => x.User_id == details.User_id && x.Panchayat_id != 0 && x.Panchayat_id != null).Select(x => new PanchayatModel { Panchayat_id = x.Panchayat_id, Panchayat_name = x.Panchayat_name }).GroupBy(x => x.Panchayat_id).Select(x => x.First()).ToList();

                details.Subdivs = list.Where(x => x.User_id == details.User_id && x.Sub_Division_id != 0).Select(x => new SubDivision { SubDivisionId = x.Sub_Division_id, SubDivisionName = x.Sub_Division_name }).GroupBy(x => x.SubDivisionId).Select(x => x.First()).ToList();

                details.Newpanchayats = list.Where(x => x.User_id == details.User_id && x.New_panchayat_id != 0 && x.New_panchayat_id != null).Select(x => new PanchayatModel { Panchayat_id = x.New_panchayat_id, Panchayat_name = x.NewPanchayatName }).GroupBy(x => x.Panchayat_id).Select(x => x.First()).ToList();
                details.Newblocks = list.Where(x => x.User_id == details.User_id && x.New_block_id != 0 && x.New_block_id != null).Select(x => new BlockLst { BlockId = x.New_block_id, BlockName = x.NewBlockName }).GroupBy(x => x.BlockId).Select(x => x.First()).ToList();

                details.NewSubdivs = list.Where(x => x.User_id == details.User_id && x.New_sub_division_id != 0 && x.New_sub_division_id != null).Select(x => new SubDivision { SubDivisionId = x.New_sub_division_id }).GroupBy(x => x.SubDivisionId).Select(x => x.First()).ToList();
            }

            return details;
        }

        /// <summary>
        /// GetPermissions.
        /// </summary>
        /// <returns>PermissionsModel.</returns>
        public PermissionsModel GetPermissions()
        {
            PermissionsModel permissions = null;

            List<DbParameter> dbparamsUserInfo = new List<DbParameter>();
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@query_name", Value = qnGetPermissions, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@User_Name", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@user_password", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@district_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@User_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@Role_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@Phone_num", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@Recp_Group_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });

            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(spGetUserInformation, dbparamsUserInfo, SqlHelper.ExecutionType.Procedure);
            if (dt != null && dt.Rows.Count > 0)
            {
                permissions = new PermissionsModel();

                var temp = SqlHelper.ConvertDataTableToList<DtoPermissionType>(dt);

                permissions.Permissions = SqlHelper.ConvertDataTableToList<PermissionName>(dt).GroupBy(x => x.Permission_name).Select(x => x.First()).ToList();

                foreach (var item in permissions.Permissions)
                {
                    item.PermissionTypes.AddRange(temp.Where(x => x.Permission_name == item.Permission_name).Select(x => new PermissionType { Permission_id = x.Permission_id, Permission_type = x.Permission_type }).ToList());
                }
            }

            return permissions;
        }

        /// <summary>
        /// Get Users By Name.
        /// </summary>
        /// <param name="userName">userName.</param>
        /// <param name="userId">userId.</param>
        /// <returns>UsersInfoDTO list.</returns>
        public List<UsersInfoDto> GetUsersByName(string userName, string userId)
        {
            List<string> userIds = new List<string>();

            DataTable dt;
            List<UsersInfoDto> list = new List<UsersInfoDto>();

            if (!userId.Equals("NA"))
            {
                userIds = userId.Split(",").ToList();

                List<DbParameter> dbparamsUserInfo = new List<DbParameter>();
                dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@query_name", Value = qnGetUserDetailsByIdorName, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@User_Name", Value = userName, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@user_password", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@district_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@User_Id", Value = userId, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@Role_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@Recp_Group_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@Phone_num", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dt = SqlHelper.ExecuteSelect<SqlConnection>(spGetUserInformation, dbparamsUserInfo, SqlHelper.ExecutionType.Procedure);
            }
            else
            {
                List<DbParameter> dbparamsUserInfo = new List<DbParameter>();
                dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@query_name", Value = qnGetUserDetailsByName, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@User_Name", Value = userName, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@user_password", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@district_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@User_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@Role_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@Recp_Group_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@Phone_num", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dt = SqlHelper.ExecuteSelect<SqlConnection>(spGetUserInformation, dbparamsUserInfo, SqlHelper.ExecutionType.Procedure);
            }

            if (dt != null && dt.Rows.Count > 0)
            {
                list = SqlHelper.ConvertDataTableToList<UsersInfoDto>(dt).GroupBy(x => x.User_id).Select(x => x.First()).ToList();

                if (!userId.Equals("NA"))
                {
                    foreach (var item in list)
                    {
                        if (userIds.Contains(item.User_id.ToString()))
                        {
                            item.CheckedRecord = true;
                        }
                    }
                }
            }

            return list;
        }

        /// <summary>
        /// Get Users By Name For All Designations.
        /// </summary>
        /// <param name="userName">userName.</param>
        /// <param name="userId">userId.</param>
        /// <returns>UsersInfoDTO list.</returns>
        public List<UsersInfoDto> GetUsersByNameForAllDesignations(string userName, string userId)
        {
            List<string> userIds = new List<string>();
            List<UsersInfoDto> list = new List<UsersInfoDto>();
            List<DbParameter> dbparamsUserInfo = new List<DbParameter>();

            if (!userId.Equals("NA"))
            {
                userIds = userId.Split(",").ToList();
                dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@query_name", Value = qnGetUserDetailsByIdorNameForAllDesignation, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@User_Name", Value = userName, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@user_password", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@district_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@User_Id", Value = userId, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@Role_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@Recp_Group_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@Phone_num", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            }
            else
            {
                dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@query_name", Value = qnGetUserDetailsByNameForAllDesignation, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@User_Name", Value = userName, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@user_password", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@district_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@User_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@Role_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@Recp_Group_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@Phone_num", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            }

            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(spGetUserInformation, dbparamsUserInfo, SqlHelper.ExecutionType.Procedure);
            if (dt != null && dt.Rows.Count > 0)
            {
                list = SqlHelper.ConvertDataTableToList<UsersInfoDto>(dt).GroupBy(x => x.User_id).Select(x => x.First()).ToList();

                if (!userId.Equals("NA"))
                {
                    foreach (var item in list)
                    {
                        if (userIds.Contains(item.User_id.ToString()))
                        {
                            item.CheckedRecord = true;
                        }
                    }
                }
            }

            return list;
        }

        /// <summary>
        /// Edit User Info.
        /// </summary>
        /// <param name="edituserDetails">edituserDetails.</param>
        /// <returns>AppUserName.</returns>
        public AppUserName EditUserInfo(EditUserModel edituserDetails)
        {
            string conflictFlag = string.Empty;

            AppUserName appUser = new AppUserName();

            List<DbParameter> dbparamsUserInfo = new List<DbParameter>();
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@user_id", Value = string.IsNullOrEmpty(edituserDetails.UserId) ? DBNull.Value : (object)edituserDetails.UserId, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@userstatus", Value = string.IsNullOrEmpty(edituserDetails.UserStatus) ? DBNull.Value : (object)edituserDetails.UserStatus, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@firstname", Value = string.IsNullOrEmpty(edituserDetails.FirstName) ? DBNull.Value : (object)edituserDetails.FirstName, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@lastname", Value = string.IsNullOrEmpty(edituserDetails.LastName) ? DBNull.Value : (object)edituserDetails.LastName, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@designation", Value = string.IsNullOrEmpty(edituserDetails.Designation) ? DBNull.Value : (object)edituserDetails.Designation, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@department", Value = string.IsNullOrEmpty(edituserDetails.Department) ? DBNull.Value : (object)edituserDetails.Department, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@district", Value = string.IsNullOrEmpty(edituserDetails.Districts) ? DBNull.Value : (object)edituserDetails.Districts, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@block", Value = string.IsNullOrEmpty(edituserDetails.Blocks) ? DBNull.Value : (object)edituserDetails.Blocks, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@panchayat", Value = string.IsNullOrEmpty(edituserDetails.Panchayats) ? DBNull.Value : (object)edituserDetails.Panchayats, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@phonenumber", Value = string.IsNullOrEmpty(edituserDetails.PhoneNumber) ? DBNull.Value : (object)edituserDetails.PhoneNumber, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@email_verified", Value = string.IsNullOrEmpty(edituserDetails.Emailverified) ? DBNull.Value : (object)edituserDetails.Emailverified, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@comments", Value = string.IsNullOrEmpty(edituserDetails.Comments) ? DBNull.Value : (object)edituserDetails.Comments, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@data_entry_flg", Value = string.IsNullOrEmpty(edituserDetails.PanchayatLevel) ? DBNull.Value : (object)edituserDetails.PanchayatLevel, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@approval_flg", Value = string.IsNullOrEmpty(edituserDetails.ApprFlag) ? DBNull.Value : (object)edituserDetails.ApprFlag, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@hq_flg", Value = string.IsNullOrEmpty(edituserDetails.HqFlg) ? DBNull.Value : (object)edituserDetails.HqFlg, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@division", Value = string.IsNullOrEmpty(edituserDetails.Divisions) ? DBNull.Value : (object)edituserDetails.Divisions, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@sub_division", Value = string.IsNullOrEmpty(edituserDetails.Subdivisions) ? DBNull.Value : (object)edituserDetails.Subdivisions, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@conflict_flag", SqlDbType = SqlDbType.VarChar, Size = 20, Direction = ParameterDirection.Output });

            Dictionary<string, dynamic> result = SqlHelper.ExecuteNonQuery<SqlConnection>(spApproveUsers, dbparamsUserInfo, SqlHelper.ExecutionType.Procedure);

            conflictFlag = result["@conflict_flag"];

            if (conflictFlag == "N")
            {
                List<DbParameter> dbparamsUserInfos = new List<DbParameter>();
                dbparamsUserInfos.Add(new SqlParameter { ParameterName = "@query_name", Value = qnGetUserInfo, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsUserInfos.Add(new SqlParameter { ParameterName = "@User_Name", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsUserInfos.Add(new SqlParameter { ParameterName = "@user_password", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsUserInfos.Add(new SqlParameter { ParameterName = "@district_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsUserInfos.Add(new SqlParameter { ParameterName = "@User_Id", Value = edituserDetails.UserId, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsUserInfos.Add(new SqlParameter { ParameterName = "@Role_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsUserInfos.Add(new SqlParameter { ParameterName = "@Phone_num", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsUserInfos.Add(new SqlParameter { ParameterName = "@Recp_Group_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });

                DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(spGetUserInformation, dbparamsUserInfos, SqlHelper.ExecutionType.Procedure);

                if (dt != null && dt.Rows.Count > 0)
                {
                    appUser = SqlHelper.ConvertDataTableToList<AppUserName>(dt)[0];

                    appUser.ApprovalStatus = "A";
                }
                else
                {
                    appUser.ApprovalStatus = "N";
                }
            }
            else if (conflictFlag == "Y")
            {
                appUser.ApprovalStatus = "C";
            }

            return appUser;
        }

        /// <summary>
        /// Insert Role Details.
        /// </summary>
        /// <param name="roleDetails">roleDetails.</param>
        /// <returns>no. of rows affected in integer.</returns>
        public int InsertRoleDetails(UserRoleModel roleDetails)
        {
            int roleId = 0;

            string status = string.Empty;
            if (roleDetails.Status.Equals("Active"))
            {
                status = "A";
            }
            else if (roleDetails.Status.Equals("Deactive"))
            {
                status = "D";
            }

            List<DbParameter> dbparamsUserInfo = new List<DbParameter>();

            if (string.IsNullOrEmpty(roleDetails.RoleId))
            {
                dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@created_by", Value = roleDetails.CreatedBy == 0 ? DBNull.Value : (object)roleDetails.CreatedBy, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            }
            else if (!string.IsNullOrEmpty(roleDetails.RoleId))
            {
                dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@role_id", Value = string.IsNullOrEmpty(roleDetails.RoleId) ? DBNull.Value : (object)Convert.ToInt32(roleDetails.RoleId), SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@updated_by", Value = roleDetails.UpdatedBy == 0 ? DBNull.Value : (object)roleDetails.UpdatedBy, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            }

            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@role_name", Value = string.IsNullOrEmpty(roleDetails.RoleName) ? DBNull.Value : (object)roleDetails.RoleName, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@role_description", Value = string.IsNullOrEmpty(roleDetails.RoleDescription) ? DBNull.Value : (object)roleDetails.RoleDescription, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@department", Value = string.IsNullOrEmpty(roleDetails.SelectedDepartment) ? DBNull.Value : (object)roleDetails.SelectedDepartment, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@designation", Value = string.IsNullOrEmpty(roleDetails.SelectedDesignation) ? DBNull.Value : (object)roleDetails.SelectedDesignation, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@division_id", Value = string.IsNullOrEmpty(roleDetails.SelectedDivision) ? DBNull.Value : (object)roleDetails.SelectedDivision, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@district_id", Value = string.IsNullOrEmpty(roleDetails.SelectedDistrict) ? DBNull.Value : (object)roleDetails.SelectedDistrict, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@block_id", Value = string.IsNullOrEmpty(roleDetails.SelectedBlocks) ? DBNull.Value : (object)roleDetails.SelectedBlocks, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@panchayat_id", Value = string.IsNullOrEmpty(roleDetails.SelectedPanchayats) ? DBNull.Value : (object)roleDetails.SelectedPanchayats, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@user_id", Value = string.IsNullOrEmpty(roleDetails.UserId) ? DBNull.Value : (object)roleDetails.UserId, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@status", Value = string.IsNullOrEmpty(status) ? DBNull.Value : (object)status, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@permission_id", Value = string.IsNullOrEmpty(roleDetails.Permissions) ? DBNull.Value : (object)roleDetails.Permissions, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@retval", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Output });

            Dictionary<string, dynamic> result = SqlHelper.ExecuteNonQuery<SqlConnection>(spInsertRoleDetails, dbparamsUserInfo, SqlHelper.ExecutionType.Procedure);
            roleId = result["@retval"];
            return roleId;
        }

        /// <summary>
        /// Get Role Details.
        /// </summary>
        /// <param name="roleId">roleId.</param>
        /// <returns>DtoRoleInfo.</returns>
        public DtoRoleInfo GetRoleDetails(string roleId)
        {
            DtoRoleInfo roleInfo = null;
            List<DbParameter> dbparamsUserInfo = new List<DbParameter>();
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@query_name", Value = qnGetRoleDetails, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@User_Name", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@user_password", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@district_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@User_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@Role_Id", Value = roleId, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@Phone_num", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@Recp_Group_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(spGetUserInformation, dbparamsUserInfo, SqlHelper.ExecutionType.Procedure);
            if (dt != null && dt.Rows.Count > 0)
            {
                roleInfo = SqlHelper.ConvertDataTableToList<DtoRoleInfo>(dt)[0];
                roleInfo.Permissions = SqlHelper.ConvertDataTableToList<PermissionType>(dt).GroupBy(x => x.Permission_id).Select(x => x.First()).ToList();
            }

            return roleInfo;
        }

        /// <summary>
        /// Get Roles List.
        /// </summary>
        /// <returns>RoleList list.</returns>
        public List<RoleList> GetRolesList()
        {
            List<RoleList> roleLists = new List<RoleList>();
            List<DbParameter> dbparamsUserInfo = new List<DbParameter>();
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@query_name", Value = qnGetRoleList, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@User_Name", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@user_password", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@district_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@User_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@Role_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@Phone_num", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@Recp_Group_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });

            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(spGetUserInformation, dbparamsUserInfo, SqlHelper.ExecutionType.Procedure);
            if (dt != null && dt.Rows.Count > 0)
            {
                roleLists = SqlHelper.ConvertDataTableToList<RoleList>(dt);
            }

            return roleLists;
        }

        /// <summary>
        /// Update Role Status.
        /// </summary>
        /// <param name="roleId">roleId.</param>
        /// <param name="status">status.</param>
        /// <returns>no. of rows affected in integer.</returns>
        public int UpdateRoleStatus(string roleId, string status)
        {
            List<DbParameter> dbparamsUserInfo = new List<DbParameter>();
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@User_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@Role_Id", Value = roleId, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@Recp_Group_ID", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@status", Value = status, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@comments", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@retval", Value = DBNull.Value, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            Dictionary<string, dynamic> result = SqlHelper.ExecuteNonQuery<SqlConnection>(spUpdateUserStaus, dbparamsUserInfo, SqlHelper.ExecutionType.Procedure);


            return 1;
        }

        /// <summary>
        /// Delete Roles.
        /// </summary>
        /// <param name="roleId">roleId.</param>
        /// <returns>no. of rows affected in integer.</returns>
        public int DeleteRoles(string roleId)
        {
            List<DbParameter> dbparams = new List<DbParameter>();

            dbparams.Add(new SqlParameter { ParameterName = "@role_id", Value = roleId, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@status", Value = "R", SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@retval", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            Dictionary<string, dynamic> resultUserRoleXref = SqlHelper.ExecuteNonQuery<SqlConnection>(spDeleteRoles, dbparams, SqlHelper.ExecutionType.Procedure);



            return 1;
        }

        /// <summary>
        /// DeleteGroup.
        /// </summary>
        /// <param name="groupId">groupId.</param>
        /// <param name="status">status.</param>
        /// <returns>status in integer.</returns>
        public int DeleteGroup(string groupId, string status)
        {
            List<DbParameter> dbparams = new List<DbParameter>();

            dbparams.Add(new SqlParameter { ParameterName = "@recp_group_id", Value = groupId, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@status", Value = status, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparams.Add(new SqlParameter { ParameterName = "@retVal", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Output });
            Dictionary<string, dynamic> result = SqlHelper.ExecuteNonQuery<SqlConnection>(spDeleteGroup, dbparams, SqlHelper.ExecutionType.Procedure);

            return 1;
        }

        /// <summary>
        /// Get User Role List.
        /// </summary>
        /// <param name="roleId">roleId.</param>
        /// <returns>DtoUserList list.</returns>
        public List<DtoUserList> GetUserRoleList(string roleId)
        {
            List<DtoUserList> userroleLists = new List<DtoUserList>();

            List<DbParameter> dbparamsUserInfo = new List<DbParameter>();
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@query_name", Value = qnGetUserRoleList, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@User_Name", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@user_password", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@district_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@User_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@Role_Id", Value = roleId, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@Phone_num", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@Recp_Group_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });

            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(spGetUserInformation, dbparamsUserInfo, SqlHelper.ExecutionType.Procedure);
            if (dt != null && dt.Rows.Count > 0)
            {
                userroleLists = SqlHelper.ConvertDataTableToList<DtoUserList>(dt).ToList();
            }

            return userroleLists;
        }

        /// <summary>
        /// Insert Group Details.
        /// </summary>
        /// <param name="userGroupModel">groupDetails.</param>
        /// <returns>no. of rows affected in integer.</returns>
        public int InsertGroupDetails(UserGroupModel userGroupModel)
        {
            int groupId = 0;

            string status = string.Empty;
            if (userGroupModel.RecipientGroupModel.Status.Equals("Active"))
            {
                status = "A";
            }
            else if (userGroupModel.RecipientGroupModel.Status.Equals("Deactive"))
            {
                status = "D";
            }

            List<DbParameter> dbparamsUserInfo = new List<DbParameter>();

            if (string.IsNullOrEmpty(userGroupModel.RecipientGroupModel.RecipientGroupID))
            {
                dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@created_by", Value = userGroupModel.CreatedBy == null ? DBNull.Value : (object)userGroupModel.CreatedBy, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            }
            else if (!string.IsNullOrEmpty(userGroupModel.RecipientGroupModel.RecipientGroupID))
            {
                dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@recp_group_id", Value = string.IsNullOrEmpty(userGroupModel.RecipientGroupModel.RecipientGroupID) ? DBNull.Value : (object)Convert.ToInt32(userGroupModel.RecipientGroupModel.RecipientGroupID), SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@send_group_id", Value = string.IsNullOrEmpty(userGroupModel.SenderGroupModel.SenderGroupID) ? DBNull.Value : (object)Convert.ToInt32(userGroupModel.SenderGroupModel.SenderGroupID), SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            }

            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@recp_group_name", Value = string.IsNullOrEmpty(userGroupModel.RecipientGroupModel.RecipientName) ? DBNull.Value : (object)userGroupModel.RecipientGroupModel.RecipientName, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@recp_group_description", Value = string.IsNullOrEmpty(userGroupModel.RecipientGroupModel.RecipientDescription) ? DBNull.Value : (object)userGroupModel.RecipientGroupModel.RecipientDescription, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@recp_department", Value = string.IsNullOrEmpty(userGroupModel.RecipientGroupModel.RecipientDepartment) ? DBNull.Value : (object)userGroupModel.RecipientGroupModel.RecipientDepartment, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@recp_designation", Value = string.IsNullOrEmpty(userGroupModel.RecipientGroupModel.RecipientDesignation) ? DBNull.Value : (object)userGroupModel.RecipientGroupModel.RecipientDesignation, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@recp_division_id", Value = string.IsNullOrEmpty(userGroupModel.RecipientGroupModel.RecipientDivision) ? DBNull.Value : (object)userGroupModel.RecipientGroupModel.RecipientDivision, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@recp_district_id", Value = string.IsNullOrEmpty(userGroupModel.RecipientGroupModel.RecipientDistrict) ? DBNull.Value : (object)userGroupModel.RecipientGroupModel.RecipientDistrict, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@recp_sub_division_id", Value = string.IsNullOrEmpty(userGroupModel.RecipientGroupModel.RecipientSubDivision) ? DBNull.Value : (object)userGroupModel.RecipientGroupModel.RecipientSubDivision, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@recp_block_id", Value = string.IsNullOrEmpty(userGroupModel.RecipientGroupModel.RecipientBlock) ? DBNull.Value : (object)userGroupModel.RecipientGroupModel.RecipientBlock, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@recp_panchayat_id", Value = string.IsNullOrEmpty(userGroupModel.RecipientGroupModel.RecipientPanchayat) ? DBNull.Value : (object)userGroupModel.RecipientGroupModel.RecipientPanchayat, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@recp_user_id", Value = string.IsNullOrEmpty(userGroupModel.RecipientGroupModel.UserId) ? DBNull.Value : (object)userGroupModel.RecipientGroupModel.UserId, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@status", Value = string.IsNullOrEmpty(status) ? DBNull.Value : (object)status, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@retval", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Output });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@send_department", Value = string.IsNullOrEmpty(userGroupModel.SenderGroupModel.SenderDepartment) ? DBNull.Value : (object)userGroupModel.SenderGroupModel.SenderDepartment, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@send_designation", Value = string.IsNullOrEmpty(userGroupModel.SenderGroupModel.SenderDesignation) ? DBNull.Value : (object)userGroupModel.SenderGroupModel.SenderDesignation, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@send_division_id", Value = string.IsNullOrEmpty(userGroupModel.SenderGroupModel.SenderDivision) ? DBNull.Value : (object)userGroupModel.SenderGroupModel.SenderDivision, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@send_district_id", Value = string.IsNullOrEmpty(userGroupModel.SenderGroupModel.SenderDistrict) ? DBNull.Value : (object)userGroupModel.SenderGroupModel.SenderDistrict, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@send_user_id", Value = string.IsNullOrEmpty(userGroupModel.SenderGroupModel.UserId) ? DBNull.Value : (object)userGroupModel.SenderGroupModel.UserId, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });

            Dictionary<string, dynamic> result = SqlHelper.ExecuteNonQuery<SqlConnection>(qnInsertGroupDetails, dbparamsUserInfo, SqlHelper.ExecutionType.Procedure);

            groupId = result["@retval"];

            return groupId;
        }

        /// <summary>
        /// GetGroupList.
        /// </summary>
        /// <returns>DtoGroupList list.</returns>
        public List<DtoGroupList> GetGroupList()
        {
            List<DtoGroupList> groupList = new List<DtoGroupList>();

            List<DbParameter> dbparamsUserInfo = new List<DbParameter>();
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@query_name", Value = qnGetGroupList, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@User_Name", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@user_password", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@district_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@User_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@Role_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@Phone_num", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@Recp_Group_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });

            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(spGetUserInformation, dbparamsUserInfo, SqlHelper.ExecutionType.Procedure);
            if (dt != null && dt.Rows.Count > 0)
            {
                groupList = SqlHelper.ConvertDataTableToList<DtoGroupList>(dt);
            }

            return groupList;
        }

        /// <summary>
        /// Get Sender User List.
        /// </summary>
        /// <param name="groupId">GroupId.</param>
        /// <returns>DtoSenderUserList list.</returns>
        public List<DtoSenderUserList> GetSenderUserList(string groupId)
        {
            List<DtoSenderUserList> senderUsergroupList = new List<DtoSenderUserList>();
            List<DbParameter> dbparamsUserInfo = new List<DbParameter>();
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@query_name", Value = qnGetSenderUserList, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@User_Name", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@user_password", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@district_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@User_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@Role_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@Phone_num", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@Recp_Group_Id", Value = groupId, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });

            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(spGetUserInformation, dbparamsUserInfo, SqlHelper.ExecutionType.Procedure);
            if (dt != null && dt.Rows.Count > 0)
            {
                senderUsergroupList = SqlHelper.ConvertDataTableToList<DtoSenderUserList>(dt).ToList();
            }

            return senderUsergroupList;
        }

        /// <summary>
        /// Get Recipients User List.
        /// </summary>
        /// <param name="groupId">GroupId.</param>
        /// <returns>DtoRecipientUserList list.</returns>
        public List<DtoRecipientUserList> GetRecipientsUserList(string groupId)
        {
            List<DtoRecipientUserList> recipientUsergroupList = new List<DtoRecipientUserList>();
            List<DbParameter> dbparamsUserInfo = new List<DbParameter>();
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@query_name", Value = qnGetRecipientsUserList, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@User_Name", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@user_password", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@district_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@User_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@Role_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@Phone_num", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@Recp_Group_Id", Value = groupId, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });

            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(spGetUserInformation, dbparamsUserInfo, SqlHelper.ExecutionType.Procedure);
            if (dt != null && dt.Rows.Count > 0)
            {
                recipientUsergroupList = SqlHelper.ConvertDataTableToList<DtoRecipientUserList>(dt).ToList();
            }

            return recipientUsergroupList;
        }

        /// <summary>
        /// Update Group Status.
        /// </summary>
        /// <param name="groupStatus">groupStatus.</param>
        /// <returns>updated status in integer.</returns>
        public int UpdateGroupStatus(DtoGroupStatus groupStatus)
        {
            List<DbParameter> dbparamsUserInfo = new List<DbParameter>();
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@User_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@Role_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@Recp_Group_ID", Value = groupStatus.GroupID, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@status", Value = groupStatus.Status, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@comments", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@retval", Value = DBNull.Value, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            var resultUpdateGroupStatus = SqlHelper.ExecuteNonQuery<SqlConnection>(spUpdateUserStaus, dbparamsUserInfo, SqlHelper.ExecutionType.Procedure);


            return 1;
        }

        /// <summary>
        /// Get Group Details.
        /// </summary>
        /// <param name="groupID">groupID.</param>
        /// <returns>DtoGroupModel entity.</returns>
        public DtoGroupModel GetGroupDetails(string groupID)
        {
            DtoGroupModel groupInfo = null;
            List<DbParameter> dbparamsUserInfo = new List<DbParameter>();
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@query_name", Value = qnGetGroupDetails, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@User_Name", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@user_password", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@district_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@User_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@Role_Id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@Phone_num", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@Recp_Group_Id", Value = groupID, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });

            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(spGetUserInformation, dbparamsUserInfo, SqlHelper.ExecutionType.Procedure);
            if (dt != null && dt.Rows.Count > 0)
            {
                groupInfo = SqlHelper.ConvertDataTableToList<DtoGroupModel>(dt)[0];
            }

            return groupInfo;
        }

        /// <summary>
        /// Insert Transfer.
        /// </summary>
        /// <param name="transferUsers">transferUsers.</param>
        /// <returns>no. of rows affected in integer.</returns>
        public int InsertTransfer(List<TransferUser> transferUsers)
        {
            if (transferUsers.Any())
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("user_id", typeof(int));
                dt.Columns.Add("first_name", typeof(string));
                dt.Columns.Add("last_name", typeof(string));
                dt.Columns.Add("phone_num", typeof(string));
                dt.Columns.Add("division_id", typeof(string));
                dt.Columns.Add("district_id", typeof(string));
                dt.Columns.Add("sub_division_id", typeof(string));
                dt.Columns.Add("block_id", typeof(string));
                dt.Columns.Add("panchayat_id", typeof(string));
                dt.Columns.Add("data_entry_flg", typeof(string));
                dt.Columns.Add("applied_user_id", typeof(int));
                dt.Columns.Add("date_applied", typeof(DateTime));
                foreach (var item in transferUsers)
                {
                    dt.Rows.Add(item.User_id, item.First_name, item.Last_name, item.Phone_num, item.Division_id, item.District_id, item.Sub_Division_id, item.Block_id, item.Panchayat_id, item.Data_entry_flg, item.Applied_user_id, DateTime.Now);
                }

                List<DbParameter> dbparams = new List<DbParameter>();

                dbparams.Add(new SqlParameter { ParameterName = "@TransferDetails", Value = dt, SqlDbType = SqlDbType.Structured, Direction = ParameterDirection.Input });
                Dictionary<string, dynamic> result = SqlHelper.ExecuteNonQuery<SqlConnection>(spInsertTransfer, dbparams, SqlHelper.ExecutionType.Procedure);


                return 1;
            }

            return 0;
        }

        /// <summary>
        /// Get Transfer History.
        /// </summary>
        /// <param name="userId">userId.</param>
        /// <returns>TransferHistory list.</returns>
        public List<TransferHistory> GetTransferHistory(string userId)
        {
            List<TransferHistory> transferHistories = new List<TransferHistory>();

            List<DbParameter> dbparamsUserInfo = new List<DbParameter>();
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@applied_user_id", Value = Convert.ToInt32(userId), SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@userstatus", Value = "ALL", SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });

            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(spGetTransferHistory, dbparamsUserInfo, SqlHelper.ExecutionType.Procedure);

            if (dt != null && dt.Rows.Count > 0)
            {
                List<TransferHistoryDto> list = SqlHelper.ConvertDataTableToList<TransferHistoryDto>(dt);

                transferHistories = list.Select(x => new TransferHistory
                {
                    User_id = x.User_id,
                    User_name = x.User_name,
                    First_name = x.First_name,
                    Last_name = x.Last_name,
                    Department = x.Department,
                    Designation = x.Designation,
                    Phone_num = x.Phone_num,
                    Data_entry_flg = x.Data_entry_flg == "Y" ? "YES" : (x.Designation == "Assistant Agriculture Officer" || x.Designation == "Assistant Director" || x.Designation == "Assistant Statistical Officer" || x.Designation == "Block Agriculture Officer" || x.Designation == "Block Horticulture Officer" || x.Designation == "Assistant Director Agriculture Chemistry" || x.Designation == "Assistant Director Agriculture Engineering" || x.Designation == "Assistant Director Agronomy" || x.Designation == "Assistant Director Horticulture" || x.Designation == "Assistant Director Plant Protection" || x.Designation == "Assistant Director Soil Conservation") ? "NA" : "NO",
                    New_data_entry_flg = x.New_data_entry_flg == "Y" ? "YES" : (x.Designation == "Assistant Agriculture Officer" || x.Designation == "Assistant Director" || x.Designation == "Assistant Statistical Officer" || x.Designation == "Block Agriculture Officer" || x.Designation == "Block Horticulture Officer" || x.Designation == "Assistant Director Agriculture Chemistry" || x.Designation == "Assistant Director Agriculture Engineering" || x.Designation == "Assistant Director Agronomy" || x.Designation == "Assistant Director Horticulture" || x.Designation == "Assistant Director Plant Protection" || x.Designation == "Assistant Director Soil Conservation") ? "NA" : "NO",
                    District_name = x.District_name,
                    District_ID = x.District_ID,
                    Division_ID = x.Division_ID,
                    Division_name = x.Division_name,
                    SubDivision_ID = x.SubDivision_ID,
                    SubDivision_name = x.SubDivision_name,
                    New_Division_ID = x.New_Division_ID,
                    New_Division_name = x.New_Division_name,
                    New_District_ID = x.New_District_ID,
                    New_district_name = x.New_district_name,
                    New_block_id = x.New_block_id,
                    New_block_name = x.New_block_name,
                    New_panchayat_id = x.New_panchayat_id,
                    New_panchayat_name = x.New_panchayat_name,
                    New_SubDivision_ID = x.New_SubDivision_ID,
                    New_SubDivision_name = x.New_SubDivision_name,
                    Date_approved = x.Date_approved?.ToString("dddd, dd MMMM yyyy HH:mm:ss"),
                    Approval_status = x.Approval_status,
                    Date_applied = x.Date_applied,
                }).Where(x => x.Approval_status == "A").GroupBy(x => new { x.User_id, x.Date_applied, x.Approval_status }).Select(x => x.First()).ToList();

                foreach (var userdetail in transferHistories)
                {
                    userdetail.Panchayats = list.Where(x => x.User_id == userdetail.User_id && x.Date_applied == userdetail.Date_applied && x.Approval_status == userdetail.Approval_status && x.Panchayat_id != 0 && x.Panchayat_id != null).Select(x => new PanchayatModel { Panchayat_id = x.Panchayat_id, Panchayat_name = x.Panchayat_name }).GroupBy(x => x.Panchayat_id).Select(x => x.First()).ToList();

                    userdetail.Blocks = list.Where(x => x.User_id == userdetail.User_id && x.Date_applied == userdetail.Date_applied && x.Approval_status == userdetail.Approval_status && x.Block_id != 0 && x.Block_id != null).Select(x => new BlockLst { BlockId = x.Block_id, BlockName = x.Block_name }).GroupBy(x => x.BlockId).Select(x => x.First()).ToList();

                    userdetail.Newpanchayats = list.Where(x => x.User_id == userdetail.User_id && x.Date_applied == userdetail.Date_applied && x.Approval_status == userdetail.Approval_status && x.New_panchayat_id != 0 && x.New_panchayat_id != null).Select(x => new PanchayatModel { Panchayat_id = x.New_panchayat_id, Panchayat_name = x.New_panchayat_name }).GroupBy(x => x.Panchayat_id).Select(x => x.First()).ToList();

                    userdetail.Newblocks = list.Where(x => x.User_id == userdetail.User_id && x.Date_applied == userdetail.Date_applied && x.Approval_status == userdetail.Approval_status && x.New_block_id != 0 && x.New_block_id != null).Select(x => new BlockLst { BlockId = x.New_block_id, BlockName = x.New_block_name }).GroupBy(x => x.BlockId).Select(x => x.First()).ToList();
                }
            }

            return transferHistories;
        }

        /// <summary>
        /// Get Conflict Users.
        /// </summary>
        /// <param name="userId">userId.</param>
        /// <returns>ConflictUsers list.</returns>
        public List<ConflictUsers> GetConflictUsers(string userId)
        {
            List<ConflictUsers> returnConflictUsers = new List<ConflictUsers>();

            List<DbParameter> dbparamsUserInfo = new List<DbParameter>();
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@user_id", Value = userId, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });

            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(spGetConflictUsers, dbparamsUserInfo, SqlHelper.ExecutionType.Procedure);

            if (dt != null && dt.Rows.Count > 0)
            {
                List<ConflictUsers> listDTO = SqlHelper.ConvertDataTableToList<ConflictUsers>(dt);

                returnConflictUsers = listDTO.Select(x => new ConflictUsers
                {
                    First_name = x.First_name,
                    Last_name = x.Last_name,
                    Email_id = x.Email_id,
                    Approval_status = x.Approval_status,
                    Conflict_flag = x.Conflict_flag,
                    Department = x.Department,
                    Designation = x.Designation,
                    Phone_num = x.Phone_num,
                    Data_entry_flg = x.Data_entry_flg == "Y" ? "YES" : (x.Designation == "Assistant Agriculture Officer" || x.Designation == "Assistant Director" || x.Designation == "Assistant Statistical Officer" || x.Designation == "Block Agriculture Officer" || x.Designation == "Block Horticulture Officer" || x.Designation == "Assistant Director Agriculture Chemistry" || x.Designation == "Assistant Director Agriculture Engineering" || x.Designation == "Assistant Director Agronomy" || x.Designation == "Assistant Director Horticulture" || x.Designation == "Assistant Director Plant Protection" || x.Designation == "Assistant Director Soil Conservation") ? "NA" : "NO",
                    District_name = x.District_name,
                    Block_name = x.Block_name,
                    Panchayat_name = x.Panchayat_name,
                    Division_name = x.Division_name,
                    SubDivision_name = x.SubDivision_name,
                }).GroupBy(x => new { x.Email_id, x.District_name, x.Block_name, x.Panchayat_name }).Select(x => x.First()).ToList();
            }

            return returnConflictUsers;
        }

        /// <summary>
        /// Reset User Password.
        /// </summary>
        /// <param name="userName">userName.</param>
        /// <returns>status in integer.</returns>
        public int ResetUserPassword(string userName)
        {
            List<DbParameter> dbparamsUserInfo = new List<DbParameter>();
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@User_Name", Value = userName, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            Dictionary<string, dynamic> result = SqlHelper.ExecuteNonQuery<SqlConnection>(spResetUserPassword, dbparamsUserInfo, SqlHelper.ExecutionType.Procedure);



            return 1;
        }

        /// <summary>
        /// Save Refresh Token.
        /// </summary>
        /// <param name="userId">userId.</param>
        /// <param name="refreshToken">refreshToken.</param>
        /// <param name="refreshTokenExpiryTime">refreshTokenExpiryTime.</param>
        /// <returns>no. of rows affected in integer.</returns>
        public int SaveRefreshToken(string userId, string refreshToken, DateTime refreshTokenExpiryTime)
        {
            List<DbParameter> tokenInfo = new List<DbParameter>();
            tokenInfo.Add(new SqlParameter { ParameterName = "@User_Name", Value = userId, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            tokenInfo.Add(new SqlParameter { ParameterName = "@refresh_token", Value = refreshToken, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            tokenInfo.Add(new SqlParameter { ParameterName = "@token_expiry_ts", Value = refreshTokenExpiryTime, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
            tokenInfo.Add(new SqlParameter { ParameterName = "@query_name", Value = qnSaveRefreshToken, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            Dictionary<string, dynamic> result = SqlHelper.ExecuteNonQuery<SqlConnection>(spGetUserInformation, tokenInfo, SqlHelper.ExecutionType.Procedure);

            int insertRowsCount = result["RowsAffected"];

            return insertRowsCount;
        }

        /// <summary>
        /// GetToken.
        /// </summary>
        /// <param name="userId">userId.</param>
        /// <returns>TokenData as entity.</returns>
        public TokenData GetToken(string userId)
        {
            TokenData resultset = new TokenData();

            List<DbParameter> dbparamsUserInfo = new List<DbParameter>();
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@User_Name", Value = userId, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@query_name", Value = qnGetToken, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });

            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(spGetUserInformation, dbparamsUserInfo, SqlHelper.ExecutionType.Procedure);

            if (dt != null && dt.Rows.Count > 0)
            {
                resultset = SqlHelper.ConvertDataTableToList<TokenData>(dt)[0];
            }

            return resultset;
        }

        /// <summary>
        /// Save Notification Token.
        /// </summary>
        /// <param name="notification">notification.</param>
        /// <returns>status in string.</returns>
        public string SaveNotificationToken(PushNotificationInfo notification)
        {
            List<DbParameter> dbparamsUserInfo = new List<DbParameter>();
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@query_name", Value = qnSaveNotificationToken, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@User_Id", Value = notification.UserId, SqlDbType = SqlDbType.BigInt, Direction = ParameterDirection.Input });
            dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@notification_token", Value = notification.NotificationId, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });

            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(spGetUserInformation, dbparamsUserInfo, SqlHelper.ExecutionType.Procedure);

            if (dt != null && dt.Rows.Count > 0)
            {
                return dt.Rows[0][0].ToString() == "Y" ? "Updated Successfully" : "Not Updated";
            }

            return string.Empty;
        }

        /// <summary>
        /// Marque Messsage.
        /// </summary>
        /// <returns>status in string.</returns>
        public string MarqueMesssage()
        {
            List<DbParameter> dbparamsMessageInfo = new List<DbParameter>();
            dbparamsMessageInfo.Add(new SqlParameter { ParameterName = "@query_name", Value = qnMarqueMesssage, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsMessageInfo.Add(new SqlParameter { ParameterName = "@User_Name", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsMessageInfo.Add(new SqlParameter { ParameterName = "@recp_group_id", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsMessageInfo.Add(new SqlParameter { ParameterName = "@Msg_Subject", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsMessageInfo.Add(new SqlParameter { ParameterName = "@Msg_Desc", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            dbparamsMessageInfo.Add(new SqlParameter { ParameterName = "@Msg_Date", Value = DBNull.Value, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });

            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(spMarqueMesssage, dbparamsMessageInfo, SqlHelper.ExecutionType.Procedure);
            if (dt != null && dt.Rows.Count > 0)
            {
                return dt.Rows[0][0].ToString();
            }

            return string.Empty;
        }

        /// <summary>
        /// Send Notification.
        /// </summary>
        /// <param name="userId">userId.</param>
        /// <param name="body">body.</param>
        /// <param name="title">title.</param>
        /// <param name="token">token.</param>
        /// <param name="groupId">groupId.</param>
        /// <param name="skey">skey.</param>
        public void SendNotification(long userId, string body, string title, string token, long groupId, string skey)
        {
            PushNotify pushNotify = new PushNotify();
            var cookies = new CookieContainer();
            var webAddr = this.notification.Value.NotificationUrl;
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(webAddr);
            httpWebRequest.Timeout = 5000;
            httpWebRequest.CookieContainer = cookies;
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Headers.Add("Authorization:key=" + skey);
            httpWebRequest.Method = "POST";

            Notification notifications = new Notification();
            Data data = new Data();
            notifications.Body = body;
            notifications.Title = title;
            pushNotify.To = token;
            pushNotify.Priority = "HIGH";
            data.Id = "1";
            data.Status = "done";
            pushNotify.Data = data;
            pushNotify.Notification = notifications;
            string json = JsonConvert.SerializeObject(pushNotify);

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                string result = streamReader.ReadToEnd();
                NotificationResponse obj = new NotificationResponse();
                obj.GroupId = Convert.ToInt64(groupId);
                obj.UserId = userId;
                obj.Token = token;
                obj.Response = result.ToString();
                this.notificationResponses.Add(obj);
                streamReader.Close();
            }
        }

        /// <summary>
        /// Send Notification.
        /// </summary>
        /// <param name="notify">notify.</param>
        /// <returns>NotificationResponse list.</returns>
        public List<NotificationResponse> SendNotification(NotificationItem notify)
        {
            string serverKey = this.notification.Value.ServerKey;
            DataTable dt;
            if (notify.GroupId != string.Empty)
            {
                string[] groupArray = notify.GroupId.Split(',');
                for (int l = 0; l <= groupArray.Length - 1; l++)
                {
                    List<DbParameter> dbparamsUserInfo = new List<DbParameter>();
                    dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@query_name", Value = qnSendNotification, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                    dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@Recp_Group_Id", Value = Convert.ToInt64(groupArray[l]), SqlDbType = SqlDbType.BigInt, Direction = ParameterDirection.Input });
                    dt = SqlHelper.ExecuteSelect<SqlConnection>(spGetUserInformation, dbparamsUserInfo, SqlHelper.ExecutionType.Procedure);

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        for (int i = 0; i <= dt.Rows.Count - 1; i++)
                        {
                            if (!string.IsNullOrEmpty(dt.Rows[i][2].ToString()))
                            {
                                this.SendNotification(Convert.ToInt64(dt.Rows[i][1]), notify.Body, notify.Title, dt.Rows[i][2].ToString(), Convert.ToInt64(groupArray[l]), serverKey);
                            }
                        }
                    }
                }

                List<DbParameter> dbparamsUser = new List<DbParameter>();
                dbparamsUser.Add(new SqlParameter { ParameterName = "@query_name", Value = qnGetUserNotification, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsUser.Add(new SqlParameter { ParameterName = "@User_Id", Value = notify.UserId, SqlDbType = SqlDbType.BigInt, Direction = ParameterDirection.Input });

                dt = SqlHelper.ExecuteSelect<SqlConnection>(spGetUserInformation, dbparamsUser, SqlHelper.ExecutionType.Procedure);

                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i <= dt.Rows.Count - 1; i++)
                    {
                        if (!string.IsNullOrEmpty(dt.Rows[i][2].ToString()))
                        {
                            this.SendNotification(notify.UserId, notify.Body, notify.Title, dt.Rows[i][1].ToString(), Convert.ToInt64(0), serverKey);
                        }
                    }
                }
            }

            return this.notificationResponses;
        }
    }
}
