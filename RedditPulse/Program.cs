var builder = Host.CreateApplicationBuilder(args);
builder.Configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

var configuration = builder.Configuration;
var clientId = configuration["Reddit:ClientId"];
var clientSecret = configuration["Reddit:ClientSecret"];
var subreddit = configuration["Reddit:Subreddit"];
var baseUrl = configuration["Reddit:BaseUrl"];

builder.Services.AddSingleton<IRedditService, RedditService>();
builder.Services.AddSingleton<IStatisticsService, StatisticsService>();
builder.Services.AddSingleton<IRestClientWrapper>(sp => new RestClientWrapper(baseUrl));
builder.Services.AddSingleton<IRedditAuthService>(sp => new RedditAuthService(clientId, clientSecret, subreddit));
builder.Services.AddSingleton<ILoggerService, ConsoleLoggerService>();

var app = builder.Build();

var redditService = app.Services.GetRequiredService<IRedditService>();
var statisticsService = app.Services.GetRequiredService<IStatisticsService>();

var RedditPost = await redditService.ListenToSubreddit(subreddit, statisticsService);

var timer = new Timer(LogStatistics, statisticsService, 0, 60000);

Console.ReadLine();

static void LogStatistics(object state)
{
    var statisticsService = (IStatisticsService)state;
    var stats = statisticsService.GetCurrentStatistics();

    Console.WriteLine($"Top Post: {stats.TopPost?.Title ?? "N/A"} with {stats.TopPost?.Upvotes ?? 0} upvotes");
    Console.WriteLine($"Top User: {stats.TopUser?.Username ?? "N/A"} with {stats.TopUser?.PostCount ?? 0} posts");
}
