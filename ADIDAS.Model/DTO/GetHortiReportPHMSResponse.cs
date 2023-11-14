//------------------------------------------------------------------------------
// <copyright file="GetHortiReportPHMSResponse.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------
namespace ADIDAS.Model.DTO
{
    using System;

    /// <summary>
    /// Get Horti Report Phms Response.
    /// </summary>
    public class GetHortiReportPhmsResponse
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
        /// Gets or Sets District_name.
        /// </summary>
        public string District_name { get; set; }

        /// <summary>
        /// Gets or Sets Panchayat_id.
        /// </summary>
        public int Panchayat_id { get; set; }

        /// <summary>
        /// Gets or Sets Panchayat_name.
        /// </summary>
        public string Panchayat_name { get; set; }

        /// <summary>
        /// Gets or Sets Block_id.
        /// </summary>
        public int Block_id { get; set; }

        /// <summary>
        /// Gets or Sets Block_name.
        /// </summary>
        public string Block_name { get; set; }

        /// <summary>
        /// Gets or Sets Struct_type.
        /// </summary>
        public string Struct_type { get; set; }

        /// <summary>
        /// Gets or Sets Facility_id.
        /// </summary>
        public int Facility_id { get; set; }

        /// <summary>
        /// Gets or Sets Facility_name.
        /// </summary>
        public string Facility_name { get; set; }

        /// <summary>
        /// Gets or Sets Facility_add.
        /// </summary>
        public string Facility_add { get; set; }

        /// <summary>
        /// Gets or Sets Capacity.
        /// </summary>
        public string Capacity { get; set; }

        /// <summary>
        /// Gets or Sets Is_facility_added.
        /// </summary>
        public string Is_facility_added { get; set; }

        /// <summary>
        /// Gets or Sets Rec_CREATEd_ts.
        /// </summary>
        public DateTime Rec_CREATEd_ts { get; set; }

        /// <summary>
        /// Gets or Sets Rec_created_userid.
        /// </summary>
        public int Rec_created_userid { get; set; }
    }
}
