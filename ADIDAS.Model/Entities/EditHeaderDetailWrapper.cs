//------------------------------------------------------------------------------
// <copyright file="EditHeaderDetailWrapper.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//-----------------------------------------------------------------------------
namespace DepartmentOfAgriculture.Admin.Models.DTO
{
    using System.Collections.Generic;

    /// <summary>
    /// EditHeaderDetailWrapper.
    /// </summary>
    public class EditHeaderDetailWrapper
    {
        /// <summary>
        /// EditHeaderDetailWrapper.
        /// </summary>
        public EditHeaderDetailWrapper()
        {
            this.Header = new List<DtoEditBametiGridData>();

            this.Detail = new List<DtoEditBametiGridData>();
        }

        /// <summary>
        /// Gets or Sets Header.
        /// </summary>
        public List<DtoEditBametiGridData> Header { get; set; }

        /// <summary>
        /// Gets or Sets Detail.
        /// </summary>
        public List<DtoEditBametiGridData> Detail { get; set; }
    }

    /// <summary>
    /// DtoEditBametiGridData.
    /// </summary>
    public class DtoEditBametiGridData
    {
        /// <summary>
        /// DtoEditBametiGridData.
        /// </summary>
        public DtoEditBametiGridData()
        {
            this.Field = new List<EditFieldsBameti>();
        }

        /// <summary>
        /// Gets or Sets Field.
        /// </summary>
        public List<EditFieldsBameti> Field { get; set; }
    }

    /// <summary>
    /// EditFieldsBameti.
    /// </summary>
    public class EditFieldsBameti
    {
        /// <summary>
        /// Gets or Sets TemplateId.
        /// </summary>
        public int TemplateId { get; set; }

        /// <summary>
        /// Gets or Sets HeaderId.
        /// </summary>
        public int HeaderId { get; set; }

        /// <summary>
        /// Gets or Sets Rowno.
        /// </summary>
        public string Rowno { get; set; }

        /// <summary>
        /// Gets or Sets FieldId.
        /// </summary>
        public string FieldId { get; set; }

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
