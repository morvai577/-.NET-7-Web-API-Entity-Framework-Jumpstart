namespace dotnet_rpg.Data
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _context;

        public AuthRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<string>> Login(string username, string password)
        {
            var response = new ServiceResponse<string>();
            var user = await _context.Users.FirstOrDefaultAsync(user => user.Username.ToLower().Equals(username.ToLower())); // The FirstOrDefaultAsync method returns the first element of a sequence that satisfies a specified condition or a default value if no such element is found. In this example, the condition is a lambda expression that checks if the username of the user matches the username parameter.
            if (user is null)
            {
                response.Success = false;
                response.Message = "User not found.";
            }

            else if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt)) // 3. This method accepts a password, a password hash, and a password salt. It returns true if the password is correct and false if it is not.
            {
                response.Success = false;
                response.Message = "Incorrect password.";
            }

            else
            {
                response.Data = CreateToken(user);
            }

            return response;
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

        /// <summary>
        /// The CreateToken method accepts a User object and returns a JWT token. The token contains a list of claims. A claim is a statement about a user. The claims in the token are used to identify the user and to control access to resources. In this example, the claim contains the user's Id and username.
        /// </summary>
        private string CreateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username)
            };

            var key = DotNetEnv.Env.GetString("TOKEN");

            if(key is null || key == string.Empty)
            {
                throw new Exception("Token not found.");
            }

            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(key)); // The SymmetricSecurityKey class is used to specify the security key that is used to sign the token.
            SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512Signature); // The SigningCredentials class is used to specify the algorithm that is used to sign the token.

            // The SecurityTokenDescriptor class is used to describe the token that will be created. The Subject, Expires, and SigningCredentials properties are used to set the claims, expiration date, and security key for the token.
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims), // The Subject property is used to set the claims for the token.
                Expires = DateTime.UtcNow.AddDays(7), // The Expires property is used to set the expiration date for the token.
                SigningCredentials = credentials // The SigningCredentials property is used to set the security key and algorithm that are used to sign the token.
            };

            // The JwtSecurityTokenHandler class is used to create and validate JWT tokens.
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor); // The CreateToken method is used to create the token.

            return tokenHandler.WriteToken(token); // The WriteToken method is used to write the token to a string.
        }
    }
}