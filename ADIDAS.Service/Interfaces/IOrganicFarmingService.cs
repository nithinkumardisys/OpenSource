//------------------------------------------------------------------------------
// <copyright file="IOrganicFarmingService.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------
namespace ADIDAS.Service.Interfaces
{
    using System.Collections.Generic;
    using ADIDAS.Model.DTO;
    using ADIDAS.Model.Entities;

    /// <summary>
    /// Interface Organic Farming Service.
    /// </summary>
    public interface IOrganicFarmingService
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
        /// <returns>NpopSchemeModel.</returns>
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

        List<PGSFarmerGroupTable> GetFarmerGroupTable(int districtId);

        List<PGSSchemeNames> GetPGSSchemeNames();

        /// <summary>
        /// InsertFarmerGroupDetails.
        /// </summary>
        /// <param name="pgsFarmerGroupDtls">pgsFarmerGroupDtls.</param>
        /// <returns>int.</returns>
        int InsertFarmerGroupDetails(PGSFarmerGroupDetails pgsFarmerGroupDtls);

        /// <summary>
        /// Get Associated Farmer Details.
        /// </summary>
        /// <param name="districtId">districtId.</param>
        /// <param name="blockId">blockId.</param>
        /// <param name="panchayatId">panchayatId.</param>
        /// <param name="seasonId">seasonId.</param>
        /// <param name="groupId">groupId.</param>
        /// <param name="status">status.</param>
        /// <returns>List NPOPDetails.</returns>
        List<AssociatedFarmerDetailsListModel> GetAssociatedFarmerDetails(int districtId, int blockId, int panchayatId, int seasonId, int groupId, string status);

        List<PGSGroupNames> GetPGSGroupNames(int districtId);

        List<PGSFarmerGroupDetailsId> GetFarmerGroupDetails(string group_reg_no);

        List<PGSMajorTownNames> GetPGSMajorTownNames();

        List<PGSMajorCropNames> GetPGSMajorCropNames();

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

        int PostPGSDetails(PgsDetailsCreateModel createPgsDetails);

        List<PGSGroupNamesAndDbtNo> GetGroupNamesAndDbtNo(int districtId);

        /// <summary>
        /// Get PGS User Details.
        /// </summary>
        /// <param name="userid">user_id.</param>
        /// <returns>PGS User.</returns>
        List<DisburseEntity> IsPgsUser(int userid);

        /// <summary>
        /// Getting Npop User Details .
        /// </summary>
        /// <param name="userid">userid.</param>
        /// <param name="designation">designation.</param>
        /// <returns>User Details.</returns>
        List<DisburseEntity> IsNpopUser(int userid, string designation);

        /// <summary>
        /// Get Npop Major Town Names.
        /// </summary>
        /// <returns>NpopMajorTownNames.</returns>
        List<NpopMajorTownNames> GetNpopMajorTownNames();

        /// <summary>
        /// Get Npop Major Crop Names.
        /// </summary>
        /// <returns>NpopMajorCropNames.</returns>
        List<NpopMajorCropNames> GetNpopMajorCropNames();

        /// <summary>
        /// InsertAgriFarmerDetails.
        /// </summary>
        /// <param name="orgFarmAgriFarmerDetails">orgFarmAgriFarmerDetails.</param>
        /// <returns>OrgFarmAgriFarmerDetails Response.</returns>
        int InsertAgriFarmerDetails(OrgFarmAgriFarmerDetails orgFarmAgriFarmerDetails);
    }
}
