//------------------------------------------------------------------------------
// <copyright file="PGSFarmerDetails.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------
namespace ADIDAS.Model.DTO
{
    using System;

    /// <summary>
    /// PGS Farmer Details.
    /// </summary>
    public class PGSFarmerDetails
    {
        /// <summary>
        /// Gets or Sets QueryName.
        /// </summary>
        public string QueryName { get; set; }

        /// <summary>
        /// Gets or Sets FarmRexId.
        /// </summary>
        public int FarmRexId { get; set; }

        /// <summary>
        /// Gets or Sets DistrictId.
        /// </summary>
        public int DistrictId { get; set; }

        /// <summary>
        /// Gets or Sets BlockId.
        /// </summary>
        public int BlockId { get; set; }

        /// <summary>
        /// Gets or Sets PanchayatID.
        /// </summary>
        public int PanchayatID { get; set; }

        /// <summary>
        /// Gets or Sets GroupId.
        /// </summary>
        public int GroupId { get; set; }

        /// <summary>
        /// Gets or Sets FPOId.
        /// </summary>
        public int FPOId { get; set; }

        /// <summary>
        /// Gets or Sets FarmerDBTRegNo.
        /// </summary>
        public long FarmerDBTRegNo { get; set; }

        /// <summary>
        /// Gets or Sets PhoneNo.
        /// </summary>
        public string PhoneNo { get; set; }

        /// <summary>
        /// Gets or Sets FarmId.
        /// </summary>
        public int FarmId { get; set; }

        /// <summary>
        /// Gets or Sets Status.
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Gets or Sets SeasonId.
        /// </summary>
        public int SeasonId { get; set; }

        /// <summary>
        /// Gets or Sets Id.
        /// </summary>
        public int Id { get; set; }
    }

    /// <summary>
    /// PGSSchemeNames
    /// </summary>
    public class PGSSchemeNames
    {
        /// <summary>
        /// Gets or Sets SchemeName.
        /// </summary>
        public string SchemeName { get; set; }

        /// <summary>
        /// Gets or Sets SchemeId.
        /// </summary>
        public string SchemeId { get; set; }
    }

    /// <summary>
    /// PGSFarmerGroupTable
    /// </summary>
    public class PGSFarmerGroupTable
    {
        /// <summary>
        /// Gets or Sets GroupName.
        /// </summary>
        public string GroupName { get; set; }

        /// <summary>
        /// Gets or Sets GroupRegNo.
        /// </summary>
        public string GroupRegNo { get; set; }

        /// <summary>
        /// Gets or Sets ServiceProviderName.
        /// </summary>
        public string ServiceProviderName { get; set; }

        /// <summary>
        /// Gets or Sets AssociatedFarmers.
        /// </summary>
        public int AssociatedFarmers { get; set; }
    }

    /// <summary>
    /// PGSMajorTownNames
    /// </summary>
    public class PGSMajorTownNames
    {
        /// <summary>
        /// Gets or Sets MajorTownName.
        /// </summary>
        public string MajorTownName { get; set; }
    }

    /// <summary>
    /// PGSMajorCropNames
    /// </summary>
    public class PGSMajorCropNames
    {
        /// <summary>
        /// Gets or Sets MajorCropName.
        /// </summary>
        public string MajorCropName { get; set; }
    }
}
