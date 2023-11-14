//------------------------------------------------------------------------------
// <copyright file="IPlantProtectionService.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------
namespace ADIDAS.Service.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using ADIDAS.Model.DTO;
    using ADIDAS.Model.Entities;

    /// <summary>
    /// IPlantProtectionService.
    /// </summary>
    public interface IPlantProtectionService
    {
        /// <summary>
        /// GetPesticideData.
        /// </summary>
        /// <returns>PesticideModel list.</returns>
        List<PesticideModel> GetPesticideData();

        /// <summary>
        /// GetPesticidePerfData.
        /// </summary>
        /// <param name="district_Id">district_Id.</param>
        /// <param name="pesticide_Id">pesticide_Id.</param>
        /// <param name="formulation_Id">formulation_Id.</param>
        /// <returns>PesticidePerf list.</returns>
        List<PesticidePerf> GetPesticidePerfData(string district_Id, string pesticide_Id, string formulation_Id);

        /// <summary>
        /// GetPesticideSurveillancePerfData.
        /// </summary>
        /// <param name="district_Id">district_Id.</param>
        /// <param name="crop_Id">crop_Id.</param>
        /// <returns>PestSurveillancePerf list.</returns>
        List<PestSurveillancePerf> GetPesticideSurveillancePerfData(string district_Id, string crop_Id);

        /// <summary>
        /// GetCombPesticideData.
        /// </summary>
        /// <returns>CombPesticide list.</returns>
        List<CombPesticide> GetCombPesticideData();

        /// <summary>
        /// GetPesticideCombPerfData.
        /// </summary>
        /// <param name="district_Id">district_Id.</param>
        /// <param name="comb_Pesticide_Id">comb_Pesticide_Id.</param>
        /// <returns>PesticideCombPerf list.</returns>
        List<PesticideCombPerf> GetPesticideCombPerfData(string district_Id, string comb_Pesticide_Id);

        /// <summary>
        /// InsertPesticidePerf.
        /// </summary>
        /// <param name="pesticidePerf">pesticidePerf.</param>
        /// <returns>integer.</returns>
        int InsertPesticidePerf(DtoPesticideperf pesticidePerf);

        /// <summary>
        /// InsertPesticidePerfComb.
        /// </summary>
        /// <param name="pesticidePerf">pesticidePerf.</param>
        /// <returns>integer.</returns>
        int InsertPesticidePerfComb(DtoPesticidePerfComb pesticidePerf);

        /// <summary>
        /// GetPesticidePerfDataMonthly.
        /// </summary>
        /// <param name="district_Id">district_Id.</param>
        /// <param name="mm_year">mm_year.</param>
        /// <returns>PesticidePerf list.</returns>
        List<PesticidePerf> GetPesticidePerfDataMonthly(string district_Id, string mm_year);

        /// <summary>
        /// GetPesticideCombPerfDataMonthly.
        /// </summary>
        /// <param name="district_Id">district_Id.</param>
        /// <param name="mm_year">mm_year.</param>
        /// <returns>PesticideCombPerf list.</returns>
        List<PesticideCombPerf> GetPesticideCombPerfDataMonthly(string district_Id, string mm_year);

        /// <summary>
        /// GetPesticideSurveillanceMonthly.
        /// </summary>
        /// <param name="district_Id">district_Id.</param>
        /// <param name="mm_year">mm_year.</param>
        /// <returns>PestSurviellanceDisease.</returns>
        PestSurviellanceDisease GetPesticideSurveillanceMonthly(string district_Id, string mm_year);

        /// <summary>
        /// InsertPestSurveillance.
        /// </summary>
        /// <param name="surviellance">surviellance.</param>
        /// <returns>integer.</returns>
        int InsertPestSurveillance(PestSurviellance surviellance);

        /// <summary>
        /// GetPesticidePerfOffline.
        /// </summary>
        /// <param name="district_Id">district_Id.</param>
        /// <returns>PesticidePerfOffline list.</returns>
        List<PesticidePerfOffline> GetPesticidePerfOffline(string district_Id);

        /// <summary>
        /// GetPesticidesurveillanceOffline.
        /// </summary>
        /// <param name="district_Id">district_Id.</param>
        /// <returns>PestSurviellanceDisease list.</returns>
        List<PestSurviellanceDisease> GetPesticidesurveillanceOffline(string district_Id);

        /// <summary>
        /// GetCombPesticidePerfOffline.
        /// </summary>
        /// <param name="district_Id">district_Id.</param>
        /// <returns>CombPesticidePerfOffline list.</returns>
        List<CombPesticidePerfOffline> GetCombPesticidePerfOffline(string district_Id);

        /// <summary>
        /// GetPesticideSurveillanceDisease.
        /// </summary>
        /// <returns>PestSurveillanceDisease list.</returns>
        List<PestSurveillanceDisease> GetPesticideSurveillanceDisease();

        /// <summary>
        /// GetCropStage.
        /// </summary>
        /// <returns>CropStageName list.</returns>
        List<CropStageName> GetCropStage();

        /// <summary>
        /// GetApprovedAreaCoverage.
        /// </summary>
        /// <param name="district_Id">district_Id.</param>
        /// <param name="crop_Id">crop_Id.</param>
        /// <param name="season_Id">season_Id.</param>
        /// <returns>ApprovedAreaCoverageRes.</returns>
        List<ApprovedAreaCoverageRes> GetApprovedAreaCoverage(string district_Id, string crop_Id, string season_Id);
    }
}
