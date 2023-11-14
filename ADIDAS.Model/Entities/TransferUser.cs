// <copyright file="TransferUser.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//----------------------------------------------------------------------------
namespace ADIDAS.Model.Entities
{
    using System;

    /// <summary>
    /// TransferUser.
    /// </summary>
    public class TransferUser
    {
        /// <summary>
        /// Gets or Sets User_id.
        /// </summary>
        public int? User_id { get; set; }

        /// <summary>
        /// Gets or Sets First_name.
        /// </summary>
        public string First_name { get; set; }

        /// <summary>
        /// Gets or Sets Last_name.
        /// </summary>
        public string Last_name { get; set; }

        /// <summary>
        /// Gets or Sets Phone_num.
        /// </summary>
        public string Phone_num { get; set; }

        /// <summary>
        /// Gets or Sets Division_id.
        /// </summary>
        public string Division_id { get; set; }

        /// <summary>
        /// Gets or Sets Sub_Division_id.
        /// </summary>
        public string Sub_Division_id { get; set; }

        /// <summary>
        /// Gets or Sets District_id.
        /// </summary>
        public string District_id { get; set; }

        /// <summary>
        /// Gets or Sets Block_id.
        /// </summary>
        public string Block_id { get; set; }

        /// <summary>
        /// Gets or Sets Panchayat_id.
        /// </summary>
        public string Panchayat_id { get; set; }

        /// <summary>
        /// Gets or Sets Data_entry_flg.
        /// </summary>
        public string Data_entry_flg { get; set; }

        /// <summary>
        /// Gets or Sets Applied_user_id.
        /// </summary>
        public int? Applied_user_id { get; set; }

        /// <summary>
        /// Gets or Sets Date_applied.
        /// </summary>
        public DateTime? Date_applied { get; set; }
    }
}
