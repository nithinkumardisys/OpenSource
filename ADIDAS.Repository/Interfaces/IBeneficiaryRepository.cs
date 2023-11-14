//------------------------------------------------------------------------------
// <copyright file="IBeneficiaryRepository.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------
namespace ADIDAS.Repository.Interfaces
{
    using System.Collections.Generic;
    using ADIDAS.Model.DTO;
    using ADIDAS.Model.Entities;

    /// <summary>
    /// IBeneficiaryRepository.
    /// </summary>
    public interface IBeneficiaryRepository
    {
        /// <summary>
        /// GetBIAUserDetails.
        /// </summary>
        /// <param name="userid">userid.</param>
        /// <returns>User Details.</returns>
        List<DisburseEntity> GetBIAUserDetails(int userid);

        /// <summary>
        /// GetBIADirectorateList.
        /// </summary>
        /// <returns>Directorate List.</returns>
        List<DirectorateSchemeDetails> GetBIADirectorateList();

        /// <summary>
        /// GetBIASchemeNames.
        /// </summary>
        /// <param name="directorate">directorate.</param>
        /// <returns>Scheme List.</returns>
        List<BiaScheme> GetBIASchemeNames(string directorate);

        /// <summary>
        /// GetAssignedAndPendingData.
        /// </summary>
        /// <param name="biaWebFilters">biaWebFilters.</param>
        /// <returns>AssignedAndPendingData.</returns>
        List<BiaWebGridDetails> GetAssignedAndPendingData(BiaWebFilters biaWebFilters);

        /// <summary>
        /// GetVerifiedData.
        /// </summary>
        /// <param name="biaWebFilters">biaWebFilters.</param>
        /// <returns>VerifiedData.</returns>
        List<BiaWebGridDetails> GetVerifiedData(BiaWebFilters biaWebFilters);

        /// <summary>
        /// GetVerifiedHistoryData.
        /// </summary>
        /// <param name="biaWebFilters">biaWebFilters.</param>
        /// <returns>VerifiedDataHistory.</returns>
        List<BiaWebGridDetails> GetVerifiedHistoryData(BiaWebFilters biaWebFilters);

        /// <summary>
        /// GetBIADetailsBasedOnClick.
        /// </summary>
        /// <param name="biaWebGridFilters">biaWebGridFilters.</param>
        /// <returns>Bia Details List.</returns>
        List<BiaWebGridDetails> GetBIADetailsBasedOnClick(BiaWebGridFilters biaWebGridFilters);

        /// <summary>
        /// GetAgricultureOfficerDetails.
        /// </summary>
        /// <param name="biaWebAgricultureOfficerFilters">biaWebAgricultureOfficerFilters.</param>
        /// <returns>AO List.</returns>
        List<BiaWebAoUserDetails> GetAgricultureOfficerDetails(BiaWebAgricultureOfficerFilters biaWebAgricultureOfficerFilters);

        /// <summary>
        /// GetAgricultureOfficerTaskDetails.
        /// </summary>
        /// <param name="userId">userId.</param>
        /// <returns>AO Task List.</returns>
        List<BiaWebAoUserDetails> GetAgricultureOfficerTaskDetails(int userId);

        /// <summary>
        /// PostAssignTask.
        /// </summary>
        /// <param name="biaWebGridDetails">biaWebGridDetails.</param>
        /// <returns>output.</returns>
        int PostAssignTask(List<BiaWebGridDetails> biaWebGridDetails);

        /// <summary>
        /// GetFinancialYear.
        /// </summary>
        /// <returns>Fin Year.</returns>
        List<FinancialYear> GetFinancialYear();

        /// <summary>
        /// GetDirectorateSchemeDetail.
        /// </summary>
        /// <returns>DirectorateSchemeDetails List.</returns>
        List<DirectorateSchemeDetails> GetDirectorateSchemeDetail();

        /// <summary>
        /// GetStatus.
        /// </summary>
        /// <returns>Status List.</returns>
        List<BiaStatus> GetStatus();

        /// <summary>
        /// GetStatus.
        /// </summary>
        /// <param name="userId">userId.</param>
        /// <returns>BeneficiaryRecord List.</returns>
        List<BiaBeneficiaryRecords> GetBeneficiaryRecord(int userId);

        /// <summary>
        /// PostDeleteBeneficiary.
        /// </summary>
        /// <param name="biaDeleteBeneficiaryInput">biaDeleteBeneficiaryInput.</param>
        /// <returns>output.</returns>
        int PostDeleteBeneficiary(List<BiaDeleteBeneficiaryInput> biaDeleteBeneficiaryInput);

        /// <summary>
        /// PostAssignBeneficiary.
        /// </summary>
        /// <param name="biaAssignBeneficiaryInput">biaAssignBeneficiaryInput.</param>
        /// <returns>output.</returns>
        int PostAssignBeneficiary(List<BiaAssignBeneficiaryInput> biaAssignBeneficiaryInput);

        /// <summary>
        /// PostBeneficiaryDetail.
        /// </summary>
        /// <param name="biaPostBeneficiaryDetail">biaPostBeneficiaryDetail.</param>
        /// <returns>output.</returns>
        int PostBeneficiaryDetail(BiaPostBeneficiaryDetail biaPostBeneficiaryDetail);

        /// <summary>
        /// GetNotificationAudits.
        /// </summary>
        /// <param name="userId">userId.</param>
        /// <returns>NotificationAudits.</returns>
        List<BiaNotificationAudits> GetNotificationAudits(int userId);

        /// <summary>
        /// GetBeneficiaryNotification.
        /// </summary>
        /// <param name="userId">userId.</param>
        /// <returns>BeneficiaryNotification.</returns>
        List<BiaNotificationAudits> GetBeneficiaryNotification(int userId);
    }
}
