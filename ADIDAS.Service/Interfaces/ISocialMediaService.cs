//------------------------------------------------------------------------------
// <copyright file="ISocialMediaService.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------

namespace ADIDAS.Service.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// ISocialMediaService.
    /// </summary>
    public interface ISocialMediaService
    {
        /// <summary>
        /// InsertYoutubeVideosData.
        /// </summary>
        /// <param name="title">title.</param>
        /// <param name="description">description.</param>
        /// <param name="fileType">fileType.</param>
        /// <param name="fileName">fileName.</param>
        /// <returns>integer.</returns>
        int InsertYoutubeVideosData(string title, string description, string fileType, string fileName);
    }
}
