//----------------------------------------------------------------------------
// <copyright file="SeedPerformance.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//----------------------------------------------------------------------------
namespace ADIDAS.Model.Entities
{
    using System;

    public class SeedPerformance
    {
        /// <summary>
        /// Gets or Sets Application_no.
        /// </summary>
        public long Application_no { get; set; }

        /// <summary>
        /// Gets or Sets Area_sown.
        /// </summary>
        public decimal? Area_sown { get; set; }

        /// <summary>
        /// Gets or Sets Production.
        /// </summary>
        public decimal? Production { get; set; }

        /// <summary>
        /// Gets or Sets Productivity.
        /// </summary>
        public decimal? Productivity { get; set; }

        /// <summary>
        /// Gets or Sets Sown_reported_dt.
        /// </summary>
        public DateTime? Sown_reported_dt { get; set; }

        /// <summary>
        /// Gets or Sets Sown_latitude.
        /// </summary>
        public string Sown_latitude { get; set; }

        /// <summary>
        /// Gets or Sets Sown_longitude.
        /// </summary>
        public string Sown_longitude { get; set; }

        /// <summary>
        /// Gets or Sets Sown_submitted_userid.
        /// </summary>
        public int? Sown_submitted_userid { get; set; }

        /// <summary>
        /// Gets or Sets Harvest_reported_dt.
        /// </summary>
        public DateTime? Harvest_reported_dt { get; set; }

        /// <summary>
        /// Gets or Sets Harvest_latitude.
        /// </summary>
        public string Harvest_latitude { get; set; }

        /// <summary>
        /// Gets or Sets Harvest_longitude.
        /// </summary>
        public string Harvest_longitude { get; set; }

        /// <summary>
        /// Gets or Sets Harvest_submitted_userid.
        /// </summary>
        public int? Harvest_submitted_userid { get; set; }

        /// <summary>
        /// Gets or Sets SownImageData.
        /// </summary>
        public string SownImageData { get; set; }

        /// <summary>
        /// Gets or Sets HarvestImageData.
        /// </summary>
        public string HarvestImageData { get; set; }

        /// <summary>
        /// Gets or Sets Sown_img_file_location.
        /// </summary>
        public string Sown_img_file_location { get; set; }

        /// <summary>
        /// Gets or Sets Sown_img_file_name.
        /// </summary>
        public string Sown_img_file_name { get; set; }

        /// <summary>
        /// Gets or Sets Harvest_img_file_location.
        /// </summary>
        public string Harvest_img_file_location { get; set; }

        /// <summary>
        /// Gets or Sets Harvest_img_file_name.
        /// </summary>
        public string Harvest_img_file_name { get; set; }
    }
}
