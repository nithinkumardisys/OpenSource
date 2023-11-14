//------------------------------------------------------------------------------
// <copyright file="CropCoverageActualPancht.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------

namespace ADIDAS.Model.Entities
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Crop Coverage Actual Pancht.
    /// </summary>
    public class CropCoverageActualPancht
    {
        /// <summary>
        /// Gets or Sets Reported_Date.
        /// </summary>
        public DateTime? Reported_Date { get; set; }

        /// <summary>
        /// Gets or Sets District_Id.
        /// </summary>
        public int District_Id { get; set; }

        /// <summary>
        /// Gets or Sets District_Name.
        /// </summary>
        public string District_Name { get; set; }

        /// <summary>
        /// Gets or Sets Block_Id.
        /// </summary>
        public int Block_Id { get; set; }

        /// <summary>
        /// Gets or Sets Block_Name.
        /// </summary>
        public string Block_Name { get; set; }

        /// <summary>
        /// Gets or Sets Panchayat_Id.
        /// </summary>
        public int Panchayat_Id { get; set; }

        /// <summary>
        /// Gets or Sets Panchayat_Name.
        /// </summary>
        public string Panchayat_Name { get; set; }

        /// <summary>
        /// Gets or Sets Season_Id.
        /// </summary>
        public int Season_Id { get; set; }

        /// <summary>
        /// Gets or Sets Season_Name.
        /// </summary>
        public string Season_Name { get; set; }

        /// <summary>
        /// Gets or Sets CropList.
        /// </summary>
        public List<CropCvrgEntity> CropList { get; set; }

        /// <summary>
        /// Gets or Sets Existing area.
        /// </summary>
        public decimal? Existing_area { get; set; }

        /// <summary>
        /// Gets or Sets AC_Submitted_date.
        /// </summary>
        public DateTime? AC_Submitted_date { get; set; }

        /// <summary>
        /// Gets or Sets AC_Submitted_userid.
        /// </summary>
        public int? AC_Submitted_userid { get; set; }

        /// <summary>
        /// Gets or Sets BAO_Approval_flag.
        /// </summary>
        public string BAO_Approval_flag { get; set; }

        /// <summary>
        /// Gets or Sets BAO_Approval_Reason.
        /// </summary>
        public string BAO_Approval_Reason { get; set; }

        /// <summary>
        /// Gets or Sets BAO_Approved_date.
        /// </summary>
        public DateTime? BAO_Approved_date { get; set; }

        /// <summary>
        /// Gets or Sets BAO_Approved_userid.
        /// </summary>
        public int? BAO_Approved_userid { get; set; }

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
        /// Gets or Sets Ac_submitted_username.
        /// </summary>
        public string Ac_submitted_username { get; set; }

        /// <summary>
        /// Gets or Sets Bao_approved_username.
        /// </summary>
        public string Bao_approved_username { get; set; }

        /// <summary>
        /// Gets or Sets Dao_approved_username.
        /// </summary>
        public string Dao_approved_username { get; set; }
    }
}
