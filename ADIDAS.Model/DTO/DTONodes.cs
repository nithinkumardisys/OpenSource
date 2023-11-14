//------------------------------------------------------------------------------
// <copyright file="DTONodes.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------
namespace ADIDAS.Model.DTO
{
    /// <summary>
    /// Nodes.
    /// </summary>
    public class DtoNodes
    {
        /// <summary>
        /// Gets or Sets Panchayat_id.
        /// </summary>
        public int Panchayat_id { get; set; }

        /// <summary>
        /// Gets or Sets Node_id.
        /// </summary>
        public int Node_id { get; set; }

        /// <summary>
        /// Gets or Sets Node_name.
        /// </summary>
        public string Node_name { get; set; }

        /// <summary>
        /// Gets or Sets Panchayat_name.
        /// </summary>
        public string Panchayat_name { get; set; }

        /// <summary>
        /// Gets or Sets District_id.
        /// </summary>
        public int District_id { get; set; }
    }
}
