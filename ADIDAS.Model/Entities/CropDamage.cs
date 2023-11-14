//------------------------------------------------------------------------------
// <copyright file="CropDamage.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//-----------------------------------------------------------------------------
namespace ADIDAS.Model.Entities
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// CropDamage.
    /// </summary>
    public class CropDamage
    {
        /// <summary>
        /// Gets or Sets Reported_date.
        /// </summary>
        public DateTime? Reported_date { get; set; }

        /// <summary>
        /// Gets or Sets Season_Id.
        /// </summary>
        public int Season_Id { get; set; }

        /// <summary>
        /// Gets or Sets Season_Name.
        /// </summary>
        public string Season_Name { get; set; }

        /// <summary>
        /// Gets or Sets Crop_Id.
        /// </summary>
        public int Crop_Id { get; set; }

        /// <summary>
        /// Gets or Sets Crop_Category.
        /// </summary>
        public string Crop_Category { get; set; }

        /// <summary>
        /// Gets or Sets Crop_Name.
        /// </summary>
        public string Crop_Name { get; set; }

        /// <summary>
        /// Gets or Sets District_Id.
        /// </summary>
        public int District_Id { get; set; }

        /// <summary>
        /// Gets or Sets District_Name.
        /// </summary>
        public string District_Name { get; set; }

        /// <summary>
        /// Gets or Sets Irrigated_Dmg_Area.
        /// </summary>
        public decimal? Irrigated_Dmg_Area { get; set; }

        /// <summary>
        /// Gets or Sets Damage_Reason.
        /// </summary>
        public string Damage_Reason { get; set; }

        /// <summary>
        /// Gets or Sets CreatedBy.
        /// </summary>
        public string CreatedBy { get; set; }

        /// <summary>
        /// Gets or Sets UpdatedBy.
        /// </summary>
        public string UpdatedBy { get; set; }

        /// <summary>
        /// Gets or Sets Refreshed_date.
        /// </summary>
        public DateTime? Refreshed_date { get; set; }

        /// <summary>
        /// Gets or Sets Refreshed_userid.
        /// </summary>
        public int? Refreshed_userid { get; set; }

        /// <summary>
        /// Gets or Sets BlockList.
        /// </summary>
        public List<CropDmgBlock> BlockList { get; set; }
    }

    /// <summary>
    /// CropDmgBlock.
    /// </summary>
    public class CropDmgBlock
    {
        /// <summary>
        /// Gets or Sets Reported_Date.
        /// </summary>
        public DateTime? Reported_date { get; set; }

        /// <summary>
        /// Gets or Sets Block_Id.
        /// </summary>
        public int Block_Id { get; set; }

        /// <summary>
        /// Gets or Sets Block_Name.
        /// </summary>
        public string Block_Name { get; set; }

        /// <summary>
        /// Gets or Sets Irrigated_Dmg_Area.
        /// </summary>
        public decimal? Irrigated_Dmg_Area { get; set; }

        /// <summary>
        /// Gets or Sets Damage_Reason.
        /// </summary>
        public string Damage_Reason { get; set; }

        /// <summary>
        /// Gets or Sets CreatedBy.
        /// </summary>
        public string CreatedBy { get; set; }

        /// <summary>
        /// Gets or Sets UpdatedBy.
        /// </summary>
        public string UpdatedBy { get; set; }

        /// <summary>
        /// Gets or Sets Refreshed_date.
        /// </summary>
        public DateTime? Refreshed_date { get; set; }

        /// <summary>
        /// Gets or Sets Refreshed_userid.
        /// </summary>
        public int? Refreshed_userid { get; set; }

        /// <summary>
        /// Gets or Sets DAO_Approval_flag.
        /// </summary>
        public string DAO_Approval_flag { get; set; }

        /// <summary>
        /// Gets or Sets DAO_Approval_Reason.
        /// </summary>
        public string DAO_Approval_Reason { get; set; }

        /// <summary>
        /// Gets or Sets DAO_Approved_date.
        /// </summary>
        public DateTime? DAO_Approved_date { get; set; }

        /// <summary>
        /// Gets or Sets DAO_Approved_userid.
        /// </summary>
        public int? DAO_Approved_userid { get; set; }

        /// <summary>
        /// Gets or Sets Refreshed_username.
        /// </summary>
        public string Refreshed_username { get; set; }

        /// <summary>
        /// Gets or Sets Dao_approved_username.
        /// </summary>
        public string Dao_approved_username { get; set; }

        /// <summary>
        /// Gets or Sets Reported_date.
        /// </summary>
        public List<CropDmgPanchayat> PanchayatList { get; set; }
    }

    /// <summary>
    /// CropDmgPanchayat.
    /// </summary>
    public class CropDmgPanchayat
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CropDmgPanchayat"/> class.
        /// CropDmgPanchayat.
        /// </summary>
        public CropDmgPanchayat()
        {
            this.DamageList = new List<DamageEntity>();
        }

        /// <summary>
        /// Gets or Sets Reported_date.
        /// </summary>
        public DateTime? Reported_date { get; set; }

        /// <summary>
        /// Gets or Sets Panchayat_Id.
        /// </summary>
        public int Panchayat_Id { get; set; }

        /// <summary>
        /// Gets or Sets Panchayat_Name.
        /// </summary>
        public string Panchayat_Name { get; set; }

        /// <summary>
        /// Gets or Sets Area_covered.
        /// </summary>
        public decimal? Area_covered { get; set; }

        /// <summary>
        /// Gets or Sets Dmg_Till_Date.
        /// </summary>
        public decimal? Dmg_Till_Date { get; set; }

        /// <summary>
        /// Gets or Sets DamageList.
        /// </summary>
        public List<DamageEntity> DamageList { get; set; }
    }
}
