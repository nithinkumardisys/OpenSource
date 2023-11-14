//------------------------------------------------------------------------------
// <copyright file="BiaSchemeInput.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------
namespace ADIDAS.Model.DTO
{
    /// <summary>
    /// BiaSchemeInput.
    /// </summary>
    public class BiaSchemeInput
    {
        /// <summary>
        /// Gets or Sets Directorate_id.
        /// </summary>
        public int? Directorate_id { get; set; }

        /// <summary>
        /// Gets or Sets Directorate_name.
        /// </summary>
        public string Directorate_name { get; set; }

        /// <summary>
        /// Gets or Sets Scheme_id.
        /// </summary>
        public int? Scheme_id { get; set; }

        /// <summary>
        /// Gets or Sets Scheme_name.
        /// </summary>
        public string Scheme_name { get; set; }
    }
}
