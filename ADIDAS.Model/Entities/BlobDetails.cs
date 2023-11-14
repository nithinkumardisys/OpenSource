//------------------------------------------------------------------------------
// <copyright file="BlobDetails.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------

namespace ADIDAS.Model.Entities
{
    using System;

    /// <summary>
    /// Blob Details.
    /// </summary>
    public class BlobDetails
    {
        /// <summary>
        /// Gets or Sets Blob_id.
        /// </summary>
        public int? Blob_id { get; set; }

        /// <summary>
        /// Gets or Sets Title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or Sets Description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or Sets File_type.
        /// </summary>
        public string File_type { get; set; }

        /// <summary>
        /// Gets or Sets File_name.
        /// </summary>
        public string File_name { get; set; }

        /// <summary>
        /// Gets or Sets File_status.
        /// </summary>
        public string File_status { get; set; }

        /// <summary>
        /// Gets or Sets Recp_group_id.
        /// </summary>
        public string Recp_group_id { get; set; }

        /// <summary>
        /// Gets or Sets Uploaded_datetime.
        /// </summary>
        public DateTime? Uploaded_datetime { get; set; }

        /// <summary>
        /// Gets or Sets Uploaded_user_id.
        /// </summary>
        public int? Uploaded_user_id { get; set; }

        /// <summary>
        /// Gets or Sets Deleted_user_id.
        /// </summary>
        public int? Deleted_user_id { get; set; }

        /// <summary>
        /// Gets or Sets Deleted_datetime.
        /// </summary>
        public DateTime? Deleted_datetime { get; set; }

        /// <summary>
        /// Gets or Sets Directory_Name.
        /// </summary>
        public string Directory_Name { get; set; }

        /// <summary>
        /// Gets or Sets Folder_Name.
        /// </summary>
        public string Folder_Name { get; set; }

        /// <summary>
        /// Gets or Sets ByteArray.
        /// </summary>
        public string ByteArray { get; set; }
    }
}
