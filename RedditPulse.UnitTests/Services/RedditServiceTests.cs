namespace RedditPulse.Tests.Services;

public class RedditServiceTests
{
    [Fact]
    public async Task ListenToSubreddit_ShouldReturnPostsAndProcessThem()
    {
        var mockLogger = new Mock<ILoggerService>();
        var mockAuthService = new Mock<IRedditAuthService>();
        var mockStatisticsService = new Mock<IStatisticsService>();
        var mockClient = new Mock<IRestClientWrapper>();

        mockAuthService.Setup(auth => auth.EnsureAccessTokenAsync()).ReturnsAsync("mock-access-token");

        var mockRestResponse = new RestResponse
        {
            StatusCode = System.Net.HttpStatusCode.OK,
            Content = "{ \"data\": { \"children\": [ { \"data\": { \"id\": \"1\", \"author\": \"user1\", \"title\": \"Post 1\", \"ups\": 10 } } ] } }"
        };

        mockClient.Setup(client => client.ExecuteAsync(It.Is<RestRequest>(r =>
            r.Resource == "/r/test-subreddit/new.json" &&
            r.Method == Method.Get)))
            .ReturnsAsync(mockRestResponse);

        var redditService = new RedditService(mockLogger.Object, mockAuthService.Object, mockClient.Object);

        var posts = await redditService.ListenToSubreddit("test-subreddit", mockStatisticsService.Object);

        Assert.Single(posts);
        mockStatisticsService.Verify(stat => stat.ProcessPost(It.IsAny<RedditPost>()), Times.Once);
        mockLogger.Verify(logger => logger.Log(It.IsAny<string>()), Times.AtLeastOnce);
    }


    [Fact]
    public async Task ListenToSubreddit_ShouldLogErrorOnFailure()
    {
        var mockLogger = new Mock<ILoggerService>();
        var mockAuthService = new Mock<IRedditAuthService>();
        var mockStatisticsService = new Mock<IStatisticsService>();
        var mockClient = new Mock<IRestClientWrapper>();

        mockAuthService.Setup(auth => auth.EnsureAccessTokenAsync()).ReturnsAsync("mock-access-token");

        var mockRestResponse = new RestResponse
        {
            StatusCode = System.Net.HttpStatusCode.BadRequest,
            Content = "Bad Request"
        };

        mockClient.Setup(client => client.ExecuteAsync(It.IsAny<RestRequest>()))
                  .ReturnsAsync(mockRestResponse);

        var redditService = new RedditService(mockLogger.Object, mockAuthService.Object, mockClient.Object);

        await Assert.ThrowsAsync<Exception>(async () => await redditService.ListenToSubreddit("test-subreddit", mockStatisticsService.Object));
        mockLogger.Verify(logger => logger.Log(It.IsAny<string>()), Times.AtLeastOnce);
    }
}
