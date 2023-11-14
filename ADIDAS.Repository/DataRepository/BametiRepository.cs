//------------------------------------------------------------------------------
// <copyright file="BametiRepository.cs" company="Government of Bihar">
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
    using System.Globalization;
    using System.Linq;
    using ADIDAS.Model.DTO;
    using ADIDAS.Model.Entities;
    using ADIDAS.Repository.Helper;
    using ADIDAS.Repository.Interfaces;
    using DepartmentOfAgriculture.Admin.Models.DTO;
    using DepartmentOfAgriculture.Admin.Models.Models;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Options;

    /// <summary>
    /// BametiRepository.
    /// </summary>
    public class BametiRepository : BaseRepository, IBametiRepository
    {
        private readonly IOptions<DBSettings> options;
        private static string spInsertBametiTemplate = "USP_Insert_BAMETI_template";
        private static string qnInsertTargetSetting = "PostTargetSetting";
        private static string spBametiProgram = "usp_getdata_bameti_program";
        private static string qnGetBametiSchemes = "GetAllBametiSchemes";
        private static string qnGetBametiSchemesbyDesignation = "GetBametiSchemeByDesgn";
        private static string qnDeleteBametiData = "DeleteGridItems";
        private static string qnGetBametiActivities = "GetBametiActivityByschemeID";
        private static string qnGetBametiActivitiesDesgn = "GetBametiActivityBySchemeIDOrDesgn";
        private static string qnGetBametiDesignation = "GetBametiDesgnBySchemeOrActivity";
        private static string qnGetBametiFields = "GetBametiFieldsBySchemeOrActivityOrDesgn";
        private static string qnGetBametiAllFields = "GetAllFields";
        private static string qnGetBametiTarget = "GetAllTargetSettings";
        private static string qnGetBametiCreateProgram = "GetCreatePrograms";
        private static string qnGetBametiTargetUOM = "GetTargetqtyOrUOM";
        private static string qnGetUOMBasedonSchemeActvity = "GetUOMBySchemeOrActivity";
        private static string qnGetBametiViewProgram = "GetViewPrograms";
        private static string qnGetBametiEditProgram = "GetViewEditPrograms";
        private static string qnGetTopic = "GetAllTopic";
        private static string spInsertBametiGridData = "USP_Insert_bameti_Program_hdr";
        private static string spInsertBametiGridDatadtl = "USP_Insert_bameti_Program_dtl";
        private static string qnGetLastUpdatedRow = "GetRowNo";
        private static string qnGetLastHeaderUpdatedRow = "GetHeaderID";
        private static string qnGetLastUpdatedTemplateId = "GetTemplateRowNo";
        private static string qnDeleteBametiTemplate = "DeleteTemplate";

        /// <summary>
        /// Initializes a new instance of the <see cref="BametiRepository"/> class.
        /// </summary>
        /// <param name="config">Config.</param>
        /// <param name="options">Options.</param>
        public BametiRepository(IConfiguration config, IOptions<DBSettings> options)
            : base(config)
        {
            this.options = options;
            SqlHelper.SetConnectionString(this.options.Value.ConnectionString);
        }

        /// <summary>
        /// InsertBametiTemplate.
        /// </summary>
        /// <param name="bametiTemplate">bametiTemplate.</param>
        /// <returns>Value.</returns>
        public int InsertBametiTemplate(BametiTemplate bametiTemplate)
        {
            int templateId = this.GetLastUpdatedTemplateId();
            int maxTemplateId = templateId + 1;
            List<string> data = new List<string>();
            foreach (var field in bametiTemplate.Field_id.Split(","))
            {
                List<DbParameter> dbparamstemplate = new List<DbParameter>();
                dbparamstemplate.Add(new SqlParameter { ParameterName = "@template_id", Value = maxTemplateId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                dbparamstemplate.Add(new SqlParameter { ParameterName = "@scheme_id", Value = bametiTemplate.Scheme_id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                dbparamstemplate.Add(new SqlParameter { ParameterName = "@activity_id", Value = bametiTemplate.Activity_id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                dbparamstemplate.Add(new SqlParameter { ParameterName = "@designation", Value = bametiTemplate.Designation, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamstemplate.Add(new SqlParameter { ParameterName = "@field_id", Value = field, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamstemplate.Add(new SqlParameter { ParameterName = "@user_id", Value = bametiTemplate.User_id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });

                DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(spInsertBametiTemplate, dbparamstemplate, SqlHelper.ExecutionType.Procedure);

                if (dt != null && dt.Rows.Count > 0)
                {
                    data.Add(!string.IsNullOrEmpty(dt.Rows[0]["Column1"].ToString()) ? dt.Rows[0]["Column1"].ToString() : string.Empty);
                }
            }

            if (!data.Any())
            {
                return 0;
            }
            else if (data.Any(x => x == "F"))
            {
                return -1;
            }
            else if (data.Any(x => string.IsNullOrEmpty(x)))
            {
                return -2;
            }

            return 1;
        }

        /// <summary>
        /// InsertTargetSetting.
        /// </summary>
        /// <param name="setting">setting.</param>
        /// <returns>Value.</returns>
        public int InsertTargetSetting(List<PostTargetSetting> setting)
        {
            foreach (var field in setting)
            {
                List<DbParameter> dbparamstemplate = new List<DbParameter>();
                dbparamstemplate.Add(new SqlParameter { ParameterName = "@District_ID", Value = field.DistrictId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                dbparamstemplate.Add(new SqlParameter { ParameterName = "@scheme_id", Value = field.SchemeId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                dbparamstemplate.Add(new SqlParameter { ParameterName = "@activity_id", Value = field.ActivityId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                dbparamstemplate.Add(new SqlParameter { ParameterName = "@Period", Value = field.Period, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamstemplate.Add(new SqlParameter { ParameterName = "@target_qty", Value = field.Targetqty, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                dbparamstemplate.Add(new SqlParameter { ParameterName = "@UOM", Value = field.Uom, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamstemplate.Add(new SqlParameter { ParameterName = "@user_id", Value = field.UserId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
                dbparamstemplate.Add(new SqlParameter { ParameterName = "@query_name", Value = qnInsertTargetSetting, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                Dictionary<string, dynamic> result = SqlHelper.ExecuteNonQuery<SqlConnection>(spBametiProgram, dbparamstemplate, SqlHelper.ExecutionType.Procedure);
            }

            return 1;
        }

        /// <summary>
        /// DeleteBametiTemplate.
        /// </summary>
        /// <param name="bametiTemplate">bametiTemplate.</param>
        /// <returns>values.</returns>
        public int DeleteBametiTemplate(DeleteTemplate bametiTemplate)
        {
            List<DbParameter> dbparamstemplate = new List<DbParameter>();
            dbparamstemplate.Add(new SqlParameter { ParameterName = "@template_id", Value = bametiTemplate.TemplateId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            dbparamstemplate.Add(new SqlParameter { ParameterName = "@bameti_xref_id", Value = bametiTemplate.Xref, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            dbparamstemplate.Add(new SqlParameter { ParameterName = "@query_name", Value = qnDeleteBametiTemplate, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            SqlHelper.ExecuteNonQuery<SqlConnection>(spBametiProgram, dbparamstemplate, SqlHelper.ExecutionType.Procedure);

            return 1;
        }

        /// <summary>
        /// GetBametiSchemes.
        /// </summary>
        /// <returns>Some List.</returns>
        public List<BametiScheme> GetBametiSchemes()
        {
            List<BametiScheme> list = new List<BametiScheme>();

            List<DbParameter> parameters = new List<DbParameter>();

            parameters.Add(new SqlParameter { ParameterName = "@query_name", Value = qnGetBametiSchemes, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(spBametiProgram, parameters, SqlHelper.ExecutionType.Procedure);

            if (dt != null && dt.Rows.Count > 0)
            {
                list = SqlHelper.ConvertDataTableToList<BametiScheme>(dt);
            }

            return list;
        }

        /// <summary>
        /// GetBametiSchemesbyDesignation.
        /// </summary>
        /// <param name="designation">designation.</param>
        /// <returns>Some list.</returns>
        public List<BametiScheme> GetBametiSchemesbyDesignation(string designation)
        {
            List<BametiScheme> list = new List<BametiScheme>();

            List<DbParameter> parameters = new List<DbParameter>();

            parameters.Add(new SqlParameter { ParameterName = "@query_name", Value = qnGetBametiSchemesbyDesignation, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@designation", Value = designation, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });

            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(spBametiProgram, parameters, SqlHelper.ExecutionType.Procedure);
            if (dt != null && dt.Rows.Count > 0)
            {
                list = SqlHelper.ConvertDataTableToList<BametiScheme>(dt);
            }

            return list;
        }

        /// <summary>
        /// DeleteBametiData.
        /// </summary>
        /// <param name="templateId">templateId.</param>
        /// <param name="headerId">headerId.</param>
        /// <param name="rowno">rowno.</param>
        /// <returns>value.</returns>
        public int DeleteBametiData(int templateId, int headerId, int rowno)
        {
            List<DbParameter> parameters = new List<DbParameter>();

            parameters.Add(new SqlParameter { ParameterName = "@query_name", Value = qnDeleteBametiData, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@header_id", Value = headerId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@row_no", Value = rowno, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@template_id", Value = templateId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });

            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(spBametiProgram, parameters, SqlHelper.ExecutionType.Procedure);

            if (dt != null && dt.Rows.Count > 0)
            {
                string gdata = dt.Rows[0]["Column1"].ToString();

                if (gdata == "S")
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }

            return 0;
        }

        /// <summary>
        /// GetBametiActivities.
        /// </summary>
        /// <param name="schemeId">schemeId.</param>
        /// <returns>List.</returns>
        public List<BametiActivity> GetBametiActivities(string schemeId)
        {
            List<BametiActivity> list = new List<BametiActivity>();

            List<DbParameter> parameters = new List<DbParameter>();

            parameters.Add(new SqlParameter { ParameterName = "@query_name", Value = qnGetBametiActivities, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@scheme_id", Value = schemeId, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });

            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(spBametiProgram, parameters, SqlHelper.ExecutionType.Procedure);
            if (dt != null && dt.Rows.Count > 0)
            {
                list = SqlHelper.ConvertDataTableToList<BametiActivity>(dt);
            }

            return list;
        }

        /// <summary>
        /// GetBametiActivities.
        /// </summary>
        /// <param name="schemeId">schemeId.</param>
        /// <param name="designation">designation.</param>
        /// <returns>List.</returns>
        public List<BametiActivity> GetBametiActivities(string schemeId, string designation)
        {
            List<BametiActivity> list = new List<BametiActivity>();

            List<DbParameter> parameters = new List<DbParameter>();

            parameters.Add(new SqlParameter { ParameterName = "@query_name", Value = qnGetBametiActivitiesDesgn, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@scheme_id", Value = schemeId, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@designation", Value = designation, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(spBametiProgram, parameters, SqlHelper.ExecutionType.Procedure);
            if (dt != null && dt.Rows.Count > 0)
            {
                list = SqlHelper.ConvertDataTableToList<BametiActivity>(dt).GroupBy(x => x.Activity_id).Select(x => x.First()).ToList();
            }

            return list;
        }

        /// <summary>
        /// GetBametiDesignation.
        /// </summary>
        /// <param name="schemeId">schemeId.</param>
        /// <param name="activityId">activityId.</param>
        /// <returns>List.</returns>
        public List<string> GetBametiDesignation(string schemeId, string activityId)
        {
            List<string> list = new List<string>();

            List<DbParameter> parameters = new List<DbParameter>();

            parameters.Add(new SqlParameter { ParameterName = "@query_name", Value = qnGetBametiDesignation, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@scheme_id", Value = schemeId, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@activity_id", Value = activityId, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });

            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(spBametiProgram, parameters, SqlHelper.ExecutionType.Procedure);
            if (dt != null && dt.Rows.Count > 0)
            {
                list = SqlHelper.ConvertDataTableToList<BametiDesignation>(dt).Select(x => x.Designation).ToList();
            }

            return list;
        }

        /// <summary>
        /// GetBametiFields.
        /// </summary>
        /// <param name="schemeId">schemeId.</param>
        /// <param name="activityId">activityId.</param>
        /// <param name="designation">designation.</param>
        /// <returns>List.</returns>
        public List<string> GetBametiFields(int schemeId, int activityId, string designation)
        {
            List<string> list = new List<string>();

            List<DbParameter> parameters = new List<DbParameter>();

            parameters.Add(new SqlParameter { ParameterName = "@query_name", Value = qnGetBametiFields, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@scheme_id", Value = schemeId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@activity_id", Value = activityId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@designation", Value = designation, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(spBametiProgram, parameters, SqlHelper.ExecutionType.Procedure);
            if (dt != null && dt.Rows.Count > 0)
            {
                list = SqlHelper.ConvertDataTableToList<BametiFields>(dt).Select(x => x.Field_name).ToList();
            }

            return list;
        }

        /// <summary>
        /// GetBametiAllFields.
        /// </summary>
        /// <returns>List.</returns>
        public List<BametiAllFields> GetBametiAllFields()
        {
            List<BametiAllFields> list = new List<BametiAllFields>();

            List<DbParameter> parameters = new List<DbParameter>();

            parameters.Add(new SqlParameter { ParameterName = "@query_name", Value = qnGetBametiAllFields, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });

            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(spBametiProgram, parameters, SqlHelper.ExecutionType.Procedure);
            if (dt != null && dt.Rows.Count > 0)
            {
                list = SqlHelper.ConvertDataTableToList<BametiAllFields>(dt).ToList();
            }

            return list;
        }

        /// <summary>
        /// GetBametiTarget.
        /// </summary>
        /// <param name="request">request.</param>
        /// <returns>List.</returns>
        public List<BametiTargetGet> GetBametiTarget(BametiTargetRequestDto request)
        {
            List<BametiTargetGet> temp = new List<BametiTargetGet>();

            List<DbParameter> parameters = new List<DbParameter>();

            parameters.Add(new SqlParameter { ParameterName = "@query_name", Value = qnGetBametiTarget, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@scheme_id", Value = request.Scheme_id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@activity_id", Value = request.Activity_id, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@Period", Value = request.Period, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@UOM", Value = request.Uom, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(spBametiProgram, parameters, SqlHelper.ExecutionType.Procedure);
            if (dt != null && dt.Rows.Count > 0)
            {
                temp = SqlHelper.ConvertDataTableToList<BametiTargetGet>(dt);

                foreach (var item in temp)
                {
                    item.Scheme_id = request.Scheme_id;
                    item.Activity_id = request.Activity_id;
                    item.Period = request.Period;
                }
            }

            return temp;
        }

        /// <summary>
        /// GetBametiAdminTemplate.
        /// </summary>
        /// <param name="schemeId">schemeId.</param>
        /// <param name="activityId">activityId.</param>
        /// <param name="designation">designation.</param>
        /// <returns>List.</returns>
        public List<BametiTemplateGet> GetBametiAdminTemplate(int schemeId, int activityId, string designation)
        {
            List<BametiTemplateGet> list = new List<BametiTemplateGet>();

            List<DbParameter> parameters = new List<DbParameter>();

            parameters.Add(new SqlParameter { ParameterName = "@query_name", Value = qnGetBametiFields, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@scheme_id", Value = schemeId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@activity_id", Value = activityId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@designation", Value = designation, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(spBametiProgram, parameters, SqlHelper.ExecutionType.Procedure);
            if (dt != null && dt.Rows.Count > 0)
            {
                List<BametiTemplateGet> temp = SqlHelper.ConvertDataTableToList<BametiTemplateGet>(dt);

                if (temp.Any())
                {
                    list = temp.Select(x => new BametiTemplateGet
                    {
                        Template_id = x.Template_id,
                        Field_id = x.Field_id,
                        Conflict_Flag = x.Conflict_Flag,
                        Bameti_xref_id = x.Bameti_xref_id,
                        Scheme_id = x.Scheme_id,
                        Field_category = x.Field_category,
                        Activity_id = x.Activity_id,
                        Designation = x.Designation,
                        Scheme_name = x.Scheme_name,
                        Activity_name = x.Activity_name,
                    })?.GroupBy(x => new { x.Activity_name, x.Scheme_name, x.Designation }).Select(x => x.First()).ToList();

                    foreach (var item in list)
                    {
                        item.Field_name = string.Join(",", temp.Where(x => x.Scheme_id == item.Scheme_id && x.Activity_id == item.Activity_id && designation == item.Designation && x.Field_category == "HEADER").Select(x => x.Field_name).Distinct().ToList());
                        item.Detail_field_name = string.Join(",", temp.Where(x => x.Scheme_id == item.Scheme_id && x.Activity_id == item.Activity_id && designation == item.Designation && x.Field_category == "DETAIL").Select(x => x.Field_name).Distinct().ToList());
                    }
                }
            }

            return list;
        }

        /// <summary>
        /// GetBametiCreateProgram.
        /// </summary>
        /// <param name="schemeId">schemeId.</param>
        /// <param name="activityId">activityId.</param>
        /// <param name="designation">designation.</param>
        /// <returns>List.</returns>
        public List<DtoCreateProgram> GetBametiCreateProgram(int schemeId, int activityId, string designation)
        {
            List<DtoCreateProgram> list = new List<DtoCreateProgram>();

            List<DbParameter> parameters = new List<DbParameter>();

            parameters.Add(new SqlParameter { ParameterName = "@query_name", Value = qnGetBametiCreateProgram, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@scheme_id", Value = schemeId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@activity_id", Value = activityId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@designation", Value = designation, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(spBametiProgram, parameters, SqlHelper.ExecutionType.Procedure);

            if (dt != null && dt.Rows.Count > 0)
            {
                List<BametiCreateProgram> temp = SqlHelper.ConvertDataTableToList<BametiCreateProgram>(dt);
                if (temp.Any())
                {
                    list = temp.Select(x => new DtoCreateProgram
                    {
                        Template_id = x.Template_id,
                        Field_id = x.Field_id,
                        Field_name = x.Field_name,
                        Field_type = x.Field_type,
                        Field_category = x.Field_category,
                        Field_value = x.Field_value,
                    })?.GroupBy(x => new { x.Field_id, x.Field_name, x.Field_type }).Select(x => x.First()).ToList();

                    foreach (var item in list)
                    {
                        var tempobj = temp.Where(x => x.Field_id == item.Field_id && x.List_value != null).Select(x => x.List_value).Distinct()?.ToList();

                        if (tempobj != null && tempobj.Any())
                        {
                            item.List_value.AddRange(tempobj);
                        }
                    }
                }
            }

            return list;
        }

        /// <summary>
        /// GetBametiTargetUOM.
        /// </summary>
        /// <param name="schemeId">schemeId.</param>
        /// <param name="activityId">activityId.</param>
        /// <param name="districtId">districtId.</param>
        /// <returns>Model Values.</returns>
        public TargetUom GetBametiTargetUOM(int schemeId, int activityId, string districtId)
        {
            TargetUom temp = null;

            List<DbParameter> parameters = new List<DbParameter>();

            parameters.Add(new SqlParameter { ParameterName = "@query_name", Value = qnGetBametiTargetUOM, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@scheme_id", Value = schemeId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@activity_id", Value = activityId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });

            if (!string.IsNullOrEmpty(districtId))
            {
                parameters.Add(new SqlParameter { ParameterName = "@District_ID", Value = Convert.ToInt32(districtId), SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            }

            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(spBametiProgram, parameters, SqlHelper.ExecutionType.Procedure);

            if (dt != null && dt.Rows.Count > 0)
            {
                temp = SqlHelper.ConvertDataTableToList<TargetUom>(dt)[0];
            }

            return temp;
        }

        /// <summary>
        /// GetUOMBasedonSchemeActvity.
        /// </summary>
        /// <param name="schemeId">schemeId.</param>
        /// <param name="activityId">activityId.</param>
        /// <param name="period">period.</param>
        /// <returns>Model Values.</returns>
        public TargetUom GetUOMBasedonSchemeActvity(int schemeId, int activityId, string period)
        {
            TargetUom temp = null;

            List<DbParameter> parameters = new List<DbParameter>();
            parameters.Add(new SqlParameter { ParameterName = "@query_name", Value = qnGetUOMBasedonSchemeActvity, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@scheme_id", Value = schemeId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@activity_id", Value = activityId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@Period", Value = period, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(spBametiProgram, parameters, SqlHelper.ExecutionType.Procedure);

            if (dt != null && dt.Rows.Count > 0)
            {
                temp = SqlHelper.ConvertDataTableToList<TargetUom>(dt)[0];
            }

            return temp;
        }

        /// <summary>
        /// GetBametiCreateProgramBasedonBeneType.
        /// </summary>
        /// <param name="schemeId">schemeId.</param>
        /// <param name="activityId">activityId.</param>
        /// <param name="designation">designation.</param>
        /// <param name="beneType">beneType.</param>
        /// <returns>List.</returns>
        public List<DtoCreateProgram> GetBametiCreateProgramBasedonBeneType(int schemeId, int activityId, string designation, string beneType)
        {
            List<DtoCreateProgram> list = new List<DtoCreateProgram>();

            List<DbParameter> parameters = new List<DbParameter>();

            if (beneType == "noBene")
            {
                beneType = string.Empty;
            }

            parameters.Add(new SqlParameter { ParameterName = "@query_name", Value = qnGetBametiCreateProgram, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@scheme_id", Value = schemeId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@activity_id", Value = activityId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@designation", Value = designation, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@beneficiary_type", Value = beneType, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });

            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(spBametiProgram, parameters, SqlHelper.ExecutionType.Procedure);
            if (dt != null && dt.Rows.Count > 0)
            {
                List<BametiCreateProgram> temp = SqlHelper.ConvertDataTableToList<BametiCreateProgram>(dt);

                if (temp.Any())
                {
                    list = temp.Select(x => new DtoCreateProgram
                    {
                        Template_id = x.Template_id,
                        Field_id = x.Field_id,
                        Field_name = x.Field_name,
                        Field_type = x.Field_type,
                        Field_category = x.Field_category,
                    })?.GroupBy(x => new { x.Field_id, x.Field_name, x.Field_type }).Select(x => x.First()).ToList();

                    foreach (var item in list)
                    {
                        var tempobj = temp.Where(x => x.Field_id == item.Field_id && x.List_value != null).Select(x => x.List_value).Distinct()?.ToList();

                        if (tempobj != null && tempobj.Any())
                        {
                            item.List_value.AddRange(tempobj);
                        }
                    }
                }
            }

            return list;
        }

        /// <summary>
        /// GetBametiViewProgram.
        /// </summary>
        /// <param name="request">request.</param>
        /// <returns>List.</returns>
        public BametiViewEditProgram GetBametiViewProgram(DtoViewProgramRequest request)
        {
            BametiViewEditProgram obj = null;
            List<DbParameter> parameters = new List<DbParameter>();
            DateTime d1 = Convert.ToDateTime(DateTime.ParseExact(request.Fromdate, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd", CultureInfo.InvariantCulture));
            DateTime d2 = Convert.ToDateTime(DateTime.ParseExact(request.ToDate, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd", CultureInfo.InvariantCulture));

            parameters.Add(new SqlParameter { ParameterName = "@query_name", Value = qnGetBametiViewProgram, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@scheme_id", Value = request.SchemeId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@activity_id", Value = request.ActivityId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@designation", Value = request.Designation, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@From_Date", Value = d1, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@To_Date", Value = d2, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(spBametiProgram, parameters, SqlHelper.ExecutionType.Procedure);

            if (dt != null && dt.Rows.Count > 0)
            {
                List<BametiCreateProgram> temp = SqlHelper.ConvertDataTableToList<BametiCreateProgram>(dt);

                List<DtoFields> fields = SqlHelper.ConvertDataTableToList<DtoFields>(dt);

                List<BametiRows> rows = SqlHelper.ConvertDataTableToList<BametiRows>(dt).GroupBy(x => new { x.Row_no, x.Header_id }).Select(x => x.First()).ToList();

                if (temp.Any() && fields.Any())
                {
                    obj = temp.Select(x => new BametiViewEditProgram
                    {
                        Template_id = x.Template_id,
                        Header_id = x.Header_id,
                    }).FirstOrDefault();
                    obj.HeaderFieldDetails = temp.Where(x => x.Field_category == "HEADER").Select(x => new DtoFields { Row_no = x.Row_no, Header_id = x.Header_id, Field_type = x.Field_type, Field_id = x.Field_id, Field_name = x.Field_name.ToLower(), Field_value = x.Field_value, Field_category = x.Field_category }).GroupBy(x => new { x.Field_id, x.Header_id }).Select(x => x.First()).ToList();
                    obj.Rows = rows;

                    foreach (var item in obj.Rows)
                    {
                        item.Detailedfields = temp.Where(x => x.Row_no == item.Row_no && x.Header_id == item.Header_id && x.Field_category == "DETAIL").Select(x => new DtoFields { Row_no = x.Row_no, Field_id = x.Field_id, Field_type = x.Field_type, Field_name = x.Field_name.ToLower(), Field_value = x.Field_value, Field_category = x.Field_category }).GroupBy(x => new { x.Row_no, x.Field_id, x.Header_id }).Select(x => x.First()).ToList();
                    }

                    foreach (var item in obj.HeaderFieldDetails)
                    {
                        var tempobj = temp.Where(x => x.Field_id == item.Field_id && x.Header_id == item.Header_id && x.List_value != null).Select(x => x.List_value).Distinct()?.ToList();

                        if (tempobj != null && tempobj.Any())
                        {
                            item.List_values.AddRange(tempobj);
                        }
                    }

                    foreach (var item in obj.Rows)
                    {
                        foreach (var det in item.Detailedfields)
                        {
                            var tempobj = temp.Where(x => x.Field_id == det.Field_id && x.Row_no == item.Row_no && x.Header_id == item.Header_id && x.List_value != null).Select(x => x.List_value).Distinct()?.ToList();

                            if (tempobj != null && tempobj.Any())
                            {
                                det.List_values.AddRange(tempobj);
                            }
                        }
                    }
                }
            }

            return obj;
        }

        /// <summary>
        /// GetBametiEditProgram.
        /// </summary>
        /// <param name="headerId">headerId.</param>
        /// <returns>List.</returns>
        public BametiViewEditProgram GetBametiEditProgram(string headerId)
        {
            BametiViewEditProgram obj = null;
            List<DbParameter> parameters = new List<DbParameter>();

            parameters.Add(new SqlParameter { ParameterName = "@query_name", Value = qnGetBametiEditProgram, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@header_id", Value = Convert.ToInt32(headerId), SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });

            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(spBametiProgram, parameters, SqlHelper.ExecutionType.Procedure);

            if (dt != null && dt.Rows.Count > 0)
            {
                List<BametiCreateProgram> temp = SqlHelper.ConvertDataTableToList<BametiCreateProgram>(dt);

                List<DtoFields> fields = SqlHelper.ConvertDataTableToList<DtoFields>(dt);

                List<BametiRows> rows = SqlHelper.ConvertDataTableToList<BametiRows>(dt).GroupBy(x => new { x.Row_no, x.Header_id }).Select(x => x.First()).ToList();

                if (temp.Any() && fields.Any())
                {
                    obj = temp.Select(x => new BametiViewEditProgram
                    {
                        Template_id = x.Template_id,
                        Header_id = x.Header_id,
                        Scheme_name = x.Scheme_name,
                        Activity_name = x.Activity_name,
                    }).FirstOrDefault();

                    obj.HeaderNames = temp.Where(x => x.Field_category == "HEADER").Select(x => x.Field_name).GroupBy(x => x).Select(x => x.First()).ToList();
                    obj.DetailNames = temp.Where(x => x.Field_category == "DETAIL").Select(x => x.Field_name).GroupBy(x => x).Select(x => x.First()).ToList();
                    obj.HeaderFieldDetails = temp.Where(x => x.Field_category == "HEADER").Select(x => new DtoFields { Row_no = x.Row_no, Header_id = x.Header_id, Field_type = x.Field_type, Field_id = x.Field_id, ActualfieldName = x.Field_name, Field_name = x.Field_name.ToLower(), Field_value = x.Field_value, Field_category = x.Field_category }).GroupBy(x => new { x.Field_id, x.Header_id }).Select(x => x.First()).ToList();
                    obj.Rows = rows;

                    foreach (var item in obj.Rows)
                    {
                        item.Detailedfields = temp.Where(x => x.Row_no == item.Row_no && x.Header_id == item.Header_id && x.Field_category == "DETAIL").Select(x => new DtoFields { Row_no = x.Row_no, Field_id = x.Field_id, Field_type = x.Field_type, ActualfieldName = x.Field_name, Field_name = x.Field_name.ToLower(), Field_value = x.Field_value, Field_category = x.Field_category }).GroupBy(x => new { x.Row_no, x.Field_id, x.Header_id }).Select(x => x.First()).ToList();
                    }

                    foreach (var item in obj.HeaderFieldDetails)
                    {
                        var tempobj = temp.Where(x => x.Field_id == item.Field_id && x.Header_id == item.Header_id && x.List_value != null).Select(x => x.List_value).Distinct()?.ToList();

                        if (tempobj != null && tempobj.Any())
                        {
                            item.List_values.AddRange(tempobj);
                        }
                    }

                    foreach (var item in obj.Rows)
                    {
                        foreach (var det in item.Detailedfields)
                        {
                            var tempobj = temp.Where(x => x.Field_id == det.Field_id && x.Row_no == item.Row_no && x.Header_id == item.Header_id && x.List_value != null).Select(x => x.List_value).Distinct()?.ToList();

                            if (tempobj != null && tempobj.Any())
                            {
                                det.List_values.AddRange(tempobj);
                            }
                        }
                    }
                }
            }

            return obj;
        }

        /// <summary>
        /// GetTopic.
        /// </summary>
        /// <param name="designation">designation.</param>
        /// <param name="userId">userId.</param>
        /// <returns>List.</returns>
        public List<TopicField> GetTopic(string designation, string userId)
        {
            List<TopicField> field = new List<TopicField>();

            List<DbParameter> parameters = new List<DbParameter>();

            parameters.Add(new SqlParameter { ParameterName = "@query_name", Value = qnGetTopic, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@usr_designation", Value = designation, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@user_id", Value = Convert.ToInt32(userId), SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(spBametiProgram, parameters, SqlHelper.ExecutionType.Procedure);
            if (dt != null && dt.Rows.Count > 0)
            {
                field = SqlHelper.ConvertDataTableToList<TopicField>(dt).Distinct().ToList();
            }

            return field;
        }

        /// <summary>
        /// InsertBametiGridData.
        /// </summary>
        /// <param name="request">request.</param>
        /// <returns>Value.</returns>
        public int InsertBametiGridData(HeaderDetailWrapper request)
        {
            int lastupdatedHeaderData = 0;

            int lastupdatedRow = 0;

            DataTable dtheader = new DataTable();

            DataTable dtdetail = new DataTable();

            List<DbParameter> dbparams = new List<DbParameter>();

            if (request.Header != null && request.Header.Any())
            {
                dtheader.Columns.Add("header_id", typeof(int));
                dtheader.Columns.Add("template_id", typeof(int));
                dtheader.Columns.Add("field_id", typeof(int));
                dtheader.Columns.Add("field_value", typeof(string));
                dtheader.Columns.Add("rec_created_userid", typeof(int));
                dtheader.Columns.Add("rec_created_date", typeof(DateTime));
                dtheader.Columns.Add("rec_updated_userid", typeof(int));
                dtheader.Columns.Add("rec_updated_date", typeof(DateTime));

                foreach (var item in request.Header)
                {
                    foreach (var items in item.Field)
                    {
                        dtheader.Rows.Add(
                                   0,
                                   items.TemplateId,
                                   items.FieldId,
                                   items.FieldValue,
                                   items.UserId,
                                   DateTime.Now,
                                   items.UserId,
                                   DateTime.Now);
                    }
                }

                dbparams.Add(new SqlParameter { ParameterName = "@bameti_program_header_chk", Value = dtheader, SqlDbType = SqlDbType.Structured, Direction = ParameterDirection.Input });
                dbparams.Add(new SqlParameter { ParameterName = "@edit_flg", Value = "N", SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparams.Add(new SqlParameter { ParameterName = "@header_id", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Output });
                dbparams.Add(new SqlParameter { ParameterName = "@row_no", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Output });

                Dictionary<string, dynamic> result = SqlHelper.ExecuteNonQuery<SqlConnection>(spInsertBametiGridData, dbparams, SqlHelper.ExecutionType.Procedure);
                lastupdatedHeaderData = DBNull.Value.Equals(result["@header_id"]) ? 0 : Convert.ToInt32(result["@header_id"]);
                lastupdatedRow = DBNull.Value.Equals(result["@row_no"]) ? 0 : Convert.ToInt32(result["@row_no"]);
            }

            if (request.Detail != null && request.Detail.Any())
            {
                dtdetail.Columns.Add("header_id", typeof(int));
                dtdetail.Columns.Add("template_id", typeof(int));
                dtdetail.Columns.Add("field_id", typeof(int));
                dtdetail.Columns.Add("row_no", typeof(int));
                dtdetail.Columns.Add("field_value", typeof(string));
                foreach (var item in request.Detail)
                {
                    lastupdatedRow++;

                    foreach (var items in item.Field)
                    {
                        dtdetail.Rows.Add(
                                   lastupdatedHeaderData,
                                   items.TemplateId,
                                   items.FieldId,
                                   lastupdatedRow,
                                   items.FieldValue);
                    }
                }

                dbparams = new List<DbParameter>();
                dbparams.Add(new SqlParameter { ParameterName = "@bameti_program_detail_chk", Value = dtdetail, SqlDbType = SqlDbType.Structured, Direction = ParameterDirection.Input });
                dbparams.Add(new SqlParameter { ParameterName = "@Response_flag", SqlDbType = SqlDbType.VarChar, Size = 100, Direction = ParameterDirection.Output });

                Dictionary<string, dynamic> result = SqlHelper.ExecuteNonQuery<SqlConnection>(spInsertBametiGridDatadtl, dbparams, SqlHelper.ExecutionType.Procedure);
            }

            return 1;
        }

        /// <summary>
        /// EditBametiGridData.
        /// </summary>
        /// <param name="request">request.</param>
        /// <returns>Value1.</returns>
        public int EditBametiGridData(EditHeaderDetailWrapper request)
        {
            DataTable dtheader = new DataTable();

            DataTable dtdetail = new DataTable();

            List<DbParameter> dbparams = new List<DbParameter>();
            if (request.Header != null && request.Header.Any())
            {
                dtheader.Columns.Add("header_id", typeof(int));
                dtheader.Columns.Add("template_id", typeof(int));
                dtheader.Columns.Add("field_id", typeof(int));
                dtheader.Columns.Add("field_value", typeof(string));
                dtheader.Columns.Add("rec_created_userid", typeof(int));
                dtheader.Columns.Add("rec_created_date", typeof(DateTime));
                dtheader.Columns.Add("rec_updated_userid", typeof(int));
                dtheader.Columns.Add("rec_updated_date", typeof(DateTime));

                foreach (var item in request.Header)
                {
                    foreach (var items in item.Field)
                    {
                        dtheader.Rows.Add(
                                   items.HeaderId,
                                   items.TemplateId,
                                   items.FieldId,
                                   items.FieldValue,
                                   items.UserId,
                                   DateTime.Now,
                                   items.UserId,
                                   DateTime.Now);
                    }
                }

                dbparams.Add(new SqlParameter { ParameterName = "@bameti_program_header_chk", Value = dtheader, SqlDbType = SqlDbType.Structured, Direction = ParameterDirection.Input });
                dbparams.Add(new SqlParameter { ParameterName = "@edit_flg", Value = "Y", SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparams.Add(new SqlParameter { ParameterName = "@header_id", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Output });
                dbparams.Add(new SqlParameter { ParameterName = "@row_no", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Output });

                Dictionary<string, dynamic> result = SqlHelper.ExecuteNonQuery<SqlConnection>(spInsertBametiGridData, dbparams, SqlHelper.ExecutionType.Procedure);
            }

            if (request.Detail != null && request.Detail.Any())
            {
                dtdetail.Columns.Add("header_id", typeof(int));

                dtdetail.Columns.Add("template_id", typeof(int));

                dtdetail.Columns.Add("field_id", typeof(int));
                dtdetail.Columns.Add("row_no", typeof(int));
                dtdetail.Columns.Add("field_value", typeof(string));

                foreach (var item in request.Detail)
                {
                    foreach (var items in item.Field)
                    {
                        dtdetail.Rows.Add(
                                   items.HeaderId,
                                   items.TemplateId,
                                   items.FieldId,
                                   items.Rowno,
                                   items.FieldValue);
                    }
                }

                dbparams = new List<DbParameter>();
                dbparams.Add(new SqlParameter { ParameterName = "@bameti_program_detail_chk", Value = dtdetail, SqlDbType = SqlDbType.Structured, Direction = ParameterDirection.Input });
                dbparams.Add(new SqlParameter { ParameterName = "@edit_flg", Value = "Y", SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparams.Add(new SqlParameter { ParameterName = "@Response_flag", SqlDbType = SqlDbType.VarChar, Size = 100, Direction = ParameterDirection.Output });

                Dictionary<string, dynamic> result = SqlHelper.ExecuteNonQuery<SqlConnection>(spInsertBametiGridDatadtl, dbparams, SqlHelper.ExecutionType.Procedure);
            }

            return 1;
        }

        /// <summary>
        /// GetLastUpdatedRow.
        /// </summary>
        /// <param name="templateId">templateId.</param>
        /// <param name="headerid">headerid.</param>
        /// <returns>Json Result.</returns>
        public int GetLastUpdatedRow(int templateId, int headerid)
        {
            int result = 0;

            List<DbParameter> parameters = new List<DbParameter>();

            parameters.Add(new SqlParameter { ParameterName = "@query_name", Value = qnGetLastUpdatedRow, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@template_id", Value = templateId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@header_id", Value = headerid, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(spBametiProgram, parameters, SqlHelper.ExecutionType.Procedure);
            if (dt != null && dt.Rows.Count > 0)
            {
                int roww = SqlHelper.ConvertDataTableToList<DtoBametiRows>(dt).Select(x => x.MAX_ROW_NO).FirstOrDefault();
                result = roww;
            }

            return result;
        }

        /// <summary>
        /// GetLastHeaderUpdatedRow.
        /// </summary>
        /// <returns>Json output.</returns>
        public int GetLastHeaderUpdatedRow()
        {
            int result = 0;

            List<DbParameter> parameters = new List<DbParameter>();

            parameters.Add(new SqlParameter { ParameterName = "@query_name", Value = qnGetLastHeaderUpdatedRow, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(spBametiProgram, parameters, SqlHelper.ExecutionType.Procedure);

            if (dt != null && dt.Rows.Count > 0)
            {
                int roww = SqlHelper.ConvertDataTableToList<DtoBametiRows>(dt).Select(x => x.MAX_ROW_NO).FirstOrDefault();
                result = roww;
            }

            return result;
        }

        /// <summary>
        /// GetLastUpdatedTemplateId.
        /// </summary>
        /// <returns>DB values.</returns>
        public int GetLastUpdatedTemplateId()
        {
            int result = 0;

            List<DbParameter> parameters = new List<DbParameter>();

            parameters.Add(new SqlParameter { ParameterName = "@query_name", Value = qnGetLastUpdatedTemplateId, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(spBametiProgram, parameters, SqlHelper.ExecutionType.Procedure);

            if (dt != null && dt.Rows.Count > 0)
            {
                int roww = SqlHelper.ConvertDataTableToList<DtoBametiRows>(dt).Select(x => x.MAX_ROW_NO).FirstOrDefault();
                result = roww;
            }

            return result;
        }

        /// <summary>
        /// GetBametiSummaryViewProgram.
        /// </summary>
        /// <param name="request">request.</param>
        /// <returns>Value.</returns>
        public DtosBametiViewProgram GetBametiSummaryViewProgram(DtoViewProgramRequest request)
        {
            DtosBametiViewProgram obj = null;
            List<DbParameter> parameters = new List<DbParameter>();

            DateTime? d1;

            DateTime? d2;

            if (string.IsNullOrEmpty(request.Fromdate) || string.IsNullOrEmpty(request.ToDate))
            {
                d1 = null;

                d2 = null;
            }
            else
            {
                d1 = Convert.ToDateTime(DateTime.ParseExact(request.Fromdate, "MM/dd/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd", CultureInfo.InvariantCulture));
                d2 = Convert.ToDateTime(DateTime.ParseExact(request.ToDate, "MM/dd/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd", CultureInfo.InvariantCulture));
            }

            if (string.IsNullOrEmpty(request.Topic))
            {
                request.Topic = null;
            }

            parameters.Add(new SqlParameter { ParameterName = "@query_name", Value = qnGetBametiViewProgram, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@scheme_id", Value = request.SchemeId, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@activity_id", Value = request.ActivityId, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@Topic", Value = request.Topic == null ? DBNull.Value : (object)request.Topic, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@designation", Value = request.Designation, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@usr_designation", Value = request.Userdesignation, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@From_Date", Value = d1 == null ? DBNull.Value : (object)d1, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@To_Date", Value = d2 == null ? DBNull.Value : (object)d2, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
            parameters.Add(new SqlParameter { ParameterName = "@user_id", Value = request.UserId, SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input });
            DataTable dt = SqlHelper.ExecuteSelect<SqlConnection>(spBametiProgram, parameters, SqlHelper.ExecutionType.Procedure);
            if (dt != null && dt.Rows.Count > 0)
            {
                List<BametiCreateProgram> temp = SqlHelper.ConvertDataTableToList<BametiCreateProgram>(dt);

                List<DTOsFields> fields = SqlHelper.ConvertDataTableToList<DTOsFields>(dt);

                List<DTOsBametiRows> rows = SqlHelper.ConvertDataTableToList<DTOsBametiRows>(dt).GroupBy(x => new { x.Row_no, x.Header_id }).Select(x => x.First()).ToList();

                if (temp.Any() && fields.Any())
                {
                    obj = temp.Select(x => new DtosBametiViewProgram
                    {
                        Template_id = x.Template_id,
                        Status = x.Status,
                    }).FirstOrDefault();
                    obj.Headers.Add("Scheme Name");
                    obj.Headers.Add("Activity Name");
                    obj.Headers.AddRange(temp.Where(x => x.Field_category == "HEADER").Select(x => x.Field_name).GroupBy(x => x).Select(x => x.First()).ToList());
                    obj.Rows = rows;

                    foreach (var item in obj.Rows)
                    {
                        item.Fields.Add(temp.Where(x => x.Row_no == item.Row_no && x.Header_id == item.Header_id && x.Field_category == "HEADER").Select(x => new DTOsFields { Row_no = x.Row_no, Field_id = 0, Field_name = "schemename", Field_value = x.Scheme_name, Header_id = x.Header_id }).GroupBy(x => new { x.Field_id, x.Header_id }).Select(x => x.First()).FirstOrDefault());

                        item.Fields.Add(temp.Where(x => x.Row_no == item.Row_no && x.Header_id == item.Header_id && x.Field_category == "HEADER").Select(x => new DTOsFields { Row_no = x.Row_no, Field_id = 0, Field_name = "activityname", Field_value = x.Activity_name, Header_id = x.Header_id }).GroupBy(x => new { x.Field_id, x.Header_id }).Select(x => x.First()).FirstOrDefault());
                    }

                    foreach (var item in obj.Rows)
                    {
                        item.Fields.AddRange(temp.Where(x => x.Row_no == item.Row_no && x.Header_id == item.Header_id && x.Field_category == "HEADER").Select(x => new DTOsFields { Row_no = x.Row_no, Field_id = x.Field_id, Field_name = x.Field_name.ToLower(), Field_value = x.Field_value, Header_id = x.Header_id }).GroupBy(x => new { x.Field_id, x.Header_id }).Select(x => x.First()).ToList());
                    }
                }
            }

            return obj;
        }
    }
}