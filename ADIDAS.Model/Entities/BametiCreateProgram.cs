//------------------------------------------------------------------------------
// <copyright file="BametiCreateProgram.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------
namespace ADIDAS.Model.Entities
{
    /// <summary>
    /// Bameti Create Program.
    /// </summary>
    public class BametiCreateProgram
    {
        /// <summary>
        /// Gets or Sets Template_id.
        /// </summary>
        public int Template_id { get; set; }

        /// <summary>
        /// Gets or Sets Row_no.
        /// </summary>
        public int? Row_no { get; set; }

        /// <summary>
        /// Gets or Sets Header_id.
        /// </summary>
        public int Header_id { get; set; }

        /// <summary>
        /// Gets or Sets Field_id.
        /// </summary>
        public int? Field_id { get; set; }

        /// <summary>
        /// Gets or Sets Status.
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Gets or Sets Scheme_name.
        /// </summary>
        public string Scheme_name { get; set; }

        /// <summary>
        /// Gets or Sets Activity_name.
        /// </summary>
        public string Activity_name { get; set; }

        /// <summary>
        /// Gets or Sets Field_category.
        /// </summary>
        public string Field_category { get; set; }

        /// <summary>
        /// Gets or Sets Field_name.
        /// </summary>
        public string Field_name { get; set; }

        /// <summary>
        /// Gets or Sets Field_type.
        /// </summary>
        public string Field_type { get; set; }

        /// <summary>
        /// Gets or Sets Field_value.
        /// </summary>
        public string Field_value { get; set; }

        /// <summary>
        /// Gets or Sets List_value.
        /// </summary>
        public string List_value { get; set; }

        /// <summary>
        /// Gets or Sets Type.
        /// </summary>
        public string Type { get; set; }
    }
}
