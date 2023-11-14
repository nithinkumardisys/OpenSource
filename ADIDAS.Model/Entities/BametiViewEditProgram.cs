//------------------------------------------------------------------------------
// <copyright file="BametiViewEditProgram.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------

namespace ADIDAS.Model.Entities
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// BametiViewEditProgram.
    /// </summary>
    public class BametiViewEditProgram
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BametiViewEditProgram"/> class.
        /// Gets or Sets BametiViewEditProgram.
        /// </summary>
        public BametiViewEditProgram()
        {
            this.HeaderFieldDetails = new List<DtoFields>();

            this.Rows = new List<BametiRows>();
        }

        /// <summary>
        /// Gets or Sets Template_id.
        /// </summary>
        public int? Template_id { get; set; }

        /// <summary>
        /// Gets or Sets Scheme_name.
        /// </summary>
        public string Scheme_name { get; set; }

        /// <summary>
        /// Gets or Sets Activity_name.
        /// </summary>
        public string Activity_name { get; set; }

        /// <summary>
        /// Gets or Sets Header_id.
        /// </summary>
        public int? Header_id { get; set; }

        /// <summary>
        /// Gets or Sets HeaderNames.
        /// </summary>
        public List<string> HeaderNames { get; set; }

        /// <summary>
        /// Gets or Sets DetailNames.
        /// </summary>
        public List<string> DetailNames { get; set; }

        /// <summary>
        /// Gets or Sets HeaderFieldDetails.
        /// </summary>
        public List<DtoFields> HeaderFieldDetails { get; set; }

        /// <summary>
        /// Gets or Sets Rows.
        /// </summary>
        public List<BametiRows> Rows { get; set; }
    }

    /// <summary>
    /// DTO Fields.
    /// </summary>
    public class DtoFields
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DtoFields"/> class.
        /// Gets or Sets DtoFields.
        /// </summary>
        public DtoFields()
        {
            this.List_values = new List<string>();
        }

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
        /// Gets or Sets Field_value.
        /// </summary>
        public string Field_value { get; set; }

        /// <summary>
        /// Gets or Sets Field_name.
        /// </summary>
        public string Field_name { get; set; }

        /// <summary>
        /// Gets or Sets ActualfieldName.
        /// </summary>
        public string ActualfieldName { get; set; }

        /// <summary>
        /// Gets or Sets Field_type.
        /// </summary>
        public string Field_type { get; set; }

        /// <summary>
        /// Gets or Sets Field_category.
        /// </summary>
        public string Field_category { get; set; }

        /// <summary>
        /// Gets or Sets List_values.
        /// </summary>
        public List<string> List_values { get; set; }
    }

    /// <summary>
    /// Bameti Rows.
    /// </summary>
    public class BametiRows
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BametiRows"/> class.
        /// Gets or Sets BametiRows.
        /// </summary>
        public BametiRows()
        {
            this.Detailedfields = new List<DtoFields>();
        }

        /// <summary>
        /// Gets or Sets Row_no.
        /// </summary>
        public int? Row_no { get; set; }

        /// <summary>
        /// Gets or Sets Header_id.
        /// </summary>
        public int Header_id { get; set; }

        /// <summary>
        /// Gets or Sets Rec_created_date.
        /// </summary>
        public DateTime? Rec_created_date { get; set; }

        /// <summary>
        /// Gets or Sets Detailedfields.
        /// </summary>
        public List<DtoFields> Detailedfields { get; set; }
    }
}
