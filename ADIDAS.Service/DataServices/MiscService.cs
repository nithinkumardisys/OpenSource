//------------------------------------------------------------------------------
// <copyright file="MiscService.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------

namespace ADIDAS.Service.DataServices
{
    using System.Collections.Generic;
    using System.Data;
    using ADIDAS.Model.DTO;
    using ADIDAS.Model.Entities;
    using ADIDAS.Repository.Interfaces;
    using ADIDAS.Service.Interfaces;
    using DepartmentOfAgriculture.Admin.Models.Models;
    using DIDAS.Model.Entities;

    public class MiscService : IMiscService
    {
        private readonly IMiscRepository miscRepository;

        public MiscService(IMiscRepository miscRepository)
        {
            this.miscRepository = miscRepository;
        }

        public List<AppReport> GetReports()
        {
            return this.miscRepository.GetReports();
        }

        public List<MobileAttributeConfig> GetSource(string attributeName)
        {
            return this.miscRepository.GetSource(attributeName);
        }

        public List<NotificationEntity> GetNotificationAudits()
        {
            return this.miscRepository.GetNotificationAudits();
        }

        public int InsertNotificationAudits(NotificationEntity entity)
        {
            return this.miscRepository.InsertNotificationAudits(entity);
        }

        public int DeleteNotification(int iD)
        {
            return this.miscRepository.DeleteNotification(iD);
        }

        public int DeleteAllNotification()
        {
            return this.miscRepository.DeleteALLNotifications();
        }

        public int InsertDistrictDim(List<DistrictLG> districtLGs)
        {
            return this.miscRepository.InsertDistrictDim(districtLGs);
        }

        public int InsecticideListAsync(List<InsecticideLicenceModel> insecticideLicenceModels)
        {
            return this.miscRepository.InsecticideListAsync(insecticideLicenceModels);
        }

        public int FarmNameListAsync(List<FarmNameModel> farmNameModels)
        {
            return this.miscRepository.FarmNameListAsync(farmNameModels);
        }

        public int InsertBlockDim(List<BlockLG> blockLG)
        {
            return this.miscRepository.InsertBlockDim(blockLG);
        }

        public int InsertPanchayatDim(List<PanchayatLG> panchayatLG)
        {
            return this.miscRepository.InsertPanchayatDim(panchayatLG);
        }

        public int InsertVillageDim(List<VillageLG> villageLG)
        {
            return this.miscRepository.InsertVillageDim(villageLG);
        }

        public int PostSource(AppConfig appConfig)
        {
            return this.miscRepository.PostSource(appConfig);
        }

        public int InsertScheme(List<Scheme> schemes)
        {
            return this.miscRepository.InsertScheme(schemes);
        }

        public int InsertbrbnApplication(List<BrbnApplication> app)
        {
            return this.miscRepository.InsertbrbnApplication(app);
        }

        public List<AppLink> GetAppLink()
        {
            return this.miscRepository.GetAppLink();
        }

        public Release GetReleaseData()
        {
            return this.miscRepository.GetReleaseData();
        }

        public dynamic GetDataFAQ()
        {
            return this.miscRepository.GetDataFAQ();
        }

        public List<BihanGuidelineModel> GetBihanGuidlines()
        {
            return this.miscRepository.GetBihanGuidlines();
        }

        public List<PanchayatData> GetPanchayatdim()
        {
            return this.miscRepository.GetPanchayatdim();
        }

        public int InsertSubsidyStatus(DataTable dt)
        {
            return this.miscRepository.InsertSubsidyStatus(dt);
        }

        public int InsertSubsidyReport(dynamic subsidyReport)
        {
            return this.miscRepository.InsertSubsidyReport(subsidyReport);
        }

        public List<District> GetdistrictCode()
        {
            return this.miscRepository.GetdistrictCode();
        }

        public List<OfmasScheme> GetOfmasSchemes()
        {
            return this.miscRepository.GetOfmasSchemes();
        }

        public List<PanchayatData> GetOfmasBlock()
        {
            return this.miscRepository.GetOfmasBlock();
        }

        public int InsertFarmerInfo(DataTable dtCast, DataTable dtGender, DataTable dtType)
        {
            return this.miscRepository.InsertFarmerInfo(dtCast, dtGender, dtType);
        }

        public List<MobileAttributeConfig> GetAllDesignations()
        {
            return this.miscRepository.GetAllDesignations();
        }

        public List<MobileAttributeConfig> GetReceipientDesignations()
        {
            return this.miscRepository.GetReceipientDesignations();
        }

        public List<SeasonInfo> GetSeasonInfo(string season)
        {
            return this.miscRepository.GetSeasonInfo(season);
        }

        public List<FarmerInfo> GetFarmerInfo(string mobileno)
        {
            return this.miscRepository.GetFarmerInfo(mobileno);
        }

        public List<PesticideInfo> GetPesticideLicenseInfo()
        {
            return this.miscRepository.GetPesticideLicenseInfo();
        }

        public List<DoaInstructions> GetDOAInstructions()
        {
            return this.miscRepository.GetDOAInstructions();
        }

        public int InsertOFMASScheme(DataTable dt)
        {
            return this.miscRepository.InsertOFMASScheme(dt);
        }

