//------------------------------------------------------------------------------
// <copyright file="SocialMediaService.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------

namespace ADIDAS.Service.DataServices
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using ADIDAS.Repository.Interfaces;
    using ADIDAS.Service.Interfaces;

    public class SocialMediaService : ISocialMediaService
    {
        private readonly ISocialMediaRepository socialMediaRepository;

        public SocialMediaService(ISocialMediaRepository socialMediaRepository)
        {
            this.socialMediaRepository = socialMediaRepository;
        }

        public int InsertYoutubeVideosData(string title, string description, string fileType, string fileName)
        {
            return this.socialMediaRepository.InsertYoutubeVideosData(title, description, fileType, fileName);
        }
    }
}
