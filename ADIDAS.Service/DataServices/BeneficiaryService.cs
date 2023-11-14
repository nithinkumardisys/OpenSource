//------------------------------------------------------------------------------
// <copyright file="BeneficiaryService.cs" company="Government of Bihar">
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

    /// <summary>
    /// BeneficiaryService.
    /// </summary>
    public class BeneficiaryService : IBeneficiaryService
    {
        private readonly IBeneficiaryRepository beneficiaryRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="BeneficiaryService"/> class.
        /// </summary>
        /// <param name="beneficiaryRepository">beneficiaryRepository.</param>
        public BeneficiaryService(IBeneficiaryRepository beneficiaryRepository)
        {
            this.beneficiaryRepository = beneficiaryRepository;
        }

        /// <summary>
        /// GetBIAUserDetails.
        /// </summary>
        /// <param name="userid">userid.</param>
        /// <returns>User Details.</returns>
        public List<DisburseEntity> GetBIAUserDetails(int userid)
        {
            return this.beneficiaryRepository.GetBIAUserDetails(userid);
        }

        /// <summary>
        /// GetBIADirectorateList.
        /// </summary>
        /// <returns>Directorate List.</returns>
        public List<DirectorateSchemeDetails> GetBIADirectorateList()
        {
            return this.beneficiaryRepository.GetBIADirectorateList();
        }

        /// <summary>
        /// GetBIASchemeNames.
        /// </summary>
        /// <param name="directorate">directorate.</param>
        /// <returns>Scheme List.</returns>
        public List<BiaScheme> GetBIASchemeNames(string directorate)
        {
            return this.beneficiaryRepository.GetBIASchemeNames(directorate);
        }

        /// <summary>
        /// GetAssignedAndPendingData.
        /// </summary>
        /// <param name="biaWebFilters">biaWebFilters.</param>
        /// <returns>AssignedAndPendingData.</returns>
        public List<BiaWebGridDetails> GetAssignedAndPendingData(BiaWebFilters biaWebFilters)
        {
            return this.beneficiaryRepository.GetAssignedAndPendingData(biaWebFilters);
        }

        /// <summary>
        /// GetVerifiedData.
        /// </summary>
        /// <param name="biaWebFilters">biaWebFilters.</param>
        /// <returns>VerifiedData.</returns>
        public List<BiaWebGridDetails> GetVerifiedData(BiaWebFilters biaWebFilters)
        {
            return this.beneficiaryRepository.GetVerifiedData(biaWebFilters);
        }

        /// <summary>
        /// GetVerifiedHistoryData.
        /// </summary>
        /// <param name="biaWebFilters">biaWebFilters.</param>
        /// <returns>VerifiedDataHistory.</returns>
        public List<BiaWebGridDetails> GetVerifiedHistoryData(BiaWebFilters biaWebFilters)
        {
            return this.beneficiaryRepository.GetVerifiedHistoryData(biaWebFilters);
        }

        /// <summary>
        /// GetBIADetailsBasedOnClick.
        /// </summary>
        /// <param name="biaWebGridFilters">biaWebGridFilters.</param>
        /// <returns>Bia Details List.</returns>
        public List<BiaWebGridDetails> GetBIADetailsBasedOnClick(BiaWebGridFilters biaWebGridFilters)
        {
            return this.beneficiaryRepository.GetBIADetailsBasedOnClick(biaWebGridFilters);
        }

        /// <summary>
        /// GetAgricultureOfficerDetails.
        /// </summary>
        /// <param name="biaWebAgricultureOfficerFilters">biaWebAgricultureOfficerFilters.</param>
        /// <returns>AO List.</returns>
        public List<BiaWebAoUserDetails> GetAgricultureOfficerDetails(BiaWebAgricultureOfficerFilters biaWebAgricultureOfficerFilters)
        {
            return this.beneficiaryRepository.GetAgricultureOfficerDetails(biaWebAgricultureOfficerFilters);
        }

        /// <summary>
        /// GetAgricultureOfficerTaskDetails.
        /// </summary>
        /// <param name="userId">userId.</param>
        /// <returns>AO Task List.</returns>
        public List<BiaWebAoUserDetails> GetAgricultureOfficerTaskDetails(int userId)
        {
            return this.beneficiaryRepository.GetAgricultureOfficerTaskDetails(userId);
        }

        /// <summary>
        /// PostAssignTask.
        /// </summary>
        /// <param name="biaWebGridDetails">biaWebGridDetails.</param>
        /// <returns>output.</returns>
        public int PostAssignTask(List<BiaWebGridDetails> biaWebGridDetails)
        {
            return this.beneficiaryRepository.PostAssignTask(biaWebGridDetails);
        }

        /// <summary>
        /// GetFinancialYear.
        /// </summary>
        /// <returns>Fin Year.</returns>
        public List<FinancialYear> GetFinancialYear()
        {
            return this.beneficiaryRepository.GetFinancialYear();
        }

        /// <summary>
        /// GetDirectorateSchemeDetail.
        /// </summary>
        /// <returns>DirectorateSchemeDetails List.</returns>
        public List<DirectorateSchemeDetails> GetDirectorateSchemeDetail()
        {
            return this.beneficiaryRepository.GetDirectorateSchemeDetail();
        }

        /// <summary>
        /// GetStatus.
        /// </summary>
        /// <returns>Status List.</returns>
        public List<BiaStatus> GetStatus()
        {
            return this.beneficiaryRepository.GetStatus();
        }

        /// <summary>
        /// GetBeneficiaryRecord.
        /// </summary>
        /// <param name="userId">userId.</param>
        /// <returns>BeneficiaryRecord List.</returns>
        public List<BiaBeneficiaryRecords> GetBeneficiaryRecord(int userId)
        {
            return this.beneficiaryRepository.GetBeneficiaryRecord(userId);
        }

        /// <summary>
        /// PostDeleteBeneficiary.
        /// </summary>
        /// <param name="biaDeleteBeneficiaryInput">biaDeleteBeneficiaryInput.</param>
        /// <returns>output.</returns>
        public int PostDeleteBeneficiary(List<BiaDeleteBeneficiaryInput> biaDeleteBeneficiaryInput)
        {
            return this.beneficiaryRepository.PostDeleteBeneficiary(biaDeleteBeneficiaryInput);
        }

        /// <summary>
        /// PostAssignBeneficiary.
        /// </summary>
        /// <param name="biaAssignBeneficiaryInput">biaAssignBeneficiaryInput.</param>
        /// <returns>output.</returns>
        public int PostAssignBeneficiary(List<BiaAssignBeneficiaryInput> biaAssignBeneficiaryInput)
        {
            return this.beneficiaryRepository.PostAssignBeneficiary(biaAssignBeneficiaryInput);
        }

        /// <summary>
        /// PostBeneficiaryDetail.
        /// </summary>
        /// <param name="biaPostBeneficiaryDetail">biaPostBeneficiaryDetail.</param>
        /// <returns>output.</returns>
        public int PostBeneficiaryDetail(BiaPostBeneficiaryDetail biaPostBeneficiaryDetail)
        {
            return this.beneficiaryRepository.PostBeneficiaryDetail(biaPostBeneficiaryDetail);
        }

        /// <summary>
        /// GetNotificationAudits.
        /// </summary>
        /// <param name="userId">userId.</param>
        /// <returns>NotificationAudits.</returns>
        public List<BiaNotificationAudits> GetNotificationAudits(int userId)
        {
            return this.beneficiaryRepository.GetNotificationAudits(userId);
        }

        /// <summary>
        /// GetBeneficiaryNotification.
        /// </summary>
        /// <param name="userId">userId.</param>
        /// <returns>BeneficiaryNotification.</returns>
        public List<BiaNotificationAudits> GetBeneficiaryNotification(int userId)
        {
            return this.beneficiaryRepository.GetBeneficiaryNotification(userId);
        }
    }
}
