//-------------------------------------------------------------------------------------
// <copyright file="IAnnualPlanOutlayRepository.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//-------------------------------------------------------------------------------------
namespace ADIDAS.Repository.Interfaces
{
    using System.Collections.Generic;
    using ADIDAS.Model.DTO;
    using ADIDAS.Model.Entities;

    /// <summary>
    /// IAnnualPlanOutlayRepository.
    /// </summary>
    public interface IAnnualPlanOutlayRepository
    {
        /// <summary>
        /// InsertAnnualPlanOutlay.
        /// </summary>
        /// <param name="reqObj">reqObj.</param>
        /// <returns>Response.</returns>
        int InsertAnnualPlanOutlay(AnnualPlanOutlayModel reqObj);

        /// <summary>
        /// GetAnnualPlanOutlaySchemes.
        /// </summary>
        /// <returns>AnnualPlanOutlaySchemes.</returns>
        List<AnnualPlanOutlaySchemes> GetAnnualPlanOutlaySchemes();

        /// <summary>
        /// GetAnnualPlanOutlayBudegt.
        /// </summary>
        /// <param name="schemeId">schemeId.</param>
        /// <param name="user_id">user_id.</param>
        /// <returns>AnnualPlanOutlayBudgetHead.</returns>
        List<AnnualPlanOutlayBudgetHead> GetAnnualPlanOutlayBudegt(int schemeId, int user_id);

        /// <summary>
        /// GetBudgetHeadBySchemeForAllotment.
        /// </summary>
        /// <param name="schemeId">schemeId.</param>
        /// <param name="user_id">user_id</param>
        /// <returns>AnnualPlanOutlayBudgetHead.</returns>
        List<AnnualPlanOutlayBudgetHead> GetBudgetHeadBySchemeForAllotment(int schemeId, int user_id);

        /// <summary>
        /// GetAnnualPlanOutlaySummary.
        /// </summary>
        /// <param name="user_id">user_id.</param>
        /// <param name="district_id">district_id.</param>
        /// <returns>AnnualPlanOutlaySummary.</returns>
        List<AnnualPlanOutlaySummary> GetAnnualPlanOutlaySummary(int user_id, int district_id);

        /// <summary>
        /// GetAnnualPlanOutlayBySchemeBudget.
        /// </summary>
        /// <param name="schemeId">schemeId.</param>
        /// <param name="budgetHead">budgetHead.</param>
        /// <param name="user_id">user_id.</param>
        /// <returns>AnnualPlanOutlaySummary.</returns>
        AnnualPlanOutlaySummary GetAnnualPlanOutlayBySchemeBudget(int schemeId, int budgetHead, int user_id);

        /// <summary>
        /// GetDrawingDisperseOfficers.
        /// </summary>
        /// <returns>GetDrawingDisperseOfficersModel.</returns>
        List<GetDrawingDisperseOfficersModel> GetDrawingDisperseOfficers();

        /// <summary>
        /// IsAnnualPlanUser.
        /// </summary>
        /// <param name="user_id">user_id.</param>
        /// <returns>DisburseEntity.</returns>
        List<DisburseEntity> IsAnnualPlanUser(int user_id);

        /// <summary>
        /// GetDisburseOficers.
        /// </summary>
        /// <param name="user_id">user_id.</param>
        /// <param name="designation">designation.</param>
        /// <returns>DisburseEntity.</returns>
        List<DisburseEntity> GetDisburseOficers(int user_id, string designation);
    }
}
