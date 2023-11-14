//------------------------------------------------------------------------------
// <copyright file="CreativeIdeasToReviewModel.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//-------------------------------------------------------------------------------
namespace ADIDAS.Model.DTO
{
    using System;

    /// <summary>
    /// Creative Ideas To Review Model.
    /// </summary>
    public class CreativeIdeasToReviewModel
    {
        /// <summary>
        /// Gets or Sets Name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or Sets District_name.
        /// </summary>
        public string District_name { get; set; }

        /// <summary>
        /// Gets or Sets Designation.
        /// </summary>
        public string Designation { get; set; }

        /// <summary>
        /// Gets or Sets Department.
        /// </summary>
        public string Department { get; set; }

        /// <summary>
        /// Gets or Sets District_Names.
        /// </summary>
        public string District_Names { get; set; }

        /// <summary>
        /// Gets or Sets Block_Names.
        /// </summary>
        public string Block_Names { get; set; }

        /// <summary>
        /// Gets or Sets Panchayat_Names.
        /// </summary>
        public string Panchayat_Names { get; set; }

        /// <summary>
        /// Gets or Sets Idea_id.
        /// </summary>
        public int Idea_id { get; set; }

        /// <summary>
        /// Gets or Sets Idea_Category.
        /// </summary>
        public string Idea_Category { get; set; }

        /// <summary>
        /// Gets or Sets Idea_Type.
        /// </summary>
        public string Idea_Type { get; set; }

        /// <summary>
        /// Gets or Sets Quarter.
        /// </summary>
        public string Quarter { get; set; }

        /// <summary>
        /// Gets or Sets Idea_Description.
        /// </summary>
        public string Idea_Description { get; set; }

        /// <summary>
        /// Gets or Sets Rec_created_date.
        /// </summary>
        public DateTime Rec_created_date { get; set; }

        /// <summary>
        /// Gets or Sets Shortlisted_flg.
        /// </summary>
        public string Shortlisted_flg { get; set; }

        /// <summary>
        /// Gets or Sets Comment.
        /// </summary>
        public string Comment { get; set; }
    }
}
