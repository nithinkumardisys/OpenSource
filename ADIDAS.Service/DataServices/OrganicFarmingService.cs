//------------------------------------------------------------------------------
// <copyright file="OrganicFarmingService.cs" company="Government of Bihar">
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
    /// Organic Farming Service
    /// </summary>
    public class OrganicFarmingService : IOrganicFarmingService
    {
        /// <summary>
        /// Interface Organic Farming Repository
        /// </summary>
        private IOrganicFarmingRepository _organicFarmingRepository;

        /// <summary>
        /// Organic Farming Service
        /// </summary>
        /// <param name="organicFarmingRepository">organicFarmingRepository</param>
        public OrganicFarmingService(IOrganicFarmingRepository organicFarmingRepository)
        {
            _organicFarmingRepository = organicFarmingRepository;
        }

        /// <summary>
        /// Get FPO Details
        /// </summary>
        /// <param name="fpoId">fpoId</param>
        /// <returns>FPODetails</returns>
        public List<FPODetails> GetFPODetails(int fpoId)
        {
            return _organicFarmingRepository.GetFPODetails(fpoId);
        }

        /// <summary>
        /// Get NPOP Farmer Details
        /// </summary>
        /// <returns>NPOPFarmerInfo</returns>
        public NpopFarmerDetailsModel GetNPOPFarmerDetails(long farmer_dbt_reg_no)
        {
            return _organicFarmingRepository.GetNPOPFarmerDetails(farmer_dbt_reg_no);
        }

        /// <summary>
        /// Get Scheme Details
        /// </summary>
        /// <returns>NPOPFarmerInfo</returns>
        public List<NpopSchemeModel> GetSchemeDetails()
        {
            return _organicFarmingRepository.GetSchemeDetails();
        }

        /// <summary>
        /// Get NPOP Farm Details
        /// </summary>
        /// <returns>NPOPFarmInfo</returns>
        public List<NpopFarmDetailsModel> GetNPOPFarmDetails(long farmer_dbt_reg_no)
        {
            return _organicFarmingRepository.GetNPOPFarmDetails(farmer_dbt_reg_no);
        }

        /// <summary>
        /// Get NPOP Crop Details
        /// </summary>
        /// <returns>NPOPCropInfo</returns>
        public List<NpopCropDetailsModel> GetNPOPCropDetails(long farmerDbtRegNo, int farmId, int seasonId)
        {
            return _organicFarmingRepository.GetNPOPCropDetails(farmerDbtRegNo, farmId, seasonId);
        }

        /// <summary>
        /// Get NPOP Details
        /// </summary>
        /// <param name="districtId">districtId</param>
        /// <param name="blockId">blockId</param>
        /// <param name="panchayatId">panchayatId</param>
        /// <param name="seasonId">seasonId</param>
        /// <param name="status">status</param>
        /// <returns>List NPOPDetails</returns>
        public List<NpopDetailsListModel> GetNPOPDetails(int districtId, string blockId, string panchayatId, int seasonId, string status)
        {
            return _organicFarmingRepository.GetNPOPDetails(districtId, blockId, panchayatId, seasonId, status);
        }

        /// <summary>
        /// Post NPOP Details
        /// </summary>
        /// <param name="createNpopDetails">createNpopDetails</param>
        /// <returns>int</returns>
        public int PostNPOPDetails(NpopDetailsCreateModel createNpopDetails)
        {
            return _organicFarmingRepository.PostNPOPDetails(createNpopDetails);
        }

        public List<PGSFarmerGroupTable> GetFarmerGroupTable(int districtId)
        {
            return _organicFarmingRepository.GetFarmerGroupTable(districtId);
        }

        public List<PGSSchemeNames> GetPGSSchemeNames()
        {
            return _organicFarmingRepository.GetPGSSchemeNames();
        }

        /// <summary>
        /// Post Farmer Group Details
        /// </summary>
        public int InsertFarmerGroupDetails(PGSFarmerGroupDetails pgsFarmerGroupDtls)
        {
            return _organicFarmingRepository.InsertFarmerGroupDetails(pgsFarmerGroupDtls);
        }

        /// <summary>
        /// Get AssociatedFarmer Details
        /// </summary>
        /// <param name="districtId">districtId</param>
        /// <param name="blockId">blockId</param>
        /// <param name="panchayatId">panchayatId</param>
        /// <param name="seasonId">seasonId</param>
        /// <param name="groupId">groupId</param>
        /// <param name="status">status</param>
        /// <returns>List NPOPDetails</returns>
        public List<AssociatedFarmerDetailsListModel> GetAssociatedFarmerDetails(int districtId, int blockId, int panchayatId, int seasonId, int groupId, string status)
        {
            return _organicFarmingRepository.GetAssociatedFarmerDetails(districtId, blockId, panchayatId, seasonId, groupId, status);
        }

        public List<PGSGroupNames> GetPGSGroupNames(int districtId)
        {
            return _organicFarmingRepository.GetPGSGroupNames(districtId);
        }

        public List<PGSFarmerGroupDetailsId> GetFarmerGroupDetails(string group_reg_no)
        {
            return _organicFarmingRepository.GetFarmerGroupDetails(group_reg_no);
        }

        public List<PGSMajorTownNames> GetPGSMajorTownNames()
        {
            return _organicFarmingRepository.GetPGSMajorTownNames();
        }

        public List<PGSMajorCropNames> GetPGSMajorCropNames()
        {
            return _organicFarmingRepository.GetPGSMajorCropNames();
        }

        public PgsFarmerDetailsModel GetPGSFarmerDetails(long farmer_dbt_reg_no)
        {
            return _organicFarmingRepository.GetPGSFarmerDetails(farmer_dbt_reg_no);
        }

        /// <summary>
        /// Get PGS Farm Details
        /// </summary>
        /// <returns>PGSFarmInfo</returns>
        public List<PgsFarmDetailsModel> GetPGSFarmDetails(long farmer_dbt_reg_no)
        {
            return _organicFarmingRepository.GetPGSFarmDetails(farmer_dbt_reg_no);
        }

        /// <summary>
        /// Get PGS Crop Details
        /// </summary>
        /// <returns>PGSCropInfo</returns>
        public List<PgsCropDetailsModel> GetPGSCropDetails(long farmerDbtRegNo, int farmId, int seasonId)
        {
            return _organicFarmingRepository.GetPGSCropDetails(farmerDbtRegNo, farmId, seasonId);
        }

        public int PostPGSDetails(PgsDetailsCreateModel createPgsDetails)
        {
            return _organicFarmingRepository.PostPGSDetails(createPgsDetails);
        }

        public List<PGSGroupNamesAndDbtNo> GetGroupNamesAndDbtNo(int districtId)
        {
            return _organicFarmingRepository.GetGroupNamesAndDbtNo(districtId);
        }

        /// <summary>
        /// Getting Pgs User.
        /// </summary>
        /// <param name="userid">userid.</param>
        /// <returns>PgsUser.</returns>
        public List<DisburseEntity> IsPgsUser(int userid)
        {
            return _organicFarmingRepository.IsPgsUser(userid);
        }

        /// <summary>
        /// Getting Npop User Details.
        /// </summary>
        /// <param name="userid">userid.</param>
        /// <param name="designation">designation.</param>
        /// <returns>User Details.</returns>
        public List<DisburseEntity> IsNpopUser(int userid, string designation)
        {
            return _organicFarmingRepository.IsNpopUser(userid, designation);
        }

        /// <summary>
        /// Get Npop Major Town Names.
        /// </summary>
        /// <returns></returns>
        public List<NpopMajorTownNames> GetNpopMajorTownNames()
        {
            return _organicFarmingRepository.GetNpopMajorTownNames();
        }

        /// <summary>
        /// Get Npop Major Crop Names.
        /// </summary>
        /// <returns>Crop Names.</returns>
        public List<NpopMajorCropNames> GetNpopMajorCropNames()
        {
            return _organicFarmingRepository.GetNpopMajorCropNames();
        }

        /// <summary>
        /// InsertAgriFarmerDetails.
        /// </summary>
        /// <param name="orgFarmAgriFarmerDetails">orgFarmAgriFarmerDetails.</param>
        /// <returns>OrgFarmAgriFarmerDetails Insert Response.</returns>
        public int InsertAgriFarmerDetails(OrgFarmAgriFarmerDetails orgFarmAgriFarmerDetails)
        {
            return _organicFarmingRepository.InsertAgriFarmerDetails(orgFarmAgriFarmerDetails);
        }
    }
}
