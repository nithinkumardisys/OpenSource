namespace ADIDAS.Service.DataServices
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using ADIDAS.Model.DTO;
    using ADIDAS.Repository.Interfaces;
    using ADIDAS.Service.Interfaces;

    public class PaisService : IPaisService
    {
        private IPaisRepository _paisRepository;

        public PaisService(IPaisRepository paisRepository)
        {
            _paisRepository = paisRepository;
        }

        public List<PaisMarketsEntity> GetMarket(string UserId)
        {
            return _paisRepository.GetMarket(UserId);
        }

        public List<PaisCommodityGroupEntity> GetCommodityGroup()
        {
            return _paisRepository.GetCommodityGroup();
        }

        public List<PaisCommodityEntity> GetCommodity(string commodityGroupId)
        {
            return _paisRepository.GetCommodity(commodityGroupId);
        }

        public List<PaisUnit> GetUnits()
        {
            return _paisRepository.GetUnits();
        }

        public List<PaisVariety> GetVariety(string commodityGroupId, string commodityId)
        {
            return _paisRepository.GetVariety(commodityGroupId, commodityId);
        }

        public PaisLocalPrefencesInfo GetSubmittedData(string marketId, string commodityGroupId)
        {
            return _paisRepository.GetSubmittedData(marketId, commodityGroupId);
        }

        public List<PaisLocalPrefencesInfo> GetSubmittedDataOffline(string marketId)
        {
            return _paisRepository.GetSubmittedDataOffline(marketId);
        }

        public List<ArrivalDetails> GetArrivalDetails(string marketId, DateTime selectedDate, string userId)
        {
            return _paisRepository.GetArrivalDetails(marketId, selectedDate, userId);
        }

        public List<ArrivalDetails> GetArrivalDetailsOffline(string marketId, string userId)
        {
            return _paisRepository.GetArrivalDetailsOffline(marketId, userId);
        }

        public List<ArrivalDetails> GetViewSubmissionData(string marketId, DateTime selectedDate, string userId)
        {
            return _paisRepository.GetViewSubmissionData(marketId, selectedDate, userId);
        }

        public List<AnamolusDate> GetAnamolusDate(string marketId, string currentYear, string userId)
        {
            return _paisRepository.GetAnamolusDate(marketId, currentYear, userId);
        }

        public List<AnamolusDate> GetAnamolusDateOffline(string marketId, string userId)
        {
            return _paisRepository.GetAnamolusDateOffline(marketId, userId);
        }

        public List<ArrivalDetails> GetEditPriceDataAnamolus(string marketId, string selectedDate, string userId)
        {
            return _paisRepository.GetEditPriceDataAnamolus(marketId, selectedDate, userId);
        }

        public List<ArrivalDetails> GetEditPriceDataAnamolusOffline(string marketId, string userId)
        {
            return _paisRepository.GetEditPriceDataAnamolusOffline(marketId, userId);
        }

        public bool InsertCommodityVariety(List<PaisLocalPrefencesInfo> paisLocalPrefencesInfo)
        {
            return _paisRepository.InsertCommodityVariety(paisLocalPrefencesInfo);
        }

        public bool InsertArrivalData(List<ArrivalDetails> arrivalDetails)
        {
            return _paisRepository.InsertArrivalData(arrivalDetails);
        }

        public bool EditPriceData(List<ArrivalDetails> arrivalDetails)
        {
            return _paisRepository.EditPriceData(arrivalDetails);
        }

        public bool InsertNilTransaction(List<NilTransaction> NilTransaction)
        {
            return _paisRepository.InsertNilTransaction(NilTransaction);
        }

        public bool DeleteCommodity(List<DeleteCommodityOrVariety> deleteCommodities)
        {
            return _paisRepository.DeleteCommodity(deleteCommodities);
        }

        public bool DeleteVariety(List<DeleteCommodityOrVariety> deleteCommodityOrVarieties)
        {
            return _paisRepository.DeleteVariety(deleteCommodityOrVarieties);
        }
    }
}
