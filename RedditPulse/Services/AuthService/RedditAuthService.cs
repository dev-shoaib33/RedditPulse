namespace RedditPulse.Services.AuthService;

public class RedditAuthService : IRedditAuthService
{
    private readonly string _clientId;
    private readonly string _clientSecret;
    private readonly string _subreddit;
    private string _accessToken;

    public RedditAuthService(string clientId, string clientSecret, string subreddit)
    {
        _clientId = clientId;
        _clientSecret = clientSecret;
        _subreddit = subreddit;
    }

    private async Task<string> GetAccessTokenAsync()
    {
        var authClient = new RestClient("https://www.reddit.com");
        var request = new RestRequest("/api/v1/access_token", Method.Post);
        request.AddHeader("Authorization", "Basic " + Convert.ToBase64String(Encoding.UTF8.GetBytes($"{_clientId}:{_clientSecret}")));
        request.AddParameter("grant_type", "client_credentials");

        var response = await authClient.ExecuteAsync(request);

        if (!response.IsSuccessful)
        {
            throw new Exception("Failed to retrieve access token");
        }

        var json = JObject.Parse(response.Content);
        return json["access_token"]?.ToString() ?? throw new Exception("Access token not found");
    }

    public async Task<string> EnsureAccessTokenAsync()
    {
        if (string.IsNullOrEmpty(_accessToken))
        {
            _accessToken = await GetAccessTokenAsync();
        }
        return _accessToken;
    }
}

