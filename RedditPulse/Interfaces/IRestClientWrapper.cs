namespace RedditPulse.Interfaces;

public interface IRestClientWrapper
{
    Task<RestResponse> ExecuteAsync(RestRequest request);
}
