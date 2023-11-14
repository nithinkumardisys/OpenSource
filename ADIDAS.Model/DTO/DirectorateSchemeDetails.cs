//------------------------------------------------------------------------------
// <copyright file="DirectorateSchemeDetails.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------
using ADIDAS.Model.Entities;
using System.Collections.Generic;

namespace ADIDAS.Model.DTO
{
    /// <summary>
    /// DirectorateSchemeDetails.
    /// </summary>
    public class DirectorateSchemeDetails
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DirectorateSchemeDetails"/> class.
        /// Gets or Sets BiaScheme.
        /// </summary>
        public DirectorateSchemeDetails()
        {
            this.Scheme_list = new List<BiaScheme>();
        }

        /// <summary>
        /// Gets or Sets Directorate_id.
        /// </summary>
        public int? Directorate_id { get; set; }

        /// <summary>
        /// Gets or Sets Directorate_name.
        /// </summary>
        public string Directorate_name { get; set; }

        /// <summary>
        /// Gets or Sets Scheme_list.
        /// </summary>
        public List<BiaScheme> Scheme_list { get; set; }
    }
}