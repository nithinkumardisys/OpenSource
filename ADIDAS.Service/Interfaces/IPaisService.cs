//------------------------------------------------------------------------------
// <copyright file="IPaisService.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------

namespace ADIDAS.Service.Interfaces
{
    using System;
    using System.Collections.Generic;
    using ADIDAS.Model.DTO;

    /// <summary>
    /// IPaisService.
    /// </summary>
    public interface IPaisService
    {
        List<PaisMarketsEntity> GetMarket(string userId);

        List<PaisCommodityGroupEntity> GetCommodityGroup();

        List<PaisCommodityEntity> GetCommodity(string commodityGroupId);

        List<PaisUnit> GetUnits();

        List<PaisVariety> GetVariety(string commodityGroupId, string commodityId);

        PaisLocalPrefencesInfo GetSubmittedData(string marketId, string commodityGroupId);

        List<PaisLocalPrefencesInfo> GetSubmittedDataOffline(string marketId);

        List<ArrivalDetails> GetArrivalDetails(string marketId, DateTime selectedDate, string userId);

        List<ArrivalDetails> GetArrivalDetailsOffline(string marketId, string userId);

        List<ArrivalDetails> GetViewSubmissionData(string marketId, DateTime selectedDate, string userId);

        List<AnamolusDate> GetAnamolusDate(string marketId, string currentYear, string userId);

        List<AnamolusDate> GetAnamolusDateOffline(string marketId, string userId);

        List<ArrivalDetails> GetEditPriceDataAnamolus(string marketId, string selectedDate, string userId);

        List<ArrivalDetails> GetEditPriceDataAnamolusOffline(string marketId, string userId);

        bool InsertCommodityVariety(List<PaisLocalPrefencesInfo> paisLocalPrefencesInfo);

        bool InsertArrivalData(List<ArrivalDetails> arrivalDetails);

        bool EditPriceData(List<ArrivalDetails> arrivalDetails);

        bool InsertNilTransaction(List<NilTransaction> nilTransactions);

        bool DeleteCommodity(List<DeleteCommodityOrVariety> deleteCommodities);

        bool DeleteVariety(List<DeleteCommodityOrVariety> deleteCommodityOrVarieties);
    }
}
