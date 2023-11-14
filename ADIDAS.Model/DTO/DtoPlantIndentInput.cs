//------------------------------------------------------------------------------
// <copyright file="DtoPlantIndentInput.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------

namespace ADIDAS.Model.DTO
{
    using System.Collections.Generic;

    /// <summary>
    /// DtoPlantIndentInput.
    /// </summary>
    public class DtoPlantIndentInput
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DtoPlantIndentInput"/> class.
        /// Gets or Sets DtoPlantIndentInput.
        /// </summary>
        public DtoPlantIndentInput()
        {
            this.Varieties = new List<DtoSeedVarietyPlantIndent>();
        }

        /// <summary>
        /// Gets or Sets Season_id.
        /// </summary>
        public int Season_id { get; set; }

        /// <summary>
        /// Gets or Sets Season_name.
        /// </summary>
        public string Season_name { get; set; }

        /// <summary>
        /// Gets or Sets Crop_name.
        /// </summary>
        public string Crop_name { get; set; }

        /// <summary>
        /// Gets or Sets Crop_id.
        /// </summary>
        public int Crop_id { get; set; }

        /// <summary>
        /// Gets or Sets Panchayat_name.
        /// </summary>
        public string Panchayat_name { get; set; }

        /// <summary>
        /// Gets or Sets Panchayat_id.
        /// </summary>
        public int Panchayat_id { get; set; }

        /// <summary>
        /// Gets or Sets Block_id.
        /// </summary>
        public int Block_id { get; set; }

        /// <summary>
        /// Gets or Sets Block_name.
        /// </summary>
        public string Block_name { get; set; }

        /// <summary>
        /// Gets or Sets District_id.
        /// </summary>
        public int District_id { get; set; }

        /// <summary>
        /// Gets or Sets Variety_flg.
        /// </summary>
        public string Variety_flg { get; set; }

        /// <summary>
        /// Gets or Sets Varieties.
        /// </summary>
        public List<DtoSeedVarietyPlantIndent> Varieties { get; set; }
    }
}
