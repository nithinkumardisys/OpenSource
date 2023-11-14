//------------------------------------------------------------------------------
// <copyright file="DTOCreateProgram.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------
namespace ADIDAS.Model.DTO
{
    using System.Collections.Generic;

    /// <summary>
    /// Create Program.
    /// </summary>
    public class DtoCreateProgram
    {
        /// <summary>
        /// Gets or Sets DtoCreateProgram.
        /// </summary>
        public DtoCreateProgram()
        {
            this.List_value = new List<string>();
        }

        /// <summary>
        /// Gets or Sets Template_id.
        /// </summary>
        public int Template_id { get; set; }

        /// <summary>
        /// Gets or Sets Field_id.
        /// </summary>
        public int? Field_id { get; set; }

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
        /// Gets or Sets Type.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Gets or Sets List_value.
        /// </summary>
        public List<string> List_value { get; set; }
    }
}
