//------------------------------------------------------------------------------
// <Copyright file="CSReportDataRepository.cs" company="Government of Bihar">
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
    /// CSReportDataRepository.
    /// </summary>
    public class CSReportDataRepository : BaseRepository, ICSReportRepository
    {
        private static string spGetCSVReports = "usp_getdata_cs";
        private readonly IOptions<DBSettings> options;

        /// <summary>
        /// Initializes a new instance of the <see cref="CSReportDataRepository"/> class.
        /// CSReportDataRepository.
        /// </summary>
        /// <param name="config">config.</param>
        /// <param name="options">options.</param>
        public CSReportDataRepository(IConfiguration config, IOptions<DBSettings> options)
            : base(config)
        {
            this.options = options;
            SqlHelper.SetConnectionString(this.options.Value.ConnectionString);
        }

        /// <summary>
        /// GetTotalUsersCount.
        /// </summary>
        /// <returns>List.</returns>
        public List<DtoUserCount> GetTotalUsersCount()
        {
            List<DtoUserCount> result = new List<DtoUserCount>();

            string query = "select dist.district_name, count(distinct usr.user_id) as TotalUsersCount from app_user usr, user_lgdir_xref lg, lg_dir_district_dim dist where dist.district_id = lg.district_id and lg.user_id = usr.user_id group by dist.district_name;";

            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(query, null, SqlHelper.ExecutionType.Query);

            if (dt != null && dt.Rows.Count > 0)
            {
                List<UsersCount> list = SqlHelper.ConvertDataTableToList<UsersCount>(dt);
                result = list.Select(x => new DtoUserCount
                {
                    District = x.District_name,
                    Totaluserscount = x.TotalUsersCount,
                }).ToList();
            }

            return result;
        }

        /// <summary>
        /// GetUsersLoggedInCount.
        /// </summary>
        /// <returns>List.</returns>
        public List<DtoUserCount> GetUsersLoggedInCount()
        {
            List<DtoUserCount> result = new List<DtoUserCount>();

            string query = "select  dist.district_name, count(distinct aud.user_id) as TotalUsersCount from user_activity_audit aud, user_lgdir_xref lg, lg_dir_district_dim dist where dist.district_id = lg.district_id and lg.user_id = aud.user_id group by  dist.district_name;";

            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(query, null, SqlHelper.ExecutionType.Query);

            if (dt != null && dt.Rows.Count > 0)
            {
                List<UsersCount> list = SqlHelper.ConvertDataTableToList<UsersCount>(dt);
                result = list.Select(x => new DtoUserCount
                {
                    District = x.District_name,
                    Totaluserscount = x.TotalUsersCount,
                }).ToList();
            }

            return result;
        }

        /// <summary>
        /// GetUsersNotLoggedInCount.
        /// </summary>
        /// <returns>List.</returns>
        public List<DtoUserCount> GetUsersNotLoggedInCount()
        {
            List<DtoUserCount> result = new List<DtoUserCount>();

            string query = "select dist.district_name, count(distinct usr.user_id) as TotalUsersCount from app_user usr, user_lgdir_xref lg, lg_dir_district_dim dist where dist.district_id = lg.district_id and lg.user_id = usr.user_id and not exists (select 1 from user_activity_audit where user_id = usr.user_id) group by dist.district_name;";

            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(query, null, SqlHelper.ExecutionType.Query);

            if (dt != null && dt.Rows.Count > 0)
            {
                List<UsersCount> list = SqlHelper.ConvertDataTableToList<UsersCount>(dt);
                result = list.Select(x => new DtoUserCount
                {
                    District = x.District_name,
                    Totaluserscount = x.TotalUsersCount,
                }).ToList();
            }

            return result;
        }

        /// <summary>
        /// GetBihanStats.
        /// </summary>
        /// <returns>List.</returns>
        public List<DtoBihanStats> GetBihanStats()
        {
            List<Bihan1Stats> list;

            List<DtoBihanStats> result = new List<DtoBihanStats>();

            string query = "with user_dtl as (Select b.district_name , count(distinct a.user_id) \"User_Count_using_v1\" from user_lgdir_xref a, LG_DIR_DIM b, app_user c, user_activity_audit d where a.district_id = b.district_id and a.user_id = c.user_id and c.user_id = d.user_id and d.activity_ts >= '2021-01-11' and c.user_name not like '%disys%' group by b.district_name), tgt_dtl as (select c.district_name, count(distinct AC_Submitted_userid) Count_of_users_submitted_target from[dbo].[crop_cvrg_target_pnchyt] a, user_lgdir_xref b, LG_DIR_DIM c where a.AC_Submitted_userid = b.user_id and b.district_id = c.district_id group by c.district_name), actl_dtl as (select c.district_name, count(distinct AC_Submitted_userid) Count_of_users_submitted_actl from [dbo].[crop_cvrg_actl_pnchyt] a, user_lgdir_xref b, LG_DIR_DIM c where a.AC_Submitted_userid = b.user_id and b.district_id = c.district_id group by c.district_name) select a.district_name as Districts, a.\"User_Count_using_v1\" , b.Count_of_users_submitted_target,c.Count_of_users_submitted_actl from user_dtl a left join tgt_dtl b on a.district_name = b.district_name left join actl_dtl c on a.district_name = c.district_name";

            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(query, null, SqlHelper.ExecutionType.Query);

            if (dt != null && dt.Rows.Count > 0)
            {
                list = SqlHelper.ConvertDataTableToList<Bihan1Stats>(dt);
                result = list.Select(x => new DtoBihanStats
                {
                    User_count_using_v1 = x.User_Count_using_v1,
                    Count_of_users_submitted_actual = x.Count_of_users_submitted_actl,
                    Count_of_users_submitted_target = x.Count_of_users_submitted_target,
                }).ToList();
            }

            return result;
        }

        /// <summary>
        /// GetNotLoggedInUserList.
        /// </summary>
        /// <returns>List.</returns>
        public List<NotLoggedInUserList> GetNotLoggedInUserList()
        {
            List<NotLoggedInUserList> list = new List<NotLoggedInUserList>();

            string query = "select distinct first_name, last_name, email_id, phone_num, department, designation, dist.district_name from app_user usr, user_lgdir_xref lg, lg_dir_district_dim dist where dist.district_id = lg.district_id and lg.user_id = usr.user_id and not exists(select 1 from user_activity_audit where user_id = usr.user_id);";

            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(query, null, SqlHelper.ExecutionType.Query);

            if (dt != null && dt.Rows.Count > 0)
            {
                list = SqlHelper.ConvertDataTableToList<NotLoggedInUserList>(dt);
            }

            return list;
        }

        /// <summary>
        /// GetACNotSubmittedTargetList.
        /// </summary>
        /// <returns>List.</returns>
        public List<ACNotSubmittedTargetList> GetACNotSubmittedTargetList()
        {
            List<ACNotSubmittedTargetList> list = new List<ACNotSubmittedTargetList>();

            string query = "select distinct first_name, last_name, email_id, phone_num, department, designation, dist.district_name, dist.block_name, dist.panchayat_name  from app_user usr, user_lgdir_xref lg, lg_dir_dim dist where dist.district_id = lg.district_id and dist.block_id = lg.block_id and dist.panchayat_id = lg.panchayat_id and lg.user_id = usr.user_id and designation in ('Kisan Salahkar', 'Agriculture Coordinator') and not exists(select 1 from crop_cvrg_actl_pnchyt where AC_Submitted_userid = usr.user_id) order by 7,8,9,5,6,1,2,3;";

            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(query, null, SqlHelper.ExecutionType.Query);

            if (dt != null && dt.Rows.Count > 0)
            {
                list = SqlHelper.ConvertDataTableToList<ACNotSubmittedTargetList>(dt);
            }

            return list;
        }

        /// <summary>
        /// GetCSVReports.
        /// </summary>
        /// <param name="id">id.</param>
        /// <returns>Datatable List result.</returns>
        public dynamic GetCSVReports(string id)
        {
            List<UsersCount> resultset = new List<UsersCount>();
            List<DbParameter> parameters = new List<DbParameter>();
            parameters.Add(new SqlParameter { ParameterName = "@query_name", Value = id, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(spGetCSVReports, parameters, SqlHelper.ExecutionType.Procedure);

            if (dt != null && dt.Rows.Count > 0)
            {
                if (id == "UserCnt_By_District" || id == "LoginCnt_By_District" || id == "NoLoginCnt_By_District")
                {
                    List<UsersCount> list = SqlHelper.ConvertDataTableToList<UsersCount>(dt);
                    List<DtoUserCount> result = list.Select(x => new DtoUserCount
                    {
                        District = x.District_name,
                        Totaluserscount = x.TotalUsersCount,
                    }).ToList();

                    return result;
                }
                else if (id == "Bihan_Stats")
                {
                    List<Bihan1Stats> list;
                    List<DtoBihanStats> result;
                    list = SqlHelper.ConvertDataTableToList<Bihan1Stats>(dt);
                    result = list.Select(x => new DtoBihanStats
                    {
                        Districts = x.Districts,
                        User_count_using_v1 = x.User_Count_using_v1,
                        Count_of_users_submitted_actual = x.Count_of_users_submitted_actl,
                        Count_of_users_submitted_target = x.Count_of_users_submitted_target,
                    }).ToList();

                    return result;
                }
                else if (id == "NoLogin_User_List")
                {
                    List<NotLoggedInUserList> list = SqlHelper.ConvertDataTableToList<NotLoggedInUserList>(dt);

                    return list;
                }
                else if (id == "NoTgt_User_List")
                {
                    List<ACNotSubmittedTargetList> list = SqlHelper.ConvertDataTableToList<ACNotSubmittedTargetList>(dt);
                    return list;
                }
                else if (id == "UserRegCnt_By_District")
                {
                    List<UserRegCntByDistrict> list = SqlHelper.ConvertDataTableToList<UserRegCntByDistrict>(dt);
                    return list;
                }
            }

            return resultset;
        }

        /// <summary>
        /// GetConsolidatedCSReports.
        /// </summary>
        /// <returns>List.</returns>
        public CSReportConsolidated GetConsolidatedCSReports()
        {
            List<string> lst = new List<string>() { "All_Counts", "NoLogin_User_List", "NoTgt_User_List", "NoCvrg_User_List", "Cvrg_Exceed_150_List", "Target_50_BlkAvg_List", "Target_50_PanchytAvg_List" };
            CSReportConsolidated resultset = new CSReportConsolidated();
            List<DbParameter> parameters = new List<DbParameter>();
            foreach (var id in lst)
                {
                    parameters.Add(new SqlParameter { ParameterName = "@query_name", Value = id, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                    DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(spGetCSVReports, parameters, SqlHelper.ExecutionType.Procedure);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        if (id == "All_Counts")
                        {
                            resultset.AllCounts = SqlHelper.ConvertDataTableToList<AllCount>(dt);
                        }
                        else if (id == "NoLogin_User_List")
                        {
                            resultset.NotLoggedInUserLists = SqlHelper.ConvertDataTableToList<NotLoggedInUserList>(dt);
                        }
                        else if (id == "NoTgt_User_List")
                        {
                            resultset.ACNotSubmittedTargetLists = SqlHelper.ConvertDataTableToList<ACNotSubmittedTargetList>(dt);
                        }
                        else if (id == "NoCvrg_User_List")
                        {
                            resultset.NoCoverageUserLists = SqlHelper.ConvertDataTableToList<NoCoverageUserList>(dt);
                        }
                        else if (id == "Cvrg_Exceed_150_List")
                        {
                            resultset.CoverageExceed150Lists = SqlHelper.ConvertDataTableToList<CoverageExceed150List>(dt);
                        }
                        else if (id == "Target_50_BlkAvg_List")
                        {
                            resultset.Target50BlkAvgLists = SqlHelper.ConvertDataTableToList<Target50BlkAvgList>(dt);
                        }
                        else if (id == "Target_50_PanchytAvg_List")
                        {
                            resultset.Target50PanchytAvgLists = SqlHelper.ConvertDataTableToList<Target50PanchytAvgList>(dt);
                        }
                    }
                }

            return resultset;
        }
    }
}
