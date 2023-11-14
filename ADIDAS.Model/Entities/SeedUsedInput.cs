//----------------------------------------------------------------------------
// <copyright file="SeedUsedInput.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//----------------------------------------------------------------------------
namespace ADIDAS.Model.Entities
{
    using System.Collections.Generic;

    /// <summary>
    /// SeedUsedInput.
    /// </summary>
    public class SeedUsedInput
    {
        /// <summary>
        /// SeedUsedInput.
        /// </summary>
        public SeedUsedInput()
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
        /// Gets or Sets Crop_category.
        /// </summary>
        public string Crop_category { get; set; }

        /// <summary>
        /// Gets or Sets Response_status.
        /// </summary>
        public string Response_status { get; set; }

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
