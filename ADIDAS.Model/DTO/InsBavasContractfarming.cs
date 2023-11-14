//------------------------------------------------------------------------------
// <copyright file="InsBavasContractfarming.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------
namespace ADIDAS.Model.DTO
{
    /// <summary>
    /// Ins Bavas Contract farming.
    /// </summary>
    public class InsBavasContractfarming
    {
        /// <summary>
        /// Gets or Sets Season_id.
        /// </summary>
        public int Season_id { get; set; }

        /// <summary>
        /// Gets or Sets Panchayat_id.
        /// </summary>
        public int Panchayat_id { get; set; }

        /// <summary>
        /// Gets or Sets Farmer_Name.
        /// </summary>
        public string Farmer_Name { get; set; }

        /// <summary>
        /// Gets or Sets Farmer_dbt_reg_no.
        /// </summary>
        public string Farmer_dbt_reg_no { get; set; }

        /// <summary>
        /// Gets or Sets Old_farmer_dbt_reg_no.
        /// </summary>
        public string Old_farmer_dbt_reg_no { get; set; }

        /// <summary>
        /// Gets or Sets Farmer_phone_num.
        /// </summary>
        public string Farmer_phone_num { get; set; }

        /// <summary>
        /// Gets or Sets Interested_In_Contract_flg.
        /// </summary>
        public string Interested_In_Contract_flg { get; set; }

        /// <summary>
        /// Gets or Sets Crop_ID.
        /// </summary>
        public string Crop_ID { get; set; }

        /// <summary>
        /// Gets or Sets Rec_created_userid.
        /// </summary>
        public int Rec_created_userid { get; set; }

        /// <summary>
        /// Gets or Sets Rec_updated_userid.
        /// </summary>
        public int Rec_updated_userid { get; set; }
    }
}
