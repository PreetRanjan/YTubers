using System.Threading.Tasks;

namespace YTubers.Web.Services
{
    public interface IYouTubeFacade
    {
        Task<ChannelData> GetChannelDataAsync(string channelLink);
    }
}