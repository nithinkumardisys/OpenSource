//------------------------------------------------------------------------------
// <copyright file="InsertHorticultureSeedPerf.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//-----------------------------------------------------------------------------
namespace ADIDAS.Model.Entities
{
    /// <summary>
    /// InsertHorticultureSeedPerf.
    /// </summary>
    public class InsertHorticultureSeedPerf
    {
        /// <summary>
        /// Gets or Sets Seed_Perf_ID.
        /// </summary>
        public int Seed_Perf_ID { get; set; }

        /// <summary>
        /// Gets or Sets Crop_production.
        /// </summary>
        public decimal Crop_production { get; set; }

        /// <summary>
        /// Gets or Sets Crop_productivity.
        /// </summary>
        public decimal Crop_productivity { get; set; }

        /// <summary>
        /// Gets or Sets Latitude.
        /// </summary>
        public string Latitude { get; set; }

        /// <summary>
        /// Gets or Sets Longitude.
        /// </summary>
        public string Longitude { get; set; }

        /// <summary>
        /// Gets or Sets Img_file_location.
        /// </summary>
        public string Img_file_location { get; set; }

        /// <summary>
        /// Gets or Sets Img_file_name.
        /// </summary>
        public string Img_file_name { get; set; }

        /// <summary>
        /// Gets or Sets ImageData.
        /// </summary>
        public string ImageData { get; set; }

        /// <summary>
        /// Gets or Sets Userid.
        /// </summary>
        public int Userid { get; set; }
    }
}
