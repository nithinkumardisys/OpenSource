//------------------------------------------------------------------------------
// <copyright file="DTOSeedUserInput.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------

namespace ADIDAS.Model.DTO
{
    using System.Collections.Generic;

    /// <summary>
    /// Seed User Input.
    /// </summary>
    public class DtoSeedUserInput
    {
        /// <summary>
        /// Gets or Sets DtoSeedUserInput.
        /// </summary>
        public DtoSeedUserInput()
        {
            this.Varieties = new List<DtoSeedVariety>();
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
        /// Gets or Sets Varieties.
        /// </summary>
        public List<DtoSeedVariety> Varieties { get; set; }
    }
}
