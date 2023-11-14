//------------------------------------------------------------------------------
// <copyright file="Node.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//-----------------------------------------------------------------------------
namespace ADIDAS.Model.Entities
{
    /// <summary>
    /// Node.
    /// </summary>
    public class Node
    {
        /// <summary>
        /// Gets or Sets District_id.
        /// </summary>
        public int District_id { get; set; }

        /// <summary>
        /// Gets or Sets Block_id.
        /// </summary>
        public int Block_id { get; set; }

        /// <summary>
        /// Gets or Sets Panchayat_id.
        /// </summary>
        public int Panchayat_id { get; set; }

        /// <summary>
        /// Gets or Sets Node_name.
        /// </summary>
        public string Node_name { get; set; }

        /// <summary>
        /// Gets or Sets Rec_created_userid.
        /// </summary>
        public int Rec_created_userid { get; set; }

        /// <summary>
        /// Gets or Sets Node_id.
        /// </summary>
        public int Node_id { get; set; }
    }
}
