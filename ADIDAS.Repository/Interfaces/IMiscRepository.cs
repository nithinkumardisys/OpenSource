//------------------------------------------------------------------------------
// <copyright file="IMiscRepository.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//----------------------------------------------------------------------------
namespace ADIDAS.Repository.Interfaces
{
    using System.Collections.Generic;
    using System.Data;
    using ADIDAS.Model.DTO;
    using ADIDAS.Model.Entities;
    using DepartmentOfAgriculture.Admin.Models.Models;
    using DIDAS.Model.Entities;

    /// <summary>
    /// interface for MiscRepository.
    /// </summary>
    public interface IMiscRepository
    {
        /// <summary>
        /// Get Reports.
        /// </summary>
        /// <returns>AppReport list.</returns>
        List<AppReport> GetReports();

        /// <summary>
        /// Get district Code.
        /// </summary>
        /// <returns>District list.</returns>
        List<District> GetdistrictCode();

        /// <summary>
        /// Get Ofmas Schemes.
        /// </summary>
        /// <returns>OfmasScheme list.</returns>
        List<OfmasScheme> GetOfmasSchemes();

        /// <summary>
        /// Get Ofmas Block.
        /// </summary>
        /// <returns>PanchayatData list.</returns>
        List<PanchayatData> GetOfmasBlock();

        /// <summary>
        /// Get Release Data.
        /// </summary>
        /// <returns>Release entity.</returns>
        Release GetReleaseData();

        /// <summary>
        /// Get DOA Instructions.
        /// </summary>
        /// <returns>DOAInstructions list.</returns>
        List<DoaInstructions> GetDOAInstructions();

        /// <summary>
        /// Insert Farmer Info.
        /// </summary>
        /// <param name="dtCast">dtCast.</param>
        /// <param name="dtGender">dtGender.</param>
        /// <param name="dtType">dtType.</param>
        /// <returns>integer result.</returns>
        int InsertFarmerInfo(DataTable dtCast, DataTable dtGender, DataTable dtType);

        /// <summary>
        /// Get Source.
        /// </summary>
        /// <param name="attributeName">attributeName.</param>
        /// <returns>MobileAttributeConfig list.</returns>
        List<MobileAttributeConfig> GetSource(string attributeName);

        /// <summary>
        /// Get Notification Audits.
        /// </summary>
        /// <returns>NotificationEntity list.</returns>
        List<NotificationEntity> GetNotificationAudits();

        /// <summary>
        /// Insert Notification Audits.
        /// </summary>
        /// <param name="entity">entity.</param>
        /// <returns>integer result.</returns>
        int InsertNotificationAudits(NotificationEntity entity);

        /// <summary>
        /// Delete Notification.
        /// </summary>
        /// <param name="id">id.</param>
        /// <returns>integer result.</returns>
        int DeleteNotification(int id);

        /// <summary>
        /// Delete ALL Notifications.
        /// </summary>
        /// <returns>integer result.</returns>
        int DeleteALLNotifications();

        /// <summary>
        /// Insert District Dim.
        /// </summary>
        /// <param name="districtLGs">districtLGs.</param>
        /// <returns>integer result.</returns>
        int InsertDistrictDim(List<DistrictLG> districtLGs);

        /// <summary>
        /// Insecticide List Async.
        /// </summary>
        /// <param name="insecticideLicenceModels">insecticideLicenceModels.</param>
        /// <returns>integer result.</returns>
        int InsecticideListAsync(List<InsecticideLicenceModel> insecticideLicenceModels);

        /// <summary>
        /// IFarm Name List Async.
        /// </summary>
        /// <param name="farmNameModels">farmNameModels.</param>
        /// <returns>integer result.</returns>
        int FarmNameListAsync(List<FarmNameModel> farmNameModels);

        /// <summary>
        /// IFarm Name List Async.
        /// </summary>
        /// <param name="blockLG">blockLG.</param>
        /// <returns>integer result.</returns>
        int InsertBlockDim(List<BlockLG> blockLG);

        /// <summary>
        /// Insert Panchayat Dim.
        /// </summary>
        /// <param name="panchayatLG">panchayatLG.</param>
        /// <returns>integer result.</returns>
        int InsertPanchayatDim(List<PanchayatLG> panchayatLG);

        /// <summary>
        /// InsertVillageDim.
        /// </summary>
        /// <param name="villageLG">villageLG.</param>
        /// <returns>integer.</returns>
        int InsertVillageDim(List<VillageLG> villageLG);

        /// <summary>
        /// Post Source.
        /// </summary>
        /// <param name="appConfig">appConfig.</param>
        /// <returns>integer result.</returns>
        int PostSource(AppConfig appConfig);

        /// <summary>
        /// Insert Scheme.
        /// </summary>
        /// <param name="schemes">schemes.</param>
        /// <returns>integer result.</returns>
        int InsertScheme(List<Scheme> schemes);

