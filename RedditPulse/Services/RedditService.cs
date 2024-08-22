namespace RedditListener.Services;

public class RedditService : IRedditService
{
    private readonly ILoggerService _logger;
    private readonly IRedditAuthService _redditAuthService;
    private readonly IRestClientWrapper _client;

    public RedditService(ILoggerService logger, IRedditAuthService redditAuthService, IRestClientWrapper client)
    {
        _logger = logger;
        _redditAuthService = redditAuthService;
        _client = client;
    }

    public async Task<IEnumerable<RedditPost>> ListenToSubreddit(string subredditName, IStatisticsService statisticsService)
    {
        _logger.Log($"Listening to subreddit: {subredditName}");

        var accessToken = await _redditAuthService.EnsureAccessTokenAsync();

        var request = new RestRequest($"/r/{subredditName}/new.json", Method.Get);
        request.AddHeader("Authorization", $"Bearer {accessToken}");

        var response = await _client.ExecuteAsync(request);

        if (response.StatusCode != HttpStatusCode.OK || response.Content == null)
        {
            throw new Exception("Failed to retrieve posts");
        }

        var posts = new List<RedditPost>();
        var json = JObject.Parse(response.Content);
        var items = json["data"]?["children"];

        if (items != null)
        {
            foreach (var item in items)
            {
                var data = item["data"];
                var post = new RedditPost
                {
                    Id = data?["id"]?.ToString(),
                    Username = data?["author"]?.ToString(),
                    Title = data?["title"]?.ToString(),
                    Upvotes = data?["ups"]?.ToObject<int>() ?? 0
                };

                posts.Add(post);
                statisticsService.ProcessPost(post);
            }
        }

        return posts;
    }
}
