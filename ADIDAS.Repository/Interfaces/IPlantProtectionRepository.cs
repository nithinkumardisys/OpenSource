//------------------------------------------------------------------------------
// <copyright file="IPlantProtectionRepository.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//----------------------------------------------------------------------------
namespace ADIDAS.Repository.Interfaces
{
    using System.Collections.Generic;
    using ADIDAS.Model.DTO;
    using ADIDAS.Model.Entities;

    /// <summary>
    /// Plant Protection Repository interface.
    /// </summary>
    public interface IPlantProtectionRepository
    {
        /// <summary>
        /// Get Pesticide Data.
        /// </summary>
        /// <returns>PesticideModel list.</returns>
        List<PesticideModel> GetPesticideData();

        /// <summary>
        /// Get Pesticide Perf Data.
        /// </summary>
        /// <param name="district_Id">district_Id.</param>
        /// <param name="pesticide_Id">pesticide_Id.</param>
        /// <param name="formulation_Id">formulation_Id.</param>
        /// <returns>PesticidePerf list.</returns>
        List<PesticidePerf> GetPesticidePerfData(string district_Id, string pesticide_Id, string formulation_Id);

        /// <summary>
        /// Get Pesticide Surveillance Perf Data.
        /// </summary>
        /// <param name="district_Id">district_Id.</param>
        /// <param name="crop_Id">crop_Id.</param>
        /// <returns>PestSurveillancePerf list.</returns>
        List<PestSurveillancePerf> GetPesticideSurveillancePerfData(string district_Id, string crop_Id);

        /// <summary>
        /// Get Comb Pesticide Data.
        /// </summary>
        /// <returns>CombPesticide list.</returns>
        List<CombPesticide> GetCombPesticideData();

        /// <summary>
        /// Get Pesticide Comb Perf Data.
        /// </summary>
        /// <param name="district_Id">district_Id.</param>
        /// <param name="comb_Pesticide_Id">comb_Pesticide_Id.</param>
        /// <returns>PesticideCombPerf list.</returns>
        List<PesticideCombPerf> GetPesticideCombPerfData(string district_Id, string comb_Pesticide_Id);

        /// <summary>
        /// Insert Pesticide Perf.
        /// </summary>
        /// <param name="pesticidePerf">pesticidePerf.</param>
        /// <returns>integer result.</returns>
        int InsertPesticidePerf(DtoPesticideperf pesticidePerf);

        /// <summary>
        /// Insert Pesticide Perf Comb.
        /// </summary>
        /// <param name="pesticidePerf">pesticidePerf.</param>
        /// <returns>integer result.</returns>
        int InsertPesticidePerfComb(DtoPesticidePerfComb pesticidePerf);

        /// <summary>
        /// Get Pesticide Perf Data Monthly.
        /// </summary>
        /// <param name="district_Id">district_Id.</param>
        /// <param name="mm_year">mm_year.</param>
        /// <returns>PesticidePerf list.</returns>
        List<PesticidePerf> GetPesticidePerfDataMonthly(string district_Id, string mm_year);

        /// <summary>
        /// Get Pesticide Comb Perf Data Monthly.
        /// </summary>
        /// <param name="district_Id">district_Id.</param>
        /// <param name="mm_year">mm_year.</param>
        /// <returns>PesticideCombPerf list.</returns>
        List<PesticideCombPerf> GetPesticideCombPerfDataMonthly(string district_Id, string mm_year);

        /// <summary>
        /// Get Pesticide Surveillance Monthly.
        /// </summary>
        /// <param name="district_Id">district_Id.</param>
        /// <param name="mm_year">mm_year.</param>
        /// <returns>PestSurviellanceDisease entity.</returns>
        PestSurviellanceDisease GetPesticideSurveillanceMonthly(string district_Id, string mm_year);

        /// <summary>
        /// Insert Pest Surveillance.
        /// </summary>
        /// <param name="surviellance">surviellance.</param>
        /// <returns>integer result.</returns>
        int InsertPestSurveillance(PestSurviellance surviellance);

        /// <summary>
        /// Get Pesticide Perf Offline.
        /// </summary>
        /// <param name="district_Id">district_Id.</param>
        /// <returns>PesticidePerfOffline list.</returns>
        List<PesticidePerfOffline> GetPesticidePerfOffline(string district_Id);

        /// <summary>
        /// Get Pesticide surveillance Offline.
        /// </summary>
        /// <param name="district_Id">district_Id.</param>
        /// <returns>PestSurviellanceDisease list.</returns>
        List<PestSurviellanceDisease> GetPesticidesurveillanceOffline(string district_Id);

        /// <summary>
        /// Get Comb Pesticide Perf Offline.
        /// </summary>
        /// <param name="district_Id">district_Id.</param>
        /// <returns>CombPesticidePerfOffline list.</returns>
        List<CombPesticidePerfOffline> GetCombPesticidePerfOffline(string district_Id);

        /// <summary>
        /// Get Pesticide Surveillance Disease.
        /// </summary>
        /// <returns>PestSurveillanceDisease list.</returns>
        List<PestSurveillanceDisease> GetPesticideSurveillanceDisease();

        /// <summary>
        /// Get Crop Stage.
        /// </summary>
        /// <returns>CropStageName list.</returns>
        List<CropStageName> GetCropStage();

        /// <summary>
        /// Get Approved Area Coverage.
        /// </summary>
        /// <param name="district_Id">district_Id.</param>
        /// <param name="crop_Id">crop_Id.</param>
        /// <param name="season_Id">season_Id.</param>
        /// <returns>ApprovedAreaCoverageRes list.</returns>
        List<ApprovedAreaCoverageRes> GetApprovedAreaCoverage(string district_Id, string crop_Id, string season_Id);
    }
}
