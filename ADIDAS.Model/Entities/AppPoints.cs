//------------------------------------------------------------------------------
// <copyright file="AppPoints.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------
namespace ADIDAS.Model.Entities
{
    /// <summary>
    /// App Points.
    /// </summary>
    public class AppPoints
    {
        /// <summary>
        /// Gets or Sets Points_cat_id.
        /// </summary>
        public int Points_cat_id { get; set; }

        /// <summary>
        /// Gets or Sets Points_cat_name.
        /// </summary>
        public string Points_cat_name { get; set; }

        /// <summary>
        /// Gets or Sets Points_cat_desc.
        /// </summary>
        public string Points_cat_desc { get; set; }

        /// <summary>
        /// Gets or Sets Points_awarded.
        /// </summary>
        public int? Points_awarded { get; set; }
    }
}
