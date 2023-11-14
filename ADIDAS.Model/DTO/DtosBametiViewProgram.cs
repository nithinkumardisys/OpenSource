//------------------------------------------------------------------------------
// <copyright file="DtosBametiViewProgram.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//-----------------------------------------------------------------------------
namespace ADIDAS.Model.DTO
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// BameticView Program.
    /// </summary>
    public class DtosBametiViewProgram
    {
        /// <summary>
        /// Gets or Sets DtosBametiViewProgram.
        /// </summary>
        public DtosBametiViewProgram()
        {
            this.Rows = new List<DTOsBametiRows>();
            this.Headers = new List<string>();
        }

        /// <summary>
        /// Gets or Sets Headers.
        /// </summary>
        public List<string> Headers { get; set; }

        /// <summary>
        /// Gets or Sets Status.
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Gets or Sets Template_id.
        /// </summary>
        public int? Template_id { get; set; }

        /// <summary>
        /// Gets or Sets Rows.
        /// </summary>
        public List<DTOsBametiRows> Rows { get; set; }
    }

    /// <summary>
    /// Fields.
    /// </summary>
    public class DTOsFields
    {
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
    }

    /// <summary>
    /// Bameti Rows.
    /// </summary>
    public class DTOsBametiRows
    {
        /// <summary>
        /// Gets or Sets DTOsBametiRows.
        /// </summary>
        public DTOsBametiRows()
        {
            this.Fields = new List<DTOsFields>();
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
        /// Gets or Sets Fields.
        /// </summary>
        public List<DTOsFields> Fields { get; set; }
    }
}
