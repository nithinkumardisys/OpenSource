//------------------------------------------------------------------------------
// <copyright file="BametiService.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------
namespace ADIDAS.Service.DataServices
{
    using System.Collections.Generic;
    using ADIDAS.Model.DTO;
    using ADIDAS.Model.Entities;
    using ADIDAS.Repository.Interfaces;
    using ADIDAS.Service.Interfaces;
    using DepartmentOfAgriculture.Admin.Models.DTO;
    using DepartmentOfAgriculture.Admin.Models.Models;

    /// <summary>
    /// BametiService.
    /// </summary>
    public class BametiService : IBametiService
    {
        private readonly IBametiRepository bametiRepo;

        /// <summary>
        /// Initializes a new instance of the <see cref="BametiService"/> class.
        /// </summary>
        /// <param name="bametiRepo">bametiRepo.</param>
        public BametiService(IBametiRepository bametiRepo)
        {
            this.bametiRepo = bametiRepo;
        }

        /// <summary>
        /// DeleteBametiData.
        /// </summary>
        /// <param name="templateId">templateId.</param>
        /// <param name="headerId">headerId.</param>
        /// <param name="rowno">rowno.</param>
        /// <returns>int.</returns>
        public int DeleteBametiData(int templateId, int headerId, int rowno)
        {
            return this.bametiRepo.DeleteBametiData(templateId, headerId, rowno);
        }

        /// <summary>
        /// DeleteBametiTemplate.
        /// </summary>
        /// <param name="bametiTemplate">bametiTemplate.</param>
        /// <returns>int.</returns>
        public int DeleteBametiTemplate(DeleteTemplate bametiTemplate)
        {
            return this.bametiRepo.DeleteBametiTemplate(bametiTemplate);
        }

        /// <summary>
        /// EditBametiGridData.
        /// </summary>
        /// <param name="request">request.</param>
        /// <returns>int.</returns>
        public int EditBametiGridData(EditHeaderDetailWrapper request)
        {
            return this.bametiRepo.EditBametiGridData(request);
        }

        /// <summary>
        /// GetBametiActivities.
        /// </summary>
        /// <param name="schemeId">schemeId.</param>
        /// <returns>list.</returns>
        public List<BametiActivity> GetBametiActivities(string schemeId)
        {
            return this.bametiRepo.GetBametiActivities(schemeId);
        }

        /// <summary>
        /// GetBametiActivities.
        /// </summary>
        /// <param name="schemeId">schemeId.</param>
        /// <param name="designation">designation.</param>
        /// <returns>List BametiActivity.</returns>
        public List<BametiActivity> GetBametiActivities(string schemeId, string designation)
        {
            return this.bametiRepo.GetBametiActivities(schemeId, designation);
        }

        /// <summary>
        /// GetBametiAdminTemplate.
        /// </summary>
        /// <param name="schemeId">schemeId.</param>
        /// <param name="activityId">activityId.</param>
        /// <param name="designation">designation.</param>
        /// <returns>List BametiTemplateGet.</returns>
        public List<BametiTemplateGet> GetBametiAdminTemplate(int schemeId, int activityId, string designation)
        {
            return this.bametiRepo.GetBametiAdminTemplate(schemeId, activityId, designation);
        }

        /// <summary>
        /// GetBametiAllFields.
        /// </summary>
        /// <returns>List BametiAllFields.</returns>
        public List<BametiAllFields> GetBametiAllFields()
        {
            return this.bametiRepo.GetBametiAllFields();
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
            return this.bametiRepo.GetBametiCreateProgram(schemeId, activityId, designation);
        }

        /// <summary>
        /// GetBametiCreateProgramBasedonBeneType.
        /// </summary>
        /// <param name="schemeId">schemeId.</param>
        /// <param name="activityId">activityId.</param>
        /// <param name="designation">designation.</param>
        /// <param name="beneType">beneType.</param>
        /// <returns>DtoCreateProgram.</returns>
        public List<DtoCreateProgram> GetBametiCreateProgramBasedonBeneType(int schemeId, int activityId, string designation, string beneType)
        {
            return this.bametiRepo.GetBametiCreateProgramBasedonBeneType(schemeId, activityId, designation, beneType);
        }

        /// <summary>
        /// GetBametiDesignation.
        /// </summary>
        /// <param name="schemeId">schemeId.</param>
        /// <param name="activityId">activityId.</param>
        /// <returns>string.</returns>
        public List<string> GetBametiDesignation(string schemeId, string activityId)
        {
            return this.bametiRepo.GetBametiDesignation(schemeId, activityId);
        }

        /// <summary>
        /// GetBametiEditProgram.
        /// </summary>
        /// <param name="headerId">headerId.</param>
        /// <returns>BametiViewEditProgram.</returns>
        public BametiViewEditProgram GetBametiEditProgram(string headerId)
        {
            return this.bametiRepo.GetBametiEditProgram(headerId);
        }

