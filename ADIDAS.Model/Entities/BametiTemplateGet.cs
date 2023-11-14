//------------------------------------------------------------------------------
// <copyright file="BametiTemplateGet.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------
namespace ADIDAS.Model.Entities
{
    /// <summary>
    /// Bameti Template Get.
    /// </summary>
    public class BametiTemplateGet
    {
        /// <summary>
        /// Gets or Sets Template_id.
        /// </summary>
        public int Template_id { get; set; }

        /// <summary>
        /// Gets or Sets Field_id.
        /// </summary>
        public int Field_id { get; set; }

        /// <summary>
        /// Gets or Sets Conflict_Flag.
        /// </summary>
        public string Conflict_Flag { get; set; }

        /// <summary>
        /// Gets or Sets Field_category.
        /// </summary>
        public string Field_category { get; set; }

        /// <summary>
        /// Gets or Sets Detail_field_name.
        /// </summary>
        public string Detail_field_name { get; set; }

        /// <summary>
        /// Gets or Sets Bameti_xref_id.
        /// </summary>
        public int Bameti_xref_id { get; set; }

        /// <summary>
        /// Gets or Sets Field_name.
        /// </summary>
        public string Field_name { get; set; }

        /// <summary>
        /// Gets or Sets Field_type.
        /// </summary>
        public string Field_type { get; set; }

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
        /// Gets or Sets Scheme_name.
        /// </summary>
        public string Scheme_name { get; set; }

        /// <summary>
        /// Gets or Sets Activity_name.
        /// </summary>
        public string Activity_name { get; set; }
    }
}
