//------------------------------------------------------------------------------
// <copyright file="ISocialMediaRepository.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//----------------------------------------------------------------------------
namespace ADIDAS.Repository.Interfaces
{
    /// <summary>
    /// Social Media Repository interface.
    /// </summary>
    public interface ISocialMediaRepository
    {
        /// <summary>
        /// Insert Youtube Videos Data.
        /// </summary>
        /// <param name="title">title.</param>
        /// <param name="description">description.</param>
        /// <param name="fileType">fileType.</param>
        /// <param name="fileName">fileName.</param>
        /// <returns>integer result.</returns>
        int InsertYoutubeVideosData(string title, string description, string fileType, string fileName);
    }
}
