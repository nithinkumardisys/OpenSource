//------------------------------------------------------------------------------
// <copyright file="IMiscService.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------

namespace ADIDAS.Service.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using ADIDAS.Model.DTO;
    using ADIDAS.Model.Entities;
    using DepartmentOfAgriculture.Admin.Models.Models;
    using DIDAS.Model.Entities;

    /// <summary>
    /// IMiscService.
    /// </summary>
    public interface IMiscService
    {
        /// <summary>
        /// GetReports.
        /// </summary>
        /// <returns>AppReport list.</returns>
        List<AppReport> GetReports();

        /// <summary>
        /// GetSource.
        /// </summary>
        /// <param name="attributeName">attributeName.</param>
        /// <returns>MobileAttributeConfig.</returns>
        List<MobileAttributeConfig> GetSource(string attributeName);

        /// <summary>
        /// GetNotificationAudits.
        /// </summary>
        /// <returns>NotificationEntity.</returns>
        List<NotificationEntity> GetNotificationAudits();

        /// <summary>
        /// InsertNotificationAudits.
        /// </summary>
        /// <param name="entity">entity.</param>
        /// <returns>integer.</returns>
        int InsertNotificationAudits(NotificationEntity entity);

        /// <summary>
        /// DeleteNotification.
        /// </summary>
        /// <param name="iD">id.</param>
        /// <returns>integer.</returns>
        int DeleteNotification(int iD);

        /// <summary>
        /// DeleteAllNotification.
        /// </summary>
        /// <returns>integer.</returns>
        int DeleteAllNotification();

        /// <summary>
        /// InsertDistrictDim.
        /// </summary>
        /// <param name="districtLGs">districtLGs.</param>
        /// <returns>integer.</returns>
        int InsertDistrictDim(List<DistrictLG> districtLGs);

        /// <summary>
        /// InsecticideListAsync.
        /// </summary>
        /// <param name="insecticideLicenceModels">insecticideLicenceModels.</param>
        /// <returns>integer.</returns>
        int InsecticideListAsync(List<InsecticideLicenceModel> insecticideLicenceModels);

        /// <summary>
        /// FarmNameListAsync.
        /// </summary>
        /// <param name="farmNameModels">farmNameModels.</param>
        /// <returns>integer.</returns>
        int FarmNameListAsync(List<FarmNameModel> farmNameModels);

        /// <summary>
        /// InsertBlockDim.
        /// </summary>
        /// <param name="blockLG">blockLG.</param>
        /// <returns>integer.</returns>
        int InsertBlockDim(List<BlockLG> blockLG);

        /// <summary>
        /// InsertPanchayatDim.
        /// </summary>
        /// <param name="panchayatLG">panchayatLG.</param>
        /// <returns>integer.</returns>
        int InsertPanchayatDim(List<PanchayatLG> panchayatLG);

        /// <summary>
        /// InsertVillageDim.
        /// </summary>
        /// <param name="villageLG">villageLG.</param>
        /// <returns>integer.</returns>
        int InsertVillageDim(List<VillageLG> villageLG);

        /// <summary>
        /// PostSource.
        /// </summary>
        /// <param name="appConfig">appConfig.</param>
        /// <returns>integer.</returns>
        int PostSource(AppConfig appConfig);

        /// <summary>
        /// InsertScheme.
        /// </summary>
        /// <param name="schemes">schemes.</param>
        /// <returns>integer.</returns>
        int InsertScheme(List<Scheme> schemes);

        /// <summary>
        /// InsertbrbnApplication.
        /// </summary>
        /// <param name="app">app.</param>
        /// <returns>integer.</returns>
        int InsertbrbnApplication(List<BrbnApplication> app);

        /// <summary>
        /// GetdistrictCode.
        /// </summary>
        /// <returns>District.</returns>
        List<District> GetdistrictCode();

        /// <summary>
        /// GetOfmasSchemes.
        /// </summary>
        /// <returns>OfmasScheme.</returns>
        List<OfmasScheme> GetOfmasSchemes();

        /// <summary>
        /// InsertFarmerInfo.
        /// </summary>
        /// <param name="dtCast">dtCast.</param>
        /// <param name="dtGender">dtGender.</param>
        /// <param name="dtType">dtType.</param>
        /// <returns>integer.</returns>
        int InsertFarmerInfo(DataTable dtCast, DataTable dtGender, DataTable dtType);

        /// <summary>
        /// GetAppLink.
        /// </summary>
        /// <returns>AppLink list.</returns>
        List<AppLink> GetAppLink();

        /// <summary>
        /// GetReleaseData.
        /// </summary>
        /// <returns>Release.</returns>
        Release GetReleaseData();

        /// <summary>
        /// GetDataFAQ.
        /// </summary>
        /// <returns>dynamic.</returns>
        dynamic GetDataFAQ();

        /// <summary>
        /// GetBihanGuidlines.
        /// </summary>
        /// <returns>BihanGuidelineModel.</returns>
        List<BihanGuidelineModel> GetBihanGuidlines();

        /// <summary>
        /// GetDOAInstructions.
        /// </summary>
        /// <returns>DoaInstructions.</returns>
        List<DoaInstructions> GetDOAInstructions();

        /// <summary>
        /// GetPanchayatdim.
        /// </summary>
        /// <returns>PanchayatData.</returns>
        List<PanchayatData> GetPanchayatdim();

        /// <summary>
        /// GetOfmasBlock.
        /// </summary>
        /// <returns>PanchayatData.</returns>
        List<PanchayatData> GetOfmasBlock();

        /// <summary>
        /// InsertSubsidyStatus.
        /// </summary>
        /// <param name="dt">dt.</param>
        /// <returns>integer.</returns>
        int InsertSubsidyStatus(DataTable dt);

        /// <summary>
        /// InsertSubsidyReport.
        /// </summary>
        /// <param name="subsidyReport">subsidyReport.</param>
        /// <returns>integer.</returns>
        int InsertSubsidyReport(dynamic subsidyReport);

        /// <summary>
        /// GetAllDesignations.
        /// </summary>
        /// <returns>MobileAttributeConfig.</returns>
        List<MobileAttributeConfig> GetAllDesignations();

        /// <summary>
        /// GetReceipientDesignations.
        /// </summary>
        /// <returns>MobileAttributeConfig.</returns>
        List<MobileAttributeConfig> GetReceipientDesignations();

        /// <summary>
        /// GetSeasonInfo.
        /// </summary>
        /// <param name="season">season.</param>
        /// <returns>SeasonInfo list.</returns>
        List<SeasonInfo> GetSeasonInfo(string season);

        /// <summary>
        /// GetFarmerInfo.
        /// </summary>
        /// <param name="mobileno">mobileno.</param>
        /// <returns>FarmerInfo list.</returns>
        List<FarmerInfo> GetFarmerInfo(string mobileno);

        /// <summary>
        /// GetPesticideLicenseInfo.
        /// </summary>
        /// <returns>PesticideInfo list.</returns>
        List<PesticideInfo> GetPesticideLicenseInfo();

        /// <summary>
        /// InsertFarmer.
        /// </summary>
        /// <param name="dt">dt.</param>
        /// <returns>integer.</returns>
        int InsertFarmer(DataTable dt);

        /// <summary>
        /// InsertOFMASScheme.
        /// </summary>
        /// <param name="dt">dt.</param>
        /// <returns>integer.</returns>
        int InsertOFMASScheme(DataTable dt);

        /// <summary>
        /// InsertOFMASAMRTDist.
        /// </summary>
        /// <param name="dt">dt.</param>
        /// <returns>integer.</returns>
        int InsertOFMASAMRTDist(DataTable dt);

        /// <summary>
        /// InsertOFMASAMRTBlk.
        /// </summary>
        /// <param name="dt">dt.</param>
        /// <returns>integer.</returns>
        int InsertOFMASAMRTBlk(DataTable dt);

        /// <summary>
        /// InsertOFMASAMRTPan.
        /// </summary>
        /// <param name="dt">dt.</param>
        /// <returns>integer.</returns>
        int InsertOFMASAMRTPan(DataTable dt);

        /// <summary>
        /// InsertOFMASKKADist.
        /// </summary>
        /// <param name="dt">dt.</param>
        /// <returns>integer.</returns>
        int InsertOFMASKKADist(DataTable dt);

        /// <summary>
        /// InsertOFMASBGREIDist.
        /// </summary>
        /// <param name="dt">dt.</param>
        /// <returns>integer.</returns>
        int InsertOFMASBGREIDist(DataTable dt);

        /// <summary>
        /// InsertOFMASBGREIBlk.
        /// </summary>
        /// <param name="dt">dt.</param>
        /// <returns>integer.</returns>
        int InsertOFMASBGREIBlk(DataTable dt);

        /// <summary>
        /// InsertOFMASBGREIPAN.
        /// </summary>
        /// <param name="dt">dt.</param>
        /// <returns>integer.</returns>
        int InsertOFMASBGREIPAN(DataTable dt);

        /// <summary>
        /// InsertOFMASNFSMDist.
        /// </summary>
        /// <param name="dt">dt.</param>
        /// <returns>integer.</returns>
        int InsertOFMASNFSMDist(DataTable dt);

        /// <summary>
        /// InsertOFMASNFSMBlk.
        /// </summary>
        /// <param name="dt">dt.</param>
        /// <returns>integer.</returns>
        int InsertOFMASNFSMBlk(DataTable dt);

        /// <summary>
        /// InsertOFMASNFSMPAN.
        /// </summary>
        /// <param name="dt">dt.</param>
        /// <returns>integer.</returns>
        int InsertOFMASNFSMPAN(DataTable dt);

        /// <summary>
        /// InsertOFMASNFSMOILSEEDSDist.
        /// </summary>
        /// <param name="dt">dt.</param>
        /// <returns>integer.</returns>
        int InsertOFMASNFSMOILSEEDSDist(DataTable dt);

        /// <summary>
        /// InsertOFMASNFSMOILSEEDSBlk.
        /// </summary>
        /// <param name="dt">dt.</param>
        /// <returns>integer.</returns>
        int InsertOFMASNFSMOILSEEDSBlk(DataTable dt);

        /// <summary>
        /// InsertOFMASNFSMOILSEEDSPAN.
        /// </summary>
        /// <param name="dt">dt.</param>
        /// <returns>integer.</returns>
        int InsertOFMASNFSMOILSEEDSPAN(DataTable dt);

        /// <summary>
        /// InsertOFMASSMAMDist.
        /// </summary>
        /// <param name="dt">dt.</param>
        /// <returns>integer.</returns>
        int InsertOFMASSMAMDist(DataTable dt);

        /// <summary>
        /// InsertOFMASSMAMBlk.
        /// </summary>
        /// <param name="dt">dt.</param>
        /// <returns>integer.</returns>
        int InsertOFMASSMAMBlk(DataTable dt);

        /// <summary>
        /// InsertOFMASSMAMPAN.
        /// </summary>
        /// <param name="dt">dt.</param>
        /// <returns>integer.</returns>
        int InsertOFMASSMAMPAN(DataTable dt);

        /// <summary>
        /// InsertOFMASSMAMSCHCDist.
        /// </summary>
        /// <param name="dt">dt.</param>
        /// <returns>integer.</returns>
        int InsertOFMASSMAMSCHCDist(DataTable dt);

        /// <summary>
        /// InsertOFMASSMAMSCHCBlk.
        /// </summary>
        /// <param name="dt">dt.</param>
        /// <returns>integer.</returns>
        int InsertOFMASSMAMSCHCBlk(DataTable dt);

        /// <summary>
        /// InsertOFMASSMAMSCHCPAN.
        /// </summary>
        /// <param name="dt">dt.</param>
        /// <returns>integer.</returns>
        int InsertOFMASSMAMSCHCPAN(DataTable dt);

        /// <summary>
        /// InsertOFMAKKABlk.
        /// </summary>
        /// <param name="dt">dt.</param>
        /// <returns>integer.</returns>
        int InsertOFMAKKABlk(DataTable dt);

        /// <summary>
        /// InsertOFMAKKAPANk.
        /// </summary>
        /// <param name="dt">dt.</param>
        /// <returns>integer.</returns>
        int InsertOFMAKKAPANk(DataTable dt);

        /// <summary>
        /// InsertMIDetails.
        /// </summary>
        /// <param name="dt">dt.</param>
        /// <returns>integer.</returns>
        int InsertMIDetails(DataTable dt);

        /// <summary>
        /// PostGamificationConfig.
        /// </summary>
        /// <param name="dt">dt.</param>
        /// <returns>integer.</returns>
        int PostGamificationConfig(GamificationConfigDto config);

        /// <summary>
        /// InsertParaliDetails.
        /// </summary>
        /// <param name="dt">dt.</param>
        /// <returns>integer.</returns>
        int InsertParaliDetails(DataTable dt);

        /// <summary>
        /// InsertSoilHealth.
        /// </summary>
        /// <param name="dt">dt.</param>
        /// <returns>integer.</returns>
        int InsertSoilHealth(DataTable dt);

        /// <summary>
        /// InsertHortiHybridSeedDetails.
        /// </summary>
        /// <param name="details">details.</param>
        /// <returns>integer.</returns>
        int InsertHortiHybridSeedDetails(dynamic details);

        /// <summary>
        /// Insertbassoca.
        /// </summary>
        /// <param name="dt">dt.</param>
        /// <returns>integer.</returns>
        int Insertbassoca(DataTable dt);

        /// <summary>
        /// OFMAS State Plan scheme panchayat data.
        /// </summary>
        /// <param name="dt">dt.</param>
        /// <returns>int.</returns>
        int InsertOFMASStatePlanPan(DataTable dt);

        /// <summary>
        /// InsertLicenseHolder.
        /// </summary>
        /// <param name="licenseHolders">licenseHolders.</param>
        /// <param name="queryName">queryName.</param>
        /// <returns>result integer.</returns>
        int InsertLicenseHolder(List<InsecticideLicenceModel> licenseHolders, string queryName);

        /// <summary>
        /// NHM data.
        /// </summary>
        /// <param name="dt">dt.</param>
        /// <param name="schemecode">schemecode.</param>
        /// <returns>int.</returns>
        int InsertNHMData(DataTable dt, string schemecode);
    }
}
