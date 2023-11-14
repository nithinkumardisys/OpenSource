//------------------------------------------------------------------------------
// <copyright file="GetHortiReportPHMSModel.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------

namespace ADIDAS.Model.DTO
{
    using System;

    /// <summary>
    /// Get Horti Report Phms Model.
    /// </summary>
    public class GetHortiReportPhmsModel
    {
        /// <summary>
        /// Gets or Sets User_Id.
        /// </summary>
        public string User_Id { get; set; }

        /// <summary>
        /// Gets or Sets Query_name.
        /// </summary>
        public string Query_name { get; set; }

        /// <summary>
        /// Gets or Sets Year.
        /// </summary>
        public int Year { get; set; }

        /// <summary>
        /// Gets or Sets Month.
        /// </summary>
        public string Month { get; set; }

        /// <summary>
        /// Gets or Sets Crop_activity.
        /// </summary>
        public string Crop_activity { get; set; }

        /// <summary>
        /// Gets or Sets Struct_type.
        /// </summary>
        public string Struct_type { get; set; }

        /// <summary>
        /// Gets or Sets Date.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets or Sets Approval_Status.
        /// </summary>
        public string Approval_Status { get; set; }

        /// <summary>
        /// Gets or Sets District_ID.
        /// </summary>
        public string District_ID { get; set; }

        /// <summary>
        /// Gets or Sets Block_ID.
        /// </summary>
        public int Block_ID { get; set; }

        /// <summary>
        /// Gets or Sets Panchayat_ID.
        /// </summary>
        public int Panchayat_ID { get; set; }

        /// <summary>
        /// Gets or Sets Crop_id.
        /// </summary>
        public string Crop_id { get; set; }

        /// <summary>
        /// Gets or Sets Stor_Name_Address.
        /// </summary>
        public string Stor_Name_Address { get; set; }

        /// <summary>
        /// Gets or Sets Strg_Id.
        /// </summary>
        public string Strg_Id { get; set; }
    }
}
