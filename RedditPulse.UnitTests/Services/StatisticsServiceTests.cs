using RedditListener.Services;
using RedditPulse.Models;

namespace RedditPulse.Tests.Services;

public class StatisticsServiceTests
{
    [Fact]
    public void ProcessPost_ShouldUpdateTopPost()
    {
        // Arrange
        var service = new StatisticsService();

        var post1 = new RedditPost { Id = "1", Title = "Post 1", Upvotes = 10, Username = "User1" };
        var post2 = new RedditPost { Id = "2", Title = "Post 2", Upvotes = 20, Username = "User1" };

        // Act
        service.ProcessPost(post1);
        service.ProcessPost(post2);

        var stats = service.GetCurrentStatistics();

        // Assert
        Assert.Equal("Post 2", stats.TopPost.Title);
        Assert.Equal(20, stats.TopPost.Upvotes);
    }

    [Fact]
    public void ProcessPost_ShouldUpdateTopUser()
    {
        // Arrange
        var service = new StatisticsService();

        var post1 = new RedditPost { Id = "1", Title = "Post 1", Upvotes = 10, Username = "User1" };
        var post2 = new RedditPost { Id = "2", Title = "Post 2", Upvotes = 20, Username = "User2" };
        var post3 = new RedditPost { Id = "3", Title = "Post 3", Upvotes = 15, Username = "User1" };

        // Act
        service.ProcessPost(post1);
        service.ProcessPost(post2);
        service.ProcessPost(post3);

        var stats = service.GetCurrentStatistics();

        // Assert
        Assert.Equal("User1", stats.TopUser.Username);
        Assert.Equal(2, stats.TopUser.PostCount);
    }
}