//------------------------------------------------------------------------------
// <copyright file="AgriMachinery.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------
namespace ADIDAS.Model.Entities
{
    using System;

    /// <summary>
    /// Agri Machinery.
    /// </summary>
    public class AgriMachinery
    {
        /// <summary>
        /// Gets or Sets Machinery_ID.
        /// </summary>
        public int Machinery_ID { get; set; }

        /// <summary>
        /// Gets or Sets Machinery_name.
        /// </summary>
        public string Machinery_name { get; set; }

        /// <summary>
        /// Gets or Sets Rec_created_userid.
        /// </summary>
        public int? Rec_created_userid { get; set; }

        /// <summary>
        /// Gets or Sets Rec_created_date.
        /// </summary>
        public DateTime? Rec_created_date { get; set; }
    }
}
