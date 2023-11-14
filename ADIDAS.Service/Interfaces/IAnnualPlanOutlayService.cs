//------------------------------------------------------------------------------
// <copyright file="IAnnualPlanOutlayService.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------

namespace ADIDAS.Service.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using ADIDAS.Model.DTO;
    using ADIDAS.Model.Entities;

    public interface IAnnualPlanOutlayService
    {
        int InsertAnnualPlanOutlay(AnnualPlanOutlayModel reqObj);

        List<AnnualPlanOutlaySchemes> GetAnnualPlanOutlaySchemes();

        List<AnnualPlanOutlayBudgetHead> GetAnnualPlanOutlayBudegt(int schemeId, int user_id);

        List<AnnualPlanOutlayBudgetHead> GetBudgetHeadBySchemeForAllotment(int schemeId, int user_id);

        List<AnnualPlanOutlaySummary> GetAnnualPlanOutlaySummary(int user_id, int district_id);

        AnnualPlanOutlaySummary GetAnnualPlanOutlayBySchemeBudget(int schemeId, int budgetHead, int user_id);

        List<GetDrawingDisperseOfficersModel> GetDrawingDisperseOfficers();

        List<DisburseEntity> IsAnnualPlanUser(int user_id);

        List<DisburseEntity> GetDisburseOficers(int user_id, string designation);
    }
}
