//------------------------------------------------------------------------------
// <copyright file="DTOSeedPerformanceAgriculture.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------

namespace ADIDAS.Model.DTO
{
    using System;

    /// <summary>
    /// Seed Performance Agriculture.
    /// </summary>
    public class DtoSeedPerformanceAgriculture
    {
        /// <summary>
        /// Gets or Sets Application_no.
        /// </summary>
        public long Application_no { get; set; }

        /// <summary>
        /// Gets or Sets Registration_no.
        /// </summary>
        public long Registration_no { get; set; }

        /// <summary>
        /// Gets or Sets Panchayat_id.
        /// </summary>
        public int Panchayat_id { get; set; }

        /// <summary>
        /// Gets or Sets Season_id.
        /// </summary>
        public int Season_id { get; set; }

        /// <summary>
        /// Gets or Sets Distribution_dt.
        /// </summary>
        public DateTime? Distribution_dt { get; set; }

        /// <summary>
        /// Gets or Sets Distributed_qty.
        /// </summary>
        public decimal? Distributed_qty { get; set; }

        /// <summary>
        /// Gets or Sets Farmer_name.
        /// </summary>
        public string Farmer_name { get; set; }

        /// <summary>
        /// Gets or Sets Variety.
        /// </summary>
        public string Variety { get; set; }

        /// <summary>
        /// Gets or Sets Seed_type.
        /// </summary>
        public string Seed_type { get; set; }

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

        /// <summary>
        /// Gets or Sets Harvest_submitted_userid.
        /// </summary>
        public int? Harvest_submitted_userid { get; set; }

        /// <summary>
        /// Gets or Sets Submission_status.
        /// </summary>
        public string Submission_status { get; set; }

        /// <summary>
        /// Gets or Sets Scheme_name.
        /// </summary>
        public string Scheme_name { get; set; }

        /// <summary>
        /// Gets or Sets Scheme_type.
        /// </summary>
        public string Scheme_type { get; set; }

        /// <summary>
        /// Gets or Sets ReportedDate.
        /// </summary>
        public DateTime ReportedDate { get; set; }

        /// <summary>
        /// Gets or Sets Crop_season.
        /// </summary>
        public string Crop_season { get; set; }

        /// <summary>
        /// Gets or Sets Panchayat_name.
        /// </summary>
        public string Panchayat_name { get; set; }

        /// <summary>
        /// Gets or Sets SownImageData.
        /// </summary>
        public string SownImageData { get; set; }

        /// <summary>
        /// Gets or Sets HarvestImageData.
        /// </summary>
        public string HarvestImageData { get; set; }

        /// <summary>
        /// Gets or Sets Flag.
        /// </summary>
        public bool Flag { get; set; }

        /// <summary>
        /// Gets or Sets Phone_num.
        /// </summary>
        public string Phone_num { get; set; }

        /// <summary>
        /// Gets or Sets SeedSownSubmitted.
        /// </summary>
        public bool SeedSownSubmitted { get; set; }

        /// <summary>
        /// Gets or Sets District_id.
        /// </summary>
        public int District_id { get; set; }

        /// <summary>
        /// Gets or Sets Block_id.
        /// </summary>
        public int Block_id { get; set; }

        /// <summary>
        /// Gets or Sets District_name.
        /// </summary>
        public string District_name { get; set; }

        /// <summary>
        /// Gets or Sets Block_name.
        /// </summary>
        public string Block_name { get; set; }

        /// <summary>
        /// Gets or Sets Crop_name.
        /// </summary>
        public string Crop_name { get; set; }
    }
}