        /// <summary>
        /// GetBametiFields.
        /// </summary>
        /// <param name="schemeId">schemeId.</param>
        /// <param name="activityId">activityId.</param>
        /// <param name="designation">designation.</param>
        /// <returns>list.</returns>
        public List<string> GetBametiFields(int schemeId, int activityId, string designation)
        {
            return this.bametiRepo.GetBametiFields(schemeId, activityId, designation);
        }

        /// <summary>
        /// GetBametiSchemes.
        /// </summary>
        /// <returns>BametiScheme.</returns>
        public List<BametiScheme> GetBametiSchemes()
        {
            return this.bametiRepo.GetBametiSchemes();
        }

        /// <summary>
        /// GetBametiSchemesbyDesignation.
        /// </summary>
        /// <param name="designation">designation.</param>
        /// <returns>List BametiScheme.</returns>
        public List<BametiScheme> GetBametiSchemesbyDesignation(string designation)
        {
            return this.bametiRepo.GetBametiSchemesbyDesignation(designation);
        }

        /// <summary>
        /// GetBametiSummaryViewProgram.
        /// </summary>
        /// <param name="request">request.</param>
        /// <returns>DtosBametiViewProgram.</returns>
        public DtosBametiViewProgram GetBametiSummaryViewProgram(DtoViewProgramRequest request)
        {
            return this.bametiRepo.GetBametiSummaryViewProgram(request);
        }

        /// <summary>
        /// GetBametiTarget.
        /// </summary>
        /// <param name="request">request.</param>
        /// <returns>BametiTargetGet.</returns>
        public List<BametiTargetGet> GetBametiTarget(BametiTargetRequestDto request)
        {
            return this.bametiRepo.GetBametiTarget(request);
        }

        /// <summary>
        /// GetBametiTargetUOM.
        /// </summary>
        /// <param name="schemeId">schemeId.</param>
        /// <param name="activityId">activityId.</param>
        /// <param name="districtId">districtId.</param>
        /// <returns>TargetUom.</returns>
        public TargetUom GetBametiTargetUOM(int schemeId, int activityId, string districtId)
        {
            return this.bametiRepo.GetBametiTargetUOM(schemeId, activityId, districtId);
        }

        /// <summary>
        /// GetBametiViewProgram.
        /// </summary>
        /// <param name="request">request.</param>
        /// <returns>BametiViewEditProgram.</returns>
        public BametiViewEditProgram GetBametiViewProgram(DtoViewProgramRequest request)
        {
            return this.bametiRepo.GetBametiViewProgram(request);
        }

        /// <summary>
        /// GetLastHeaderUpdatedRow.
        /// </summary>
        /// <returns>int.</returns>
        public int GetLastHeaderUpdatedRow()
        {
            return this.bametiRepo.GetLastHeaderUpdatedRow();
        }

        /// <summary>
        /// GetTopic.
        /// </summary>
        /// <param name="designation">designation.</param>
        /// <param name="userId">userId.</param>
        /// <returns>TopicField.</returns>
        public List<TopicField> GetTopic(string designation, string userId)
        {
            return this.bametiRepo.GetTopic(designation, userId);
        }

        /// <summary>
        /// GetUOMBasedonSchemeActvity.
        /// </summary>
        /// <param name="schemeId">schemeId.</param>
        /// <param name="activityId">activityId.</param>
        /// <param name="period">period.</param>
        /// <returns>TargetUom.</returns>
        public TargetUom GetUOMBasedonSchemeActvity(int schemeId, int activityId, string period)
        {
            return this.bametiRepo.GetUOMBasedonSchemeActvity(schemeId, activityId, period);
        }

        /// <summary>
        /// InsertBametiGridData.
        /// </summary>
        /// <param name="request">request.</param>
        /// <returns>int.</returns>
        public int InsertBametiGridData(HeaderDetailWrapper request)
        {
            return this.bametiRepo.InsertBametiGridData(request);
        }

        /// <summary>
        /// InsertBametiTemplate.
        /// </summary>
        /// <param name="bametiTemplate">bametiTemplate.</param>
        /// <returns>int.</returns>
        public int InsertBametiTemplate(BametiTemplate bametiTemplate)
        {
            return this.bametiRepo.InsertBametiTemplate(bametiTemplate);
        }

        /// <summary>
        /// InsertTargetSetting.
        /// </summary>
        /// <param name="setting">setting.</param>
        /// <returns>int.</returns>
        public int InsertTargetSetting(List<PostTargetSetting> setting)
        {
            return this.bametiRepo.InsertTargetSetting(setting);
        }
    }
}
