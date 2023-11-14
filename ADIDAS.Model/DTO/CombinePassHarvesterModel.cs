//------------------------------------------------------------------------------
// <copyright file="CombinePassHarvesterModel.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------
namespace ADIDAS.Model.DTO
{
    using System;

    /// <summary>
    /// CombinePassHarvesterModel.
    /// </summary>
    public class CombinePassHarvesterModel
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
        /// Gets or Sets District_id.
        /// </summary>
        public int District_id { get; set; }

        /// <summary>
        /// Gets or Sets District_name.
        /// </summary>
        public string District_name { get; set; }

        /// <summary>
        /// Gets or Sets Machine_type_id.
        /// </summary>
        public int Machine_type_id { get; set; }

        /// <summary>
        /// Gets or Sets Machine_type_name.
        /// </summary>
        public string Machine_type_name { get; set; }

        /// <summary>
        /// Gets or Sets Applicant_name.
        /// </summary>
        public string Applicant_name { get; set; }

        /// <summary>
        /// Gets or Sets Mobile_number.
        /// </summary>
        public string Mobile_number { get; set; }

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
        /// Gets or Sets Village_name.
        /// </summary>
        public string Village_name { get; set; }

        /// <summary>
        /// Gets or Sets Combine_harvester_number.
        /// </summary>
        public string Combine_harvester_number { get; set; }

        /// <summary>
        /// Gets or Sets Issue_date.
        /// </summary>
        public DateTime? Issue_date { get; set; }

        /// <summary>
        /// Gets or Sets Rec_created_userid.
        /// </summary>
        public int Rec_created_userid { get; set; }

        /// <summary>
        /// Gets or Sets Rec_created_date.
        /// </summary>
        public DateTime? Rec_created_date { get; set; }
    }
}
