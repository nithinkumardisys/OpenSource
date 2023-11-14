//------------------------------------------------------------------------------
// <copyright file="DTOFacilityDetails.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------
namespace ADIDAS.Model.DTO
{
    using System;

    /// <summary>
    /// FacilityDetails.
    /// </summary>
    public class DtoFacilityDetails
    {
        /// <summary>
        /// Gets or Sets Old_facility_name.
        /// </summary>
        public string Old_facility_name { get; set; }

        /// <summary>
        /// Gets or Sets Old_address.
        /// </summary>
        public string Old_address { get; set; }

        /// <summary>
        /// Gets or Sets Old_capacity.
        /// </summary>
        public string Old_capacity { get; set; }

        /// <summary>
        /// Gets or Sets Facility_name.
        /// </summary>
        public string Facility_name { get; set; }

        /// <summary>
        /// Gets or Sets Facility_add.
        /// </summary>
        public string Facility_add { get; set; }

        /// <summary>
        /// Gets or Sets Facility_id.
        /// </summary>
        public int Facility_id { get; set; }

        /// <summary>
        /// Gets or Sets Capacity.
        /// </summary>
        public string Capacity { get; set; }

        /// <summary>
        /// Gets or Sets Panchayat_Id.
        /// </summary>
        public int Panchayat_Id { get; set; }

        /// <summary>
        /// Gets or Sets Panchayat_name.
        /// </summary>
        public string Panchayat_name { get; set; }

        /// <summary>
        /// Gets or Sets Structure_Id.
        /// </summary>
        public int Structure_Id { get; set; }

        /// <summary>
        /// Gets or Sets Approval_status.
        /// </summary>
        public string Approval_status { get; set; }

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
        /// Gets or Sets District_Id.
        /// </summary>
        public int District_Id { get; set; }

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
        /// Gets or Sets Structure_Name.
        /// </summary>
        public string Structure_Name { get; set; }

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
}
