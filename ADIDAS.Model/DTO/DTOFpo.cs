//------------------------------------------------------------------------------
// <copyright file="DTOFpo.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------
namespace ADIDAS.Model.DTO
{
    using System;

    /// <summary>
    /// Fpo.
    /// </summary>
    public class DtoFpo
    {
        /// <summary>
        /// Gets or Sets Fpo_id.
        /// </summary>
        public int Fpo_id { get; set; }

        /// <summary>
        /// Gets or Sets Fpo_name.
        /// </summary>
        public string Fpo_name { get; set; }

        /// <summary>
        /// Gets or Sets Fpo_phone_num.
        /// </summary>
        public string Fpo_phone_num { get; set; }

        /// <summary>
        /// Gets or Sets Fpo_type.
        /// </summary>
        public string Fpo_type { get; set; }

        /// <summary>
        /// Gets or Sets Fpo_contact_person.
        /// </summary>
        public string Fpo_contact_person { get; set; }

        /// <summary>
        /// Gets or Sets Rec_updated_userId.
        /// </summary>
        public int Rec_updated_userId { get; set; }

        /// <summary>
        /// Gets or Sets Crops.
        /// </summary>
        public string Crops { get; set; }

        /// <summary>
        /// Gets or Sets District_ID.
        /// </summary>
        public int District_ID { get; set; }

        /// <summary>
        /// Gets or Sets District_NAME.
        /// </summary>
        public string District_NAME { get; set; }

        /// <summary>
        /// Gets or Sets Rec_updated_date.
        /// </summary>
        public DateTime Rec_updated_date { get; set; }

        /// <summary>
        /// Gets or Sets Panchayat_id.
        /// </summary>
        public int Panchayat_id { get; set; }

        /// <summary>
        /// Gets or Sets Interested_In_Contract_flg.
        /// </summary>
        public string Interested_In_Contract_flg { get; set; }

        /// <summary>
        /// Gets or Sets Rec_created_date.
        /// </summary>
        public DateTime Rec_created_date { get; set; }

        /// <summary>
        /// Gets or Sets Rec_created_userid.
        /// </summary>
        public int Rec_created_userid { get; set; }
    }

    /// <summary>
    /// CropsFpo.
    /// </summary>
    public class DtoCropsFpo
    {
        /// <summary>
        /// Gets or Sets Crop_Id.
        /// </summary>
        public int Crop_Id { get; set; }

        /// <summary>
        /// Gets or Sets Fpo_id.
        /// </summary>
        public int Fpo_id { get; set; }

        /// <summary>
        /// Gets or Sets Panchayat_id.
        /// </summary>
        public int Panchayat_id { get; set; }
    }
}