        /// <summary>
        /// Insert brbn Application.
        /// </summary>
        /// <param name="app">app.</param>
        /// <returns>integer result.</returns>
        int InsertbrbnApplication(List<BrbnApplication> app);

        /// <summary>
        /// Get App Link.
        /// </summary>
        /// <returns>AppLink list.</returns>
        List<AppLink> GetAppLink();

        /// <summary>
        /// Insert brbn Application Status.
        /// </summary>
        /// <param name="appstatus">appstatus.</param>
        /// <returns>integer result.</returns>
        int InsertbrbnApplicationStatus(List<BrBnApplicationStatus> appstatus);

        /// <summary>
        /// Get Data FAQ.
        /// </summary>
        /// <returns>dynamic result.</returns>
        dynamic GetDataFAQ();

        /// <summary>
        /// GetBihanGuidlines.
        /// </summary>
        /// <returns>BihanGuidelineModel list.</returns>
        List<BihanGuidelineModel> GetBihanGuidlines();

        /// <summary>
        /// GetPanchayatdim.
        /// </summary>
        /// <returns>PanchayatData list.</returns>
        List<PanchayatData> GetPanchayatdim();

        /// <summary>
        /// Insert Subsidy Status.
        /// </summary>
        /// <param name="dt">dt.</param>
        /// <returns>integer result.</returns>
        int InsertSubsidyStatus(DataTable dt);

        /// <summary>
        /// InsertSubsidyReport.
        /// </summary>
        /// <param name="subsidyReport">subsidyReport.</param>
        /// <returns>integer.</returns>
        int InsertSubsidyReport(dynamic subsidyReport);

        /// <summary>
        /// Get All Designations.
        /// </summary>
        /// <returns>MobileAttributeConfig list.</returns>
        List<MobileAttributeConfig> GetAllDesignations();

        /// <summary>
        /// GetReceipientDesignations.
        /// </summary>
        /// <returns>list.</returns>
        List<MobileAttributeConfig> GetReceipientDesignations();

        /// <summary>
        /// Get Season Info.
        /// </summary>
        /// <param name="season">season.</param>
        /// <returns>SeasonInfo list.</returns>
        List<SeasonInfo> GetSeasonInfo(string season);

        /// <summary>
        /// Get Pesticide License Info.
        /// </summary>
        /// <returns>PesticideInfo list.</returns>
        List<PesticideInfo> GetPesticideLicenseInfo();

        /// <summary>
        /// InsertFarmer.
        /// </summary>
        /// <param name="dt">dt.</param>
        /// <returns>result integer.</returns>
        int InsertFarmer(DataTable dt);

        /// <summary>
        /// Get Farmer Info.
        /// </summary>
        /// <param name="mobileno">mobileno.</param>
        /// <returns>FarmerInfo list.</returns>
        List<FarmerInfo> GetFarmerInfo(string mobileno);

        /// <summary>
        /// Insert OFMAS Scheme.
        /// </summary>
        /// <param name="dt">dt.</param>
        /// <returns>integer result.</returns>
        int InsertOFMASScheme(DataTable dt);

        /// <summary>
        /// Insert OFMA SAMRT Dist.
        /// </summary>
        /// <param name="dt">dt.</param>
        /// <returns>integer result.</returns>
        int InsertOFMASAMRTDist(DataTable dt);

        /// <summary>
        /// Insert OFMA SAMRT Blk.
        /// </summary>
        /// <param name="dt">dt.</param>
        /// <returns>integer result.</returns>
        int InsertOFMASAMRTBlk(DataTable dt);

        /// <summary>
        /// Insert OFMA SAMRT Pan.
        /// </summary>
        /// <param name="dt">dt.</param>
        /// <returns>integer result.</returns>
        int InsertOFMASAMRTPan(DataTable dt);

        /// <summary>
        /// Insert OFMA SKKA Dist.
        /// </summary>
        /// <param name="dt">dt.</param>
        /// <returns>integer result.</returns>
        int InsertOFMASKKADist(DataTable dt);

        /// <summary>
        /// Insert OFMAS BGREI Dist.
        /// </summary>
        /// <param name="dt">dt.</param>
        /// <returns>integer result.</returns>
        int InsertOFMASBGREIDist(DataTable dt);

        /// <summary>
        /// Insert OFMAS BGREI block.
        /// </summary>
        /// <param name="dt">dt.</param>
        /// <returns>integer result.</returns>
        int InsertOFMASBGREIBlk(DataTable dt);

        /// <summary>
        /// Insert OFMAS BGREI panchayat.
        /// </summary>
        /// <param name="dt">dt.</param>
        /// <returns>integer result.</returns>
        int InsertOFMASBGREIPAN(DataTable dt);

