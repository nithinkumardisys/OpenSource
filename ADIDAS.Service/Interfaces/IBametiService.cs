//------------------------------------------------------------------------------
// <copyright file="IBametiService.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------

namespace ADIDAS.Service.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Text;
    using ADIDAS.Model.DTO;
    using ADIDAS.Model.Entities;
    using DepartmentOfAgriculture.Admin.Models.DTO;
    using DepartmentOfAgriculture.Admin.Models.Models;

    /// <summary>
    /// IBametiService.
    /// </summary>
    public interface IBametiService
    {
        /// <summary>
        /// InsertBametiTemplate.
        /// </summary>
        /// <param name="bametiTemplate">bametiTemplate.</param>
        /// <returns>integer.</returns>
        int InsertBametiTemplate(BametiTemplate bametiTemplate);

        /// <summary>
        /// GetBametiSchemes.
        /// </summary>
        /// <returns>BametiScheme list.</returns>
        List<BametiScheme> GetBametiSchemes();

        /// <summary>
        /// GetBametiDesignation.
        /// </summary>
        /// <param name="schemeId">schemeId.</param>
        /// <param name="activityId">activityId.</param>
        /// <returns>string list.</returns>
        List<string> GetBametiDesignation(string schemeId, string activityId);

        /// <summary>
        /// GetBametiFields.
        /// </summary>
        /// <param name="schemeId">schemeId.</param>
        /// <param name="activityId">activityId.</param>
        /// <param name="designation">designation.</param>
        /// <returns>string list.</returns>
        List<string> GetBametiFields(int schemeId, int activityId, string designation);

        /// <summary>
        /// GetBametiAdminTemplate.
        /// </summary>
        /// <param name="schemeId">schemeId.</param>
        /// <param name="activityId">activityId.</param>
        /// <param name="designation">designation.</param>
        /// <returns>BametiTemplateGet list.</returns>
        List<BametiTemplateGet> GetBametiAdminTemplate(int schemeId, int activityId, string designation);

        /// <summary>
        /// GetBametiCreateProgram.
        /// </summary>
        /// <param name="schemeId">schemeId.</param>
        /// <param name="activityId">activityId.</param>
        /// <param name="designation">designation.</param>
        /// <returns>DtoCreateProgram list.</returns>
        List<DtoCreateProgram> GetBametiCreateProgram(int schemeId, int activityId, string designation);

        /// <summary>
        /// GetBametiViewProgram.
        /// </summary>
        /// <param name="request">request.</param>
        /// <returns>BametiViewEditProgram entity.</returns>
        BametiViewEditProgram GetBametiViewProgram(DtoViewProgramRequest request);

        /// <summary>
        /// InsertBametiGridData.
        /// </summary>
        /// <param name="request">request.</param>
        /// <returns>integer.</returns>
        int InsertBametiGridData(HeaderDetailWrapper request);

        /// <summary>
        /// GetBametiAllFields.
        /// </summary>
        /// <returns>BametiAllFields list.</returns>
        List<BametiAllFields> GetBametiAllFields();

        /// <summary>
        /// GetBametiActivities.
        /// </summary>
        /// <param name="schemeId">schemeId.</param>
        /// <returns>BametiActivity list.</returns>
        List<BametiActivity> GetBametiActivities(string schemeId);

        /// <summary>
        /// GetBametiActivities.
        /// </summary>
        /// <param name="schemeId">schemeId.</param>
        /// <param name="designation">designation.</param>
        /// <returns>BametiActivity list.</returns>
        List<BametiActivity> GetBametiActivities(string schemeId, string designation);

        /// <summary>
        /// GetBametiSchemesbyDesignation.
        /// </summary>
        /// <param name="designation">designation.</param>
        /// <returns>BametiScheme list.</returns>
        List<BametiScheme> GetBametiSchemesbyDesignation(string designation);

        /// <summary>
        /// DeleteBametiTemplate.
        /// </summary>
        /// <param name="bametiTemplate">bametiTemplate.</param>
        /// <returns>integer.</returns>
        int DeleteBametiTemplate(DeleteTemplate bametiTemplate);

        /// <summary>
        /// GetBametiTarget.
        /// </summary>
        /// <param name="request">request.</param>
        /// <returns>BametiTargetGet list.</returns>
        List<BametiTargetGet> GetBametiTarget(BametiTargetRequestDto request);

        /// <summary>
        /// InsertTargetSetting.
        /// </summary>
        /// <param name="setting">setting.</param>
        /// <returns>integer.</returns>
        int InsertTargetSetting(List<PostTargetSetting> setting);

        /// <summary>
        /// GetBametiCreateProgramBasedonBeneType.
        /// </summary>
        /// <param name="schemeId">schemeId.</param>
        /// <param name="activityId">activityId.</param>
        /// <param name="designation">designation.</param>
        /// <param name="beneType">beneType.</param>
        /// <returns>DtoCreateProgram list.</returns>
        List<DtoCreateProgram> GetBametiCreateProgramBasedonBeneType(int schemeId, int activityId, string designation, string beneType);

        /// <summary>
        /// Get Last Header Updated Row.
        /// </summary>
        /// <returns>integer.</returns>
        int GetLastHeaderUpdatedRow();

        /// <summary>
        /// GetBametiTargetUOM.
        /// </summary>
        /// <param name="schemeId">schemeId.</param>
        /// <param name="activityId">activityId.</param>
        /// <param name="districtId">districtId.</param>
        /// <returns>TargetUom entity.</returns>
        TargetUom GetBametiTargetUOM(int schemeId, int activityId, string districtId);

        /// <summary>
        /// GetUOMBasedonSchemeActvity.
        /// </summary>
        /// <param name="schemeId">schemeId.</param>
        /// <param name="activityId">activityId.</param>
        /// <param name="period">period.</param>
        /// <returns>TargetUom.</returns>
        TargetUom GetUOMBasedonSchemeActvity(int schemeId, int activityId, string period);

        /// <summary>
        /// GetTopic.
        /// </summary>
        /// <param name="designation">designation.</param>
        /// <param name="userId">userId.</param>
        /// <returns>TopicField list.</returns>
        List<TopicField> GetTopic(string designation, string userId);

        /// <summary>
        /// GetBametiSummaryViewProgram.
        /// </summary>
        /// <param name="request">request.</param>
        /// <returns>DtosBametiViewProgram.</returns>
        DtosBametiViewProgram GetBametiSummaryViewProgram(DtoViewProgramRequest request);

        /// <summary>
        /// GetBametiEditProgram.
        /// </summary>
        /// <param name="headerId">headerId.</param>
        /// <returns>BametiViewEditProgram.</returns>
        BametiViewEditProgram GetBametiEditProgram(string headerId);

        /// <summary>
        /// EditBametiGridData.
        /// </summary>
        /// <param name="request">request.</param>
        /// <returns>integer.</returns>
        int EditBametiGridData(EditHeaderDetailWrapper request);

        /// <summary>
        /// DeleteBametiData.
        /// </summary>
        /// <param name="templateId">templateId.</param>
        /// <param name="headerId">headerId.</param>
        /// <param name="rowno">rowno.</param>
        /// <returns>integer.</returns>
        int DeleteBametiData(int templateId, int headerId, int rowno);
    }
}
