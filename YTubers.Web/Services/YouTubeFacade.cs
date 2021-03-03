using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YTubers.Web.Utility;

namespace YTubers.Web.Services
{
    public class YouTubeFacade : IYouTubeFacade
    {
        private readonly IOptions<YouTubeData> options;

        public YouTubeFacade(IOptions<YouTubeData> options)
        {
            this.options = options;
        }
        public async Task<ChannelData> GetChannelDataAsync(string channelLink)
        {
            var youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = options.Value.APIKey,
                ApplicationName = options.Value.ApplicationName
            });
            var channelRequest = youtubeService.Channels.List("statistics,snippet");
            Func<Uri, string[]> getTypes = (x) => {
                var type = x.AbsoluteUri.ToLower().Contains("user") ? "user" : "channel";
                return new string[] { type, x.Segments[x.Segments.Length - 1] };
            };
            var result = getTypes(new Uri(channelLink));
            switch (result[0])
            {
                case "channel":
                    channelRequest.Id = result[1];
                    break;
                case "user":
                    channelRequest.ForUsername = result[1];
                    break;
                default:
                    break;
            }
            var channelResponse = await channelRequest.ExecuteAsync();

            var channelData = new ChannelData
            {
                SubsCount = channelResponse.Items[0].Statistics.SubscriberCount,
                VideoCount = channelResponse.Items[0].Statistics.VideoCount,
                ThumbnailUrl = channelResponse.Items[0].Snippet.Thumbnails.Medium.Url,
                ChannelId = result[1],
                Title = channelResponse.Items[0].Snippet.Title,
                Country = channelResponse.Items[0].Snippet.Country,
                Description = channelResponse.Items[0].Snippet.Description,
                CustomUrl = channelResponse.Items[0].Snippet.CustomUrl,
                ChannelUrl = channelLink
            };
            return await Task.FromResult(channelData);
        }
        public string[] GetKeyFromLink(string link)
        {
            string type = "";
            var url = new Uri(link);
            var key = url.Segments;
            if (link.ToLower().Contains("user"))
            {
                type = "user";
            }
            else if (link.ToLower().Contains("channel"))
            {
                type = "channel";
            }
            return new string[] { type, key[key.Length - 1] };
        }
    }
    public class ChannelData
    {
        public ulong? SubsCount { get; set; }
        public ulong? VideoCount { get; set; }
        public string ThumbnailUrl { get; set; }
        public string ChannelId { get; set; }
        public string Country { get; set; }
        public string Title { get; set; }
        public string Description { get; internal set; }
        public string CustomUrl { get; internal set; }
        public string ChannelUrl { get; set; }
    }
}
