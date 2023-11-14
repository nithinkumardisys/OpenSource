//------------------------------------------------------------------------------
// <copyright file="IStorageService.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------

namespace ADIDAS.Service.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using ADIDAS.Model.Entities;
    using Azure.Storage.Blobs.Models;

    /// <summary>
    /// IStorageService.
    /// </summary>
    public interface IStorageService
    {
        /// <summary>
        /// GetBlobAsync.
        /// </summary>
        /// <param name="directoryName">directoryName.</param>
        /// <param name="folderName">folderName.</param>
        /// <returns>BlobDownloadInfo.</returns>
        Task<BlobDownloadInfo> GetBlobAsync(string directoryName, string folderName);

        /// <summary>
        /// UploadFileStream.
        /// </summary>
        /// <param name="blobEntity">blobEntity.</param>
        /// <returns>integer.</returns>
        Task<int> UploadFileStream(BlobEntity blobEntity);
    }
}
