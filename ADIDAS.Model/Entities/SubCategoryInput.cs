//------------------------------------------------------------------------------
// <copyright file="SubCategoryInput.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------
namespace ADIDAS.Model.Entities
{
    /// <summary>
    /// SubCategoryInput.
    /// </summary>
    public class SubCategoryInput
    {
        /// <summary>
        /// Gets or Sets Category_id.
        /// </summary>
        public int? Category_id { get; set; }

        /// <summary>
        /// Gets or Sets Category_name.
        /// </summary>
        public string Category_name { get; set; }

        /// <summary>
        /// Gets or Sets Sub_category_id.
        /// </summary>
        public int? Sub_category_id { get; set; }

        /// <summary>
        /// Gets or Sets Sub_category_name.
        /// </summary>
        public string Sub_category_name { get; set; }
    }
}
