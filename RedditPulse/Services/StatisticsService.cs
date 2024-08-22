namespace RedditListener.Services;
public class StatisticsService : IStatisticsService
{
    private readonly ConcurrentDictionary<string, RedditUser> _userPosts;
    private RedditPost _topPost;

    public StatisticsService()
    {
        _userPosts = new ConcurrentDictionary<string, RedditUser>();
        _topPost = null;
    }

    public void ProcessPost(RedditPost post)
    {
        if (_topPost == null || post.Upvotes > _topPost.Upvotes)
        {
            _topPost = post;
        }

        _userPosts.AddOrUpdate(post.Username,
            new RedditUser { Username = post.Username, PostCount = 1 },
            (key, user) => { user.PostCount++; return user; });
    }

    public SubredditStatistics GetCurrentStatistics()
    {
        var topUser = _userPosts.Values.OrderByDescending(u => u.PostCount).FirstOrDefault();
        return new SubredditStatistics
        {
            TopPost = _topPost,
            TopUser = topUser
        };
    }
}
