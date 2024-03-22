namespace LingoLabs.App.Contracts
{
    public interface ITokenService
    {
        Task<string> GetTokenAsync();
        Task RemoveTokenAsync();
        Task SetTokenAsync(string token);
    }
}
