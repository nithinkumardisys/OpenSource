//------------------------------------------------------------------------------
// <copyright file="DTOFarmerContractingDetails.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------
namespace ADIDAS.Model.DTO
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Farmer Contracting Details.
    /// </summary>
    public class DtoFarmerContractingDetails
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DtoFarmerContractingDetails"/> class.
        /// </summary>
        public DtoFarmerContractingDetails()
        {
            this.Crops = new List<DtoFarmerContractCrops>();
        }

        /// <summary>
        /// Gets or Sets Season_Id.
        /// </summary>
        public int Season_Id { get; set; }

        /// <summary>
        /// Gets or Sets Season_name.
        /// </summary>
        public string Season_name { get; set; }

        /// <summary>
        /// Gets or Sets Farmer_name.
        /// </summary>
        public string Farmer_name { get; set; }

        /// <summary>
        /// Gets or Sets Farmer_dbt_reg_no.
        /// </summary>
        public string Farmer_dbt_reg_no { get; set; }

        /// <summary>
        /// Gets or Sets Farmer_phone_num.
        /// </summary>
        public string Farmer_phone_num { get; set; }

        /// <summary>
        /// Gets or Sets Panchayat_Id.
        /// </summary>
        public int Panchayat_Id { get; set; }

        /// <summary>
        /// Gets or Sets Panchayat_name.
        /// </summary>
        public string Panchayat_name { get; set; }

        /// <summary>
        /// Gets or Sets Farmer_id.
        /// </summary>
        public int Farmer_id { get; set; }

        /// <summary>
        /// Gets or Sets Interested_In_Contract_flg.
        /// </summary>
        public string Interested_In_Contract_flg { get; set; }

        /// <summary>
        /// Gets or Sets Rec_updated_date.
        /// </summary>
        public DateTime Rec_updated_date { get; set; }

        /// <summary>
        /// Gets or Sets Rec_updated_userid.
        /// </summary>
        public int Rec_updated_userid { get; set; }

        /// <summary>
        /// Gets or Sets Rec_created_date.
        /// </summary>
        public DateTime Rec_created_date { get; set; }

        /// <summary>
        /// Gets or Sets Rec_created_userid.
        /// </summary>
        public int Rec_created_userid { get; set; }

        /// <summary>
        /// Gets or Sets Crops.
        /// </summary>
        public List<DtoFarmerContractCrops> Crops { get; set; }
    }
}
