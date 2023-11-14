//------------------------------------------------------------------------------
// <copyright file="DtoSeedIndentInput.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------

namespace ADIDAS.Model.DTO
{
    using System.Collections.Generic;

    /// <summary>
    /// DtoSeedIndentInput.
    /// </summary>
    public class DtoSeedIndentInput
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DtoSeedIndentInput"/> class.
        /// Gets or Sets DtoSeedIndentInput.
        /// </summary>
        public DtoSeedIndentInput()
        {
            this.Varieties = new List<DtoSeedVarietyIndent>();
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
        /// Gets or Sets Varieties.
        /// </summary>
        public List<DtoSeedVarietyIndent> Varieties { get; set; }
    }
}
