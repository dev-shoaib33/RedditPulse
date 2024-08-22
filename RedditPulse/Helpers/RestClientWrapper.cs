namespace RedditPulse.Helpers;

public class RestClientWrapper : IRestClientWrapper
{
    private readonly RestClient _client;

    public RestClientWrapper(string baseUrl)
    {
        _client = new RestClient(baseUrl);
    }

    public Task<RestResponse> ExecuteAsync(RestRequest request)
    {
        return _client.ExecuteAsync(request);
    }
}
