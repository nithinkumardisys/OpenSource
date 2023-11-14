﻿//------------------------------------------------------------------------------
// <copyright file="PostSubmitProductivityModel.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------
namespace ADIDAS.Model.DTO
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Post Submit Productivity Model.
    /// </summary>
    public class PostSubmitProductivityModel
    {
        /// <summary>
        /// Gets or Sets PostSubmitProductivityModel.
        /// </summary>
        public PostSubmitProductivityModel()
        {
            this.Croplist = new List<Croplist>();
        }

        /// <summary>
        /// Gets or Sets District_id.
        /// </summary>
        public int District_id { get; set; }

        /// <summary>
        /// Gets or Sets District_name.
        /// </summary>
        public string District_name { get; set; }

        /// <summary>
        /// Gets or Sets Croplist.
        /// </summary>
        public List<Croplist> Croplist { get; set; }
    }

    /// <summary>
    /// Crop list.
    /// </summary>
    public class Croplist
    {
        /// <summary>
        /// Gets or Sets Crop_id.
        /// </summary>
        public int Crop_id { get; set; }

        /// <summary>
        /// Gets or Sets Crop_category.
        /// </summary>
        public string Crop_category { get; set; }

        /// <summary>
        /// Gets or Sets Crop_name.
        /// </summary>
        public string Crop_name { get; set; }

        /// <summary>
        /// Gets or Sets Season_id.
        /// </summary>
        public int Season_id { get; set; }

        /// <summary>
        /// Gets or Sets Season_name.
        /// </summary>
        public string Season_name { get; set; }

        /// <summary>
        /// Gets or Sets Productivity.
        /// </summary>
        public decimal Productivity { get; set; }

        /// <summary>
        /// Gets or Sets Is_Deleted.
        /// </summary>
        public int Is_Deleted { get; set; }

        /// <summary>
        /// Gets or Sets Rec_created_date.
        /// </summary>
        public DateTime Rec_created_date { get; set; }

        /// <summary>
        /// Gets or Sets Rec_updated_date.
        /// </summary>
        public DateTime Rec_updated_date { get; set; }

        /// <summary>
        /// Gets or Sets Rec_updated_userid.
        /// </summary>
        public int Rec_updated_userid { get; set; }

        /// <summary>
        /// Gets or Sets Rec_created_userid.
        /// </summary>
        public int Rec_created_userid { get; set; }
    }
}
