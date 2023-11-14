// <copyright file="PesticideInfo.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//----------------------------------------------------------------------------
namespace ADIDAS.Model.Entities
{
    using System;

    /// <summary>
    /// PesticideInfo.
    /// </summary>
    public class PesticideInfo
    {
        /// <summary>
        /// Gets or Sets Firm_Name.
        /// </summary>
        public string Firm_Name { get; set; }

        /// <summary>
        /// Gets or Sets License_No.
        /// </summary>
        public string License_No { get; set; }

        /// <summary>
        /// Gets or Sets Licence_Date.
        /// </summary>
        public DateTime? Licence_Date { get; set; }

        /// <summary>
        /// Gets or Sets District_lg_code.
        /// </summary>
        public int? District_lg_code { get; set; }

        /// <summary>
        /// Gets or Sets District_name.
        /// </summary>
        public string District_name { get; set; }

        /// <summary>
        /// Gets or Sets Rec_created_userid.
        /// </summary>
        public int? Rec_created_userid { get; set; }

        /// <summary>
        /// Gets or Sets Rec_created_date.
        /// </summary>
        public DateTime? Rec_created_date { get; set; }

        /// <summary>
        /// Gets or Sets Rec_updated_userid.
        /// </summary>
        public int? Rec_updated_userid { get; set; }

        /// <summary>
        /// Gets or Sets Rec_updated_date.
        /// </summary>
        public DateTime? Rec_updated_date { get; set; }
    }
}
