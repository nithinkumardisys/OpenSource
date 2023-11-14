//----------------------------------------------------------------------------
// <copyright file="Machinery.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//----------------------------------------------------------------------------
namespace ADIDAS.Model.Entities
{
    using System;

    /// <summary>
    /// Machinery.
    /// </summary>
    public class Machinery
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
        /// Gets or Sets Panchayat_ID.
        /// </summary>
        public int Panchayat_ID { get; set; }

        /// <summary>
        /// Gets or Sets Panchayat_name.
        /// </summary>
        public string Panchayat_name { get; set; }

        /// <summary>
        /// Gets or Sets Quantity.
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Gets or Sets Machinery_ID.
        /// </summary>
        public int Machinery_ID { get; set; }

        /// <summary>
        /// Gets or Sets Machinery_name.
        /// </summary>
        public string Machinery_name { get; set; }

        /// <summary>
        /// Gets or Sets Rec_created_userid.
        /// </summary>
        public int Rec_created_userid { get; set; }

        /// <summary>
        /// Gets or Sets Rec_created_date.
        /// </summary>
        public DateTime? Rec_created_date { get; set; }

        /// <summary>
        /// Gets or Sets Submitted_by.
        /// </summary>
        public string Submitted_by { get; set; }
    }

    /// <summary>
    /// AgriAssetNoFacilityData.
    /// </summary>
    public class AgriAssetNoFacilityData
    {
        /// <summary>
        /// Gets or Sets Panchayat_id.
        /// </summary>
        public int Panchayat_id { get; set; }

        /// <summary>
        /// Gets or Sets Is_facility_added.
        /// </summary>
        public string Is_facility_added { get; set; }

        /// <summary>
        /// Gets or Sets Period.
        /// </summary>
        public string Period { get; set; }

        /// <summary>
        /// Gets or Sets Rec_created_userid.
        /// </summary>
        public int Rec_created_userid { get; set; }

        /// <summary>
        /// Gets or Sets Rec_updated_userid.
        /// </summary>
        public int Rec_updated_userid { get; set; }

        /// <summary>
        /// Gets or Sets Rec_created_ts.
        /// </summary>
        public DateTime? Rec_created_ts { get; set; }

        /// <summary>
        /// Gets or Sets Rec_updated_ts.
        /// </summary>
        public DateTime? Rec_updated_ts { get; set; }

        /// <summary>
        /// Gets or Sets Block_id.
        /// </summary>
        public int Block_id { get; set; }

        /// <summary>
        /// Gets or Sets Is_facility_added_historical.
        /// </summary>
        public string Is_facility_added_historical { get; set; }
    }
}