//----------------------------------------------------------------------------
// <copyright file="SeedIndentInput.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//----------------------------------------------------------------------------
namespace ADIDAS.Model.Entities
{
    using System.Collections.Generic;

    /// <summary>
    /// SeedIndentInput.
    /// </summary>
    public class SeedIndentInput
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SeedIndentInput"/> class.
        /// </summary>
        public SeedIndentInput()
        {
            this.SeedVariety = new List<SeedVariety>();
        }

        /// <summary>
        /// Gets or Sets Season_id.
        /// </summary>
        public int Season_id { get; set; }

        /// <summary>
        /// Gets or Sets Crop_id.
        /// </summary>
        public int Crop_id { get; set; }

        /// <summary>
        /// Gets or Sets District_id.
        /// </summary>
        public int District_id { get; set; }

        /// <summary>
        /// Gets or Sets Panchayat_id.
        /// </summary>
        public int Panchayat_id { get; set; }

        /// <summary>
        /// Gets or Sets UserId.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or Sets SeedVariety.
        /// </summary>
        public List<SeedVariety> SeedVariety { get; set; }
    }
}
