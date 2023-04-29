namespace dotnet_rpg.Services.CharacterService
{
    public interface IAuthRepository
    {
        Task<ServiceResponse<int>> Register(User user, string password);
        Task<ServiceResponse<string>> Login(string username, string password);
        Task<bool> UserExists(string username); // 1. This method returns true if a user with the given username exists in the database.
    }
}