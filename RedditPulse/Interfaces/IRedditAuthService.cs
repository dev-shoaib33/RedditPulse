namespace RedditPulse.Interfaces;
public interface IRedditAuthService
{
    Task<string> EnsureAccessTokenAsync();
}
