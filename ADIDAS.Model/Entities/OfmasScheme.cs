// <copyright file="OfmasScheme.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//----------------------------------------------------------------------------
namespace ADIDAS.Model.Entities
{
    /// <summary>
    /// OfmasScheme.
    /// </summary>
    public class OfmasScheme
    {
        /// <summary>
        /// Gets or Sets Financial_Year.
        /// </summary>
        public string Financial_Year { get; set; }

        /// <summary>
        /// Gets or Sets Scheme_name.
        /// </summary>
        public string Scheme_name { get; set; }

        /// <summary>
        /// Gets or Sets Scheme_id.
        /// </summary>
        public int Scheme_id { get; set; }

        /// <summary>
        /// Gets or Sets Phase.
        /// </summary>
        public int Phase { get; set; }
    }
}
