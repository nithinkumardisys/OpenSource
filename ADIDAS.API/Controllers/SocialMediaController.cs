//------------------------------------------------------------------------------
// <copyright file="SocialMediaController.cs" company="Government of Bihar">
// Copyright (c) Government of Bihar. All rights reserved.
// </copyright>
// <author>Disys</author>
//------------------------------------------------------------------------------
namespace ADIDAS.API.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;
    using ADIDAS.Model.DTO;
    using ADIDAS.Model.Entities;
    using ADIDAS.Service.Interfaces;
    using Google.Apis.Services;
    using Google.Apis.YouTube.v3;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    /// <summary>
    /// Social Media Controller.
    /// </summary>
    [Authorize]
    [Route("/[controller]")]
    [ApiController]
    public class SocialMediaController : Controller
    {
        private readonly IStorageService storageController;
        private readonly ISocialMediaService socialMediaService;
        private readonly HttpClient client = new HttpClient();
        private readonly ILogger<SocialMediaController> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="SocialMediaController"/> class.
        /// Social Media Controller.
        /// </summary>
        /// <param name="storageController">storageController.</param>
        /// <param name="socialMediaService">socialMediaService.</param>
        /// <param name="logger">logger.</param>
        public SocialMediaController(IStorageService storageController, ISocialMediaService socialMediaService, ILogger<SocialMediaController> logger)
        {
            this.storageController = storageController;
            this.socialMediaService = socialMediaService;
            this.logger = logger;
        }

        /// <summary>
        /// Get Video.
        /// </summary>
        /// <returns>Action Result.</returns>
        [HttpGet("GetVideo")]
        public async Task<IActionResult> GetVideo()
        {
            try
            {
                List<Google.Apis.YouTube.v3.Data.PlaylistItemListResponse> listAllPlayListData;

                var blobInfo = this.storageController.GetBlobAsync("JSON", "Youtube_Sabour_Response.json");

                if (blobInfo.Result.ContentLength != 0 && blobInfo.Result.Details.LastModified.Date == DateTime.Now.Date)
                {
                    StringBuilder stringBuilder = new StringBuilder();
                    using (var streamReader = new StreamReader(blobInfo.Result.Content))
                    {
                        while (!streamReader.EndOfStream)
                        {
                            var line = await streamReader.ReadLineAsync();
                            stringBuilder.Append(line);
                        }
                    }

                    listAllPlayListData = JsonConvert.DeserializeObject<List<Google.Apis.YouTube.v3.Data.PlaylistItemListResponse>>(stringBuilder.ToString());
                    if (listAllPlayListData != null && listAllPlayListData.Any())
                    {
                        foreach (var playListData in listAllPlayListData)
                        {
                            foreach (var item in playListData.Items)
                            {
                                this.socialMediaService.InsertYoutubeVideosData("Sabour", "Sabour", item.Snippet.ResourceId.Kind, item.Snippet.ResourceId.VideoId);
                            }
                        }
                    }

                    return this.Ok(listAllPlayListData);
                }

                var yt = new YouTubeService(new BaseClientService.Initializer() { ApiKey = "AIzaSyC5bCesboP1jKt9LD2wn1AGZknbgQ7axb8" });
                listAllPlayListData = new List<Google.Apis.YouTube.v3.Data.PlaylistItemListResponse>();
                var channelsListRequest = yt.Channels.List("contentDetails");
                channelsListRequest.ForUsername = "bausabour";
                var channelsListResponse = channelsListRequest.Execute();
                foreach (var channel in channelsListResponse.Items)
                {
                    // of videos uploaded to the authenticated user's channel.
                    var uploadsListId = channel.ContentDetails.RelatedPlaylists.Uploads;
                    var nextPageToken = string.Empty;
                    while (nextPageToken != null)
                    {
                        var playlistItemsListRequest = yt.PlaylistItems.List("snippet");
                        playlistItemsListRequest.PlaylistId = uploadsListId;
                        playlistItemsListRequest.MaxResults = 50;
                        playlistItemsListRequest.PageToken = nextPageToken;

                        // Retrieve the list of videos uploaded to the authenticated user's channel.
                        var playlistItemsListResponse = playlistItemsListRequest.Execute();
                        listAllPlayListData.Add(playlistItemsListResponse);
                        nextPageToken = playlistItemsListResponse.NextPageToken;
                    }
                }

                if (!listAllPlayListData.Any())
                {
                    return this.NotFound(this.Json("Not Found"));
                }

                BlobEntity blobEntity = new BlobEntity();
                blobEntity.DirectoryName = "JSON";
                blobEntity.FolderName = "Youtube_Sabour_Response.json";
                blobEntity.ByteArray = JsonConvert.SerializeObject(listAllPlayListData);
                await this.storageController.UploadFileStream(blobEntity);
                return this.Ok(listAllPlayListData);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound(this.Json("Not Found"));
            }
        }

        /// <summary>
        /// Get YouTube Channel Videos.
        /// </summary>
        /// <param name="channelId">channelId.</param>
        /// <returns>Action Result.</returns>
        [HttpGet("GetYouTubeChannelVideos/{channelId}")]
        public IActionResult GetYouTubeChannelVideos(string channelId)
        {
            try
            {
                Google.Apis.YouTube.v3.Data.SearchListResponse responseData;
                List<Google.Apis.YouTube.v3.Data.SearchListResponse> listAllData;
                YouTubeService yt = new YouTubeService(new BaseClientService.Initializer() { ApiKey = "AIzaSyC5bCesboP1jKt9LD2wn1AGZknbgQ7axb8" });

                listAllData = new List<Google.Apis.YouTube.v3.Data.SearchListResponse>();

                // to get the snippet from youtube
                var searchListRequest = yt.Search.List("snippet");
                var channel = channelId;

                // to get perticular channel infromation
                searchListRequest.ChannelId = channel;
                var searchListResult = searchListRequest.Execute();
                searchListRequest.MaxResults = 50;

                // to get all the ids,url,views,description and list
                foreach (var item in searchListResult.Items)
                {
                    Console.WriteLine("ID:" + item.Id.VideoId);
                    Console.WriteLine("snippet:" + item.Snippet.Title);
                    this.socialMediaService.InsertYoutubeVideosData("YouTubeVideo", "YouTubeVideo", item.Id.Kind, item.Id.VideoId);
                }

                responseData = searchListResult;
                listAllData.Add(responseData);
                if (!listAllData.Any())
                {
                    return this.NotFound(this.Json("Not Found"));
                }

                return this.Ok(listAllData);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound(this.Json("Not Found"));
            }
        }

        /// <summary>
        /// Get YouTube List Videos.
        /// </summary>
        /// <returns>Action Result.</returns>
        [HttpGet("GetYouTubeVideos")]
        public async Task<IActionResult> GetYouTubeListVideos()
        {
            try
            {
                List<Google.Apis.YouTube.v3.Data.PlaylistItemListResponse> listAllPlayListData;
                var blobInfo = this.storageController.GetBlobAsync("JSON", "Youtube_IPRD_Response.json");

                if (blobInfo.Result.ContentLength != 0 && blobInfo.Result.Details.LastModified.Date == DateTime.Now.Date)
                {
                    StringBuilder stringBuilder = new StringBuilder();
                    using (var streamReader = new StreamReader(blobInfo.Result.Content))
                    {
                        while (!streamReader.EndOfStream)
                        {
                            var line = await streamReader.ReadLineAsync();
                            stringBuilder.Append(line);
                        }
                    }

                    listAllPlayListData = JsonConvert.DeserializeObject<List<Google.Apis.YouTube.v3.Data.PlaylistItemListResponse>>(stringBuilder.ToString());

                    if (listAllPlayListData != null && listAllPlayListData.Any())
                    {
                        foreach (var playListData in listAllPlayListData)
                        {
                            foreach (var item in playListData.Items)
                            {
                                this.socialMediaService.InsertYoutubeVideosData("IPRD", "IPRD", item.Snippet.ResourceId.Kind, item.Snippet.ResourceId.VideoId);
                            }
                        }
                    }

                    return this.Ok(listAllPlayListData);
                }

                var yt = new YouTubeService(new BaseClientService.Initializer() { ApiKey = "AIzaSyC5bCesboP1jKt9LD2wn1AGZknbgQ7axb8" });
                listAllPlayListData = new List<Google.Apis.YouTube.v3.Data.PlaylistItemListResponse>();
                var channelsListRequest = yt.Channels.List("contentDetails");
                channelsListRequest.ForUsername = "jnandharm";
                var channelsListResponse = channelsListRequest.Execute();
                foreach (var channel in channelsListResponse.Items)
                {
                    // of videos uploaded to the authenticated user's channel.
                    var uploadsListId = channel.ContentDetails.RelatedPlaylists.Uploads;
                    var nextPageToken = string.Empty;
                    while (nextPageToken != null)
                    {
                        var playlistItemsListRequest = yt.PlaylistItems.List("snippet");
                        playlistItemsListRequest.PlaylistId = uploadsListId;
                        playlistItemsListRequest.MaxResults = 50;
                        playlistItemsListRequest.PageToken = nextPageToken;

                        // Retrieve the list of videos uploaded to the authenticated user's channel.
                        var playlistItemsListResponse = playlistItemsListRequest.Execute();
                        listAllPlayListData.Add(playlistItemsListResponse);
                        nextPageToken = playlistItemsListResponse.NextPageToken;
                    }
                }

                if (!listAllPlayListData.Any())
                {
                    return this.NotFound(this.Json("Not Found"));
                }

                BlobEntity blobEntity = new BlobEntity();
                blobEntity.DirectoryName = "JSON";
                blobEntity.FolderName = "Youtube_IPRD_Response.json";
                blobEntity.ByteArray = JsonConvert.SerializeObject(listAllPlayListData);
                await this.storageController.UploadFileStream(blobEntity);
                return this.Ok(listAllPlayListData);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound(this.Json("Not Found"));
            }
        }

        /// <summary>
        /// Get Bameti YouTube Videos.
        /// </summary>
        /// <returns>Action Result.</returns>
        [HttpGet("GetBametiYouTubeVideos")]
        public async Task<IActionResult> GetBametiYouTubeVideos()
        {
            try
            {
                List<Google.Apis.YouTube.v3.Data.PlaylistItemListResponse> listAllPlayListData;
                var blobInfo = this.storageController.GetBlobAsync("JSON", "Youtube_BAMETI_Response.json");

                if (blobInfo.Result.ContentLength != 0 && blobInfo.Result.Details.LastModified.Date == DateTime.Now.Date)
                {
                    StringBuilder stringBuilder = new StringBuilder();
                    using (var streamReader = new StreamReader(blobInfo.Result.Content))
                    {
                        while (!streamReader.EndOfStream)
                        {
                            var line = await streamReader.ReadLineAsync();
                            stringBuilder.Append(line);
                        }
                    }

                    listAllPlayListData = JsonConvert.DeserializeObject<List<Google.Apis.YouTube.v3.Data.PlaylistItemListResponse>>(stringBuilder.ToString());

                    if (listAllPlayListData != null && listAllPlayListData.Any())
                    {
                        foreach (var playListData in listAllPlayListData)
                        {
                            foreach (var item in playListData.Items)
                            {
                                this.socialMediaService.InsertYoutubeVideosData("Bameti", "Bameti Videos", item.Snippet.ResourceId.Kind, item.Snippet.ResourceId.VideoId);
                            }
                        }
                    }

                    return this.Ok(listAllPlayListData);
                }

                var yt = new YouTubeService(new BaseClientService.Initializer() { ApiKey = "AIzaSyC5bCesboP1jKt9LD2wn1AGZknbgQ7axb8" });
                listAllPlayListData = new List<Google.Apis.YouTube.v3.Data.PlaylistItemListResponse>();
                var channelsListRequest = yt.Channels.List("contentDetails");
                channelsListRequest.ForUsername = "bametipatnabihar";
                var channelsListResponse = channelsListRequest.Execute();
                foreach (var channel in channelsListResponse.Items)
                {
                    // of videos uploaded to the authenticated user's channel.
                    var uploadsListId = channel.ContentDetails.RelatedPlaylists.Uploads;
                    var nextPageToken = string.Empty;
                    while (nextPageToken != null)
                    {
                        var playlistItemsListRequest = yt.PlaylistItems.List("snippet");
                        playlistItemsListRequest.PlaylistId = uploadsListId;
                        playlistItemsListRequest.MaxResults = 50;
                        playlistItemsListRequest.PageToken = nextPageToken;

                        // Retrieve the list of videos uploaded to the authenticated user's channel.
                        var playlistItemsListResponse = playlistItemsListRequest.Execute();

                        listAllPlayListData.Add(playlistItemsListResponse);

                        nextPageToken = playlistItemsListResponse.NextPageToken;
                    }
                }

                if (!listAllPlayListData.Any())
                {
                    return this.NotFound(this.Json("Not Found"));
                }

                BlobEntity blobEntity = new BlobEntity();
                blobEntity.DirectoryName = "JSON";
                blobEntity.FolderName = "Youtube_BAMETI_Response.json";

                blobEntity.ByteArray = JsonConvert.SerializeObject(listAllPlayListData);

                await this.storageController.UploadFileStream(blobEntity);
                return this.Ok(listAllPlayListData);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound(this.Json("Not Found"));
            }
        }

        /// <summary>
        /// Get YouTube Live Video Info.
        /// </summary>
        /// <param name="channelId">channelId.</param>
        /// <param name="fileName">fileName.</param>
        /// <returns>Action Result.</returns>
        [HttpGet("GetYouTubeLiveVideoInfo/{ChannelId}/{fileName}")]
        public async Task<IActionResult> GetYouTubeLiveVideoInfo(string channelId, string fileName)
        {
            try
            {
                List<string> videoTypes = new List<string> { "Live", "UpComing" };

                List<Google.Apis.YouTube.v3.Data.SearchResult> listAllPlayListData = new List<Google.Apis.YouTube.v3.Data.SearchResult>();

                var blobInfo = this.storageController.GetBlobAsync("JSON", fileName);

                if (blobInfo.Result.ContentLength != 0 && blobInfo.Result.Details.LastModified.Date == DateTime.Now.Date)
                {
                    StringBuilder stringBuilder = new StringBuilder();
                    using (var streamReader = new StreamReader(blobInfo.Result.Content))
                    {
                        while (!streamReader.EndOfStream)
                        {
                            var line = await streamReader.ReadLineAsync();
                            stringBuilder.Append(line);
                        }
                    }

                    listAllPlayListData = JsonConvert.DeserializeObject<List<Google.Apis.YouTube.v3.Data.SearchResult>>(stringBuilder.ToString());

                    if (listAllPlayListData != null && listAllPlayListData.Any())
                    {
                        foreach (var playListData in listAllPlayListData)
                        {
                            this.socialMediaService.InsertYoutubeVideosData("Bameti", "Bameti Videos", playListData.Id.Kind, playListData.Id.VideoId);
                        }
                    }

                    return this.Ok(listAllPlayListData);
                }

                var youtubeService = new YouTubeService(new BaseClientService.Initializer()
                {
                    ApiKey = "AIzaSyC5bCesboP1jKt9LD2wn1AGZknbgQ7axb8",
                });

                foreach (var vidTypes in videoTypes)
                {
                    if (vidTypes == "Live")
                    {
                        var searchListRequest = youtubeService.Search.List("snippet");
                        searchListRequest.MaxResults = 50;
                        searchListRequest.ChannelId = channelId;
                        searchListRequest.EventType = SearchResource.ListRequest.EventTypeEnum.Live;
                        searchListRequest.Type = "video";
                        var searchListResponse = await searchListRequest.ExecuteAsync();

                        // Add each result to the appropriate list, and then display the lists of
                        // matching videos, channels, and playlists.
                        foreach (var searchResult in searchListResponse.Items)
                        {
                            listAllPlayListData.Add(searchResult);
                        }
                    }
                    else if (vidTypes == "UpComing")
                    {
                        var searchListRequest = youtubeService.Search.List("snippet");
                        searchListRequest.MaxResults = 50;
                        searchListRequest.ChannelId = channelId;
                        searchListRequest.EventType = SearchResource.ListRequest.EventTypeEnum.Upcoming;
                        searchListRequest.Type = "video";

                        // Call the search.list method to retrieve results matching the specified query term.
                        var searchListResponse = await searchListRequest.ExecuteAsync();

                        // Add each result to the appropriate list, and then display the lists of
                        // matching videos, channels, and playlists.
                        foreach (var searchResult in searchListResponse.Items)
                        {
                            listAllPlayListData.Add(searchResult);
                        }
                    }
                }

                if (!listAllPlayListData.Any())
                {
                    return this.NotFound(this.Json("Not Found"));
                }

                BlobEntity blobEntity = new BlobEntity();
                blobEntity.DirectoryName = "JSON";
                blobEntity.FolderName = fileName;

                blobEntity.ByteArray = JsonConvert.SerializeObject(listAllPlayListData);

                await this.storageController.UploadFileStream(blobEntity);
                return this.Ok(listAllPlayListData);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound(this.Json("Not Found"));
            }
        }

        /// <summary>
        /// Get YouTube Live Video Detailed Info.
        /// </summary>
        /// <param name="livevideoDetails">livevideoDetails.</param>
        /// <returns>Action Result.</returns>
        [HttpPost("GetYouTubeLiveVideoDetailedInfo")]
        public async Task<IActionResult> GetYouTubeLiveVideoDetailedInfo(LiveVideoDetailRequest livevideoDetails)
        {
            try
            {
                List<Google.Apis.YouTube.v3.Data.Video> listAllPlayListData = new List<Google.Apis.YouTube.v3.Data.Video>();
                var blobInfo = this.storageController.GetBlobAsync("JSON", livevideoDetails.FileName);
                if (blobInfo.Result.ContentLength != 0 && blobInfo.Result.Details.LastModified.Date == DateTime.Now.Date)
                {
                    StringBuilder stringBuilder = new StringBuilder();
                    using (var streamReader = new StreamReader(blobInfo.Result.Content))
                    {
                        while (!streamReader.EndOfStream)
                        {
                            var line = await streamReader.ReadLineAsync();
                            stringBuilder.Append(line);
                        }
                    }

                    listAllPlayListData = JsonConvert.DeserializeObject<List<Google.Apis.YouTube.v3.Data.Video>>(stringBuilder.ToString());
                    return this.Ok(listAllPlayListData);
                }

                var youtubeService = new YouTubeService(new BaseClientService.Initializer()
                {
                    ApiKey = "AIzaSyC5bCesboP1jKt9LD2wn1AGZknbgQ7axb8",
                });

                foreach (var videoId in livevideoDetails.VideoIds)
                {
                    var searchListRequest = youtubeService.Videos.List("liveStreamingDetails");
                    searchListRequest.MaxResults = 50;
                    searchListRequest.Id = videoId;
                    var searchListResponse = await searchListRequest.ExecuteAsync();

                    // Add each result to the appropriate list, and then display the lists of
                    // matching videos, channels, and playlists.
                    foreach (var searchResult in searchListResponse.Items)
                    {
                        listAllPlayListData.Add(searchResult);
                    }
                }

                if (!listAllPlayListData.Any())
                {
                    return this.NotFound(this.Json("Not Found"));
                }

                BlobEntity blobEntity = new BlobEntity();
                blobEntity.DirectoryName = "JSON";
                blobEntity.FolderName = livevideoDetails.FileName;

                blobEntity.ByteArray = JsonConvert.SerializeObject(listAllPlayListData);

                await this.storageController.UploadFileStream(blobEntity);
                return this.Ok(listAllPlayListData);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound(this.Json("Not Found"));
            }
        }

        /// <summary>
        /// Get Instagram Media.
        /// </summary>
        /// <returns>Action Result.</returns>
        [HttpGet("GetInstagramMedia")]
        public IActionResult GetInstagramMedia()
        {
            try
            {
                // to set instagram access token
                string instagramAccessToken = "IGQVJVWldNWkJLeTBYQV9yTmZAiLTQ0aEkwbkVrZAV93aEFialN5UFQyTFNRZAlRENkhzazdlTDNMZAWxMZA0o4X0F3eVVWZAHBnWlF1NndQQjhHS1ZA0UE9rdnVocmlnNTdyODE4SGkwZAXJJQUYzMGltSzRJTQZDZD";
                string baseUrl = new Uri("https://graph.instagram.com/").AbsoluteUri;

                // to set limit for instagram media
                int limit = 50;

                // to set all instagram media fields
                string fields = "me/media?fields=caption,media_url,media_type,like_count,comments_count,timestamp,id&access_token=" + instagramAccessToken + "&limit=" + limit;
                HttpResponseMessage response = this.client.GetAsync(baseUrl + fields).Result;
                var result = response.Content.ReadAsStringAsync().Result;
                JObject json = JObject.Parse(result);
                List<InstagramMedia> mediaLists = new List<InstagramMedia>();

                // to add instagram media in the list.
                foreach (var item in json["data"])
                {
                    mediaLists.Add(new InstagramMedia
                    {
                        ID = (long)item["id"],
                        Media_Url = (string)item["media_url"],
                        Media_Type = (string)item["media_type"],
                        Caption = (string)item["caption"] == null ? string.Empty : (string)item["caption"],
                        TimeStamp = (DateTime)item["timestamp"],
                    });
                }

                if (!mediaLists.Any())
                {
                    return this.NotFound(this.Json("Not Found"));
                }

                return this.Ok(mediaLists);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.NotFound(this.Json("Not Found"));
            }
        }
    }
}
