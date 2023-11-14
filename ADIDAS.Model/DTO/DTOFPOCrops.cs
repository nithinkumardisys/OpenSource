//------------------------------------------------------------------------------
// <copyright file="DTOFPOCrops.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------
namespace ADIDAS.Model.DTO
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// FpoCrops.
    /// </summary>
    public class DtoFpoCrops
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DtoFpoCrops"/> class.
        ///  </summary>
        public DtoFpoCrops()
        {
            this.Crops = new List<CropsDtofpo>();
        }

        /// <summary>
        /// Gets or Sets Fpo_id.
        /// </summary>
        public int Fpo_id { get; set; }

        /// <summary>
        /// Gets or Sets Fpo_name.
        /// </summary>
        public string Fpo_name { get; set; }

        /// <summary>
        /// Gets or Sets Fpo_contact_person.
        /// </summary>
        public string Fpo_contact_person { get; set; }

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

        /// <summary>
        /// Gets or Sets Crops.
        /// </summary>
        public List<CropsDtofpo> Crops { get; set; }
    }
}
