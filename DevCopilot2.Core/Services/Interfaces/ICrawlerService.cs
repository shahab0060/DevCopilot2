using DevCopilot2.Domain.DTOs.Crawlers;

namespace DevCopilot2.Core.Services.Interfaces
{
    public interface ICrawlerService : IService
    {
        Task<FilterInstagramPostsDto> FilterInstagramPosts(FilterInstagramPostsDto filter);
    }
}
