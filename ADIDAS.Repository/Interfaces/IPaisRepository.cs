//------------------------------------------------------------------------------
// <copyright file="IPaisRepository.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------
namespace ADIDAS.Repository.Interfaces
{
    using System;
    using System.Collections.Generic;
    using ADIDAS.Model.DTO;

    public interface IPaisRepository
    {
        /// <summary>
        /// GetMarket.
        /// </summary>
        /// <param name="userId">UserId.</param>
        /// <returns>List.</returns>
        List<PaisMarketsEntity> GetMarket(string userId);

        /// <summary>
        /// GetCommodityGroup.
        /// </summary>
        /// <returns>List.</returns>
        List<PaisCommodityGroupEntity> GetCommodityGroup();

        /// <summary>
        /// GetCommodity.
        /// </summary>
        /// <param name="commodityGroupId">marketId.</param>
        /// <returns>List.</returns>
        List<PaisCommodityEntity> GetCommodity(string commodityGroupId);

        /// <summary>
        /// GetUnits.
        /// </summary>

        /// <returns>List.</returns>
        List<PaisUnit> GetUnits();

        /// <summary>
        /// GetVariety.
        /// </summary>
        /// <param name="commodityGroupId">commodityGroupId.</param>
        /// <param name="commodityId">commodityId.</param>
        /// <returns>List.</returns>
        List<PaisVariety> GetVariety(string commodityGroupId, string commodityId);

        /// <summary>
        /// GetSubmittedData.
        /// </summary>
        /// <param name="marketId">marketId.</param>
        /// <param name="commodityGroupId">selectedDate.</param>
        /// <returns>List.</returns>
        PaisLocalPrefencesInfo GetSubmittedData(string marketId, string commodityGroupId);

        /// <summary>
        /// GetSubmittedDataOffline.
        /// </summary>
        /// <param name="marketId">marketId.</param>

        /// <returns>List.</returns>
        List<PaisLocalPrefencesInfo> GetSubmittedDataOffline(string marketId);

        /// <summary>
        /// GetArrivalDetails.
        /// </summary>
        /// <param name="marketId">marketId.</param>
        /// <param name="selectedDate">selectedDate.</param>
        /// <param name="userId">userId.</param>
        /// <returns>List.</returns>
        List<ArrivalDetails> GetArrivalDetails(string marketId, DateTime selectedDate, string userId);

        /// <summary>
        /// GetArrivalDetailsOffline.
        /// </summary>
        /// <param name="marketId">marketId.</param>
        /// <param name="userId">userId.</param>
        /// <returns>List.</returns>
        List<ArrivalDetails> GetArrivalDetailsOffline(string marketId, string userId);

        /// <summary>
        /// GetViewSubmissionData.
        /// </summary>
        /// <param name="marketId">marketId.</param>
        /// <param name="selectedDate">selectedDate.</param>
        /// <param name="userId">userId.</param>
        /// <returns>List.</returns>
        List<ArrivalDetails> GetViewSubmissionData(string marketId, DateTime selectedDate, string userId);

        /// <summary>
        /// GetAnamolusDate.
        /// </summary>
        /// <param name="marketId">marketId.</param>
        /// <param name="currentYear">currentYear.</param>
        /// <param name="userId">userId.</param>
        /// <returns>List.</returns>
        List<AnamolusDate> GetAnamolusDate(string marketId, string currentYear, string userId);

        /// <summary>
        /// GetAnamolusDateOffline.
        /// </summary>
        /// <param name="marketId">marketId.</param>
        /// <param name="userId">userId.</param>
        /// <returns>List.</returns>
        List<AnamolusDate> GetAnamolusDateOffline(string marketId, string userId);

        /// <summary>
        /// GetEditPriceDataAnamolus.
        /// </summary>
        /// <param name="marketId">marketId.</param
        /// <param name="selectedDate">selectedDate.</param>
        /// <param name="userId">userId.</param>
        /// <returns>List.</returns>
        List<ArrivalDetails> GetEditPriceDataAnamolus(string marketId, string selectedDate, string userId);

        /// <summary>
        /// GetEditPriceDataAnamolusOffline.
        /// </summary>
        /// <param name="marketId">marketId.</param>
        /// <param name="userId">userId.</param>
        /// <returns>List.</returns>
        List<ArrivalDetails> GetEditPriceDataAnamolusOffline(string marketId, string userId);

        /// <summary>
        /// InsertCommodityVariety.
        /// </summary>
        /// <param name="paisLocalPrefencesInfo">paisLocalPrefencesInfo.</param>
        /// <returns>bool.</returns>
        bool InsertCommodityVariety(List<PaisLocalPrefencesInfo> paisLocalPrefencesInfo);

        /// <summary>
        /// InsertArrivalData.
        /// </summary>
        /// <param name="arrivalDetails">arrivalDetails.</param>
        /// <returns>bool.</returns>
        bool InsertArrivalData(List<ArrivalDetails> arrivalDetails);

        /// <summary>
        /// InsertNilTransaction.
        /// </summary>
        /// <param name="nilTransactions">NilTransactions.</param>
        /// <returns>bool.</returns>
        bool InsertNilTransaction(List<NilTransaction> nilTransactions);

        /// <summary>
        /// EditPriceData.
        /// </summary>
        /// <param name="arrivalDetails">arrivalDetails.</param>
        /// <returns>bool.</returns>
        bool EditPriceData(List<ArrivalDetails> arrivalDetails);

        /// <summary>
        /// DeleteCommodity.
        /// </summary>
        /// <param name="deleteCommodities">deleteCommodities.</param>
        /// <returns>bool.</returns>
        bool DeleteCommodity(List<DeleteCommodityOrVariety> deleteCommodities);

        /// <summary>
        ///DeleteVariety.
        /// </summary>
        /// <param name="deleteCommodityOrVarieties">deleteCommodityOrVarieties.</param>
        /// <returns>bool.</returns>
        bool DeleteVariety(List<DeleteCommodityOrVariety> deleteCommodityOrVarieties);
    }
}
