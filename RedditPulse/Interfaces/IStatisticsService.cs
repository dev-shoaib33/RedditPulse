using RedditPulse.Models;
namespace RedditPulse.Interfaces;
public interface IStatisticsService
{
    void ProcessPost(RedditPost post);
    SubredditStatistics GetCurrentStatistics();
}
