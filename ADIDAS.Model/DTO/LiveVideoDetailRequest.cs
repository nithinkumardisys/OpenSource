//------------------------------------------------------------------------------
// <copyright file="LiveVideoDetailRequest.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------

namespace ADIDAS.Model.DTO
{
    using System.Collections.Generic;

    /// <summary>
    /// Live Video Detail Request.
    /// </summary>
    public class LiveVideoDetailRequest
    {
        /// <summary>
        /// Gets or Sets FileName.
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// Gets or Sets VideoIds.
        /// </summary>
        public List<string> VideoIds { get; set; }
    }
}
