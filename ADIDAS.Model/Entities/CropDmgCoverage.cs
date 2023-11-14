//------------------------------------------------------------------------------
// <copyright file="CropDmgCoverage.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//-----------------------------------------------------------------------------
namespace ADIDAS.Model.Entities
{
    using System;

    /// <summary>
    /// CropDmgCoverage.
    /// </summary>
    public class CropDmgCoverage
    {
        /// <summary>
        /// Gets or Sets Damage_id.
        /// </summary>
        public int Damage_id { get; set; }

        /// <summary>
        /// Gets or Sets Panchayat_id.
        /// </summary>
        public int Panchayat_id { get; set; }

        /// <summary>
        /// Gets or Sets Damage_reason_id.
        /// </summary>
        public int Damage_reason_id { get; set; }

        /// <summary>
        /// Gets or Sets Net_sown_area.
        /// </summary>
        public decimal Net_sown_area { get; set; }

        /// <summary>
        /// Gets or Sets Irrigated_area.
        /// </summary>
        public decimal Irrigated_area { get; set; }

        /// <summary>
        /// Gets or Sets Nonirrigated_area.
        /// </summary>
        public decimal Nonirrigated_area { get; set; }

        /// <summary>
        /// Gets or Sets Total_area.
        /// </summary>
        public decimal Total_area { get; set; }

        /// <summary>
        /// Gets or Sets Perennial_horti.
        /// </summary>
        public decimal Perennial_horti { get; set; }

        /// <summary>
        /// Gets or Sets Perennial_sugarcane.
        /// </summary>
        public decimal Perennial_sugarcane { get; set; }

        /// <summary>
        /// Gets or Sets Grand_total_area.
        /// </summary>
        public decimal Grand_total_area { get; set; }

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
        /// Gets or Sets AC_Submitted_date.
        /// </summary>
        public DateTime AC_Submitted_date { get; set; }

        /// <summary>
        /// Gets or Sets AC_Submitted_userid.
        /// </summary>
        public int AC_Submitted_userid { get; set; }

        /// <summary>
        /// Gets or Sets BAO_Approval_flag.
        /// </summary>
        public string BAO_Approval_flag { get; set; }

        /// <summary>
        /// Gets or Sets BAO_Approved_date.
        /// </summary>
        public DateTime BAO_Approved_date { get; set; }

        /// <summary>
        /// Gets or Sets BAO_Approved_userid.
        /// </summary>
        public int BAO_Approved_userid { get; set; }

        /// <summary>
        /// Gets or Sets DAO_Approval_flag.
        /// </summary>
        public string DAO_Approval_flag { get; set; }

        /// <summary>
        /// Gets or Sets DAO_Approved_date.
        /// </summary>
        public DateTime DAO_Approved_date { get; set; }

        /// <summary>
        /// Gets or Sets DAO_Approved_userid.
        /// </summary>
        public int DAO_Approved_userid { get; set; }

        /// <summary>
        /// Gets or Sets DM_Final_Approval_Flag.
        /// </summary>
        public string DM_Final_Approval_Flag { get; set; }
    }
}
