//------------------------------------------------------------------------------
// <copyright file="GetNotSubittedModel.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------

namespace ADIDAS.Model.DTO
{
    /// <summary>
    /// Get Not Subitted Model.
    /// </summary>
    public class GetNotSubittedModel
    {
        /// <summary>
        /// Gets or Sets District_ID.
        /// </summary>
        public int District_ID { get; set; }

        /// <summary>
        /// Gets or Sets District_name.
        /// </summary>
        public string District_name { get; set; }

        /// <summary>
        /// Gets or Sets Panchayat_ID.
        /// </summary>
        public int Panchayat_ID { get; set; }

        /// <summary>
        /// Gets or Sets Panchayat_Name.
        /// </summary>
        public string Panchayat_Name { get; set; }

        /// <summary>
        /// Gets or Sets Block_id.
        /// </summary>
        public int Block_id { get; set; }

        /// <summary>
        /// Gets or Sets Block_name.
        /// </summary>
        public string Block_name { get; set; }
    }
}
