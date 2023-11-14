//------------------------------------------------------------------------------
// <copyright file="PlantProtectionService.cs" company="Government of Bihar">
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

    public class PlantProtectionService : IPlantProtectionService
    {
        private readonly IPlantProtectionRepository protectionRepository;

        public PlantProtectionService(IPlantProtectionRepository plantProtectionRepository)
        {
            this.protectionRepository = plantProtectionRepository;
        }

        public List<CombPesticide> GetCombPesticideData()
        {
            return this.protectionRepository.GetCombPesticideData();
        }

        public List<PesticideCombPerf> GetPesticideCombPerfData(string district_Id, string comb_Pesticide_Id)
        {
            return this.protectionRepository.GetPesticideCombPerfData(district_Id, comb_Pesticide_Id);
        }

        public List<PesticideCombPerf> GetPesticideCombPerfDataMonthly(string district_Id, string mm_year)
        {
            return this.protectionRepository.GetPesticideCombPerfDataMonthly(district_Id, mm_year);
        }

        public List<PesticideModel> GetPesticideData()
        {
            return this.protectionRepository.GetPesticideData();
        }

        public List<PesticidePerf> GetPesticidePerfData(string district_Id, string pesticide_Id, string formulation_Id)
        {
            return this.protectionRepository.GetPesticidePerfData(district_Id, pesticide_Id, formulation_Id);
        }

        public List<PesticidePerf> GetPesticidePerfDataMonthly(string district_Id, string mm_year)
        {
            return this.protectionRepository.GetPesticidePerfDataMonthly(district_Id, mm_year);
        }

        public List<PesticidePerfOffline> GetPesticidePerfOffline(string district_Id)
        {
            return this.protectionRepository.GetPesticidePerfOffline(district_Id);
        }

        public List<PestSurviellanceDisease> GetPesticidesurveillanceOffline(string district_Id)
        {
            return protectionRepository.GetPesticidesurveillanceOffline(district_Id);
        }

        public List<CombPesticidePerfOffline> GetCombPesticidePerfOffline(string district_Id)
        {
            return protectionRepository.GetCombPesticidePerfOffline(district_Id);
        }

        public PestSurviellanceDisease GetPesticideSurveillanceMonthly(string district_Id, string mm_year)
        {
            return protectionRepository.GetPesticideSurveillanceMonthly(district_Id, mm_year);
        }

        public List<PestSurveillancePerf> GetPesticideSurveillancePerfData(string district_Id, string crop_Id)
        {
            return protectionRepository.GetPesticideSurveillancePerfData(district_Id, crop_Id);
        }

        public int InsertPesticidePerf(DtoPesticideperf pesticidePerf)
        {
            return protectionRepository.InsertPesticidePerf(pesticidePerf);
        }

        public int InsertPesticidePerfComb(DtoPesticidePerfComb pesticidePerf)
        {
            return protectionRepository.InsertPesticidePerfComb(pesticidePerf);
        }

        public int InsertPestSurveillance(PestSurviellance surviellance)
        {
            return protectionRepository.InsertPestSurveillance(surviellance);
        }

        public List<PestSurveillanceDisease> GetPesticideSurveillanceDisease()
        {
            return protectionRepository.GetPesticideSurveillanceDisease();
        }

        public List<CropStageName> GetCropStage()
        {
            return protectionRepository.GetCropStage();
        }

        public List<ApprovedAreaCoverageRes> GetApprovedAreaCoverage(string district_Id, string crop_Id, string season_Id)
        {
            return protectionRepository.GetApprovedAreaCoverage(district_Id, crop_Id, season_Id);
        }
    }
}