        public int InsertFarmer(DataTable dt)
        {
            return this.miscRepository.InsertFarmer(dt);
        }

        public int InsertOFMASAMRTDist(DataTable dt)
        {
            return this.miscRepository.InsertOFMASAMRTDist(dt);
        }

        public int InsertOFMASAMRTBlk(DataTable dt)
        {
            return this.miscRepository.InsertOFMASAMRTBlk(dt);
        }

        public int InsertOFMASAMRTPan(DataTable dt)
        {
            return this.miscRepository.InsertOFMASAMRTPan(dt);
        }

        public int InsertOFMASKKADist(DataTable dt)
        {
            return this.miscRepository.InsertOFMASKKADist(dt);
        }

        public int InsertOFMASBGREIDist(DataTable dt)
        {
            return this.miscRepository.InsertOFMASBGREIDist(dt);
        }

        public int InsertOFMASBGREIBlk(DataTable dt)
        {
            return this.miscRepository.InsertOFMASBGREIBlk(dt);
        }

        public int InsertOFMASBGREIPAN(DataTable dt)
        {
            return this.miscRepository.InsertOFMASBGREIPAN(dt);
        }

        public int InsertOFMASNFSMDist(DataTable dt)
        {
            return this.miscRepository.InsertOFMASNFSMDist(dt);
        }

        public int InsertOFMASNFSMBlk(DataTable dt)
        {
            return this.miscRepository.InsertOFMASNFSMBlk(dt);
        }

        public int InsertOFMASNFSMPAN(DataTable dt)
        {
            return this.miscRepository.InsertOFMASNFSMPAN(dt);
        }

        public int InsertOFMASNFSMOILSEEDSDist(DataTable dt)
        {
            return this.miscRepository.InsertOFMASNFSMOILSEEDSDist(dt);
        }

        public int InsertOFMASNFSMOILSEEDSBlk(DataTable dt)
        {
            return this.miscRepository.InsertOFMASNFSMOILSEEDSBlk(dt);
        }

        public int InsertOFMASNFSMOILSEEDSPAN(DataTable dt)
        {
            return this.miscRepository.InsertOFMASNFSMOILSEEDSPAN(dt);
        }

        public int InsertOFMASSMAMDist(DataTable dt)
        {
            return this.miscRepository.InsertOFMASSMAMDist(dt);
        }

        public int InsertOFMASSMAMBlk(DataTable dt)
        {
            return this.miscRepository.InsertOFMASSMAMBlk(dt);
        }

        public int InsertOFMASSMAMPAN(DataTable dt)
        {
            return this.miscRepository.InsertOFMASSMAMPAN(dt);
        }

        public int InsertOFMASSMAMSCHCDist(DataTable dt)
        {
            return this.miscRepository.InsertOFMASSMAMSCHCDist(dt);
        }

        public int InsertOFMASSMAMSCHCBlk(DataTable dt)
        {
            return this.miscRepository.InsertOFMASSMAMSCHCBlk(dt);
        }

        public int InsertOFMASSMAMSCHCPAN(DataTable dt)
        {
            return this.miscRepository.InsertOFMASSMAMSCHCPAN(dt);
        }

        public int InsertOFMAKKABlk(DataTable dt)
        {
            return this.miscRepository.InsertOFMAKKABlk(dt);
        }

        public int InsertOFMAKKAPANk(DataTable dt)
        {
            return this.miscRepository.InsertOFMAKKAPANk(dt);
        }

        public int InsertMIDetails(DataTable dt)
        {
            return this.miscRepository.InsertMIDetails(dt);
        }

        public int PostGamificationConfig(GamificationConfigDto config)
        {
            return this.miscRepository.PostGamificationConfig(config);
        }

        public int InsertParaliDetails(DataTable dt)
        {
            return this.miscRepository.InsertParaliDetails(dt);
        }

        public int InsertSoilHealth(DataTable dt)
        {
            return this.miscRepository.InsertSoilHealth(dt);
        }

        public int InsertHortiHybridSeedDetails(dynamic details)
        {
            return this.miscRepository.InsertHortiHybridSeedDetails(details);
        }

        public int Insertbassoca(DataTable dt)
        {
            return this.miscRepository.Insertbassoca(dt);
        }

        /// <summary>
        /// OFMAS State Plan scheme panchayat data.
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public int InsertOFMASStatePlanPan(DataTable dt)
        {
            return this.miscRepository.InsertOFMASStatePlanPan(dt);
        }

        /// <summary>
        /// InsertLicenseHolder.
        /// </summary>
        /// <param name="licenseHolders">licenseHolders.</param>
        /// <param name="queryName">queryName.</param>
        /// <returns>result integer.</returns>
        public int InsertLicenseHolder(List<InsecticideLicenceModel> licenseHolders, string queryName)
        {
            return this.miscRepository.InsertLicenseHolder(licenseHolders, queryName);
        }

        /// <summary>
        /// NHM data.
        /// </summary>
        /// <param name="dt">dt.</param>
        /// <param name="schemecode">schemecode.</param>
        /// <returns>int.</returns>
        public int InsertNHMData(DataTable dt, string schemecode)
        {
            return this.miscRepository.InsertNHM_Data(dt, schemecode);
        }
    }
}
