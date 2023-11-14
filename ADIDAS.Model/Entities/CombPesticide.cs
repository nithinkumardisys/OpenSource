//------------------------------------------------------------------------------
// <copyright file="CombPesticide.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------
namespace ADIDAS.Model.Entities
{
    /// <summary>
    /// Comb Pesticide.
    /// </summary>
    public class CombPesticide
    {
        /// <summary>
        /// Gets or Sets Comb_pesticide_id.
        /// </summary>
        public int Comb_pesticide_id { get; set; }

        /// <summary>
        /// Gets or Sets Pesticide_type.
        /// </summary>
        public string Pesticide_type { get; set; }

        /// <summary>
        /// Gets or Sets Combination_product.
        /// </summary>
        public string Combination_product { get; set; }

        /// <summary>
        /// Gets or Sets Company_name.
        /// </summary>
        public string Company_name { get; set; }
    }
}
