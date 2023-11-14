// <copyright file="Release.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//----------------------------------------------------------------------------
namespace ADIDAS.Model.Entities
{
    using System;

    /// <summary>
    /// Release.
    /// </summary>
    public class Release
    {
        /// <summary>
        /// Gets or Sets Release_id.
        /// </summary
        public int Release_id { get; set; }

        /// <summary>
        /// Gets or Sets Release_no.
        /// </summary
        public string Release_no { get; set; }

        /// <summary>
        /// Gets or Sets Release_date.
        /// </summary
        public DateTime? Release_date { get; set; }

        /// <summary>
        /// Gets or Sets Release_notes.
        /// </summary
        public string Release_notes { get; set; }

        /// <summary>
        /// Gets or Sets Optional_flag.
        /// </summary
        public string Optional_flag { get; set; }
    }
}
