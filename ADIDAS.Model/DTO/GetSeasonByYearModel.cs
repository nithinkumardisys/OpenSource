//------------------------------------------------------------------------------
// <copyright file="GetSeasonByYearModel.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------

namespace ADIDAS.Model.DTO
{
    /// <summary>
    /// Get Season By Year Model.
    /// </summary>
    public class GetSeasonByYearModel
    {
        /// <summary>
        /// Gets or Sets Season_id.
        /// </summary>
        public int Season_id { get; set; }

        /// <summary>
        /// Gets or Sets Season_name.
        /// </summary>
        public string Season_name { get; set; }

        /// <summary>
        /// Gets or Sets Is_Village_Included.
        /// </summary>
        public string Is_Village_Included { get; set; }
    }

    /// <summary>
    /// Get Crop By SeasonId Model.
    /// </summary>
    public class GetCropBySeasonIdModel
    {
        /// <summary>
        /// Gets or Sets Crop_id.
        /// </summary>
        public int Crop_id { get; set; }

        /// <summary>
        /// Gets or Sets Crop_name.
        /// </summary>
        public string Crop_name { get; set; }

        /// <summary>
        /// Gets or Sets crop_category.
        /// </summary>
        public string Crop_category { get; set; }
    }
}
