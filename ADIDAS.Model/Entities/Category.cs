//------------------------------------------------------------------------------
// <copyright file="Category.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------

namespace ADIDAS.Model.Entities
{
    using System.Collections.Generic;

    /// <summary>
    /// Category.
    /// </summary>
    public class Category
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Category"/> class.
        /// Gets or Sets SubCategory.
        /// </summary>
        public Category()
        {
            this.Sub_category_list = new List<SubCategory>();
        }

        /// <summary>
        /// Gets or Sets Category_id.
        /// </summary>
        public int? Category_id { get; set; }

        /// <summary>
        /// Gets or Sets Category_name.
        /// </summary>
        public string Category_name { get; set; }

        /// <summary>
        /// Gets or Sets Sub_category_list.
        /// </summary>
        public List<SubCategory> Sub_category_list { get; set; }
    }
}
