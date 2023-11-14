//------------------------------------------------------------------------------
// <copyright file="IOrganicFarmingRepository.cs" company="Government of Bihar">
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
    /// Interface Organic Farming Repository.
    /// </summary>
    public interface IOrganicFarmingRepository
    {
        /// <summary>
        /// Get FPO Details.
        /// </summary>
        /// <param name="fpoId">fpoId.</param>
        /// <returns>FPODetails.</returns>
        List<FPODetails> GetFPODetails(int fpoId);

        /// <summary>
        /// GetNPOPFarmerDetails.
        /// </summary>
        /// <param name="farmer_dbt_reg_no">farmer_dbt_reg_no.</param>
        /// <returns>NpopFarmerDetailsModel.</returns>
        NpopFarmerDetailsModel GetNPOPFarmerDetails(long farmer_dbt_reg_no);

        /// <summary>
        /// Get Scheme Details.
        /// </summary>
        /// <returns>NPOPScheme.</returns>
        List<NpopSchemeModel> GetSchemeDetails();

        /// <summary>
        /// GetNPOPFarmDetails.
        /// </summary>
        /// <param name="farmer_dbt_reg_no">farmer_dbt_reg_no.</param>
        /// <returns>NpopFarmDetailsModel.</returns>
        List<NpopFarmDetailsModel> GetNPOPFarmDetails(long farmer_dbt_reg_no);

        /// <summary>
        /// GetNPOPCropDetails.
        /// </summary>
        /// <param name="farmerDbtRegNo">farmerDbtRegNo.</param>
        /// <param name="farmId">farmId.</param>
        /// <param name="seasonId">seasonId.</param>
        /// <returns>NpopCropDetailsModel.</returns>
        List<NpopCropDetailsModel> GetNPOPCropDetails(long farmerDbtRegNo, int farmId, int seasonId);

        /// <summary>
        /// Get NPOP Details.
        /// </summary>
        /// <param name="districtId">districtId.</param>
        /// <param name="blockId">blockId.</param>
        /// <param name="panchayatId">panchayatId.</param>
        /// <param name="seasonId">seasonId.</param>
        /// <param name="status">status.</param>
        /// <returns>List NPOPDetails.</returns>
        List<NpopDetailsListModel> GetNPOPDetails(int districtId, string blockId, string panchayatId, int seasonId, string status);

        /// <summary>
        /// Post NPOP Details.
        /// </summary>
        /// <param name="createNpopDetails">createNpopDetails.</param>
        /// <returns>int.</returns>
        int PostNPOPDetails(NpopDetailsCreateModel createNpopDetails);

        /// <summary>
        /// Get Farmer Group Table.
        /// </summary>
        /// <param name="districtId">districtId.</param>
        /// <returns>List.</returns>
        List<PGSFarmerGroupTable> GetFarmerGroupTable(int districtId);

        /// <summary>
        /// Get PGS Scheme Names.
        /// </summary>
        /// <returns>List.</returns>
        List<PGSSchemeNames> GetPGSSchemeNames();

        /// <summary>
        /// Post Farmer Group Details.
        /// </summary>
        /// <param name="pgsFarmerGroupDtls">pgsFarmerGroupDtls.</param>
        /// <returns>int.</returns>
        int InsertFarmerGroupDetails(PGSFarmerGroupDetails pgsFarmerGroupDtls);

        /// <summary>
        /// Get AssociatedFarmer Details.
        /// </summary>
        /// <param name="districtId">districtId.</param>
        /// <param name="blockId">blockId.</param>
        /// <param name="panchayatId">panchayatId.</param>
        /// <param name="seasonId">seasonId.</param>
        /// <param name="groupId">groupId.</param>
        /// <param name="status">status.</param>
        /// <returns>List AssociatedFarmerDetails.</returns>
        List<AssociatedFarmerDetailsListModel> GetAssociatedFarmerDetails(int districtId, int blockId, int panchayatId, int seasonId, int groupId, string status);

        /// <summary>
        /// Get PGS Group Names.
        /// </summary>
        /// <param name="districtId">districtId.</param>
        /// <returns>List.</returns>
        List<PGSGroupNames> GetPGSGroupNames(int districtId);

        /// <summary>
        /// Get Farmer GroupDetails.
        /// </summary>
        /// <param name="regno">Regno.</param>
        /// <returns>List.</returns>
        List<PGSFarmerGroupDetailsId> GetFarmerGroupDetails(string regno);

        /// <summary>
        /// Get PGS Major Town Names.
        /// </summary>
        /// <param name="Regno">Regno.</param>
        /// <returns>List.</returns>
        List<PGSMajorTownNames> GetPGSMajorTownNames();

        /// <summary>
        /// Get PGS Major Crop Names.
        /// </summary>
        /// <returns>List.</returns>
        List<PGSMajorCropNames> GetPGSMajorCropNames();

        /// <summary>
        /// Get PGS Farmer Details.
        /// </summary>
        /// <param name="farmer_dbt_reg_no">farmer_dbt_reg_no.</param>
        /// <returns>List.</returns>
        PgsFarmerDetailsModel GetPGSFarmerDetails(long farmer_dbt_reg_no);

        /// <summary>
        /// GetPGSFarmDetails.
        /// </summary>
        /// <param name="farmer_dbt_reg_no">farmer_dbt_reg_no.</param>
        /// <returns>PgsFarmDetailsModel.</returns>
        List<PgsFarmDetailsModel> GetPGSFarmDetails(long farmer_dbt_reg_no);

        /// <summary>
        /// GetPGSCropDetails.
        /// </summary>
        /// <param name="farmerDbtRegNo">farmerDbtRegNo.</param>
        /// <param name="farmId">farmId.</param>
        /// <param name="seasonId">seasonId.</param>
        /// <returns>PgsCropDetailsModel.</returns>
        List<PgsCropDetailsModel> GetPGSCropDetails(long farmerDbtRegNo, int farmId, int seasonId);

        /// <summary>
        /// Post PGS Details.
        /// </summary>
        /// <param name="createPgsDetails">createPgsDetails.</param>
        /// <returns>int.</returns>
        int PostPGSDetails(PgsDetailsCreateModel createPgsDetails);

        /// <summary>
        /// GetGroupNamesAndDbtNo.
        /// </summary>
        /// <param name="districtId">districtId.</param>
        /// <returns>List.</returns>
        List<PGSGroupNamesAndDbtNo> GetGroupNamesAndDbtNo(int districtId);

        /// <summary>
        /// Getting PGS details Repository.
        /// </summary>
        /// <param name="userid">userid.</param>
        /// <returns>User Details.</returns>
        List<DisburseEntity> IsPgsUser(int userid);

        /// <summary>
        /// Getting NPOP User Details .
        /// </summary>
        /// <param name="userid">userid.</param>
        /// <param name="designation">designation.</param>
        /// <returns>User Details.</returns>
        List<DisburseEntity> IsNpopUser(int userid, string designation);

        /// <summary>
        /// Get Npop Major Town Names.
        /// </summary>
        /// <returns>Town Names.</returns>
        List<NpopMajorTownNames> GetNpopMajorTownNames();

        /// <summary>
        /// Get Npop Major Crop Names.
        /// </summary>
        /// <returns>Crop Names.</returns>
        List<NpopMajorCropNames> GetNpopMajorCropNames();

        /// <summary>
        /// InsertAgriFarmerDetails.
        /// </summary>
        /// <param name="orgFarmAgriFarmerDetails">orgFarmAgriFarmerDetails.</param>
        /// <returns>OrgFarmAgriFarmerDetails Response.</returns>
        int InsertAgriFarmerDetails(OrgFarmAgriFarmerDetails orgFarmAgriFarmerDetails);
    }
}
