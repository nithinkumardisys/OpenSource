//------------------------------------------------------------------------------
// <copyright file="UpdateIdeasModel.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------
namespace ADIDAS.Model.DTO
{
    using System;

    /// <summary>
    /// Update Ideas Model.
    /// </summary>
    public class UpdateIdeasModel
    {
        /// <summary>
        /// Gets or Sets Idea_id.
        /// </summary>
        public int Idea_id { get; set; }

        /// <summary>
        /// Gets or Sets User_Id.
        /// </summary>
        public int User_Id { get; set; }

        /// <summary>
        /// Gets or Sets Start_date.
        /// </summary>
        public DateTime Start_date { get; set; }

        /// <summary>
        /// Gets or Sets End_date.
        /// </summary>
        public DateTime End_date { get; set; }

        /// <summary>
        /// Gets or Sets Shortlisted_flg.
        /// </summary>
        public string Shortlisted_flg { get; set; }

        /// <summary>
        /// Gets or Sets District_Id.
        /// </summary>
        public int District_Id { get; set; }

        /// <summary>
        /// Gets or Sets Comment.
        /// </summary>
        public string Comment { get; set; }
    }
}
