//------------------------------------------------------------------------------
// <copyright file="BlobEntity.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------
namespace ADIDAS.Model.Entities
{
    /// <summary>
    /// Blob Entity.
    /// </summary>
    public class BlobEntity
    {
        /// <summary>
        /// Gets or Sets DirectoryName.
        /// </summary>
        public string DirectoryName { get; set; }

        /// <summary>
        /// Gets or Sets FolderName.
        /// </summary>
        public string FolderName { get; set; }

        /// <summary>
        /// Gets or Sets LocalFilePath.
        /// </summary>
        public string LocalFilePath { get; set; }

        /// <summary>
        /// Gets or Sets LocalFolderPath.
        /// </summary>
        public string LocalFolderPath { get; set; }

        /// <summary>
        /// Gets or Sets BlobName.
        /// </summary>
        public string BlobName { get; set; }

        /// <summary>
        /// Gets or Sets ByteArray.
        /// </summary>
        public string ByteArray { get; set; }
    }
}
