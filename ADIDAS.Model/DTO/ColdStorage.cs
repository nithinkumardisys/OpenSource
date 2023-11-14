//------------------------------------------------------------------------------
// <copyright file="ColdStorage.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------
namespace ADIDAS.Model.DTO
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Cold Storage.
    /// </summary>
    public class ColdStorage
    {
        /// <summary>
        /// Gets or Sets Crop_Id.
        /// </summary>
        public int? Crop_Id { get; set; }

        /// <summary>
        /// Gets or Sets District_Id.
        /// </summary>
        public int? District_Id { get; set; }

        /// <summary>
        /// Gets or Sets Crop_name.
        /// </summary>
        public string Crop_name { get; set; }

        /// <summary>
        /// Gets or Sets Strg_ID.
        /// </summary>
        public int Strg_ID { get; set; }

        /// <summary>
        /// Gets or Sets Week_open_bal.
        /// </summary>
        public decimal? Week_open_bal { get; set; }

        /// <summary>
        /// Gets or Sets Curr_open_bal.
        /// </summary>
        public decimal? Curr_open_bal { get; set; }

        /// <summary>
        /// Gets or Sets Deposit.
        /// </summary>
        public decimal? Deposit { get; set; }

        /// <summary>
        /// Gets or Sets Release.
        /// </summary>
        public decimal? Release { get; set; }

        /// <summary>
        /// Gets or Sets Submitted_date.
        /// </summary>
        public DateTime? Submitted_date { get; set; }

        /// <summary>
        /// Gets or Sets Submitted_userid.
        /// </summary>
        public int? Submitted_userid { get; set; }
    }

    /// <summary>
    /// Cold Storage Details.
    /// </summary>
    public class ColdStorageDetails
    {
        /// <summary>
        /// Gets or Sets District_Id.
        /// </summary>
        public int? District_Id { get; set; }

        /// <summary>
        /// Gets or Sets District_Name.
        /// </summary>
        public string District_Name { get; set; }

        /// <summary>
        /// Gets or Sets Strg_Nos.
        /// </summary>
        public int? Strg_Nos { get; set; }

        /// <summary>
        /// Gets or Sets Strg_Name.
        /// </summary>
        public string Strg_Name { get; set; }

        /// <summary>
        /// Gets or Sets Strg_Address.
        /// </summary>
        public string Strg_Address { get; set; }

        /// <summary>
        /// Gets or Sets License_no.
        /// </summary>
        public string License_no { get; set; }

        /// <summary>
        /// Gets or Sets Capacity_mt.
        /// </summary>
        public decimal? Capacity_mt { get; set; }

        /// <summary>
        /// Gets or Sets Rec_created_ts.
        /// </summary>
        public DateTime? Rec_created_ts { get; set; }

        /// <summary>
        /// Gets or Sets Rec_created_userid.
        /// </summary>
        public int? Rec_created_userid { get; set; }
    }

    /// <summary>
    /// Cold Storage Request.
    /// </summary>
    public class ColdStorageRequest
    {
        /// <summary>
        /// Gets or Sets Query_name.
        /// </summary>
        public string Query_name { get; set; }

        /// <summary>
        /// Gets or Sets Crop_id.
        /// </summary>
        public int? Crop_id { get; set; }

        /// <summary>
        /// Gets or Sets District_Id.
        /// </summary>
        public int? District_Id { get; set; }

        /// <summary>
        /// Gets or Sets Crop_name.
        /// </summary>
        public string Crop_name { get; set; }

        /// <summary>
        /// Gets or Sets Strg_ID.
        /// </summary>
        public int Strg_ID { get; set; }
    }

    /// <summary>
    /// Get All ColdStorage.
    /// </summary>
    public class GetAllColdStorage
    {
        /// <summary>
        /// Gets or Sets Strg_ID.
        /// </summary>
        public int Strg_ID { get; set; }

        /// <summary>
        /// Gets or Sets District_Name.
        /// </summary>
        public string District_Name { get; set; }

        /// <summary>
        /// Gets or Sets District_Id.
        /// </summary>
        public int District_Id { get; set; }

        /// <summary>
        /// Gets or Sets Stor_Name_Address.
        /// </summary>
        public string Stor_Name_Address { get; set; }

        /// <summary>
        /// Gets or Sets Stor_Name.
        /// </summary>
        public string Stor_Name { get; set; }

        /// <summary>
        /// Gets or Sets Stor_Address.
        /// </summary>
        public string Stor_Address { get; set; }

        /// <summary>
        /// Gets or Sets License_no.
        /// </summary>
        public string License_no { get; set; }

        /// <summary>
        /// Gets or Sets Capacity_mt.
        /// </summary>
        public decimal Capacity_mt { get; set; }

        /// <summary>
        /// Gets or Sets Rec_created_ts.
        /// </summary>
        public DateTime Rec_created_ts { get; set; }

        /// <summary>
        /// Gets or Sets Rec_created_userid.
        /// </summary>
        public int Rec_created_userid { get; set; }

        /// <summary>
        /// Gets or Sets Rec_updated_ts.
        /// </summary>
        public DateTime Rec_updated_ts { get; set; }

        /// <summary>
        /// Gets or Sets Rec_updated_userid.
        /// </summary>
        public int Rec_updated_userid { get; set; }
    }

    /// <summary>
    /// Get All Crop ColdStorage.
    /// </summary>
    public class GetAllCropColdStorage
    {
        /// <summary>
        /// Gets or Sets Crop_id.
        /// </summary>
        public int Crop_id { get; set; }

        /// <summary>
        /// Gets or Sets Crop_category.
        /// </summary>
        public string Crop_category { get; set; }

        /// <summary>
        /// Gets or Sets Crop_name.
        /// </summary>
        public string Crop_name { get; set; }

        /// <summary>
        /// Gets or Sets Variety.
        /// </summary>
        public int Variety { get; set; }

        /// <summary>
        /// Gets or Sets Rec_created_date.
        /// </summary>
        public DateTime Rec_created_date { get; set; }

        /// <summary>
        /// Gets or Sets Rec_created_userid.
        /// </summary>
        public string Rec_created_userid { get; set; }

        /// <summary>
        /// Gets or Sets Rec_updated_date.
        /// </summary>
        public DateTime Rec_updated_date { get; set; }

        /// <summary>
        /// Gets or Sets Rec_updated_userid.
        /// </summary>
        public string Rec_updated_userid { get; set; }
    }

    /// <summary>
    /// Get All Crop ColdStorageId.
    /// </summary>
    public class GetAllCropColdStorageId
    {
        /// <summary>
        /// Gets or Sets Strg_ID.
        /// </summary>
        public int Strg_ID { get; set; }

        /// <summary>
        /// Gets or Sets District_Name.
        /// </summary>
        public string District_Name { get; set; }

        /// <summary>
        /// Gets or Sets District_ID.
        /// </summary>
        public int District_ID { get; set; }

        /// <summary>
        /// Gets or Sets Stor_Name_Address.
        /// </summary>
        public string Stor_Name_Address { get; set; }

        /// <summary>
        /// Gets or Sets License_no.
        /// </summary>
        public string License_no { get; set; }

        /// <summary>
        /// Gets or Sets Capacity_mt.
        /// </summary>
        public decimal Capacity_mt { get; set; }

        /// <summary>
        /// Gets or Sets Rec_created_ts.
        /// </summary>
        public DateTime Rec_created_ts { get; set; }

        /// <summary>
        /// Gets or Sets Rec_created_userid.
        /// </summary>
        public int Rec_created_userid { get; set; }

        /// <summary>
        /// Gets or Sets Rec_updated_ts.
        /// </summary>
        public DateTime Rec_updated_ts { get; set; }

        /// <summary>
        /// Gets or Sets Rec_updated_userid.
        /// </summary>
        public int Rec_updated_userid { get; set; }

        /// <summary>
        /// Gets or Sets Crop_id.
        /// </summary>
        public int Crop_id { get; set; }

        /// <summary>
        /// Gets or Sets Strg_ID1.
        /// </summary>
        public int Strg_ID1 { get; set; }

        /// <summary>
        /// Gets or Sets District_id1.
        /// </summary>
        public int District_id1 { get; set; }

        /// <summary>
        /// Gets or Sets Week_open_bal.
        /// </summary>
        public decimal Week_open_bal { get; set; }

        /// <summary>
        /// Gets or Sets Curr_open_bal.
        /// </summary>
        public decimal Curr_open_bal { get; set; }

        /// <summary>
        /// Gets or Sets Deposit.
        /// </summary>
        public decimal Deposit { get; set; }

        /// <summary>
        /// Gets or Sets Release.
        /// </summary>
        public decimal Release { get; set; }

        /// <summary>
        /// Gets or Sets Curr_bal.
        /// </summary>
        public decimal Curr_bal { get; set; }

        /// <summary>
        /// Gets or Sets Submitted_userid.
        /// </summary>
        public int Submitted_userid { get; set; }

        /// <summary>
        /// Gets or Sets Submitted_date.
        /// </summary>
        public DateTime Submitted_date { get; set; }

        /// <summary>
        /// Gets or Sets Crop_name.
        /// </summary>
        public string Crop_name { get; set; }

        /// <summary>
        /// Gets or Sets Crop_type.
        /// </summary>
        public string Crop_type { get; set; }

        /// <summary>
        /// Gets or Sets Crop_category.
        /// </summary>
        public string Crop_category { get; set; }

        /// <summary>
        /// Gets or Sets Rec_updated_date.
        /// </summary>
        public DateTime Rec_updated_date { get; set; }

        /// <summary>
        /// Gets or Sets Rec_updated_userid1.
        /// </summary>
        public int Rec_updated_userid1 { get; set; }
    }

    /// <summary>
    /// Last ColdStorage.
    /// </summary>
    public class LstCpldStorage
    {
        /// <summary>
        /// Gets or Sets Week_nm.
        /// </summary>
        public int Week_nm { get; set; }

        /// <summary>
        /// Gets or Sets Week_Start.
        /// </summary>
        public DateTime Week_Start { get; set; }

        /// <summary>
        /// Gets or Sets Week_end.
        /// </summary>
        public DateTime Week_end { get; set; }

        /// <summary>
        /// Gets or Sets Strg_ID.
        /// </summary>
        public int Strg_ID { get; set; }

        /// <summary>
        /// Gets or Sets Stor_Name_Address.
        /// </summary>
        public string Stor_Name_Address { get; set; }

        /// <summary>
        /// Gets or Sets District_Name.
        /// </summary>
        public string District_Name { get; set; }

        /// <summary>
        /// Gets or Sets Crop_id.
        /// </summary>
        public int Crop_id { get; set; }

        /// <summary>
        /// Gets or Sets Crop_name.
        /// </summary>
        public string Crop_name { get; set; }

        /// <summary>
        /// Gets or Sets Crop_category.
        /// </summary>
        public string Crop_category { get; set; }

        /// <summary>
        /// Gets or Sets District_Id.
        /// </summary>
        public int District_Id { get; set; }

        /// <summary>
        /// Gets or Sets Agg_Week_Open_Bal.
        /// </summary>
        public decimal Agg_Week_Open_Bal { get; set; }

        /// <summary>
        /// Gets or Sets Agg_Curr_Open_Bal.
        /// </summary>
        public decimal Agg_Curr_Open_Bal { get; set; }

        /// <summary>
        /// Gets or Sets Agg_Deposit.
        /// </summary>
        public decimal Agg_Deposit { get; set; }

        /// <summary>
        /// Gets or Sets Agg_Release.
        /// </summary>
        public decimal Agg_Release { get; set; }

        /// <summary>
        /// Gets or Sets Agg_Curr_Balance.
        /// </summary>
        public decimal Agg_Curr_Balance { get; set; }

        /// <summary>
        /// Gets or Sets Capacity_mt.
        /// </summary>
        public decimal Capacity_mt { get; set; }

        /// <summary>
        /// Gets or Sets Rec_created_ts.
        /// </summary>
        public DateTime Rec_created_ts { get; set; }
    }

    /// <summary>
    /// Last District ColdStorage.
    /// </summary>
    public class LstDistCpldStorage
    {
        /// <summary>
        /// Gets or Sets Week_nm.
        /// </summary>
        public int Week_nm { get; set; }

        /// <summary>
        /// Gets or Sets Week_Start.
        /// </summary>
        public DateTime Week_Start { get; set; }

        /// <summary>
        /// Gets or Sets Week_end.
        /// </summary>
        public DateTime Week_end { get; set; }
    }

    /// <summary>
    /// Last District ColdStorage details.
    /// </summary>
    public class LstCpldStorageDets
    {
        /// <summary>
        /// Gets or Sets Week_nm.
        /// </summary>
        public int Week_nm { get; set; }

        /// <summary>
        /// Gets or Sets Week_Start.
        /// </summary>
        public DateTime Week_Start { get; set; }

        /// <summary>
        /// Gets or Sets Week_end.
        /// </summary>
        public DateTime Week_end { get; set; }

        /// <summary>
        /// Gets or Sets District_Id.
        /// </summary>
        public int District_Id { get; set; }

        /// <summary>
        /// Gets or Sets Strg_list.
        /// </summary>
        public List<StorageDet> Strg_list { get; set; }
    }

    /// <summary>
    /// Storage details.
    /// </summary>
    public class StorageDet
    {
        /// <summary>
        /// Gets or Sets Strg_ID.
        /// </summary>
        public int Strg_ID { get; set; }

        /// <summary>
        /// Gets or Sets StorageName.
        /// </summary>
        public string StorageName { get; set; }

        /// <summary>
        /// Gets or Sets StorageAddress.
        /// </summary>
        public string StorageAddress { get; set; }

        /// <summary>
        /// Gets or Sets District_Name.
        /// </summary>
        public string District_Name { get; set; }

        /// <summary>
        /// Gets or Sets Crop_list.
        /// </summary>
        public List<CroplstDet> Crop_list { get; set; }
    }

    /// <summary>
    /// Last crop details.
    /// </summary>
    public class CroplstDet
    {
        /// <summary>
        /// Gets or Sets Crop_id.
        /// </summary>
        public int Crop_id { get; set; }

        /// <summary>
        /// Gets or Sets Crop_name.
        /// </summary>
        public string Crop_name { get; set; }

        /// <summary>
        /// Gets or Sets Crop_category.
        /// </summary>
        public string Crop_category { get; set; }

        /// <summary>
        /// Gets or Sets Agg_Week_Open_Bal.
        /// </summary>
        public decimal Agg_Week_Open_Bal { get; set; }

        /// <summary>
        /// Gets or Sets Agg_Curr_Open_Bal.
        /// </summary>
        public decimal Agg_Curr_Open_Bal { get; set; }

        /// <summary>
        /// Gets or Sets Agg_Deposit.
        /// </summary>
        public decimal Agg_Deposit { get; set; }

        /// <summary>
        /// Gets or Sets Agg_Release.
        /// </summary>
        public decimal Agg_Release { get; set; }

        /// <summary>
        /// Gets or Sets Agg_Curr_Balance.
        /// </summary>
        public decimal Agg_Curr_Balance { get; set; }

        /// <summary>
        /// Gets or Sets Capacity_mt.
        /// </summary>
        public decimal Capacity_mt { get; set; }

        /// <summary>
        /// Gets or Sets SubmittedDate.
        /// </summary>
        public DateTime SubmittedDate { get; set; }
    }

    /// <summary>
    /// Last ColdStorage details.
    /// </summary>
    public class LstCpldStorageDet
    {
        /// <summary>
        /// Gets or Sets Week_nm.
        /// </summary>
        public int Week_nm { get; set; }

        /// <summary>
        /// Gets or Sets Strg_ID.
        /// </summary>
        public int Strg_ID { get; set; }

        /// <summary>
        /// Gets or Sets District_Name.
        /// </summary>
        public string District_Name { get; set; }

        /// <summary>
        /// Gets or Sets Crop_id.
        /// </summary>
        public int Crop_id { get; set; }

        /// <summary>
        /// Gets or Sets Crop_name.
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Gets or Sets District_Id.
        /// </summary>
        public int District_Id { get; set; }

        /// <summary>
        /// Gets or Sets Agg_Week_Open_Bal.
        /// </summary>
        public decimal Agg_Week_Open_Bal { get; set; }

        /// <summary>
        /// Gets or Sets Agg_Curr_Open_Bal.
        /// </summary>
        public decimal Agg_Curr_Open_Bal { get; set; }

        /// <summary>
        /// Gets or Sets Agg_Deposit.
        /// </summary>
        public decimal Agg_Deposit { get; set; }

        /// <summary>
        /// Gets or Sets Agg_Release.
        /// </summary>
        public decimal Agg_Release { get; set; }

        /// <summary>
        /// Gets or Sets Agg_Curr_Balance.
        /// </summary>
        public decimal Agg_Curr_Balance { get; set; }

        /// <summary>
        /// Gets or Sets Capacity_mt.
        /// </summary>
        public decimal Capacity_mt { get; set; }

        /// <summary>
        /// Gets or Sets SubmittedDate.
        /// </summary>
        public DateTime SubmittedDate { get; set; }

        /// <summary>
        /// Gets or Sets StorageName.
        /// </summary>
        public string StorageName { get; set; }
    }

    /// <summary>
    /// Hort Agg Crop Coverage Actual.
    /// </summary>
    public class HortAggCropCoverageActual
    {
        /// <summary>
        /// Gets or Sets Reported_date.
        /// </summary>
        public DateTime Reported_date { get; set; }

        /// <summary>
        /// Gets or Sets Panchayat_id.
        /// </summary>
        public int Panchayat_id { get; set; }

        /// <summary>
        /// Gets or Sets Season_id.
        /// </summary>
        public int Season_id { get; set; }

        /// <summary>
        /// Gets or Sets Crop_id.
        /// </summary>
        public int Crop_id { get; set; }
    }

    /// <summary>
    /// PhmsStructure.
    /// </summary>
    public class PhmsStructure
    {
        /// <summary>
        /// Gets or Sets DistrictId.
        /// </summary>
        public int DistrictId { get; set; }

        /// <summary>
        /// Gets or Sets StructType.
        /// </summary>
        public string StructType { get; set; }

        /// <summary>
        /// Gets or Sets UserId.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or Sets Is_Name_Mandatory_flg.
        /// </summary>
        public string Is_Name_Mandatory_flg { get; set; }

        /// <summary>
        /// Gets or Sets Is_Addr_Mandatory_flg.
        /// </summary>
        public string Is_Addr_Mandatory_flg { get; set; }

        /// <summary>
        /// Gets or Sets Is_Capacity_Mandatory_flg.
        /// </summary>
        public string Is_Capacity_Mandatory_flg { get; set; }

        /// <summary>
        /// Gets or Sets Unit_of_measure.
        /// </summary>
        public string Unit_of_measure { get; set; }
    }

    /// <summary>
    /// Get Structure.
    /// </summary>
    public class GetStructure
    {
        /// <summary>
        /// Gets or Sets Struct_id.
        /// </summary>
        public int Struct_id { get; set; }

        /// <summary>
        /// Gets or Sets District_id.
        /// </summary>
        public int District_id { get; set; }

        /// <summary>
        /// Gets or Sets Struct_type.
        /// </summary>
        public string Struct_type { get; set; }

        /// <summary>
        /// Gets or Sets Structure_desc.
        /// </summary>
        public string Structure_desc { get; set; }

        /// <summary>
        /// Gets or Sets Is_Name_Mandatory_flg.
        /// </summary>
        public string Is_Name_Mandatory_flg { get; set; }

        /// <summary>
        /// Gets or Sets Is_Addr_Mandatory_flg.
        /// </summary>
        public string Is_Addr_Mandatory_flg { get; set; }

        /// <summary>
        /// Gets or Sets Is_Capacity_Mandatory_flg.
        /// </summary>
        public string Is_Capacity_Mandatory_flg { get; set; }

        /// <summary>
        /// Gets or Sets Unit_of_measure.
        /// </summary>
        public string Unit_of_measure { get; set; }
    }

    /// <summary>
    /// Hort NewCrop.
    /// </summary>
    public class HortNewCrop
    {
        /// <summary>
        /// Gets or Sets DistrictId.
        /// </summary>
        public int DistrictId { get; set; }

        /// <summary>
        /// Gets or Sets CropId.
        /// </summary>
        public int CropId { get; set; }

        /// <summary>
        /// Gets or Sets SeasonId.
        /// </summary>
        public int SeasonId { get; set; }

        /// <summary>
        /// Gets or Sets CropName.
        /// </summary>
        public string CropName { get; set; }

        /// <summary>
        /// Gets or Sets CropCategory.
        /// </summary>
        public string CropCategory { get; set; }

        /// <summary>
        /// Gets or Sets UnitOfMeasure.
        /// </summary>
        public string UnitOfMeasure { get; set; }
    }

    /// <summary>
    /// Distinct Horticulture Crop.
    /// </summary>
    public class DistinctHorticultureCrop
    {
        /// <summary>
        /// Gets or Sets Crop_id.
        /// </summary>
        public int Crop_id { get; set; }

        /// <summary>
        /// Gets or Sets Crop_category.
        /// </summary>
        public string Crop_category { get; set; }

        /// <summary>
        /// Gets or Sets Crop_name.
        /// </summary>
        public string Crop_name { get; set; }
    }

    /// <summary>
    /// Hort Produce Season.
    /// </summary>
    public class HortProduceSeason
    {
        /// <summary>
        /// Gets or Sets Season_id.
        /// </summary>
        public int Season_id { get; set; }

        /// <summary>
        /// Gets or Sets District_Id.
        /// </summary>
        public int District_Id { get; set; }

        /// <summary>
        /// Gets or Sets Crop_Id.
        /// </summary>
        public int Crop_Id { get; set; }

        /// <summary>
        /// Gets or Sets Block_Id.
        /// </summary>
        public int Block_Id { get; set; }

        /// <summary>
        /// Gets or Sets Panchayat_Id.
        /// </summary>
        public int Panchayat_Id { get; set; }
    }

    /// <summary>
    /// Hort Produce Season Response.
    /// </summary>
    public class HortProduceSeasonResponse
    {
        /// <summary>
        /// Gets or Sets Season_id.
        /// </summary>
        public int Season_id { get; set; }

        /// <summary>
        /// Gets or Sets Season_name.
        /// </summary>
        public string Season_name { get; set; }

        /// <summary>
        /// Gets or Sets Season_year.
        /// </summary>
        public string Season_year { get; set; }

        /// <summary>
        /// Gets or Sets Start_date.
        /// </summary>
        public DateTime Start_date { get; set; }

        /// <summary>
        /// Gets or Sets End_date.
        /// </summary>
        public DateTime End_date { get; set; }

        /// <summary>
        /// Gets or Sets Rec_created_userid.
        /// </summary>
        public string Rec_created_userid { get; set; }

        /// <summary>
        /// Gets or Sets Rec_created_date.
        /// </summary>
        public DateTime Rec_created_date { get; set; }

        /// <summary>
        /// Gets or Sets Rec_updated_userid.
        /// </summary>
        public string Rec_updated_userid { get; set; }

        /// <summary>
        /// Gets or Sets Rec_updated_date.
        /// </summary>
        public DateTime Rec_updated_date { get; set; }

        /// <summary>
        /// Gets or Sets Seasonname_yy.
        /// </summary>
        public string Seasonname_yy { get; set; }
    }

    /// <summary>
    /// Hort Produce Panchayat Response.
    /// </summary>
    public class HortProducePanchayatResponse
    {
        /// <summary>
        /// Gets or Sets District_id.
        /// </summary>
        public int District_id { get; set; }

        /// <summary>
        /// Gets or Sets District_name.
        /// </summary>
        public string District_name { get; set; }

        /// <summary>
        /// Gets or Sets Block_id.
        /// </summary>
        public int Block_id { get; set; }

        /// <summary>
        /// Gets or Sets Block_name.
        /// </summary>
        public string Block_name { get; set; }

        /// <summary>
        /// Gets or Sets Panchayat_id.
        /// </summary>
        public int Panchayat_id { get; set; }

        /// <summary>
        /// Gets or Sets Panchayat_name.
        /// </summary>
        public string Panchayat_name { get; set; }

        /// <summary>
        /// Gets or Sets Panchayat_agg_flag.
        /// </summary>
        public string Panchayat_agg_flag { get; set; }
    }

    /// <summary>
    /// Hort Produce Tran Approval.
    /// </summary>
    public class HortProduceTranApproval
    {
        /// <summary>
        /// Gets or Sets Reported_date.
        /// </summary>
        public DateTime Reported_date { get; set; }

        /// <summary>
        /// Gets or Sets Season_id.
        /// </summary>
        public int Season_id { get; set; }

        /// <summary>
        /// Gets or Sets Panchayat_id.
        /// </summary>
        public int Panchayat_id { get; set; }

        /// <summary>
        /// Gets or Sets District_id.
        /// </summary>
        public int District_id { get; set; }

        /// <summary>
        /// Gets or Sets Block_id.
        /// </summary>
        public int Block_id { get; set; }

        /// <summary>
        /// Gets or Sets Crop_id.
        /// </summary>
        public int Crop_id { get; set; }

        /// <summary>
        /// Gets or Sets Produce_Curr.
        /// </summary>
        public int Produce_Curr { get; set; }

        /// <summary>
        /// Gets or Sets Produce_prev.
        /// </summary>
        public int Produce_prev { get; set; }

        /// <summary>
        /// Gets or Sets ADH_approval_flag.
        /// </summary>
        public string ADH_approval_flag { get; set; }

        /// <summary>
        /// Gets or Sets ADH_approval_reason.
        /// </summary>
        public string ADH_approval_reason { get; set; }

        /// <summary>
        /// Gets or Sets ADH_approved_userid.
        /// </summary>
        public int ADH_approved_userid { get; set; }

        /// <summary>
        /// Gets or Sets ADH_approved_date.
        /// </summary>
        public DateTime ADH_approved_date { get; set; }

        /// <summary>
        /// Gets or Sets Bho_approval_flag.
        /// </summary>
        public string Bho_approval_flag { get; set; }

        /// <summary>
        /// Gets or Sets Bho_approval_reason.
        /// </summary>
        public string Bho_approval_reason { get; set; }

        /// <summary>
        /// Gets or Sets Bho_approved_userid.
        /// </summary>
        public int Bho_approved_userid { get; set; }

        /// <summary>
        /// Gets or Sets Bho_approved_date.
        /// </summary>
        public DateTime Bho_approved_date { get; set; }

        /// <summary>
        /// Gets or Sets Ac_submitted_userid.
        /// </summary>
        public int Ac_submitted_userid { get; set; }

        /// <summary>
        /// Gets or Sets Ac_submitted_date.
        /// </summary>
        public DateTime Ac_submitted_date { get; set; }

        /// <summary>
        /// Gets or Sets Submission_source.
        /// </summary>
        public string Submission_source { get; set; }
    }

    /// <summary>
    /// Hort Produce Tran.
    /// </summary>
    public class HortProduceTran
    {
        /// <summary>
        /// Gets or Sets Reported_date.
        /// </summary>
        public DateTime Reported_date { get; set; }

        /// <summary>
        /// Gets or Sets Week_nm.
        /// </summary>
        public int Week_nm { get; set; }

        /// <summary>
        /// Gets or Sets Season_id.
        /// </summary>
        public int Season_id { get; set; }

        /// <summary>
        /// Gets or Sets Crop_id.
        /// </summary>
        public int Crop_id { get; set; }

        /// <summary>
        /// Gets or Sets District_Id.
        /// </summary>
        public int District_Id { get; set; }

        /// <summary>
        /// Gets or Sets Block_Id.
        /// </summary>
        public int Block_Id { get; set; }

        /// <summary>
        /// Gets or Sets Panchayat_Id.
        /// </summary>
        public int Panchayat_Id { get; set; }

        /// <summary>
        /// Gets or Sets Produce_Prev.
        /// </summary>
        public int Produce_Prev { get; set; }

        /// <summary>
        /// Gets or Sets Produce_Curr.
        /// </summary>
        public int Produce_Curr { get; set; }

        /// <summary>
        /// Gets or Sets Produce_tot.
        /// </summary>
        public int Produce_tot { get; set; }

        /// <summary>
        /// Gets or Sets Rec_created_userid.
        /// </summary>
        public int Rec_created_userid { get; set; }

        /// <summary>
        /// Gets or Sets Rec_created_date.
        /// </summary>
        public DateTime Rec_created_date { get; set; }

        /// <summary>
        /// Gets or Sets Rec_updated_userid.
        /// </summary>
        public int Rec_updated_userid { get; set; }

        /// <summary>
        /// Gets or Sets Rec_updated_date.
        /// </summary>
        public DateTime Rec_updated_date { get; set; }

        /// <summary>
        /// Gets or Sets Approval_reason.
        /// </summary>
        public string Approval_reason { get; set; }

        /// <summary>
        /// Gets or Sets AC_Submitted_date.
        /// </summary>
        public DateTime AC_Submitted_date { get; set; }

        /// <summary>
        /// Gets or Sets AC_Submitted_userid.
        /// </summary>
        public int AC_Submitted_userid { get; set; }

        /// <summary>
        /// Gets or Sets BHO_Approval_flag.
        /// </summary>
        public string BHO_Approval_flag { get; set; }

        /// <summary>
        /// Gets or Sets BHO_Approved_date.
        /// </summary>
        public DateTime BHO_Approved_date { get; set; }

        /// <summary>
        /// Gets or Sets BHO_Approved_userid.
        /// </summary>
        public int BHO_Approved_userid { get; set; }

        /// <summary>
        /// Gets or Sets ADH_Approval_flag.
        /// </summary>
        public string ADH_Approval_flag { get; set; }

        /// <summary>
        /// Gets or Sets ADH_Approved_date.
        /// </summary>
        public DateTime ADH_Approved_date { get; set; }

        /// <summary>
        /// Gets or Sets ADH_Approved_userid.
        /// </summary>
        public int ADH_Approved_userid { get; set; }

        /// <summary>
        /// Gets or Sets BHO_Approval_Reason.
        /// </summary>
        public string BHO_Approval_Reason { get; set; }

        /// <summary>
        /// Gets or Sets ADH_Approval_Reason.
        /// </summary>
        public string ADH_Approval_Reason { get; set; }

        /// <summary>
        /// Gets or Sets Ac_submit_flag.
        /// </summary>
        public string Ac_submit_flag { get; set; }

        /// <summary>
        /// Gets or Sets Refreshed_date.
        /// </summary>
        public DateTime Refreshed_date { get; set; }

        /// <summary>
        /// Gets or Sets Refreshed_userid.
        /// </summary>
        public int Refreshed_userid { get; set; }
    }

    /// <summary>
    /// Hort Produce Actual Panchayat.
    /// </summary>
    public class HortProduceActualPanchayat
    {
        /// <summary>
        /// Gets or Sets Reported_date.
        /// </summary>
        public DateTime Reported_date { get; set; }

        /// <summary>
        /// Gets or Sets District_id.
        /// </summary>
        public int District_id { get; set; }

        /// <summary>
        /// Gets or Sets District_name.
        /// </summary>
        public string District_name { get; set; }

        /// <summary>
        /// Gets or Sets Block_id.
        /// </summary>
        public int Block_id { get; set; }

        /// <summary>
        /// Gets or Sets Block_name.
        /// </summary>
        public string Block_name { get; set; }

        /// <summary>
        /// Gets or Sets Panchayat_id.
        /// </summary>
        public int Panchayat_id { get; set; }

        /// <summary>
        /// Gets or Sets Panchayat_name.
        /// </summary>
        public string Panchayat_name { get; set; }

        /// <summary>
        /// Gets or Sets Season_id.
        /// </summary>
        public int Season_id { get; set; }

        /// <summary>
        /// Gets or Sets Season_name.
        /// </summary>
        public string Season_name { get; set; }

        /// <summary>
        /// Gets or Sets CropList.
        /// </summary>
        public List<HortProduceActualPanchtCrop> CropList { get; set; }

        /// <summary>
        /// Gets or Sets Ac_submitted_date.
        /// </summary>
        public DateTime Ac_submitted_date { get; set; }

        /// <summary>
        /// Gets or Sets Ac_submitted_userid.
        /// </summary>
        public int Ac_submitted_userid { get; set; }

        /// <summary>
        /// Gets or Sets Bho_approval_flag.
        /// </summary>
        public string Bho_approval_flag { get; set; }

        /// <summary>
        /// Gets or Sets Bho_approval_reason.
        /// </summary>
        public string Bho_approval_reason { get; set; }

        /// <summary>
        /// Gets or Sets Bho_approved_date.
        /// </summary>
        public DateTime Bho_approved_date { get; set; }

        /// <summary>
        /// Gets or Sets Bho_approved_userid.
        /// </summary>
        public int Bho_approved_userid { get; set; }

        /// <summary>
        /// Gets or Sets Adh_approval_flag.
        /// </summary>
        public string Adh_approval_flag { get; set; }

        /// <summary>
        /// Gets or Sets Adh_approval_reason.
        /// </summary>
        public string Adh_approval_reason { get; set; }

        /// <summary>
        /// Gets or Sets Adh_approved_date.
        /// </summary>
        public DateTime Adh_approved_date { get; set; }

        /// <summary>
        /// Gets or Sets Adh_approved_userid.
        /// </summary>
        public int Adh_approved_userid { get; set; }

        /// <summary>
        /// Gets or Sets Ac_submitted_username.
        /// </summary>
        public string Ac_submitted_username { get; set; }

        /// <summary>
        /// Gets or Sets Bho_approved_username.
        /// </summary>
        public string Bho_approved_username { get; set; }

        /// <summary>
        /// Gets or Sets Adh_approved_username.
        /// </summary>
        public string Adh_approved_username { get; set; }
    }

    /// <summary>
    /// Hort Produce Actual Pancht Crop.
    /// </summary>
    public class HortProduceActualPanchtCrop
    {
        /// <summary>
        /// Gets or Sets Reported_date.
        /// </summary>
        public DateTime Reported_date { get; set; }

        /// <summary>
        /// Gets or Sets Crop_id.
        /// </summary>
        public int Crop_id { get; set; }

        /// <summary>
        /// Gets or Sets Crop_category.
        /// </summary>
        public string Crop_category { get; set; }

        /// <summary>
        /// Gets or Sets Crop_name.
        /// </summary>
        public string Crop_name { get; set; }

        /// <summary>
        /// Gets or Sets Produce_prev.
        /// </summary>
        public decimal Produce_prev { get; set; }

        /// <summary>
        /// Gets or Sets Unit_of_measure.
        /// </summary>
        public string Unit_of_measure { get; set; }

        /// <summary>
        /// Gets or Sets Produce_curr.
        /// </summary>
        public decimal Produce_curr { get; set; }

        /// <summary>
        /// Gets or Sets Production.
        /// </summary>
        public decimal Production { get; set; }

        /// <summary>
        /// Gets or Sets Ac_submitted_date.
        /// </summary>
        public DateTime Ac_submitted_date { get; set; }

        /// <summary>
        /// Gets or Sets Ac_submitted_userid.
        /// </summary>
        public int Ac_submitted_userid { get; set; }

        /// <summary>
        /// Gets or Sets Bho_approval_flag.
        /// </summary>
        public string Bho_approval_flag { get; set; }

        /// <summary>
        /// Gets or Sets Bho_approval_reason.
        /// </summary>
        public string Bho_approval_reason { get; set; }

        /// <summary>
        /// Gets or Sets Bho_approved_date.
        /// </summary>
        public DateTime Bho_approved_date { get; set; }

        /// <summary>
        /// Gets or Sets Bho_approved_userid.
        /// </summary>
        public int Bho_approved_userid { get; set; }

        /// <summary>
        /// Gets or Sets Adh_approval_flag.
        /// </summary>
        public string Adh_approval_flag { get; set; }

        /// <summary>
        /// Gets or Sets Adh_approval_reason.
        /// </summary>
        public string Adh_approval_reason { get; set; }

        /// <summary>
        /// Gets or Sets Adh_approved_date.
        /// </summary>
        public DateTime Adh_approved_date { get; set; }

        /// <summary>
        /// Gets or Sets Adh_approved_userid.
        /// </summary>
        public int Adh_approved_userid { get; set; }

        /// <summary>
        /// Gets or Sets Ac_submitted_username.
        /// </summary>
        public string Ac_submitted_username { get; set; }

        /// <summary>
        /// Gets or Sets Bho_approved_username.
        /// </summary>
        public string Bho_approved_username { get; set; }

        /// <summary>
        /// Gets or Sets Adh_approved_username.
        /// </summary>
        public string Adh_approved_username { get; set; }

        /// <summary>
        /// Gets or Sets Ac_submit_flag.
        /// </summary>
        public string Ac_submit_flag { get; set; }

        /// <summary>
        /// Gets or Sets Bho_add_edit_flag.
        /// </summary>
        public string Bho_add_edit_flag { get; set; }

        /// <summary>
        /// Gets or Sets Adh_add_edit_flag.
        /// </summary>
        public string Adh_add_edit_flag { get; set; }

        /// <summary>
        /// Gets or Sets Final_cvrg_flg.
        /// </summary>
        public string Final_cvrg_flg { get; set; }

        /// <summary>
        /// Gets or Sets Submission_source.
        /// </summary>
        public string Submission_source { get; set; }

        /// <summary>
        /// Gets or Sets Updatedby.
        /// </summary>
        public string Updatedby { get; set; }

        /// <summary>
        /// Gets or Sets Rec_Updated_Userid.
        /// </summary>
        public int Rec_Updated_Userid { get; set; }

        /// <summary>
        /// Gets or Sets Rec_Updated_Date.
        /// </summary>
        public DateTime Rec_Updated_Date { get; set; }
    }

    /// <summary>
    /// Hort Produce Actual Pancht.
    /// </summary>
    public class HortProduceActualPancht
    {
        /// <summary>
        /// Gets or Sets Reported_date.
        /// </summary>
        public DateTime Reported_date { get; set; }

        /// <summary>
        /// Gets or Sets Season_id.
        /// </summary>
        public int Season_id { get; set; }

        /// <summary>
        /// Gets or Sets Season_name.
        /// </summary>
        public string Season_name { get; set; }

        /// <summary>
        /// Gets or Sets District_id.
        /// </summary>
        public int District_id { get; set; }

        /// <summary>
        /// Gets or Sets District_name.
        /// </summary>
        public string District_name { get; set; }

        /// <summary>
        /// Gets or Sets Block_id.
        /// </summary>
        public int Block_id { get; set; }

        /// <summary>
        /// Gets or Sets Block_name.
        /// </summary>
        public string Block_name { get; set; }

        /// <summary>
        /// Gets or Sets Panchayat_id.
        /// </summary>
        public int Panchayat_id { get; set; }

        /// <summary>
        /// Gets or Sets Panchayat_name.
        /// </summary>
        public string Panchayat_name { get; set; }

        /// <summary>
        /// Gets or Sets Crop_id.
        /// </summary>
        public int Crop_id { get; set; }

        /// <summary>
        /// Gets or Sets Crop_category.
        /// </summary>
        public string Crop_category { get; set; }

        /// <summary>
        /// Gets or Sets Crop_name.
        /// </summary>
        public string Crop_name { get; set; }

        /// <summary>
        /// Gets or Sets Produce_prev.
        /// </summary>
        public decimal Produce_prev { get; set; }

        /// <summary>
        /// Gets or Sets Produce_curr.
        /// </summary>
        public decimal Produce_curr { get; set; }

        /// <summary>
        /// Gets or Sets Ac_submitted_date.
        /// </summary>
        public DateTime Ac_submitted_date { get; set; }

        /// <summary>
        /// Gets or Sets Ac_submitted_userid.
        /// </summary>
        public int Ac_submitted_userid { get; set; }

        /// <summary>
        /// Gets or Sets Bho_approval_flag.
        /// </summary>
        public string Bho_approval_flag { get; set; }

        /// <summary>
        /// Gets or Sets Bho_approval_reason.
        /// </summary>
        public string Bho_approval_reason { get; set; }

        /// <summary>
        /// Gets or Sets Bho_approved_date.
        /// </summary>
        public DateTime Bho_approved_date { get; set; }

        /// <summary>
        /// Gets or Sets Bho_approved_userid.
        /// </summary>
        public int Bho_approved_userid { get; set; }

        /// <summary>
        /// Gets or Sets Adh_approval_flag.
        /// </summary>
        public string Adh_approval_flag { get; set; }

        /// <summary>
        /// Gets or Sets Adh_approval_reason.
        /// </summary>
        public string Adh_approval_reason { get; set; }

        /// <summary>
        /// Gets or Sets Adh_approved_date.
        /// </summary>
        public DateTime Adh_approved_date { get; set; }

        /// <summary>
        /// Gets or Sets Adh_approved_userid.
        /// </summary>
        public int Adh_approved_userid { get; set; }

        /// <summary>
        /// Gets or Sets Ac_submitted_username.
        /// </summary>
        public string Ac_submitted_username { get; set; }

        /// <summary>
        /// Gets or Sets Bho_approved_username.
        /// </summary>
        public string Bho_approved_username { get; set; }

        /// <summary>
        /// Gets or Sets Adh_approved_username.
        /// </summary>
        public string Adh_approved_username { get; set; }

        /// <summary>
        /// Gets or Sets Ac_submit_flag.
        /// </summary>
        public string Ac_submit_flag { get; set; }

        /// <summary>
        /// Gets or Sets Bho_add_edit_flag.
        /// </summary>
        public string Bho_add_edit_flag { get; set; }

        /// <summary>
        /// Gets or Sets Adh_add_edit_flag.
        /// </summary>
        public string Adh_add_edit_flag { get; set; }

        /// <summary>
        /// Gets or Sets Final_cvrg_flg.
        /// </summary>
        public string Final_cvrg_flg { get; set; }

        /// <summary>
        /// Gets or Sets Updatedby.
        /// </summary>
        public string Updatedby { get; set; }

        /// <summary>
        /// Gets or Sets Rec_Created_Userid.
        /// </summary>
        public int Rec_Created_Userid { get; set; }

        /// <summary>
        /// Gets or Sets Rec_Created_Date.
        /// </summary>
        public DateTime Rec_Created_Date { get; set; }

        /// <summary>
        /// Gets or Sets Rec_Updated_Userid.
        /// </summary>
        public int Rec_Updated_Userid { get; set; }

        /// <summary>
        /// Gets or Sets Rec_Updated_Date.
        /// </summary>
        public DateTime Rec_Updated_Date { get; set; }
    }

    /// <summary>
    /// Hort Produce Actual Block.
    /// </summary>
    public class HortProduceActualBlock
    {
        /// <summary>
        /// Gets or Sets Reported_date.
        /// </summary>
        public DateTime Reported_date { get; set; }

        /// <summary>
        /// Gets or Sets Season_id.
        /// </summary>
        public int Season_id { get; set; }

        /// <summary>
        /// Gets or Sets Season_name.
        /// </summary>
        public string Season_name { get; set; }

        /// <summary>
        /// Gets or Sets District_id.
        /// </summary>
        public int District_id { get; set; }

        /// <summary>
        /// Gets or Sets District_name.
        /// </summary>
        public string District_name { get; set; }

        /// <summary>
        /// Gets or Sets Block_id.
        /// </summary>
        public int Block_id { get; set; }

        /// <summary>
        /// Gets or Sets Block_name.
        /// </summary>
        public string Block_name { get; set; }

        /// <summary>
        /// Gets or Sets Crop_id.
        /// </summary>
        public int Crop_id { get; set; }

        /// <summary>
        /// Gets or Sets Crop_category.
        /// </summary>
        public string Crop_category { get; set; }

        /// <summary>
        /// Gets or Sets Crop_name.
        /// </summary>
        public string Crop_name { get; set; }

        /// <summary>
        /// Gets or Sets Produce_prev.
        /// </summary>
        public decimal Produce_prev { get; set; }

        /// <summary>
        /// Gets or Sets Produce_curr.
        /// </summary>
        public decimal Produce_curr { get; set; }

        /// <summary>
        /// Gets or Sets Approval_flag.
        /// </summary>
        public string Approval_flag { get; set; }

        /// <summary>
        /// Gets or Sets Refreshed_date.
        /// </summary>
        public DateTime Refreshed_date { get; set; }

        /// <summary>
        /// Gets or Sets Refreshed_userid.
        /// </summary>
        public int Refreshed_userid { get; set; }

        /// <summary>
        /// Gets or Sets Adh_approval_flag.
        /// </summary>
        public string Adh_approval_flag { get; set; }

        /// <summary>
        /// Gets or Sets Adh_approval_reason.
        /// </summary>
        public string Adh_approval_reason { get; set; }

        /// <summary>
        /// Gets or Sets Adh_approved_date.
        /// </summary>
        public DateTime Adh_approved_date { get; set; }

        /// <summary>
        /// Gets or Sets Adh_approved_userid.
        /// </summary>
        public int Adh_approved_userid { get; set; }

        /// <summary>
        /// Gets or Sets Refreshed_username.
        /// </summary>
        public string Refreshed_username { get; set; }

        /// <summary>
        /// Gets or Sets Adh_approved_username.
        /// </summary>
        public string Adh_approved_username { get; set; }
    }

    /// <summary>
    /// Hort Produce Actual District.
    /// </summary>
    public class HortProduceActualDistrict
    {
        /// <summary>
        /// Gets or Sets Reported_date.
        /// </summary>
        public DateTime Reported_date { get; set; }

        /// <summary>
        /// Gets or Sets Season_id.
        /// </summary>
        public int Season_id { get; set; }

        /// <summary>
        /// Gets or Sets Season_name.
        /// </summary>
        public string Season_name { get; set; }

        /// <summary>
        /// Gets or Sets District_id.
        /// </summary>
        public int District_id { get; set; }

        /// <summary>
        /// Gets or Sets District_name.
        /// </summary>
        public string District_name { get; set; }

        /// <summary>
        /// Gets or Sets Crop_id.
        /// </summary>
        public int Crop_id { get; set; }

        /// <summary>
        /// Gets or Sets Crop_category.
        /// </summary>
        public string Crop_category { get; set; }

        /// <summary>
        /// Gets or Sets Crop_name.
        /// </summary>
        public string Crop_name { get; set; }

        /// <summary>
        /// Gets or Sets Produce_prev.
        /// </summary>
        public decimal Produce_prev { get; set; }

        /// <summary>
        /// Gets or Sets Produce_curr.
        /// </summary>
        public decimal Produce_curr { get; set; }

        /// <summary>
        /// Gets or Sets Approval_flag.
        /// </summary>
        public string Approval_flag { get; set; }

        /// <summary>
        /// Gets or Sets Refreshed_date.
        /// </summary>
        public DateTime Refreshed_date { get; set; }

        /// <summary>
        /// Gets or Sets Refreshed_userid.
        /// </summary>
        public int Refreshed_userid { get; set; }

        /// <summary>
        /// Gets or Sets Refreshed_username.
        /// </summary>
        public string Refreshed_username { get; set; }
    }

    /// <summary>
    /// Hort Produce Coverage Actual.
    /// </summary>
    public class HortProduceCoverageActual
    {
        /// <summary>
        /// Gets or Sets Reported_date.
        /// </summary>
        public DateTime? Reported_date { get; set; }

        /// <summary>
        /// Gets or Sets Season_id.
        /// </summary>
        public int Season_id { get; set; }

        /// <summary>
        /// Gets or Sets Season_name.
        /// </summary>
        public string Season_name { get; set; }

        /// <summary>
        /// Gets or Sets Crop_id.
        /// </summary>
        public int Crop_id { get; set; }

        /// <summary>
        /// Gets or Sets Crop_category.
        /// </summary>
        public string Crop_category { get; set; }

        /// <summary>
        /// Gets or Sets Crop_name.
        /// </summary>
        public string Crop_name { get; set; }

        /// <summary>
        /// Gets or Sets District_id.
        /// </summary>
        public int District_id { get; set; }

        /// <summary>
        /// Gets or Sets District_name.
        /// </summary>
        public string District_name { get; set; }

        /// <summary>
        /// Gets or Sets Production.
        /// </summary>
        public decimal Production { get; set; }

        /// <summary>
        /// Gets or Sets Produce_prev.
        /// </summary>
        public decimal Produce_prev { get; set; }

        /// <summary>
        /// Gets or Sets Produce_curr.
        /// </summary>
        public decimal Produce_curr { get; set; }

        /// <summary>
        /// Gets or Sets Createdby.
        /// </summary>
        public string Createdby { get; set; }

        /// <summary>
        /// Gets or Sets Updatedby.
        /// </summary>
        public string Updatedby { get; set; }

        /// <summary>
        /// Gets or Sets Refreshed_date.
        /// </summary>
        public DateTime? Refreshed_date { get; set; }

        /// <summary>
        /// Gets or Sets Refreshed_userid.
        /// </summary>
        public int Refreshed_userid { get; set; }

        /// <summary>
        /// Gets or Sets BlockList.
        /// </summary>
        public List<HortProduceBlock> BlockList { get; set; }
    }

    /// <summary>
    /// Hort Produce Block.
    /// </summary>
    public class HortProduceBlock
    {
        /// <summary>
        /// Gets or Sets Reported_date.
        /// </summary>
        public DateTime? Reported_date { get; set; }

        /// <summary>
        /// Gets or Sets Block_id.
        /// </summary>
        public int Block_id { get; set; }

        /// <summary>
        /// Gets or Sets Crop_id.
        /// </summary>
        public int Crop_id { get; set; }

        /// <summary>
        /// Gets or Sets Block_name.
        /// </summary>
        public string Block_name { get; set; }

        /// <summary>
        /// Gets or Sets Production.
        /// </summary>
        public decimal Production { get; set; }

        /// <summary>
        /// Gets or Sets Produce_prev.
        /// </summary>
        public decimal Produce_prev { get; set; }

        /// <summary>
        /// Gets or Sets Produce_curr.
        /// </summary>
        public decimal Produce_curr { get; set; }

        /// <summary>
        /// Gets or Sets Createdby.
        /// </summary>
        public string Createdby { get; set; }

        /// <summary>
        /// Gets or Sets Updatedby.
        /// </summary>
        public string Updatedby { get; set; }

        /// <summary>
        /// Gets or Sets Refreshed_date.
        /// </summary>
        public DateTime? Refreshed_date { get; set; }

        /// <summary>
        /// Gets or Sets Refreshed_userid.
        /// </summary>
        public int Refreshed_userid { get; set; }

        /// <summary>
        /// Gets or Sets Adh_approval_flag.
        /// </summary>
        public string Adh_approval_flag { get; set; }

        /// <summary>
        /// Gets or Sets Adh_approval_reason.
        /// </summary>
        public string Adh_approval_reason { get; set; }

        /// <summary>
        /// Gets or Sets Adh_approved_date.
        /// </summary>
        public DateTime? Adh_approved_date { get; set; }

        /// <summary>
        /// Gets or Sets Adh_approved_userid.
        /// </summary>
        public int Adh_approved_userid { get; set; }

        /// <summary>
        /// Gets or Sets Refreshed_username.
        /// </summary>
        public string Refreshed_username { get; set; }

        /// <summary>
        /// Gets or Sets Adh_approved_username.
        /// </summary>
        public string Adh_approved_username { get; set; }

        /// <summary>
        /// Gets or Sets PanchayatList.
        /// </summary>
        public List<HortProduceCoveragePanchayat> PanchayatList { get; set; }
    }

    /// <summary>
    /// Hort Produce Coverage Panchayat.
    /// </summary>
    public class HortProduceCoveragePanchayat
    {
        /// <summary>
        /// Gets or Sets Reported_date.
        /// </summary>
        public DateTime? Reported_date { get; set; }

        /// <summary>
        /// Gets or Sets Panchayat_id.
        /// </summary>
        public int Panchayat_id { get; set; }

        /// <summary>
        /// Gets or Sets Panchayat_name.
        /// </summary>
        public string Panchayat_name { get; set; }

        /// <summary>
        /// Gets or Sets Production.
        /// </summary>
        public decimal Production { get; set; }

        /// <summary>
        /// Gets or Sets Produce_prev.
        /// </summary>
        public decimal Produce_prev { get; set; }

        /// <summary>
        /// Gets or Sets Produce_curr.
        /// </summary>
        public decimal Produce_curr { get; set; }

        /// <summary>
        /// Gets or Sets Unit_of_measure.
        /// </summary>
        public string Unit_of_measure { get; set; }

        /// <summary>
        /// Gets or Sets Createdby.
        /// </summary>
        public string Createdby { get; set; }

        /// <summary>
        /// Gets or Sets Updatedby.
        /// </summary>
        public string Updatedby { get; set; }

        /// <summary>
        /// Gets or Sets Submission_source.
        /// </summary>
        public string Submission_source { get; set; }

        /// <summary>
        /// Gets or Sets Block_id.
        /// </summary>
        public int Block_id { get; set; }

        /// <summary>
        /// Gets or Sets Crop_id.
        /// </summary>
        public int Crop_id { get; set; }

        /// <summary>
        /// Gets or Sets Ac_submitted_date.
        /// </summary>
        public DateTime? Ac_submitted_date { get; set; }

        /// <summary>
        /// Gets or Sets Ac_submitted_userid.
        /// </summary>
        public int Ac_submitted_userid { get; set; }

        /// <summary>
        /// Gets or Sets Bho_approval_flag.
        /// </summary>
        public string Bho_approval_flag { get; set; }

        /// <summary>
        /// Gets or Sets Bho_approval_reason.
        /// </summary>
        public string Bho_approval_reason { get; set; }

        /// <summary>
        /// Gets or Sets Bho_approved_date.
        /// </summary>
        public DateTime Bho_approved_date { get; set; }

        /// <summary>
        /// Gets or Sets Bho_approved_userid.
        /// </summary>
        public int Bho_approved_userid { get; set; }

        /// <summary>
        /// Gets or Sets Adh_approval_flag.
        /// </summary>
        public string Adh_approval_flag { get; set; }

        /// <summary>
        /// Gets or Sets Adh_approval_reason.
        /// </summary>
        public string Adh_approval_reason { get; set; }

        /// <summary>
        /// Gets or Sets Adh_approved_date.
        /// </summary>
        public DateTime? Adh_approved_date { get; set; }

        /// <summary>
        /// Gets or Sets Adh_approved_userid.
        /// </summary>
        public int Adh_approved_userid { get; set; }

        /// <summary>
        /// Gets or Sets Ac_submitted_username.
        /// </summary>
        public string Ac_submitted_username { get; set; }

        /// <summary>
        /// Gets or Sets Bho_approved_username.
        /// </summary>
        public string Bho_approved_username { get; set; }

        /// <summary>
        /// Gets or Sets Adh_approved_username.
        /// </summary>
        public string Adh_approved_username { get; set; }

        /// <summary>
        /// Gets or Sets Ac_submit_flag.
        /// </summary>
        public string Ac_submit_flag { get; set; }

        /// <summary>
        /// Gets or Sets Bho_add_edit_flag.
        /// </summary>
        public string Bho_add_edit_flag { get; set; }

        /// <summary>
        /// Gets or Sets Adh_add_edit_flag.
        /// </summary>
        public string Adh_add_edit_flag { get; set; }

        /// <summary>
        /// Gets or Sets Final_cvrg_flg.
        /// </summary>
        public string Final_cvrg_flg { get; set; }

        /// <summary>
        /// Gets or Sets Rec_updated_userid.
        /// </summary>
        public int Rec_updated_userid { get; set; }

        /// <summary>
        /// Gets or Sets Rec_updated_date.
        /// </summary>
        public DateTime? Rec_updated_date { get; set; }
    }

    /// <summary>
    /// Hort Produce Coverage Actl.
    /// </summary>
    public class HortProduceCoverageActl
    {
        /// <summary>
        /// Gets or Sets Reported_date.
        /// </summary>
        public DateTime? Reported_date { get; set; }

        /// <summary>
        /// Gets or Sets Season_id.
        /// </summary>
        public int Season_id { get; set; }

        /// <summary>
        /// Gets or Sets Season_name.
        /// </summary>
        public string Season_name { get; set; }

        /// <summary>
        /// Gets or Sets Crop_id.
        /// </summary>
        public int Crop_id { get; set; }

        /// <summary>
        /// Gets or Sets Crop_category.
        /// </summary>
        public string Crop_category { get; set; }

        /// <summary>
        /// Gets or Sets Crop_name.
        /// </summary>
        public string Crop_name { get; set; }

        /// <summary>
        /// Gets or Sets District_id.
        /// </summary>
        public int District_id { get; set; }

        /// <summary>
        /// Gets or Sets District_name.
        /// </summary>
        public string District_name { get; set; }

        /// <summary>
        /// Gets or Sets Production.
        /// </summary>
        public decimal Production { get; set; }

        /// <summary>
        /// Gets or Sets Produce_prev.
        /// </summary>
        public decimal Produce_prev { get; set; }

        /// <summary>
        /// Gets or Sets Produce_curr.
        /// </summary>
        public decimal Produce_curr { get; set; }

        /// <summary>
        /// Gets or Sets Createdby.
        /// </summary>
        public string Createdby { get; set; }

        /// <summary>
        /// Gets or Sets Updatedby.
        /// </summary>
        public string Updatedby { get; set; }

        /// <summary>
        /// Gets or Sets Refreshed_date.
        /// </summary>
        public DateTime? Refreshed_date { get; set; }

        /// <summary>
        /// Gets or Sets Refreshed_userid.
        /// </summary>
        public int Refreshed_userid { get; set; }

        /// <summary>
        /// Gets or Sets BlockList.
        /// </summary>
        public List<HortProduceBlk> BlockList { get; set; }
    }

    /// <summary>
    /// Hort Produce Bulk.
    /// </summary>
    public class HortProduceBlk
    {
        /// <summary>
        /// Gets or Sets Reported_date.
        /// </summary>
        public DateTime? Reported_date { get; set; }

        /// <summary>
        /// Gets or Sets Block_id.
        /// </summary>
        public int Block_id { get; set; }

        /// <summary>
        /// Gets or Sets Block_name.
        /// </summary>
        public string Block_name { get; set; }

        /// <summary>
        /// Gets or Sets Crop_id.
        /// </summary>
        public int Crop_id { get; set; }

        /// <summary>
        /// Gets or Sets Production.
        /// </summary>
        public decimal Production { get; set; }

        /// <summary>
        /// Gets or Sets Produce_prev.
        /// </summary>
        public decimal Produce_prev { get; set; }

        /// <summary>
        /// Gets or Sets Produce_curr.
        /// </summary>
        public decimal Produce_curr { get; set; }

        /// <summary>
        /// Gets or Sets Createdby.
        /// </summary>
        public string Createdby { get; set; }

        /// <summary>
        /// Gets or Sets Updatedby.
        /// </summary>
        public string Updatedby { get; set; }

        /// <summary>
        /// Gets or Sets Refreshed_date.
        /// </summary>
        public DateTime? Refreshed_date { get; set; }

        /// <summary>
        /// Gets or Sets Refreshed_userid.
        /// </summary>
        public int Refreshed_userid { get; set; }

        /// <summary>
        /// Gets or Sets Adh_approval_flag.
        /// </summary>
        public string Adh_approval_flag { get; set; }

        /// <summary>
        /// Gets or Sets Adh_approval_reason.
        /// </summary>
        public string Adh_approval_reason { get; set; }

        /// <summary>
        /// Gets or Sets Adh_approved_date.
        /// </summary>
        public DateTime? Adh_approved_date { get; set; }

        /// <summary>
        /// Gets or Sets Adh_approved_userid.
        /// </summary>
        public int Adh_approved_userid { get; set; }

        /// <summary>
        /// Gets or Sets Refreshed_username.
        /// </summary>
        public string Refreshed_username { get; set; }

        /// <summary>
        /// Gets or Sets Adh_approved_username.
        /// </summary>
        public string Adh_approved_username { get; set; }

        /// <summary>
        /// Gets or Sets PanchayatList.
        /// </summary>
        public List<HortProduceCoveragePanchyt> PanchayatList { get; set; }
    }

    /// <summary>
    /// Hort Produce Coverage Panchyt.
    /// </summary>
    public class HortProduceCoveragePanchyt
    {
        /// <summary>
        /// Gets or Sets Reported_date.
        /// </summary>
        public DateTime? Reported_date { get; set; }

        /// <summary>
        /// Gets or Sets Block_id.
        /// </summary>
        public int Block_id { get; set; }

        /// <summary>
        /// Gets or Sets Crop_id.
        /// </summary>
        public int Crop_id { get; set; }

        /// <summary>
        /// Gets or Sets Panchayat_id.
        /// </summary>
        public int Panchayat_id { get; set; }

        /// <summary>
        /// Gets or Sets Panchayat_name.
        /// </summary>
        public string Panchayat_name { get; set; }

        /// <summary>
        /// Gets or Sets Production.
        /// </summary>
        public decimal Production { get; set; }

        /// <summary>
        /// Gets or Sets Produce_prev.
        /// </summary>
        public decimal Produce_prev { get; set; }

        /// <summary>
        /// Gets or Sets Unit_of_measure.
        /// </summary>
        public string Unit_of_measure { get; set; }

        /// <summary>
        /// Gets or Sets Produce_curr.
        /// </summary>
        public decimal Produce_curr { get; set; }

        /// <summary>
        /// Gets or Sets Createdby.
        /// </summary>
        public string Createdby { get; set; }

        /// <summary>
        /// Gets or Sets Updatedby.
        /// </summary>
        public string Updatedby { get; set; }

        /// <summary>
        /// Gets or Sets Submission_source.
        /// </summary>
        public string Submission_source { get; set; }

        /// <summary>
        /// Gets or Sets Ac_submitted_date.
        /// </summary>
        public DateTime? Ac_submitted_date { get; set; }

        /// <summary>
        /// Gets or Sets Ac_submitted_userid.
        /// </summary>
        public int Ac_submitted_userid { get; set; }

        /// <summary>
        /// Gets or Sets Bho_approval_flag.
        /// </summary>
        public string Bho_approval_flag { get; set; }

        /// <summary>
        /// Gets or Sets Bho_approval_reason.
        /// </summary>
        public string Bho_approval_reason { get; set; }

        /// <summary>
        /// Gets or Sets Bho_approved_date.
        /// </summary>
        public DateTime? Bho_approved_date { get; set; }

        /// <summary>
        /// Gets or Sets Bho_approved_userid.
        /// </summary>
        public int Bho_approved_userid { get; set; }

        /// <summary>
        /// Gets or Sets Adh_approval_flag.
        /// </summary>
        public string Adh_approval_flag { get; set; }

        /// <summary>
        /// Gets or Sets Adh_approval_reason.
        /// </summary>
        public string Adh_approval_reason { get; set; }

        /// <summary>
        /// Gets or Sets Adh_approved_date.
        /// </summary>
        public DateTime? Adh_approved_date { get; set; }

        /// <summary>
        /// Gets or Sets Adh_approved_userid.
        /// </summary>
        public int Adh_approved_userid { get; set; }

        /// <summary>
        /// Gets or Sets Ac_submitted_username.
        /// </summary>
        public string Ac_submitted_username { get; set; }

        /// <summary>
        /// Gets or Sets Bho_approved_username.
        /// </summary>
        public string Bho_approved_username { get; set; }

        /// <summary>
        /// Gets or Sets Adh_approved_username.
        /// </summary>
        public string Adh_approved_username { get; set; }

        /// <summary>
        /// Gets or Sets Ac_submit_flag.
        /// </summary>
        public string Ac_submit_flag { get; set; }

        /// <summary>
        /// Gets or Sets Bho_add_edit_flag.
        /// </summary>
        public string Bho_add_edit_flag { get; set; }

        /// <summary>
        /// Gets or Sets Adh_add_edit_flag.
        /// </summary>
        public string Adh_add_edit_flag { get; set; }

        /// <summary>
        /// Gets or Sets Final_cvrg_flg.
        /// </summary>
        public string Final_cvrg_flg { get; set; }

        /// <summary>
        /// Gets or Sets Rec_updated_userid.
        /// </summary>
        public int Rec_updated_userid { get; set; }

        /// <summary>
        /// Gets or Sets Rec_updated_date.
        /// </summary>
        public DateTime? Rec_updated_date { get; set; }

        /// <summary>
        /// Gets or Sets Pending_adh_value.
        /// </summary>
        public decimal Pending_adh_value { get; set; }

        /// <summary>
        /// Gets or Sets Approved_adh_value.
        /// </summary>
        public decimal Approved_adh_value { get; set; }
    }
}
