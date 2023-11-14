//------------------------------------------------------------------------------
// <copyright file="StorageService.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------

namespace ADIDAS.Service.DataServices
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using System.Threading.Tasks;
    using ADIDAS.Model.DTO;
    using ADIDAS.Model.Entities;
    using ADIDAS.Service.Interfaces;
    using Azure;
    using Azure.Storage.Blobs;
    using Azure.Storage.Blobs.Models;
    using Microsoft.Extensions.Options;

    public class StorageService : IStorageService
    {
        private const string V = "/";
        private readonly BlobServiceClient blobServiceClient;
        private readonly IOptions<BlobConfig> blobConfig;

        public StorageService(IOptions<BlobConfig> blobConfig)
        {
            this.blobConfig = blobConfig;
            this.blobServiceClient = new BlobServiceClient(this.blobConfig.Value.BlobConnection);
        }

        public async Task<BlobDownloadInfo> GetBlobAsync(string directoryName, string folderName)
        {
            BlobListEntity blobEntity = new BlobListEntity();
            BlobContainerClient containerClient = this.blobServiceClient.GetBlobContainerClient("mobileapp");
            string path = directoryName + V + folderName;
            AsyncPageable<BlobItem> blobs = containerClient.GetBlobsAsync(BlobTraits.Metadata, prefix: path);
            await foreach (BlobItem blobItem in blobs)
            {
                blobEntity.BlobName = blobItem.Name;
                blobEntity.Metadata = blobItem.Metadata;
            }

            var blobClient = containerClient.GetBlobClient(blobEntity.BlobName);
            var blobDownloadInfo = await blobClient.DownloadAsync();
            return blobDownloadInfo.Value;
        }

        public async Task<int> UploadFileStream(BlobEntity blobEntity)
        {
            BlobContainerClient containerClient = this.blobServiceClient.GetBlobContainerClient("mobileapp");
            string blobPath = blobEntity.DirectoryName + V + blobEntity.FolderName;
            BlobClient blobClient = containerClient.GetBlobClient(blobPath);
            var bytes = Encoding.UTF8.GetBytes(blobEntity.ByteArray);
            await using var memoryStream = new MemoryStream(bytes);
            await blobClient.UploadAsync(memoryStream, overwrite: true);
            return 1;
        }
    }
}
