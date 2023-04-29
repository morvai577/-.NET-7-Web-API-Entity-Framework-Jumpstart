namespace dotnet_rpg.Data
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _context;

        public AuthRepository(DataContext context)
        {
            _context = context;
        }

        public Task<ServiceResponse<string>> Login(string username, string password)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UserExists(string username)
        {
            if (await _context.Users.AnyAsync(user => user.Username.ToLower().Equals(username.ToLower()))) // 2. The Any method returns true if any element in the sequence satisfies the condition in the predicate. In this example, the predicate is a lambda expression that checks if the username of the user matches the username parameter.
            {
                return true;
            }

            return false;
        }

    }
}