        /// <summary>
        /// Insert OFMAS SNFSM Dist.
        /// </summary>
        /// <param name="dt">dt.</param>
        /// <returns>integer result.</returns>
        int InsertOFMASNFSMDist(DataTable dt);

        /// <summary>
        /// Insert OFMAS SNFSM block.
        /// </summary>
        /// <param name="dt">dt.</param>
        /// <returns>integer result.</returns>
        int InsertOFMASNFSMBlk(DataTable dt);

        /// <summary>
        /// Insert OFMAS SNFSM panchayat.
        /// </summary>
        /// <param name="dt">dt.</param>
        /// <returns>integer result.</returns>
        int InsertOFMASNFSMPAN(DataTable dt);

        /// <summary>
        /// Insert OFMAS MOILSEEDS Dist.
        /// </summary>
        /// <param name="dt">dt.</param>
        /// <returns>integer result.</returns>
        int InsertOFMASNFSMOILSEEDSDist(DataTable dt);

        /// <summary>
        /// Insert OFMAS MOILSEEDS block.
        /// </summary>
        /// <param name="dt">dt.</param>
        /// <returns>integer result.</returns>
        int InsertOFMASNFSMOILSEEDSBlk(DataTable dt);

        /// <summary>
        /// Insert OFMAS MOILSEEDS panchayat.
        /// </summary>
        /// <param name="dt">dt.</param>
        /// <returns>integer result.</returns>
        int InsertOFMASNFSMOILSEEDSPAN(DataTable dt);

        /// <summary>
        /// Insert OFMAS SSMAM Dist.
        /// </summary>
        /// <param name="dt">dt.</param>
        /// <returns>integer result.</returns>
        int InsertOFMASSMAMDist(DataTable dt);

        /// <summary>
        /// Insert OFMAS SSMAM block.
        /// </summary>
        /// <param name="dt">dt.</param>
        /// <returns>integer result.</returns>
        int InsertOFMASSMAMBlk(DataTable dt);

        /// <summary>
        /// Insert OFMAS SSMAM panchayat.
        /// </summary>
        /// <param name="dt">dt.</param>
        /// <returns>integer result.</returns>
        int InsertOFMASSMAMPAN(DataTable dt);

        /// <summary>
        /// Insert OFMAS SSMAM SCH Dist.
        /// </summary>
        /// <param name="dt">dt.</param>
        /// <returns>integer result.</returns>
        int InsertOFMASSMAMSCHCDist(DataTable dt);

        /// <summary>
        /// Insert OFMAS SSMAM SCH block.
        /// </summary>
        /// <param name="dt">dt.</param>
        /// <returns>integer result.</returns>
        int InsertOFMASSMAMSCHCBlk(DataTable dt);

        /// <summary>
        /// Insert OFMAS SSMAM SCH Panchayat.
        /// </summary>
        /// <param name="dt">dt.</param>
        /// <returns>integer result.</returns>
        int InsertOFMASSMAMSCHCPAN(DataTable dt);

        /// <summary>
        /// Insert OFMAS KKA block.
        /// </summary>
        /// <param name="dt">dt.</param>
        /// <returns>integer result.</returns>
        int InsertOFMAKKABlk(DataTable dt);

        /// <summary>
        /// Insert OFMAKKA PANk.
        /// </summary>
        /// <param name="dt">dt.</param>
        /// <returns>integer result.</returns>
        int InsertOFMAKKAPANk(DataTable dt);

        /// <summary>
        /// Insert MI Details.
        /// </summary>
        /// <param name="dt">dt.</param>
        /// <returns>integer result.</returns>
        int InsertMIDetails(DataTable dt);

        /// <summary>
        /// Post Gamification Config.
        /// </summary>
        /// <param name="config">config.</param>
        /// <returns>integer result.</returns>
        int PostGamificationConfig(GamificationConfigDto config);

        /// <summary>
        /// Insert Parali Details.
        /// </summary>
        /// <param name="dt">dt.</param>
        /// <returns>integer result.</returns>
        int InsertParaliDetails(DataTable dt);

        /// <summary>
        /// Insert Soil Health.
        /// </summary>
        /// <param name="dt">dt.</param>
        /// <returns>integer result.</returns>
        int InsertSoilHealth(DataTable dt);

        /// <summary>
        /// Insert Horti Hybrid Seed Details.
        /// </summary>
        /// <param name="details">details.</param>
        /// <returns>integer result.</returns>
        int InsertHortiHybridSeedDetails(dynamic details);

        /// <summary>
        /// Insert bassoca.
        /// </summary>
        /// <param name="dt">dt.</param>
        /// <returns>integer result.</returns>
        int Insertbassoca(DataTable dt);

        /// <summary>
        /// OFMAS State Plan scheme panchayat data.
        /// </summary>
        /// <param name="dt">dt</param>
        /// <returns>InsertOFMASStatePlanPan</returns>
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
        int InsertNHM_Data(DataTable dt, string schemecode);
    }
}
