// <copyright file="PesticideModel.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//----------------------------------------------------------------------------
namespace ADIDAS.Model.Entities
{
    /// <summary>
    /// PesticideModel.
    /// </summary>
    public class PesticideModel
    {
        /// <summary>
        /// Gets or Sets Pesticide_id.
        /// </summary
        public int Pesticide_id { get; set; }

        /// <summary>
        /// Gets or Sets Formulation_id.
        /// </summary
        public int Formulation_id { get; set; }

        /// <summary>
        /// Gets or Sets Pesticide_name.
        /// </summary
        public string Pesticide_name { get; set; }

        /// <summary>
        /// Gets or Sets No_of_formulations.
        /// </summary
        public int? No_of_formulations { get; set; }

        /// <summary>
        /// Gets or Sets Formulation_registered.
        /// </summary
        public string Formulation_registered { get; set; }
    }
}
