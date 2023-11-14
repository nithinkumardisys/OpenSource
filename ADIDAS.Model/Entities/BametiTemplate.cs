//------------------------------------------------------------------------------
// <copyright file="BametiTemplate.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------
namespace ADIDAS.Model.Entities
{
    /// <summary>
    /// Bameti Template.
    /// </summary>
    public class BametiTemplate
    {
        /// <summary>
        /// Gets or Sets Scheme_id.
        /// </summary>
        public int Scheme_id { get; set; }

        /// <summary>
        /// Gets or Sets Activity_id.
        /// </summary>
        public int Activity_id { get; set; }

        /// <summary>
        /// Gets or Sets Designation.
        /// </summary>
        public string Designation { get; set; }

        /// <summary>
        /// Gets or Sets Field_id.
        /// </summary>
        public string Field_id { get; set; }

        /// <summary>
        /// Gets or Sets User_id.
        /// </summary>
        public int User_id { get; set; }
    }
}
