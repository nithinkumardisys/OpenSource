//------------------------------------------------------------------------------
// <copyright file="AnnualPlanOutlayService.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------
namespace ADIDAS.Service.DataServices
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using ADIDAS.Model.DTO;
    using ADIDAS.Model.Entities;
    using ADIDAS.Repository.Interfaces;
    using ADIDAS.Service.Interfaces;

    /// <summary>
    /// Annual PlanOutlay Service.
    /// </summary>
    public class AnnualPlanOutlayService : IAnnualPlanOutlayService
    {
        private readonly IAnnualPlanOutlayRepository repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="AnnualPlanOutlayService"/> class.
        /// </summary>
        /// <param name="annualPlanOutlayRepository">annualPlanOutlayRepository.</param>
        public AnnualPlanOutlayService(IAnnualPlanOutlayRepository annualPlanOutlayRepository)
        {
            this.repository = annualPlanOutlayRepository;
        }

        /// <summary>
        /// Get Annual Plan Outlay Budegt.
        /// </summary>
        /// <param name="schemeId">schemeId.</param>
        /// <param name="user_id">user_id.</param>
        /// <returns>List.</returns>
        public List<AnnualPlanOutlayBudgetHead> GetAnnualPlanOutlayBudegt(int schemeId, int user_id)
        {
            return this.repository.GetAnnualPlanOutlayBudegt(schemeId, user_id);
        }

        /// <summary>
        /// Get Budget HeadBy Scheme For Allotment.
        /// </summary>
        /// <param name="schemeId">schemeId.</param>
        /// <param name="user_id">user_id.</param>
        /// <returns>List AnnualPlanOutlayBudgetHead.</returns>
        public List<AnnualPlanOutlayBudgetHead> GetBudgetHeadBySchemeForAllotment(int schemeId, int user_id)
        {
            return this.repository.GetBudgetHeadBySchemeForAllotment(schemeId, user_id);
        }

        /// <summary>
        /// Get Annual Plan Outlay ByScheme Budget.
        /// </summary>
        /// <param name="schemeId">schemeId.</param>
        /// <param name="budgetHead">budgetHead.</param>
        /// <param name="user_id">user_id.</param>
        /// <returns>AnnualPlanOutlaySummary.</returns>
        public AnnualPlanOutlaySummary GetAnnualPlanOutlayBySchemeBudget(int schemeId, int budgetHead, int user_id)
        {
            return this.repository.GetAnnualPlanOutlayBySchemeBudget(schemeId, budgetHead, user_id);
        }

        /// <summary>
        /// Get Drawing Disperse Officers.
        /// </summary>
        /// <returns>List GetDrawingDisperseOfficersModel.</returns>
        public List<GetDrawingDisperseOfficersModel> GetDrawingDisperseOfficers()
        {
            return this.repository.GetDrawingDisperseOfficers();
        }

        /// <summary>
        /// Get Annual Plan Outlay Schemes.
        /// </summary>
        /// <returns>List AnnualPlanOutlaySchemes</returns>
        public List<AnnualPlanOutlaySchemes> GetAnnualPlanOutlaySchemes()
        {
            return this.repository.GetAnnualPlanOutlaySchemes();
        }

        /// <summary>
        /// Get Annual Plan Outlay Summary.
        /// </summary>
        /// <param name="user_id">user_id.</param>
        /// <param name="district_id">district_id.</param>
        /// <returns>List AnnualPlanOutlaySummary.</returns>
        public List<AnnualPlanOutlaySummary> GetAnnualPlanOutlaySummary(int user_id, int district_id)
        {
            return this.repository.GetAnnualPlanOutlaySummary(user_id, district_id);
        }

        /// <summary>
        /// Insert Annual Plan Outlay.
        /// </summary>
        /// <param name="reqObj">reqObj.</param>
        /// <returns>int.</returns>
        public int InsertAnnualPlanOutlay(AnnualPlanOutlayModel reqObj)
        {
            return this.repository.InsertAnnualPlanOutlay(reqObj);
        }

        /// <summary>
        /// Is Annual PlanUser.
        /// </summary>
        /// <param name="user_id">user_id.</param>
        /// <returns>List DisburseEntity.</returns>
        public List<DisburseEntity> IsAnnualPlanUser(int user_id)
        {
            return this.repository.IsAnnualPlanUser(user_id);
        }

        /// <summary>
        /// Get Disburse Oficers.
        /// </summary>
        /// <param name="user_id">user_id.</param>
        /// <param name="designation">designation.</param>
        /// <returns>List DisburseEntity.</returns>
        public List<DisburseEntity> GetDisburseOficers(int user_id, string designation)
        {
            return this.repository.GetDisburseOficers(user_id, designation);
        }
    }
}
