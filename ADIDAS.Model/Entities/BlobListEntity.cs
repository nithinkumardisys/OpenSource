//------------------------------------------------------------------------------
// <copyright file="BlobListEntity.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------

namespace ADIDAS.Model.Entities
{
    using System.Collections.Generic;

    /// <summary>
    /// Blob List Entity.
    /// </summary>
    public class BlobListEntity
    {
        /// <summary>
        /// Gets or Sets BlobName.
        /// </summary>
        public string BlobName { get; set; }

        /// <summary>
        /// Gets or Sets Metadata.
        /// </summary>
        public IDictionary<string, string> Metadata { get; set; }
    }
}
