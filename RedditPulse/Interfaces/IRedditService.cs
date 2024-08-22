using RedditPulse.Models;
namespace RedditPulse.Interfaces;
public interface IRedditService
{
    Task<IEnumerable<RedditPost>> ListenToSubreddit(string subredditName, IStatisticsService statisticsService);
}
