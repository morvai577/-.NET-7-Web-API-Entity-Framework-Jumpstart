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

        public async Task<ServiceResponse<int>> Register(User user, string password)
        {
            var response = new ServiceResponse<int>();
            
            if (await UserExists(user.Username))
            {
                response.Success = false;
                response.Message = "User already exists.";
                return response;
            }

            CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt); // 1. This method accepts a password and returns a password hash and salt. The password hash and salt are out parameters. An out parameter is used when a method returns more than one value.
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            response.Data = user.Id;
            return response;
        }

        public async Task<bool> UserExists(string username)
        {
            if (await _context.Users.AnyAsync(user => user.Username.ToLower().Equals(username.ToLower()))) // 2. The Any method returns true if any element in the sequence satisfies the condition in the predicate. In this example, the predicate is a lambda expression that checks if the username of the user matches the username parameter.
            {
                return true;
            }

            return false;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt) // 2. This method accepts a password and returns a password hash and salt. The password hash and salt are out parameters. An out parameter is used when a method returns more than one value.
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512()) // 3. The HMACSHA512 class is used to generate a hash. The using keyword is used to ensure that the HMACSHA512 object is disposed of after use.
            {
                passwordSalt = hmac.Key; // 4. The Key property is used to generate a random key
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password)); // 5. The ComputeHash method is used to generate a hash from the password. The password is converted to a byte array using the UTF8 encoding.
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password)); // 6. The ComputeHash method is used to generate a hash from the password. The password is converted to a byte array using the UTF8 encoding. 
                return computedHash.SequenceEqual(passwordHash); // 7. The SequenceEqual method is used to compare the computed hash to the stored hash. The SequenceEqual method returns true if the two byte arrays are equal.
            }
        }
    }
}