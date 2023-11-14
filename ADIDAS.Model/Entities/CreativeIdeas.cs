//------------------------------------------------------------------------------
// <copyright file="CreativeIdeas.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------
namespace ADIDAS.Model.Entities
{
    /// <summary>
    /// Creative Ideas.
    /// </summary>
    public class CreativeIdeas
    {
        /// <summary>
        /// Gets or Sets User_Id.
        /// </summary>
        public string User_Id { get; set; }

        /// <summary>
        /// Gets or Sets Idea_Type.
        /// </summary>
        public string Idea_Type { get; set; }

        /// <summary>
        /// Gets or Sets Idea_Category.
        /// </summary>
        public string Idea_Category { get; set; }

        /// <summary>
        /// Gets or Sets Idea_Description.
        /// </summary>
        public string Idea_Description { get; set; }
    }
}
