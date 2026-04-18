namespace GoalZone.API.Services.AuthServices
{
    public interface IAuthService
    {
        Task<bool> ValidateUserAsync(string username, string password);
    }
}
