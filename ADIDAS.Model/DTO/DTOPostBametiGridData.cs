//------------------------------------------------------------------------------
// <copyright file="DTOPostBametiGridData.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------
namespace DepartmentOfAgriculture.Admin.Models.DTO
{
    using System.Collections.Generic;

    /// <summary>
    /// Post Bameti Grid Data.
    /// </summary>
    public class DtoPostBametiGridData
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DtoPostBametiGridData"/> class.
        /// </summary>
        public DtoPostBametiGridData()
        {
            this.Field = new List<FieldsBameti>();
        }

        /// <summary>
        /// Gets or Sets Field.
        /// </summary>
        public List<FieldsBameti> Field { get; set; }
    }

    /// <summary>
    /// Fields Bameti.
    /// </summary>
    public class FieldsBameti
    {
        /// <summary>
        /// Gets or Sets TemplateId.
        /// </summary>
        public int TemplateId { get; set; }

        /// <summary>
        /// Gets or Sets FieldId.
        /// </summary>
        public string FieldId { get; set; }

        /// <summary>
        /// Gets or Sets HeaderId.
        /// </summary>
        public int HeaderId { get; set; }

        /// <summary>
        /// Gets or Sets Rowno.
        /// </summary>
        public int Rowno { get; set; }

        /// <summary>
        /// Gets or Sets FieldType.
        /// </summary>
        public string FieldType { get; set; }

        /// <summary>
        /// Gets or Sets FieldCategory.
        /// </summary>
        public string FieldCategory { get; set; }

        /// <summary>
        /// Gets or Sets FieldValue.
        /// </summary>
        public string FieldValue { get; set; }

        /// <summary>
        /// Gets or Sets UserId.
        /// </summary>
        public int UserId { get; set; }
    }
}
