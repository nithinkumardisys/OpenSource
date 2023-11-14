//------------------------------------------------------------------------------
// <copyright file="PaisInfoFields.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------
namespace ADIDAS.Model.DTO
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// PaisInfoFields
    /// </summary>
    public class PaisInfoFields
    {
    }

    /// <summary>
    /// PaisMarketsEntity
    /// </summary>
    public class PaisMarketsEntity
    {
        /// <summary>
        /// Gets or Sets Rank.
        /// </summary>
        public int Mkt_id { get; set; }

        /// <summary>
        /// Gets or Sets Rank.
        /// </summary>
        public string Mkt_name { get; set; }

        /// <summary>
        /// Gets or Sets Rank.
        /// </summary>
        public string Is_local_preferences_added { get; set; }
    }

    /// <summary>
    /// PaisCommodityGroupEntity
    /// </summary>
    public class PaisCommodityGroupEntity
    {
        /// <summary>
        /// Gets or Sets Rank.
        /// </summary>
        public int Cmdt_grp_id { get; set; }

        /// <summary>
        /// Gets or Sets Rank.
        /// </summary>
        public string Cmdt_grp_name { get; set; }
    }

    /// <summary>
    /// PaisCommodityEntity
    /// </summary>
    public class PaisCommodityEntity
    {
        /// <summary>
        /// Gets or Sets Rank.
        /// </summary>
        public int Cmdt_id { get; set; }

        /// <summary>
        /// Gets or Sets Rank.
        /// </summary>
        public string Cmdt_name { get; set; }
    }

    /// <summary>
    /// PaisVariety
    /// </summary>
    public class PaisVariety
    {
        /// <summary>
        /// Gets or Sets Rank.
        /// </summary>
        public int Variety_id { get; set; }

        /// <summary>
        /// Gets or Sets Rank.
        /// </summary>
        public string Variety_name { get; set; }
    }

    /// <summary>
    /// PaisUnit
    /// </summary>
    public class PaisUnit
    {
        /// <summary>
        /// Gets or Sets Rank.
        /// </summary>
        public int Attribute_id { get; set; }

        /// <summary>
        /// Gets or Sets Rank.
        /// </summary>
        public string Attribute_Value { get; set; }
    }

    /// <summary>
    /// PaisLocalPrefencesInfo
    /// </summary>
    public class PaisLocalPrefencesInfo
    {
        /// <summary>
        /// Gets or Sets Rank.
        /// </summary>
        public int MarketId { get; set; }

        /// <summary>
        /// Gets or Sets Rank.
        /// </summary>
        public string MarketName { get; set; }

        /// <summary>
        /// Gets or Sets Rank.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or Sets Rank.
        /// </summary>
        public int CommodityGroupId { get; set; }

        /// <summary>
        /// Gets or Sets Rank.
        /// </summary>
        public string CommodityGroupName { get; set; }

        /// <summary>
        /// Gets or Sets Rank.
        /// </summary>
        public List<CommodityDetail> CommodityDetails { get; set; }
    }

    /// <summary>
    /// CommodityDetail
    /// </summary>
    public class CommodityDetail
    {
        /// <summary>
        /// Gets or Sets Rank.
        /// </summary>
        public int CommodityId { get; set; }

        /// <summary>
        /// Gets or Sets Rank.
        /// </summary>
        public string CommodityName { get; set; }

        /// <summary>
        /// Gets or Sets Rank.
        /// </summary>
        public int ArrivalUnitId { get; set; }

        /// <summary>
        /// Gets or Sets Rank.
        /// </summary>
        public decimal ArrivalCompValue { get; set; }

        /// <summary>
        /// Gets or Sets Rank.
        /// </summary>
        public string ArrivalUnitName { get; set; }

        /// <summary>
        /// Gets or Sets Rank.
        /// </summary>
        public List<Variety> Varieties { get; set; }
    }

    /// <summary>
    /// Variety
    /// </summary>
    public class Variety
    {
        /// <summary>
        /// Gets or Sets Rank.
        /// </summary>
        public int VarietyId { get; set; }

        /// <summary>
        /// Gets or Sets Rank.
        /// </summary>
        public string VarietyName { get; set; }

        /// <summary>
        /// Gets or Sets Rank.
        /// </summary>
        public int PriceUnitId { get; set; }

        /// <summary>
        /// Gets or Sets Rank.
        /// </summary>
        public decimal PriceCompValue { get; set; }

        /// <summary>
        /// Gets or Sets Rank.
        /// </summary>
        public string PriceUnitName { get; set; }
    }

    /// <summary>
    /// DeleteCommodityOrVariety
    /// </summary>
    public class DeleteCommodityOrVariety
    {
        /// <summary>
        /// Gets or Sets Rank.
        /// </summary>
        public int MarketId { get; set; }

        /// <summary>
        /// Gets or Sets Rank.
        /// </summary>
        public int CmdtGrpId { get; set; }

        /// <summary>
        /// Gets or Sets Rank.
        /// </summary>
        public int CmdtId { get; set; }

        /// <summary>
        /// Gets or Sets Rank.
        /// </summary>
        public int VarietyId { get; set; }
    }

    /// <summary>
    /// NilTransaction
    /// </summary>
    public class NilTransaction
    {
        /// <summary>
        /// Gets or Sets Rank.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or Sets Rank.
        /// </summary>
        public int MarketId { get; set; }

        /// <summary>
        /// Gets or Sets Rank.
        /// </summary>
        public string MarketName { get; set; }

        /// <summary>
        /// Gets or Sets Rank.
        /// </summary>
        public DateTime ReportedDate { get; set; }

        /// <summary>
        /// Gets or Sets Rank.
        /// </summary>
        public int Districtid { get; set; }
    }

    /// <summary>
    /// ArrivalDetails
    /// </summary>
    public class ArrivalDetails
    {
        /// <summary>
        /// Gets or Sets Rank.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or Sets Rank.
        /// </summary>
        public int MarketId { get; set; }

        /// <summary>
        /// Gets or Sets Rank.
        /// </summary>
        public string MarketName { get; set; }

        /// <summary>
        /// Gets or Sets Rank.
        /// </summary>
        public int Districtid { get; set; }

        /// <summary>
        /// Gets or Sets Rank.
        /// </summary>
        public string DistrictName { get; set; }

        /// <summary>
        /// Gets or Sets Rank.
        /// </summary>
        public List<SubmittedDate> SubmittedDateList { get; set; }
    }

    /// <summary>
    /// SubmittedDate
    /// </summary>
    public class SubmittedDate
    {
        /// <summary>
        /// Gets or Sets Rank.
        /// </summary>
        public DateTime ReportedDate { get; set; }

        /// <summary>
        /// Gets or Sets Rank.
        /// </summary>
        public List<ArrivalCommodityGroupDetail> ArrivalCommodityGroupDetail { get; set; }
    }

    /// <summary>
    /// ArrivalCommodityGroupDetail
    /// </summary>
    public class ArrivalCommodityGroupDetail
    {
        /// <summary>
        /// Gets or Sets Rank.
        /// </summary>
        public int CommodityGroupId { get; set; }

        /// <summary>
        /// Gets or Sets Rank.
        /// </summary>
        public string CommodityGroupName { get; set; }

        /// <summary>
        /// Gets or Sets Rank.
        /// </summary>
        public List<ArrivalCommodityDetail> ArrivalCommodityDetail { get; set; }
    }

    /// <summary>
    /// ArrivalCommodityDetail
    /// </summary>
    public class ArrivalCommodityDetail
    {
        /// <summary>
        /// Gets or Sets Rank.
        /// </summary>
        public int CommodityId { get; set; }

        /// <summary>
        /// Gets or Sets Rank.
        /// </summary>
        public string CommodityName { get; set; }

        /// <summary>
        /// Gets or Sets Rank.
        /// </summary>
        public int ArrivalUnitId { get; set; }

        /// <summary>
        /// Gets or Sets Rank.
        /// </summary>
        public double ArrivalStdUnit { get; set; }

        /// <summary>
        /// Gets or Sets Rank.
        /// </summary>
        public decimal ArrivalCompValue { get; set; }

        /// <summary>
        /// Gets or Sets Rank.
        /// </summary>
        public string ArrivalUnitName { get; set; }

        /// <summary>
        /// Gets or Sets Rank.
        /// </summary>
        public decimal LocArrivalQty { get; set; }

        /// <summary>
        /// Gets or Sets Rank.
        /// </summary>
        public decimal ArrivalQty { get; set; }

        /// <summary>
        /// Gets or Sets Rank.
        /// </summary>
        public string Trend { get; set; }

        /// <summary>
        /// Gets or Sets Rank.
        /// </summary>
        public List<ArrivalVariety> Varietylist { get; set; }
    }

    /// <summary>
    /// ArrivalVariety
    /// </summary>
    public class ArrivalVariety
    {
        /// <summary>
        /// Gets or Sets Rank.
        /// </summary>
        public int VarietyId { get; set; }

        /// <summary>
        /// Gets or Sets Rank.
        /// </summary>
        public string VarietyName { get; set; }

        /// <summary>
        /// Gets or Sets Rank.
        /// </summary>
        public decimal PriceCompValue { get; set; }

        /// <summary>
        /// Gets or Sets Rank.
        /// </summary>
        public int VarietyUnit { get; set; }

        /// <summary>
        /// Gets or Sets Rank.
        /// </summary>
        public string VarietyUnitName { get; set; }

        /// <summary>
        /// Gets or Sets Rank.
        /// </summary>
        public decimal MinPrice { get; set; }

        /// <summary>
        /// Gets or Sets Rank.
        /// </summary>
        public decimal ModelPrice { get; set; }

        /// <summary>
        /// Gets or Sets Rank.
        /// </summary>
        public decimal MaxPrice { get; set; }

        /// <summary>
        /// Gets or Sets Rank.
        /// </summary>
        public string Grade { get; set; }
    }

    /// <summary>
    /// AnamolusDate
    /// </summary>
    public class AnamolusDate
    {
        /// <summary>
        /// Gets or Sets Rank.
        /// </summary>
        public int MarketId { get; set; }

        /// <summary>
        /// Gets or Sets Rank.
        /// </summary>
        public DateTime ReportedDate { get; set; }

        /// <summary>
        /// Gets or Sets Rank.
        /// </summary>
        public int UserId { get; set; }
    }
}